using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpStorageShop.Console.Common
{
    public class Product : EntityBase<Guid>
    {
        public ICollection<Product> Products { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }

    }
}
