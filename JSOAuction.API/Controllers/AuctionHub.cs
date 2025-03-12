using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace JSOAuction.API.Hubs
{
    // Hub class to manage real-time communication with clients
    public class AuctionHub : Hub
    {
        // Method to send a message to all connected clients
        public async Task SendMessage(object data)
        {
            try
            {
                // Log the data being sent
                Console.WriteLine($"📤 Sending message: {data}");

                // Send the message to all connected clients
                await Clients.All.SendAsync("ReceiveMessage", data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        // Notify when a client is connected
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("messageReceived", "System", "You are connected!");
            Console.WriteLine($"✅ Client Connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        // Handle disconnection of clients
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"⚠️ Client Disconnected: {Context.ConnectionId}, Reason: {exception?.Message}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
