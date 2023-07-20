using Domain.Common;
using Domain.Enums;
using Domain.Identity;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public ProductCrawlType ProductCrawlType { get; set; }
        public int RequestedAmount { get; set; }
        public ICollection<OrderEvent> OrderEvent { get; set; }
        public ICollection<Product> Product { get; set; }
        public Guid UserId { get; set; }
        public int TotalFoundAmount { get; set; }
    }
}
