Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_WorkInfoNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSalaryPopup As ctrlFindSalaryPopup
    Public Overrides Property MustAuthorize As Boolean = True

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"
    Public Property popupId As String
    Public Property AjaxManagerId As String
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

    Property Working As WorkingBeforeDTO
        Get
            Return ViewState(Me.ID & "_Family")
        End Get
        Set(ByVal value As WorkingBeforeDTO)
            ViewState(Me.ID & "_Family") = value
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
    Property code_timekeeping As Integer
        Get
            Return ViewState(Me.ID & "_code_timekeeping")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_code_timekeeping") = value
        End Set
    End Property
    Property start_rankid As Integer
        Get
            Return ViewState(Me.ID & "_start_rankid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_start_rankid") = value
        End Set
    End Property
    Property object_labour As Integer
        Get
            Return ViewState(Me.ID & "_object_labour")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_object_labour") = value
        End Set
    End Property
    Property direct_manager As Integer
        Get
            Return ViewState(Me.ID & "_direct_manager")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_direct_manager") = value
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
    Property Is_dis As String
        Get
            Return ViewState(Me.ID & "_Is_dis")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Is_dis") = value
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
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            'If (strUrl.ToUpper.Contains("DIALOG")) Then
            '    isPopup = True
            'End If
            'If (isPopup) Then
            '    btnEmployee.Visible = False
            'End If
            Dim startTime As DateTime = DateTime.UtcNow
            'txtThamNien.Enabled = False
            GetParams()
            Refresh()
            UpdateControlState()

            If (_flag = False) Then
                EnableControlAll_Cus(False, LeftPane)
            End If

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
                'ViewConfig(LeftPane)
                'GirdConfig(rgAllow)
            End If
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
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
            Me.MainToolBar = tbarFamily

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            Dim use As New ProfileRepositoryBase

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = False
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
                    Working = rep.GetWorkingBeforeByID(hidID.Value)
                    If Working IsNot Nothing Then
                        hidID.Value = Working.ID.ToString
                        txtUploadFile.Text = Working.UPLOAD_FILE
                        txtUpload.Text = Working.FILE_NAME
                        If txtUpload.Text <> "" Then
                            btnDownload.Visible = True
                            If txtUpload.Text.ToUpper.Contains(".JPG") Or Working.FILE_NAME.ToUpper.Contains(".GIF") Or Working.FILE_NAME.ToUpper.Contains(".PNG") Then
                                btnView.Visible = True
                                Dim file = rep.GetFileForView(txtUploadFile.Text)
                                Dim link As String = file.LINK & "\" & file.FILE_NAME
                                link = link.Replace("\", "(slash)")
                                hidLink.Value = link
                            Else
                                btnView.Visible = False
                            End If
                        Else
                            btnDownload.Visible = False
                        End If
                        hidEmployeeID.Value = Working.EMPLOYEE_ID.ToString
                        txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                        txtEmployeeName.Text = Working.EMPLOYEE_NAME
                        txtOrg_Name.Text = Working.ORG_NAME
                        txtTITLE.Text = Working.TITLE_NAME
                        txtCompanyName.Text = Working.COMPANY_NAME
                        txtDEPARTMENT.Text = Working.DEPARTMENT
                        txtCompanyAddress.Text = Working.COMPANY_ADDRESS
                        txtTitleName.Text = Working.TITLE_NAME_BEFORE
                        'chkHSV.Checked = If(Working.IS_HSV Is Nothing, 0, Working.IS_HSV)
                        rdJoinDate.SelectedDate = Working.JOIN_DATE
                        rdEndDate.SelectedDate = Working.END_DATE
                        If Working.THAM_NIEN IsNot Nothing Then
                            txtThamNien.Text = Working.THAM_NIEN
                        End If
                        If IsNumeric(txtThamNien.Text) Then
                            txtThamNien_Detail.Text = If(CInt(CDec(txtThamNien.Text) \ 12) > 0, CInt(CDec(txtThamNien.Text) \ 12).ToString + " Năm ", "") + If(Math.Round(CDec(txtThamNien.Text) Mod 12, 2) > 0, Math.Round(CDec(txtThamNien.Text) Mod 12, 2).ToString + " Tháng", "")
                        End If
                        txtWork.Text = Working.WORK
                        txtTerReason.Text = Working.TER_REASON
                        'chkIs_Thamnien.Checked = If(Working.IS_THAMNIEN Is Nothing, 0, Working.IS_THAMNIEN)
                        'If Family.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                        '    MainToolBar.Items(0).Enabled = False
                        'End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    Dim dt As New DataTable
                    'rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                    'rgAllow.DataBind()
                    If Not IsPostBack Then
                        If Request.Params("empID") IsNot Nothing Then
                            Dim item = rep.GetContractEmployeeByID(CDec(Request.Params("empID")))
                            If item IsNot Nothing Then
                                If item.WORK_STATUS IsNot Nothing Then
                                    hidWorkStatus.Value = item.WORK_STATUS
                                End If
                                If IsNumeric(item.ID.ToString) Then
                                    hidEmployeeID.Value = item.ID.ToString
                                End If

                                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                                txtEmployeeName.Text = item.FULLNAME_VN
                                txtTITLE.Text = item.TITLE_NAME_VN
                                txtOrg_Name.Text = item.ORG_NAME
                                Dim employeeId As Double = 0
                                Double.TryParse(hidEmployeeID.Value, employeeId)
                            End If
                        End If
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
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
        Dim objFamily As New FamilyDTO
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
                    Page.Validate()

                    If Page.IsValid Then
                        Dim employee = rep.GetEmployeeByID(Decimal.Parse(hidEmployeeID.Value))

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                If Execute() Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)

                                    'If (isPopup) Then
                                    '    Response.Redirect("/Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&empID=" & hidEmployeeID.Value)
                                    'Else
                                    '    Session("Result") = 1
                                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1")
                                    'End If
                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT

                                If Execute() Then
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)

                                    'If (isPopup) Then
                                    '    Dim str As String = "getRadWindow().close('1');"
                                    '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    'Else
                                    '    Session("Result") = 1
                                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1")
                                    'End If
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1&empID=" & hidEmployeeID.Value)
                                Else
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Dim str As String = "document.getElementsByClassName('rtbBtn')[0].style.pointerEvents = 'auto';"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Exit Sub
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    'If (isPopup) Then
                    '    Dim str As String = "getRadWindow().close('0');"
                    '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    'Else
                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfo&group=Business")
                    'End If
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function Execute() As Boolean
        Try
            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
            Dim objWorkingBefore As New WorkingBeforeDTO
            Dim rep As New ProfileBusinessRepository
            objWorkingBefore.COMPANY_ADDRESS = txtCompanyAddress.Text
            objWorkingBefore.COMPANY_NAME = txtCompanyName.Text
            objWorkingBefore.DEPARTMENT = txtDEPARTMENT.Text
            objWorkingBefore.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
            objWorkingBefore.END_DATE = rdEndDate.SelectedDate
            objWorkingBefore.JOIN_DATE = rdJoinDate.SelectedDate
            'objWorkingBefore.IS_HSV = chkHSV.Checked
            objWorkingBefore.TER_REASON = txtTerReason.Text
            If IsNumeric(txtThamNien.Text) Then
                objWorkingBefore.THAM_NIEN = CDec(txtThamNien.Text)
            End If
            objWorkingBefore.TITLE_NAME_BEFORE = txtTitleName.Text
            objWorkingBefore.WORK = txtWork.Text
            objWorkingBefore.FILE_NAME = txtUploadFile.Text
            'objWorkingBefore.IS_THAMNIEN = chkIs_Thamnien.Checked
            Dim gID As Decimal
            If hidID.Value = "" Then
                rep.InsertWorkingBefore(objWorkingBefore, gID)
            Else
                objWorkingBefore.ID = Decimal.Parse(hidID.Value)
                rep.ModifyWorkingBefore(objWorkingBefore, gID)
            End If
            IDSelect = gID
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

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
        Handles ctrlFindSigner.CancelClicked, ctrlFindSigner2.CancelClicked,
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
    ''' Xu ly su kien selected cua control ctrlFindSigner_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSignPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID.ToString
                'txtSignTitle.Text = item.TITLE_NAME
                'txtSigner.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindSignPopup2_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign2.Value = item.ID.ToString
                'txtSignTitle2.Text = item.TITLE_NAME
                'txtSignName2.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
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
                        FillData(empID)
                        isLoadPopup = 0
                    ElseIf Count > 1 Then
                        If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                            'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                        End If
                        If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            ctrlFindEmployeePopup.EMP_CODE_OR_NAME = txtEmployeeCode.Text
                            ctrlFindEmployeePopup.MultiSelect = False
                            ctrlFindEmployeePopup.MustHaveContract = False
                            ctrlFindEmployeePopup.LoadAllOrganization = False
                            FindEmployee.Controls.Add(ctrlFindEmployeePopup)
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
            hidWorkStatus.Value = Nothing
            hidEmployeeID.Value = Nothing
            txtEmployeeName.Text = ""
            txtTITLE.Text = ""
            txtOrg_Name.Text = ""

            Dim employeeId As Double = 0
            Double.TryParse(hidEmployeeID.Value, employeeId)
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    'Public Sub Show(strfile As Object)
    '    ctrlViewImagePopup = Me.Register("ctrlViewimagePopup", "Profile", "ctrlViewImagePopup", "Shared")
    '    ctrlViewImagePopup.URL = strfile
    '    ViewImage.Controls.Add(ctrlViewimagePopup)
    '    ctrlViewImagePopup.Show()
    'End Sub
    'Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        Dim file As New FileUploadDTO
    '        Using rep As New ProfileBusinessRepository
    '            file = rep.GetFileForView(txtUploadFile.Text)
    '        End Using
    '        Dim link As String = file.LINK & "\" & file.FILE_NAME
    '        Dim strName As String = IO.Path.GetExtension(link).ToUpper()

    '        link = link.Replace("\", "(slash)")
    '        If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
    '            Show(link)
    '        Else
    '            ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim fileObj As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    fileObj = rep.GetFileForView(txtUploadFile.Text)
                End Using
                Dim link = fileObj.LINK
                Dim name = fileObj.FILE_NAME
                Dim path = link & name
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
                Response.WriteFile(file.FullName)
                Response.End()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    Dim fileContent As Byte() = New Byte(file.ContentLength) {}

                    Dim buffer As Byte() = New Byte(file.ContentLength - 1) {}
                    Using str As Stream = file.InputStream
                        str.Read(buffer, 0, buffer.Length)
                    End Using
                    Dim guidID = Guid.NewGuid.ToString()
                    Dim obj As New FileUploadDTO
                    obj.FILE_NAME = file.FileName
                    obj.CODE_PATH = "STAFFDETAILWORKINGBEFORE"
                    obj.NAME = guidID
                    If Not rep.AddFileUpload(obj, buffer) Then
                        txtUpload.Text = file.FileName
                        txtUploadFile.Text = obj.NAME
                        btnDownload.Visible = True
                        If obj.FILE_NAME.ToUpper.Contains(".JPG") Or obj.FILE_NAME.ToUpper.Contains(".GIF") Or obj.FILE_NAME.ToUpper.Contains(".PNG") Then
                            btnView.Visible = True
                            Dim fileObj = rep.GetFileForView(txtUploadFile.Text)
                            Dim link As String = fileObj.LINK & "\" & fileObj.FILE_NAME
                            link = link.Replace("\", "(slash)")
                            hidLink.Value = link
                        Else
                            btnView.Visible = False
                        End If
                    End If
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(Translate("Upload file bị lỗi"), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"



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
            'btnEmployee.Enabled = True
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
            If txtUpload.Text <> "" Then
                btnDownload.Visible = True
                If txtUpload.Text.ToUpper.Contains(".JPG") Or txtUpload.Text.ToUpper.Contains(".GIF") Or txtUpload.Text.ToUpper.Contains(".PNG") Then
                    btnView.Visible = True
                Else
                    btnView.Visible = False
                End If
            Else
                btnDownload.Visible = False
                btnView.Visible = False
            End If
            'If Is_dis = "dis_emp" Then
            '    EnableControlAll(False, txtEmployeeCode, btnEmployee)

            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                'cboFamilyType.AutoPostBack = True
                If Request.Params("ID") IsNot Nothing Then
                    hidID.Value = Request.Params("ID")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
                If Request.Params("ID") IsNot Nothing Then
                    FillData(Request.Params("ID"))
                End If
                'If Request.Params("Is_dis") IsNot Nothing Then
                '    Is_dis = Request.Params("Is_dis")
                'End If

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
                'rdStartDate.MaxDate = Date.MaxValue
                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    MainToolBar.Items(0).Enabled = False
                Else
                    MainToolBar.Items(0).Enabled = True

                End If

                If item.WORK_STATUS IsNot Nothing Then
                    hidWorkStatus.Value = item.WORK_STATUS
                End If
                If IsNumeric(item.ID.ToString) Then
                    hidEmployeeID.Value = item.ID.ToString
                End If

                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTITLE.Text = item.TITLE_NAME_VN
                'txtSTAFF_RANK.Text = item.STAFF_RANK_NAME
                txtOrg_Name.Text = item.ORG_NAME

                'ClearControlValue(txtCompanyAddress, txtCompanyName, chkHSV, rdJoinDate, rdEndDate, txtWork, txtTerReason, txtThamNien, txtTileName)

                Dim employeeId As Double = 0
                Double.TryParse(hidEmployeeID.Value, employeeId)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 1
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            Case 2
                If Not FindSigner.Controls.Contains(ctrlFindSigner) Then
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
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
            Case 4
                If Not FindSigner.Controls.Contains(ctrlFindSigner2) Then
                    ctrlFindSigner2 = Me.Register("ctrlFindSigner2", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner2)
                    ctrlFindSigner2.MultiSelect = False
                    ctrlFindSigner2.MustHaveContract = True
                    ctrlFindSigner2.LoadAllOrganization = True
                End If

        End Select
    End Sub

    Private Function CalculateSeniority(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim dSoNam As Double
        Dim dSoThang As Double
        Dim dNgayDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim clsDate As Framework.Data.DateDifference = New Framework.Data.DateDifference(dStart, dEnd)
            dSoNam = clsDate.Years
            dSoThang = clsDate.Months
            dNgayDuThang = clsDate.Days

            If dNgayDuThang < 15 Then
                dSoThang = dSoThang + 0.5
            Else
                dSoThang = dSoThang + 1
                If dSoThang = 12 Then
                    dSoNam = dSoNam + 1
                    dSoThang = 0
                End If
            End If
            Dim str As String

            If dSoNam = 0 And dSoThang = 0 Then
                str = ""
            End If

            If dSoNam = 0 And dSoThang <> 0 Then
                str = dSoThang & " tháng"
            End If

            If dSoNam <> 0 And dSoThang = 0 Then
                str = dSoNam & " năm"
            End If

            If dSoNam <> 0 And dSoThang <> 0 Then
                str = dSoNam & " năm " & dSoThang & " tháng"
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Protected Sub rd_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdJoinDate.SelectedDateChanged, rdEndDate.SelectedDateChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDate.SelectedDate IsNot Nothing Then
                If rdEndDate.SelectedDate IsNot Nothing Then
                    If rdJoinDate.SelectedDate <= rdEndDate.SelectedDate Then
                        'txtThamNien.Text = CalculateSeniority(rdJoinDate.SelectedDate, rdEndDate.SelectedDate)
                        Dim Cal_Day = Math.Round((CDate(rdEndDate.SelectedDate).Subtract(CDate(rdJoinDate.SelectedDate)).TotalDays) + 1, 2)
                        txtThamNien.Text = Math.Round(Cal_Day / 365 * 12, 2)
                        txtThamNien_Detail.Text = If(CInt(CDec(txtThamNien.Text) \ 12) > 0, CInt(CDec(txtThamNien.Text) \ 12).ToString + " Năm ", "") + If(Math.Round(CDec(txtThamNien.Text) Mod 12, 2) > 0, Math.Round(CDec(txtThamNien.Text) Mod 12, 2).ToString + " Tháng", "")
                    Else
                        txtThamNien.Text = vbNullString
                        txtThamNien_Detail.Text = vbNullString
                    End If
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    'Protected Sub RadInput_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtThamNien.TextChanged
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If IsNumeric(txtThamNien.Text) Then
    '            txtThamNien.Text = Math.Round(CDec(txtThamNien.Text), 2)
    '            txtThamNien_Detail.Text = If(CInt(CDec(txtThamNien.Text) \ 12) > 0, CInt(CDec(txtThamNien.Text) \ 12).ToString + " Năm ", "") + If(Math.Round(CDec(txtThamNien.Text) Mod 12, 2) > 0, Math.Round(CDec(txtThamNien.Text) Mod 12, 2).ToString + " Tháng", "")
    '        Else
    '            ShowMessage(Translate("Vui lòng nhập số."), Utilities.NotifyType.Warning)
    '            ClearControlValue(txtThamNien, txtThamNien_Detail)
    '        End If
    '    Catch ex As Exception
    '        ClearControlValue(txtThamNien, txtThamNien_Detail)
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
#End Region

End Class