Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_CB_AssessmentNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstCBAssessmentDtl As List(Of CBAssessmentDtlDTO)
        Get
            Return ViewState(Me.ID & "_lstCBAssessmentDtl")
        End Get
        Set(ByVal value As List(Of CBAssessmentDtlDTO))
            ViewState(Me.ID & "_lstCBAssessmentDtl") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("MATHE", GetType(String))
                dt.Columns.Add("RESULT", GetType(String))
                dt.Columns.Add("RESULT_NAME", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Property popupId As String

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWorkschedule
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popupId = popup.ClientID

            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgWorkschedule
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim objCB = rep.GetCBAssessment(hidID.Value)
                    If objCB IsNot Nothing Then
                        If objCB.CONFIRM_YEAR.HasValue Then
                            rnConfirmYear.Value = objCB.CONFIRM_YEAR
                        End If
                        If objCB.ASSESSMENT_YEAR.HasValue Then
                            rnAssessmentYear.Value = objCB.ASSESSMENT_YEAR
                        End If
                        txtContent.Text = objCB.CONTENT
                        txtRemark.Text = objCB.REMARK
                        lstCBAssessmentDtl = objCB.lstDtl
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    lstCBAssessmentDtl = New List(Of CBAssessmentDtlDTO)
            End Select
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmp.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmp.ID
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click nut cancel cua popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If lstCBAssessmentDtl.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên"), NotifyType.Error)
                            Exit Sub
                        End If
                        Dim objCBPlan = New CBAssessmentDTO()
                        If IsNumeric(rnConfirmYear.Value) Then
                            objCBPlan.CONFIRM_YEAR = rnConfirmYear.Value
                        End If
                        If IsNumeric(rnAssessmentYear.Value) Then
                            objCBPlan.ASSESSMENT_YEAR = rnAssessmentYear.Value
                        End If
                        objCBPlan.CONTENT = txtContent.Text
                        objCBPlan.REMARK = txtRemark.Text
                        objCBPlan.lstDtl = lstCBAssessmentDtl

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                If rep.InsertCBAssessment(objCBPlan) Then
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_CB_Assessment&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCBPlan.ID = Decimal.Parse(hidID.Value)

                                If rep.ModifyCBAssessment(objCBPlan) Then
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_CB_Assessment&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Exit Sub
                    End If
                Case (CommonMessage.TOOLBARITEM_CANCEL)
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_CB_Assessment&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeName, txtCurrentTitle, txtOrgName, txtMaThe)
            If ctrlFindEmployeePopup.SelectedEmployee.Count <> 0 Then
                Dim objEmp = ctrlFindEmployeePopup.SelectedEmployee(0)
                hidEmpID.Value = objEmp.EMPLOYEE_ID
                txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                txtEmployeName.Text = objEmp.FULLNAME_VN
                txtOrgName.Text = objEmp.ORG_NAME
                txtMaThe.Text = objEmp.MATHE_NAME
                txtCurrentTitle.Text = objEmp.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            'Refresh()
            If lstCBAssessmentDtl IsNot Nothing AndAlso lstCBAssessmentDtl.Count > 0 Then
                rgData.DataSource = lstCBAssessmentDtl
            Else
                lstCBAssessmentDtl = New List(Of CBAssessmentDtlDTO)
                rgData.DataSource = New List(Of CBAssessmentDtlDTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Try
            Dim rep As New ProfileRepository
            Select Case e.CommandName
                Case "Add"
                    If Not IsNumeric(hidEmpID.Value) Then
                        ShowMessage(Translate("Bạn phải Chọn CBCNV"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboResult.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kết quả đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim check = From p In lstCBAssessmentDtl Where p.EMPLOYEE_ID = hidEmpID.Value
                    If check.Any Then
                        ShowMessage(Translate("CBCNV đã tồn tại trên lưới"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim objEmp As New CBAssessmentDtlDTO
                    If cboResult.SelectedValue <> "" Then
                        objEmp.RESULT = cboResult.SelectedValue
                        objEmp.RESULT_NAME = cboResult.Text
                    End If
                    objEmp.EMPLOYEE_NAME = txtEmployeName.Text
                    objEmp.EMPLOYEE_ID = hidEmpID.Value
                    objEmp.EMPLOYEE_CODE = txtEmployeeCode.Text
                    objEmp.ORG_NAME = txtOrgName.Text
                    objEmp.TITLE_NAME = txtCurrentTitle.Text
                    objEmp.ORG_NAME = txtOrgName.Text
                    objEmp.MATHE_NAME = txtMaThe.Text
                    objEmp.REMARK = txtDtlRemark.Text
                    lstCBAssessmentDtl.Add(objEmp)
                    ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeName, txtOrgName, txtCurrentTitle, txtDtlRemark, cboResult)
                    rgData.Rebind()
                Case "Delete"
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        For Each item As GridDataItem In rgData.SelectedItems
                            lstCBAssessmentDtl.Remove((From p In lstCBAssessmentDtl Where p.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")))
                        Next
                    End If
                    rgData.Rebind()
                Case "Export"
                    Dim dtData = rep.GetOtherList("RANK")
                    Dim dsData As New DataSet
                    dsData.Tables.Add(dtData)
                    ExportTemplate("Profile/Import/Template_Import_CB_Assessent.xlsx",
                                  dsData, Nothing, "Template_Import_CB_Assessent" & Format(Date.Now, "yyyymmdd"))
                Case "Import"
                    ctrlUpload1.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
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
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("STT<>'""'").CopyToDataTable.Rows
                If (IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "") AndAlso (IsDBNull(rows("EMPLOYEE_NAME")) OrElse rows("EMPLOYEE_NAME") = "") Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("EMPLOYEE_NAME") = rows("EMPLOYEE_NAME")
                newRow("MATHE") = rows("MATHE")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("RESULT") = rows("RESULT")
                newRow("RESULT_NAME") = rows("RESULT_NAME")
                newRow("REMARK") = rows("REMARK")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                For Each item In dtData.Rows
                    Dim objEmp As New CBAssessmentDtlDTO
                    If IsNumeric(item("EMPLOYEE_ID")) Then
                        objEmp.EMPLOYEE_ID = CDec(item("EMPLOYEE_ID"))
                    End If
                    objEmp.EMPLOYEE_CODE = item("EMPLOYEE_CODE")
                    objEmp.EMPLOYEE_NAME = item("EMPLOYEE_NAME")
                    objEmp.MATHE_NAME = item("MATHE")
                    objEmp.RESULT = CDec(item("RESULT"))
                    objEmp.RESULT_NAME = item("RESULT_NAME")
                    objEmp.REMARK = item("REMARK")
                    lstCBAssessmentDtl.Add(objEmp)
                Next
                rgData.Rebind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    _filter.DM_ID = True
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên không thuộc nhóm NQL."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim objEmp = EmployeeList(0)
                        hidEmpID.Value = objEmp.EMPLOYEE_ID
                        txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                        txtEmployeName.Text = objEmp.FULLNAME_VN
                        txtOrgName.Text = objEmp.ORG_NAME
                        txtMaThe.Text = objEmp.MATHE
                        txtCurrentTitle.Text = objEmp.TITLE_NAME
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.IS_DM = True
                            phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeName, txtOrgName, txtCurrentTitle, txtMaThe)
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.IS_DM = True
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien load data cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim dtData As New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            dtData = rep.GetOtherList("RANK", True)
            FillRadCombobox(cboResult, dtData, "NAME", "ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "IDSelect"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "EmpID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    hidID.Value = Request.Params("ID")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack();", True)
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
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(2)
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
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

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
            dtError = dtData.Clone
            Dim rep As New ProfileBusinessRepository
            Dim lstTemp As New List(Of CBAssessmentDtlDTO)
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn kết quả đánh giá"
                ImportValidate.EmptyValue("RESULT", row, rowError, isError, sError)

                sError = "Chưa nhập CBCNV"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)


                If (Not IsDBNull(row("EMPLOYEE_CODE")) AndAlso row("EMPLOYEE_CODE") <> "") Then
                    Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))

                    If empId = 0 Then
                        sError = "CBCNV không tồn tại"
                        ImportValidate.IsValidTime("EMPLOYEE_NAME", row, rowError, isError, sError)
                    Else
                        Dim empObj = rep.GetEmployeeByID(empId)
                        row("EMPLOYEE_ID") = empId
                        row("EMPLOYEE_NAME") = empObj.FULLNAME_VN
                    End If
                End If
                If Not IsDBNull(row("EMPLOYEE_ID")) Then
                    Dim check = From p In lstCBAssessmentDtl Where p.EMPLOYEE_ID = row("EMPLOYEE_ID")
                    If check.Any Then
                        rowError("EMPLOYEE_CODE") = "CBCNV đã tồn tại trên lưới"
                        isError = True
                    End If
                    Dim check2 = From p In lstTemp Where (Not IsDBNull(row("EMPLOYEE_ID")) AndAlso p.EMPLOYEE_ID = row("EMPLOYEE_ID")) OrElse (p.EMPLOYEE_NAME.ToUpper().Equals(row("EMPLOYEE_NAME").ToString.ToUpper) And IsDBNull(row("EMPLOYEE_ID")))
                    If check2.Any Then
                        rowError("EMPLOYEE_CODE") = "CBCNV đã tồn tại trong file"
                        isError = True
                    End If
                    Dim objEmp As New CBAssessmentDtlDTO
                    objEmp.EMPLOYEE_ID = CDec(row("EMPLOYEE_ID"))
                    objEmp.EMPLOYEE_NAME = row("EMPLOYEE_NAME")
                    lstTemp.Add(objEmp)
                End If
                If isError Then

                    If IsDBNull(rowError("EMPLOYEE_CODE")) OrElse String.IsNullOrEmpty(rowError("EMPLOYEE_CODE")) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    If IsDBNull(rowError("EMPLOYEE_NAME")) OrElse String.IsNullOrEmpty(rowError("EMPLOYEE_NAME")) Then
                        rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    End If
                    rowError("STT") = row("STT").ToString
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Dim dsErr = New DataSet
                dsErr.Tables.Add(dtError)
                Session("CBASSESSMENT_ERROR") = dsErr
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_CB_Assessent_Error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region
End Class

