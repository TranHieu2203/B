using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon
{
    public class ResponseData
    {
        public string Error { set; get; }

        public object Data { set; get; }

        public string Message { set; get; }

    }

    public class ResponseExecute
    {
        public string Message { set; get; }

        public int ResponseStatus { set; get; }

        public int Count { set; get; }

        public List<object> ParameterOutput { set; get; }

        public List<object> Items { set; get; }
    }
    public class ParamOutput
    {
        public string Message { set; get; }

        public int ResponseStatus { set; get; }
    }
}