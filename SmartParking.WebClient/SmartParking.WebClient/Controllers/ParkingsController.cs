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
    public class ParkingsController : Controller
    {
        //
        // GET: /Parkings/

        public async Task<ActionResult> Parkings_Read([DataSourceRequest]DataSourceRequest request)
        {
            List<ParkingModel> parkings = new List<ParkingModel>();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ParkingMaster/GetParkingMasterList");
                if (response.IsSuccessStatusCode)
                {
                    parkings = await response.Content.ReadAsAsync<List<ParkingModel>>();
                }
            }

            DataSourceResult result = parkings.ToDataSourceResult(request);
            return Json(result);
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadIndex()
        {
            return PartialView("_ParkingsGrid");
        }
        [HttpGet]
        public PartialViewResult GetParkingAddForm()
        {
            return PartialView("_AddParking");
        }

        [HttpGet]
        public async Task<PartialViewResult> GetParkingEditForm(int parkId)
        {
            ParkingModel parkingToEdit = new ParkingModel();
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ParkingMaster/GetParkingMaster/" + parkId);
                if (response.IsSuccessStatusCode)
                {
                    parkingToEdit = await response.Content.ReadAsAsync<ParkingModel>();
                }
            }
            ViewBag.Mode = "Edit";
            return PartialView("_AddParking", parkingToEdit);
        }

        public async Task<ActionResult> AddParking(ParkingModel parkingModel)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/ParkingMaster/PostParking", parkingModel);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            return Json(result);
        }
                
        public async Task<ActionResult> UpdateParking(ParkingModel parkingModel)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["SmartParkingAPI"];
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync("api/ParkingMaster/PostUpdateParking", parkingModel);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            return Json(result);
        }

    }
}
