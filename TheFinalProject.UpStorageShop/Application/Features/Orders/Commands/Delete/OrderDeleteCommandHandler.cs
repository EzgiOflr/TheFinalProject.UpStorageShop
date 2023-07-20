using Application.Common.Interfaces;
using Application.Features.Orders.Commands.Add;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Delete
{
    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, Response<bool>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public OrderDeleteCommandHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<Response<bool>> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var order = await _upStorageShopDbContext.Order.FirstOrDefaultAsync(x => x.Id == command.OrderId, cancellationToken);

            if (order == null)
            {
                return new Response<bool>($"Order not found.", false);
            }

            var orderEvents = await _upStorageShopDbContext.OrderEvent.Where(x => x.OrderId == command.OrderId).ToListAsync(cancellationToken);
            var orderProduts = await _upStorageShopDbContext.Product.Where(x => x.OrderId == command.OrderId).ToListAsync(cancellationToken);

            _upStorageShopDbContext.Order.Remove(order);
            _upStorageShopDbContext.OrderEvent.RemoveRange(orderEvents);
            _upStorageShopDbContext.Product.RemoveRange(orderProduts);

            var numberOfDeleted = await _upStorageShopDbContext.SaveChangesAsync(cancellationToken);

            if (numberOfDeleted > 0)
                return new Response<bool>($"Order deleted from the database.", true);

            return new Response<bool>($"An error occured.");
        }
    }
}