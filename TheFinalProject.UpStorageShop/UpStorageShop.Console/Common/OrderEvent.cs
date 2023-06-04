using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpStorageShop.Console.Common
{
    public class OrderEvent 

    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
