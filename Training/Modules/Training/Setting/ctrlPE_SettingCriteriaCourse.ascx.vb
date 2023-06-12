Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Imports WebAppLog

Public Class ctrlPE_SettingCriteriaCourse
    Inherits Common.CommonView
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Training\Modules\Training\Setting" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Public Property lstDeleteID As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstDeleteID")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstDeleteID") = value
        End Set
    End Property

    Property lstSettingCriteriaDetail As List(Of SettingCriteriaDetailDTO)
        Get
            Return ViewState(Me.ID & "_lstSettingCriteriaDetail")
        End Get
        Set(ByVal value As List(Of SettingCriteriaDetailDTO))
            ViewState(Me.ID & "_lstSettingCriteriaDetail") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgMain.AllowCustomPaging = True
            rgMain.SetFilter()
            InitControl()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                        lstSettingCriteriaDetail.Clear()
                        rgCriteria.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                        lstSettingCriteriaDetail.Clear()
                        rgCriteria.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                        lstSettingCriteriaDetail.Clear()
                        rgCriteria.Rebind()
                    Case ""
                        'cboCourse.AutoPostBack = False
                        'cboCriteria.AutoPostBack = False
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New TrainingRepository
        Dim _filter As New SettingCriteriaCourseDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then

                If Sorts IsNot Nothing Then
                    Return rep.GET_SETTING_CRITERIA_COURSE(_filter, Sorts).ToTable()
                Else
                    Return rep.GET_SETTING_CRITERIA_COURSE(_filter, Sorts).ToTable()
                End If
            Else
                Dim SETTING_CRITERIA_COURSE As List(Of SettingCriteriaCourseDTO)
                If Sorts IsNot Nothing Then
                    SETTING_CRITERIA_COURSE = rep.GET_SETTING_CRITERIA_COURSE(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    SETTING_CRITERIA_COURSE = rep.GET_SETTING_CRITERIA_COURSE(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = SETTING_CRITERIA_COURSE
            End If
            rep.Dispose()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function


    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    rgCriteria.Enabled = True
                    cboCourse.AutoPostBack = True
                    cboCriteria.AutoPostBack = True
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    rgCriteria.Enabled = False
                    cboCourse.AutoPostBack = False
                    cboCriteria.AutoPostBack = False
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    rgCriteria.Enabled = True
                    cboCourse.AutoPostBack = True
                    cboCriteria.AutoPostBack = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DELETE_SETTING_CRITERIA_COURSE(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rep.Dispose()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New TrainingStoreProcedure
        Try
            Dim dtData As DataTable
            dtData = rep.GET_TR_COURSE(True)
            FillRadCombobox(cboCourse, dtData, "NAME", "ID")
            dtData = rep.GET_TR_CRITERIA(True)
            FillRadCombobox(cboCriteria, dtData, "NAME", "ID")
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EFFECT_FROM", rdEffectFrom)
            dic.Add("EFFECT_TO", rdEffectTo)
            dic.Add("REMARK", txtRemark)
            dic.Add("TR_COURSE_ID", cboCourse)
            dic.Add("TR_CRITERIA_GROUP_ID", cboCriteria)
            dic.Add("SCALE_POINT", rnScale_Point)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                lstSettingCriteriaDetail.Clear()
                rgCriteria.Rebind()
            ElseIf e.ActionName = "REMOVE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each selected As String In lstDeleteID
                    lstSettingCriteriaDetail.RemoveAll(Function(x) x.CRITERIA_ID = selected)
                Next
                rgCriteria.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New SettingCriteriaCourseDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    lstSettingCriteriaDetail.Clear()
                    rgCriteria.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
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
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    lstSettingCriteriaDetail.Clear()
                    rgCriteria.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim sum As Decimal = 0
                    If Page.IsValid Then
                        For Each item As SettingCriteriaDetailDTO In lstSettingCriteriaDetail
                            sum = sum + item.RATIO
                        Next
                        If sum <> 100 Then
                            ShowMessage(Translate("Tổng tỷ trọng của các tiêu chí phải bằng 100%"), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                        obj.REMARK = txtRemark.Text.Trim
                        If rdEffectTo.SelectedDate IsNot Nothing Then
                            obj.EFFECT_TO = rdEffectTo.SelectedDate
                        End If
                        obj.EFFECT_FROM = rdEffectFrom.SelectedDate
                        If cboCourse.SelectedValue <> "" Then
                            obj.TR_COURSE_ID = cboCourse.SelectedValue
                        End If
                        If cboCriteria.SelectedValue <> "" Then
                            obj.TR_CRITERIA_GROUP_ID = cboCriteria.SelectedValue
                        End If
                        If rnScale_Point.Value IsNot Nothing Then
                            obj.SCALE_POINT = rnScale_Point.Value
                        End If
                        obj.SettingCriteriaDetail = lstSettingCriteriaDetail

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                obj.ID = 0
                                If rep.INSERT_SETTING_CRITERIA_COURSE(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgMain.SelectedValue
                                Dim lst As New List(Of Decimal)
                                lst.Add(obj.ID)
                                If rep.MODIFY_SETTING_CRITERIA_COURSE(obj, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = obj.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(rdEffectFrom, rdEffectTo, cboCourse, cboCriteria, txtRemark, rnPointMax, rnRatio, rnScale_Point)
                    lstSettingCriteriaDetail.Clear()
                    rgCriteria.Rebind()
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteria.SelectedIndexChanged
        Try
            Dim rep As New TrainingStoreProcedure
            Dim dtData As New DataTable
            If cboCriteria.SelectedValue <> "" Then
                dtData = rep.GET_POINT_MAX_BY_CRITERIA(cboCriteria.SelectedValue)
                rnPointMax.Value = Convert.ToDecimal(dtData.Rows(0).Item(0))
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Dim rep As New TrainingRepository
        Dim obj As New SettingCriteriaDetailDTO
        Try
            If rgMain.SelectedItems.Count = 1 Then
                For Each selected As GridDataItem In rgMain.SelectedItems
                    obj.COURSE_ID = Convert.ToDecimal(selected.GetDataKeyValue("ID"))
                Next
                lstSettingCriteriaDetail = rep.GET_SETTING_CRITERIA_DETAIL_BY_COURSE(obj)
                rgCriteria.Rebind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rgCriteria_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriteria.NeedDataSource
        Try
            If lstSettingCriteriaDetail IsNot Nothing AndAlso lstSettingCriteriaDetail.Count > 0 Then
                rgCriteria.DataSource = lstSettingCriteriaDetail
            Else
                lstSettingCriteriaDetail = New List(Of SettingCriteriaDetailDTO)
                rgCriteria.DataSource = New List(Of SettingCriteriaDetailDTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgCriteria_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCriteria.ItemCommand
        Try
            Select Case e.CommandName
                Case "Add"
                    If cboCriteria.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Tiêu chí"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rnRatio.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Tỷ trọng"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rnPointMax.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Mức độ hữu ích"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lst As New List(Of Decimal)
                    For Each item As SettingCriteriaDetailDTO In lstSettingCriteriaDetail
                        lst.Add(item.CRITERIA_ID)
                    Next
                    If lst.Contains(Convert.ToDecimal(cboCriteria.SelectedValue)) Then
                        ShowMessage(Translate("Tiêu chí đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim SettingCriteriaDetail As SettingCriteriaDetailDTO
                    SettingCriteriaDetail = New SettingCriteriaDetailDTO
                    SettingCriteriaDetail.ID = 0
                    SettingCriteriaDetail.CRITERIA_ID = cboCriteria.SelectedValue
                    SettingCriteriaDetail.CRITERIA_NAME = cboCriteria.Text
                    SettingCriteriaDetail.RATIO = rnRatio.Value
                    SettingCriteriaDetail.POINT_MAX = rnPointMax.Value

                    lstSettingCriteriaDetail.Add(SettingCriteriaDetail)

                    rgCriteria.Rebind()
                Case "Delete"
                    If rgCriteria.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        If lstDeleteID IsNot Nothing Then
                            lstDeleteID.Clear()
                        Else
                            lstDeleteID = New List(Of Decimal)
                        End If

                        For Each selected As GridDataItem In rgCriteria.SelectedItems
                            lstDeleteID.Add(selected.GetDataKeyValue("CRITERIA_ID").ToString)
                        Next
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = "REMOVE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái cho Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region
End Class