using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Configuration;
using Smart.OracleDBServices;
namespace ConfigTool
{
    public partial class frmConfigTool : Form
    {
        //System.Threading.Thread thrRead; 
        public frmConfigTool()
        {
            InitializeComponent();
            // BOConnectString = txtConnectString.Text;
        }
        private String BOConnectString;
        private System.Data.DataTable control;
        private System.Data.DataTable girdColumm;
        private System.Data.DataSet viewconfig = new System.Data.DataSet("viewconfig");

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog?.SelectedPath != String.Empty)
            {
                txtPath.Text = folderBrowserDialog?.SelectedPath;
            }
        }
        private void AddRowToTableGrid(int Stt, HtmlNode node)
        {
            try
            {
                System.Data.DataRow row = girdColumm.NewRow();
                row["ID"] = node?.Attributes["datafield"]?.Value;
                if (row["ID"]is System.DBNull  )
                    row["ID"]= node?.Attributes["uniquename"]?.Value;
                row["Name"] = node?.Attributes["HeaderText"]?.Value;
                row["Is_Visible"] = "true";
                row["Width"] = 200;
                row["DataType"] = "String";
                row["Orderby"] = Stt;
                girdColumm.Rows.Add(row);
            }
            catch (Exception)
            {
            }
        }
        private void AddRowToTableControl(String ID, HtmlNode tableform)
        {
            try
            {
                HtmlNodeCollection NodeValidator = tableform?.SelectNodes(@"//td//requiredfieldvalidator");
                HtmlNodeCollection NodeLable = tableform?.SelectNodes(@"//td//label");
                System.Data.DataRow row = control.NewRow();
                row["Ctl_ID"] = ID;
                var validator = NodeValidator?.Where(val => val.Attributes["controltovalidate"].Value.Contains(ID))?.ToList();
               
                    row["ErrorMessage"] = validator?.Count > 0 ? validator[0]?.Attributes["errormessage"]?.Value : "";
                    row["ErrorToolTip"] = validator?.Count > 0 ? validator[0]?.Attributes["tooltip"]?.Value : "";
                    row["Validator_ID"] = validator?.Count > 0 ? validator[0]?.Attributes["ID"]?.Value : "";
               
              
                row["Is_Visible"] = "true";
                row["Is_Validator"] = "false";
                var lable = NodeLable?.Where(val => val.Attributes["ID"].Value.Contains(ID.Substring(3)))?.ToList();
                if (lable != null && lable?.Count == 0)
                {
                    lable = NodeLable?.Where(val => val.Attributes["ID"].Value.Contains(ID.Substring(2)))?.ToList();
                }
                row["Label_ID"] = lable?.Count > 0 ? lable[0]?.Attributes["id"]?.Value : "";
                row["Label_text"] = lable?.Count > 0 ? lable[0]?.Attributes["text"]?.Value : "";
                control.Rows.Add(row);
            }
            catch (Exception) { }
        }

        private void GetColumnConfigGrid(String ColTypeGrid, int Stt, HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            var gridConfig = htmlDoc?.DocumentNode?.SelectNodes("//" + ColTypeGrid);
            if (gridConfig == null) { return; }
            foreach (var element in gridConfig)
            {
                try
                {
                    AddRowToTableGrid(Stt, element);
                    Stt += 1;
                }
                catch (Exception)
                {
                    continue;
                }
            }

        }
        private void SaveDB(String FileName)
        {
            try
            {
                if (control.Rows.Count > 0 || girdColumm.Rows.Count > 0)
                {
                    if (!viewconfig.Tables.Contains(control.TableName))
                        viewconfig.Tables.Add(control);
                    if (!viewconfig.Tables.Contains(girdColumm.TableName))
                        viewconfig.Tables.Add(girdColumm);
                    StringWriter sw = new StringWriter();
                    String DocXml = String.Empty;
                    viewconfig.WriteXml(sw);
                    DocXml = sw.ToString();
                    //save DB
                    OracleHelper.ExecuteNonquery(BOConnectString, "PKG_COMMON_BUSINESS.INSERT_SE_VIEW_CONFIG", new object[] { DocXml, FileName.Replace(".ascx", "") });
                    sw.Dispose();
                    control.Dispose();
                    girdColumm.Dispose();
                    viewconfig.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadFilesInFolder(String path, Boolean isFile = false)
        {
            DirectoryInfo Directory;
            FileInfo[] Files;
            if (isFile)
            {
                List<FileInfo> lstFile = new List<FileInfo>();
                lstFile.Add(new FileInfo(path));
                Files = lstFile.ToArray();
            }
            else
            {
                Directory = new DirectoryInfo(path);
                Files = Directory?.GetFiles("*.ascx");
            }
            String fileName = String.Empty;
            foreach (System.IO.FileInfo item in Files)
            {
                try
                {
                    //EditAscxFile(item);
                    CreateStructTable();
                    fileName = item.FullName;
                    HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
                    htmlWeb.OverrideEncoding = Encoding.UTF8;
                    HtmlAgilityPack.HtmlDocument htmlDoc = htmlWeb.Load(fileName);
                    String _html = htmlDoc?.DocumentNode?.InnerHtml.Replace("tlk:", "");
                    _html = _html.Replace("asp:", "");
                    htmlDoc.Text = _html;
                    htmlDoc.DocumentNode.InnerHtml = _html;
                    HtmlNode tableform = htmlDoc?.DocumentNode?.SelectSingleNode(@"//table[@class='table-form']");
                    HtmlNodeCollection elements = tableform?.SelectNodes(@"//td");
                    HtmlNodeCollection NodeValidator = tableform?.SelectNodes(@"//td//requiredfieldvalidator");
                    if (elements == null) goto GRID;
                    List<string> lst = new List<string>();
                    foreach (var element in elements)
                    {
                        String IDText = String.Empty;
                        try
                        {
                            IDText = element?.SelectSingleNode(".//radtextbox")?.Attributes["id"]?.Value;
                            if (IDText != String.Empty && IDText != null)
                            {
                                AddRowToTableControl(IDText, tableform);
                            }
                            IDText = element?.SelectSingleNode(".//raddatepicker")?.Attributes["id"]?.Value;
                            if (IDText != String.Empty && IDText != null)
                            {
                                AddRowToTableControl(IDText, tableform);
                            }
                            IDText = element?.SelectSingleNode(".//radnumerictextbox")?.Attributes["id"]?.Value;
                            if (IDText != String.Empty && IDText != null)
                            {
                                AddRowToTableControl(IDText, tableform);
                            }
                            IDText = element?.SelectSingleNode(".//radcombobox")?.Attributes["id"]?.Value;
                            if (IDText != String.Empty && IDText != null)
                            {
                                AddRowToTableControl(IDText, tableform);
                            }
                            IDText = element?.SelectSingleNode(".//checkbox")?.Attributes["id"]?.Value;
                            if (IDText != String.Empty && IDText != null)
                            {
                                AddRowToTableControl(IDText, tableform);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    goto GRID;
                GRID:
                    int stt = 1;
                    GetColumnConfigGrid("gridboundcolumn", stt, htmlDoc);
                    GetColumnConfigGrid("gridclientselectcolumn", stt, htmlDoc);
                    GetColumnConfigGrid("gridtemplatecolumn", stt, htmlDoc);
                    GetColumnConfigGrid("gridnumericcolumn", stt, htmlDoc);
                    GetColumnConfigGrid("griddatetimecolumn", stt, htmlDoc);
                    GetColumnConfigGrid("gridcheckboxcolumn", stt, htmlDoc);
                    GetColumnConfigGrid("gridbinaryimagecolumn", stt, htmlDoc);
                    //delete translate
                    //GridBinaryImageColumn
                    DeleteTranslate();
                    // data ---> XML
                    SaveDB(item.Name);
                    //step += 1;
                    //processBar.PerformStep();
                }
                catch (Exception ex)
                {
                    //thu kiem tra saveDB
                    SaveDB(item.Name);
                    continue;
                }
            }
        }
        private void DeleteTranslate()
        {
            foreach (DataRow row in control.Rows)
            {
                var s = row["ErrorMessage"].ToString().Replace("<%$ Translate: ", "").Replace(" %>","");
                row["ErrorMessage"] = s;
                s = row["ErrorToolTip"].ToString().Replace("<%$ Translate: ","").Replace("%>", "");
                row["ErrorToolTip"] = s;
            }
            foreach (DataRow row in girdColumm.Rows)
            {
                var s = row["Name"].ToString().Replace("<%$ Translate: ", "").Replace(" %>", "");
                row["Name"] = s;
            }
        }
        private void EditAscxFile(FileInfo file)
        {
            var htmlWeb = new HtmlAgilityPack.HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            var htmlDoc = htmlWeb.Load(file.FullName);
            var tableform = htmlDoc?.DocumentNode?.SelectSingleNode(@"//table[@class='table-form']");
            var elements = tableform.Descendants("td").ToList();
            List<HtmlNode> lslb = new List<HtmlNode>();
            List<string> lsid = new List<string>();
            for (int i = 0; i < elements.Count() - 1; i++)
            {
                foreach (var item1 in elements[i].DescendantNodes())
                {
                    if (item1.Attributes["runat"]?.Value != "server")
                    {
                        var s = item1.InnerHtml.ToString();
                        if (s.Contains("<%# Translate(\"") && s.Contains("\")%>"))
                        {
                            foreach (var item2 in elements[i + 1].DescendantNodes())
                            {
                                var t = item2.OuterHtml;
                                if (t.Contains("tlk:"))
                                {
                                    lsid.Add(item2.Attributes["ID"]?.Value);
                                    break;
                                }
                            }
                            lslb.Add(item1);
                            break;
                        }
                    }
                }
            }
            foreach (var item in lslb)
            {
                tableform.InnerText.Replace(item.InnerText, "");
            }
            htmlDoc.Save(@"d:\t.ascx");
        }

        private void CreateStructTable()
        {
            viewconfig = new DataSet("viewconfig");
            control = new System.Data.DataTable("control");
            control.Columns.Add("Ctl_ID", typeof(System.String));
            control.Columns.Add("Label_ID", typeof(System.String));
            control.Columns.Add("Label_text", typeof(System.String));
            control.Columns.Add("Is_Visible", typeof(System.String));
            control.Columns.Add("Is_Validator", typeof(System.String));
            control.Columns.Add("ErrorMessage", typeof(System.String));
            control.Columns.Add("ErrorToolTip", typeof(System.String));
            control.Columns.Add("Validator_ID", typeof(System.String));
            girdColumm = new System.Data.DataTable("girdColumm");
            girdColumm.Columns.Add("ID", typeof(System.String));
            girdColumm.Columns.Add("Name", typeof(System.String));
            girdColumm.Columns.Add("Is_Visible", typeof(System.String));
            girdColumm.Columns.Add("Width", typeof(System.String));
            girdColumm.Columns.Add("DataType", typeof(System.String));
            girdColumm.Columns.Add("Orderby", typeof(System.String));
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            String FolderPath = String.Empty;
            Boolean isFile = false;
            FolderPath = txtPath.Text.Equals("") ? txtFilePath.Text : txtPath.Text;
            isFile = txtPath.Text.Equals("") ? true : false;
            ReadFilesInFolder(FolderPath, isFile);
            btnStop.Enabled = true;
            MessageBox.Show("Thao ket thuc");
        }

        private void GetControlInHtmlDoc(String docHtml)
        {

        }

        private void frmConfigTool_Load(object sender, EventArgs e)
        {
            // BOConnectString=
            BOConnectString = ConfigurationManager.AppSettings["BOConnectString"];
            txtConnectString.Text = ConfigurationManager.AppSettings["BOConnectString"]; //BOConnectString;
            BOConnectString = txtConnectString.Text;
            btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChoseFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "ascx files (*.ascx)|*.ascx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.ShowDialog();
                if (openFileDialog?.FileName != String.Empty)
                {
                    txtFilePath.Text = openFileDialog?.FileName;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtConnectString_TextChanged(object sender, EventArgs e)
        {
            BOConnectString = txtConnectString.Text;
        }
    }
}
