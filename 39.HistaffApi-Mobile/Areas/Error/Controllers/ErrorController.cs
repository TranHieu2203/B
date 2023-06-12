using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.Error.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error/Error
        public ActionResult Index()
        {
            if (RouteData.Values["exception"] != null)
            {
                Exception ex = (Exception)RouteData.Values["exception"];
                ViewData["Message"] = ex.Message;
            }
            else
            {
                ViewData["Message"] = "Không có thông tin exception";
            }
            return View("Index500");
        }

    }
}