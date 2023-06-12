Imports System.IO
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlMailTemplate
    Inherits CommonView

#Region "properties"

#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(tbarMain.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"


            rgGrid.AllowCustomPaging = True
            rgGrid.PageSize = Common.DefaultPageSize
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            '  rgGrid.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New CommonRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtTitle.ReadOnly = False
                    txtCC.ReadOnly = False
                    chkIsMailCC.Enabled = True
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboGROUP, True)


                Case CommonMessage.STATE_NORMAL

                    txtCode.ReadOnly = True
                    txtName.ReadOnly = True
                    txtTitle.ReadOnly = True
                    txtCC.ReadOnly = True
                    chkIsMailCC.Enabled = False
                    txtRemark.ReadOnly = True
                    Utilities.EnableRadCombo(cboGROUP, False)
                Case CommonMessage.STATE_EDIT
                    txtCode.ReadOnly = False
                    txtName.ReadOnly = False
                    txtTitle.ReadOnly = False
                    txtCC.ReadOnly = False
                    chkIsMailCC.Enabled = True
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboGROUP, True)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGrid.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGrid.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteMailTemplate(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
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
            Dim dtbModule As New DataTable
            'Module
            dtbModule = (New CommonProgramsRepository).FillComboboxModules_Mid()
            cboGROUP.DataSource = dtbModule
            cboGROUP.DataTextField = "NAME"
            cboGROUP.DataValueField = "CODE"
            cboGROUP.DataBind()
            cboGROUP.SelectedIndex = -1


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else

                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGrid.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgGrid.CurrentPageIndex = 0
                        rgGrid.MasterTableView.SortExpressions.Clear()
                        rgGrid.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgGrid.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objMailTemplate As New MailTemplateDTO
        Dim rep As New CommonRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtName, txtRemark, cboGROUP, txtTitle, txtCC, chkIsMailCC)
                    radEditContent.Content = ""
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE

                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgGrid.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgGrid.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_TITLE) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgGrid.ExportExcel(Server, Response, dtData, "Title")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        objMailTemplate.CODE = txtCode.Text
                        objMailTemplate.MAIL_CC = txtCC.Text
                        objMailTemplate.IS_MAIL_CC = chkIsMailCC.Checked
                        objMailTemplate.NAME = txtName.Text
                        objMailTemplate.REMARK = txtRemark.Text
                        objMailTemplate.TITLE = txtTitle.Text
                        If cboGROUP.SelectedValue <> "" Then
                            objMailTemplate.GROUP_MAIL = cboGROUP.SelectedValue
                        End If

                        objMailTemplate.CONTENT = radEditContent.Content


                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                If Not rep.CheckValidEmailTemplate(txtCode.Text, cboGROUP.SelectedValue) Then
                                    ShowMessage("Mã template đã tồn tại, vui lòng nhập mã khác", NotifyType.Alert)
                                    Exit Sub
                                End If

                                If rep.InsertMailTemplate(objMailTemplate) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objMailTemplate.ID = rgGrid.SelectedValue
                                If rep.ModifyMailTemplate(objMailTemplate) Then
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
                    ClearControlValue(txtCode, txtName, txtRemark, cboGROUP, txtTitle, txtCC, chkIsMailCC)
                    radEditContent.Content = ""
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

    Private Sub rgGrid_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        If rgGrid.SelectedItems.Count > 0 Then
            Dim slItem As GridDataItem

            slItem = rgGrid.SelectedItems(0)
            hidID.Value = slItem.GetDataKeyValue("ID")

            txtCode.Text = slItem.GetDataKeyValue("CODE")
            txtName.Text = slItem.GetDataKeyValue("NAME")
            txtTitle.Text = slItem.GetDataKeyValue("TITLE")
            txtCC.Text = slItem.GetDataKeyValue("MAIL_CC")
            chkIsMailCC.Checked = slItem.GetDataKeyValue("IS_MAIL_CC")
            cboGROUP.SelectedValue = slItem.GetDataKeyValue("GROUP_MAIL")
            txtRemark.Text = slItem.GetDataKeyValue("REMARK")
            radEditContent.Content = slItem.GetDataKeyValue("CONTENT")
        End If
    End Sub
#End Region

#Region "Custom"
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New CommonRepository
        Dim _filter As New MailTemplateDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgGrid, _filter)
            Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetMailTemplate(_filter, Sorts).ToTable()
                Else
                    Return rep.GetMailTemplate(_filter).ToTable()
                End If
            Else
                Dim MailTemplates As List(Of MailTemplateDTO)
                If Sorts IsNot Nothing Then
                    MailTemplates = rep.GetMailTemplate(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    MailTemplates = rep.GetMailTemplate(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
                'lstTitle = MailTemplates
                rgGrid.VirtualItemCount = MaximumRows
                rgGrid.DataSource = MailTemplates
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region







    'Private Sub btnImport_Click(sender As Object, e As System.EventArgs) Handles btnImport.Click
    '    ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif"
    '    ctrlUpload1.Show()
    'End Sub

    Private Sub ctrlUpload1_OkClicked(sender As Object, e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim listExtension = New List(Of String)
        listExtension.Add(".doc")
        listExtension.Add(".docx")
        Dim fileName As String
        If ctrlUpload1.UploadedFiles.Count >= 1 Then
            Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
            fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Common/"), file.FileName)
            file.SaveAs(fileName, True)
            Dim reader As StreamReader = New StreamReader(Server.MapPath("~/ReportTemplates/Common/" + file.FileName))
            Dim fileInfor As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Common/"), file.FileName))
            If fileInfor.Exists Then
                Using fileStream As New FileStream(Server.MapPath("~/ReportTemplates/Common/" + file.FileName), FileMode.Open, FileAccess.Read)
                    radEditContent.LoadRtfContent(reader.ReadToEnd)
                End Using
            End If

        End If
    End Sub
End Class