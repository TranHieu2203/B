Imports Common
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPortalApproveKPIAssessmentView
    Inherits CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstAssesmentDetail As List(Of PE_KPI_ASSESMENT_DETAIL_DTO)
        Get
            Return ViewState(Me.ID & "_lstAssesmentDetail")
        End Get
        Set(ByVal value As List(Of PE_KPI_ASSESMENT_DETAIL_DTO))
            ViewState(Me.ID & "_lstAssesmentDetail") = value
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
    Dim FormType As Integer

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
            InitControl()
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
            hidGoalID.Value = Request.Params("ID")

            If hidGoalID.Value <> 0 Then
                Dim rep As New PerformanceRepository
                Dim obj As New KPI_ASSESSMENT_DTO
                Dim reps As New PerformanceStoreProcedure
                Dim dtData As New DataTable
                obj = rep.GetKpiAssessmentByID(hidGoalID.Value)
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

            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Cancel)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

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
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlPortalApproveKPIAssessment")
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

#End Region

End Class

