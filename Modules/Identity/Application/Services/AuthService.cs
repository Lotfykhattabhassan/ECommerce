using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;
using MiniECommerce.Modules.Identity.Application.DTOs.User;
using MiniECommerce.Modules.Identity.Application.Exceptions;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;
using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthenticationOptions _authenticationOptions;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenHasher _refreshTokenHasher;

        private readonly IValidator<RegisterUserDto> _validator;
        private readonly IValidator<LoginDto> _validatorLogin;
        private readonly IValidator<ChangePasswordDto> _validatorPass;

        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public AuthService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IUserCredentialRepository userCredentialRepository,
            IUserRoleRepository userRoleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IValidator<RegisterUserDto> validator,
            IValidator<LoginDto> validatorLogin,
            IPasswordHasher passwordHasher,
            IValidator<ChangePasswordDto> validatorPass,
            IMapper mapper,
            ICurrentUser currentUser,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IAuthenticationOptions authenticationOptions,
            IRefreshTokenHasher refreshTokenHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userCredentialRepository = userCredentialRepository;
            _userRoleRepository = userRoleRepository;
            _refreshTokenRepository = refreshTokenRepository;

            _validator = validator;
            _validatorLogin = validatorLogin;
            _passwordHasher = passwordHasher;
            _validatorPass = validatorPass;

            _mapper = mapper;
            _currentUser = currentUser;
            _jwtTokenGenerator = jwtTokenGenerator;

            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenHasher = refreshTokenHasher;

            _authenticationOptions = authenticationOptions;
        }

        public async Task<Guid> RegisterUser(
            RegisterUserDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new RegisterDataNotValidException();

            var user = User.Create(
                dto.FirstName,
                dto.LastName,
                dto.Email);

            await _userRepository.AddAsync(
                user,
                cancellationToken);

            var hashedPassword = _passwordHasher.Hash(
                dto.Password);

            var userCredential = UserCredential.Create(
                user.Id,
                hashedPassword);

            await _userCredentialRepository.AddAsync(
                userCredential,
                cancellationToken);

            var userRole = UserRole.Create(
                user.Id,
                dto.RoleId);

            await _userRoleRepository.AddAsync(
                userRole,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return user.Id;
        }

        public async Task<JwtTokenResult> Login(
            LoginDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorLogin.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new UnauthorizedUserLoginException();

            var user = await _userRepository.GetUserByEmailAsync(
                dto.Email,
                cancellationToken);

            if (user == null)
                throw new UnauthorizedUserLoginException();

            user.Credential.CanLogin();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            var validatePassword = _passwordHasher.Verify(
                dto.Password,
                user.Credential.PasswordHash);

            if (!validatePassword)
            {
                user.Credential.FailedLogin();

                await _unitOfWork.SaveChangesAsync(
                    cancellationToken);

                throw new UnauthorizedUserLoginException();
            }

            var roles = user.Roles
                .Select(x => x.Role.RoleName)
                .ToList();

            var token = _jwtTokenGenerator.Generate(
                user.Id,
                user.Email,
                roles);

            var rawRefreshToken =
                _refreshTokenGenerator.Generate();

            var refreshTokenHash =
                _refreshTokenHasher.Hash(
                    rawRefreshToken);

            var tokenFamilyId = Guid.NewGuid();

            var refreshTokenExpiresAt =
                DateTime.UtcNow.AddDays(
                    _authenticationOptions.RefreshTokenLifetimeDays);

            var refreshToken = RefreshToken.Create(
                user.Id,
                refreshTokenHash,
                tokenFamilyId,
                refreshTokenExpiresAt);

            await _refreshTokenRepository.AddAsync(
                refreshToken,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new JwtTokenResult(
                token.accessToken,
                rawRefreshToken,
                token.accessTokenExpiresAt,
                refreshTokenExpiresAt);
        }

        public async Task<JwtTokenResult> RefreshAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = _refreshTokenHasher.Hash(
                request.RefreshToken);

            var refreshToken =
                await _refreshTokenRepository.GetByTokenHashAsync(
                    tokenHash,
                    cancellationToken);

            if (refreshToken is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            if (refreshToken.IsRevoked)
            {
                await _unitOfWork.BeginTransactionAsync(
                    cancellationToken);

                try
                {
                    await _refreshTokenRepository.RevokeTokenFamilyAsync(
                        refreshToken.TokenFamilyId,
                        cancellationToken);

                    await _unitOfWork.SaveChangesAsync(
                        cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(
                        cancellationToken);
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync(
                        cancellationToken);

                    throw;
                }

                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            if (refreshToken.IsExpired)
            {
                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            var user = await _userRepository.GetByIdAsync(
                refreshToken.UserId,
                cancellationToken);

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }

            var roles = user.Roles
                .Select(x => x.Role.RoleName)
                .ToList();

            var token = _jwtTokenGenerator.Generate(
                user.Id,
                user.Email,
                roles);

            await _unitOfWork.BeginTransactionAsync(
                cancellationToken);

            try
            {
                var newRawRefreshToken =
                    _refreshTokenGenerator.Generate();

                var newRefreshTokenHash =
                    _refreshTokenHasher.Hash(
                        newRawRefreshToken);

                var refreshTokenExpiresAt =
                    DateTime.UtcNow.AddDays(
                        _authenticationOptions.RefreshTokenLifetimeDays);

                var newRefreshToken = RefreshToken.Create(
                    user.Id,
                    newRefreshTokenHash,
                    refreshToken.TokenFamilyId,
                    refreshTokenExpiresAt);

                refreshToken.Revoke(
                    newRefreshToken.Id);

                await _refreshTokenRepository.AddAsync(
                    newRefreshToken,
                    cancellationToken);

                await _unitOfWork.SaveChangesAsync(
                    cancellationToken);

                await _unitOfWork.CommitTransactionAsync(
                    cancellationToken);

                return new JwtTokenResult(
                    token.accessToken,
                    newRawRefreshToken,
                    token.accessTokenExpiresAt,
                    refreshTokenExpiresAt);
            }
            catch (DbUpdateConcurrencyException)
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                throw new UnauthorizedAccessException(
                    "Invalid refresh token.");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(
                    cancellationToken);

                throw;
            }
        }

        public async Task LogoutAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = _refreshTokenHasher.Hash(
                request.RefreshToken);

            var refreshToken =
                await _refreshTokenRepository.GetByTokenHashAsync(
                    tokenHash,
                    cancellationToken);

            if (refreshToken is null)
                return;

            if (refreshToken.IsRevoked)
                return;

            refreshToken.Revoke();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<ChangePasswordResponseDto> ChangeUserPassword(
            ChangePasswordDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorPass.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new ArgumentException();

            var userId = _currentUser.UserId;

            if (userId is null)
                throw new UnauthorizedUserLoginException();

            var user = await _userRepository.GetByIdAsync(
                userId.Value,
                cancellationToken);

            if (user == null)
                throw new UserNotFoundException();

            var hashedPassword = _passwordHasher.Hash(
                dto.NewPassword);

            user.Credential.ChangePassword(
                hashedPassword);

            await _refreshTokenRepository.RevokeAllForUserAsync(
                userId.Value,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new ChangePasswordResponseDto
            {
                Message = "Password Changed Successfully"
            };
        }

        public async Task<UserReadDto?> GetMe(
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;

            if (userId is null)
                throw new UnauthorizedUserLoginException();

            var user = await _userRepository.GetByIdAsync(
                userId.Value,
                cancellationToken);

            if (user == null)
                throw new UserNotFoundException();

            return _mapper.Map<UserReadDto>(user);
        }
    }
}