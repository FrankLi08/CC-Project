using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TheDream.Domain.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    //Temporary example
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
    }
    public class MyPrincipal : IPrincipal
    {
        public MyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }
        public IIdentity Identity
        {
            get;
            private set;
        }
        public User User { get; set; }
        public bool IsInRole(string role)
        {
            return true;
        }
    }
}