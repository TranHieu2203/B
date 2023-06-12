Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramPrepare
    Inherits Common.CommonView

    Protected WithEvents ctrlFindPreparePopup As ctrlFindEmployeePopup
#Region "Property"

    Public Property Prepares As List(Of ProgramPrepareDTO)
        Get
            Return ViewState(Me.ID & "_Prepares")
        End Get
        Set(ByVal value As List(Of ProgramPrepareDTO))
            ViewState(Me.ID & "_Prepares") = value
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

    '0 - normal
    '1 - Prepare
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
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
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
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidProgramID.Value = Request.Params("PROGRAM_ID")
                Dim objProgram As ProgramDTO
                Using rep As New TrainingRepository
                    objProgram = rep.GetProgramById(hidProgramID.Value)
                End Using
                txtProgramName.Text = objProgram.TR_PROGRAM_CODE
                txtCourse.Text = objProgram.TR_COURSE_NAME
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
        Dim obj As New ProgramPrepareDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)
            obj.TR_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Prepares = rep.GetPrepare(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            Else
                Me.Prepares = rep.GetPrepare(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Me.Prepares
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            If phFindPrepare.Controls.Contains(ctrlFindPreparePopup) Then
                phFindPrepare.Controls.Remove(ctrlFindPreparePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindPreparePopup = Me.Register("ctrlFindPreparePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindPreparePopup.MustHaveContract = False
                    phFindPrepare.Controls.Add(ctrlFindPreparePopup)
                    ctrlFindPreparePopup.MultiSelect = False
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rgMain.SelectedValue IsNot Nothing Then
                        Dim item = (From p In Prepares Where p.ID = rgMain.SelectedValue).SingleOrDefault
                        txtCode.Text = item.EMPLOYEE_CODE
                        txtName.Text = item.EMPLOYEE_NAME
                        hidEmployeeID.Value = item.EMPLOYEE_ID
                        cboPrepare.SelectedValue = item.TR_LIST_PREPARE_ID
                    End If
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboPrepare, True)
                    btnFindPrepare.Enabled = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboPrepare, False)
                    btnFindPrepare.Enabled = False

                    txtCode.Text = ""
                    txtName.Text = ""
                    hidEmployeeID.Value = ""
                    cboPrepare.ClearSelection()
                    cboPrepare.Text = ""
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboPrepare, True)
                    btnFindPrepare.Enabled = True



                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of ProgramPrepareDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New ProgramPrepareDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeletePrepare(lstDeletes) Then
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
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository
                dtData = rep.GetOtherList("TR_LIST_PREPARE", True)
                FillRadCombobox(cboPrepare, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EMPLOYEE_CODE", txtCode)
            dic.Add("EMPLOYEE_NAME", txtName)
            dic.Add("EMPLOYEE_ID", hidEmployeeID)
            dic.Add("TR_LIST_PREPARE_ID", cboPrepare)
            dic.Add("TR_PROGRAM_ID", hidProgramID)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objPrepare As New ProgramPrepareDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    txtCode.Text = ""
                    txtName.Text = ""
                    cboPrepare.ClearSelection()
                    cboPrepare.Text = ""
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

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "Prepare")
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
                        objPrepare.EMPLOYEE_ID = hidEmployeeID.Value
                        objPrepare.TR_LIST_PREPARE_ID = cboPrepare.SelectedValue
                        objPrepare.TR_PROGRAM_ID = hidProgramID.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertPrepare(objPrepare, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objPrepare.ID = rgMain.SelectedValue
                                If rep.ModifyPrepare(objPrepare, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objPrepare.ID
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

    Protected Sub btnFindPrepare_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindPrepare.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindPreparePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try


    End Sub

    Private Sub ctrlFindPreparePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindPreparePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindPreparePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            Dim itm = lstCommonEmployee(0)
            hidEmployeeID.Value = itm.EMPLOYEE_ID
            txtCode.Text = itm.EMPLOYEE_CODE
            txtName.Text = itm.FULLNAME_VN
            isLoadPopup = 0
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