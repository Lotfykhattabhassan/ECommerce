using AutoMapper;
using FluentValidation;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Application.DTOs.User;
using MiniECommerce.Modules.Identity.Application.DTOs.UserRoleDto;
using MiniECommerce.Modules.Identity.Application.Exceptions;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;
using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<CreateUserDto> _validator;
        private readonly IValidator<UpdateUserDto> _validatorUpdate;

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRole;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRoleRepository userRole,
            IValidator<CreateUserDto> validator,
            IValidator<UpdateUserDto> validatorUpdate)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRole = userRole;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }

        public async Task<Guid> CreateUserAsync(
            CreateUserDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new InvalidUserDataException();

            var user = User.Create(
                dto.firstName,
                dto.lastName,
                dto.email);

            await _userRepository.AddAsync(
                user,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return user.Id;
        }

        public async Task<UserReadDto?> GetUserById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidUserDataException();

            var user = await _userRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (user == null)
                throw new UserNotFoundException();

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<DeleteUserResponseDto> DeleteUser(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidUserDataException();

            var user = await _userRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (user == null)
                throw new UserNotFoundException();

            _userRepository.Delete(user);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new DeleteUserResponseDto
            {
                Message = "User Deleted Successfully"
            };
        }

        public async Task<IReadOnlyList<UserReadDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllAsync(
                cancellationToken);

            return _mapper.Map<List<UserReadDto>>(users);
        }

        public async Task<IReadOnlyList<UserRoleReadDto>> GetUserRoles(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new InvalidUserDataException();

            var userRoles = await _userRole.GetByUserIdAsync(
                userId,
                cancellationToken);

            return _mapper.Map<List<UserRoleReadDto>>(
                userRoles);
        }

        public async Task<UpdateUserResponseDto> UpdateUserName(
            UpdateUserDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorUpdate.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new InvalidUserDataException();

            var userToUpdate = await _userRepository.GetByIdAsync(
                dto.Id,
                cancellationToken);

            if (userToUpdate == null)
                throw new UserNotFoundException();

            userToUpdate.ChangeUserName(
                dto.FirstName,
                dto.LastName);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new UpdateUserResponseDto
            {
                Message = "User Updated Successfully"
            };
        }
    }
}

