Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlInsMaternityMng
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister
    Private store As New Store_Insurance_Business()
    Private userlog As UserLog
    Public Overrides Property MustAuthorize As Boolean = True

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

            ctrlOrg.AutoPostBack = False
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            'txtFROMDATE.SelectedDate = New Date(Now.Year, 1, 1)
            'txtTODATE.SelectedDate = New Date(Now.Year, 12, 31)

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Me.rgGridData.SetFilter()            
            'ThanhNT added 02/03/2016
            'If IDSelect <> 0 Then
            '    LoadDataGrid()
            '    SelectedItemDataGridByKey(rgGridData, Decimal.Parse(IDSelect))
            '    rgGridData_SelectedIndexChanged(rgGridData, New EventArgs())
            'End If 'ThanhNT added 02/03/2016            

            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

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

        'rgGridData.Rebind()
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
                    Call Export()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Call LoadDataGrid(False)
    End Sub

    'Private Sub rgGridData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGridData.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        datarow("PHONG_BAN").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
    '    End If
    'End Sub

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
            'Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUserName(), InsCommon.getNumber(IDSelect) _
            '                                                , txtEMPLOYEEID_SEARCH.Text _
            '                                                , InsCommon.getNumber(ctrlOrg.CurrentValue) _
            '                                                , txtFROMDATE.SelectedDate _
            '                                                , txtTODATE.SelectedDate _
            '                                                , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0))
            '                                                )
            Dim lstSource As DataTable = store.GET_INS_MATERNITY_MNG(Common.Common.GetUserName(), txtEMPLOYEEID_SEARCH.Text, Nothing, Nothing, InsCommon.getNumber(ctrlOrg.CurrentValue), InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)))

            Dim dc As DataColumn = New DataColumn("nghi_thai_san_new", Type.GetType("System.Boolean"))
            Dim ordinal = lstSource.Columns("nghi_thai_san").Ordinal
            lstSource.Columns.Add(dc)
            For Each dr As DataRow In lstSource.Rows
                dr("nghi_thai_san_new") = Convert.ChangeType(dr("nghi_thai_san"), Type.GetType("System.Boolean"))
            Next
            lstSource.Columns.Remove("nghi_thai_san")
            dc.ColumnName = "nghi_thai_san"

            '    Using (DataColumn dc = New DataColumn(columnName + "_new", newType))
            '{
            '    // Add the New column which has the New type, And move it to the ordinal of the old column
            '    Int ordinal = dt.Columns[columnName].Ordinal;
            '    dt.Columns.Add(dc);
            '    dc.SetOrdinal(ordinal);

            '    // Get And convert the values of the old column, And insert them into the New
            '    foreach(DataRow dr In dt.Rows)
            '        dr[dc.ColumnName] = Convert.ChangeType(dr[columnName], newType);

            '    // Remove the old column
            '    dt.Columns.Remove(columnName);

            '    // Give the New column the old column's name
            '    dc.ColumnName = columnName;
            '}

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

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            LoadDataGrid()
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
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                Dim id As Integer = Integer.Parse(item.GetDataKeyValue("ID"))
                'isResult = rep.DeleteInsArisingManual(Common.Common.GetUserName(), InsCommon.getNumber(id))
                userlog = New UserLog
                userlog = LogHelper.GetUserLog
                isResult = New CommonProcedureNew().DELETE_DATA_BY_TABLE(Contant_OtherList_Code_Insurance.table_INS_MATERNITY_MNG, id, userlog.Username, String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                If isResult <> 1 Then
                    isFail = 1
                End If
            Next
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

                'Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                '                                                    , txtEMPLOYEEID_SEARCH.Text _
                '                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                '                                                    , txtFROMDATE.SelectedDate _
                '                                                    , txtTODATE.SelectedDate _
                '                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0))
                '                                                    )
                Dim lstSource As DataTable = store.GET_INS_MATERNITY_MNG(Common.Common.GetUserName(), txtEMPLOYEEID_SEARCH.Text, Nothing, Nothing, InsCommon.getNumber(ctrlOrg.CurrentValue), InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)))

                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsMaternityMng.xlsx"),
                    "Quản lý thai sản", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
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