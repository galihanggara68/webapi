using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_Project.DTO;
using Web_Project.Models;

namespace Web_Project.Controllers
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
        public ActionResult PostLogin(User user)
        {
            if (ModelState.IsValid)
            {
                using(APIEntities api = new APIEntities())
                {
                    User usr = api.Users.FirstOrDefault(u => 
                                u.username.Equals(user.username) && 
                                u.password.Equals(user.password)
                    );
                    if(usr != null)
                    {
                        string microSeconds = DateTime.Now.ToString("HH:mm:ss.ffffff");
                        string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(microSeconds));
                        usr.token = token;
                        api.SaveChanges();
                        Session.Add("user", new UserDTO { UserId = usr.user_id, Username = usr.username, Token = token });
                        return Redirect("~/dashboard");
                    }
                    
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            UserDTO user = (UserDTO)Session["user"];
            using (APIEntities api = new APIEntities())
            {
                User u = api.Users.Find(user.UserId);
                u.token = null;
                api.SaveChanges();
            }
            Session["user"] = null;
            return Redirect("~/login");
        }
    }
}
