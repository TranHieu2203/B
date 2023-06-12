using HiStaffAPI.AppCommon.Notification;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace HiStaffAPI.ApiControllers.Attendance
{
    public partial class AttendanceController
    {
        #region Đăng ký thời giờ

        [HttpPost]
        [Route(ApiName.DeletePortalRegisterNew)]
        public async Task<IHttpActionResult> DeletePortalRegisterNewAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<string>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = false;
                foreach (var m in request.Ids)
                {
                    data = await attendanceBusinessClient.DeletePortalRegisterAsync(m);
                };
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data ? "Success" : "Something is wrong. Delete data is failed";
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
        [Route(ApiName.SendRegisterToApprove)]
        public async Task<IHttpActionResult> SendRegisterToApproveAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<string>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.SendRegisterToApproveAsync(request.Ids, request.Process, request.Url);
                if (string.IsNullOrEmpty(data))
                {
                    //send notification
                    foreach (decimal d in request.Ids)
                    {
                        await NotifyModule.GetAndSendNotification(true, d.ToString(), request.Process);
                    }
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
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

      
        [HttpPost]
        [Route(ApiName.CheckPeriodClose)]
        public async Task<IHttpActionResult> CheckPeriodCloseAsync(CheckPeriodCloseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.CheckPeriodCloseAsync(request);
                response.Status = data.CheckPeriodCloseResult; ;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.CheckPeriodCloseResult;
                response.Message = data.sAction;
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
        [Route(ApiName.GetHolidayByCalender)]
        public async Task<IHttpActionResult> GetHolidayByCalenderAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<DateTime>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.GetHolidayByCalenderAsync(request.StartDate.Value, request.EndDate.Value);
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
        [Route(ApiName.GetPhepNam)]
        public async Task<IHttpActionResult> GetPhepNamAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<AT_ENTITLEMENTDTO>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.GetPhepNamAsync(request.EmployeeId, request.Year);
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
        [Route(ApiName.GetTotalLeaveInYear)]
        public async Task<IHttpActionResult> GetTotalLeaveInYearAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<decimal>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data =  await attendanceBusinessClient.GetTotalLeaveInYearAsync(request.EmployeeId, request.Year);
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
        [Route(ApiName.GetTotalPHEPBU)]
        public async Task<IHttpActionResult> GetTotalPHEPBUAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<decimal>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data =  await attendanceBusinessClient.GetTotalPHEPBUAsync(request.EmployeeId.ToInt(0).Value, request.Year.ToInt(0).Value, request.TypeId.ToInt(0).Value);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                if (data != null && data.Rows.Count > 0)
                {
                    response.Data = data.Rows[0][0].ToDecimal(0).Value;
                }
                else
                {
                    response.Data = 0;
                }
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
        [Route(ApiName.CheckExitRegister)]
        public async Task<IHttpActionResult> CheckExitRegisterAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);

                //var checkExists = await attendanceBusinessClient.GetRegisterAppointmentInPortalByEmployeeAsync(
                //    token.EMPLOYEE_ID.Value,
                //    request.StartDate.Value,
                //    request.StartDate.Value,
                //    new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = request.TypeId } },
                //    new List<short>() { 0, 1, 2 }
                //    );

                //var data = await attendanceBusinessClient.CheckExitRegisterAsync(request.StartDate.Value, request.IdString, request.Type);
                //response.Status = checkExists.Count > 0;
                //response.Error = HttpStatusCode.OK.ToString();
                //response.Data = checkExists.Count > 0;
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
        [Route(ApiName.CheckDataOTByShift)]
        public async Task<IHttpActionResult> CheckDataOTByShiftAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                //Check core non OM ko có -> luôn trả về false
                var data = false;//await attendanceBusinessClient.CheckDataOTByShiftAsync(request.EmployeeId, request.StartDate.Value, request.StartHour, request.EndHour);
                response.Status = data;
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
        [Route(ApiName.CheckOTRegisterOverlap)]
        public async Task<IHttpActionResult> CheckOTRegisterOverlapAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                //var checkExists = await attendanceBusinessClient.GetRegisterAppointmentInPortalOTAsync(
                //    request.EmployeeId,
                //    request.StartDate.Value,
                //    request.StartDate.Value,
                //    new List<OT_OTHERLIST_DTO>() { new OT_OTHERLIST_DTO() { ID = request.TypeId } },
                //    new List<short>() { 0, 1, 2 }
                //    );

                //var data = await attendanceBusinessClient.CheckOTRegisterOverlapAsync(request.EmployeeId, request.StartDate.Value, request.StartHour, request.EndHour);
                //response.Status =  checkExists.Count > 0;
                response.Error = HttpStatusCode.OK.ToString();
                //response.Data = checkExists.Count > 0;
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
        [Route(ApiName.GetTotalPHEPNAM)]
        public async Task<IHttpActionResult> GetTotalPHEPNAMAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataTable>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                //var data =await attendanceBusinessClient.GetTotalPHEPNAMAsync(request.EmployeeId.ToInt(0).Value, request.Year.ToInt(0).Value, request.TypeId.ToInt(0).Value);
                var data = new DataTable ();
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

        #endregion
    }

}