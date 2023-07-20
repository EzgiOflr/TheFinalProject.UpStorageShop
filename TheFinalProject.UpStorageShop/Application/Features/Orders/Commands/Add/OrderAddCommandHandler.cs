using Application.Common.Interfaces;
using Application.Features.Orders.Commands.Add;
using Domain.Common;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderAddCommandHandler(IUpStorageShopDbContext upStorageShopDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<Guid>> Handle(OrderAddCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Domain.Entities.Order()
                {
                    CreatedOn = DateTimeOffset.Now,
                    ProductCrawlType = command.ProductCrawlType,
                    RequestedAmount = command.RequestedAmount,
                    TotalFoundAmount = command.TotalFoundAmount,
                    UserId = (Guid)_httpContextAccessor.HttpContext.Items["User"],
                };

                await _upStorageShopDbContext.Order.AddAsync(order, cancellationToken);

                var numberOfInserted = await _upStorageShopDbContext.SaveChangesAsync(cancellationToken);

                if (numberOfInserted > 0)
                    return new Response<Guid>($"Order saved to the database.", order.Id);

                return new Response<Guid>($"An error occured.");
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}