Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI

Public Class ctrlInsListOtherList
    Inherits Common.CommonView


    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property


#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = tbarOtherLists
            Me.ctrlMessageBox.Listener = Me
            Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()

            Dim dic As New Dictionary(Of String, Control)
            InitControl()

            'dic.Add("TYPE", txtTYPE)
            dic.Add("ID", txtID)
            'dic.Add("NAME", txtNAME)
            'dic.Add("STATUS", chkSTATUS)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists
            Common.Common.BuildToolbar(Me.MainToolBar,
                                        ToolbarItem.Create,
                                        ToolbarItem.Edit,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Save,
                                        ToolbarItem.Cancel,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Delete,
                                        ToolbarItem.Seperator,
                                       ToolbarItem.Export,
                                        ToolbarItem.Seperator, ToolbarItem.Active, ToolbarItem.Deactive)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
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
            Call LoadDataGrid()
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateToolbarState(CurrentState)
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    txtNAME.Enabled = False
                    chkSTATUS.Enabled = False
                    ddlNVAL1.Enabled = False
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    txtNAME.Enabled = True
                    chkSTATUS.Enabled = True
                    ddlNVAL1.Enabled = True
                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    txtNAME.Focus()
                    chkSTATUS.Checked = True
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    txtNAME.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                    Call ResetForm()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgGridData.SelectedItems.Count > 1 Then

                        ShowMessage(Translate("Vui lòng chỉ chọn 1 dòng dữ liệu để xóa!"), NotifyType.Warning)
                        Exit Sub
                    Else
                        Dim item As GridDataItem = rgGridData.SelectedItems(0)
                        txtID.Text = item.GetDataKeyValue("ID").ToString()
                    End If
                    If InsCommon.CheckData_Delete("INS_MASTERLIST", InsCommon.getNumber(txtID.Text)) Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'rgGridData.ExportSettings.ExportOnlyData = True
                    'rgGridData.ExportSettings.OpenInNewWindow = True
                    'rgGridData.ExportSettings.FileName = "export.xls"
                    'rgGridData.ExportSettings.IgnorePaging = True
                    'rgGridData.MasterTableView.UseAllDataFields = True
                    'rgGridData.MasterTableView.ExportToExcel()
                    Call Export()
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    'Private Sub rgGridData_ExcelExportCellFormatting(ByVal sender As Object, ByVal e As ExportCellFormattingEventArgs) Handles rgGridData.ExportCellFormatting
    '    Try
    '        e.Cell.Style("mso-number-format") = "\@"
    '        'If e.FormattedColumn.UniqueName = "JoinDate" Then
    '        '    e.Cell.Style("mso-number-format") = "dd\/mm\/yyyy"
    '        '    e.Cell.Style("width") = "100"
    '        '    e.Cell.Style("text-align") = "right"
    '        'ElseIf e.FormattedColumn.UniqueName <> "EmployeeCode" _
    '        '    And e.FormattedColumn.UniqueName <> "EmployeeName" _
    '        '    And e.FormattedColumn.UniqueName <> "PosName" Then
    '        '    e.Cell.Style("mso-number-format") = "\#\,\#\#0\.0"
    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Call LoadDataGrid(False)
    End Sub
    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call DeleteData()
                Call ResetForm()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstActive As String = ""
                For idx = 0 To rgGridData.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                    If idx = rgGridData.SelectedItems.Count - 1 Then
                        lstActive = lstActive & item.GetDataKeyValue("ID").ToString
                    Else
                        lstActive = lstActive & item.GetDataKeyValue("ID").ToString & ","
                    End If
                Next
                Dim rep As New InsuranceBusiness.InsuranceBusinessClient
                If rep.UpdateStatusForList(Common.Common.GetUserName(), "LIST_MASTERLIST" _
                                                                 , lstActive _
                                                                , InsCommon.getNumber(0)) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgGridData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim lstActive As String = ""
                For idx = 0 To rgGridData.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgGridData.SelectedItems(idx)
                    If idx = rgGridData.SelectedItems.Count - 1 Then
                        lstActive = lstActive & item.GetDataKeyValue("ID").ToString
                    Else
                        lstActive = lstActive & item.GetDataKeyValue("ID").ToString & ","
                    End If
                Next
                Dim rep As New InsuranceBusiness.InsuranceBusinessClient
                If rep.UpdateStatusForList(Common.Common.GetUserName(), "LIST_MASTERLIST" _
                                                                 , lstActive _
                                                                , InsCommon.getNumber(1)) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgGridData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub



#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            txtNAME.Text = ""
            InsCommon.SetNumber(ddlNVAL1, Nothing)
            chkSTATUS.Checked = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'ShowMessage("Đã tồn tại phòng ban cao nhất?", NotifyType.Warning)

                    If rep.UpdateInsListMasterlist(Common.Common.GetUserName(), ddlTYPE.SelectedValue _
                                                                                , txtID.Text _
                                                                                , txtNAME.Text _
                                                                                , txtSVAL1.Text _
                                                                                , txtSVAL2.Text _
                                                                                , txtSVAL3.Text _
                                                                                , InsCommon.getNumber(ddlNVAL1.SelectedValue) _
                                                                                , InsCommon.getNumber(txtNVAL2.Text) _
                                                                                , InsCommon.getNumber(txtNVAL3.Text) _
                                                                                , (txtDVAL1.SelectedDate) _
                                                                                , (txtDVAL2.SelectedDate) _
                                                                                , (txtDVAL3.SelectedDate) _
                                                                                , InsCommon.getNumber(txtIDX.Text) _
                                                                                , InsCommon.getNumberBoolean(True)) Then
                        Refresh("InsertView")
                        'Common.Common.OrganizationLocationDataSession = Nothing

                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                    'Common.Common.OrganizationLocationDataSession = Nothing
                Case CommonMessage.STATE_EDIT
                    'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                    If rep.UpdateInsListMasterlist(Common.Common.GetUserName(), ddlTYPE.SelectedValue _
                                                                                , txtID.Text _
                                                                                , txtNAME.Text _
                                                                                , txtSVAL1.Text _
                                                                                , txtSVAL2.Text _
                                                                                , txtSVAL3.Text _
                                                                                , InsCommon.getNumber(ddlNVAL1.SelectedValue) _
                                                                                , InsCommon.getNumber(txtNVAL2.Text) _
                                                                                , InsCommon.getNumber(txtNVAL3.Text) _
                                                                                , (txtDVAL1.SelectedDate) _
                                                                                , (txtDVAL2.SelectedDate) _
                                                                                , (txtDVAL3.SelectedDate) _
                                                                                , InsCommon.getNumber(txtIDX.Text) _
                                                                                , InsCommon.getNumberBoolean(chkSTATUS.Checked)) Then
                        Refresh("UpdateView")
                        'Common.Common.OrganizationLocationDataSession = Nothing
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim item As GridDataItem = rgGridData.SelectedItems(0)
            txtID.Text = item.GetDataKeyValue("ID").ToString()
            'objOrgFunction.ID = Decimal.Parse(hidID.Value)
            If rep.DeleteInsListMasterlist(txtID.Text) Then
                Refresh("UpdateView")
                'Common.Common.OrganizationLocationDataSession = Nothing
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim lstSource As DataTable = rep.GetInsListMasterlist(Common.Common.GetUsername(), "MODIFY_TYPE", 0, "", "", 0, 0, Nothing, Nothing, 1)
            FillRadCombobox(ddlNVAL1, lstSource, "NAME", "ID", True)

            'lstSource = rep.GetInsListMasterlist(Common.Common.GetUsername(), "INS_MASTERLIST", 0, "", "", 0, 0, Nothing, Nothing, 1)
            'FillRadCombobox(ddlTYPE, lstSource, "NAME", "ID", True)
            'ddlTYPE.SelectedValue = "MODIFY_INFO"


        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgGridData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGridData.SelectedIndexChanged
        Try
            If rgGridData.SelectedItems.Count > 0 Then
                Dim dr As GridDataItem
                dr = rgGridData.SelectedItems(0)
                'Session("dr") = dr
                txtID.Text = dr("ID").Text
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListMasterlist(Common.Common.GetUserName(), InsCommon.getString(ddlTYPE.SelectedValue), InsCommon.getNumber(txtID.Text), "", "", 0, 0, Nothing, Nothing, 0)

                InsCommon.SetString(txtNAME, lstSource.Rows(0)("NAME"))
                InsCommon.SetNumber(ddlNVAL1, lstSource.Rows(0)("NVAL1"))
                InsCommon.SetNumber(chkSTATUS, lstSource.Rows(0)("STATUS"))

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListMasterlist(Common.Common.GetUserName(), InsCommon.getString(ddlTYPE.SelectedValue), 0, "", "", 0, 0, Nothing, Nothing, 0)
            rgGridData.DataSource = lstSource
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex

            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)

            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListMasterlist(Common.Common.GetUserName(), InsCommon.getString(ddlTYPE.SelectedValue), 0, "", "", 0, 0, Nothing, Nothing, 0)

                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsListModify.xlsx"),
                    "DMThayDoiThongTinBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region
    
End Class