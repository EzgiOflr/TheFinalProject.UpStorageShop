using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Response<Guid>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public ProductAddCommandHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<Response<Guid>> Handle(ProductAddCommand command, CancellationToken cancellationToken)
        {
            var Product = command.Products.Select(x => new Product()
            {
                CreatedOn = DateTimeOffset.Now,
                IsOnSale = x.IsOnSale,
                Name = x.Name,
                OrderId = x.OrderId,
                Picture = x.Picture,
                Price = x.Price,
                SalePrice = x.SalePrice
            });

            await _upStorageShopDbContext.Product.AddRangeAsync(Product, cancellationToken);

            var order = await _upStorageShopDbContext.Order.FirstOrDefaultAsync(x => x.Id == command.OrderId);

            if (order != null)
                order.TotalFoundAmount = Product.Count();

            var numberOfInserted = await _upStorageShopDbContext.SaveChangesAsync(cancellationToken);

            if (numberOfInserted > 0)
                return new Response<Guid>($"{Product.Count()} products saved to the database.");

            return new Response<Guid>($"An error occured.");
        }
    }
}