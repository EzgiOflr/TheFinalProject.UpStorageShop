using Application.Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace UpStorageShop.WebApi.Hubs
{
    public class TriggerHub : Hub
    {
        public async Task CrawlerTriggerAsync(CrawlerTriggerDto crawlerTriggerDto)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReactDataReceived", crawlerTriggerDto);
        }
    }
}
