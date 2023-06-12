Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Telerik.Web.UI

Public Class ctrlTinhLuong
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
#Region "Property"
    Property vDataTK As DataTable
        Get
            Return ViewState(Me.ID & "_dtTK")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTK") = value
        End Set
    End Property
    Property vDataTH As DataTable
        Get
            Return ViewState(Me.ID & "_dtTH")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTH") = value
        End Set
    End Property

    Property vLstSalary As List(Of PAListSalariesDTO)
        Get
            Return ViewState(Me.ID & "_lstSalari")
        End Get
        Set(value As List(Of PAListSalariesDTO))
            ViewState(Me.ID & "_lstSalari") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgDataTK.AllowCustomPaging = True
            rgDataTK.PageSize = 50
            rgDataTH.AllowCustomPaging = True
            rgDataTH.PageSize = 50
            'rgDataTK.ClientSettings.EnablePostBackOnRowClick = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            rnmYear.Value = Date.Now.Year
            GetDataCombo()
            GetListSalary()
            rcboDsach.SelectedIndex = 1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarTerminates
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Refresh,
                                       ToolbarItem.Calculate)
            MainToolBar.Items(0).Text = Translate("Dùng")
            MainToolBar.Items(1).Text = Translate("Khóa")
            MainToolBar.Items(2).Text = Translate("Tải dữ liệu")
            MainToolBar.Items(3).Text = Translate("Tính lương")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub rcboDsach_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcboDsach.SelectedIndexChanged
        GetDataCalculate()
    End Sub
    Private Sub rnmYear_TextChanged(sender As Object, e As System.EventArgs) Handles rnmYear.TextChanged
        GetDataCombo()
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_ACTIVE
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                Case CommonMessage.TOOLBARITEM_REFRESH
                    'Select Case rtabSalary.SelectedIndex
                    '    Case 0
                    '        LoadDataCalculate()
                    '    Case 1
                    '        LoadDataSum()
                    'End Select
                    Load_Calculate_Load()
                    GetDataCalculate()
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    'Select Case rtabSalary.SelectedIndex
                    '    Case 0
                    '        CalculateDataTemp()
                    '    Case 1
                    '        CalculateDataSum()
                    'End Select
                    If Utilities.ObjToInt(rcboDsach.SelectedValue) <= 0 Then
                        ShowMessage("Dữ liệu chưa tải không thể tính toán", NotifyType.Warning)
                        Exit Sub
                    End If
                    CalculateDataSum()
                    GetDataCalculate()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            GetDataCalculate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgDataTH_ColumnCreated(sender As Object, e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgDataTH.ColumnCreated
        CreateCol(rgDataTH, 4123)
    End Sub

    Private Sub rgDataTK_ColumnCreated(sender As Object, e As Telerik.Web.UI.GridColumnCreatedEventArgs) Handles rgDataTK.ColumnCreated
        CreateCol(rgDataTK, 4122)
    End Sub

    Protected Sub rgDataTH_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDataTH.NeedDataSource
        Try
            rgDataTH.DataSource = Me.vDataTH
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgDataTK_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDataTK.NeedDataSource
        Try
            rgDataTK.DataSource = Me.vDataTK
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Tạo cột theo danh sách trong cơ sở dữ liệu
    ''' </summary>
    ''' <param name="rgData"> Lưới truyền vào để tạo cột</param>
    ''' <remarks></remarks>
    Protected Sub CreateCol(ByVal rgData As RadGrid, ByVal typePayment As Integer)
        Try

            Dim listcol() As String = {"cbStatus", "EMPLOYEE_CODE", "FULLNAME_VN", "ORG_NAME"}
            Dim i As Integer = 0
            While (i < rgData.Columns.Count)
                Dim c As GridColumn = rgData.Columns(i)
                If Not listcol.Contains(c.UniqueName) Then
                    rgData.Columns.Remove(c)
                    Continue While
                End If
                i = i + 1
            End While
            Dim stringKey As New List(Of String)
            stringKey.Add("ID")
            stringKey.Add("EMPLOYEE_CODE")
            stringKey.Add("FULLNAME_VN")
            stringKey.Add("ORG_NAME")
            If rcboDsach.SelectedValue = 0 Then
                Exit Sub
            End If
            For Each sal As PAListSalariesDTO In (From s In vLstSalary Where s.TYPE_PAYMENT = typePayment Order By s.COL_INDEX Ascending
                                                  Select New PAListSalariesDTO With
                                                         {.COL_NAME = s.COL_NAME, .COL_INDEX = s.COL_INDEX, .NAME_VN = s.NAME_VN}).ToList
                Dim col As New GridBoundColumn
                col.HeaderText = sal.NAME_VN
                col.DataField = sal.COL_NAME
                col.UniqueName = sal.COL_NAME
                col.DataFormatString = "{0:N0}"
                col.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                col.HeaderStyle.Width = 200
                rgData.MasterTableView.Columns.Add(col)
                stringKey.Add(col.DataField)
            Next
            rgData.MasterTableView.ClientDataKeyNames = stringKey.ToArray
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetDataCombo()
        Dim rep As New PayrollRepository
        Dim objPeriod As List(Of ATPeriodDTO)
        Dim id As Integer = 0
        Try
            If rnmYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                objPeriod = rep.GetPeriodbyYear(rnmYear.Value)
                rep.Dispose()
                rcboPeriod.DataSource = objPeriod
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                Dim lst = (From s In objPeriod Where s.MONTH = Date.Now.Month).FirstOrDefault
                If Not lst Is Nothing Then
                    rcboPeriod.SelectedValue = lst.ID
                End If
                rcboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub GetListSalary()
        Dim rep As New PayrollRepository
        Try
            vLstSalary = rep.GetListSalaryVisibleCol()
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub GetDataCalculate()
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Try
            Dim Sorts As String = rgDataTK.MasterTableView.SortExpressions.GetSortString()
            Dim ds As DataSet
            If Sorts IsNot Nothing Then
                ds = rep.GetLitsCalculate(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue), Sorts)
            Else
                ds = rep.GetLitsCalculate(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue))
            End If
            Me.vDataTK = ds.Tables(0)
            Me.vDataTH = ds.Tables(1)
            rgDataTK.VirtualItemCount = Me.vDataTK.Rows.Count
            rgDataTH.VirtualItemCount = Me.vDataTH.Rows.Count

            rgDataTK.Rebind()
            rgDataTH.Rebind()
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub Load_Calculate_Load()
        ' Tải dữ liệu bảng dữ liệu tính toán.
        ' Tính toán trên dữ liệu lương trong kỳ
        ' Tải dữ liệu sang lương tổng hợp
        Dim rep As New PayrollRepository
        Try
            If rep.Load_Calculate_Load(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue)) Then
                ShowMessage("Tải dữ liệu thành công.", NotifyType.Success)
            Else
                ShowMessage("Tải dữ liệu không thành công.", NotifyType.Error)
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub LoadDataCalculate()

        Dim rep As New PayrollRepository
        Try
            If rep.Load_data_calculate(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue)) Then
                ShowMessage("Tải dữ liệu thành công.", NotifyType.Success)
            Else
                ShowMessage("Tải dữ liệu không thành công.", NotifyType.Error)
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub CalculateDataTemp()
        Dim rep As New PayrollRepository
        Try
            If rep.Calculate_data_temp(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue)) Then
                ShowMessage("Tính lương thành công.", NotifyType.Success)
            Else
                ShowMessage("Tính lương không thành công.", NotifyType.Error)
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub LoadDataSum()

        Dim rep As New PayrollRepository
        Try
            If rep.Load_data_sum(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue)) Then
                ShowMessage("Tải dữ liệu thành công.", NotifyType.Success)
            Else
                ShowMessage("Tải dữ liệu không thành công.", NotifyType.Error)
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub CalculateDataSum()
        Dim rep As New PayrollRepository
        Try
            If rep.Calculate_data_sum(Utilities.ObjToInt(ctrlOrg.CurrentValue), Utilities.ObjToInt(rcboPeriod.SelectedValue), ctrlOrg.IsDissolve, Utilities.ObjToInt(rcboDsach.SelectedValue)) Then
                ShowMessage("Tính lương thành công.", NotifyType.Success)
            Else
                ShowMessage("Tính lương không thành công.", NotifyType.Error)
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class