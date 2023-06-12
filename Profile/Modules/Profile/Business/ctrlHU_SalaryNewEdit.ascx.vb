Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffWebAppResources.My.Resources
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_SalaryNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindFrameProductivityPopup As ctrlFindFrameProductivityPopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private commonStore As New CommonProcedureNew
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Dim _lttv As Decimal
    Dim _tyLeThuViec As Decimal
    Dim _tyLeChinhThuc As Decimal
#Region "Property"
    Dim lstAllow As New List(Of WorkingAllowanceDTO)

    Property dtSalaryGroup As DataTable
        Get
            Return ViewState(Me.ID & "_dtSalaryGroup")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_dtSalaryGroup") = value
        End Set
    End Property
    Dim _allowDataCache As New List(Of AllowanceListDTO)
    Property Working As HU_SALARYDTO
        Get
            Return ViewState(Me.ID & "_Working")
        End Get
        Set(ByVal value As HU_SALARYDTO)
            ViewState(Me.ID & "_Working") = value
        End Set
    End Property
    Property code_attendent As Decimal?
        Get
            Return ViewState(Me.ID & "_code_attendent")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_code_attendent") = value
        End Set
    End Property
    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    Property total As Decimal
        Get
            Return ViewState(Me.ID & "_total")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_total") = value
        End Set
    End Property

    Property basicSal As Decimal
        Get
            Return ViewState(Me.ID & "_basicSal")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_basicSal") = value
        End Set
    End Property
    Public Property List_FrameProductivity_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_FrameProductivity_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_FrameProductivity_ID")
        End Get
    End Property
#End Region
#Region "Page"
    ''' <summary>
    ''' Khoi tao page, load control, menu toolbar, data grid
    ''' Set trang thai page, control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            If (isPopup) Then
                btnFindEmployee.Visible = False
            End If
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao cac control
    ''' set thuoc tinh grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox: Loai to trinh/QD
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As DataTable = New DataTable()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMassTransferSalary
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data theo ID nhan vien neu page o trang thai edit
    ''' Load trang thai page
    ''' </summary>
    ''' <param name="Message">Check trang thai cua page</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim profileRep As New ProfileRepository
        Dim comRep As New CommonRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As DataTable
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Working = rep.GetHuSalaryByID(Working)
                    hidID.Value = Working.ID.ToString
                    hidEmp.Value = Working.EMPLOYEE_ID
                    EmployeeID = hidEmp.Value
                    txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                    txtEmployeeName.Text = Working.EMPLOYEE_NAME
                    If Working.TITLE_ID.HasValue Then
                        hidTitle.Value = Working.TITLE_ID
                    End If
                    If Working.ORG_ID.HasValue Then
                        hidOrg.Value = Working.ORG_ID
                    End If

                    txtTitleName.Text = Working.TITLE_NAME
                    txtOrgName.Text = Working.ORG_NAME
                    rdEffectDate.SelectedDate = Working.EFFECT_DATE
                    txtRemark.Text = Working.REMARK
                    rtSAL_RANK_ID.Text = Working.PRODUCTIVITY_NAME
                    rnCOEFFICIENT.Value = Working.COEFICIENT_NSLDBQ
                    If Working.UNCLE_SALARY.HasValue Then
                        HidFrameProductivity.Value = Working.UNCLE_SALARY
                        Dim LstrankObj = comRep.GetFrameSalaryAll()
                        Dim item = (From p In LstrankObj Where p.ID = Working.UNCLE_SALARY).FirstOrDefault
                        If item IsNot Nothing Then
                            lbInfo.Text = item.DESCRIPTION_PATH.ToString.Substring(item.DESCRIPTION_PATH.ToString.IndexOf(";") + 1).Replace(";", "-->") + " - " + "Hệ số " + item.COEFFICIENT.ToString
                        End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
#Region "Event"

    ''' <summary>
    ''' Event click item cua menu toolbar
    ''' Check validate page khi an luu
    ''' Redirect ve trang Quan ly ho so luong khi an huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New HU_SALARYDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        Dim factorSal As Decimal = 0

                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn Ngày hiệu lực"), NotifyType.Warning)
                            rdEffectDate.Focus()
                            Exit Sub
                        End If

                        Dim gID As Decimal
                        With objWorking
                            .EMPLOYEE_ID = hidEmp.Value
                            .TITLE_ID = hidTitle.Value
                            .ORG_ID = hidOrg.Value
                            .EFFECT_DATE = rdEffectDate.SelectedDate
                            .UNCLE_SALARY = HidFrameProductivity.Value
                            .COEFICIENT_NSLDBQ = rnCOEFFICIENT.Value
                            .REMARK = txtRemark.Text
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                If rep.InsertHu_Salary(objWorking, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    If (isPopup) Then
                                        Response.Redirect("/Dialog.aspx?mid=Profile&fid=ctrlHU_SalaryNewEdit&group=Business&empID=" & hidEmp.Value)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Salary&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWorking.ID = Decimal.Parse(hidID.Value)
                                'If Not ValidateDecisionNo(objWorking) Then

                                '    ShowMessage(Translate("Ngày hiệu lực bị trùng"), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                If rep.ModifyHu_Salary(objWorking, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Salary&group=Business")

                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Salary&group=Business")
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_CHECK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim gID As Decimal
                Dim objWorking As New HU_SALARYDTO
                Dim rep As New ProfileBusinessRepository
                Dim strUrl As String = Request.Url.ToString()
                Dim isPopup As Boolean = False
                If (strUrl.ToUpper.Contains("DIALOG")) Then
                    isPopup = True
                End If
                Dim factorSal As Decimal = 0
                With objWorking
                    .EMPLOYEE_ID = hidEmp.Value
                    .TITLE_ID = hidTitle.Value
                    .ORG_ID = hidOrg.Value

                    .EFFECT_DATE = rdEffectDate.SelectedDate



                    If hidSign.Value <> "" Then
                        .SIGN_ID = hidSign.Value
                    End If

                    .REMARK = txtRemark.Text


                End With

                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        'If Not ValidateDecisionNo(objWorking) Then
                        '    ShowMessage(Translate("Ngày hiệu lực bị trùng"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rep.InsertHu_Salary(objWorking, gID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            If (isPopup) Then
                                Response.Redirect("/Dialog.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&empID=" & hidEmp.Value)
                            Else
                                Session("Result") = 1
                                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
                            End If
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        objWorking.ID = Decimal.Parse(hidID.Value)
                        If rep.ModifyHu_Salary(objWorking, gID) Then
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


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
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()

            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click huy tren form popup list Nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked,
                                 ctrlFindSigner.CancelClicked,
                                 ctrlFindFrameProductivityPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub btnFindFrameProductivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindFrameProductivity.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            If sender IsNot Nothing Then
                List_FrameProductivity_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindFrameProductivityPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.FrameProductivitySelectedEventArgs) Handles ctrlFindFrameProductivityPopup.FrameProductivitySelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim Item = ctrlFindFrameProductivityPopup.CurrentItemDataObject

            If Item IsNot Nothing Then
                If Item.IS_LEVEL3 Then
                    HidFrameProductivity.Value = e.CurrentValue
                    'hidFrameProductivityRank.Value = Item.COEFFICIENT
                    rtSAL_RANK_ID.Text = Item.NAME_VN
                    If IsNumeric(Item.COEFFICIENT) Then
                        rnCOEFFICIENT.Value = CDec(Item.COEFFICIENT)
                    End If
                    lbInfo.Text = Item.DESCRIPTION_PATH.ToString.Substring(Item.DESCRIPTION_PATH.ToString.IndexOf(";") + 1).Replace(";", "-->") + " - " + "Hệ số " + Item.COEFFICIENT.ToString
                Else
                    HidFrameProductivity.Value = Nothing
                    rtSAL_RANK_ID.Text = Nothing
                    rdEffectDate.SelectedDate = Nothing
                    ShowMessage(Translate("Vui lòng chọn khung lương Level3"), Utilities.NotifyType.Warning)

                End If


            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub rtSAL_RANK_ID_TextChanged(sender As Object, e As EventArgs) Handles rtSAL_RANK_ID.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If rtSAL_RANK_ID.Text.Trim <> "" Then
                    Dim List_FrameProductivity = rep.GetFrameProductivityAll()
                    Dim FrameProductivityList = (From p In List_FrameProductivity Where p.NAME_VN.ToUpper.Contains(rtSAL_RANK_ID.Text.Trim.ToUpper)).ToList
                    If FrameProductivityList.Count <= 0 Then
                        ShowMessage(Translate("Hệ số năng suất không tồn tại."), Utilities.NotifyType.Warning)
                        ClearControlValue(hidOrg, rtSAL_RANK_ID)
                    Else
                        List_FrameProductivity_ID = (From p In FrameProductivityList Select p.ID).ToList
                        btnFindFrameProductivity_Click(Nothing, Nothing)
                    End If
                Else
                    HidFrameProductivity.Value = Nothing
                    rtSAL_RANK_ID.Text = ""

                    HidFrameProductivity.Value = Nothing
                    hidFrameProductivityRank.Value = Nothing
                    rtSAL_RANK_ID.Text = ""
                End If
            Else
                HidFrameProductivity.Value = Nothing
                rtSAL_RANK_ID.Text = ""

                HidFrameProductivity.Value = Nothing
                hidFrameProductivityRank.Value = Nothing
                rtSAL_RANK_ID.Text = ""
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click Chon ma nhan vien tu popup list nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            hidempid1.Value = ctrlFindEmployeePopup.SelectedEmployeeID(0)

            isLoadPopup = 0

            FillDataByEmployeeID(empID)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Chon ngay het hieu luc phai lon hon ngay hieu luc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep1 As New ProfileBusinessRepository
        Dim store As New ProfileStoreProcedure
        Try


        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub txtEmployeeCode_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCode.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothing
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Reset_Find_Emp()
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0)
                        isLoadPopup = 0
                        FillDataByEmployeeID(empID.ID)

                    ElseIf Count > 1 Then
                        If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            hidID.Value = Nothing
            hidEmp.Value = Nothing
            hidempid1.Value = Nothing
            EmployeeID = 0
            code_attendent = Nothing
            hidTitle.Value = Nothing
            hidOrg.Value = Nothing


            hidStaffRank.Value = Nothing
            hidStaffRank.Value = Nothing
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
#Region "Custom"

    ''' <summary>
    ''' Khoi tao control, Khoi tao popup list Danh sach nhan vien theo 2 loai man hinh
    ''' Set trang thai page, trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, btnFindEmployee)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnFindEmployee)
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
                Case 2
                    If Not phFindSign.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = True
                        ctrlFindSigner.MustHaveContract = False
                    End If
                Case 3

                    ctrlFindFrameProductivityPopup = Me.Register("ctrlFindFrameProductivityPopup", "Common", "ctrlFindFrameProductivityPopup")
                    ctrlFindFrameProductivityPopup.FrameProductivityType = FrameProductivityType.FrameProductivityLocation
                    If List_FrameProductivity_ID IsNot Nothing AndAlso List_FrameProductivity_ID.Count > 0 Then
                        ctrlFindFrameProductivityPopup.Bind_Find_ValueKeys = List_FrameProductivity_ID

                    End If
                    ctrlFindFrameProductivityPopup.IS_HadLoad = False
                    phFindFrameProductivity.Controls.Add(ctrlFindFrameProductivityPopup)
            End Select
            Dim strUrl As String = Request.Url.ToString()
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                txtEmployeeCode.Enabled = False
                btnFindEmployee.Enabled = False

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Get ID Nhan vien tu ctrlHU_WageMng khi o trang thai Edit
    ''' Fill data theo ID Nhan vien len cac control khi o trang thai sua
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    Dim ID As String = Request.Params("ID")
                    If Working Is Nothing Then
                        Working = New HU_SALARYDTO With {.ID = Decimal.Parse(ID)}
                    End If
                    Refresh("UpdateView")
                    Exit Sub
                End If
                If Request.Params("empID") IsNot Nothing Then
                    Dim empID = Request.Params("empID")
                    'FillData(empID)
                End If
                Refresh("NormalView")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Fill data len control theo id moi nhat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillData()
        Try
            Dim rep As New ProfileBusinessRepository
            Dim profileRep As New ProfileRepository
            Dim Working1 As HU_SALARYDTO


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub FillDataByEmployeeID(ByVal gID As Decimal)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            Dim emp = rep.GetEmployeeByID(gID)
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtEmployeeName.Text = emp.FULLNAME_VN
            txtTitleName.Text = emp.TITLE_NAME_VN
            txtOrgName.Text = emp.ORG_NAME
            hidEmp.Value = gID
            hidempid1.Value = gID
            hidTitle.Value = emp.TITLE_ID
            hidOrg.Value = emp.ORG_ID
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region
#Region "Utitily"""
    Public Function GetYouMustChoseMsg(ByVal input) As String
        Return String.Format("{0} {1}", Errors.YouMustChose, input)
    End Function

    Private Function ConvertNumber(ByVal value As Decimal?) As Decimal
        If value.HasValue Then
            Return value.Value
        End If
        Return 0
    End Function

    'Private Function ValidateDecisionNo(ByVal working As HU_SALARYDTO) As Boolean
    '    Using rep As New ProfileBusinessRepository
    '        'Dim a = rep.ValidateWorking("EXIST_EFFECT_DATE", working)
    '        Return rep.ValidateWorking("EXIST_EFFECT_DATE_IS_WAGE", working)
    '    End Using
    'End Function


#End Region

End Class
Structure DATA_IN_SALARY
    Public EMPLOYEE_ID As Decimal?
    Public EFFECT_DATE As String
End Structure