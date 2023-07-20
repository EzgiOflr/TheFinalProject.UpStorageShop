using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<GetOrdersDto>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetOrdersQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetOrdersDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var userId = (Guid)_httpContextAccessor.HttpContext.Items["User"];

            var dbQuery = _upStorageShopDbContext.Order.Include(x => x.OrderEvent).Include(x => x.Product).Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);

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
