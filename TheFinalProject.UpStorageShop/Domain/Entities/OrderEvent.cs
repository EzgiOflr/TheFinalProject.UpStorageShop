using Domain.Common;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class OrderEvent : EntityBase
    {

        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        public OrderStatusEnum Status { get; set; }
    }
}
