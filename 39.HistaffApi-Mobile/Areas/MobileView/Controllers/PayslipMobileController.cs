using HiStaffAPI.AppConstants;
using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Areas.MobileView.Models;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.Attributes;
using HiStaffAPI.CommonBusiness;
using HiStaffAPI.PayrollBusiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView.Controllers
{
    [MobileAuthen]
    public class PayslipMobileController : Controller
    {
       
        public PayslipMobileController() { }
        #region Thông tin thu nhập

        [Route("payslip")]
        public async Task<ActionResult> Payslip()
        {
            try
            {
                ViewData["lang"] = "vi-VN";
                List<SelectListItem> yearOptions = new List<SelectListItem>();
                for (int i = 2015; i <= DateTime.Now.Year + 1; i++)
                {
                    yearOptions.Add(new SelectListItem
                    {
                        Text = i.ToString(),
                        Value = i.ToString(),
                        Selected = i == DateTime.Now.Year
                    });
                }
                decimal? periodId = null;

                using var payrollBusinessClient = new PayrollBusinessClient();
                
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


                var dataPeriod = await payrollBusinessClient.GetPeriodbyYearAPIAsync(
                    new PayrollBusiness.TokenDTO()
                    {
                        DeviceID = Session[SessionName.User_DeviceID].ToString(),
                        Username = tokenModel.Username,
                        Token = tokenModel.Token,
                    }, DateTime.Now.Year);

                if (dataPeriod != null)
                {
                    var period = dataPeriod.FirstOrDefault(s => s.START_DATE.Value.ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"));
                    if (period != null)
                    {
                        periodId = period.ID;
                    }
                }

                return View("PayslipMobile", new PayslipResponse
                {
                    Year = DateTime.Now.Year,
                    YearOptions = yearOptions,
                    PeriodId = periodId,
                    PeriodIdOptions = dataPeriod.AsQueryable().Select(m => new SelectListItem() { Value = m.ID.Value.ToString(), Text = m.PERIOD_NAME, Selected = m.ID == periodId })
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Route("GetData")]
        public async Task<ActionResult> GetPayslip(PayslipRequest request)
        {
            try
            {
                if (request == null || request.PeriodId == null || request.PeriodId == 0)
                {
                    throw new WrongInputException("PeriodId is not null");
                }

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


                using var payrollBusinessClient = new PayrollBusinessClient();
                var data = await payrollBusinessClient.GetPayrollSheetSumSheetAPIAsync(
                    new PayrollBusiness.TokenDTO() { Token = tokenModel.Token, Username = currentUser.USERNAME }, (int)request.PeriodId.Value, currentEmp.ID.ToString(),
                    new PayrollBusiness.UserLog()
                    {
                        ActionName = "payslip/get",
                        EMPLOYEE_ID = currentEmp.ID,
                        Username = currentUser.USERNAME
                    }, "CREATED_DATE");


                if (data == null)
                {
                    data = new DataTable();
                }

                var keys = new List<KeyValuePair<string, object>>();
                foreach (DataRow row in data.Rows)
                {
                    foreach (DataColumn col in data.Columns)
                    {
                        keys.Add(new KeyValuePair<string, object>(
                            col.ColumnName.ToLower(),
                            row[col] == DBNull.Value ? null : row[col]
                        ));
                    }
                }
                
                return Json(keys);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Route("payslip/get-period-by-year")]
        public async Task<ActionResult> GetPayslipPeriodByYear(decimal? year)
        {
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


                using var attendanceBusinessClient = new AttendanceBusinessClient();
                var data = await attendanceBusinessClient.LOAD_PERIODBylinqAPIAsync(new AttendanceBusiness.TokenDTO() { Token = tokenModel.Token, Username = tokenModel.Username },
                    new AT_PERIODDTO() { YEAR = year.Value },
                   new AttendanceBusiness.UserLog()
                   {
                       ActionName = "GetPayslipPeriodByYear",
                       EMPLOYEE_ID = currentEmp.ID,
                       Username = currentUser.USERNAME
                   });
                return Json(data);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex.Message);
                throw ex;
            }
        }

        #endregion


    }
}