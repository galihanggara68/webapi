using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Web_API_Project.Filters;

namespace Web_API_Project.Controllers
{
    [SessionFilter]
    public class DashboardController : Controller
    {
        
        [Route("apiclient")]
        public ActionResult GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                // Set Base Address
                client.BaseAddress = new Uri("http://localhost:52535/");

                // Set Authorization Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "MTAwOlN0ZXZlbg==");

                // Execute Get Method
                var get = client.GetAsync("getprincipal");
                get.Wait();

                // Get Result
                var result = get.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Read Result
                    var read = result.Content.ReadAsAsync<string>();
                    read.Wait();
                    // Store result to data
                    var data = read.Result;
                    // Set ViewBag.Title
                    ViewBag.Title = data;
                }
                return View("Index");
            }
        }
    }
}