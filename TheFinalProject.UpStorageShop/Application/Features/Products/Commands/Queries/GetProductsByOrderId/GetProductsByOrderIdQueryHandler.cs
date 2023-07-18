using Application.Common.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Queries.GetProductsByOrderId
{
    public class GetProductsByOrderIdQueryHandler : IRequestHandler<GetProductsByOrderIdQuery, List<GetProductsByOrderIdDto>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;

        public GetProductsByOrderIdQueryHandler(IUpStorageShopDbContext upStorageShopDbContext)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
        }

        public async Task<List<GetProductsByOrderIdDto>> Handle(GetProductsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _upStorageShopDbContext.Product.Where(x => x.OrderId == request.OrderId);

            var products = await dbQuery.ToListAsync(cancellationToken);
            var productDtos = MapProductsToGettAllDtos(products);
            return productDtos.ToList();
        }

        private IEnumerable<GetProductsByOrderIdDto> MapProductsToGettAllDtos(List<Product> products)
        {
            foreach (var product in products)
            {
                yield return new GetProductsByOrderIdDto()
                {
                    OrderId = product.OrderId,
                    Date = product.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    IsOnSale = product.IsOnSale ? "Yes" : "No",
                    Name = product.Name,
                    Picture = product.Picture,
                    Price = product.Price,
                    SalePrice = product.SalePrice
                };
            }
        }
    }
}
