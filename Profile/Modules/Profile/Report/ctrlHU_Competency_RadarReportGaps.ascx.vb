Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlHU_Competency_RadarReportGaps

    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property


#End Region
    Public title As String = "Biểu đồ năng lực nhân viên đợt"
    Public title2 As String = "Biểu đồ năng lực nhân viên đợt"
    Public name1 As String = "Năng lực tối đa"
    Public name2 As String = "Năng lực cá nhân"
    Public name3 As String = "Năng lực chuẩn chức danh"
    Public categories As String = ""
    Public categories2 As String = ""
    Public dataset1 As String = ""
    Public dataset2 As String = ""
    Public dataset3 As String = ""
    Public dataset2_1 As String = ""
    Public dataset2_2 As String = ""
    Public dataset2_3 As String = ""
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            If Not IsPostBack Then
                CreateDataFilter()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateDataFilter()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidYear.Value = Request.Params("year")
                hidPeriodID.Value = Request.Params("periodid")
                If Request.Params("emp") IsNot Nothing Then
                    hidEmpID.Value = Request.Params("emp")
                    Using rep As New ProfileRepository
                        Dim obj As New CompetencyAssDTO
                        obj.EMPLOYEE_ID = hidEmpID.Value
                        obj.COMPETENCY_PERIOD_ID = hidPeriodID.Value
                        Dim lst = rep.GetCompetencyAss(obj)
                        If lst.Count > 0 Then
                            obj = lst(0)
                            txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                            txtEmployeeName.Text = obj.EMPLOYEE_NAME
                            hidTitleID.Value = obj.TITLE_ID
                            txtTitleName.Text = obj.TITLE_NAME
                            txtOrgName.Text = obj.ORG_NAME
                            Dim lstAppendix = rep.GetCompetencyAppendix(New CompetencyAppendixDTO With {.TITLE_ID = hidTitleID.Value})
                        End If
                    End Using
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter()
        If Not checkData() Then
            Exit Function
        End If
        title = "Biểu đồ năng lực nhân viên đợt " & cboCompetencyPeriod.Text.ToLower()
        title2 = "Biểu đồ năng lực nhân viên đợt " & cboCompetencyPeriod1.Text.ToLower()
        Dim rep As New ProfileRepository
        Dim _filter As New CompetencyAssDtlDTO
        Try
            If hidTitleID.Value <> "" Then
                _filter.TITLE_ID = hidTitleID.Value
            End If
            If hidEmpID.Value <> "" Then
                _filter.EMPLOYEE_ID = hidEmpID.Value
            End If
            If cboCompetencyPeriod.SelectedValue <> "" Then
                _filter.COMPETENCY_PERIOD_ID = cboCompetencyPeriod.SelectedValue
            End If
            
            Dim lst = rep.GetCompetencyAssDtl(_filter)
            If lst.Count > 0 Then
                For Each item As CompetencyAssDtlDTO In lst
                    categories &= "'" & item.COMPETENCY_NAME & "',"
                    dataset1 &= "4,"
                    dataset2 &= item.LEVEL_NUMBER_ASS & ","
                    dataset3 &= item.LEVEL_NUMBER_STANDARD & ","
                Next
                categories = categories.Remove(categories.Length - 1, 1)
                dataset1 = dataset1.Remove(dataset1.Length - 1, 1)
                dataset2 = dataset3.Remove(dataset2.Length - 1, 1)
                dataset3 = dataset3.Remove(dataset3.Length - 1, 1)
            Else
                dataset1 = ""
                dataset2 = ""
                dataset3 = ""
                categories = ""
            End If

            If cboCompetencyPeriod.SelectedValue <> "" Then
                _filter.COMPETENCY_PERIOD_ID = cboCompetencyPeriod1.SelectedValue
            End If
            lst = rep.GetCompetencyAssDtl(_filter)
            If lst.Count > 0 Then
                For Each item As CompetencyAssDtlDTO In lst
                    categories2 &= "'" & item.COMPETENCY_NAME & "',"
                    dataset2_1 &= "4,"
                    dataset2_2 &= item.LEVEL_NUMBER_ASS & ","
                    dataset2_3 &= item.LEVEL_NUMBER_STANDARD & ","
                Next
                categories2 = categories2.Remove(categories2.Length - 1, 1)
                dataset2_1 = dataset2_1.Remove(dataset2_1.Length - 1, 1)
                dataset2_2 = dataset2_2.Remove(dataset2_2.Length - 1, 1)
                dataset2_3 = dataset2_3.Remove(dataset2_3.Length - 1, 1)
            Else
                dataset2_1 = ""
                dataset2_2 = ""
                dataset2_3 = ""
                categories2 = ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function checkData() As Boolean
        If hidEmpID.Value = Nothing Then
            If IsPostBack Then
                ShowMessage(Translate("Bạn phải chọn nhân viên."), Utilities.NotifyType.Error)
            End If
            Return False
        End If
        If cboCompetencyPeriod.SelectedValue = Nothing Or cboCompetencyPeriod1.SelectedValue = Nothing Then
            If IsPostBack Then
                ShowMessage(Translate("Bạn phải chọn đợt đánh giá."), Utilities.NotifyType.Error)
            End If
            Return False
        End If
        Return True
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnEmployee)
                Case Else
                    EnableControlAll(True, btnEmployee)
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim table As New DataTable
            Dim tablePeriod As New DataTable
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
            If cboYear.SelectedValue <> "" Then
                Using rep As New ProfileRepository
                    tablePeriod = rep.GetHU_CompetencyPeriodList(cboYear.SelectedValue)
                    FillRadCombobox(cboCompetencyPeriod, tablePeriod, "NAME", "ID")
                End Using
            End If

            FillRadCombobox(cboYear1, table, "YEAR", "ID")
            If cboYear1.SelectedValue <> "" Then
                Using rep As New ProfileRepository
                    tablePeriod = rep.GetHU_CompetencyPeriodList(cboYear1.SelectedValue)
                    FillRadCombobox(cboCompetencyPeriod1, tablePeriod, "NAME", "ID")
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Dim dtData As DataTable
            Dim year As Decimal = 0
            If cboYear.SelectedValue <> "" Then
                year = cboYear.SelectedValue
            End If
            Using rep As New ProfileRepository
                dtData = rep.GetHU_CompetencyPeriodList(year)
                FillRadCombobox(cboCompetencyPeriod, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboYear1_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear1.SelectedIndexChanged
        Try
            Dim dtData As DataTable
            Dim year As Decimal = 0
            If cboYear1.SelectedValue <> "" Then
                year = cboYear1.SelectedValue
            End If
            Using rep As New ProfileRepository
                dtData = rep.GetHU_CompetencyPeriodList(year)
                FillRadCombobox(cboCompetencyPeriod1, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub


    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Try
            Dim emp = ctrlFindEmployeePopup.SelectedEmployee(0)
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtEmployeeName.Text = emp.FULLNAME_VN
            txtTitleName.Text = emp.TITLE_NAME
            txtOrgName.Text = emp.ORG_NAME
            hidTitleID.Value = emp.TITLE_ID
            hidEmpID.Value = emp.EMPLOYEE_ID
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class