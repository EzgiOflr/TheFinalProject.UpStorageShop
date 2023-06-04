using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommand : IRequest<Response<Guid>>
    {
        public List<ProductDto> Products { get; set; }
        public Guid OrderId { get; set; }
    }

    public class ProductDto
    {
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }
    }
}