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
        #region Quản lý tuyển dụng

       
        [HttpGet]
        [Route(ApiName.GetTitleByTitleID)]
        public async Task<IHttpActionResult> GetTitleByTitleIDAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<TitleDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetTitleByIDAsync(request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.FirstOrDefault();
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetOrgOMByID)]
        public async Task<IHttpActionResult> GetOrgOMByIDAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<OrganizationDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetOrganizationByIDAsync(request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.CheckIsOwner)]
        public async Task<IHttpActionResult> CheckIsOwnerAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                //var data = await profileBusinessClient.CheckIsOwnerAsync(request.OrgId);
                response.Status = false;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = false;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.ModifyTitleById)]
        public async Task<IHttpActionResult> ModifyTitleByIdAsync(BaseRequest<TitleDTO> request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.ModifyTitleAsync(new ModifyTitleRequest() { log = userLog, gID = 0, objTitle = request.Filter });
                response.Status = data.ModifyTitleResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ModifyTitleResult;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.InsertTitleNB)]
        public async Task<IHttpActionResult> InsertTitleNBAsync(BaseRequest<TitleDTO> request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.InsertTitleAsync(new InsertTitleRequest() { log = userLog, objTitle = request.Filter, gID = 0 });//.InsertTitleNBAsync(request.Filter, request.OrgId, request.AddressId, userLog);
                response.Status = data.InsertTitleResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.InsertTitleResult;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        #endregion

    }

}