using System.Web;
using System.Web.Mvc;
using Web_API_Project.Filters;

namespace Web_API_Project
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
