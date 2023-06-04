using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommand : IRequest<Response<Guid>>
    {
        public ProductCrawlType ProductCrawlType { get; set; }
        public int RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
    }
}