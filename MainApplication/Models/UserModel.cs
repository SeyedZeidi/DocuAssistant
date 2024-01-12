using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        public UserModel(int userId, string username, string passwordHash, string role, string firstName, string lastName, string email, DateTime registrationDate)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RegistrationDate = registrationDate;
        }
    }

}

