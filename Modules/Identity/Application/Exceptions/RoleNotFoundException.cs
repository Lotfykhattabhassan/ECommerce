
namespace MiniECommerce.Modules.Identity.Application.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException() : base("role not found")
        {
            
        }
    }
}
