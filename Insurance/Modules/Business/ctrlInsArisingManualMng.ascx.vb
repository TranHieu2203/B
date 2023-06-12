Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlInsArisingManualMng
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup

#Region "Property & Variable"

    Public Property popup As RadWindow
    Public Property popupId As String
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

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
        Try
            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            'ThanhNT added 02/03/2016
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Quản lý biến động bảo hiểm"
            popupId = popup.ClientID 'ThanhNT added 02/03/2016

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)

            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.CheckChildNodes = True
            'ctrlOrg.CheckBoxes = TreeNodeTypes.All

            If Not IsPostBack Then
                'ViewConfig(LeftPane)
                GirdConfig(rgGridData)
            End If

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            IDSelect = 0 'Khoi tao cho IdSelect = 0
            GetParams() 'ThanhNT added 02/03/2016                        
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Call LoadDataGrid()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        IDSelect = 0 'ThanhNT added 02/03/2016 reset lại IDSelect
        Call LoadDataGrid()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.SelectedItems.Count = 0 Then
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
                    'Call Export()
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUsername(), 0 _
                                                                    , txtEMPLOYEEID_SEARCH.Text _
                                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                    , txtFROMDATE.SelectedDate _
                                                                    , txtTODATE.SelectedDate _
                                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                    , InsCommon.getNumber(ctrlOrg.IsDissolve)
                                                                    )
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            ConvertColumnType(dtData, "SI_CHK", GetType(Boolean))
                            ConvertColumnType(dtData, "HI_CHK", GetType(Boolean))
                            ConvertColumnType(dtData, "UI_CHK", GetType(Boolean))
                            ConvertColumnType(dtData, "BHTNLDBNN_CHK", GetType(Boolean))

                            rgGridData.ExportExcel(Server, Response, dtData, "BienDongBH")
                            Exit Sub
                        End If
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ConvertColumnType(ByVal dt As DataTable, ByVal columnName As String, ByVal newType As Type)
        Using dc As DataColumn = New DataColumn(columnName & "_new", newType)
            Dim ordinal As Integer = dt.Columns(columnName).Ordinal
            dt.Columns.Add(dc)
            dc.SetOrdinal(ordinal)

            For Each dr As DataRow In dt.Rows
                dr(dc.ColumnName) = Convert.ChangeType(dr(columnName), newType)
            Next

            dt.Columns.Remove(columnName)
            dc.ColumnName = columnName
        End Using
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

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUsername(), 0 _
                                                            , txtEMPLOYEEID_SEARCH.Text _
                                                            , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                            , txtFROMDATE.SelectedDate _
                                                            , txtTODATE.SelectedDate _
                                                            , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                            , InsCommon.getNumber(IIf(ctrlOrg.IsDissolve, 1, 0))
                                                            )
            Dim maximumRows As Double = 0
            Dim startRowIndex As Double = 0
            Dim dtb As New DataTable
            If lstSource.Rows.Count = 0 Then
                rgGridData.DataSource = lstSource
                rgGridData.MasterTableView.VirtualItemCount = 0
                rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                If IsDataBind Then
                    rgGridData.DataBind()
                End If
                Return
            Else
                Dim filterExp = rgGridData.MasterTableView.FilterExpression
                If lstSource.Select(filterExp).AsEnumerable.Count = 0 Then
                    rgGridData.DataSource = dtb
                    rgGridData.MasterTableView.GroupsDefaultExpanded = True
                    rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
                    rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                    If IsDataBind Then
                        rgGridData.DataBind()
                    End If
                    Return
                Else
                    lstSource = lstSource.Select(filterExp).AsEnumerable.CopyToDataTable
                End If
                startRowIndex = IIf(lstSource.Rows.Count <= rgGridData.PageSize, 0, rgGridData.CurrentPageIndex * rgGridData.PageSize)
                maximumRows = IIf(lstSource.Rows.Count <= rgGridData.PageSize, lstSource.Rows.Count, Math.Min(startRowIndex + rgGridData.PageSize, lstSource.Rows.Count))
                dtb = lstSource.Clone()
                For i As Integer = startRowIndex To maximumRows - 1
                    dtb.ImportRow(lstSource.Rows(i))
                Next
            End If
            rgGridData.DataSource = dtb
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            Dim lstID As String = ""
            For Each selectedItem As GridDataItem In rgGridData.SelectedItems
                lstID = lstID + selectedItem.GetDataKeyValue("ID").ToString + ","
            Next
            isResult = rep.DeleteInsArisingManual(Common.Common.GetUsername(), InsCommon.getString(lstID))
            If isResult <> 1 Then
                isFail = 1
            End If

            'For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
            '    Dim item As GridDataItem = rgGridData.SelectedItems(i)
            '    If i = rgGridData.SelectedItems.Count - 1 Then
            '        lstID = lstID & item("ID").Text.ToString
            '    Else
            '        lstID = lstID & item("ID").Text.ToString & ","
            '    End If
            '    isResult = rep.DeleteInsArisingManual(Common.Common.GetUserName(), InsCommon.getString(lstID))
            '    If isResult <> 1 Then
            '        isFail = 1
            '    End If
            'Next
            If isFail = 0 Then
                Refresh("UpdateView")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    'ThanhNT addd 02/03/2016
    Private Sub GetParams()
        Dim sEmpID As String = String.Empty
        Dim sOrgID As String = String.Empty

        Try
            If CurrentState Is Nothing Then
                If Request.Params("EmpID") IsNot Nothing Then
                    sEmpID = Request.Params("EmpID")
                End If
                If Request.Params("OrgID") IsNot Nothing Then
                    sOrgID = Request.Params("OrgID")
                End If
                If Request.Params("IDSelect") IsNot Nothing Then
                    IDSelect = Request.Params("IDSelect")
                End If
            End If

        Catch ex As Exception
            Throw ex
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

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUsername(), 0 _
                                                                    , txtEMPLOYEEID_SEARCH.Text _
                                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                    , txtFROMDATE.SelectedDate _
                                                                    , txtTODATE.SelectedDate _
                                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                    , InsCommon.getNumber(ctrlOrg.IsDissolve)
                                                                    )
                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsArisingManual.xlsx"),
                    "BienDongBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            LoadDataGrid()
        Catch ex As Exception

        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGridData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("DEP_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class