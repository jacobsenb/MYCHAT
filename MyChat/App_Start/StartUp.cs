using Microsoft.Owin;
using Owin;

namespace MyChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}