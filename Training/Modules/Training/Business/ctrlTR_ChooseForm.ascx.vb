Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ChooseForm
    Inherits Common.CommonView

#Region "Property"

    Public Property ChooseForms As List(Of ChooseFormDTO)
        Get
            Return ViewState(Me.ID & "_ChooseForms")
        End Get
        Set(ByVal value As List(Of ChooseFormDTO))
            ViewState(Me.ID & "_ChooseForms") = value
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
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)
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

    Protected Sub CreateDataFilter()
        Dim rep As New TrainingRepository
        Dim obj As New ChooseFormDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.ChooseForms = rep.GetChooseForm(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            Else
                Me.ChooseForms = rep.GetChooseForm(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Me.ChooseForms
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rgMain.SelectedValue IsNot Nothing Then
                        Dim item = (From p In ChooseForms Where p.ID = rgMain.SelectedValue).SingleOrDefault
                        txtRemark.Text = item.REMARK
                        cboAssForm.Text = item.TR_ASSESSMENT_FORM_ID
                        rntxtYear.Value = item.YEAR
                        cboCourse.SelectedValue = item.TR_PROGRAM_ID
                    End If
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboCourse, True)
                    Utilities.EnableRadCombo(cboAssForm, True)
                    rntxtYear.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtYear.AutoPostBack = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboCourse, False)
                    Utilities.EnableRadCombo(cboAssForm, False)
                    rntxtYear.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rntxtYear.AutoPostBack = False

                    txtRemark.Text = ""
                    cboCourse.ClearSelection()
                    cboCourse.Text = ""
                    cboAssForm.ClearSelection()
                    cboAssForm.Text = ""
                    rntxtYear.Value = Date.Now.Year
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboCourse, True)
                    Utilities.EnableRadCombo(cboAssForm, True)
                    rntxtYear.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rntxtYear.AutoPostBack = True

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of ChooseFormDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New ChooseFormDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteChooseForm(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rntxtYear.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData
            Using rep As New TrainingRepository
                dtData = rep.GetTrAssFormList(False)
                FillRadCombobox(cboAssForm, dtData, "NAME", "ID")
            End Using
            rntxtYear.Value = Date.Now.Year
            GetProgramInYear()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("TR_ASSESSMENT_FORM_ID", cboAssForm)
            dic.Add("REMARK", txtRemark)
            dic.Add("YEAR", rntxtYear)
            dic.Add("TR_PROGRAM_ID;TR_PROGRAM_NAME", cboCourse)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objChooseForm As New ChooseFormDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtRemark.Text = ""
                    cboCourse.ClearSelection()
                    cboCourse.Text = ""
                    cboAssForm.ClearSelection()
                    cboAssForm.Text = ""
                    rntxtYear.Value = Date.Now.Year
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
                    cboCourse.Items.Clear()
                    cboCourse.ClearSelection()
                    rntxtYear_TextChanged(Nothing, Nothing)
                    Dim item As GridDataItem = rgMain.SelectedItems(0)
                    If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
                        cboCourse.SelectedValue = item.GetDataKeyValue("TR_PROGRAM_ID")
                    End If
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "ChooseForm")

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        With objChooseForm
                            .TR_ASSESSMENT_FORM_ID = cboAssForm.SelectedValue
                            .REMARK = txtRemark.Text
                            .TR_PROGRAM_ID = cboCourse.SelectedValue
                            .YEAR = rntxtYear.Value
                        End With
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertChooseForm(objChooseForm, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objChooseForm.ID = rgMain.SelectedValue
                                If rep.ModifyChooseForm(objChooseForm, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objChooseForm.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
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
        Dim _validate As New ChooseFormDTO
        Try
            If cboCourse.SelectedValue <> "" Then
                _validate.TR_PROGRAM_ID = cboCourse.SelectedValue
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.ID = rgMain.SelectedValue
                    args.IsValid = rep.ValidateChooseForm(_validate)
                Else
                    args.IsValid = rep.ValidateChooseForm(_validate)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rntxtYear_TextChanged(sender As Object, e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetProgramInYear()
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

    Protected Sub GetProgramInYear()
        Try

            If rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramByYear(True, rntxtYear.Value)
                    FillRadCombobox(cboCourse, dtData, "NAME", "ID")
                End Using
            Else
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex

        End Try

    End Sub
#End Region

End Class