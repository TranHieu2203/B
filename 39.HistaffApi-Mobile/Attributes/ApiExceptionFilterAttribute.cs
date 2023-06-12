using Autofac.Integration.WebApi;
using HiStaffAPI.AppCommon;
using HiStaffAPI.AppException;
using HiStaffAPI.AppHelpers;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace HiStaffAPI.Attributes
{
    public class ApiExceptionFilterAttribute : IAutofacExceptionFilter
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Response.StatusCode = (HttpStatusCode)System.Net.HttpStatusCode.InternalServerError;
            //filterContext.Response.ClearContent();

            //Clear default error and response
            //filterContext.ClearError();
            //filterContext.Response.ContentType = "application/json";
            //filterContext.Response.Content.Write(
            //    JsonConvert.SerializeObject(
            //        new ResponseData()
            //    {
            //        code = filterContext.HttpContext.Response.StatusCode.ToString(),
            //        message = filterContext.Exception.Message,
            //        data = filterContext.Exception.StackTrace
            //    }));
            //filterContext.HttpContext.Response.Flush();

            //write log
            //LogHelper.WriteExceptionToLog(filterContext.Exception);

        }

        /// <summary>
        /// OnExceptionAsync
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //LogHelper.WriteExceptionToLog(actionExecutedContext.Request);
            ResponseData rsData = new ResponseData();
            //Phân loại exception ghi log và trả về
            if (actionExecutedContext.Exception is UnauthorizeException)
            {
                rsData.Error = HttpStatusCode.Unauthorized.ToString();
                rsData.Message = actionExecutedContext.Exception.Message;
                rsData.Data = "";

            }
            else if (actionExecutedContext.Exception is WrongInputException)
            {
                rsData.Error = HttpStatusCode.NotAcceptable.ToString();
                rsData.Message = actionExecutedContext.Exception.Message;
                rsData.Data = "";
            }
            else
            {
                rsData.Error = HttpStatusCode.InternalServerError.ToString();
                //rsData.Message = "Internal server error: Please validate your input data"; //actionExecutedContext.Exception.Message;
                rsData.Message = "Incorect Username or password"; //actionExecutedContext.Exception.Message;
                rsData.Data = "";
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, rsData, "application/json");
            LogHelper.WriteExceptionToLog(actionExecutedContext);
            return Task.Delay(1);
        }
    }
}