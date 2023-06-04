using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public ProductCrawlType ProductCrawlType { get; set; }
        public int RequestedAmount { get; set; }
        public ICollection<OrderEvent> OrderEvent { get; set; } //BotStarted
        public ICollection<Product> Product { get; set; } //BotStarted
        public int TotalFoundAmount { get; set; }
    }
}
