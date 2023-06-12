namespace HiStaffAPI.AppCommon.PortalModel
{
    public class DashboardCheckInRequest
    {
        public string OrgIds { set; get; }
        public string DateString { set; get; }

        public string EmployeeId { set; get; }
    }

    public class DashboardCheckInResponse
    {
        public string ORG_NAME { set; get; }
        public string TITLE_NAME { set; get; }
        public string EMPLOYEE_NAME { set; get; }
        public string DESCRIPTION { set; get; }
        public string FILTER_STATUS { set; get; }
        public string CHECKIN { set; get; }
        public string REASON { set; get; }
        public string STATUS { set; get; }
        public string STATUS_NAME { set; get; }
    }
}