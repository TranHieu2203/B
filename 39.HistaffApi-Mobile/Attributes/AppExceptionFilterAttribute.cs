using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace HiStaffAPI.Attributes
{
    public class AppExceptionFilterAttribute : ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //write log
            LogHelper.WriteExceptionToLog(filterContext.Exception);
            
            //Filter exception 
            if (filterContext.Exception is UnauthorizeException)
            {
                filterContext.ActionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
                //filterContext.ActionContext.Response.Redirect("/Client/RequireLogin", true);
                throw new UnauthorizeException("UnauthorizeException");
            }
            else if (filterContext.Exception is WrongInputException)
            {
                filterContext.ActionContext.Response.StatusCode = HttpStatusCode.NotAcceptable;
                //filterContext.HttpContext.Response.Redirect("/Home/NoPermission");
                throw new System.Exception("WrongInputException");
            }
            else
            {
                filterContext.ActionContext.Response.StatusCode = HttpStatusCode.InternalServerError;

                var errRes = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            StatusCode = filterContext.ActionContext.Response.StatusCode,
                            Message = filterContext.Exception.Message,
                            Data = filterContext.Exception.InnerException?.Message
                        }).ToString(), System.Text.Encoding.UTF8)
                };

                throw new System.Exception(filterContext.Exception.Message);

                //filterContext.ActionContext.Response.ClearContent();

                //Clear default error and response
                //filterContext.HttpContext.ClearError();
                //filterContext.HttpContext.Response.ContentType = "application/json";
                //filterContext.HttpContext.Response.Write(
                //    JsonConvert.SerializeObject(new
                //    {
                //        StatusCode = filterContext.HttpContext.Response.StatusCode,
                //        Message = filterContext.Exception.Message,
                //        Data = filterContext.Exception.StackTrace
                //    }).ToString());
                //filterContext.HttpContext.Response.Flush();
                //filterContext.HttpContext.Response.Redirect("/Home/Error");

            }
        }

        public void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}