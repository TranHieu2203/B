using System.Web.Http;

/// <summary>
/// Created by: hoadm
/// Created date: 2020/10/09
/// </summary>
namespace HiStaffAPI.ApiControllers
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        public HomeController()
        {
        }

        [Route("health-check")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult HealthCheck()
        {
            return Json($"health-check Ok!");
        }
    }
}