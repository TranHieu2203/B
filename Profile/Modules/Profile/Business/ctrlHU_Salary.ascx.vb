Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffWebAppResources.My.Resources
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Salary
    Inherits Common.CommonView
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    'Private ReadOnly RestClient As IServerDataRestClient = New ServerDataRestClient()

#Region "Property"
    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))

                dt.Columns.Add("EXPRITE_DATE", GetType(String))
                dt.Columns.Add("TOTAL_SALARY", GetType(String))

                dt.Columns.Add("SAL_TYPE_NAME", GetType(String))
                dt.Columns.Add("TAX_NAME", GetType(String))
                dt.Columns.Add("SAL_GROUP_NAME", GetType(String))
                dt.Columns.Add("SAL_LEVEL_NAME", GetType(String))
                dt.Columns.Add("SAL_RANK_NAME", GetType(String))
                dt.Columns.Add("FACTORSALARY", GetType(String))
                dt.Columns.Add("SAL_BASIC", GetType(String))
                dt.Columns.Add("PERCENTSALARY", GetType(String))
                dt.Columns.Add("OTHERSALARY1", GetType(String))
                dt.Columns.Add("OTHERSALARY2", GetType(String))
                dt.Columns.Add("COST_SUPPORT", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))


                dt.Columns.Add("EMPLOYEE_TYPE", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_NAME", GetType(String))
                dt.Columns.Add("SALARY_BHXH", GetType(String))
                'dt.Columns.Add("OTHERSALARY4", GetType(String))
                'dt.Columns.Add("OTHERSALARY5", GetType(String))
                dt.Columns.Add("LTT_V1", GetType(String))
                dt.Columns.Add("SAL_TYPE_ID", GetType(String))
                dt.Columns.Add("TAX_ID", GetType(String))
                dt.Columns.Add("SAL_GROUP_ID", GetType(String))
                dt.Columns.Add("SAL_LEVEL_ID", GetType(String))
                dt.Columns.Add("SAL_RANK_ID", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                dt.Columns.Add("SIGN_DATE", GetType(String))
                dt.Columns.Add("SIGN_ID", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Load page, set trang thai cua page va control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao page, set thuoc tinh cho grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.SetFilter()
            rgWorking.AllowCustomPaging = True
            InitControl()
            ctrlUpload1.isMultiple = False
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgWorking)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao page, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarWorkings
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' reset trang thai page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' get data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
#If DEBUG Then
            ' Dim test = RestClient.GetAll
#End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region

#Region "Event"
    ''' <summary>
    ''' event click item menu toolbar
    ''' update lai trang thai control, trang thai page sau khi process event xong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgWorking.ExportExcel(Server, Response, dtData, "Wage")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select

            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load lai grid khi click node in treeview
    ''' Rebind=> reload lai ham NeedDataSource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click nut tim kiem theo thang bien dong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event Yes/No cua message popup
    ''' update lai trang thai page, trang thai control sau khi process xong event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then Continue For
                'Dim repFactor As String = rows("FACTORSALARY").ToString.Trim.Replace(",", ".")
                Dim newRow As DataRow = dtData.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("EXPRITE_DATE") = rows("EXPRITE_DATE")
                newRow("SAL_TYPE_NAME") = rows("SAL_TYPE_NAME")
                newRow("TAX_NAME") = rows("TAX_NAME")
                newRow("SAL_GROUP_NAME") = rows("SAL_GROUP_NAME")
                newRow("SAL_LEVEL_NAME") = rows("SAL_LEVEL_NAME")
                newRow("SAL_RANK_NAME") = rows("SAL_RANK_NAME")


                newRow("EMPLOYEE_TYPE") = rows("EMPLOYEE_TYPE")
                newRow("EMPLOYEE_TYPE_NAME") = rows("EMPLOYEE_TYPE_NAME")
                newRow("SALARY_BHXH") = rows("SALARY_BHXH") 'If(IsNumeric(rows("SALARY_BHXH")), Decimal.Parse(rows("SALARY_BHXH")), "n")
                newRow("TOTAL_SALARY") = rows("TOTAL_SALARY") 'If(IsNumeric(rows("TOTAL_SALARY")), Decimal.Parse(rows("TOTAL_SALARY")), "n")

                'newRow("FACTORSALARY") = If(IsNumeric(rows("FACTORSALARY")), repFactor, "n")
                newRow("SAL_BASIC") = rows("SAL_BASIC") 'If(IsNumeric(rows("SAL_BASIC")), Decimal.Parse(rows("SAL_BASIC")), "n")
                newRow("PERCENTSALARY") = rows("PERCENTSALARY") 'If(IsNumeric(rows("PERCENTSALARY")), Decimal.Parse(rows("PERCENTSALARY")), "n")
                newRow("OTHERSALARY1") = If(IsNumeric(rows("OTHERSALARY1")), Decimal.Parse(rows("OTHERSALARY1")), 0)
                'newRow("OTHERSALARY2") = If(IsNumeric(rows("OTHERSALARY2")), Decimal.Parse(rows("OTHERSALARY2")), 0)
                'newRow("COST_SUPPORT") = If(IsNumeric(rows("COST_SUPPORT")), Decimal.Parse(rows("COST_SUPPORT")), 0)
                'newRow("STATUS_NAME") = rows("STATUS_NAME")
                newRow("SAL_TYPE_ID") = If(IsNumeric(rows("SAL_TYPE_ID")), rows("SAL_TYPE_ID"), 0)
                newRow("TAX_ID") = If(IsNumeric(rows("TAX_ID")), rows("TAX_ID"), 0)
                newRow("SAL_GROUP_ID") = If(IsNumeric(rows("SAL_GROUP_ID")), rows("SAL_GROUP_ID"), 0)
                newRow("SAL_LEVEL_ID") = If(IsNumeric(rows("SAL_LEVEL_ID")), rows("SAL_LEVEL_ID"), 0)
                newRow("SAL_RANK_ID") = If(IsNumeric(rows("SAL_RANK_ID")), rows("SAL_RANK_ID"), 0)
                newRow("STATUS_ID") = If(IsNumeric(rows("STATUS_ID")), rows("STATUS_ID"), 0)

                If IsDate(rows("SIGN_DATE")) Then
                    newRow("SIGN_DATE") = rows("SIGN_DATE")
                Else
                    newRow("SIGN_DATE") = rows("EFFECT_DATE")
                End If
                newRow("SIGN_ID") = rows("SIGN_ID")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_HoSoLuong(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgWorking.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub Template_ImportHoSoLuong()
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = rep.GetHoSoLuongImport()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            dsData.Tables(3).TableName = "Table3"
            dsData.Tables(4).TableName = "Table4"
            dsData.Tables(5).TableName = "Table5"
            dsData.Tables(6).TableName = "Table6"
            dsData.Tables(7).TableName = "Table7"
            rep.Dispose()
            'If File.Exists(configPath + "Payroll\TEMP_IMPORT_HOSOLUONG.xls") Then
            '    ExportTemplate(configPath + "Payroll\TEMP_IMPORT_HOSOLUONG.xls",
            '                          dsData, Nothing, "Template_HoSoLuong_" & Format(Date.Now, "yyyyMMdd"))
            'Else
            '    ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
            '    Exit Sub
            'End If
            'If File.Exists(configPath + "payroll\temp_import_hosoluong.xls") Then

            ExportTemplate("Payroll\Business\TEMP_IMPORT_HOSOLUONG.xls",
                                  dsData, Nothing, "import_HoSoLuong" & Format(Date.Now, "yyyymmdd"))


            'Using cls As New ExcelCommon
            '    Dim _error As String = String.Empty
            '    Dim bCheck = cls.ExportExcelTemplate(
            ' Server.MapPath("~/ReportTemplates//Payroll//Business//TEMP_IMPORT_HOSOLUONG.xls"), "import_HoSoLuong", dtData, Response, String.Empty, ExcelCommon.ExportType.Excel)
            '    If Not bCheck Then
            '        Select Case _error
            '            Case 1
            '                ShowMessage(Translate("File mẫu không tồn tại"), NotifyType.Warning)
            '            Case 2
            '                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
            '        End Select
            '        Exit Sub
            '    End If
            'End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Check data khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportWorking = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New HU_SALARYDTO
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If row("EFFECT_DATE") Is DBNull.Value OrElse row("EFFECT_DATE") = "" Then
                    sError = "Chưa nhập ngày hiệu lực"
                    ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("EFFECT_DATE")) = False Then
                        sError = "Ngày hiệu lực không đúng định dạng"
                        ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                    End If
                    Try
                        If IBusiness.ValEffectdateByEmpCode(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_DATE"))) = False Then
                            sError = "Tồn tại hồ sơ lương trùng ngày hiệu lực"
                            ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                        End If
                    Catch ex As Exception
                        GoTo VALIDATE
                    End Try
                End If
VALIDATE:
                Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))

                If empId = 0 Then
                    sError = "Mã nhân viên không tồn tại"
                    ImportValidate.IsValidTime("FULLNAME_VN", row, rowError, isError, sError)
                End If

                If row("EXPRITE_DATE").ToString <> "" Then
                    If IsDate(row("EXPRITE_DATE")) = False Then
                        sError = "Ngày hết hiệu lực không đúng định dạng"
                        ImportValidate.IsValidTime("EXPRITE_DATE", row, rowError, isError, sError)
                    End If
                End If
                'If row("EXPRITE_DATE") IsNot Nothing Then
                '    If IsDate(row("EXPRITE_DATE")) = False Then
                '        sError = "Ngày hết hiệu lực không đúng định dạng"
                '        ImportValidate.IsValidTime("EXPRITE_DATE", row, rowError, isError, sError)
                '    End If
                'End If


                'If row("FACTORSALARY") Is DBNull.Value OrElse row("FACTORSALARY") = "0" Then
                '    sError = "Chưa nhập hệ số/mức tiền"
                '    ImportValidate.IsValidTime("FACTORSALARY", row, rowError, isError, sError)
                'ElseIf IsNumeric(row("FACTORSALARY")) = False Then
                '    sError = "Hệ số/mức tiền không đúng định dạng"
                '    ImportValidate.IsValidTime("FACTORSALARY", row, rowError, isError, sError)
                'End If
                If row("TOTAL_SALARY") = "" Then
                    sError = "Chưa nhập tổng mức lương"
                    ImportValidate.IsValidTime("TOTAL_SALARY", row, rowError, isError, sError)
                ElseIf IsNumeric(row("TOTAL_SALARY")) = False Then
                    sError = "Tổng mức lương không đúng định dạng"
                    ImportValidate.IsValidTime("TOTAL_SALARY", row, rowError, isError, sError)
                End If
                If row("SALARY_BHXH") = "" Then
                    sError = "Chưa nhập Lương đóng BHXH"
                    ImportValidate.IsValidTime("SALARY_BHXH", row, rowError, isError, sError)
                ElseIf IsNumeric(row("SALARY_BHXH")) = False Then
                    sError = "Lương đóng BHXH không đúng định dạng"
                    ImportValidate.IsValidTime("SALARY_BHXH", row, rowError, isError, sError)
                End If
                If row("PERCENTSALARY") = "" Then
                    sError = "Chưa nhập % hưởng lương"
                    ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                ElseIf IsNumeric(row("PERCENTSALARY")) = False Then
                    sError = "% hưởng lương không đúng định dang"
                    ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                End If
                If row("SAL_BASIC") = "" Then
                    sError = "Chưa nhập lương cơ bản"
                    ImportValidate.IsValidTime("SAL_BASIC", row, rowError, isError, sError)
                ElseIf IsNumeric(row("SAL_BASIC")) = False Then
                    sError = "Lương cơ bản không đúng định dạng"
                    ImportValidate.IsValidTime("SAL_BASIC", row, rowError, isError, sError)
                End If
                'If row("STATUS_ID") Is DBNull.Value OrElse row("STATUS_NAME") = "" Then
                '    sError = "Chưa chọn trạng thái"
                '    ImportValidate.IsValidTime("STATUS_ID", row, rowError, isError, sError)
                'End If
                If row("SAL_TYPE_ID") Is DBNull.Value OrElse row("SAL_TYPE_ID") = "0" Then
                    sError = "Chưa nhập nhóm lương"
                    ImportValidate.IsValidTime("SAL_TYPE_NAME", row, rowError, isError, sError)
                End If
                If row("EMPLOYEE_TYPE") Is DBNull.Value OrElse row("EMPLOYEE_TYPE") = "" Then
                    sError = "Chưa chọn loại nhân viên"
                    ImportValidate.IsValidTime("EMPLOYEE_TYPE", row, rowError, isError, sError)
                End If
                If row("TAX_ID") Is DBNull.Value OrElse row("TAX_ID") = "0" Then
                    sError = "Chưa nhập biểu thuế"
                    ImportValidate.IsValidTime("TAX_NAME", row, rowError, isError, sError)
                End If
                If Not row("PERCENTSALARY") Is DBNull.Value OrElse Not row("PERCENTSALARY") = "" Then
                    If row("EMPLOYEE_TYPE_NAME").ToString = "Thử việc" Then
                        If IsNumeric(row("PERCENTSALARY")) Then
                            If Integer.Parse(row("PERCENTSALARY")) < 85 Or Integer.Parse(row("PERCENTSALARY")) > 100 Then
                                sError = "Giá trị nhập không đúng quy định"
                                ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                            End If
                        End If
                    End If
                    If row("EMPLOYEE_TYPE_NAME").ToString = "Chính thức" Then
                        If IsNumeric(row("PERCENTSALARY")) Then
                            If Integer.Parse(row("PERCENTSALARY")) <> 100 Then
                                sError = "Giá trị nhập không đúng quy định"
                                ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                            End If
                        End If
                    End If
                End If
                ''NGUOI KY k có trong hệ thống
                If IsNumeric(row("SIGN_ID")) Then
                    If IBusiness.CHECK_SIGN(row("SIGN_ID")) = 0 Then
                        sError = "Người ký - Không tồn tại"
                        ImportValidate.IsValidTime("SIGN_ID", row, rowError, isError, sError)
                    Else
                        Dim SIG_Id = rep.CheckEmployee_Exits(row("SIGN_ID"))
                        If SIG_Id <> 0 Then
                            row("SIGN_ID") = SIG_Id.ToString
                        Else
                            row("SIGN_ID") = 0
                        End If
                    End If
                Else
                    If IsDate(row("EFFECT_DATE")) Then
                        Dim signer = sp.GET_SIGNER_BY_FUNC("ctrlHU_WageNewEdit", CDate(row("EFFECT_DATE")))
                        If signer.Rows.Count > 0 Then
                            row("SIGN_ID") = signer.Rows(0)("ID").ToString
                        Else
                            row("SIGN_ID") = 0
                        End If
                    Else
                        row("SIGN_ID") = 0
                    End If
                End If
                If isError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                'dtError.TableName = "DATA"
                '' gộp các lỗi vào 1 cột ghi chú 
                'Dim dtErrorGroup As New DataTable
                'Dim RowErrorGroup As DataRow
                'dtErrorGroup.Columns.Add("STT")
                'dtErrorGroup.Columns.Add("NOTE")
                'For j As Integer = 0 To dtError.Rows.Count - 1
                '    Dim strNote As String = String.Empty
                '    RowErrorGroup = dtErrorGroup.NewRow
                '    For k As Integer = 1 To dtError.Columns.Count - 1
                '        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                '            strNote &= dtError.Rows(j)(k) & "\"
                '        End If
                '    Next
                '    RowErrorGroup("STT") = dtError.Rows(j)("EMPLOYEE_CODE")
                '    RowErrorGroup("NOTE") = strNote
                '    dtErrorGroup.Rows.Add(RowErrorGroup)
                'Next
                'dtErrorGroup.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_HOSOLUONG');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    ''' <summary>
    ''' Thiet lap lai trang thai control
    ''' process event xoa du lieu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep1 As New ProfileStoreProcedure
        Try
            tbarWorkings.Enabled = True
            rgWorking.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_DELETE
                    Dim lstdel = ","

                    For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                        lstdel = lstdel + selectedItem.GetDataKeyValue("ID").ToString + ","
                    Next
                    If rep1.DEL_HU_SALARY(lstdel) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorking.CurrentPageIndex = 0
                        rgWorking.MasterTableView.SortExpressions.Clear()

                        rgWorking.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    'Using rep As New ProfileBusinessRepository
                    '    If rep.DeleteWorking(New WorkingDTO With {.ID = rgWorking.SelectedValue}) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '        CurrentState = CommonMessage.STATE_NORMAL
                    '        rgWorking.CurrentPageIndex = 0
                    '        rgWorking.MasterTableView.SortExpressions.Clear()

                    '        rgWorking.Rebind()
                    '    Else
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    '    End If
                    'End Using

                Case CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    Using rep As New ProfileBusinessRepository
                        Dim workingIds = New List(Of Decimal)
                        Dim acti = ""
                        For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                            workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                            acti = CDec(Val(selectedItem.GetDataKeyValue("STATUS_ID")))
                        Next
                        Dim bCheckHasfile = rep.CheckHasFile(workingIds)
                        'If bCheckHasfile = 1 Then
                        '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rep.ApproveWorkings(workingIds, acti).Status = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgWorking.CurrentPageIndex = 0
                            rgWorking.MasterTableView.SortExpressions.Clear()
                            rgWorking.Rebind()
                        End If
                    End Using
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Get data from database to grid
    ''' </summary>
    ''' <param name="isFull">If = true thi full data, =false load filter or phan trang</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New HU_SALARYDTO
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgWorking.DataSource = New List(Of EmployeeDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgWorking, _filter)

            '_filter.FROM_DATE = rdEffectDate.SelectedDate
            '_filter.TO_DATE = rdExpireDate.SelectedDate

            _filter.IS_TER = chkTerminate.Checked
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorking.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetHu_Salary(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetHu_Salary(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgWorking.DataSource = rep.GetHu_Salary(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgWorking.DataSource = rep.GetHu_Salary(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param)
                End If

                rgWorking.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region
#Region "Utility"
    Public Function GetUiResx(ByVal input) As String
        Return UI.ResourceManager.GetString(input)
    End Function
    Public Function GetErrorsResx(ByVal input) As String
        Return Errors.ResourceManager.GetString(input)
    End Function
#End Region
End Class