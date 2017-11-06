using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheDream.Domain.Extension
{
    public interface ITokenContainer
    {
        object ApiToken { get; set; }
    }
    public class TokenContainer : ITokenContainer
    {
        private const string ApiTokenKey = "ApiToken";

        public object ApiToken
        {
            get { return Current.Session != null ? Current.Session[ApiTokenKey] : null; }
            set { if (Current.Session != null) Current.Session[ApiTokenKey] = value; }
        }

        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}