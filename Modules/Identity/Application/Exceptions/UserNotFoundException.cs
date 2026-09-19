
namespace MiniECommerce.Modules.Identity.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User Not Found")
        {
            
        }
        
    }
}
