Imports System.Globalization
Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAtToxicLeaveEmp
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"
    Public IDSelect As Integer
    Public Property At_Holiday As List(Of AT_TOXIC_LEAVE_EMPDTO)
        Get
            Return ViewState(Me.ID & "_Holiday")
        End Get
        Set(ByVal value As List(Of AT_TOXIC_LEAVE_EMPDTO))
            ViewState(Me.ID & "_Holiday") = value
        End Set
    End Property

    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh("")
            UpdateControlState()
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            rgDanhMuc.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export,
                                       ToolbarItem.ExportTemplate,
                                       ToolbarItem.Import)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(6), RadToolBarButton).Text = "Xuất file mẫu"
            CType(MainToolBar.Items(7), RadToolBarButton).Text = "Nhập file mẫu"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TOXIC_LEAVE_EMPDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()

            obj.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            obj.IS_DISSOLVE = ctrlOrg.IsDissolve

            If Not isFull Then

                If Sorts IsNot Nothing Then
                    Me.At_Holiday = rep.GetAtToxicLeaveEmp(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, Sorts)
                Else
                    Me.At_Holiday = rep.GetAtToxicLeaveEmp(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.At_Holiday
            Else
                Return rep.GetAtToxicLeaveEmp(obj).ToTable
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, rdNote, txtTitleName, txtFromEffect1, ntxtToxicLeave1, txtFromEffect2, ntxtToxicLeave2, txtFromEffect3, ntxtToxicLeave3, txtFromEffect4, ntxtToxicLeave4, rdEffectDate, rdExpireDate)
                    EnableControlAll(False, txtFromEffect1, txtEmployeeCode, ntxtToxicLeave1, txtFromEffect2, ntxtToxicLeave2, txtFromEffect3, ntxtToxicLeave3, txtFromEffect4, ntxtToxicLeave4, rdEffectDate, rdExpireDate, rdNote)
                    btnFindEmployee.Enabled = False
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT

                    btnFindEmployee.Enabled = True
                    EnableControlAll(True, txtFromEffect1, txtEmployeeCode, ntxtToxicLeave1, txtFromEffect2, ntxtToxicLeave2, txtFromEffect3, ntxtToxicLeave3, txtFromEffect4, ntxtToxicLeave4, rdEffectDate, rdExpireDate, rdNote)
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAtToxicLeaveEmp(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Update By : Tran Ngoc Hung
            'Update Date : 07/12/2022
            'Description : BCG-850

            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("EMPLOYEE_ID", hidEmp)
            dic.Add("EMPLOYEE_CODE", txtEmployeeCode)
            dic.Add("EMPLOYEE_NAME", txtEmployeeName)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("TITLE_NAME", txtTitleName)
            dic.Add("FROM_EFFECT1", txtFromEffect1)
            dic.Add("TOXICLEAVE1", ntxtToxicLeave1)
            dic.Add("FROM_EFFECT2", txtFromEffect2)
            dic.Add("TOXICLEAVE2", ntxtToxicLeave2)
            dic.Add("FROM_EFFECT3", txtFromEffect3)
            dic.Add("TOXICLEAVE3", ntxtToxicLeave3)
            dic.Add("FROM_EFFECT4", txtFromEffect4)
            dic.Add("TOXICLEAVE4", ntxtToxicLeave4)
            dic.Add("NOTE", rdNote)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("EXPIRE_DATE", rdExpireDate)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objHoliday As New AT_TOXIC_LEAVE_EMPDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    btnFindEmployee.Enabled = True
                    EnableControlAll(True, txtFromEffect1, txtEmployeeCode, ntxtToxicLeave1, txtFromEffect2, ntxtToxicLeave2, txtFromEffect3, ntxtToxicLeave3, txtFromEffect4, ntxtToxicLeave4, rdEffectDate, rdExpireDate, rdNote)
                    ClearControlValue(txtEmployeeCode, txtEmployeeName, txtOrgName, rdNote, txtTitleName, txtFromEffect1, ntxtToxicLeave1, txtFromEffect2, ntxtToxicLeave2, txtFromEffect3, ntxtToxicLeave3, txtFromEffect4, ntxtToxicLeave4, rdEffectDate, rdExpireDate)
                    rgDanhMuc.SelectedIndexes.Clear()
                    EnabledGridNotPostback(rgDanhMuc, False)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMuc.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim startDate As Date = Nothing
                    Dim startDate1 As Date = Nothing
                    Dim startDate2 As Date = Nothing
                    Dim startDate3 As Date = Nothing
                    If Page.IsValid Then
                        objHoliday.EMPLOYEE_ID = hidEmp.Value

                        'check date
                        If txtFromEffect1.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtFromEffect1.Text, Date.Now.Year), startDate) Then
                            ShowMessage(Translate("Hiệu lực 1 không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtFromEffect2.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtFromEffect2.Text, Date.Now.Year), startDate1) Then
                            ShowMessage(Translate("Hiệu lực 2 không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtFromEffect3.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtFromEffect3.Text, Date.Now.Year), startDate2) Then
                            ShowMessage(Translate("Hiệu lực 3 không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtFromEffect4.Text <> "" AndAlso Not CheckDate(String.Format("{0}/{1}", txtFromEffect4.Text, Date.Now.Year), startDate3) Then
                            ShowMessage(Translate("Hiệu lực 4 không đúng định dạng, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If rdEffectDate.SelectedDate IsNot Nothing AndAlso rdExpireDate.SelectedDate IsNot Nothing AndAlso rdEffectDate.SelectedDate >= rdExpireDate.SelectedDate Then
                            ShowMessage(Translate("Ngày kết thúc phải lớn hơn ngày hiệu lực, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If IsDate(startDate) AndAlso txtFromEffect2.Text <> "" AndAlso startDate >= startDate1 Then
                            ShowMessage(Translate("Hiệu lực 2 phải lớn hơn Hiệu lực 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If IsDate(startDate) AndAlso txtFromEffect3.Text <> "" AndAlso startDate >= startDate2 Then
                            ShowMessage(Translate("Hiệu lực 3 phải lớn hơn Hiệu lực 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If IsDate(startDate) AndAlso txtFromEffect4.Text <> "" AndAlso startDate >= startDate3 Then
                            ShowMessage(Translate("Hiệu lực 4 phải lớn hơn Hiệu lực 1, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtFromEffect2.Text <> "" AndAlso txtFromEffect3.Text <> "" AndAlso startDate1 >= startDate2 Then
                            ShowMessage(Translate("Hiệu lực 3 phải lớn hơn Hiệu lực 2, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtFromEffect2.Text <> "" AndAlso txtFromEffect4.Text <> "" AndAlso startDate1 >= startDate3 Then
                            ShowMessage(Translate("Hiệu lực 4 phải lớn hơn Hiệu lực 2, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If txtFromEffect3.Text <> "" AndAlso txtFromEffect4.Text <> "" AndAlso startDate2 >= startDate3 Then
                            ShowMessage(Translate("Hiệu lực 4 phải lớn hơn Hiệu lực 3, Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        objHoliday.FROM_EFFECT1 = txtFromEffect1.Text
                        If ntxtToxicLeave1.Value IsNot Nothing Then
                            objHoliday.TOXICLEAVE1 = ntxtToxicLeave1.Value
                        End If

                        objHoliday.FROM_EFFECT2 = txtFromEffect2.Text
                        If ntxtToxicLeave2.Value IsNot Nothing Then
                            objHoliday.TOXICLEAVE2 = ntxtToxicLeave2.Value
                        End If

                        objHoliday.FROM_EFFECT3 = txtFromEffect3.Text
                        If ntxtToxicLeave3.Value IsNot Nothing Then
                            objHoliday.TOXICLEAVE3 = ntxtToxicLeave3.Value
                        End If

                        objHoliday.FROM_EFFECT4 = txtFromEffect4.Text
                        If ntxtToxicLeave4.Value IsNot Nothing Then
                            objHoliday.TOXICLEAVE4 = ntxtToxicLeave4.Value
                        End If

                        If rdEffectDate.SelectedDate IsNot Nothing Then
                            objHoliday.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If

                        If rdExpireDate.SelectedDate IsNot Nothing Then
                            objHoliday.EXPIRE_DATE = rdExpireDate.SelectedDate
                        End If

                        objHoliday.NOTE = rdNote.Text

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.CheckAtToxicLeaveEmp_DATE(objHoliday) = False Then
                                    ShowMessage(Translate("Đã tồn tại thông tin đã thiết lập."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertAtToxicLeaveEmp(objHoliday, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objHoliday.ID = rgDanhMuc.SelectedValue
                                If rep.CheckAtToxicLeaveEmp_DATE(objHoliday) = False Then
                                    ShowMessage(Translate("Đã tồn tại thông tin đã thiết lập."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyAtToxicLeaveEmp(objHoliday, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objHoliday.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        rgDanhMuc.SelectedIndexes.Clear()
                    Else
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "Thiết lập phép độc hại theo nhân viên")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Using xls As New ExcelCommon
                        Dim tempPath = "~/ReportTemplates//Attendance//Import//IMPORT_AT_TOXICLEAVE_EMP.xlsx"
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_AT_TOXICLEAVE_EMP_" & Format(Date.Now, "yyyyMMdd"), Nothing, Nothing, Response, "", ExportType.Excel, False)
                    End Using

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged

        Dim item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("ID").ToString
        Dim At_ListParamSystem1 = (From p In At_Holiday Where p.ID = Decimal.Parse(ID)).SingleOrDefault

        rdEffectDate.SelectedDate = At_ListParamSystem1.EFFECT_DATE
        rdExpireDate.SelectedDate = At_ListParamSystem1.EXPIRE_DATE

    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event click huy tren form popup list Nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <summary>
    ''' Chon row in grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            'xu ly lay thong tin nhan vien dua vào controls 
            If lstCommonEmployee IsNot Nothing AndAlso lstCommonEmployee.Count = 1 Then
                Dim empID = lstCommonEmployee(0)
                txtEmployeeCode.Text = empID.EMPLOYEE_CODE
                txtEmployeeName.Text = empID.FULLNAME_VN
                txtOrgName.Text = empID.ORG_NAME
                txtTitleName.Text = empID.TITLE_NAME
                hidEmp.Value = empID.EMPLOYEE_ID
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
    '    Dim rep As New AttendanceRepository
    '    Dim _validate As New AT_HOLIDAYDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.ID = rgDanhMuc.SelectedValue
    '            _validate.CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        Else
    '            _validate.CODE = txtCode.Text.Trim
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        End If

    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Ham xu ly viec check ngay thang
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvDate.ServerValidate
    '    Dim rep As New AttendanceRepository
    '    Dim _validate As New AT_HOLIDAYDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If CurrentState = CommonMessage.STATE_EDIT Then
    '            _validate.ID = rgDanhMuc.SelectedValue
    '            _validate.WORKINGDAY = rdDate.SelectedDate
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        Else
    '            _validate.WORKINGDAY = rdDate.SelectedDate
    '            args.IsValid = rep.ValidateHoliday(_validate)
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub


    Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0)
                        txtEmployeeCode.Text = empID.EMPLOYEE_CODE
                        txtEmployeeName.Text = empID.FULLNAME_VN
                        txtOrgName.Text = empID.ORG_NAME
                        txtTitleName.Text = empID.TITLE_NAME
                        hidEmp.Value = empID.EMPLOYEE_ID
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Reset_Find_Emp()
        'txtEmployeeCode.Text = ""
        txtEmployeeName.Text = ""
        txtOrgName.Text = ""
        txtTitleName.Text = ""
        hidEmp.Value = ""
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgDanhMuc.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub rgDanhMuc_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDanhMuc.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim fileName As String
            Dim workbook As Aspose.Cells.Workbook
            Dim worksheet As Aspose.Cells.Worksheet
            Dim dsDataPrepare As New DataSet

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xlsx")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            Dim dtData As New DataTable("Table")
            dtData.Columns.Add("STT", GetType(String))
            dtData.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtData.Columns.Add("EMPLOYEE_ID", GetType(String))
            dtData.Columns.Add("EMPLOYEE_NAME", GetType(String))
            dtData.Columns.Add("FROM_EFFECT1", GetType(String))
            dtData.Columns.Add("TOXICLEAVE1", GetType(String))
            dtData.Columns.Add("EFFECT_DATE", GetType(String))
            dtData.Columns.Add("EXPIRE_DATE", GetType(String))
            dtData.Columns.Add("NOTE", GetType(String))
            dtData.Columns.Add("OTHER_ERROR", GetType(String))
            dtData.Columns.Add("IS_ERROR", GetType(String))
            For Each row In dsDataPrepare.Tables(0).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtData.ImportRow(row)
                End If
            Next

            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate("File không có dữ liệu, kiểm tra lại file import"), NotifyType.Warning)
                Exit Sub
            End If

            Dim tbEmp As DataTable
            Dim startDate As Date
            Dim objHoliday As AT_TOXIC_LEAVE_EMPDTO
            For Each row In dtData.Rows
                row("IS_ERROR") = "0"

                If IsDBNull(row("EMPLOYEE_CODE")) OrElse row("EMPLOYEE_CODE").ToString = "" Then
                    row("EMPLOYEE_CODE") = "Bắt buộc nhập"
                    row("IS_ERROR") = "1"
                Else
                    tbEmp = rep.GetEmployeeID(row("EMPLOYEE_CODE").ToString, DateTime.Now)
                    If tbEmp.Rows.Count = 0 Then
                        row("EMPLOYEE_CODE") = "Không tồn tại"
                        row("IS_ERROR") = "1"
                    Else
                        row("EMPLOYEE_ID") = tbEmp.Rows(0)("ID").ToString
                    End If
                End If

                If IsDBNull(row("FROM_EFFECT1")) OrElse row("FROM_EFFECT1").ToString = "" Then
                    row("FROM_EFFECT1") = "Bắt buộc nhập"
                    row("IS_ERROR") = "1"
                ElseIf Not CheckDate(String.Format("{0}/{1}", row("FROM_EFFECT1").ToString, Date.Now.Year), startDate) Then
                    row("FROM_EFFECT1") = "Sai định dạng"
                    row("IS_ERROR") = "1"
                End If

                If IsDBNull(row("TOXICLEAVE1")) OrElse row("TOXICLEAVE1").ToString = "" Then
                    row("TOXICLEAVE1") = "Bắt buộc nhập"
                    row("IS_ERROR") = "1"
                Else
                    row("TOXICLEAVE1") = row("TOXICLEAVE1").ToString.Replace(",", ".")
                    If Not IsNumeric(row("TOXICLEAVE1").ToString) Then
                        row("TOXICLEAVE1") = "Sai định dạng"
                        row("IS_ERROR") = "1"
                    End If
                End If

                If IsDBNull(row("EFFECT_DATE")) OrElse row("EFFECT_DATE").ToString = "" Then
                    row("EFFECT_DATE") = "Bắt buộc nhập"
                    row("IS_ERROR") = "1"
                ElseIf Not CheckDate(row("EFFECT_DATE").ToString, startDate) Then
                    row("EFFECT_DATE") = "Sai định dạng"
                    row("IS_ERROR") = "1"
                End If

                If Not IsDBNull(row("EXPIRE_DATE")) AndAlso row("EXPIRE_DATE").ToString <> "" AndAlso Not CheckDate(row("EXPIRE_DATE").ToString, startDate) Then
                    row("EXPIRE_DATE") = "Sai định dạng"
                    row("IS_ERROR") = "1"
                End If

                If row("IS_ERROR") = "0" Then
                    objHoliday = New AT_TOXIC_LEAVE_EMPDTO
                    objHoliday.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID").ToString)
                    objHoliday.EFFECT_DATE = DateTime.ParseExact(row("EFFECT_DATE").ToString, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None)
                    If rep.CheckAtToxicLeaveEmp_DATE(objHoliday) = False Then
                        row("OTHER_ERROR") = "Đã tồn tại thông tin thiết lập"
                        row("IS_ERROR") = "1"
                    End If
                End If
            Next

            If dtData.Select("IS_ERROR = '1'").Length > 0 Then
                ShowMessage(Translate("Tác vụ thực hiện không thành công"), NotifyType.Error)
                Session("IMPORT_AT_TOXICLEAVE_EMP") = dtData.Select("IS_ERROR = '1'").CopyToDataTable
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('IMPORT_AT_TOXICLEAVE_EMP')", True)
            Else
                Dim sw As New StringWriter()
                dtData.WriteXml(sw, False)
                Dim DocXml = sw.ToString()

                If rep.IMPORT_AT_TOXICLEAVE_EMP(DocXml) Then
                    ShowMessage(Translate("Tác vụ thực hiện thành công"), NotifyType.Success)
                    rgDanhMuc.Rebind()
                Else
                    ShowMessage(Translate("Tác vụ thực hiện không thành công"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class