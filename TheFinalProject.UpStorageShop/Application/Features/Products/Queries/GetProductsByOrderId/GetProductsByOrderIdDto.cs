using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductsByOrderId
{
    public class GetProductsByOrderIdDto
    {
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string IsOnSale { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }

        public string Date { get; set; }
    }
}
