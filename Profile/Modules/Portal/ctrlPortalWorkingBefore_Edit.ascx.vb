Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalWorkingBefore_Edit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgWorkingBefore.SetFilter()
            rgWorkingBeforeEdit.SetFilter()

            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator,
                         ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidWorkingID)
            dic.Add("COMPANY_ADDRESS", txtCompanyAddress)
            dic.Add("COMPANY_NAME", txtCompanyName)
            dic.Add("TELEPHONE", txtTelephone)
            dic.Add("TITLE_NAME", txtTitleName)
            dic.Add("LEVEL_NAME", txtLevelName)
            dic.Add("SALARY", txtSalary)
            dic.Add("TER_REASON", txtTerReason)
            dic.Add("JOIN_DATE", rdJoinDate)
            dic.Add("END_DATE", rdEndDate)
            Utilities.OnClientRowSelectedChanged(rgWorkingBefore, dic)

            Dim dic1 As New Dictionary(Of String, Control)
            dic1.Add("COMPANY_ADDRESS", txtCompanyAddress)
            dic1.Add("COMPANY_NAME", txtCompanyName)
            dic1.Add("TELEPHONE", txtTelephone)
            dic1.Add("TITLE_NAME", txtTitleName)
            dic1.Add("LEVEL_NAME", txtLevelName)
            dic1.Add("SALARY", txtSalary)
            dic1.Add("TER_REASON", txtTerReason)
            dic1.Add("JOIN_DATE", rdJoinDate)
            dic1.Add("END_DATE", rdEndDate)
            dic1.Add("ID", hidID)
            dic1.Add("FK_PKEY", hidWorkingID)
            Utilities.OnClientRowSelectedChanged(rgWorkingBeforeEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, hidWorkingID, txtCompanyName, txtLevelName, txtTitleName,
                                     txtCompanyAddress, txtSalary, txtTelephone, txtTerReason,
                                     rdEndDate, rdJoinDate)

                    EnabledGridNotPostback(rgWorkingBefore, True)
                    EnabledGridNotPostback(rgWorkingBeforeEdit, True)
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, hidWorkingID, txtCompanyName, txtLevelName, txtTitleName,
                                    txtCompanyAddress, txtSalary, txtTelephone, txtTerReason,
                                    rdEndDate, rdJoinDate)

                    EnabledGridNotPostback(rgWorkingBefore, False)
                    EnabledGridNotPostback(rgWorkingBeforeEdit, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, hidWorkingID, txtCompanyName, txtLevelName, txtTitleName,
                                    txtCompanyAddress, txtSalary, txtTelephone, txtTerReason,
                                    rdEndDate, rdJoinDate)

                    EnabledGridNotPostback(rgWorkingBefore, False)
                    EnabledGridNotPostback(rgWorkingBeforeEdit, False)
            End Select

            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(hidWorkingID, hidID, txtCompanyName, txtLevelName, txtTitleName,
                                   txtCompanyAddress, txtSalary, txtTelephone, txtTerReason,
                                   rdEndDate, rdJoinDate)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim objWorkingBefore As New WorkingBeforeDTOEdit
                        objWorkingBefore.EMPLOYEE_ID = EmployeeID

                        objWorkingBefore.COMPANY_ADDRESS = txtCompanyAddress.Text.Trim()
                        objWorkingBefore.COMPANY_NAME = txtCompanyName.Text.Trim()
                        objWorkingBefore.TITLE_NAME = txtTitleName.Text.Trim()
                        objWorkingBefore.LEVEL_NAME = txtLevelName.Text.Trim()
                        If txtSalary.Value IsNot Nothing Then
                            objWorkingBefore.SALARY = txtSalary.Value
                        End If
                        objWorkingBefore.TELEPHONE = txtTelephone.Text.Trim()
                        objWorkingBefore.TER_REASON = txtTerReason.Text.Trim()
                        objWorkingBefore.JOIN_DATE = rdJoinDate.SelectedDate()
                        objWorkingBefore.END_DATE = rdEndDate.SelectedDate()

                        Using rep As New ProfileBusinessRepository
                            If hidWorkingID.Value <> "" Then
                                objWorkingBefore.FK_PKEY = hidWorkingID.Value
                                Dim bCheck = rep.CheckExistWorkingBeforeEdit(hidWorkingID.Value)
                                If bCheck IsNot Nothing Then
                                    Dim status = bCheck.STATUS
                                    Dim pkey = bCheck.ID
                                    ' Trạng thái chờ phê duyệt
                                    If status = 1 Then
                                        ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    isInsert = False
                                    objWorkingBefore.ID = pkey
                                End If
                            End If


                            If hidID.Value <> "" Then
                                isInsert = False
                            End If

                            If isInsert Then
                                rep.InsertWorkingBeforeEdit(objWorkingBefore, 0)
                            Else
                                objWorkingBefore.ID = hidID.Value
                                rep.ModifyWorkingBeforeEdit(objWorkingBefore, 0)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgWorkingBefore.Rebind()
                            rgWorkingBeforeEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(hidWorkingID, hidID, txtCompanyName, txtLevelName, txtTitleName,
                                  txtCompanyAddress, txtSalary, txtTelephone, txtTerReason,
                                  rdEndDate, rdJoinDate)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgWorkingBeforeEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgWorkingBeforeEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 3 Then
                            ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorkingBefore_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkingBefore.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgWorkingBefore, New WorkingBeforeDTO)

            Using rep As New ProfileBusinessRepository
                rgWorkingBefore.DataSource = rep.GetWorkingBefore(EmployeeID)
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorkingBeforeEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkingBeforeEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgWorkingBeforeEdit, New WorkingBeforeDTOEdit)

            Using rep As New ProfileBusinessRepository
                rgWorkingBeforeEdit.DataSource = rep.GetWorkingBeforeEdit(New WorkingBeforeDTOEdit With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgWorkingBeforeEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkingBeforeEdit.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim item = CType(e.Item, GridDataItem)
                Dim status As String = ""
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    status = item.GetDataKeyValue("STATUS")
                End If
                Select Case status
                    Case 1
                        ShowMessage("Bản ghi đang Chờ phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 2
                        ShowMessage("Bản ghi đã Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 3
                        ShowMessage("Bản ghi không được Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case Else
                        CurrentState = CommonMessage.STATE_EDIT
                End Select
                hidWorkingID.Value = item.GetDataKeyValue("ID")

                txtCompanyAddress.Text = item.GetDataKeyValue("COMPANY_ADDRESS")
                txtCompanyName.Text = item.GetDataKeyValue("COMPANY_NAME")
                txtTelephone.Text = item.GetDataKeyValue("TELEPHONE")
                txtTitleName.Text = item.GetDataKeyValue("TITLE_NAME")
                txtLevelName.Text = item.GetDataKeyValue("LEVEL_NAME")
                txtSalary.Text = item.GetDataKeyValue("SALARY")
                txtTerReason.Text = item.GetDataKeyValue("TER_REASON")
                rdJoinDate.SelectedDate = item.GetDataKeyValue("JOIN_DATE")
                rdEndDate.SelectedDate = item.GetDataKeyValue("END_DATE")

                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidWorkingID.Value = item.GetDataKeyValue("FK_PKEY")
                End If
                hidID.Value = item.GetDataKeyValue("ID")
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkingBefore.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                CurrentState = CommonMessage.STATE_EDIT
                Dim item = CType(e.Item, GridDataItem)
                hidWorkingID.Value = item.GetDataKeyValue("ID")
                txtCompanyAddress.Text = item.GetDataKeyValue("COMPANY_ADDRESS")
                txtCompanyName.Text = item.GetDataKeyValue("COMPANY_NAME")
                txtTelephone.Text = item.GetDataKeyValue("TELEPHONE")
                txtTitleName.Text = item.GetDataKeyValue("TITLE_NAME")
                txtLevelName.Text = item.GetDataKeyValue("LEVEL_NAME")
                txtSalary.Text = item.GetDataKeyValue("SALARY")
                txtTerReason.Text = item.GetDataKeyValue("TER_REASON")
                rdJoinDate.SelectedDate = item.GetDataKeyValue("JOIN_DATE")
                rdEndDate.SelectedDate = item.GetDataKeyValue("END_DATE")

                hidWorkingID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgWorkingBeforeEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If lstID.Count > 0 Then
                        rep.SendWorkingBeforeEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgWorkingBefore.Rebind()
                    rgWorkingBeforeEdit.Rebind()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class