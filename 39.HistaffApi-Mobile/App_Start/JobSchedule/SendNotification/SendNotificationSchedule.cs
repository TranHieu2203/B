using HiStaffAPI.AppHelpers;
using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;

namespace HiStaffAPI.App_Start.JobSchedule.SendNotification
{
    public class SendNotificationSchedule
    {
        private readonly IScheduler _scheduler;


        public SendNotificationSchedule(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        /// <summary>
        /// Thiết lập lịch và bắt đầu chạy job
        /// </summary>
        public async void Start()
        {
            try
            {
                //_scheduler = StdSchedulerFactory.GetDefaultScheduler();
                await _scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<SendNotification>()
                    .WithIdentity("SendNotification", "AutoScheduler")
                    .Build();
                var cronStr = "0 * * ? * * 2016/1";
                cronStr = ConfigurationManager.AppSettings.Get("cron");
                // Trigger the job to run now, and then every 40 seconds
                //ITrigger trigger = TriggerBuilder.Create()
                //  .WithIdentity("SendNotificationTrigger", "AutoScheduler")
                //  .StartNow()
                //  .WithSimpleSchedule(x => x
                //      //.WithIntervalInHours(1)
                //      .WithIntervalInSeconds(15)
                //      .RepeatForever())
                //  .Build();
                var trigger = TriggerBuilder.Create().WithIdentity("SendNotificationTrigger", "AutoScheduler")
                    .WithCronSchedule(cronStr)
                    .WithPriority(1).Build();
                await _scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog("Register_SendNotificationSchedule", ex);
            }
        }
    }
}