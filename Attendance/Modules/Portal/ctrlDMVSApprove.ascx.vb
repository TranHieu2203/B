Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlDMVSApprove
    Inherits Common.CommonView

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    Public ReadOnly Property ProcessApprove As String
        Get
            Return ATConstant.GSIGNCODE_WLEO
        End Get
    End Property
    Public ReadOnly Property PageId As String
        Get
            ' Lấy Page ID của form đăng ký
            Return "ctrlDMVSRegister"
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
        grvWaiting.AllowMultiRowSelection = False
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Refresh()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Dim db As New AttendanceRepository

            Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
                .EmployeeIdName = txtEmployee.Text,
                .FromDate = rdFromDateSearch.SelectedDate,
                .ToDate = rdToDateSearch.SelectedDate,
                .Status = 0
            }

            If cboLeaveTypeSearch.SelectedIndex <> 0 Then
                filter.SignId = Decimal.Parse(cboLeaveTypeSearch.SelectedValue)
            End If

            Dim listApprove = db.GetListWaitingForApprove(EmployeeID, ProcessApprove, filter)

            GridData = listApprove

            grvApproved.Rebind()
            grvDenied.Rebind()
            grvWaiting.Rebind()
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub BindData()
        LoadComboData()

        rdFromDateSearch.SelectedDate = Date.Now.FirstDateOfMonth()
        rdToDateSearch.SelectedDate = Date.Now.LastDateOfMonth()
    End Sub

#End Region

#Region "Events"
    Private Sub grvApproved_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvApproved.NeedDataSource
        Try
            Dim listApproved = GridData.Where(Function(p) p.STATUS = 2).ToList

            grvApproved.DataSource = listApproved
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub grvDenied_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvDenied.NeedDataSource
        Try
            Dim listDenied = GridData.Where(Function(p) p.STATUS = 3).ToList

            grvDenied.DataSource = listDenied
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub grvWaiting_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvWaiting.NeedDataSource
        Try
            Dim listWaiting = GridData.Where(Function(p) p.STATUS = 1).ToList

            grvWaiting.DataSource = listWaiting
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Refresh()
    End Sub

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Dim sAction As String = ""
        Try
            If grvWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn bản đăng ký cần phê duyệt."), NotifyType.Warning)
                Exit Sub
            End If

            Dim id = grvWaiting.SelectedValues("ID_REGGROUP")
            'linhnv
            'If Not AttendanceRepositoryStatic.Instance.CheckRegisterByRegGroup(id, sAction) Then
            '    ShowMessage(Translate(sAction), NotifyType.Warning)
            '    Exit Sub
            'End If
            Dim db As New AttendanceRepository

            If db.ApprovePortalRegister(id, EmployeeID, 2,
                                        txtNote.Text, Request.Url.Scheme & Request.Url.SchemeDelimiter & Request.Url.DnsSafeHost,
                                        ProcessApprove) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Refresh()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If

        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub

    Private Sub btnDeny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Try

            If grvWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn bản đăng ký cần phê duyệt."), NotifyType.Warning)
                Exit Sub
            End If
            If txtNote.Text.Length = 0 Then
                ShowMessage(Translate("Bạn chưa nhập ý kiến"), NotifyType.Warning)
                Exit Sub
            End If
            Dim id = grvWaiting.SelectedValues("ID_REGGROUP")

            Dim db As New AttendanceRepository

            If db.ApprovePortalRegister(id, EmployeeID, 3,
                                        txtNote.Text, Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost,
                                        ProcessApprove) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Refresh()
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If

        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub LoadComboData()
        Try
            Dim db As New AttendanceRepository
            Dim listAtSign = AttendanceRepositoryStatic.Instance.GetSignByPage(PageId)

            For Each item In listAtSign
                item.NAME_VN = "[" & item.CODE & "] " & item.NAME_VN
            Next

            Utilities.FillDropDownList(cboLeaveTypeSearch, listAtSign, "NAME_VN", "ID", Common.Common.SystemLanguage)
            cboLeaveTypeSearch.SelectedIndex = 0

        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region

End Class