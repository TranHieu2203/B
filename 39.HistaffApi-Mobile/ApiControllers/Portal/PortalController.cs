using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.Attributes;
using HiStaffAPI.PortalBusiness;
using System.Web.Http;


namespace HiStaffAPI.ApiControllers.Portal
{
    [PortalAuthen]
    [RoutePrefix(ApiName.PrefixApiPortal)]
    public partial class PortalController : ApiController
    {
        public PortalController()
        {
        }


        #region Danh sách đầu việc

        //[HttpGet]
        //[Route(ApiName.GetATOfferJobList)]
        //public async Task<IHttpActionResult> GetATOfferJobListAsync(GetATOfferJobListRequest request)
        //{
        //    var response = new BaseJsonResponse<List<AT_OFFER_JOB_LISTDTO>>();
        //    try
        //    {
        //        using var portalBusinessClient = new PortalBusinessClient();
        //        var userLog = GetUserLog(RequestHelper.GetTokenInfo());
        //        var data = await portalBusinessClient.GetATOfferJobListAsync(request);
        //        response.Status = true;
        //        response.Error = HttpStatusCode.OK.ToString();
        //        response.Data = data.GetATOfferJobListResult;
        //        return Json(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = HttpStatusCode.BadRequest.ToString();
        //        response.Message = ex.Message;
        //        return Json(response);
        //    }
        //}

        #endregion

        public UserLog GetUserLog(TokenApiDTO token)
        {
            return new UserLog
            {
                ActionName = token.ActionName,
                Username = token.Username
            };
        }
    }

}