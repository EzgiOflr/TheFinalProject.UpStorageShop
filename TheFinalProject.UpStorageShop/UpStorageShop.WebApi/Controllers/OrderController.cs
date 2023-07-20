using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Queries.GetOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    [Authorize]
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

        [HttpPost("DeleteOrder")]
        public async Task<IActionResult> DeleteOrderAsync(OrderDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}