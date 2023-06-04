using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport
{
    public class GetOrderEventsByOrderIdExportQuery : IRequest<Response<string>>
    {
        public GetOrderEventsByOrderIdExportQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
