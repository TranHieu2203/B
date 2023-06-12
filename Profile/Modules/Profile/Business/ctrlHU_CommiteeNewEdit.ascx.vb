Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_CommiteeNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstCommiteeEmployee As List(Of CommiteeEmpDTO)
        Get
            Return ViewState(Me.ID & "_lstCommiteeEmployee")
        End Get
        Set(ByVal value As List(Of CommiteeEmpDTO))
            ViewState(Me.ID & "_lstCommiteeEmployee") = value
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
                dt.Columns.Add("MATHE_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("CURRENT_TITLE", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("COMMITEE_TITLE_NAME", GetType(String))
                dt.Columns.Add("COMMITEE_TITLE_ID", GetType(String))
                dt.Columns.Add("IS_OUTSIDE", GetType(String))
                dt.Columns.Add("ORG_OUTSIDE_NAME", GetType(String))
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
                    Dim objCm = rep.GetCommitee(hidID.Value)
                    If objCm IsNot Nothing Then
                        If objCm.YEAR.HasValue Then
                            cboYear.SelectedValue = objCm.YEAR
                        End If
                        txtDeciosionNo.Text = objCm.DECISION_NO
                        txtName.Text = objCm.NAME
                        txtTarget.Text = objCm.TARGET
                        txtRemark.Text = objCm.REMARK
                        If objCm.SIGNER_ID.HasValue Then
                            hidSigner.Value = objCm.SIGNER_ID
                        End If
                        txtSignerCode.Text = objCm.SIGNER_CODE
                        txtSignerName.Text = objCm.SIGNER_NAME
                        If objCm.FROM_DATE.HasValue Then
                            rdFromdate.SelectedDate = objCm.FROM_DATE
                        End If
                        If objCm.TO_DATE.HasValue Then
                            rdToDate.SelectedDate = objCm.TO_DATE
                        End If
                        If objCm.lstEmp.Count > 0 Then
                            lstCommiteeEmployee = objCm.lstEmp
                        End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    lstCommiteeEmployee = New List(Of CommiteeEmpDTO)
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
                                btnFindEmp.Click, btnFingSigner.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmp.ID
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case btnFingSigner.ID
                    isLoadPopup = 2
                    UpdateControlState()
                    ctrlFindSigner.Show()
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
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindSigner.CancelClicked
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
                        If lstCommiteeEmployee.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên"), NotifyType.Error)
                            Exit Sub
                        End If
                        Dim objCommit = New CommiteeDTO()
                        If cboYear.SelectedValue <> "" Then
                            objCommit.YEAR = cboYear.SelectedValue
                        End If
                        objCommit.DECISION_NO = txtDeciosionNo.Text
                        If IsNumeric(hidSigner.Value) Then
                            objCommit.SIGNER_ID = hidSigner.Value
                        End If
                        If IsDate(rdFromdate.SelectedDate) Then
                            objCommit.FROM_DATE = rdFromdate.SelectedDate
                        End If
                        If IsDate(rdToDate.SelectedDate) Then
                            objCommit.TO_DATE = rdToDate.SelectedDate
                        End If
                        objCommit.NAME = txtName.Text
                        objCommit.TARGET = txtTarget.Text
                        objCommit.REMARK = txtRemark.Text
                        objCommit.lstEmp = lstCommiteeEmployee

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertCommitee(objCommit) Then
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commitee&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCommit.ID = Decimal.Parse(hidID.Value)

                                If rep.ModifyCommitee(objCommit) Then
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commitee&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Exit Sub
                    End If
                Case (CommonMessage.TOOLBARITEM_CANCEL)
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commitee&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(hidSigner, txtSignerCode, txtSignerName)
            If ctrlFindSigner.SelectedEmployee.Count <> 0 Then
                Dim objEmp = ctrlFindSigner.SelectedEmployee(0)
                hidSigner.Value = objEmp.EMPLOYEE_ID
                txtSignerCode.Text = objEmp.EMPLOYEE_CODE
                txtSignerName.Text = objEmp.FULLNAME_VN
            End If
            isLoadPopup = 0
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
            If lstCommiteeEmployee IsNot Nothing AndAlso lstCommiteeEmployee.Count > 0 Then
                rgData.DataSource = lstCommiteeEmployee
            Else
                lstCommiteeEmployee = New List(Of CommiteeEmpDTO)
                rgData.DataSource = New List(Of CommiteeEmpDTO)
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
                    If cboCommiteTitle.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn chức danh hội đồng"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If cboStatus.SelectedValue = "" Then
                    '    ShowMessage(Translate("Bạn phải chọn Trạng thái"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    Dim check = From p In lstCommiteeEmployee Where (IsNumeric(hidEmpID.Value) AndAlso p.EMPLOYEE_ID = hidEmpID.Value) Or (Not IsNumeric(hidEmpID.Value) And p.EMPLOYEE_NAME.ToUpper.Equals(txtEmpOutsideName.Text.ToUpper))
                    If check.Any Then
                        ShowMessage(Translate("CBCNV đã tồn tại trên lưới"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim objEmp As New CommiteeEmpDTO
                    If cboCommiteTitle.SelectedValue <> "" Then
                        objEmp.COMMITEE_TITLE_ID = cboCommiteTitle.SelectedValue
                    End If
                    If Not String.IsNullOrEmpty(txtEmployeeCode.Text) Then
                        objEmp.EMPLOYEE_ID = hidEmpID.Value
                    End If
                    objEmp.EMPLOYEE_CODE = txtEmployeeCode.Text
                    objEmp.EMPLOYEE_NAME = txtEmployeName.Text
                    objEmp.IS_OUTSIDE = chkOutside.Checked
                    objEmp.ORG_OUTSIDE_NAME = txtOrgOutsideName.Text
                    objEmp.TITLE_NAME = txtCurrentTitle.Text
                    objEmp.ORG_NAME = If(Not IsNumeric(hidEmpID.Value), txtOrgOutsideName.Text, txtOrgName.Text)
                    objEmp.COMMITEE_TITLE_NAME = cboCommiteTitle.Text
                    objEmp.MATHE_NAME = txtMaThe.Text
                    objEmp.COMMITEE_ID = If(Not IsNumeric(hidID.Value), "0", hidID.Value)
                    lstCommiteeEmployee.Add(objEmp)
                    ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeName, txtOrgName, txtCurrentTitle, txtEmpOutsideName, txtOrgOutsideName, txtMaThe, cboCommiteTitle)
                    rgData.Rebind()
                    chkOutside.Checked = False
                    chkOutside_CheckedChanged(Nothing, Nothing)
                Case "Delete"
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        For Each item As GridDataItem In rgData.SelectedItems
                            lstCommiteeEmployee.Remove((From p In lstCommiteeEmployee Where (IsNumeric(item.GetDataKeyValue("EMPLOYEE_ID")) AndAlso p.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")) OrElse (Not IsNumeric(item.GetDataKeyValue("EMPLOYEE_ID")) AndAlso p.EMPLOYEE_NAME.ToUpper().Equals(item.GetDataKeyValue("EMPLOYEE_NAME").ToString.ToUpper))).FirstOrDefault)
                        Next
                    End If
                    rgData.Rebind()
                Case "Export"
                    Dim dtData = rep.GetOtherList("HU_TITLE_TBL")
                    Dim dsData As New DataSet
                    dsData.Tables.Add(dtData)
                    ExportTemplate("Profile/Import/Template_Import_TLHD.xlsx",
                                  dsData, Nothing, "Template_Import_TLHD" & Format(Date.Now, "yyyymmdd"))
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
                newRow("COMMITEE_TITLE_ID") = rows("COMMITEE_TITLE_ID")
                newRow("COMMITEE_TITLE_NAME") = rows("COMMITEE_TITLE_NAME")
                newRow("ORG_OUTSIDE_NAME") = rows("ORG_OUTSIDE_NAME")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                For Each item In dtData.Rows
                    Dim objEmp As New CommiteeEmpDTO
                    If IsNumeric(item("EMPLOYEE_ID")) Then
                        objEmp.EMPLOYEE_ID = CDec(item("EMPLOYEE_ID"))
                    End If
                    objEmp.EMPLOYEE_CODE = item("EMPLOYEE_CODE")
                    objEmp.EMPLOYEE_NAME = item("EMPLOYEE_NAME")
                    objEmp.COMMITEE_TITLE_ID = CDec(item("COMMITEE_TITLE_ID").ToString)
                    objEmp.COMMITEE_TITLE_NAME = item("COMMITEE_TITLE_NAME")
                    objEmp.COMMITEE_ID = If(Not IsNumeric(hidID.Value), "0", hidID.Value)
                    objEmp.ORG_NAME = If(CBool(item("IS_OUTSIDE").ToString), item("ORG_OUTSIDE_NAME").ToString, "")
                    objEmp.IS_OUTSIDE = CBool(item("IS_OUTSIDE").ToString)
                    objEmp.ORG_OUTSIDE_NAME = item("ORG_OUTSIDE_NAME")
                    lstCommiteeEmployee.Add(objEmp)
                Next
                rgData.Rebind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub txtSignerCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSignerCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtSignerCode.Text <> "" Then
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtSignerCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtSignerName.Text = ""
                    ElseIf Count = 1 Then
                        Dim objEmp = EmployeeList(0)
                        hidSigner.Value = objEmp.EMPLOYEE_ID
                        txtSignerCode.Text = objEmp.EMPLOYEE_CODE
                        txtSignerName.Text = objEmp.FULLNAME_VN
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindSigner.Controls.Contains(ctrlFindSigner) Then
                            phFindSigner.Controls.Remove(ctrlFindSigner)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindSigner.Controls.Contains(ctrlFindSigner) Then
                            ctrlFindSigner = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindSigner.EMP_CODE_OR_NAME = txtSignerCode.Text
                            ctrlFindSigner.MultiSelect = False
                            ctrlFindSigner.LoadAllOrganization = False
                            ctrlFindSigner.MustHaveContract = False
                            phFindEmp.Controls.Add(ctrlFindSigner)
                            ctrlFindSigner.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    ClearControlValue(hidSigner, txtSignerCode, txtSignerName)
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtSignerName.Text = ""
                    ElseIf Count = 1 Then
                        Dim objEmp = EmployeeList(0)
                        hidEmpID.Value = objEmp.EMPLOYEE_ID
                        txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                        txtEmployeName.Text = objEmp.FULLNAME_VN
                        txtOrgName.Text = objEmp.ORG_NAME
                        txtCurrentTitle.Text = objEmp.TITLE_NAME
                        txtMaThe.Text = objEmp.MATHE_NAME
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

    Private Sub chkOutside_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutside.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ClearControlValue(hidEmpID, txtEmployeeCode, txtEmployeName, txtOrgName, txtCurrentTitle, txtOrgOutsideName, txtEmpOutsideName, txtMaThe)
            EnableControlAll(Not chkOutside.Checked, txtEmployeeCode, btnFindEmp)
            EnableControlAll(chkOutside.Checked, txtEmpOutsideName, txtOrgOutsideName)
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
                    End If
                Case 2
                    If Not phFindSigner.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindSigner.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = False
                        ctrlFindSigner.MustHaveContract = False
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
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            'dtData = rep.GetOtherList("CB_STATUS1", True)
            'FillRadCombobox(cboStatus, table, "NAME", "ID")
            dtData = rep.GetOtherList("HU_TITLE_TBL", True)
            FillRadCombobox(cboCommiteTitle, dtData, "NAME", "ID")
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
            Dim lstTemp As New List(Of CommiteeEmpDTO)
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn chức danh hội đồng"
                ImportValidate.EmptyValue("COMMITEE_TITLE_NAME", row, rowError, isError, sError)

                If (Not IsDBNull(row("EMPLOYEE_CODE")) AndAlso row("EMPLOYEE_CODE") <> "") Then
                    Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))

                    If empId = 0 Then
                        sError = "CBCNV không tồn tại"
                        ImportValidate.IsValidTime("EMPLOYEE_NAME", row, rowError, isError, sError)
                    Else
                        Dim empObj = rep.GetEmployeeByID(empId)
                        row("EMPLOYEE_ID") = empId
                        row("IS_OUTSIDE") = "0"
                        row("EMPLOYEE_NAME") = empObj.FULLNAME_VN
                    End If
                Else
                    row("IS_OUTSIDE") = "-1"
                End If

                Dim check = From p In lstCommiteeEmployee Where (Not IsDBNull(row("EMPLOYEE_ID")) AndAlso p.EMPLOYEE_ID = row("EMPLOYEE_ID")) OrElse (p.EMPLOYEE_NAME.ToUpper().Equals(row("EMPLOYEE_NAME").ToString.ToUpper) And IsDBNull(row("EMPLOYEE_ID")))
                If check.Any Then
                    rowError("EMPLOYEE_CODE") = "CBCNV đã tồn tại trên lưới"
                    isError = True
                End If
                Dim check2 = From p In lstTemp Where (Not IsDBNull(row("EMPLOYEE_ID")) AndAlso p.EMPLOYEE_ID = row("EMPLOYEE_ID")) OrElse (p.EMPLOYEE_NAME.ToUpper().Equals(row("EMPLOYEE_NAME").ToString.ToUpper) And IsDBNull(row("EMPLOYEE_ID")))
                If check2.Any Then
                    rowError("EMPLOYEE_CODE") = "CBCNV đã tồn tại trong file"
                    isError = True
                End If
                Dim objEmp As New CommiteeEmpDTO
                If IsNumeric(row("EMPLOYEE_ID")) Then
                    objEmp.EMPLOYEE_ID = CDec(row("EMPLOYEE_ID"))
                End If
                objEmp.EMPLOYEE_NAME = row("EMPLOYEE_NAME")
                lstTemp.Add(objEmp)
                If isError Then

                    If IsDBNull(rowError("EMPLOYEE_CODE")) OrElse String.IsNullOrEmpty(rowError("EMPLOYEE_CODE")) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    If IsDBNull(rowError("EMPLOYEE_NAME")) OrElse String.IsNullOrEmpty(rowError("EMPLOYEE_NAME")) Then
                        rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    End If
                    rowError("STT") = row("STT").ToString
                    rowError("ORG_OUTSIDE_NAME") = row("ORG_OUTSIDE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Dim dsErr = New DataSet
                dsErr.Tables.Add(dtError)
                Session("COMMITEE_ERROR") = dsErr
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_TLHD_Error');", True)
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

