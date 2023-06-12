Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlRC_Manning
    Inherits Common.CommonView
    Protected WithEvents ManningView As ViewBase
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))
    Dim repProGram As New RecruitmentRepository
    Dim repStore As New RecruitmentStoreProcedure

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Recuitment/Modules/Recuitment/Bussiness/" + Me.GetType().Name.ToString()

#Region "Properties"
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Public Property hasError As Decimal
        Get
            Return ViewState(Me.ID & "_hasError")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_hasError") = value
        End Set
    End Property
    Public Property isFlag As Decimal
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property
    Public Property isFlag1 As Decimal
        Get
            Return ViewState(Me.ID & "_isFlag1")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isFlag1") = value
        End Set
    End Property
    Public Property isExport As Decimal
        Get
            Return ViewState(Me.ID & "_isExport")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isExport") = value
        End Set
    End Property
    Public Property lstOrg As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstOrg")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstOrg") = value
        End Set
    End Property

    Public Property lstOrgStr As String
        Get
            Return ViewState(Me.ID & "_lstOrgStr")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_lstOrgStr") = value
        End Set
    End Property

    Public Property programID As Decimal
        Get
            Return ViewState(Me.ID & "_programID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_programID") = value
        End Set
    End Property

    Public Property fileUrl As String
        Get
            Return ViewState(Me.ID & "_fileUrl")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileUrl") = value
        End Set
    End Property

    Public Property requestID As Decimal
        Get
            Return ViewState(Me.ID & "_requestID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_requestID") = value
        End Set
    End Property

    Public Property fileOut_name As String
        Get
            Return ViewState(Me.ID & "_fileOut_name")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_fileOut_name") = value
        End Set
    End Property

    Public Property template As String
        Get
            Return ViewState(Me.ID & "_template")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_template") = value
        End Set
    End Property

    Public Property mid As String
        Get
            Return ViewState(Me.ID & "_mid")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_mid") = value
        End Set
    End Property

    Private Property dtbInfoEmp As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbRGT")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbRGT") = value
        End Set
    End Property

    Private Property dtbImport As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbImport")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbImport") = value
        End Set
    End Property

    Private Property dtDataTitle As DataTable
        Get
            Return PageViewState(Me.ID & "_dtDataTitle")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtDataTitle") = value
        End Set
    End Property

    Public Property tabSource As DataTable
        Get
            Return PageViewState(Me.ID & "_tabSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_tabSource") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Try
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                Refresh()
                UpdateControlState()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgManning.SetFilter()
            rgManning.AllowCustomPaging = True
            rgManning.PageSize = Common.Common.DefaultPageSize
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Approve, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(7), RadToolBarButton).Text = "Cập nhật định biên"
            'Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            'rgManning.ClientSettings.EnablePostBackOnRowClick = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    rbtManName.Enabled = False
                    rbtNote.Enabled = False
                    rdEffectDate.Enabled = False
                    cbStatus.Enabled = False
                    EnableRadCombo(cboYear, True)
                    EnableRadCombo(cboListManning, True)
                    EnabledGrid(rgManning, True)
                    ctrlOrganization.Enabled = True
                    'EnableGridView(False)
                    rgManning.Rebind()
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    rbtManName.Enabled = True
                    rbtNote.Enabled = True
                    rdEffectDate.Enabled = True
                    cbStatus.Enabled = False
                    EnableRadCombo(cboYear, True)
                    EnableRadCombo(cboListManning, True)
                    EnabledGrid(rgManning, True)
                    ctrlOrganization.Enabled = False
                    'EnableGridView(True)
                    rgManning.Rebind()
                Case CommonMessage.STATE_DELETE
                    If DeleteData() Then
                        'reload combobox
                        Dim dtListMannName As DataTable
                        dtListMannName = repStore.LoadComboboxListMannName(ctrlOrganization.CurrentValue, cboYear.SelectedValue)
                        FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")
                        cboListManning.SelectedIndex = 0

                        Refresh("UpdateView")
                        UpdateControlState()
                        rgManning.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.TOOLBARITEM_IMPORT
                    rbtManName.Enabled = True
                    rbtNote.Enabled = True
                    rdEffectDate.Enabled = True
                    cbStatus.Enabled = False
                    EnableRadCombo(cboYear, True)
                    EnableRadCombo(cboListManning, True)
                    EnabledGrid(rgManning, True)
                    ctrlOrganization.Enabled = False
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        'rgManning.Rebind()
                        CreateDataFilter()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgManning.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New RecruitmentRepository
        Dim dtData As DataTable
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    'If ctrlOrganization.CurrentValue = "" Or ctrlOrganization.CurrentValue = 1 Then
                    '    ShowMessage(Translate("Bạn phải chọn đơn vị thuộc cấp Công ty"), Utilities.NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    If ctrlOrganization.PARENT_ID_VALUE <> 1 Then
                        ShowMessage(Translate("Bạn phải chọn đơn vị thuộc cấp Công ty"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_NEW

                    'reset manning org info
                    hidMannOrgId.Value = ""
                    rbtManName.Text = ""
                    rbtManName.Focus()
                    rbtNote.Text = String.Empty
                    rdEffectDate.SelectedDate = DateTime.Now
                    cbStatus.Checked = True
                    rbtManningCountMobilize.Text = "0"
                    rbtManningCurrentCount.Text = "0"
                    rbtManningNew.Text = "0"
                    rbtManningOld.Text = "0"

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If cboListManning.SelectedValue = Nothing Then
                        'ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        ShowMessage("Vui lòng chọn tên định biên!", NotifyType.Warning)
                        Exit Sub
                    End If
                    rgManning.MasterTableView.EditMode = GridEditMode.InPlace

                    'EnableGridView(True)

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    rbtManName.Focus()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If cboListManning.SelectedValue Is Nothing Then
                        ShowMessage("Chưa chọn định biên!", NotifyType.Warning)
                        Exit Sub
                    End If
                    repStore.UpdateNewManningTitle1(cboListManning.SelectedValue)
                    ShowMessage("Đã cập nhật lại định biên!", NotifyType.Success)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rgManning.SelectedItems.Count = 0 AndAlso CurrentState = CommonMessage.STATE_EDIT Then
                            ShowMessage("Vui lòng chọn 1 dòng trước khi lưu", NotifyType.Error)
                            Exit Sub
                        End If
                        If SaveData() = 1 Then
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    ' reload combobox year
                                    Dim listYear As DataTable = repStore.LoadComboboxYear(Int32.Parse(ctrlOrganization.CurrentValue))
                                    FillRadCombobox(cboYear, listYear, "YEAR", "YEAR")
                                    cboYear.SelectedValue = rdEffectDate.SelectedDate.Value.Year

                                    'reload combobox manning org
                                    Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))
                                    Dim dtListMannName As DataTable
                                    dtListMannName = repStore.LoadComboboxListMannName(Int32.Parse(ctrlOrganization.CurrentValue), year)
                                    FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")
                                    cboListManning.SelectedIndex = 0

                                    'load mannning org
                                    GetManningOrgInfo()

                                    'reload gird
                                    'rgManning.DataSource = repStore.GetListManningByName(If(hidMannOrgId.Value = "", 0, Int32.Parse(hidMannOrgId.Value)), Int32.Parse(ctrlOrganization.CurrentValue), year)
                                    'rgManning.Rebind()
                                    CreateDataFilter()

                                Case CommonMessage.STATE_EDIT
                                    'reload combobox manning org
                                    Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))
                                    Dim dtListMannName As DataTable
                                    dtListMannName = repStore.LoadComboboxListMannName(Int32.Parse(ctrlOrganization.CurrentValue), year)
                                    FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")
                                    cboListManning.Text = rbtManName.Text
                                    saveNewMainning()
                                    UpdateInforGridview()
                                    'load mannning org
                                    GetManningOrgInfo()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    rgManning.DataSource = repStore.GetListManningByName(If(hidMannOrgId.Value = "", 0, Int32.Parse(hidMannOrgId.Value)), Int32.Parse(ctrlOrganization.CurrentValue), year, 0, New ManningOrgDTO)
                                    rgManning.Rebind()
                            End Select

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            EnabledGrid(rgManning, False)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                    GetManningOrgInfo()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    If cboListManning.SelectedValue = "" Then
                        ShowMessage(Translate("Chọn tên định biên cần nhập dữ liệu."), Utilities.NotifyType.Warning)
                        Return
                    End If
                    ctrlUpload.isMultiple = True
                    ctrlUpload.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    If cboListManning.SelectedValue <> "" Then
                        'ViewReport_Excel(Nothing)
                        Using xls As New ExcelCommon
                            isExport = 1
                            CreateDataFilter()
                            isExport = 0
                            If tabSource IsNot Nothing AndAlso tabSource.Rows.Count > 0 Then
                                Dim dsData As New DataSet
                                Dim dtDatan As New DataTable
                                dtDatan = tabSource.Copy
                                Dim _error As String = String.Empty
                                Dim bCheck As Boolean = False
                                dsData.Tables.Add(dtDatan)
                                dsData.Tables(0).TableName = "DATA"
                                ' rgManning.ExportExcel(Server, Response, tabSource, "Danh sách đơn vị bảo hiểm")
                                Using cls As New ExcelCommon
                                    bCheck = cls.ExportExcelTemplate(
                                 Server.MapPath("~/ReportTemplates//Recruitment//Import//Dinhbien_template.xls"), "Danh sách định biên", dtDatan, Response, String.Empty, ExcelCommon.ExportType.Excel)
                                    If Not bCheck Then
                                        Select Case _error
                                            Case 1
                                                ShowMessage(Translate("File mẫu không tồn tại"), NotifyType.Warning)
                                            Case 2
                                                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                                        End Select
                                        Exit Sub
                                    End If
                                End Using
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_DATA_EMPTY), NotifyType.Warning)
                                Exit Sub
                            End If
                        End Using
                    Else
                        ShowMessage(Translate("Bạn chưa chọn tên định biên cần xuất!."), Utilities.NotifyType.Warning)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
            End Select
            'ChangeToolbarState()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgManning.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Try
            Using rep As New RecruitmentRepository
                hidOrgID.Value = ""
                lblOrgName.Text = ""

                If ctrlOrganization.CurrentValue <> "" Then
                    hidOrgID.Value = ctrlOrganization.CurrentValue
                    lblOrgName.Text = ctrlOrganization.CurrentText

                    Dim listYear As DataTable = repStore.LoadComboboxYear(Int32.Parse(ctrlOrganization.CurrentValue))
                    FillRadCombobox(cboYear, listYear, "YEAR", "YEAR")
                    cboYear.SelectedIndex = 0
                    If listYear.Rows.Count = 0 Then
                        cboYear.Text = ""
                    End If
                    Dim dtListMannName As DataTable
                    dtListMannName = repStore.LoadComboboxListMannName(ctrlOrganization.CurrentValue, If(cboYear.SelectedValue = "", 0, Int32.Parse(cboYear.SelectedValue)))
                    FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")
                    cboListManning.Text = ""
                End If
                rgManning.DataSource = Nothing
                rgManning.Rebind()
                'rgManning.CurrentPageIndex = 0
                'rgManning.Rebind()
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Dim dtListMannName As DataTable
            If cboYear.SelectedValue Is Nothing Or cboYear.SelectedValue = "" Then
                FillRadCombobox(cboListManning, New DataTable, "NAME", "ID")
            Else
                cboListManning.ClearSelection()
                dtListMannName = repStore.LoadComboboxListMannName(ctrlOrganization.CurrentValue, cboYear.SelectedValue)
                FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")

            End If
            rgManning.DataSource = Nothing
            rgManning.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboListManning_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboListManning.SelectedIndexChanged
        Try
            'rgManning.Enabled = True
            hidMannOrgId.Value = cboListManning.SelectedValue

            'Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))

            'rgManning.DataSource = repStore.GetListManningByName(Decimal.Parse(cboListManning.SelectedValue), Int32.Parse(hidOrgID.Value), year)
            ' tabSource = repStore.GetListManningByName(If(cboListManning.SelectedValue <> "", Decimal.Parse(cboListManning.SelectedValue), 0), If(hidOrgID.Value = "", 0, Int32.Parse(hidOrgID.Value)), year)
            ' rgManning.DataSource = tabSource
            'rgManning.Rebind()

            'Load thông tin định biên
            'GetManningOrgInfo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        'Dim periodId As String = cbxSignPeriod.SelectedValue
        ImportManning()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_REJECT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_REJECT
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgManning.Enabled = True
            hidMannOrgId.Value = CDec(Val(cboListManning.SelectedValue))

            Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))
            Dim manning As Integer = If(cboListManning.SelectedValue = [String].Empty, 0, Decimal.Parse(cboListManning.SelectedValue))
            'tabSource = repStore.GetListManningByName(manning, Int32.Parse(ctrlOrganization.CurrentValue), year)
            'rgManning.DataSource = tabSource
            rgManning.Rebind()
            'CreateDataFilter()
            'Load thông tin định biên
            GetManningOrgInfo()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Function & Sub"
    Private Sub saveNewMainning()
        Dim objMT As New ManningTitleDTO
        For Each item As GridDataItem In rgManning.SelectedItems
            objMT.ID = Int32.Parse(item.GetDataKeyValue("ID").ToString())
            objMT.CURRENT_MANNING = Int32.Parse(item.GetDataKeyValue("CURRENT_MANNING").ToString())
            objMT.NEW_MANNING = Int32.Parse(TryCast(item.FindControl("txtNewManning"), RadNumericTextBox).Value)
            objMT.MOBILIZE_COUNT_MANNING = objMT.NEW_MANNING - objMT.CURRENT_MANNING
            objMT.NOTE = TryCast(item.FindControl("txtNote"), RadTextBox).Text
            'updatel/reload summary manning org
            repStore.UpdateNewManningTitle(objMT)
            repStore.UpdateNewManningOrg(cboListManning.SelectedValue)
            GetManningOrgInfo()
        Next
        'CreateDataFilter()
        rgManning.Rebind()
    End Sub

    Private Function SaveData() As Integer
        Try
            Dim repStore As New RecruitmentStoreProcedure
            Dim IsCompeted As Integer = 0
            Dim objMO As New ManningOrgDTO
            Dim objMT As New ManningTitleDTO

            objMO.ORG_ID = ctrlOrganization.CurrentValue
            If isFlag = 1 Then

            Else
                objMO.MANNING_NAME = rbtManName.Text
                objMO.EFFECT_DATE = rdEffectDate.SelectedDate
                objMO.NOTE = rbtNote.Text
                objMO.OLD_MANNING = 0
                objMO.CURRENT_MANNING = 0
                objMO.NEW_MANNING = 0
                objMO.MOBILIZE_COUNT_MANNING = 0
            End If
            If cbStatus.Checked Then
                objMO.STATUS = 1
            Else
                objMO.STATUS = 0
            End If
            If hidMannOrgId.Value = "" Then
                IsCompeted = repStore.AddNewManningOrg(objMO)
                Dim idManningOrg As Int32 = repStore.GetManningOrgId()
                hidMannOrgId.Value = idManningOrg
                IsCompeted = repStore.AddNewManningTitle(objMO.ORG_ID, idManningOrg)
                IsCompeted = repStore.UpdateNewManningOrg(idManningOrg)
            Else
                If isFlag = 1 Then
                    IsCompeted = 1
                    isFlag = 0
                Else
                    objMO.ID = hidMannOrgId.Value
                    IsCompeted = repStore.UpdateManningOrg(objMO)
                    repStore.UpdateNewManningOrg(cboListManning.SelectedValue)
                End If
            End If

            'If IsCompeted = 1 Then
            '    ' reload combobox year
            '    Dim listYear As DataTable = repStore.LoadComboboxYear(Int32.Parse(ctrlOrganization.CurrentValue))
            '    FillRadCombobox(cboYear, listYear, "YEAR", "YEAR")
            '    cboYear.SelectedValue = objMO.EFFECT_DATE.Value.Year

            '    'reload combobox manning org
            '    Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))
            '    Dim dtListMannName As DataTable
            '    dtListMannName = repStore.LoadComboboxListMannName(Int32.Parse(ctrlOrganization.CurrentValue), year)
            '    FillRadCombobox(cboListManning, dtListMannName, "NAME", "ID")
            '    cboListManning.SelectedIndex = 0

            '    'load default mannning mới tạo
            '    GetManningOrgInfo()

            '    ''reload grid
            '    'CreateDataFilter()

            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            'Else
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            'End If

            Return IsCompeted

        Catch ex As Exception
            Return 0
        End Try
    End Function
    ''' <summary>
    ''' cập nhật Thông tin thay đổi trên lưới
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInforGridview() As Boolean
        Try
            Dim objMT As New ManningTitleDTO
            Dim count As Integer = 0
            Dim complete As Integer = 0
            'Dim objMO As New ManningOrgDTO
            'objMO.CURRENT_MANNING = 0
            'objMO.MOBILIZE_COUNT_MANNING = 0
            'objMO.NEW_MANNING = 0
            If isFlag1 <> 1 Then
                If rgManning.SelectedItems.Count > 0 Then
                    For Each data As GridDataItem In rgManning.SelectedItems
                        count = count + 1
                        objMT.ID = data.GetDataKeyValue("ID")
                        If data.GetDataKeyValue("NEW_MANNING") IsNot Nothing Then
                            objMT.NEW_MANNING = Int32.Parse(data.GetDataKeyValue("NEW_MANNING"))
                            'CType(data.Item("NEW_MANNING").Controls(0), RadNumericTextBox).Value
                            'objMO.NEW_MANNING += objMT.NEW_MANNING                            
                        Else
                            objMT.NEW_MANNING = 0
                        End If
                        If data.GetDataKeyValue("CURRENT_MANNING") IsNot Nothing Then
                            objMT.CURRENT_MANNING = Int32.Parse(data.GetDataKeyValue("CURRENT_MANNING"))
                            'objMO.CURRENT_MANNING += objMT.CURRENT_MANNING
                        Else
                            objMT.CURRENT_MANNING = 0
                        End If

                        objMT.MOBILIZE_COUNT_MANNING = objMT.NEW_MANNING - objMT.CURRENT_MANNING
                        objMT.NOTE = data.GetDataKeyValue("NOTE").ToString
                        'CType(data.Item("NOTE").Controls(0), TextBox).Text
                        'objMT.MOBILIZE_COUNT_MANNING += objMT.MOBILIZE_COUNT_MANNING

                        If (repStore.UpdateNewManningTitle(objMT) = 1) Then
                            complete = complete + 1
                        End If
                    Next
                    If count = complete Then
                        'cap nhat lai so luong định biên
                        'objMO.ID = hidMannOrgId.Value
                        'repStore.UpdateInforManningOrg(objMO)
                        GetManningOrgInfo()
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                rgManning.SelectedIndexes.Clear()
                isFlag1 = 0
                GetManningOrgInfo()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function DeleteData() As Boolean
        Dim lstID As String = String.Empty
        Try
            For i As Integer = 0 To rgManning.SelectedItems.Count - 1
                Dim item As GridDataItem = rgManning.SelectedItems(i)
                lstID = If(lstID = String.Empty, item.GetDataKeyValue("ID"), lstID & "," & item.GetDataKeyValue("ID"))
            Next
            Return repStore.DeleteManning(lstID, Int32.Parse(cboListManning.SelectedValue))
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ViewReport_Excel(Optional ByVal isExportXML As Boolean = False)
        Try
            Dim lstParaValue As New List(Of Object)
            Dim rep As New HistaffFrameworkRepository
            Dim _error As Integer = 0
            Dim itmp As Integer = 1
            Dim lstlst As New List(Of List(Of Object))
            Dim dataSet As New DataSet
            Dim dtVariable As New DataTable
            Dim dsReport As New DataSet
            Dim store_Out As String
            ' Lấy danh sách các tham số
            lstParaValue = GetListParametersValue()
            If hasError = -1 Then
                Exit Sub
            End If

            'If type = ATConstant.IMPORT_MANNING Then
            'Lấy thông tin chứa store xuất báo cáo, đường dẫn Template 
            programID = ATConstant.PROGRAMID_IMPORT_MANNING
            'End If

            repProGram.GetTemplateInfo(programID, fileUrl, fileOut_name, mid)

            If fileOut_name <> "" Then
                store_Out = fileOut_name
                fileOut_name = fileOut_name.Substring(fileOut_name.IndexOf("."c) + 1)
                dsReport = rep.ExecuteToDataSet(store_Out, lstParaValue)
                template = fileOut_name + ".xlsx"
            Else
                ShowMessage("Store không tồn tại!", Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            End If

            If (dsReport Is Nothing OrElse dsReport.Tables.Count = 0) Then
                ' Không có dữ liệu để in báo báo
                ShowMessage(Translate("AT_IMPORT_MANNING_NO_DATA_PRINT_REPORT"), Framework.UI.Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If programID <> -1 Then
                If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 1) 'theo đường dẫn vật lý
                    Else 'Thực hiện export file sau khi xử lý báo cáo
                        If Not File.Exists(System.IO.Path.Combine(Framework.UI.Utilities.PathTemplateInFolder & mid(), template)) Then
                            ' Mẫu báo cáo không tồn tại
                            ShowMessage(Translate("AT_IMPORT_MANNING_NO_EXIST_TEMPLATE"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Using xls As New AsposeExcelCommon
                            If requestID <> 0 Then
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                System.IO.Path.Combine(Framework.UI.Utilities.PathTemplateInFolder & mid(), template), Framework.UI.Utilities.PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            Else
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                System.IO.Path.Combine(Framework.UI.Utilities.PathTemplateInFolder & mid(), template), Framework.UI.Utilities.PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            End If

                        End Using
                    End If
                Else '0: server.mappath                
                    If isExportXML Then
                        ExportFileDataXML(dsReport, requestID, programID, mid, 0) 'theo đường dẫn tương đối Mappath
                    Else
                        If Not File.Exists(Server.MapPath("~/ReportTemplates/" & mid() & "/" & template)) Then
                            ' Mẫu báo cáo không tồn tại
                            ShowMessage(Translate("AT_IMPORTTIMESHEET_NO_EXIST_TEMPLATE"), Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Using xls As New AsposeExcelCommon
                            If requestID <> 0 Then
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                Server.MapPath("~/ReportTemplates/" & mid() & "/" & template), Framework.UI.Utilities.PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            Else
                                Dim bCheck = xls.ExportExcelTemplateReport(
                                Server.MapPath("~/ReportTemplates/" & mid() & "/" & template), Framework.UI.Utilities.PathTemplateOutFolder, fileOut_name & "_" & requestID, dsReport, Response)
                            End If
                        End Using
                    End If

                End If
            Else
                ShowMessage((Translate("AT_IMPORTTIMESHEET_NO_EXIST_TEMPLATE")), Framework.UI.Utilities.NotifyType.Warning)
            End If
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function GetListParametersValue() As List(Of Object)
        Dim rs As New List(Of Object)
        Dim index As Decimal = 0
        Dim str As String = ""
        Dim typefield As String = ""
        Dim selectedSign As New List(Of ManningOrgDTO)
        hasError = 0
        ' Kiểm tra chọn phòng ban 
        If ctrlOrganization.CurrentValue Is Nothing Then
            'Bạn CHƯA chọn phòng ban!
            ShowMessage(Translate("AT_IMPORTTIMESHEET_NO_SELECT_ORG."), Utilities.NotifyType.Warning)
            hasError = -1
        Else
            'lstOrg =
            lstOrgStr = ctrlOrganization.CurrentValue
            'For i As Integer = 0 To lstOrg.Count - 1
            '    If i = lstOrg.Count - 1 Then
            '        lstOrgStr = lstOrgStr & lstOrg(i).ToString
            '    Else
            '        lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
            '    End If
            'Next
            rs.Add(lstOrgStr)
        End If
        ' Kiểm tra chọn kỳ công 
        If cboListManning.SelectedValue Is Nothing Then
            'Bạn chưa chọn kỳ công
            ShowMessage(Translate("AT_IMPORTTIMESHEET_NO_SELECT_PERIOD."), Utilities.NotifyType.Warning)
            hasError = -1
        Else
            rs.Add(cboListManning.SelectedValue)
        End If

        ' Kiểm tra chọn kỳ công 
        'If cboTitle.SelectedValue Is Nothing Then
        '    'Bạn chưa chọn loai import
        '    ShowMessage(Translate("AT_IMPORTTIMESHEET_NO_SELECT_TYPE_IMPORT."), Utilities.NotifyType.Warning)
        '    hasError = -1
        'Else
        '    rs.Add(cboTitle.SelectedValue)
        'End If
        Return rs
    End Function

    Private Sub ImportManning()
        'Cột bắt đầu đọc
        '  Dim colStart As Integer
        'Dòng bắt đầu đọc
        ' Dim rowStart As Integer
        'Dim countDay As Integer = (endPeriodid - startPeriodid).TotalDays + 1 '=4
        'Dim colDataPeriodBegin As Integer = (colStart + countDay) - 1 'Default Index từ 0
        'Dim drPeriod As DataRow = dtbPeriod.Select(String.Format("ID = {0}", periodId)).FirstOrDefault()
        'Dim startDate As Date = Date.Parse(drPeriod("STARTDATE"))
        'Dim endDate As Date = Date.Parse(drPeriod("ENDDATE"))
        'Dim countDayInPeriod As Long = DateDiff(DateInterval.Day, startDate, endDate) + 1
        ' Dim SumAllColImport As Integer
        Dim fileName As String
        'isCheckContract = ""
        'isCheckExists = ""
        'isCheckJoinDate = ""
        'isCheckTereffectDate = ""
        'isCheckWorking = ""
        'isCheckSign = ""
        'isCheckAnnual = ""
        'DesignGridViewTimeSheet(startPeriodid, endPeriodid)
        Try
            'Lấy thông tin nhân viên để kiểm tra khi import
            lstOrg = ctrlOrganization.CheckedValueKeys
            lstOrgStr = ctrlOrganization.CurrentValue
            'For i As Integer = 0 To lstOrg.Count - 1
            '    If i = lstOrg.Count - 1 Then
            '        lstOrgStr = lstOrgStr & lstOrg(i).ToString
            '    Else
            '        lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
            '    End If
            'Next
            dtbInfoEmp = (New RecruitmentStoreProcedure).LoadInfoManningByOrg(lstOrgStr, cboListManning.SelectedValue)

            '1. Đọc dữ liệu từ file Excel
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim ds As New DataSet


            '2. Check file và import
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                '1.1 Lưu file lên server
                file.SaveAs(fileName, True)

                '2.1 Đọc dữ liệu trong file Excel
                Using ep As New ExcelPackage
                    ds = ExcelPackage.ReadExcelToDataSet(fileName, False)
                End Using

                '2.2. File có dữ liệu thì check điều kiện import
                If CheckDataImport(ds) Then
                    dtbImport = CreateDataForGridViewImport(ds, 1, 0)
                    'AssignHeader()
                    If dtbImport.Rows.Count > 0 Then
                        rgManning.DataSource = dtbImport
                        rgManning.DataBind()
                    End If
                End If
            End If
            'Cập nhật trạng thái sửa để user sửa và lưu
            rgManning.MasterTableView.EditMode = GridEditMode.InPlace
            RequiredFieldValidator1.Enabled = False
            RequiredFieldValidator2.Enabled = False
            ' EnableGridView(True)
            isFlag = 1

            Dim sw As New StringWriter()
            Dim DocXml As String = String.Empty
            dtbImport.WriteXml(sw, False)
            DocXml = sw.ToString
            Dim callst As New RecruitmentStoreProcedure

            If callst.IMPORT_DINH_BIEN(LogHelper.GetUserLog.Username.ToUpper, DocXml) Then
                ShowMessage(Translate("Import thành công"), NotifyType.Success)
                rgManning.Rebind()
            Else
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            End If


            'Dim objMT As New ManningTitleDTO
            'For Each item In dtbImport.Rows
            '    objMT.ID = CDec(Val(item("ID")))
            '    objMT.NEW_MANNING = CDec(Val(item("NEW_MANNING")))
            '    objMT.CURRENT_MANNING = CDec(Val(item("CURRENT_MANNING")))
            '    objMT.MOBILIZE_COUNT_MANNING = CDec(Val(item("NEW_MANNING"))) - CDec(Val(item("CURRENT_MANNING")))
            '    objMT.NOTE = item("NOTE")
            '    'updatel/reload summary manning org
            '    repStore.UpdateNewManningTitle(objMT)
            '    repStore.UpdateNewManningOrg(cboListManning.SelectedValue)
            '    GetManningOrgInfo()
            'Next
            'CreateDataFilter()
            'rgManning.Rebind()

            CurrentState = CommonMessage.STATE_NORMAL
            UpdateControlState()
            'CurrentState = CommonMessage.STATE_EDIT
            'UpdateControlState()

        Catch ex As Exception
            'ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub GetManningOrgInfo()
        'Load thông tin định biên
        If cboListManning.SelectedValue <> Nothing Then
            Dim tab As DataTable = repStore.GetManningOrgByID(cboListManning.SelectedValue)
            If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                hidMannOrgId.Value = tab.Rows(0)("ID").ToString()
                rbtManName.Text = tab.Rows(0)("NAME").ToString()
                rdEffectDate.SelectedDate = tab.Rows(0)("EFFECT_DATE")
                rbtNote.Text = tab.Rows(0)("NOTE").ToString
                rbtManningOld.Text = tab.Rows(0)("OLD_MANNING").ToString
                rbtManningCurrentCount.Text = tab.Rows(0)("CURRENT_MANNING").ToString
                rbtManningCountMobilize.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString
                rbtManningNew.Text = tab.Rows(0)("NEW_MANNING").ToString
                cbStatus.Checked = If(tab.Rows(0)("STATUS").ToString() = "0", False, True)
            End If
        Else
            hidMannOrgId.Value = ""
        End If
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try

            Dim year As Integer = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))
            Dim cboList As Decimal

            cboList = If(cboListManning.SelectedValue = "", 0, Decimal.Parse(cboListManning.SelectedValue))

            'Dim lstData As DataTable = repStore.GetListManningByName(-1, Int32.Parse(hidOrgID.Value), year)
            'Dim lstData As DataTable = repStore.GetListManningByName(-1, 0, year)
            'tabSource = repStore.GetListManningByName(-1, 0, year)
            Dim MaximumRows As Integer
            Dim _filter As New ManningOrgDTO
            SetValueObjectByRadGrid(rgManning, _filter)
            tabSource = repStore.GetListManningByName(cboList, If(ctrlOrganization.CurrentValue = "", 0, Int32.Parse(ctrlOrganization.CurrentValue)), year, isExport, _filter, MaximumRows, rgManning.CurrentPageIndex, rgManning.PageSize)

            'If dtbImport IsNot Nothing Then
            '    tabSource = dtbImport
            'Else
            '    tabSource = repStore.GetListManningByName(cboList, If(ctrlOrganization.CurrentValue = "", 0, Int32.Parse(ctrlOrganization.CurrentValue)), year, isExport, _filter, MaximumRows, rgManning.CurrentPageIndex, rgManning.PageSize)
            'End If
            If tabSource Is Nothing Then
                tabSource = New DataTable
            End If

            'rgManning.DataSource = tabSource
            'rgManning.MasterTableView.VirtualItemCount = tabSource.Rows.Count
            'rgManning.CurrentPageIndex = rgManning.MasterTableView.CurrentPageIndex
            rgManning.DataSource = tabSource
            rgManning.VirtualItemCount = MaximumRows
        Catch ex As Exception
            tabSource = New DataTable
            rgManning.DataSource = tabSource
            rgManning.VirtualItemCount = 0
        End Try
    End Function
    Private Sub EnableGridView(ByVal value As Boolean)

        For Each item As GridItem In rgManning.MasterTableView.Items
            item.Edit = value
            item.FindControl("txtNewManning").EnableViewState = value
            item.FindControl("txtNote").EnableViewState = value
        Next

        'rgManning.Rebind()
    End Sub
    Public Sub ExportFileDataXML(ByVal dsReport As DataSet, ByVal requestId As Decimal, ByVal programID As Decimal, ByVal mid As String, ByVal type As Decimal)
        Try
            If type = 1 Then 'đường dẫn vật lý được lấy ở web.config
                dsReport.WriteXml(System.IO.Path.Combine(Framework.UI.Utilities.PathTemplateOutFolder & mid, programID & "_XML_DATA_" & requestId & ".xml"))
                Dim fileToDownload = System.IO.Path.Combine(Framework.UI.Utilities.PathTemplateOutFolder & mid, programID & "_XML_DATA_" & requestId & ".xml") 'Server.MapPath("~/App_Data/someFile.pdf")

                Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileToDownload)

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
            Else
                dsReport.WriteXml(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/ReportOut/" & mid), programID & "_XML_DATA_" & requestId & ".xml"))
                Dim fileToDownload = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/ReportOut/" & mid), programID & "_XML_DATA_" & requestId & ".xml") 'Server.MapPath("~/App_Data/someFile.pdf")

                Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileToDownload)

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
            End If
        Catch ex As Exception
            'Throw ex
        End Try

    End Sub

    Private Function CheckDataImport(ByVal ds As DataSet) As Boolean
        Dim msg As String = String.Empty
        Dim checkCol As Integer
        Dim countTableImport As Integer = ds.Tables(0).Columns.Count
        '1. Check file import có dữ liệu hay không
        If ds Is Nothing OrElse ds.Tables Is Nothing OrElse ds.Tables(0).Rows Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
            '[File invalid] File không có dữ liệu để import!"
            msg = "File không có dữ liệu để import!"
            GoTo BREAKFUNCTION
        End If

        'If SumColImport <> countTableImport Then
        '    '[File invalid] File import không hợp lệ, số ngày import không bằng với số ngày của kỳ công
        '    msg = "File import không hợp lệ!"
        '    GoTo BREAKFUNCTION
        'End If

        Return True
BREAKFUNCTION:
        ShowMessage(msg, Utilities.NotifyType.Warning)
        Return False
    End Function

    Private Function CreateDataForGridViewImport(ByVal ds As DataSet,
                                                 ByVal rowDataBegin As Integer,
                                                 ByVal countDayInPeriod As Integer) As DataTable
        Dim dtb As New DataTable()
        Dim dr As DataRow
        Dim endColDataAT As Integer
        Dim endcol As Integer
        'If cbxType.SelectedValue = ATConstant.IMPORT_MONTH Then
        '    endColDataAT = (colDataPeriodBegin + countDayInPeriod)
        'Else
        '    endColDataAT = (colDataPeriodBegin + countDayInPeriod)
        'End If
        Dim stt As Integer = 1

        dtb = CreateDataTableImport()

        For i = rowDataBegin To ds.Tables(0).Rows.Count - 1
            dr = dtb.NewRow
            If ds.Tables(0).Rows(i)(1).ToString <> "" Then
                'dr("ID") = stt
                dr("ID") = ds.Tables(0).Rows(i)(0)
                ' dr("ORG_NAME") = ds.Tables(0).Rows(i)(1)

                dr("NAME") = ds.Tables(0).Rows(i)(1)
                dr("ORG_NAME") = ds.Tables(0).Rows(i)(2)
                dr("TITLE_GROUP") = ds.Tables(0).Rows(i)(3)
                dr("TITLE_NAME") = ds.Tables(0).Rows(i)(4)
                dr("EFFECT_DATE") = DateTime.FromOADate(ds.Tables(0).Rows(i)(5))
                dr("OLD_MANNING") = ds.Tables(0).Rows(i)(6)
                dr("CURRENT_MANNING") = ds.Tables(0).Rows(i)(7)
                dr("NEW_MANNING") = ds.Tables(0).Rows(i)(8)
                dr("MOBILIZE_COUNT_MANNING") = dr("NEW_MANNING") - dr("CURRENT_MANNING")
                dr("NOTE") = If(ds.Tables(0).Rows(i).Field(Of String)(10) Is Nothing, "", ds.Tables(0).Rows(i).Field(Of String)(10))
                'For j = colDataPeriodBegin To endColDataAT - 1
                '    dr(j) = ds.Tables(0).Rows(i)(j)
                'Next
                dtb.Rows.Add(dr)
                stt = stt + 1
            Else
                Continue For
            End If
        Next
        dtb.AcceptChanges()
        Return dtb
    End Function

    Private Function CreateDataTableImport(Optional ByVal tableName As String = "table", Optional ByVal idRowItems As String = "ImportList_IS_SELECTED") As DataTable
        Dim dtb As New DataTable
        Dim i As Integer = 1
        'Dim dateTemp As Date = startPeriod
        'Dim dateTemp2 As Date = endPeriod
        dtb.TableName = tableName
        dtb.Columns.Add("ID", GetType(Decimal))
        dtb.Columns.Add("ORG_NAME", GetType(String))
        dtb.Columns.Add("TITLE_GROUP", GetType(String))
        dtb.Columns.Add("TITLE_NAME", GetType(String))
        dtb.Columns.Add("NAME", GetType(String))
        dtb.Columns.Add("EFFECT_DATE", GetType(Date))
        dtb.Columns.Add("OLD_MANNING", GetType(Decimal))
        dtb.Columns.Add("CURRENT_MANNING", GetType(Decimal))
        dtb.Columns.Add("NEW_MANNING", GetType(Decimal))
        dtb.Columns.Add("MOBILIZE_COUNT_MANNING", GetType(Decimal))
        dtb.Columns.Add("NOTE", GetType(String))
        'If countDay = 28 Then
        '    dateTemp2 = dateTemp2.AddDays(3)
        'ElseIf countDay = 29 Then
        '    dateTemp2 = dateTemp2.AddDays(2)
        'ElseIf countDay = 30 Then
        '    dateTemp2 = dateTemp2.AddDays(1)
        'End If
        'If startPeriod <> Nothing Then
        '    Do
        '        dtb.Columns.Add("DATA" + i.ToString, GetType(String))
        '        dateTemp = dateTemp.AddDays(1)
        '        i = i + 1
        '    Loop Until dateTemp > dateTemp2
        'End If

        Return dtb
    End Function

    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)

        'If CurrentState <> CommonMessage.STATE_NORMAL Then
        Dim str As String
        Dim arr() As String
        arr = e.Argument.Split("_")
        str = arr(arr.Count - 1)

        Dim objMT As New ManningTitleDTO
        'Dim item = TryCast(rgManning.SelectedItems(0), GridDataItem)
        Select Case str
            Case "txtNewManning"
                For Each item As GridDataItem In rgManning.SelectedItems
                    objMT.ID = Int32.Parse(item.GetDataKeyValue("ID").ToString())
                    objMT.CURRENT_MANNING = Int32.Parse(item.GetDataKeyValue("CURRENT_MANNING").ToString())
                    objMT.NEW_MANNING = Int32.Parse(TryCast(item.FindControl("txtNewManning"), RadNumericTextBox).Value)
                    objMT.MOBILIZE_COUNT_MANNING = objMT.NEW_MANNING - objMT.CURRENT_MANNING
                    repStore.UpdateNewManningTitle(objMT)
                    objMT.NOTE = TryCast(item.FindControl("txtNote"), RadTextBox).Text
                    repStore.UpdateNewManningTitle(objMT)
                    item("MOBILIZE_COUNT_MANNING").Text = objMT.MOBILIZE_COUNT_MANNING.Value
                    'updatel/reload summary manning org
                    repStore.UpdateNewManningOrg(cboListManning.SelectedValue)
                    GetManningOrgInfo()
                    'rgManning.SelectedIndexes.Clear()
                Next
            Case "txtNote"
                For Each item As GridDataItem In rgManning.SelectedItems
                    objMT.ID = Int32.Parse(item.GetDataKeyValue("ID").ToString())
                    'objMT.NEW_MANNING = Int32.Parse(TryCast(item.FindControl("txtNewManning"), RadNumericTextBox).Value)
                    objMT.NOTE = TryCast(item.FindControl("txtNote"), RadTextBox).Text
                    repStore.UpdateNewManningTitle(objMT)
                    objMT.CURRENT_MANNING = Int32.Parse(item.GetDataKeyValue("CURRENT_MANNING").ToString())
                    objMT.NEW_MANNING = Int32.Parse(TryCast(item.FindControl("txtNewManning"), RadNumericTextBox).Value)
                    objMT.MOBILIZE_COUNT_MANNING = objMT.NEW_MANNING - objMT.CURRENT_MANNING
                    repStore.UpdateNewManningTitle(objMT)
                    '  rgManning.SelectedIndexes.Clear()
                Next
        End Select
        isFlag1 = 1
    End Sub
#End Region

    Protected Sub rgManning_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgManning.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then
            Dim txtNewManning As RadNumericTextBox = TryCast(e.Item.FindControl("txtNewManning"), RadNumericTextBox)
            Dim txtNote As RadTextBox = TryCast(e.Item.FindControl("txtNote"), RadTextBox)
            If CurrentState = CommonMessage.STATE_NORMAL Then
                txtNewManning.Enabled = False
                txtNote.Enabled = False
            Else
                txtNewManning.Enabled = True
                txtNote.Enabled = True
            End If
            'If CurrentState = CommonMessage.STATE_NORMAL Then
            '    EnableGridView(False)
            'Else
            '    EnableGridView(True)
            'End If
        End If
    End Sub

End Class