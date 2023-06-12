using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace HiStaffAPI.ApiControllers.Attendance
{
    public partial class AttendanceController
    {
        #region Bảng công nhóm

        [HttpPost]
        [Route(ApiName.LOAD_PERIODBylinq)]
        public async Task<IHttpActionResult> LOAD_PERIODBylinqAsync(AT_PERIODDTO request)
        {
            var response = new BaseJsonResponse<List<AT_PERIODDTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.LOAD_PERIODBylinqAsync(request, userLog);
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
        [Route(ApiName.GetTimeSheetPortal)]
        public async Task<IHttpActionResult> GetTimeSheetPortalAsync(GetTimeSheetPortalRequest request)
        {
            var response = new BaseJsonResponse<List<AT_TIME_TIMESHEET_MONTHLYDTO>>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());

                request.log = userLog;
                var data = await attendanceBusinessClient.GetTimeSheetPortalAsync(request);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.GetTimeSheetPortalResult;
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

        #region Bảng công cá nhân

        [HttpPost]
        [Route(ApiName.GetMyTimeSheetPortal)]
        public async Task<IHttpActionResult> GetMyTimeSheetPortalAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);

                //Lấy kỳ công hiện tại theo năm, tháng truyền vào

                var data = await attendanceBusinessClient.GetTimeSheetPortalAsync(
                    new GetTimeSheetPortalRequest()
                    {
                        log = userLog,
                        _filter = new AT_TIME_TIMESHEET_MONTHLYDTO()
                        {
                            EMPLOYEE_ID = token.EMPLOYEE_ID.Value,
                            PERIOD_ID = request.PeriodId,
                        },
                        PageIndex = 0,
                        PageSize = 100,
                        Sorts = "PERIOD_ID"
                    }
                    );
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                if (data.GetTimeSheetPortalResult.Count == 0) return Json(new BaseJsonResponse<DataSet>() { Status = false, Error = HttpStatusCode.InternalServerError.ToString() });
                var ds = new DataSet();
                ds.Tables.Add(data.GetTimeSheetPortalResult.ToList());
                response.Data = ds;

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
        [Route(ApiName.GetMyTimeSheetPortalByMonth)]
        public async Task<IHttpActionResult> GetMyTimeSheetPortalByMonthAsync([FromBody] GetTimeSheetByMonthRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var token = RequestHelper.GetTokenInfo();
                var userLog = GetUserLog(token);

                //Lấy kỳ công hiện tại theo năm, tháng truyền vào
                var periodAllInfo = await attendanceBusinessClient.GetAT_PERIODAsync();
                var periodCurrent = periodAllInfo.ToList<AT_PERIODDTO>().Find(m => m.YEAR == request.Year && m.START_DATE.Value.Month == request.Month);
                if (periodCurrent != null)
                {
                    var data = await attendanceBusinessClient.GetTimeSheetPortalAsync(
                        new GetTimeSheetPortalRequest()
                        {
                            log = userLog,
                            _filter = new AT_TIME_TIMESHEET_MONTHLYDTO()
                            {
                                EMPLOYEE_ID = token.EMPLOYEE_ID.Value,
                                PERIOD_ID = periodCurrent.ID
                            },
                            PageIndex = 0,
                            PageSize = 100,
                            Sorts = "PERIOD_ID"
                        }
                        );// .GetMyTimeSheetPortal_ByMonthAsync(request.Year.ToInt(0).Value, request.Month, request.Lang);
                    response.Status = true;
                    response.Error = HttpStatusCode.OK.ToString();
                    response.Data = data.GetTimeSheetPortalResult.ToList().DataSet;
                }
                else
                {
                    response.Status = false;
                    response.Error = HttpStatusCode.NotFound.ToString();
                    response.Message = "Period is not found";
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
        [Route(ApiName.LOAD_PERIODByID)]
        public async Task<IHttpActionResult> LOAD_PERIODByIDAsync(AT_PERIODDTO request)
        {
            var response = new BaseJsonResponse<AT_PERIODDTO>();
            try
            {
                using var attendanceBusinessClient = new AttendanceBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await attendanceBusinessClient.LOAD_PERIODByIDAsync(request, userLog);
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