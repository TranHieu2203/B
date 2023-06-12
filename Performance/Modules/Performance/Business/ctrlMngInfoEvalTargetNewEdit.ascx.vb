Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports Performance.PerformanceBusiness
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports WebAppLog

Public Class ctrlMngInfoEvalTargetNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Performance/Module/Performance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property lstDeleteID As List(Of String)
        Get
            Return ViewState(Me.ID & "_lstDeleteID")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_lstDeleteID") = value
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

    Public Property lstResultID As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstResultID")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstResultID") = value
        End Set
    End Property

    Property lstAssesmentDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Get
            Return ViewState(Me.ID & "_lstAssesmentDetail")
        End Get
        Set(ByVal value As List(Of PE_KPI_ASSESMENT_DETAIL_DTO))
            ViewState(Me.ID & "_lstAssesmentDetail") = value
        End Set
    End Property
    Property dtAllowUpdate As DataTable
        Get
            Return ViewState(Me.ID & "_dtAllowUpdate")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtAllowUpdate") = value
        End Set
    End Property
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isFlag As Boolean
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property
    Property isCheck As Boolean
        Get
            Return ViewState(Me.ID & "_isCheck")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isCheck") = value
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
#End Region

#Region "Page"
    Public Property popupId As String

    ''' <lastupdate>
    ''' 15/08/2017 10:00
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
            CurrentState = CommonMessage.STATE_EDIT
            If Not IsPostBack Then
            End If
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWorkschedule
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popupId = popup.ClientID

            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Create)
            CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgWorkschedule
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("FormType")
            Dim Struct As Decimal = 1
            Dim ID_PH As Decimal = 0
            If IsNumeric(Request.Params("ID")) Then
                Struct = 0
                ID_PH = Decimal.Parse(Request.Params("ID"))
            End If
            hidGoalID.Value = ID_PH

            If ID_PH <> 0 Then
                Dim rep As New PerformanceRepository
                Dim obj As New KPI_ASSESSMENT_DTO
                Dim reps As New PerformanceStoreProcedure
                Dim dtData As New DataTable
                obj = rep.GetKpiAssessmentByID(ID_PH)
                rtEmployee_Code.Text = obj.EMPLOYEE_CODE
                rtEmployee_id.Text = obj.EMPLOYEE
                rtEmployee_Name.Text = obj.EMPLOYEE_NAME
                rtTitle_Id.Text = obj.TITLE_ID
                rtTitle_Name.Text = obj.TITLE_NAME
                rtOrg_id.Text = obj.ORG_ID
                rtOrg_Name.Text = obj.ORG_NAME
                If obj.YEAR IsNot Nothing Then
                    cboYear.SelectedValue = obj.YEAR
                    dtData = reps.GET_PE_PERIOD_BY_YEAR(True, cboYear.SelectedValue)
                    FillRadCombobox(cboPeriod, dtData, "NAME", "ID", True)
                    If obj.PE_PERIOD_ID IsNot Nothing Then
                        cboPeriod.Text = obj.PE_PERIOD_NAME
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
                If obj.STATUS_ID IsNot Nothing Then
                    cboStatus.SelectedValue = obj.STATUS_ID
                End If
                If obj.PORTAL_ID IsNot Nothing Then
                    chkPortalShow.Checked = obj.PORTAL_ID
                End If
                If obj.IS_CONFIRM IsNot Nothing Then
                    chkIsConfirm.Checked = obj.IS_CONFIRM
                End If
                txtMonthNumber.Text = obj.NUMBER_MONTH
                txtGoal.Text = obj.GOAL
                txtNote.Text = obj.REMARK
                If obj.assesmentDetail.Count > 0 Then
                    lstAssesmentDetail = obj.assesmentDetail
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

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
            If cboPeriod.SelectedValue <> "" AndAlso rtEmployee_id.Text <> "" Then
                dtData = rep.CHECK_JOIN_DATE_EMP(Convert.ToDecimal(rtEmployee_id.Text), If(cboPeriod.SelectedValue <> "", cboPeriod.SelectedValue, 0))
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
            If cboPeriod.SelectedValue <> "" AndAlso rtEmployee_id.Text <> "" Then
                dtData = rep.CHECK_JOIN_DATE_EMP(Convert.ToDecimal(rtEmployee_id.Text), If(cboPeriod.SelectedValue <> "", cboPeriod.SelectedValue, 0))
                Dim joinDate As Date = CDate(dtData.Rows(0).Item(0))
                Dim startDate As Date = CDate(dtData.Rows(0).Item(1))
                Dim endDate As Date = CDate(dtData.Rows(0).Item(2))
                If joinDate >= endDate Then
                    ShowMessage(Translate("Nhân viên chưa đủ điều kiện thực hiện đánh giá"), NotifyType.Warning)
                    cboPeriod.ClearSelection()
                Else
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
                End If
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
    ''' Xu ly su kien click nut cancel cua popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
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
                            sumRATIO += If(item.GetDataKeyValue("RATIO") Is Nothing, 0, Decimal.Parse(item.GetDataKeyValue("RATIO").ToString))
                        Next
                        If sumRATIO <> 100 Then
                            ShowMessage("Tổng trọng số phải bằng 100%", Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    assesment.EMPLOYEE = Convert.ToDecimal(rtEmployee_id.Text)
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
                    If cboStatus.SelectedValue <> "" Then
                        assesment.STATUS_ID = cboStatus.SelectedValue
                    End If
                    assesment.IS_CONFIRM = If(chkIsConfirm.Checked, 1, 0)
                    assesment.PORTAL_ID = If(chkPortalShow.Checked, 1, 0)
                    If lstAssesmentDetail.Count > 0 Then
                        Dim check = (From p In lstAssesmentDetail Where p.BENCHMARK Is Nothing).Any
                        If check Then
                            ShowMessage("Hãy tính điểm chuẩn trước khi lưu", Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        assesment.assesmentDetail = lstAssesmentDetail
                    End If

                    assesment.GOAL = txtGoal.Text
                    assesment.REMARK = txtNote.Text
                    If hidGoalID.Value <> 0 Then
                        assesment.ID = Decimal.Parse(hidGoalID.Value)
                        If rep.ValidateDateAssessment(assesment) Then
                            ShowMessage(Translate("Ngày bắt đầu của mục tiêu đánh giá mới phải lớn hơn ngày bắt đầu của mực tiêu đánh giá trước đó"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.UpdateKpiAssessment(assesment) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                            'Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTarget&group=Business")
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
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                            'Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTarget&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case (CommonMessage.TOOLBARITEM_CANCEL)
                    Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTarget&group=Business")
                Case (CommonMessage.TOOLBARITEM_CREATE)
                    Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTargetNewEdit&group=Business&FormType=0")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New PerformanceStoreProcedure
            Dim dtData As New DataTable
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            FillData(empID)
            If cboPeriod.SelectedValue <> "" AndAlso rtEmployee_id.Text <> "" Then
                dtData = rep.CHECK_JOIN_DATE_EMP(Convert.ToDecimal(rtEmployee_id.Text), cboPeriod.SelectedValue)
                Dim joinDate As Date = CDate(dtData.Rows(0).Item(0))
                Dim startDate As Date = CDate(dtData.Rows(0).Item(1))
                Dim endDate As Date = CDate(dtData.Rows(0).Item(2))
                If joinDate >= endDate Then
                    ShowMessage(Translate("Nhân viên chưa đủ điều kiện thực hiện đánh giá"), NotifyType.Warning)
                    cboPeriod.ClearSelection()
                Else
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
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            'Refresh()
            If lstAssesmentDetail IsNot Nothing AndAlso lstAssesmentDetail.Count > 0 Then
                rgData.DataSource = lstAssesmentDetail
            Else
                lstAssesmentDetail = New List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
                rgData.DataSource = New List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
            End If
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
                    If rnWeighted.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập Trọng số"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rtEmployee_id.Text = "" Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If cboPeriod.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn kỳ đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    'If rep.CHECK_EXIST_ASSESMENT(Convert.ToDecimal(rtEmployee_id.Text), cboPeriod.SelectedValue, txtCriteria.Text, rdEffectDate.SelectedDate) > 0 Then
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
                    assesmentDetail.SOURCE_NAME = cboSource.Text
                    assesmentDetail.TARGET_TYPE_ID = cboEvaluateType.SelectedValue
                    assesmentDetail.TARGET_TYPE_NAME = cboEvaluateType.Text
                    assesmentDetail.TARGET = txtTargets.Text
                    assesmentDetail.TARGET_MIN = txtThreshold.Text
                    assesmentDetail.RATIO = rnWeighted.Value
                    assesmentDetail.GOAL_TYPE = cboCriteriaType.SelectedValue
                    assesmentDetail.GOAL_TYPE_NAME = cboCriteriaType.Text
                    assesmentDetail.IS_IN_DB = ""

                    lstAssesmentDetail.Add(assesmentDetail)

                    rgData.Rebind()
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
                    Dim sum As Decimal = 0
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                        sum = sum + item.RATIO
                    Next
                    For Each item As PE_KPI_ASSESMENT_DETAIL_DTO In lstAssesmentDetail
                        item.BENCHMARK = item.RATIO / sum * 100
                    Next
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

                rgData.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien load data cho cac combobox
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
            FillRadCombobox(cboSource, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("TYPE_DG", True)
            FillRadCombobox(cboCriteriaType, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("TYPE_CRITERIA", True)
            FillRadCombobox(cboEvaluateType, dtData, "NAME", "ID", True)
            dtData = rep2.GetOtherList("PROCESS_STATUS", False)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID", True)
                cboStatus.SelectedValue = PortalStatus.Saved
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As New DataTable()
        Dim dateValue = Date.Now
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New Profile.ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If IsNumeric(obj.ID) Then
                    rtEmployee_id.Text = obj.EMPLOYEE_ID.ToString()
                End If
                rtEmployee_Name.Text = obj.EMPLOYEE_NAME
                rtEmployee_Code.Text = obj.EMPLOYEE_CODE
                rtOrg_Name.Text = obj.ORG_NAME
                If IsNumeric(obj.ORG_ID) Then
                    rtOrg_id.Text = obj.ORG_ID.ToString()
                End If
                rtTitle_Name.Text = obj.TITLE_NAME
                If IsNumeric(obj.TITLE_ID) Then
                    rtTitle_Id.Text = obj.TITLE_ID.ToString()
                End If

            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rtEmployee_Name.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If rtEmployee_Name.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = rtEmployee_Name.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        rtEmployee_Name.Text = ""
                    ElseIf Count = 1 Then
                        Dim item = EmployeeList(0)
                        FillData(item.EMPLOYEE_ID)
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = rtEmployee_Name.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.Show()
                            isLoadPopup = 1
                        End If
                    End If
                Else
                    Reset_Find_Emp()
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Reset_Find_Emp()
        rtEmployee_id.Text = ""
        'rtEmployee_Name.Text = ""
        rtEmployee_Code.Text = ""
        rtOrg_Name.Text = ""
        rtOrg_id.Text = ""
        rtTitle_Name.Text = ""
        rtTitle_Id.Text = ""
    End Sub
#End Region
End Class

