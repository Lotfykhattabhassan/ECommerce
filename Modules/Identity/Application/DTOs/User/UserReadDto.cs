
namespace MiniECommerce.Modules.Identity.Application.DTOs.User
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool HasCredentials { get; set; }
        public string Status { get; set; } = null!;
        public List<string> Roles { get; set; } 
            = new List<string>();
    }
}
