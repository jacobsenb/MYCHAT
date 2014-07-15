using Microsoft.AspNet.SignalR;

namespace MyChat.Controllers
{
    public class ChatHub : Hub
    {
        public void Echo(string message)
        {
            Clients.All.echo(message);
        }
    }
}