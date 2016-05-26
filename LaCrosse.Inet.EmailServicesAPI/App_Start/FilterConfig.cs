using System.Web;
using System.Web.Mvc;


namespace LaCrosse.Inet.EmailServicesAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}