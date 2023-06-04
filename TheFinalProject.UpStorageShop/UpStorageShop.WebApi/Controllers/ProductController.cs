using Application.Features.Products.Commands.Add;
using Application.Features.Products.Commands.Queries.GetProductsByOrderId;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpPost("CreateProducts")]
        public async Task<IActionResult> AddAsync(ProductAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetProductsByOrderId")]
        public async Task<IActionResult> GetProductsByOrderIdAsync(GetProductsByOrderIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
