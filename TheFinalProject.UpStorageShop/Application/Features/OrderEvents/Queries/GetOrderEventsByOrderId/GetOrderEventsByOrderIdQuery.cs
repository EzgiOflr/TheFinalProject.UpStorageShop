using Application.Features.Products.Commands.Queries.GetProductsByOrderId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
