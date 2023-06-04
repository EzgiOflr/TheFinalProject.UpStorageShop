using Application.Interfaces;
using Domain.Common;
using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Commands.Add
{
    public class OrderEventAddCommandHandler : IRequestHandler<OrderEventAddCommand, Response<Guid>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public OrderEventAddCommandHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<Response<Guid>> Handle(OrderEventAddCommand command, CancellationToken cancellationToken)
        {
            var OrderEvent = new Domain.Entities.OrderEvent()
            {
                CreatedOn = DateTimeOffset.Now,
                OrderId = command.OrderId,
                Status = command.Status,
            };

            await _upStorageShopDbContext.OrderEvent.AddAsync(OrderEvent, cancellationToken);

            var numberOfInserted = await _upStorageShopDbContext.SaveChangesAsync(cancellationToken);

            if (numberOfInserted > 0)
                return new Response<Guid>($"OrderEvent saved to the database.", OrderEvent.Id);

            return new Response<Guid>($"An error occured.");
        }
    }
}
