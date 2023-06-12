Imports Common
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_LabourProtectionMngNewEdit
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
    Dim _WorkStatus As String = ""
#Region "Property"

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

#End Region

#Region "Page"
    ''' <summary>
    ''' Get ID tu page ctrlHU_LabourProtectionMng
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            GetParams()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    ''' <summary>
    ''' Khoi tao, Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            LoadControlPopup()
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate></lastupdate>
    ''' <summary>
    ''' Event OK selected Mã nhân viên trên form popup list nhân viên (Giao diện 1)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidIDEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEmployeeCode.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN
                txtORG.Text = lstCommonEmployee(0).ORG_NAME
                txtTITLE.Text = lstCommonEmployee(0).TITLE_NAME
                txtSTAFF_RANK.Text = lstCommonEmployee(0).STAFF_RANK_NAME
                _WorkStatus = lstCommonEmployee(0).WORK_STATUS
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event OK selected Mã nhân viên trên form popup list nhân viên (Giao diện 2)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            LoadControlPopup()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim objdata As LabourProtectionMngDTO
            Dim objList As New List(Of LabourProtectionMngDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objdata = New LabourProtectionMngDTO
                        objdata.LABOURPROTECTION_ID = cboLabourProtect.SelectedValue
                        objdata.LABOUR_SIZE_ID = cboSize.SelectedValue
                        objdata.EMPLOYEE_ID = hidIDEmp.Value
                        objdata.QUANTITY = nmQuantity.Value
                        objdata.DAYS_ALLOCATED = dpDAYS_ALLOCATED.SelectedDate
                        objdata.SDESC = txtSDESC.Text
                        objdata.DEPOSIT = nmDEPOSIT.Value
                        objdata.RETRIEVE_DATE = dpRETRIEVE_DATE.SelectedDate
                        objdata.RETRIEVED = chkRETRIEVED.Checked
                        objdata.RECOVERY_DATE = rdRecovery.SelectedDate
                        objList.Add(objdata)
                        If CurrentState = CommonMessage.STATE_NEW Then
                            Dim rep As New ProfileBusinessRepository
                            If rep.InsertLabourProtectionMng(objList) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                ClearControlValue(cboLabourProtect, cboSize, rnUnitPrice, nmQuantity, nmDEPOSIT, dpDAYS_ALLOCATED, dpRETRIEVE_DATE, chkRETRIEVED, txtSDESC, rdRecovery)
                                Dim str As String = "getRadWindow().close('1');"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                            rep.Dispose()
                        ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                            Dim rep As New ProfileBusinessRepository
                            objdata.ID = hidID.Value
                            Dim lstID As New List(Of Decimal)
                            lstID.Add(hidID.Value)
                            If rep.ValidateBusiness("HU_LABOURPROTECTION_MNG", "ID", lstID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                            If rep.ModifyLabourProtectionMng(objList) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMng&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            End If
                            rep.Dispose()
                        End If

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMng&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboLabourProtect_ItemDataBound(ByVal sender As Object, ByVal e As RadComboBoxItemEventArgs) Handles cboLabourProtect.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dataItem As LabourProtectionDTO = CType(e.Item.DataItem, LabourProtectionDTO)
            If dataItem.UNIT_PRICE IsNot Nothing Then
                e.Item.Attributes("UNIT_PRICE") = dataItem.UNIT_PRICE
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Validate cvalSize ton tai, ap dung
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalSize_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalSize.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboSize.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = ProfileCommon.OT_SIZE.Code 'Type ID = 2041 la gia tri cot TYPE_ID (Ma Danh Muc Size) trong OT_OTHER_LIST
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
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate cvalLabourProtect
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalLabourProtect_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalLabourProtect.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New LabourProtectionDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
                validate.ID = cboLabourProtect.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateLabourProtection(validate)
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
    ''' <summary>
    ''' Load popup list mã nhân viên
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
                'ctrlFindEmployeePopup.MustHaveContract = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' get ID từ form cha
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("gUID") IsNot Nothing Then
                hidID.Value = Request.Params("gUID")
                CurrentState = CommonMessage.STATE_EDIT
                LoadData()
            Else
                CurrentState = CommonMessage.STATE_NEW
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim ListComboData As ComboBoxDataDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LABOURPROTECTION = True
            ListComboData.GET_SIZE_LABOURPROTECTION = True
            rep.GetComboList(ListComboData)
            FillRadCombobox(cboLabourProtect, ListComboData.LIST_LABOURPROTECTION, "NAME", "ID")
            FillDropDownList(cboSize, ListComboData.LIST_SIZE_LABOURPROTECTION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSize.SelectedValue)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data lên các control theo ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try
            Dim item = rep.GetLabourProtectionMngById(hidID.Value)
            If item IsNot Nothing Then
                cboLabourProtect.AutoPostBack = False
                hidIDEmp.Value = item.EMPLOYEE_ID
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEMPLOYEE_NAME.Text = item.EMPLOYEE_NAME
                txtORG.Text = item.ORG_NAME
                txtTITLE.Text = item.TITLE_NAME
                cboLabourProtect.SelectedValue = item.LABOURPROTECTION_ID
                cboSize.SelectedValue = item.LABOUR_SIZE_ID
                nmQuantity.Value = item.QUANTITY
                nmDEPOSIT.Value = item.DEPOSIT
                txtSDESC.Text = item.SDESC
                txtORG.Text = item.ORG_NAME
                rdRecovery.SelectedDate = item.RECOVERY_DATE
                dpDAYS_ALLOCATED.SelectedDate = item.DAYS_ALLOCATED
                dpRETRIEVE_DATE.SelectedDate = item.RETRIEVE_DATE
                rnUnitPrice.Value = item.UNIT_PRICE
                chkRETRIEVED.Checked = item.RETRIEVED
                cboLabourProtect.AutoPostBack = True

                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    MainToolBar.Items(0).Enabled = False
                    LeftPane.Enabled = False
                End If
                rep.Dispose()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class