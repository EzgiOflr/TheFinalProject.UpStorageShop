using Application.Features.Orders.Commands.Add;
using Application.Interfaces;
using Domain.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public OrderAddCommandHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<Response<Guid>> Handle(OrderAddCommand command, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order()
            {
                CreatedOn = DateTimeOffset.Now,
                ProductCrawlType = command.ProductCrawlType,
                RequestedAmount = command.RequestedAmount,
                TotalFoundAmount = command.TotalFoundAmount
            };

            await _upStorageShopDbContext.Order.AddAsync(order, cancellationToken);

            var numberOfInserted = await _upStorageShopDbContext.SaveChangesAsync(cancellationToken);

            if (numberOfInserted > 0)
                return new Response<Guid>($"Order saved to the database.", order.Id);

            return new Response<Guid>($"An error occured.");
        }
    }
}