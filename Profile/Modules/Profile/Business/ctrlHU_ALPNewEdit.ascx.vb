Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_ALPNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSignPopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSalaryPopup As ctrlFindSalaryPopup
    Public Overrides Property MustAuthorize As Boolean = False

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

    Property Contract As TrainningManageDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As TrainningManageDTO)
            ViewState(Me.ID & "_Contract") = value
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

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
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
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 06/07/2017 14:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
                ViewConfig(LeftPane)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContract

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Contract = rep.GetALPByID(New TrainningManageDTO With {.ID = hidID.Value})
                    If Contract IsNot Nothing Then
                        hidID.Value = Contract.ID
                        hidEmployeeID.Value = Contract.EMPLOYEE_ID.ToString
                        hidID.Value = Contract.ID.ToString
                        txtEmployeeCode.Text = Contract.EMPLOYEE_CODE
                        txtEmployeeName.Text = Contract.EMPLOYEE_NAME
                        txtTITLE.Text = Contract.TITLE_NAME
                        txtOrg_Name.Text = Contract.ORG_NAME
                        rdStartDate.SelectedDate = Contract.START_DATE
                        rdExpireDate.SelectedDate = Contract.EXPIRE_DATE
                        txtRemark.Text = Contract.REMARK
                        txtCost.Text = Contract.COST
                        txtYear.Value = Contract.WORK_STATUS
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

    ''' <lastupdate>
    ''' 06/07/2017 15:41
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objContract As New TrainningManageDTO
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        'Dim stt As OtherListDTO
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
                        Dim employee = rep.GetEmployeeByID(Decimal.Parse(hidEmployeeID.Value))
                        objContract.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
                        objContract.WORK_STATUS = txtYear.Value
                        objContract.EXPIRE_DATE = rdExpireDate.SelectedDate
                        objContract.START_DATE = rdStartDate.SelectedDate
                        objContract.ORG_ID = employee.ORG_ID
                        objContract.TITLE_ID = employee.TITLE_ID
                        If txtCost.Text <> "" Then
                            objContract.COST = txtCost.Text
                        End If
                        objContract.REMARK = txtRemark.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertALP(objContract, gID) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ALP&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objContract.ID = Decimal.Parse(hidID.Value)
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(hidID.Value)
                                If rep.ModifyALP(objContract, gID) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ALP&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Exit Sub
                    End If
                Case "UNLOCK"
                    objContract.ID = Decimal.Parse(hidID.Value)

                Case CommonMessage.TOOLBARITEM_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ALP&group=Business")
                    End If
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:34
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            'LoadPopup(1)
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xu ly su kien click vao button btnSigner
    ''' Hien thi popup co isLoadPopup = 2 khi click vao button
    ''' Cap nhat lai trang thai cac control tren page hien tai
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '' <lastupdate>
    '' 06/07/2017 17:44
    '' </lastupdate>
    '' <summary>
    '' Xu ly su kien click khi click vao button btnSalary
    '' Hien thi popup voi isLoadPopup = 3
    '' Cap nhat lai trang thai cac control tren page
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    'Protected Sub btnSalary_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalary.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If hidEmployeeID.Value = "" Then
    '            ShowMessage(Translate("Bạn phải chọn nhân viên."), NotifyType.Warning)
    '            Exit Sub
    '        End If
    '        isLoadPopup = 3
    '        UpdateControlState()
    '        ' LoadPopup(3)
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindSignPopup.CancelClicked,
        ctrlFindEmployeePopup.CancelClicked,
        ctrlFindSalaryPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSignPopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                FillData(lstEmpID(0))
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSalaryPopup_Salaryq
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cua control ctrlMessageBox_Button
    ''' Neu command là item xoa thi cap nhat lai trang thai hien tai la xoa
    ''' Cap nhat lai trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selectedIndexChanged cua control cboContractType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub cboContractType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContractType.SelectedIndexChanged
    '    Dim item As New ContractTypeDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If rdStartDate.SelectedDate IsNot Nothing Then

    '            Dim dExpire As Date = rdStartDate.SelectedDate
    '            item = (From p In ListComboData.LIST_CONTRACTTYPE Where p.ID = Decimal.Parse(cboContractType.SelectedValue)).SingleOrDefault
    '            If item IsNot Nothing Then
    '                hidPeriod.Value = item.PERIOD
    '            End If

    '            If CType(hidPeriod.Value, Double) = 0 Then
    '                rdExpireDate.SelectedDate = Nothing
    '            Else
    '                dExpire = dExpire.AddMonths(CType(hidPeriod.Value, Double))
    '                rdExpireDate.SelectedDate = dExpire
    '            End If
    '        End If

    '        Dim employeeId As Double = 0
    '        Double.TryParse(hidEmployeeID.Value, employeeId)
    '        'txtContractNo.Text = CreateDynamicContractNo(employeeId)
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedDateChanged cua control rdStartDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cua control CompareStartDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CompareStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CompareStartDate.ServerValidate
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim _filter As New ContractDTO
            _filter.START_DATE = rdStartDate.SelectedDate
            _filter.EMPLOYEE_ID = hidEmployeeID.Value
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                    _filter.ID = hidID.Value
            End Select
            'args.IsValid = rep.ValidateContract("EXIST_EFFECT_DATE", _filter)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cua control cusvalContractType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>

#Region "Custom"

    ''' <lastupdate>
    ''' 06/07/2018 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedDateChanged cua control rdStartDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdStartDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdStartDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim difference As TimeSpan
        Try
            If rdStartDate.SelectedDate IsNot Nothing AndAlso rdExpireDate.SelectedDate IsNot Nothing Then
                difference = rdExpireDate.SelectedDate.Value.AddDays(1) - rdStartDate.SelectedDate
                txtCost.Value = difference.TotalDays
            Else
                txtCost.Value = 0
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 06/07/2018 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedDateChanged cua control rdStartDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdExpireDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdExpireDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim difference As TimeSpan
        Try
            If rdStartDate.SelectedDate IsNot Nothing AndAlso rdExpireDate.SelectedDate IsNot Nothing Then
                difference = rdExpireDate.SelectedDate.Value.AddDays(1) - rdStartDate.SelectedDate
                txtCost.Value = difference.TotalDays
            Else
                txtCost.Value = 0
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub



    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm cập nhật trạng thái của các control trên page
    ''' Xử lý đăng ký popup ứng với giá trị isLoadPopup
    ''' </summary>
    ''' <remarks></remarks>

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            btnEmployee.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
            End Select
            LoadPopup(isLoadPopup)
            If (hidID.Value = "") Then
                If _toolbar Is Nothing Then Exit Sub
                Dim item As RadToolBarButton
                For i = 0 To _toolbar.Items.Count - 1
                    item = CType(_toolbar.Items(i), RadToolBarButton)
                    'Select Case CurrentState
                    '    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    If item.CommandName = "UNLOCK" Then
                        item.Enabled = False
                    End If
                    'End Select
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' cboContractType, cboStatus
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        ListComboData = New ComboBoxDataDTO
        ListComboData.GET_TRAINING_FORM = True
        rep.GetComboList(ListComboData)
        'FillDropDownList(cboContractType, ListComboData.LIST_TRAINING_FORM, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
        rep.Dispose()
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "IDSelect"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "EmpID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                'cboContractType.AutoPostBack = True
                If Request.Params("IDSelect") IsNot Nothing Then
                    hidID.Value = Request.Params("IDSelect")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
                If Request.Params("EmpID") IsNot Nothing Then
                    FillData(Request.Params("EmpID"))
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức fill dữ liệu cho page
    ''' theo các trạng thái maintoolbar và trạng thái item trên trang
    ''' </summary>
    ''' <param name="empID"></param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Using rep As New ProfileBusinessRepository
                Dim item = rep.GetContractEmployeeByID(empID)
                rdStartDate.MaxDate = Date.MaxValue
                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    MainToolBar.Items(0).Enabled = False
                Else
                    MainToolBar.Items(0).Enabled = True

                End If

                If item.WORK_STATUS IsNot Nothing Then
                    hidWorkStatus.Value = item.WORK_STATUS
                End If
                hidEmployeeID.Value = item.ID.ToString
                hidOrgCode.Value = item.ORG_CODE
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTITLE.Text = item.TITLE_NAME_VN
                'txtSTAFF_RANK.Text = item.STAFF_RANK_NAME
                txtOrg_Name.Text = item.ORG_NAME
                Dim employeeId As Double = 0
                Double.TryParse(hidEmployeeID.Value, employeeId)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc tạo số hợp đồng một cách tự động
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CreateDynamicContractNo(ByVal empId As Double) As String
        If empId < 1 Then
            Return String.Empty
        End If
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        'Try
        '    If CurrentState = CommonMessage.STATE_NORMAL Or
        '        CurrentState Is Nothing Or
        '         String.IsNullOrWhiteSpace(cboContractType.SelectedValue) Then
        '        Return String.Empty
        '    End If
        '    Using rep As New ProfileBusinessRepository
        '        Return rep.CreateContractNo(New ContractDTO With {
        '                                               .START_DATE = rdStartDate.SelectedDate,
        '                                               .ORG_CODE = hidOrgCode.Value,
        '                                               .EMPLOYEE_ID = empId,
        '                                               .EMPLOYEE_CODE = txtEmployeeCode.Text,
        '                                               .CONTRACTTYPE_ID = cboContractType.SelectedValue
        '                                               })
        '    End Using
        '    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        'Catch ex As Exception
        '    DisplayException(ViewName, ID, ex)
        '    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        '    Return String.Empty
        'End Try
    End Function
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy giá trị kinh nghiệm làm việc của nhân viên đạt mức lương cao nhất
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 1
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            Case 2
                If Not FindSigner.Controls.Contains(ctrlFindSignPopup) Then
                    ctrlFindSignPopup = Me.Register("ctrlFindSignPopup", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSignPopup)
                    ctrlFindSignPopup.MultiSelect = False
                    ctrlFindSignPopup.MustHaveContract = True
                    ctrlFindSignPopup.LoadAllOrganization = True
                End If

            Case 3
                If Not FindSalary.Controls.Contains(ctrlFindSalaryPopup) Then
                    ctrlFindSalaryPopup = Me.Register("ctrlFindSalaryPopup", "Profile", "ctrlFindSalaryPopup", "Shared")
                    ctrlFindSalaryPopup.MultiSelect = False
                    If hidEmployeeID.Value <> "" Then
                        ctrlFindSalaryPopup.EmployeeID = Decimal.Parse(hidEmployeeID.Value)
                        Session("EmployeeID") = Decimal.Parse(hidEmployeeID.Value)
                    End If

                    FindSalary.Controls.Add(ctrlFindSalaryPopup)
                    ctrlFindSalaryPopup.Show()
                End If
        End Select
    End Sub
#End Region


End Class