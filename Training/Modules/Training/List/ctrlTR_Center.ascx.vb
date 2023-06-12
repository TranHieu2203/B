Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_Center
    Inherits Common.CommonView

#Region "Property"

    Public Property Centers As List(Of CenterDTO)
        Get
            Return ViewState(Me.ID & "_Centers")
        End Get
        Set(ByVal value As List(Of CenterDTO))
            ViewState(Me.ID & "_Centers") = value
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
        Dim _filter As New CenterDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCenter(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCenter(_filter).ToTable()
                End If
            Else

                If Sorts IsNot Nothing Then
                    Me.Centers = rep.GetCenter(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.Centers = rep.GetCenter(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.Centers
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
                    txtAddress.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtContactEmail.ReadOnly = False
                    txtContactPhone.ReadOnly = False
                    txtContactPerson.ReadOnly = False
                    txtDescription.ReadOnly = False
                    txtFax.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    txtNameVN.ReadOnly = False
                    txtPhone.ReadOnly = False
                    txtRepresent.ReadOnly = False
                    txtTaxCode.ReadOnly = False
                    txtTrainField.ReadOnly = False
                    txtWeb.ReadOnly = False
                    txtRemark.ReadOnly = False

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    txtAddress.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtContactEmail.ReadOnly = True
                    txtContactPhone.ReadOnly = True
                    txtContactPerson.ReadOnly = True
                    txtDescription.ReadOnly = True
                    txtFax.ReadOnly = True
                    txtNameEN.ReadOnly = True
                    txtNameVN.ReadOnly = True
                    txtPhone.ReadOnly = True
                    txtRepresent.ReadOnly = True
                    txtTaxCode.ReadOnly = True
                    txtTrainField.ReadOnly = True
                    txtWeb.ReadOnly = True
                    txtRemark.ReadOnly = True

                    txtAddress.Text = ""
                    txtCode.Text = ""
                    txtContactEmail.Text = ""
                    txtContactPhone.Text = ""
                    txtContactPerson.Text = ""
                    txtDescription.Text = ""
                    txtFax.Text = ""
                    txtNameEN.Text = ""
                    txtNameVN.Text = ""
                    txtPhone.Text = ""
                    txtRepresent.Text = ""
                    txtTaxCode.Text = ""
                    txtTrainField.Text = ""
                    txtRemark.Text = ""
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    txtAddress.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtContactEmail.ReadOnly = False
                    txtContactPhone.ReadOnly = False
                    txtContactPerson.ReadOnly = False
                    txtDescription.ReadOnly = False
                    txtFax.ReadOnly = False
                    txtNameEN.ReadOnly = False
                    txtNameVN.ReadOnly = False
                    txtPhone.ReadOnly = False
                    txtRepresent.ReadOnly = False
                    txtTaxCode.ReadOnly = False
                    txtTrainField.ReadOnly = False
                    txtWeb.ReadOnly = False
                    txtRemark.ReadOnly = False

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of CenterDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New CenterDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteCenter(lstDeletes) Then
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
                    If rep.ActiveCenter(lstDeletes, True) Then
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
                    If rep.ActiveCenter(lstDeletes, False) Then
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
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("CODE", txtCode)
        dic.Add("NAME_VN", txtNameVN)
        dic.Add("NAME_EN", txtNameEN)
        dic.Add("ADDRESS", txtAddress)
        dic.Add("PHONE", txtPhone)
        dic.Add("FAX", txtFax)
        dic.Add("WEB", txtWeb)
        dic.Add("CONTACT_PERSON", txtContactPerson)
        dic.Add("CONTACT_PHONE", txtContactPhone)
        dic.Add("CONTACT_EMAIL", txtContactEmail)
        dic.Add("TAX_CODE", txtTaxCode)
        dic.Add("TRAIN_FIELD", txtTrainField)
        dic.Add("DESCRIPTION", txtDescription)
        dic.Add("REPRESENT", txtRepresent)
        dic.Add("REMARK", txtRemark)
        Utilities.OnClientRowSelectedChanged(rgMain, dic)
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCenter As New CenterDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW

                    txtAddress.Text = ""
                    txtCode.Text = ""
                    txtContactEmail.Text = ""
                    txtContactPhone.Text = ""
                    txtContactPerson.Text = ""
                    txtDescription.Text = ""
                    txtFax.Text = ""
                    txtNameEN.Text = ""
                    txtNameVN.Text = ""
                    txtPhone.Text = ""
                    txtRepresent.Text = ""
                    txtTaxCode.Text = ""
                    txtTrainField.Text = ""
                    txtRemark.Text = ""
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
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Center")
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
                        objCenter.CODE = txtCode.Text
                        objCenter.NAME_VN = txtNameVN.Text
                        objCenter.NAME_EN = txtNameEN.Text
                        objCenter.ADDRESS = txtAddress.Text
                        objCenter.PHONE = txtPhone.Text
                        objCenter.FAX = txtFax.Text
                        objCenter.WEB = txtWeb.Text
                        objCenter.CONTACT_PERSON = txtContactPerson.Text
                        objCenter.CONTACT_PHONE = txtContactPhone.Text
                        objCenter.CONTACT_EMAIL = txtContactEmail.Text
                        objCenter.TAX_CODE = txtTaxCode.Text
                        objCenter.TRAIN_FIELD = txtTrainField.Text
                        objCenter.DESCRIPTION = txtDescription.Text
                        objCenter.REPRESENT = txtRepresent.Text
                        objCenter.REMARK = txtRemark.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objCenter.ACTFLG = True
                                If rep.InsertCenter(objCenter, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objCenter.ID = rgMain.SelectedValue
                                If rep.ModifyCenter(objCenter, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objCenter.ID
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
        Dim _validate As New CenterDTO
        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateCenter(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateCenter(_validate)
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