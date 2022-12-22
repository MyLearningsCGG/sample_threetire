using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_Three_Tire.Entities
{
    public class LoginUser
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }

        public string ipaddress { get; set; }
    }
}