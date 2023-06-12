using HiStaffAPI.AppCommon;
using HiStaffAPI.AppCommon.Config;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.MobileModel;
using HiStaffAPI.AppCommon.Notification;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.CommonBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace HiStaffAPI.ApiControllers.Mobile
{
    public partial class MobileController
    {
        /// <summary>
        /// Đăng ký nghỉ + gửi chờ duyệt - Chuyển xuống tầng business thực hiện
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dparam"></param>
        /// <param name="companyCode"></param>
        /// <param name="userId"></param>
        /// <param name="checkTokenObject"></param>
        /// <returns></returns>
        private async Task<JsonResult<JObject>> API_RegisterLeave(string functionName, JObject dparam, string companyCode, string userId, ExecuteResult checkTokenObject)
        {
            int error;
            string message;
            var authorObject = JObject.Parse(checkTokenObject.StringResult);
            string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();
            string devideId = authorObject[functionName]["ParameterOutput"]["DevideId"].ToString();
            //string devideId = "30281833-B62A-483E-B9A9-92013C2D1084";

            //if (checkTokenObject.BoolResult)
            //{
            //    var lstEmp = new List<EmployeeDTO>();
            //    EmployeeDTO currentEmp = new EmployeeDTO();
            //    lstEmp.Add(currentEmp);
            //    //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
            //    using (var repCommon = new CommonBusinessClient())
            //    {
            //        var userInfo = await repCommon.GetUserWithPermisionAsync(userName);
            //        currentEmp.FULLNAME_VN = userInfo.FULLNAME;
            //        currentEmp.ID = (decimal)userInfo.EMPLOYEE_ID;
            //        currentEmp.EMPLOYEE_CODE = userInfo.EMPLOYEE_CODE;
            //    }

            //    List<ParamInputStore> inputParas = JsonConvert.DeserializeObject<List<ParamInputStore>>(dparam["parameterInput"].ToString());

            //    var rsCheckStartDate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "LeaveFrom").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate);
            //    var rsCheckEnddate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "LeaveTo").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime toDate);

            //    if (!rsCheckStartDate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));
            //    if (!rsCheckEnddate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Đến ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));

            //    //Check ràng buộc theo nghiệp vụ nếu có
            //    if ((startDate - toDate).TotalMinutes > 0) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không được lớn hơn đến ngày", ResponseStatus = -600 }));

            //    if ((toDate - startDate).TotalDays > 7) return Json(JObject.FromObject(new ResponseExecute() { Message = "Khoảng thời gian đăng ký không được vượt quá 7 ngày.", ResponseStatus = -600 }));


            //    int SymbolId = int.Parse(inputParas.Find(m => m.ParamName == "SymbolId").InputValue);
            //    string Remark = (inputParas.Find(m => m.ParamName == "Remark").InputValue);

            //    using var repAtten = new AttendanceBusinessClient();
            //    #region "InsertREG"
            //    //Kiểm tra kỳ công đã đóng hay chưa
            //    var rsCheckPeriod = await repAtten.CheckPeriodCloseAsync(
            //            new CheckPeriodCloseRequest(lstEmp.Select(m => m.ID).ToList(), startDate, toDate, "")
            //        );
            //    if (!rsCheckPeriod.CheckPeriodCloseResult)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Kỳ công đã đóng, không thể đăng ký", ResponseStatus = -600 }));
            //    }

            //    //Lấy thông tin qua mã đăng ký
            //    var SIGN = await repAtten.GetAT_TIME_MANUALByIdAsync((decimal)SymbolId);

            //    //Check ngày nghỉ lễ không thể đăng ký
            //    var lstHolidays = await repAtten.GetHolidayByCalenderAsync(startDate, toDate);//lấy ds ngày nghỉ trong khoảng
            //    if (lstHolidays.Count > 0 && SIGN.CODE != "ML")
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Ngày lễ không thể đăng ký nghỉ. Thao tác thực hiện không thành công.", ResponseStatus = -600 }));
            //    }

            //    //Check phép năm không được <-3 và phép bù không được < 0
            //    var dtDataUserPHEPNAM = await repAtten.GetTotalPHEPNAMAsync((int)currentEmp.ID, toDate.Year, SymbolId);
            //    var at_entilement = await repAtten.GetPhepNamAsync((int)currentEmp.ID, toDate.Year);
            //    var at_compensatory = await repAtten.GetNghiBuAsync((int)currentEmp.ID, toDate.Year);
            //    var totalDayRes = await repAtten.GetTotalDAYAsync((int)currentEmp.ID, 251, startDate, toDate);
            //    var totalDayWanApp = await repAtten.GetTotalLeaveInYearAsync((int)currentEmp.ID, toDate.Year);

            //    //Check nếu là kiểu đăng ký nghỉ phép
            //    if (SIGN.MORNING_ID == 251 && SIGN.AFTERNOON_ID == 251)
            //    {
            //        if (dtDataUserPHEPNAM != null && at_entilement != null)
            //        {
            //            if (at_entilement.TOTAL_HAVE.Value - (decimal.Parse(totalDayRes.Rows[0][0].ToString()) + totalDayWanApp) < 0)
            //            {
            //                return Json(JObject.FromObject(new ResponseExecute() { Message = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép.", ResponseStatus = -600 }));
            //            }
            //        }
            //    }
            //    else if (SIGN.MORNING_ID == 251 || SIGN.AFTERNOON_ID == 251)
            //    {
            //        if (dtDataUserPHEPNAM != null && at_entilement != null)
            //        {
            //            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tổng số ngày nghỉ phép của bạn trong năm nay đã vượt quá mức cho phép.", ResponseStatus = -600 }));
            //        }
            //    }

            //    //Check nếu là kiểu đăng ký nghỉ bù
            //    var dtDataUserPHEPBU = await repAtten.GetTotalPHEPBUAsync((int)currentEmp.ID, toDate.Year, SymbolId);
            //    totalDayRes = await repAtten.GetTotalDAYAsync((int)currentEmp.ID, 255, startDate, toDate);
            //    if (SIGN.MORNING_ID == 255 && SIGN.AFTERNOON_ID == 255)
            //    {
            //        if (dtDataUserPHEPBU != null && at_entilement != null)
            //        {
            //            if (at_compensatory.TOTAL_HAVE.Value - (decimal.Parse(dtDataUserPHEPBU.Rows[0][0].ToString()) + decimal.Parse(totalDayRes.Rows[0][0].ToString()) + totalDayWanApp) < 0)
            //            {
            //                return Json(JObject.FromObject(new ResponseExecute() { Message = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép.", ResponseStatus = -600 }));
            //            }
            //        }
            //    }
            //    else if (SIGN.MORNING_ID == 255 || SIGN.AFTERNOON_ID == 255)
            //    {
            //        if (dtDataUserPHEPBU != null && at_entilement != null)
            //        {
            //            if (at_compensatory.TOTAL_HAVE.Value - (decimal.Parse(dtDataUserPHEPBU.Rows[0][0].ToString()) + (decimal.Parse(totalDayRes.Rows[0][0].ToString()) / 2) + totalDayWanApp) < 0)
            //            {
            //                return Json(JObject.FromObject(new ResponseExecute() { Message = "Tổng số ngày nghỉ bù của bạn đã vượt quá mức cho phép.", ResponseStatus = -600 }));
            //            }
            //        }
            //    }

            //    //Insert đăng ký
            //    var listReg = new List<AT_PORTAL_REG_DTO>();
            //    for (DateTime iStart = startDate; iStart <= toDate; iStart = iStart.AddDays(1))
            //    {
            //        listReg.Add(new AT_PORTAL_REG_DTO()
            //        {
            //            ID_EMPLOYEE = currentEmp.ID,
            //            ID_SIGN = SymbolId,
            //            FROM_DATE = iStart,
            //            TO_DATE = iStart,
            //            NVALUE = (decimal)((SIGN.MORNING_ID.Equals(SIGN.AFTERNOON_ID) ? 1 : 0.5)),
            //            SVALUE = "LEAVE",
            //            NOTE = Remark,
            //            PROCESS = "LEAVE"
            //        });
            //    }

            //    //Ds ghi nhận những ngày đã lưu khởi tạo thành công
            //    List<DateTime> lstSaveSuccess = new List<DateTime>();
            //    //Check đã đăng ký
            //    foreach (var oReg in listReg)
            //    {
            //        var lstCurrentRegis = await repAtten.GetRegisterAppointmentInPortalByEmployeeAsync(currentEmp.ID, oReg.FROM_DATE.Value, oReg.TO_DATE.Value, new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = SymbolId } }, new List<short> { 0, 1, 2 });

            //        if (lstCurrentRegis.Count > 0)
            //        {
            //            error = -600;
            //            message = "Đã tồn tại đăng ký nghỉ ngày " + oReg.FROM_DATE.Value.ToString("dd/MM/yyyy");
            //            return Json(JObject.FromObject(new ResponseExecute() { Message = message, ResponseStatus = error }));
            //        }
            //        else
            //        {
            //            var rsInsertReg = await repAtten.InsertPortalRegisterAsync(oReg, new AttendanceBusiness.UserLog() { ComputerName = "Mobile", Username = userName });
            //            if (rsInsertReg)
            //            {
            //                error = 1;
            //                message = "IsSuccess";
            //                lstSaveSuccess.Add(oReg.FROM_DATE.Value);
            //            }
            //            else
            //            {
            //                error = -600;
            //                message = "Đăng ký thất bại, phát sinh lỗi tại tầng business logic ";
            //                break;
            //            }
            //        }
            //    }

            //    #endregion "InsertREG"

            //    //Gửi duyệt luôn
            //    if (lstSaveSuccess.Count > 0)
            //    {
            //        //lấy ds vừa khởi tạo xong status = 0 và nằm trong khoảng tgian trên
            //        var lstRegisted = await repAtten.GetRegisterAppointmentInPortalByEmployeeAsync(currentEmp.ID, lstSaveSuccess.Min(), lstSaveSuccess.Max(), new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = SymbolId } }, new List<short> { 0 });
            //        var rsString = await repAtten.SendRegisterToApproveAsync(lstRegisted.Select(m => m.ID).ToList(), "LEAVE", ApiHelper.PortalUrl);

            //        if (string.IsNullOrEmpty(rsString))
            //        {
            //            //send notification
            //            await NotifyModule.GetAndSendNotification(false, lstRegisted.FirstOrDefault().ID.ToString(), "LEAVE");

            //            //sucess
            //            error = 1;
            //            message = "IsSuccess";
            //        }
            //        else
            //        {
            //            //error
            //            error = -600;
            //            message = rsString;
            //        }

            //        return Json(JObject.FromObject(new ResponseExecute() { Message = message, ResponseStatus = error })); ;
            //    }
            //}

            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tác vụ không được thực hiện", ResponseStatus = -600 }));
        }


        /// <summary>
        /// Đăng ký làm thêm + gửi chờ duyệt - Chuyển xuống tầng business thực hiện
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dparam"></param>
        /// <param name="companyCode"></param>
        /// <param name="userId"></param>
        /// <param name="checkTokenObject"></param>
        /// <returns></returns>
        private async Task<JsonResult<JObject>> API_RegisterOT(string functionName, JObject dparam, string companyCode, string userId, ExecuteResult checkTokenObject)
        {
            //if (checkTokenObject.BoolResult)
            //{
            //    var authorObject = JObject.Parse(checkTokenObject.StringResult);
            //    string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();
            //    string devideId = authorObject[functionName]["ParameterOutput"]["DevideId"].ToString();
            //    int error;
            //    string message;

            //    var lstEmp = new List<EmployeeDTO>();
            //    EmployeeDTO currentEmp = new EmployeeDTO();
            //    lstEmp.Add(currentEmp);
            //    //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
            //    using (var repCommon = new CommonBusinessClient())
            //    {
            //        var userInfo = await repCommon.GetUserWithPermisionAsync(userName);
            //        currentEmp.FULLNAME_VN = userInfo.FULLNAME;
            //        currentEmp.ID = (decimal)userInfo.EMPLOYEE_ID;
            //        currentEmp.EMPLOYEE_CODE = userInfo.EMPLOYEE_CODE;
            //    }

            //    List<ParamInputStore> inputParas = JsonConvert.DeserializeObject<List<ParamInputStore>>(dparam["parameterInput"].ToString());
            //    //DateTime startDate = DateTime.ParseExact(inputParas.Find(m => m.ParamName == "FromDate").InputValue, "yyyy-MM-dd", null);
            //    //DateTime toDate = DateTime.ParseExact(inputParas.Find(m => m.ParamName == "ToDate").InputValue, "yyyy-MM-dd", null);

            //    var rsCheckStartDate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "FromDate").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate);
            //    var rsCheckEnddate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "ToDate").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime toDate);

            //    if (!rsCheckStartDate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));
            //    if (!rsCheckEnddate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Đến ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));

            //    if ((startDate - toDate).TotalMinutes > 0) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không được lớn hơn đến ngày", ResponseStatus = -600 }));


            //    string fromHour = inputParas.Find(m => m.ParamName == "FromHour").InputValue;
            //    string toHour = inputParas.Find(m => m.ParamName == "ToHour").InputValue;
            //    int SymbolId = int.Parse(inputParas.Find(m => m.ParamName == "SymbolId").InputValue);
            //    string Remark = (inputParas.Find(m => m.ParamName == "Remark").InputValue);

            //    if (SymbolId <= 0) return Json(JObject.FromObject(new ResponseExecute() { Message = "Loại đăng ký không đúng", ResponseStatus = -600 }));

            //    using var repAtten = new AttendanceBusinessClient();
            //    #region "InsertREG"
            //    //Kiểm tra kỳ công đã đóng hay chưa
            //    var rsCheckPeriod = await repAtten.CheckPeriodCloseAsync(
            //            new CheckPeriodCloseRequest(lstEmp.Select(m => m.ID).ToList(), startDate, toDate, "")
            //        );
            //    if (!rsCheckPeriod.CheckPeriodCloseResult)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Kỳ công đã đóng, không thể đăng ký", ResponseStatus = -600 }));
            //    }

            //    //Check đăng ký đã bị trùng giờ hoặc đã có

            //    var lstRegisted = await repAtten.GetRegisterAppointmentInPortalOTAsync(currentEmp.ID, startDate, toDate, new List<OT_OTHERLIST_DTO>() { new OT_OTHERLIST_DTO() { ID = SymbolId } }, new List<short> { 0, 1, 2 });//  currentEmp.ID, startDate, toDate, new List<OT_OTHERLIST_DTO>() { new OT_OTHERLIST_DTO() { ID = SymbolId } }.ToArray(), new short[1] { 0 });
            //    if (lstRegisted.Count() > 0)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Khoảng thời gian đã có đăng ký", ResponseStatus = -600 }));
            //    }

            //    var isPreStartHour = DateTime.TryParseExact(fromHour, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime repStartHour);
            //    var isPreEndHour = DateTime.TryParseExact(toHour, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime repEndHour);
            //    if (!isPreStartHour) return Json(JObject.FromObject(new ResponseExecute() { Message = "Định dạng thời gian không đúng (HH:mm):" + fromHour, ResponseStatus = -600 }));
            //    if (!isPreEndHour) return Json(JObject.FromObject(new ResponseExecute() { Message = "Định dạng thời gian không đúng (HH:mm):" + toHour, ResponseStatus = -600 }));

            //    if ((repStartHour - repEndHour).TotalMinutes > 0)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Khoảng thời gian chọn không hợp lệ", ResponseStatus = -600 }));
            //    }

            //    //Insert đăng ký
            //    var listReg = new List<AT_PORTAL_REG_DTO>();
            //    for (DateTime iStart = startDate; iStart <= toDate; iStart = iStart.AddDays(1))
            //    {
            //        listReg.Add(new AT_PORTAL_REG_DTO()
            //        {
            //            ID_EMPLOYEE = currentEmp.ID,
            //            ID_SIGN = SymbolId,
            //            FROM_DATE = iStart,
            //            TO_DATE = iStart,
            //            FROM_HOUR = iStart.AddHours(repStartHour.Hour).AddMinutes(repStartHour.Minute),
            //            TO_HOUR = iStart.AddHours(repEndHour.Hour).AddMinutes(repEndHour.Minute),
            //            NVALUE = 1,
            //            SVALUE = "OVERTIME",
            //            NOTE = "Lý do OT: " + Remark,
            //            PROCESS = "OVERTIME"
            //        });
            //    }

            //    //Ds ghi nhận những ngày đã lưu khởi tạo thành công
            //    List<DateTime> lstSaveSuccess = new List<DateTime>();

            //    foreach (var oReg in listReg)
            //    {
            //        var rsInsertReg = await repAtten.InsertPortalRegisterAsync(oReg,
            //                           new AttendanceBusiness.UserLog() { ComputerName = "Mobile", Username = userName }
            //                           );
            //        if (rsInsertReg)
            //        {
            //            error = 1;
            //            message = "IsSuccess";
            //            lstSaveSuccess.Add(oReg.FROM_DATE.Value);
            //        }
            //        else
            //        {
            //            error = -600;
            //            message = "Đăng ký thất bại, phát sinh lỗi tại tầng business logic ";
            //            break;
            //        }
            //    }
            //    #endregion "InsertREG"

            //    //Gửi duyệt luôn
            //    if (lstSaveSuccess.Count > 0)
            //    {
            //        //lấy ds vừa khởi tạo xong status = 0 và nằm trong khoảng tgian trên
            //        var lstRegistedInit = await repAtten.GetRegisterAppointmentInPortalByEmployeeAsync(currentEmp.ID, lstSaveSuccess.Min(), lstSaveSuccess.Max(), new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = SymbolId } }, new List<short> { 0 });
            //        var rsString = await repAtten.SendRegisterToApproveAsync(lstRegistedInit.Select(m => m.ID).ToList(), "OVERTIME", ApiHelper.PortalUrl);

            //        if (string.IsNullOrEmpty(rsString))
            //        {
            //            //send notification
            //            await NotifyModule.GetAndSendNotification(false, lstRegisted.FirstOrDefault().ID.ToString(), "OT");

            //            //sucess
            //            error = 1;
            //            message = "IsSuccess";
            //        }
            //        else
            //        {
            //            //error
            //            error = -600;
            //            message = rsString;
            //        }

            //        return Json(JObject.FromObject(new ResponseExecute() { Message = message, ResponseStatus = error })); ;
            //    }
            //}

            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tác vụ không được thực hiện", ResponseStatus = -600 }));
        }

        /// <summary>
        /// Đăng ký đi muộn về sớm - chuyển xuống tần business thực hiện
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dparam"></param>
        /// <param name="companyCode"></param>
        /// <param name="userId"></param>
        /// <param name="checkTokenObject"></param>
        /// <returns></returns>
        private async Task<JsonResult<JObject>> API_RegisterWLEO(string functionName, JObject dparam, string companyCode, string userId, ExecuteResult checkTokenObject)
        {

            //if (checkTokenObject.BoolResult)
            //{
            //    var authorObject = JObject.Parse(checkTokenObject.StringResult);
            //    string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();
            //    string devideId = authorObject[functionName]["ParameterOutput"]["DevideId"].ToString();
            //    int error;
            //    string message;

            //    var lstEmp = new List<EmployeeDTO>();
            //    EmployeeDTO currentEmp = new EmployeeDTO();
            //    lstEmp.Add(currentEmp);
            //    //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
            //    using (var repCommon = new CommonBusinessClient())
            //    {
            //        var userInfo = await repCommon.GetUserWithPermisionAsync(userName);
            //        currentEmp.FULLNAME_VN = userInfo.FULLNAME;
            //        currentEmp.ID = (decimal)userInfo.EMPLOYEE_ID;
            //        currentEmp.EMPLOYEE_CODE = userInfo.EMPLOYEE_CODE;
            //    }

            //    List<ParamInputStore> inputParas = JsonConvert.DeserializeObject<List<ParamInputStore>>(dparam["parameterInput"].ToString());
            //    //DateTime startDate = DateTime.ParseExact(inputParas.Find(m => m.ParamName == "LeaveFrom").InputValue, "yyyy-MM-dd", null);
            //    //DateTime toDate = DateTime.ParseExact(inputParas.Find(m => m.ParamName == "LeaveTo").InputValue, "yyyy-MM-dd", null);

            //    var rsCheckStartDate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "LeaveFrom").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate);
            //    var rsCheckEnddate = DateTime.TryParseExact(inputParas.Find(m => m.ParamName == "LeaveTo").InputValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime toDate);

            //    if (!rsCheckStartDate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));
            //    if (!rsCheckEnddate) return Json(JObject.FromObject(new ResponseExecute() { Message = "Đến ngày không đúng định dạng yyyy-MM-dd", ResponseStatus = -600 }));

            //    if ((startDate - toDate).TotalMinutes > 0) return Json(JObject.FromObject(new ResponseExecute() { Message = "Từ ngày không được lớn hơn đến ngày", ResponseStatus = -600 }));

            //    int SymbolId = int.Parse(inputParas.Find(m => m.ParamName == "SymbolId").InputValue);
            //    string Remark = (inputParas.Find(m => m.ParamName == "Remark").InputValue);
            //    //string fromHour = inputParas.Find(m => m.ParamName == "FromHour").InputValue;
            //    // toHour = inputParas.Find(m => m.ParamName == "ToHour").InputValue;
            //    int dmMinute = int.Parse(inputParas.Find(m => m.ParamName == "DM_Minute").InputValue);
            //    int vsMinute = int.Parse(inputParas.Find(m => m.ParamName == "VS_Minute").InputValue);

            //    using var repAtten = new AttendanceBusinessClient();
            //    #region "InsertREG"
            //    //Kiểm tra kỳ công đã đóng hay chưa
            //    var rsCheckPeriod = await repAtten.CheckPeriodCloseAsync(
            //            new CheckPeriodCloseRequest(lstEmp.Select(m => m.ID).ToList(), startDate, toDate, "")
            //        );
            //    if (!rsCheckPeriod.CheckPeriodCloseResult)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Kỳ công đã đóng, không thể đăng ký", ResponseStatus = -600 }));
            //    }

            //    //Check đã đăng ký trong khoảng này chưa
            //    var lstCurrentRegisted = await repAtten.GetRegisterAppointmentInPortalByEmployeeAsync(currentEmp.ID, startDate, toDate, new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = SymbolId } }, new List<short> { 0, 1, 2 });
            //    if (lstCurrentRegisted.Count() > 0)
            //    {
            //        return Json(JObject.FromObject(new ResponseExecute() { Message = "Khoảng thời gian đã có đăng ký", ResponseStatus = -600 }));
            //    }
            //    //Insert đăng ký
            //    var listReg = new List<AT_PORTAL_REG_DTO>();
            //    for (DateTime iStart = startDate; iStart <= toDate; iStart = iStart.AddDays(1))
            //    {
            //        if (dmMinute > 0)
            //        {
            //            listReg.Add(new AT_PORTAL_REG_DTO()
            //            {
            //                ID_EMPLOYEE = currentEmp.ID,
            //                ID_SIGN = 168,
            //                FROM_DATE = iStart,
            //                TO_DATE = iStart,
            //                //FROM_HOUR = iStart.AddHours(int.Parse(fromHour)),
            //                //TO_HOUR = iStart.AddHours(int.Parse(toHour)),
            //                NVALUE = dmMinute,
            //                NOTE = Remark,
            //                PROCESS = "WLEO"
            //            });
            //        }
            //        if (vsMinute > 0)
            //        {
            //            listReg.Add(new AT_PORTAL_REG_DTO()
            //            {
            //                ID_EMPLOYEE = currentEmp.ID,
            //                ID_SIGN = 167,
            //                FROM_DATE = iStart,
            //                TO_DATE = iStart,
            //                //FROM_HOUR = iStart.AddHours(int.Parse(fromHour)),
            //                //TO_HOUR = iStart.AddHours(int.Parse(toHour)),
            //                NVALUE = vsMinute,
            //                NOTE = Remark,
            //                PROCESS = "WLEO"
            //            });
            //        }
            //    }

            //    //Ds ghi nhận những ngày đã lưu khởi tạo thành công
            //    List<DateTime> lstSaveSuccess = new List<DateTime>();
            //    //Check đã đăng ký
            //    foreach (var oReg in listReg)
            //    {
            //        var rsInsertReg = await repAtten.InsertPortalRegisterAsync(oReg, new AttendanceBusiness.UserLog() { ComputerName = "Mobile", Username = userName });
            //        if (rsInsertReg)
            //        {
            //            error = 1;
            //            message = "IsSuccess";
            //            lstSaveSuccess.Add(oReg.FROM_DATE.Value);
            //        }
            //        else
            //        {
            //            error = -600;
            //            message = "Đăng ký thất bại, phát sinh lỗi tại tầng business logic ";
            //            break;
            //        }
            //    }

            //    #endregion "InsertREG"

            //    //Gửi duyệt luôn
            //    if (lstSaveSuccess.Count > 0)
            //    {
            //        //lấy ds vừa khởi tạo xong status = 0 và nằm trong khoảng tgian trên
            //        var lstRegisted = await repAtten.GetRegisterAppointmentInPortalByEmployeeAsync(currentEmp.ID, lstSaveSuccess.Min(), lstSaveSuccess.Max(), new List<AT_TIME_MANUALDTO>() { new AT_TIME_MANUALDTO() { ID = SymbolId } }, new List<short> { 0 });
            //        var rsString = await repAtten.SendRegisterToApproveAsync(lstRegisted.Select(m => m.ID).ToList(), "WLEO", ApiHelper.PortalUrl);

            //        if (string.IsNullOrEmpty(rsString))
            //        {
            //            //send notification
            //            await NotifyModule.GetAndSendNotification(false, lstRegisted.FirstOrDefault().ID.ToString(), "WLEO");

            //            //sucess
            //            error = 1;
            //            message = "IsSuccess";
            //        }
            //        else
            //        {
            //            //error
            //            error = -600;
            //            message = rsString;
            //        }

            //        return Json(JObject.FromObject(new ResponseExecute() { Message = message, ResponseStatus = error })); ;
            //    }
            //}

            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tác vụ không được thực hiện", ResponseStatus = -600 }));
        }

        /// <summary>
        /// Api xử lý phê duyệt chung cho mobile (chuyển xuống WCF xử lý)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dparam"></param>
        /// <param name="companyCode"></param>
        /// <param name="userId"></param>
        /// <param name="checkTokenObject"></param>
        /// <param name="regisType"></param>
        /// <param name="status"></param>
        /// <param name="procResult"></param>
        /// <returns></returns>
        private async Task<JsonResult<JObject>> API_ApproveRegister(string functionName, JObject dparam, string companyCode, string userId, ExecuteResult checkTokenObject, string regisType, int status, string procResult)
        {
            var strMessage = "";
            if (checkTokenObject.BoolResult)
            {
                var authorObject = JObject.Parse(checkTokenObject.StringResult);
                string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();

                var lstEmp = new List<EmployeeDTO>();
                EmployeeDTO currentEmp = new EmployeeDTO();
                lstEmp.Add(currentEmp);
                //Truy vấn thông tin employee -> có thể thay cách khác hoặc thông tin trả về từ checktoken
                using (var repCommon = new CommonBusinessClient())
                {
                    var userInfo = await repCommon.GetUserWithPermisionAsync(userName);
                    currentEmp.FULLNAME_VN = userInfo.FULLNAME;
                    currentEmp.ID = (decimal)userInfo.EMPLOYEE_ID;
                    currentEmp.EMPLOYEE_CODE = userInfo.EMPLOYEE_CODE;
                }

                JObject dJson = JObject.Parse(procResult);

                JObject outputParas = JObject.Parse(dJson[functionName].ToString());

                //Các items danh sách đăng ký trả về từ store phê duyệt
                if (outputParas["Items"].Count() == 0)
                {
                    return Json(JObject.FromObject(new ResponseExecute() { Message = outputParas["Message"].ToString(), ResponseStatus = -600 }));
                }

                var resultStatus = outputParas["ResponseStatus"].ToString();

                if (!";-600;-1;-99;".Contains(";" + resultStatus))
                {
                    using var repAtten = new AttendanceBusinessClient();
                    try
                    {
                        var rsString = false;
                        //Duyệt ds ID đăng ký từ AT_PORTAL_REG để gọi WCF phê duyệt
                        foreach (JObject oItems in outputParas["Items"])
                        {

                            var listIds = oItems["LISTID"].ToString();
                            var comment = oItems["REMARK"].ToString();
                            var id_regroup = Guid.Parse(oItems["ID_REGGROUP"].ToString());

                            rsString = await repAtten.ApprovePortalRegisterAsync(id_regroup, currentEmp.ID, status, comment, ApiHelper.PortalUrl, regisType, new AttendanceBusiness.UserLog() { ComputerName = "", Ip = RequestHelper.GetIPAddress(), Username = userName });
                        }

                        if (rsString)
                        {
                            //send notification
                            //Không cần truyền ID do đã tạo noti từ store gọi trước hàm này
                            await NotifyModule.GetAndSendNotification(false);

                            //sucess
                            return Json(JObject.FromObject(new ResponseExecute() { Message = "IsSuccess", ResponseStatus = 1 }));
                        }
                        else
                        {
                            //error
                            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tác vụ không được thực hiện", ResponseStatus = -600 }));
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(JObject.FromObject(new ResponseExecute() { Message = ex.Message, ResponseStatus = -600 }));
                    }
                }
                else
                {
                    strMessage = outputParas["Message"].ToString();
                }
            }

            return Json(JObject.FromObject(new ResponseExecute() { Message = "Tác vụ không được thực hiện (" + strMessage + ")", ResponseStatus = -600 }));
        }


    }
}