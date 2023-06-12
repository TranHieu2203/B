using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.AttendanceBusiness;
using HiStaffAPI.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;


namespace HiStaffAPI.ApiControllers.Attendance
{
    [PortalAuthen]
    [RoutePrefix(ApiName.PrefixApiAttendance)]
    public partial class AttendanceController : ApiController
    {
        private readonly IOracleDBManager _dBTool;
        public AttendanceController(IOracleDBManager DBTool)
        {
            _dBTool = DBTool;
        }

        #region Dùng chung

        [HttpPost]
        [Route(ApiName.GetComboboxData)]
        public async Task<IHttpActionResult> GetComboboxDataAsync(GetComboboxDataRequest request)
        {
            var response = new BaseJsonResponse<ComboBoxDataDTO>();
            try
            {
                using var profileBusinessClient = new AttendanceBusinessClient();
                var data = await profileBusinessClient.GetComboboxDataAsync(request);
                response.Status = true;
                response.Data = data.cbxData;
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
        #endregion

        /// <summary>
        /// API lấy thông tin thống kê check in 
        /// </summary>
        /// <param name="OrgIds"></param>
        /// <param name="DateString"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiName.GetDashboardCheckInSummary)]
        public async Task<IHttpActionResult> GetDashboardCheckInSummary(DashboardCheckInRequest request)
        {
            request.DateString = request.DateString.Replace("--", "").Replace("*/", "").Replace("/*", "");
            request.OrgIds = request.OrgIds?.Replace("--", "").Replace("*/", "").Replace("/*", "");
            request.EmployeeId = request.EmployeeId?.Replace("--", "").Replace("*/", "").Replace("/*", "");
            //
            var strCommand = @"
SELECT 	--ho.DESCRIPTION_PATH as ORG_PATH,
		--ho.HIERARCHICAL_PATH ,
		ho.NAME_VN AS ORG_NAME,
		ht.NAME_VN  AS TITLE_NAME,
		he.FULLNAME_VN  AS EMPLOYEE_NAME,
		CASE WHEN NVL(he.ITIME_ID ,'')='' THEN u'Chưa thiết lập mã chấm công'  
			 WHEN lich.EMPLOYEE_ID IS NULL  THEN u'Chưa xếp ca/lịch làm việc'
			 WHEN atm.ID IS NOT NULL THEN u'Đăng ký '||atm.NAME 
			 WHEN minin.checkin IS NULL THEN u'Không có dữ liệu'
			 WHEN TO_CHAR(minin.checkin,'HH24MI') > TO_CHAR(ca.HOURS_START ,'HH24MI') AND TO_CHAR(minin.checkin,'HH24MI') < TO_CHAR(ca.BREAKS_FORM ,'HH24MI') THEN u'Đi muộn '
			 ELSE u''
		END DESCRIPTION,
 		CASE WHEN atm.id IS NOT NULL THEN 2 --có đăng ký
			 WHEN TO_CHAR(minin.checkin,'HH24MI') > TO_CHAR(ca.HOURS_START ,'HH24MI') AND TO_CHAR(minin.checkin,'HH24MI') < TO_CHAR(ca.BREAKS_FORM ,'HH24MI') THEN 3 -- đi muộn
			 WHEN minin.checkin IS NULL THEN 4
			 ELSE 1 
		END FILTER_STATUS ,
		minin.checkin ,
		dangky.NOTE AS REASON,
		dangky.STATUS ,
		CASE WHEN dangky.status = 0 THEN u'Khởi tạo'
			 WHEN dangky.status = 1 THEN u'Chờ duyệt'
			 WHEN dangky.status = 2 THEN u'Đã duyệt'
			 when dangky.status = 3 THEN u'Từ chối'
		END STATUS_NAME 
FROM 	HU_ORGANIZATION ho 
LEFT JOIN 
		HU_TITLE ht ON ht.ORG_ID  = ho.id 
LEFT JOIN 
		HU_EMPLOYEE he ON he.TITLE_ID = ht.ID 
LEFT JOIN (
			SELECT 	at2.ORG_ID ,asd.TERMINAL_ID ,ITIME_ID , min(VALTIME ) checkin 
			FROM 	AT_SWIPE_DATA asd 
			LEFT JOIN 
					AT_TERMINALS at2 ON at2.ID =  asd.TERMINAL_ID 
			WHERE 	TO_CHAR(WORKINGDAY ,'YYYYMMDD') = '{0}'
			GROUP BY asd.ITIME_ID ,asd.TERMINAL_ID,at2.ORG_ID
		) minin ON minin.ITIME_ID = he.ITIME_ID  AND ho.HIERARCHICAL_PATH  LIKE '%'||minin.ORG_ID||'%'
LEFT JOIN (
			SELECT  r.ID_EMPLOYEE,r.NOTE , r.STATUS  ,r.ID_SIGN 
			FROM 	AT_PORTAL_REG r
			WHERE 	'{0}' >= TO_CHAR(r.FROM_DATE,'YYYYMMDD')
			AND 	'{0}' <= TO_CHAR(r.TO_DATE,'YYYYMMDD') 
		) dangky	ON dangky.ID_EMPLOYEE = he.ID 	
LEFT JOIN AT_WORKSIGN lich ON lich.EMPLOYEE_ID  = he.id AND TO_CHAR(lich.WORKINGDAY ,'YYYYMMDD') = '{0}'
LEFT JOIN AT_SHIFT ca ON ca.id = lich.SHIFT_ID 
LEFT JOIN AT_TIME_MANUAL atm ON atm.id = dangky.ID_SIGN 
WHERE 	ho.ACTFLG  = 'A'
AND 	ht.ACTFLG  = 'A'
AND 	he.FULLNAME_VN IS NOT NULL 
AND 	he.WORK_STATUS IN (258)
and     (ho.ID in ({1})  or he.id = {2})
ORDER BY 
		ho.HIERARCHICAL_PATH ";

            var tbData = await _dBTool.GetDataTableAsync(string.Format(strCommand, request.DateString, request.OrgIds, request.EmployeeId ?? "0"));
            var response = new BaseJsonResponse<List<DashboardCheckInResponse>>();
            response.Data = JsonConvert.DeserializeObject<List<DashboardCheckInResponse>>(JsonHelper.JSON(tbData));
            response.Status = true;
            response.Error = HttpStatusCode.OK.ToString();
            return Json(response);
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