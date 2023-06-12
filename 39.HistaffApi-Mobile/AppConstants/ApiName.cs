using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppConstants
{
    public class ApiName
    {
        /// <summary>
        ///Prefix
        /// </summary>
        public const string PrefixApi = "Api/Mobile";
        public const string PrefixApiSystem = "Api/Mobile/System";
        public const string PrefixApiPortal = "Api/Portal";
        public const string PrefixApiCommon = "Api/Common";
        public const string PrefixApiProfile = "Api/Profile";
        public const string PrefixApiAttendance = "Api/Attendance";
        public const string PrefixApiPayroll = "Api/Payroll";
        //OM
        public const string PrefixApiOM = "Api/MobileOM";
        public const string PrefixApiOMSystem = "Api/MobileOM/System";
        public const string PrefixApiOMPortal = "Api/OM/Portal";
        public const string PrefixApiOMCommon = "Api/OM/Common";
        public const string PrefixApiOMProfile = "Api/OM/Profile";
        public const string PrefixApiOMAttendance = "Api/OM/Attendance";
        public const string PrefixApiOMPayroll = "Api/OM/Payroll";

        //System name
        public const string Login = "Login";
        public const string LoginWToken = "LoginWithToken";
        public const string ChangePassword = "ChangePassword";

        //Business
        public const string RegisterLeave = "RegisterLeave";

        //Profile
        public const string GetEmployeeImage = "GetEmployeeImage";
        public const string GetEmployeeAllByID = "GetEmployeeAllByID";
        public const string GetWorkingBefore = "GetWorkingBefore";
        public const string GetEmployeeFamily = "GetEmployeeFamily";
        public const string GetProcessTraining = "GetProcessTraining";
        public const string GetPositionProcess = "GetPositionProccess";
        public const string GetDisciplineProcess = "GetDisciplineProccess";
        public const string GetCommendProcess = "GetCommendProccess";
        public const string GetContractProcess = "GetContractProccess";
        public const string GetEmployeeByID = "GetEmployeeByID";
        public const string CheckPermissionByPosition = "CheckPermissionByPosition";
        public const string GetListJobPosition = "GetListJobPosition";
        public const string GetChartJobPosition = "GetChartJobPosition";
        public const string GetJobPosTreePortal = "GetJobPosTreePortal";
        public const string ModifyJobPosTreeList = "ModifyJobPosTreeList";
        public const string GetJobChileTree = "GetJobChileTree";
        public const string OrgChartRptPortal = "OrgChartRptPortal";
        public const string GetPossitionPortal = "GetPossitionPortal";
        public const string SwapMasterInterim = "SwapMasterInterim";
        public const string GetOtherList = "GetOtherList";
        public const string GetProvinceList = "GetProvinceList";
        public const string GetDistrictList = "GetDistrictList";
        public const string GetWardList = "GetWardList";
        public const string GetComboList = "GetComboList";
        public const string ValidateEmployee = "ValidateEmployee";
        public const string SendEmployeeEdit = "SendEmployeeEdit";
        public const string GetEmployeeByEmployeeID = "GetEmployeeByEmployeeID";
        public const string GetEmployeeEditByID = "GetEmployeeEditByID";
        public const string InsertEmployeeEdit = "InsertEmployeeEdit";
        public const string ModifyEmployeeEdit = "ModifyEmployeeEdit";

        public const string CheckExistWorkingBeforeEdit = "CheckExistWorkingBeforeEdit";
        public const string InsertWorkingBeforeEdit = "InsertWorkingBeforeEdit";
        public const string ModifyWorkingBeforeEdit = "ModifyWorkingBeforeEdit";
        public const string GetWorkingBeforeEdit = "GetWorkingBeforeEdit";
        public const string SendWorkingBeforeEdit = "SendWorkingBeforeEdit";

        public const string CheckExistProcessTrainingEdit = "CheckExistProcessTrainingEdit";
        public const string InsertProcessTrainingEdit = "InsertProcessTrainingEdit";
        public const string ModifyProcessTrainingEdit = "ModifyProcessTrainingEdit";
        public const string GetProcessTrainingEdit = "GetProcessTrainingEdit";
        public const string SendProcessTrainingEdit = "SendProcessTrainingEdit";

        public const string CheckExistFamilyEdit = "CheckExistFamilyEdit";
        public const string InsertEmployeeFamilyEdit = "InsertEmployeeFamilyEdit";
        public const string ModifyEmployeeFamilyEdit = "ModifyEmployeeFamilyEdit";
        public const string GetEmployeeFamilyEdit = "GetEmployeeFamilyEdit";
        public const string SendEmployeeFamilyEdit = "SendEmployeeFamilyEdit";
        public const string GetEmployeeFamilyEditByID = "GetEmployeeFamilyEditByID";
        public const string CheckFamilyCMND = "CheckFamilyCMND";

        public const string GetPositionByOrgIDPortal = "GetPositionByOrgIDPortal";
        public const string GetTitleByTitleID = "GetTitleByTitleID";
        public const string GetOrgOMByID = "GetOrgOMByID";
        public const string AutoGenCodeHuTile = "AutoGenCodeHuTile";
        public const string CheckIsOwner = "CheckIsOwner";
        public const string ModifyTitleById = "ModifyTitleById";
        public const string InsertTitleNB = "InsertTitleNB";

        //Attendance
        public const string GetListTaskApprove = "GetListTaskApprove";
        public const string GetListWaitingForApprove = "GetListWaitingForApprove";
        public const string GetApproveStatusList = "GetApproveStatusList";
        public const string GetRegisterTypeList = "GetRegisterTypeList";
        public const string GetListRegisterDMVSApprove = "GetListRegisterDMVSApprove";
        public const string GetListRegisterLeaveApprove = "GetListRegisterLeaveApprove";
        public const string GetListRegisterOTApprove = "GetListRegisterOTApprove";
        public const string GetRegisterOTPortal = "GetRegisterOTPortal";
        public const string GetRegisterLeavePortal = "GetRegisterLeavePortal";
        public const string ApproveRegisterPortal = "ApproveRegisterPortal";
        public const string LOAD_PERIODBylinq = "LOAD_PERIODBylinq";
        public const string GetTimeSheetPortal = "GetTimeSheetPortal";
        public const string DeletePortalRegisterNew = "DeletePortalRegisterNew";
        public const string GetRegisterLeaveDetailByParentID = "GetRegisterLeaveDetailByParentID";
        public const string GetRegisterDMVSDetailByParentID = "GetRegisterDMVSDetailByParentID";
        public const string SendRegisterToApprove = "SendRegisterToApprove";
        public const string GetListTaskRegister = "GetListTaskRegister";
        public const string GetRegisterLeaveByID = "GetRegisterLeaveByID";
        public const string InsertRegisterDMVS = "InsertRegisterDMVS";
        public const string ModifyRegisterDMVS = "ModifyRegisterDMVS";
        public const string CheckPeriodClose = "CheckPeriodClose";
        public const string GetHolidayByCalender = "GetHolidayByCalender";
        public const string GetPhepNam = "GetPhepNam";
        public const string GetNghiBu = "GetNghiBu";
        public const string GetTotalDAY = "GetTotalDAY";
        public const string GetTotalDayOff = "GetTotalDayOff";
        public const string GetTotalLeaveInYear = "GetTotalLeaveInYear";
        public const string GetTotalPHEPBU = "GetTotalPHEPBU";
        public const string CheckExitRegister = "CheckExitRegister";
        public const string InsertRegisterLeave = "InsertRegisterLeave";
        public const string ModifyRegisterLeave = "ModifyRegisterLeave";
        public const string GetComboboxData = "GetComboboxData";
        public const string CheckDataOTByShift = "CheckDataOTByShift";
        public const string CheckOTRegisterOverlap = "CheckOTRegisterOverlap";
        public const string InsertPortalRegisterOT = "InsertPortalRegisterOT";
        public const string ModifyPortalRegisterOT = "ModifyPortalRegisterOT";
        public const string GetMyTimeSheetPortal = "GetMyTimeSheetPortal";
        public const string GetMyTimeSheetPortalByMonth = "GetMyTimeSheetPortalByMonth";
        public const string GetTotalTimeSheetPortalByMonth = "GetTotalTimeSheetPortalByMonth";
        public const string LOAD_PERIODByID = "LOAD_PERIODByID";
        public const string GetComboboxOrgTree = "GetComboboxOrgTree";
        public const string CheckExistInDatabase = "CheckExistInDatabase";
        public const string GetTotalPHEPNAM = "GetTotalPHEPNAM";
        public const string GetDashboardCheckInSummary = "GetDashboardCheckInSummary";

        //Payroll
        public const string GetPeriodbyYear = "GetPeriodbyYear";
        public const string GetPayrollSheetSumSheet = "GetPayrollSheetSumSheet";

        //Portal
        public const string GetATOfferJobList = "GetATOfferJobList";

        //Common
        public const string GetOrganizationAll = "GetOrganizationAll";
        public const string GetOrganizationLocationTreeView = "GetOrganizationLocationTreeView";
        public const string GetOrganizationStructureInfo = "GetOrganizationStructureInfo";
        public const string GetOrgTree = "GetOrgTree";
        public const string GetPassword = "GetPassword";
        public const string ChangeUserPassword = "ChangeUserPassword";
        public const string GetUserWithPermission = "GetUserWithPermision";
        public const string ChangePasswordRequest = "ChangePasswordRequest";
    }
}