using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheDream.Model.Models;

namespace TheDream.Domain.Repository.Interface
{
   public  interface IHttpClientRepo
    {
        Task<HttpResponseMessage> GetRoles(string userName);
        Task<HttpResponseMessage> SignUp(Users model);
        Task<HttpResponseMessage> ChangePassword(ChangePasswordModel model);
        Task<HttpResponseMessage> ResetPassword(string userName);
        Task<HttpResponseMessage> GetUserInfo(string userName);
        Task<HttpResponseMessage> UpdateUser(UpdateUser model);
        Task<HttpResponseMessage> SendRecipe(RecipeViewModel model);
        Task<HttpResponseMessage> GetVegList();
        Task<HttpResponseMessage> GetMeatList();
        Task<HttpResponseMessage> GetSeasonList();
        Task<HttpResponseMessage> GetEggList();

    }
}
