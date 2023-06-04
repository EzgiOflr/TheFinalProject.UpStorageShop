using Application.Features.Products.Commands.Queries.GetProductsByOrderId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<GetOrdersDto>>
    {
        public GetOrdersQuery()
        {

        }
    }
}
