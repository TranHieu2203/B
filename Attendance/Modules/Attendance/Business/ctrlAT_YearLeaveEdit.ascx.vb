Imports System.IO
Imports Aspose.Cells
Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlAT_YearLeaveEdit
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrlOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrlFindSigner
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = True

    ''' <summary>
    ''' dsDataComper
    ''' </summary>
    ''' <remarks></remarks>
    Dim dsDataComper As New DataTable

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' isLoadPopupSP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopupSP As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopupSP")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopupSP") = value
        End Set
    End Property


    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' InsertSetUp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertSetUp As List(Of AT_SIGNDEFAULTDTO)
        Get
            Return ViewState(Me.ID & "_InsertSetUp")
        End Get
        Set(ByVal value As List(Of AT_SIGNDEFAULTDTO))
            ViewState(Me.ID & "_InsertSetUp") = value
        End Set
    End Property

    ''' <summary>
    ''' AT_SignDF
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_SignDF As List(Of AT_YEAR_LEAVE_EDITDTO)
        Get
            Return ViewState(Me.ID & "_SignDF")
        End Get
        Set(ByVal value As List(Of AT_YEAR_LEAVE_EDITDTO))
            ViewState(Me.ID & "_SignDF") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("YEAR", GetType(String))
                dt.Columns.Add("LEAVE_NUMBER", GetType(Decimal))
                dt.Columns.Add("NOTE", GetType(String))
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
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueSign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueSign As Decimal
        Get
            Return ViewState(Me.ID & "_ValueSign")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueSign") = value
        End Set
    End Property

    Property ListShifts As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_ListShifts")
        End Get
        Set(value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_ListShifts") = value
        End Set
    End Property


    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property


#End Region

#Region "Page"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            'SetGridFilter(rgWorkschedule)
            'rgWorkschedule.AllowCustomPaging = True

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try

            rgWorkschedule.SetFilter()
            rgWorkschedule.AllowCustomPaging = True

            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>Load cac control, menubar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceBusinessClient

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWorkschedule.Rebind()
                        SelectedItemDataGridByKey(rgWorkschedule, IDSelect, , rgWorkschedule.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWorkschedule.CurrentPageIndex = 0
                        rgWorkschedule.MasterTableView.SortExpressions.Clear()
                        rgWorkschedule.Rebind()
                        SelectedItemDataGridByKey(rgWorkschedule, IDSelect, )
                    Case "Cancel"
                        rgWorkschedule.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_YEAR_LEAVE_EDITDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgWorkschedule, obj)
            Dim Sorts As String = rgWorkschedule.MasterTableView.SortExpressions.GetSortString()


            If ctrlOrg.CurrentValue IsNot Nothing Then
                obj.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                rgWorkschedule.DataSource = New List(Of AT_SIGNDEFAULTDTO)
                Exit Function
            End If

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_SignDF = rep.GetAT_YEARLEAVE_EDIT(obj, rgWorkschedule.CurrentPageIndex, rgWorkschedule.PageSize, MaximumRows, Sorts)
                Else
                    Me.AT_SignDF = rep.GetAT_YEARLEAVE_EDIT(obj, rgWorkschedule.CurrentPageIndex, rgWorkschedule.PageSize, MaximumRows)
                End If
                rgWorkschedule.VirtualItemCount = MaximumRows
                rgWorkschedule.DataSource = Me.AT_SignDF
            Else
                Return rep.GetAT_YEARLEAVE_EDIT(obj).ToTable
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
                Case 2
                    phPopup.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtEmpCode, txtNote, ntxtYear, txtLeaveNumber, ntxtYear, txtLeaveOld)
                    EnableControlAll(False, txtEmpName, txtOrg, txtTitle)
                    btnChooseEmployee.Enabled = True
                    'ExcuteScript("Clear", "clRadDatePicker()")
                    EnabledGridNotPostback(rgWorkschedule, False)
                Case CommonMessage.STATE_NORMAL
                    btnChooseEmployee.Enabled = False

                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber, ntxtYear, txtLeaveOld)
                    EnableControlAll(False, txtNote, txtEmpCode, txtEmpName, txtOrg, txtTitle, ntxtYear, txtLeaveNumber, txtLeaveOld)
                    EnabledGridNotPostback(rgWorkschedule, True)
                Case CommonMessage.STATE_EDIT
                    btnChooseEmployee.Enabled = True
                    EnableControlAll(True, txtEmpCode, txtNote, ntxtYear, txtLeaveNumber, txtLeaveOld)
                    EnableControlAll(False, txtEmpName, txtOrg, txtTitle)
                    EnabledGridNotPostback(rgWorkschedule, False)
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorkschedule.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SIGNDEFAULT(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorkschedule.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                        ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber, txtLeaveOld)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWorkschedule.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWorkschedule.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_YEARLEAVE_EDIT(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        rgWorkschedule.Rebind()
                        ExcuteScript("Clear", "clRadDatePicker()")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

            btnChooseEmployee.Focus()
            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("TITLE_ID", hidTitleID)
            dic.Add("ORG_NAME", txtOrg)
            dic.Add("ORG_ID", hidOrgID)
            dic.Add("EMPLOYEE_CODE", txtEmpCode)
            dic.Add("EMPLOYEE_ID", hidEmpID)
            dic.Add("EMPLOYEE_NAME", txtEmpName)
            dic.Add("TITLE_NAME", txtTitle)
            dic.Add("YEAR", ntxtYear)
            dic.Add("LEAVE_NUMBER", txtLeaveNumber)
            dic.Add("LEAVE_OLD", txtLeaveOld)
            dic.Add("NOTE", txtNote)
            dic.Add("ID", hidID)
            Utilities.OnClientRowSelectedChanged(rgWorkschedule, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click button chon nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChooseEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnChooseEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnChooseEmployee.ID
                    ctrlFindEmployeePopup.IsHideTerminate = False
                    ctrlFindEmployeePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSIGN As New AT_YEAR_LEAVE_EDITDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim store As New AttendanceStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    hidID.Value = 0
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber, txtLeaveOld)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgWorkschedule.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgWorkschedule.SelectedItems(0)
                    hidID.Value = CDec(item.GetDataKeyValue("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    'isValidate = True
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWorkschedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        'objSIGN.YEAR = ntxtYear.Value
                        'objSIGN.LEAVE_NUMBER = Decimal.Parse(txtLeaveNumber.Text.Replace(".", ","))
                        'objSIGN.LEAVE_OLD = Decimal.Parse(txtLeaveOld.Text.Replace(".", ","))
                        'objSIGN.NOTE = txtNote.Text

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'objSIGN.EMPLOYEE_ID = hidEmpID.Value

                                Dim check As Decimal = store.CHECK_EXITS_YEARLEAVEEDIT(hidEmpID.Value, ntxtYear.Value, 0)

                                If check > 0 Then
                                    'ShowMessage(Translate("Dữ liệu phép điều chỉnh theo năm đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Error)
                                    'Exit Sub
                                    ctrlMessageBox.MessageText = "Dữ liệu đã tồn tại, bạn có muốn update thông tin"
                                    ctrlMessageBox.ActionName = "ReplaceExistAT_YEARLEAVE_EDIT"
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else
                                    Save()
                                End If

                                'If rep.InsertAT_YEARLEAVE_EDIT(objSIGN, gID) Then
                                '    CurrentState = CommonMessage.STATE_NORMAL
                                '    IDSelect = gID
                                '    Refresh("InsertView")
                                '    UpdateControlState()
                                '    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber)
                                '    rgWorkschedule.Rebind()
                                '    ExcuteScript("Clear", "clRadDatePicker()")
                                'Else
                                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                'End If
                            Case CommonMessage.STATE_EDIT

                                'objSIGN.EMPLOYEE_ID = hidEmpID.Value
                                'objSIGN.ID = hidID.Value

                                Dim check As Decimal = store.CHECK_EXITS_YEARLEAVEEDIT(hidEmpID.Value, ntxtYear.Value, hidID.Value)

                                If check > 0 Then
                                    'ShowMessage(Translate("Dữ liệu phép điều chỉnh theo năm đã tồn tại, Vui lòng kiểm tra lại."), Utilities.NotifyType.Error)
                                    'Exit Sub
                                    ctrlMessageBox.MessageText = "Dữ liệu đã tồn tại, bạn có muốn update thông tin"
                                    ctrlMessageBox.ActionName = "ReplaceExistAT_YEARLEAVE_EDIT"
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else
                                    Save()
                                End If

                                'If rep.ModifyAT_YEARLEAVE_EDIT(objSIGN, rgWorkschedule.SelectedValue) Then
                                '    CurrentState = CommonMessage.STATE_NORMAL
                                '    IDSelect = objSIGN.ID
                                '    Refresh("UpdateView")
                                '    UpdateControlState()
                                '    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber)
                                '    rgWorkschedule.Rebind()
                                '    ExcuteScript("Clear", "clRadDatePicker()")
                                'Else
                                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                'End If
                        End Select
                    Else
                        ExcuteScript("Resize", "setDefaultSize()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    ExcuteScript("Clear", "clRadDatePicker()")
                    UpdateControlState()

                Case "EXPORT_TEMP"
                    ExportTemplate("Attendance\Import\Template_YearLeaveEdit.xlsx", "Import_phepdieuchinh" & Format(Date.Now, "yyyyMMdd"))

                Case "IMPORT_TEMP"
                    ctrlUpload.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgWorkschedule.ExportExcel(Server, Response, dtDatas, "DSPhepDieuChinh")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
            rep.Dispose()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    Public Function ExportTemplate(ByVal sReportFileName As String,
                                ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String
        Dim filePathOut As String
        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName
            filePathOut = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\"
            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            'designer.SetDataSource(dsData)

            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", Aspose.Cells.ContentDisposition.Attachment, New XlsSaveOptions(Aspose.Cells.SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function



    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
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

            If e.ActionName = "ReplaceExistAT_YEARLEAVE_EDIT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New AttendanceRepository
                rep.DeleteExistAT_YEARLEAVE_EDIT(hidEmpID.Value, ntxtYear.Value)
                Save()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
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
            rgWorkschedule.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgWorkschedule.SelectedIndexChanged
    '    Dim item = CType(rgWorkschedule.SelectedItems(0), GridDataItem)
    '    Dim ID = item.GetDataKeyValue("ID").ToString
    '    Dim data = (From p In AT_SignDF Where p.ID = Decimal.Parse(ID)).SingleOrDefault

    '    ntxtLeaveNumber.Value = data.LEAVE_NUMBER
    'End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [chọn] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_SingDefault&orgid= " & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Import_Data()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New AttendanceRepository
            '_mylog = LogHelper.GetUserLog
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping1(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_AT_YEARLEAVE_EDIT(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgWorkschedule.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(1).ColumnName = "YEAR"
        dtTemp.Columns(2).ColumnName = "LEAVE_NUMBER"
        dtTemp.Columns(3).ColumnName = "LEAVE_OLD"
        dtTemp.Columns(4).ColumnName = "NOTE"

        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim empId As Integer
        Dim rep As New ProfileBusinessRepository
        Dim store As New AttendanceStoreProcedure
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 3 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If Not IsNumeric(rows("YEAR")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Năm - Không đúng định dạng,"
                _error = False
            End If

            If Not IsNumeric(rows("LEAVE_NUMBER")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số ngày điều chỉnh - Không đúng định dạng,"
                _error = False
            Else
                rows("LEAVE_NUMBER") = rows("LEAVE_NUMBER").ToString.Replace(",", ".")
            End If

            If Not IsDBNull(rows("LEAVE_OLD")) AndAlso rows("LEAVE_OLD").ToString <> "" Then
                If Not IsNumeric(rows("LEAVE_OLD").ToString) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Số ngày cũ điều chỉnh - Không đúng định dạng,"
                    _error = False
                Else
                    rows("LEAVE_OLD") = rows("LEAVE_OLD").ToString.Replace(",", ".")
                End If
            Else
                rows("LEAVE_OLD") = 0
            End If

            If IsNumeric(rows("YEAR")) Then
                Dim check As Decimal = store.CHECK_EXITS_YEARLEAVEEDIT(empId, rows("YEAR"), 0)

                If check > 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã nhân viên + Năm đã tồn tại,"
                    _error = False
                End If
            End If

            'If IsNumeric(rows("YEAR")) AndAlso IsNumeric(rows("LEAVE_NUMBER")) Then
            '    Dim check As Decimal = store.CHECK_EXITS_YEARLEAVEEDIT(empId, rows("YEAR"), rows("LEAVE_NUMBER"))

            '    If check > 0 Then
            '        newRow("DISCIPTION") = newRow("DISCIPTION") + "Số ngày điều chỉnh theo năm  - đã tồn tại,"
            '    End If
            'End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next

        dtTemp.AcceptChanges()
    End Sub
    Private Sub txtEmpCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmpCode.TextChanged, ntxtYear.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmpCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmpCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmpCode.Text = ""
                    ElseIf Count = 1 Then
                        GetValue_Find_Emp(EmployeeList)
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmpCode.Text
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

            If txtEmpCode.Text <> "" And ntxtYear.Text <> "" Then
                Dim psp As New ProfileStoreProcedure
                txtLeaveHave.Text = psp.CAL_REMAINING_LEAVE_HAVE(CDec(hidEmpID.Value), CDec(ntxtYear.Text)).ToString
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            ListShifts = ListComboData.LIST_LIST_SHIFT


            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve nhân viên khi click button [chọn] o popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)

                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_SIGNDEFAULTDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.EMPLOYEE_NAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.TITLE_ID = lstCommonEmployee(idx).TITLE_ID
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    InsertSetUp.Add(item)
                    txtEmpCode.Text = lstCommonEmployee(idx).EMPLOYEE_CODE
                    txtEmpName.Text = lstCommonEmployee(idx).FULLNAME_VN
                    txtOrg.Text = lstCommonEmployee(idx).ORG_NAME
                    txtTitle.Text = lstCommonEmployee(idx).TITLE_NAME
                    ' đẩy dữ liệu vào hider
                    hidEmpID.Value = lstCommonEmployee(idx).ID
                    hidOrgID.Value = lstCommonEmployee(idx).ORG_ID
                    hidTitleID.Value = lstCommonEmployee(idx).TITLE_ID
                Next
                isLoadPopup = 0
            Else
                InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
                ' SetGridEditRow()
                isLoadPopup = 0
            End If

            rgWorkschedule.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub GetValue_Find_Emp(ByVal lstCommonEmployee As List(Of CommonBusiness.EmployeePopupFindListDTO))
        If lstCommonEmployee.Count <> 0 Then
            InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)

            For idx = 0 To lstCommonEmployee.Count - 1
                Dim item As New AT_SIGNDEFAULTDTO
                item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                item.EMPLOYEE_NAME = lstCommonEmployee(idx).FULLNAME_VN
                item.TITLE_ID = lstCommonEmployee(idx).TITLE_ID
                item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                InsertSetUp.Add(item)
                txtEmpCode.Text = lstCommonEmployee(idx).EMPLOYEE_CODE
                txtEmpName.Text = lstCommonEmployee(idx).FULLNAME_VN
                txtOrg.Text = lstCommonEmployee(idx).ORG_NAME
                txtTitle.Text = lstCommonEmployee(idx).TITLE_NAME
                ' đẩy dữ liệu vào hider
                hidEmpID.Value = lstCommonEmployee(idx).ID
                hidOrgID.Value = lstCommonEmployee(idx).ORG_ID
                hidTitleID.Value = lstCommonEmployee(idx).TITLE_ID
            Next
            isLoadPopup = 0
        Else
            InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
            ' SetGridEditRow()
            isLoadPopup = 0
        End If
    End Sub
    Private Sub Reset_Find_Emp()
        InsertSetUp = New List(Of AT_SIGNDEFAULTDTO)
        'txtEmpCode.Text = ""
        txtEmpName.Text = ""
        txtOrg.Text = ""
        txtTitle.Text = ""
        ' đẩy dữ liệu vào hider
        hidEmpID.Value = Nothing
        hidOrgID.Value = Nothing
        hidTitleID.Value = Nothing
    End Sub

    Private Sub Save()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim objSIGN As New AT_YEAR_LEAVE_EDITDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim store As New AttendanceStoreProcedure

        'HUNGTN === 20/12/2022 === BCG-901
        If IsNumeric(ntxtYear.Value) Then
            objSIGN.YEAR = ntxtYear.Value
        End If
        If IsNumeric(txtLeaveNumber.Text.Replace(".", ",")) Then
            objSIGN.LEAVE_NUMBER = Decimal.Parse(txtLeaveNumber.Text.Replace(".", ","))
        End If
        If IsNumeric(txtLeaveOld.Text.Replace(".", ",")) Then
            objSIGN.LEAVE_OLD = Decimal.Parse(txtLeaveOld.Text.Replace(".", ","))
        Else
            objSIGN.LEAVE_OLD = 0
        End If
        objSIGN.NOTE = txtNote.Text

        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                objSIGN.EMPLOYEE_ID = hidEmpID.Value
                If rep.InsertAT_YEARLEAVE_EDIT(objSIGN, gID) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    IDSelect = gID
                    Refresh("InsertView")
                    UpdateControlState()
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber)
                    rgWorkschedule.Rebind()
                    ExcuteScript("Clear", "clRadDatePicker()")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            Case CommonMessage.STATE_EDIT
                objSIGN.EMPLOYEE_ID = hidEmpID.Value
                objSIGN.ID = hidID.Value
                If rep.ModifyAT_YEARLEAVE_EDIT(objSIGN, rgWorkschedule.SelectedValue) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    IDSelect = objSIGN.ID
                    Refresh("UpdateView")
                    UpdateControlState()
                    ClearControlValue(txtEmpCode, txtEmpName, txtOrg, txtTitle, txtNote, ntxtYear, txtLeaveNumber)
                    rgWorkschedule.Rebind()
                    ExcuteScript("Clear", "clRadDatePicker()")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
        End Select
    End Sub

    Private Sub rgWorkschedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorkschedule.ItemDataBound
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



End Class