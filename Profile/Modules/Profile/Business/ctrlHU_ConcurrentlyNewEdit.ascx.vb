Imports System.IO
Imports System.Reflection
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_ConcurrentlyNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindQuanLyTTPopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSign As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSign2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSignStop As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSignStop2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgGocPopup As ctrlFindOrgPopup
    Dim com As New CommonProcedureNew
    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim cons_com As New Contant_Common
#Region "Property"
    Property Is_con As Integer
        Get
            Return ViewState(Me.ID & "_Is_con")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Is_con") = value
        End Set

    End Property
    Property LinkEmpId As String
        Get
            Return ViewState(Me.ID & "_LinkEmpId")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_LinkEmpId") = value
        End Set

    End Property

    Property checkAuthor As Integer
        Get
            Return ViewState(Me.ID & "_checkAuthor")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkAuthor") = value
        End Set
    End Property

    Private Property checkStatus As String
        Get
            Return PageViewState(Me.ID & "_checkStatus")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_checkStatus") = value
        End Set

    End Property

    Private Property FileUpLoad As String
        Get
            Return PageViewState(Me.ID & "_FileUpLoad")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_FileUpLoad") = value
        End Set
    End Property

    Private Property tempPathFile As String
        Get
            Return PageViewState(Me.ID & "_tempPathFile")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_tempPathFile") = value
        End Set
    End Property

    Private Property strID As String
        Get
            Return PageViewState(Me.ID & "_strID")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_strID") = value
        End Set

    End Property

    Property APPROVAL As Integer
        Get
            Return ViewState(Me.ID & "_APPROVAL")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_APPROVAL") = value
        End Set
    End Property

    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    'Property SalaryList As List(Of EmployeeSalaryDTO)
    '    Get
    '        Return ViewState(Me.ID & "_SalaryList")
    '    End Get
    '    Set(ByVal value As List(Of EmployeeSalaryDTO))
    '        ViewState(Me.ID & "_SalaryList") = value
    '    End Set
    'End Property
    Property OrgTitle As List(Of OrgTitleDTO)
        Get
            Return ViewState(Me.ID & "_OrgTitle")
        End Get
        Set(ByVal value As List(Of OrgTitleDTO))
            ViewState(Me.ID & "_OrgTitle") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Property IsEdit As Boolean
        Get
            Return ViewState(Me.ID & "_IsEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsEdit") = value
        End Set
    End Property

    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    '2 - Sign
    '3 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property LoadNew As Boolean = True
    Property FormType As String
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FormType") = value
        End Set
    End Property

    Property dtCon As DataTable
        Get
            Return ViewState(Me.ID & "_dtCon")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtCon") = value
        End Set
    End Property

    Property dtSalary As DataTable
        Get
            Return ViewState(Me.ID & "_dtSalary")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtSalary") = value
        End Set
    End Property

    Property IsUpload As Decimal
        Get
            Return ViewState(Me.ID & "_IsUpload")
        End Get
        Set(value As Decimal)
            ViewState(Me.ID & "_IsUpload") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            If (isPopup) Then
                btnFindEmp.Visible = False
                btnOrgId.Visible = False
                'cboTitleId.Enabled = False
            End If
            If Not IsPostBack Then
                GetParams()
                tempPathFile = Server.MapPath(ConfigurationManager.AppSettings("PathFileAppendixContract"))
                FileUpLoad = ConfigurationManager.AppSettings("FileUpload")
                Refresh()
            End If
            UpdateControlState()
            If CType(CommonConfig.dicConfig("APP_SETTING_13"), Boolean) Then
                'Label1.Visible = False
                'txtSIGN.Visible = False
                'btnSIGN.Visible = False
                'Label2.Visible = False
                'txtSIGN_TITLE.Visible = False

                'Label5.Visible = False
                'txtSIGN_ID_STOP.Visible = False
                'btnSIGN_STOP.Visible = False
                'Label6.Visible = False
                'txtSIGN_TITLE_STOP.Visible = False
            End If

            If Request.Params("empID") IsNot Nothing And Request.Params("kind") IsNot Nothing Then
                MainToolBar.Items(0).Visible = True
                MainToolBar.Items(1).Visible = True
                MainToolBar.Items(0).Enabled = True
                MainToolBar.Items(1).Enabled = True

                MainToolBar.Items(2).Visible = False
                'MainToolBar.Items(3).Visible = False
                'MainToolBar.Items(4).Visible = False
            Else
                MainToolBar.Items(1).Visible = True
                MainToolBar.Items(1).Enabled = True
            End If
            If Request.Params("IsConStop") IsNot Nothing Then
                MainToolBar.Items(0).Visible = True
                MainToolBar.Items(0).Enabled = True
                EnableControlAll(False, txtEmpCode, btnFindEmp, txtEmpOrgName, btnOrgId, txtEmpName, txtEmpTitle, txtCON_NO, rdSIGN_DATE, cboStatus, txtRemark, btnSIGN,
                                    btnORG_CON, cboTITLE_CON, cboJobLevel, rdEFFECT_DATE_CON, btnUploadFile, txtBasic)
                EnableControlAll(True, btnSIGN_ID_STOP, reqSTATUS_STOP, cboSTATUS_STOP, reqEFFECT_DATE_STOP, rdEFFECT_DATE_STOP, rdSIGN_DATE_STOP, txtConNoStop, reqConNoStop)
            Else

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()

            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim dtData As New DataTable
        Try
            dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
            If dtData.Rows.Count > 0 Then
                FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
                FillRadCombobox(cboSTATUS_STOP, dtData, "NAME", "ID", True)
            End If
            dtData = rep.GetOtherList(OtherTypes.CHUCVU_CON, True)
            Dim jobLevel = rep.GetDataByProcedures(12, 0)
            If jobLevel IsNot Nothing AndAlso jobLevel.Rows.Count > 0 Then
                FillRadCombobox(cboJobLevel, jobLevel, "NAME_VN", "ID")
                FillRadCombobox(cboStaffRank, jobLevel, "NAME_VN", "ID")
            End If
            'If dtData.Rows.Count > 0 Then
            '    FillRadCombobox(cboChucVu, dtData, "NAME", "ID", True)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarCon
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete)

            'CType(MainToolBar.Items(4), RadToolBarButton).Text = Translate("Mở phê duyệt TKN")
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Select Case FormType
                Case 0
                    CurrentState = CommonMessage.STATE_NEW
                Case 1
                    CurrentState = CommonMessage.STATE_EDIT
                    If Not IsPostBack Then
                        FillData()
                        rgConcurrently.Rebind()
                        SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                        EnabledGrid(rgConcurrently, False)
                    End If
                Case 2
                    CurrentState = CommonMessage.STATE_NORMAL
                    If Not IsPostBack Then
                        FillData()
                        rgConcurrently.Rebind()
                        SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                    End If
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'DisableControls(LeftPane, False)
                    EnabledGrid(rgConcurrently, True)
                    'cboTitleId.Enabled = False
                    btnDownload.Enabled = True
                    btnUploadFile.Enabled = True
                    'btnDownload1.Enabled = True
                    'btnUploadFile1.Enabled = True
                Case CommonMessage.STATE_NEW
                    'btnSIGN.Enabled = False
                Case CommonMessage.STATE_EDIT
                    'If cboStatus.SelectedValue <> "447" Then
                    '    btnSIGN.Enabled = False
                    'End If
                    rgConcurrently.Rebind()
                    EnabledGrid(rgConcurrently, False)
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim _err As String = ""
        Dim status As Integer
        Dim id As Integer
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                'Case CommonMessage.TOOLBARITEM_CREATE
                '    CurrentState = CommonMessage.STATE_NEW
                '    FormType = 0
                '    'com.ResetControlValue(LeftPane)
                '    IDSelect = 0
                '    hidEmpID.Value = ""
                '    checkStatus = "0"
                '    rgConcurrently.Rebind()
                '    EnabledGrid(rgConcurrently, False)
                '    cboTitleId.Enabled = False
                '    cboStatus.Text = ""
                '    cboStatus.SelectedValue = cons_com.AWAITING_APPROVAL
                '    cboSTATUS_STOP.Text = ""
                '    cboSTATUS_STOP.SelectedValue = cons_com.AWAITING_APPROVAL
                'Case CommonMessage.TOOLBARITEM_EDIT
                '    If cboStatus.SelectedValue = "447" And cboSTATUS_STOP.SelectedValue = "447" Then
                '        ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                '        Exit Sub
                '    ElseIf cboStatus.SelectedValue = "447" And cboSTATUS_STOP.SelectedValue <> "447" Then
                '        Is_con = 1
                '    End If
                '    If rgConcurrently.SelectedItems.Count = 0 Then
                '        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                '        Exit Sub
                '    End If
                '    CurrentState = CommonMessage.STATE_EDIT
                '    FormType = 1
                '    EnabledGrid(rgConcurrently, False)
                '    cboTitleId.Enabled = False
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'If cboSTATUS_STOP.SelectedValue <> "" Then
                        '    If cboSTATUS_STOP.SelectedValue = 1 Then
                        '        If txtUploadFile1.Text = "" Then
                        '            ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                        '            Exit Sub
                        '        End If
                        '    End If
                        'End If
                        'If cboStatus.SelectedValue <> "" Then
                        '    If cboStatus.SelectedValue = 1 Then
                        '        If txtUploadFile.Text = "" Then
                        '            ShowMessage(Translate("bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                        '            Exit Sub
                        '        End If
                        '    End If
                        'End If
                        'If cboSTATUS_STOP.Enabled = True Then
                        '    If rdEFFECT_DATE_STOP.SelectedDate Is Nothing Then
                        '        ShowMessage(Translate("bạn phải nhập ngày hiệu lực thôi kiêm nhiệm"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If
                        If Save(strID, _err) Then
                            'FillData()
                            'CurrentState = CommonMessage.STATE_EDIT
                            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                            'FormType = 2
                            'EnabledGrid(rgConcurrently, True)
                            ''cboTitleId.Enabled = False
                            'rgConcurrently.Rebind()
                            'SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                            'IDSelect = 0
                            ''txtORG_CONName.Text = ""
                            'cboTITLE_CON.ClearValue()
                            'cboTITLE_CON.Text = ""
                            'rdEFFECT_DATE_CON.Clear()
                            ''cboChucVu.ClearValue()
                            ''cboChucVu.Text = ""
                            ''cboChucVu.Text = ""
                            'txtUploadFile.Text = ""
                            'txtBasic.Text = ""
                            'txtCON_NO.Text = ""
                            'rdSIGN_DATE.Clear()
                            'cboStatus.ClearValue()
                            'cboStatus.Text = ""
                            'txtRemark.Text = ""
                            'rdMonney.ClearValue()
                            'rdMonney.Text = ""

                            If Request.Params("empID") IsNot Nothing And Request.Params("kind") IsNot Nothing Then
                                Dim str As String = "getRadWindow().close('1');"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            Else
                                Dim str As String = "window.close(this);"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            End If

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    'CurrentState = CommonMessage.STATE_NORMAL
                    'FormType = 2
                    'FillData()
                    'EnabledGrid(rgConcurrently, True)
                    'cboTitleId.Enabled = False
                    'CType(MainToolBar.Items(1), RadToolBarButton).Attributes.Add("OnClick", "window.close();")
                    If Request.Params("empID") IsNot Nothing And Request.Params("kind") IsNot Nothing Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Dim str As String = "window.close(this);"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                        'Case CommonMessage.TOOLBARITEM_PRINT
                        '    PrintConcurrently("PCKN")
                        'Case CommonMessage.TOOLBARITEM_REJECT
                    '    PrintConcurrently("TPCKN") 
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    'If (rdSIGN_DATE_STOP.SelectedDate IsNot Nothing Or rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing Or hidSignStop.Value <> "" Or cboSTATUS_STOP.SelectedValue <> "") Then
                    '    ShowMessage("Phải xóa thông tin thôi kiêm kiệm trước khi mở phê duyệt", NotifyType.Warning)

                    '    Exit Sub
                    'End If
                    If cboStatus.SelectedValue = "" Or cboStatus.SelectedValue = "446" Then
                        ShowMessage("Trạng thái kiêm nhiệm chưa có hoặc đang chờ phê duyệt", NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboStatus.SelectedValue = "447" Then
                        If (IDSelect <> 0) Then
                            Dim store As New ProfileStoreProcedure
                            Dim dbResult = store.UPDATE_STATUS_CONCURRENTHLY(IDSelect, 2)
                            If dbResult.Rows.Count > 0 And dbResult.Rows(0)("RESULT") = 1 Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                FillData()
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            End If
                        End If
                    End If

                    'Case CommonMessage.TOOLBARITEM_UNLOCK

                    'If cboSTATUS_STOP.SelectedValue = "" Or cboSTATUS_STOP.SelectedValue.ToString = "446" Then
                    '    ShowMessage("Trạng thái thôi kiêm nhiệm chưa có hoặc đang chờ phê duyệt", NotifyType.Warning)
                    '    Exit Sub
                    'ElseIf cboSTATUS_STOP.SelectedValue = "447" Then
                    '    If (IDSelect <> 0) Then
                    '        Dim store As New ProfileStoreProcedure
                    '        Dim dbResult = store.UPDATE_STATUS_CONCURRENTHLY(IDSelect, 1)
                    '        If dbResult.Rows.Count > 0 And dbResult.Rows(0)("RESULT") = 1 Then
                    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    '            FillData()
                    '        Else
                    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                    '        End If
                    '    End If
                    'End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Update by: quangdt
    ''' Update date: 08/12/2022
    ''' Add action name "CHECK_TITLE" handling when choosing a job position that has been taken by someone
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        'Dim rep As New ProfileBusinessRepository
        log = LogHelper.GetUserLog
        Dim rep As New ProfileBusinessRepository
        Dim rep2 As New ProfileRepository
        Dim status As Integer
        Dim lstDeletes As New List(Of Decimal)
        Try
            If e.ActionName = "CHECK_TITLE" Then
                If e.ButtonID = MessageBoxButtonType.ButtonNo Then
                    ClearControlValue(cboTITLE_CON, txtDirectManagerPosition, txtDirectManager)
                    cboTITLE_CON.Focus()
                Else
                    Dim obj = rep2.GetPositionID(Decimal.Parse(cboTITLE_CON.SelectedValue))
                    If obj IsNot Nothing Then
                        If obj.LM IsNot Nothing Then
                            txtDirectManagerPosition.Text = obj.LM_NAME
                            txtDirectManager.Text = obj.EMP_LM
                        Else
                            txtDirectManagerPosition.Text = "-"
                            txtDirectManager.Text = ""
                        End If
                    Else
                        txtDirectManagerPosition.Text = "-"
                        txtDirectManager.Text = ""
                    End If
                End If
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'If com.DELETE_DATA_BY_TABLE(cons.HU_CONCURRENTLY, strID, log.Username, log.Ip + "/" + log.ComputerName) = 1 Then
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                '    FormType = 2
                '    UpdateControlState()
                '    com.ResetControlValue(LeftPane)
                '    rgConcurrently.Rebind()
                'Else
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                'End If

                For Each grid As GridDataItem In rgConcurrently.SelectedItems
                    ID = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
                    lstDeletes.Add(ID)
                    status = If(grid.GetDataKeyValue("STATUS").ToString() = "", 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                    If status = 1 Then
                        ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                        Exit Sub
                    End If
                    strID &= IIf(strID = vbNullString, ID, "," & ID)
                Next
                If rep.DELETE_CONCURRENTLY(lstDeletes) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    ClearControlValue(cboTITLE_CON, cboJobLevel, txtEmpTitle, txtChucDanhQLTT, txtEmpCode, txtEmpName, txtEmpOrgName, txtEmpOrgName, txtQuanLyTrucTiep,
                                      txtQuanLyTrucTiep, txtRemark, txtCON_NO, txtSIGN, txtSIGN_TITLE, txtSIGN_TITLE2, txtSIGN2,
                                      txtUploadFile, txtUploadFile_Link, cboStatus, rdEFFECT_DATE_CON, rdSIGN_DATE)
                    rgConcurrently.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub cbTitle_ORG_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTitle_ORG.SelectedIndexChanged
    '    If cbTitle_ORG.SelectedValue <> 2 Then
    '        cbTITILE_ORG_TEMP.SelectedValue = cbTitle_ORG.SelectedValue
    '        cbTITILE_ORG_TEMP.Text = cbTitle_ORG.Text
    '    End If
    'End Sub
    'Private Sub cboChucVu_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboChucVu.SelectedIndexChanged
    '    Dim dt As DataTable
    '    If cboChucVu.SelectedValue <> "" Then
    '        dt = psp.GET_MONEY_BY_CHUCVU(cboChucVu.SelectedValue)
    '        rdMonney.Text = dt(0)("REMARK").ToString()
    '    End If
    'End Sub

    'Private Sub cboTITLE_CON_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTITLE_CON.SelectedIndexChanged
    '    Dim rep As New ProfileRepository
    '    Try
    '        If cboTITLE_CON.SelectedValue <> "" Then
    '            Dim obj = rep.GetPositionID(Decimal.Parse(cboTITLE_CON.SelectedValue))
    '            If obj IsNot Nothing Then
    '                If obj.LM IsNot Nothing Then
    '                    txtDirectManagerPosition.Text = obj.LM_NAME
    '                    txtDirectManager.Text = obj.EMP_LM
    '                Else
    '                    txtDirectManagerPosition.Text = "-"
    '                    txtDirectManager.Text = ""
    '                End If
    '            Else
    '                txtDirectManagerPosition.Text = "-"
    '                txtDirectManager.Text = ""
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmp.Click,
                                btnORG_CON.Click,
                                btnSIGN.Click,
                                btnOrgId.Click,
                                btnSIGN2.Click,
                                btnSIGN_ID_STOP.Click,
                                btnQuanLyTrucTiep.Click
        Try
            Select Case sender.ID
                Case btnFindEmp.ID
                    isLoadPopup = 1
                Case btnORG_CON.ID
                    isLoadPopup = 2
                Case btnSIGN.ID
                    isLoadPopup = 3
                Case btnSIGN_ID_STOP.ID
                    isLoadPopup = 4
                Case btnOrgId.ID
                    isLoadPopup = 5
                Case btnSIGN2.ID
                    isLoadPopup = 6
                'Case btnSIGN_STOP2.ID
                '    isLoadPopup = 7
                Case btnQuanLyTrucTiep.ID
                    isLoadPopup = 8
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmp.ID
                    If Hid_IsEnter.Value.ToUpper <> "ISENTER" Then
                        ctrlFindEmployeePopup.Show()
                    End If
                    Hid_IsEnter.Value = Nothing
                Case btnORG_CON.ID
                    ctrlFindOrgPopup.Show()
                Case btnSIGN.ID
                    ctrlFindEmployeeSign.Show()
                Case btnSIGN_ID_STOP.ID
                    ctrlFindEmployeeSignStop.Show()
                Case btnSIGN2.ID
                    ctrlFindEmployeeSign2.Show()
                'Case btnSIGN_STOP2.ID
                '    ctrlFindEmployeeSignStop2.Show()
                Case btnOrgId.ID
                    ctrlFindOrgGocPopup.Show()
                Case btnQuanLyTrucTiep.ID
                    ctrlFindQuanLyTTPopup.Show_LoadAllOrganization()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                    ctrlFindEmployeeSign.CancelClicked,
                                    ctrlFindOrgPopup.CancelClicked,
                                    ctrlFindEmployeeSignStop.CancelClicked,
                                    ctrlFindOrgGocPopup.CancelClicked,
                                    ctrlFindEmployeeSign2.CancelClicked,
                                    ctrlFindQuanLyTTPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmpID.Value = item.ID
                EmpOrgNameToolTip.Text = DrawTreeByString(item.ORG_DESC)
                txtEmpCode.Text = item.EMPLOYEE_CODE

                hidEmpCode.Value = item.EMPLOYEE_CODE
                hidOrgName.Value = item.ORG_NAME_2

                txtEmpName.Text = item.FULLNAME_VN
                txtEmpOrgName.Text = item.ORG_NAME
                hidOrgId.Value = item.ORG_ID
                txtEmpTitle.Text = item.TITLE_NAME
                If item.STAFF_RANK_ID IsNot Nothing Then
                    cboStaffRank.SelectedValue = item.STAFF_RANK_ID
                End If
                'Dim dtData As DataTable = rep.GET_TITLE_ORG(hidOrgId.Value)
                'If dtData.Rows.Count > 0 Then
                '    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
                '    cboTitleId.Text = String.Empty
                'End If
                'cboTitleId.SelectedValue = item.TITLE_ID
                rgConcurrently.Rebind()
                rep.Dispose()
            End If
            isLoadPopup = 0
            AutoCreate_DecisionNo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim dtData As New DataTable
        Dim rep As New ProfileBusinessRepository
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgCOn.Value = e.CurrentValue
                txtORG_CONName.Text = orgItem.NAME_VN
                ORG_CONNameToolTip.Text = DrawTreeByString(orgItem.Description_Path)
                ' Lay chuc danh theo phong ban
                'dtData = rep.GET_TITLE_ORG(e.CurrentValue)
                'If dtData.Rows.Count > 0 Then
                '    FillRadCombobox(cboTITLE_CON, dtData, "NAME_VN", "ID", True)
                '    cboTITLE_CON.Text = String.Empty
                'End If
                Dim repa As New ProfileRepository
                Dim dtDataTitle = repa.GetDataByProcedures(9, e.CurrentValue, hidEmpID.Value, Common.Common.SystemLanguage.Name)
                FillRadCombobox(cboTITLE_CON, dtDataTitle, "NAME", "ID", False)
            End If
            rep.Dispose()
            Session.Remove("CallAllOrg")
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlOrgGocPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgGocPopup.OrganizationSelected
        Dim dtData As New DataTable
        Dim rep As New ProfileBusinessRepository
        Try
            Dim orgItem = ctrlFindOrgGocPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgId.Value = e.CurrentValue
                txtEmpOrgName.Text = orgItem.NAME_VN
                EmpOrgNameToolTip.Text = DrawTreeByString(orgItem.Description_Path)
                ' Lay chuc danh theo phong ban
                dtData = rep.GET_TITLE_ORG(e.CurrentValue)
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboTITLE_CON, dtData, "NAME_VN", "ID", True)
                    cboTITLE_CON.Text = String.Empty
                End If
            End If
            rep.Dispose()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlFindEmployeeSign_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSign.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSign.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID
                txtSIGN.Text = item.EMPLOYEE_CODE & " - " & item.FULLNAME_VN
                txtSIGN_TITLE.Text = item.TITLE_CODE & " - " & item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeSign_EmployeeSelected2(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSign2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSign2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign2.Value = item.ID
                txtSIGN2.Text = item.FULLNAME_VN
                txtSIGN_TITLE2.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeSignStop_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSignStop.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSignStop.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSignStop.Value = item.ID
                txtSIGN_ID_STOP.Text = item.EMPLOYEE_CODE & " - " & item.FULLNAME_VN
                txtSIGN_TITLE_STOP.Text = item.TITLE_CODE & " - " & item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeeSignStop_EmployeeSelected2(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSignStop2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSignStop2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSignStop2.Value = item.ID
                'txtSIGN_ID_STOP2.Text = item.FULLNAME_VN
                'txtSIGN_TITLE_STOP2.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindQuanLyTTPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindQuanLyTTPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindQuanLyTTPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hdQuanLyTrucTiep.Value = item.ID
                txtQuanLyTrucTiep.Text = item.FULLNAME_VN
                txtChucDanhQLTT.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgConcurrently_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgConcurrently.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            If hidEmpID.Value <> "" Then
                dtCon = rep.GET_CONCURRENTLY_BY_EMP(hidEmpID.Value)
                DesignGrid(dtCon)
                If hidEmpID.Value <> "" Then
                    rgConcurrently.DataSource = dtCon
                Else
                    rgConcurrently.DataSource = Nothing
                End If
            Else
                rgConcurrently.DataSource = Nothing
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgConcurrently_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgConcurrently.SelectedIndexChanged
        Dim item As GridDataItem
        Try
            item = CType(rgConcurrently.SelectedItems(0), GridDataItem)
            IDSelect = item.GetDataKeyValue("ID").ToString
            hidID.Value = IDSelect
            FillData()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ' Cảnh báo nếu trạng thái phê quyệt thì bắt buộc nhập số quyết định
    'Private Sub curDecisionNo_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles curDecisionNo.ServerValidate
    '    Try

    '        If cboStatus.SelectedValue <> "" Then
    '            If cboStatus.SelectedValue = 1 AndAlso txtCON_NO.Text.Trim = "" Then
    '                args.IsValid = False
    '            Else
    '                args.IsValid = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ' Cảnh báo nếu trạng thái phê quyệt thì bắt buộc nhập ngày hiệu lực
    'Private Sub curStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles curStartDate.ServerValidate
    '    Try

    '        If cboStatus.SelectedValue <> "" Then
    '            If cboStatus.SelectedValue = 1 AndAlso rdSIGN_DATE.SelectedDate Is Nothing Then
    '                args.IsValid = False
    '            Else
    '                args.IsValid = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    'Private Sub CusToDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
    '    Try
    '        'Nếu ngày nghỉ bé hơn ngày vào thì cảnh báo
    '        If rdEFFECT_DATE_CON.SelectedDate IsNot Nothing AndAlso rdEXPIRE_DATE_CON.SelectedDate IsNot Nothing Then
    '            If rdEFFECT_DATE_CON.SelectedDate > rdEXPIRE_DATE_CON.SelectedDate Then
    '                args.IsValid = False
    '            Else
    '                args.IsValid = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub CusDecisionNoSame_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CusDecisionNoSame.ServerValidate
    'Try
    '    If txtCON_NO.Text = "" Then
    '        args.IsValid = True
    '        Exit Sub
    '    End If
    '    Select Case CurrentState
    '        Case STATE_NEW
    '            Using rep As New ProfileBusinessRepository
    '                args.IsValid = psp.CHECK_CON_CODE(IDSelect, txtCON_NO.Text.Trim, 0, 0, 1)
    '            End Using
    '        Case STATE_EDIT
    '            Using rep As New ProfileBusinessRepository
    '                args.IsValid = psp.CHECK_CON_CODE(IDSelect, txtCON_NO.Text.Trim, hidEmpID.Value, 1, 1)
    '            End Using
    '        Case Else
    '            args.IsValid = True
    '    End Select
    'Catch ex As Exception
    '    DisplayException(Me.ViewName, Me.ID, ex)
    'End Try
    'End Sub
    Private Sub txtEmpCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmpCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Dim rep_PROFILE As New ProfileBusinessRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                'Hid_IsEnter.Value = Nothing
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
                        Dim empID = EmployeeList(0).EMPLOYEE_ID
                        Dim lst As New List(Of Decimal) From {empID}
                        Dim emp_list = rep.GetEmployeeToPopupFind_EmployeeID(lst)
                        Dim item = emp_list(0)
                        hidEmpID.Value = item.ID
                        EmpOrgNameToolTip.Text = DrawTreeByString(item.ORG_DESC)
                        txtEmpCode.Text = item.EMPLOYEE_CODE
                        If item.STAFF_RANK_ID IsNot Nothing Then
                            cboStaffRank.SelectedValue = item.STAFF_RANK_ID
                        End If
                        hidEmpCode.Value = item.EMPLOYEE_CODE
                        hidOrgName.Value = item.ORG_NAME_2
                        txtEmpName.Text = item.FULLNAME_VN
                        txtEmpOrgName.Text = item.ORG_NAME
                        hidOrgId.Value = item.ORG_ID
                        txtEmpTitle.Text = item.TITLE_NAME
                        'Dim dtData As DataTable = rep_PROFILE.GET_TITLE_ORG(hidOrgId.Value)
                        'If dtData.Rows.Count > 0 Then
                        '    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
                        '    cboTitleId.Text = String.Empty
                        'End If
                        'cboTitleId.SelectedValue = item.TITLE_ID
                        rep.Dispose()
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmpCode.Text
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            hidEmpID.Value = Nothing
            EmpOrgNameToolTip.Text = ""
            'txtEmpCode.Text = ""

            hidEmpCode.Value = Nothing
            hidOrgName.Value = Nothing

            txtEmpName.Text = ""
            txtEmpOrgName.Text = ""
            hidOrgId.Value = ""
            txtEmpTitle.Text = ""
            'ClearControlValue(cboTitleId)
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Create by: quangdt
    ''' Create date: 08/12/2022
    ''' handling when choosing a job position that has been taken by someone
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTITLE_CON.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If cboTITLE_CON.SelectedValue <> "" Then
                Dim a = cboTITLE_CON.SelectedValue
                Dim rep As New ProfileRepository
                Dim dtDataTitle As DataTable
                dtDataTitle = rep.GetDataByProcedures(9, hidOrgCOn.Value, hidEmpID.Value.ToString, Common.Common.SystemLanguage.Name)
                Dim dv As New DataView(dtDataTitle, "ID = " + cboTITLE_CON.SelectedValue.ToString + " AND (MASTER_NAME <> '' OR INTERIM_NAME <> '')", "", DataViewRowState.CurrentRows)
                Dim dTable As DataTable
                dTable = dv.ToTable
                If dTable.Rows.Count > 0 Then
                    ctrlMessageBox.MessageText = Translate("Vị trí này đã có người ngồi. Bạn có muốn chọn không.")
                    ctrlMessageBox.ActionName = "CHECK_TITLE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Else
                    Dim obj = rep.GetPositionID(Decimal.Parse(cboTITLE_CON.SelectedValue))
                    If obj IsNot Nothing Then
                        If obj.LM IsNot Nothing Then
                            txtDirectManagerPosition.Text = obj.LM_NAME
                            txtDirectManager.Text = obj.EMP_LM
                        Else
                            txtDirectManagerPosition.Text = "-"
                            txtDirectManager.Text = ""
                        End If
                    Else
                        txtDirectManagerPosition.Text = "-"
                        txtDirectManager.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Try


            If ctrlFindEmployeePopup IsNot Nothing AndAlso phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If
            If phFindOrgCon IsNot Nothing AndAlso phFindOrgCon.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrgCon.Controls.Remove(ctrlFindOrgPopup)
            End If
            If phFindEmployeeGoc IsNot Nothing AndAlso phFindEmployeeGoc.Controls.Contains(ctrlFindOrgGocPopup) Then
                phFindOrgCon.Controls.Remove(ctrlFindOrgGocPopup)
            End If
            If ctrlFindEmployeeSign IsNot Nothing AndAlso phFindSign.Controls.Contains(ctrlFindEmployeeSign) Then
                phFindSign.Controls.Remove(ctrlFindEmployeeSign)
            End If
            If ctrlFindEmployeeSignStop IsNot Nothing AndAlso phFindSignStop.Controls.Contains(ctrlFindEmployeeSignStop) Then
                phFindSignStop.Controls.Remove(ctrlFindEmployeeSignStop)
            End If
            If ctrlFindEmployeeSign2 IsNot Nothing AndAlso phFindSign2.Controls.Contains(ctrlFindEmployeeSign) Then
                phFindSign2.Controls.Remove(ctrlFindEmployeeSign)
            End If
            If ctrlFindEmployeeSignStop2 IsNot Nothing AndAlso phFindSignStop2.Controls.Contains(ctrlFindEmployeeSignStop2) Then
                phFindSignStop2.Controls.Remove(ctrlFindEmployeeSignStop2)
            End If

            If ctrlFindQuanLyTTPopup IsNot Nothing AndAlso phFindQuanLyTT.Controls.Contains(ctrlFindQuanLyTTPopup) Then
                phFindQuanLyTT.Controls.Remove(ctrlFindQuanLyTTPopup)
            End If

            ' Tạo các popup
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ' Popup tìm nhân viên
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.IsHideTerminate = False
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                    End If
                Case 2
                    ' Popup phòng ban
                    'HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgCon.Controls.Add(ctrlFindOrgPopup)
                Case 3
                    If Not phFindSign.Controls.Contains(ctrlFindEmployeeSign) Then
                        'Popup người ký
                        ctrlFindEmployeeSign = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindEmployeeSign)
                        ctrlFindEmployeeSign.IsHideTerminate = False
                        ctrlFindEmployeeSign.MultiSelect = False
                        ctrlFindEmployeeSign.LoadAllOrganization = False
                        ctrlFindEmployeeSign.FunctionName = "ctrlHU_ConcurrentlyNewEdit"
                        ctrlFindEmployeeSign.EmployeeOrg = If(hidOrgId.Value <> "", CDec(hidOrgId.Value), 0)
                        ctrlFindEmployeeSign.EffectDate = If(rdEFFECT_DATE_CON.SelectedDate IsNot Nothing, CDbl(rdEFFECT_DATE_CON.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    End If


                Case 4
                    If Not phFindSignStop.Controls.Contains(ctrlFindEmployeeSignStop) Then
                        'Popup người ký thôi kiêm nhiệm
                        ctrlFindEmployeeSignStop = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSignStop.Controls.Add(ctrlFindEmployeeSignStop)
                        ctrlFindEmployeeSignStop.IsHideTerminate = False
                        ctrlFindEmployeeSignStop.MultiSelect = False
                        ctrlFindEmployeeSignStop.LoadAllOrganization = False
                        ctrlFindEmployeeSignStop.FunctionName = "ctrlHU_ConcurrentlyNewEdit"
                        ctrlFindEmployeeSignStop.EmployeeOrg = If(hidOrgId.Value <> "", CDec(hidOrgId.Value), 0)
                        ctrlFindEmployeeSignStop.EffectDate = If(rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing, CDbl(rdEFFECT_DATE_STOP.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    End If

                Case 5
                    ' Popup phòng ban
                    ctrlFindOrgGocPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgCon.Controls.Add(ctrlFindOrgGocPopup)
                Case 6
                    If Not phFindSign2.Controls.Contains(ctrlFindEmployeeSign) Then
                        'Popup người ký 2
                        ctrlFindEmployeeSign2 = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign2.Controls.Add(ctrlFindEmployeeSign2)
                        ctrlFindEmployeeSign2.IsHideTerminate = True
                        ctrlFindEmployeeSign2.MultiSelect = False
                        ctrlFindEmployeeSign2.LoadAllOrganization = False
                        ctrlFindEmployeeSign2.IsOnlyWorkingWithoutTer =
                        ctrlFindEmployeeSign2.FunctionName = "ctrlHU_ConcurrentlyNewEdit"
                        ctrlFindEmployeeSign2.EmployeeOrg = If(hidOrgId.Value <> "", CDec(hidOrgId.Value), 0)
                        ctrlFindEmployeeSign2.EffectDate = If(rdEFFECT_DATE_CON.SelectedDate IsNot Nothing, CDbl(rdEFFECT_DATE_CON.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    End If
                Case 7
                    If Not phFindSignStop2.Controls.Contains(ctrlFindEmployeeSignStop2) Then
                        'Popup người ký thôi kiêm nhiệm
                        ctrlFindEmployeeSignStop2 = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSignStop.Controls.Add(ctrlFindEmployeeSignStop2)
                        ctrlFindEmployeeSignStop2.IsHideTerminate = True
                        ctrlFindEmployeeSignStop2.MultiSelect = False
                        ctrlFindEmployeeSignStop2.LoadAllOrganization = False
                        ctrlFindEmployeeSignStop2.IsOnlyWorkingWithoutTer = True
                        ctrlFindEmployeeSignStop2.FunctionName = "ctrlHU_ConcurrentlyNewEdit"
                        ctrlFindEmployeeSignStop2.EmployeeOrg = If(hidOrgId.Value <> "", CDec(hidOrgId.Value), 0)
                        ctrlFindEmployeeSignStop2.EffectDate = If(rdEFFECT_DATE_CON.SelectedDate IsNot Nothing, CDbl(rdEFFECT_DATE_CON.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    End If
                Case 8
                    If Not phFindQuanLyTT.Controls.Contains(ctrlFindQuanLyTTPopup) Then
                        ctrlFindQuanLyTTPopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindQuanLyTT.Controls.Add(ctrlFindQuanLyTTPopup)
                        ctrlFindQuanLyTTPopup.IsHideTerminate = True
                        ctrlFindQuanLyTTPopup.MultiSelect = False
                        ctrlFindQuanLyTTPopup.LoadAllOrganization = False
                        ctrlFindQuanLyTTPopup.IsOnlyWorkingWithoutTer = True
                    End If
            End Select
            ' Nếu trạng thái khai báo thôi kiêm nhiệm
            If Is_con = 1 Then
                txtEmpCode.Enabled = False
                btnFindEmp.Enabled = False
                btnORG_CON.Enabled = False
                btnOrgId.Enabled = False
                cboTITLE_CON.Enabled = False
                cboJobLevel.Enabled = False
                rntxtALLOW_MONEY.ReadOnly = True
                rdEFFECT_DATE_CON.Enabled = False
                'rdEXPIRE_DATE_CON.Enabled = False
                txtCON_NO.ReadOnly = True
                rdSIGN_DATE.Enabled = False
                cboStatus.Enabled = False
                btnSIGN.Enabled = False
                btnSIGN2.Enabled = False
                txtRemark.ReadOnly = True
                txtConNoStop.ReadOnly = False
                chkALLOW.Enabled = False
                chkIsChuyen.Enabled = False
                btnUploadFile.Enabled = False
            End If
            'ChangeToolbarState()
            Me.Send(CurrentState)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ' Fill dữ liệu working lên control
    Private Sub FillData()
        Using rep As New ProfileBusinessRepository
            Try
                If IDSelect <> 0 Then
                    Dim dr = rep.GET_CONCURRENTLY_BY_ID(IDSelect)
                    If dr.Rows.Count > 0 Then
                        EmpOrgNameToolTip.Text = Utilities.DrawTreeByString(dr(0)("Description_Path").ToString)
                        ORG_CONNameToolTip.Text = Utilities.DrawTreeByString(dr(0)("OrgCon_Description_Path").ToString)
                        hidEmpID.Value = dr(0)("EMPLOYEE_ID").ToString
                        txtEmpCode.Text = dr(0)("EMPLOYEE_CODE").ToString
                        txtEmpName.Text = dr(0)("FULLNAME_VN").ToString
                        'cboTitleId.SelectedValue = dr(0)("TITLE_id").ToString
                        txtEmpTitle.Text = dr(0)("TITLE_NAME").ToString
                        txtEmpOrgName.Text = dr(0)("ORG_NAME").ToString
                        hidOrgId.Value = dr(0)("ORG_ID").ToString
                        hidOrgCOn.Value = dr(0)("ORG_CON").ToString
                        If Not IsDBNull(dr(0)("STAFF_RANK_ID")) Then
                            cboJobLevel.SelectedValue = dr(0)("STAFF_RANK_ID").ToString
                        End If
                        If Not IsDBNull(dr(0)("STAFF_RANK_ID_EMP")) Then
                            cboStaffRank.SelectedValue = dr(0)("STAFF_RANK_ID_EMP").ToString
                        End If
                        If hidOrgCOn.Value <> "" Then
                            Dim repa As New ProfileRepository
                            Dim dtDataTitle = repa.GetDataByProcedures(9, hidOrgCOn.Value, hidEmpID.Value, Common.Common.SystemLanguage.Name)
                            FillRadCombobox(cboTITLE_CON, dtDataTitle, "NAME", "ID", False)
                            cboTITLE_CON.SelectedValue = dr(0)("TITLE_CON").ToString
                            If cboTITLE_CON.SelectedValue <> "" Then
                                Dim repo As New ProfileRepository
                                Dim obj = repo.GetPositionID(Decimal.Parse(cboTITLE_CON.SelectedValue))
                                If obj IsNot Nothing Then
                                    If obj.LM IsNot Nothing Then
                                        txtDirectManagerPosition.Text = obj.LM_NAME
                                        txtDirectManager.Text = obj.EMP_LM
                                    Else
                                        txtDirectManagerPosition.Text = "-"
                                        txtDirectManager.Text = ""
                                    End If
                                Else
                                    txtDirectManagerPosition.Text = "-"
                                    txtDirectManager.Text = ""
                                End If
                            End If
                        End If
                        txtORG_CONName.Text = dr(0)("ORG_CON_NAME").ToString
                        If dr(0)("EFFECT_DATE_CON").ToString <> "" Then
                            rdEFFECT_DATE_CON.SelectedDate = dr(0)("EFFECT_DATE_CON").ToString
                        End If
                        'If dr(0)("EXPIRE_DATE_CON").ToString <> "" Then
                        '    rdEXPIRE_DATE_CON.SelectedDate = dr(0)("EXPIRE_DATE_CON").ToString
                        'End If
                        rntxtALLOW_MONEY.Value = dr(0)("ALLOW_MONEY").ToString
                        txtCON_NO.Text = dr(0)("CON_NO").ToString
                        cboStatus.SelectedValue = dr(0)("STATUS").ToString
                        cboStatus.Text = dr(0)("STATUS_NAME").ToString

                        If dr(0)("SIGN_DATE").ToString <> "" Then
                            rdSIGN_DATE.SelectedDate = dr(0)("SIGN_DATE").ToString
                        End If

                        hidSign.Value = dr(0)("SIGN_ID").ToString
                        txtSIGN.Text = dr(0)("SIGN_NAME").ToString
                        txtSIGN_TITLE.Text = dr(0)("SIGN_TITLE_NAME").ToString

                        hidSign2.Value = dr(0)("SIGN_ID_2").ToString
                        txtSIGN2.Text = dr(0)("SIGN_NAME2").ToString
                        txtSIGN_TITLE2.Text = dr(0)("SIGN_TITLE_NAME2").ToString

                        txtRemark.Text = dr(0)("REMARK").ToString
                        txtConNoStop.Text = dr(0)("CON_NO_STOP").ToString
                        If txtConNoStop.Text <> "" Then
                            IsEdit = True
                        End If
                        If dr(0)("SIGN_DATE_STOP").ToString <> "" Then
                            rdSIGN_DATE_STOP.SelectedDate = dr(0)("SIGN_DATE_STOP").ToString
                        End If
                        If dr(0)("EXPIRE_DATE_CON").ToString <> "" Then
                            rdEFFECT_DATE_STOP.SelectedDate = dr(0)("EXPIRE_DATE_CON").ToString
                        End If

                        cboSTATUS_STOP.SelectedValue = dr(0)("STATUS_STOP").ToString
                        'cboSTATUS_STOP.Text = dr(0)("STATUS_STOP_NAME").ToString
                        hidSignStop.Value = dr(0)("SIGN_ID_STOP").ToString
                        txtSIGN_ID_STOP.Text = dr(0)("SIGN_NAME_STOP").ToString
                        txtSIGN_TITLE_STOP.Text = dr(0)("SIGN_TITLE_NAME_STOP").ToString

                        hidSignStop2.Value = dr(0)("SIGN_ID_STOP_2").ToString
                        'txtSIGN_ID_STOP2.Text = dr(0)("SIGN_NAME_STOP2").ToString
                        'txtSIGN_TITLE_STOP2.Text = dr(0)("SIGN_TITLE_NAME_STOP2").ToString


                        'txtREMARK_STOP.Text = dr(0)("REMARK_STOP").ToString

                        txtUploadFile.Text = dr(0)("FILE_BYTE").ToString
                        txtUploadFile_Link.Text = dr(0)("ATTACH_FOLDER_BYTE").ToString
                        'txtUploadFile1.Text = dr(0)("FILE_BYTE1").ToString
                        'txtUploadFile1_Link.Text = dr(0)("ATTACH_FOLDER_BYTE1").ToString
                        'lstFile = dr(0)("FILE_BYTE").ToString
                        'LoadListFileUpload(dr(0)("FILE_BYTE").ToString)
                        'lstFile1 = dr(0)("FILE_BYTE1").ToString
                        'LoadListFileUpload1(dr(0)("FILE_BYTE1").ToString)

                        If dr(0)("IS_ALLOW").ToString <> "0" Then
                            chkALLOW.Checked = True
                        Else
                            chkALLOW.Checked = False
                        End If
                        If dr(0)("IS_CHUYEN").ToString <> "0" Then
                            chkIsChuyen.Checked = True
                        Else
                            chkIsChuyen.Checked = False
                        End If
                        If IsNumeric(cboStatus.SelectedValue) Then
                            Dim Enable As Boolean = IIf(Integer.Parse(cboStatus.SelectedValue) = 447, False, True)
                            SetStatusControl(Enable)
                        End If
                        'cboChucVu.SelectedValue = dr(0)("CHUCVU_CON").ToString
                        'cboChucVu.Text = dr(0)("CHUCVU_CON_NAME").ToString
                        rdMonney.Text = dr(0)("MONEY").ToString
                        txtBasic.Text = dr(0)("BASIC1").ToString
                        'Cấp quản lý trực tiếp
                        hdQuanLyTrucTiep.Value = IIf(dr(0).IsNull("CAP_QUAN_LY_TT"), String.Empty, dr(0)("CAP_QUAN_LY_TT").ToString())
                        txtQuanLyTrucTiep.Text = IIf(dr(0).IsNull("CAP_QUAN_LY"), String.Empty, dr(0)("CAP_QUAN_LY").ToString())
                        txtChucDanhQLTT.Text = IIf(dr(0).IsNull("CHUC_DANH_QUAN_LY"), String.Empty, dr(0)("CHUC_DANH_QUAN_LY").ToString())
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)

        SetStatusToolBar(sTrangThai)
    End Sub

    Protected Sub SetStatusToolBar(ByVal sTrangThai As Boolean)
        Try
            MainToolBar.Items(1).Visible = True
            MainToolBar.Items(1).Enabled = True

            MainToolBar.Items(0).Visible = sTrangThai
            MainToolBar.Items(0).Enabled = sTrangThai
            'Select Case CurrentState
            '    Case STATE_NORMAL
            '        Me.MainToolBar.Items(0).Enabled = True 'Add
            '        Me.MainToolBar.Items(1).Enabled = True 'Edit
            '        Me.MainToolBar.Items(7).Enabled = True 'Delete
            '        Me.MainToolBar.Items(5).Enabled = True 'Thôi kiêm nhiệm
            '        Me.MainToolBar.Items(3).Enabled = False 'Save
            '        Me.MainToolBar.Items(4).Enabled = True  'Cancel
            '    Case STATE_NEW, STATE_EDIT
            '        Me.MainToolBar.Items(0).Enabled = False 'Add
            '        Me.MainToolBar.Items(1).Enabled = False 'Edit
            '        Me.MainToolBar.Items(7).Enabled = False 'Delete
            '        Me.MainToolBar.Items(5).Enabled = False 'Thôi kiêm nhiệm
            '        Me.MainToolBar.Items(3).Enabled = True 'Save
            '        Me.MainToolBar.Items(4).Enabled = True 'Cancel
            'End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub loadDatasource(ByVal AttachID As Decimal, ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                If AttachID = 0 Then
                    txtUploadFile_Link.Text = Down_File
                    txtUploadFile.Text = strUpload
                    'Else
                    '    txtUploadFile1_Link.Text = Down_File
                    '    txtUploadFile1.Text = strUpload
                End If
            Else
                strUpload = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Dim ID As String = ""
        Try

            If Request.Params("empID") IsNot Nothing Then
                hidEmpID.Value = Request.Params("empID")
                If Request.Params("kind") IsNot Nothing Then
                    Using rep As New ProfileBusinessRepository
                        Dim item = rep.GetEmployeeByEmployeeID(hidEmpID.Value)
                        EmpOrgNameToolTip.Text = DrawTreeByString(item.ORG_DESC)
                        txtEmpCode.Text = item.EMPLOYEE_CODE

                        hidEmpCode.Value = item.EMPLOYEE_CODE
                        hidOrgName.Value = item.ORG_NAME_2
                        txtEmpName.Text = item.FULLNAME_VN
                        txtEmpOrgName.Text = item.ORG_NAME
                        hidOrgId.Value = item.ORG_ID
                        txtEmpTitle.Text = item.TITLE_NAME_VN
                        'Dim dtData As DataTable = rep.GET_TITLE_ORG(hidOrgId.Value)
                        'If dtData.Rows.Count > 0 Then
                        '    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
                        '    cboTitleId.Text = String.Empty
                        'End If
                        'cboTitleId.SelectedValue = item.TITLE_ID
                        EnableControlAll(False, txtEmpCode)
                        'btnFindEmp.Visible = False
                        rgConcurrently.Rebind()

                        rep.Dispose()
                    End Using

                End If
            End If
            If Request.Params("IDSelect") IsNot Nothing Then
                IDSelect = Request.Params("IDSelect")
                hidID.Value = Request.Params("IDSelect")
            End If
            If Request.Params("APPROVAL") IsNot Nothing Then
                APPROVAL = Request.Params("APPROVAL")
            End If
            If Request.Params("Check") IsNot Nothing Then
                checkStatus = Request.Params("Check")
            End If
            If Request.Params("LinkEmpId") IsNot Nothing Then
                LinkEmpId = Request.Params("LinkEmpId")
            End If
            If LinkEmpId <> "" Then
                LoadEmployeeLink()
            End If
            If Request.Params("Is_con") IsNot Nothing Then
                Is_con = Request.Params("Is_con")
            End If

            'Select Case CurrentState
            '    Case CommonMessage.STATE_NEW
            '        Refresh("NewView")
            '    Case CommonMessage.STATE_EDIT
            '        Refresh("UpdateView")
            '    Case Else
            '        Refresh("NormalView")
            'End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgConcurrently.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgConcurrently.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
            End If
        Next
    End Sub

    Private Function Save(ByRef strID As Decimal, Optional ByRef _err As String = "") As Boolean
        Dim rep As New ProfileBusinessRepository
        Dim result As Integer
        Dim CON As New Temp_ConcurrentlyDTO
        log = LogHelper.GetUserLog
        If hidID.Value <> "" Then
            CON.ID = hidID.Value
        End If
        CON.EMPLOYEE_ID = hidEmpID.Value
        If hidOrgCOn.Value <> "" Then
            CON.ORG_CON = hidOrgCOn.Value
        End If
        If cboTITLE_CON.SelectedValue <> "" Then
            CON.TITLE_CON = cboTITLE_CON.SelectedValue
        End If

        If hidOrgId.Value <> "" Then
            CON.ORG_ID = hidOrgId.Value
        End If
        If cboJobLevel.SelectedValue <> "" Then
            CON.STAFF_RANK_ID = cboJobLevel.SelectedValue
        End If
        'If cboTitleId.SelectedValue <> "" Then
        '    CON.TITLE_ID = cboTitleId.SelectedValue
        'End If

        CON.EFFECT_DATE_CON = rdEFFECT_DATE_CON.SelectedDate
        'CON.EXPIRE_DATE_CON = rdEXPIRE_DATE_CON.SelectedDate
        If rntxtALLOW_MONEY.Text <> "" Then
            CON.ALLOW_MONEY = rntxtALLOW_MONEY.Value
        End If
        CON.CON_NO = txtCON_NO.Text
        CON.STATUS = cboStatus.SelectedValue
        CON.SIGN_DATE = rdSIGN_DATE.SelectedDate
        If hidSign.Value <> "" Then
            CON.SIGN_ID = hidSign.Value
        End If
        CON.SIGN_TITLE_NAME = txtSIGN_TITLE.Text.Trim
        If hidSign2.Value <> "" Then
            CON.SIGN_ID_2 = hidSign2.Value
        End If
        CON.REMARK = txtRemark.Text
        CON.CON_NO_STOP = txtConNoStop.Text
        If rdSIGN_DATE_STOP.SelectedDate IsNot Nothing Then
            CON.SIGN_DATE_STOP = rdSIGN_DATE_STOP.SelectedDate
        End If
        If rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing Then
            CON.EXPIRE_DATE_CON = rdEFFECT_DATE_STOP.SelectedDate
        End If
        If cboSTATUS_STOP.SelectedValue <> "" Then
            CON.STATUS_STOP = cboSTATUS_STOP.SelectedValue
        End If
        If hidSignStop.Value <> "" Then
            CON.SIGN_ID_STOP = hidSignStop.Value
        End If
        CON.SIGN_TITLE_NAME_STOP = txtSIGN_TITLE_STOP.Text.Trim
        If hidSignStop2.Value <> "" Then
            CON.SIGN_ID_STOP_2 = hidSignStop2.Value
        End If

        'CON.REMARK_STOP = txtREMARK_STOP.Text

        CON.CREATED_BY = log.Username
        CON.CREATED_LOG = log.Ip + "/" + log.ComputerName

        If chkALLOW.Checked = True Then
            CON.IS_ALLOW = 1
        Else
            CON.IS_ALLOW = 0
        End If

        If chkIsChuyen.Checked = True Then
            CON.IS_CHUYEN = 1
        Else
            CON.IS_CHUYEN = 0
        End If

        'CON.FILE_BYTE = lstFile
        'CON.FILE_BYTE1 = lstFile1

        CON.FILE_BYTE = txtUploadFile.Text.Trim
        CON.ATTACH_FOLDER_BYTE = txtUploadFile_Link.Text.Trim
        'CON.FILE_BYTE1 = txtUploadFile1.Text.Trim
        'CON.ATTACH_FOLDER_BYTE1 = txtUploadFile1_Link.Text.Trim
        If IsNumeric(hdQuanLyTrucTiep.Value) Then
            CON.CAP_QUAN_LY_TT = CType(hdQuanLyTrucTiep.Value, Decimal)
        End If
        'If cboChucVu.SelectedValue <> "" Then
        '    CON.CHUCVU_CON = cboChucVu.SelectedValue
        'End If
        CON.MONEY = rdMonney.Value
        CON.BASIC1 = txtBasic.Text
        If IDSelect <> 0 Then
            CON.ID = IDSelect
            If rep.Validate_Concurrently(CON) Then
                ShowMessage(Translate("Đã tồn tại quyết định kiêm nhiệm"), NotifyType.Warning)
                CurrentState = CommonMessage.STATE_EDIT
                UpdateControlState()
            Else
                result = rep.UPDATE_CONCURRENTLY(CON)
            End If
        Else
            CON.ID = 0
            If rep.Validate_Concurrently(CON) Then
                ShowMessage(Translate("Đã tồn tại quyết định kiêm nhiệm"), NotifyType.Warning)
                CurrentState = CommonMessage.STATE_EDIT
                UpdateControlState()
            Else
                result = rep.INSERT_CONCURRENTLY(CON)
            End If
        End If

        If cboStatus.SelectedValue = "447" AndAlso rdEFFECT_DATE_CON.SelectedDate <= Date.Now AndAlso Request.Params("IsConStop") Is Nothing Then
            rep.INSERT_EMPLOYEE_KN(txtEmpCode.Text, hidOrgCOn.Value, cboTITLE_CON.SelectedValue, rdEFFECT_DATE_CON.SelectedDate, result, If(cboStaffRank.SelectedValue = "", 0, cboStaffRank.SelectedValue))
        End If

        If rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing AndAlso cboSTATUS_STOP.SelectedValue = "447" AndAlso Request.Params("IsConStop") IsNot Nothing Then
            rep.UPDATE_EMPLOYEE_KN(result, rdEFFECT_DATE_STOP.SelectedDate)
        End If

        rep.Dispose()

        If result <> 0 Then
            hidID.Value = result
            IDSelect = result
            Return True
        Else
            Return False
        End If

    End Function

    'Lấy thông tin nhân viên từ tiện ích bên hồ sơ nhân viên
    Private Sub LoadEmployeeLink()
        ' Dim item = psp.GET_EMPLOYEE_LINK(LinkEmpId)
        'hidEmpID.Value = item.ID
        'txtEmpCode.Text = item.EMPLOYEE_CODE
        'txtEmpName.Text = item.FULLNAME_VN
        'txtEmpOrgName.Text = item.ORG_NAME
        'hidOrgId.Value = item.ORG_ID
        '' Dim dtData As DataTable = psp.GET_TITLE_ORG(hidOrgId.Value)
        'If dtData.Rows.Count > 0 Then
        '    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
        '    cboTitleId.Text = String.Empty
        'End If
        'cboTitleId.SelectedValue = item.TITLE_ID

    End Sub

    Private Sub PrintConcurrently(ByVal print_type As String)
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim dtData As DataTable
        Dim reportName As String = ""
        Dim reportNameOut As String = ""
        Try
            If rgConcurrently.Items.Count > 0 Then
                If rgConcurrently.SelectedItems.Count = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    Exit Sub
                End If
                If IDSelect <> 0 Then
                    ' dtData = psp.PRINT_CONCURRENTLY(IDSelect.ToString)
                End If
                If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
                    ShowMessage("Không có dữ liệu in quyết định", NotifyType.Warning)
                    Exit Sub
                End If

                If print_type = "TPCKN" Then
                    'reportName = "Decision\" + cboCancelConcurrently.SelectedValue + ".doc"
                    reportNameOut = "Chấm dứt phân công kiêm nhiệm" + ".doc"
                Else
                    'reportName = "Decision\" + cboConcurrentlyType.SelectedValue + ".doc"
                    reportNameOut = "Phân công kiểm nhiệm" + ".doc"
                End If

                If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                           reportNameOut,
                           dtData,
                           Response)
                Else
                    ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
                End If
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ' Chuyen trang thai control
    Public Sub DisableControls(ByVal control As System.Web.UI.Control, ByVal status As Boolean)
        For Each c As System.Web.UI.Control In control.Controls
            If TypeOf c Is RadTabStrip Then
            Else
                ' Get the Enabled property by reflection.
                Dim type As Type = c.GetType
                Dim prop As PropertyInfo = type.GetProperty("Enabled")

                ' Set it to False to disable the control.
                If Not prop Is Nothing Then
                    prop.SetValue(c, status, Nothing)
                End If
                ' Recurse into child controls.
                If c.Controls.Count > 0 Then
                    Me.DisableControls(c, status)
                End If
            End If

        Next

    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim fileNameZip As String

            'If order = 0 Then
            '    fileNameZip = txtUpload_LG.Text.Trim
            'ElseIf order = 1 Then
            '    fileNameZip = txtUpload_HD.Text.Trim
            'Else
            '    fileNameZip = txtUpload_FT.Text.Trim
            'End If

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Xử lý file đính kèm"

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        IsUpload = 0
        ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
        ctrlUpload1.Show()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim strPath_Down As String
        If txtUploadFile.Text <> "" Then
            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/ConcurrentlyInfo/" + txtUploadFile_Link.Text + txtUploadFile.Text)
            ZipFiles(strPath_Down, 0)
        End If
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If IsUpload = 0 Then
                txtUploadFile.Text = ""
                'Else
                '    txtUploadFile1.Text = ""
            End If

            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/ConcurrentlyInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        If IsUpload = 0 Then
                            txtUploadFile.Text = file.FileName
                            'Else
                            '    txtUploadFile1.Text = file.FileName
                        End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS,XLSX,TXT,CTR,DOC,DOCX,XML,PNG,JPG,BITMAP,JPEG,GIF,PDF,RAR,ZIP,PPT,PPTX"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                If IsUpload = 0 Then
                    loadDatasource(IsUpload, txtUploadFile.Text)
                    'Else
                    '    loadDatasource(IsUpload, txtUploadFile1.Text)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub btnUploadFile1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile1.Click
    '    IsUpload = 1
    '    ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
    '    ctrlUpload1.Show()
    'End Sub

    'Private Sub btnDownload1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload1.Click
    '    Dim strPath_Down As String
    '    If txtUploadFile1.Text <> "" Then
    '        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/ConcurrentlyInfo/" + txtUploadFile1_Link.Text + txtUploadFile1.Text)
    '        ZipFiles(strPath_Down, 1)
    '    End If
    'End Sub
#End Region

#End Region

    Private Sub rdEFFECT_DATE_CON_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdEFFECT_DATE_CON.SelectedDateChanged
        Dim store As New ProfileStoreProcedure
        Try
            ClearControlValue(txtSIGN, txtSIGN_TITLE, hidSign)
            If IsDate(rdEFFECT_DATE_CON.SelectedDate) Then
                Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEFFECT_DATE_CON.SelectedDate)
                If signer.Rows.Count > 0 Then
                    If IsNumeric(signer.Rows(0)("ID")) Then
                        hidSign.Value = CDec(signer.Rows(0)("ID"))
                    End If
                    txtSIGN.Text = signer.Rows(0)("EMPLOYEE_NAME")
                    txtSIGN_TITLE.Text = signer.Rows(0)("TITLE_NAME")
                End If
            End If
            If rdEFFECT_DATE_CON.SelectedDate IsNot Nothing And hidEmpID.Value IsNot Nothing Then
                rdSIGN_DATE.SelectedDate = rdEFFECT_DATE_CON.SelectedDate
            End If
            AutoCreate_DecisionNo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdEFFECT_DATE_STOP_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdEFFECT_DATE_STOP.SelectedDateChanged
        Dim store As New ProfileStoreProcedure
        If rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing Then
            If rdEFFECT_DATE_STOP.SelectedDate <= rdEFFECT_DATE_CON.SelectedDate Then
                ClearControlValue(rdEFFECT_DATE_STOP)
                ShowMessage(Translate("Ngày hiệu lực phải lơn hơn ngày hiệu lực của QĐ kiêm nhiệm"), NotifyType.Warning)
                Exit Sub
            End If
            AutoCreate_DecisionNo2()
        End If
    End Sub
    Private Sub AutoCreate_DecisionNo()
        Dim store As New ProfileStoreProcedure
        Try
            If IsDBNull(hidEmpID.Value) Then
                Exit Sub
            End If

            If rdEFFECT_DATE_CON.SelectedDate Is Nothing Then
                Exit Sub
            End If

            ClearControlValue(txtCON_NO)
            Dim contract_no = store.AUTOCREATE_CONCURRENTLYNO(Decimal.Parse(hidEmpID.Value),
                                                              LogHelper.CurrentUser.EMPLOYEE_ID,
                                                              rdEFFECT_DATE_CON.SelectedDate)

            txtCON_NO.Text = contract_no
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AutoCreate_DecisionNo2()
        Dim store As New ProfileStoreProcedure
        Try
            If IsDBNull(hidEmpID.Value) Then
                Exit Sub
            End If

            If rdEFFECT_DATE_STOP.SelectedDate Is Nothing Then
                Exit Sub
            End If

            ClearControlValue(txtConNoStop)
            Dim contract_no = store.AUTOCREATE_CONCURRENTLYNO2(Decimal.Parse(hidEmpID.Value),
                                                              LogHelper.CurrentUser.EMPLOYEE_ID,
                                                              rdEFFECT_DATE_STOP.SelectedDate)

            txtConNoStop.Text = contract_no
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class