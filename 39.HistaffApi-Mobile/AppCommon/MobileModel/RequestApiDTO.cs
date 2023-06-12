using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.MobileModel
{
    public class RequestApiDTO
    {
        public string Token { set; get; }
        public string DeviceID { set; get; }
        public string CompanyCode { get; set; }
        public string UserID { set; get; }
        public string Username { set; get; }
        public string Language { set; get; }
    }
}