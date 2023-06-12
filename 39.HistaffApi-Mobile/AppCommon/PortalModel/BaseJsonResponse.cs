using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class BaseJsonResponse
    {
        public bool Status { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }
    }

    public class BaseJsonResponse<T> : BaseJsonResponse
    {
        public int? Total { get; set; }

        public T Data { get; set; }
    }
}