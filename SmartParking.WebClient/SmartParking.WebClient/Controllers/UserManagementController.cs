using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartParkingSystemAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;

namespace SmartParking.WebClient.Controllers
{
    public class UserManagementController : Controller
    {

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }
         
        public async Task<ActionResult> User_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<UserProfileModel> users = new List<UserProfileModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/UserProfile/GetUserProfileList");
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<UserProfileModel>>();
                }
            } DataSourceResult result = users.ToDataSourceResult(request);
            return Json(result);
        }
        [HttpGet]
        public PartialViewResult GetUserEntryForm()
        {
            return PartialView("_UserForm");
        }
        public async Task<ActionResult> GetUserEditForm(int userID)
        {
            UserProfileModel userToEdit = new UserProfileModel();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/UserProfile/GetUserProfile/" + userID);
                if (response.IsSuccessStatusCode)
                {
                    userToEdit = await response.Content.ReadAsAsync<UserProfileModel>();
                }
            }

            ViewBag.Mode = "Edit";
            return PartialView("_UserForm", userToEdit);
        }


        public async Task<ActionResult> User_Destroy([DataSourceRequest]DataSourceRequest request, UserProfileModel user)
        {
            List<UserProfileModel> users = new List<UserProfileModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                user.IsActive = false;
                HttpResponseMessage response = await client.PostAsJsonAsync("api/UserProfile/PostUpdateUserProfile", user);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<UserProfileModel>>();
                }
            } DataSourceResult result = users.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddUser(UserProfileModel userToAdd)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/UserProfile/PostUserProfile", userToAdd);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }

            return PartialView("_UsersGrid");
        }

        public async Task<ActionResult> UpdateUser(UserProfileModel userToEdit)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/UserProfile/PostUpdateUserProfile", userToEdit);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult LoadIndex()
        {
            return PartialView("_UsersGrid");
        }
    }
}














#region COMMENTED
//public async Task<ActionResult> User_Create([DataSourceRequest]DataSourceRequest request, UserProfileModel user)
//{
//    List<UserProfileModel> users = new List<UserProfileModel>();
//    using (var client = new HttpClient())
//    {
//        string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
//        client.BaseAddress = new Uri(url);
//        client.DefaultRequestHeaders.Accept.Clear();
//        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        HttpResponseMessage response = await client.GetAsync("api/UserProfile/GetUserProfileList");
//        if (response.IsSuccessStatusCode)
//        {
//            //   users = await response.Content.ReadAsAsync<List<UserProfileModel>>();
//        }
//    } DataSourceResult result = users.ToDataSourceResult(request);
//    return Json(result);
//}
//public async Task<ActionResult> User_Update([DataSourceRequest]DataSourceRequest request, UserProfileModel user)
//{
//    List<UserProfileModel> users = new List<UserProfileModel>();
//    using (var client = new HttpClient())
//    {
//        string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
//        client.BaseAddress = new Uri(url);
//        client.DefaultRequestHeaders.Accept.Clear();
//        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        HttpResponseMessage response = await client.PutAsJsonAsync("api/UserProfile/GetUserProfileList", user);
//        if (response.IsSuccessStatusCode)
//        {
//            users = await response.Content.ReadAsAsync<List<UserProfileModel>>();
//        }
//    } DataSourceResult result = users.ToDataSourceResult(request);
//    return Json(result, JsonRequestBehavior.AllowGet);
//}
#endregion