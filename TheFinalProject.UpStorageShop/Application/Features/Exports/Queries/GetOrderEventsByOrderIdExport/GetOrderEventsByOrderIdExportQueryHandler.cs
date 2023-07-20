using Application.Common.Helpers;
using Application.Common.Interfaces;
using ClosedXML.Excel;
using Domain.Common;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport
{
    public class GetOrderEventsByOrderIdExportQueryHandler : IRequestHandler<GetOrderEventsByOrderIdExportQuery, Response<string>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public GetOrderEventsByOrderIdExportQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, IHostingEnvironment env, UserManager<User> userManager)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _env = env;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(GetOrderEventsByOrderIdExportQuery query, CancellationToken cancellationToken)
        {
            var order = await _upStorageShopDbContext.Order.FirstOrDefaultAsync(x => x.Id == query.OrderId);

            var user = await _userManager.FindByIdAsync(order.UserId.ToString());

            if (!user.IsMailAllowed)
            {
                return new Response<string>($"User not allowed to send mail.");
            }

            var orderEvents = _upStorageShopDbContext.OrderEvent.Where(x => x.OrderId == query.OrderId).ToList();

            var response = orderEvents.Select(x => new GetOrderEventsByOrderIdExportDto()
            {
                OrderId = x.OrderId,
                Date = x.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                Status = x.Status.ToString()
            }).ToList();

            var dataTable = ExcelHelpers.ConvertToDataTable(response);

            var exportPath = Path.Combine(_env.WebRootPath, "Export");

            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            var fileName = Guid.NewGuid().ToString() + ".xlsx";

            exportPath = Path.Combine(exportPath, fileName);

            ExcelHelpers.ExportDataTableToExcel(dataTable, exportPath);
            MailHelpers.SendMail(exportPath, "Order Event Export Excel", "Order Events", user.Email);

            return new Response<string>($"Mail sent successfuly.");
        }
    }
}
