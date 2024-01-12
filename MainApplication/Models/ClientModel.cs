using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public ClientModel(int clientId, string clientName, string? contactPerson = null, string? email = null, string? phone = null, string? address = null)
        {
            ClientId = clientId;
            ClientName = clientName;
            ContactPerson = contactPerson;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }

}
