using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HiStaffAPI.Areas.MobileView.Controllers;
using HiStaffAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HiStaffAPI.App_Start
{
    public class ExceptionHandlerModule : Autofac.Module
    {
        /// <summary>
        /// Hàm load đăng ký exception
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(e => new AppExceptionFilterAttribute()).AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
            
            //Catch for WebApi
            builder.RegisterType<ApiExceptionFilterAttribute>().AsWebApiExceptionFilterFor<ApiController>().InstancePerRequest();
            //builder.RegisterType<AuthorizeRequestAttribute>().AsWebApiAuthenticationFilterFor<ApiController>().InstancePerRequest();

            //Catch for MVC
            builder.Register(e => new AppExceptionFilterAttribute()).AsExceptionFilterFor<Controller>().InstancePerRequest();
        }
    }
}