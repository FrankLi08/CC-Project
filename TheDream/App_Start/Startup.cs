using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//[assembly:OwinStartup(typeof(TheDream.App_Start.Startup))]
namespace TheDream.App_Start
{
    public class Startup
    {
       public void Configuartion(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("Account/Login"),
            });
        }
    }
}