using Domain.Common;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }
    }
}
