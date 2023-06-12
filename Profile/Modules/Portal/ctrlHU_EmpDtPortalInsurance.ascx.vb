Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI



Public Class ctrlHU_EmpDtPortalInsurance
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
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

    Private Property dt2 As DataTable
        Get
            Return PageViewState(Me.ID & "_dt2")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dt2") = value
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
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property
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
            EmployeeInfo = New Profile.ProfileBusiness.EmployeeDTO
            EmployeeInfo.EMPLOYEE_CODE = LogHelper.CurrentUser.EMPLOYEE_CODE
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            rgGrid.SetFilter()
            rgGrid.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'Me.MainToolBar = tbarDetail
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export, ToolbarItem.Import)
            'CType(MainToolBar.Items(0), RadToolBarButton).Text = Translate("Xuất thông tin bảo hiểm")
            'CType(MainToolBar.Items(1), RadToolBarButton).Text = Translate("Xuất thông tin biến động")
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                        'rgGrid.ExportExcel(Server, Response, dt, "DanhSachThongtinBaoHiem")
                    Else
                        ShowMessage(Translate("Không có dữ liệu để xuất báo cáo"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    If dt2.Rows.Count > 0 Then
                        'rgGrid2.ExportExcel(Server, Response, dt2, "DanhSachBienDongBaoHiem")
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

            dt = psp.GET_PROCESS_INSURANCE_INFOR(EmployeeInfo.EMPLOYEE_CODE)
            If Not IsPostBack Then
                DesignGrid(dt)
            End If
            rgGrid.DataSource = dt

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                SetString(txtSOCIAL_NUMBER, dt.Rows(0)("SOCIAL_NUMBER"))
                SetNumber(txtSALARY, dt.Rows(0)("JOIN_SALARY_INS"))
                SetDate(txtSI_FROM_MONTH, dt.Rows(0)("SI_FROM_MONTH"))

                SetString(txtHEALTH_NUMBER, dt.Rows(0)("HEALTH_NUMBER"))
                SetString(rtRegisterPlace, dt.Rows(0)("REGISTER_PLACE"))
                SetDate(txtHI_FROM_MONTH, dt.Rows(0)("HI_FROM_MONTH"))

                SetString(ddlHEALTH_AREA_INS_ID, dt.Rows(0)("REG_PLACE"))
                SetDate(txtBHTNLD_BNN_FROM_MONTH, dt.Rows(0)("UNEMP_FROM_MONTH"))
                dt.Dispose()

            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Shared Sub SetString(rtb As RadTextBox, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.Text = ""
            Else
                rtb.Text = obj
            End If
        Catch ex As Exception
            rtb.Text = ""
        End Try
    End Sub
    Public Shared Sub SetDate(rtb As RadMonthYearPicker, obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.SelectedDate = Nothing
            Else
                rtb.SelectedDate = obj
            End If
        Catch ex As Exception
            rtb.SelectedDate = Nothing
        End Try
    End Sub
    Public Shared Sub SetNumber(rtb As RadNumericTextBox, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.Value = Nothing
            Else
                rtb.Text = obj
            End If
        Catch ex As Exception
            rtb.Value = Nothing
        End Try
    End Sub
    Private Sub rgGrid2_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid2.NeedDataSource
        Try

            dt2 = psp.GET_PROCESS_INSURANCE_ARISING(EmployeeInfo.EMPLOYEE_CODE)
            If Not IsPostBack Then
                DesignGrid2(dt2)
            End If
            rgGrid2.DataSource = dt2
            dt2.Dispose()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rNCol As GridNumericColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgGrid.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgGrid.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") And
             Not column.ColumnName.Contains("FROM_MONTH") And Not column.ColumnName.Contains("TO_MONTH") And Not column.ColumnName.Contains("JOIN_SALARY_INS") Then
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
            End If
            If column.ColumnName.Contains("FROM_MONTH") OrElse column.ColumnName.Contains("TO_MONTH") Then
                rColDate = New GridDateTimeColumn()
                rgGrid.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FMONTHYEARGRID")
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

            If column.ColumnName.Contains("JOIN_SALARY_INS") Then
                rNCol = New GridNumericColumn()
                rgGrid.MasterTableView.Columns.Add(rNCol)
                rNCol.DataField = column.ColumnName
                rNCol.DataFormatString = ConfigurationManager.AppSettings("FNUMBERGRID")
                rNCol.HeaderText = Translate(column.ColumnName)
                rNCol.HeaderStyle.Width = 150
                rNCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rNCol.AutoPostBackOnFilter = True
                rNCol.CurrentFilterFunction = GridKnownFunction.Contains
                rNCol.ShowFilterIcon = False
                rNCol.FilterControlWidth = 130
            End If
        Next
    End Sub

    Protected Sub DesignGrid2(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rNCol As GridNumericColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgGrid2.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgGrid2.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") And
             Not column.ColumnName.Contains("SALARY_PRE_PERIOD") And Not column.ColumnName.Contains("SALARY_NOW_PERIOD") And Not column.ColumnName.Contains("A_SI") And
             Not column.ColumnName.Contains("R_SI") And Not column.ColumnName.Contains("A_HI") And Not column.ColumnName.Contains("R_HI") And Not column.ColumnName.Contains("A_UI") And
             Not column.ColumnName.Contains("R_UI") Then
                rCol = New GridBoundColumn()
                rgGrid2.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgGrid2.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
            End If
            If column.ColumnName.Contains("SALARY_PRE_PERIOD") OrElse column.ColumnName.Contains("SALARY_NOW_PERIOD") OrElse column.ColumnName.Contains("A_SI") OrElse
                column.ColumnName.Contains("R_SI") OrElse column.ColumnName.Contains("A_HI") OrElse column.ColumnName.Contains("R_HI") OrElse column.ColumnName.Contains("A_UI") OrElse
                column.ColumnName.Contains("R_UI") Then
                rNCol = New GridNumericColumn()
                rgGrid2.MasterTableView.Columns.Add(rNCol)
                rNCol.DataField = column.ColumnName
                rNCol.DataFormatString = ConfigurationManager.AppSettings("FNUMBERGRID")
                rNCol.HeaderText = Translate(column.ColumnName)
                rNCol.HeaderStyle.Width = 150
                rNCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rNCol.AutoPostBackOnFilter = True
                rNCol.CurrentFilterFunction = GridKnownFunction.Contains
                rNCol.ShowFilterIcon = False
                rNCol.FilterControlWidth = 130
            End If
        Next
    End Sub
#End Region


End Class
