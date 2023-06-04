using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exports.Queries.GetProductsByOrderIdExport
{
    public class GetProductsByOrderIdExportQuery : IRequest<Response<string>>
    {
        public GetProductsByOrderIdExportQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
