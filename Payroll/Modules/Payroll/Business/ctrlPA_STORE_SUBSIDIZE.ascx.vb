Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_STORE_SUBSIDIZE
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
    Property dtData_Import As DataTable
        Get
            If ViewState(Me.ID & "_dtData_Import") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("BRAND_ID", GetType(String))
                dt.Columns.Add("BRAND_RATE", GetType(String))
                dt.Columns.Add("TARGET_PLAN", GetType(String))
                dt.Columns.Add("REVENUE_MIN", GetType(String))
                dt.Columns.Add("LESS_REVENUE", GetType(String))
                dt.Columns.Add("THAN_REVENUE", GetType(String))
                dt.Columns.Add("BENEFIT_VALUE", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData_Import") = dt
            End If
            Return ViewState(Me.ID & "_dtData_Import")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData_Import") = value
        End Set
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
                    EnableControlAll(True, rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote)
                    EnableControlAll(False, rdFromDate, rdToDate, btnSearch)
                    rqOrgSetName.Enabled = True
                    rgData.Enabled = False
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote)
                    EnableControlAll(True, rdFromDate, rdToDate, btnSearch)
                    rgData.Enabled = True
                    rqOrgSetName.Enabled = False
                Case CommonMessage.STATE_EDIT
                    rgData.Enabled = False
                    EnableControlAll(True, rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote)
                    EnableControlAll(False, rdFromDate, rdToDate, btnSearch)
                    rqOrgSetName.Enabled = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeletePA_STORE_SUBSIDIZE(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            rgData.Rebind()
                            ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, hidOrgID)
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
            ListComboData.GET_TYPE_SHOP = True
            rep.GetComboboxData(ListComboData)
            'FillDropDownList(ListComboData.LIST_TYPE_SHOP, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboVehicle_type.SelectedValue)
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
        Dim objData As New PA_STORE_SUBSIDIZEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, txtNote, hidOrgID, rdFromDate, rdToDate)
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
                        objData.ORG_ID = hidOrgID.Value
                        objData.BRAND_ID = hidBrandID.Value
                        objData.BRAND_RATE = rnRate.Value
                        objData.TARGET_PLAN = rnTARGET_PLAN.Value
                        objData.REVENUE_MIN = rnREVENUE_MIN.Value
                        objData.LESS_REVENUE = rnLESS_REVENUE.Value
                        objData.THAN_REVENUE = rnTHAN_REVENUE.Value
                        objData.BENEFIT_VALUE = rnBENEFIT_VALUE.Value
                        objData.EFFECT_DATE = rdpEFFECT_DATE.SelectedDate
                        objData.NOTE = txtNote.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objData.ID = 0

                                    If rep.ValidatePA_STORE_SUBSIDIZE(objData) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.InsertPA_STORE_SUBSIDIZE(objData) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("InsertView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, hidOrgID)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objData.ID = rgData.SelectedValue
                                    If rep.ValidatePA_STORE_SUBSIDIZE(objData) Then
                                        ShowMessage(Translate("Dữ liệu đã tồn tại!"), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                    If rep.ModifyPA_STORE_SUBSIDIZE(objData) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, hidOrgID)
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
                    ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, hidOrgID)
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
        Dim _filter As New PA_STORE_SUBSIDIZEDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, _filter)
            If IsDate(rdFromDate.SelectedDate) Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If IsDate(rdToDate.SelectedDate) Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstObj As New List(Of PA_STORE_SUBSIDIZEDTO)
            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetPA_STORE_SUBSIDIZE(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetPA_STORE_SUBSIDIZE(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        lstObj = rep.GetPA_STORE_SUBSIDIZE(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                    Else
                        lstObj = rep.GetPA_STORE_SUBSIDIZE(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData_Import = dtData_Import.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Rows
                If String.IsNullOrEmpty(rows("ORG_ID").ToString) AndAlso String.IsNullOrEmpty(rows("EFFECT_DATE").ToString) AndAlso String.IsNullOrEmpty(rows("TARGET_PLAN").ToString) Then Continue For
                Dim newRow As DataRow = dtData_Import.NewRow
                newRow("ORG_ID") = rows("ORG_ID")
                newRow("BRAND_RATE") = rows("BRAND_RATE")
                newRow("TARGET_PLAN") = rows("TARGET_PLAN")
                newRow("REVENUE_MIN") = rows("REVENUE_MIN")
                newRow("LESS_REVENUE") = rows("LESS_REVENUE")
                newRow("THAN_REVENUE") = rows("THAN_REVENUE")
                newRow("BENEFIT_VALUE") = rows("BENEFIT_VALUE")
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("NOTE") = rows("NOTE")
                dtData_Import.Rows.Add(newRow)
            Next
            dtData_Import.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData_Import.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New PayrollStoreProcedure
                If sp.IMPORT_PA_STORE_SUBSIDIZE(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim rep As New PayrollRepository
            ClearControlValue(hidOrgID, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote)
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            Dim _filter As New PA_STORE_SUBSIDIZEDTO
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtORG_NAME.Text = e.CurrentText
                _filter.ORG_ID = e.CurrentValue
                _filter.EFFECT_DATE = CDate(rdpEFFECT_DATE.SelectedDate)
                Dim Brand = rep.Get_Brand_Name(_filter)

                If Brand IsNot Nothing Then
                    txtBrand_NAME.Text = Brand.BRAND_NAME
                    hidBrandID.Value = Brand.BRAND_ID
                    _filter.BRAND_ID = Brand.BRAND_ID
                    Dim Rate = rep.Get_Rate(_filter)
                    If Rate IsNot Nothing Then
                        rnRate.Value = Rate.BRAND_RATE
                    End If
                End If
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
        ClearControlValue(hidOrgID, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote)
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgData.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PayrollRepository
        Try

            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rdpEFFECT_DATE, txtORG_NAME, txtBrand_NAME, rnRate, rnTARGET_PLAN, rnBENEFIT_VALUE, rnREVENUE_MIN, rnLESS_REVENUE, rnTHAN_REVENUE, txtNote, hidOrgID)
            If rgData.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)
                hidOrgID.Value = item.GetDataKeyValue("ORG_ID")
                hidBrandID.Value = item.GetDataKeyValue("BRAND_ID")
                txtBrand_NAME.Text = item.GetDataKeyValue("BRAND_NAME")
                txtORG_NAME.Text = item.GetDataKeyValue("ORG_NAME")
                rnRate.Value = If(IsNumeric(item.GetDataKeyValue("BRAND_RATE")), CDec(item.GetDataKeyValue("BRAND_RATE")), Nothing)
                rnTARGET_PLAN.Value = If(IsNumeric(item.GetDataKeyValue("TARGET_PLAN")), CDec(item.GetDataKeyValue("TARGET_PLAN")), Nothing)
                rnREVENUE_MIN.Value = If(IsNumeric(item.GetDataKeyValue("REVENUE_MIN")), CDec(item.GetDataKeyValue("REVENUE_MIN")), Nothing)
                rnLESS_REVENUE.Value = If(IsNumeric(item.GetDataKeyValue("LESS_REVENUE")), CDec(item.GetDataKeyValue("LESS_REVENUE")), Nothing)
                rnTHAN_REVENUE.Value = If(IsNumeric(item.GetDataKeyValue("THAN_REVENUE")), CDec(item.GetDataKeyValue("THAN_REVENUE")), Nothing)
                rnBENEFIT_VALUE.Value = If(IsNumeric(item.GetDataKeyValue("BENEFIT_VALUE")), CDec(item.GetDataKeyValue("BENEFIT_VALUE")), Nothing)
                rdpEFFECT_DATE.SelectedDate = If(IsDate(item.GetDataKeyValue("EFFECT_DATE")), CDate(item.GetDataKeyValue("EFFECT_DATE")), Nothing)
                txtNote.Text = item.GetDataKeyValue("NOTE")
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

    Private Sub Template_ImportWorkInf()
        Dim rep As New PayrollRepository
        Try
            Dim configPath As String = "Payroll\Import\Template_Import_ThietLapkhungthuong_HTCH.xls"

            Dim dsData As DataSet = rep.GET_PA_STORE_SUBSIDIZE_IMPORT()

            If File.Exists(System.IO.Path.Combine(Server.MapPath("ReportTemplates\" + configPath))) Then

                Using xls As New AsposeExcelCommon
                    Dim bCheck = ExportTemplate(configPath, dsData, Nothing, "Template_Import_ThietLapkhungthuong_HTCH" & Format(Date.Now, "yyyyMMdd"))

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
    'Private Sub Import_Data()
    '    Try
    '        Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
    '        Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
    '        Dim fileName As String
    '        Dim savepath = Context.Server.MapPath(tempPath)
    '        Dim rep As New PayrollRepository
    '        '_mylog = LogHelper.GetUserLog
    '        Dim ds As New DataSet
    '        If countFile > 0 Then
    '            Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
    '            fileName = System.IO.Path.Combine(savepath, file.FileName)
    '            file.SaveAs(fileName, True)
    '            Using ep As New ExcelPackage
    '                ds = ep.ReadExcelToDataSet(fileName, False)
    '            End Using
    '        End If
    '        If ds Is Nothing Then
    '            Exit Sub
    '        End If
    '        TableMapping(ds.Tables(0))
    '        If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
    '            Dim DocXml As String = String.Empty
    '            Dim sw As New StringWriter()
    '            If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
    '                ds.Tables(0).WriteXml(sw, False)
    '                DocXml = sw.ToString
    '                If rep.IMPORT_DATA_VEHICLE_NORM_IMPORT(ds.Tables(0)) Then
    '                    ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
    '                    rgData.Rebind()
    '                Else
    '                    ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
    '                End If
    '            End If
    '        Else
    '            Session("EXPORTREPORT") = dtLogs
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('PA_VEHICLE_NORM')", True)
    '            ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)

    '        End If
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
    '    End Try
    'End Sub
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
    'Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
    '    Try
    '        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
    '        dtTemp.Columns(2).ColumnName = "ORG_NAME"
    '        dtTemp.Columns(8).ColumnName = "TYPE_SHOP"
    '        dtTemp.Columns(9).ColumnName = "ORG_ID"
    '        dtTemp.Columns(5).ColumnName = "EFFECT_MONTH"
    '        dtTemp.Columns(6).ColumnName = "MONEY_NORM"
    '        dtTemp.Columns(7).ColumnName = "NOTE"
    '        dtTemp.Rows(0).Delete()
    '        dtTemp.Rows(1).Delete()
    '        dtTemp.Rows(2).Delete()

    '        ' add Log
    '        Dim _error As Boolean = True
    '        Dim count As Integer
    '        Dim newRow As DataRow
    '        Dim rep As New PayrollRepository

    '        If dtLogs Is Nothing Then
    '            dtLogs = New DataTable("data")
    '            dtLogs.Columns.Add("STT", GetType(Integer))
    '            dtLogs.Columns.Add("ORG_NAME", GetType(String))
    '            dtLogs.Columns.Add("EFFECT_MONTH", GetType(String))
    '            dtLogs.Columns.Add("DISCIPTION", GetType(String))
    '        End If
    '        dtLogs.Clear()
    '        Dim DT_COPY = New DataTable
    '        DT_COPY = dtTemp.Copy()
    '        'XOA NHUNG DONG DU LIEU NULL ORG_ID
    '        Dim rowDel As DataRow
    '        For i As Integer = 3 To dtTemp.Rows.Count - 1
    '            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
    '            rowDel = dtTemp.Rows(i)
    '            If rowDel("ORG_ID").ToString.Trim = "" Then
    '                dtTemp.Rows(i).Delete()
    '            End If
    '        Next
    '        For Each rows As DataRow In dtTemp.Rows
    '            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
    '            newRow = dtLogs.NewRow
    '            newRow("ORG_NAME") = rows("ORG_NAME")
    '            newRow("EFFECT_MONTH") = rows("EFFECT_MONTH")


    '            If IsDBNull(rows("ORG_ID")) Then
    '                rows("ORG_ID") = "NULL"
    '                newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã Cửa hàng - Bắt buộc nhập,"
    '                _error = False
    '            End If
    '            If IsDBNull(rows("NOTE")) Then
    '                rows("NOTE") = ""
    '            End If
    '            ''Kiem tra trung du lieu trong file excel
    '            Dim Counts = DT_COPY.Select("ORG_ID =" & rows("ORG_ID") & "AND EFFECT_MONTH='" & rows("EFFECT_MONTH") & "'").Count
    '            If Counts > 1 Then
    '                newRow("DISCIPTION") = newRow("DISCIPTION") + "Trùng lặp dữ liệu trong file excel,"
    '                _error = False
    '            End If

    '            If IsDBNull(rows("EFFECT_MONTH")) OrElse rows("EFFECT_MONTH") = "" Then
    '                rows("EFFECT_MONTH") = "NULL"
    '                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng hiệu lực - Bắt buộc nhập,"
    '                _error = False
    '            Else
    '                Dim arr As String() = rows("EFFECT_MONTH").Split("/")
    '                Dim C_effect_month = "01/" + arr(0) + "/" + arr(1)
    '                'Dim firstDay = New DateTime(arr(1), CInt(arr(0)), 1)
    '                If IsDate(C_effect_month) Then
    '                    rows("EFFECT_MONTH") = "01/" + arr(0) + "/" + arr(1)
    '                Else
    '                    rows("EFFECT_MONTH") = "NULL"
    '                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tháng hiệu lực - Không đúng định dạng,"
    '                    _error = False
    '                End If
    '            End If
    '            'Kiem tra trung du lieu duoi db
    '            Dim objVehicle_Norm As New PA_STORE_SUBSIDIZEDTO
    '            objVehicle_Norm.EFFECT_DATE = CDate(rows("EFFECT_DATE"))
    '            objVehicle_Norm.ORG_ID = CDec(rows("ORG_ID"))
    '            objVehicle_Norm.ID = 0
    '            If rep.ValidatePA_STORE_SUBSIDIZE(objVehicle_Norm) Then
    '                newRow("DISCIPTION") = newRow("DISCIPTION") + "Dữ liệu đã tồn tại,"
    '                _error = False
    '            End If
    '            If IsDBNull(rows("MONEY_NORM")) OrElse rows("MONEY_NORM") = "" Then
    '                rows("MONEY_NORM") = "NULL"
    '                newRow("DISCIPTION") = newRow("DISCIPTION") + "Định mức tiền xe - Bắt buộc nhập,"
    '                _error = False
    '            Else
    '                If Not IsNumeric(rows("MONEY_NORM")) Then
    '                    rows("MONEY_NORM") = "NULL"
    '                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Định mức tiền xe - Không đúng định dạng,"
    '                    _error = False
    '                End If
    '            End If

    '            If _error = False Then
    '                dtLogs.Rows.Add(newRow)
    '                _error = True
    '            End If
    '            count += 1
    '        Next

    '        dtTemp.AcceptChanges()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtEmpTable As New DataTable
        Dim rep As New PayrollRepository

        Dim rep2 As New Attendance.AttendanceBusiness.AttendanceBusinessClient
        Dim store As New PayrollStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData_Import.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim lstEmp As New List(Of String)
            dtError = dtData_Import.Clone
            Dim iRow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData_Import.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa chọn nhãn hàng"
                ImportValidate.EmptyValue("ORG_ID", row, rowError, isError, sError)

                sError = "Chưa nhập ngày hiệu lực"
                ImportValidate.EmptyValue("EFFECT_DATE", row, rowError, isError, sError)

                sError = "Chưa nhập target doanh thu"
                ImportValidate.EmptyValue("TARGET_PLAN", row, rowError, isError, sError)

                If Not IsDBNull(row("BRAND_RATE")) AndAlso Not String.IsNullOrEmpty(row("BRAND_RATE")) Then
                    sError = "Chỉ được nhập số"
                    ImportValidate.IsValidNumber("BRAND_RATE", row, rowError, isError, sError)
                End If




                If Not IsDBNull(row("ORG_ID")) AndAlso Not IsDBNull(row("EFFECT_DATE")) Then
                    If IsNumeric(row("ORG_ID")) Then
                        Dim _filter As New PA_STORE_SUBSIDIZEDTO
                        _filter.ORG_ID = CDec(row("ORG_ID"))
                        _filter.EFFECT_DATE = CDate(row("EFFECT_DATE"))
                        sError = "không tồn tại"
                        Dim Brand = rep.Get_Brand_Name(_filter)
                        If Brand IsNot Nothing Then
                            row("BRAND_ID") = Brand.BRAND_ID
                            '_filter.BRAND_ID = Brand.BRAND_ID
                            'Dim Rate = rep.Get_Rate(_filter)
                            'If Rate IsNot Nothing Then
                            '    row("BRAND_RATE") = Rate.BRAND_RATE
                            'Else
                            '    ImportValidate.IsValidTime("BRAND_RATE", row, rowError, isError, sError)
                            'End If
                        Else
                            ImportValidate.IsValidTime("BRAND_ID", row, rowError, isError, sError)
                        End If




                        'Dim dtData = store.GET_PERIOD_BY_MONTH(CDec(row("YEAR").ToString), CDec(row("MONTH").ToString))
                        'If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                        '    row("PERIOD_ID") = dtData.Rows(0)("ID")
                        'Else
                        '    ImportValidate.IsValidTime("MONTH", row, rowError, isError, sError)
                        'End If
                    End If
                End If


                If isError Then
                    ''rowError("ID") = iRow
                    If IsDBNull(rowError("BRAND_NAME")) Then
                        rowError("BRAND_NAME") = row("BRAND_NAME").ToString
                    End If
                    If IsDBNull(rowError("YEAR")) Then
                        rowError("YEAR") = row("YEAR").ToString
                    End If
                    If IsDBNull(rowError("MONTH")) Then
                        rowError("MONTH") = row("MONTH").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("DTTD_ECD_ERR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_DTTD_ECD_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            If isError Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region


End Class