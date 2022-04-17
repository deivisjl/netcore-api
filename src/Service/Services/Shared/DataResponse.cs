using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared
{
    public class DataResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
