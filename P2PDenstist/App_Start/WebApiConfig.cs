using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace P2PDenstist
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
        //  EnableCorsAttribute cors = new EnableCorsAttribute("https://localhost:44311/", "*", "GET,POST");
            EnableCorsAttribute cors = new EnableCorsAttribute("http://directoryapi.p2pdentist.com/", "*", "GET,POST");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                              .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));
           
        }
    }
}
