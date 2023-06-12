Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalContractAppendix
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Dim psp As New ProfileStoreProcedure
#Region "Property"

    Private Property dt As DataTable
        Get
            Return PageViewState(Me.ID & "_dt")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dt") = value
        End Set
    End Property
    Public Property GridList As List(Of ContractDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of ContractDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property

    Public Property GridListAppendix As List(Of FileContractDTO)
        Get
            Return PageViewState(Me.ID & "_GridListAppendix")
        End Get
        Set(ByVal value As List(Of FileContractDTO))
            PageViewState(Me.ID & "_GridListAppendix") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
    'Thông tin cơ bản của nhân viên.
    'Property EmployeeInfo As EmployeeDTO
    '    Get
    '        Return PageViewState(Me.ID & "_EmployeeInfo")
    '    End Get
    '    Set(ByVal value As EmployeeDTO)
    '        PageViewState(Me.ID & "_EmployeeInfo") = value
    '    End Set
    'End Property
    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            rgGrid.SetFilter()
            rgGrid.Rebind()
            Me.CurrentPlaceHolder = Me.ViewName
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'Me.MainToolBar = tbarDetail
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case Common.CommonMessage.TOOLBARITEM_EXPORT
                    If dt.Rows.Count > 0 Then
                        rgGrid.ExportExcel(Server, Response, dt, "DanhSachQuaTrinhPhuLuc")
                    Else
                        ShowMessage(Translate("Không có dữ liệu để xuất báo cáo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Try
            Dim rep As New ProfileRepository
            Dim rep2 As New ProfileBusinessRepository
            Dim emp_info = rep2.GetEmployeeByEmployeeID(EmployeeID)
            dt = rep.GET_PROCESS_PLCONTRACT(emp_info.EMPLOYEE_CODE) 'rep.GET_PROCESS_PLCONTRACT_PORTAL(EmployeeID)
            rgGrid.DataSource = dt
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        'rgGrid.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgGrid.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And _
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgGrid.MasterTableView.Columns.Add(rCol)
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
                rgGrid.MasterTableView.Columns.Add(rColDate)
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


#End Region


End Class