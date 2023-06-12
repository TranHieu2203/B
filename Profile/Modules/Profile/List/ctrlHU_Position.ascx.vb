Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Position
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

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

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property Is_Vacant As Boolean
        Get
            Return ViewState(Me.ID & "_Is_Vacant")
        End Get

        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Is_Vacant") = value
        End Set
    End Property

#End Region

#Region "Page"

    Protected WithEvents ctrlUpdateManager As ctrlUpdateManager
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

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            If Not IsPostBack Then
                GirdConfig(rgMain)
            End If
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Print,
                                       ToolbarItem.Sync)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Text = "In JD"
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = "Thay đổi QL hàng loạt"
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SEARCH_VACANT", ToolbarIcons.Vacant,
                                                                   ToolbarAuthorize.None, Translate("Vacant")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SWAP", ToolbarIcons.Find,
                                                                   ToolbarAuthorize.Export, Translate("Hoán đổi")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT, ToolbarIcons.Export,
                                                                   ToolbarAuthorize.Export, Translate("Xuất Excel")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DEACTIVE, ToolbarIcons.DeActive,
                                                                   ToolbarAuthorize.Special2, Translate("Ngừng áp dụng")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_ACTIVE, ToolbarIcons.Active,
                                                                   ToolbarAuthorize.Special2, Translate("Áp dụng")))
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(8), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        ctrlOrg.IsOM = False
        ctrlOrg.is_UYBAN = True
        ctrlOrg.ShowCommitee = True
        ctrlOrg.build_UYBAN = False
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileRepository
        Dim _filter As New TitleDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgMain.DataSource = New List(Of TitleDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)

            _filter.Is_Vacant = Is_Vacant
            _filter.IS_UYBAN = TryCast(ctrlOrg.FindControl("chkViewCommitee"), CheckBox).Checked
            hidCommittee.Value = TryCast(ctrlOrg.FindControl("chkViewCommitee"), CheckBox).Checked

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPossition(_filter, _param, True, Sorts).ToTable()
                Else
                    Return rep.GetPossition(_filter, _param, True).ToTable()
                End If
            Else
                Dim Titles As List(Of TitleDTO)
                If Sorts IsNot Nothing Then
                    Titles = rep.GetPossition(_filter, _param, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, True, Sorts)
                Else
                    Titles = rep.GetPossition(_filter, _param, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, True)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Titles

            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    Refresh("Cancel")

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboTitleGroup, True)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteTitle(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Dim Status As String = ""
                    If rep.ActivePositions(lstDeletes, "A", Status) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(Status), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    Dim Status As String = ""
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActivePositions(lstDeletes, "I", Status) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(Status), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    Dim Id = item.GetDataKeyValue("ID")
                    If rep.SwapMasterInterim(Id, "APP") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

            End Select

            txtNameVN.Focus()

            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtNameVN)
            dic.Add("REMARK", txtRemark)
            dic.Add("TITLE_GROUP_ID", cboTitleGroup)

            Utilities.OnClientRowSelectedChanged(rgMain, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTitle As New TitleDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup)
                    txtCode.Text = rep.AutoGenCode("CD", "HU_TITLE", "CODE")
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

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If item.GetDataKeyValue("ACTFLG").ToString = "Áp dụng" Then
                            ShowMessage("Tồn tại bản ghi có trạng thái Áp dụng. Không thể xóa", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_TITLE) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Title")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If (item.GetDataKeyValue("ACTFLG") IsNot Nothing AndAlso item.GetDataKeyValue("ACTFLG").ToString = "Áp dụng") Then
                            ShowMessage("Chỉ có thể Ngừng áp dụng vị trí ở trạng thái Áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If (item.GetDataKeyValue("MASTER") IsNot Nothing AndAlso item.GetDataKeyValue("MASTER").ToString <> "") Then
                            ShowMessage("Tồn tại bản ghi có giá trị ở cột Master. Không thể Ngừng áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                        If (item.GetDataKeyValue("ACTFLG") IsNot Nothing AndAlso item.GetDataKeyValue("ACTFLG").ToString = "Ngừng áp dụng") Then
                            ShowMessage("Chỉ có thể Ngừng áp dụng vị trí ở trạng thái Áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SWAP"
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If (item.GetDataKeyValue("MASTER") Is Nothing OrElse item.GetDataKeyValue("MASTER").ToString = "") AndAlso (item.GetDataKeyValue("INTERIM") Is Nothing OrElse item.GetDataKeyValue("INTERIM").ToString = "") Then
                            ShowMessage("Không thể hoán đổi vì trống Master và Interim", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Swap Master Interim?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Exit Sub
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objTitle.CODE = txtCode.Text.Trim
                        objTitle.NAME_VN = txtNameVN.Text.Trim
                        objTitle.NAME_EN = txtNameVN.Text.Trim
                        objTitle.REMARK = txtRemark.Text.Trim

                        If cboTitleGroup.SelectedValue <> "" Then
                            objTitle.TITLE_GROUP_ID = cboTitleGroup.SelectedValue
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objTitle.ACTFLG = "A"
                                If rep.InsertTitle(objTitle, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                objTitle.ID = rgMain.SelectedValue

                                Dim lst As New List(Of Decimal)
                                lst.Add(objTitle.ID)
                                If commonRes.CheckExistIDTable(lst, "HU_TITLE", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                If rep.ModifyTitle(objTitle, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTitle.ID
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
                    ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case "SEARCH_VACANT"
                    Is_Vacant = Not Is_Vacant
                    If Is_Vacant Then
                        CType(Me.MainToolBar.Items(5), RadToolBarButton).BackColor = Drawing.Color.DarkOrange
                    Else
                        CType(Me.MainToolBar.Items(5), RadToolBarButton).BackColor = Drawing.Color.Transparent
                    End If
                    rgMain.CurrentPageIndex = 0
                    rgMain.MasterTableView.SortExpressions.Clear()
                    rgMain.Rebind()
                Case Common.CommonMessage.TOOLBARITEM_PRINT
                    Print_JD()
                Case CommonMessage.TOOLBARITEM_SYNC
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlUpdateManager.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Print_JD()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim rp As New ProfileStoreProcedure
        Dim ID As Decimal
        Dim reportName As String = String.Empty
        Dim reportNameOut As String = "String.Empty"
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Try
            If rgMain.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If rgMain.SelectedItems.Count > 1 Then
                ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                ID = Decimal.Parse(dr.GetDataKeyValue("ID").ToString())
            Next

            Dim obj = rep.GetPrintJD(ID)
            Dim lstData1 As New List(Of TitleDTO)
            lstData1.Add(obj)

            Dim lstData2 As New List(Of JobDescriptionDTO)

            Dim lstData3 As New List(Of JobDescriptionDTO)

            Dim lstData4 As New List(Of JobDescriptionDTO)

            Dim lstData5 As New List(Of JobDescriptionDTO)

            Dim lstData6 As New List(Of JobDescriptionDTO)

            Dim lstData7 As New List(Of JobDescriptionDTO)
            If obj.JobDescription IsNot Nothing Then
                lstData2.Add(obj.JobDescription)
                Dim obj1 As New JobDescriptionDTO
                Dim obj2 As New JobDescriptionDTO
                Dim obj3 As New JobDescriptionDTO
                Dim obj4 As New JobDescriptionDTO
                Dim obj5 As New JobDescriptionDTO
                obj1.RESPONSIBILITY_1 = obj.JobDescription.RESPONSIBILITY_1
                obj1.DETAIL_RESPONSIBILITY_1 = obj.JobDescription.DETAIL_RESPONSIBILITY_1
                obj1.OUT_RESULT_1 = obj.JobDescription.OUT_RESULT_1
                obj2.RESPONSIBILITY_1 = obj.JobDescription.RESPONSIBILITY_2
                obj2.DETAIL_RESPONSIBILITY_1 = obj.JobDescription.DETAIL_RESPONSIBILITY_2
                obj2.OUT_RESULT_1 = obj.JobDescription.OUT_RESULT_2
                obj3.RESPONSIBILITY_1 = obj.JobDescription.RESPONSIBILITY_3
                obj3.DETAIL_RESPONSIBILITY_1 = obj.JobDescription.DETAIL_RESPONSIBILITY_3
                obj3.OUT_RESULT_1 = obj.JobDescription.OUT_RESULT_3
                obj4.RESPONSIBILITY_1 = obj.JobDescription.RESPONSIBILITY_4
                obj4.DETAIL_RESPONSIBILITY_1 = obj.JobDescription.DETAIL_RESPONSIBILITY_4
                obj4.OUT_RESULT_1 = obj.JobDescription.OUT_RESULT_4
                obj5.RESPONSIBILITY_1 = obj.JobDescription.RESPONSIBILITY_5
                obj5.DETAIL_RESPONSIBILITY_1 = obj.JobDescription.DETAIL_RESPONSIBILITY_5
                obj5.OUT_RESULT_1 = obj.JobDescription.OUT_RESULT_5
                If obj1.RESPONSIBILITY_1 <> "" Then lstData3.Add(obj1)
                If obj2.RESPONSIBILITY_1 <> "" Then lstData3.Add(obj2)
                If obj3.RESPONSIBILITY_1 <> "" Then lstData3.Add(obj3)
                If obj4.RESPONSIBILITY_1 <> "" Then lstData3.Add(obj4)
                If obj5.RESPONSIBILITY_1 <> "" Then lstData3.Add(obj5)
                Dim jobTarget1 As New JobDescriptionDTO
                Dim jobTarget2 As New JobDescriptionDTO
                Dim jobTarget3 As New JobDescriptionDTO
                Dim jobTarget4 As New JobDescriptionDTO
                Dim jobTarget5 As New JobDescriptionDTO
                Dim jobTarget6 As New JobDescriptionDTO
                jobTarget1.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_1
                jobTarget2.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_2
                jobTarget3.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_3
                jobTarget4.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_4
                jobTarget5.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_5
                jobTarget6.JOB_TARGET_1 = obj.JobDescription.JOB_TARGET_6
                If jobTarget1.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget1)
                If jobTarget2.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget2)
                If jobTarget3.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget3)
                If jobTarget4.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget4)
                If jobTarget5.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget5)
                If jobTarget6.JOB_TARGET_1 <> "" Then lstData4.Add(jobTarget6)
                Dim internal1 As New JobDescriptionDTO
                Dim internal2 As New JobDescriptionDTO
                Dim internal3 As New JobDescriptionDTO
                internal1.INTERNAL_1 = obj.JobDescription.INTERNAL_1
                internal2.INTERNAL_1 = obj.JobDescription.INTERNAL_2
                internal3.INTERNAL_1 = obj.JobDescription.INTERNAL_3
                If internal1.INTERNAL_1 <> "" Then lstData5.Add(internal2)
                If internal2.INTERNAL_1 <> "" Then lstData5.Add(internal2)
                If internal3.INTERNAL_1 <> "" Then lstData5.Add(internal3)
                Dim outside1 As New JobDescriptionDTO
                Dim outside2 As New JobDescriptionDTO
                Dim outside3 As New JobDescriptionDTO
                internal1.OUTSIDE_1 = obj.JobDescription.OUTSIDE_1
                internal2.OUTSIDE_1 = obj.JobDescription.OUTSIDE_2
                internal3.OUTSIDE_1 = obj.JobDescription.OUTSIDE_3
                If internal1.OUTSIDE_1 <> "" Then lstData6.Add(internal1)
                If internal2.OUTSIDE_1 <> "" Then lstData6.Add(internal2)
                If internal3.OUTSIDE_1 <> "" Then lstData6.Add(internal3)
                Dim permission1 As New JobDescriptionDTO
                Dim permission2 As New JobDescriptionDTO
                Dim permission3 As New JobDescriptionDTO
                Dim permission4 As New JobDescriptionDTO
                Dim permission5 As New JobDescriptionDTO
                Dim permission6 As New JobDescriptionDTO
                permission1.PERMISSION_1 = obj.JobDescription.PERMISSION_1
                permission2.PERMISSION_1 = obj.JobDescription.PERMISSION_2
                permission3.PERMISSION_1 = obj.JobDescription.PERMISSION_3
                permission4.PERMISSION_1 = obj.JobDescription.PERMISSION_4
                permission5.PERMISSION_1 = obj.JobDescription.PERMISSION_5
                permission6.PERMISSION_1 = obj.JobDescription.PERMISSION_6
                If permission1.PERMISSION_1 <> "" Then lstData7.Add(permission1)
                If permission2.PERMISSION_1 <> "" Then lstData7.Add(permission2)
                If permission3.PERMISSION_1 <> "" Then lstData7.Add(permission3)
                If permission4.PERMISSION_1 <> "" Then lstData7.Add(permission4)
                If permission5.PERMISSION_1 <> "" Then lstData7.Add(permission5)
                If permission6.PERMISSION_1 <> "" Then lstData7.Add(permission6)
            End If

            Dim dtData = lstData1.ToTable
            If lstData2.Count = 0 Then
                lstData2.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData2 = lstData2.ToTable
            If lstData3.Count = 0 Then
                lstData3.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData3 = lstData3.ToTable
            If lstData4.Count = 0 Then
                lstData4.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData4 = lstData4.ToTable
            If lstData5.Count = 0 Then
                lstData5.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData5 = lstData5.ToTable
            If lstData6.Count = 0 Then
                lstData6.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData6 = lstData6.ToTable
            If lstData7.Count = 0 Then
                lstData7.Add(New JobDescriptionDTO With {.TITLE_ID = obj.ID})
            End If
            Dim dtData7 = lstData7.ToTable

            dtData.Columns.Add(New DataColumn("IMAGE", GetType(String)))

            If File.Exists(Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + obj.ATTACH_FILE_LOGO + obj.FILE_LOGO)) Then
                dtData.Rows(0)("IMAGE") = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/" + obj.ATTACH_FILE_LOGO + obj.FILE_LOGO)
            End If

            Dim dsData As New DataSet
            dsData.Tables.Add(dtData)
            dsData.Tables.Add(dtData2)
            dsData.Tables.Add(dtData3)
            dsData.Tables.Add(dtData4)
            dsData.Tables.Add(dtData5)
            dsData.Tables.Add(dtData6)
            dsData.Tables.Add(dtData7)
            If dsData Is Nothing AndAlso dsData.Tables(0) IsNot Nothing Then
                ShowMessage("Không có dữ liệu in báo cáo", NotifyType.Warning)
                Exit Sub
            End If

            dsData.Tables(0).TableName = "DT"
            dsData.Tables(1).TableName = "DT1"
            dsData.Tables(2).TableName = "DT2"
            dsData.Tables(3).TableName = "DT3"
            dsData.Tables(4).TableName = "DT4"
            dsData.Tables(5).TableName = "DT5"
            dsData.Tables(6).TableName = "DT6"
            reportName = "Employee\JD_Template.doc"
            Dim item As GridDataItem = rgMain.SelectedItems(0)
            reportNameOut = "JD" & item.GetDataKeyValue("CODE") & ".doc"
            If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                                  reportNameOut,
                                  dsData,
                                  Response)
            Else
                ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
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
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpdateManager_ManagerSelected(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlUpdateManager.ManagerSelected
        Try
            Dim mngItem = ctrlUpdateManager.SelectedManager
            Dim lstID As New List(Of Decimal)
            For idx = 0 To rgMain.SelectedItems.Count - 1
                Dim item As GridDataItem = rgMain.SelectedItems(idx)
                lstID.Add(item.GetDataKeyValue("ID"))
            Next
            Using rep As New ProfileRepository
                If rep.UpdateManager(lstID, mngItem) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgMain.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgMain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = dataItem.GetDataKeyValue("COLOR")
            dataItem.ForeColor = Drawing.Color.FromArgb(Int32.Parse(id.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
            Dim Both = dataItem.GetDataKeyValue("BOTH")
            If (id = "#969696") Then
                Dim baseCss As String
                If (e.Item.ItemType = Telerik.Web.UI.GridItemType.Item) Then
                    baseCss = "rgRow"
                Else
                    baseCss = "rgAltRow"
                End If
                dataItem.CssClass = baseCss & " rgRow-alternating-item"
            End If
            If Both = 1 Then
                dataItem.Font.Bold = True
            End If
            dataItem("ORG_NAME").ToolTip = Utilities.DrawTreeByString(dataItem.GetDataKeyValue("ORG_DESC"))
        End If
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
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Check sự kiện validate cho combobox tồn tại hoặc ngừng áp dụng</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New TitleDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTitle(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTitle(_validate)
            End If

            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("CD", "HU_TITLE", "CODE")
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox Nhóm chức danh
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTitleGroup_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTitleGroup.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboTitleGroup.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "HU_TITLE_GROUP"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

                cboTitleGroup.Items.Insert(0, New RadComboBoxItem("", ""))
                cboTitleGroup.ClearSelection()
                cboTitleGroup.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/05/2020 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgMain
    ''' Bind lai du lieu cho rgMain
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            hidOrgID.Value = ctrlOrg.CurrentValue
            hidOrgName.Value = ctrlOrg.CurrentText

            rgMain.CurrentPageIndex = 0
            rgMain.MasterTableView.SortExpressions.Clear()
            rgMain.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            If phUpdateManager.Controls.Contains(ctrlUpdateManager) Then
                phUpdateManager.Controls.Remove(ctrlUpdateManager)
            End If
            Select Case isLoadPopup
                Case 1
                    If Not phUpdateManager.Controls.Contains(ctrlUpdateManager) Then
                        ctrlUpdateManager = Me.Register("ctrlUpdateManager", "Profile", "ctrlUpdateManager", "Shared")
                        phUpdateManager.Controls.Add(ctrlUpdateManager)
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class