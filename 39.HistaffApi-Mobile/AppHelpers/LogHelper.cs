using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppHelpers
{
    public class LogHelper
    {
        public static readonly ILog SystemLog = LogManager.GetLogger("log4net-default-repository", "LogInputFile");
        public static readonly ILog ErrorLog = LogManager.GetLogger("log4net-default-repository", "LogErrorFile");
        public static readonly ILog AuthorizeLog = LogManager.GetLogger("log4net-default-repository", "LogAuthorizeFile");
        public static readonly ILog GeneralLog = LogManager.GetLogger("log4net-default-repository", "LogGeneralFile");


        public static void WriteExceptionToLog(object message)
        {
            try
            {
                ErrorLog.Error(message);
            }
            catch { }
        }
        public static void WriteExceptionToLog(object message, Exception ex)
        {
            try
            {
                ErrorLog.Error(message, ex);
            }
            catch { }
        }

        public static void WriteGeneralLog(object msg)
        {
            try
            {
                GeneralLog.Info(msg);
            }
            catch { }
        }

        public static void WriteGeneralLog(object msg, Exception ex)
        {
            GeneralLog.Info(msg, ex);
        }

        public static void WriteAuthorizeLog(object msg)
        {
            try
            {
                AuthorizeLog.Info(msg);
            }
            catch { }
        }
        public static void WriteAuthorizeLog(object msg, Exception ex)
        {
            try
            {
                AuthorizeLog.Info(msg, ex);
            }
            catch { }
        }

        public static void WriteSystemLog(object msg)
        {
            try { SystemLog.Info(msg); } catch { }
        }
        public static void WriteSystemLog(object msg, Exception ex)
        {
            try
            {
                SystemLog.Info(msg, ex);
            }
            catch { }
        }

    }
}
