using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exports.Queries.GetProductsByOrderIdExport
{
    public class GetProductsByOrderIdExportDto
    {
        public Guid OrderId { get; set; }
        public string Date { get; set; }
        public string IsOnSale { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
    }
}
