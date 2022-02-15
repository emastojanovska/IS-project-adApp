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
    public class CategoriesController : Controller
    {
        string prefixURL = "https://localhost:44306/api/Admin";
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();


            string URI = prefixURL+"/GetCategories";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<Category>>().Result;

            return View(result);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Category category)
        {
            HttpClient client = new HttpClient();
            string URI = prefixURL + "/CreateCategory";

            var model = new Category
            {
                Name = category.Name
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


            return RedirectToAction("Index");
        }


        // GET: Categories/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetCategory";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            var category = responseMessage.Content.ReadAsAsync<Category>().Result;

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name, Id")] Category category)
        {
            HttpClient client = new HttpClient();
            string URI = prefixURL+ "/EditCategory";

            var model = new Category
            {
                Name = category.Name,
                Id = category.Id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetCategory";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            var category = responseMessage.Content.ReadAsAsync<Category>().Result;


            if (category == null)
            {
                return NotFound();
            }

            client = new HttpClient();
            URI = prefixURL +"/DeleteCategory";

            model = new
            {
                Id = id
            };

           content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

           responseMessage = client.PostAsync(URI, content).Result;

           return RedirectToAction("Index");
        }
    }

    
}
