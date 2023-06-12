Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Vehicle_Norm
    Inherits Common.CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            Me.MainToolBar = tbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Import)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            MainToolBar.Items(6).Text = Translate("Xuất file mẫu")
            CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = "~/Static/Images/Toolbar/import1.png"
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
    ''' 23/08/2017 09:00
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
    ''' 23/08/2017 09:00
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
                    EnableControlAll(True, txtVehicleName, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, btnFindOrg, txtNote)
                    EnableControlAll(False, rdFromDate, rdToDate, btnSearch)
                    rqOrgSetName.Enabled = True
                    reqEffect_month.Enabled = True
                    rgData.Enabled = False
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtVehicleName, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, btnFindOrg, txtNote)
                    EnableControlAll(True, rdFromDate, rdToDate, btnSearch)
                    rgData.Enabled = True
                    rqOrgSetName.Enabled = False
                    reqEffect_month.Enabled = False
                Case CommonMessage.STATE_EDIT
                    rgData.Enabled = False
                    EnableControlAll(True, txtVehicleName, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, btnFindOrg, txtNote)
                    EnableControlAll(False, rdFromDate, rdToDate, btnSearch)
                    rqOrgSetName.Enabled = True
                    reqEffect_month.Enabled = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteVehicleNorm(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            rgData.Rebind()
                            ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID)
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
            End Select
            If ctrlFindOrgPopup IsNot Nothing AndAlso phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            If isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                    ctrlFindOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                End If
                ctrlFindOrgPopup.IS_HadLoad = False
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            ElseIf isLoadPopup = 1 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            End If
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Dim id As Integer = 0

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData = New ComboBoxDataDTO
            cboVehicle_type.ClearSelection()
            ListComboData.GET_TYPE_SHOP = True
            rep.GetComboboxData(ListComboData)
            FillDropDownList(cboVehicle_type, ListComboData.LIST_TYPE_SHOP, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboVehicle_type.SelectedValue)
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objVehicle_Norm As New PA_Vehicle_NormDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID, rdFromDate, rdToDate)
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
                    ClearControlValue(rdFromDate, rdToDate)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Vehicle_Norm")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objVehicle_Norm.ORG_ID = hidOrgID.Value
                        objVehicle_Norm.TYPE_SHOP_ID = cboVehicle_type.SelectedValue
                        Dim firstDay = New DateTime(CDate(rdEffect_month.SelectedDate).Year, CDate(rdEffect_month.SelectedDate).Month, 1)
                        objVehicle_Norm.EFFECT_MONTH = firstDay
                        objVehicle_Norm.MONEY_NORM = rnMoney_Norm.Value
                        objVehicle_Norm.NOTE = txtNote.Text.Trim
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objVehicle_Norm.ID = 0

                                    If rep.ValidateVehicleNorm(objVehicle_Norm) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.InsertVehicleNorm(objVehicle_Norm) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("InsertView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objVehicle_Norm.ID = rgData.SelectedValue
                                    If rep.ValidateVehicleNorm(objVehicle_Norm) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.ModifyVehicleNorm(objVehicle_Norm) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID)
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportWorkInf()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
            End Select
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
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
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                    CurrentState = CommonMessage.STATE_DELETE
                End If
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
    ''' 23/08/2017 09:00
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New PA_Vehicle_NormDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, _filter)
            If IsDate(rdFromDate.SelectedDate) Then
                Dim firstDay = New DateTime(CDate(rdFromDate.SelectedDate).Year, CDate(rdFromDate.SelectedDate).Month, 1)
                _filter.FROM_DATE = firstDay
            End If
            If IsDate(rdToDate.SelectedDate) Then
                Dim firstDayOfMonth = New DateTime(CDate(rdToDate.SelectedDate).Year, CDate(rdToDate.SelectedDate).Month, 1)
                Dim lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1)
                _filter.TO_DATE = lastDayOfMonth
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_Vehicle_NormDTO)
            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetVehicleNorm(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetVehicleNorm(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        lstObj = rep.GetVehicleNorm(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                    Else
                        lstObj = rep.GetVehicleNorm(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                    End If

                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = lstObj
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlUpload_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            ClearControlValue(hidOrgID, txtVehicleName, txtCostCenter, txtCode_Vehicle)
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtVehicleName.Text = orgItem.NAME_VN
                txtCode_Vehicle.Text = orgItem.CODE
                txtCostCenter.Text = orgItem.COST_CENTER_CODE
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
        ClearControlValue(hidOrgID, txtVehicleName, txtCostCenter, txtCode_Vehicle)
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtVehicleName, txtCode_Vehicle, txtCostCenter, cboVehicle_type, rdEffect_month, rnMoney_Norm, txtNote, hidOrgID)
            If rgData.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)
                hidOrgID.Value = item.GetDataKeyValue("ORG_ID")
                txtVehicleName.Text = item.GetDataKeyValue("ORG_NAME")
                txtCode_Vehicle.Text = item.GetDataKeyValue("ORG_CODE")
                txtCostCenter.Text = item.GetDataKeyValue("ORG_COST_CENTER_CODE")
                cboVehicle_type.SelectedValue = item.GetDataKeyValue("TYPE_SHOP_ID")
                rdEffect_month.SelectedDate = item.GetDataKeyValue("EFFECT_MONTH")
                rnMoney_Norm.Value = CDec(item.GetDataKeyValue("MONEY_NORM"))
                txtNote.Text = item.GetDataKeyValue("NOTE")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtVehicleName_TextChanged(sender As Object, e As EventArgs) Handles txtVehicleName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtVehicleName.Text.Trim <> "" Then
                Dim List_org = rep.GetOrganizationLocationTreeView()
                Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtVehicleName.Text.Trim.ToUpper)).ToList
                If orgList.Count <= 0 Then
                    ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    ClearControlValue(hidOrgID, txtVehicleName, txtCostCenter, txtCode_Vehicle)
                ElseIf orgList.Count = 1 Then
                    hidOrgID.Value = orgList(0).ID
                    txtVehicleName.Text = orgList(0).NAME_VN
                    txtCode_Vehicle.Text = orgList(0).CODE
                    txtCostCenter.Text = orgList(0).COST_CENTER_CODE
                Else
                    List_Oganization_ID = (From p In orgList Select p.ID).ToList
                    btnFindOrg_Click(Nothing, Nothing)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    Private Sub Template_ImportWorkInf()
        Dim rep As New PayrollRepository
        Try
            Dim configPath As String = "Payroll\Import\Template_Import_TienXeHTCH.xls"

            Dim dsData As DataSet = rep.GET_VEHICLE_NORM_IMPORT()

            If File.Exists(System.IO.Path.Combine(Server.MapPath("ReportTemplates\" + configPath))) Then

                Using xls As New AsposeExcelCommon
                    Dim bCheck = ExportTemplate(configPath, dsData, Nothing, "Template_Import_TienXeHTCH" & Format(Date.Now, "yyyyMMdd"))

                End Using
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
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
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xlsx", Aspose.Cells.ContentDisposition.Attachment, New XlsSaveOptions(Aspose.Cells.SaveFormat.Xlsx))

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New PayrollRepository
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
            TableMapping(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(ds.Tables(0)) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('PA_VEHICLE_NORM')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(2).ColumnName = "ORG_NAME"
            dtTemp.Columns(8).ColumnName = "TYPE_SHOP"
            dtTemp.Columns(9).ColumnName = "ORG_ID"
            dtTemp.Columns(5).ColumnName = "EFFECT_MONTH"
            dtTemp.Columns(6).ColumnName = "MONEY_NORM"
            dtTemp.Columns(7).ColumnName = "NOTE"
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(2).Delete()

            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim rep As New PayrollRepository

            If dtLogs Is Nothing Then
                dtLogs = New DataTable("data")
                dtLogs.Columns.Add("STT", GetType(Integer))
                dtLogs.Columns.Add("ORG_NAME", GetType(String))
                dtLogs.Columns.Add("EFFECT_MONTH", GetType(String))
                dtLogs.Columns.Add("DISCIPTION", GetType(String))
            End If
            dtLogs.Clear()
            Dim DT_COPY = New DataTable
            DT_COPY = dtTemp.Copy()
            'XOA NHUNG DONG DU LIEU NULL ORG_ID
            Dim rowDel As DataRow
            For i As Integer = 3 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("ORG_ID").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next
            For Each rows As DataRow In dtTemp.Rows
                If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
                newRow = dtLogs.NewRow
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("EFFECT_MONTH") = rows("EFFECT_MONTH")


                If IsDBNull(rows("ORG_ID")) Then
                    rows("ORG_ID") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã Cửa hàng - Bắt buộc nhập,"
                    _error = False
                End If
                If IsDBNull(rows("NOTE")) Then
                    rows("NOTE") = ""
                End If
                ''Kiem tra trung du lieu trong file excel
                Dim Counts = DT_COPY.Select("ORG_ID =" & rows("ORG_ID") & "AND EFFECT_MONTH='" & rows("EFFECT_MONTH") & "'").Count
                If Counts > 1 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Trùng lặp dữ liệu trong file excel,"
                    _error = False
                End If

                If IsDBNull(rows("EFFECT_MONTH")) OrElse rows("EFFECT_MONTH") = "" Then
                    rows("EFFECT_MONTH") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng hiệu lực - Bắt buộc nhập,"
                    _error = False
                Else
                    Dim arr As String() = rows("EFFECT_MONTH").Split("/")
                    Dim C_effect_month = "01/" + arr(0) + "/" + arr(1)
                    'Dim firstDay = New DateTime(arr(1), CInt(arr(0)), 1)
                    If IsDate(C_effect_month) Then
                        rows("EFFECT_MONTH") = "01/" + arr(0) + "/" + arr(1)
                    Else
                        rows("EFFECT_MONTH") = "NULL"
                        newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng hiệu lực - Không đúng định dạng,"
                        _error = False
                    End If
                End If
                'Kiem tra trung du lieu duoi db
                Dim objVehicle_Norm As New PA_Vehicle_NormDTO
                objVehicle_Norm.EFFECT_MONTH = CDate(rows("EFFECT_MONTH"))
                objVehicle_Norm.ORG_ID = CDec(rows("ORG_ID"))
                objVehicle_Norm.ID = 0
                If rep.ValidateVehicleNorm(objVehicle_Norm) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Dữ liệu đã tồn tại,"
                    _error = False
                End If
                If IsDBNull(rows("MONEY_NORM")) OrElse rows("MONEY_NORM") = "" Then
                    rows("MONEY_NORM") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Định mức tiền xe - Bắt buộc nhập,"
                    _error = False
                Else
                    If Not IsNumeric(rows("MONEY_NORM")) Then
                        rows("MONEY_NORM") = "NULL"
                        newRow("DISCIPTION") = newRow("DISCIPTION") + "Định mức tiền xe - Không đúng định dạng,"
                        _error = False
                    End If
                End If

                If _error = False Then
                    dtLogs.Rows.Add(newRow)
                    _error = True
                End If
                count += 1
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class