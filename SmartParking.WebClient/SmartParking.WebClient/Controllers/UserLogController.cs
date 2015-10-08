using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmartParkingSystemAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace SmartParking.WebClient.Controllers
{
    public class UserLogController : Controller
    {
        //
        // GET: /UserLog/

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAllUsers()
        {
            List<UserProfileModel> allUsers = new List<UserProfileModel>();
            List<UserProfileModel> allUsersWhoAreActive = new List<UserProfileModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/UserProfile/GetUserProfileList");
                if (response.IsSuccessStatusCode)
                {
                    allUsers = await response.Content.ReadAsAsync<List<UserProfileModel>>();
                }
                allUsersWhoAreActive=allUsers.Where(usr => usr.IsActive == true).ToList();
            }
            return Json(allUsersWhoAreActive, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Log_Read([DataSourceRequest]DataSourceRequest request, string userID = "")
        {
            List<SlotTransactionModel> allSlotTransaction = new List<SlotTransactionModel>();
            if (string.IsNullOrWhiteSpace(userID))
            {
               
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/SlotTransaction/GetSlotTransaction");
                    if (response.IsSuccessStatusCode)
                    {
                        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
                    }
                }
                DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
                return Json(result);
            }
            else
            {
                int UserID = int.Parse(userID);                 
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/SlotTransaction/GetSlotTransactionAsUser?userId=" + UserID);
                    if (response.IsSuccessStatusCode)
                    {
                        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
                    }
                }
                DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
                return Json(result);
            }
            
        }
    }
}
