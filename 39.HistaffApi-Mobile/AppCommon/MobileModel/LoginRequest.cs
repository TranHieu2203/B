using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.MobileModel
{
    public class LoginRequest
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string DeviceId { get; set; }
        public string Firebase_Client_Id { get; set; }
        public string Language { get; set; }

        public LoginRequest()
        {
            Language = "vi-VN";
        }
    }
}