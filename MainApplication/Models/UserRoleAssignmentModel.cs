using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class UserRoleAssignmentModel
    {
        public int UserId { get; set; }
        public string Role { get; set; }

        public UserRoleAssignmentModel(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }

}
