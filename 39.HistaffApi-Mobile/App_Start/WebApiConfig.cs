using HiStaffAPI.AppConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HiStaffAPI.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            AreaRegistration.RegisterAllAreas();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "{controller}/{action}/{id}"
             );
            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: ApiName.PrefixApi + "/{controller}"
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: ApiName.PrefixApiSystem + "/{controller}"
            );
            config.Routes.MapHttpRoute(
               name: "DefaultApi4",
               routeTemplate: ApiName.PrefixApiOM + "/{controller}"
           );
            config.Routes.MapHttpRoute(
                name: "DefaultApi5",
                routeTemplate: ApiName.PrefixApiOMSystem + "/{controller}"
            );

        }
    }
}