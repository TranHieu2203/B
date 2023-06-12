Imports Framework.UI
Imports Common
Imports Common.Common
Imports Framework.UI.Utilities
Imports System.IO
Imports Telerik.Web.UI
Imports Common.CommonView
Imports System.Threading
Imports Common.CommonBusiness
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports System.Configuration
Imports System.Net.Mime
Imports HistaffFrameworkPublic
Imports Common.CommonMessage
Imports System.Globalization

Public Class ctrlSalaryDetailPopup
    Inherits CommonView

    Delegate Sub SalarySelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event SalarySelected As SalarySelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick

#Region "Property"

    Public Overrides Property MustAuthorize As Boolean = False

    Private Property Opened As Boolean
        Get
            Return ViewState(Me.ID & "_Opened")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Opened") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    Public Property MultiSelect() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            rgSalary.AllowMultiRowSelection = False
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Show()
        Refresh()
        Opened = True
    End Sub

    Public Sub Hide()
        Opened = False
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub rgSalary_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSalary.NeedDataSource
        Try
            Try
                Dim emp_code = Request.QueryString("emp_code")
                'get store out with program id
                Dim rep As New HistaffFrameworkRepository
                Dim repc As New CommonRepository
                Dim lstParaValue As List(Of Object)
                lstParaValue = ctrlPROCESS.lstParaValueShare
                lstParaValue.Add(emp_code)
                Dim DataSourceDS As New DataSet
                DataSourceDS = rep.ExecuteToDataSet("PKG_PA_BUSINESS.LOAD_PAYROLL_SHEET_SUM_EMP", lstParaValue)
                If DataSourceDS Is Nothing Then
                    ShowMessage("Không có dữ liệu sau khi tính toán", NotifyType.Error)
                Else
                    DesignGrid(DataSourceDS)
                End If
                rgSalary.DataSource = DataSourceDS.Tables(0)
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub DesignGrid(ByVal ds As DataSet)
        Try
            Dim rCol As GridBoundColumn
            Dim columnCode As String
            Dim columnName As String
            If ds.Tables.Count > 1 Then
                rgSalary.MasterTableView.Columns.Clear()
                For i = 0 To ds.Tables(0).Columns.Count - 1
                    'create column name in RadGrid
                    columnCode = ds.Tables(0).Columns(i).ColumnName
                    columnName = If(ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault() Is Nothing, columnCode, ds.Tables(1).Select(String.Format("CODE = '{0}'", columnCode)).FirstOrDefault()("NAME"))
                    rCol = New GridBoundColumn()
                    If columnCode = "EMPLOYEE_CODE_DT" Then
                    Else
                        rgSalary.MasterTableView.Columns.Add(rCol)
                    End If
                    rCol.DataField = columnCode
                    rCol.HeaderText = columnName
                    rCol.HeaderTooltip = columnName
                    rCol.DataFormatString = "{0:#,##0.###}"
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.AutoPostBackOnFilter = True
                    rCol.AllowFiltering = True
                    If columnName.Length > 50 Then
                        rCol.HeaderStyle.Width = 150
                    Else
                        rCol.HeaderStyle.Width = 80
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

End Class