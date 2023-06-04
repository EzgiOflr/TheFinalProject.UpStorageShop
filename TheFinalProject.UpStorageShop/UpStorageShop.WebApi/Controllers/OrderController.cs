using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.Queries.GetOrders;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    public class OrderController : ApiControllerBase
    {
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> AddAsync(OrderAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync(GetOrdersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}