Imports Attendance.AttendanceBusiness
Imports Common
Imports Common.Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Public Class ctrlDMVSTime_Timesheet
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True
    Dim psp As New AttendanceStoreProcedure
#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    ''' <summary>
    ''' Obj LEAVESHEET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LEAVESHEET As DataTable
        Get
            Return ViewState(Me.ID & "_LEAVESHEET")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_LEAVESHEET") = value
        End Set
    End Property


    Property LeaveMasters As List(Of AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_LeaveMasters")
        End Get
        Set(ByVal value As List(Of AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_LeaveMasters") = value
        End Set
    End Property

    Property LeaveMasterTotal As Int32
        Get
            Return ViewState(Me.ID & "_LeaveMasterTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_LeaveMasterTotal") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
            End Select
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = 50
        SetFilter(rgMain)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Đăng ký"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            If Not IsPostBack Then
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
                FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
                If lsData.Count > 0 Then
                    Dim periodid = (From d In lsData Where d.MONTH = Date.Now.Month And d.YEAR = Date.Now.Year Select d).FirstOrDefault
                    If periodid IsNot Nothing Then
                        cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    Else
                        cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                    End If
                End If
                Dim dtData = rep.GetOtherList("OBJECT_EMPLOYEE")
                FillRadCombobox(cboEmpObj, dtData, "NAME", "ID", True)
                Dim emp = rep.GetEmpId(EmployeeID)
                cboEmpObj.SelectedValue = emp.OBJECT_EMPLOYEE_ID
                cboEmpObj_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDMVSMng")
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim lstApp As New List(Of AT_PORTAL_REG_DTO)
            Dim strId As String
            Dim sign_id As Integer
            Dim period_id As Integer
            Dim id_group As Integer
            Dim sumday As Integer
            For Each dr As GridDataItem In rgMain.SelectedItems
                strId += dr.GetDataKeyValue("ID").ToString + ","
            Next
            strId = strId.Remove(strId.LastIndexOf(",")).ToString
            Dim dtCheckSendApprove As DataTable = psp.CHECK_APPROVAL(strId)
            If dtCheckSendApprove.Rows.Count > 0 Then
                If dtCheckSendApprove(0)("MESSAGE") > 1 Then
                    ShowMessage(Translate("Không thể gửi phê duyệt các loại nghỉ khác nhau cùng lúc"), NotifyType.Warning)
                    Exit Sub
                End If
            End If
            period_id = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("PERIOD_ID"))
            sign_id = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SIGN_ID"))
            id_group = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("ID_REGGROUP"))
            sumday = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SUMDAY"))
            Using rep As New AttendanceRepository
                Dim check = rep.CHECK_PERIOD_CLOSE(period_id)

                If check = 0 Then
                    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            End Using
            Dim outNumber As Decimal

            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Dim r As New Random
            Dim sb As New StringBuilder
            For i As Integer = 1 To 32
                Dim idx As Integer = r.Next(0, 35)
                sb.Append(s.Substring(idx, 1))
            Next
            Dim token = sb.ToString() + EmployeeID.ToString

            Try
                Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                outNumber = IAttendance.PRI_PROCESS_APP(EmployeeID, period_id, "LEAVE", 0, sumday, sign_id, id_group, token)
            Catch ex As Exception
                ShowMessage(ex.ToString, NotifyType.Error)
            End Try

            If outNumber = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            ElseIf outNumber = 1 Then
                ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
            ElseIf outNumber = 2 Then
                ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
            ElseIf outNumber = 3 Then
                ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
            End If
            rgMain.Rebind()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim repS As New AttendanceStoreProcedure
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = 1
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)

            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        Finally
            rep.Dispose()
        End Try

    End Sub

    Private Sub cboEmpObj_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmpObj.SelectedIndexChanged
        Try
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim rep = New AttendanceRepository
            Dim ddate = rep.Load_date(CDec(Val(cboPeriod.SelectedValue)), CDec(Val(cboEmpObj.SelectedValue)))

            If ddate.END_DATE IsNot Nothing And ddate.START_DATE IsNot Nothing Then
                rdtungay.SelectedDate = ddate.START_DATE
                rdDenngay.SelectedDate = ddate.END_DATE
            Else
                ClearControlValue(rdtungay, rdDenngay)
            End If
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_MACHINETDTO
        Try
            If Not IsPostBack Then
                rgMain.VirtualItemCount = 0
                rgMain.DataSource = New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
                Exit Function
            End If
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            obj.PERIOD_ID = CDec(Val(cboPeriod.SelectedValue))
            If cboEmpObj.SelectedValue <> "" Then
                obj.EMP_OBJ_ID = CDec(Val(cboEmpObj.SelectedValue))
            End If
            If rdtungay.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            obj.EMPLOYEE_ID = EmployeeID
            For Each item In cbFilter.CheckedItems
                If item.Value = 1 Then
                    obj.IS_LATE = True
                End If
                If item.Value = 2 Then
                    obj.IS_EARLY = True
                End If
                If item.Value = 3 Then
                    obj.IS_REALITY = True
                End If
                If item.Value = 4 Then
                    obj.IS_NON_WORKING_VALUE = True
                End If
                If item.Value = 5 Then
                    obj.IS_WRONG_SHIFT = True
                End If
            Next
            Dim LIST_STATUS_SEARCH As New List(Of String)
            If obj.IS_LATE = True Or obj.IS_EARLY = True Then
                LIST_STATUS_SEARCH.Add("DITRE")
                LIST_STATUS_SEARCH.Add("DITRE_VESOM")
            End If
            If obj.IS_REALITY = True Then
                LIST_STATUS_SEARCH.Add("THIEUQT")
            End If
            If obj.IS_NON_WORKING_VALUE = True Then
                LIST_STATUS_SEARCH.Add("KHONGQT")
            End If
            If obj.IS_WRONG_SHIFT = True Then
                LIST_STATUS_SEARCH.Add("SAICA")
            End If
            obj.LIST_STATUS_SEARCH = LIST_STATUS_SEARCH
            If obj.IS_LATE = True And obj.IS_EARLY = True And obj.IS_REALITY = True And obj.IS_NON_WORKING_VALUE = True And obj.IS_WRONG_SHIFT = True Then
                obj.LIST_STATUS_SEARCH = New List(Of String)
            End If
            Dim lstTimeSheet As New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstTimeSheet = rep.GetPortalEmpMachines(obj, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
                Else
                    lstTimeSheet = rep.GetPortalEmpMachines(obj, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            Else
                Return rep.GetPortalEmpMachines(obj).ToTable()
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = lstTimeSheet
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

End Class