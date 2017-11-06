using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using TheDream.Domain.Models;
using TheDream.Domain.Response;

namespace TheDream.Domain.Helper
{
    public class FormAuthHelper
    {
        public static bool ValidateUser(UserViewModel model, TokenResponse tokenResponse, HttpResponseBase Response)
        {
            bool result = false;
            if(tokenResponse.StatusIsSuccessful==true)
            {
                // Create the authentiation ticket with custom user data
                var serializer = new JavaScriptSerializer();
                string userRoles = tokenResponse.Role;
                
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    model.UserName,
                    DateTime.Now,
                    DateTime.Now.AddDays(30),
                    true,
                    userRoles,
                    FormsAuthentication.FormsCookiePath
                    );
                //Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                //Create the cookie.

                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                result = true;
            }
            return result;
        }
    }
}