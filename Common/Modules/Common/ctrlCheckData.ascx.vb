Imports Common.CommonBusiness
Imports Framework.UI
Imports HistaffFrameworkPublic
Imports Telerik.Web.UI

Public Class ctrlCheckData
    Inherits CommonView
    Dim log As New UserLog

    Dim com As New CommonProcedureNew
    Dim rep As New HistaffFrameworkRepository

#Region "Property"
    Private Property strID As String
        Get
            Return PageViewState(Me.ID & "_strID")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_strID") = value
        End Set

    End Property
    Private Property dtCheckData As DataTable
        Get
            Return PageViewState(Me.ID & "_dtCheckData")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtCheckData") = value
        End Set
    End Property

    '0 - Điều chuyển thay đổi lương
    '1 - Điều chuyển hàng loạt
    Property FormType As String
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FormType") = value
        End Set
    End Property

    Public Property popupId As String

#End Region

#Region "Page"


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCheckData.SetFilter()
            rgCheckData.AllowCustomPaging = True
            rgCheckData.PageSize = Common.DefaultPageSize

            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Xuất dữ liệu"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim _error As Integer = 0
                    Using xls As New ExcelCommon
                        If dtCheckData.Rows.Count > 0 Then
                            rgCheckData.ExportExcel(Server, Response, dtCheckData, "Data")
                        End If
                    End Using
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnExcute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcute.Click
        Try
            txtSqlExcute.Text = txtSqlExcute.Text.Replace(";", "")
            dtCheckData = com.GET_DATA_BY_EXCUTE(txtSqlExcute.Text)
            DesignGrid(dtCheckData)
            rgCheckData.DataSource = dtCheckData
            rgCheckData.VirtualItemCount = dtCheckData.Rows.Count
            rgCheckData.Rebind()
        Catch ex As Exception
            ShowMessage(Translate("Thực thi câu lệnh bị lỗi"), Utilities.NotifyType.Warning, 5)
        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            If txtSqlUpdate.Text.Trim() = "" Then
                ShowMessage(Translate("Vui lòng nhập câu lệnh"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            Dim sql As Array = txtSqlUpdate.Text.Split(";")
            Dim countExcute As Int32 = 0

            For index = 0 To sql.Length - 1
                sql(index) = sql(index).Replace(";", "")
                If sql(index) <> "" Then
                    If com.UPDATE_DATA_BY_EXCUTE(sql(index)) Then
                        countExcute += 1
                    Else
                        RequiredFieldValidator1.IsValid = False
                        RequiredFieldValidator1.ToolTip = "Thực thi câu lệnh bị lỗi: <br/>" + sql(index).ToString
                        RequiredFieldValidator1.ErrorMessage = "Thực thi câu lệnh bị lỗi: <br/>" + sql(index).ToString
                        Exit Sub
                    End If
                End If
            Next
            If countExcute > 0 Then
                ShowMessage(Translate("Đã thực hiện thành công {0} câu lệnh", countExcute.ToString), Utilities.NotifyType.Success)
            End If
        Catch ex As Exception
            ShowMessage(Translate("Thực thi câu lệnh bị lỗi"), Utilities.NotifyType.Warning, 5)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCheckData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            ShowMessage(Translate("Thực thi câu lệnh bị lỗi"), Utilities.NotifyType.Warning, 5)
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgCheckData.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgCheckData.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If column.ColumnName <> "STATUS" And _
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgCheckData.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
                rCol.HeaderTooltip = Translate(column.ColumnName)
                rCol.FilterControlToolTip = Translate(column.ColumnName)
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgCheckData.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
                rColDate.HeaderTooltip = Translate(column.ColumnName)
                rColDate.FilterControlToolTip = Translate(column.ColumnName)
            End If
        Next
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable

        txtSqlExcute.Text = txtSqlExcute.Text.Replace(";", "")
        dtCheckData = com.GET_DATA_BY_EXCUTE(txtSqlExcute.Text)

        If Not IsPostBack Then
            DesignGrid(dtCheckData)
        End If
        rgCheckData.DataSource = dtCheckData
        rgCheckData.VirtualItemCount = dtCheckData.Rows.Count
    End Function


#End Region

End Class