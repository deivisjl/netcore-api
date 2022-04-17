using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Rol : IdentityRole
    {
        public ICollection<UserRol> UserRol { get; set; }
    }
}
