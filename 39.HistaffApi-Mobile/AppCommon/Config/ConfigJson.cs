using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.Config
{
    public class ConfigJson
    {
        /// <summary>
        /// Trạng thái server require https : 1 , http: 0
        /// </summary>
        public int Https { set; get; }

        public List<ConfigDetail> config { set; get; }

        public string LanguageDefault { set; get; }
    }

    public class ConfigDetail
    {
        /// <summary>
        /// Là api cho mobile =1
        /// </summary>
        public int IsMobile { set; get; }

        /// <summary>
        /// Chuỗi xác thực nguồn truy cập Api [HRMMOBILE]
        /// </summary>
        public string clientId { set; get; }

        /// <summary>
        /// Danh sách các chức năng/api
        /// </summary>
        public List<ConfigFunction> functionListName { set; get; }

        #region "Other config"

        public string username { set; get; }

        public string password { set; get; }

        public string companyCode { set; get; }

        public List<ClientIP> clientListIP { set; get; }

        #endregion "Other config"
    }

    public class ClientIP
    {
        public string IPAddress { set; get; }
    }

    public class ConfigFunction
    {
        /// <summary>
        /// Tên api/chức năng
        /// </summary>
        public string FunctionName { set; get; }

        /// <summary>
        /// Tên thủ tục DB
        /// </summary>
        public string StoreProceduceName { set; get; }

        /// <summary>
        /// Trạng thái sử dụng ds param input = 1
        /// </summary>
        public int IsParamInputList { set; get; }

        public List<ConfigParamIn> ParamIn { set; get; }

        public List<ConfigParamOut> ParamOut { set; get; }


    }

    public class ConfigParamIn
    {
        public string ParamName { set; get; }

        public string ParamType { set; get; }

        public int ParamLength { set; get; }

        public int IsHidden { set; get; }

        public string ParamJson { set; get; }
    }

    public class ConfigParamOut
    {
        public string ParamName { set; get; }

        public string ParamType { set; get; }

        public int ParamLength { set; get; }

        public int IsHidden { set; get; }

        public string ParamJson { set; get; }
    }
    public class ParamInputStore
    {
        public string ParamName { set; get; }

        public string ParamType { set; get; }

        public int ParamLength { set; get; }

        public int ParamInOut { set; get; }

        public string InputValue { set; get; }
    }
}