
namespace MiniECommerce.Modules.Identity.Application.DTOs.Auth
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; private set; } = 1;

    }
}
