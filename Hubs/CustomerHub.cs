using Microsoft.AspNetCore.SignalR;

namespace CustomersRepo.Hubs
{
    public class CustomerHub : Hub
    {
        private static readonly Dictionary<string, string> UserConnections = new();

        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
            {
                UserConnections[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
            {
                UserConnections.Remove(userId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendUpdateToAllExcept(string excludedUserId)
        {
            foreach (var userId in UserConnections.Keys)
            {
                if (userId != excludedUserId)
                {
                    await Clients.Client(UserConnections[userId]).SendAsync("ReceiveCustomerUpdate");
                }
            }
        }
    }
}
