using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared
{
    public class IdentityAccess
    {
        public int code { get; set; }
        public bool Succeeded { get; set; }
        public string AccessToken { get; set; }

        public string Message { get; set; }
    }
}
