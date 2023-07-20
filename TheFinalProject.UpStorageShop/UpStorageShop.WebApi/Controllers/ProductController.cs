using Application.Features.Products.Commands.Add;
using Application.Features.Products.Queries.GetProductsByOrderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    [Authorize]
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
