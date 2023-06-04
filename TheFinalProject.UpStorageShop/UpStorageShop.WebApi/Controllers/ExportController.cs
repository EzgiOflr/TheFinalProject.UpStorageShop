using ClosedXML.Excel;
using Domain.Dtos;
using Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using Application.Features.Orders.Commands.Queries.GetOrders;
using MediatR;
using Application.Features.Exports.Queries.OrdersExport;
using Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport;
using Application.Features.Exports.Queries.GetProductsByOrderIdExport;

namespace UpStorageShop.WebApi.Controllers
{
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

