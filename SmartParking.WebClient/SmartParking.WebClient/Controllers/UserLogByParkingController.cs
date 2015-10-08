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
    public class UserLogByParkingController : Controller
    {
        //
        // GET: /UserLogByParking/

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAllParkings()
        {
            List<ParkingModel> allParkings = new List<ParkingModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ParkingMaster/GetParkingMasterList");
                if (response.IsSuccessStatusCode)
                {
                    allParkings = await response.Content.ReadAsAsync<List<ParkingModel>>();
                }
            }
            return Json(allParkings, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetSlotNames(string parkingID)
        {
            int parkID = int.Parse(parkingID);
            List<SlotModel> allSlots = new List<SlotModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/SlotMaster/GetSlots?id=" + parkID);
                if (response.IsSuccessStatusCode)
                {
                    allSlots = await response.Content.ReadAsAsync<List<SlotModel>>();
                }
            }
            return Json(allSlots, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Log_ReadBySlot([DataSourceRequest]DataSourceRequest request, string userID = "", string slotID = "")
        {

            List<SlotTransactionModel> allSlotTransaction = new List<SlotTransactionModel>();
            if ((string.IsNullOrWhiteSpace(userID)) && (string.IsNullOrWhiteSpace(slotID)))
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
            else if (!string.IsNullOrWhiteSpace(userID))
            {
                int UserID = int.Parse(userID);
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string uri = String.Format("api/SlotTransaction/GetSlotTransactionAsUser?userId={0}",UserID);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
                    }
                    DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
                    return Json(result);
                }

            }
            else if (!string.IsNullOrWhiteSpace(slotID))
            {
                int SlotID = int.Parse(slotID);                 
                using (var client = new HttpClient())
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string uri = String.Format("api/SlotTransaction/GetSlotTransaction_BySlot?slotId={0}",SlotID);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
                    }
                    DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
                    return Json(result);
                }
            }
            else
            {
                DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
                return Json(result);
            }
            

            //if ((string.IsNullOrWhiteSpace(userID) && string.IsNullOrWhiteSpace(slotID)) || (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(slotID)))
            //{

            //using (var client = new HttpClient())
            //{
            //    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
            //    client.BaseAddress = new Uri(url);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = await client.GetAsync("api/SlotTransaction/GetSlotTransaction");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
            //    }
            //}
            //DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
            //return Json(result);
            //}

            //else
            //{
            //int UserID = int.Parse(userID);
            //int SlotID = int.Parse(slotID);
            //using (var client = new HttpClient())
            //{
            //    string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
            //    client.BaseAddress = new Uri(url);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    string uri = String.Format("api/SlotTransaction/GetSlotTransaction?slotId={0}&userId={1}",SlotID,UserID);
            //    HttpResponseMessage response = await client.GetAsync(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        allSlotTransaction = await response.Content.ReadAsAsync<List<SlotTransactionModel>>();
            //    }
            //}
            //DataSourceResult result = allSlotTransaction.ToDataSourceResult(request);
            //return Json(result);
            //}

        }


    }
}
