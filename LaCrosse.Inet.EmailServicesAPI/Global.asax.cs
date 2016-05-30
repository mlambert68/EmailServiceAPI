using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Routing;
//using System.Web.Security;
//using System.Web.SessionState;
//using System.Net;
//using System.Net.Http;

namespace LaCrosse.Inet.EmailServicesAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.Routes.Add("default", new HttpRoute("{controller}"));
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //FormatConfig.Register(GlobalConfiguration.Configuration);
        }

    }   
}