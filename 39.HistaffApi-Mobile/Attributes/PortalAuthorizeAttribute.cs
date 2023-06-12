using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace HiStaffAPI.Attributes
{
    /// <summary>
    /// Check quyền được phép truy cập, sử dụng chức năng...
    /// </summary>
    public class PortalAuthorizeAttribute : AuthorizeAttribute
    {

        string _functionName = "";
        string _actionName = "";

        public PortalAuthorizeAttribute(string functionName = "HOME", string actionName = "INDEX")
        {
            _functionName = functionName;
            _actionName = actionName;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            return true;//base.IsAuthorized(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }


    /// <summary>
    /// Xác thực token đăng nhập
    /// </summary>
    public class PortalAuthenAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var getAuthor = actionContext.Request.Headers.Authorization;
            if (getAuthor == null) throw new UnauthorizeException("Require authorization data");

            var token = JsonConvert.DeserializeObject<TokenApiDTO>(Encoding.UTF8.GetString(Convert.FromBase64String(getAuthor.ToString().Replace("Bearer ", ""))));
            var isvalid = TokenHelper.CheckToken(token, "checktoken");
            if (!isvalid)
            {
                throw new UnauthorizeException("TokenIsNotValid");
            }

            return true;//base.IsAuthorized(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }
}