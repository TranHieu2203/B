Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SalaryRank
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property SalaryRanks As List(Of SalaryRankDTO)
        Get
            Return ViewState(Me.ID & "_SalaryRanks")
        End Get
        Set(ByVal value As List(Of SalaryRankDTO))
            ViewState(Me.ID & "_SalaryRanks") = value
        End Set
    End Property

    Property DeleteSalaryRanks As List(Of SalaryRankDTO)
        Get
            Return ViewState(Me.ID & "_DeleteSalaryRanks")
        End Get
        Set(ByVal value As List(Of SalaryRankDTO))
            ViewState(Me.ID & "_DeleteSalaryRanks") = value
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

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 22/08/2017 14:00
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
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryRanks
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim obj As New SalaryRankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If cboSalaryGroup.SelectedValue <> "" Then
                obj.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            End If
            Using rep As New PayrollRepository

                If (Utilities.Account = "U") Then
                    If isFull Then
                        If Sorts IsNot Nothing Then
                            Return rep.GetSalaryRank_Unilever(obj, Sorts).ToTable
                        Else
                            Return rep.GetSalaryRank_Unilever(obj).ToTable
                        End If
                    Else
                        If Sorts IsNot Nothing Then
                            Me.SalaryRanks = rep.GetSalaryRank_Unilever(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                        Else
                            Me.SalaryRanks = rep.GetSalaryRank_Unilever(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                        End If
                    End If
                Else
                    If isFull Then
                        If Sorts IsNot Nothing Then
                            Return rep.GetSalaryRank(obj, Sorts).ToTable
                        Else
                            Return rep.GetSalaryRank(obj).ToTable
                        End If
                    Else
                        If Sorts IsNot Nothing Then
                            Me.SalaryRanks = rep.GetSalaryRank(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                        Else
                            Me.SalaryRanks = rep.GetSalaryRank(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                        End If
                    End If
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = Me.SalaryRanks
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    'cboSalaryLevel.SelectedValue = Nothing
                    txtRank.Text = ""
                    'rntxtSalaryBasic.Text = ""
                    txtRemark.Text = ""
                    rntxtOrders.Text = "1"
                    ClearControlValue(txtRank, txtRemark, rntxtOrders, cboSalaryLevel, txtheso, rntxtSalaryBasic)
                    rntxtSalaryBasic.ReadOnly = False
                    txtheso.ReadOnly = False
                    txtRank.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtOrders.ReadOnly = False
                    Utilities.EnableRadCombo(cboSalaryLevel, True)
                    Utilities.EnableRadCombo(cboSalaryGroup, True)
                Case CommonMessage.STATE_NORMAL
                    txtRank.Text = ""
                    'rntxtSalaryBasic.Text = ""
                    txtRemark.Text = ""
                    rntxtOrders.Text = ""
                    ClearControlValue(txtRank, txtRemark, rntxtOrders, cboSalaryLevel, txtheso, rntxtSalaryBasic)
                    EnabledGridNotPostback(rgData, True)
                    rntxtSalaryBasic.ReadOnly = True
                    txtheso.ReadOnly = True
                    txtRank.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtOrders.ReadOnly = True
                    Utilities.EnableRadCombo(cboSalaryLevel, False)
                    Utilities.EnableRadCombo(cboSalaryGroup, True)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    rntxtSalaryBasic.ReadOnly = False
                    txtheso.ReadOnly = False
                    txtRank.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtOrders.ReadOnly = False
                    Utilities.EnableRadCombo(cboSalaryLevel, False)
                    Utilities.EnableRadCombo(cboSalaryGroup, False)
                Case CommonMessage.STATE_DELETE
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryRank(DeleteSalaryRanks) Then
                            DeleteSalaryRanks = Nothing
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryRank(lstDeletes, "A", cboSalaryLevel.SelectedValue) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryRank(lstDeletes, "I", cboSalaryLevel.SelectedValue) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    End Using
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dic As New Dictionary(Of String, Control)
        'dic.Add("SAL_GROUP_ID", cboSalaryGroup)
        dic.Add("SAL_LEVEL_ID", cboSalaryLevel)
        dic.Add("RANK", txtRank)
        dic.Add("SALARY_BASIC", rntxtSalaryBasic)
        dic.Add("HE_SO", txtheso)
        dic.Add("REMARK", txtRemark)
        dic.Add("ORDERS", rntxtOrders)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
        Try
            Dim rep As New PayrollRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO

            comboBoxDataDTO.GET_SALARY_GROUP = True
            rep.GetComboboxData(comboBoxDataDTO)

            FillDropDownList(cboSalaryGroup, comboBoxDataDTO.LIST_SALARY_GROUP, "NAME", "ID", Common.Common.SystemLanguage, False)
            If (comboBoxDataDTO.LIST_SALARY_GROUP.Count > 0) Then
                cboSalaryGroup.SelectedIndex = 1
                cboSalaryGroup_SelectedIndexChanged(Nothing, Nothing)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryRank As New SalaryRankDTO
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of SalaryRankDTO)
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(New SalaryRankDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                               .SAL_LEVEL_ID = cboSalaryLevel.SelectedValue})
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_RANK) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                            Return
                        End If
                    End Using
                    DeleteSalaryRanks = lstDeletes
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryRank")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim salary As Decimal = 0
                        If cboSalaryLevel.SelectedValue = "" Or cboSalaryLevel.SelectedValue Is Nothing Then
                            ShowMessage("Bạn chưa chọn ngạch công chức", NotifyType.Warning)
                            Exit Sub
                        End If
                        Try
                            salary = CDec(Val(rntxtSalaryBasic.Text))
                        Catch ex As Exception
                            ShowMessage("Số tiền/ Hệ số phải là số", NotifyType.Warning)
                            Exit Sub
                        End Try
                        objSalaryRank.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                        objSalaryRank.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                        objSalaryRank.RANK = txtRank.Text.Trim
                        objSalaryRank.SALARY_BASIC = salary
                        objSalaryRank.HE_SO = CDec(Val(txtheso.Text))
                        objSalaryRank.REMARK = txtRemark.Text.Trim
                        objSalaryRank.ORDERS = If(String.IsNullOrEmpty(rntxtOrders.Text), 1, Decimal.Parse(rntxtOrders.Text))
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objSalaryRank.ACTFLG = "A"
                                    If rep.InsertSalaryRank(objSalaryRank, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objSalaryRank.ID = rgData.SelectedValue
                                    Dim repCheck As New CommonRepository
                                    Dim lstCheck As New List(Of Decimal)
                                    lstCheck.Add(objSalaryRank.ID)
                                    If repCheck.CheckExistIDTable(lstCheck, "PA_SALARY_RANK", "ID") Then
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        ClearControlValue(txtRank, txtRemark, rntxtOrders, txtheso, rntxtSalaryBasic)
                                        cboSalaryLevel.ClearValue()
                                        UpdateControlState()
                                        rgData.Rebind()
                                        Exit Sub
                                    End If
                                    If rep.ModifySalaryRank(objSalaryRank, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objSalaryRank.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
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
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho combobox cboSalaryGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboSalaryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalaryGroup.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboSalaryGroup.SelectedValue = "" Then
                'ShowMessage(Translate("Bạn chưa chọn thang bảng lương"), Utilities.NotifyType.Warning)
                cboSalaryLevel.Items.Clear()
                cboSalaryLevel.ClearCheckedItems()
                cboSalaryLevel.ClearSelection()
                cboSalaryLevel.Text = ""
                Return
            End If
            LoadComboSalaryLevel()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien custom validate cua cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim _validate As New SalaryRankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New PayrollRepository
                If cboSalaryLevel.SelectedValue = "" Then
                    args.IsValid = False
                Else
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        _validate.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                        _validate.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                        _validate.RANK = txtRank.Text.Trim()
                        _validate.ID = rgData.SelectedValue
                        args.IsValid = rep.ValidateSalaryRank(_validate)

                    Else
                        _validate.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
                        _validate.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                        _validate.RANK = txtRank.Text.Trim()
                        args.IsValid = rep.ValidateSalaryRank(_validate)
                    End If
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien custom validate cua cusSalaryLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusSalaryLevel_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusSalaryLevel.ServerValidate
        Dim rep As New PayrollRepository
        Dim validate As New SalaryLevelDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            validate.ID = cboSalaryLevel.SelectedValue
            validate.ACTFLG = "A"
            args.IsValid = rep.ValidateSalaryLevel(validate)
            If Not args.IsValid Then
                cboSalaryLevel.SelectedItem.Remove()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 22/08/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadComboSalaryLevel()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim obj As New SalaryLevelDTO
            Dim MaximumRows As Integer
            If (Not String.IsNullOrEmpty(cboSalaryGroup.SelectedValue)) Then
                obj.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            End If
            obj.ACTFLG_DB = "A"
            Using rep As New PayrollRepository
                Dim SalaryLevelByGroup As List(Of SalaryLevelDTO)
                If (Utilities.Account = "U") Then
                    SalaryLevelByGroup = rep.GetSalaryLevel_Unilever(obj, 0, Integer.MaxValue, MaximumRows)
                Else
                    SalaryLevelByGroup = rep.GetSalaryLevel(obj, 0, Integer.MaxValue, MaximumRows)
                End If
                FillDropDownList(cboSalaryLevel, SalaryLevelByGroup, "NAME", "ID", Common.Common.SystemLanguage, False)
                rgData.Rebind()
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class