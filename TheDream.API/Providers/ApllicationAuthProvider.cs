using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace TheDream.API.Providers
{
    public class ApllicationAuthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            bool user = Membership.ValidateUser(context.UserName, context.Password);

            if (user == false)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }



            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
           foreach(string value in Roles.GetRolesForUser(context.UserName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, value));
            }
            List<Claim> roles = identity.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            AuthenticationProperties properties = CreateProperties(Newtonsoft.Json.JsonConvert.SerializeObject(roles.Select(x => x.Value)));
           
            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            context.Validated(identity);
           
        }

        public static AuthenticationProperties CreateProperties(string role)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "role", role },
                {"version","1.0" }
            };
            return new AuthenticationProperties(data);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var roles = context.Identity.FindAll(ClaimTypes.Role);
            List<string> roleList = new List<string>();
            if(roles != null)
            {
                foreach(var claim in roles)
                {
                    roleList.Add(claim.Value);
                }
                context.AdditionalResponseParameters.Add("roles", string.Join(",", roleList.ToArray()));
            }

            
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}