using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using TheDream.Domain.Models;

namespace TheDream.Domain
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // look if any security information exists for this request
            if (HttpContext.Current.User != null)
            {
                // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var authCookies = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if(authCookies!=null)
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookies.Value);
                        if(authTicket !=null&&!authTicket.Expired)
                        {
                            var roles = authTicket.UserData.Split(',');
                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new  FormsIdentity(authTicket), roles);
                            var newTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
                            string newEncryptedTicket = FormsAuthentication.Encrypt(newTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, newEncryptedTicket);
                            HttpContext.Current.Response.Cookies.Add(authCookie);
                        }

                        
                    }
                    // see if the authentication is done using FormsAuthentication
                    //if (HttpContext.Current.User.Identity is FormsIdentity)
                    //{
                    //    // Get the roles stored for this request from the ticket
                    //    // get the identity of the user
                    //    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    //    //Get the form authentication ticket of the user
                    //    FormsAuthenticationTicket ticket = identity.Ticket;
                    //    //Get the roles stored as UserData into ticket
                    //    //string[] roles = { };
                    //    string userData = ticket.UserData;
                    //       string[] roles = userData.Split(',');
                    //    //Create general prrincipal and assign it to current request

                        
                    }
                }
            }
        }
    
}
