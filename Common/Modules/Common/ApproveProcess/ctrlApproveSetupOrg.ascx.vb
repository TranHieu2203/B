Imports System.IO
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveSetupOrg
    Inherits CommonView
    Private rep As New CommonProcedureNew
    Dim user As UserLog
    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ApproveSetups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApproveSetups() As List(Of ApproveSetupDTO)
        Get
            Return ViewState(Me.ID & "_ApproveSetups")
        End Get

        Set(ByVal value As List(Of ApproveSetupDTO))
            ViewState(Me.ID & "_ApproveSetups") = value
        End Set
    End Property

    ''' <summary>
    ''' ListToDelete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListToDelete As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Delete_List")
        End Get

        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Delete_List") = value
        End Set
    End Property

    ''' <summary>
    ''' OrgID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrgID As Decimal
        Get
            Return ViewState(Me.ID & "_OrgID")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_OrgID") = value
        End Set
    End Property

    ''' <summary>
    ''' IDDetailSelecting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDetailSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_ID_Detail_Selecting")
        End Get

        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_ID_Detail_Selecting") = value
        End Set
    End Property

#End Region

#Region "Event"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                Refresh()
            End If

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarDetail
            Common.BuildToolbar(tbarDetail,
                                ToolbarItem.Create,
                                ToolbarItem.Edit, ToolbarItem.Seperator,
                                ToolbarItem.Save,
                                ToolbarItem.Cancel, ToolbarItem.Seperator,
                                ToolbarItem.Export,
                                ToolbarItem.Next,
                                ToolbarItem.Import, ToolbarItem.Seperator,
                                ToolbarItem.Delete)

            CType(tbarDetail.Items(3), RadToolBarButton).CausesValidation = True

            SetStatusToolbar(tbarDetail, CommonMessage.STATE_NORMAL, False)
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(7), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(6), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(8), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            rgDetail.ClientSettings.EnablePostBackOnRowClick = True
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Load data cho combobox </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            LoadComboData()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDetail.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load data vào textbox khi chọn checkbox ở grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDetail.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            IDDetailSelecting = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)

            LoadDetailView()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi lựa chọn phần tử của popup ctrlOrg</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            OrgID = Decimal.Parse(ctrlOrg.CurrentValue)
            CurrentState = CommonMessage.STATE_NORMAL

            CreateDataFilter()
            UpdateControlState()
            Refresh()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)            
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi click button Detail vủa 1 row trên grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarDetail_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Me.CurrentState = CommonMessage.STATE_NEW
                    IDDetailSelecting = Nothing
                    UpdateControlState()
                    ClearControlForInsertOrDelete()
                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                Case CommonMessage.TOOLBARITEM_EDIT
                    Me.CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Not Page.IsValid Then
                        ExcuteScript("Resize", "setDefaultSize()")
                        Exit Sub
                    End If
                    Dim db As New CommonRepository
                    Dim OBJ = (From item In cboTitle.CheckedItems Select New TitleDTO With {.ID = item.Value}).ToList()
                    If OBJ.Count = 0 Then

                        If Not IDDetailSelecting.HasValue Then
                            'Thêm mới
                            If cboPosition.CheckedItems.Count > 0 Then
                                For ii As Integer = 0 To cboPosition.CheckedItems.Count - 1 Step 1
                                    Dim itemAdd As New ApproveSetupDTO
                                    With itemAdd
                                        .ORG_ID = OrgID
                                        .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                                        .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                                        .TITLE_ID = Nothing
                                        .SIGN_ID = If(cboKieuCong.SelectedValue <> "", Decimal.Parse(cboKieuCong.SelectedValue), Nothing)
                                        .LEAVEPLAN_ID = If(cboLeavePlan.SelectedValue <> "", Decimal.Parse(cboLeavePlan.SelectedValue), Nothing)
                                        .FROM_HOUR = If(txtFromHour.Value IsNot Nothing, Decimal.Parse(txtFromHour.Value), Nothing)
                                        .TO_HOUR = If(txtToHour.Value IsNot Nothing, Decimal.Parse(txtToHour.Value), Nothing)
                                        .FROM_DAY = If(rntxtFromDay.Value IsNot Nothing, Decimal.Parse(rntxtFromDay.Value), Nothing)
                                        .TO_DAY = If(rntxtToDay.Value IsNot Nothing, Decimal.Parse(rntxtToDay.Value), Nothing)
                                        .FROM_DATE = rdFromDate.SelectedDate
                                        .TO_DATE = rdToDate.SelectedDate
                                        .MAIL_ACCEPTED = txtCCMailAccepted.Text
                                        .MAIL_ACCEPTING = txtCCMailAccepting.Text
                                        .IS_IGNORE_APPROVE_LEVEL = chkIgnoreapprovelvl.Checked
                                        .IS_OVER_LIMIT = ChkDB.Checked
                                        .GROUP_TITLE_ID = cboPosition.CheckedItems(ii).Value
                                    End With
                                    If CheckExistValueCombo(itemAdd.PROCESS_ID, itemAdd.TEMPLATE_ID) Then
                                        LoadComboData()
                                        cboApproveProcess.SelectedIndex = 0
                                        cboApproveTemplate.SelectedIndex = 0
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                        Exit Sub
                                    End If

                                    If db.InsertApproveSetup(itemAdd) Then
                                        ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                        Exit Sub
                                    End If
                                Next
                            Else
                                Dim itemAdd As New ApproveSetupDTO
                                With itemAdd
                                    .ORG_ID = OrgID
                                    .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                                    .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                                    .TITLE_ID = Nothing
                                    .SIGN_ID = If(cboKieuCong.SelectedValue <> "", Decimal.Parse(cboKieuCong.SelectedValue), Nothing)
                                    .LEAVEPLAN_ID = If(cboLeavePlan.SelectedValue <> "", Decimal.Parse(cboLeavePlan.SelectedValue), Nothing)
                                    .FROM_HOUR = If(txtFromHour.Value IsNot Nothing, Decimal.Parse(txtFromHour.Value), Nothing)
                                    .TO_HOUR = If(txtToHour.Value IsNot Nothing, Decimal.Parse(txtToHour.Value), Nothing)
                                    .FROM_DAY = If(rntxtFromDay.Value IsNot Nothing, Decimal.Parse(rntxtFromDay.Value), Nothing)
                                    .TO_DAY = If(rntxtToDay.Value IsNot Nothing, Decimal.Parse(rntxtToDay.Value), Nothing)
                                    .FROM_DATE = rdFromDate.SelectedDate
                                    .TO_DATE = rdToDate.SelectedDate
                                    .MAIL_ACCEPTED = txtCCMailAccepted.Text
                                    .MAIL_ACCEPTING = txtCCMailAccepting.Text
                                    .IS_IGNORE_APPROVE_LEVEL = chkIgnoreapprovelvl.Checked
                                    .IS_OVER_LIMIT = ChkDB.Checked
                                    .GROUP_TITLE_ID = 0
                                End With
                                If CheckExistValueCombo(itemAdd.PROCESS_ID, itemAdd.TEMPLATE_ID) Then
                                    LoadComboData()
                                    cboApproveProcess.SelectedIndex = 0
                                    cboApproveTemplate.SelectedIndex = 0
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                If db.InsertApproveSetup(itemAdd) Then
                                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        Else
                            ' Sửa đổi
                            If cboTitle.CheckedItems.Count > 1 Then
                                ShowMessage(Translate("Khi thực hiện sửa thì chỉ được chọn 1 chức danh. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            If cboPosition.CheckedItems.Count > 1 Then
                                ShowMessage(Translate("Khi thực hiện sửa thì chỉ được chọn 1 cấp chức danh. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If

                            Dim idEdit As Decimal = IDDetailSelecting.Value

                            Dim itemEdit As ApproveSetupDTO = db.GetApproveSetup(idEdit)
                            With itemEdit
                                .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                                .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                                .TITLE_ID = Nothing
                                .SIGN_ID = If(cboKieuCong.SelectedValue <> "", Decimal.Parse(cboKieuCong.SelectedValue), Nothing)
                                .LEAVEPLAN_ID = If(cboLeavePlan.SelectedValue <> "", Decimal.Parse(cboLeavePlan.SelectedValue), Nothing)
                                .FROM_HOUR = If(txtFromHour.Value IsNot Nothing, Decimal.Parse(txtFromHour.Value), Nothing)
                                .TO_HOUR = If(txtToHour.Value IsNot Nothing, Decimal.Parse(txtToHour.Value), Nothing)
                                .FROM_DAY = If(rntxtFromDay.Value IsNot Nothing, Decimal.Parse(rntxtFromDay.Value), Nothing)
                                .TO_DAY = If(rntxtToDay.Value IsNot Nothing, Decimal.Parse(rntxtToDay.Value), Nothing)
                                .FROM_DATE = rdFromDate.SelectedDate
                                .TO_DATE = rdToDate.SelectedDate
                                .MAIL_ACCEPTED = txtCCMailAccepted.Text
                                .MAIL_ACCEPTING = txtCCMailAccepting.Text
                                .IS_IGNORE_APPROVE_LEVEL = chkIgnoreapprovelvl.Checked
                                .IS_OVER_LIMIT = ChkDB.Checked
                                .GROUP_TITLE_ID = If(IsNumeric(cboPosition.SelectedValue), Decimal.Parse(cboPosition.SelectedValue), Nothing)
                            End With

                            If CheckExistValueCombo(itemEdit.PROCESS_ID, itemEdit.TEMPLATE_ID) Then
                                LoadComboData()
                                cboApproveProcess.SelectedIndex = 0
                                cboApproveTemplate.SelectedIndex = 0
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                Exit Sub
                            End If

                            If db.UpdateApproveSetup(itemEdit) Then
                                ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                    Else
                        For Each item In OBJ
                            If Not IDDetailSelecting.HasValue Then
                                'thêm mới
                                Dim itemAdd As New ApproveSetupDTO
                                With itemAdd
                                    .ORG_ID = OrgID
                                    .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                                    .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                                    .TITLE_ID = item.ID
                                    .SIGN_ID = If(cboKieuCong.SelectedValue <> "", Decimal.Parse(cboKieuCong.SelectedValue), Nothing)
                                    .LEAVEPLAN_ID = If(cboLeavePlan.SelectedValue <> "", Decimal.Parse(cboLeavePlan.SelectedValue), Nothing)
                                    .FROM_HOUR = If(txtFromHour.Value IsNot Nothing, Decimal.Parse(txtFromHour.Value), Nothing)
                                    .TO_HOUR = If(txtToHour.Value IsNot Nothing, Decimal.Parse(txtToHour.Value), Nothing)
                                    .FROM_DAY = If(rntxtFromDay.Value IsNot Nothing, Decimal.Parse(rntxtFromDay.Value), Nothing)
                                    .TO_DAY = If(rntxtToDay.Value IsNot Nothing, Decimal.Parse(rntxtToDay.Value), Nothing)
                                    .FROM_DATE = rdFromDate.SelectedDate
                                    .TO_DATE = rdToDate.SelectedDate
                                    .MAIL_ACCEPTED = txtCCMailAccepted.Text
                                    .MAIL_ACCEPTING = txtCCMailAccepting.Text
                                    .IS_IGNORE_APPROVE_LEVEL = chkIgnoreapprovelvl.Checked
                                    .IS_OVER_LIMIT = ChkDB.Checked
                                    .GROUP_TITLE_ID = Nothing
                                End With
                                If CheckExistValueCombo(itemAdd.PROCESS_ID, itemAdd.TEMPLATE_ID) Then
                                    LoadComboData()
                                    cboApproveProcess.SelectedIndex = 0
                                    cboApproveTemplate.SelectedIndex = 0
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                If db.InsertApproveSetup(itemAdd) Then
                                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    Exit Sub
                                End If
                            Else

                                If cboTitle.CheckedItems.Count > 1 Then
                                    ShowMessage(Translate("Khi thực hiện sửa thì chỉ được chọn 1 chức danh. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If

                                If cboPosition.CheckedItems.Count > 1 Then
                                    ShowMessage(Translate("Khi thực hiện sửa thì chỉ được chọn 1 cấp chức danh. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                Dim idEdit As Decimal = IDDetailSelecting.Value

                                Dim itemEdit As ApproveSetupDTO = db.GetApproveSetup(idEdit)
                                With itemEdit
                                    .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                                    .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                                    .TITLE_ID = item.ID
                                    .SIGN_ID = If(cboKieuCong.SelectedValue <> "", Decimal.Parse(cboKieuCong.SelectedValue), Nothing)
                                    .LEAVEPLAN_ID = If(cboLeavePlan.SelectedValue <> "", Decimal.Parse(cboLeavePlan.SelectedValue), Nothing)
                                    .FROM_HOUR = If(txtFromHour.Value IsNot Nothing, Decimal.Parse(txtFromHour.Value), Nothing)
                                    .TO_HOUR = If(txtToHour.Value IsNot Nothing, Decimal.Parse(txtToHour.Value), Nothing)
                                    .FROM_DAY = If(rntxtFromDay.Value IsNot Nothing, Decimal.Parse(rntxtFromDay.Value), Nothing)
                                    .TO_DAY = If(rntxtToDay.Value IsNot Nothing, Decimal.Parse(rntxtToDay.Value), Nothing)
                                    .FROM_DATE = rdFromDate.SelectedDate
                                    .TO_DATE = rdToDate.SelectedDate
                                    .MAIL_ACCEPTED = txtCCMailAccepted.Text
                                    .MAIL_ACCEPTING = txtCCMailAccepting.Text
                                    .IS_IGNORE_APPROVE_LEVEL = chkIgnoreapprovelvl.Checked
                                    .IS_OVER_LIMIT = ChkDB.Checked
                                    .GROUP_TITLE_ID = Nothing
                                End With

                                If CheckExistValueCombo(itemEdit.PROCESS_ID, itemEdit.TEMPLATE_ID) Then
                                    LoadComboData()
                                    cboApproveProcess.SelectedIndex = 0
                                    cboApproveTemplate.SelectedIndex = 0
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                If db.UpdateApproveSetup(itemEdit) Then
                                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If

                    Refresh()
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate, rntxtFromDay, rntxtToDay, txtFromHour, txtToHour, txtCCMailAccepting, txtCCMailAccepted)
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dt As DataTable = rep.GET_ALL_APPROVE_SETUP_ORG()
                        If dt.Rows.Count > 0 Then
                            xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/Common/Approve/Import_Phe_Duyet_PhongBan.xls"), "DanhSachPheDuyetPhongBan", dt, Response)
                        Else
                            ShowMessage(Translate("Không có dữ liệu để xuất danh sách"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    Using xls As New ExcelCommon
                        Dim db As New CommonRepository
                        Dim ds As DataSet = New DataSet
                        Dim org = (From l In db.GetOrganizationList()
                                   Select New With {l.DESCRIPTION_PATH, l.ID}).ToList()
                        Dim process = (From l In db.GetApproveProcessList()
                                       Select New With {l.NAME, l.ID}).ToList()
                        Dim template = (From l In db.GetApproveTemplateList()
                                        Select New With {l.TEMPLATE_NAME, l.ID}).ToList()
                        Dim title = (From l In db.GetTitleList()
                                     Select New With {l.NAME_VN, l.CODE}).ToList()
                        Dim sign = (From l In db.GetSignList()
                                    Select New With {l.NAME, l.ID}).ToList()
                        Dim leaveplan = (From l In db.GetLeavePlanList()
                                         Select New With {l.NAME_VN, l.ID}).ToList()
                        ds.Tables.Add(org.ToTable())
                        ds.Tables.Add(process.ToTable())
                        ds.Tables.Add(template.ToTable())
                        ds.Tables.Add(title.ToTable())
                        ds.Tables.Add(sign.ToTable())
                        ds.Tables.Add(leaveplan.ToTable())
                        xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/Common/Approve/Import_Phe_Duyet_PhongBan.xls"), "Template phê duyệt phòng ban", ds, Response, 1)
                    End Using
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim idDelete = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
                    Dim db As New CommonRepository
                    Dim listDelete As New List(Of Decimal)

                    listDelete.Add(idDelete)
                    ListToDelete = listDelete

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    ClearControlForInsertOrDelete()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi click button của popup warning</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim db As New CommonRepository

                If db.DeleteApproveSetup(ListToDelete) Then
                    ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ' ''' <lastupdate>25/07/2017</lastupdate>
    ' ''' <summary>Sự kiện khi click button của popup warning</summary>
    ' ''' <param name="source"></param>
    ' ''' <param name="args"></param>
    ' ''' <remarks></remarks>
    'Private Sub cvalCheckDateExist_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCheckDateExist.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        Dim db As New CommonRepository
    '        Dim idExclude As Decimal? = IDDetailSelecting
    '        Dim itemCheck As New ApproveSetupDTO _
    '        With {
    '            .ORG_ID = OrgID,
    '            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue),
    '            .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue),
    '            .FROM_DATE = rdFromDate.SelectedDate,
    '            .TO_DATE = rdToDate.SelectedDate
    '        }

    '        args.IsValid = db.IsExistApproveSetupByDate(itemCheck, idExclude)

    '        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Cập nhật trạng thái control </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetStatusToolbar(tbarDetail, Me.CurrentState)
            Select Case Me.CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGrid(rgDetail, True)
                    pnlDetail.Enabled = False
                    ctrlOrg.Enabled = True
                    cboApproveProcess.AutoPostBack = False
                    ApproveProcessChange()
                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    EnabledGrid(rgDetail, True)
                    pnlDetail.Enabled = True
                    ctrlOrg.Enabled = False
                    cboApproveProcess.AutoPostBack = True
                    cboApproveProcess.CausesValidation = False
                    ApproveProcessChange()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Dim obj As New ApproveSetupDTO

        Try
            If ctrlOrg.CurrentValue = "" Then
                rgDetail.DataSource = New List(Of String)
            Else
                Dim MaximumRows As Integer

                SetValueObjectByRadGrid(rgDetail, obj)

                Dim Sorts As String = rgDetail.MasterTableView.SortExpressions.GetSortString()
                If Sorts IsNot Nothing Then
                    Me.ApproveSetups = rep.GetApproveSetupByOrg(OrgID, Sorts)
                Else
                    Me.ApproveSetups = rep.GetApproveSetupByOrg(OrgID)
                End If

                rgDetail.VirtualItemCount = MaximumRows
                rgDetail.DataSource = Me.ApproveSetups
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Khoi tao, refresh grid</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ClearControlValue(cboApproveProcess, cboApproveTemplate, cboKieuCong, cboPosition, cboTitle, cboLeavePlan, rdFromDate, rdToDate)
            rgDetail.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Clear ctrl trước khi thêm mới</summary>
    ''' <remarks></remarks>
    Private Sub ClearControlForInsertOrDelete()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            cboApproveProcess.SelectedIndex = 0
            cboApproveTemplate.SelectedIndex = 0
            cboPosition.SelectedIndex = 0
            cboKieuCong.SelectedIndex = 0
            cboLeavePlan.SelectedIndex = 0
            txtFromHour.Text = String.Empty
            txtToHour.Text = String.Empty
            rntxtFromDay.Text = String.Empty
            rntxtToDay.Text = String.Empty
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing
            txtCCMailAccepted.Text = String.Empty
            txtCCMailAccepting.Text = String.Empty

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub LoadComboData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim dbProcess = db.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A").ToList()
            cboApproveProcess.DataSource = dbProcess
            cboApproveProcess.DataTextField = "NAME"
            cboApproveProcess.DataValueField = "ID"
            cboApproveProcess.DataBind()

            Dim dbTemplate = db.GetApproveTemplateList().Where(Function(p) p.TEMPLATE_TYPE = 0).ToList()
            cboApproveTemplate.DataSource = dbTemplate
            cboApproveTemplate.DataTextField = "TEMPLATE_NAME"
            cboApproveTemplate.DataValueField = "ID"
            cboApproveTemplate.DataBind()

            Dim dbTitle = db.GetTitleList()
            'cboPosition.DataSource = dbTitle
            'cboPosition.DataTextField = "NAME_VN"
            'cboPosition.DataValueField = "ID"
            'cboPosition.DataBind()
            For Each i In dbTitle
                Dim item As New RadComboBoxItem()
                item.Text = i.NAME_VN
                item.Value = i.ID
                cboPosition.Items.Add(item)
            Next

            Dim dtSignList = db.GetSignList()
            cboKieuCong.DataSource = dtSignList
            cboKieuCong.DataTextField = "NAME"
            cboKieuCong.DataValueField = "ID"
            cboKieuCong.DataBind()

            Dim dtLeavePlan = db.GetLeavePlanList()
            cboLeavePlan.DataSource = dtLeavePlan
            cboLeavePlan.DataTextField = "NAME_VN"
            cboLeavePlan.DataValueField = "ID"
            cboLeavePlan.DataBind()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load thông tin chi tiết của mỗi thiết lập</summary>
    ''' <remarks></remarks>
    Private Sub LoadDetailView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim itemSelected = db.GetApproveSetup(IDDetailSelecting)

            If itemSelected IsNot Nothing Then
                cboApproveProcess.SelectedValue = itemSelected.PROCESS_ID.ToString
                cboApproveTemplate.SelectedValue = itemSelected.TEMPLATE_ID.ToString
                'cboPosition.SelectedValue = itemSelected.TITLE_ID.ToString
                cboKieuCong.SelectedValue = itemSelected.SIGN_ID.ToString
                cboLeavePlan.SelectedValue = itemSelected.LEAVEPLAN_ID.ToString
                txtFromHour.Value = itemSelected.FROM_HOUR
                txtToHour.Value = itemSelected.TO_HOUR
                rntxtFromDay.Value = itemSelected.FROM_DAY
                rntxtToDay.Value = itemSelected.TO_DAY
                txtFromHour.DisplayText = itemSelected.FROM_HOUR.ToString
                txtToHour.DisplayText = itemSelected.TO_HOUR.ToString
                rntxtFromDay.DisplayText = itemSelected.FROM_DAY.ToString
                rntxtToDay.DisplayText = itemSelected.TO_DAY.ToString
                rdFromDate.SelectedDate = itemSelected.FROM_DATE
                rdToDate.SelectedDate = itemSelected.TO_DATE
                txtCCMailAccepted.Text = itemSelected.MAIL_ACCEPTED
                txtCCMailAccepting.Text = itemSelected.MAIL_ACCEPTING
                chkIgnoreapprovelvl.Checked = itemSelected.IS_IGNORE_APPROVE_LEVEL
                Dim dbProcess = db.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A" And p.PROCESS_CODE = "RECRUITMENT" And p.ID = itemSelected.PROCESS_ID.ToString).ToList()
                If dbProcess.Count > 0 Then
                    ChkDB.Visible = True
                    ChkDB.Checked = itemSelected.IS_OVER_LIMIT
                Else
                    ChkDB.Visible = False
                End If
                Dim title_lst = db.GetTitleAll()
                For Each i In title_lst
                    Dim item As New RadComboBoxItem()
                    item.Text = i.NAME_VN
                    item.Value = i.ID
                    cboTitle.Items.Add(item)
                Next
                Dim itemcheck = cboTitle.FindItemByValue(itemSelected.TITLE_ID.ToString)
                If itemcheck IsNot Nothing Then
                    itemcheck.Checked = True
                Else
                    cboTitle.Items.Clear()
                End If
                If itemSelected.GROUP_TITLE_ID IsNot Nothing Then
                    For i As Integer = 0 To cboPosition.Items.Count - 1 Step 1
                        If cboPosition.Items(i).Value = itemSelected.GROUP_TITLE_ID.ToString Then
                            cboPosition.Items(i).Checked = True
                        End If
                    Next
                End If
            Else
                ClearControlForInsertOrDelete()
                ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Error)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub cboPosition_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboPosition.SelectedIndexChanged
        Try
            cboTitle.Items.Clear()
            Dim db As New CommonRepository
            Dim OBJ = (From item In cboPosition.CheckedItems Select New OtherListDTO With {.ID = item.Value}).ToList()
            Dim title_lst = db.GetTitleByRank_ID(OBJ)
            For Each i In title_lst
                Dim item As New RadComboBoxItem()
                item.Text = i.NAME_VN
                item.Value = i.ID
                cboTitle.Items.Add(item)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Thiết lập trạng thái của Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub SetStatusToolbar(ByVal _tbar As RadToolBar, ByVal formState As String, Optional ByVal stateToAll As Boolean? = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case formState
                Case CommonMessage.STATE_NORMAL
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = False
                            Case Else
                                item.Enabled = True
                        End Select
                    Next

                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = True
                            Case Else
                                item.Enabled = False
                        End Select
                    Next
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Kiểm tra value của combobox Quy trình, Template áp dụng có tồn tại hay không</summary>
    ''' <param name="decAppProcess"></param>
    ''' <param name="decAppTemplate"></param>
    ''' <remarks></remarks>
    Private Function CheckExistValueCombo(ByVal decAppProcess As Decimal, ByVal decAppTemplate As Decimal) As Boolean
        CheckExistValueCombo = False
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim lst As New List(Of Decimal)

            lst.Add(decAppProcess)
            If db.CheckExistIDTable(lst, "SE_APP_PROCESS", "ID") Then
                Return True
            End If

            lst.Clear()
            lst.Add(decAppTemplate)
            If db.CheckExistIDTable(lst, "SE_APP_TEMPLATE", "ID") Then
                Return True
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            user = LogHelper.GetUserLog
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                ds = ExcelPackage.ReadExcelToDataSet(fileName, False)
            End If
            If ds Is Nothing Then
                Exit Sub
            End If

            TableMapping(ds.Tables(0))

            Dim DocXml As String = String.Empty
            Dim sw As New StringWriter()
            If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).WriteXml(sw, False)
                DocXml = sw.ToString
                If rep.IMPORT_APPROVE_SETUP_ORG(DocXml, user) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                    rgDetail.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        'dtTemp.Columns(1).ColumnName = "ORG_NAME"
        dtTemp.Columns(2).ColumnName = "ORG_ID"
        'dtTemp.Columns(3).ColumnName = "PROCESS_NAME"
        dtTemp.Columns(4).ColumnName = "PROCESS_ID"
        'dtTemp.Columns(5).ColumnName = "TEMPLATE_NAME"
        dtTemp.Columns(6).ColumnName = "TEMPLATE_ID"
        'dtTemp.Columns(7).ColumnName = "TITLE_NAME"
        dtTemp.Columns(8).ColumnName = "TITLE_ID"
        'dtTemp.Columns(9).ColumnName = "SIGN_NAME"
        dtTemp.Columns(10).ColumnName = "SIGN_ID"
        dtTemp.Columns(12).ColumnName = "LEAVEPLAN_ID"
        dtTemp.Columns(13).ColumnName = "FROM_DAY"
        dtTemp.Columns(14).ColumnName = "TO_DAY"
        dtTemp.Columns(15).ColumnName = "FROM_HOUR"
        dtTemp.Columns(16).ColumnName = "TO_HOUR"
        dtTemp.Columns(17).ColumnName = "MAIL_ACCEPTED"
        dtTemp.Columns(18).ColumnName = "MAIL_ACCEPTING"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        'Neu template empty thi xoa row do, 
        For i As Integer = dtTemp.Rows.Count - 1 To 1 Step -1 'dtTemp.Rows.Count - 1 To 1 => vi da xoa rows(0) o tren
            If IsDBNull(dtTemp.Rows(i)(5)) Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        dtTemp.AcceptChanges()
    End Sub
#End Region

    Private Sub cboApproveProcess_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboApproveProcess.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository
        Try

            Dim dbProcess = db.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A" And p.PROCESS_CODE = "RECRUITMENT" And p.ID = cboApproveProcess.SelectedValue).ToList()
            If dbProcess.Count > 0 Then
                ChkDB.Visible = True
            Else
                ChkDB.Visible = False
            End If
            ApproveProcessChange()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ApproveProcessChange()
        Dim rep As New CommonRepository
        Dim dbProcess = rep.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A").ToList()
        If cboApproveProcess.SelectedValue And (CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT) Then
            Dim query = (From p In dbProcess Where p.ID = cboApproveProcess.SelectedValue).FirstOrDefault
            If query.PROCESS_CODE = "LEAVE" Then
                lbKieuCong.Visible = True
                cboKieuCong.Visible = True
                lbFromDay.Visible = True
                rntxtFromDay.Visible = True
                lbToDay.Visible = True
                rntxtToDay.Visible = True
            Else
                lbKieuCong.Visible = False
                cboKieuCong.Visible = False
                lbFromDay.Visible = False
                rntxtFromDay.Visible = False
                lbToDay.Visible = False
                rntxtToDay.Visible = False
            End If
            If query.PROCESS_CODE = "WORKSIGN" Then
                txtFromHour.Enabled = False
            Else
                txtFromHour.Enabled = True
            End If
        Else
            lbKieuCong.Visible = True
            cboKieuCong.Visible = True
            lbFromDay.Visible = True
            rntxtFromDay.Visible = True
            lbToDay.Visible = True
            rntxtToDay.Visible = True
            txtFromHour.Enabled = True
        End If
    End Sub
End Class