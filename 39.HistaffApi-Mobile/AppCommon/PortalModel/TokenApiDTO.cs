using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class TokenApiDTO
    {
        public string Token { set; get; }

        public string DeviceID { set; get; }

        public string SessionID { set; get; }

        public string Username { set; get; }

        public long TimeCreated { set; get; }

        public long TimeExpired { set; get; }

        public string FunctionName { set; get; }

        public string ActionName { set; get; }

        public string EMPLOYEE_CODE { set; get; }

        public decimal? EMPLOYEE_ID { set; get; }

        public bool IsNotSameFunctionName { set; get; }

        public string CompanyCode { set; get; }
        public string UserId { set; get; }
    }
}