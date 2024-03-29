using MarchLW_MVC.Data;
using MarchLW_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MarchLW_MVC.Controllers
{
    public class RidesController : Controller
    {
        string BaseURL = "https://localhost:7018/api/";
        private readonly HttpClient client;

        private readonly ApplicationDbContext db;
        public RidesController(ApplicationDbContext dbc)
        {
            client = new HttpClient();
            this.db = dbc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRides()
        {
            List<Rides> response = new List<Rides>();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("Tickets/GetAllRides");
            if (Res.IsSuccessStatusCode)
            {
                var Rides = Res.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<List<Rides>>(Rides);
            }
            return PartialView("_GetAllRides", response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRide(Rides ride)
        {
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(ride);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("Rides/Create", stringContent);
            
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRide(int id)
        {
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"Rides/GetRide/{id}");

            if (response.IsSuccessStatusCode)
            {
                var ride = response.Content.ReadAsStringAsync().Result;
                var rideData = JsonConvert.DeserializeObject<Rides>(ride);
                return Json(rideData);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRide(Rides ride)
        {
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(ride);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("Rides/Update", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRide(int id)
        {
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync($"Rides/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

    }
}
