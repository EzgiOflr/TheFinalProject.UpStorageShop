using Application.Common.Models;
using Application.Common.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IJwtService
    {
        JwtDto Generate(Guid userId, string email, string firstName, string lastName, List<string> roles, bool isMailAllowed, bool isNotificationAllowed);
        Guid? ValidateJwtToken(string token);
    }
}
