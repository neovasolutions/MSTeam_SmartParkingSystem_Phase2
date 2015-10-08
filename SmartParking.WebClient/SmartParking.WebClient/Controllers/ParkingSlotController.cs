using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartParkingSystemAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;

namespace SmartParking.WebClient.Controllers
{
    public class ParkingSlotController : Controller
    {
        bool result = false;
        public async Task<ActionResult> Slot_Read([DataSourceRequest]DataSourceRequest request, string parkingID = "")
        {
            if (string.IsNullOrWhiteSpace(parkingID))
            {
                List<SlotModel> slots = new List<SlotModel>();
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/SlotMaster/GetSlots");
                    if (response.IsSuccessStatusCode)
                    {
                        slots = await response.Content.ReadAsAsync<List<SlotModel>>();
                    }
                }
                DataSourceResult result = slots.ToDataSourceResult(request);
                return Json(result);
            }
            else
            {
                int parkID = int.Parse(parkingID);
                List<SlotModel> slots = new List<SlotModel>();
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/SlotMaster/GetSlots?id=" + parkID);
                    if (response.IsSuccessStatusCode)
                    {
                        slots = await response.Content.ReadAsAsync<List<SlotModel>>();
                    }
                }
                DataSourceResult result = slots.ToDataSourceResult(request);
                return Json(result);            
            }            
        }

        public async Task<ActionResult> Slot_Destroy([DataSourceRequest]DataSourceRequest request, UserProfileModel user)
        {
            List<UserProfileModel> users = new List<UserProfileModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                user.IsActive = false;
                HttpResponseMessage response = await client.PostAsJsonAsync("api/SlotMaster/PostUpdateUserProfile", user);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<UserProfileModel>>();
                }
            } 
            DataSourceResult result = users.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult GetSlotAddForm()
        {
            return PartialView("_AddSlot");
        }
        
        [HttpGet]
        public async Task<PartialViewResult> GetSlotEditForm(int slotID)
        {
            SlotModel slot = new SlotModel();
            using (var client = new HttpClient())
            {   
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Slotmaster/GetSlotByID/" + slotID);
                if (response.IsSuccessStatusCode)
                {
                    slot = await response.Content.ReadAsAsync<SlotModel>();
                }
            }
            ViewBag.Mode = "Edit";
            return PartialView("_AddSlot", slot);
        }
        public async Task<JsonResult> GetParkings()
        {
            List<ParkingModel> listParkingModel = new List<ParkingModel>();

            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ParkingMaster/GetParkingMasterList");
                if (response.IsSuccessStatusCode)
                {
                    listParkingModel = await response.Content.ReadAsAsync<List<ParkingModel>>();
                }
            }            
            return Json(listParkingModel, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult LoadIndex()
        {            
            return PartialView("_ParkingSlotList");
        }

        public async Task<ActionResult> AddSlot(SlotModel slotModel)
        {
           // bool result=false;
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/SlotMaster/PostSlotMaster",slotModel);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }            
            return Json(result);
        }

        public async Task<ActionResult> UpdateSlot(SlotModel slotModel)
        {
            //bool result=false;            
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/SlotMaster/PostUpdateSlotMaster", slotModel);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            return Json(result);
        }
    }
}
