using System.Collections.Generic;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class SendEmailRequest
    {
        public string ActionLink { set; get; }
        public List<decimal> ListUserId { set; get; }
        public string Subject { set; get; }
        public string Content { set; get; }
    }
}