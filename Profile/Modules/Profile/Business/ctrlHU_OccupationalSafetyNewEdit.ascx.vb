Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_OccupationalSafetyNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' Obj ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj OccupationalSafety
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OccupationalSafety As OccupationalSafetyDTO
        Get
            Return ViewState(Me.ID & "_OccupationalSafetyDTO")
        End Get
        Set(ByVal value As OccupationalSafetyDTO)
            ViewState(Me.ID & "_OccupationalSafetyDTO") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj _Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Id As Decimal
        Get
            Return ViewState(Me.ID & "_Id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Id") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadPopup
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            GetParams()
            Me.MainToolBar = tbarMenu
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            LoadControlPopup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidIDEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEmployeeCode.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN
                txtORG.Text = lstCommonEmployee(0).ORG_NAME
                txtTITLE.Text = lstCommonEmployee(0).TITLE_NAME
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnFindEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadControlPopup()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objdata As OccupationalSafetyDTO
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objdata = New OccupationalSafetyDTO
                        objdata.EMPLOYEE_ID = hidIDEmp.Value
                        objdata.REASON_ID = Utilities.ObjToDecima(cboREASON_ID.SelectedValue)
                        objdata.HOLIDAY_ACCIDENTS = nmHOLIDAY_ACCIDENTS.Value
                        objdata.DESCRIBED_INCIDENT = txtDESCRIBED_INCIDENT.Text
                        objdata.DATE_OF_ACCIDENT = dpDATE_OF_ACCIDENT.SelectedDate
                        objdata.EXTENT_OF_INJURY = txtEXTENT_OF_INJURY.Text
                        objdata.THE_COST_OF_ACCIDENTS = rntxtTHE_COST_OF_ACCIDENTS.Value
                        objdata.SDESC = txtSDESC.Text
                        If CurrentState = CommonMessage.STATE_NEW Then
                            Dim rep As New ProfileBusinessRepository
                            If rep.InsertOccupationalSafety(objdata) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_OccupationalSafety&group=Business")

                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                            rep.Dispose()
                        ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                            Dim rep As New ProfileBusinessRepository
                            objdata.ID = OccupationalSafety.ID
                            Dim lstID As New List(Of Decimal)
                            lstID.Add(objdata.ID)
                            If rep.ValidateBusiness("HU_OCCUPATIONAL_SAFETY", "ID", lstID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                            If rep.ModifyOccupationalSafety(objdata) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_OccupationalSafety&group=Business")

                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                            rep.Dispose()
                        End If

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_OccupationalSafety&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validation ngừng sử dụng cho combobox cboREASON_ID
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusREASON_ID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusREASON_ID.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboREASON_ID.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.OT_RESON.Code
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load các control cho popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadControlPopup()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not phFindSign.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                phFindSign.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.LoadAllOrganization = False
                ctrlFindEmployeePopup.MustHaveContract = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "gUID"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "gUID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Decimal.Parse(Request.Params("gUID"))
                LoadData()
                CurrentState = CommonMessage.STATE_EDIT
            Else
                CurrentState = CommonMessage.STATE_NEW
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_REASON = True
            rep.GetComboList(ListComboData)
            rep.Dispose()
            FillDropDownList(cboREASON_ID, ListComboData.LIST_REASON, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboREASON_ID.SelectedValue)
            cboREASON_ID.SelectedIndex = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try
            OccupationalSafety = rep.GetOccupationalSafetyById(_Id)
            If Not OccupationalSafety Is Nothing Then
                hidIDEmp.Value = OccupationalSafety.EMPLOYEE_ID
                txtEmployeeCode.Text = OccupationalSafety.EMPLOYEE_CODE
                txtEMPLOYEE_NAME.Text = OccupationalSafety.EMPLOYEE_NAME
                txtORG.Text = OccupationalSafety.ORG_NAME
                txtTITLE.Text = OccupationalSafety.TITLE_NAME

                If OccupationalSafety.REASON_ID IsNot Nothing Then
                    cboREASON_ID.SelectedValue = OccupationalSafety.REASON_ID
                End If

                dpDATE_OF_ACCIDENT.SelectedDate = OccupationalSafety.DATE_OF_ACCIDENT
                nmHOLIDAY_ACCIDENTS.Value = OccupationalSafety.HOLIDAY_ACCIDENTS
                txtDESCRIBED_INCIDENT.Text = OccupationalSafety.DESCRIBED_INCIDENT
                txtEXTENT_OF_INJURY.Text = OccupationalSafety.EXTENT_OF_INJURY
                rntxtTHE_COST_OF_ACCIDENTS.Value = OccupationalSafety.THE_COST_OF_ACCIDENTS
                txtSDESC.Text = OccupationalSafety.SDESC

                If OccupationalSafety.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    MainToolBar.Items(0).Enabled = False
                    LeftPane.Enabled = False
                End If

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class