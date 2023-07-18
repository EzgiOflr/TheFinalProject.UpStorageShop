using Application.Common.Models;
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

        public async Task CrawlerTriggerAsync(CrawlerTriggerDto crawlerTriggerDto)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReactDataReceived", crawlerTriggerDto);
        }
    }
}