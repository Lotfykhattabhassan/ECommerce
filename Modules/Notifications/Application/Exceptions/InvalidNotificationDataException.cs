
namespace MiniECommerce.Modules.Notifications.Application.Exceptions
{
    public class InvalidNotificationDataException : Exception
    {
        public InvalidNotificationDataException() : base("this notification Data is not valid")
        {
            
        }
    }
}
