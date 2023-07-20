using MediatR;

namespace Application.Features.OrderEvents.Queries.GetOrderEventsByOrderId
{
    public class GetOrderEventsByOrderIdQuery : IRequest<List<GetOrderEventsByOrderIdDto>>
    {
        public GetOrderEventsByOrderIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
