using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SmartParkingSystemAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                 name: "DefaultApiWithAction",
                 routeTemplate: "api/{controller}/{action}/{inputId}",
                 defaults: new { inputId = RouteParameter.Optional }
                 );

        }
    }
}
