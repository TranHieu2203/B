Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities

Public Class ctrlLeaveHistory
    Inherits Common.CommonView
#Region "Property"
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property ProcessApprove As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property
    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property
#End Region

#Region "View"
    Property GridData As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_GridData")
        End Get
        Set(ByVal value As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_GridData") = value
        End Set
    End Property

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgData.AllowMultiRowSelection = False
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Refresh()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim _date As Date
            Dim fdate As Date
            Dim tdate As Date
            Dim str As String = Request.QueryString("Date")
            If str <> String.Empty Then
                _date = Date.Parse(str)
                fdate = _date.FirstDateOfMonth()
                tdate = _date.LastDateOfMonth()
            End If
            Dim rep As New AttendanceRepository
            Dim _filter As New HistoryLeaveDTO
            _filter.FROM_DATE = fdate
            _filter.TO_DATE = tdate
            _filter.EMPLOYEE_ID = Request.QueryString("EmpId")
            Dim list As New List(Of HistoryLeaveDTO)
            list = rep.GetHistoryLeave(_filter)
            rgData.DataSource = list
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim _date As Date
        Dim fdate As Date
        Dim tdate As Date
        Dim str As String = Request.QueryString("Date")
        If str <> String.Empty Then
            _date = Date.Parse(str)
            fdate = _date.FirstDateOfMonth()
            tdate = _date.LastDateOfMonth()
        End If
        rdFromDateSearch.SelectedDate = fdate
        rdToDateSearch.SelectedDate = tdate
    End Sub

#End Region

#Region "Events"

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            Dim rep As New AttendanceRepository
            Dim _filter As New HistoryLeaveDTO
            _filter.FROM_DATE = rdFromDateSearch.SelectedDate
            _filter.TO_DATE = rdToDateSearch.SelectedDate
            _filter.EMPLOYEE_ID = Request.QueryString("EmpId")
            Dim list As New List(Of HistoryLeaveDTO)
            list = rep.GetHistoryLeave(_filter)
            rgData.DataSource = list
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"
    Private Sub LoadComboData()
        Dim rep As New AttendanceRepository
        Try

        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region
End Class