using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersDto
    {
        public Guid OrderId { get; set; }
        public int RequestedCount { get; set; }
        public int TotalFoundAmount { get; set; }
        public string CrawledType { get; set; }
        public string Date { get; set; }
    }
}
