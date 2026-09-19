
namespace MiniECommerce.Modules.Identity.Application.DTOs.Role
{
    public class UpdateRoleDto
    {
        public int RoleId { get; set; }
        public string NewRoleName { get; set; } = string.Empty;
        public string NewDescription { get; set; } = string.Empty;
    }
}
