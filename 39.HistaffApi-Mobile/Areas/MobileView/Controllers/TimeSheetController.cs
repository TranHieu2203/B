using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.Attributes;
using HiStaffAPI.CommonBusiness;
using HiStaffAPI.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView.Controllers
{
    [MobileAuthen]
    public class TimeSheetController : Controller
    {
        public TimeSheetController() { }
        // GET: MobileView/TimeSheet
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Return view
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MonthDetail()
        {
            var lstEmp = new List<EmployeeDTO>();
            EmployeeDTO currentEmp = new EmployeeDTO();
            UserDTO currentUser = new UserDTO();
            lstEmp.Add(currentEmp);
            //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
            using (var repCommon = new CommonBusinessClient())
            {
                currentUser = await repCommon.GetUserWithPermisionAsync(Session[SessionName.User_UserName].ToString());
                currentEmp.FULLNAME_VN = currentUser.FULLNAME;
                currentEmp.ID = (decimal)currentUser.EMPLOYEE_ID;
                currentEmp.EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE;
            }

            var tokenModel = TokenHelper.GenerateToken("MobileOM",
               currentUser.USERNAME,
               currentUser.EMPLOYEE_ID,
               currentUser.EMPLOYEE_CODE,
                "login", "", true, Session[SessionName.User_DeviceID].ToString());
            var atToken = new AttendanceBusiness.TokenDTO()
            {
                Username = currentUser.USERNAME,
                EMPLOYEE_ID = currentUser.EMPLOYEE_ID,
                EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE,
                Token = tokenModel.Token
            };
            using var attendanceBusinessClient = new AttendanceBusinessClient();

            //Lấy ds loại nghỉ đưa vào DataSet
            var lstTypeLeave = new AttendanceBusiness.ComboBoxDataDTO() { GET_GSIGN = true, GET_LIST_SIGN = true };
            var types = await attendanceBusinessClient.GetComboboxDataAPIAsync(new GetComboboxDataAPIRequest(atToken, lstTypeLeave));
            var rsList = types.cbxData.LIST_GSIGN.Select(p => new { CODE = p.CODE, NAME = p.NAME }).ToList();
            types.cbxData.LIST_LIST_SIGN.ForEach(m => rsList.Add(new { CODE = m.CODE, NAME = m.NAME_VN }));
            ViewData["LIST_TYPE_LEAVE"] = rsList;

            return View("MonthDetail");
        }


        [HttpPost]
        [Route(ApiName.GetMyTimeSheetPortalByMonth)]
        public async Task<string> GetMyTimeSheetPortalByMonth(GetTimeSheetByMonthRequest request)
        {
            var response = new BaseJsonResponse<DataSet>();
            try
            {
                var lstEmp = new List<EmployeeDTO>();
                EmployeeDTO currentEmp = new EmployeeDTO();
                UserDTO currentUser = new UserDTO();
                lstEmp.Add(currentEmp);
               
                //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
                using (var repCommon = new CommonBusinessClient())
                {
                    currentUser = await repCommon.GetUserWithPermisionAsync(Session[SessionName.User_UserName].ToString());
                    currentEmp.FULLNAME_VN = currentUser.FULLNAME;
                    currentEmp.ID = (decimal)currentUser.EMPLOYEE_ID;
                    currentEmp.EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE;
                }

                var tokenModel = TokenHelper.GenerateToken("MobileOM",
                   currentUser.USERNAME,
                   currentUser.EMPLOYEE_ID,
                   currentUser.EMPLOYEE_CODE,
                    "login", "", true, Session[SessionName.User_DeviceID].ToString());
                var atToken = new AttendanceBusiness.TokenDTO()
                {
                    Username = currentUser.USERNAME,
                    EMPLOYEE_ID = currentUser.EMPLOYEE_ID,
                    EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE,
                    Token = tokenModel.Token
                };
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var data = await attendanceBusinessClient.GetMyTimeSheetPortal_ByMonthAsync(
                   atToken,
                    request.Year.ToInt(0).Value, request.Month, request.Lang);

                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Newtonsoft.Json.JsonConvert.SerializeObject(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Newtonsoft.Json.JsonConvert.SerializeObject(response);
            }
        }

        [HttpPost]
        [Route(ApiName.GetTotalTimeSheetPortalByMonth)]
        public async Task<string> GetTotalTimeSheetPortalByMonth(GetTimeSheetByMonthRequest request)
        {

            var response = new BaseJsonResponse<List<AT_TIME_TIMESHEET_MONTHLYDTO>>();
            try
            {
                var lstEmp = new List<EmployeeDTO>();
                EmployeeDTO currentEmp = new EmployeeDTO();
                UserDTO currentUser = new UserDTO();
                lstEmp.Add(currentEmp);
              
                //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
                using (var repCommon = new CommonBusinessClient())
                {
                    currentUser = await repCommon.GetUserWithPermisionAsync(Session[SessionName.User_UserName].ToString());
                    currentEmp.FULLNAME_VN = currentUser.FULLNAME;
                    currentEmp.ID = (decimal)currentUser.EMPLOYEE_ID;
                    currentEmp.EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE;
                }

                var tokenModel = TokenHelper.GenerateToken("MobileOM",
                   currentUser.USERNAME,
                   currentUser.EMPLOYEE_ID,
                   currentUser.EMPLOYEE_CODE,
                    "login", "", true, Session[SessionName.User_DeviceID].ToString());
                var atToken = new AttendanceBusiness.TokenDTO()
                {
                    Username = currentUser.USERNAME,
                    EMPLOYEE_ID = currentUser.EMPLOYEE_ID,
                    EMPLOYEE_CODE = currentUser.EMPLOYEE_CODE,
                    Token = tokenModel.Token
                };
                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var data = await attendanceBusinessClient.GetTimeSheetPortal_ByMonthAsync(
                   atToken,
                    request.Year.ToInt(0).Value, request.Month, request.Lang, new AttendanceBusiness.UserLog() { EMPLOYEE_ID = currentUser.EMPLOYEE_ID });

                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return JsonConvert.SerializeObject(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Newtonsoft.Json.JsonConvert.SerializeObject(response);
            }
        }
    }
}