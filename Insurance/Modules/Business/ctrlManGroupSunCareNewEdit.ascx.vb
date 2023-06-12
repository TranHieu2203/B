Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlManGroupSunCareNewEdit
    Inherits CommonView

    ''' <summary>
    ''' ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrlFindSigner
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance\Modules\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' isLoadPopup
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

    ''' <summary>
    ''' isEdit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    ''' <summary>
    ''' Employee_id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    ''' <summary>
    ''' _Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    ''' <summary>
    ''' RegisterOT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RegisterOT As INS_INFORMATIONDTO
        Get
            Return ViewState(Me.ID & "_objIns_Info")
        End Get
        Set(ByVal value As INS_INFORMATIONDTO)
            ViewState(Me.ID & "_objIns_Info") = value
        End Set
    End Property

    ''' <summary>
    ''' INFOOLDDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property INFOOLDDTO As INS_INFORMATIONDTO
        Get
            Return ViewState(Me.ID & "_INS_INFODTO")
        End Get
        Set(ByVal value As INS_INFORMATIONDTO)
            ViewState(Me.ID & "_INS_INFODTO") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
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
    ''' ValueCostLeverNew
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueCostLeverNew As Decimal
        Get
            Return ViewState(Me.ID & "_ValueCostLeverNew")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueCostLeverNew") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            UpdateControlState()
            If IsPostBack Then
                ExcuteScript("OnFocus", "fnRegisterOnfocusOut()")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New INS_GROUP_SUN_CAREDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetGroup_SunCareById(obj.ID)
                    INFOOLDDTO = New INS_INFORMATIONDTO

                    If obj IsNot Nothing Then
                        hidEmpID.Value = obj.EMPLOYEE_ID
                        hidOrgID.Value = obj.ORG_ID
                        hidID.Value = obj.ID
                        txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                        txtName.Text = obj.EMPLOYEE_NAME
                        txtDonVi.Text = obj.ORG_NAME

                        If obj.TITLE_ID_OLD <> 0 Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("HU_TITLE", "ID", obj.TITLE_ID_OLD) Then
                                cboTitleOld.SelectedValue = obj.TITLE_ID_OLD
                            End If
                        End If

                        If obj.TITLE_ID_NEW <> 0 Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("HU_TITLE", "ID", obj.TITLE_ID_NEW) Then
                                cboTitleNew.SelectedValue = obj.TITLE_ID_NEW
                            End If
                        End If

                        If obj.STAFF_RANK_ID_OLD <> 0 Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("HU_STAFF_RANK", "ID", obj.STAFF_RANK_ID_OLD) Then
                                cboStaffRankOld.SelectedValue = obj.STAFF_RANK_ID_OLD
                            End If
                        End If

                        If obj.STAFF_RANK_ID_NEW <> 0 Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("HU_STAFF_RANK", "ID", obj.STAFF_RANK_ID_NEW) Then
                                cboStaffRankNew.SelectedValue = obj.STAFF_RANK_ID_NEW
                            End If
                        End If

                        If obj.COST_LEVER_ID_OLD <> 0 Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("INS_COST_FOLLOW_LEVER", "ID", obj.COST_LEVER_ID_OLD) Then
                                cboCostLeverOld.SelectedValue = obj.COST_LEVER_ID_OLD
                            End If
                        End If

                        If obj.COST_LEVER_ID_NEW IsNot Nothing Then
                            Dim repCom As New CommonRepository
                            If Not repCom.ValidateComboboxActive("INS_COST_FOLLOW_LEVER", "ID", obj.COST_LEVER_ID_OLD) Then
                                cboCostLeverNew.SelectedValue = obj.COST_LEVER_ID_NEW
                            End If
                        End If

                        nmMucCP.Value = Utilities.ObjToInt(obj.COST_MONEY)
                        rdEffectDate.SelectedDate = obj.EFFECTDATE_COST_NEW
                        txtNote.Text = obj.NOTE
                        INFOOLDDTO.ORG_ID = obj.ORG_ID
                        _Value = obj.ID
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve nhân viên khi click button [hủy] o popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Show popup chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.IsHideTerminate = True
                    ctrlFindEmployeePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As INS_GROUP_SUN_CAREDTO
        Dim rep As New InsuranceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If _Value.HasValue Then

                            Dim cmRep As New CommonRepository
                            Dim lstID As New List(Of Decimal)

                            lstID.Add(Convert.ToDecimal(_Value))

                            If cmRep.CheckExistIDTable(lstID, "INS_GROUP_SUN_CARE", "ID") Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                Exit Sub
                            End If

                            obj = New INS_GROUP_SUN_CAREDTO
                            obj.ID = hidID.Value
                            obj.EMPLOYEE_ID = hidEmpID.Value
                            obj.TITLE_ID_OLD = cboTitleOld.SelectedValue
                            obj.TITLE_ID_NEW = cboTitleNew.SelectedValue
                            obj.STAFF_RANK_ID_OLD = cboStaffRankOld.SelectedValue
                            obj.STAFF_RANK_ID_NEW = cboStaffRankNew.SelectedValue
                            obj.COST_LEVER_ID_OLD = cboCostLeverOld.SelectedValue

                            If cboCostLeverNew.SelectedValue <> "" Then
                                obj.COST_LEVER_ID_NEW = cboCostLeverNew.SelectedValue
                            End If

                            obj.EFFECTDATE_COST_NEW = rdEffectDate.SelectedDate
                            obj.COST_MONEY = nmMucCP.Value
                            obj.NOTE = txtNote.Text.Trim
                            rep.ModifyGroup_SunCare(obj, gstatus)
                        Else
                            obj = New INS_GROUP_SUN_CAREDTO
                            obj.EMPLOYEE_ID = hidEmpID.Value
                            obj.TITLE_ID_OLD = cboTitleOld.SelectedValue
                            obj.TITLE_ID_NEW = cboTitleNew.SelectedValue
                            obj.STAFF_RANK_ID_OLD = cboStaffRankOld.SelectedValue
                            obj.STAFF_RANK_ID_NEW = cboStaffRankNew.SelectedValue
                            obj.COST_LEVER_ID_OLD = cboCostLeverOld.SelectedValue

                            If cboCostLeverNew.SelectedValue <> "" Then
                                obj.COST_LEVER_ID_NEW = ValueCostLeverNew
                            End If

                            obj.EFFECTDATE_COST_NEW = rdEffectDate.SelectedDate
                            obj.COST_MONEY = nmMucCP.Value
                            obj.NOTE = txtNote.Text.Trim
                            rep.InsertGroup_SunCare(obj, gstatus)
                        End If
                        'POPUPTOLINK
                        Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlManGroupSunCare&group=Business")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlManGroupSunCare&group=Business")
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event Load thông tin nhân viên đã được chọn ở popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim emp As New DataTable

        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            emp = rep.GetEmployeeByIdProcess(empID)

            If emp IsNot Nothing Then
                If emp.Rows.Count = 2 Then ' nếu có 2 dòng thì nv đó có bản cũ và bản ghi mới
                    hidEmpID.Value = Utilities.ObjToInt(emp.Rows(0)("EMPLOYEE_ID"))
                    hidOrgID.Value = Utilities.ObjToInt(emp.Rows(0)("ORG_ID"))
                    txtEmployeeCode.Text = emp.Rows(0)("EMPLOYEE_CODE")
                    txtName.Text = emp.Rows(0)("FULLNAME_VN")
                    txtDonVi.Text = emp.Rows(0)("ORG_NAME")
                    cboTitleOld.SelectedValue = Utilities.ObjToInt(emp.Rows(1)("TITLE_ID"))
                    cboTitleNew.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("TITLE_ID"))
                    cboStaffRankOld.SelectedValue = Utilities.ObjToInt(emp.Rows(1)("STAFF_RANK_ID"))
                    cboStaffRankNew.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("STAFF_RANK_ID"))
                    cboCostLeverOld.SelectedValue = Utilities.ObjToInt(emp.Rows(1)("COST_LEVEL_ID_OLD"))

                ElseIf emp.Rows.Count = 1 Then ' nếu có 1 dòng thì nv đó chỉ có bản ghi mới
                    hidEmpID.Value = Utilities.ObjToInt(emp.Rows(0)("EMPLOYEE_ID"))
                    hidOrgID.Value = Utilities.ObjToInt(emp.Rows(0)("ORG_ID"))
                    txtEmployeeCode.Text = emp.Rows(0)("EMPLOYEE_CODE")
                    txtName.Text = emp.Rows(0)("FULLNAME_VN")
                    txtDonVi.Text = emp.Rows(0)("ORG_NAME")
                    cboTitleOld.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("TITLE_ID"))
                    cboTitleNew.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("TITLE_ID"))
                    cboStaffRankOld.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("STAFF_RANK_ID"))
                    cboStaffRankNew.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("STAFF_RANK_ID"))
                    cboCostLeverOld.SelectedValue = Utilities.ObjToInt(emp.Rows(0)("COST_LEVEL_ID_OLD"))

                Else ' nếu không có dòng nào thì nv chưa được khai báo trong quản lý BH sun care
                    ShowMessage("Nhân viên chưa được khai báo trong quản lý BH Sun Care!", NotifyType.Warning)
                    Exit Sub
                End If

                ClearControlValue(cboCostLeverNew, txtNote, rdEffectDate)
            Else
                ShowMessage("Nhân viên chưa được khai báo trong quản lý BH Sun Care!", NotifyType.Warning)
                Exit Sub
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Event thay đổi giá trị của combobox NHÓM BẢO HIỂM MỚI
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboCostLeverNew_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCostLeverNew.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If cboCostLeverNew.SelectedValue <> "" Then
                Dim dtCost As New INS_COST_FOLLOW_LEVERDTO
                dtCost = rep.GetIns_Cost_LeverByID(Utilities.ObjToInt(cboCostLeverNew.SelectedValue))
                If dtCost IsNot Nothing Then
                    nmMucCP.Value = dtCost.COST_MONEY
                End If
            Else
                nmMucCP.Value = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox NHÓM BẢO HIỂM MỚI có tồn tại hoặc bị ngừng áp dụng hay không? 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCostLeverNew_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCostLeverNew.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            ValueCostLeverNew = cboCostLeverNew.SelectedValue
            ListComboData = New ComboBoxDataDTO
            Dim dto As New INS_COST_FOLLOW_LEVERDTO
            Dim list As New List(Of INS_COST_FOLLOW_LEVERDTO)

            dto.ID = Convert.ToDecimal(cboCostLeverNew.SelectedValue)
            list.Add(dto)

            ListComboData.GET_LIST_COST_LEVER = True
            ListComboData.LIST_LIST_COST_LEVER = list

            If rep.ValidateCombobox(ListComboData) Then
                args.IsValid = True
            Else
                args.IsValid = False
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cboCostLeverNew, ListComboData.LIST_LIST_COST_LEVER, "COST_NAME", "ID", True)
                ClearControlValue(cboCostLeverNew)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Get data và bind vao combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New InsuranceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_COST_LEVER = True
                ListComboData.GET_LIST_TITLE = True
                ListComboData.GET_LIST_STAFF_RANK = True
                rep.GetComboboxData(ListComboData)
            End If

            FillRadCombobox(cboCostLeverNew, ListComboData.LIST_LIST_COST_LEVER, "COST_NAME", "ID", True)
            If ListComboData.LIST_LIST_COST_LEVER.Count > 0 Then
                cboCostLeverNew.SelectedIndex = 0
            End If

            FillRadCombobox(cboCostLeverOld, ListComboData.LIST_LIST_COST_LEVER, "COST_NAME", "ID", True)
            If ListComboData.LIST_LIST_COST_LEVER.Count > 0 Then
                cboCostLeverOld.SelectedIndex = 0
            End If

            FillRadCombobox(cboStaffRankOld, ListComboData.LIST_LIST_STAFF_RANK, "NAME", "ID", True)
            If ListComboData.LIST_LIST_STAFF_RANK.Count > 0 Then
                cboStaffRankOld.SelectedIndex = 0
            End If

            FillRadCombobox(cboStaffRankNew, ListComboData.LIST_LIST_STAFF_RANK, "NAME", "ID", True)
            If ListComboData.LIST_LIST_STAFF_RANK.Count > 0 Then
                cboStaffRankNew.SelectedIndex = 0
            End If

            FillRadCombobox(cboTitleOld, ListComboData.LIST_LIST_TITLE, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_TITLE.Count > 0 Then
                cboTitleOld.SelectedIndex = 0
            End If

            FillRadCombobox(cboTitleNew, ListComboData.LIST_LIST_TITLE, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_TITLE.Count > 0 Then
                cboTitleNew.SelectedIndex = 0
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class

