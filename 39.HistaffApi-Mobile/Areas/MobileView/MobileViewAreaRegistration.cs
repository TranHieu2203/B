using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView
{
    public class MobileViewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MobileView";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MobileView_default",
                "MobileView/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}