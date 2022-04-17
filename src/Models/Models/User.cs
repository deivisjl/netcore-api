using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRol> UserRol { get; set; }
    }
}
