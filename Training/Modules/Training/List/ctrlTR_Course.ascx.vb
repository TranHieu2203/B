Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_Course
    Inherits Common.CommonView

    Private tsp As New TrainingStoreProcedure()

#Region "Property"

    Public Property Courses As List(Of CourseDTO)
        Get
            Return ViewState(Me.ID & "_Courses")
        End Get
        Set(ByVal value As List(Of CourseDTO))
            ViewState(Me.ID & "_Courses") = value
        End Set
    End Property

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
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
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New TrainingRepository
        Dim _filter As New CourseDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCourse(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCourse(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Courses = rep.GetCourse(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.Courses = rep.GetCourse(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.Courses
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
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboCertificateGroup, True)
                    Utilities.EnableRadCombo(cboCertificate, True)
                    Utilities.EnableRadCombo(cboTrainField, True)
                    Utilities.EnableRadCombo(cboProgramGroup, True)

                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    cboCertificateGroup.AutoPostBack = True
                    cboTrainField.AutoPostBack = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboCertificateGroup, False)
                    Utilities.EnableRadCombo(cboCertificate, False)
                    Utilities.EnableRadCombo(cboTrainField, False)
                    Utilities.EnableRadCombo(cboProgramGroup, False)
                    txtCode.ReadOnly = True
                    txtName.ReadOnly = True
                    txtRemark.ReadOnly = True
                    txtCode.Text = ""
                    txtName.Text = ""
                    txtRemark.Text = String.Empty
                    cboCertificateGroup.ClearSelection()
                    cboCertificateGroup.Text = ""
                    cboCertificate.ClearSelection()
                    cboCertificate.Text = ""
                    cboTrainField.ClearSelection()
                    cboTrainField.Text = ""
                    cboProgramGroup.ClearSelection()
                    cboProgramGroup.Text = ""
                    cboCertificateGroup.AutoPostBack = False
                    cboTrainField.AutoPostBack = False
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboCertificateGroup, True)
                    Utilities.EnableRadCombo(cboCertificate, True)
                    Utilities.EnableRadCombo(cboTrainField, True)
                    Utilities.EnableRadCombo(cboProgramGroup, True)
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False
                    cboCertificateGroup.AutoPostBack = True
                    cboTrainField.AutoPostBack = True

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of CourseDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New CourseDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteCourse(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveCourse(lstDeletes, True) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveCourse(lstDeletes, False) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository
                dtData = rep.GetOtherList("TR_TRAIN_FIELD", True)
                FillRadCombobox(cboTrainField, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("TR_CER_GROUP", True)
                FillRadCombobox(cboCertificateGroup, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("TR_CERTIFICATE_ID", cboCertificate)
            dic.Add("TR_CER_GROUP_ID", cboCertificateGroup)
            dic.Add("TR_TRAIN_FIELD_ID", cboTrainField)
            'dic.Add("TR_PROGRAM_GROUP_ID", cboProgramGroup)
            dic.Add("DRIVER", cbDriver)
            dic.Add("REMARK", txtRemark)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCourse As New CourseDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    cboCertificateGroup.ClearSelection()
                    cboCertificateGroup.Text = ""
                    cboCertificate.ClearSelection()
                    cboCertificate.Text = ""
                    cboTrainField.ClearSelection()
                    cboTrainField.Text = ""
                    cboProgramGroup.ClearSelection()
                    cboProgramGroup.Text = ""
                    txtCode.Text = ""
                    txtName.Text = ""
                    txtRemark.Text = String.Empty
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
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    If item.GetDataKeyValue("TR_CER_GROUP_ID") IsNot Nothing Then
                        cboCertificateGroup.SelectedValue = item.GetDataKeyValue("TR_CER_GROUP_ID")
                        cboCertificateGroup_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If item.GetDataKeyValue("TR_CERTIFICATE_ID") IsNot Nothing Then
                        cboCertificateGroup.SelectedValue = item.GetDataKeyValue("TR_CERTIFICATE_ID")
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
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
                            rgMain.ExportExcel(Server, Response, dtData, "Course")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objCourse.CODE = txtCode.Text
                        objCourse.NAME = txtName.Text
                        If cboCertificateGroup.SelectedValue <> "" Then
                            objCourse.TR_CER_GROUP_ID = cboCertificateGroup.SelectedValue
                            If cboCertificate.SelectedValue <> "" Then
                                objCourse.TR_CERTIFICATE_ID = cboCertificate.SelectedValue
                            End If
                        End If
                        If cboTrainField.SelectedValue <> "" Then
                            objCourse.TR_TRAIN_FIELD_ID = cboTrainField.SelectedValue
                        End If
                        If cboProgramGroup.SelectedValue <> "" Then
                            objCourse.TR_PROGRAM_GROUP_ID = cboProgramGroup.SelectedValue
                        End If
                        objCourse.DRIVER = cbDriver.Checked
                        objCourse.REMARK = txtRemark.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objCourse.ACTFLG = True
                                If rep.InsertCourse(objCourse, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCourse.ID = rgMain.SelectedValue
                                If rep.ModifyCourse(objCourse, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objCourse.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
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
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New TrainingRepository
        Dim _validate As New CourseDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateCourse(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateCourse(_validate)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCertificateGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCertificateGroup.SelectedIndexChanged
        Try
            If cboCertificateGroup.SelectedValue <> "" Then
                Dim dtData As DataTable
                Using rep As New TrainingRepository
                    dtData = rep.GetTrCertificateList(cboCertificateGroup.SelectedValue, True)
                    FillRadCombobox(cboCertificate, dtData, "NAME", "ID")
                End Using
            Else
                cboCertificate.Items.Clear()
                cboCertificate.SelectedValue = Nothing
                cboCertificate.Text = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboTrainField_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTrainField.SelectedIndexChanged
        Try
            Dim dtData As DataTable
            If cboTrainField.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    dtData = tsp.ProgramGroupGetList()
                    FillRadComboboxProgramGroupList(cboProgramGroup, dtData, "NAME", "ID", cboTrainField.SelectedValue)
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Dim dtData As DataTable
        Dim item = CType(rgMain.SelectedItems(0), GridDataItem)
        Dim ID = item.GetDataKeyValue("ID").ToString
        Dim data = (From p In Courses Where p.ID = Decimal.Parse(ID)).SingleOrDefault
        Using rep As New TrainingRepository
            dtData = tsp.ProgramGroupGetList()
            FillRadComboboxProgramGroupList(cboProgramGroup, dtData, "NAME", "ID", data.TR_TRAIN_FIELD_ID)
        End Using
        cboProgramGroup.SelectedValue = data.TR_PROGRAM_GROUP_ID

    End Sub

#End Region

#Region "Custom"

    Public Shared Sub FillRadComboboxProgramGroupList(ByVal cbo As RadComboBox,
                                   ByVal dtData As DataTable,
                                   ByVal sFieldText As String,
                                   ByVal sFieldValue As String,
                                   ByVal TFid As Decimal,
                                   Optional ByVal isFirstSelect As Boolean = False)
        Try

            cbo.DataValueField = sFieldValue
            cbo.DataTextField = sFieldText
            Dim Datarow() = dtData.Select("ACTFLG_ID <> 0 and TRAIN_FIELD_ID = " & TFid)
            If Datarow.Length > 0 Then
                Dim dt1data As DataTable = Datarow.CopyToDataTable()
                cbo.DataSource = dt1data
                If dt1data.Rows.Count > 0 And isFirstSelect Then
                    cbo.SelectedIndex = 0
                End If
                cbo.DataBind()
            Else
                Dim dt1data As DataTable = New DataTable
                cbo.DataSource = dt1data
                cbo.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    'Protected Sub rgMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
    '    Try
    '        If rgMain.SelectedItems.Count = 0 Then Exit Sub

    '        Dim item As GridDataItem = CType(rgMain.SelectedItems(0), GridDataItem)
    '        If item.GetDataKeyValue("TR_TRAIN_FIELD_ID") IsNot Nothing Then
    '            cboTrainField.SelectedValue = CDec(item.GetDataKeyValue("TR_TRAIN_FIELD_ID"))
    '        Else
    '            cboTrainField.SelectedValue = Nothing
    '            cboTrainField.ClearSelection()
    '            cboTrainField.Items.Clear()
    '        End If

    '        If item.GetDataKeyValue("CODE") IsNot Nothing Then
    '            txtCode.Text = item.GetDataKeyValue("CODE").ToString
    '        Else
    '            txtCode.ClearValue()
    '        End If

    '        If item.GetDataKeyValue("NAME") IsNot Nothing Then
    '            txtName.Text = item.GetDataKeyValue("NAME").ToString
    '        Else
    '            txtName.ClearValue()
    '        End If

    '        If item.GetDataKeyValue("TR_CER_GROUP_ID") IsNot Nothing Then
    '            cboCertificateGroup.SelectedValue = CDec(item.GetDataKeyValue("TR_CER_GROUP_ID"))
    '        Else
    '            cboCertificateGroup.SelectedValue = Nothing
    '            cboCertificateGroup.ClearSelection()
    '            cboCertificateGroup.Items.Clear()
    '        End If

    '        If item.GetDataKeyValue("TR_CERTIFICATE_ID") IsNot Nothing Then
    '            cboCertificate.SelectedValue = CDec(item.GetDataKeyValue("TR_CERTIFICATE_ID"))
    '        Else
    '            cboCertificate.SelectedValue = Nothing
    '            cboCertificate.ClearSelection()
    '            cboCertificate.Items.Clear()
    '        End If

    '        If item.GetDataKeyValue("REMARK") IsNot Nothing Then
    '            txtRemark.Text = item.GetDataKeyValue("REMARK").ToString
    '        Else
    '            txtRemark.ClearValue()
    '        End If

    '        If item.GetDataKeyValue("DRIVER") IsNot Nothing Then
    '            cbDriver.Value = CBool(item.GetDataKeyValue("DRIVER"))
    '        Else
    '            cbDriver.Value = False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class