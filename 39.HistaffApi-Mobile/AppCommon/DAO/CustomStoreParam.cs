using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.DAO
{
    public class CustomStoreParam
    {
        //Convert from sample
        //var StoreParam = JObject.Parse("{\"parameterInput\":[" +
        //            "{\"ParamName\":\"Token\", \"ParamType\":\"12\", \"ParamInOut\":\"1\", \"ParamLength\":\"200\", \"InputValue\":\"" + requestApi.Token + "\"}," +
        //            "{\"ParamName\":\"Message\", \"ParamType\":\"12\", \"ParamInOut\":\"3\", \"ParamLength\":\"600\", \"InputValue\":\"\"}," +
        //            "{\"ParamName\":\"ResponseStatus\", \"ParamType\":\"8\", \"ParamInOut\":\"3\", \"ParamLength\":\"4\", \"InputValue\":\"0\"}]}");

        public List<ParamObject> parameterInput { set; get; }

        public class ParamObject
        {
            public string ParamName { set; get; }
            public string ParamType { set; get; }
            public ParameterDirection ParamInOut { set; get; }
            public string ParamLength { set; get; }
            public string InputValue { set; get; }
        }
    }
}