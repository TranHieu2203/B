Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_Unit
    Inherits Common.CommonView
    'TruongNN: 12/05/2016
    Private tsp As New TrainingStoreProcedure()
    Private log As New UserLog()

#Region "Property"
    Public Property Unit As DataTable
        Get
            Return ViewState(Me.ID & "_Unit")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_Unit") = value
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

    Protected Sub CreateDataFilter()
        Dim rep As New TrainingRepository
        Dim _filter As New CertificateDTO
        Try
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            Unit = tsp.UnitGetList()
            rgMain.DataSource = Unit

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    UpdateControStateNewEdit()

                Case CommonMessage.STATE_NORMAL

                    EnabledGridNotPostback(rgMain, True)
                    txtAddress.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtContact.ReadOnly = True
                    txtContactEmail.ReadOnly = True
                    txtContactPhone.ReadOnly = True
                    txtFax.ReadOnly = True
                    txtName.ReadOnly = True
                    txtPhone.ReadOnly = True
                    txtRemark.ReadOnly = True
                    txtRepresent.ReadOnly = True
                    txtTaxCode.ReadOnly = True
                    txtTrainField.ReadOnly = True
                    txtWeb.ReadOnly = True

                    CleanControl()

                Case CommonMessage.STATE_EDIT
                    UpdateControStateNewEdit()

                Case CommonMessage.STATE_DELETE
                    Dim idSelected = GetListSelected()
                    Dim tb As String = tsp.CheckUnitDelete(idSelected)
                    If tb = "" Then
                        If tsp.UnitDelete(idSelected) = 0 Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            UpdateControlState()
                        End If
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(tb, NotifyType.Error)
                        UpdateControlState()
                    End If


                Case CommonMessage.STATE_ACTIVE
                    Dim idSelected = GetListSelected()
                    If tsp.UnitActive(idSelected, -1) = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim idSelected = GetListSelected()
                    If tsp.UnitActive(idSelected, 0) = 1 Then
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
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("ADDRESS", txtAddress)
            dic.Add("PHONE", txtPhone)
            dic.Add("FAX", txtFax)
            dic.Add("WEB", txtWeb)
            dic.Add("TAX_CODE", txtTaxCode)
            dic.Add("TRAIN_FIELD", txtTrainField)
            dic.Add("REPRESENT", txtRepresent)
            dic.Add("CONTACT_PERSON", txtContact)
            dic.Add("CONTACT_PHONE", txtContactPhone)
            dic.Add("CONTACT_EMAIL", txtContactEmail)
            dic.Add("REMARK", txtRemark)
            dic.Add("DESC_U", txtDesc)

            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
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
                    Using xls As New ExcelCommon
                        If Unit.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, Unit, "Unit")
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
                    Dim id As Int32 = 0
                    Dim code As String = txtCode.Text.Trim()
                    Dim name As String = txtName.Text.Trim()
                    Dim address As String = txtAddress.Text.Trim()
                    Dim phone As String = txtPhone.Text.Trim()
                    Dim fax As String = txtFax.Text.Trim()
                    Dim web As String = txtWeb.Text.Trim()
                    Dim taxCode As String = txtTaxCode.Text.Trim()
                    Dim trainField As String = txtTrainField.Text.Trim()
                    Dim represent As String = txtRepresent.Text.Trim()
                    Dim contactPerson As String = txtContact.Text.Trim()
                    Dim contactPhone As String = txtContactPhone.Text.Trim()
                    Dim contactEmail As String = txtContactEmail.Text.Trim()
                    Dim remark As String = txtRemark.Text.Trim()
                    Dim desc_u As String = txtDesc.Text.Trim()

                    If Page.IsValid Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If tsp.UnitCreate(code, name, address, phone, fax, web, taxCode, trainField, represent, contactPerson, contactPhone, contactEmail, remark, desc_u, log.Username, log.ComputerName) = 1 Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                id = rgMain.SelectedValue
                                If tsp.UnitUpdate(id, code, name, address, phone, fax, web, taxCode, trainField, represent, contactPerson, contactPhone, contactEmail, remark, desc_u, log.Username, log.ComputerName) = 1 Then
                                    CurrentState = CommonMessage.STATE_NORMAL
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
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' Cập nhật trạng thái control New & Edit State
    Private Sub UpdateControStateNewEdit()
        EnabledGridNotPostback(rgMain, False)
        txtAddress.ReadOnly = False
        txtCode.ReadOnly = False
        txtContact.ReadOnly = False
        txtContactEmail.ReadOnly = False
        txtContactPhone.ReadOnly = False
        txtFax.ReadOnly = False
        txtName.ReadOnly = False
        txtPhone.ReadOnly = False
        txtRemark.ReadOnly = False
        txtRepresent.ReadOnly = False
        txtTaxCode.ReadOnly = False
        txtTrainField.ReadOnly = False
        txtWeb.ReadOnly = False
        txtDesc.ReadOnly = False
        If CurrentState = CommonMessage.STATE_NEW Then
            CleanControl()
        End If
    End Sub

    ' Clean Control
    Private Sub CleanControl()
        txtAddress.Text = String.Empty
        txtCode.Text = String.Empty
        txtContact.Text = String.Empty
        txtContactEmail.Text = String.Empty
        txtContactPhone.Text = String.Empty
        txtFax.Text = String.Empty
        txtName.Text = String.Empty
        txtPhone.Text = String.Empty
        txtRemark.Text = String.Empty
        txtRepresent.Text = String.Empty
        txtTaxCode.Text = String.Empty
        txtTrainField.Text = String.Empty
        txtWeb.Text = String.Empty
        txtDesc.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Lấy danh sách Item được chọn - 1,2,3
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListSelected() As String
        Dim str As String = String.Empty
        Dim item As GridDataItem
        Dim len As Int32 = 0
        For idx = 0 To rgMain.SelectedItems.Count - 1
            item = rgMain.SelectedItems(idx)
            str = str & item.GetDataKeyValue("ID") & ","
        Next
        len = str.Length
        If len > 0 Then
            str = str.Substring(0, len - 1)
        End If
        Return str
    End Function
#End Region

End Class