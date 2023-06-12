using HiStaffAPI.Attributes;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView.Controllers
{
    [MobileAuthen]
    public class DashboardChartController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
}