Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlInsListOrgInsurance
    Inherits Common.CommonView

#Region "Property"

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property dtData As DataTable
        Get
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Public Property codeEdit As String
        Get
            Return ViewState(Me.ID & "_codeEdit")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_codeEdit") = value
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

#End Region

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
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", txtID)
            dic.Add("CODE", txtCODE)
            dic.Add("NAME", txtNAME)
            dic.Add("ADDRESS", txtADDRESS)
            dic.Add("PHONE_NUMBER", txtPHONE_NUMBER)

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
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export, ToolbarItem.Active, ToolbarItem.Deactive)
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
                    txtCODE.Enabled = False
                    txtNAME.Enabled = False
                    txtADDRESS.Enabled = False
                    txtPHONE_NUMBER.Enabled = False
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    txtCODE.Enabled = True
                    txtNAME.Enabled = True
                    txtADDRESS.Enabled = True
                    txtPHONE_NUMBER.Enabled = True
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

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    CType(Me.MainToolBar.Items(5), RadToolBarButton).Enabled = False

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

        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    txtCODE.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    txtCODE.Focus()
                    codeEdit = txtCODE.Text

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Call ResetForm()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim result() As DataRow = dtData.Select("CODE = '" + txtCODE.Text + "'")
                    If (result.Count > 0 And codeEdit <> txtCODE.Text) Then
                        ShowMessage(Translate("Mã đơn vị bảo hiểm đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Call SaveData()
                    codeEdit = ""
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
                    'If InsCommon.CheckData_Delete("INS_LIST_INSURANCE", InsCommon.getNumber(txtID.Text)) Then
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    'Else
                    'ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    'End If
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
                    Call Export()
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
                If rep.UpdateStatusForList(Common.Common.GetUserName(), "LIST_INSURANCE" _
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
                If rep.UpdateStatusForList(Common.Common.GetUserName(), "LIST_INSURANCE" _
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
            txtCODE.Text = ""
            txtNAME.Text = ""
            txtADDRESS.Text = ""
            txtPHONE_NUMBER.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If rep.UpdateInsListInsurance(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                            , txtCODE.Text _
                                                                            , txtNAME.Text _
                                                                            , txtADDRESS.Text _
                                                                            , txtPHONE_NUMBER.Text) Then
                        Refresh("InsertView")

                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.STATE_EDIT
                    If rep.UpdateInsListInsurance(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                            , txtCODE.Text _
                                                                            , txtNAME.Text _
                                                                            , txtADDRESS.Text _
                                                                            , txtPHONE_NUMBER.Text) Then
                        Refresh("UpdateView")
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
            If rep.DeleteInsListInsurance(InsCommon.getNumber(txtID.Text)) Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListInsurance(True)
            dtData = lstSource
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

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListInsurance(True)

                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsListInsurance.xlsx"),
                    "DMDonViBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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