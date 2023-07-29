using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new();

        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, username);
            // This code will send a message to the specified channel with the message "User Connected!"
            await AddMessageToChat(string.Empty,$"{username} joined the party!");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"{username} left!");
           
        }

        /// <summary> 
        /// Sends  messages to all connected clients.
        /// </summary>
        /// <param name="User">The user sending the message.</param>
        /// <param name="message">The message to be sent.</param>
        /// <returns>
        /// An asynchronous task that represents the sending of the message.
        /// </returns>
        public async Task AddMessageToChat(string User, string message)
        {
            await Clients.All.SendAsync("GetThatMessageDude", User, message);
        }
    }
}
