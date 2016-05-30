using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LaCrosse.Inet.EmailServicesAPI.Utils;

namespace LaCrosse.Inet.EmailServicesAPI
{
    public class FormatConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new EmailMessageContentMediaTypeFormatter());
        }
    }
}