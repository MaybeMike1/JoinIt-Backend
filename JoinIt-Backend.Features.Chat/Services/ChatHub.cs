using JoinIt_Backend.Shared.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace JoinIt_Backend.Features.Chat.Services
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly DatabaseContext _databaseContext;

        public ChatHub(ILogger<ChatHub> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public async Task SendMessage(string user, string message, string room)
        {
            await Clients.All.SendAsync(user, message, room);
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public override Task OnConnectedAsync()        
        {
            _logger.LogInformation($"Client has successfully connected to Chat {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
    }
}
