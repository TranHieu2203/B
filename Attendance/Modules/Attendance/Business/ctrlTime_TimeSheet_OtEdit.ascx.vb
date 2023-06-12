Imports Attendance.AttendanceBusiness
Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Public Class ctrlTime_TimeSheet_OtEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property
    Property Period_id As Decimal?
        Get
            Return ViewState(Me.ID & "_Period_id")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Period_id") = value
        End Set
    End Property
    Property Org_id As Decimal?
        Get
            Return ViewState(Me.ID & "_Org_id")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Org_id") = value
        End Set
    End Property
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    Property RegisterLeave As AT_TIME_TIMESHEET_OTDTO
        Get
            Return ViewState(Me.ID & "_AT_TIME_TIMESHEET_OTDTO")
        End Get
        Set(ByVal value As AT_TIME_TIMESHEET_OTDTO)
            ViewState(Me.ID & "_AT_TIME_TIMESHEET_OTDTO") = value
        End Set
    End Property

    Property Shift As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_AT_SHIFTDTO")
        End Get
        Set(ByVal value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_AT_SHIFTDTO") = value
        End Set
    End Property
    Property Sign As List(Of AT_FMLDTO)
        Get
            Return ViewState(Me.ID & "_AT_FMLDTO")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
            ViewState(Me.ID & "_AT_FMLDTO") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Protected Property ListSign As List(Of AT_FMLDTO)
        Get
            Return PageViewState(Me.ID & "_ListSign")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
            PageViewState(Me.ID & "_ListSign") = value
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        Try
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_TIME_TIMESHEET_OTDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetTimeSheetOtById(obj)
                    RegisterLeave = New AT_TIME_TIMESHEET_OTDTO
                    If obj IsNot Nothing Then
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        txtChucDanh.Text = obj.TITLE_NAME
                        txtDonVi.Text = obj.ORG_NAME
                        txtName.Text = obj.VN_FULLNAME
                        Period_id = obj.PERIOD_ID
                        Employee_id = obj.EMPLOYEE_ID
                        Org_id = obj.ORG_ID
                        If obj.NUMBER_FACTOR_CP.HasValue Then
                            txtNUMBER_FACTOR_CP.Text = obj.NUMBER_FACTOR_CP
                        End If
                        If obj.NUMBER_FACTOR_PAY.HasValue Then
                            txtNUMBER_FACTOR_PAY.Text = obj.NUMBER_FACTOR_PAY
                        End If
                        If obj.BACKUP_MONTH_BEFFORE.HasValue Then
                            txtbackup.Text = obj.BACKUP_MONTH_BEFFORE
                        End If
                        _Value = obj.ID
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim obj As AT_TIME_TIMESHEET_OTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If _Value.HasValue Then
                        Dim _param = New PARAMDTO With {.EMPLOYEE_ID = Employee_id, _
                                                        .ORG_ID = Org_id, _
                                                        .PERIOD_ID = Period_id}
                        ' kiem tra ky cong da dong chua?
                        If rep.IS_PERIODSTATUS(_param) = False Then
                            ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm/sửa"), NotifyType.Error)
                            Exit Sub
                        End If
                        obj = New AT_TIME_TIMESHEET_OTDTO
                        obj.EMPLOYEE_CODE = txtEmployeeCode.Text.Trim
                        obj.PERIOD_ID = Period_id
                        If Not String.IsNullOrEmpty(txtNUMBER_FACTOR_CP.Text) Then
                            obj.NUMBER_FACTOR_CP = Decimal.Parse(txtNUMBER_FACTOR_CP.Text)
                        End If
                        If Not String.IsNullOrEmpty(txtNUMBER_FACTOR_PAY.Text) Then
                            obj.NUMBER_FACTOR_PAY = Decimal.Parse(txtNUMBER_FACTOR_PAY.Text)
                        End If
                        If Not String.IsNullOrEmpty(txtbackup.Text.Trim) Then
                            obj.BACKUP_MONTH_BEFFORE = Decimal.Parse(txtbackup.Text)
                        Else
                            obj.BACKUP_MONTH_BEFFORE = 0
                        End If
                        rep.ModifyLeaveSheetOt(obj, gstatus)
                    End If

                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand


    End Sub

#End Region

#Region "Custom"
    Private Sub GetDataCombo()

    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            Try

            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class

