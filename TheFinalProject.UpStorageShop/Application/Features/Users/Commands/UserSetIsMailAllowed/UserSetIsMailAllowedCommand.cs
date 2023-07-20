using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Users.Commands.UserSetIsMailAllowed
{
    public class UserSetIsMailAllowedCommand : IRequest<Response<bool>>
    {
        public bool NewState { get; set; }
    }
}