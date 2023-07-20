using MediatR;

namespace Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<GetOrdersDto>>
    {
        public GetOrdersQuery()
        {

        }
    }
}
