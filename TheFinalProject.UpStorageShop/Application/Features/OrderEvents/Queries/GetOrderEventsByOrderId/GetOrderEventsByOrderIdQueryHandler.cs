using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderEvents.Queries.GetOrderEventsByOrderId
{
    public class GetOrderEventsByOrderIdQueryHandler : IRequestHandler<GetOrderEventsByOrderIdQuery, List<GetOrderEventsByOrderIdDto>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public GetOrderEventsByOrderIdQueryHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<List<GetOrderEventsByOrderIdDto>> Handle(GetOrderEventsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _upStorageShopDbContext.OrderEvent.Where(x => x.OrderId == request.OrderId);

            var OrderEvents = await dbQuery.ToListAsync(cancellationToken);
            var OrderEventDtos = MapDto(OrderEvents);
            return OrderEventDtos.ToList();
        }

        private IEnumerable<GetOrderEventsByOrderIdDto> MapDto(List<OrderEvent> OrderEvents)
        {
            foreach (var OrderEvent in OrderEvents)
            {
                yield return new GetOrderEventsByOrderIdDto()
                {
                    OrderId = OrderEvent.OrderId,
                    Date = OrderEvent.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    Status = OrderEvent.Status.ToString()
                };
            }
        }
    }
}
