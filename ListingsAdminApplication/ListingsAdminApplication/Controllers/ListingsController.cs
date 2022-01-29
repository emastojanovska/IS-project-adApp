using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ListingsAdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ListingsAdminApplication.Controllers
{
    public class ListingsController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URI = "https://localhost:5001/api/AdminListings/GetListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingPost>>().Result;

            return View(result);
        }

        public IActionResult Inactive()
        {
            HttpClient client = new HttpClient();

            string URI = "https://localhost:5001/api/AdminListings/GetInactiveListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingPost>>().Result;

            return View(result);
        }

        // GET: Listings/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = "https://localhost:5001/api/AdminListings/GetListingPostDetails";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            var listing = responseMessage.Content.ReadAsAsync<ListingPost>().Result;

            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        // GET: Listings/Validate/5
        public IActionResult Validate(Guid? id, String action)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = "https://localhost:5001/api/AdminListings/GetListing";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            var listing = responseMessage.Content.ReadAsAsync<ListingPost>().Result;


            if (listing == null)
            {
                return NotFound();
            }


            client = new HttpClient();
            URI = "https://localhost:5001/api/AdminListings/ApproveListing";

            var validateModel = new
            {
                Id = id,
                Action = action 
            };

            content = new StringContent(JsonConvert.SerializeObject(validateModel), Encoding.UTF8, "application/json");

            responseMessage = client.PostAsync(URI, content).Result;

            return RedirectToAction("Index");
        }

       

    }
}
