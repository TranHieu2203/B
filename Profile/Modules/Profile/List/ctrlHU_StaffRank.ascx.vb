Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_StaffRank
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"


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
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
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

        End Try

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
            Me.MainToolBar = tbarMain ' gan thuoc tinh MainToolBar(CommonView)
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
        'Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else

                Select Case Message
                    Case "UpdateView"

                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"

                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        'valSum.Visible = False
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New StaffRankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter) ' Lấy giá trị filter trên radgrid gán vào object
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetStaffRank(_filter, Sorts).ToTable() 'Get data DB va co sap xep
                Else
                    Return rep.GetStaffRank(_filter).ToTable() 'Get data DB va khong sap xep
                End If
            Else
                Dim StaffRanks As List(Of StaffRankDTO)
                If Sorts IsNot Nothing Then
                    StaffRanks = rep.GetStaffRank(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts) 'Get data DB phan trang va co sap xep
                Else
                    StaffRanks = rep.GetStaffRank(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows) 'Get data DB phan trang va ko sap xep
                End If
                Dim dt As DataTable = StaffRanks.ToTable
                rgMain.DataSource = StaffRanks
                rgMain.MasterTableView.VirtualItemCount = MaximumRows
                rgMain.CurrentPageIndex = rgMain.MasterTableView.CurrentPageIndex
            End If
            rep.Dispose()
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
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ' Cap nhat trang thai enable cho cac control khi an nut them moi, Textbox ma visiable
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, txtCode, txtName, nmlever, txtRemark)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_NORMAL
                    ' cap nhat trang thai disable cac control khi o trang thai xem
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, txtCode, txtName, nmlever, txtRemark)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_EDIT
                    ' Cap nhat trang thai enable cho cac control khi an nut sua, Textbox ma visiable
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, txtCode, txtName, nmlever, txtRemark)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_DELETE

                    Dim lstDeletes As New List(Of StaffRankDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New StaffRankDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteStaffRank(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                        ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                        rgMain.SelectedIndexes.Clear()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_ACTIVE
                    ActiveAndDeactiveRank("A", rep)
                    ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                    rgMain.SelectedIndexes.Clear()
                Case CommonMessage.STATE_DEACTIVE
                    ActiveAndDeactiveRank("I", rep)
                    ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                    rgMain.SelectedIndexes.Clear()
            End Select
            rep.Dispose()
            'txtName.Focus()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' Creatby: HongDX
    ''' Cdate: 26-06-2017
    ''' <summary>
    ''' Ham xu ly update trang thai Ap dung hay Ngung Ap dung 
    ''' Duoc update vao Feild: ACTFLG (= A (Ap dung), = I (ngung Ap dung))
    ''' </summary>
    ''' <param name="state">Trang thai = A hoac = I</param>
    ''' <param name="rep">Class goi ham ActiveStaffRank</param>
    ''' <remarks></remarks>
    Private Sub ActiveAndDeactiveRank(ByVal state As String, ByVal rep As ProfileRepository)
        Try
            Dim lstDeletes As New List(Of Decimal)
            For idx = 0 To rgMain.SelectedItems.Count - 1
                Dim item As GridDataItem = rgMain.SelectedItems(idx)
                lstDeletes.Add(item.GetDataKeyValue("ID"))
            Next
            'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_STAFF_RANK) Then
            '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
            '    Return
            'End If
            ' Update trang thai list duoc chon tu grid
            If rep.ActiveStaffRank(lstDeletes, state) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                CurrentState = CommonMessage.STATE_NORMAL
                rgMain.Rebind()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
            End If
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("LEVER", nmlever)
            dic.Add("NAME", txtName)
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
        Dim objStaffRank As New StaffRankDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    rgMain.SelectedIndexes.Clear()
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtName, nmlever, txtName, txtRemark)
                    txtCode.Text = rep.AutoGenCode("CBNS", "HU_STAFF_RANK", "CODE")
                    txtName.Focus()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    ' event sua
                    txtName.Focus()
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    ' event xoa
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ' Xoa nhieu dong tren luoi
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    ' Kiem tra ton tai ban ghi truoc khi xoa
                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_STAFF_RANK) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    ' event xuat excel
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "StaffRank")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    ' event Ap Dung
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    ' event Ngung Ap dung
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    ' event luu du lieu
                    If Page.IsValid Then
                        objStaffRank.CODE = txtCode.Text
                        objStaffRank.NAME = txtName.Text.Trim
                        objStaffRank.REMARK = txtRemark.Text.Trim
                        objStaffRank.LEVER = nmlever.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                ' Luu du lieu khi them moi
                                Dim validateStaff As New StaffRankDTO
                                validateStaff.CODE = txtCode.Text
                                If Not rep.ValidateStaffRank(validateStaff) Then
                                    txtCode.Text = rep.AutoGenCode("CBNS", "HU_STAFF_RANK", "CODE")
                                End If
                                objStaffRank.ACTFLG = "A"
                                If rep.InsertStaffRank(objStaffRank, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                                    rgMain.SelectedIndexes.Clear()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                ' Luu du lieu khi sua
                                Dim ValidateStaffRank As New StaffRankDTO
                                ValidateStaffRank.ID = rgMain.SelectedValue
                                'ValidateStaffRank.CODE = txtCode.Text
                                If rep.ValidateStaffRank(ValidateStaffRank) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                                    rgMain.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                objStaffRank.ID = rgMain.SelectedValue
                                If rep.ModifyStaffRank(objStaffRank, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objStaffRank.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(txtCode, txtName, txtRemark, nmlever)
                                    rgMain.SelectedIndexes.Clear()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ' event huy
                    ClearControlValue(txtCode, txtName, nmlever, txtName, txtRemark)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()
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
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
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
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
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
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New StaffRankDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateStaffRank(_validate) 'Check ton tai
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateStaffRank(_validate) ' Check ton tai
            End If
            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("CBNS", "HU_STAFF_RANK", "CODE") ' Gen ma cap nhan su moi khi them moi
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
#End Region

End Class