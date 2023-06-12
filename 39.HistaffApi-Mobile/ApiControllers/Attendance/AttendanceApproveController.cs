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
        #region Phê duyệt thời giờ

        [HttpPost]
        [Route(ApiName.GetListWaitingForApprove)]
        public async Task<IHttpActionResult> GetListTaskApproveAsync(BaseRequest<ATRegSearchDTO> request)
        {
            var response = new BaseJsonResponse<List<AT_PORTAL_REG_DTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.GetListWaitingForApproveAsync(request.EmployeeId, request.Process, request.Filter);
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
        [Route(ApiName.GetApproveStatusList)]
        public async Task<IHttpActionResult> GetApproveStatusListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<AT_LISTPARAM_SYSTEAMDTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var a = new List<AT_LISTPARAM_SYSTEAMDTO>();
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = -1, NAME = (request.Lang != "vi-VN" ? "All" : "Tất cả") });
                if (request.IsCreated) a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 0, NAME = (request.Lang != "vi-VN" ? "Initialized" : "Khởi tạo") });
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 1, NAME = (request.Lang != "vi-VN" ? "Pending" : "Chờ phê duyệt") });
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 2, NAME = (request.Lang != "vi-VN" ? "Approved" : "Phê duyệt") });
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 3, NAME = (request.Lang != "vi-VN" ? "Rejected" : "Từ chối") });

                //var data = await attendanceBusinessClient.GetApproveStatusListAsync(request.Lang, request.IsCreated);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = a;
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
        [Route(ApiName.GetRegisterTypeList)]
        public async Task<IHttpActionResult> GetRegisterTypeListAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<AT_LISTPARAM_SYSTEAMDTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var a = new List<AT_LISTPARAM_SYSTEAMDTO>();
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 1, CODE = "LEAVE", NAME = (request.Lang == "vi-VN" ? "Đăng ký nghỉ" : "Leave") });
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 2, CODE = "OVERTIME", NAME = (request.Lang == "vi-VN" ? "Đăng ký làm thêm" : "Overtime") });
                a.Add(new AT_LISTPARAM_SYSTEAMDTO { ID = 3, CODE = "WLEO", NAME = (request.Lang == "vi-VN" ? "Đăng ký đi muộn, về sớm" : "Late-in, early-out") });

                //var data = await attendanceBusinessClient.GetRegisterTypeListAsync(request.Lang);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = a;
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
        [Route(ApiName.GetListRegisterDMVSApprove)]
        public async Task<IHttpActionResult> GetListRegisterDMVSApproveAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);
                var data = await attendanceBusinessClient.GetListWaitingForApproveAsync(
                    token.EMPLOYEE_ID.Value,
                    "WLEO",
                    new ATRegSearchDTO() { Status = 1, FromDate = request.StartDate.Value, ToDate = request.EndDate.Value }
                    );

                //.GetListRegisterDMVSApproveAsync(request.EmployeeId, request.StartDate.Value, request.EndDate.Value, request.Type, request.Color, request.Lang, request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ToList().DataSet;
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
        [Route(ApiName.GetListRegisterLeaveApprove)]
        public async Task<IHttpActionResult> GetListRegisterLeaveApproveAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);
                var data = await attendanceBusinessClient.GetListWaitingForApproveAsync(
                    token.EMPLOYEE_ID.Value,
                    "LEAVE",
                    new ATRegSearchDTO() { Status = 1, FromDate = request.StartDate.Value, ToDate = request.EndDate.Value }
                    );

                //var data = await attendanceBusinessClient.GetListRegisterLeaveApproveAsync(request.EmployeeId, request.StartDate.Value, request.EndDate.Value, request.Lang, request.Color, request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ToList().DataSet;
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
        [Route(ApiName.GetListRegisterOTApprove)]
        public async Task<IHttpActionResult> GetListRegisterOTApproveAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);
                var data = await attendanceBusinessClient.GetListWaitingForApproveAsync(
                    token.EMPLOYEE_ID.Value,
                    "OVERTIME",
                    new ATRegSearchDTO() { Status = 1, FromDate = request.StartDate.Value, ToDate = request.EndDate.Value }
                    );

                //var data = await attendanceBusinessClient.GetListRegisterOTApproveAsync(request.EmployeeId, request.StartDate.Value, request.EndDate.Value, request.Color, request.Lang, request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ToList().DataSet;
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
        [Route(ApiName.GetRegisterOTPortal)]
        public async Task<IHttpActionResult> GetRegisterOTPortalAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<AT_PORTAL_REG_DTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);
                var lstTypesCombo = await attendanceBusinessClient.GetComboboxDataAsync(new GetComboboxDataRequest()
                {
                    cbxData = new ComboBoxDataDTO() { GET_LIST_TYPE_OT = true }
                });
                var lstTypes = new List<decimal>();
                lstTypes.AddRange(lstTypesCombo.cbxData.LIST_LIST_TYPE_OT.Select(m => m.ID).ToList());
                var lstIdSign = new List<AT_TIME_MANUALDTO>();
                foreach (var o in lstTypes)
                {
                    lstIdSign.Add(new AT_TIME_MANUALDTO() { ID = o });
                }

                //var getData = await attendanceBusinessClient.GetRegisterAppointmentInPortalByEmployeeAsync(
                //    token.EMPLOYEE_ID.Value,
                //    request.StartDate.Value,
                //    request.EndDate.Value,
                //    lstIdSign,
                //   new List<short>() { 0, 1, 2, 3 }
                //    );

                //var data = await attendanceBusinessClient.GetRegisterOTPortalAsync(request.StartDate.Value, request.EndDate.Value, request.Lang);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                //response.Data = getData.ToList().ToList<AT_PORTAL_REG_DTO>();
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
        [Route(ApiName.GetRegisterLeavePortal)]
        public async Task<IHttpActionResult> GetRegisterLeavePortalAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<AT_PORTAL_REG_DTO>>();
            try
            {
                //using var attendanceBusinessClient = new AttendanceBusinessClient();
                //var token = RequestHelper.GetTokenInfo();
                //var userLog = GetUserLog(token);
                //var lstTypesCombo = await attendanceBusinessClient.GetComboboxDataAsync(new GetComboboxDataRequest()
                //{
                //    cbxData = new ComboBoxDataDTO() { GET_LIST_TYPE_MANUAL_LEAVE = true }
                //});
                //var lstTypes = new List<decimal>();
                //lstTypes.AddRange(lstTypesCombo.cbxData.LIST_LIST_TYPE_MANUAL_LEAVE.Select(m => m.ID).ToList());
                //var lstIdSign = new List<AT_TIME_MANUALDTO>();
                //foreach (var o in lstTypes)
                //{
                //    lstIdSign.Add(new AT_TIME_MANUALDTO() { ID = o });
                //}

                //var getData = await attendanceBusinessClient.GetRegisterAppointmentInPortalByEmployeeAsync(
                //    token.EMPLOYEE_ID.Value,
                //    request.StartDate.Value,
                //    request.EndDate.Value,
                //    lstIdSign,
                //   new List<short>() { 0, 1, 2, 3 }
                //    );

                ////.GetRegisterLeavePortalAsync(request.StartDate.Value, request.EndDate.Value, request.Lang);
                //response.Status = true;
                //response.Error = HttpStatusCode.OK.ToString();
                //response.Data = getData.ToList().ToList<AT_PORTAL_REG_DTO>();
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
        [Route(ApiName.ApproveRegisterPortal)]
        public async Task<IHttpActionResult> ApproveRegisterPortalAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.ApprovePortalRegisterAsync(
                    Guid.Parse(request.IdString),
                    request.EmployeeId,
                    request.StatusId.ToInt().Value,
                    request.Note, request.Url, request.Process, userLog
                    );
                if (data)
                {
                    //send notification
                    await NotifyModule.GetAndSendNotification(true, request.Id.ToString(), request.Process);
                }

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


        #endregion

    }

}