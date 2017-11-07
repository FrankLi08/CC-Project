using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using TheDream.Model.Common;
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
    
    [Route("GetByUserName")]
    [HttpGet]
    public HttpResponseMessage GetByUserName(string username)
    {
        if (ModelState.IsValid)
        {
            MembershipUser newUser = Membership.GetUser(username);
            if (newUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Users memberuser = new Users();
                memberuser.UserName = newUser.UserName;
                memberuser.Email = newUser.Email;
                return Request.CreateResponse(HttpStatusCode.OK, memberuser);
            }
        }
        return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    [HttpPut]
    [Route("UpdateUser")]
    public HttpResponseMessage UpdateUser(Users model)
    {
        if (!ModelState.IsValid)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        string currentusername = User.Identity.GetUserName();
        if (model.UserName != currentusername)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, "You can not edit others account");
        }

        MembershipUser updateuser = Membership.GetUser(model.UserName);
        if (updateuser == null)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, "User is not Exist");
        }

        updateuser.Email = model.Email;

        Membership.UpdateUser(updateuser);
        //Test if the account is locked can we update information.
        return Request.CreateResponse(HttpStatusCode.OK);
    }
    [HttpPost]
    [Route("ChangePassword")]
    public HttpResponseMessage ChangePassword(ChangePasswordModel model)
    {
        if (!ModelState.IsValid || model.password == null || model.newPassword == null)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        MembershipUser user = Membership.GetUser(model.username);
        if (user != null)
        {
            bool validation = user.ChangePassword(model.password, model.newPassword);

            if (validation == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
        return Request.CreateResponse(HttpStatusCode.NotFound);
    }
    [HttpGet]
    [Route("GetUserInfo")]
    public HttpResponseMessage GetUserInfo(string userName)
    {
        MembershipUser userInfo = Membership.GetUser(userName);
        if (userInfo == null)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        else
        {
            UpdateUser model = new UpdateUser();
            model.username = userInfo.UserName;
            model.Email = userInfo.Email;
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user != null)
            {
                if (user.IsLockedOut == true)
                {
                    user.UnlockUser();
                }
                string oldPassword = user.ResetPassword();
                PasswordGenerator Pwg = new PasswordGenerator();
                string newPassword = Pwg.GeneratePassword(6, 32, 1, 1, 1, 1);
                user.ChangePassword(oldPassword, newPassword);

                //TO DO: add send email function.

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
