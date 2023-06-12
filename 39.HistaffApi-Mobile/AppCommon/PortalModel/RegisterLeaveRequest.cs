using System.Collections.Generic;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class RegisterLeaveRequest
    {
        public string LeaveFrom { set; get; }
        public string LeaveTo { set; get; }
        public string FromHour { set; get; }
        public string ToHour { set; get; }
        public string Remark { set; get; }
        public string SymbolId { set; get; }

    }
}