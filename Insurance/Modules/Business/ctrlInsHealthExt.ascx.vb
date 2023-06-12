Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI


Public Class ctrlInsHealthExt
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

#Region "Property & Variable"
    Public Property popup As RadWindow
    Public Property popupId As String
    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property


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

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            GetDataCombo()

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Behaviors = WindowBehaviors.Close
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Export,
                                        ToolbarItem.Seperator)
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.CheckChildNodes = True
            'ctrlOrg.CheckBoxes = TreeNodeTypes.All

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'ShowPopupEmployee()
            rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
            End If

            'rgGridData.Culture = Common.Common.SystemLanguage
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)

            dic.Add("ID", txtID)
            'dic.Add("EMPLOYEE_CODE", txtEMPLOYEE_ID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

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

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)


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
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    ' CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False

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

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Call LoadDataGrid()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    'txtCODE.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
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
                    End If
                    If CheckData_Delete() Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    'rgGridData.ExportSettings.ExportOnlyData = True
                    'rgGridData.ExportSettings.OpenInNewWindow = True
                    'rgGridData.ExportSettings.FileName = "export.xls"
                    'rgGridData.ExportSettings.IgnorePaging = True
                    'rgGridData.MasterTableView.UseAllDataFields = True
                    'rgGridData.MasterTableView.ExportToExcel()
                    Call Export()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    Else
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Function CheckData_Delete() As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            'txtCODE.Text = ""
            'txtNAME.Text = ""
            'txtADDRESS.Text = ""
            'txtPHONE_NUMBER.Text = ""
            'txtTREATMENTCODE.Text = ""
            'chkSTATUS.Checked = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần lưu."), NotifyType.Warning)
                Exit Sub
            ElseIf txtEFFECTIVE_FROM_DATE.SelectedDate Is Nothing Then
                ShowMessage(Translate("Bạn chưa chọn Từ ngày."), NotifyType.Warning)
                Exit Sub
            ElseIf txtEFFECTIVE_TO_DATE.SelectedDate Is Nothing Then
                ShowMessage(Translate("Bạn chưa chọn Đến ngày."), NotifyType.Warning)
                Exit Sub
            ElseIf txtEFFECTIVE_FROM_DATE.SelectedDate > txtEFFECTIVE_TO_DATE.SelectedDate Then
                ShowMessage(Translate("Đến ngày phải lớn hơn Từ ngày."), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                'Dim id As Integer = Integer.Parse(item("ID").Text)
                Dim empid As Integer = Integer.Parse(item("EMPID").Text)
                isResult = rep.UpdateInsHealthExt(Common.Common.GetUserName(), 0, empid, 0, "" _
                                                  , txtEFFECTIVE_FROM_DATE.SelectedDate, txtEFFECTIVE_TO_DATE.SelectedDate _
                                                  , txtEFFECTIVE_FROM_DATE.SelectedDate, txtEFFECTIVE_TO_DATE.SelectedDate)
                If isResult = 0 Then
                    isFail = 1
                End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            Dim lstID As String = ""
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                If i = rgGridData.SelectedItems.Count - 1 Then
                    lstID = lstID & item("ID").Text.ToString
                Else
                    lstID = lstID & item("ID").Text.ToString & ","
                End If
            Next
            isResult = rep.DeleteInsHealthExt(Common.Common.GetUserName(), InsCommon.getString(lstID))
            If isResult = 1 Then
                Refresh("UpdateView")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Refresh("UpdateView")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            Call LoadDataGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsHealthExt(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                        , InsCommon.getString(txtEMPLOYEE_ID.Text) _
                                        , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                        , "" _
                                        , (txtEFFECTIVE_FROM_DATE.SelectedDate) _
                                        , (txtEFFECTIVE_TO_DATE.SelectedDate) _
                                        , (txtEFFECTIVE_FROM_DATE.SelectedDate) _
                                        , (txtEFFECTIVE_TO_DATE.SelectedDate))
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

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsHealthExt(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                         , InsCommon.getString(txtEMPLOYEE_ID.Text) _
                                         , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                         , "" _
                                         , (txtEFFECTIVE_FROM_DATE.SelectedDate) _
                                         , (txtEFFECTIVE_TO_DATE.SelectedDate) _
                                         , (txtEFFECTIVE_FROM_DATE.SelectedDate) _
                                         , (txtEFFECTIVE_TO_DATE.SelectedDate))
                'Dim lstSourceTMP As DataTable = lstSource.Clone()
                'If rgGridData.SelectedItems.Count <> lstSource.Rows.Count Then
                '    For Each dr As DataRow In lstSource.Rows
                '        For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                '            Dim item As GridDataItem = rgGridData.SelectedItems(i)
                '            Dim empid As Integer = Integer.Parse(item("EMPID").Text)
                '            If empid = dr("EMPID") Then
                '                lstSourceTMP.ImportRow(dr)
                '                Exit For
                '            End If
                '        Next
                '    Next
                'End If


                lstSource.TableName = "TABLE"



                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsHealthExt.xlsx"),
                    "GiaHanThe", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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