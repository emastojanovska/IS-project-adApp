﻿using System;
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

            string URI = "https://localhost:44306/api/AdminListings/GetListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingPost>>().Result;

            return View(result);
        }

        public IActionResult Inactive()
        {
            HttpClient client = new HttpClient();

            string URI = "https://localhost:44306/api/AdminListings/GetInactiveListings";

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

            string URI = "https://localhost:44306/api/AdminListings/GetListingPostDetails";

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

        // GET: Listings/Approve/5
        public IActionResult Approve(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = "https://localhost:44306/api/AdminListings/GetListing";

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

        // POST: Listings/Approve/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveConfirmed(Guid id)
        {
            HttpClient client = new HttpClient();
            string URI = "https://localhost:44306/api/AdminListings/ApproveListing";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            return RedirectToAction("Index");
        }
    }
}