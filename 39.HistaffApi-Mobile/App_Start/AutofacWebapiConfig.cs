using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HiStaffAPI.App_Start.JobSchedule.SendNotification;
using HiStaffAPI.AppCommon.Authen;
using HiStaffAPI.AppCommon.DAO;
using HiStaffAPI.Attributes;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace HiStaffAPI.App_Start
{
    public class AutofacWebapiConfig
    {
        /// <summary>
        /// Container
        /// </summary>
        public static IContainer Container;
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        /// Init cấu hình
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));

        }

        /// <summary>
        /// Init cấu hình và container
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Đăng ký DI cho các controller, business hay dataaccess


            //builder.RegisterType<DBCustomerEntities>().As<DbContext>().InstancePerRequest();
            //builder.RegisterType<HttpContext>().As<IHttpContextAccessor>().SingleInstance();
            //builder.RegisterType<MobileSystemOMController>().As<IMobileSystemOMController>().InstancePerLifetimeScope();
            //Business

            //DataAccess

            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new ExceptionHandlerModule());
            builder.RegisterType<SendNotificationSchedule>().AsSelf();

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            builder.RegisterType<OracleDBManagerOM>().As<IOracleDBManagerOM>().InstancePerLifetimeScope();
            builder.RegisterType<OracleDBManager>().As<IOracleDBManager>().InstancePerLifetimeScope();

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SendNotification).Assembly));
            builder.RegisterType<Authen>().As<IAuthen>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenOM>().As<IAuthenOM>().InstancePerLifetimeScope();


            MobileAuthenAttribute.AuthenProp = new Authen();
            MobileAuthenOMAttribute.AuthenProp = new AuthenOM();
            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();
            ConfigureScheduler(Container);
            return Container;
        }
        private static void ConfigureScheduler(IContainer container)
        {
            //Run schedule
            var scheduler = container.Resolve<SendNotificationSchedule>();
            scheduler.Start();
        }
    }
}