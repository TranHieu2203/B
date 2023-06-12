using HiStaffAPI.App_Start;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Areas.Error.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;

namespace HiStaffAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Init loghelper
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(Server.MapPath("~") + "log4net.config"));
            var repo = log4net.LogManager.CreateRepository("log4net-default-repository", typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            //Enable memory cache
            MemoryCacheHelper.EnableCache = true;
            //Writelog app start
            LogHelper.WriteSystemLog("Application_Start " + DateTime.Now.ToString("yyyyMMdd HHmmss"));

            //Register config (DI, Attribute...)

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //SwaggerConfig.Register();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            LogHelper.WriteExceptionToLog(sender + e.ToString());
            try
            {
                HandleApplicationErrors();
                Response.TrySkipIisCustomErrors = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                Response.StatusCode = 500;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void HandleApplicationErrors(int? statusCode = null)
        {
            try
            {
                Exception ex = Server.GetLastError();
                Response.Clear();
                LogHelper.WriteExceptionToLog("HandleApplicationErrors", ex);

                HttpException httpEx = ex as HttpException;
                if (statusCode == null && httpEx != null) statusCode = httpEx.GetHttpCode();

                var routeData = new RouteData { DataTokens = { { "area", "Error" } }, Values = { { "controller", "Error" } } };

                //routeData.Values.Add("controller", "Error");
                switch (statusCode)
                {
                    case 404:
                        routeData.Values.Add("action", "Index");
                        break;
                    case 403:
                        routeData.Values.Add("action", "Index");
                        break;
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
                routeData.Values.Add("exception", ex);

                Server.ClearError();
                this.Server.ClearError();
                this.Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(new HttpContextWrapper(this.Context), routeData));
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("HandleApplicationErrors", ex);
                Response.Write("HandleApplicationError: Internal server error, please validate your input data!");
                Response.StatusCode = 500;
            }
        }
    }
}