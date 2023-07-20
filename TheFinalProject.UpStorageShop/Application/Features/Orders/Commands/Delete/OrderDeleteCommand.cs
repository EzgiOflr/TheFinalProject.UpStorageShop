using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderDeleteCommand : IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
    }
}