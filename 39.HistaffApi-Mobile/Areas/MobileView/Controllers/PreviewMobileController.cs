using HiStaffAPI.AppConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView.Controllers
{
    [AllowAnonymous]
    public class PreviewMobileController : Controller
    {
        // GET: MobileView/PreviewMobile
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Preview(string tokenstring, string typetest)
        {
            HttpContext.Session[SessionName.User_Authorize] = tokenstring;
            switch (typetest.ToUpper())
            {
                case "TIMESHEET":
                    //return Redirect("/MobileView/TimeSheet/MonthDetail");
                    return Redirect("/API/MobileOM/MobileView/TimeSheet/MonthDetail");
                case "PAYSLIP":
                    return Redirect("/API/MobileOM/MobileView/PayslipMobile/payslip");
                default:
                    return View();
            }
        }

    }
}