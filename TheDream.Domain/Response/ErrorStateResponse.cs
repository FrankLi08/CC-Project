using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheDream.Domain.Response
{
    public class ErrorStateResponse
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> ModelState { get; set; }
    }
}