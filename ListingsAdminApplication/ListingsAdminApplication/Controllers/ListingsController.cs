using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ListingsAdminApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ListingsAdminApplication.Controllers
{
    public class ListingsController : Controller
    {
        string prefixURL = "https://localhost:44306/api/AdminListings";

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingNoImage>>().Result;

            return View(result);
        }

        public IActionResult Inactive()
        {
            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetInactiveListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingNoImage>>().Result;

            return View(result);
        }
        public IActionResult Approved()
        {
            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetApprovedListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingNoImage>>().Result;

            return View(result);
        }
        public IActionResult Disapproved()
        {
            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetDisapprovedListings";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<ListingNoImage>>().Result;

            return View(result);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetListingPostDetails";

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
        public IActionResult Validate(Guid? id, String actionType)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            string URI = prefixURL + "/GetListing";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;

            var listing = responseMessage.Content.ReadAsAsync<ListingNoImage>().Result;


            if (listing == null)
            {
                return NotFound();
            }


            client = new HttpClient();
            URI = prefixURL+"/ApproveListing";

            var validateModel = new
            {
                Id = id,
                Action = actionType
            };

            content = new StringContent(JsonConvert.SerializeObject(validateModel), Encoding.UTF8, "application/json");

            responseMessage = client.PostAsync(URI, content).Result;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileContentResult ExportListings(DateTime dateExport)
        {
            string fileName = "Listings.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Listings");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Description";
                worksheet.Cell(1, 4).Value = "Price";
                worksheet.Cell(1, 5).Value = "Date created";

                var col1 = worksheet.Column("E");
                col1.Width = 20;

                worksheet.Cell(1, 6).Value = "Category";
                worksheet.Cell(1, 7).Value = "Location";
                worksheet.Cell(1, 8).Value = "Status";

                HttpClient client = new HttpClient();

                string URI = prefixURL + "/GetAllByDate";
                var model = new
                {
                    Date = dateExport
                };

                HttpContent parametars = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = client.PostAsync(URI, parametars).Result;

                var result = responseMessage.Content.ReadAsAsync<List<ListingNoImage>>().Result;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Title;
                    worksheet.Cell(i + 1, 3).Value = item.Description;
                    worksheet.Cell(i + 1, 4).Value = item.Price.ToString();
                    worksheet.Cell(i + 1, 5).Value = item.DateCreated.ToString("dd/MM/yyyy HH:mm:ss");
                    worksheet.Cell(i + 1, 6).Value = item.Category.Name;
                    worksheet.Cell(i + 1, 7).Value = item.Location.City;
                    worksheet.Cell(i + 1, 8).Value = item.Status;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }


    }
}
