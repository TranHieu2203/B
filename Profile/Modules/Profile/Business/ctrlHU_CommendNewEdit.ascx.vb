Imports System.IO
Imports Common
Imports Common.CommonBusiness
'Imports Ionic.Zip
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_CommendNewEdit
    Inherits CommonView
    Dim log As New UserLog

    ''' <summary>
    ''' ctrl FindOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrl FindEmployeeImportPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmpImportPopup As ctrlFindEmployeeImportPopup

    ''' <summary>
    ''' ctrl FindOrgImportPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindOrgImportPopup As ctrlFindOrgImportPopup

    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' Must Authorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = True

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property

    ''' <summary>
    ''' Profile StoreProcedure
    ''' </summary>
    ''' <remarks></remarks>
    Dim psp As New ProfileStoreProcedure

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Commend
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Commend As CommendDTO
        Get
            Return ViewState(Me.ID & "_Commend")
        End Get
        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_Commend") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Org
    ''' 3 - Sign
    ''' </returns>
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
    ''' Employee_Commend
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_Commend As List(Of CommendEmpDTO)
        Get
            Return ViewState(Me.ID & "_Employee_Commend")
        End Get
        Set(value As List(Of CommendEmpDTO))
            ViewState(Me.ID & "_Employee_Commend") = value
        End Set
    End Property

    ''' <summary>
    ''' List_Org
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property List_Org As List(Of CommendOrgDTO)
        Get
            Return ViewState(Me.ID & "_List_Org")
        End Get
        Set(value As List(Of CommendOrgDTO))
            ViewState(Me.ID & "_List_Org") = value
        End Set
    End Property

    Public Property bsSource As DataTable
        Get
            Return PageViewState(Me.ID & "_bsSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_bsSource") = value
        End Set
    End Property

    ''' <summary>
    ''' FormType
    ''' </summary>
    ''' <remarks>
    ''' 0 - NEW
    ''' 1 - EDIT
    ''' </remarks>
    Dim FormType As Integer

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Dim IDSelect As Decimal?

#End Region

#Region "Page"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetParams()
            Refresh()
            UpdateControlState()
            If CType(CommonConfig.dicConfig("APP_SETTING_11"), Boolean) Then
                lbSignerName1.Visible = False
                txtSignerName.Display = False
                btnFindSinger.Visible = False
                lbSignerTitle.Visible = False
                txtSignerTitle.Visible = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
                Select Case FormType
                    Case 0
                        Me.ViewDescription = Translate("Tạo mới khen thưởng")
                    Case 1
                        Me.ViewDescription = Translate("Sửa khen thưởng")
                End Select
            End If

            Me.MainToolBar = tbarCommend

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)

            'Me.btnDownload.OnClientClicked = "OnClientButtonClicked"
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Not IsPostBack Then
                ViewConfig(RightPane)
                'GirdConfig(rgEmployee)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case Message
                Case "UpdateView"
                    Commend = rep.GetCommendByID(New CommendDTO With {.ID = IDSelect})
                    CurrentState = CommonMessage.STATE_EDIT
                    txtDecisionNo.Text = Commend.NO
                    txtRemark.Text = Commend.NOTE
                    txtSignerName.Text = Commend.SIGNER_NAME
                    txtSignerTitle.Text = Commend.SIGNER_TITLE
                    rdSignDate.SelectedDate = Commend.SIGN_DATE

                    If IsNumeric(Commend.STATUS_ID) Then
                        cboStatus.SelectedValue = Commend.STATUS_ID
                        If Commend.STATUS_ID = 714 Then
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If
                    End If

                    cboCommendLevel.SelectedValue = Commend.COMMEND_LEVEL
                    'txtDecisionName.Text = Commend.NAME
                    'rdExpireDate.SelectedDate = Commend.EXPIRE_DATE
                    'rdIssueDate.SelectedDate = Commend.ISSUE_DATE

                    'If Commend.COMMEND_LEVEL IsNot Nothing Then
                    '    cboCommendLevel.SelectedValue = Commend.COMMEND_LEVEL
                    'End If

                    cboCommendObj.SelectedValue = Commend.COMMEND_OBJ.ToString
                    cboCommendObj.Enabled = False

                    VisibleGridview(cboCommendObj.SelectedIndex)

                    If Commend.COMMEND_TYPE IsNot Nothing Then
                        cboCommendType.SelectedValue = Commend.COMMEND_TYPE.ToString
                    End If

                    If Commend.YEAR IsNot Nothing Then
                        cboYear.SelectedValue = Commend.YEAR.ToString
                    End If

                    rntxtMoney.Value = Commend.MONEY
                    hidDecisionID.Value = Commend.ID.ToString
                    hidID.Value = Commend.ID.ToString

                    If Commend.SIGN_ID IsNot Nothing Then
                        hidSignID.Value = Commend.SIGN_ID
                    End If

                    If Commend.COMMEND_TITLE_ID IsNot Nothing Then
                        cboCommend_Title.SelectedValue = Commend.COMMEND_TITLE_ID
                    End If

                    txtCommend_Detail.Text = Commend.COMMEND_DETAIL

                    If Commend.COMMEND_PAY IsNot Nothing Then
                        cboCommendPay.SelectedValue = Commend.COMMEND_PAY
                    End If

                    'If Commend.COMMEND_LIST_ID IsNot Nothing Then
                    '    cboCommendList.SelectedValue = Commend.COMMEND_LIST_ID
                    'End If

                    'If Commend.POWER_PAY_ID Then
                    '    cboPowerPay.SelectedValue = Commend.POWER_PAY_ID
                    'End If

                    If Commend.IS_TAX IsNot Nothing Then
                        chkTAX.Checked = Commend.IS_TAX
                    End If

                    rdEffectDate.SelectedDate = Commend.EFFECT_DATE
                    rdEffectDate_SelectedDateChanged(Nothing, Nothing)

                    'If Commend.EXPIRE_DATE IsNot Nothing Then
                    '    rdExpireDate.SelectedDate = Commend.EXPIRE_DATE
                    'End If

                    If Commend.PERIOD_ID IsNot Nothing Then
                        cboPeriod.SelectedValue = Commend.PERIOD_ID
                    End If

                    If Commend.PERIOD_TAX IsNot Nothing Then
                        cboPeriodTax.SelectedValue = Commend.PERIOD_TAX
                    End If

                    txtCommend_Detail.Text = Commend.REMARK

                    'If Commend.FORM_ID IsNot Nothing Then
                    '    cboForm.SelectedValue = Commend.FORM_ID
                    'End If
                    'txtYear.Text = Commend.YEAR.ToString()
                    txtUploadFile.Text = Commend.FILENAME
                    txtRemindLink.Text = If(Commend.UPLOADFILE Is Nothing, "", Commend.UPLOADFILE)
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)

                    If Commend.STATUS_ID = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                        'RightPane.Enabled = False
                        'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True
                    End If

                    'dien dữ lieu vao lưới ( nhan vien hoac phòng ban )
                    FilldataGridView(cboCommendObj.SelectedIndex)
                    rgEmployee.Rebind()
                    rgOrg.Rebind()
                    For Each i As GridItem In rgEmployee.Items
                        i.Edit = True
                    Next
                    For Each i As GridItem In rgOrg.Items
                        i.Edit = True
                    Next

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    Employee_Commend = New List(Of CommendEmpDTO)

                    List_Org = New List(Of CommendOrgDTO)
            End Select
            rep.Dispose()
            rgEmployee.Rebind()
            rgOrg.Rebind()

            If FormType = 1 Then
                For Each dataItemEmp As GridDataItem In rgEmployee.MasterTableView.Items
                    dataItemEmp.Selected = True
                Next
                For Each dataItemOrg As GridDataItem In rgOrg.MasterTableView.Items
                    dataItemOrg.Selected = True
                Next
            End If

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Commend/")
            If Directory.Exists(strPath) Then
                For Each deleteFile In Directory.GetFiles(strPath, "*.*", SearchOption.TopDirectoryOnly)
                    File.Delete(deleteFile)
                Next
            Else
                Directory.CreateDirectory(strPath)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objCommend As New CommendDTO
        Dim objOther As Profile.ProfileBusiness.OtherListDTO
        Dim lstCommendEmp As New List(Of CommendEmpDTO)
        Dim lstOrg As New List(Of CommendOrgDTO)
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim _filter As New CommendDTO

                        If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            If txtDecisionNo.Text = "" Then
                                ShowMessage(Translate("Vui lòng nhập số quyết định"), NotifyType.Warning)
                                Exit Sub
                            End If
                            'If txtUpload.Text = "" Then
                            '    ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                            If txtSignerName.Text = "" Then
                                ShowMessage("Vui lòng chọn Người phê duyệt", Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If rdSignDate.SelectedDate Is Nothing Then
                                ShowMessage("Vui lòng chọn ngày ký", Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _filter.ID = Decimal.Parse(hidDecisionID.Value)
                        End If

                        objCommend.COMMEND_OBJ = Decimal.Parse(cboCommendObj.SelectedValue)

                        If cboCommendType.SelectedValue <> "" Then
                            objCommend.COMMEND_TYPE = Decimal.Parse(cboCommendType.SelectedValue)
                        End If

                        If cboYear.SelectedValue <> "" Then
                            objCommend.YEAR = CDec(Val(cboYear.SelectedValue))
                        End If

                        objOther = (From p In ListComboData.LIST_COMMEND_OBJ Where p.ID = objCommend.COMMEND_OBJ).SingleOrDefault

                        If rntxtMoney.Value IsNot Nothing Then
                            objCommend.MONEY = rntxtMoney.Value
                        End If
                        objCommend.FILENAME = txtUpload.Text.Trim
                        objCommend.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objCommend.UPLOADFILE = "" Then
                            objCommend.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objCommend.UPLOADFILE = If(objCommend.UPLOADFILE Is Nothing, "", objCommend.UPLOADFILE)
                        End If
                        objCommend.NOTE = txtRemark.Text
                        objCommend.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
                        objCommend.EFFECT_DATE = rdEffectDate.SelectedDate
                        'objCommend.EXPIRE_DATE = rdExpireDate.SelectedDate
                        objCommend.SIGNER_NAME = txtSignerName.Text
                        objCommend.SIGN_DATE = rdSignDate.SelectedDate


                        If hidSignID.Value <> "" Then
                            objCommend.SIGN_ID = hidSignID.Value
                        End If

                        objCommend.NO = txtDecisionNo.Text
                        objCommend.SIGNER_TITLE = txtSignerTitle.Text

                        'If cboCommendLevel.SelectedValue <> "" Then
                        '    objCommend.COMMEND_LEVEL = cboCommendLevel.SelectedValue
                        'End If

                        If cboCommend_Title.SelectedValue <> "" Then
                            objCommend.COMMEND_TITLE_ID = cboCommend_Title.SelectedValue
                        End If

                        objCommend.COMMEND_DETAIL = txtCommend_Detail.Text
                        If Not String.IsNullOrWhiteSpace(cboCommendPay.SelectedValue) Then
                            objCommend.COMMEND_PAY = cboCommendPay.SelectedValue
                            If objCommend.COMMEND_PAY.HasValue AndAlso IsTienMatCommendPay(objCommend.COMMEND_PAY.Value) Then
                                If Not rntxtMoney.Value.HasValue Then
                                    ShowMessage(Translate("Vui lòng nhập mức thưởng khi loại thưởng là 'Tiền Mặt'"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If

                        ''thong tin ben sasco
                        'If cboCommendList.SelectedValue <> "" Then
                        '    objCommend.COMMEND_LIST_ID = cboCommendList.SelectedValue
                        'End If

                        'If cboPowerPay.SelectedValue <> "" Then
                        '    objCommend.POWER_PAY_ID = cboPowerPay.SelectedValue
                        'End If

                        objCommend.IS_TAX = chkTAX.Checked

                        If cboPeriodTax.SelectedValue <> "" Then
                            objCommend.PERIOD_TAX = cboPeriodTax.SelectedValue
                        End If

                        If cboPeriod.SelectedValue <> "" Then
                            objCommend.PERIOD_ID = cboPeriod.SelectedValue
                        End If
                        If cboCommendLevel.SelectedValue <> "" Then
                            objCommend.COMMEND_LEVEL = cboCommendLevel.SelectedValue
                        End If

                        objCommend.REMARK = txtCommend_Detail.Text
                        'objCommend.UPLOADFILE = txtUploadFile.Text

                        'If cboForm.SelectedValue <> "" Then
                        '    objCommend.FORM_ID = cboForm.SelectedValue
                        'End If

                        'kiem tra neu doi tuong cá nhan hay tap the
                        If cboCommendObj.SelectedIndex = 0 Then

                            Dim ValidGrid As Tuple(Of Boolean, String)
                            ValidGrid = ValidateGrid_Emp()
                            If ValidateGrid_Emp.Item1 = False Then
                                ShowMessage(ValidateGrid_Emp.Item2, NotifyType.Warning)
                                Exit Sub
                            End If

                            Dim TotalMoney As Decimal = 0
                            Dim dtrgEmployee As DataTable = GetDataFromGrid(rgEmployee)
                            For Each row As DataRow In dtrgEmployee.Rows
                                'If row("cbStatus") = 1 Then
                                Dim o As New CommendEmpDTO
                                o.GUID_ID = If(Not IsDBNull(row("GUID_ID")), row("GUID_ID"), "")
                                o.HU_EMPLOYEE_ID = If(Not IsDBNull(row("HU_EMPLOYEE_ID")), Decimal.Parse(row("HU_EMPLOYEE_ID")), 0)
                                If Not IsDBNull(row("MONEY")) Then
                                    o.MONEY = Decimal.Parse(row("MONEY"))
                                End If
                                o.COMMEND_PAY = If(Not IsDBNull(row("COMMEND_PAY")), Decimal.Parse(row("COMMEND_PAY")), Nothing)
                                o.ORG_ID = If(Not IsDBNull(row("ORG_ID")), Decimal.Parse(row("ORG_ID")), Nothing)
                                o.TITLE_ID = If(Not IsDBNull(row("TITLE_ID")), Decimal.Parse(row("TITLE_ID")), Nothing)
                                o.FULLNAME = If(Not IsDBNull(row("FULLNAME")), row("FULLNAME"), "")
                                o.ORG_NAME = If(Not IsDBNull(row("ORG_NAME")), row("ORG_NAME"), "")
                                o.TITLE_NAME = If(Not IsDBNull(row("TITLE_NAME")), row("TITLE_NAME"), "")
                                o.NO = If(Not IsDBNull(row("NO")), row("NO"), "")
                                lstCommendEmp.Add(o)
                                'End If
                            Next
                            If lstCommendEmp.Count = 0 Then
                                ShowMessage(Translate("Vui lòng chọn nhân viên trước khi lưu"), NotifyType.Warning)
                                Exit Sub
                            End If

                            objCommend.COMMEND_EMP = lstCommendEmp
                        Else
                            Dim ValidGrid2 As Tuple(Of Boolean, String)
                            ValidGrid2 = ValidateGrid_Org()
                            If ValidateGrid_Org.Item1 = False Then
                                ShowMessage(ValidateGrid_Org.Item2, NotifyType.Warning)
                                Exit Sub
                            End If

                            Dim dtrgOrg As DataTable = GetDataFromGrid(rgOrg)
                            For Each row As DataRow In dtrgOrg.Rows
                                If row("cbStatus") = 1 Then
                                    Dim o As New CommendOrgDTO
                                    o.GUID_ID = If(Not IsDBNull(row("GUID_ID")), row("GUID_ID"), "")
                                    If Not IsDBNull(row("MONEY")) Then
                                        o.MONEY = Decimal.Parse(row("MONEY"))
                                    End If
                                    o.COMMEND_PAY = If(row("COMMEND_PAY") <> "", Decimal.Parse(row("COMMEND_PAY")), Nothing)
                                    o.ORG_ID = row("ORG_ID")
                                    lstOrg.Add(o)
                                End If
                            Next

                            If lstOrg.Count = 0 Then
                                ShowMessage(Translate("Vui lòng chọn phòng ban trước khi lưu"), NotifyType.Warning)
                                Exit Sub
                            End If

                            objCommend.LIST_COMMEND_ORG = lstOrg
                        End If
                        'objCommend.YEAR = If(txtYear.Text.ToString <> "", Decimal.Parse(txtYear.Text), Nothing)
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertCommend(objCommend, gID) Then
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_CommendNewEdit&group=Business&FormType=0")
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commend&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                'If cboStatus.SelectedValue = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                                '    ShowMessage(Translate("Khen thưởng đã phê duyệt không được phép khen thưởng."), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                objCommend.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyCommend(objCommend, gID) Then
                                    'Dim str As String = "getRadWindow().close('1');"
                                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ''POPUPTOLINK
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commend&group=Business")
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commend&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Commend&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function IsTienMatCommendPay(ByVal commend_pay_id) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                Dim commend_pays = rep.GetOtherList("COMMEND_PAY", True).ToList(Of Profile.ProfileBusiness.OtherListDTO)()
                Dim current = commend_pays.FirstOrDefault(Function(f) f.ID = commend_pay_id)
                If current IsNot Nothing Then
                    Return current.CODE = ProfileCommon.Commend_Pay.TienMat
                End If
            End Using
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
            Return False
        End Try
        Return False
    End Function
    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Bật popup FindSigner</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindSinger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindSinger.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindSigner.MustHaveContract = True
            ctrlFindSigner.LoadAllOrganization = False
            ctrlFindSigner.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner.FunctionName = "ctrlHU_CommendNewEdit"
            ctrlFindSigner.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmployee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        ClearControlValue(hidSignID, txtSignerTitle, txtSignerName)
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                'Dim item = lstCommonEmployee(0)
                'txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'hidEmpID.Value = item.ID.ToString
                If Employee_Commend Is Nothing Then
                    Employee_Commend = New List(Of CommendEmpDTO)
                End If
                'If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                '    Employee_Commend.Clear()
                'End If

                hidOrgID.Value = lstCommonEmployee(0).ORG_ID

                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_Commend.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        ShowMessage(Translate("Nhân viên đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim employee As New CommendEmpDTO
                    employee.GUID_ID = Guid.NewGuid.ToString()
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.ORG_NAME_2 = emp.ORG_NAME_2
                    employee.HU_EMPLOYEE_ID = emp.ID
                    employee.FULLNAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID

                    If emp.TITLE_ID IsNot Nothing Then
                        Dim title = repNew.GetTitleID(emp.TITLE_ID)
                        If title IsNot Nothing Then
                            employee.LEVEL_NAME = title.LEVEL_TITLE_NAME
                        End If
                    End If

                    Employee_Commend.Add(employee)
                Next
                'If Employee_Commend.Count > 1 Then
                '    Employee_Commend.Clear()
                '    ShowMessage(Translate("Vui lòng chỉ chọn 1 nhân viên."), NotifyType.Warning)
                '    Exit Sub
                'End If
                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next

                rgEmployee.Rebind()
            End If
            'rep.Dispose()
            isLoadPopup = 0
            'AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindSigner</summary>
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
                txtSignerName.Text = item.FULLNAME_VN
                txtSignerTitle.Text = item.TITLE_NAME
                hidSignID.Value = item.ID
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện cho nút hủy của popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked, ctrlFindSigner.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi chọn combobox CommendObj</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboCommendObj_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCommendObj.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim validate As New Profile.ProfileBusiness.OtherListDTO
        Dim bResult As Boolean = False
        Dim rep As New ProfileRepository

        Try
            GetSigner()
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboCommendObj.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "COMMEND_OBJECT"
                bResult = rep.ValidateOtherList(validate)
            End If

            If Not bResult Then
                ListComboData.GET_COMMEND_OBJ = True
                rep.GetComboList(ListComboData)
                FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False)

                cboCommendObj.ClearSelection()
                cboCommendObj.SelectedIndex = 0
            End If
            rep.Dispose()
            VisibleGridview(cboCommendObj.SelectedIndex)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện bắt validation cho combobox CommendObj </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalCommendObj_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCommendObj.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If cboCommendObj.SelectedValue <> "" Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Protected Sub cvalCommendType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCommendType.ServerValidate
    '    Try
    '        If cboCommendType.SelectedValue <> "" Then
    '            args.IsValid = True
    '        Else
    '            args.IsValid = False
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện bắt validation cho combobox Money</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalMoney_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMoney.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If rntxtMoney.Value Is Nothing Then
                args.IsValid = True
                Exit Sub
            End If

            If rntxtMoney.Value <= 0 Then
                args.IsValid = False
                Exit Sub
            End If

            args.IsValid = True

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện bắt validation cho combobox Status</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalStatus_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalStatus.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If cboStatus.SelectedValue > 0 Then
    '            args.IsValid = True
    '            Exit Sub
    '        Else
    '            args.IsValid = False
    '            Exit Sub
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub cvalTotal_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTotal.ServerValidate
    '    Try
    '        Dim totalMoney = 0
    '        If rntxtMoney.Value IsNot Nothing Then
    '            totalMoney += rntxtMoney.Value
    '        End If
    '        Dim TotalMoneyEmp As Decimal = 0
    '        For Each i As GridDataItem In rgEmployee.Items
    '            TotalMoneyEmp += Utilities.ObjToDecima(CType(i("MONEY").Controls(0), RadNumericTextBox).Value)
    '        Next
    '        args.IsValid = totalMoney = TotalMoneyEmp
    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện thay đổi ngày trên grid EffectDate</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdEffectDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ClearControlValue(hidSignID, txtSignerTitle, txtSignerName)
            'GetSigner()
            Get_Infor_Period()
            If IsDate(rdEffectDate.SelectedDate) Then
                rdSignDate.SelectedDate = rdEffectDate.SelectedDate
            End If
            'AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện set properties cho row cua grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim dtData As New DataTable
            Try
                If e.Item.Edit Then
                    Dim rep As New ProfileRepository
                    Dim edit = CType(e.Item, GridEditableItem)
                    Dim item As GridDataItem = CType(e.Item, GridDataItem)

                    Dim cbCommend_Pay As New RadComboBox
                    cbCommend_Pay = CType(edit.FindControl("cbCommend_Pay"), RadComboBox)

                    'hinh thức khen thưởng
                    Dim payData As DataTable
                    payData = rep.GetOtherList("COMMEND_PAY", True)
                    If cbCommend_Pay IsNot Nothing Then
                        FillRadCombobox(cbCommend_Pay, payData, "NAME", "ID", False)
                    End If
                    'cbCommend_Pay.Width = 85

                    SetDataToGrid(edit)
                    'cbCommend_Pay.Dispose()
                    rep.Dispose()
                    edit.Dispose()
                    item.Dispose()
                End If
                If dtData IsNot Nothing Then dtData.Dispose()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cbCommend_Pay_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim edit = CType(sender, RadComboBox)
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
            Dim COMMEND_PAY = item.GetDataKeyValue("COMMEND_PAY")
            Dim guidId = item.GetDataKeyValue("GUID_ID")
            For Each rows As CommendEmpDTO In Employee_Commend
                If rows.GUID_ID = guidId Then
                    If edit.SelectedValue <> "" Then
                        rows.COMMEND_PAY = edit.SelectedValue
                        Exit For
                    End If
                End If
            Next
            rgEmployee.Rebind()
            For Each items As GridDataItem In rgEmployee.MasterTableView.Items
                items.Edit = True
            Next
            rgEmployee.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub txtNote_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadTextBox)
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            Dim NO = edit.Text
            Dim guidId = item.GetDataKeyValue("GUID_ID")
            For Each row As CommendEmpDTO In Employee_Commend
                If row.GUID_ID = guidId Then
                    row.NO = NO
                    Exit For
                End If
            Next
            rgEmployee.Rebind()
            For Each items As GridDataItem In rgEmployee.MasterTableView.Items
                items.Edit = True
            Next
            rgEmployee.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rnMONEY_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadNumericTextBox)
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            'Dim rnMoney As RadNumericTextBox = CType(item.FindControl("rnMONEY"), RadNumericTextBox)
            Dim MONEY = edit.Value
            Dim guidId = item.GetDataKeyValue("GUID_ID")
            For Each row As CommendEmpDTO In Employee_Commend
                If row.GUID_ID = guidId Then
                    row.MONEY = MONEY
                    Exit For
                End If
            Next
            rgEmployee.Rebind()
            For Each items As GridDataItem In rgEmployee.MasterTableView.Items
                items.Edit = True
            Next
            rgEmployee.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cbCommend_PayORG_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim edit = CType(sender, RadComboBox)
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
            'Dim COMMEND_PAY = item.GetDataKeyValue("COMMEND_PAY")
            Dim guidId = item.GetDataKeyValue("GUID_ID")
            For Each rows As CommendOrgDTO In List_Org
                If rows.GUID_ID = guidId Then
                    If edit.SelectedValue <> "" Then
                        rows.COMMEND_PAY = edit.SelectedValue
                        Exit For
                    End If
                End If
            Next
            rgOrg.Rebind()
            For Each items As GridDataItem In rgOrg.MasterTableView.Items
                items.Edit = True
            Next
            rgOrg.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rnMONEY_ORG_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadNumericTextBox)
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            'Dim rnMoney As RadNumericTextBox = CType(item.FindControl("rnMONEY"), RadNumericTextBox)
            Dim MONEY = edit.Value
            Dim guidId = item.GetDataKeyValue("GUID_ID")
            For Each row As CommendOrgDTO In List_Org
                If row.GUID_ID = guidId Then
                    row.MONEY = MONEY
                    Exit For
                End If
            Next
            rgOrg.Rebind()
            For Each items As GridDataItem In rgOrg.MasterTableView.Items
                items.Edit = True
            Next
            rgOrg.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load data cho grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgEmployee.DataSource = Employee_Commend
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện ItemCommand cho grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case e.CommandName
                Case "ShareEmployee"
                    Dim countEmp = rgEmployee.Items.Count
                    If countEmp = 0 Then
                        Exit Sub
                    End If

                    Dim totalMoney = 0
                    If rntxtMoney.Value IsNot Nothing Then
                        totalMoney += rntxtMoney.Value
                    End If

                    Dim shareEmp = Decimal.Round(rntxtMoney.Value / countEmp)
                    For Each item As GridDataItem In rgEmployee.Items
                        CType(item("MONEY").Controls(0), RadNumericTextBox).Value = shareEmp
                    Next

                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()

                Case "FindEmployeeImport"
                    isLoadPopup = 4
                    UpdateControlState()
                    ctrlFindEmpImportPopup.Show()

                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_Commend Where
                                 q.GUID_ID = i.GetDataKeyValue("GUID_ID")).FirstOrDefault
                        Employee_Commend.Remove(s)
                    Next
                    rgEmployee.Rebind()
                Case "Export"
                    Dim rep As New ProfileRepository
                    Dim tempPath = "~/ReportTemplates//Profile//Import//KhenThuong_Import.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ' Mẫu báo cáo không tồn tại
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim payData As DataTable
                    payData = rep.GetOtherList("COMMEND_PAY", True)

                    Dim dsDanhMuc As DataSet = New DataSet
                    If payData.Rows.Count > 0 Then
                        dsDanhMuc.Tables.Add(payData)
                        dsDanhMuc.Tables(0).TableName = "TABLE"
                    End If

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_KhenThuong", dsDanhMuc, Nothing, Response)
                    End Using
                Case "Import"
                    ctrlUpload2.isMultiple = False
                    ctrlUpload2.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub TableMapping2(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New ProfileBusinessClient
        Dim repc As New CommonRepository
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(1).ColumnName = "ORG_NAME"
        dtTemp.Columns(2).ColumnName = "TITLE_NAME"
        dtTemp.Columns(3).ColumnName = "COMMEND_TYPE_NAME"
        dtTemp.Columns(4).ColumnName = "COMMEND_TYPE"
        dtTemp.Columns(5).ColumnName = "MONEY"
        dtTemp.Columns(6).ColumnName = "NO"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
            If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã nhân viên - Bắt buộc nhập,"
                _error = False
            Else
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = rows("EMPLOYEE_CODE").ToString
                Dim lstEmpCode As New List(Of String)
                lstEmpCode.Add(rows("EMPLOYEE_CODE").ToString)
                Dim EmployeeList = repc.CheckExistValue(lstEmpCode, "EMPLOYEE_CODE", "HU_EMPLOYEE")
                If Not EmployeeList Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Mã nhân viên - Không tồn tại,"
                    _error = False
                End If
            End If


            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Import_Commend2()
    End Sub
    Private Sub Import_Commend2()
        Dim fileName As String
        Try
            '1. Đọc dữ liệu từ file Excel
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload2.UploadedFiles.Count
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim newRow1 As DataRow
            Dim newRow2 As DataRow
            Dim rep_emp As New CommonRepository
            Dim rep As New ProfileBusinessRepository
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload2.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                '1.1 Lưu file lên server
                file.SaveAs(fileName, True)
                '2.1 Đọc dữ liệu trong file Excel
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using

                TableMapping2(ds.Tables(0))

                If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                    If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Employee_Commend.Clear()
                        For Each rows As DataRow In ds.Tables(0).Rows
                            Dim emp_id = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))
                            Dim emp_sign = rep_emp.GetEmployeeID(emp_id)
                            Dim employee As New CommendEmpDTO
                            employee.GUID_ID = Guid.NewGuid.ToString()
                                employee.HU_EMPLOYEE_ID = 0
                            employee.ORG_ID = 0
                            employee.HU_EMPLOYEE_ID = emp_sign.ID
                            employee.ORG_ID = emp_sign.ORG_ID
                            employee.ORG_NAME = emp_sign.ORG_NAME
                            employee.TITLE_ID = emp_sign.TITLE_ID
                            employee.TITLE_NAME = emp_sign.TITLE_NAME
                            employee.FULLNAME = emp_sign.FULLNAME_VN
                            employee.EMPLOYEE_CODE = rows("EMPLOYEE_CODE")
                            If Not IsDBNull(rows("ORG_NAME")) Then
                                employee.ORG_NAME = rows("ORG_NAME")
                            End If
                            employee.TITLE_ID = 0
                            If Not IsDBNull(rows("ORG_NAME")) Then
                                employee.TITLE_NAME = rows("TITTLE_NAME")
                            End If
                            If IsNumeric(rows("COMMEND_TYPE")) Then
                                employee.COMMEND_PAY = CDec(rows("COMMEND_TYPE"))
                            End If
                            If IsNumeric(rows("MONEY")) Then
                                employee.MONEY = CDec(rows("MONEY"))
                            End If
                            If Not IsDBNull(rows("NO")) Then
                                employee.NO = rows("NO")
                            End If
                            Employee_Commend.Add(employee)
                        Next
                        rgEmployee.Rebind()

                        For Each i As GridItem In rgEmployee.Items
                            i.Edit = True
                        Next
                        rgEmployee.Rebind()
                    End If
                Else
                    Session("EXPORTREPORT") = dtLogs
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                    ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện bắt validation cho textbox DecisionNo</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusDecisionNo_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusDecisionNo.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If hidEmpID.Value = "" Then
    '            args.IsValid = True
    '            Exit Sub
    '        End If

    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateCommend("EXIST_DECISION_NO",
    '                                                        New CommendDTO With {
    '                                                            .EMPLOYEE_ID = hidEmpID.Value,
    '                                                            .NO = txtDecisionNo.Text})
    '                End Using

    '            Case CommonMessage.STATE_EDIT
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateCommend("EXIST_DECISION_NO",
    '                                                        New CommendDTO With {
    '                                                            .ID = hidID.Value,
    '                                                            .EMPLOYEE_ID = hidEmpID.Value,
    '                                                            .NO = txtDecisionNo.Text})
    '                End Using

    '            Case Else
    '                args.IsValid = True
    '        End Select

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện cho nút hủy cua popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmpImportPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmpImportPopup.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmpImport</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmpImportPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmpImportPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmpImportPopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                'Dim item = lstCommonEmployee(0)
                'txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'hidEmpID.Value = item.ID.ToString
                If Employee_Commend Is Nothing Then
                    Employee_Commend = New List(Of CommendEmpDTO)
                End If

                If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                    Employee_Commend.Clear()
                End If

                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New CommendEmpDTO
                    employee.GUID_ID = Guid.NewGuid.ToString()
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.HU_EMPLOYEE_ID = emp.ID
                    employee.FULLNAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    employee.ORG_NAME_2 = emp.ORG_NAME_2
                    If emp.TITLE_ID IsNot Nothing Then
                        Dim title = repNew.GetTitleID(emp.TITLE_ID)
                        If title IsNot Nothing Then
                            employee.LEVEL_NAME = title.LEVEL_TITLE_NAME
                        End If
                    End If

                    Employee_Commend.Add(employee)
                Next

                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If
            repNew.Dispose()
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện ItemCommand cho grid rgOrg</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOrg_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgOrg.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case e.CommandName
                Case "FindOrg"
                    isLoadPopup = 2
                    UpdateControlState()
                    ctrlFindOrgPopup.Show()

                Case "FindOrgImport"
                    isLoadPopup = 5
                    UpdateControlState()
                    ctrlFindOrgImportPopup.Show()

                Case "DeleteOrg"
                    For Each i As GridDataItem In rgOrg.SelectedItems
                        Dim s = (From q In List_Org Where
                                 q.GUID_ID = i.GetDataKeyValue("GUID_ID")).FirstOrDefault
                        List_Org.Remove(s)
                    Next
                    rgOrg.Rebind()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện set properties cho row cua grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOrg_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgOrg.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim dtData As New DataTable
            Try
                If e.Item.Edit Then
                    Dim rep As New ProfileRepository
                    Dim edit = CType(e.Item, GridEditableItem)
                    Dim item As GridDataItem = CType(e.Item, GridDataItem)

                    Dim cbCommend_Pay As New RadComboBox
                    cbCommend_Pay = CType(edit.FindControl("cbCommend_PayORG"), RadComboBox)

                    'hinh thức khen thưởng
                    Dim payData As DataTable
                    payData = rep.GetOtherList("COMMEND_PAY", True)
                    If cbCommend_Pay IsNot Nothing Then
                        FillRadCombobox(cbCommend_Pay, payData, "NAME", "ID", False)
                    End If

                    SetDataToGrid_Org(edit)
                    'cbCommend_Pay.Dispose()
                    rep.Dispose()
                    edit.Dispose()
                    item.Dispose()
                End If
                If dtData IsNot Nothing Then dtData.Dispose()
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub



    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho grid ORG</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOrg_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgOrg.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If List_Org Is Nothing Then
                List_Org = New List(Of CommendOrgDTO)
            End If

            rgOrg.DataSource = List_Org

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Organization được chọn ở popup FindOrg</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Dim listID As List(Of Decimal) = ctrlFindOrgPopup.CheckedValueKeys()
            Dim lstOrg As List(Of Common.CommonBusiness.OrganizationDTO) = ctrlFindOrgPopup.ListOrgChecked()
            If List_Org Is Nothing Then
                List_Org = New List(Of CommendOrgDTO)
            End If

            For Each org_Check As Common.CommonBusiness.OrganizationDTO In lstOrg
                If List_Org.Any(Function(f) f.ORG_ID = org_Check.ID) Then
                    ShowMessage(Translate("Phòng ban đã tồn tại"), NotifyType.Warning)
                    Exit Sub
                End If
                Dim org As New CommendOrgDTO
                org.GUID_ID = Guid.NewGuid.ToString
                org.ID = org_Check.ID
                org.ORG_ID = org_Check.ID
                org.ORG_NAME = org_Check.NAME_VN
                List_Org.Add(org)
            Next


            If lstOrg.Count > 0 Then
                hidOrgID.Value = lstOrg(0).ID
            End If

            rgOrg.Rebind()
            For Each i As GridItem In rgOrg.Items
                i.Edit = True
            Next
            rgOrg.Rebind()
            'AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi chkTAX thay đổi giá trị</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkTAX_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTAX.CheckedChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Get_Infor_Period()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi click btn Download</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()        
    '    Dim bCheck As Boolean = False

    '    Try
    '        If cboUpload.CheckedItems.Count >= 1 Then
    '            'Using zip As New ZipFile
    '            '    zip.AlternateEncodingUsage = ZipOption.AsNecessary
    '            '    zip.AddDirectoryByName("Files")

    '            '    For Each item As RadComboBoxItem In cboUpload.CheckedItems
    '            '        Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Profile/Commend/"), item.Text))
    '            '        If file.Exists Then
    '            '            zip.AddFile(file.FullName, "Files")
    '            '        End If
    '            '    Next

    '            '    Response.Clear()
    '            '    Response.BufferOutput = False
    '            '    Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
    '            '    Response.ContentType = "application/zip"
    '            '    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
    '            '    zip.Save(Response.OutputStream)
    '            '    Response.[End]()
    '            'End Using

    '            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Commend/")

    '            For Each item As RadComboBoxItem In cboUpload.CheckedItems
    '                Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(strPath, item.Text))
    '                If file.Exists Then
    '                    bCheck = True
    '                End If
    '            Next

    '            If bCheck Then
    '                ZipFiles(strPath)
    '            End If
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi click btn UploadFile</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi click [OK] xác nhận sẽ Upload file</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/CommendInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        'If Commend IsNot Nothing Then
                        '    If Commend.UPLOADFILE IsNot Nothing Then
                        '        strPath += Commend.UPLOADFILE
                        '    Else
                        '        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        '        strPath = strPath + str_Filename
                        '    End If
                        '    fileName = System.IO.Path.Combine(strPath, file.FileName)
                        '    file.SaveAs(fileName, True)
                        '    Commend.UPLOADFILE = str_Filename
                        '    txtUploadFile.Text = file.FileName
                        'Else
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        'End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindOrgImport</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindOrgImportPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgImportPopup.EmployeeSelected
        Dim lstCommonOrg As New List(Of Common.CommonBusiness.OrganizationDTO)
        'Dim rep As New ProfileBusinessRepository
        'Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonOrg = CType(ctrlFindOrgImportPopup.SelectedOrgImport, List(Of Common.CommonBusiness.OrganizationDTO))
            If lstCommonOrg.Count > 0 Then
                'Dim item = lstCommonEmployee(0)
                'txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'hidEmpID.Value = item.ID.ToString
                If List_Org Is Nothing Then
                    List_Org = New List(Of CommendOrgDTO)
                End If

                If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                    Employee_Commend.Clear()
                End If

                For Each orgImport As CommonBusiness.OrganizationDTO In lstCommonOrg
                    Dim org As New CommendOrgDTO
                    org.ORG_ID = orgImport.ID
                    org.ORG_NAME = orgImport.NAME_VN
                    org.ID = orgImport.ID
                    List_Org.Add(org)
                Next

                rgOrg.Rebind()

                For Each i As GridItem In rgOrg.Items
                    i.Edit = True
                Next

                rgOrg.Rebind()
            End If

            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox trạng thái
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusStatus.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New OtherListDTO
    '    Dim dtData As DataTable = Nothing
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboStatus.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = "DECISION_STATUS"
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If

    '        If Not args.IsValid Then
    '            'dtData = rep.GetOtherList("COMMEND_STATUS", True)
    '            'FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
    '            Dim dtData1 As New DataTable
    '            dtData1 = rep.GetOtherList(OtherTypes.DecisionStatus, True)
    '            FillRadCombobox(cboStatus, dtData1, "NAME", "ID", True)
    '            'cboStatus.ClearSelection()
    '            'cboStatus.SelectedIndex = 0
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox loại khen thưởng
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusCommendList_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCommendList.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New OtherListDTO
    '    Dim dtData As DataTable = Nothing
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboCommendList.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = "COMMEND_CATEGORY"
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If

    '        If Not args.IsValid Then
    '            dtData = rep.GetOtherList("COMMEND_CATEGORY", True)
    '            FillRadCombobox(cboCommendList, dtData, "NAME", "ID", True)

    '            cboCommendList.ClearSelection()
    '            cboCommendList.SelectedIndex = 0
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox biểu mẫu 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusForm_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusForm.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New OtherListDTO
    '    Dim dtData As DataTable = Nothing
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboForm.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = "FORM_COMMEND"
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If

    '        If Not args.IsValid Then
    '            dtData = rep.GetOtherList("FORM_COMMEND", True)
    '            FillRadCombobox(cboForm, dtData, "NAME", "ID", True)

    '            cboForm.ClearSelection()
    '            cboForm.SelectedIndex = 0
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox hình thức trả thưởng
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusCommendPay_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCommendPay.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New Profile.ProfileBusiness.OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboCommendPay.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "COMMEND_PAY"
                args.IsValid = rep.ValidateOtherList(validate)
            End If

            If Not args.IsValid Then
                dtData = rep.GetOtherList("COMMEND_PAY", True)

                FillRadCombobox(cboCommendPay, dtData, "NAME", "ID", True)

                cboCommendPay.ClearSelection()
                cboCommendPay.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox danh hiệu khen thưởng
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusCommend_Title_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCommend_Title.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New CommendListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboCommend_Title.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateCommendList(validate)
            End If

            If Not args.IsValid Then
                VisibleGridview(cboCommendObj.SelectedIndex)

                cboCommend_Title.ClearSelection()
                cboCommend_Title.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox hình thức khen thưởng
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusCommendType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCommendType.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New CommendListDTO
    '    Dim dtData As DataTable = Nothing
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboCommendType.SelectedValue
    '            validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateCommendList(validate)
    '        End If

    '        If Not args.IsValid Then
    '            VisibleGridview(cboCommendObj.SelectedIndex)

    '            cboCommendType.ClearSelection()
    '            cboCommendType.SelectedIndex = 0
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox cấp khen thưởng 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusCommendLevel_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCommendLevel.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim validate As New CommendListDTO
    '    Dim dtData As DataTable = Nothing
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboCommendLevel.SelectedValue
    '            validate.LEVEL_ID = cboCommendLevel.SelectedValue
    '            validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateCommendList(validate)
    '        End If

    '        If Not args.IsValid Then
    '            dtData = psp.Get_Commend_Level(True)
    '            FillRadCombobox(cboStatus, dtData, "NAME", "ID", False)

    '            cboCommendLevel.ClearSelection()
    '            cboCommendLevel.SelectedIndex = 0
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Try
    '        Dim tempPath = "~/ReportTemplates//Profile//Import//import_khenthuong.xls"
    '        If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
    '            ' Mẫu báo cáo không tồn tại
    '            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
    '            Exit Sub
    '        End If

    '        Using xls As New AsposeExcelCommon
    '            Dim bCheck = xls.ExportExcelTemplateNoData(
    '              System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_KhenThuong", Response)
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub btnImportFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportFile.Click
    '    Try
    '        ctrlUpload.isMultiple = False
    '        ctrlUpload.Show()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    '    Try
    '        Dim employee As New CommendEmpDTO
    '        employee.GUID_ID = Guid.NewGuid.ToString()
    '        employee.HU_EMPLOYEE_ID = 0
    '        employee.ORG_ID = 0
    '        employee.ORG_NAME = txtOrgName.Text
    '        employee.FULLNAME = txtFullName.Text
    '        employee.TITLE_ID = 0
    '        employee.TITLE_NAME = txtTitleName.Text
    '        Employee_Commend.Add(employee)
    '        rgEmployee.Rebind()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Import_Commend()
    End Sub
    Private Sub Import_Commend()
        Dim fileName As String
        Try
            '1. Đọc dữ liệu từ file Excel
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim newRow1 As DataRow
            Dim newRow2 As DataRow

            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                '1.1 Lưu file lên server
                file.SaveAs(fileName, True)
                '2.1 Đọc dữ liệu trong file Excel
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using

                TableMapping(ds.Tables(0))

                If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                    If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Employee_Commend.Clear()
                        For Each rows As DataRow In ds.Tables(0).Rows
                            Dim employee As New CommendEmpDTO
                            employee.GUID_ID = Guid.NewGuid.ToString()
                            employee.HU_EMPLOYEE_ID = 0
                            employee.ORG_ID = 0
                            employee.ORG_NAME = rows("ORG_TEXT")
                            employee.FULLNAME = rows("FULLNAME_TEXT")
                            employee.TITLE_ID = 0
                            employee.TITLE_NAME = rows("TITLE_TEXT")

                            Employee_Commend.Add(employee)
                        Next
                        rgEmployee.Rebind()

                        For Each i As GridItem In rgEmployee.Items
                            i.Edit = True
                        Next
                        rgEmployee.Rebind()
                    End If
                Else
                    Session("EXPORTREPORT") = dtLogs
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                    ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New ProfileBusinessClient
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(1).ColumnName = "FULLNAME_TEXT"
        dtTemp.Columns(2).ColumnName = "ORG_TEXT"
        dtTemp.Columns(3).ColumnName = "TITLE_TEXT"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("FULLNAME_TEXT").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("FULLNAME_TEXT")

            If IsDBNull(rows("ORG_TEXT")) OrElse rows("ORG_TEXT") = "" Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Phòng ban - Bắt buộc nhập,"
                _error = False
            End If

            If IsDBNull(rows("TITLE_TEXT")) OrElse rows("TITLE_TEXT") = "" Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Chức Danh - Bắt buộc nhập,"
                _error = False
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạn thái của control</summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        log = LogHelper.GetUserLog
        Try
            'If log.Username.ToUpper = "ADMIN" Then
            '    lbStatus.Visible = True
            '    cboStatus.Visible = True
            'Else
            '    lbStatus.Visible = False
            '    cboStatus.Visible = False
            'End If

            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If FindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                FindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If

            If FindSign.Controls.Contains(ctrlFindSigner) Then
                FindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            If FindEmployeeImport.Controls.Contains(ctrlFindEmpImportPopup) Then
                FindEmployeeImport.Controls.Remove(ctrlFindEmpImportPopup)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = True
                    ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)

                    If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                        ctrlFindEmployeePopup.MultiSelect = True
                    ElseIf cboCommendObj.SelectedValue = 389 Then ' tap the
                        ctrlFindEmployeePopup.MultiSelect = True

                    End If
                    ctrlFindEmployeePopup._isCommend = True

                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                    ctrlFindOrgPopup.CheckChildNodes = True
                    ctrlFindOrgPopup.LoadAllOrganization = False

                    FindOrg.Controls.Add(ctrlFindOrgPopup)

                Case 3
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = False
                    ctrlFindSigner.IsOnlyWorkingWithoutTer = True
                    ctrlFindSigner.FunctionName = "ctrlHU_CommendNewEdit"
                    ctrlFindSigner.MultiSelect = False
                    ctrlFindSigner.EmployeeOrg = If(LogHelper.CurrentUser.ORG_ID IsNot Nothing, LogHelper.CurrentUser.ORG_ID, 0)
                    ctrlFindSigner.EffectDate = If(rdEffectDate.SelectedDate IsNot Nothing, CDbl(rdEffectDate.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))

                    FindSign.Controls.Add(ctrlFindSigner)

                Case 4
                    ctrlFindEmpImportPopup = Me.Register("ctrlFindEmpImportPopup", "Profile/Shared", "ctrlFindEmployeeImportPopup")

                    FindEmployeeImport.Controls.Add(ctrlFindEmpImportPopup)

                Case 5
                    ctrlFindOrgImportPopup = Me.Register("ctrlFindOrgImportPopup", "Profile/Shared", "ctrlFindOrgImportPopup")
                    FindOrgImport.Controls.Add(ctrlFindOrgImportPopup)
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim row As DataRow
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            For index = 2018 To Date.Now.Year + 2
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_COMMEND_OBJ = True
                ListComboData.GET_COMMEND_STATUS = True
                ListComboData.GET_COMMEND_TYPE = True
                rep.GetComboList(ListComboData)
            End If

            'FillDropDownList(cboStatus, ListComboData.LIST_COMMEND_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            Dim dtData As New DataTable
            dtData = rep.GetOtherList("COMMEND_STATUS", False)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            cboStatus.SelectedValue = (From p In dtData Where p("CODE") = 0 Select p("ID")).FirstOrDefault
            FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            FillDropDownList(cboCommendType, ListComboData.LIST_COMMEND_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False)

            cboStatus.SelectedIndex = 0
            cboCommendObj.SelectedIndex = 0
            'txtDecisionNo.ReadOnly = True
            'cboCommendType.SelectedIndex = 0

            Dim payData As DataTable
            payData = rep.GetOtherList("COMMEND_PAY", True)

            FillRadCombobox(cboCommendPay, payData, "NAME", "ID", False)

            'danh hieu khen thuong
            Dim titleCommend As DataTable
            titleCommend = psp.Get_Commend_Title(True, cboCommendObj.SelectedValue)
            FillRadCombobox(cboCommend_Title, titleCommend, "NAME", "ID", False)

            'get loai khen thuong
            'Dim categoryData As DataTable
            'categoryData = rep.GetOtherList("COMMEND_CATEGORY", True)
            'FillRadCombobox(cboCommendList, categoryData, "NAME", "ID", False)

            'thong NGUON CHI
            Dim powerpayData As DataTable
            '  powerpayData = psp.Get_Commend_PowerPay(True)
            ' powerpayData = rep.GetOtherList("POWER_PAY", True)
            ' FillRadCombobox(cboPowerPay, powerpayData, "NAME", "ID", False)

            powerpayData = psp.Get_PAY_POWER(True, Date.Now.Year)
            ' FillRadCombobox(cboPowerPay, powerpayData, "NAME", "ID", False)

            'hinh thức khen thưởng
            'Dim listCommend As DataTable
            'listCommend = psp.Get_Commend_Formality(True, cboCommendObj.SelectedValue)
            'FillRadCombobox(cboCommendType, listCommend, "NAME", "ID", False)


            'cap khen thưởng
            Dim levelCommend As DataTable
            levelCommend = psp.Get_Commend_Level(True)
            FillRadCombobox(cboCommendLevel, levelCommend, "NAME", "ID", False)
            cboCommendLevel.SelectedValue = levelCommend.Rows(0)("ID")
            cboCommendLevel.Text = levelCommend.Rows(0)("NAME")
            'load bieu mau
            'Dim formData As DataTable
            'formData = rep.GetOtherList("FORM_COMMEND", True)
            'FillRadCombobox(cboForm, formData, "NAME", "ID", False)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Get parameters</summary>
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

                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

                If Request.Params("Status") IsNot Nothing Then
                    If Convert.ToDecimal(Request.Params("Status")) = 714 Then 'ProfileCommon.HU_COMMEND_STATUS.APPROVE Then
                        EnableControlAll(False, rdEffectDate, txtDecisionNo, cboYear, cboStatus, rdSignDate, txtSignerName,
                                         btnFindSinger, txtSignerTitle, cboCommendObj, cboCommend_Title,
                                         cboCommendType, cboCommendPay, rntxtMoney,
                                         cboPeriod, chkTAX, cboPeriodTax, txtCommend_Detail, txtRemark, cboCommendLevel,
                                         txtYear)
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        Utilities.EnabledGrid(rgEmployee, False, False)
                        Utilities.EnabledGrid(rgOrg, False, False)
                        EnableControlAll(True, btnDownload, btnUploadFile)
                    End If

                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Đổ dữ liệu lên grid</summary>
    ''' <remarks></remarks>
    Private Sub FilldataGridView(ByVal id As Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Using rep As New ProfileBusinessRepository
                If id = 0 Then
                    Employee_Commend = rep.GetEmployeeCommendByID(Utilities.ObjToDecima(IDSelect))
                Else
                    List_Org = rep.GetOrgCommendByID(Utilities.ObjToDecima(IDSelect))
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Lấy thông tin kỳ lương</summary>
    ''' <remarks></remarks>
    Private Sub Get_Infor_Period()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If rdEffectDate.SelectedDate IsNot Nothing Then
                Dim periodData As DataTable
                periodData = psp.Get_Commend_Period(True, rdEffectDate.SelectedDate.Value.Year)
                FillRadCombobox(cboPeriod, periodData, "NAME", "ID", False)
                If chkTAX.Checked Then
                    Dim periodTaxData As DataTable
                    periodTaxData = psp.Get_Commend_Period(True, rdEffectDate.SelectedDate.Value.Year)
                    FillRadCombobox(cboPeriodTax, periodTaxData, "NAME", "ID", False)
                Else
                    cboPeriodTax.ClearSelection()
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Set trạng thái hiển thị cho các grid</summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Private Sub VisibleGridview(ByVal index As Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            If cboCommendObj.SelectedIndex = 0 Then
                rgOrg.Visible = False
                rgEmployee.Visible = True
            Else
                rgOrg.Visible = True
                rgEmployee.Visible = False
            End If

            'danh hieu khen thuong
            Dim titleCommend As DataTable
            titleCommend = psp.Get_Commend_Title(True, cboCommendObj.SelectedValue)
            FillRadCombobox(cboCommend_Title, titleCommend, "NAME", "ID", False)

            cboCommend_Title.ClearSelection()
            cboCommend_Title.SelectedIndex = 0

            'hinh thức khen thưởng
            'Dim listCommend As DataTable
            'listCommend = psp.Get_Commend_Formality(True, cboCommendObj.SelectedValue)
            'FillRadCombobox(cboCommendType, listCommend, "NAME", "ID", False)

            'cboCommendType.ClearSelection()
            'cboCommendType.SelectedIndex = 0

            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_COMMEND_TYPE = True
                rep.GetComboList(ListComboData)
            End If
            FillDropDownList(cboCommendType, ListComboData.LIST_COMMEND_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Get datasource cho combobox cboUpload</summary>
    ''' <param name="strUpload"></param>
    ''' <remarks></remarks>
    'Private Sub loadDatasource(ByVal strUpload As String)
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        Dim data As New DataTable
    '        data.Columns.Add("FileName")
    '        Dim row As DataRow
    '        Dim str() As String

    '        If strUpload <> "" Then
    '            txtUploadFile.Text = strUpload
    '            str = strUpload.Split(";")

    '            For Each s As String In str
    '                If s <> "" Then
    '                    row = data.NewRow
    '                    row("FileName") = s
    '                    data.Rows.Add(row)
    '                End If
    '            Next

    '            cboUpload.DataSource = data
    '            cboUpload.DataTextField = "FileName"
    '            cboUpload.DataValueField = "FileName"
    '            cboUpload.DataBind()
    '        Else
    '            cboUpload.DataSource = Nothing
    '            cboUpload.ClearSelection()
    '            cboUpload.ClearCheckedItems()
    '            cboUpload.Items.Clear()
    '            strUpload = String.Empty
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>06/09/2017</lastupdate>
    ''' <summary>
    ''' Phuong thuc xu ly viec zip file vao folder Zip 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    'Private Sub ZipFiles(ByVal path As String)
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        Dim crc As New Crc32()
    '        Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
    '        Dim fileNameZip As String = "ThongTinKhenThuong.zip"

    '        If Not Directory.Exists(pathZip) Then
    '            Directory.CreateDirectory(pathZip)
    '        Else
    '            For Each deleteFile In Directory.GetFiles(pathZip, "*.*", SearchOption.TopDirectoryOnly)
    '                File.Delete(deleteFile)
    '            Next
    '        End If

    '        Dim s As New ZipOutputStream(File.Create(pathZip & fileNameZip))
    '        s.SetLevel(0)
    '        ' 0 - store only to 9 - means best compression
    '        For i As Integer = 0 To Directory.GetFiles(path).Length - 1
    '            ' Must use a relative path here so that files show up in the Windows Zip File Viewer
    '            ' .. hence the use of Path.GetFileName(...)
    '            Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

    '            Dim entry As New ZipEntry(fileName)
    '            entry.DateTime = DateTime.Now

    '            ' Read in the 
    '            Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
    '                Dim buffer As Byte() = New Byte(fs.Length - 1) {}
    '                fs.Read(buffer, 0, buffer.Length)
    '                entry.Size = fs.Length
    '                fs.Close()
    '                crc.Reset()
    '                crc.Update(buffer)
    '                entry.Crc = crc.Value
    '                s.PutNextEntry(entry)
    '                s.Write(buffer, 0, buffer.Length)
    '            End Using
    '        Next
    '        s.Finish()
    '        s.Close()

    '        Using FileStream = File.Open(pathZip & fileNameZip, FileMode.Open)
    '            Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
    '            FileStream.Read(buffer, 0, buffer.Length)
    '            Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
    '            Response.Clear()
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileNameZip, "_"))
    '            Response.AddHeader("Content-Length", FileStream.Length.ToString())
    '            Response.ContentType = "application/octet-stream"
    '            Response.BinaryWrite(buffer)
    '            FileStream.Close()
    '        End Using
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        HttpContext.Current.Trace.Warn(ex.ToString())
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String = txtUploadFile.Text.Trim

            'If Not Directory.Exists(pathZip) Then
            '    Directory.CreateDirectory(pathZip)
            'Else
            '    For Each deleteFile In Directory.GetFiles(pathZip, "*.*", SearchOption.TopDirectoryOnly)
            '        File.Delete(deleteFile)
            '    Next
            'End If

            'Dim s As New ZipOutputStream(File.Create(pathZip & fileNameZip))
            's.SetLevel(0)
            '' 0 - store only to 9 - means best compression
            'For i As Integer = 0 To Directory.GetFiles(path).Length - 1
            '    ' Must use a relative path here so that files show up in the Windows Zip File Viewer
            '    ' .. hence the use of Path.GetFileName(...)
            '    Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

            '    Dim entry As New ZipEntry(fileName)
            '    entry.DateTime = DateTime.Now

            '    ' Read in the 
            '    Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
            '        Dim buffer As Byte() = New Byte(fs.Length - 1) {}
            '        fs.Read(buffer, 0, buffer.Length)
            '        entry.Size = fs.Length
            '        fs.Close()
            '        crc.Reset()
            '        crc.Update(buffer)
            '        entry.Crc = crc.Value
            '        s.PutNextEntry(entry)
            '        s.Write(buffer, 0, buffer.Length)
            '    End Using
            'Next
            's.Finish()
            's.Close()

            'Using FileStream = File.Open(path & fileNameZip, FileMode.Open)
            '    Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
            '    FileStream.Read(buffer, 0, buffer.Length)
            '    Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
            '    Response.Clear()
            '    Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileNameZip, "_"))
            '    Response.AddHeader("Content-Length", FileStream.Length.ToString())
            '    Response.ContentType = "application/octet-stream"
            '    Response.BinaryWrite(buffer)
            '    FileStream.Close()
            'End Using

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try

            If txtUpload.Text <> "" Then
                Dim strPath_Down As String
                If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
                    If txtRemindLink.Text IsNot Nothing Then
                        If txtRemindLink.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/CommendInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/CommendInfo/" + Down_File)
                        'bCheck = True
                        ZipFiles(strPath_Down)
                    End If
                End If
                'If bCheck Then
                '    ZipFiles(strPath_Down)
                'End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Dim data As New DataTable
            'data.Columns.Add("FileName")
            'Dim row As DataRow
            'Dim str() As String

            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload

                'Str = strUpload.Split(";")

                'For Each s As String In str
                '    If s <> "" Then
                '        row = data.NewRow
                '        row("FileName") = s
                '        data.Rows.Add(row)
                '    End If
                'Next

                'txtUpload.DataSource = data
                'txtUpload.DataTextField = "FileName"
                'txtUpload.DataValueField = "FileName"
                'txtUpload.DataBind()
            Else
                'txtUpload.DataSource = Nothing
                'txtUpload.ClearSelection()
                'txtUpload.ClearCheckedItems()
                'txtUpload.Items.Clear()
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function ValidateGrid_Emp() As Tuple(Of Boolean, String)
        Dim flag As Boolean = True
        Dim msgError As String = "Bạn chưa nhập đầy đủ thông tin. Vui lòng xem vị trí tô màu đỏ và gợi nhắc ở lưới."
        Try
            For Each items In rgEmployee.Items
                Dim cbGroupSalary = CType(items.FindControl("cbCommend_Pay"), RadComboBox)
                If IsNumeric(cbGroupSalary.SelectedValue) = False Then
                    cbGroupSalary.BackColor = System.Drawing.Color.Red
                    cbGroupSalary.ErrorMessage = "Bạn phải chọn hình thức khen thưởng"
                    flag = False
                End If
                Dim txtMoney = CType(items.FindControl("rnMONEY"), RadNumericTextBox)
                If IsNumeric(txtMoney.Value) = False Then
                    If cbGroupSalary.SelectedValue <> "" Then
                        If cbGroupSalary.Text = "Tiền mặt" Or cbGroupSalary.Text = "Chuyển khoản" Then
                            txtMoney.BackColor = System.Drawing.Color.Red
                            txtMoney.ToolTip = "Bạn phải nhập mức thưởng"
                            flag = False
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Dim Tuple As New Tuple(Of Boolean, String)(flag, msgError)
        Return Tuple
    End Function
    Private Function ValidateGrid_Org() As Tuple(Of Boolean, String)
        Dim flag As Boolean = True
        Dim msgError As String = "Bạn chưa nhập đầy đủ thông tin. Vui lòng xem vị trí tô màu đỏ và gợi nhắc ở lưới."
        Try
            For Each items In rgOrg.Items
                Dim cbGroupSalary = CType(items.FindControl("cbCommend_PayORG"), RadComboBox)
                If IsNumeric(cbGroupSalary.SelectedValue) = False Then
                    cbGroupSalary.BackColor = System.Drawing.Color.Red
                    cbGroupSalary.ErrorMessage = "Bạn phải chọn hình thức khen thưởng"
                    flag = False
                End If

                Dim txtMoney = CType(items.FindControl("rnMONEY_ORG"), RadNumericTextBox)
                If IsNumeric(txtMoney.Value) = False Then
                    If cbGroupSalary.SelectedValue <> "" Then
                        If cbGroupSalary.Text = "Tiền mặt" Or cbGroupSalary.Text = "Chuyển khoản" Then
                            txtMoney.BackColor = System.Drawing.Color.Red
                            txtMoney.ToolTip = "Bạn phải nhập mức thưởng"
                            flag = False
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Dim Tuple As New Tuple(Of Boolean, String)(flag, msgError)
        Return Tuple
    End Function
    Private Function GetDataFromGrid(ByVal gr As RadGrid) As DataTable
        Dim bsSource As DataTable
        Try
            bsSource = New DataTable()
            For Each Col As GridColumn In gr.Columns
                Dim DataColumn As DataColumn = New DataColumn(Col.UniqueName)
                bsSource.Columns.Add(DataColumn)
            Next
            'coppy data to grid
            For Each Item As GridDataItem In gr.EditItems
                If Item.Display = False Then Continue For
                Dim Dr As DataRow = bsSource.NewRow()
                For Each col As GridColumn In gr.Columns
                    Try
                        If col.UniqueName = "cbStatus" Then
                            If Item.Selected = True Then
                                Dr(col.UniqueName) = 1
                            Else
                                Dr(col.UniqueName) = 0
                            End If
                            Continue For
                        End If
                        If InStr(",COMMEND_PAY,MONEY,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "cb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rt"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
                                Case "rd"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadDatePicker).SelectedDate
                            End Select
                        Else
                            Dr(col.UniqueName) = Item.GetDataKeyValue(col.UniqueName)
                        End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                bsSource.Rows.Add(Dr)
            Next
            bsSource.AcceptChanges()
            Return bsSource
        Catch ex As Exception
        End Try
    End Function

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim rep As New ProfileRepository

        If Employee_Commend Is Nothing OrElse Employee_Commend.Count = 0 Then Exit Sub

        'Ngạch lương
        Dim cbCommend_Pay As New RadComboBox
        cbCommend_Pay = CType(EditItem.FindControl("cbCommend_Pay"), RadComboBox)

        Try
            For Each col As GridColumn In rgEmployee.Columns

                Dim dtData As DataTable = New DataTable()
                Dim employee_id = EditItem.GetDataKeyValue("HU_EMPLOYEE_ID")
                Dim comment_id = EditItem.GetDataKeyValue("HU_COMMEND_ID")
                Dim rowData As CommendEmpDTO = Employee_Commend.Find(Function(p) p.HU_COMMEND_ID = comment_id And p.HU_EMPLOYEE_ID = employee_id)
                Try
                    If IsNumeric(rowData.COMMEND_PAY) Then
                        dtData = rep.GetOtherList("COMMEND_PAY", True)
                        FillRadCombobox(cbCommend_Pay, dtData, "NAME", "ID")
                    End If
                Catch ex As Exception
                End Try
                Try
                    If InStr(",COMMEND_PAY,MONEY,NO,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "cb"
                                CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadComboBox).ClearSelection()
                                CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue = rowData.COMMEND_PAY
                            Case "rn"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.Text = rowData.MONEY.ToString()
                            Case "tx"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = If(rowData.NO IsNot Nothing, rowData.NO.ToString(), "")
                        End Select
                    Else
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            ' cbCommend_Pay.Dispose()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetDataToGrid_Org(ByVal EditItem As GridEditableItem)
        Dim rep As New ProfileRepository

        If List_Org Is Nothing OrElse List_Org.Count = 0 Then Exit Sub

        'Ngạch lương
        Dim cbCommend_Pay As New RadComboBox
        cbCommend_Pay = CType(EditItem.FindControl("cbCommend_PayORG"), RadComboBox)

        Try
            For Each col As GridColumn In rgOrg.Columns

                Dim dtData As DataTable = New DataTable()
                Dim org_id = EditItem.GetDataKeyValue("ORG_ID")
                Dim comment_id = EditItem.GetDataKeyValue("HU_COMMEND_ID")
                Dim rowData As CommendOrgDTO = List_Org.Find(Function(p) p.HU_COMMEND_ID = comment_id And p.ORG_ID = org_id)
                Try
                    If IsNumeric(rowData.COMMEND_PAY) Then
                        dtData = rep.GetOtherList("COMMEND_PAY", True)
                        FillRadCombobox(cbCommend_Pay, dtData, "NAME", "ID")
                    End If
                Catch ex As Exception
                End Try
                Try
                    If InStr(",COMMEND_PAY,MONEY,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "cb"
                                CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadComboBox).ClearSelection()
                                CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue = rowData.COMMEND_PAY
                            Case "rn"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData.MONEY.ToString()
                        End Select
                    Else
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            'cbCommend_Pay.Dispose()

        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetSigner()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            ClearControlValue(txtSignerName, txtSignerTitle, hidSignID)
            If IsDate(rdEffectDate.SelectedDate) AndAlso cboCommendObj.SelectedValue <> "" Then
                Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, cboCommendObj.SelectedValue)
                If signer.Rows.Count > 0 Then
                    If IsNumeric(signer.Rows(0)("ID")) Then
                        hidSignID.Value = CDec(signer.Rows(0)("ID"))
                    End If
                    txtSignerName.Text = signer.Rows(0)("EMPLOYEE_NAME")
                    txtSignerTitle.Text = signer.Rows(0)("TITLE_NAME")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Sub AutoCreate_DecisionNo()
        Dim store As New ProfileStoreProcedure
        Try

            If rdEffectDate.SelectedDate Is Nothing Then
                Exit Sub
            End If

            If cboYear.SelectedValue = "" Then
                Exit Sub
            End If

            ClearControlValue(txtDecisionNo)
            Dim contract_no = store.AUTOCREATE_COMMENDNO(LogHelper.CurrentUser.EMPLOYEE_ID,
                                                         rdEffectDate.SelectedDate)

            txtDecisionNo.Text = contract_no
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class