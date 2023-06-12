Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports WebAppLog

Public Class ctrlPortalMngIETNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property EmployeeID As Decimal

    Property lstAssesmentDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Get
            Return ViewState(Me.ID & "_lstAssesmentDetail")
        End Get
        Set(ByVal value As List(Of PE_KPI_ASSESMENT_DETAIL_DTO))
            ViewState(Me.ID & "_lstAssesmentDetail") = value
        End Set
    End Property

    Public Property lstDeleteID As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstDeleteID")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstDeleteID") = value
        End Set
    End Property

    Public Property is_Confirm As Decimal
        Get
            Return ViewState(Me.ID & "_is_Confirm")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_is_Confirm") = value
        End Set
    End Property

    Public Property status_id As Decimal
        Get
            Return ViewState(Me.ID & "_status_id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_status_id") = value
        End Set
    End Property
    Public Property is_Change As Decimal
        Get
            Return ViewState(Me.ID & "_is_Change")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_is_Change") = value
        End Set
    End Property

    Public Property dtSource As DataTable
        Get
            Return ViewState(Me.ID & "_dtSource")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtSource") = value
        End Set
    End Property

    Public Property lstExistID As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstExistID")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstExistID") = value
        End Set
    End Property

    Public Property FormType As Decimal
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_FormType") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                GetParams()
            End If
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim repCM As New CommonRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                EmployeeID = LogHelper.CurrentUser.EMPLOYEE_ID
                Dim objEmp = repCM.GetEmployeeID(EmployeeID)
                txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                txtEmployeeName.Text = objEmp.FULLNAME_VN
                txtTitleName.Text = objEmp.TITLE_NAME
                txtOrgName.Text = objEmp.ORG_NAME

                Select Case Message
                    Case "UpdateView"
                        Dim rep As New PerformanceRepository
                        Dim obj As New KPI_ASSESSMENT_DTO
                        Dim reps As New PerformanceStoreProcedure
                        Dim dtData As New DataTable
                        obj = rep.GetKpiAssessmentByID(hidGoalID.Value)
                        Me.status_id = obj.STATUS_ID
                        If obj.YEAR IsNot Nothing Then
                            cboYear.SelectedValue = obj.YEAR
                            dtData = reps.GET_PE_PERIOD_BY_YEAR(True, cboYear.SelectedValue)
                            FillRadCombobox(cboPeriod, dtData, "NAME", "ID", True)
                            If obj.PE_PERIOD_ID IsNot Nothing Then
                                cboPeriod.SelectedValue = obj.PE_PERIOD_ID
                            End If
                        End If
                        If obj.START_DATE IsNot Nothing Then
                            rdStartDate.SelectedDate = obj.START_DATE
                        End If
                        If obj.END_DATE IsNot Nothing Then
                            rdEndDate.SelectedDate = obj.END_DATE
                        End If
                        If obj.EFFECT_DATE IsNot Nothing Then
                            rdEffectDate.SelectedDate = obj.EFFECT_DATE
                        End If
                        txtMonthNumber.Text = obj.NUMBER_MONTH
                        txtGoal.Text = obj.GOAL
                        txtNote.Text = obj.REMARK
                        If obj.assesmentDetail.Count > 0 Then
                            lstAssesmentDetail = obj.assesmentDetail
                        End If
                        CurrentState = CommonMessage.STATE_EDIT


                        If (obj.STATUS_ID <> 3 AndAlso obj.STATUS_ID <> 2) OrElse (obj.IS_CONFIRM IsNot Nothing AndAlso (obj.IS_CONFIRM = 0 Or obj.IS_CONFIRM = 4)) Then
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If

                        dtData = reps.CHECK_JOIN_DATE_EMP(EmployeeID, cboPeriod.SelectedValue)
                        If dtData.Rows.Count > 0 Then
                            Dim numberMonth As Decimal = CDec(dtData.Rows(0).Item(4))
                            hidNumberMonth.Value = numberMonth
                        End If

                        If obj.IS_CONFIRM IsNot Nothing Then
                            Me.is_Confirm = obj.IS_CONFIRM
                            If obj.IS_CONFIRM = 1 Then
                                EnableControlAll(False, cboYear, cboPeriod, rdStartDate, rdEndDate, rdEffectDate, txtMonthNumber, txtGoal, txtNote, cboCriteria,
                                                 txtCriteria, cboUnit, cboFrequency, txtRemark, cboSource, cboCriteriaType, cboEvaluateType, txtTargets, txtThreshold, rnWeighted)
                            End If
                        End If

                    Case "InsertView"
                        CurrentState = CommonMessage.STATE_NEW
                        Me.is_Confirm = 3
                    Case "NormalView"
                        Dim rep As New PerformanceRepository
                        Dim obj As New KPI_ASSESSMENT_DTO
                        Dim reps As New PerformanceStoreProcedure
                        Dim dtData As New DataTable
                        obj = rep.GetKpiAssessmentByID(hidGoalID.Value)
                        Me.status_id = obj.STATUS_ID
                        If obj.YEAR IsNot Nothing Then
                            cboYear.SelectedValue = obj.YEAR
                            dtData = reps.GET_PE_PERIOD_BY_YEAR(True, cboYear.SelectedValue)
                            FillRadCombobox(cboPeriod, dtData, "NAME", "ID", True)
                            If obj.PE_PERIOD_ID IsNot Nothing Then
                                cboPeriod.SelectedValue = obj.PE_PERIOD_ID
                            End If
                        End If
                        If obj.START_DATE IsNot Nothing Then
                            rdStartDate.SelectedDate = obj.START_DATE
                        End If
                        If obj.END_DATE IsNot Nothing Then
                            rdEndDate.SelectedDate = obj.END_DATE
                        End If
                        If obj.EFFECT_DATE IsNot Nothing Then
                            rdEffectDate.SelectedDate = obj.EFFECT_DATE
                        End If
                        txtMonthNumber.Text = obj.NUMBER_MONTH
                        txtGoal.Text = obj.GOAL
                        txtNote.Text = obj.REMARK
                        If obj.assesmentDetail.Count > 0 Then
                            lstAssesmentDetail = obj.assesmentDetail
                        End If
                        CurrentState = CommonMessage.STATE_EDIT
                        If obj.IS_CONFIRM IsNot Nothing Then
                            Me.is_Confirm = obj.IS_CONFIRM
                        End If
                        If (obj.STATUS_ID <> 3 AndAlso obj.STATUS_ID <> 2) OrElse (obj.IS_CONFIRM IsNot Nothing AndAlso (obj.IS_CONFIRM = 0 Or obj.IS_CONFIRM = 4)) Then
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If

                        dtData = reps.CHECK_JOIN_DATE_EMP(EmployeeID, If(cboPeriod.SelectedValue <> "", cboPeriod.SelectedValue, 0))
                        If dtData.Rows.Count > 0 Then
                            Dim numberMonth As Decimal = CDec(dtData.Rows(0).Item(4))
                            hidNumberMonth.Value = numberMonth
                        End If
                        cboYear.Enabled = False
                        cboPeriod.Enabled = False
                        rdStartDate.Enabled = False
                        rdEndDate.Enabled = False
                        rdEffectDate.Enabled = False
                        txtMonthNumber.Enabled = False
                        txtGoal.Enabled = False
                        txtNote.Enabled = False
                        cboCriteria.Enabled = False
                        txtCriteria.Enabled = False
                        cboUnit.Enabled = False
                        cboFrequency.Enabled = False
                        txtRemark.Enabled = False
                        cboSource.Enabled = False
                        cboCriteriaType.Enabled = False
                        cboEvaluateType.Enabled = False
                        txtTargets.Enabled = False
                        txtThreshold.Enabled = False
                        rnWeighted.Enabled = False
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            Dim dtData As New DataTable
            If cboYear.SelectedValue <> "" Then
                dtData = rep.GET_PE_PERIOD_BY_YEAR(True, cboYear.SelectedValue)
                FillRadCombobox(cboPeriod, dtData, "NAME", "ID", True)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            Dim dtData As New DataTable
            If cboPeriod.SelectedValue <> "" Then
                dtData = rep.CHECK_JOIN_DATE_EMP(EmployeeID, cboPeriod.SelectedValue)
                Dim joinDate As Date = CDate(dtData.Rows(0).Item(0))
                Dim startDate As Date = CDate(dtData.Rows(0).Item(1))
                Dim endDate As Date = CDate(dtData.Rows(0).Item(2))
                If rdEffectDate.SelectedDate < startDate Or rdEffectDate.SelectedDate >= endDate Or rdEffectDate.SelectedDate < joinDate Then
                    ShowMessage(Translate("Ngày bắt đầu phải lớn hơn hoặc bằng Ngày chính thức và nằm trong kỳ đánh giá"), NotifyType.Warning)
                    rdEffectDate.Clear()
                Else
                    Dim monthsNumber As New Decimal
                    Dim soA As New Decimal
                    Dim soB As New Decimal
                    If endDate.Day < 5 Then
                        soA = 0
                    Else
                        soA = 1
                    End If
                    If rdEffectDate.SelectedDate.Value.Day < 5 Then
                        soB = 1
                    Else
                        soB = 0
                    End If
                    monthsNumber = endDate.Month - rdEffectDate.SelectedDate.Value.Month - 1 + soA + soB
                    txtMonthNumber.Text = monthsNumber.ToString
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            Dim dtData As New DataTable
            If cboPeriod.SelectedValue <> "" Then
                dtData = rep.CHECK_JOIN_DATE_EMP(EmployeeID, cboPeriod.SelectedValue)
                Dim isEmpExist As Decimal = rep.CHECK_EXIST_EMP_IN_PERIOD(EmployeeID, cboPeriod.SelectedValue)
                Dim joinDate As Date = CDate(dtData.Rows(0).Item(0))
                Dim startDate As Date = CDate(dtData.Rows(0).Item(1))
                Dim endDate As Date = CDate(dtData.Rows(0).Item(2))
                Dim numberDay As Decimal = CDec(dtData.Rows(0).Item(3))
                Dim numberMonth As Decimal = CDec(dtData.Rows(0).Item(4))
                Dim dateNow As Date = Date.Now
                If joinDate >= endDate Then
                    ShowMessage(Translate("Nhân viên chưa đủ điều kiện thực hiện đánh giá"), NotifyType.Warning)
                    cboPeriod.ClearSelection()
                    Exit Sub
                End If
                If isEmpExist = 0 Then
                    ShowMessage(Translate("Bạn không đủ điều kiện đăng ký mục tiêu đánh giá"), NotifyType.Warning)
                    cboPeriod.ClearSelection()
                    Exit Sub
                End If
                If joinDate <= startDate Then
                    If dateNow > startDate.AddDays(numberDay) Then
                        ShowMessage(Translate("Ngày thực hiện đã quá thời gian cho phép đăng ký mục tiêu đánh giá"), NotifyType.Warning)
                        cboPeriod.ClearSelection()
                        Exit Sub
                    End If
                Else
                    If dateNow > joinDate.AddDays(numberDay) Then
                        ShowMessage(Translate("Ngày thực hiện đã quá thời gian cho phép đăng ký mục tiêu đánh giá"), NotifyType.Warning)
                        cboPeriod.ClearSelection()
                        Exit Sub
                    End If
                End If
                rdStartDate.SelectedDate = startDate
                rdEndDate.SelectedDate = endDate
                If joinDate <= startDate Then
                    rdEffectDate.SelectedDate = startDate
                Else
                    rdEffectDate.SelectedDate = joinDate
                End If
                Dim monthsNumber As New Decimal
                Dim soA As New Decimal
                Dim soB As New Decimal
                If endDate.Day < 5 Then
                    soA = 0
                Else
                    soA = 1
                End If
                If rdEffectDate.SelectedDate.Value.Day < 5 Then
                    soB = 1
                Else
                    soB = 0
                End If
                monthsNumber = endDate.Month - startDate.Month - 1 + soA + soB
                txtMonthNumber.Text = monthsNumber.ToString
                hidNumberMonth.Value = numberMonth
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtTargets_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTargets.TextChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            If txtTargets.Text <> "" Then
                Dim evaluateCode = ""
                Dim criteriaCode = ""
                If cboCriteriaType.SelectedValue = "" Then
                    ShowMessage(Translate("Phải chọn Loại tiêu chí"), NotifyType.Warning)
                    txtTargets.ClearValue()
                    Exit Sub
                Else
                    rep.GET_CRITERIA_CODE(cboCriteriaType.SelectedValue)
                End If
                If cboEvaluateType.SelectedValue = "" Then
                    ShowMessage(Translate("Phải chọn Loại đánh giá"), NotifyType.Warning)
                    txtTargets.ClearValue()
                    Exit Sub
                Else
                    evaluateCode = rep.GET_EVALUATE_CODE(cboEvaluateType.SelectedValue)
                End If
                If evaluateCode = "NUMBER" Then
                    If Not IsNumeric(txtTargets.Text) Then
                        ShowMessage(Translate("Chỉ được nhập số"), NotifyType.Warning)
                        txtTargets.ClearValue()
                        Exit Sub
                    End If
                    If txtThreshold.Text <> "" Then
                        If criteriaCode = "CNCT" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) > Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải lớn hơn Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        ElseIf criteriaCode = "CLCT" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) < Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bé hơn Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        ElseIf criteriaCode = "DKD" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) = Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bằng Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        End If
                    End If
                ElseIf evaluateCode = "DATE" Then
                    If Not IsDate(txtTargets.Text) Then
                        ShowMessage(Translate("Chỉ được nhập ngày (dd/MM/yyyy)"), NotifyType.Warning)
                        txtTargets.ClearValue()
                        Exit Sub
                    End If
                    If txtThreshold.Text <> "" Then
                        If criteriaCode = "CNCT" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) > Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải lớn hơn Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        ElseIf criteriaCode = "CLCT" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) < Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bé hơn Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        ElseIf criteriaCode = "DKD" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) = Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bằng Chỉ tiêu"), NotifyType.Warning)
                                txtTargets.ClearValue()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtThreshold_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtThreshold.TextChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            If txtThreshold.Text <> "" Then
                Dim evaluateCode = ""
                Dim criteriaCode = ""
                If cboCriteriaType.SelectedValue = "" Then
                    ShowMessage(Translate("Phải chọn Loại tiêu chí"), NotifyType.Warning)
                    txtThreshold.ClearValue()
                    Exit Sub
                Else
                    criteriaCode = rep.GET_CRITERIA_CODE(cboCriteriaType.SelectedValue)
                End If
                If cboEvaluateType.SelectedValue = "" Then
                    ShowMessage(Translate("Phải chọn Loại đánh giá"), NotifyType.Warning)
                    txtThreshold.ClearValue()
                    Exit Sub
                Else
                    evaluateCode = rep.GET_EVALUATE_CODE(cboEvaluateType.SelectedValue)
                End If
                If evaluateCode = "NUMBER" Then
                    If Not IsNumeric(txtThreshold.Text) Then
                        ShowMessage(Translate("Chỉ được nhập số"), NotifyType.Warning)
                        txtThreshold.ClearValue()
                        Exit Sub
                    End If
                    If txtTargets.Text <> "" Then
                        If criteriaCode = "CNCT" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) > Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải lớn hơn Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        ElseIf criteriaCode = "CLCT" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) < Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bé hơn Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        ElseIf criteriaCode = "DKD" Then
                            If Not Convert.ToDecimal(txtThreshold.Text) = Convert.ToDecimal(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bằng Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        End If
                    End If
                ElseIf evaluateCode = "DATE" Then
                    If Not IsDate(txtThreshold.Text) Then
                        ShowMessage(Translate("Chỉ được nhập ngày (dd/MM/yyyy)"), NotifyType.Warning)
                        txtThreshold.ClearValue()
                        Exit Sub
                    End If
                    If txtTargets.Text <> "" Then
                        If criteriaCode = "CNCT" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) > Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải lớn hơn Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        ElseIf criteriaCode = "CLCT" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) < Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bé hơn Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        ElseIf criteriaCode = "DKD" Then
                            If Not Convert.ToDateTime(txtThreshold.Text) = Convert.ToDateTime(txtTargets.Text) Then
                                ShowMessage(Translate("Giá trị Ngưỡng phải bằng Chỉ tiêu"), NotifyType.Warning)
                                txtThreshold.ClearValue()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCriteriaType_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteriaType.SelectedIndexChanged, cboEvaluateType.SelectedIndexChanged
        Try
            txtThreshold.ClearValue()
            txtTargets.ClearValue()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboCriteria.SelectedIndexChanged
        Try
            Dim rep As New PerformanceStoreProcedure
            Dim dtData As New DataTable
            If cboCriteria.SelectedValue <> "" Then
                dtData = rep.GET_CRITERIA_BY_ID(cboCriteria.SelectedValue)
                txtCriteria.Text = cboCriteria.Text
                If Not IsDBNull(dtData.Rows(0).Item(4)) Then
                    cboFrequency.SelectedValue = dtData.Rows(0).Item(4)
                End If
                If Not IsDBNull(dtData.Rows(0).Item(7)) Then
                    cboSource.SelectedValue = dtData.Rows(0).Item(7)
                End If
                If Not IsDBNull(dtData.Rows(0).Item(2)) Then
                    cboUnit.SelectedValue = dtData.Rows(0).Item(2)
                End If
                txtRemark.Text = dtData.Rows(0).Item(6)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim assesment As New KPI_ASSESSMENT_DTO
            Dim rep As New PerformanceRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If rgData.Items.Count > 0 Then
                        Dim sumRATIO As Decimal = 0
                        For Each item As GridDataItem In rgData.Items
                            Dim rnRATIO = DirectCast(item.FindControl("rnRATIO"), RadNumericTextBox)
                            sumRATIO += If(Not IsNothing(rnRATIO), If(rnRATIO.Value.HasValue, rnRATIO.Value, 0), item.GetDataKeyValue("RATIO"))
                        Next

                        If sumRATIO <> 100 Then
                            ShowMessage("Tổng trọng số phải bằng 100%", Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    assesment.EMPLOYEE = EmployeeID
                    If cboYear.SelectedValue <> "" Then
                        assesment.YEAR = cboYear.SelectedValue
                    End If
                    If cboPeriod.SelectedValue <> "" Then
                        assesment.PE_PERIOD_ID = cboPeriod.SelectedValue
                    End If
                    If rdStartDate.SelectedDate IsNot Nothing Then
                        assesment.START_DATE = rdStartDate.SelectedDate
                    End If
                    If rdEndDate.SelectedDate IsNot Nothing Then
                        assesment.END_DATE = rdEndDate.SelectedDate
                    End If
                    If rdEffectDate.SelectedDate IsNot Nothing Then
                        assesment.EFFECT_DATE = rdEffectDate.SelectedDate
                    End If
                    If txtMonthNumber.Text <> "" Then
                        assesment.NUMBER_MONTH = Convert.ToDecimal(txtMonthNumber.Text)
                    End If

                    If lstAssesmentDetail.Count > 0 Then
                        Dim check = (From p In lstAssesmentDetail Where p.BENCHMARK Is Nothing).Any
                        If check Then
                            ShowMessage("Hãy tính điểm chuẩn trước khi lưu", Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        assesment.assesmentDetail = lstAssesmentDetail
                    Else
                        ShowMessage("Vui lòng thêm KPI trước khi lưu", Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    assesment.GOAL = txtGoal.Text
                    assesment.REMARK = txtNote.Text
                    assesment.STATUS_ID = 3
                    assesment.PORTAL_ID = 1
                    assesment.IS_CONFIRM = Me.is_Confirm
                    If IsNumeric(hidGoalID.Value) Then
                        assesment.ID = Decimal.Parse(hidGoalID.Value)
                        If rep.ValidateDateAssessment(assesment) Then
                            ShowMessage(Translate("Ngày bắt đầu của mục tiêu đánh giá mới phải lớn hơn ngày bắt đầu của mực tiêu đánh giá trước đó"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.UpdateKpiAssessment(assesment) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlPortalMngInfoEvalTarget")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else
                        assesment.ID = 0
                        If rep.ValidateDateAssessment(assesment) Then
                            ShowMessage(Translate("Ngày bắt đầu của mục tiêu đánh giá mới phải lớn hơn ngày bắt đầu của mực tiêu đánh giá trước đó"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.InsertKpiAssessment(assesment) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlPortalMngInfoEvalTarget")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case (CommonMessage.TOOLBARITEM_CANCEL)
                    Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlPortalMngInfoEvalTarget")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            lbSumRatioVal.Text = ""
            If IsPostBack AndAlso IsNumeric(hidGoalID.Value) And Me.is_Change <> 1 Then
                Dim rep As New PerformanceRepository
                Dim obj As New KPI_ASSESSMENT_DTO
                obj = rep.GetKpiAssessmentByID(hidGoalID.Value)
                lstAssesmentDetail = obj.assesmentDetail
            End If
            If lstAssesmentDetail IsNot Nothing AndAlso lstAssesmentDetail.Count > 0 Then
                rgData.DataSource = lstAssesmentDetail
                lbSumRatioVal.Text = lstAssesmentDetail.Sum(Function(f) f.RATIO)
            Else
                lstAssesmentDetail = New List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
                rgData.DataSource = New List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
            End If
            Me.is_Change = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Try
            Dim rep As New PerformanceStoreProcedure
            Select Case e.CommandName
                Case "Add"
                    If Not IsDate(rdEffectDate.SelectedDate) Then
                        ShowMessage(Translate("Bạn phải nhập Ngày bắt đầu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtCriteria.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Chỉ số đo lường"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboUnit.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Đơn vị tính"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboUnit.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Đơn vị tính"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboFrequency.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Tần suất đo"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtRemark.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập PP & Công thức"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboSource.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Nguồn đo"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboCriteriaType.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Loại tiêu chí"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboEvaluateType.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Loại đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtTargets.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Chỉ tiêu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If txtThreshold.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Ngưỡng"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rnWeighted.Text = "" Then
                    '    ShowMessage(Translate("Bạn phải nhập Trọng số"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kỳ đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rep.CHECK_EXIST_ASSESMENT(EmployeeID, cboPeriod.SelectedValue, txtCriteria.Text, rdEffectDate.SelectedDate) > 0 Then
                    '    ShowMessage(Translate("Chỉ số đo lường đã tồn tại trong kỳ đánh giá"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    Dim lst As New List(Of String)
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                        lst.Add(item.KPI_ASSESSMENT_TEXT)
                    Next
                    If lst.Contains(txtCriteria.Text) Then
                        ShowMessage(Translate("Chỉ số đo lường đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim assesmentDetail As PE_KPI_ASSESMENT_DETAIL_DTO
                    assesmentDetail = New PE_KPI_ASSESMENT_DETAIL_DTO
                    assesmentDetail.ID = 0

                    Dim check = (From p In dtSource Where p("ID") = cboSource.SelectedValue AndAlso p("CODE") = "KHAC").Any

                    If cboCriteria.SelectedValue <> "" Then
                        assesmentDetail.KPI_ASSESSMENT = cboCriteria.SelectedValue
                    Else
                        assesmentDetail.KPI_TYPE = "KPI mới"
                    End If
                    assesmentDetail.KPI_ASSESSMENT_TEXT = txtCriteria.Text
                    assesmentDetail.UNIT_ID = cboUnit.SelectedValue
                    assesmentDetail.UNIT_NAME = cboUnit.Text
                    assesmentDetail.FREQUENCY_ID = cboFrequency.SelectedValue
                    assesmentDetail.FREQUENCY_NAME = cboFrequency.Text
                    assesmentDetail.DESCRIPTION = txtRemark.Text
                    assesmentDetail.SOURCE_ID = cboSource.SelectedValue
                    If check Then
                        assesmentDetail.SOURCE_NAME = txtOtherSource.Text
                    Else
                        assesmentDetail.SOURCE_NAME = cboSource.Text
                    End If
                    assesmentDetail.TARGET_TYPE_ID = cboEvaluateType.SelectedValue
                    assesmentDetail.TARGET_TYPE_NAME = cboEvaluateType.Text
                    assesmentDetail.TARGET = txtTargets.Text
                    assesmentDetail.TARGET_MIN = txtThreshold.Text
                    assesmentDetail.RATIO = rnWeighted.Value
                    assesmentDetail.GOAL_TYPE = cboCriteriaType.SelectedValue
                    assesmentDetail.GOAL_TYPE_NAME = cboCriteriaType.Text
                    assesmentDetail.IS_IN_DB = ""

                    lstAssesmentDetail.Add(assesmentDetail)
                    Me.is_Change = 1
                    ClearControlValue(cboCriteria, rnWeighted, txtCriteria, cboUnit, cboFrequency, txtRemark, cboSource, txtOtherSource, cboCriteriaType, cboEvaluateType, txtTargets, txtThreshold)
                    txtOtherSource.Enabled = False
                    rgData.Rebind()
                    For Each items As GridDataItem In rgData.MasterTableView.Items
                        items.Edit = True
                    Next
                    rgData.MasterTableView.Rebind()
                Case "Delete"
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    Else
                        If lstDeleteID IsNot Nothing Then
                            lstDeleteID.Clear()
                        Else
                            lstDeleteID = New List(Of String)
                        End If

                        If lstExistID IsNot Nothing Then
                            lstExistID.Clear()
                        Else
                            lstExistID = New List(Of Decimal)
                        End If

                        For Each selected As GridDataItem In rgData.SelectedItems
                            If Not String.IsNullOrEmpty(selected.GetDataKeyValue("EMPLOYEE_POINT")) Then
                                ShowMessage("Không được phép xóa do đã phát sinh điểm", NotifyType.Warning)
                                Exit Sub
                            Else
                                lstDeleteID.Add(selected.GetDataKeyValue("KPI_ASSESSMENT_TEXT").ToString)
                                If Not String.IsNullOrEmpty(selected.GetDataKeyValue("IS_IN_DB")) Then
                                    lstExistID.Add(selected.GetDataKeyValue("ID").ToString)
                                End If
                            End If
                        Next
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = "REMOVE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "Calculate"
                    For Each item As GridDataItem In rgData.Items
                        Dim rnRATIO = DirectCast(item.FindControl("rnRATIO"), RadNumericTextBox)

                        If rnRATIO.Value Is Nothing Then
                            ShowMessage("Vui lòng nhập đầy đủ trọng số trước khi tính điểm chuẩn", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    Dim sum As Decimal = 0
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                        sum = sum + item.RATIO
                    Next
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                        item.BENCHMARK = item.RATIO / sum * 100
                    Next
                    Me.is_Change = 1
                    rgData.Rebind()
                Case "Result"
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New PerformanceRepository
        Try
            If e.ActionName = "REMOVE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                For Each selected As String In lstDeleteID
                    lstAssesmentDetail.RemoveAll(Function(x) x.KPI_ASSESSMENT_TEXT = selected)
                Next

                rep.Delete_PE_KPI_ASSESSMENT_DETAIL(lstExistID)

                Me.is_Change = 1
                rgData.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSource.SelectedIndexChanged
        Try
            txtOtherSource.ClearValue
            If cboSource.SelectedValue <> "" Then
                Dim check = (From p In dtSource Where p("ID") = cboSource.SelectedValue AndAlso p("CODE") = "KHAC").Any
                txtOtherSource.Enabled = check
            Else
                txtOtherSource.Enabled = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub RadGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.PreRender
        Try
            If Not IsPostBack Then
                If Request.Params("ID") Is Nothing Then
                    For Each items As GridDataItem In rgData.MasterTableView.Items
                        items.Edit = True
                    Next
                    rgData.MasterTableView.Rebind()
                Else
                    If Me.FormType <> 0 And (Me.is_Confirm = 2 Or Me.is_Confirm = 3) Then
                        For Each items As GridDataItem In rgData.MasterTableView.Items
                            items.Edit = True
                        Next
                        rgData.MasterTableView.Rebind()
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rnRatio_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadNumericTextBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim kpiText = row.GetDataKeyValue("KPI_ASSESSMENT_TEXT")
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                If item.KPI_ASSESSMENT_TEXT.ToUpper.Equals(kpiText.ToString.ToUpper) Then
                    item.RATIO = If(IsNumeric(edit.Value), edit.Value, 0)
                    Exit For
                End If
            Next
            lbSumRatioVal.Text = lstAssesmentDetail.Sum(Function(f) f.RATIO)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly cac trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
        Catch ex As Exception
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New PerformanceStoreProcedure
        Dim rep2 As New Profile.ProfileRepository
        Dim dtData As New DataTable
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtData = rep.GetYear(True)
            FillRadCombobox(cboYear, dtData, "YEAR", "ID", True)
            dtData = rep.GET_CRITERIA(True)
            FillRadCombobox(cboCriteria, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("FREQUENCY_DG", True)
            FillRadCombobox(cboFrequency, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("DVT_DG", True)
            FillRadCombobox(cboUnit, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("SOURCE_DG", True)
            dtSource = rep2.GetOtherList("SOURCE_DG", False)
            FillRadCombobox(cboSource, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("TYPE_DG", True)
            FillRadCombobox(cboCriteriaType, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("TYPE_CRITERIA", True)
            FillRadCombobox(cboEvaluateType, dtData, "NAME", "ID", True)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 16/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien lay cac params chuyen sang tu trang view
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetParams()
        Dim ID As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request.Params("FormType") IsNot Nothing Then
                Me.FormType = Request.Params("FormType")
                hidTypeID.Value = Request.Params("FormType")
            End If
            If Request.Params("ID") IsNot Nothing Then
                hidGoalID.Value = Request.Params("ID")
                If Me.FormType = 0 Then
                    Refresh("NormalView")
                Else
                    Refresh("UpdateView")
                End If
            Else
                Refresh("InsertView")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound

        If (TypeOf e.Item Is GridCommandItem) Then
            Dim addButtonDelete As RadButton = CType(e.Item.FindControl("btnDelete"), RadButton)
            Dim addButtonResult As RadButton = CType(e.Item.FindControl("btnResult"), RadButton)
            Dim addButtonAdd As RadButton = CType(e.Item.FindControl("btnAdd"), RadButton)
            Dim addButtonCalculate As RadButton = CType(e.Item.FindControl("btnCalculate"), RadButton)
            Dim addButtonHistory As RadButton = CType(e.Item.FindControl("btnResultHistory"), RadButton)
            If Request.Params("ID") IsNot Nothing Then
                If Me.FormType = 0 Then
                    If Me.is_Confirm <> 3 Then
                        addButtonDelete.Visible = False
                        addButtonAdd.Visible = False
                        addButtonCalculate.Visible = False
                    Else
                        addButtonDelete.Visible = False
                        addButtonResult.Visible = False
                        addButtonAdd.Visible = False
                        addButtonCalculate.Visible = False
                        addButtonHistory.Visible = False
                    End If

                Else
                    If Me.is_Confirm = 2 Or Me.is_Confirm = 3 Then
                        addButtonResult.Visible = False
                    Else
                        addButtonDelete.Visible = False
                        addButtonAdd.Visible = False
                        addButtonCalculate.Visible = False
                    End If
                End If
            Else
                If Me.FormType = 0 Then
                    addButtonResult.Visible = False
                    addButtonHistory.Visible = False
                End If
            End If
            If Me.status_id = 1 Then
                addButtonResult.Visible = False
            End If
        End If
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rnRATIO As New RadNumericTextBox
            rnRATIO = CType(edit.FindControl("rnRATIO"), RadNumericTextBox)
            Dim kpiText = edit.GetDataKeyValue("KPI_ASSESSMENT_TEXT")
            For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                If item.KPI_ASSESSMENT_TEXT.ToUpper.Equals(kpiText.ToString.ToUpper) Then
                    rnRATIO.Value = item.RATIO
                End If
            Next
        End If

    End Sub

#End Region

End Class

