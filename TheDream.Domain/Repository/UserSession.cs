using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using TheDream.Domain.Repository.Interface;

namespace TheDream.Domain.Repository
{
   

    public class UserSession : IUserSession
    {
        public string BearerToken
        {
            get
            {
                return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("AcessToken").Value;
            }
        }
    }
}