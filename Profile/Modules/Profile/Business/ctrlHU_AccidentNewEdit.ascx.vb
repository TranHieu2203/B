Imports System.Globalization
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_AccidentNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager

    Private psp As New ProfileStoreProcedure
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _year As Decimal = Year(DateTime.Now)
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDDetailSelecting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDebtSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_IDDebtSelecting")
        End Get

        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_IDDebtSelecting") = value
        End Set
    End Property

    Property lstHandoverContent As List(Of HandoverContentDTO)
        Get
            Return ViewState(Me.ID & "_lstHandoverContent")
        End Get
        Set(ByVal value As List(Of HandoverContentDTO))
            ViewState(Me.ID & "_lstHandoverContent") = value
        End Set
    End Property

    Property lstDebtForEdit As List(Of DebtDTO)
        Get
            Return ViewState(Me.ID & "_lstDebtForEdit")
        End Get
        Set(ByVal value As List(Of DebtDTO))
            ViewState(Me.ID & "_lstDebtForEdit") = value
        End Set
    End Property

    'Property lstReason As List(Of TerminateReasonDTO)
    '    Get
    '        Return ViewState(Me.ID & "_lstReason")
    '    End Get
    '    Set(ByVal value As List(Of TerminateReasonDTO))
    '        ViewState(Me.ID & "_lstReason") = value
    '    End Set
    'End Property

    Property Terminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_Terminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_Terminate") = value
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

    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property

    Property objTerminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_objTerminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_objTerminate") = value
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

    Property dt_TRUYTHUBHYT As DataTable
        Get
            Return ViewState(Me.ID & "_dt_TRUYTHUBHYT")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dt_TRUYTHUBHYT") = value
        End Set
    End Property

    Property SelectOrg As String
        Get
            Return ViewState(Me.ID & "_SelectOrg")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrg") = value
        End Set
    End Property

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

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer

    Dim IDSelect As Decimal?

#End Region

#Region "Page"

    ''' <summary>
    ''' Khởi tạo, Load page, load info cho control theo ID
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            GetParams()
            Refresh()
            UpdateControlState()


            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Khởi tạo load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Khởi tạo, Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarTerminate
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(3), RadToolBarButton).Text = Translate("Mở chờ phê duyệt")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    hidID.Value = IDSelect
                    Dim acc = rep.GetAccidentByID(New AccidentDTO With {.ID = IDSelect})

                    hidEmpID.Value = acc.EMPLOYEE_ID
                    txtEmployeeCode.Text = acc.EMPLOYEE_CODE
                    txtOrgName.Text = acc.ORG_NAME
                    txtTitleName.Text = acc.TITLE_NAME
                    txtEmployeeName.Text = acc.EMPLOYEE_NAME
                    txtMaThe.Text = acc.MA_THE
                    If acc.ACCIDENT_DATE IsNot Nothing Then
                        txtAccidentDate.SelectedDate = acc.ACCIDENT_DATE
                    End If

                    txtCode.Text = acc.COST
                    If acc.REASON_ID IsNot Nothing Then
                        cboReasin.SelectedValue = acc.REASON_ID
                    End If

                    txtInfo.Text = acc.INFORMATION
                    txtTreatment.Text = acc.TREATMENT_PLACE
                    txtMoney.Text = acc.MONEY
                    If acc.MONEY_DATE IsNot Nothing Then
                        rdMoney.SelectedDate = acc.MONEY_DATE
                    End If

                    RadTextBox1.Text = acc.REMARK

                Case "InsertView"

                    CurrentState = CommonMessage.STATE_NEW
                Case "NormalView"

                    Refresh("UpdateView")
                    txtEmployeeCode.ReadOnly = True
                    txtEmployeeName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtOrgName.ReadOnly = True
                    txtMaThe.ReadOnly = True
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
    ''' Event Click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim rep_Store As New ProfileStoreProcedure
        Dim _filter As New TerminateDTO
        Dim dtData As New DataTable
        Dim _objfilter As New TerminateDTO
        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _objfilter.ID = hidID.Value
                        End If

                        Dim acc = New AccidentDTO

                        acc.EMPLOYEE_ID = hidEmpID.Value
                        acc.ACCIDENT_DATE = If(txtAccidentDate.SelectedDate IsNot Nothing, txtAccidentDate.SelectedDate, Nothing)
                        acc.COST = CDec(Val(txtCode.Text))
                        If cboReasin.SelectedValue <> "" Then
                            acc.REASON_ID = cboReasin.SelectedValue
                        End If
                        acc.INFORMATION = txtInfo.Text
                        acc.TREATMENT_PLACE = txtTreatment.Text
                        acc.MONEY = CDec(Val(txtMoney.Text))
                        acc.MONEY_DATE = If(rdMoney.SelectedDate IsNot Nothing, rdMoney.SelectedDate, Nothing)
                        acc.REMARK = RadTextBox1.Text

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertAccident(acc, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Accident&group=Business")
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_AccidentNewEdit&group=Business&FormType=0")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                acc.ID = hidID.Value
                                Dim listID As New List(Of Decimal)
                                listID.Add(hidID.Value)
                                If rep.ValidateBusiness("HU_ACCIDENT", "ID", listID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyAccident(acc, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Accident&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Accident&group=Business")
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                        ShowMessage(Translate("Trạng thái hiện tại đang là Chờ Phê Duyệt"), NotifyType.Warning)
                        Exit Sub
                    Else

                        If hidEmpID.Value <> "" Then
                            If rep_Store.UPDATE_STATUS_UNLOCK_TERMINATE(Decimal.Parse(hidEmpID.Value)) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                            End If
                        End If
                    End If

            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


    ''' <summary>
    ''' Event Yes/No trên Message popup hỏi áp dụng, ngừng áp dụng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If rep.InsertTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        objTerminate.ID = Decimal.Parse(hidID.Value)
                        objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                        If rep.ModifyTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub






    ''' <summary>
    ''' Event button tìm mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 1)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Using rep1 As New ProfileBusinessRepository
                    Dim check = rep.Check_has_Ter(item.ID)
                    If check = 1 Then
                        ShowMessage(Translate("Nhân viên đã có quyết định nghỉ việc"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Using
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'txtDecisionNo.Text = item.EMPLOYEE_CODE.Substring(1) + " / QDTV-KSF"
                FillDataByEmployeeID(item.ID)

            End If

            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Fill data lên các control theo ID truyền đến
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
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
            hidOrgAbbr.Value = emp.ORG_ID
            hidEmpID.Value = emp.ID
            hidTitleID.Value = emp.TITLE_ID
            hidOrgID.Value = emp.ORG_ID
            txtMaThe.Text = emp.MA_THE

            Dim dt = store.CAL_DEBT_EMP(Decimal.Parse(hidEmpID.Value))

            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Thâm niên công tác = ngày làm việc cuối - ngày vào làm
    'ngày < 15 = 0.5 ngày
    'ngày >= 15 = 1 tháng
    Private Function CalculateSeniority(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim dSoNam As Double
        Dim dSoThang As Double
        Dim dNgayDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim clsDate As Framework.Data.DateDifference = New Framework.Data.DateDifference(dStart, Date.Now)
            'Dim clsDate As DateDifference = New DateDifference(dStart, dEnd)
            'dSoNam = Math.Round(((dEnd - dStart).Days + 1) / 365, 0)
            Dim sonam = ((dEnd - dStart).Days + 1) / 365
            'dSoThang = clsDate.Months
            Dim pos = sonam.ToString().IndexOf(",")
            If pos = -1 Then
                dSoNam = sonam

                dSoThang = 0
            Else
                dSoNam = sonam.ToString().Substring(0, 2)
                sonam = (sonam - dSoNam) * 12
                Dim sonamdu As Decimal = "0" + sonam.ToString().Substring(pos + 1, 2)
                dSoThang = Math.Round(sonam, 2)
            End If


            'If dNgayDuThang < 15 Then
            '    dSoThang = dSoThang + 0.5
            'Else
            '    dSoThang = dSoThang + 1
            '    If dSoThang = 12 Then
            '        dSoNam = dSoNam + 1
            '        dSoThang = 0
            '    End If
            'End If
            Dim str As String

            If dSoNam = 0 And dSoThang = 0 Then
                str = ""
            End If

            If dSoNam = 0 And dSoThang <> 0 Then
                str = dSoThang & " Tháng"
            End If

            If dSoNam <> 0 And dSoThang = 0 Then
                str = dSoNam & " Năm"
            End If

            If dSoNam <> 0 And dSoThang <> 0 Then
                str = dSoNam & " Năm " & dSoThang & " Tháng"
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Function CalculateSeniority1(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileBusinessRepository
        Dim Cal_Month_Emp As Int32 = 0
        Dim str As String = ""
        Dim Cal1 As Integer = 0
        Dim Cal2 As Integer = 0
        Dim lastDayOfMonth As Integer = 0
        Dim dSoNam As Double = 0
        Dim dSoThang As Double = 0
        Dim Total_Month As Decimal = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Dim Cal_Day_Emp = Math.Round((CDate(dEnd).Subtract(CDate(dStart)).TotalDays) + 1, 2)
            'Dim Cal_Month_Emp = Math.Round(Cal_Day_Emp / 365 * 12, 2) 


            ' Tinh tham nien lam viec truoc
            Dim Month_Work = rep.Get_Month_Work_Before(hidEmpID.Value)
            ' tham nien nhan vien tai HSV
            Cal_Month_Emp = (DateDiff(DateInterval.Month, CDate(dStart), CDate(dEnd))) + 1
            If CDate(dStart).Day <= 5 Then
                Cal1 = 1
            Else
                Cal1 = 0
            End If
            lastDayOfMonth = (DateTime.DaysInMonth(CDate(dEnd).Year, CDate(dEnd).Month)) - 5
            If CDate(dEnd).Day >= lastDayOfMonth Then
                Cal2 = 1
            Else
                Cal2 = 0
            End If
            Total_Month = Math.Round(((Cal_Month_Emp - 2 + Cal1 + Cal2) + Month_Work), 2)
            If IsNumeric(Total_Month) Then
                dSoNam = Total_Month \ 12
                dSoThang = Math.Round(CDec(Total_Month) Mod 12, 2)
                str = If(dSoNam > 0, dSoNam.ToString + " Năm ", "") + If(Math.Round(CDec(dSoThang) Mod 12, 2) > 0, Math.Round(CDec(dSoThang) Mod 12, 2).ToString + " Tháng", "")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub GetYearOrMonthByText(ByVal str As String, ByRef dSoNam As Double, ByRef dSoThang As Double)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'str = "11,5 Tháng"
            If str = "" Then
                dSoNam = 0
                dSoThang = 0
            End If
            Dim findYear = str.IndexOf("Năm")
            Dim findMonth = str.IndexOf("Tháng")
            If findYear <> -1 And findMonth <> -1 Then
                dSoNam = Double.Parse(str.Substring(0, 2))
                dSoThang = Double.Parse(str.Substring(findYear + 3, findMonth - findYear - 3).Trim)
            ElseIf findYear = -1 And findMonth <> -1 Then
                dSoNam = 0
                dSoThang = Double.Parse(str.Substring(0, findMonth))
            ElseIf findYear <> -1 And findMonth = -1 Then
                dSoNam = Double.Parse(str.Substring(0, findYear))
                dSoThang = 0
            Else
                dSoNam = 0
                dSoThang = 0
            End If
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 2)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
            End If
            isLoadPopup = 0
            'rgLabourProtect.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Cancel Popup list mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
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
                        Dim empID = EmployeeList(0).EMPLOYEE_ID
                        If empID IsNot Nothing Then
                            Using rep1 As New ProfileBusinessRepository
                                Dim check = rep1.Check_has_Ter(empID)
                                If check = 1 Then
                                    ShowMessage(Translate("Nhân viên đã có quyết định nghỉ việc"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End Using
                            'txtDecisionNo.Text = item.EMPLOYEE_CODE.Substring(1) + " / QDTV-KSF"
                            FillDataByEmployeeID(EmployeeList(0).ID)

                        End If


                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.CurrentValue = SelectOrg
                            ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.IS_3B = 2
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.MultiSelect = False
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
            ClearControlValue(txtEmployeeName, txtTitleName, txtOrgName)

        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"


    ''' <summary>
    ''' Khoi tao, load popup list ma Nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindSign.Controls.Contains(ctrlFindSigner) Then
                phFindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.CurrentValue = SelectOrg
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.IS_3B = 2
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                Case 2
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.CurrentValue = SelectOrg
                    'ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                    phFindSign.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository

        Try

            Dim dtData As New DataTable
            dtData = rep.GetOtherList("CB_DANHGIA", True)
            FillRadCombobox(cboReasin, dtData, "NAME", "ID", True)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Get data theo ID Ma nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                Select Case FormType
                    Case 0
                        Refresh("InsertView")
                    Case 1
                        Refresh("UpdateView")
                    Case 2
                        Refresh("NormalView")
                    Case 3
                        Dim empID = Request.Params("empid")
                        FillDataByEmployeeID(empID)

                        hidEmpID.Value = empID
                        Refresh("InsertView")
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load dataSource cho grid Ly Do Nghi Viec
    ''' </summary>
    ''' <param name="lstReason"></param>
    ''' <remarks></remarks>
    Private Sub CreateDataHandoverContent(Optional ByVal lstHandoverContent As List(Of HandoverContentDTO) = Nothing)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If lstHandoverContent Is Nothing Then
                lstHandoverContent = New List(Of HandoverContentDTO)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub






    Public Sub GetSigner()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



#End Region

#Region "Caculate"



    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "DateDifference"

    Public Class DateDifference
        ''' <summary>
        ''' defining Number of days in month; index 0=> january and 11=> December
        ''' february contain either 28 or 29 days, that's why here value is -1
        ''' which wil be calculate later.
        ''' </summary>
        Private monthDay() As Integer = {31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

        ''' <summary>
        ''' từ ngày
        ''' </summary>
        Private fromDate As Date

        ''' <summary>
        ''' đến ngày
        ''' </summary>
        Private toDate As Date

        ''' <summary>
        ''' 3 tham số trả về
        ''' </summary>
        Private year As Integer
        Private month As Integer
        Private day As Integer
        Dim _mylog As New MyLog()
        Dim _pathLog As String = _mylog._pathLog
        Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

        Public Sub New(ByVal d1 As Date, ByVal d2 As Date)
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim increment As Integer

                If d1 > d2 Then
                    Me.fromDate = d2
                    Me.toDate = d1
                Else
                    Me.fromDate = d1
                    Me.toDate = d2
                End If

                ' Tính toán ngày
                increment = 0

                If Me.fromDate.Day > Me.toDate.Day Then
                    increment = Me.monthDay(Me.fromDate.Month - 1)
                End If

                ' Nếu là tháng 2
                If increment = -1 Then
                    ' kiểm tra năm nhuận
                    If Date.IsLeapYear(Me.fromDate.Year) Then
                        ' nếu là năm nhuận tháng 2 có 29 ngày
                        increment = 29
                    Else
                        increment = 28
                    End If
                End If
                ' Nếu không phải tháng 2
                If increment <> 0 Then
                    day = (Me.toDate.Day + increment) - Me.fromDate.Day
                    increment = 1
                Else
                    day = Me.toDate.Day - Me.fromDate.Day
                End If

                ' tính ra số tháng
                If (Me.fromDate.Month + increment) > Me.toDate.Month Then
                    Me.month = (Me.toDate.Month + 12) - (Me.fromDate.Month + increment)
                    increment = 1
                Else
                    Me.month = (Me.toDate.Month) - (Me.fromDate.Month + increment)
                    increment = 0
                End If

                ' tính ra số năm
                Me.year = Me.toDate.Year - (Me.fromDate.Year + increment)
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try


        End Sub

        Public ReadOnly Property Years() As Integer
            Get
                Return Me.year
            End Get
        End Property

        Public ReadOnly Property Months() As Integer
            Get
                Return Me.month
            End Get
        End Property

        Public ReadOnly Property Days() As Integer
            Get
                Return Me.day
            End Get
        End Property

    End Class
#End Region

End Class
