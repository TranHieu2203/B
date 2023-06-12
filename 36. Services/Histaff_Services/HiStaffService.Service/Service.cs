using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.ServiceProcess;
using System.Text;
//using System.ServiceModel;
using System.Threading;
using System.Data.OleDb;
using System.Data;
using Smart.OracleDBServices;
using System.Configuration;
using System.IO;
using System.Collections;
using Smart;
using System.Globalization;
namespace Histaff_Services
{
    public class HiStaff_Service : ServiceBase
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(HiStaff_Service));
        //private string PathArchiveFolder = "";
        //private string PathImportFolder = "";
        //private string PathImportTempFolder = "";
        //private string PathControlFolder = "";
        private string SBOConnectString = "";
        private string BOConnectString = "";
        private int intRequestTimer = 500;
        private string strLogFile = "C:\\Log.txt";
        private string strFilePathBackup = "C:\\LogBackup.txt";
        private int intLogFileSize = 500;
        private int intWriteLog = 0;
        private int isRunRequest = 0;
        private int isRunImport = 0;
        private int isRunExport = 0;
        Thread thrProcess = null;
        Thread thrImport = null;
        Thread thrRunProcess = null;
        Thread thrRunImport = null;
        Thread thrRunExport = null;
        //DateTime dtBusDate;
        //IFormatProvider provider;
        private string tnsName;

        public HiStaff_Service()
        {
            try
            {
                //Utility.WriteLog("Begin to start services", "Histaff_Service");
                log.Info("Init to start services");
                string strValueConfig = string.Empty;
                BOConnectString = ConfigurationManager.AppSettings["BOConnectString"];
                tnsName = ConfigurationManager.AppSettings["BOTNSName"];


                strValueConfig = ConfigurationManager.AppSettings["ScanTimer"];
                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    intRequestTimer = int.Parse(strValueConfig);
                }
                strLogFile = ConfigurationManager.AppSettings["LogFileName"];
                strFilePathBackup = ConfigurationManager.AppSettings["LogFileBackupName"];
                strValueConfig = ConfigurationManager.AppSettings["LogFileSize"];
                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    intLogFileSize = int.Parse(strValueConfig);
                }

                strValueConfig = ConfigurationManager.AppSettings["IsWriteLog"];
                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    intWriteLog = int.Parse(strValueConfig);
                }

                strValueConfig = ConfigurationManager.AppSettings["isRunRequest"];

                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    isRunRequest = int.Parse(strValueConfig);
                }

                strValueConfig = ConfigurationManager.AppSettings["isRunImport"];

                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    isRunImport = int.Parse(strValueConfig);
                }

                strValueConfig = ConfigurationManager.AppSettings["isRunExport"];

                if (strValueConfig != null && strValueConfig != string.Empty)
                {
                    isRunExport = int.Parse(strValueConfig);
                }

                //ThanhNT them phan lay thong tin duong` dan~ thu muc
                //PathArchiveFolder = ConfigurationManager.AppSettings["PathArchiveFolder"];
                //PathImportFolder = ConfigurationManager.AppSettings["PathImportFolder"];
                //PathImportTempFolder = ConfigurationManager.AppSettings["PathImportTempFolder"];
                //PathControlFolder = ConfigurationManager.AppSettings["PathControlFolder"];
                //end

                IFormatProvider provider = new CultureInfo("en-US");
                this.ServiceName = "Histaff_Services";
            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ServiceLog_GetInfo.Func");
                log.ErrorFormat("Init to start services with error: {0}", ex);
            }
        }

        /// <summary>
        /// Init Function Controller Host
        /// </summary>

        protected override void OnStart(string[] args)
        {
            try
            {

                //string des = Path.Combine(strLogFile, "ExceptionLogCash.Func");
                //string sour = Path.Combine(strFilePathBackup, DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogCash", ".Func");

                //BackupFile(des, sour, "ExceptionLogCashTran", ".Func");

                //des = Path.Combine(strLogFile, "ExceptionLogAccount.Func");
                //sour = Path.Combine(strFilePathBackup, "ExceptionLogAccount" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogAccount", ".Func");
                //des = Path.Combine(strLogFile, "ExceptionLogOrder.Func");
                //sour = Path.Combine(strLogFile, "ExceptionLogOrder" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogOrder", ".Func");
                //des = Path.Combine(strLogFile, "ExceptionLogOrderDetail.Func");
                //sour = Path.Combine(strLogFile, "ExceptionLogOrderDetail" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogOrderDetail", ".Func");
                //String str="";
                //DataSet ds = OracleHelper.ExecuteDataset(BOConnectString, "PKG_HCM_SYSTEM.PR_GETMENU", new object[] { 1 });
                //ds.Tables[0].Columns[0].ColumnName = "Group_ID";
                //ds.Tables[0].Columns[1].ColumnName = "Item_ID";
                //ds.Tables[0].TableName = "Group";
                //ds.Tables[1].Columns[1].ColumnName = "Value";
                //ds.Tables[1].Columns[2].ColumnName = "Text";
                //ds.Tables[1].Columns[3].ColumnName = "PostBack";
                //ds.Tables[1].Columns[4].ColumnName = "NavigateUrl";
                //ds.Tables[1].Columns[5].ColumnName = "CheckPermission";
                //ds.Tables[1].TableName = "Item";
                ////str="	<?xml version=""1.0"" standalone=""yes""?>" ;
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    foreach (DataRow drd in ds.Tables[1].Rows)
                //    {
                //        if (dr[1].ToString() == drd[0].ToString())
                //        {

                //        }

                //    }
                //}

                //ds.WriteXml("D:\\Menu.xml");

                //Utility.WriteLog("Start to running services", "Start");
                log.InfoFormat("Started at: {0}", DateTime.Now.ToString("hh:mm"));

                if (isRunRequest > 0)
                {
                    //Utility.WriteLog("Start to running request services", "Start");
                    log.InfoFormat("Start to running request services at: {0}", DateTime.Now.ToString("hh:mm"));
                    //Get all request with state is 'P' (Pending)
                    //run request with strore_execute_in and parameters
                    thrRunProcess = new Thread(() => ScanRequest());
                    thrRunProcess.IsBackground = true;
                    thrRunProcess.Start();

                }

                if (isRunImport > 0)
                {
                    //Utility.WriteLog("Start to running import services", "Start");
                    log.InfoFormat("Start to running import services at: {0}", DateTime.Now.ToString("hh:mm"));
                    //Get all request with state is 'P' (Pending)
                    //run request with strore_execute_in and parameters                
                    thrRunImport = new Thread(() => ScanImport());
                    thrRunImport.IsBackground = true;
                    thrRunImport.Start();
                }
                if (isRunExport > 0)
                {
                    //Utility.WriteLog("Start to running import services", "Start");
                    log.InfoFormat("Start to running import services at: {0}", DateTime.Now.ToString("hh:mm"));
                    //Get all request with state is 'P' (Pending)
                    //run request with strore_execute_in and parameters                
                    thrRunExport = new Thread(() => ScanExport());
                    thrRunExport.IsBackground = true;
                    thrRunExport.Start();
                }

                //WriteOrderLog(DateTime.Now.ToString() + " : Service StockzSync2Back has started!", "ServiceLog.Func");
            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ServiceLog_Start.Func");
                log.ErrorFormat("OnStart error: {0}", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                //string des = Path.Combine(strLogFile, "ExceptionLogCash.Func");
                //string sour = Path.Combine(strFilePathBackup, DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogCash", ".Func");
                //des = Path.Combine(strLogFile, "ExceptionLogAccount.Func");
                //sour = Path.Combine(strFilePathBackup, "ExceptionLogAccount" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogAccount", ".Func");
                //des = Path.Combine(strLogFile, "ExceptionLogOrder.Func");
                //sour = Path.Combine(strLogFile, "ExceptionLogOrder" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogOrder", ".Func");
                //des = Path.Combine(strLogFile, "ExceptionLogOrderDetail.Func");
                //sour = Path.Combine(strLogFile, "ExceptionLogOrderDetail" + DateTime.Now.ToString().Replace(':', '-').Replace('/', ' ') + ".Func");
                //BackupFile(des, sour, "ExceptionLogOrderDetail", ".Func");
                //WriteOrderLog(DateTime.Now.ToString() + " : Service StockzSync2Back has stoped!", "ServiceLog.Func");
                log.Info("Stopping services");

                if (thrProcess != null)
                    thrProcess.Abort();
                if (thrImport != null)
                    thrImport.Abort();
                if (thrRunExport != null)
                    thrRunExport.Abort();
                if (thrRunProcess != null)
                    thrRunProcess.Abort();

                log.InfoFormat("Stoped at: {0}", DateTime.Now.ToString("hh:mm"));
            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ServiceLog_Stop.Func");
                log.ErrorFormat("Stoped error: {0}", ex);
            }
            finally
            {
                
            }

        }

        public void OnStartDebug()
        {
            this.OnStart(null);
        }

        public void OnStopDebug()
        {
            this.OnStop();
        }

        private void ScanRequest()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        DataTable dt = null;

                        dt = OracleHelper.ExecuteDataTable(BOConnectString, "PKG_HCM_SYSTEM.PRO_GET_ALL_LIST_REQUEST", new object[] { 0 });
                        //Utility.WriteLog("Begin to execute request services", "Request");
                        log.Info("Begin to execute request services");
                        if (dt != null)
                        {
                            //Utility.WriteLog("Count request services" + dt.Rows.Count.ToString(), "Request");
                            log.InfoFormat("{0} request services executed", dt.Rows.Count);
                            foreach (DataRow dr in dt.Rows)
                            {
                                //chay store in 
                                //coder phai tu viet cursor de lay ra store in tuong ung  trong than storeprocedure cua minh                            
                                int requestID = Int32.Parse(dr["REQUEST_ID"].ToString());
                                int try_count = Int32.Parse(dr["TRY_COUNT"].ToString());
                                string storeIn = dr["STORE_EXECUTE_IN"].ToString();
                                thrProcess = new Thread(() => RunRequest(requestID, storeIn, try_count));
                                thrProcess.IsBackground = true;
                                thrProcess.Start();
                            }
                        }
                        else
                        {
                            //Utility.WriteLog("No count request services", "Request");
                            log.Info("Not found request services execute");
                        }
                        Thread.Sleep(intRequestTimer);
                    }
                    catch (Exception ex)
                    {
                        //WriteExceptionLog(ex, "ExceptionLogOrder_ScanRequest.Func");
                        log.ErrorFormat("Loop scan request error: {0}", ex);
                    }
                }

            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ExceptionLogOrder_ScanRequest.Func");
                log.ErrorFormat("Scan request error: {0}", ex);
            }

        }
        private void ScanImport()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        DataTable dt = null;

                        dt = OracleHelper.ExecuteDataTable(BOConnectString, "PKG_HCM_SYSTEM.PRO_GET_ALL_LIST_REQUEST", new object[] { 1 });
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                //chay store in 
                                //coder phai tu viet cursor de lay ra store in tuong ung  trong than storeprocedure cua minh                            
                                int requestID = Int32.Parse(dr["REQUEST_ID"].ToString());
                                int try_count = Int32.Parse(dr["TRY_COUNT"].ToString());
                                string storeIn = dr["STORE_EXECUTE_IN"].ToString();
                                thrImport = new Thread(() => RunImport(requestID, storeIn, try_count));
                                thrImport.IsBackground = true;
                                thrImport.Start();
                            }
                        }
                        Thread.Sleep(intRequestTimer);
                    }
                    catch (Exception ex)
                    {
                        //WriteExceptionLog(ex, "ExceptionLogOrder_ScanImport.Func");
                        log.ErrorFormat("Loop scan import error: {0}", ex);
                    }
                }

            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ExceptionLogOrder_ScanImport.Func");
                log.ErrorFormat("Scan import error: {0}", ex);
            }

        }
        private void ScanExport()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        DataTable dt = null;

                        dt = OracleHelper.ExecuteDataTable(BOConnectString, "PKG_HCM_SYSTEM.PRO_GET_ALL_LIST_REQUEST", new object[] { 2 });
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                //chay store in 
                                //coder phai tu viet cursor de lay ra store in tuong ung  trong than storeprocedure cua minh                            
                                int requestID = Int32.Parse(dr["REQUEST_ID"].ToString());
                                int try_count = Int32.Parse(dr["TRY_COUNT"].ToString());
                                string strProgram_Code = dr["CODE"].ToString();
                                string storeIn = dr["STORE_EXECUTE_IN"].ToString();
                                thrImport = new Thread(() => RunExport(requestID, strProgram_Code, storeIn, try_count));
                                thrImport.IsBackground = true;
                                thrImport.Start();
                            }
                        }
                        Thread.Sleep(intRequestTimer);
                    }
                    catch (Exception ex)
                    {
                        //WriteExceptionLog(ex, "ExceptionLogOrder_ScanExport.Func");
                        log.ErrorFormat("Loop scan export error: {0}", ex);
                    }
                }

            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ExceptionLogOrder_ScanExport.Func");
                log.ErrorFormat("Scan Export error: {0}", ex);
            }

        }
        private void RunRequest(int requestid, String storeIn, int try_count)
        {
            DataTable dt = null;
            int i = 0, st = 0;
            int stt_running = 2;
            Object[] LstParamValue = null;
            try
            {
                if (storeIn.Trim().Length == 0)
                {
                    return;
                }

                dt = OracleHelper.ExecuteDataTable(BOConnectString, "PKG_HCM_SYSTEM.PRP_GET_PARAMETERS_FOR_REQUEST", new object[] { requestid });
                ArrayList objArr = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, stt_running }); //RUNNING           
                if (dt != null)
                {
                    if (dt.Rows.Count > 0 && objArr.Count > 0)
                    {
                        LstParamValue = new Object[dt.Rows.Count + 1];
                        foreach (DataRow dr in dt.Rows)
                        {
                            string nv = dr["VALUE"].ToString();
                            LstParamValue[i] = (object)nv;
                            i++;
                        }
                        string request = requestid.ToString();
                        LstParamValue[i] = (object)request;
                        //} 
                    }
                }

                //Run store_In of request      
                int rsSatus = 0;
                ArrayList objArrRS = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, storeIn, LstParamValue);
                rsSatus = int.Parse(objArrRS[0].ToString());

                if (rsSatus == 3)//run complete
                {
                    //update state in AD_REQUESTS + AD_REQUESTS_QUEUES
                    st = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, rsSatus });
                }
                else
                {
                    //update state in AD_REQUESTS + AD_REQUESTS_QUEUES
                    st = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, rsSatus });                 //error do package tra ve
                }
            }
            catch (Exception ex)
            {
                ////ex.Message
                //WriteExceptionLog(ex, "ServiceLog_RunRequest.Func");
                //OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST", new object[] { requestid, ex.Message.ToString() });
                //int rs = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 4 });  
                //WriteExceptionLog(ex, "ServiceLog_RunRequest_GetStatus.Func"); 
                log.ErrorFormat("Run Request with param requestid = {0}, storeIn = {1}, try_count = {2} error: {3}", requestid, storeIn, try_count, ex);
                OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST", new object[] { requestid, ex.Message.ToString() });
                //Index was out of range
                if (ex.Message.Contains("Index was out of range") && try_count <= 2)
                {
                    int rs = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 1 });
                    int rs2 = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_TRY_IN_REQUESTS", new object[] { requestid, try_count + 1 });

                }
                else
                {
                    int rs = OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 4 });
                }
            }
        }
        private void RunExport(int requestid, string strProgram_Code, String storeIn, int try_count)
        {
            Object[] LstParamValue = null;
            DataTable dt = null;
            int i = 0;
            int stt_running = 2;
            try
            {

                //get file csv in folder in request
                ArrayList objectArchive = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER", new object[] { "PATH_ARCHIVE_FOLDER" });
                string archivePath = objectArchive[0].ToString();


                if (storeIn.Trim().Length == 0)
                {
                    return;
                }

                dt = OracleHelper.ExecuteDataTable(BOConnectString, "PKG_HCM_SYSTEM.PRP_GET_PARAMETERS_FOR_REQUEST", new object[] { requestid });
                ArrayList objArr = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, stt_running }); //RUNNING           
                if (dt != null)
                {
                    if (dt.Rows.Count > 0 && objArr.Count > 0)
                    {
                        LstParamValue = new Object[dt.Rows.Count + 1];
                        foreach (DataRow dr in dt.Rows)
                        {
                            string nv = dr["VALUE"].ToString();
                            LstParamValue[i] = (object)nv;
                            i++;
                        }
                        string request = requestid.ToString();
                        LstParamValue[i] = (object)request;
                        //} 
                    }
                }
                //Run store_In of request      
                DataSet dsCSV = new DataSet();

                object objArrRS = OracleHelper.ExecuteNonqueryToDataset(BOConnectString, storeIn, LstParamValue);
                dsCSV = (DataSet)objArrRS;

                //4. Convert sang file csv


                StringBuilder sb = new StringBuilder();
                int iRow = 0;
                while (iRow <= dsCSV.Tables[0].Rows.Count - 1)
                {
                    for (i = 0; i <= dsCSV.Tables[0].Columns.Count - 1; i++)
                    {
                        // sb.Append(dsCSV.Tables[0].Rows[iRow].ItemArray[i].ToString().Replace(",", "☺") + ",");
                        sb.Append(dsCSV.Tables[0].Rows[iRow].ItemArray[i].ToString() + "¦");
                    }
                    sb.Replace("¦", "\n", sb.Length - 1, 1);
                    iRow += 1;
                }

                // 5. Lưu lại file sau khi convert vào folder import
                if (File.Exists(archivePath + "\\" + strProgram_Code + ".csv"))
                {
                    File.Delete(archivePath + "\\" + strProgram_Code + ".csv");
                }

                string output = System.IO.Path.Combine(archivePath, strProgram_Code + ".csv");
                StreamWriter csv = new StreamWriter(output, false);
                csv.Write(sb.ToString());
                csv.Close();
                ArrayList objArr5 = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 3 }); //ERROR           

            }
            catch (Exception ex)
            {
                OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST", new object[] { requestid, ex.Message.ToString() });
                ArrayList objArr4 = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 4 }); //ERROR           

                //WriteExceptionLog(ex, "ServiceLog_RunExport.Func");
                log.ErrorFormat("RunExport with param requestid = {0}, strProgram_Code = {1}, storeIn = {2}, try_count = {3} error: {4}", requestid, strProgram_Code, storeIn, try_count, ex);
                // throw;
            }
        }
        private void RunImport(int requestid, String storeIn, int try_count)
        {
            Process proc = new Process();
            string myCommand = @"CMD.EXE";
            string strConfig = "";
            try
            {
                ArrayList objArr = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 2 }); //RUNNING           

                proc.StartInfo = new ProcessStartInfo(myCommand);

                //get file csv in folder in request
                //Set up arguments for CMD.EXE
                ArrayList objectURLFileControl = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRO_GET_FILECONTROL_URL", new object[] { requestid });
                string fileControlURL = objectURLFileControl[0].ToString();

                //get file csv in folder in request
                //Set up arguments for CMD.EXE
                ArrayList objectCSVFile = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER", new object[] { "PATH_IMPORT_FOLDER" });
                string fileCSV = objectCSVFile[0].ToString();

                //lay phan he theo request id
                ArrayList objectModule = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRR_GET_MODULE_IN_REQUEST", new object[] { requestid });
                string modulePath = objectModule[0].ToString();

                ArrayList objectCONTROL_PATH = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER", new object[] { "PATH_CONTROL_FOLDER" });
                string fileControlPath = objectCONTROL_PATH[0].ToString();

                strConfig = @"/c SQLLDR " + tnsName + " CONTROL=" + fileControlPath + "\\" + modulePath + "\\" + fileControlURL + " log='log_" + requestid + ".log'";
                proc.StartInfo.Arguments = strConfig;

                //adfds\dsfds.txt

                proc.StartInfo.RedirectStandardOutput = true;

                proc.StartInfo.RedirectStandardError = true;

                proc.StartInfo.UseShellExecute = false;

                proc.StartInfo.WorkingDirectory = @"" + fileCSV + "\\" + modulePath;
                proc.Start();
                proc.WaitForExit();
                if (proc.ExitCode == 0)
                {
                    //move file csv qua thu muc Archive
                    ArrayList objectProgramCode = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.READ_PROGRAM_WITH_REQUEST_ID", new object[] { requestid });
                    string programCode = objectProgramCode[0].ToString();

                    ArrayList objectArchive = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER", new object[] { "PATH_ARCHIVE_FOLDER" });
                    string archivePath = objectArchive[0].ToString();

                    System.IO.File.Move(System.IO.Path.Combine(fileCSV + "\\" + modulePath, programCode + ".csv"), System.IO.Path.Combine(archivePath + "\\" + modulePath, programCode + "_" + requestid + ".csv"));
                    //Thuc thi store, neu co.
                    ArrayList objArr2 = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 5 }); //RUNNING        
                    RunRequest(requestid, storeIn, try_count);
                }
                else
                {
                    ArrayList objArr5 = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 4 }); //ERROR           
                }
            }
            catch (Exception ex)
            {
                OracleHelper.ExecuteNonquery(BOConnectString, "PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST", new object[] { requestid, ex.Message.ToString() });
                ArrayList objArr4 = OracleHelper.ExecuteNonquery_ReturnValue(BOConnectString, "PKG_HCM_SYSTEM.PRU_STATE_IN_REQUESTS", new object[] { requestid, 4 }); //ERROR           
                proc.Close();
                WriteExceptionLog(ex, "ServiceLog_RunImport.Func");
                log.ErrorFormat("RunImport with param requestid = {0}, storeIn = {1}, try_count = {2} error: {3}", requestid, storeIn, try_count, ex);
                // throw;
            }


        }

        private void ScanOrder()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        UpdateOrderFront2Back();
                        Thread.Sleep(intRequestTimer);
                    }
                    catch (Exception ex)
                    {
                        //WriteExceptionLog(ex, "ExceptionLogOrder.Func");
                        log.ErrorFormat("Loop ScanOrder error: {0}", ex);
                    }
                }

            }
            catch (Exception ex)
            {
                //WriteExceptionLog(ex, "ExceptionLogOrder.Func");
                log.ErrorFormat("ScanOrder error: {0}", ex);
            }

        }

        private void UpdateOrderFront2Back()
        {
            string strSuccess = "";
            string strUnSuccess = "";
            try
            {
                DataTable dt = null;
                ArrayList objArr = null;
                int intResult = 0;
                //for in req
                dt = OracleHelper.ExecuteDataTable(BOConnectString, "pkg_odSync2back.getodtran2back", new object[] { 1 });

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objArr = OracleHelper.ExecuteNonquery_ReturnValue(SBOConnectString, "pkg_Sync.SyncODfromFO",
                            new object[] {double.Parse( dr["ordernumber"].ToString()),
                                dr["CONFIRM_NUMBER"].ToString(),
                                dr["account_no"].ToString(),                                
                                dr["stock_code"].ToString(),
                                int.Parse( dr["oorb"].ToString()),
                                int.Parse( dr["norp"].ToString()),
                                int.Parse( dr["qtty"].ToString()),
                                double.Parse( dr["price"].ToString()),
                                int.Parse( dr["ORDERTYPE"].ToString()),                                
                                double.Parse((dr["secureratio"].ToString() != "" ? dr["secureratio"].ToString() : "0")), 
                                double.Parse((dr["brokerid"].ToString() != "" ? dr["brokerid"].ToString() : "0")),                               
                                0,
                                "",
                                dr["coaccount_no"].ToString(),
                                "",
                                dr["contact"].ToString(),         
                                dr["channel"].ToString(),
                                dr["notes"].ToString(),
                                dr["CREATED_BY"].ToString(),
                                "",
                                "VND",                                                                                     
                                dr["TRAN_DATE"].ToString(),
                                dr["ODSTATUS"].ToString(),
                                int.Parse(dr["SESSIONEX"].ToString()),
                                int.Parse(dr["NOM"].ToString())
                            });

                        if (objArr != null && objArr.Count > 0)
                        {
                            intResult = int.Parse(objArr[0].ToString());
                            if (intResult == 1)
                            {
                                strSuccess = strSuccess + dr["ORDER_ID"].ToString() + "@";
                            }
                            else
                            {
                                strUnSuccess = strUnSuccess + dr["ORDER_ID"].ToString() + "@";
                            }
                        }
                    }
                    if (strSuccess != "" || strUnSuccess != "")
                    {
                        OracleHelper.ExecuteScalar(BOConnectString, "pkg_odSync2back.updateodtran2back", new object[] { strSuccess, strUnSuccess });
                    }
                }
                strSuccess = "";
                strUnSuccess = "";
            }
            catch (Exception ex)
            {
                if (strSuccess != "" || strUnSuccess != "")
                {
                    OracleHelper.ExecuteScalar(BOConnectString, "pkg_odSync2back.updateodtran2back", new object[] { strSuccess, strUnSuccess });
                }
                //WriteExceptionLog(ex1, "ExceptionLogOrder.Func");
                log.ErrorFormat("UpdateOrderFront2Back error: {0}", ex);
            }

        }

        private void LoadData()
        {

        }
        private void BackupFile(string des, string sour, string strFileName, string strFileFormat)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(strLogFile);
                FileInfo[] fileNames = dirInfo.GetFiles(strFileName + strFileFormat);
                if (fileNames.Length > 0)
                {
                    if (fileNames[0].Length / 1024 > intLogFileSize)
                    {
                        if (File.Exists(strFilePathBackup) == true)
                        {
                            File.Move(des, sour);
                        }
                        else
                        {
                            Directory.CreateDirectory(strFilePathBackup);
                            File.Move(des, sour);
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }
        private void WriteExceptionLog(Exception ex, string strFileName)
        {
            try
            {
                Utility.WriteExceptionLog(ex, strFileName, strLogFile);

                //string strFilePath = Path.Combine(strLogFile, strFileName);
                //string strLogMessage = "Time : " + DateTime.Now.ToString() + " // Function Name : " + ex.TargetSite + " // Exception info : " + ex.Message;
                ////string strLogMessage = strMessageTest;
                //if (File.Exists(strFilePath) == true)
                //{
                //    FileStream fileStream = new FileStream(strFilePath, FileMode.Append);
                //    StreamWriter w = new StreamWriter(fileStream, Encoding.Unicode);
                //    w.WriteLine(strLogMessage);
                //    w.Flush();
                //    w.Close();
                //    fileStream.Close();
                //    //File.AppendAllText(strFilePath, strLogMessage + "\r", Encoding.Unicode);
                //}
                //else
                //{
                //    Directory.CreateDirectory(strLogFile);
                //    //File.AppendAllText(strFilePath, strLogMessage + "\r", Encoding.Unicode);
                //    FileStream fileStream = new FileStream(strFilePath, FileMode.Append);
                //    StreamWriter w = new StreamWriter(fileStream, Encoding.Unicode);
                //    w.WriteLine(strLogMessage);
                //    w.Flush();
                //    w.Close();
                //    fileStream.Close();
                //}
            }
            catch
            {

                return;
            }
        }
        private void WriteOrderLog(string srtMessSent, string strFileName)
        {
            try
            {
                string strFilePath = Path.Combine(strLogFile, strFileName);
                //string strLogMessage = "Time : " + DateTime.Now.ToString() + " // Function Name : " + ex.TargetSite + " // Exception info : " + ex.Message;
                string strLogMessage = srtMessSent;
                if (File.Exists(strFilePath) == true)
                {
                    FileStream fileStream = new FileStream(strFilePath, FileMode.Append);
                    StreamWriter w = new StreamWriter(fileStream, Encoding.Unicode);
                    w.WriteLine(strLogMessage);
                    w.Flush();
                    w.Close();
                    fileStream.Close();
                    //File.AppendAllText(strFilePath, strLogMessage + "\r", Encoding.Unicode);
                }
                else
                {
                    Directory.CreateDirectory(strLogFile);
                    //File.AppendAllText(strFilePath, strLogMessage + "\r", Encoding.Unicode);
                    FileStream fileStream = new FileStream(strFilePath, FileMode.Append);
                    StreamWriter w = new StreamWriter(fileStream, Encoding.Unicode);
                    w.WriteLine(strLogMessage);
                    w.Flush();
                    w.Close();
                    fileStream.Close();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
