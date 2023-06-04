using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exports.Queries.OrdersExport
{
    public class OrdersExportQuery : IRequest<Response<string>>
    {
        public OrdersExportQuery()
        {

        }
    }
}
