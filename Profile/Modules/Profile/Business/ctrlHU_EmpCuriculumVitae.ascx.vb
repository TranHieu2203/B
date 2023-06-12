Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_EmpCuriculumVitae
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
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
            If Not IsPostBack Then
                btnEdit.Enabled = False
            End If
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
            rgChangeInfo.AllowCustomPaging = True
            rgChangeInfo.SetFilter()
            rgContract.AllowCustomPaging = True
            rgContract.SetFilter()
            rgContractAppendix.AllowCustomPaging = True
            rgContractAppendix.SetFilter()
            rgWorkingSal.AllowCustomPaging = True
            rgWorkingSal.SetFilter()
            rgAllowance.AllowCustomPaging = True
            rgAllowance.SetFilter()
            rgCon.AllowCustomPaging = True
            rgCon.SetFilter()
            rgCommend.AllowCustomPaging = True
            rgCommend.SetFilter()
            rgDiscipline.AllowCustomPaging = True
            rgDiscipline.SetFilter()
            rgTraining.AllowCustomPaging = True
            rgTraining.SetFilter()
            rgWorkInfo.AllowCustomPaging = True
            rgWorkInfo.SetFilter()
            rgDocument.AllowCustomPaging = True
            rgDocument.SetFilter()
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
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                    If Not IsPostBack Then
                        FillData(hidID.Value)
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    If Not IsPostBack Then
                        rbiEmployeeImage.DataValue = rep.GetEmployeeImage(0, "") 'Lấy ảnh mặc định (NoImage.jpg)
                    End If
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
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
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
            'ctrlFindEmployeePopup.EMP_CODE_OR_NAME = Nothingx
            If Hid_IsEnter.Value.ToUpper = "ISENTER" Then
                Hid_IsEnter.Value = Nothing
                If txtEmployeeCode.Text <> "" Then
                    Dim Count = 0
                    Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                    Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                    EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                    Reset_Find_Emp()
                    If Count <= 0 Then
                        ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                        txtEmployeeCode.Text = ""
                    ElseIf Count = 1 Then
                        Dim empID = EmployeeList(0)
                        FillData(empID.EMPLOYEE_ID)
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
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(hidID, txtEmployeeCode, txtEmployeeName, txtEmployeeCodeOld, txtOrg_Name, txtTITLE, txtEmployeeType, rdJoinDate, rdJoindateState, txtContractType, rdBirthDate, txtGender, txtMarriage,
                            txtNational, txtBirthPlace, txtProvinceNQ, txtNative, txtReligion, txtIDNo, rdIDDate, rdExpireIDNO, txtIDPlace, txtNhomMau, txtChieuCao, txtCanNang, txtBookNo, txtPITNo, rdEffectDate, txtPITPlace, txtNoiKhamChuaBenh,
                            txtPassNo, rdPassDate, rdPassExpireDate, txtNoiCapHoChieu, txtVisa, rdVisaDate, rdVisaExpireDate, txtNoiCapVisa, txtSoSoLaoDong, rdNgayCapSSLD, rdNgayHetHanSSLD, txtNoiCapSSLD, txtPerAddress, txtMobilePhone,
                            txtPerProvince, txtPerDistrict, txtPerWard, txtHomePhone, txtNavAddress, txtPerEmail, txtNavProvince, txtNavDistrict, txtNavWard, txtWorkEmail, txtContactPerson, txtRelationNLH, txtContactPerIDNo, txtContactPerMobilePhone, txtAddressPerContact, txtContactPerPhone)
            btnEdit.Enabled = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New ContractDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsNumeric(hidID.Value) Then
                rgContract.VirtualItemCount = 0
                rgContract.DataSource = New List(Of ContractDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}

            _filter.EMPLOYEE_ID = hidID.Value
            SetValueObjectByRadGrid(rgContract, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContract.MasterTableView.SortExpressions.GetSortString()
            Dim lstContract As New List(Of ContractDTO)
            If Sorts IsNot Nothing Then
                lstContract = rep.GetContract(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param, Sorts)
            Else
                lstContract = rep.GetContract(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param)
            End If

            rgContract.VirtualItemCount = MaximumRows
            rgContract.DataSource = lstContract
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgChangeInfo_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgChangeInfo.NeedDataSource
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New WorkingDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsNumeric(hidID.Value) Then
                rgChangeInfo.VirtualItemCount = 0
                rgChangeInfo.DataSource = New List(Of WorkingDTO)
                Exit Sub
            End If
            _filter.EMPLOYEE_ID = hidID.Value
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContract.MasterTableView.SortExpressions.GetSortString()

            SetValueObjectByRadGrid(rgChangeInfo, _filter)

            If Sorts IsNot Nothing Then
                rgChangeInfo.DataSource = rep.GetWorking(_filter, rgChangeInfo.CurrentPageIndex, rgChangeInfo.PageSize, MaximumRows, _param, Sorts)
            Else
                rgChangeInfo.DataSource = rep.GetWorking(_filter, rgChangeInfo.CurrentPageIndex, rgChangeInfo.PageSize, MaximumRows, _param)
            End If

            rgChangeInfo.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgContractAppendix_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContractAppendix.NeedDataSource
        Dim rep As New ProfileRepository
        Dim _filter As New FileContractDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgContractAppendix.VirtualItemCount = 0
                rgContractAppendix.DataSource = New List(Of FileContractDTO)
                Exit Sub
            End If
            _filter.EMPLOYEE_ID = hidID.Value
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                          .IS_DISSOLVE = False}
            SetValueObjectByRadGrid(rgContract, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContractAppendix.MasterTableView.SortExpressions.GetSortString()
            Dim Contracts = rep.GetContractAppendixPaging(_filter, rgContractAppendix.CurrentPageIndex, rgContractAppendix.PageSize, MaximumRows, _param)
            If Contracts IsNot Nothing And Contracts.Count <> 0 Then
                rgContractAppendix.DataSource = Contracts
            Else
                rgContractAppendix.DataSource = New List(Of FileContractDTO)
            End If
            rep.Dispose()
            rgContract.VirtualItemCount = MaximumRows
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub rgWorkingSal_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorkingSal.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New WorkingDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgWorkingSal.VirtualItemCount = 0
                rgWorkingSal.DataSource = New List(Of WorkingDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}

            SetValueObjectByRadGrid(rgWorkingSal, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorkingSal.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgWorkingSal.DataSource = rep.GetWorking(_filter, rgWorkingSal.CurrentPageIndex, rgWorkingSal.PageSize, MaximumRows, _param, Sorts)
            Else
                rgWorkingSal.DataSource = rep.GetWorking(_filter, rgWorkingSal.CurrentPageIndex, rgWorkingSal.PageSize, MaximumRows, _param)
            End If

            rgWorkingSal.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgAllowance_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAllowance.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New HUAllowanceDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgAllowance.VirtualItemCount = 0
                rgAllowance.DataSource = New List(Of HUAllowanceDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}

            SetValueObjectByRadGrid(rgAllowance, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgAllowance.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgAllowance.DataSource = rep.GetWorkingAllowance1(_filter, _param, rgAllowance.CurrentPageIndex, rgAllowance.PageSize, MaximumRows, Sorts)
            Else
                rgAllowance.DataSource = rep.GetWorkingAllowance1(_filter, _param, rgAllowance.CurrentPageIndex, rgAllowance.PageSize, MaximumRows)
            End If

            rgAllowance.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgCon_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCon.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New Temp_ConcurrentlyDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgCon.VirtualItemCount = 0
                rgCon.DataSource = New List(Of Temp_ConcurrentlyDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgCon, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgCon.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgCon.DataSource = rep.GET_LIST_CONCURRENTLY(_filter, rgCon.CurrentPageIndex, rgCon.PageSize, MaximumRows, Sorts)
            Else
                rgCon.DataSource = rep.GET_LIST_CONCURRENTLY(_filter, rgCon.CurrentPageIndex, rgCon.PageSize, MaximumRows)
            End If

            rgCon.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgCommend_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCommend.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New CommendDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgCommend.VirtualItemCount = 0
                rgCommend.DataSource = New List(Of CommendDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgCommend, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            _filter.param = New ParamDTO
            _filter.param.ORG_ID = 1
            _filter.param.IS_DISSOLVE = False
            Dim MaximumRows As Integer
            Dim Sorts As String = rgCommend.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgCommend.DataSource = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows, Sorts)
            Else
                rgCommend.DataSource = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows)
            End If

            rgCommend.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgDiscipline_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDiscipline.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New DisciplineDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgDiscipline.VirtualItemCount = 0
                rgDiscipline.DataSource = New List(Of DisciplineDTO)
                Exit Sub
            End If

            SetValueObjectByRadGrid(rgDiscipline, _filter)

            _filter.param = New ParamDTO
            _filter.param.ORG_ID = 1
            _filter.param.IS_DISSOLVE = False
            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgDiscipline.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgDiscipline.DataSource = rep.GetDiscipline(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows, Sorts)
            Else
                rgDiscipline.DataSource = rep.GetDiscipline(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows)
            End If

            rgDiscipline.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub rgTraining_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTraining.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New HU_PRO_TRAIN_OUT_COMPANYDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgTraining.VirtualItemCount = 0
                rgTraining.DataSource = New List(Of DisciplineDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                          .IS_DISSOLVE = False}
            SetValueObjectByRadGrid(rgTraining, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgTraining.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgTraining.DataSource = rep.GetCertificates(_filter, _param, rgTraining.CurrentPageIndex, rgTraining.PageSize, MaximumRows, Sorts)
            Else
                rgTraining.DataSource = rep.GetCertificates(_filter, _param, rgTraining.CurrentPageIndex, rgTraining.PageSize, MaximumRows)
            End If

            rgTraining.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgFamily_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New FamilyDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsNumeric(hidID.Value) Then
                rgFamily.VirtualItemCount = 0
                rgFamily.DataSource = New List(Of FamilyDTO)
                Exit Sub
            End If

            _filter.EMPLOYEE_ID = hidID.Value
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                            .IS_DISSOLVE = False}


            SetValueObjectByRadGrid(rgFamily, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgFamily.MasterTableView.SortExpressions.GetSortString()
            Dim lstFamily As List(Of FamilyDTO)

            If Sorts IsNot Nothing Then
                lstFamily = rep.GetEmployeeFamily_1(_filter, rgFamily.CurrentPageIndex, rgFamily.PageSize, MaximumRows, _param, Sorts)
            Else
                lstFamily = rep.GetEmployeeFamily_1(_filter, rgFamily.CurrentPageIndex, rgFamily.PageSize, MaximumRows, _param)
            End If

            rgFamily.VirtualItemCount = MaximumRows
            rgFamily.DataSource = lstFamily

            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgWorkInfo_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorkInfo.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New WorkingBeforeDTO
        Try
            If Not IsNumeric(hidID.Value) Then
                rgWorkInfo.VirtualItemCount = 0
                rgWorkInfo.DataSource = New List(Of DisciplineDTO)
                Exit Sub
            End If
            Dim _param = New ParamDTO With {.ORG_ID = 1,
                                          .IS_DISSOLVE = False}
            SetValueObjectByRadGrid(rgWorkInfo, _filter)

            _filter.EMPLOYEE_ID = hidID.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorkInfo.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                rgWorkInfo.DataSource = rep.GetListWorkingBefore(_filter, rgWorkInfo.CurrentPageIndex, rgWorkInfo.PageSize, MaximumRows, _param, Sorts)
            Else
                rgWorkInfo.DataSource = rep.GetListWorkingBefore(_filter, rgWorkInfo.CurrentPageIndex, rgWorkInfo.PageSize, MaximumRows, _param)
            End If

            rgWorkInfo.VirtualItemCount = MaximumRows
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub rgDocument_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDocument.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim repst = New ProfileStoreProcedure
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsNumeric(hidID.Value) Then
                rgDocument.VirtualItemCount = 0
                rgDocument.DataSource = New DataTable
                Exit Sub
            End If

            Dim dtData = repst.Get_List_Document(hidID.Value)

            rgDocument.DataSource = dtData
            rgDocument.VirtualItemCount = dtData.Rows.Count
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
            End Select
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
    ''' Phương thức xử lý việc lấy về parameter "IDSelect"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "EmpID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If Request.Params("emp") IsNot Nothing Then
                hidID.Value = Request.Params("emp")
                Refresh("UpdateView")
                Exit Sub
            End If
            Refresh("NormalView")
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
        Dim store As New ProfileStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empCv As New EmployeeCVDTO
            Dim empHealth As New EmployeeHealthDTO
            Using rep As New ProfileBusinessRepository
                Dim objEmp = rep.GetEmployeeCuriculumVitae(empID, empCv, empHealth)
                Reset_Find_Emp()
                If objEmp IsNot Nothing Then
                    hidID.Value = objEmp.ID
                    txtEmployeeCode.Text = objEmp.EMPLOYEE_CODE
                    txtEmployeeName.Text = objEmp.FULLNAME_VN
                    txtEmployeeCodeOld.Text = objEmp.EMPLOYEE_CODE_OLD
                    txtOrg_Name.Text = objEmp.ORG_NAME
                    txtTITLE.Text = objEmp.TITLE_NAME_VN
                    txtEmployeeType.Text = objEmp.EMP_STATUS_NAME
                    If objEmp.JOIN_DATE.HasValue Then
                        rdJoinDate.SelectedDate = objEmp.JOIN_DATE
                    End If
                    If objEmp.JOIN_DATE_STATE.HasValue Then
                        rdJoindateState.SelectedDate = objEmp.JOIN_DATE_STATE
                    End If
                    txtContractType.Text = objEmp.CONTRACT_TYPE_NAME

                    ''CV
                    If empCv IsNot Nothing Then
                        If empCv.BIRTH_DATE.HasValue Then
                            rdBirthDate.SelectedDate = empCv.BIRTH_DATE
                        End If
                        txtGender.Text = empCv.GENDER_NAME
                        txtMarriage.Text = empCv.MARITAL_STATUS_NAME
                        txtNational.Text = empCv.NATIONALITY_NAME
                        txtBirthPlace.Text = empCv.BIRTH_PLACENAME
                        txtProvinceNQ.Text = empCv.PROVINCENQ_NAME
                        txtNative.Text = empCv.NATIVE_NAME
                        txtReligion.Text = empCv.RELIGION_NAME
                        txtIDNo.Text = empCv.ID_NO
                        If empCv.ID_DATE.HasValue Then
                            rdIDDate.SelectedDate = empCv.ID_DATE
                        End If
                        If empCv.EXPIRE_DATE_IDNO.HasValue Then
                            rdExpireIDNO.SelectedDate = empCv.EXPIRE_DATE_IDNO
                        End If

                        txtIDPlace.Text = empCv.PLACE_NAME
                        If empHealth IsNot Nothing Then
                            txtNhomMau.Text = empHealth.NHOM_MAU
                            txtChieuCao.Text = empHealth.CHIEU_CAO
                            txtCanNang.Text = empHealth.CAN_NANG
                        End If
                        txtBookNo.Text = objEmp.BOOK_NO_SOCIAL
                        txtPITNo.Text = empCv.PIT_CODE
                        If empCv.PIT_CODE_DATE.HasValue Then
                            rdEffectDate.SelectedDate = empCv.PIT_CODE_DATE
                        End If
                        txtPITPlace.Text = empCv.PIT_CODE_PLACE
                        txtNoiKhamChuaBenh.Text = empCv.HEALTH_AREA_INS_NAME
                        txtPassNo.Text = empCv.PASS_NO
                        If empCv.PASS_DATE.HasValue Then
                            rdPassDate.SelectedDate = empCv.PASS_DATE
                        End If
                        If empCv.PASS_EXPIRE.HasValue Then
                            rdPassExpireDate.SelectedDate = empCv.PASS_EXPIRE
                        End If
                        txtNoiCapHoChieu.Text = empCv.PASS_PLACE

                        txtVisa.Text = empCv.VISA
                        If empCv.VISA_DATE.HasValue Then
                            rdVisaDate.SelectedDate = empCv.VISA_DATE
                        End If
                        If empCv.VISA_EXPIRE.HasValue Then
                            rdVisaExpireDate.SelectedDate = empCv.VISA_EXPIRE
                        End If
                        txtNoiCapVisa.Text = empCv.VISA_PLACE

                        txtSoSoLaoDong.Text = empCv.BOOK_NO
                        If empCv.BOOK_DATE.HasValue Then
                            rdNgayCapSSLD.SelectedDate = empCv.BOOK_DATE
                        End If
                        If empCv.BOOK_EXPIRE.HasValue Then
                            rdNgayHetHanSSLD.SelectedDate = empCv.BOOK_EXPIRE
                        End If
                        txtNoiCapSSLD.Text = empCv.SSLD_PLACE_NAME

                        txtPerAddress.Text = empCv.PER_ADDRESS
                        txtMobilePhone.Text = empCv.MOBILE_PHONE
                        txtPerProvince.Text = empCv.PER_PROVINCE_NAME
                        txtPerDistrict.Text = empCv.PER_DISTRICT_NAME
                        txtPerWard.Text = empCv.PER_WARD_NAME
                        txtHomePhone.Text = empCv.HOME_PHONE
                        txtNavAddress.Text = empCv.NAV_ADDRESS
                        txtPerEmail.Text = empCv.PER_EMAIL
                        txtNavProvince.Text = empCv.NAV_PROVINCE_NAME
                        txtNavDistrict.Text = empCv.NAV_DISTRICT_NAME
                        txtNavWard.Text = empCv.NAV_WARD_NAME
                        txtWorkEmail.Text = empCv.WORK_EMAIL

                        txtContactPerson.Text = empCv.CONTACT_PER
                        txtRelationNLH.Text = empCv.RELATION_PER_CTR_NAME
                        txtContactPerIDNo.Text = empCv.CONTACT_PER_IDNO
                        txtContactPerMobilePhone.Text = empCv.CONTACT_PER_MBPHONE
                        txtAddressPerContact.Text = empCv.ADDRESS_PER_CTR
                        txtContactPerPhone.Text = empCv.CONTACT_PER_PHONE
                    End If

                    Dim sError As String = ""
                    If objEmp.IMAGE IsNot Nothing Then
                        rbiEmployeeImage.DataValue = rep.GetEmployeeImage(objEmp.ID, sError) 'Lấy ảnh của nhân viên.
                    Else
                        rbiEmployeeImage.DataValue = rep.GetEmployeeImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
                    End If
                    rbiEmployeeImage.Visible = True
                    btnEdit.Enabled = True
                Else
                    rbiEmployeeImage.DataValue = rep.GetEmployeeImage(0, "") 'Lấy ảnh mặc định (NoImage.jpg)
                End If

            End Using

            rgChangeInfo.Rebind()
            rgContract.Rebind()
            rgContractAppendix.Rebind()
            rgWorkingSal.Rebind()
            rgAllowance.Rebind()
            rgCon.Rebind()
            rgCommend.Rebind()
            rgDiscipline.Rebind()
            rgTraining.Rebind()
            rgWorkInfo.Rebind()
            rgDocument.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class