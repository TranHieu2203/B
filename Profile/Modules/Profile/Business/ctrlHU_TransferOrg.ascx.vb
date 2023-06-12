Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_TransferOrg
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Protected WithEvents treeViewOrg As Global.Telerik.Web.UI.RadTreeView

#Region "Property"
    Public Property EmployeeID As Decimal
    Public Property SoLuong As Decimal
    Public Property DiaDiem As String

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
        End Set
    End Property

    Public Property lstTitleLeft As List(Of TitleDTO)
        Get
            Return ViewState(Me.ID & "_lstTitleLeft")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            ViewState(Me.ID & "_lstTitleLeft") = value
        End Set
    End Property

    Public Property lstTitleRight As List(Of TitleDTO)
        Get
            Return ViewState(Me.ID & "_lstTitleRight")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            ViewState(Me.ID & "_lstTitleRight") = value
        End Set
    End Property

    Public Property lstTitleRightSave As List(Of TitleDTO)
        Get
            Return ViewState(Me.ID & "_lstTitleRightSave")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            ViewState(Me.ID & "_lstTitleRightSave") = value
        End Set
    End Property


    'Public Property lstDataCanNotSchedule As List(Of ProgramClassStudentDTO)
    '    Get
    '        If ViewState(Me.ID & "_lstDataCanNotSchedule") Is Nothing Then
    '            ViewState(Me.ID & "_lstDataCanNotSchedule") = New List(Of ProgramClassStudentDTO)
    '        End If
    '        Return ViewState(Me.ID & "_lstDataCanNotSchedule")
    '    End Get
    '    Set(ByVal value As List(Of ProgramClassStudentDTO))
    '        ViewState(Me.ID & "_lstDataCanNotSchedule") = value
    '    End Set
    'End Property
    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"
    Protected Sub treeViewOrg_NodeClick(
        ByVal sender As Object,
        ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeViewOrg.NodeClick
        MsgBox(e.Node.Text)
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            rgLeft.PageSize = 50
            rgLeft.AllowCustomPaging = True
            rgRight.PageSize = 50
            rgRight.AllowCustomPaging = True
            'hidClassID.Value = Request.Params("CLASS_ID") 
            'If Not IsPostBack Then
            '    Using rep As New ProfileRepository
            '        Dim obj = rep.GetOrgTreeApp().Where(Function(f) f.PARENT_ID Is Nothing).FirstOrDefault
            '        If obj IsNot Nothing Then cboOrgTree1.SelectedValue = obj.ID
            '        'cboOrgTree2.SelectedValue = obj.ID
            '    End Using
            'End If
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgLeft.AllowCustomPaging = True
            rgLeft.SetFilter()
            rgRight.AllowCustomPaging = True
            rgRight.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

            Using rep As New ProfileRepository
                Dim lstOrg = rep.GetOrgTreeApp()
                cboOrgTree1.DataSource = lstOrg
                cboOrgTree1.DataBind()
                cboOrgTree1.ExpandAllDropDownNodes()

                cboOrgTree2.DataSource = lstOrg
                cboOrgTree2.DataBind()
                cboOrgTree2.ExpandAllDropDownNodes()

                Dim dtData As New DataTable()
                dtData.Columns.Add("NAME", GetType(System.String))
                dtData.Columns.Add("ID", GetType(System.Int32))

                dtData.Rows.Add("Master", 1)
                dtData.Rows.Add("Interim", 2)
                dtData.Rows.Add("Kiêm nhiệm", 3)
                FillRadCombobox(cboPositionFilter, dtData, "NAME", "ID")
                FillRadCombobox(cboPositionFilter2, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            'Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Cancel)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                'Using rep As New TrainingRepository
                '    Dim obj = rep.GetClassByID(New ProgramClassDTO With {.ID = Decimal.Parse(hidClassID.Value)})
                '    txtName.Text = obj.NAME
                '    rdStartDate.SelectedDate = obj.START_DATE
                '    rdEndDate.SelectedDate = obj.END_DATE
                'End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        phFindOrg.Controls.Clear()
        Select Case isLoadPopup
            Case 1
                ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                phFindOrg.Controls.Add(ctrlOrgPopup)

        End Select

        'Using rep As New ProfileRepository
        '    Dim lst As New List(Of ProgramClassStudentDTO)

        '    Select Case CurrentState
        '        Case CommonMessage.STATE_NEW
        '            For Each studentID In SelectedItemCanNotSchedule
        '                Dim obj As New ProgramClassStudentDTO
        '                obj.EMPLOYEE_ID = studentID
        '                obj.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
        '                lst.Add(obj)
        '            Next

        '            If rep.InsertClassStudent(lst) Then
        '                CurrentState = CommonMessage.STATE_NORMAL
        '                SelectedItemCanNotSchedule = Nothing
        '                SelectedItemCanSchedule = Nothing
        '                rgLeft.Rebind()
        '                rgRight.Rebind()
        '                UpdateControlState()
        '            Else
        '                CurrentState = CommonMessage.STATE_NORMAL
        '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
        '                UpdateControlState()
        '            End If

        '        Case CommonMessage.STATE_DELETE
        '            For Each studentID In SelectedItemCanSchedule
        '                Dim obj As New ProgramClassStudentDTO
        '                obj.EMPLOYEE_ID = studentID
        '                obj.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
        '                lst.Add(obj)
        '            Next

        '            If rep.DeleteClassStudent(lst) Then
        '                CurrentState = CommonMessage.STATE_NORMAL
        '                SelectedItemCanNotSchedule = Nothing
        '                SelectedItemCanSchedule = Nothing
        '                rgLeft.Rebind()
        '                rgRight.Rebind()
        '            Else
        '                CurrentState = CommonMessage.STATE_NORMAL
        '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
        '                UpdateControlState()
        '            End If
        '    End Select

        'End Using
    End Sub

#End Region

#Region "Event"
    Private Sub rgLeft_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgLeft.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = dataItem.GetDataKeyValue("COLOR")
            Dim Both = dataItem.GetDataKeyValue("BOTH")
            dataItem.ForeColor = Drawing.Color.FromArgb(Int32.Parse(id.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
            If Both = 1 Then
                dataItem.Font.Bold = True
            End If
        End If
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("ID")
            If SelectedItemCanNotSchedule IsNot Nothing AndAlso SelectedItemCanNotSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgLeft_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgLeft.PageIndexChanged
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgLeft_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgLeft.PageSizeChanged
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgLeft_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgLeft.SortCommand
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgRight_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRight.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = dataItem.GetDataKeyValue("COLOR")
            Dim Both = dataItem.GetDataKeyValue("BOTH")
            dataItem.ForeColor = Drawing.Color.FromArgb(Int32.Parse(id.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
            If Both = 1 Then
                dataItem.Font.Bold = True
            End If
        End If
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("ID")
            If SelectedItemCanSchedule IsNot Nothing AndAlso SelectedItemCanSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgRight_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRight.PageIndexChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgRight_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgRight.PageSizeChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgRight_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgRight.SortCommand
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgLeft_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLeft.NeedDataSource
        CreateDataFilterLeft()
    End Sub

    Private Sub rgRight_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRight.NeedDataSource
        CreateDataFilterRight()
    End Sub

    Private Sub btnNhanBan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNhanBan.Click
        Try
            Dim rep As New ProfileRepository
            Dim orgIdRight As Decimal '//= cboOrgTree2.SelectedValue
            Dim check As Boolean = False
            If cboOrgTree2.SelectedText = "" Then
                Me.ShowMessage(Translate("Bạn chưa chọn đơn vị cần nhân bản."), NotifyType.Warning)
                Exit Sub
            End If
            If rgLeft.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            'If cboOrgTree2.SelectedText <> "" Then
            '    Dim str As String, i As Integer, j As Integer
            '    str = cboOrgTree2.SelectedText
            '    i = str.IndexOf("(")
            '    If (i > -1) Then
            '        j = str.IndexOf(") - ", i + 1)
            '        orgIdRight = Decimal.Parse(str.Substring(i + 1, j - i - 1))
            '    End If
            'End If
            If (cboOrgTree2.SelectedValue <> "") Then
                orgIdRight = Decimal.Parse(cboOrgTree2.SelectedValue)
            End If

            'If orgIdRight < 0 Then
            '    orgIdRight = 0 - orgIdRight
            '    If rep.CheckIsOwner(orgIdRight) Then
            '        check = True
            '    End If
            'End If
            'If Page.IsValid Then
            '    For Each item As GridDataItem In rgRight.SelectedItems
            '        If item.GetDataKeyValue("IS_OWNER") = True And item.GetDataKeyValue("IS_PLAN") = False And check = True Then
            '            Me.ShowMessage(Translate("Đơn vị đã có vị trí trưởng."), NotifyType.Warning)
            '            Exit Sub
            '        End If
            '    Next
            '    
            'End If
            For Each item As GridDataItem In rgLeft.SelectedItems
                For Each i In lstTitleRight
                    If item.GetDataKeyValue("IS_OWNER") = True And i.IS_OWNER = True And i.IS_PLAN = False Then
                        Me.ShowMessage(Translate("Đơn vị đã có vị trí trưởng."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
            Next
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnNhanBan();", True)
        Catch ex As Exception
            Throw ex
        End Try

        'GetCanScheduleSelected()
        'ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCanSchedule.Count & " học viên?"
        'ctrlMessageBox.ActionName = "DELETE"
        'ctrlMessageBox.DataBind()
        'ctrlMessageBox.Show()

    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            Dim rep As New ProfileRepository
            If Page.IsValid Then
                Dim orgIdLeft As Decimal '//= cboOrgTree1.SelectedValue
                Dim orgIdRight As Decimal '//= cboOrgTree2.SelectedValue
                Dim check As Boolean = False
                If cboOrgTree2.SelectedText = "" Then
                    Me.ShowMessage(Translate("Bạn chưa chọn đơn vị cần điều chuyển đến."), NotifyType.Warning)
                    Exit Sub
                End If

                If (cboOrgTree1.SelectedValue <> "") Then
                    orgIdLeft = Decimal.Parse(cboOrgTree1.SelectedValue)
                End If

                If (cboOrgTree2.SelectedValue <> "") Then
                    orgIdRight = Decimal.Parse(cboOrgTree2.SelectedValue)
                End If

                'If cboOrgTree1.SelectedText <> "" Then
                '    Dim str As String, i As Integer, j As Integer
                '    str = cboOrgTree1.SelectedText
                '    i = str.IndexOf("(")
                '    If (i > -1) Then
                '        j = str.IndexOf(") - ", i + 1)
                '        orgIdLeft = Decimal.Parse(str.Substring(i + 1, j - i - 1))
                '    End If
                'End If

                'If cboOrgTree2.SelectedText <> "" Then
                '    Dim str As String, i As Integer, j As Integer
                '    str = cboOrgTree2.SelectedText
                '    i = str.IndexOf("(")
                '    If (i > -1) Then
                '        j = str.IndexOf(") - ", i + 1)
                '        orgIdRight = Decimal.Parse(str.Substring(i + 1, j - i - 1))
                '    End If
                'End If

                If rgLeft.SelectedItems.Count = 0 Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    Exit Sub
                End If
                If orgIdLeft = orgIdRight Then
                    Me.ShowMessage(Translate("Cần chuyển vị trí đến đơn vị khác đơn vị ban đầu."), NotifyType.Warning)
                    Exit Sub
                End If
                'If orgIdRight < 0 Then
                '    'orgIdRight = 0 - orgIdRight
                '    If rep.CheckIsOwner(orgIdRight) Then
                '        check = True
                '    End If
                'End If
                For Each item As GridDataItem In rgLeft.SelectedItems
                    For Each i In lstTitleRight
                        If item.GetDataKeyValue("IS_OWNER") = True And i.IS_OWNER = True And i.IS_PLAN = False Then
                            Me.ShowMessage(Translate("Đơn vị đã có vị trí trưởng."), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                Next
                'ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "btnInsert();", True)
                RadButton1_Click(Nothing, Nothing)
                'GetCanNotScheduleSelected()
                'ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCanNotSchedule.Count & " nhân viên thành học viên?"
                'ctrlMessageBox.ActionName = "INSERT"
                'ctrlMessageBox.DataBind()
                'ctrlMessageBox.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            rgLeft.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
    Private Sub btnSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
        Try
            rgRight.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub cboOrgTree1_EntryAdded(sender As Object, e As Telerik.Web.UI.DropDownTreeEntryEventArgs) Handles cboOrgTree1.EntryAdded
        Try
            txtTextbox1.Text = Nothing
            If cboOrgTree1.SelectedValue Is Nothing Then
                ShowMessage(Translate("Bạn phải chọn đơn vị"), NotifyType.Warning)
                Exit Sub
            End If
            rgLeft.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub


    Private Sub cboOrgTree2_EntryAdded(sender As Object, e As Telerik.Web.UI.DropDownTreeEntryEventArgs) Handles cboOrgTree2.EntryAdded
        Try
            txtTextbox2.Text = Nothing
            If cboOrgTree2.SelectedValue Is Nothing Then
                ShowMessage(Translate("Bạn phải chọn đơn vị"), NotifyType.Warning)
                Exit Sub
            End If
            rgRight.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilterRight(Optional ByVal isFull = False)
        Try
            Dim rep As New ProfileRepository
            If cboOrgTree2.SelectedValue = "" Then
                rgRight.VirtualItemCount = 0
                rgRight.DataSource = New List(Of TitleDTO)
                Exit Function
            End If
            Dim _filter As New TitleDTO
            SetValueObjectByRadGrid(rgRight, _filter)

            For Each item As RadComboBoxItem In cboPositionFilter2.CheckedItems
                If item.Checked Then
                    If item.Value = 1 Then
                        _filter.IS_MASTER = -1
                    ElseIf item.Value = 2 Then
                        _filter.IS_INTERIM = -1
                    Else
                        _filter.IS_CONCURRENTLY = -1
                    End If
                End If
            Next

            If txtTextbox2.Text <> "" Then
                _filter.TEXTBOX2_SEARCH = txtTextbox2.Text
            Else
                If (cboOrgTree2.SelectedValue <> "") Then
                    _filter.ORG_ID2_SEARCH = Decimal.Parse(cboOrgTree2.SelectedValue)
                End If
            End If
            Dim Sorts As String = rgRight.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Integer
            If isFull Then

                If Sorts IsNot Nothing Then
                    Return rep.GetPositionByOrgID(_filter,
                                                           0,
                                                           Integer.MaxValue,
                                                           0,
                                                           Sorts)
                Else

                    Return rep.GetPositionByOrgID(_filter,
                                                           0,
                                                           Integer.MaxValue ,
                                                           0)
                End If
            Else

                If Sorts IsNot Nothing Then
                    lstTitleRight = rep.GetPositionByOrgID(_filter,
                                                           rgRight.CurrentPageIndex,
                                                           rgRight.PageSize,
                                                           MaximumRows,
                                                           Sorts)
                Else

                    lstTitleRight = rep.GetPositionByOrgID(_filter,
                                                           rgRight.CurrentPageIndex,
                                                           rgRight.PageSize,
                                                           MaximumRows)
                End If
                rgRight.VirtualItemCount = MaximumRows
                rgRight.DataSource = lstTitleRight
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Protected Function CreateDataFilterLeft(Optional isFull As Boolean = False)
        Try
            Dim rep As New ProfileRepository
            Dim _filter As New TitleDTO
            rgLeft.VirtualItemCount = 0
            rgLeft.DataSource = New List(Of TitleDTO)
            SetValueObjectByRadGrid(rgLeft, _filter)

            For Each item As RadComboBoxItem In cboPositionFilter.CheckedItems
                If item.Checked Then
                    If item.Value = 1 Then
                        _filter.IS_MASTER = -1
                    ElseIf item.Value = 2 Then
                        _filter.IS_INTERIM = -1
                    Else
                        _filter.IS_CONCURRENTLY = -1
                    End If
                End If
            Next

            If txtTextbox1.Text <> "" Then
                _filter.TEXTBOX_SEARCH = txtTextbox1.Text
            Else
                If cboOrgTree1.SelectedValue <> "" Then
                    _filter.ORG_ID_SEARCH = Decimal.Parse(cboOrgTree1.SelectedValue)
                End If
            End If
            Dim Sorts As String = rgLeft.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Integer
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPositionByOrgID(_filter, 0,
                                                                Integer.MaxValue,
                                                                0,
                                                                Sorts)
                Else
                    Return rep.GetPositionByOrgID(_filter, 0,
                                                                Integer.MaxValue,
                                                                0)
                End If
            Else
                If Sorts IsNot Nothing Then
                    lstTitleLeft = rep.GetPositionByOrgID(_filter, rgLeft.CurrentPageIndex,
                                                                rgLeft.PageSize,
                                                                MaximumRows,
                                                                Sorts)
                Else
                    lstTitleLeft = rep.GetPositionByOrgID(_filter, rgLeft.CurrentPageIndex,
                                                                rgLeft.PageSize,
                                                                MaximumRows)
                End If

                Dim lstLeftDel As New List(Of TitleDTO)
                If lstTitleRightSave IsNot Nothing AndAlso lstTitleRightSave.Count > 0 Then

                    For Each it1 In lstTitleRightSave
                        For Each it In lstTitleLeft
                            If it.ID = it1.ID Then
                                lstTitleLeft.Remove(it)
                                Exit For
                            End If
                        Next
                    Next

                    rgLeft.VirtualItemCount = MaximumRows - lstTitleRightSave.Count
                Else
                    rgLeft.VirtualItemCount = MaximumRows
                End If

                rgLeft.DataSource = lstTitleLeft
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Sub GetCanNotScheduleSelected()
        Dim rep As New ProfileRepository
        Dim LstTitle As New List(Of TitleDTO)
        If txtQtyPos.Value <> "" Then
            SoLuong = Decimal.Parse(txtQtyPos.Value)
        End If
        'DiaDiem = txtWorkLocation.Value
        If lstTitleRightSave Is Nothing Then
            lstTitleRightSave = New List(Of TitleDTO)
        End If
        Dim OrgIdRight As Decimal '//= cboOrgTree2.SelectedValue
        Dim Is_Plan As Decimal
        Dim OrgName As String = ""

        'If cboOrgTree2.SelectedText <> "" Then
        '    Dim str As String, i As Integer, j As Integer
        '    str = cboOrgTree2.SelectedText
        '    i = str.IndexOf("(")
        '    If (i > -1) Then
        '        j = str.IndexOf(") - ", i + 1)
        '        OrgIdRight = Decimal.Parse(str.Substring(i + 1, j - i - 1))
        '    End If
        'End If
        If (cboOrgTree2.SelectedValue <> "") Then
            OrgIdRight = Decimal.Parse(cboOrgTree2.SelectedValue)
            OrgName = rep.GetOrgOMByID(OrgIdRight).CODE
        End If

        If OrgIdRight < 0 Then
            Is_Plan = -1
        Else
            Is_Plan = 0
        End If
        'Nếu SoLuong = 0 là điều chuyển, SoLuong > 0 là nhân bản
        If SoLuong = 0 Then
            For Each item As Telerik.Web.UI.GridDataItem In rgLeft.SelectedItems
                Dim obj = (New TitleDTO With {
                    .ID = item.GetDataKeyValue("ID"),
                    .CODE = item.GetDataKeyValue("CODE"),
                    .NAME_VN = item.GetDataKeyValue("NAME_VN"),
                    .NAME_EN = item.GetDataKeyValue("NAME_EN"),
                    .ORG_ID = item.GetDataKeyValue("ORG_ID"),
                    .ORG_NAME = OrgName,
                    .MASTER = If(item.GetDataKeyValue("MASTER") IsNot Nothing, Decimal.Parse(item.GetDataKeyValue("MASTER")), Nothing),
                    .INTERIM = If(item.GetDataKeyValue("INTERIM") IsNot Nothing, Decimal.Parse(item.GetDataKeyValue("INTERIM")), Nothing),
                    .MASTER_NAME = item.GetDataKeyValue("MASTER_NAME"),
                    .INTERIM_NAME = item.GetDataKeyValue("INTERIM_NAME"),
                    .MASTER_CODE = item.GetDataKeyValue("MASTER_CODE"),
                    .REMARK = item.GetDataKeyValue("REMARK"),
                    .HIRING_STATUS = item.GetDataKeyValue("HIRING_STATUS"),
                    .CONCURRENT = item.GetDataKeyValue("CONCURRENT"),
                    .IS_OWNER = item.GetDataKeyValue("IS_OWNER"),
                    .IS_PLAN_LEFT = item.GetDataKeyValue("IS_PLAN"),
                    .IS_PLAN = Is_Plan,
                    .FLAG = 0,
                    .COLOR = item.GetDataKeyValue("COLOR"),
                    .SOLUONG = SoLuong})

                If obj.MASTER <> 0 AndAlso obj.INTERIM = 0 Then
                    Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có master là " & obj.MASTER_NAME & " không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển"), NotifyType.Warning)
                    rgLeft.Rebind()
                    Exit Sub
                End If
                If obj.MASTER = 0 AndAlso obj.INTERIM <> 0 Then
                    Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có interim là " & obj.INTERIM_NAME & " không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển"), NotifyType.Warning)
                    rgLeft.Rebind()
                    Exit Sub
                End If
                If obj.MASTER <> 0 AndAlso obj.INTERIM <> 0 Then
                    Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có master và interim không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển."), NotifyType.Warning)
                    rgLeft.Rebind()
                    Exit Sub
                End If
                LstTitle.Add(obj)
                If lstTitleRight IsNot Nothing Then
                    lstTitleRight.Add(obj)
                End If
                If lstTitleRightSave IsNot Nothing Then
                    lstTitleRightSave.Add(obj)
                End If
            Next
            If LstTitle.Count > 0 Then
                Dim lstTitleLeftAll As New List(Of TitleDTO)
                Dim lstTitleRightAll As New List(Of TitleDTO)
                lstTitleLeftAll = CreateDataFilterLeft(True)
                lstTitleRightAll = CreateDataFilterRight(True)
                lstTitleLeft = (From item In rgLeft.MasterTableView.Items
                                Select New TitleDTO With {
                                .ID = item.GetDataKeyValue("ID"),
                                .CODE = item.GetDataKeyValue("CODE"),
                                .NAME_VN = item.GetDataKeyValue("NAME_VN"),
                                .NAME_EN = item.GetDataKeyValue("NAME_EN"),
                                .ORG_ID = item.GetDataKeyValue("ORG_ID"),
                                .ORG_NAME = OrgName,
                                .MASTER = If(item.GetDataKeyValue("MASTER") IsNot Nothing, Decimal.Parse(item.GetDataKeyValue("MASTER")), Nothing),
                                .INTERIM = If(item.GetDataKeyValue("INTERIM") IsNot Nothing, Decimal.Parse(item.GetDataKeyValue("INTERIM")), Nothing),
                                .MASTER_NAME = item.GetDataKeyValue("MASTER_NAME"),
                                .INTERIM_NAME = item.GetDataKeyValue("INTERIM_NAME"),
                                .MASTER_CODE = item.GetDataKeyValue("MASTER_CODE"),
                                .REMARK = item.GetDataKeyValue("REMARK"),
                                .HIRING_STATUS = item.GetDataKeyValue("HIRING_STATUS"),
                                .CONCURRENT = item.GetDataKeyValue("CONCURRENT"),
                                .IS_OWNER = item.GetDataKeyValue("IS_OWNER"),
                                .IS_PLAN_LEFT = item.GetDataKeyValue("IS_PLAN"),
                                .IS_PLAN = Is_Plan,
                                .FLAG = 0,
                                .COLOR = item.GetDataKeyValue("COLOR"),
                                .SOLUONG = SoLuong}).ToList

                For Each it1 In lstTitleRightSave
                    For Each it In lstTitleLeft
                        If it.ID = it1.ID Then
                            lstTitleLeft.Remove(it)
                            Exit For
                        End If
                    Next
                Next
                rgLeft.DataSource = lstTitleLeft
                rgLeft.VirtualItemCount = lstTitleLeftAll.Count - lstTitleRightSave.Count
                rgLeft.Rebind()
                rgRight.DataSource = lstTitleRight
                If lstTitleRightAll IsNot Nothing AndAlso lstTitleRightAll.Count > 0 Then
                    rgRight.VirtualItemCount = lstTitleRightAll.Count + lstTitleRightSave.Count
                Else
                    rgRight.VirtualItemCount = lstTitleRightSave.Count
                End If

                rgRight.Rebind()

            End If
        Else
            Dim lstString As New List(Of String)
            Dim code
            Dim TitleCode As Decimal
            code = rep.AutoGenCodeHuTile("HU_TITLE", "CODE")
            TitleCode = Decimal.Parse(code) - 1

            For i = 0 To SoLuong - 1
                For Each item As Telerik.Web.UI.GridDataItem In rgLeft.SelectedItems
                    If SoLuong > 1 And item.GetDataKeyValue("IS_OWNER") = True Then
                        Me.ShowMessage(Translate("Không được nhân bản một bản ghi trưởng phòng thành nhiều bản ghi mới."), NotifyType.Warning)
                        Exit Sub
                    End If
                    TitleCode = TitleCode + 1
                    Dim CharCode = Format(TitleCode, "00000")
                    'Dim strCode = "CD" & TitleCode
                    Dim obj = (New TitleDTO With {
                        .ID = item.GetDataKeyValue("ID"),
                        .CODE = CharCode,
                        .NAME_VN = item.GetDataKeyValue("NAME_VN"),
                        .NAME_EN = item.GetDataKeyValue("NAME_EN"),
                        .MASTER_NAME = "(Vacant)",
                        .INTERIM_NAME = "(Vacant)",
                        .ORG_ID = item.GetDataKeyValue("ORG_ID"),
                        .ORG_NAME = OrgName,
                        .HIRING_STATUS = item.GetDataKeyValue("HIRING_STATUS"),
                        .IS_OWNER = item.GetDataKeyValue("IS_OWNER"),
                        .IS_PLAN_LEFT = item.GetDataKeyValue("IS_PLAN"),
                        .IS_PLAN = Is_Plan,
                        .JOB_ID = If(item.GetDataKeyValue("JOB_ID") IsNot Nothing, Decimal.Parse(item.GetDataKeyValue("JOB_ID")), Nothing),
                        .COST_CENTER = item.GetDataKeyValue("COST_CENTER"),
                        .IS_NONPHYSICAL = item.GetDataKeyValue("IS_NONPHYSICAL"),
                        .FLAG = 0,
                        .COLOR = item.GetDataKeyValue("COLOR"),
                        .SOLUONG = SoLuong})

                    If obj.MASTER <> 0 AndAlso obj.INTERIM = 0 Then
                        Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có master là " & obj.MASTER_NAME & " không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển"), NotifyType.Warning)
                        rgLeft.Rebind()
                        Exit Sub
                    End If
                    If obj.MASTER = 0 AndAlso obj.INTERIM <> 0 Then
                        Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có interim là " & obj.INTERIM_NAME & " không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển"), NotifyType.Warning)
                        rgLeft.Rebind()
                        Exit Sub
                    End If
                    If obj.MASTER <> 0 AndAlso obj.INTERIM <> 0 Then
                        Me.ShowMessage(Translate("Vị trí " & obj.CODE & " đang có master và interim không thể điều chuyển. Vui lòng tạo quyết định để điều chuyển."), NotifyType.Warning)
                        rgLeft.Rebind()
                        Exit Sub
                    End If
                    lstTitleRight.Add(obj)
                    lstTitleRightSave.Add(obj)
                Next
            Next
            rgRight.DataSource = lstTitleRight
            rgRight.Rebind()
        End If
    End Sub

    Private Sub GetCanScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgRight.Items
            Dim id As String = dr.GetDataKeyValue("ID")
            If dr.Selected Then
                If Not SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Add(id)
            Else
                If SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Remove(id)
            End If
        Next
    End Sub

    Private Sub PopulatingListTitle()
        Dim lstOrgIds = New List(Of Decimal)
        If hidOrg.Value <> "" Then
            lstOrgIds.Add(hidOrg.Value)
        End If
        'Using rep As New TrainingBusinessClient
        '    lstPositions.Items.Clear()
        '    Dim titles = rep.GetTitlesByOrgs(lstOrgIds, Common.Common.SystemLanguage.Name)
        '    For Each item In titles
        '        lstPositions.Items.Add(New RadListBoxItem(item.NAME, item.ID))
        '    Next
        'End Using
    End Sub

#End Region

    Private Sub RadButton1_Click(sender As Object, e As System.EventArgs) Handles RadButton1.Click
        GetCanNotScheduleSelected()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Try
            Dim rep As New ProfileRepository
            Dim orgIdRight As Decimal '//= cboOrgTree2.SelectedValue
            Dim check As Boolean = False
            Dim checkLuu As Boolean = False
            If txtQtyPos.Value <> "" Then
                SoLuong = Decimal.Parse(txtQtyPos.Value)
            End If


            If (cboOrgTree2.SelectedValue <> "") Then
                orgIdRight = Decimal.Parse(cboOrgTree2.SelectedValue)
            End If

            If rep.CheckIsOwner(orgIdRight) Then
                check = True
            End If
            If orgIdRight < 0 Then
                orgIdRight = 0 - orgIdRight
            End If
            For Each obj In lstTitleRightSave
                If obj.MASTER IsNot Nothing AndAlso obj.INTERIM Is Nothing Then
                    Me.ShowMessage(Translate("Vị trí này đang có master và interim không thể điều chuyển."), NotifyType.Warning)
                    Exit Sub
                End If
                If obj.SOLUONG = 0 Then
                    If obj.IS_OWNER = True And check = True Then
                        Me.ShowMessage(Translate("Đơn vị đích đến đã có vị trí trưởng."), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Hàm lưu điều chuyển
                    If rep.ModifyTitleById(obj, orgIdRight, DiaDiem) Then
                        checkLuu = True
                    End If
                Else
                    If obj.IS_OWNER = True And obj.SOLUONG > 1 Then
                        Me.ShowMessage(Translate("Không được nhân bản một bản ghi trưởng phòng thành nhiều bản ghi mới."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If obj.IS_OWNER = True And check = True Then
                        Me.ShowMessage(Translate("Đơn vị đích đến đã có vị trí trưởng."), NotifyType.Warning)
                        Exit Sub
                    End If
                    'Hàm lưu nhân bản
                    If rep.InsertTitleNB(obj, orgIdRight, DiaDiem) Then
                        checkLuu = True
                    End If

                End If
            Next
            If checkLuu = True Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                lstTitleLeft = Nothing
                lstTitleRight = Nothing
                lstTitleRightSave = Nothing
                rgLeft.Rebind()
                rgRight.Rebind()
            Else
                Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As System.EventArgs) Handles btnUndo.Click
        lstTitleRightSave = Nothing
        lstTitleLeft = Nothing
        lstTitleRight = Nothing
        rgLeft.Rebind()
        rgRight.Rebind()
    End Sub
End Class