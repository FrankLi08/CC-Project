using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheDream.Extension
{
    public static class ApiExtensions
    {
        public static KeyValuePair<string, string> AsPair(this string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}