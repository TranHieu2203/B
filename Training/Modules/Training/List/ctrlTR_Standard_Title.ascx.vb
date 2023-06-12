Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_Standard_Title
    Inherits Common.CommonView
    Dim dtData As New DataTable

#Region "Property"
    Public IDSelect As Integer
    Public Property TR_STANDARD_TITLE As List(Of TR_STANDARD_TITLEDTO)
        Get
            Return ViewState(Me.ID & "_TR_STANDARD_TITLE")
        End Get
        Set(ByVal value As List(Of TR_STANDARD_TITLEDTO))
            ViewState(Me.ID & "_TR_STANDARD_TITLE") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgDanhMuc)
        rgDanhMuc.AllowCustomPaging = True
        ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
        ctrlUpload.MaxFileInput = 1
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim rep As New TrainingRepository
        Dim obj As New TR_STANDARD_TITLEDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.TR_STANDARD_TITLE = rep.GetTR_STANDARD_TITLE(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.TR_STANDARD_TITLE = rep.GetTR_STANDARD_TITLE(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.TR_STANDARD_TITLE
            Else
                Return rep.GetTR_STANDARD_TITLE(obj).ToTable()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtCode, cboGroupTitle, cboTitle, cboStaffRank, cboCourse, rdNgayHieuLuc, rdNgayhetHL, txtNote)
                    ClearControlValue(txtCode, cboGroupTitle, cboTitle, cboStaffRank, cboCourse, rdNgayHieuLuc, rdNgayhetHL, txtNote)
                    txtCode.Text = rep.AutoGenCode("DTTCD", "TR_STANDARD_TITLE", "CODE")
                    cboGroupTitle.AutoPostBack = True
                    EnabledGridNotPostback(rgDanhMuc, False)

                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtCode, cboGroupTitle, cboTitle, cboStaffRank, cboCourse, rdNgayHieuLuc, rdNgayhetHL, txtNote)
                    ClearControlValue(txtCode, cboGroupTitle, cboTitle, cboStaffRank, cboCourse, rdNgayHieuLuc, rdNgayhetHL, txtNote)
                    cboGroupTitle.AutoPostBack = False
                    EnabledGridNotPostback(rgDanhMuc, True)


                Case CommonMessage.STATE_EDIT
                    cboGroupTitle.AutoPostBack = True
                    EnableControlAll(True, txtCode, cboGroupTitle, cboTitle, cboStaffRank, cboCourse, rdNgayHieuLuc, rdNgayhetHL, txtNote)
                    EnabledGridNotPostback(rgDanhMuc, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteTR_STANDARD_TITLE(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        GetDataCombo()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", txtCode)
        dic.Add("GROUP_TITLE_ID", cboGroupTitle)
        dic.Add("HU_TITLE_ID", cboTitle)
        dic.Add("HU_STAFF_RANK_ID", cboStaffRank)
        dic.Add("TR_COURSE_ID", cboCourse)
        dic.Add("EFFECTIVE_DATE_FROM", rdNgayHieuLuc)
        dic.Add("EFFECTIVE_DATE_TO", rdNgayhetHL)
        dic.Add("NOTE", txtNote)
        Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
    End Sub

    Private Sub GetDataCombo()
        Try
            dtData = New DataTable()
            Using rep As New TrainingRepository
                dtData = rep.GetTR_COURSEBINCOMBO()
                FillRadCombobox(cboCourse, dtData, "NAME", "ID")
            End Using

            dtData = New DataTable()
            Using rep As New TrainingRepository
                dtData = rep.GETTITLE_OTHERLIST()
                FillRadCombobox(cboGroupTitle, dtData, "NAME_VN", "ID")
            End Using

            dtData = New DataTable()
            Using rep As New TrainingRepository
                dtData = rep.GETHU_STAFF_RANK()
                FillRadCombobox(cboStaffRank, dtData, "NAME", "ID")
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboGroupTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGroupTitle.SelectedIndexChanged
        If cboGroupTitle.SelectedValue <> "" Then
            dtData = New DataTable()
            Using rep As New TrainingRepository
                dtData = rep.GETHU_TITLE(cboGroupTitle.SelectedValue)
                FillRadCombobox(cboTitle, dtData, "NAME_VN", "ID")
            End Using
        End If
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objShift As New TR_STANDARD_TITLEDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, TrainingCommonTABLE_NAME.TR_STANDARD_TITLE) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objShift.CODE = txtCode.Text
                        If cboGroupTitle.SelectedValue <> "" Then
                            objShift.GROUP_TITLE_ID = cboGroupTitle.SelectedValue
                        End If
                        If cboTitle.SelectedValue <> "" Then
                            objShift.HU_TITLE_ID = cboTitle.SelectedValue
                        End If
                        If cboCourse.SelectedValue <> "" Then
                            objShift.TR_COURSE_ID = cboCourse.SelectedValue
                        End If
                        If cboStaffRank.SelectedValue <> "" Then
                            objShift.HU_STAFF_RANK_ID = cboStaffRank.SelectedValue
                        End If
                        objShift.EFFECTIVE_DATE_FROM = rdNgayHieuLuc.SelectedDate
                        objShift.EFFECTIVE_DATE_TO = rdNgayhetHL.SelectedDate
                        objShift.NOTE = txtNote.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertTR_STANDARD_TITLE(objShift, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objShift.ID = rgDanhMuc.SelectedValue
                                If rep.ModifyTR_STANDARD_TITLE(objShift, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objShift.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "STANDARD_TITLE")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
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
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New TrainingRepository
        Dim _validate As New TR_STANDARD_TITLEDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTR_STANDARD_TITLE(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTR_STANDARD_TITLE(_validate)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class