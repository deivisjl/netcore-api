using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.User.Application.Login
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
