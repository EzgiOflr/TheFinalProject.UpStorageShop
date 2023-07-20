using Application.Features.Users.Queries.GetAllUsers;
using MediatR;

namespace Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersDto>>
    {
        public GetAllUsersQuery()
        {

        }
    }
}
