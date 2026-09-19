using AutoMapper;
using FluentValidation;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Application.DTOs.Role;
using MiniECommerce.Modules.Identity.Application.Exceptions;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;
using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateRoleDto> _validator;
        private readonly IValidator<UpdateRoleDto> _validatorUpdate;

        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateRoleDto> validator,
            IMapper mapper,
            IValidator<UpdateRoleDto> validatorUpdate)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _validatorUpdate = validatorUpdate;
        }

        public async Task<int> CreateRoleAsync(
            CreateRoleDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new InvalidRoleDataException();

            var role = Role.Create(
                dto.RoleName,
                dto.Description);

            await _roleRepository.AddAsync(
                role,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return role.Id;
        }

        public async Task<RoleReadDto?> GetRoleById(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidRoleDataException();

            var role = await _roleRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (role == null)
                throw new RoleNotFoundException();

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<IReadOnlyList<RoleReadDto>> GetAllRoles(
            CancellationToken cancellationToken = default)
        {
            var roles = await _roleRepository.GetAllAsync(
                cancellationToken);

            return _mapper.Map<List<RoleReadDto>>(roles);
        }

        public async Task<UpdateRoleResponseDto> UpdateRoleAsync(
            UpdateRoleDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorUpdate.ValidateAsync(
                dto,
                cancellationToken);

            if (!result.IsValid)
                throw new InvalidRoleDataException();

            var role = await _roleRepository.GetByIdAsync(
                dto.RoleId,
                cancellationToken);

            if (role == null)
                throw new RoleNotFoundException();

            role.ChangeRoleName(dto.NewRoleName);
            role.ChangeRoleDescription(dto.NewDescription);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new UpdateRoleResponseDto
            {
                Message = "Role Updated Successfully"
            };
        }

        public async Task<DeleteRoleResponseDto> DeleteRoleAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidRoleDataException();

            var role = await _roleRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (role == null)
                throw new RoleNotFoundException();

            _roleRepository.Delete(role);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new DeleteRoleResponseDto
            {
                Message = "Role Deleted Successfully"
            };
        }
    }
}
