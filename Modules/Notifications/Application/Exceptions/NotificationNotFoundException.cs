
namespace MiniECommerce.Modules.Notifications.Application.Exceptions
{
    public class NotificationNotFoundException : Exception
    {
        public NotificationNotFoundException()
            : base("Notification not found")
        {
            
        }
    }
}
