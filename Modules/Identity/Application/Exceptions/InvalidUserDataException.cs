namespace MiniECommerce.Modules.Identity.Application.Exceptions
{
    public class InvalidUserDataException : Exception
    {
        public InvalidUserDataException() : base("this is invalid user data")
        {
            
        }
    }
}
