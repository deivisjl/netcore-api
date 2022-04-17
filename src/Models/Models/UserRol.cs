using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserRol : IdentityUserRole<string>
    {
        public Rol Rol { get; set; }
        public User User { get; set; }
    }
}
