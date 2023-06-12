using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Extension;
using HiStaffAPI.ProfileBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace HiStaffAPI.ApiControllers.Profile
{
    public partial class ProfileController 
    {
        #region Danh bạ nhân viên

        /// <summary>
        /// Nghiệp vụ OM có thêm danh bạ -> ko viết thêm WCF nên convert tạm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route(ApiName.GetListJobPosition)]
        //public async Task<IHttpActionResult> GetListJobPositionAsync(OMProfileBusiness.GetListJobPositionRequest request)
        //{
        //    var response = new BaseJsonResponse<List<OMProfileBusiness.PositionChartDTO>>();
        //    try
        //    {
        //        using var profileBusinessClient = new ProfileBusinessClient();

        //        var data = await profileBusinessClient.GetEmployeeOrgChartAsync(new List<decimal>() { request._filter.ORG_ID.Value }, GetUserLog(RequestHelper.GetTokenInfo()));
        //        response.Status = true;
        //        response.Error = HttpStatusCode.OK.ToString();
        //        response.Data = data.ToList().ToList<OMProfileBusiness.PositionChartDTO>();
        //        response.Total = data.Count;
        //        return Json(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = HttpStatusCode.BadRequest.ToString();
        //        response.Message = ex.Message;
        //        return Json(response);
        //    }
        //}

        /// <summary>
        /// Nghiệp vụ Portal OM thêm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route(ApiName.GetChartJobPosition)]
        //public async Task<IHttpActionResult> GetChartJobPositionAsync(OMProfileBusiness.GetChartJobPositionRequest request)
        //{
        //    var response = new BaseJsonResponse<List<OMProfileBusiness.PositionChartDTO>>();
        //    try
        //    {
        //        using var profileBusinessClient = new ProfileBusinessClient();

        //        var data = await profileBusinessClient.GetEmployeeOrgChartAsync(new List<decimal>() { request.Org_ID }, GetUserLog(RequestHelper.GetTokenInfo()));
        //        response.Status = true;
        //        response.Error = HttpStatusCode.OK.ToString();
        //        response.Data = data.ToList().ToList<OMProfileBusiness.PositionChartDTO>();
        //        response.Total = data.Count;
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

    }

}