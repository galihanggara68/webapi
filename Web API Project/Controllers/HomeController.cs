
using System.Web.Mvc;

namespace Web_API_Project.Controllers
{
  
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult PostLogin(string username, string password)
        {
            if(username != null && password != null)
            {
                if(username == "admin" && password == "admin123")
                {
                    Session.Add("user", username);
                    return Redirect("~/apiclient");
                }
            }
            return View("Login");
        }

    }
}
