Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPE_PortalHTCHAssessmentResult
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Performance/Module/Portal/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property EmployeeID As Decimal

    Public Property ListDtl As List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
        Get
            Return ViewState(Me.ID & "_lstDtl")
        End Get
        Set(value As List(Of PE_HTCH_ASSESSMENT_DTL_DTO))
            ViewState(Me.ID & "_lstDtl") = value
        End Set
    End Property
    Public Property FormType As Decimal
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(value As Decimal)
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
            GetParams()
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
                'Dim objEmp = repCM.GetEmployeeID(EmployeeID)
                'txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                'txtEmployeeName.Text = objEmp.FULLNAME_VN
                'txtTitleName.Text = objEmp.TITLE_NAME
                'txtOrgName.Text = objEmp.ORG_NAME
                Select Case Message
                    Case "UpdateView"
                        Dim rep As New PerformanceRepository
                        Dim obj = rep.GetHTCHAssessmentByID(hidID.Value)
                        If obj IsNot Nothing Then
                            txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                            txtEmployeeName.Text = obj.EMPLOYEE_NAME
                            txtTitleName.Text = obj.TITLE_NAME
                            txtOrgName.Text = obj.ORG_NAME
                            txtYear.Text = obj.YEAR
                            txtPeriod.Text = obj.PERIOD_NAME
                            If obj.START_DATE IsNot Nothing Then
                                rdStartDate.SelectedDate = obj.START_DATE
                            End If
                            If obj.END_DATE IsNot Nothing Then
                                rdEndDate.SelectedDate = obj.END_DATE
                            End If
                            txtStrengthNote.Text = obj.STRENGTH_NOTE
                            txtImproveNote.Text = obj.IMPROVE_NOTE
                            txtProspect.Text = obj.PROSPECT_NOTE
                            txtRemark.Text = obj.REMARK
                            txtBranchEvaluate.Text = obj.BRANCH_EVALUATE
                            If obj.lstDetail IsNot Nothing Then
                                ListDtl = obj.lstDetail
                            Else
                                ListDtl = New List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
                            End If
                        End If
                    Case "InsertView"
                        CurrentState = CommonMessage.STATE_NEW
                    Case "NormalView"

                End Select
                If Not IsPostBack AndAlso FormType = 2 Then
                    EnableControlAll(False, txtStrengthNote, txtImproveNote, txtProspect, txtBranchEvaluate, txtRemark)
                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                End If
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
                    Dim obj As New PE_HTCH_ASSESSMENT_DTO
                    obj.ID = hidID.Value
                    obj.STRENGTH_NOTE = txtStrengthNote.Text
                    obj.IMPROVE_NOTE = txtImproveNote.Text
                    obj.PROSPECT_NOTE = txtProspect.Text
                    obj.BRANCH_EVALUATE = txtBranchEvaluate.Text
                    obj.REMARK = txtRemark.Text
                    Dim lstHTCHDetail As New List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
                    For Each item As GridEditableItem In rgData.MasterTableView.Items
                        If item.GetDataKeyValue("IS_CHECK") = -1 Then
                            Continue For
                        End If
                        Dim objDtl As New PE_HTCH_ASSESSMENT_DTL_DTO
                        Dim txtResultActual As RadTextBox = CType(item("RESULT_ACTUAL").Controls(1), RadTextBox)
                        If Not IsNumeric(txtResultActual.Text) Then
                            ShowMessage(Translate("Kết quả import chỉ được nhập số"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        objDtl.ID = item.GetDataKeyValue("ID")
                        objDtl.POINTS_ACTUAL = item.GetDataKeyValue("POINTS_ACTUAL")
                        objDtl.RESULT_ACTUAL = CDec(txtResultActual.Text.Trim)
                        objDtl.POINTS_FINAL = item.GetDataKeyValue("POINTS_FINAL")
                        objDtl.NOTE = item.GetDataKeyValue("NOTE")
                        lstHTCHDetail.Add(objDtl)
                    Next
                    obj.lstDetail = lstHTCHDetail

                    If rep.SaveHTCHAssessment(obj) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('0');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            If IsPostBack AndAlso IsNumeric(hidID.Value) Then
                Dim rep As New PerformanceRepository
                Dim obj = rep.GetHTCHAssessmentByID(hidID.Value)
                ListDtl = obj.lstDetail
            End If
            rgData.DataSource = ListDtl

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Try
            Dim rep As New PerformanceRepository
            Select Case e.CommandName
                Case "Save"
                    Dim lstHTCHDetail As New List(Of PE_HTCH_ASSESSMENT_DTL_DTO)
                    For Each item As GridEditableItem In rgData.MasterTableView.Items
                        If item.GetDataKeyValue("IS_CHECK") = -1 Then
                            Continue For
                        End If
                        Dim objDtl As New PE_HTCH_ASSESSMENT_DTL_DTO
                        Dim txtResultActual As RadTextBox = CType(item("RESULT_ACTUAL").Controls(1), RadTextBox)
                        If Not IsNumeric(txtResultActual.Text) Then
                            ShowMessage(Translate("Kết quả import chỉ được nhập số"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        objDtl.ID = item.GetDataKeyValue("ID")
                        objDtl.POINTS_ACTUAL = item.GetDataKeyValue("POINTS_ACTUAL")
                        objDtl.RESULT_ACTUAL = CDec(txtResultActual.Text.Trim)
                        objDtl.POINTS_FINAL = item.GetDataKeyValue("POINTS_FINAL")
                        objDtl.NOTE = item.GetDataKeyValue("NOTE")
                        lstHTCHDetail.Add(objDtl)
                    Next

                    If rep.SaveHTCHAssessmentListDetail(lstHTCHDetail) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        ListDtl = rep.GetHTCHAssessmentListDetail(hidID.Value)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case "Change"
                    If rep.CHANGE_HTCH_ASSESSMENT_DTL(hidID.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        ListDtl = rep.GetHTCHAssessmentListDetail(hidID.Value)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case "Calculate"
                    If rep.CAL_HTCT_ASSESS_DTL(hidID.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        ListDtl = rep.GetHTCHAssessmentListDetail(hidID.Value)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgData.PreRender
        Try
            If Not IsPostBack Then
                If FormType <> 2 Then
                    For Each items As GridDataItem In rgData.MasterTableView.Items
                        items.Edit = True
                    Next
                End If
                rgData.MasterTableView.Rebind()
            End If
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
        Try

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
            Me.FormType = If(IsNothing(Request.Params("FormType")), 0, Request.Params("FormType"))
            If Request.Params("ID") IsNot Nothing Then
                hidID.Value = Request.Params("ID")
                Refresh("UpdateView")
            Else
                Refresh("InsertView")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim txtRESULT_ACTUAL As New RadTextBox
        Dim addButtonSave As RadButton = CType(e.Item.FindControl("btnSave"), RadButton)
        Dim addButtonCalculate As RadButton = CType(e.Item.FindControl("btnCalculate"), RadButton)
        Dim addButtonChange As RadButton = CType(e.Item.FindControl("btnChange"), RadButton)
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                txtRESULT_ACTUAL = CType(edit.FindControl("txtRESULT_ACTUAL"), RadTextBox)
                Dim id = edit.GetDataKeyValue("ID")
                For Each item As PE_HTCH_ASSESSMENT_DTL_DTO In ListDtl
                    If item.ID = id Then
                        If Not IsNothing(item.RESULT_ACTUAL) Then
                            txtRESULT_ACTUAL.Text = item.RESULT_ACTUAL
                        End If
                        If item.IS_CHECK = -1 Then
                            txtRESULT_ACTUAL.Enabled = False
                        End If
                        Exit For
                    End If
                Next
            End If
            If FormType = 2 AndAlso addButtonSave IsNot Nothing AndAlso addButtonCalculate IsNot Nothing AndAlso addButtonChange IsNot Nothing Then
                addButtonSave.Enabled = False
                addButtonChange.Enabled = False
                addButtonCalculate.Enabled = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class

