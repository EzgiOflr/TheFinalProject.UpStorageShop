using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductsByOrderId
{
    public class GetProductsByOrderIdQuery : IRequest<List<GetProductsByOrderIdDto>>
    {
        public GetProductsByOrderIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
