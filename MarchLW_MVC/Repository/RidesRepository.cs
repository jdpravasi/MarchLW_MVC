using MarchLW_MVC.Models;
using MarchLW_MVC.Repository.Interface;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;

namespace MarchLW_MVC.Repository
{
    public class RidesRepository : IRides
    {
        string BaseURL = "https://localhost:7018/api/";
        private readonly HttpClient client;
        public RidesRepository()
        {
            client = new HttpClient();
        }
        public IEnumerable<Rides> GenerateRow(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Rides>> SearchRides(string searchQuery)
        {
            List<Rides> response = new List<Rides>();

            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync($"Tickets/SearchRides?searchQuery={searchQuery}");
            if (Res.IsSuccessStatusCode)
            {
                var Rides = Res.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<IEnumerable<Rides>>(Rides).ToList();
                return response;
            }
            else
            {
                // Log or display an error message
                Console.WriteLine($"Error: {Res.StatusCode}");
            }
            return Enumerable.Empty<Rides>();
        }

    }
}
