using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }

    public class RegisterDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class UserRegister:RegisterDto
    {
        public string   Nama { get; set; }
        public Gender Gender { get; set; }
        public string NIK { get; set; }

    }

}
