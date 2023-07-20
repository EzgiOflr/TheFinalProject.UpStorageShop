using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersDto
    {
        public Guid UserId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public bool IsNotificationAllowed { get; set; }
        public bool IsMailAllowed { get; set; }
    }
}
