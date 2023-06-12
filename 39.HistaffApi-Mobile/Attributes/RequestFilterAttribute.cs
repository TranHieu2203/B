using HiStaffAPI.AppCommon;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace HiStaffAPI.Attributes
{
    public class RequestFilterAttribute : ActionFilterAttribute
    {
        private readonly string jsonMediaType = "application/json";

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
        }
        public override void OnActionExecuting(HttpActionContext context)
        {
            var clientContentType = context.Request.Content.Headers.ContentType.ToString();
            var clientAccept = context.Request.Headers.Accept.ToString();
            var data = "";
            var responseCode = HttpStatusCode.NotAcceptable;
            if (clientAccept.ToLower() != jsonMediaType)
            {
                ResponseData rsData = (new ResponseData()
                {
                    Error = responseCode.ToString(),
                    Message = "{\"Error\":\"" + responseCode + "\", \"Message\": \"3.1. Accept fail\", \"Data\": " + data + "}",
                    Data = ""
                }
                );
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, rsData, jsonMediaType);
            }
            if (clientContentType.ToLower() != jsonMediaType)
            {
                ResponseData rsData = (new ResponseData()
                {
                    Error = HttpStatusCode.NotAcceptable.ToString(),
                    Message = "{\"Error\":\"" + responseCode + "\", \"Message\": \"3.2. Content-Type fail\", \"Data\": " + data + "}",
                    Data = ""
                });
                context.Response = context.Request.CreateResponse(HttpStatusCode.NotAcceptable, rsData, jsonMediaType);
            }
        }

    }
}