using Listing.Domain.DomainModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationListings.Hubs
{
    public class ChatHub : Hub
    {
     /*   public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync("recieveMessage", message);*/

        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("receiveMessage", message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }
    }
}
