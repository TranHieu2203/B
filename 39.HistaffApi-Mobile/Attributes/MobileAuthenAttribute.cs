using Autofac.Integration.Mvc;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HiStaffAPI.Attributes
{
    public class MobileAuthenAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public static IAuthen AuthenProp { set; get; }

        public MobileAuthenAttribute() { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string strToken = "";

            //Tạm thời xử lý xóa session nếu có tham số
            if (httpContext.Request.QueryString["renew"] != null)
            {
                if (httpContext.Request.QueryString["renew"].ToString() == "1") httpContext.Session[SessionName.User_Authorize] = null;
            }

            if (httpContext.Request.Headers["Authorization"] == null)
            {
                if (httpContext.Session[SessionName.User_Authorize] == null)
                {
                    //for test
                    //Check thêm case request phiếu lương, bảng công => cách này ko an toàn
                    if (httpContext.Request.Path.ToString().ToLower().Contains("mobileview/timesheet/monthdetail")
                        || httpContext.Request.Path.ToString().ToLower().Contains("mobileview/payslipmobile/payslip")
                        )
                    {
                        try
                        {
                            strToken = httpContext.Request.QueryString["Authorization"].ToString();
                        }
                        catch { }
                    }
                    else
                    {
                        throw new UnauthorizeException("Authorization data is not found");
                    }
                }
                else
                {
                    if (httpContext.Request.Path.ToString().ToLower().Contains("mobileview/timesheet")
                        || httpContext.Request.Path.ToString().ToLower().Contains("mobileview/payslipmobile")
                        )
                    {
                        strToken = httpContext.Session[SessionName.User_Authorize].ToString();
                    }
                    else
                    {
                        throw new UnauthorizeException("Authorization data is not found");
                    }
                }
            }
            else
            {
                strToken = httpContext.Request.Headers["Authorization"].ToString();
            }

            if (string.IsNullOrEmpty(strToken)) throw new UnauthorizeException("Authorization data is not found");
            var token = RequestHelper.GetTokenInfo(strToken);
            var functionName = ProcedureName.API_User_LoginWToken;

            var checkTokenResult = AuthenProp.CheckToken(token, functionName);
            if (!checkTokenResult.BoolResult) throw new UnauthorizeException("Token is not accepted");

            var authorObject = JObject.Parse(checkTokenResult.StringResult);
            string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();
            string devideId = authorObject[functionName]["ParameterOutput"]["DevideId"].ToString();

            //Lưu session và sử dụng do request = jquery ajax nên không có đủ thông tin
            httpContext.Session[SessionName.User_Authorize] = strToken;
            httpContext.Session[SessionName.User_UserName] = userName;
            httpContext.Session[SessionName.User_DeviceID] = devideId;

            return true;// base.AuthorizeCore(httpContext);
        }
    }

    public class MobileAuthenOMAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public static IAuthenOM AuthenProp { set; get; }

        public MobileAuthenOMAttribute() { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string strToken = "";

            //Tạm thời xử lý xóa session nếu có tham số
            if (httpContext.Request.QueryString["renew"] != null)
            {
                if (httpContext.Request.QueryString["renew"].ToString() == "1") httpContext.Session[SessionName.User_Authorize] = null;
            }

            if (httpContext.Request.Headers["Authorization"] == null)
            {
                if (httpContext.Session[SessionName.User_Authorize] == null)
                {
                    //for test
                    //Check thêm case request phiếu lương, bảng công => cách này ko an toàn
                    if (httpContext.Request.Path.ToString().ToLower().Contains("mobileview/timesheet/monthdetail")
                        || httpContext.Request.Path.ToString().ToLower().Contains("mobileview/payslipmobile/payslip")
                        )
                    {
                        try
                        {
                            strToken = httpContext.Request.QueryString["Authorization"].ToString();
                        }
                        catch { }
                    }
                    else
                    {
                        throw new UnauthorizeException("Authorization data is not found");
                    }
                }
                else
                {
                    if (httpContext.Request.Path.ToString().ToLower().Contains("mobileview/timesheet")
                        || httpContext.Request.Path.ToString().ToLower().Contains("mobileview/payslipmobile")
                        )
                    {
                        strToken = httpContext.Session[SessionName.User_Authorize].ToString();
                    }
                    else
                    {
                        throw new UnauthorizeException("Authorization data is not found");
                    }
                }
            }
            else
            {
                strToken = httpContext.Request.Headers["Authorization"].ToString();
            }

            if (string.IsNullOrEmpty(strToken)) throw new UnauthorizeException("Authorization data is not found");
            var token = RequestHelper.GetTokenInfo(strToken);
            var functionName = ProcedureName.API_User_LoginWToken;

            var checkTokenResult = AuthenProp.CheckToken(token, functionName);
            if (!checkTokenResult.BoolResult) throw new UnauthorizeException("Token is not accepted");

            var authorObject = JObject.Parse(checkTokenResult.StringResult);
            string userName = authorObject[functionName]["ParameterOutput"]["UserName"].ToString();
            string devideId = authorObject[functionName]["ParameterOutput"]["DevideId"].ToString();

            //Lưu session và sử dụng do request = jquery ajax nên không có đủ thông tin
            httpContext.Session[SessionName.User_Authorize] = strToken;
            httpContext.Session[SessionName.User_UserName] = userName;
            httpContext.Session[SessionName.User_DeviceID] = devideId;

            return true;// base.AuthorizeCore(httpContext);
        }
    }
}