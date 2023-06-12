Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlPortalTimeTimesheetDetails
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase

#Region "Property"
    Public Property GridList As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Get
            Return PageViewState(Me.ID & "_GridList")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_MONTHLYDTO))
            PageViewState(Me.ID & "_GridList") = value
        End Set
    End Property

    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            GridList = Nothing
            rgiTime.DataSource = Me.GridList

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_MONTHLYDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgiTime, obj)
            Dim _param = New PARAMDTO With {.ORG_ID = 46, _
                                            .IS_DISSOLVE = False,
                                            .IS_FULL = True}
            Dim Sorts As String = rgiTime.MasterTableView.SortExpressions.GetSortString()

            If cboPeriod.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriod.SelectedValue
                obj.EMPLOYEE_ID = EmployeeID
            Else
                obj.PERIOD_ID = 0
                obj.EMPLOYEE_ID = 0
            End If



            If Sorts IsNot Nothing Then
                Me.GridList = rep.GetTimeSheetPortal(obj, _param, MaximumRows, rgiTime.CurrentPageIndex, rgiTime.PageSize, "CREATED_DATE desc")
            Else
                Me.GridList = rep.GetTimeSheetPortal(obj, _param, MaximumRows, rgiTime.CurrentPageIndex, rgiTime.PageSize)
            End If

            rgiTime.VirtualItemCount = MaximumRows
            rgiTime.DataSource = Me.GridList
            rgiTime.DataBind()



        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            'Dim dtable As DataTable
            Dim rp As New AttendanceRepository

            If cboPeriod.SelectedValue = "" Then
                ShowMessage(Translate("Chưa chọn kỳ công ."), NotifyType.Warning)
                Exit Sub
            End If

            If Not rp.CheckPeriod(cboPeriod.SelectedValue, EmployeeID) Then
                ShowMessage(Translate("Kỳ công đang mở, không xem được."), NotifyType.Warning)
                Exit Sub
            End If

            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO

        If cboYear.SelectedValue <> "" Then
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()

                    rgiTime.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
                End If
            End If
        End If

    End Sub


#End Region

#Region "Custom"

#End Region
End Class