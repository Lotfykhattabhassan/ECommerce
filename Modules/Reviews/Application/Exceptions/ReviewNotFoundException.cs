
namespace MiniECommerce.Modules.Reviews.Application.Exceptions
{
    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException() : base("Review Not Found")
        {
            
        }
    }
}
