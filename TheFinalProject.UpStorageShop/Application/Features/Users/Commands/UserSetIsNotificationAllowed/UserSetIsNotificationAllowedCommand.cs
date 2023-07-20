using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Users.Commands.UserSetIsNotifactionAllowed
{
    public class UserSetIsNotifactionAllowedCommand : IRequest<Response<bool>>
    {
        public bool NewState { get; set; }
    }
}