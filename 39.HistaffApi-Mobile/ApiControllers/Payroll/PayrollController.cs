using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.PayrollBusiness;
using HiStaffAPI.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HiStaffAPI.Attributes;

namespace HiStaffAPI.ApiControllers.Payroll
{

    [PortalAuthen]
    [RoutePrefix(ApiName.PrefixApiPayroll)]
    public partial class PayrollController : ApiController
    {
        public PayrollController()
        {
        }


        #region Thông tin thu nhập

        [HttpGet]
        [Route(ApiName.GetPeriodbyYear)]
        public async Task<IHttpActionResult> GetPeriodbyYearAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<ATPeriodDTO>>();
            try
            {
                using var payrollBusinessClient = new PayrollBusinessClient();
                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await payrollBusinessClient.GetPeriodbyYearAsync(request.Year);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                var lstData = new List<ATPeriodDTO>();
                lstData.AddRange(data);
                response.Data = lstData;
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
        [Route(ApiName.GetPayrollSheetSumSheet)]
        public async Task<IHttpActionResult> GetPayrollSheetSumSheetAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataTable>();
            try
            {
                using var payrollBusinessClient = new PayrollBusinessClient();
                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                //var data = await payrollBusinessClient.GetPayrollSheetSumSheetAsync(request.PeriodId.ToInt(0).Value, request.EmployeeId.ToString(), userLog);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                //response.Data = data;
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