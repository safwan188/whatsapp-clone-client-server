using Microsoft.AspNetCore.SignalR;
namespace WebApplication6.Hubs
   
{
    public class MyHub : Hub
    {
        public async Task changed(string value)
        {
            await Clients.All.SendAsync("ChangeReceived",value);
        }
    }
}
