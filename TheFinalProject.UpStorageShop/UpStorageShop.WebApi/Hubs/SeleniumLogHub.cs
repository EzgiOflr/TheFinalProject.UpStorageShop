using Domain.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace UpStorageShop.WebApi.Hubs
{
    public class SeleniumLogHub : Hub
    {
        public async Task SendLogNotificationAsync(SeleniumLogDto log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", log);
        }
    }
}
