Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_LabourProtection
    Inherits Common.CommonView
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

#End Region

#Region "Page"
    ''' <summary>
    ''' Set thuoc tinh grid, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()
            rgGridData.SetFilter()
            rgGridData.AllowCustomPaging = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load va khoi tao cac control, goi ham Initcontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    ''' <summary>
    ''' set ngon ngu cho cac label va columnname grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            CurrentState = CommonMessage.STATE_NORMAL
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtNAME)
            dic.Add("UNIT_PRICE", nmUNIT_PRICE)
            dic.Add("SDESC", txtSDESC)
            Utilities.OnClientRowSelectedChanged(rgGridData, dic)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao menu toolbar (set cac nut su dung), set trang thai page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        'Dim startTime As DateTime = DateTime.UtcNow 
        'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() 
        'Try
        '    Me.ctrlMessageBox.Listener = Me
        '    Me.MainToolBar = tbarOtherLists
        '    Common.Common.BuildToolbar(Me.MainToolBar,
        '                               ToolbarItem.Create, ToolbarItem.Edit,
        '                               ToolbarItem.Save, ToolbarItem.Cancel,
        '                               ToolbarItem.Active, ToolbarItem.Deactive,
        '                               ToolbarItem.Export, ToolbarItem.Delete)
        '    CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        '    Refresh("")
        '    Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        '    CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        '    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "") 
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        '    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "") 
        'End Try
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists ' gan thuoc tinh MainToolBar(CommonView)
            'Tao menu toolbar, add cac nut them moi, sua, luu, huy, xoa, ap dung, ngung ap dung, xuat excel
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' reset page sau khi thuc hien update hay cancel thanh cong, set trang thai page, trang thai control
    ''' </summary>
    ''' <param name="Message">Xac dinh phuong thuc xu ly la update, insert hay cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridData.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        ClearControlValue(txtCode, txtNAME, txtSDESC, nmUNIT_PRICE)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGridData.CurrentPageIndex = 0
                        rgGridData.MasterTableView.SortExpressions.Clear()
                        rgGridData.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                        ClearControlValue(txtCode, txtNAME, txtSDESC, nmUNIT_PRICE)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgGridData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(txtCode, txtNAME, txtSDESC)
                End Select
            End If
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    ''' <summary>
    ''' set trang thai cua cac control, menu theo trang thai page, gen code moi khi them moi
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    EnableControlAll(False, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    'ClearControlValue(txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    ClearControlValue(txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    EnableControlAll(True, txtNAME, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    Using s As New ProfileRepository
                        txtCode.Text = s.AutoGenCode("BHLD", "HU_LABOURPROTECTION", "CODE")
                    End Using
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    EnableControlAll(True, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_DELETE
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    ClearControlValue(txtNAME, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    EnableControlAll(False, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    txtCode.ReadOnly = True
                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    EnableControlAll(True, txtCode, txtNAME, nmUNIT_PRICE, txtSDESC)
                    txtCode.ReadOnly = True
            End Select

            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' event click cua cac nut: them, sua, xoa, ap dung, ngung ap dung, luu
    ''' set lai trang thai control, trang thai page khi event thuc hien xong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtNAME.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGridData.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        Call SaveData()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridData')")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE, CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                    Select Case CType(e.Item, RadToolBarButton).CommandName
                        Case CommonMessage.TOOLBARITEM_DELETE
                            CurrentState = CommonMessage.ACTION_DELETED
                        Case CommonMessage.TOOLBARITEM_ACTIVE
                            CurrentState = CommonMessage.ACTION_ACTIVE
                        Case CommonMessage.TOOLBARITEM_DEACTIVE
                            CurrentState = CommonMessage.ACTION_DEACTIVE
                    End Select
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn thực hiện không ?")
                    ctrlMessageBox.ActionName = CType(e.Item, RadToolBarButton).CommandName
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim MaximumRows As Integer = Integer.MaxValue
                    Using Sql As New ProfileRepository
                        Using xls As New ExcelCommon
                            Dim dtData = LoadDataGrid(True)
                            If dtData.Rows.Count > 0 Then
                                rgGridData.ExportExcel(Server, Response, dtData, "DSBaoHoLaoDong")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            End If
                        End Using
                    End Using
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        LoadDataGrid()
    End Sub

    ''' <summary>
    ''' event Yes/No cua popup message
    ''' process xoa, ap dung, ngung ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileRepository
            Dim lst As New List(Of Decimal)
            For Each item As GridDataItem In rgGridData.SelectedItems
                lst.Add(Utilities.ObjToDecima(item.GetDataKeyValue("ID")))
            Next
            Try
                If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    Call DeleteData(lst)
                ElseIf e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    If rep.ActiveLabourProtection(lst, "A") Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgGridData.Rebind()
                        ClearAllData()
                    End If
                ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                    If rep.ActiveLabourProtection(lst, "I") Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgGridData.Rebind()
                        ClearAllData()
                    End If
                End If
                rep.Dispose()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New LabourProtectionDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgGridData.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateLabourProtection(_validate)

            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateLabourProtection(_validate)
            End If
            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("BHLD", "HU_LABOURPROTECTION", "CODE") ' Gen ma cap nhan su moi khi them moi
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Function & Sub"
    ''' <summary>
    ''' sub process luu du lieu khi them, sua
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim gId As Decimal

            If hidID.Value.ToString <> "" Then
                gId = hidID.Value
            End If

            Dim rep As New ProfileRepository
            Dim objData As New LabourProtectionDTO

            objData.NAME = txtNAME.Text.Trim
            objData.CODE = txtCode.Text.Trim
            objData.UNIT_PRICE = nmUNIT_PRICE.Value
            objData.SDESC = txtSDESC.Text.Trim
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.InsertLabourProtection(objData, gId) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgGridData.Rebind()
                        ClearAllData()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    objData.ID = Utilities.ObjToDecima(rgGridData.SelectedValue)
                    Dim validate As New LabourProtectionDTO
                    validate.ID = Utilities.ObjToDecima(rgGridData.SelectedValue)
                    If rep.ValidateLabourProtection(validate) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    If rep.ModifyLabourProtection(objData, gId) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgGridData.Rebind()
                        ClearAllData()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' sub process xoa du lieu
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal lst As List(Of Decimal))
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New ProfileRepository
            If rep.DeleteLabourProtection(lst) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Refresh()
                rgGridData.Rebind()
                ClearAllData()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custorms"
    '''<createby>HongDX</createby>
    ''' <summary>
    ''' Clear data trên các control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAllData()
        ClearControlValue(txtCode, txtNAME, txtSDESC, nmUNIT_PRICE)
        rgGridData.SelectedIndexes.Clear()
    End Sub
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="isFull">trang thai load full du lieu hoac load phan trang, filter</param>
    ''' <returns>datatable</returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer = 0
            Dim lstSource As List(Of LabourProtectionDTO)
            Dim obj As New LabourProtectionDTO
            SetValueObjectByRadGrid(rgGridData, obj)
            Dim Sorts As String = rgGridData.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetLabourProtection(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GetLabourProtection(obj, rgGridData.CurrentPageIndex, rgGridData.PageSize, MaximumRows)
                End If
                rgGridData.DataSource = lstSource
                rgGridData.MasterTableView.VirtualItemCount = MaximumRows
                rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            Else
                Return rep.GetLabourProtection(obj, 0, Integer.MaxValue, 0, "CREATED_DATE").ToTable()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class