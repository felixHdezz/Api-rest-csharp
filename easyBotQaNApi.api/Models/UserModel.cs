using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
    }

    public class SaveUserModel {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserModel {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }
}