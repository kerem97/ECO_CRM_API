using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.User.Requests
{
    public class AddUserRequest
    {
        
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
