using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpStorageShop.Console.Enums;

namespace UpStorageShop.Console.Common
{
    public class Order
    {
        public Guid Id { get; set; }
        public int TotalFoundAmount { get; set; }
        public ProductCrawlType ProductCrawlType { get; set; }
        public int RequestedAmount { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public ICollection<OrderEvent> OrderEvents { get; set; } //BotStarted

    }
}
