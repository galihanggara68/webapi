using System;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Net.Http;

namespace Web_API_Project.Filters
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.RequestContext.HttpContext;
            if (context.Session["user"] == null)
            {
                context.Response.Redirect("~/login");
            }

            base.OnActionExecuting(filterContext);
        }

    }
}