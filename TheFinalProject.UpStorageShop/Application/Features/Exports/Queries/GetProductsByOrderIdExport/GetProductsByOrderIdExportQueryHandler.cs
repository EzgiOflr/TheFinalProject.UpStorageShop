using Application.Common.Helpers;
using Application.Interfaces;
using ClosedXML.Excel;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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

        public GetProductsByOrderIdExportQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, IHostingEnvironment env)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _env = env;
        }

        public async Task<Response<string>> Handle(GetProductsByOrderIdExportQuery query, CancellationToken cancellationToken)
        {
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
            MailHelpers.SendMail(exportPath, "Products Export Excel", "Products");

            return new Response<string>($"Mail sent successfuly.");
        }
    }
}
