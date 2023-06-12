Imports Attendance.AttendanceBusiness
Imports Common
Imports Telerik.Web.UI
Public Class ctrlNewInOut
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
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
    Property EMPLOYEE_ID As Decimal?
        Get
            Return ViewState(Me.ID & "_EMPLOYEE_ID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_EMPLOYEE_ID") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'Refresh()
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

    Public Overrides Sub BindData()
        Try
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
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                  ByVal e As EventArgs) Handles btnEmployee.Click
        Try
            Select Case sender.ID
                Case btnEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim obj As AT_DATAINOUTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lstDataInout As New List(Of AT_DATAINOUTDTO)
                    Dim startdate = rdFromDate.SelectedDate.Value
                    Dim timeselect = rdhour.SelectedTime.Value
                    While startdate <= rdToDate.SelectedDate
                        obj = New AT_DATAINOUTDTO
                        obj.EMPLOYEE_CODE = txtEmployeeCode.Text
                        obj.EMPLOYEE_ID = EMPLOYEE_ID
                        obj.VALIN1 = New Date(startdate.Year, startdate.Month, startdate.Day,
                                              timeselect.Hours, timeselect.Minutes, 0)
                        obj.WORKINGDAY = startdate
                        lstDataInout.Add(obj)
                        startdate = startdate.AddDays(1)
                    End While
                    rep.InsertDataInout(lstDataInout, rdFromDate.SelectedDate, rdToDate.SelectedDate)
                    'Dim str As String = "getRadWindow().close('1');"
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    ''POPUPTOLINK
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlMngIO&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlMngIO&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try
            Try

                Select Case isLoadPopup
                    Case 1
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.MultiSelect = True
                            ctrlFindEmployeePopup.IsHideTerminate = False
                        End If
                End Select
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_DATAINOUTDTO
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.STAFF_RANK_NAME = lstCommonEmployee(idx).STAFF_RANK_NAME
                    isLoadPopup = 0
                    EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    txtEmployeeCode.Text = item.EMPLOYEE_CODE
                    txtEmployeeName.Text = item.VN_FULLNAME
                    txtTitleName.Text = item.TITLE_NAME
                    txtOrgName.Text = item.ORG_NAME
                    txtCapNS.Text = item.STAFF_RANK_NAME
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class

