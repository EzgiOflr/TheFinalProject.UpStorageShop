using Application.Features.Users.Commands.UserSetIsMailAllowed;
using Application.Features.Users.Commands.UserSetIsNotifactionAllowed;
using Application.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpStorageShop.WebApi.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync(GetAllUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("UserSetIsNotifactionAllowed")]
        public async Task<IActionResult> UserSetIsNotifactionAllowedAsync(UserSetIsNotifactionAllowedCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("UserSetIsMailAllowed")]
        public async Task<IActionResult> UserSetIsMailAllowedAsync(UserSetIsMailAllowedCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
