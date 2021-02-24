using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Results.WebAPI.Models.RestModels.User
{
    public class RegisterUserRest : UserRestBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
        public bool ValidatePassword()
        {
            if (String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(ConfirmPassword))
            {
                return false;
            }

            if (!String.Equals(Password, ConfirmPassword))
            {
                return false;
            }

            return true;
        }

    }
}