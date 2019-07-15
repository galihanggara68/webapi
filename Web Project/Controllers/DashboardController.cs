using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_Project.DTO;
using Web_Project.Filters;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    [SessionFilter]
    public class DashboardController : Controller
    {
        [Route("dashboard")]
        public ActionResult Index()
        {
            UserDTO user = (UserDTO)Session["user"];
            if (user == null) return Redirect("~/login");
            string token = user.Username + ":" + user.Token;
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            using (HttpClient client = new HttpClient())
            {
                // Set Base Address
                client.BaseAddress = new Uri("https://localhost:44362/");

                // Set Authorization Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

                // Execute Get Method
                var get = client.GetAsync("api/values");
                get.Wait();

                // Get Result
                var result = get.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Read Result
                    var read = result.Content.ReadAsAsync<List<ItemDTO>>();
                    read.Wait();
                    // Store result to data
                    var data = read.Result;
                    // Set ViewBag.Title
                    return View(data);
                }
            }
            return View(new List<ItemDTO>());
        }
    }
}