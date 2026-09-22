using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SKINET.Server.NewFolder
{
    [Authorize]
    public class NotificationHub :Hub
    {
        private static readonly ConcurrentDictionary<string, string> userConnections = new();
        public override Task OnConnectedAsync()
        {
            var email = Context.User?.GetEmail();
            if (!string.IsNullOrEmpty(email)) userConnections[email] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var email = Context.User?.GetEmail();
            if (!string.IsNullOrEmpty(email)) userConnections.TryRemove(email,out _);
            return base.OnDisconnectedAsync(exception);
        }
        public static  string? GetconnectionByemail(string email)
        {
            userConnections.TryGetValue(email, out var id);
            return id;
        }
    }
}
