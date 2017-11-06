using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using TheDream.Model.Models;

namespace TheDream.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [Authorize(Roles ="Administrator")]
        public HttpResponseMessage GetUserRoles(string userName)
        {
            List<string> roleList =  Roles.GetRolesForUser(userName).ToList();
            if (roleList.Count>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, roleList);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }

        
        [Route("SignUp")]
        [HttpPost]
        public HttpResponseMessage SignUp([FromBody] Users user)
        {
            if(ModelState.IsValid)
            {
               if( Membership.GetUser(user.UserName)!=null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User already exist");
                }
               if(Membership.CreateUser(user.UserName, user.Password,user.Email)==null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
               else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(string userName)
        {
            if(userName==null||userName=="")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "username is Invalid");
            }
            MembershipUser newUser = Membership.GetUser(userName);
            if (newUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User is not Exist");
            }
          
            Roles.RemoveUserFromRole(userName, Roles.GetRolesForUser(userName).First().ToString());

            bool Issuccess = Membership.DeleteUser(userName);
            if (Issuccess == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
