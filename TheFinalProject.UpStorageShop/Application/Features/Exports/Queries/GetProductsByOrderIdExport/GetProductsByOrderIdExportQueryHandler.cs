using Application.Common.Helpers;
using Application.Common.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace Application.Features.Exports.Queries.GetProductsByOrderIdExport
{
    public class GetProductsByOrderIdExportQueryHandler : IRequestHandler<GetProductsByOrderIdExportQuery, Response<string>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public GetProductsByOrderIdExportQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, IHostingEnvironment env, UserManager<User> userManager)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _env = env;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(GetProductsByOrderIdExportQuery query, CancellationToken cancellationToken)
        {
            var order = await _upStorageShopDbContext.Order.FirstOrDefaultAsync(x => x.Id == query.OrderId);

            var user = await _userManager.FindByIdAsync(order.UserId.ToString());

            if (!user.IsMailAllowed)
            {
                return new Response<string>($"User not allowed to send mail.");
            }

            var products = _upStorageShopDbContext.Product.Where(x => x.OrderId == query.OrderId).ToList();

            var response = products.Select(x => new GetProductsByOrderIdExportDto()
            {
                OrderId = x.OrderId,
                Date = x.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                IsOnSale = x.IsOnSale ? "Yes" : "No",
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price,
                SalePrice = x.SalePrice

            }).ToList();

            var dataTable = ExcelHelpers.ConvertToDataTable(response);

            var exportPath = Path.Combine(_env.WebRootPath, "Export");

            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            var fileName = Guid.NewGuid().ToString() + ".xlsx";

            exportPath = Path.Combine(exportPath, fileName);

            ExcelHelpers.ExportDataTableToExcel(dataTable, exportPath);
            MailHelpers.SendMail(exportPath, "Products Export Excel", "Products", user.Email);

            return new Response<string>($"Mail sent successfuly.");
        }
    }
}
