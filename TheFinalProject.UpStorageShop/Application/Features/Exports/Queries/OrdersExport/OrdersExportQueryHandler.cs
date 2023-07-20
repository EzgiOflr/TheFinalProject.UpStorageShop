using Application.Common.Helpers;
using Application.Common.Interfaces;
using ClosedXML.Excel;
using Domain.Common;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Application.Features.Exports.Queries.OrdersExport
{
    public class OrdersExportQueryHandler : IRequestHandler<OrdersExportQuery, Response<string>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersExportQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, IHostingEnvironment env, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _env = env;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(OrdersExportQuery query, CancellationToken cancellationToken)
        {
            var userId = (Guid)_httpContextAccessor.HttpContext.Items["User"];

            var user = await _userManager.FindByIdAsync(userId.ToString());

            var orders = await _upStorageShopDbContext.Order.Include(x => x.OrderEvent).Include(x => x.Product).Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).ToListAsync();

            //response modeli react icin düzenledim
            var response = orders.Select(x => new OrdersExportDto()
            {
                CrawledType = x.ProductCrawlType.ToString(),
                Date = x.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                OrderId = x.Id,
                RequestedCount = x.RequestedAmount,
                TotalFoundAmount = x.TotalFoundAmount
            }).ToList();

            var dataTable = ExcelHelpers.ConvertToDataTable(response);

            var exportPath = Path.Combine(_env.WebRootPath, "Export");

            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            var fileName = Guid.NewGuid().ToString() + ".xlsx";

            exportPath = Path.Combine(exportPath, fileName);

            ExcelHelpers.ExportDataTableToExcel(dataTable, exportPath);
            //MailHelpers.SendMail(exportPath, "Order Export Excel", "Orders", user.Email);

            return new Response<string>(fileName);
        }
    }
}
