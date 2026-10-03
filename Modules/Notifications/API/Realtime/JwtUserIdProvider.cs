using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MiniECommerce.Modules.Notifications.API.Realtime
{
    public class JwtUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?
                .FindFirst(JwtRegisteredClaimNames.Sub)?
                .Value;
        }
    }
}
