using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using System.IO;

public static class Utility
{
    /// <summary>
    /// Writes the exception log.
    /// </summary>
    /// <param name="objErr">The obj err.</param>
    /// <param name="sFunc">The s func.</param>
    public static void WriteExceptionLog(Exception objErr, string sFunc, string strFolder = "")
    {
        StringBuilder err = new StringBuilder();
        string logFile = string.Empty;
        StreamWriter logWriter = null;
        string logfolder = null;
        string logformat = null;
        string strLogFolder;
        try
        {
            err.AppendLine("Function: " + sFunc);
            err.AppendLine("Datetime: " + DateTime.Now);
            err.AppendLine("Error message: " + objErr.Message);
            err.AppendLine("Stack trace: " + objErr.StackTrace);

            if (objErr.InnerException != null)
            {
                err.AppendLine("Inner error message: " + objErr.InnerException.Message);
                err.AppendLine("Inner stack trace: " + objErr.InnerException.StackTrace);
            }

            // err.AppendLine("Other Text: " + OtherText);
            logfolder = "Exception";
            if (string.IsNullOrEmpty(logfolder))
                logfolder = "Exception";

            if (string.IsNullOrEmpty(strFolder))
            {
                strLogFolder = strFolder;
            }
            else
            {
                strLogFolder = AppDomain.CurrentDomain.BaseDirectory + "\\" + logfolder;
            }



            logformat = "yyyyMMdd.log";

            if (!Directory.Exists(strLogFolder))
            {
                Directory.CreateDirectory(strLogFolder);
            }

            logFile = strLogFolder + "\\" + DateTime.Now.ToString(logformat);

            if (File.Exists(logFile))
            {
                logWriter = File.AppendText(logFile);
            }
            else
            {
                logWriter = File.CreateText(logFile);
            }

            logWriter.WriteLine(err.ToString());
            logWriter.Close();

        }
        catch (Exception e)
        {
        }
    }
    public static void WriteLog(string strLog, string sFunc, string strFolder = "")
    {
        StringBuilder err = new StringBuilder();
        string logFile = string.Empty;
        StreamWriter logWriter = null;
        string logfolder = null;
        string logformat = null;
        string strLogFolder;
        try
        {
            err.AppendLine("Function: " + sFunc);
            err.AppendLine("Datetime: " + DateTime.Now);
            err.AppendLine("Log message: " + strLog);


            // err.AppendLine("Other Text: " + OtherText);
            logfolder = "Exception";
            if (string.IsNullOrEmpty(logfolder))
                logfolder = "Exception";

            if (string.IsNullOrEmpty(strFolder))
            {
                strLogFolder = strFolder;
            }
            else
            {
                strLogFolder = AppDomain.CurrentDomain.BaseDirectory + "\\" + logfolder;
            }



            logformat = "yyyyMMdd.log";

            if (!Directory.Exists(strLogFolder))
            {
                Directory.CreateDirectory(strLogFolder);
            }

            logFile = strLogFolder + "\\" + DateTime.Now.ToString(logformat);

            if (File.Exists(logFile))
            {
                logWriter = File.AppendText(logFile);
            }
            else
            {
                logWriter = File.CreateText(logFile);
            }

            logWriter.WriteLine(err.ToString());
            logWriter.Close();

        }
        catch (Exception e)
        {
        }
    }
    public static double DateDiffDay(System.DateTime startdate, System.DateTime enddate)
    {
        try
        {
            TimeSpan sDate = enddate - startdate;
            return sDate.TotalDays + 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}