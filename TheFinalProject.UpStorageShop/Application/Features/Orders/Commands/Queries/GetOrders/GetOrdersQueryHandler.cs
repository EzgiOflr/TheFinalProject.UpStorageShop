using Application.Features.Products.Commands.Queries.GetProductsByOrderId;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<GetOrdersDto>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public GetOrdersQueryHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<List<GetOrdersDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _upStorageShopDbContext.Order.Include(x => x.OrderEvent).Include(x => x.Product).OrderByDescending(x => x.CreatedOn);

            var orders = await dbQuery.ToListAsync(cancellationToken);
            var orderDtos = MapDto(orders);
            return orderDtos.ToList();
        }

        private IEnumerable<GetOrdersDto> MapDto(List<Order> orders)
        {
            foreach (var order in orders)
            {
                yield return new GetOrdersDto()
                {
                    CrawledType = order.ProductCrawlType.ToString(),
                    Date = order.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    OrderId = order.Id,
                    RequestedCount = order.RequestedAmount,
                    TotalFoundAmount = order.TotalFoundAmount
                };
            }
        }
    }
}
