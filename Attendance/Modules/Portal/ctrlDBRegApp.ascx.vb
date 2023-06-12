Imports Attendance.AttendanceBusiness
Imports Common

Public Class ctrlDBRegApp
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public Property StatisticType As String
        Get
            Return Session(Me.ID & "_StatisticType")
        End Get
        Set(ByVal value As String)
            Session(Me.ID & "_StatisticType") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Request("width") IsNot Nothing Then
                width = CInt(Request("width"))
            End If
            If Request("height") IsNot Nothing Then
                height = CInt(Request("height"))
            End If
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            If Not IsPostBack Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim db As New AttendanceRepository
            Dim filter As New ATRegSearchDTO With {
                .EmployeeIdName = String.Empty,
                .FromDate = Date.Now.FirstDateOfMonth(),
                .ToDate = Date.Now.LastDateOfMonth(),
            .Status = 0
            }
            Dim listApprove = db.PRS_DASHBOARD_BY_APPROVE(Session("_EmployeeID"), ATConstant.GSIGNCODE_LEAVE)
            'listApprove = listApprove.Where(Function(p) p.STATUS = 0).ToList()
            ltrTime_LEAVE.DataSource = listApprove
            ltrTime_LEAVE.DataBind()

            Dim listApproveOv As DataTable = db.PRS_DASHBOARD_BY_APPROVE(Session("_EmployeeID"), ATConstant.GSIGNCODE_OVERTIME)
            'listApproveOv = listApproveOv.Where(Function(p) p.STATUS = 0).ToList()
            ltrOVERTIME.DataSource = listApproveOv
            ltrOVERTIME.DataBind()


            'Dim listApproveDMVS = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_WLEO, filter)
            'listApproveDMVS = listApproveDMVS.Where(Function(p) p.STATUS = 0).ToList()
            'ltrWLEO.DataSource = listApproveDMVS
            'ltrWLEO.DataBind()

            'Dim listAssess As DataTable = db.GET_PE_ASSESS_MESS(LogHelper.CurrentUser.EMPLOYEE_ID)
            'ltrASSESS.DataSource = listAssess
            'ltrASSESS.DataBind()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
#End Region
End Class