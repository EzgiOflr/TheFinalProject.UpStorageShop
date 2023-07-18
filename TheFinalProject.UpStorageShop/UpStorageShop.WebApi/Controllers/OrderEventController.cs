using Application.Features.OrderEvents.Commands.Add;
using Application.Features.OrderEvents.Queries.GetOrderEventsByOrderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    [Authorize]
    public class OrderEventController : ApiControllerBase
    {
        [HttpPost("CreateOrderEvent")]
        public async Task<IActionResult> AddAsync(OrderEventAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetOrderEventsByOrderId")]
        public async Task<IActionResult> GetOrderEventsByOrderIdAsync(GetOrderEventsByOrderIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
