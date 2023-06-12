Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPortalMngInfoEvalTargetNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

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
#End Region

#Region "Page"
    Dim FormType As Integer
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
            FormType = Request.Params("FormType")
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
                txtMonthNumber.Text = obj.NUMBER_MONTH
                txtGoal.Text = obj.GOAL
                txtNote.Text = obj.REMARK
                If obj.assesmentDetail.Count > 0 Then
                    lstAssesmentDetail = obj.assesmentDetail
                End If
                If obj.IS_CONFIRM IsNot Nothing Then
                    Me.is_Confirm = obj.IS_CONFIRM
                End If
                If obj.STATUS_ID IsNot Nothing Then
                    Me.status_id = obj.STATUS_ID
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
                    If lstAssesmentDetail.Count > 0 Then
                        assesment.assesmentDetail = lstAssesmentDetail
                    End If
                    assesment.GOAL = txtGoal.Text
                    assesment.REMARK = txtNote.Text
                    assesment.STATUS_ID = Me.status_id
                    assesment.PORTAL_ID = 1
                    assesment.IS_CONFIRM = Me.is_Confirm
                    If hidGoalID.Value <> 0 Then
                        assesment.ID = Decimal.Parse(hidGoalID.Value)
                        If rep.UpdateKpiAssessment(assesment) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                            'Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTarget&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else
                        If rep.InsertKpiAssessment(assesment) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                            'Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlMngInfoEvalTarget&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlPortalApproveEvalTarget")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            Refresh()
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

    Protected Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim rep As New PerformanceRepository
        Dim obj As New KPI_ASSESSMENT_DTO
        Dim ID_PH As Decimal = 0
        If IsNumeric(Request.Params("ID")) Then
            ID_PH = Decimal.Parse(Request.Params("ID"))
        End If
        obj = rep.GetKpiAssessmentByID(ID_PH)
        If TypeOf (e.Item) Is GridCommandItem Then
            Dim cmdItem As GridCommandItem = e.Item
            If ID_PH <> 0 AndAlso obj.STATUS_ID IsNot Nothing AndAlso (obj.STATUS_ID = 1 Or obj.STATUS_ID = 2) Then
                cmdItem.Enabled = False
            Else
                cmdItem.Enabled = True
            End If
            'Dim addButtonDelete As RadButton = CType(e.Item.FindControl("btnDelete"), RadButton)
            'Dim addButtonAdd As RadButton = CType(e.Item.FindControl("btnAdd"), RadButton)
            'Dim addButtonCalculate As RadButton = CType(e.Item.FindControl("btnCalculate"), RadButton)
            'If Request.Params("ID") IsNot Nothing Then
            '    If FormType = 2 Then
            '        addButtonDelete.Enabled = False
            '        addButtonAdd.Enabled = False
            '        addButtonCalculate.Enabled = False
            '    End If
            'End If
        End If
    End Sub
End Class

