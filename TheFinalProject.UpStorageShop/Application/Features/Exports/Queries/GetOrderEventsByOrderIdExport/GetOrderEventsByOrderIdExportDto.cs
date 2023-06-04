using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport
{
    public class GetOrderEventsByOrderIdExportDto
    {
        public Guid OrderId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}
