using Application.Common.Interfaces;
using Application.Features.Users.Queries.GetAllUsers;
using DocumentFormat.OpenXml.Office.CustomUI;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersDto>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly UserManager<User> _userManager;
        public GetAllUsersQueryHandler(IUpStorageShopDbContext upStorageShopDbContext, UserManager<User> userManager)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _userManager = userManager;
        }

        public async Task<List<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _userManager.Users;

            var users = await dbQuery.ToListAsync(cancellationToken);
            var userDtos = MapUsersToDto(users);
            return userDtos.ToList();
        }

        private IEnumerable<GetAllUsersDto> MapUsersToDto(List<User> users)
        {
            foreach (var user in users)
            {
                yield return new GetAllUsersDto()
                {
                    IsMailAllowed = user.IsMailAllowed,
                    IsNotificationAllowed = user.IsNotificationAllowed,
                    Email = user.Email,
                    NameSurname = user.FirstName + " " + user.LastName,
                    UserId = user.Id
                };
            }
        }
    }
}
