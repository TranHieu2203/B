using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Attributes;
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
    [RoutePrefix(ApiName.PrefixApiProfile)]
    [PortalAuthen]
    public partial class ProfileController : ApiController
    {
        public ProfileController()
        {
        }

        [HttpPost]
        [Route(ApiName.CheckPermissionByPosition)]
        public async Task<IHttpActionResult> CheckPermissionByPositionAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<decimal>>();
            try
            {
                //request.Ids position id -> ignore
                //request.OrgId -> input param check theo userid 
                using var commonRep = new CommonBusiness.CommonBusinessClient();
                //var listOrgPermit = await commonRep.GetUserOrganizationAsync(RequestHelper.GetTokenInfo().UserId.ToDecimal(0).Value);
                //if (listOrgPermit.Contains(request.OrgId))
                //{
                //    response.Status = true;
                //    response.Error = HttpStatusCode.OK.ToString();
                //    response.Data = listOrgPermit;
                //}
                //else
                //{
                //    response.Status = true;
                //    response.Error = HttpStatusCode.OK.ToString();
                //    response.Data = listOrgPermit;
                //}

                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        #region Dùng chung

        [HttpGet]
        [Route(ApiName.GetOtherList)]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetOtherListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<SelectItemRequest>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();
                var data = await profileBusinessClient.GetOtherListAsync(request.Type, request.Lang, request.IsBlank);
                response.Status = true;
                response.Data = data.ToList<SelectItemRequest>();
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.GetProvinceList)]
        public async Task<IHttpActionResult> GetProvinceListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<SelectItemRequest>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetProvinceAsync(new GetProvinceRequest() { PageIndex = 0, PageSize = 63, _filter = new ProvinceDTO(), Sorts = "NAME_VN" });
                response.Data = (data.GetProvinceResult.Select(a => new SelectItemRequest() { ID = a.ID, NAME = request.Lang == "vi-VN" ? a.NAME_VN : a.NAME_EN })).ToList();
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.GetDistrictList)]
        public async Task<IHttpActionResult> GetDistrictListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<SelectItemRequest>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetDistrictByProvinceIDAsync(request.ProvinceId, "A");
                response.Data = (data.Select(a => new SelectItemRequest() { ID = a.ID, NAME = request.Lang == "vi-VN" ? a.NAME_VN : a.NAME_EN })).ToList();
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.GetWardList)]
        public async Task<IHttpActionResult> GetWardListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<SelectItemRequest>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetWardByDistrictIDAsync(request.DistrictId, "A");
                response.Data = (data.Select(a => new SelectItemRequest() { ID = a.ID, NAME = request.Lang == "vi-VN" ? a.NAME_VN : a.NAME_EN })).ToList();
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.GetComboList)]
        public async Task<IHttpActionResult> GetComboListAsync(GetComboListRequest request)
        {
            var response = new BaseJsonResponse<ComboBoxDataDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();
                var data = await profileBusinessClient.GetComboListAsync(request);
                response.Status = true;
                response.Data = data._combolistDTO;
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.AutoGenCodeHuTile)]
        public async Task<IHttpActionResult> AutoGenCodeHuTileAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<string>();
            try
            {
                //Case này ko sử dụng trên non OM
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.AutoGenCodeAsync("", request.TableName, request.ColumnName);
                response.Status = true;
                response.Data = data;
                response.Error = HttpStatusCode.OK.ToString();
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
        [Route(ApiName.GetOrgTree)]
        public async Task<IHttpActionResult> GetOrgTreeAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<OrganizationDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetOrgsTreeAsync();
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
        [Route(ApiName.GetComboboxOrgTree)]
        public async Task<IHttpActionResult> GetComboboxOrgTreeAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<OrganizationDTO>>();
            try
            {
                //Case này ko sử dụng trên non OM
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetOrgFromUsernameAsync(RequestHelper.GetTokenInfo().Username);
                var dataOrg = await profileBusinessClient.GetOrganizationAsync("");

                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = dataOrg.FindAll(m => m.ID == data.Value);
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