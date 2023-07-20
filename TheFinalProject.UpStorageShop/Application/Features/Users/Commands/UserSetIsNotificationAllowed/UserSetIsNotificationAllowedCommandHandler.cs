using Application.Common.Interfaces;
using Domain.Common;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.UserSetIsNotifactionAllowed
{
    public class UserSetIsNotifactionAllowedCommandHandler : IRequestHandler<UserSetIsNotifactionAllowedCommand, Response<bool>>
    {
        private readonly IUpStorageShopDbContext _upStorageShopDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserSetIsNotifactionAllowedCommandHandler(IUpStorageShopDbContext upStorageShopDbContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _upStorageShopDbContext = upStorageShopDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Response<bool>> Handle(UserSetIsNotifactionAllowedCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userId = (Guid)_httpContextAccessor.HttpContext.Items["User"];
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    return new Response<bool>($"User not found.", false);

                user.IsNotificationAllowed = command.NewState;

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                    return new Response<bool>($"Notifaction status updated successfully.", true);

                return new Response<bool>($"An error occured.", false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}