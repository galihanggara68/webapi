using System.Web.Mvc;

namespace Web_Project.Filters
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