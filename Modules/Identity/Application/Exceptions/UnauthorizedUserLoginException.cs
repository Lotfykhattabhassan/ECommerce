
namespace MiniECommerce.Modules.Identity.Application.Exceptions
{
    public class UnauthorizedUserLoginException : Exception
    {
        public UnauthorizedUserLoginException() : base("this credentials is not valid")
        {
            
        }
    }
}
