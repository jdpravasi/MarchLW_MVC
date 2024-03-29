using Azure;
using MarchLW_MVC.Data;
using MarchLW_MVC.Models;
using MarchLW_MVC.Models.VM;
using MarchLW_MVC.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MarchLW_MVC.Controllers
{
    public class TicketsController : Controller
    {
        string BaseURL = "https://localhost:7018/api/";
        private readonly HttpClient client;

        private readonly ApplicationDbContext db;
        public TicketsController(ApplicationDbContext dbc)
        {
            client = new HttpClient();
            this.db = dbc;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GenerateRow(string name)
        {
            List<Rides> response = new List<Rides>();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync($"Tickets/SearchRides?searchQuery={name}");
            if (Res.IsSuccessStatusCode)
            {
                var Rides = Res.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<List<Rides>>(Rides);
            }
            return PartialView("_GenerateRow", response[0]);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            List<Rides> response = new List<Rides>();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync($"Tickets/SearchRides?searchQuery={searchString}");
            if (Res.IsSuccessStatusCode)
            {
                var Rides = Res.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<List<Rides>>(Rides);
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TicketVM model)
        {

            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("Tickets/CreateTicket", data);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { status = "Created" });
            }
            else
            {
                return Json(new { status = "Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTicketDetails()
        {
            List<TicketDetails> response = new List<TicketDetails>();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("Tickets/GetAllTicketDetails");
            if (Res.IsSuccessStatusCode)
            {
                var TicketDetails = Res.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<List<TicketDetails>>(TicketDetails);
            }
            return PartialView("_TicketDetails", response);
        }
    }
}

