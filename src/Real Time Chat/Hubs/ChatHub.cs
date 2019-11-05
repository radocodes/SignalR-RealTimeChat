using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Time_Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var user = this.Context.User.Identity.Name;

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
               var user = this.Context.User.Identity.Name;

            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task Join(string chatGroupName)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, chatGroupName);

            var user = this.Context.User.Identity.Name;

            var joinMessage = $"{user} joined the chat room!";

            const string ServerName = "Server";

            await Clients.Group(chatGroupName).SendAsync("ReceiveMessage", ServerName, joinMessage);
        }
    }
}
