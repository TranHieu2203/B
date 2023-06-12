using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class LoginRequest
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}