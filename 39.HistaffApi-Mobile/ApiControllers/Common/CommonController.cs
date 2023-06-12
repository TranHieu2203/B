using HiStaffAPI.AppCommon;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.Config;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Attributes;
using HiStaffAPI.CommonBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

/// <summary>
/// Created by: hoadm
/// Created date: 2020/10/09
/// </summary>
namespace HiStaffAPI.ApiControllers.Common
{
    [PortalAuthen]
    [RoutePrefix(ApiName.PrefixApiCommon)]
    public partial class CommonController : ApiController
    {
        public CommonController()
        {
        }

        [HttpGet]
        [Route(ApiName.GetOrganizationAll)]
        public async Task<IHttpActionResult> GetOrganizationAllAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<OrganizationDTO>>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();
                var data = await commonBusinessClient.GetOrganizationAllAsync();
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


        //[HttpGet]
        //[Route(ApiName.GetOrganizationLocationTreeView)]
        //public async Task<IHttpActionResult> GetOrganizationLocationTreeViewAsync(BaseRequest request)
        //{
        //    var response = new BaseJsonResponse<List<OrganizationDTO>>();
        //    try
        //    {
        //        using var commonBusinessClient = new CommonBusinessClient();
        //        var data = await commonBusinessClient.GetOrganizationLocationTreeViewAsync(request.Lang);
        //        response.Status = true;
        //        response.Error = HttpStatusCode.OK.ToString();
        //        response.Data = data;
        //        return Json(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = HttpStatusCode.BadRequest.ToString();
        //        response.Message = ex.Message;
        //        return Json(response);
        //    }
        //}


        [HttpGet]
        [Route(ApiName.GetOrganizationStructureInfo)]
        public async Task<IHttpActionResult> GetOrganizationStructureInfoAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<OrganizationStructureDTO>>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();
                var data = await commonBusinessClient.GetOrganizationStructureInfoAsync(request.OrgId);
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
        [Route(ApiName.GetPassword)]
        public async Task<IHttpActionResult> GetPasswordAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<string>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();
                var data = await commonBusinessClient.GetPasswordAsync(request.UserName);
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


        [HttpPost]
        [Route(ApiName.ChangeUserPassword)]
        public async Task<IHttpActionResult> ChangeUserPasswordAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();
                TokenApiDTO token = null;
                UserLog log = null;
                try { token = RequestHelper.GetTokenInfo(); } catch { }
                if (token != null)
                {
                    log = GetUserLog(token);
                }
                else
                {
                    log = new UserLog() { ComputerName = "API", Ip = RequestHelper.GetIPAddress(), Username = request.UserName };
                }
                var data = await commonBusinessClient.ChangeUserPasswordAsync(request.UserName, request.PasswordOld, request.Password, log);
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

        [AllowAnonymous]
        [HttpPost]
        [Route(ApiName.GetUserWithPermission)]
        public async Task<IHttpActionResult> GetUserWithPermission(string userName)
        {
            var response = new BaseJsonResponse<UserDTO>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();
                var data = await commonBusinessClient.GetUserWithPermisionAsync(userName);
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

        [AllowAnonymous]
        [HttpPost]
        [Route(ApiName.ChangePasswordRequest)]
        public async Task<IHttpActionResult> ChangePasswordRequest(ForgotPasswordModel request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var commonBusinessClient = new CommonBusinessClient();

                //Kiểm tra và xác nhận email và user name trùng trong DB
                //GetUserWithPermision
                var userInfo = await commonBusinessClient.GetUserWithPermisionAsync(request.UserName);
                if (userInfo == null) return Json(new BaseJsonResponse { Message = ("Không đúng thông tin người dùng") });
                if (userInfo.EMAIL.ToUpper() != request.Email.ToUpper()) return Json(new BaseJsonResponse { Message = ("Không đúng thông tin người dùng") });

                //Gửi email link xác nhận
                string subjectTemp = @"{0}, here's the link to reset your password";
                string contentTemp = @"
Hi {0}<br/>
Reset your password, and we'll get you on your way.<br/>
To change your HiStaff password, click the link below.<br/>
<a href='{1}'>Reset my password<a/><br/>
This link will expire in {2} minutes, so be sure to use it right away.<br/>
Thank you for using HiStaff!<br/>
TVC Team";

                var data = await commonBusinessClient.SendMailConfirmUserPasswordAsync(
                    new List<decimal>() { userInfo.ID },
                    string.Format(subjectTemp, userInfo.FULLNAME),
                    string.Format(contentTemp, userInfo.FULLNAME, ApiHelper.PortalUrl + request.ActionLink, request.ExpiryMinutes));

                if (data)
                {
                    response.Status = true;
                    response.Error = HttpStatusCode.OK.ToString();
                    response.Data = data;
                }
                else
                {
                    response.Status = false;
                    response.Error = HttpStatusCode.InternalServerError.ToString();
                    response.Data = data;
                }
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }


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