using Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport;
using Application.Features.Exports.Queries.GetProductsByOrderIdExport;
using Application.Features.Exports.Queries.OrdersExport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    [Authorize]
    public class ExportController : ApiControllerBase
    {
        [HttpPost("OrdersExport")]
        public async Task<IActionResult> OrdersExportAsync(OrdersExportQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("GetOrderEventsByOrderIdExport")]
        public async Task<IActionResult> GetOrderEventsByOrderIdExportAsync(GetOrderEventsByOrderIdExportQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("GetProductsByOrderIdExport")]
        public async Task<IActionResult> GetProductsByOrderIdExportAsync(GetProductsByOrderIdExportQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}

