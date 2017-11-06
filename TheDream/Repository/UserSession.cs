using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace TheDream.Repository
{
    public interface IUserSession
    {
        string BearerToken { get; }
    }
    public class UserSession:IUserSession
    {
        public string BearerToken
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("AcessToken").Value; }
        }

    }
}