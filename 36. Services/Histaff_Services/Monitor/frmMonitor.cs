using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using Smart.OracleDBServices;
using System.Timers;
namespace MainForm
{
    public partial class frmMonitor : Form
    {
        public ServiceController[] services;
        public ServiceController bosc2service;
        private string strLogFile = "C:\\Log.txt";
        string conStr = "user id=LIVETRADEHNX;password=bosc;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=itgroup)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=Test))); User Id=LIVETRADEHNX;Password=bosc;";
        private string sqlSelect = "select MTI,TRANSCODE,TRANSSUBCODE,TRANSDATE,TRANSTIME,SECSYMBOL,TRANSDESC,TRANSSTATUS,TRANSTYPE,AMOUNT,PRICE,FIRM,TRADEID,BOARD,SRCACCOUNT,CLIENTFLAG,BOORGTRANSNUM,BOORGSUBTRANNUM,LOCALMACHINE,LOCALIPADDRESS,MAKERID,CHECKERID,SECUSERNAME,SECUSERPASSWORD from HNX_ORDERS_OUT ";
       // OracleConnection con;
        private DataSet dsOrder = null;
        private DataTable dtNormalOrder = null;
        private DataTable dtPutOrder = null;
        private string strFilePathBackup = "C:\\LogBackup.txt";
        private int intLogFileSize = 500;
        private int intRequestTimer = 500;
        //wcfService.WCFQNRequestState a = new WCFQNRequestState();
        private delegate void AppendTextHandler(DataTable dt);
        private delegate void FillNormalOrderHandler();
        private Thread thrMain;
        private Thread thrChildren;
        public frmMonitor()
        {
            InitializeComponent();
            string strValueConfig = string.Empty;
            conStr = ConfigurationManager.AppSettings["ConnectionString"];
            strValueConfig = ConfigurationManager.AppSettings["ScanTimer"];
            if (strValueConfig != string.Empty)
            {
                intRequestTimer = int.Parse(strValueConfig);
            }
            //strValueConfig = ConfigurationManager.AppSettings["OracleVersionNumber"];
            //if (strValueConfig != string.Empty)
            //{
            //    intOracleVersionNumber = int.Parse(strValueConfig);
            //}
            sqlSelect = ConfigurationManager.AppSettings["SQL"];
            strLogFile = ConfigurationManager.AppSettings["LogFileName"];
            strFilePathBackup = ConfigurationManager.AppSettings["LogFileBackupName"];
            strValueConfig = ConfigurationManager.AppSettings["LogFileSize"];
            if (strValueConfig != null && strValueConfig != string.Empty)
            {
                intLogFileSize = int.Parse(strValueConfig);
            }
            con = new OracleConnection(conStr); 
            //ODBNetProcess();
            cboType.SelectedIndex = 0;
        }
        private void LoadService()
        {
            try
            {
                services = ServiceController.GetServices();
                ListViewItem datalist;
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == "BOSC2LiveTrade")
                    {
                        bosc2service = service;
                        ServiceList.Items.Clear();
                        datalist = new System.Windows.Forms.ListViewItem(service.ServiceName.ToString());
                        datalist.SubItems.Add(service.DisplayName);
                        datalist.SubItems.Add(service.Status.ToString());
                        ServiceList.Items.Add(datalist);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadAllService()
        {
            try
            {
                ServiceList.Items.Clear();
                services = ServiceController.GetServices();
                ListViewItem datalist;
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == "BOSC2LiveTrade")
                    {
                        bosc2service = service;
                    }
                    datalist = new System.Windows.Forms.ListViewItem(service.ServiceName.ToString());
                    datalist.SubItems.Add(service.DisplayName);
                    datalist.SubItems.Add(service.Status.ToString());
                    ServiceList.Items.Add(datalist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshNormalOrderList(DataTable dt)
        {
            try
            {
                if (griNormalOrder.InvokeRequired)
                    griNormalOrder.Invoke(new AppendTextHandler(RefreshNormalOrderList), new object[] { dt });
                else
                {
                    griNormalOrder.DataSource = dt;
                    if (dt != null)
                    {
                        lblnumRow.Text = "Number of Record :" + dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblnumRow.Text = "Number of Record : 0";
                    }
                }
            }
            catch (Exception ex)
            {

                WriteExceptionLog(ex);
            }
            
        }
        private void RefreshPutOrderList(DataTable dt)
        {
            try
            {
                if (griPutOrder.InvokeRequired)
                    griPutOrder.Invoke(new AppendTextHandler(RefreshPutOrderList), new object[] { dt });
                else
                {
                    griPutOrder.DataSource = dt;
                    if (dt != null)
                    {
                        lblnumRow.Text = "Number of Record :" + dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblnumRow.Text = "Number of Record : 0";
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }
        private void RefreshGridView()
        {
            while (true)
            {
                try
                {

                    if (tabOrder.InvokeRequired )
                        tabOrder.Invoke(new FillNormalOrderHandler(RefreshGridView));
                    else
                    {
                        ProcessView();
                    }
                }
                catch (Exception ex)
                {
                    //WriteExceptionLog(ex);
                    MessageBox.Show(ex.Message);
                }
                Thread.Sleep(1000);
            }
            
        }
        private void ProcessView()
        {
            try
            {
                if (tabOrder.SelectedIndex == 0)
                {
                    lblnumRow.Visible = false;
                    lblStatus.Visible = false;
                    cboType.Visible = false;
                    btnView.Visible = true;
                    btnViewAll.Visible = true;
                    btnStart.Visible = true;
                    btnStop.Visible = true;
                    btnPause.Visible = true;
                    btnContinute.Visible = true;
                    return;
                }
                lblnumRow.Visible = true;
                lblStatus.Visible = true;
                cboType.Visible = true;
                btnView.Visible = false;
                btnViewAll.Visible = false;
                btnStart.Visible = false;
                btnStop.Visible = false;
                btnPause.Visible = false;
                btnContinute.Visible = false;
                int intType = 0;
                IEnumerable<DataRow> queryNomalOrder = null;
                if (tabOrder.SelectedIndex == 1)
                {

                    switch (cboType.SelectedIndex)
                    {
                        case 0:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0"
                                                  select order;
                                break;
                            }
                        case 1:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "1"
                                                  select order;
                                break;
                            }
                        case 2:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() != "1"
                                                  select order;
                                break;
                            }
                        case 3:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "4"
                                                  select order;
                                break;
                            }
                        case 4:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "5"
                                                  select order;
                                break;
                            }
                        default:
                            break;
                    }

                    if (queryNomalOrder.Count() > 0)
                    {
                        //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
                        RefreshNormalOrderList(queryNomalOrder.CopyToDataTable());
                    }
                    else
                    {
                        RefreshNormalOrderList(null);
                    }
                }
                else
                {
                    switch (cboType.SelectedIndex)
                    {
                        case 0:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0"
                                                  select order;
                                break;
                            }
                        case 1:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "1"
                                                  select order;
                                break;
                            }
                        case 2:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() != "1"
                                                  select order;
                                break;
                            }
                        case 3:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "4"
                                                  select order;
                                break;
                            }
                        case 4:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "5"
                                                  select order;
                                break;
                            }
                        default:
                            break;
                    }

                    if (queryNomalOrder.Count() > 0)
                    {
                        //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
                        RefreshPutOrderList(queryNomalOrder.CopyToDataTable());
                    }
                    else
                    {
                        RefreshPutOrderList(null);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ODBNetProcess()
        {
            try
            {
                //dsNormalOrder = SubmitDataRequest(sqlSelect + " where TRANSSUBCODE = 0");
                //griNormalOrder.DataSource = dsNormalOrder.Tables[0];
                //dsPutOrder = SubmitDataRequest(sqlSelect + " where TRANSSUBCODE > 0");
                //griPutOrder.DataSource = dsPutOrder.Tables[0];
                dsOrder = SubmitDataRequest(sqlSelect);
                if (dsOrder != null && dsOrder.Tables.Count > 0)
                {
                    //DataColumn colum = new DataColumn();
                    //colum.ColumnName = "BOORGSUBTRANNUM";
                    dsOrder.Tables[0].PrimaryKey = new DataColumn[] { dsOrder.Tables[0].Columns["BOORGSUBTRANNUM"] };
                    IEnumerable<DataRow> queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                           where order.Field<string>("TRANSSUBCODE") == "0"
                                                           select order;
                    if (queryNomalOrder.Count() > 0)
                    {
                        //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
                        RefreshNormalOrderList(queryNomalOrder.CopyToDataTable());
                    }
                    IEnumerable<DataRow> queryPutOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                         where order.Field<string>("TRANSSUBCODE") != "0"
                                                         select order;
                    if (queryPutOrder.Count() > 0)
                    {
                        //griPutOrder.DataSource = queryPutOrder.CopyToDataTable();
                        RefreshPutOrderList(queryPutOrder.CopyToDataTable());
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
        }
        private DataSet SubmitDataRequest(string strSql)
        {
            try
            {
                OracleDependency dep = null;
                OracleDataAdapter da = null;
                OracleCommand cmd = null;
                DataSet ds = new DataSet();
                cmd = new OracleCommand(strSql, con);
                //cmd = new OracleCommand("select * from sgo.adallcode", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.AddRowid = true;
                dep = new OracleDependency(cmd);
                cmd.Notification.IsNotifiedOnce = false;
                dep.OnChange += new OnChangeEventHandler(dep_OnChange);
                da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return null;
            }
        }
        private DataSet SubmitDataRequest1(string strSql)
        {
            try
            {
                OracleDataAdapter da = null;
                OracleCommand cmd = null;
                DataSet ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd = new OracleCommand(strSql, con);
                //cmd = new OracleCommand("select * from adallcode", con);
                da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return null;
            }
        }
        private void WriteExceptionLog(Exception ex)
        {
            try
            {
                string strFilePath = Path.Combine(strLogFile, "ExceptionLog.txt");
                string strLogMessage = "Time : " + DateTime.Now.ToString() + " // Function Name : " + ex.TargetSite + " // Exception info : " + ex.Message;
                //string strLogMessage = strMessageTest;
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
        /// <summary>
        /// Called by SqlDependency when data has changed
        /// </summary>
        //private void dep_OnChange(object sender, OracleNotificationEventArgs e)
        //{
        //    try
        //    {
               
        //        if (e.Type == OracleNotificationType.Change)
        //        {
        //            //DataTable ds = new DataTable();
        //            //IEnumerable<DataRow> matchingRows = (from DataRow row in e.Details.Rows
        //            //                                     select row);
        //            //matchingRows = matchingRows.Distinct();
        //            //// IEnumerable<DataRow> query = 
        //            //     (from row in e.Details.AsEnumerable()
        //            //      select row).Distinct();
        //            IEnumerable<string> matchingRows = (from row in e.Details.AsEnumerable()
        //                                                select row.Field<string>("Rowid")).Distinct();
        //            string strWhere = "(";
        //            //foreach (DataRow dr in e.Details.Rows)
        //            //{
        //            //    strWhere = strWhere + "'" + dr["Rowid"].ToString() + "',";
        //            //}
        //            foreach (string strRowid in matchingRows)
        //            {
        //                if (strRowid !=null && strRowid != string.Empty)
        //                {
        //                    strWhere = strWhere + "'" + strRowid.ToString() + "',";
        //                }
        //            }
        //            strWhere = strWhere.Substring(0, strWhere.Length - 1) + ")";
        //            string strSql = sqlSelect + " where rowid in " + strWhere;
                   
        //              DataSet ds =  SubmitDataRequest1(strSql);

        //              if (ds != null && ds.Tables.Count > 0)
        //              {
        //                  if (dsOrder == null || dsOrder.Tables.Count==0)
        //                  {
        //                      dsOrder = ds;
        //                      dsOrder.Tables[0].PrimaryKey = new DataColumn[] { dsOrder.Tables[0].Columns["BOORGSUBTRANNUM"] };
        //                  }
        //                  else
        //                  {
        //                      foreach (DataRow dr in ds.Tables[0].Rows)
        //                      {
        //                          if (e.Info.ToString() == "Insert")
        //                          {
        //                              dsOrder.Tables[0].ImportRow(dr);
        //                              //dsOrder.Tables[0].Merge(ds.Tables[0]);
        //                              //dsOrder.Tables[0].
        //                          }
        //                          if (e.Info.ToString() == "Delete")
        //                          {
        //                              DataRow dr2 = dsOrder.Tables[0].Rows.Find(dr["BOORGSUBTRANNUM"]);
        //                              if (dr != null)
        //                              {
        //                                  dsOrder.Tables[0].Rows.Remove(dr2);
        //                              }
        //                          }
        //                          if (e.Info.ToString() == "Update")
        //                          {
        //                              DataRow dr2 = dsOrder.Tables[0].Rows.Find(dr["BOORGSUBTRANNUM"]);
        //                              if (dr != null)
        //                              {
        //                                  dsOrder.Tables[0].Rows.Remove(dr2);
        //                                  dsOrder.Tables[0].ImportRow(dr);
        //                              }
                                      
        //                              //IEnumerable<DataRow> queryPutOrder = from order in dsOrder.Tables[0].AsEnumerable()
        //                              //                                     where order.Field<string>("BOORGSUBTRANNUM") == dr["BOORGSUBTRANNUM"].ToString()
        //                              //                                     select order;
        //                              //dsOrder.Tables[0].LoadDataRow(new[] { dr["BOORGSUBTRANNUM"] },LoadOption.OverwriteChanges);
        //                              //dsOrder.Tables[0].Rows.Remove(queryPutOrder.CopyToDataTable().Rows[0]);
        //                              //dsOrder.Tables[0].Rows.Add(dr);
        //                          }
        //                      }
        //                  }
        //                  if (dsOrder != null && dsOrder.Tables.Count > 0)
        //                  {
        //                      IEnumerable<DataRow> queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
        //                                                             where order.Field<string>("TRANSSUBCODE") == "0"
        //                                                             select order;
        //                      if (queryNomalOrder.Count() > 0)
        //                      {
        //                          //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
        //                          RefreshNormalOrderList(queryNomalOrder.CopyToDataTable());
        //                      }
        //                      else
        //                      {
        //                          RefreshNormalOrderList(null);
        //                      }
        //                      IEnumerable<DataRow> queryPutOrder = from order in dsOrder.Tables[0].AsEnumerable()
        //                                                           where order.Field<string>("TRANSSUBCODE") != "0"
        //                                                           select order;
        //                      if (queryPutOrder.Count() > 0)
        //                      {
        //                          //griPutOrder.DataSource = queryPutOrder.CopyToDataTable();
        //                          RefreshPutOrderList(queryPutOrder.CopyToDataTable());
        //                      }
        //                      else
        //                      {
        //                          RefreshPutOrderList(null);
        //                      }
        //                  }
        //              }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteExceptionLog(ex);
        //    }
        //}
        private void cboTableList_TextChanged(object sender, EventArgs e)
        {
            ////DataSet ds = new DataSet();
            //if (cboTableList.Text != "")
            //{
            //    //ds.Tables.Add(a.DataBase.Tables[cboTableList.Text].Copy());
            //    grdViewer.DataSource = dSet.Tables[cboTableList.Text];
            //}
        }
        private void ScanDB()
        {
            try
            {
                //DataSet ds = null;
                while (true)
                {
                    try
                    {
                        dsOrder = OracleHelper.ExecuteDataset(conStr, CommandType.Text, sqlSelect);
                        //OracleHelper.FillDataSet(conStr, ds, "SELECTANDUPDATE", new object[] { });
                        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        //{
                        //    //string strGatewayResutl = requestResult.ProcessMain(ds.GetXml());
                        //    //ProcessOders(ds.GetXml());
                        //    // new ProcessOrder(ProcessOders(ds.GetXml()));
                        //    ds = null;
                        //}
                        Thread.Sleep(intRequestTimer);
                    }
                    catch (Exception ex1)
                    {
                        WriteExceptionLog(ex1);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }

        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabOrder.SelectedIndex == 0)
                {
                    lblnumRow.Visible = false;
                    lblStatus.Visible = false;
                    cboType.Visible = false;
                    btnView.Visible = true;
                    btnViewAll.Visible = true;
                    btnStart.Visible = true;
                    btnStop.Visible = true;
                    btnPause.Visible = true;
                    btnContinute.Visible = true;
                    return;
                }
                lblnumRow.Visible = true;
                lblStatus.Visible = true;
                cboType.Visible = true;
                btnView.Visible = false;
                btnViewAll.Visible = false;
                btnStart.Visible = false;
                btnStop.Visible = false;
                btnPause.Visible = false;
                btnContinute.Visible = false;
                int intType = 0;
                IEnumerable<DataRow> queryNomalOrder = null;
                if (tabOrder.SelectedIndex == 1)
                {

                    switch (cboType.SelectedIndex)
                    {
                        case 0:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0"
                                                  select order;
                                break;
                            }
                        case 1:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "1"
                                                  select order;
                                break;
                            }
                        case 2:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() != "1"
                                                  select order;
                                break;
                            }
                        case 3:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "4"
                                                  select order;
                                break;
                            }
                        case 4:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") == "0" && order.Field<Decimal>("STATUS").ToString() == "5"
                                                  select order;
                                break;
                            }
                        default:
                            break;
                    }

                    if (queryNomalOrder.Count() > 0)
                    {
                        //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
                        RefreshNormalOrderList(queryNomalOrder.CopyToDataTable());
                    }
                    else
                    {
                        RefreshNormalOrderList(null);
                    }
                }
                else
                {
                    switch (cboType.SelectedIndex)
                    {
                        case 0:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0"
                                                  select order;
                                break;
                            }
                        case 1:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "1"
                                                  select order;
                                break;
                            }
                        case 2:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() != "1"
                                                  select order;
                                break;
                            }
                        case 3:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "4"
                                                  select order;
                                break;
                            }
                        case 4:
                            {
                                queryNomalOrder = from order in dsOrder.Tables[0].AsEnumerable()
                                                  where order.Field<string>("TRANSSUBCODE") != "0" && order.Field<Decimal>("STATUS").ToString() == "5"
                                                  select order;
                                break;
                            }
                        default:
                            break;
                    }

                    if (queryNomalOrder.Count() > 0)
                    {
                        //griNormalOrder.DataSource = queryNomalOrder.CopyToDataTable();
                        RefreshPutOrderList(queryNomalOrder.CopyToDataTable());
                    }
                    else
                    {
                        RefreshPutOrderList(null);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void tabOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboType_SelectedIndexChanged(sender, null);
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            LoadService();
            btnContinute.Enabled = false;
            btnPause.Enabled = false;
            thrMain = new Thread(new ThreadStart(ScanDB));
            thrMain.IsBackground = true;
            thrMain.Start();
            //thrChildren = new Thread(new ThreadStart(RefreshGridView));
            //thrChildren.IsBackground = true;
            //thrChildren.Start();
           

        }
       private void TickHandler(object sender, EventArgs e)
        {
            // do some work
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceList.Items.Count == 1)
                {
                    LoadService();
                    if (bosc2service.CanStop)
                    {
                        bosc2service.Stop();
                        btnStart.Enabled = true;
                        btnStop.Enabled = false;
                        ServiceList.Items[0].SubItems[2].Text = "Stopped";
                    }
                    else
                    {
                        MessageBox.Show("This service couldn't be stopped", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (ServiceList.SelectedItems.Count != 0)
                    {
                        int n = ServiceList.FocusedItem.Index;
                        string strServiceName = ServiceList.FocusedItem.SubItems[0].Text;
                        LoadAllService();
                        if (services[n].ServiceName != strServiceName)
                        {
                            MessageBox.Show("Service list is old, Please Click View All button!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnViewAll.Focus();
                            return;
                        }
                        if (services[n].CanStop)
                        {
                            services[n].Stop();
                            btnStart.Enabled = true;
                            btnStop.Enabled = false;
                            ServiceList.Items[n].SubItems[2].Text = "Stopped";
                        }
                        else
                        {
                            MessageBox.Show("This service couldn't be stopped", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please,choose a service that you want to stop", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceList.Items.Count == 1)
                {
                    LoadService();
                    if (bosc2service.Status==ServiceControllerStatus.Stopped)
                    {
                        bosc2service.Start();
                        btnStart.Enabled = false;
                        btnStop.Enabled = true;
                        ServiceList.Items[0].SubItems[2].Text = "Started";
                    }
                    else
                    {
                        MessageBox.Show("This service couldn't be started!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (ServiceList.SelectedItems.Count != 0)
                    {
                        int n = ServiceList.FocusedItem.Index;
                        string strServiceName = ServiceList.FocusedItem.SubItems[0].Text;
                        LoadAllService();
                        if (services[n].ServiceName != strServiceName)
                        {
                            MessageBox.Show("Service list is old, Please Click View All button!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnViewAll.Focus();
                            return;
                        }
                        ServiceController service = null;
                        //service = services.ToDictionary(

                        if (services[n].Status == ServiceControllerStatus.Stopped)
                        {
                            services[n].Start();
                            btnStart.Enabled = false;
                            btnStop.Enabled = true;
                            //ServiceList.Items[0].SubItems["STATUS"].Text = "Started";
                            ServiceList.Items[n].SubItems[2].Text = "Started";
                        }
                        else
                        {
                            MessageBox.Show("This service couldn't be started", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please,choose a service that you want to start", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadService();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadAllService();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceList.Items.Count == 1)
                {
                    LoadService();
                    if (bosc2service.CanPauseAndContinue && bosc2service.Status == ServiceControllerStatus.Running)
                    {
                        bosc2service.Pause();
                        btnContinute.Enabled = true;
                        btnPause.Enabled = false;
                        ServiceList.Items[0].SubItems[2].Text = "Paused";
                    }
                    else
                    {
                        MessageBox.Show("This service couldn't be Paused !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (ServiceList.SelectedItems.Count != 0)
                    {
                        int n = ServiceList.FocusedItem.Index;
                        string strServiceName = ServiceList.FocusedItem.SubItems[0].Text;
                        LoadAllService();
                        if (services[n].ServiceName != strServiceName)
                        {
                            MessageBox.Show("Service list is old, Please Click View All button!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnViewAll.Focus();
                            return;
                        }
                        if (services[n].CanPauseAndContinue && services[n].Status == ServiceControllerStatus.Running)
                        {
                            services[n].Pause();
                            btnPause.Enabled = false;
                            btnContinute.Enabled = true;
                            ServiceList.Items[n].SubItems[2].Text = "Paused";
                        }
                        else
                        {
                            MessageBox.Show("This service couldn't be Paused", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please,choose a service that you want to pause", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ServiceList.SelectedItems.Count > 0)
                {
                    int n = ServiceList.SelectedItems[0].Index;

                    if (services[n].CanPauseAndContinue)
                    {
                        if (services[n].Status == ServiceControllerStatus.Paused)
                        {
                            btnPause.Enabled = false;
                            btnContinute.Enabled = true;
                        }
                        if (services[n].Status == ServiceControllerStatus.Running)
                        {
                            btnPause.Enabled = true;
                            btnContinute.Enabled = false;
                        }
                    }
                    else
                    {
                        btnPause.Enabled = false;
                        btnContinute.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message,"Messgae",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnContinute_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceList.Items.Count == 1)
                {
                    LoadService();
                    if (bosc2service.CanPauseAndContinue && bosc2service.Status == ServiceControllerStatus.Paused)
                    {
                        bosc2service.Continue();
                        btnContinute.Enabled = false;
                        btnPause.Enabled = true;
                        ServiceList.Items[0].SubItems[2].Text = "Running";
                    }
                    else
                    {
                        MessageBox.Show("This service couldn't be continuted !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (ServiceList.SelectedItems.Count != 0)
                    {
                        int n = ServiceList.FocusedItem.Index;
                        string strServiceName = ServiceList.FocusedItem.SubItems[0].Text;
                        LoadAllService();
                        if (services[n].ServiceName != strServiceName)
                        {
                            MessageBox.Show("Service list is old, Please Click View All button!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnViewAll.Focus();
                            return;
                        }
                        if (services[n].CanPauseAndContinue && services[n].Status == ServiceControllerStatus.Paused)
                        {
                            services[n].Continue();
                            btnPause.Enabled = true;
                            btnContinute.Enabled = false;
                            ServiceList.Items[n].SubItems[2].Text = "Running";
                        }
                        else
                        {
                            MessageBox.Show("This service couldn't be continuted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please,choose a service that you want to continute", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thrChildren = new Thread(new ThreadStart(RefreshGridView));
            thrChildren.IsBackground = true;
            thrChildren.Start();
        }
    }
}
