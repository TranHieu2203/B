Imports System.Web.Script.Serialization
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffWebAppResources.My.Resources
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_WageNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindFrameSalaryPopup As ctrlFindFrameSalaryPopup
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
    Property Working As WorkingDTO
        Get
            Return ViewState(Me.ID & "_Working")
        End Get
        Set(ByVal value As WorkingDTO)
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
    Public Property List_FrameSalary_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_FrameSalary_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_FrameSalary_ID")
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
            'lay gia tri cac variable mac dinh
            LoadPercentDefault()
            If CommonConfig.APP_SETTING_18() Then
                Label_Signer.Visible = False
                txtSignName.Display = False
                btnFindSign.Visible = False
                Label_title.Visible = False
                txtSignTitle.Display = False
            End If

            If CommonConfig.APP_SETTING_18() Then
                cbSalaryGroup.Visible = False
                cbSalaryLevel.Visible = False
                cbSalaryRank.Visible = False
                lbSalaryGroup.Visible = False
                lbSalaryLevel.Visible = False
                lbSalaryRank.Visible = False
            End If
            'If LogHelper.GetUserLog().Username.ToUpper <> "ADMIN" Then
            '    hide1.Visible = False
            '    hide2.Visible = False
            '    hide3.Visible = False
            'End If
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
            rgAllow.AllowSorting = False
            'rgAllowCur.AllowSorting = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(LeftPane)
            '    GirdConfig(rgAllow)
            'End If
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
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList(OtherTypes.TaxTable)
                FillRadCombobox(cboTaxTable, dtData, "NAME", "ID", True)

                dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)

                dtData = rep.GetOtherList(OtherTypes.EmployeeType, True)
                FillRadCombobox(cboEmployeeType, dtData, "NAME", "ID", True)
                Dim item As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                item.Text = "85"
                item.Value = "85"
                cboPercentSalary.Items.Add(item)
                item2.Text = "90"
                item2.Value = "90"
                cboPercentSalary.Items.Add(item2)
                item3.Text = "95"
                item3.Value = "95"
                cboPercentSalary.Items.Add(item3)
                item4.Text = "100"
                item4.Value = "100"
                cboPercentSalary.Items.Add(item4)
            End Using
            Using rep As New ProfileRepository
                dtData = rep.GetSalaryGroupCombo(Date.Now, True)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryGroup, dtData, "NAME", "ID", True)
                End If
            End Using
            dtSalaryGroup = dtData
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
                    Working = rep.GetWorkingByID(Working)
                    hidID.Value = Working.ID.ToString
                    hidEmp.Value = Working.EMPLOYEE_ID
                    EmployeeID = hidEmp.Value
                    txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                    txtEmployeeName.Text = Working.EMPLOYEE_NAME
                    hidTitle.Value = Working.TITLE_ID
                    txtTitleName.Text = Working.TITLE_NAME
                    'txtTitleGroup.Text = Working.TITLE_GROUP_NAME
                    hidOrg.Value = Working.ORG_ID
                    txtOrgName.Text = Working.ORG_NAME
                    If Working.STATUS_ID IsNot Nothing Then
                        If Working.STATUS_ID = 447 Then
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If
                        cboStatus.SelectedValue = Working.STATUS_ID
                        cboStatus.Text = Working.STATUS_NAME
                    End If
                    If Working.SAL_TYPE_ID IsNot Nothing Then
                        cboSalTYPE.SelectedValue = Working.SAL_TYPE_ID
                        cboSalTYPE.Text = Working.SAL_TYPE_NAME
                    End If
                    If Working.SAL_PAYMENT_ID IsNot Nothing Then
                        cboSalPayment.SelectedValue = Working.SAL_PAYMENT_ID
                        cboSalPayment.Text = Working.SAL_PAYMENT_NAME
                    End If

                    If Working.SALE_COMMISION_ID IsNot Nothing Then
                        cboSaleCommision.SelectedValue = Working.SALE_COMMISION_ID
                        cboSaleCommision.Text = Working.SALE_COMMISION_NAME
                    End If

                    rdEffectDate.SelectedDate = Working.EFFECT_DATE
                    rdExpireDate.SelectedDate = Working.EXPIRE_DATE
                    txtDecisionNo.Text = Working.DECISION_NO
                    If Working.SAL_GROUP_ID IsNot Nothing Then
                        cbSalaryGroup.SelectedValue = Working.SAL_GROUP_ID
                        cbSalaryGroup.Text = Working.SAL_GROUP_NAME
                        dtData = profileRep.GetSalaryLevelCombo(Working.SAL_GROUP_ID, True)
                        FillRadCombobox(cbSalaryLevel, dtData, "NAME", "ID", True)
                    End If
                    If Working.SAL_LEVEL_ID IsNot Nothing Then
                        cbSalaryLevel.SelectedValue = Working.SAL_LEVEL_ID
                        cbSalaryLevel.Text = Working.SAL_LEVEL_NAME
                        dtData = profileRep.GetSalaryRankCombo(Working.SAL_LEVEL_ID, True)
                        FillRadCombobox(cbSalaryRank, dtData, "NAME", "ID", True)
                    End If
                    If Working.SAL_RANK_ID IsNot Nothing Then
                        cbSalaryRank.SelectedValue = Working.SAL_RANK_ID
                        cbSalaryRank.Text = Working.SAL_RANK_NAME
                    End If
                    If IsNumeric(Working.PERCENTSALARY) Then
                        cboPercentSalary.SelectedValue = Working.PERCENTSALARY
                    End If
                    If IsNumeric(Working.FACTORSALARY) Then
                        rnFactorSalary.Text = Working.FACTORSALARY.ToString
                    End If
                    If IsNumeric(Working.OTHERSALARY1) Then
                        rnOtherSalary1.Visible = True
                        lbOtherSalary1.Visible = True
                        tdOtherSalary1.Visible = True
                        tdOtherSalary11.Visible = True
                        rnOtherSalary1.Value = Working.OTHERSALARY1
                    Else
                        rnOtherSalary1.Visible = False
                        lbOtherSalary1.Visible = False
                        tdOtherSalary1.Visible = False
                        tdOtherSalary11.Visible = False
                    End If
                    If IsNumeric(Working.OTHERSALARY2) Then
                        rnOtherSalary2.Value = Working.OTHERSALARY2
                    End If
                    If IsNumeric(Working.OTHERSALARY3) Then
                        rnOtherSalary3.Value = Working.OTHERSALARY3
                    End If
                    If IsNumeric(Working.OTHERSALARY4) Then
                        rnOtherSalary4.Value = Working.OTHERSALARY4
                    End If
                    If IsNumeric(Working.OTHERSALARY5) Then
                        rnOtherSalary5.Value = Working.OTHERSALARY5
                    End If
                    txtUploadFile.Text = Working.ATTACH_FILE
                    txtUpload.Text = Working.FILENAME
                    If Working.STAFF_RANK_ID IsNot Nothing Then
                        hidStaffRank.Value = Working.STAFF_RANK_ID
                    End If
                    If IsDate(Working.SIGN_DATE) Then
                        rdSignDate.SelectedDate = Working.SIGN_DATE
                    End If

                    If Working.SIGN_ID IsNot Nothing Then
                        hidSign.Value = Working.SIGN_ID
                    End If
                    txtSignName.Text = Working.SIGN_NAME
                    txtSignTitle.Text = Working.SIGN_TITLE
                    txtRemark.Text = Working.REMARK
                    lstAllow = Working.lstAllowance
                    rgAllow.Rebind()
                    If Working.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                        Working.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        EnableControlAll_Cus(False, LeftPane)
                        EnableControlAll(False, rnPhone, rnShareSal, rnMinSal, rnGas)
                        '  MainToolBar.Items(0).Enabled = False
                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True
                    End If

                    If Working.EMPLOYEE_TYPE IsNot Nothing Then
                        cboEmployeeType.SelectedValue = Working.EMPLOYEE_TYPE
                        cboEmployeeType.Text = Working.EMPLOYEE_TYPE_NAME
                    End If
                    If Working.TAX_TABLE_ID IsNot Nothing Then
                        cboTaxTable.SelectedValue = Working.TAX_TABLE_ID
                        cboTaxTable.Text = Working.TAX_TABLE_Name
                    End If
                    If Working.SAL_BASIC IsNot Nothing Then
                        basicSalary.Value = Working.SAL_BASIC
                        'basicSalary.Text = Working.SAL_BASIC
                        basicSal = Working.SAL_BASIC
                    End If
                    If Working.SALARY_BHXH IsNot Nothing Then
                        rdSalaryBHXH.Value = Working.SALARY_BHXH
                        'rdSalaryBHXH.Text = Working.SALARY_BHXH
                    End If
                    If Working.SAL_INS IsNot Nothing Then
                        SalaryInsurance.Text = Working.SAL_INS
                    End If
                    If Working.ALLOWANCE_TOTAL IsNot Nothing Then
                        cboAllowance_Total.Text = Working.ALLOWANCE_TOTAL
                    End If
                    If Working.SAL_TOTAL IsNot Nothing Then
                        Salary_Total.Value = Working.SAL_TOTAL
                        total = Working.SAL_TOTAL
                    End If
                    cbSalaryGroup.Enabled = False
                    cbSalaryLevel.Enabled = False
                    cbSalaryRank.Enabled = False
                    rnFactorSalary.Enabled = True
                    'basicSalary.Enabled = False
                    'rnPercentSalary.Enabled = False
                    Salary_Total.Enabled = False
                    'rnOtherSalary1.Enabled = False
                    'GetDATA_IN()
                    'CalculatorSalary()
                    If IsNumeric(Working.TOXIC_RATE) Then
                        rnToxic_rate.Value = Working.TOXIC_RATE
                    End If
                    If IsNumeric(Working.TOXIC_SALARY) Then
                        rnToxic_salary.Value = Working.TOXIC_SALARY
                    End If
                    If IsDate(Working.NEXTSALARY_DATE) Then
                        rdCOEFFICIENT.SelectedDate = Working.NEXTSALARY_DATE
                    End If
                    rtSAL_RANK_ID.Text = Working.SAL_RANK_NAME

                    If IsNumeric(Working.REGION_SAL) Then
                        rnMinSal.Value = Working.REGION_SAL
                    End If
                    If IsNumeric(Working.GAS_SAL) Then
                        rnGas.Visible = True
                        lbGas.Visible = True
                        tdGas.Visible = True
                        tdGas1.Visible = True
                        rnGas.Value = Working.GAS_SAL
                    Else
                        lbGas.Visible = False
                        rnGas.Visible = False
                        tdGas.Visible = False
                        tdGas1.Visible = False
                    End If
                    If IsNumeric(Working.ADDITIONAL_SAL) Then
                        rnAddtionalSal.Visible = True
                        lbAddtionalSal.Visible = True
                        tdAddtionalSal.Visible = True
                        tdAddtionalSal1.Visible = True
                        rnAddtionalSal.Value = Working.ADDITIONAL_SAL
                    Else
                        rnAddtionalSal.Visible = False
                        lbAddtionalSal.Visible = False
                        tdAddtionalSal.Visible = False
                        tdAddtionalSal1.Visible = False
                    End If
                    If IsNumeric(Working.PHONE_SAL) Then
                        rnPhone.Visible = True
                        lbPhone.Visible = True
                        tdPhone.Visible = True
                        tdPhone1.Visible = True
                        rnPhone.Value = Working.PHONE_SAL
                    Else
                        rnPhone.Visible = False
                        lbPhone.Visible = False
                        tdPhone.Visible = False
                        tdPhone1.Visible = False
                    End If
                    If IsNumeric(Working.SHARE_SAL) Then
                        rnShareSal.Value = Working.SHARE_SAL
                    End If
                    If IsNumeric(Working.SAL_RANK_ID) Then
                        HidFrameSalary.Value = Working.SAL_RANK_ID
                        Dim LstrankObj = comRep.GetFrameSalaryAll()
                        Dim item = (From p In LstrankObj Where p.ID = Working.SAL_RANK_ID).FirstOrDefault
                        If item IsNot Nothing Then
                            lbInfo.Text = item.DESCRIPTION_PATH.ToString.Substring(item.DESCRIPTION_PATH.ToString.IndexOf(";") + 1).Replace(";", "-->") + " - " + "Hệ số " + item.COEFFICIENT.ToString
                        End If
                    End If

                    If IsNumeric(Working.COEFFICIENT) Then
                        hidFrameSalaryRank.Value = Working.COEFFICIENT
                        rtSAL_RANK_ID.Text = rtSAL_RANK_ID.Text & " - hệ số " & Working.SAL_RANK_NAME
                    End If

                    If IsNumeric(Working.PC1) Then
                        rnPC1.Visible = True
                        lbPC1.Visible = True
                        tdPC1.Visible = True
                        tdPC11.Visible = True
                        rnPC1.Value = Working.PC1
                    Else
                        rnPC1.Visible = False
                        lbPC1.Visible = False
                        tdPC1.Visible = False
                        tdPC11.Visible = False
                    End If
                    If IsNumeric(Working.PC2) Then
                        rnPC2.Visible = True
                        lbPC2.Visible = True
                        tdPC2.Visible = True
                        tdPC21.Visible = True
                        rnPC2.Value = Working.PC2
                    Else
                        rnPC2.Visible = False
                        lbPC2.Visible = False
                        tdPC2.Visible = False
                        tdPC21.Visible = False
                    End If
                    If IsNumeric(Working.PC3) Then
                        rnPC3.Visible = True
                        lbPC3.Visible = True
                        tdPC3.Visible = True
                        tdPC31.Visible = True
                        rnPC3.Value = Working.PC3
                    Else
                        rnPC3.Visible = False
                        lbPC3.Visible = False
                        tdPC3.Visible = False
                        tdPC31.Visible = False
                    End If
                    If IsNumeric(Working.PC4) Then
                        rnPC4.Visible = True
                        lbPC4.Visible = True
                        tdPC4.Visible = True
                        tdPC41.Visible = True
                        rnPC4.Value = Working.PC4
                    Else
                        rnPC4.Visible = False
                        lbPC4.Visible = False
                        tdPC4.Visible = False
                        tdPC41.Visible = False
                    End If
                    If IsNumeric(Working.PC5) Then
                        rnPC5.Visible = True
                        lbPC5.Visible = True
                        tdPC5.Visible = True
                        tdPC51.Visible = True
                        rnPC5.Value = Working.PC5
                    Else
                        rnPC5.Visible = False
                        lbPC5.Visible = False
                        tdPC5.Visible = False
                        tdPC51.Visible = False
                    End If
                    chkIS_ALLOW_SALARY_LESS_THAN.Checked = Working.IS_ALLOW_SALARY_LESS_THAN
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    cboStatus.SelectedIndex = 1
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/SalaryInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUpload.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUpload.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownloadOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/SalaryInfo/" + txtUploadFile.Text + txtUpload.Text)
                'bCheck = True
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub cbSalaryRank_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbSalaryRank.SelectedIndexChanged
        Try
            Using rep As New ProfileRepository
                Dim dtdata As DataTable = New DataTable()
                If IsNumeric(cbSalaryLevel.SelectedValue) Then
                    dtdata = rep.GetSalaryRankList(cbSalaryLevel.SelectedValue, True)
                    Dim row = dtdata.Select("ID='" + If(cbSalaryRank.SelectedValue = "", 0, cbSalaryRank.SelectedValue) + "'")(0)
                    If row IsNot Nothing And Not CType(CommonConfig.dicConfig("APP_SETTING"), Boolean) Then
                        rnFactorSalary.Text = row("SALARY_BASIC").ToString
                    End If
                    If row IsNot Nothing And CType(CommonConfig.dicConfig("APP_SETTING"), Boolean) Then
                        rnFactorSalary.Text = row("HE_SO").ToString
                    End If
                    'If row IsNot Nothing And Not CType(CommonConfig.dicConfig("APP_SETTING"), Boolean) Then
                    '    basicSalary.Text = CDec(Val(row("SALARY_BASIC")))
                    '    basicSalary.Text = row("SALARY_BASIC").ToString
                    '    rdSalaryBHXH.Text = CDec(Val(row("SALARY_BASIC")))
                    '    If rnPercentSalary.Text <> "" Then
                    '        Salary_Total.Text = CDec(Val(row("SALARY_BASIC"))) * CDec(Val(rnPercentSalary.Text)) / 100
                    '    End If
                    'End If
                End If
            End Using
            CalculatorSalary()
            basicSalary.AutoPostBack = True
            'ClearControlValue(rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cbSalaryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbSalaryGroup.SelectedIndexChanged
        Try
            Dim dtData As DataTable = New DataTable()
            ClearControlValue(cbSalaryLevel, cbSalaryRank)
            Using rep As New ProfileRepository
                If IsNumeric(cbSalaryGroup.SelectedValue) Then
                    dtData = rep.GetSalaryLevelCombo(cbSalaryGroup.SelectedValue, True)
                End If
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryLevel, dtData, "NAME", "ID", True)
                End If
            End Using
            CalculatorSalary()
            'ClearControlValue(cbSalaryLevel, cbSalaryRank, rnFactorSalary, SalaryInsurance, basicSalary, Salary_Total, rnOtherSalary1, _
            '                  rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cbSalaryLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbSalaryLevel.SelectedIndexChanged
        Try
            Dim dtData As DataTable = New DataTable()
            ClearControlValue(cbSalaryRank)
            Using rep As New ProfileRepository
                If IsNumeric(cbSalaryLevel.SelectedValue) Then
                    dtData = rep.GetSalaryRankCombo(cbSalaryLevel.SelectedValue, True)
                End If
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryRank, dtData, "NAME", "ID", True)
                End If
            End Using
            'ClearControlValue(rnFactorSalary, cbSalaryRank, SalaryInsurance, basicSalary, Salary_Total, rnOtherSalary1, _
            '                  rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Event click item cua menu toolbar
    ''' Check validate page khi an luu
    ''' Redirect ve trang Quan ly ho so luong khi an huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim rep As New ProfileBusinessRepository
        Dim store As New ProfileStoreProcedure
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
                        Dim INFO_SALARY = store.GET_INFO_SALARYITEMSPERCENT(hidOrg.Value, rdEffectDate.SelectedDate)
                        If INFO_SALARY.Rows.Count <= 0 Then
                            ShowMessage(Translate(" Đơn vị của nhân viên chưa được thiết lập cơ cấu lương, vui lòng thiết lập trước khi thêm Hồ sơ lương"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim factorSal As Decimal = 0
                        'If cboSalTYPE.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn đối tượng lương"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn Ngày hiệu lực"), NotifyType.Warning)
                            rdEffectDate.Focus()
                            Exit Sub
                        End If
                        'If cboStatus.SelectedValue = 447 Then
                        '    If txtUpload.Text = "" Then
                        '        ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If

                        If cboPercentSalary.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn % hưởng lương"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If cboSalTYPE.Text = "Kiêm nhiệm" Then
                            If rnOtherSalary2.Text = "" Then
                                ShowMessage(Translate("Bạn phải nhập mức phụ cấp kiêm nhiệm"), NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            'If basicSalary.Text <> "" Then
                            '    ShowMessage(Translate("Bạn phải nhập lương cơ bản"), NotifyType.Warning)
                            '    Exit Sub
                            'End If
                        End If
                        'factorSal = If(IsNumeric(rnFactorSalary.Text), Decimal.Parse(rnFactorSalary.Text), 0)
                        'If factorSal <= 0 Then
                        '    ShowMessage(Translate("Hệ số/Mức thưởng phải lớn hơn 0"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If cboSalTYPE.Text = "Chính thức" Then
                        '    If rnPercentSalary.Value < 100 Then
                        '        ShowMessage(Translate("nhập % hưởng lương 'chính thức' > 100%"), NotifyType.Warning)
                        '        'rnPercentSalary.Value = 100
                        '        rnPercentSalary.Focus()
                        '        Exit Sub
                        '    End If
                        'End If
                        If cboSalTYPE.Text = "Thử việc" Then
                            If CDec(cboPercentSalary.SelectedValue) < 85 Then
                                ShowMessage(Translate("nhập % hưởng lương 'thử việc' > 85%"), NotifyType.Warning)
                                'rnPercentSalary.Value = 85
                                cboPercentSalary.Focus()
                                Exit Sub
                            End If
                        End If

                        'Dim EmpTypeCode As String = rep.GET_CODE_EMP_TYPE(cboEmployeeType.SelectedValue)

                        'If EmpTypeCode = "CT" Then
                        '    Dim minAmount As Decimal = rep.GET_REGION_BY_DATE(hidEmp.Value, rdEffectDate.SelectedDate)
                        If rdSalaryBHXH.Value IsNot Nothing And rnMinSal.Value IsNot Nothing Then
                            If Not chkIS_ALLOW_SALARY_LESS_THAN.Checked And rdSalaryBHXH.Value < rnMinSal.Value Then
                                ShowMessage(Translate("Lương đóng BHXH phải lớn hơn hoặc bằng lương tối thiểu vùng bảo hiểm"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        'End If

                        'If EmpTypeCode = "CT" AndAlso rnPercentSalary.Value <> 100 Then
                        '    ctrlMessageBox.MessageText = Translate("Phần trăm hưởng lương chính thức # 100, bạn có chắc chắn không?")
                        '    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CHECK
                        '    ctrlMessageBox.DataBind()
                        '    ctrlMessageBox.Show()
                        '    Exit Sub
                        'End If
                        If If(basicSalary.Value Is Nothing, 0, basicSalary.Value) = If(rdSalaryBHXH.Value Is Nothing, 0, rdSalaryBHXH.Value) +
                                 If(rnGas.Value Is Nothing, 0, rnGas.Value) +
                                 If(rnAddtionalSal.Value Is Nothing, 0, rnAddtionalSal.Value) +
                                 If(rnPhone.Value Is Nothing, 0, rnPhone.Value) +
                                 If(rnOtherSalary1.Value Is Nothing, 0, rnOtherSalary1.Value) +
                                 If(rnPC1.Value Is Nothing, 0, rnPC1.Value) +
                                 If(rnPC2.Value Is Nothing, 0, rnPC2.Value) +
                                 If(rnPC3.Value Is Nothing, 0, rnPC3.Value) +
                                 If(rnPC4.Value Is Nothing, 0, rnPC4.Value) +
                                 If(rnPC5.Value Is Nothing, 0, rnPC5.Value) Then
                        Else
                            ShowMessage(Translate("Tổng các khoản lương đang không bằng Lương thỏa thuận, vui lòng kiểm tra lại trước khi Lưu"), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim gID As Decimal
                        With objWorking
                            .EMPLOYEE_ID = hidEmp.Value
                            .TITLE_ID = hidTitle.Value
                            .ORG_ID = hidOrg.Value
                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = cboStatus.SelectedValue
                            End If
                            .EFFECT_DATE = rdEffectDate.SelectedDate
                            .EXPIRE_DATE = rdExpireDate.SelectedDate
                            .DECISION_NO = txtDecisionNo.Text
                            .CODE_ATTENDANCE = code_attendent
                            If cboSalTYPE.SelectedValue <> "" Then
                                .SAL_TYPE_ID = cboSalTYPE.SelectedValue
                            End If
                            If cboSalPayment.SelectedValue <> "" Then
                                .SAL_PAYMENT_ID = cboSalPayment.SelectedValue
                            End If

                            If cboSaleCommision.SelectedValue <> "" Then
                                .SALE_COMMISION_ID = cboSaleCommision.SelectedValue
                            End If

                            If cbSalaryGroup.SelectedValue <> "" Then
                                .SAL_GROUP_ID = cbSalaryGroup.SelectedValue
                            End If
                            If cbSalaryLevel.SelectedValue <> "" Then
                                .SAL_LEVEL_ID = cbSalaryLevel.SelectedValue
                            End If
                            'If cbSalaryRank.SelectedValue <> "" Then
                            '    .SAL_RANK_ID = cbSalaryRank.SelectedValue
                            'End If
                            If IsNumeric(HidFrameSalary.Value) Then
                                .SAL_RANK_ID = HidFrameSalary.Value
                            End If
                            If hidStaffRank.Value <> "" Then
                                .STAFF_RANK_ID = hidStaffRank.Value
                            End If
                            If cboPercentSalary.SelectedValue <> "" Then
                                .PERCENTSALARY = cboPercentSalary.SelectedValue
                            End If
                            If rnFactorSalary.Text.Contains(".") Then
                                factorSal = rnFactorSalary.Text.Replace(".", ",").ToString
                                .FACTORSALARY = If(IsNumeric(factorSal), Decimal.Parse(factorSal), Nothing)
                            Else
                                .FACTORSALARY = If(IsNumeric(rnFactorSalary.Text), Decimal.Parse(rnFactorSalary.Text), Nothing)
                            End If
                            'If IsNumeric(rnFactorSalary.Value) Then
                            '    .FACTORSALARY = rnFactorSalary.Value
                            'End If
                            If IsNumeric(rnOtherSalary1.Value) Then
                                .OTHERSALARY1 = rnOtherSalary1.Value
                            End If
                            If IsNumeric(rnOtherSalary2.Value) Then
                                .OTHERSALARY2 = rnOtherSalary2.Value
                            End If
                            If IsNumeric(rnOtherSalary3.Value) Then
                                .OTHERSALARY3 = rnOtherSalary3.Value
                            End If
                            If IsNumeric(rnOtherSalary4.Value) Then
                                .OTHERSALARY4 = rnOtherSalary4.Value
                            End If
                            If IsNumeric(rnOtherSalary5.Value) Then
                                .OTHERSALARY5 = rnOtherSalary5.Value
                            End If
                            If IsNumeric(rnMinSal.Value) Then
                                .REGION_SAL = rnMinSal.Value
                            End If
                            If IsNumeric(rnGas.Value) Then
                                .GAS_SAL = rnGas.Value
                            End If
                            If IsNumeric(rnAddtionalSal.Value) Then
                                .ADDITIONAL_SAL = rnAddtionalSal.Value
                            End If
                            If IsNumeric(rnPhone.Value) Then
                                .PHONE_SAL = rnPhone.Value
                            End If
                            If IsNumeric(rnShareSal.Value) Then
                                .SHARE_SAL = rnShareSal.Value
                            End If
                            .SAL_BASIC = basicSalary.Value
                            .SALARY_BHXH = rdSalaryBHXH.Value
                            .SIGN_DATE = rdSignDate.SelectedDate
                            .ATTACH_FILE = txtUploadFile.Text 'Guid directory
                            .FILENAME = txtUpload.Text 'ten file (a.jpg)
                            If hidSign.Value <> "" Then
                                .SIGN_ID = hidSign.Value
                            End If
                            .SIGN_NAME = txtSignName.Text
                            .SIGN_TITLE = txtSignTitle.Text
                            .REMARK = txtRemark.Text
                            .IS_PROCESS = True
                            .IS_MISSION = False
                            .IS_WAGE = True
                            .IS_3B = False
                            '.PERCENT_SALARY = rntxtPercentSalary.Value
                            Dim isError As Boolean = False
                            For Each item As GridDataItem In rgAllow.Items
                                Dim allow = New WorkingAllowanceDTO
                                allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                                allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                                allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                                allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                                allow.EFFECT_DATE = rdEffectDate.SelectedDate
                                allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                                If allow.EXPIRE_DATE IsNot Nothing Then
                                    If allow.EXPIRE_DATE <= rdEffectDate.SelectedDate Then
                                        isError = True
                                    End If
                                End If
                                lstAllow.Add(allow)
                                .ALLOWANCE_TOTAL = .ALLOWANCE_TOTAL + allow.AMOUNT
                            Next
                            If isError Then
                                ShowMessage("Ngày kết thúc phụ cấp phải lớn hơn Ngày hiệu lực tờ trình", NotifyType.Warning)
                                rgAllow.Rebind()
                                Exit Sub
                            End If
                            .lstAllowance = lstAllow
                            .SAL_TOTAL = Salary_Total.Value
                            .TAX_TABLE_ID = Decimal.Parse(cboTaxTable.SelectedValue)
                            .EMPLOYEE_TYPE = CDec(Val(cboEmployeeType.SelectedValue))
                            .SAL_INS = SalaryInsurance.Value
                            .TOXIC_RATE = rnToxic_rate.Value
                            .TOXIC_SALARY = rnToxic_salary.Value
                            .NEXTSALARY_DATE = rdCOEFFICIENT.SelectedDate
                            If IsNumeric(rnPC1.Value) Then
                                .PC1 = CDec(rnPC1.Value)
                            End If
                            If IsNumeric(rnPC2.Value) Then
                                .PC2 = CDec(rnPC2.Value)
                            End If
                            If IsNumeric(rnPC3.Value) Then
                                .PC3 = CDec(rnPC3.Value)
                            End If
                            If IsNumeric(rnPC4.Value) Then
                                .PC4 = CDec(rnPC4.Value)
                            End If
                            If IsNumeric(rnPC5.Value) Then
                                .PC5 = CDec(rnPC5.Value)
                            End If
                            .IS_ALLOW_SALARY_LESS_THAN = chkIS_ALLOW_SALARY_LESS_THAN.Checked

                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If Not ValidateDecisionNo(objWorking) Then

                                    ShowMessage(Translate("Ngày hiệu lực bị trùng"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertWorking(objWorking, gID) Then
                                    'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    'If (isPopup) Then
                                    '    Response.Redirect("/Dialog.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&empID=" & hidEmp.Value)
                                    'Else
                                    '    Session("Result") = 1
                                    '    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
                                    'End If
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWorking.ID = Decimal.Parse(hidID.Value)
                                If Not ValidateDecisionNo(objWorking) Then

                                    ShowMessage(Translate("Ngày hiệu lực bị trùng"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyWorking(objWorking, gID) Then
                                    ''POPUPTOLINK
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")

                                    'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
                                    End If
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
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
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
                Dim objWorking As New WorkingDTO
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
                    If cboStatus.SelectedValue <> "" Then
                        .STATUS_ID = cboStatus.SelectedValue
                    End If
                    .EFFECT_DATE = rdEffectDate.SelectedDate
                    .EXPIRE_DATE = rdExpireDate.SelectedDate
                    .DECISION_NO = txtDecisionNo.Text
                    .CODE_ATTENDANCE = code_attendent
                    If cboSalTYPE.SelectedValue <> "" Then
                        .SAL_TYPE_ID = cboSalTYPE.SelectedValue
                    End If
                    If cboSalPayment.SelectedValue <> "" Then
                        .SAL_PAYMENT_ID = cboSalPayment.SelectedValue
                    End If

                    If cboSaleCommision.SelectedValue <> "" Then
                        .SALE_COMMISION_ID = cboSaleCommision.SelectedValue
                    End If

                    If cbSalaryGroup.SelectedValue <> "" Then
                        .SAL_GROUP_ID = cbSalaryGroup.SelectedValue
                    End If
                    If cbSalaryLevel.SelectedValue <> "" Then
                        .SAL_LEVEL_ID = cbSalaryLevel.SelectedValue
                    End If
                    If cbSalaryRank.SelectedValue <> "" Then
                        .SAL_RANK_ID = cbSalaryRank.SelectedValue
                    End If
                    If hidStaffRank.Value <> "" Then
                        .STAFF_RANK_ID = hidStaffRank.Value
                    End If
                    If cboPercentSalary.SelectedValue <> "" Then
                        .PERCENTSALARY = cboPercentSalary.SelectedValue
                    End If
                    If rnFactorSalary.Text.Contains(".") Then
                        factorSal = rnFactorSalary.Text.Replace(".", ",").ToString
                        .FACTORSALARY = If(IsNumeric(factorSal), Decimal.Parse(factorSal), Nothing)
                    Else
                        .FACTORSALARY = If(IsNumeric(rnFactorSalary.Text), Decimal.Parse(rnFactorSalary.Text), Nothing)
                    End If
                    'If IsNumeric(rnFactorSalary.Value) Then
                    '    .FACTORSALARY = rnFactorSalary.Value
                    'End If
                    If IsNumeric(rnOtherSalary1.Value) Then
                        .OTHERSALARY1 = rnOtherSalary1.Value
                    End If
                    If IsNumeric(rnOtherSalary2.Value) Then
                        .OTHERSALARY2 = rnOtherSalary2.Value
                    End If
                    If IsNumeric(rnOtherSalary3.Value) Then
                        .OTHERSALARY3 = rnOtherSalary3.Value
                    End If
                    If IsNumeric(rnOtherSalary4.Value) Then
                        .OTHERSALARY4 = rnOtherSalary4.Value
                    End If
                    If IsNumeric(rnOtherSalary5.Value) Then
                        .OTHERSALARY5 = rnOtherSalary5.Value
                    End If
                    .SAL_BASIC = basicSalary.Value
                    .SALARY_BHXH = rdSalaryBHXH.Value
                    .SIGN_DATE = rdSignDate.SelectedDate
                    .ATTACH_FILE = txtUploadFile.Text 'Guid directory
                    .FILENAME = txtUpload.Text 'ten file (a.jpg)
                    If hidSign.Value <> "" Then
                        .SIGN_ID = hidSign.Value
                    End If
                    .SIGN_NAME = txtSignName.Text
                    .SIGN_TITLE = txtSignTitle.Text
                    .REMARK = txtRemark.Text
                    .IS_PROCESS = True
                    .IS_MISSION = False
                    .IS_WAGE = True
                    .IS_3B = False
                    '.PERCENT_SALARY = rntxtPercentSalary.Value
                    Dim isError As Boolean = False
                    For Each item As GridDataItem In rgAllow.Items
                        Dim allow = New WorkingAllowanceDTO
                        allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                        allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                        allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                        allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                        allow.EFFECT_DATE = rdEffectDate.SelectedDate
                        allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                        If allow.EXPIRE_DATE IsNot Nothing Then
                            If allow.EXPIRE_DATE <= rdEffectDate.SelectedDate Then
                                isError = True
                            End If
                        End If
                        lstAllow.Add(allow)
                        .ALLOWANCE_TOTAL = .ALLOWANCE_TOTAL + allow.AMOUNT
                    Next
                    If isError Then
                        ShowMessage("Ngày kết thúc phụ cấp phải lớn hơn Ngày hiệu lực tờ trình", NotifyType.Warning)
                        rgAllow.Rebind()
                        Exit Sub
                    End If
                    .lstAllowance = lstAllow
                    .SAL_TOTAL = Salary_Total.Value
                    .TAX_TABLE_ID = Decimal.Parse(cboTaxTable.SelectedValue)
                    .EMPLOYEE_TYPE = CDec(Val(cboEmployeeType.SelectedValue))
                    .SAL_INS = SalaryInsurance.Value
                End With

                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If Not ValidateDecisionNo(objWorking) Then
                            ShowMessage(Translate("Ngày hiệu lực bị trùng"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rep.InsertWorking(objWorking, gID) Then
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
                        If rep.ModifyWorking(objWorking, gID) Then
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_CHECK And e.ButtonID = MessageBoxButtonType.ButtonNo Then
                cboPercentSalary.SelectedValue = 100

                If cboPercentSalary.SelectedValue <> "" Then
                    If cboSalTYPE.Text = "Kiêm nhiệm" Then
                        total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                               If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                               If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                        Salary_Total.Value = total
                    Else
                        Salary_Total.Value = (basicSalary.Value +
                            If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    End If
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
                                btnFindEmployee.Click,
                                btnFindSign.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
                Case btnFindSign.ID
                    isLoadPopup = 2
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                    ctrlFindEmployeePopup.Show()
                Case btnFindSign.ID
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = False
                    ctrlFindSigner.IsOnlyWorkingWithoutTer = True
                    ctrlFindSigner.EmployeeOrg = If(hidOrg.Value <> "", CDec(hidOrg.Value), 0)
                    ctrlFindSigner.EffectDate = If(rdEffectDate.SelectedDate IsNot Nothing, CDbl(rdEffectDate.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    ctrlFindSigner.Show()
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
                                 ctrlFindFrameSalaryPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub btnFindFrameSalary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindFrameSalary.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            If sender IsNot Nothing Then
                List_FrameSalary_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindFrameSalaryPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.FrameSalarySelectedEventArgs) Handles ctrlFindFrameSalaryPopup.FrameSalarySelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim Item = ctrlFindFrameSalaryPopup.CurrentItemDataObject
            lbInfo.Text = ""
            If Item IsNot Nothing Then
                If Item.IS_LEVEL3 Then
                    HidFrameSalary.Value = e.CurrentValue
                    hidFrameSalaryRank.Value = Item.COEFFICIENT
                    rtSAL_RANK_ID.Text = Item.NAME_VN & " - hệ số " & Item.COEFFICIENT
                    If IsDate(rdEffectDate.SelectedDate) AndAlso IsNumeric(Item.COEFFICIENT) Then
                        Dim newDate As DateTime = CDate(rdEffectDate.SelectedDate).AddMonths(CDec(Item.PROMOTE_MONTH))
                        rdCOEFFICIENT.SelectedDate = newDate
                    End If
                    lbInfo.Text = Item.DESCRIPTION_PATH.ToString.Substring(Item.DESCRIPTION_PATH.ToString.IndexOf(";") + 1).Replace(";", "-->") + " - " + "Hệ số " + Item.COEFFICIENT.ToString
                Else
                    HidFrameSalary.Value = Nothing
                    rtSAL_RANK_ID.Text = Nothing
                    rdEffectDate.SelectedDate = Nothing
                    ShowMessage(Translate("Vui lòng chọn khung lương Level3"), Utilities.NotifyType.Warning)

                End If


            End If
            isLoadPopup = 0
            CalTotal()
            If cboPercentSalary.SelectedValue <> "" Then
                If cboSalTYPE.Text = "Kiêm nhiệm" Then
                    total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                           If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                           If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    Salary_Total.Value = total
                Else
                    Salary_Total.Value = (basicSalary.Value +
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                End If
            End If
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
                    Dim List_FrameSalary = rep.GetFrameSalaryAll()
                    Dim FrameSalaryList = (From p In List_FrameSalary Where p.NAME_VN.ToUpper.Contains(rtSAL_RANK_ID.Text.Trim.ToUpper)).ToList
                    If FrameSalaryList.Count <= 0 Then
                        ShowMessage(Translate("Hệ số lương không tồn tại."), Utilities.NotifyType.Warning)
                        ClearControlValue(rtSAL_RANK_ID)
                    Else
                        List_FrameSalary_ID = (From p In FrameSalaryList Select p.ID).ToList
                        btnFindFrameSalary_Click(Nothing, Nothing)
                    End If
                Else
                    HidFrameSalary.Value = Nothing
                    rtSAL_RANK_ID.Text = ""

                    HidFrameSalary.Value = Nothing
                    hidFrameSalaryRank.Value = Nothing
                    rtSAL_RANK_ID.Text = ""
                End If
            Else
                HidFrameSalary.Value = Nothing
                rtSAL_RANK_ID.Text = ""

                HidFrameSalary.Value = Nothing
                hidFrameSalaryRank.Value = Nothing
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
            FillData(empID)
            isLoadPopup = 0
            ClearControlValue(cbSalaryLevel, rnFactorSalary, cbSalaryRank, SalaryInsurance, basicSalary, rdSalaryBHXH, Salary_Total, rnOtherSalary1,
                              rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5, cboPercentSalary, hidSign, txtSignName, txtSignTitle)
            FillData()
            AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click Chon ma nhan vien tu popup list nhan vien voi man hinh tim kiem signer (gia tri isLoadPopup = 2)
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindSigner.SelectedEmployee(0)
            txtSignName.Text = objEmployee.FULLNAME_VN
            txtSignTitle.Text = objEmployee.TITLE_NAME
            hidSign.Value = objEmployee.EMPLOYEE_ID

            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data cho cac combobox khi click vao combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboSalTYPE.ItemsRequested,
         cboStatus.ItemsRequested, cboAllowance.ItemsRequested, cboSaleCommision.ItemsRequested, cboSalPayment.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim dateValue As Date
                Select Case sender.ID
                    Case cboAllowance.ID
                        dtData = rep.GetHU_AllowanceList()
                        _allowDataCache.Clear()
                        For i As Integer = 0 To dtData.Rows.Count - 1
                            _allowDataCache.Add(New AllowanceListDTO With {.ID = Decimal.Parse(dtData.Rows(i)("ID")),
                                                .IS_INSURANCE = CType(dtData.Rows(i)("IS_INSURANCE"), Boolean)})
                        Next
                    Case cboSalTYPE.ID
                        ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, cboTaxTable, cboEmployeeType, rnFactorSalary, SalaryInsurance, cboAllowance_Total, basicSalary, rdSalaryBHXH,
                              Salary_Total, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5)
                        If e.Context("valueCustom") Is Nothing Then
                            dateValue = Date.Now
                        Else
                            dateValue = Date.Parse(e.Context("valueCustom"))
                        End If
                        dtData = rep.GetSalaryTypeList(dateValue, True)
                    Case cbSalaryGroup.ID
                        If e.Context("valueCustom") Is Nothing Then
                            dateValue = Date.Now
                        Else
                            dateValue = Date.Parse(e.Context("valueCustom"))
                        End If
                        dtData = rep.GetSalaryGroupList(dateValue, True)
                    Case cbSalaryLevel.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetSalaryLevelList(dValue, True)
                    Case cbSalaryRank.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetSalaryRankList(dValue, True)
                        'Case cboStatus.ID
                        '    dtData = rep.GetOtherList(OtherTypes.DecisionStatus)

                    Case cboTaxTable.ID
                        dtData = rep.GetOtherList(OtherTypes.TaxTable)
                    Case cboSalPayment.ID
                        dtData = rep.GetOtherList("SAL_PAYMENT", True)
                    Case cboSaleCommision.ID
                        dtData = rep.GetSaleCommisionList(dateValue, True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    If dtExist.Count = 0 Then
                        Dim dtFilter = (From p In dtData
                                        Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                        If dtFilter.Count > 0 Then
                            dtData = dtFilter.CopyToDataTable
                        Else
                            dtData = dtData.Clone
                        End If

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                        e.EndOfItems = endOffset = dtData.Rows.Count

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                Case cbSalaryRank.ID
                                    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    Else

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = dtData.Rows.Count
                        e.EndOfItems = True

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            sender.Items.Add(radItem)
                        Next
                    End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event them phu cap xuong grid phu cap, Xoa phu cap tren grid phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAllow_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAllow.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "InsertAllow"
                    If cboAllowance.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn phụ cấp"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rntxtAmount.Value Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập số tiền"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rdEffectDate.SelectedDate Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập Ngày hiệu lực hồ sơ lương"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rdAllowExpireDate.SelectedDate IsNot Nothing Then
                        If rdAllowExpireDate.SelectedDate <= rdEffectDate.SelectedDate Then
                            ShowMessage(Translate("Ngày kết thúc phụ cấp phải lớn hơn Ngày hiệu lực tờ trình"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    Dim lstAllowList As New List(Of Decimal)
                    For Each item As GridDataItem In rgAllow.Items
                        lstAllowList.Add(item.GetDataKeyValue("ALLOWANCE_LIST_ID"))
                    Next
                    If lstAllowList.Contains(cboAllowance.SelectedValue) Then
                        ShowMessage(Translate("Phụ cấp đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgAllow.Items
                        Dim allow = New WorkingAllowanceDTO
                        allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                        allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                        allow.AMOUNT = item.GetDataKeyValue("AMOUNT")

                        allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                        allow.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
                        allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                        lstAllow.Add(allow)
                    Next
                    Dim allow1 As WorkingAllowanceDTO
                    allow1 = New WorkingAllowanceDTO
                    allow1.ALLOWANCE_LIST_ID = cboAllowance.SelectedValue
                    allow1.ALLOWANCE_LIST_NAME = cboAllowance.Text
                    allow1.AMOUNT = rntxtAmount.Value

                    allow1.IS_INSURRANCE = chkIsInsurrance.Checked
                    If rdAllowEffectDate.SelectedDate.HasValue Then
                        allow1.EFFECT_DATE = rdAllowEffectDate.SelectedDate.Value
                    Else
                        allow1.EFFECT_DATE = rdEffectDate.SelectedDate
                    End If
                    allow1.EXPIRE_DATE = rdAllowExpireDate.SelectedDate
                    lstAllow.Add(allow1)
                    cboAllowance_Total.Value = If(cboAllowance_Total.Value Is Nothing, 0, cboAllowance_Total.Value) + allow1.AMOUNT
                    ClearControlValue(cboAllowance, rntxtAmount, chkIsInsurrance, rdAllowExpireDate)
                    CalculatorSalary()
                    rgAllow.Rebind()
                Case "DeleteAllow"
                    For Each item As GridDataItem In rgAllow.Items
                        Dim isExist As Boolean = False
                        For Each selected As GridDataItem In rgAllow.SelectedItems
                            If item.GetDataKeyValue("ALLOWANCE_LIST_ID") = selected.GetDataKeyValue("ALLOWANCE_LIST_ID") Then
                                isExist = True
                                Exit For
                            End If
                        Next
                        If Not isExist Then
                            Dim allow As New WorkingAllowanceDTO
                            allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                            allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                            allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                            allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                            allow.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
                            allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                            lstAllow.Add(allow)
                        End If
                    Next
                    ClearControlValue(cboAllowance_Total)
                    CalculatorSalary()
                    rgAllow.Rebind()
            End Select
            'RebindValue()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' load data len grid phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataAllowance(lstAllow)
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
            AutoCreate_DecisionNo()

            'If (rdEffectDate.SelectedDate IsNot Nothing) AndAlso IsNumeric(hidOrg.Value) Then
            '    ShowHideControlSal()
            '    Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, hidOrg.Value)
            '    If signer.Rows.Count > 0 Then
            '        If IsNumeric(signer.Rows(0)("ID")) Then
            '            hidSign.Value = CDec(signer.Rows(0)("ID"))
            '        End If
            '        txtSignName.Text = signer.Rows(0)("EMPLOYEE_NAME").ToString
            '        txtSignTitle.Text = signer.Rows(0)("TITLE_NAME").ToString
            '    End If
            'End If
            'Dim obj = rep1.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = hidempid1.Value})
            'If rdEffectDate.SelectedDate IsNot Nothing Then
            '    txtDecisionNo.Text = obj.EMPLOYEE_CODE + "/" + rdEffectDate.SelectedDate.Value.Month.ToString + "/" + rdEffectDate.SelectedDate.Value.Year.ToString + "/QĐL-" + obj.SHORT_NAME
            'End If
            Dim startTime As DateTime = DateTime.UtcNow
            For Each item As GridDataItem In rgAllow.Items
                Dim eff = item.GetDataKeyValue("EFFECT_DATE")
                Dim exp = item.GetDataKeyValue("EXPIRE_DATE")
                If exp IsNot Nothing Then
                    If rdEffectDate.SelectedDate > exp Then
                        item("EXPIRE_DATE").Text = ""
                    End If
                End If
            Next
            ClearControlValue(hidSign, txtSignName, txtSignTitle)
            If (rdEffectDate.SelectedDate IsNot Nothing) Then
                rdSignDate.SelectedDate = rdEffectDate.SelectedDate
                EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank)
                Dim org_id As Decimal? = Nothing
                If IsNumeric(hidOrg.Value) Then
                    org_id = hidOrg.Value
                End If
                'Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, org_id)
                'If signer.Rows.Count > 0 Then
                '    If IsNumeric(signer.Rows(0)("ID")) Then
                '        hidSign.Value = CDec(signer.Rows(0)("ID"))
                '    End If
                '    txtSignName.Text = signer.Rows(0)("EMPLOYEE_NAME").ToString
                '    txtSignTitle.Text = signer.Rows(0)("TITLE_NAME").ToString
                'End If
            Else
                EnableControlAll(False, cbSalaryGroup, cbSalaryLevel, cbSalaryRank)
            End If
            'Ngay hieu luc thay doi => load lai thang luong theo ngay hieu luc
            ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, SalaryInsurance, rnFactorSalary)
            Dim dtData As DataTable = New DataTable()
            Using rep As New ProfileRepository
                dtData = rep.GetSalaryGroupCombo(rdEffectDate.SelectedDate, True)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryGroup, dtData, "NAME", "ID", True)
                End If
            End Using
            dtSalaryGroup = dtData
            'tam thoi khoa ham nay lai,,chua hieu tai s goi o cho nay,khi nao co bug thi xem lai sau
            'CalculatorSalary()
            If Request.Params("ID") = "" Then
                ClearControlValue(rnPC1, rnPC2, rnPC3, rnPC4, rnPC5, cboSalTYPE, cboSalPayment, basicSalary, cboTaxTable, cboPercentSalary, rdSalaryBHXH, rnGas, rnAddtionalSal, rnPhone, rnOtherSalary1, rnShareSal)
            End If

            If (rdEffectDate.SelectedDate IsNot Nothing) AndAlso IsNumeric(hidOrg.Value) Then
                ShowHideControlSal()
            End If
            If rdEffectDate.SelectedDate IsNot Nothing And hidEmp.Value <> "" Then
                Dim MinAmount = store.GET_MIN_AMOUNT(hidEmp.Value, rdEffectDate.SelectedDate)
                If IsNumeric(MinAmount) AndAlso MinAmount > 0 Then
                    rnMinSal.Value = MinAmount
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub rnFactorSalary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnFactorSalary.TextChanged
        Try
            Select Case cboSalTYPE.Text
                Case "Thử việc"
                    If cboPercentSalary.SelectedValue <> "" AndAlso CDec(cboPercentSalary.SelectedValue) < 85 Then
                        cboPercentSalary.SelectedValue = 85
                        ShowMessage(Translate("Giá trị nhập không đúng qui định, vui lòng kiểm tra lại"), NotifyType.Alert)
                        Exit Sub
                    End If
                Case "Chính thức"
                    If cboPercentSalary.SelectedValue <> "" AndAlso CDec(cboPercentSalary.SelectedValue) < 100 Then
                        cboPercentSalary.SelectedValue = 100
                        ShowMessage(Translate("Giá trị nhập không đúng qui định, vui lòng kiểm tra lại"), NotifyType.Alert)
                        Exit Sub
                    End If
            End Select
            CalculatorSalary()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub basicSalary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles basicSalary.TextChanged
        Try
            'CalculatorSalary()
            If cbSalaryGroup.SelectedValue <> "" AndAlso cbSalaryRank.SelectedValue <> "" AndAlso cbSalaryLevel.SelectedValue <> "" AndAlso basicSalary.Value < basicSal Then
                ShowMessage(Translate("Không được điều chỉnh lương cơ bản nhỏ hơn hệ số nhân với lương tối thiểu vùng"), NotifyType.Warning)
                Exit Sub
            End If
            If basicSalary.Value Is Nothing Then
                ShowMessage(Translate("Bạn hãy nhập lương cơ bản"), NotifyType.Warning)
                Exit Sub
            End If

            If cboPercentSalary.SelectedValue <> "" Then
                If cboSalTYPE.Text = "Kiêm nhiệm" Then
                    total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                             If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                             If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    ' basicSalary.Enabled = True
                Else
                    total = (basicSalary.Value +
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    ' basicSalary.Enabled = False
                End If
            End If
            Salary_Total.Value = total
            'rdSalaryBHXH.Value = basicSalary.Value

            CalTotal()
            totalSalItemsPercent()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOtherSalary1_TextChanged(sender As Object, e As System.EventArgs) Handles rnOtherSalary1.TextChanged
        Try
            If cboPercentSalary.SelectedValue <> "" Then
                If cboSalTYPE.Text = "Kiêm nhiệm" Then
                    total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                           If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                           If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    Salary_Total.Value = total
                Else
                    Salary_Total.Value = (basicSalary.Value +
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rnOtherSalary2_TextChanged(sender As Object, e As System.EventArgs) Handles rnOtherSalary2.TextChanged
        Try
            If cboPercentSalary.SelectedValue <> "" Then
                If cboSalTYPE.Text = "Kiêm nhiệm" Then
                    total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                           If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                           If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    Salary_Total.Value = total
                Else
                    Salary_Total.Value = (basicSalary.Value +
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub cboPercentSalary_TextChanged(sender As Object, e As System.EventArgs) Handles cboPercentSalary.SelectedIndexChanged
    '    Try
    '        If cboPercentSalary.SelectedValue <> "" Then
    '            If cboSalTYPE.Text = "Kiêm nhiệm" Then
    '                total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
    '                       If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
    '                       If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
    '                Salary_Total.Value = total
    '            Else
    '                Salary_Total.Value = (basicSalary.Value +
    '                    If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
    '            End If
    '        End If
    '        totalSalItemsPercent()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub rnToxic_rate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnToxic_rate.TextChanged
        Try

            If Not IsNumeric(rnToxic_rate.Value) Then
                rnToxic_rate.Value = 0
            End If
            CalTotal()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rdSalaryBHXH_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdSalaryBHXH.TextChanged
        Try
            'If IsNumeric(rdSalaryBHXH.Value) AndAlso IsNumeric(basicSalary.Value) Then
            '    Dim store As New ProfileStoreProcedure
            '    Dim INFO_SALARY = store.GET_INFO_SALARYITEMSPERCENT2(hidOrg.Value, rdEffectDate.SelectedDate)
            '    Dim Salary = basicSalary.Value
            '    If INFO_SALARY.Rows.Count > 0 Then
            '        Dim INFO_SALARY2 = store.GET_INFO_SALARYITEMSPERCENT(hidOrg.Value, rdEffectDate.SelectedDate)
            '        Dim luongCB = Salary * CDec(INFO_SALARY2.Rows(0)("LUONGCB")) / 100
            '        If INFO_SALARY.Rows(0)("XANGXE") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("XANGXE").ToString = "LEFT" Then
            '            rnGas.Value = Salary * CDec(INFO_SALARY2.Rows(0)("XANGXE")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("OTHER5") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("OTHER5").ToString = "LEFT" Then
            '            rnPC5.Value = Salary * CDec(INFO_SALARY2.Rows(0)("OTHER5")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("OTHER4") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("OTHER4").ToString = "LEFT" Then
            '            rnPC4.Value = Salary * CDec(INFO_SALARY2.Rows(0)("OTHER4")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("OTHER3") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("OTHER3").ToString = "LEFT" Then
            '            rnPC3.Value = Salary * CDec(INFO_SALARY2.Rows(0)("OTHER3")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("OTHER2") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("OTHER2").ToString = "LEFT" Then
            '            rnPC2.Value = Salary * CDec(INFO_SALARY2.Rows(0)("OTHER2")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("OTHER1") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("OTHER1").ToString = "LEFT" Then
            '            rnPC1.Value = Salary * CDec(INFO_SALARY2.Rows(0)("OTHER1")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("THUONGYTCLCV") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("THUONGYTCLCV").ToString = "LEFT" Then
            '            rnOtherSalary1.Value = Salary * CDec(INFO_SALARY2.Rows(0)("THUONGYTCLCV")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("LUONGBS") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("LUONGBS").ToString = "LEFT" Then
            '            rnAddtionalSal.Value = Salary * CDec(INFO_SALARY2.Rows(0)("LUONGBS")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        ElseIf INFO_SALARY.Rows(0)("DIENTHOAI") IsNot Nothing AndAlso INFO_SALARY.Rows(0)("DIENTHOAI").ToString = "LEFT" Then
            '            rnPhone.Value = Salary * CDec(INFO_SALARY2.Rows(0)("DIENTHOAI")) / 100 - (rdSalaryBHXH.Value - luongCB)
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Event selected item combobox phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboAllowance_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAllowance.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                Dim allownace = rep.GetAllowanceList(New AllowanceListDTO With {.ID = cboAllowance.SelectedValue}).FirstOrDefault
                If allownace IsNot Nothing Then
                    chkIsInsurrance.Checked = allownace.IS_INSURANCE
                Else
                    chkIsInsurrance.Checked = False
                End If
            End Using
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
                        Dim empID = EmployeeList(0)
                        FillData(empID.EMPLOYEE_ID)
                        isLoadPopup = 0
                        ClearControlValue(cbSalaryLevel, rnFactorSalary, cbSalaryRank, SalaryInsurance, basicSalary, rdSalaryBHXH, Salary_Total, rnOtherSalary1,
                              rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5, cboPercentSalary)
                        FillData()

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
            ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, cboTaxTable, cboEmployeeType, cboSalTYPE, cboSalPayment, rnFactorSalary, SalaryInsurance, cboAllowance_Total, basicSalary, rdSalaryBHXH,
                              Salary_Total, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5, txtDecisionNo, txtEmployeeName, txtTitleName, txtOrgName, SalaryInsurance)
            rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
            rgAllow.Rebind()
            hidID.Value = Nothing
            hidEmp.Value = Nothing
            hidempid1.Value = Nothing
            EmployeeID = 0
            code_attendent = Nothing
            hidTitle.Value = Nothing
            hidOrg.Value = Nothing
            cbSalaryGroup.SelectedValue = ""
            cbSalaryLevel.SelectedValue = ""
            cbSalaryRank.SelectedValue = ""

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
                    EnableControlAll(True, btnFindEmployee, chkIsInsurrance, SalaryInsurance)
                    If (rdEffectDate.SelectedDate IsNot Nothing) Then
                        EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank)
                    Else
                        EnableControlAll(False, cbSalaryGroup, cbSalaryLevel, cbSalaryRank)
                    End If
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnFindEmployee, chkIsInsurrance)
                    If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                        EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, rnMinSal, rnGas, rnAddtionalSal, rnPhone, rnShareSal)
                    End If
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                        ctrlFindEmployeePopup.IsShowKiemNhiem = true
                    End If
                Case 2
                    If Not phFindSign.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = True
                        ctrlFindSigner.MustHaveContract = False
                        ctrlFindSigner.FunctionName = "ctrlHU_WageNewEdit"
                    End If
                Case 3

                    ctrlFindFrameSalaryPopup = Me.Register("ctrlFindFrameSalaryPopup", "Common", "ctrlFindFrameSalaryPopup")
                    ctrlFindFrameSalaryPopup.FrameSalaryType = FrameSalaryType.FrameSalaryLocation
                    If List_FrameSalary_ID IsNot Nothing AndAlso List_FrameSalary_ID.Count > 0 Then
                        ctrlFindFrameSalaryPopup.Bind_Find_ValueKeys = List_FrameSalary_ID

                    End If
                    ctrlFindFrameSalaryPopup.IS_HadLoad = False
                    phFindFrameSalary.Controls.Add(ctrlFindFrameSalaryPopup)
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
                        Working = New WorkingDTO With {.ID = Decimal.Parse(ID)}
                    End If
                    Refresh("UpdateView")
                    Exit Sub
                End If
                If Request.Params("empID") IsNot Nothing Then
                    Dim empID = Request.Params("empID")
                    FillData(empID)
                End If
                If Request.Params("empID") IsNot Nothing AndAlso Request.Params("kind") IsNot Nothing Then
                    FillData()
                    ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, SalaryInsurance, rnFactorSalary)
                    ClearControlValue(rnPC1, rnPC2, rnPC3, rnPC4, rnPC5, cboSalTYPE, cboSalPayment, basicSalary, cboTaxTable, cboPercentSalary, rdSalaryBHXH, rnGas, rnAddtionalSal, rnPhone, rnOtherSalary1, rnShareSal, chkIS_ALLOW_SALARY_LESS_THAN)
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
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            HideControl()
            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                'If rdEffectDate.SelectedDate IsNot Nothing Then
                '    txtDecisionNo.Text = obj.EMPLOYEE_CODE + "/" + rdEffectDate.SelectedDate.Value.Month.ToString + "/" + rdEffectDate.SelectedDate.Value.Year.ToString + "/QĐL-" + obj.SHORT_NAME
                'End If

                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                'If obj.DIRECT_MANAGER Is Nothing Or obj.OBJECT_ATTENDANCE Is Nothing Or obj.OBJECT_LABOR Is Nothing Then
                '    ShowMessage(Translate(" Nhân viên chưa có dữ liệu Quản lý trực tiếp, Đối tượng chấm công, Đối tượng lao động. Vui lòng kiểm tra và bổ sung trước khi tạo hồ sơ lương."), NotifyType.Warning)
                '    Exit Sub
                'End If
                ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, cboTaxTable, cboEmployeeType, cboSalTYPE, cboSalPayment, rnFactorSalary, SalaryInsurance, cboAllowance_Total, basicSalary, rdSalaryBHXH,
                              Salary_Total, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, rnOtherSalary4, rnOtherSalary5, rnMinSal, rnGas, rnAddtionalSal, rnPhone, rnShareSal)
                rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                rgAllow.Rebind()
                hidID.Value = obj.ID.ToString
                hidEmp.Value = obj.EMPLOYEE_ID.ToString()
                hidempid1.Value = obj.EMPLOYEE_ID.ToString()
                EmployeeID = empid
                If IsNumeric(obj.DM_ID) Then
                    hidDM_ID.Value = obj.DM_ID
                Else
                    hidDM_ID.Value = 0
                End If
                If obj.CODE_ATTENDANCE IsNot Nothing Then
                    code_attendent = obj.CODE_ATTENDANCE
                Else
                    code_attendent = Nothing
                End If

                txtEmployeeCode.Text = obj.EMPLOYEE_CODE

                txtEmployeeName.Text = obj.EMPLOYEE_NAME
                hidTitle.Value = obj.TITLE_ID
                txtTitleName.Text = obj.TITLE_NAME
                ' txtTitleGroup.Text = obj.TITLE_GROUP_NAME
                hidOrg.Value = obj.ORG_ID
                txtOrgName.Text = obj.ORG_NAME
                If obj.SAL_GROUP_ID IsNot Nothing Then
                    cbSalaryGroup.SelectedValue = obj.SAL_GROUP_ID
                    cbSalaryGroup.Text = obj.SAL_GROUP_NAME
                Else
                    cbSalaryGroup.SelectedValue = ""
                End If
                If obj.SAL_LEVEL_ID IsNot Nothing Then
                    cbSalaryLevel.SelectedValue = obj.SAL_LEVEL_ID
                    cbSalaryLevel.Text = obj.SAL_LEVEL_NAME
                Else
                    cbSalaryLevel.SelectedValue = ""
                End If
                If obj.SAL_RANK_ID IsNot Nothing Then
                    cbSalaryRank.SelectedValue = obj.SAL_RANK_ID
                    cbSalaryRank.Text = obj.SAL_RANK_NAME
                Else
                    cbSalaryRank.SelectedValue = ""
                End If
                If obj.STAFF_RANK_ID IsNot Nothing Then
                    hidStaffRank.Value = obj.STAFF_RANK_ID
                    'txtStaffRank.Text = obj.STAFF_RANK_NAME
                Else
                    hidStaffRank.Value = ""
                    'txtStaffRank.Text = vbNullString
                End If
                If obj.REGION_SAL IsNot Nothing Then
                    rnShareSal.Value = obj.REGION_SAL
                Else
                    ClearControlValue(rnShareSal)
                End If
                'SalaryInsurance.Text = obj.SAL_INS

            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Fill data len control theo id moi nhat
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData()
        Try
            Dim rep As New ProfileBusinessRepository
            Dim profileRep As New ProfileRepository
            Dim dtData As DataTable
            Dim Working1 As WorkingDTO
            Dim workingMaxId As Decimal = rep.GET_SALARY_BY_DATE(hidEmp.Value, Date.Now)
            If workingMaxId <> 0 Then
                Working1 = New WorkingDTO With {.ID = workingMaxId}
                Working1 = rep.GetWorkingByID(Working1)

                If Working1.SAL_TYPE_ID IsNot Nothing Then
                    cboSalTYPE.SelectedValue = Working1.SAL_TYPE_ID
                    cboSalTYPE.Text = Working1.SAL_TYPE_NAME
                End If

                If Working1.SAL_PAYMENT_ID IsNot Nothing Then
                    cboSalPayment.SelectedValue = Working1.SAL_PAYMENT_ID
                    cboSalPayment.Text = Working1.SAL_PAYMENT_NAME
                End If
                If Working1.TAX_TABLE_ID IsNot Nothing Then
                    cboTaxTable.SelectedValue = Working1.TAX_TABLE_ID
                    cboTaxTable.Text = Working1.TAX_TABLE_Name
                End If
                If Working1.EMPLOYEE_TYPE IsNot Nothing Then
                    cboEmployeeType.SelectedValue = Working1.EMPLOYEE_TYPE
                    cboEmployeeType.Text = Working1.EMPLOYEE_TYPE_NAME
                End If
                If Working1.SAL_GROUP_ID IsNot Nothing Then
                    cbSalaryGroup.SelectedValue = Working1.SAL_GROUP_ID
                    cbSalaryGroup.Text = Working1.SAL_GROUP_NAME
                    dtData = profileRep.GetSalaryLevelCombo(Working1.SAL_GROUP_ID, True)
                    FillRadCombobox(cbSalaryLevel, dtData, "NAME", "ID", True)
                End If
                If Working1.SAL_LEVEL_ID IsNot Nothing Then
                    cbSalaryLevel.SelectedValue = Working1.SAL_LEVEL_ID
                    cbSalaryLevel.Text = Working1.SAL_LEVEL_NAME
                    dtData = profileRep.GetSalaryRankCombo(Working1.SAL_LEVEL_ID, True)
                    FillRadCombobox(cbSalaryRank, dtData, "NAME", "ID", True)
                End If
                If Working1.SAL_RANK_ID IsNot Nothing Then
                    cbSalaryRank.SelectedValue = Working1.SAL_RANK_ID
                    cbSalaryRank.Text = Working1.SAL_RANK_NAME
                End If

                If IsNumeric(Working1.PERCENTSALARY) Then
                    cboPercentSalary.SelectedValue = Working1.PERCENTSALARY
                End If
                If IsNumeric(Working1.FACTORSALARY) Then
                    rnFactorSalary.Text = Working1.FACTORSALARY.ToString
                End If
                If Working1.SAL_BASIC IsNot Nothing Then
                    basicSalary.Value = Working1.SAL_BASIC
                    basicSal = Working1.SAL_BASIC
                End If
                If Working1.SALARY_BHXH IsNot Nothing Then
                    rdSalaryBHXH.Value = Working1.SALARY_BHXH
                End If
                If Working1.SAL_INS IsNot Nothing Then
                    SalaryInsurance.Text = Working1.SAL_INS
                End If
                If Working1.ALLOWANCE_TOTAL IsNot Nothing Then
                    cboAllowance_Total.Text = Working1.ALLOWANCE_TOTAL
                End If
                If Working1.SAL_TOTAL IsNot Nothing Then
                    Salary_Total.Value = Working1.SAL_TOTAL
                    total = Working1.SAL_TOTAL
                End If

                If IsNumeric(Working1.OTHERSALARY1) Then
                    rnOtherSalary1.Value = Working1.OTHERSALARY1
                End If

                If IsNumeric(Working1.TOXIC_RATE) Then
                    rnToxic_rate.Value = Working1.TOXIC_RATE
                Else
                    rnToxic_rate.Value = 0
                End If
                If IsNumeric(Working1.TOXIC_SALARY) Then
                    rnToxic_salary.Value = Working1.TOXIC_SALARY
                Else
                    rnToxic_salary.Value = 0
                End If
                If IsDate(Working1.NEXTSALARY_DATE) Then
                    rdCOEFFICIENT.SelectedDate = Working1.NEXTSALARY_DATE
                End If

                'If IsNumeric(Working1.REGION_SAL) Then
                '    rnMinSal.Value = Working1.REGION_SAL
                'End If
                'If IsNumeric(Working1.GAS_SAL) Then
                '    rnGas.Value = Working1.GAS_SAL
                'End If
                'If IsNumeric(Working1.ADDITIONAL_SAL) Then
                '    rnAddtionalSal.Value = Working1.ADDITIONAL_SAL
                'End If
                'If IsNumeric(Working1.PHONE_SAL) Then
                '    rnPhone.Value = Working1.PHONE_SAL
                'End If
                If IsNumeric(Working1.SHARE_SAL) Then
                    rnShareSal.Value = Working1.SHARE_SAL
                End If

            End If
            Dim store As New ProfileStoreProcedure
            If rdEffectDate.SelectedDate IsNot Nothing And hidEmp.Value <> "" Then
                Dim MinAmount = store.GET_MIN_AMOUNT(hidEmp.Value, rdEffectDate.SelectedDate)
                If IsNumeric(MinAmount) AndAlso MinAmount > 0 Then
                    rnMinSal.Value = MinAmount
                End If
            End If
            'Dim INFO_SALARY = store.GET_INFO_SALARYITEMSPERCENT(hidOrg.Value)
            'If INFO_SALARY.Rows.Count <= 0 Then
            '    ShowMessage(Translate(" Đơn vị của nhân viên chưa được thiết lập cơ cấu lương, vui lòng thiết lập trước khi thêm Hồ sơ lương"), Utilities.NotifyType.Warning)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load data phu cap len grid phu cap
    ''' </summary>
    ''' <param name="lstAllow"></param>
    ''' <remarks></remarks>
    Private Sub CreateDataAllowance(Optional ByVal lstAllow As List(Of WorkingAllowanceDTO) = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If lstAllow Is Nothing Then
                lstAllow = New List(Of WorkingAllowanceDTO)
            End If
            rgAllow.DataSource = lstAllow
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFile.Text = Down_File
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim crc As New Crc32()
            'Dim fileNameZip As String
            'fileNameZip = txtUploadFile.Text.Trim
            'Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function GetDATA_IN() As String
        Try
            Dim obj As DATA_IN
            If IsNumeric(hidEmp.Value) Then
                obj.EMPLOYEE_ID = hidEmp.Value
            End If
            If IsDate(rdEffectDate.SelectedDate) Then
                obj.EFFECT_DATE = rdEffectDate.SelectedDate
            End If
            If IsNumeric(SalaryInsurance.Value) Then
                obj.SALARYINSURANCE = SalaryInsurance.Value
            End If
            If IsNumeric(rnFactorSalary.Text) Then
                obj.FACTORSALARY = CDec(Val(rnFactorSalary.Text))
            End If
            obj.MUCLUONGCS = 0
            If IsNumeric(Salary_Total.Value) Then
                obj.TOTALSALARY = Salary_Total.Value
            End If
            If IsNumeric(basicSalary.Value) Then
                obj.BASICSALARY = basicSalary.Value
            End If
            If IsNumeric(rdSalaryBHXH.Value) Then
                obj.SALARY_BHXH = rdSalaryBHXH.Value
            End If
            If IsNumeric(cboAllowance_Total.Value) Then
                obj.ALLOWANCE_TOTAL = cboAllowance_Total.Value
            End If
            If cboPercentSalary.SelectedValue <> "" Then
                obj.PERCENT_SALARY = cboPercentSalary.SelectedValue
            End If
            If IsNumeric(cbSalaryGroup.SelectedValue) Then
                obj.GROUP_SALARY = cbSalaryGroup.SelectedValue
            End If
            If IsNumeric(cbSalaryRank.SelectedValue) Then
                obj.RANK_SALARY = cbSalaryRank.SelectedValue
            End If
            If IsNumeric(cbSalaryLevel.SelectedValue) Then
                obj.LEVEL_SALARY = cbSalaryLevel.SelectedValue
            End If
            'If IsNumeric(rnOtherSalary1.Value) Then
            '    obj.OTHERSALARY1 = rnOtherSalary1.Value
            'End If
            If IsNumeric(rnOtherSalary2.Value) Then
                obj.OTHERSALARY2 = rnOtherSalary2.Value
            End If
            If IsNumeric(rnOtherSalary3.Value) Then
                obj.OTHERSALARY3 = rnOtherSalary3.Value
            End If
            If IsNumeric(rnOtherSalary4.Value) Then
                obj.OTHERSALARY4 = rnOtherSalary4.Value
            End If
            If IsNumeric(rnOtherSalary5.Value) Then
                obj.OTHERSALARY5 = rnOtherSalary5.Value
            End If
            Dim dataArray As New ArrayList()
            dataArray.Add(obj)
            Dim jsonSerialiser = New JavaScriptSerializer()
            Dim strData_In = jsonSerialiser.Serialize(dataArray)
            Return strData_In
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub CalculatorSalary()
        Try
            Dim DATA_OUT As DataTable
            If getSE_CASE_CONFIG("ctrlHU_WageNewEdit_case1") > 0 Then 'Active

            Else 'Unactive
                Using rep As New ProfileBusinessRepository
                    DATA_OUT = rep.Calculator_Salary(GetDATA_IN())
                End Using
                'BIDING DATA TO CONTROLS
                If DATA_OUT IsNot Nothing AndAlso DATA_OUT.Rows.Count > 0 Then
                    BidingDataToControls(DATA_OUT)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BidingDataToControls(ByVal dtdata As DataTable)
        Try
            'If cbSalaryGroup.SelectedValue.ToString <> "" Then
            '    Dim total As Decimal
            '    Dim basicSal As Decimal = 0
            '    Dim factorSal As String = ""
            '    Dim rnFactorSal As Decimal
            '    If rnFactorSalary.Text.Contains(".") Then
            '        factorSal = rnFactorSalary.Text.Replace(".", ",").ToString
            '        rnFactorSal = If(IsNumeric(factorSal), Decimal.Parse(factorSal), Nothing)
            '    Else
            '        rnFactorSal = If(IsNumeric(rnFactorSalary.Text), Decimal.Parse(rnFactorSalary.Text), Nothing)
            '    End If
            '    If IsNumeric(rnFactorSal) Then
            '        basicSal = rnFactorSal * _lttv
            '        basicSalary.Value = basicSal
            '    End If


            '    If rnPercentSalary.Value.HasValue Then
            '        If cboSalTYPE.Text = "Kiêm nhiệm" Then
            '            total = (basicSal + If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) + _
            '                If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * rnPercentSalary.Value / 100
            '        Else
            '            total = basicSal * rnPercentSalary.Value / 100 + _
            '                If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) + _
            '                If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0) + _
            '                If(rnOtherSalary3.Value.HasValue, rnOtherSalary3.Value, 0) + _
            '                If(rnOtherSalary4.Value.HasValue, rnOtherSalary4.Value, 0) + _
            '                If(rnOtherSalary5.Value.HasValue, rnOtherSalary5.Value, 0) + _
            '                If(cboAllowance_Total.Value.HasValue, cboAllowance_Total.Value, 0)
            '        End If
            '    End If
            '    Salary_Total.Value = total
            '    basicSalary.Enabled = False
            'End If
            'Dim total As Decimal
            ' Dim basicSal As Decimal = 0
            Dim factorSal As String = ""
            Dim rnFactorSal As Decimal
            'Get luong toi thieu vung
            If rdEffectDate.SelectedDate IsNot Nothing Then
                LoadMinAreaSalary()
            End If

            'If rnFactorSalary.Text.Contains(".") Then
            '    factorSal = rnFactorSalary.Text.Replace(".", ",").ToString
            '    rnFactorSal = If(IsNumeric(factorSal), Decimal.Parse(factorSal), Nothing)
            'Else
            '    rnFactorSal = If(IsNumeric(rnFactorSalary.Text), Decimal.Parse(rnFactorSalary.Text), Nothing)
            'End If
            'If IsNumeric(rnFactorSal) AndAlso rnFactorSal > 0 Then
            '    basicSal = rnFactorSal * _lttv
            '    If Not CType(CommonConfig.dicConfig("APP_SETTING"), Boolean) Then
            '        basicSal = basicSalary.Text
            '        Dim rep As New ProfileRepository
            '        dtdata = rep.GetSalaryRankList(cbSalaryLevel.SelectedValue, True)
            '        Dim row = dtdata.Select("ID='" + If(cbSalaryRank.SelectedValue = "", 0, cbSalaryRank.SelectedValue) + "'")(0)
            '        If row IsNot Nothing Then
            '            basicSal = row("SALARY_BASIC").ToString
            '        End If
            '    End If
            '    basicSalary.Value = basicSal
            '    rdSalaryBHXH.Value = basicSal
            'Else
            '    basicSalary.Value = 0
            '    rdSalaryBHXH.Value = 0
            'End If

            If cboPercentSalary.SelectedValue <> "" Then
                If cboSalTYPE.Text = "Kiêm nhiệm" Then
                    total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                             If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                             If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                    ' basicSalary.Enabled = True
                Else
                    total = basicSal * CDec(cboPercentSalary.SelectedValue) / 100 +
                        If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)
                    ' basicSalary.Enabled = False
                End If
            End If
            Salary_Total.Value = total
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadMinAreaSalary()
        Try
            Dim lttv = If(EmployeeID > 0, commonStore.GET_MIN_AMOUNT(EmployeeID, rdEffectDate.SelectedDate), 0)
            If IsNumeric(lttv) Then
                _lttv = lttv
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadPercentDefault()
        Try
            Dim tyLeThuViec = commonStore.GET_VALUE_PA_PAYMENT("TyLeThuViec")
            Dim tyLeChinhThuc = commonStore.GET_VALUE_PA_PAYMENT("TyLeChinhThuc")
            If IsNumeric(tyLeThuViec) Then
                _tyLeThuViec = tyLeThuViec
            End If
            If IsNumeric(tyLeChinhThuc) Then
                _tyLeChinhThuc = tyLeChinhThuc
            End If
        Catch ex As Exception
            Throw ex
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

    Private Function ValidateDecisionNo(ByVal working As WorkingDTO) As Boolean
        Using rep As New ProfileBusinessRepository
            'Dim a = rep.ValidateWorking("EXIST_EFFECT_DATE", working)
            Return rep.ValidateWorking("EXIST_EFFECT_DATE_IS_WAGE", working)
        End Using
    End Function

    Protected Sub cboSalTYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalTYPE.SelectedIndexChanged
        Dim taxTables As New List(Of OtherListDTO)
        If String.IsNullOrWhiteSpace(cboSalTYPE.SelectedValue) Then
            Exit Sub
        End If
        Using rep As New ProfileRepository
            taxTables = rep.GetOtherList(OtherTypes.TaxTable).ToList(Of OtherListDTO)()
        End Using
        Select Case cboSalTYPE.Text
            Case "Thử việc"
                EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, rnFactorSalary, cboPercentSalary, Salary_Total, rnOtherSalary1, basicSalary)
                'rnPercentSalary.Value = _tyLeThuViec
                ClearControlValue(rnFactorSalary, basicSalary, rdSalaryBHXH, rdSalaryBHXH)
            Case "Chính thức"
                EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, rnFactorSalary, cboPercentSalary, Salary_Total, rnOtherSalary1, basicSalary)
                'rnPercentSalary.Value = _tyLeChinhThuc
                ClearControlValue(rnFactorSalary, basicSalary, rdSalaryBHXH, rdSalaryBHXH)
            Case "Kiêm nhiệm"
                EnableControlAll(True, cboPercentSalary, rnOtherSalary1, rnOtherSalary2)
                EnableControlAll(False, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, Salary_Total, rnFactorSalary, basicSalary, rdSalaryBHXH)
                rnFactorSalary.Text = 0
                basicSalary.Text = 0
                rdSalaryBHXH.Text = 0
                ClearControlValue(cbSalaryGroup, cbSalaryLevel, cbSalaryRank, Salary_Total)
        End Select
        If cboPercentSalary.SelectedValue <> "" Then
            If cboSalTYPE.Text = "Kiêm nhiệm" Then
                total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                       If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                       If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                Salary_Total.Value = total
            Else
                Salary_Total.Value = (basicSalary.Value +
                    If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
            End If
        End If
    End Sub
    Private Sub SetTaxTableByCode(ByVal taxTables As List(Of OtherListDTO), ByVal code As String)
        Dim taxTable = taxTables.FirstOrDefault(Function(f) f.CODE = code)
        cboTaxTable.ClearSelection()
        If taxTable IsNot Nothing Then
            SetValueComboBox(cboTaxTable, taxTable.ID, taxTable.NAME_VN)
        End If
    End Sub
#End Region

    Private Sub CalTotal()
        If IsNumeric(cboTaxTable.SelectedValue) AndAlso cboTaxTable.SelectedValue = 6363 Then
            If hidFrameSalaryRank.Value = "" Then
                ShowMessage(Translate("Vui chọn hệ số lương."), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            If IsDate(rdEffectDate.SelectedDate) Then
                Dim IS_CBCD = commonStore.GET_VALUE_CBCD(hidEmp.Value, rdEffectDate.SelectedDate)
                If IsNumeric(IS_CBCD) AndAlso IS_CBCD <> 0 Then
                    Dim TLSG_NVBH = commonStore.GET_VALUE_PA_PAYMENT_LIST("TLSG_NVBH", rdEffectDate.SelectedDate)
                    basicSalary.Value = If(IsNumeric(hidFrameSalaryRank.Value), hidFrameSalaryRank.Value, 0) * TLSG_NVBH
                Else
                    Dim TLSG_CTY = commonStore.GET_VALUE_PA_PAYMENT_LIST("TLSG_CTY", rdEffectDate.SelectedDate)
                    basicSalary.Value = If(IsNumeric(hidFrameSalaryRank.Value), hidFrameSalaryRank.Value, 0) * TLSG_CTY
                End If
            End If
        End If

        If Not rnToxic_rate.Value.HasValue Then
            rnToxic_rate.Value = 0
        End If
        rnToxic_salary.Value = basicSalary.Value * If(rnToxic_rate.Value.HasValue, rnToxic_rate.Value, 0)


        If cboEmployeeType.Text = "Chính thức" Then
            If IsNumeric(hidDM_ID.Value) AndAlso hidDM_ID.Value <> -1 Then
                rdSalaryBHXH.Value = If(basicSalary.Value.HasValue, basicSalary.Value, 0) + If(rnToxic_salary.Value.HasValue, rnToxic_salary.Value, 0)
            Else
                Dim MLTTC = commonStore.GET_VALUE_PA_PAYMENT_LIST("MLTTC", If(IsDate(rdEffectDate.SelectedDate), rdEffectDate.SelectedDate, Date.Now.Date))
                rdSalaryBHXH.Value = If(IsNumeric(hidFrameSalaryRank.Value), hidFrameSalaryRank.Value, 0) * If(IsNumeric(MLTTC), MLTTC, 0) + If(rnToxic_salary.Value.HasValue, rnToxic_salary.Value, 0)
            End If
        Else
            rdSalaryBHXH.Value = 0
        End If



    End Sub
    Private Sub cboTaxTable_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboTaxTable.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'If cboTaxTable.SelectedValue = 6361 Or cboTaxTable.SelectedValue = 6362 Then
            '    rdSalaryBHXH.Text = 0
            'End If
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub cboEmployeeType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmployeeType.SelectedIndexChanged
        'Dim taxTables As New List(Of OtherListDTO)
        If String.IsNullOrWhiteSpace(cboEmployeeType.SelectedValue) Then
            Exit Sub
        End If
        Select Case cboEmployeeType.Text
            Case "Thử việc"
                EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, rnFactorSalary, cboPercentSalary, Salary_Total, rnOtherSalary1, basicSalary)
                cboPercentSalary.SelectedValue = _tyLeThuViec
                ClearControlValue(rnFactorSalary, basicSalary, rdSalaryBHXH, rdSalaryBHXH)
                rdSalaryBHXH.Text = 0
            Case "Chính thức"
                EnableControlAll(True, cbSalaryGroup, cbSalaryLevel, cbSalaryRank, rnFactorSalary, cboPercentSalary, Salary_Total, rnOtherSalary1, basicSalary)
                cboPercentSalary.SelectedValue = _tyLeChinhThuc
                ClearControlValue(rnFactorSalary, basicSalary, rdSalaryBHXH, rdSalaryBHXH)
            Case Else
                rdSalaryBHXH.Text = 0
        End Select
        If cboPercentSalary.SelectedValue <> "" Then
            If cboSalTYPE.Text = "Kiêm nhiệm" Then
                total = (If(basicSalary.Value.HasValue, basicSalary.Value, 0) +
                       If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0) +
                       If(rnOtherSalary2.Value.HasValue, rnOtherSalary2.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
                Salary_Total.Value = total
            Else
                Salary_Total.Value = (basicSalary.Value +
                    If(rnOtherSalary1.Value.HasValue, rnOtherSalary1.Value, 0)) * CDec(cboPercentSalary.SelectedValue) / 100
            End If
        End If
        CalTotal()
    End Sub

    Private Sub AutoCreate_DecisionNo()
        Dim store As New ProfileStoreProcedure
        Try
            If IsDBNull(hidEmp.Value) Then
                Exit Sub
            End If

            If rdEffectDate.SelectedDate Is Nothing Then
                Exit Sub
            End If

            ClearControlValue(txtDecisionNo)
            Dim contract_no = store.AUTOCREATE_DECISIONNO(Decimal.Parse(hidEmp.Value),
                                                          rdEffectDate.SelectedDate)

            txtDecisionNo.Text = contract_no
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub HideControl()
        lbGas.Visible = False
        rnGas.Visible = False
        tdGas.Visible = False
        tdGas1.Visible = False
        rnPhone.Visible = False
        lbPhone.Visible = False
        tdPhone.Visible = False
        tdPhone1.Visible = False
        rnAddtionalSal.Visible = False
        lbAddtionalSal.Visible = False
        tdAddtionalSal.Visible = False
        tdAddtionalSal1.Visible = False
        rnOtherSalary1.Visible = False
        lbOtherSalary1.Visible = False
        tdOtherSalary1.Visible = False
        tdOtherSalary11.Visible = False
        rnPC1.Visible = False
        lbPC1.Visible = False
        tdPC1.Visible = False
        tdPC11.Visible = False
        rnPC2.Visible = False
        lbPC2.Visible = False
        tdPC2.Visible = False
        tdPC21.Visible = False
        rnPC3.Visible = False
        lbPC3.Visible = False
        tdPC3.Visible = False
        tdPC31.Visible = False
        rnPC4.Visible = False
        lbPC4.Visible = False
        tdPC4.Visible = False
        tdPC41.Visible = False
        rnPC5.Visible = False
        lbPC5.Visible = False
        tdPC5.Visible = False
        tdPC51.Visible = False
    End Sub
    Private Sub totalSalItemsPercent()
        If Not IsNumeric(basicSalary.Value) Then
            Exit Sub
        End If
        Dim Salary = basicSalary.Value
        'If IsNumeric(rnPercentSalary.Value) Then
        '    Salary = Salary * CDec(Val(rnPercentSalary.Text)) / 100
        'End If
        Dim store As New ProfileStoreProcedure
        Dim INFO_SALARY = store.GET_INFO_SALARYITEMSPERCENT(hidOrg.Value, rdEffectDate.SelectedDate)
        If INFO_SALARY.Rows.Count > 0 Then

            If INFO_SALARY.Rows(0)("UNUSE_RATIO") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("UNUSE_RATIO")) AndAlso CDec(INFO_SALARY.Rows(0)("UNUSE_RATIO")) <> -1 Then

                If INFO_SALARY.Rows(0)("LUONGCB") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("LUONGCB")) AndAlso CDec(INFO_SALARY.Rows(0)("LUONGCB")) > 0 Then
                    rdSalaryBHXH.Value = Salary * CDec(INFO_SALARY.Rows(0)("LUONGCB")) / 100
                End If

                If INFO_SALARY.Rows(0)("XANGXE") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("XANGXE")) AndAlso CDec(INFO_SALARY.Rows(0)("XANGXE")) > 0 Then

                    rnGas.Value = Salary * CDec(INFO_SALARY.Rows(0)("XANGXE")) / 100

                End If
                If INFO_SALARY.Rows(0)("DIENTHOAI") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("DIENTHOAI")) AndAlso CDec(INFO_SALARY.Rows(0)("DIENTHOAI")) > 0 Then

                    rnPhone.Value = Salary * CDec(INFO_SALARY.Rows(0)("DIENTHOAI")) / 100

                End If
                If INFO_SALARY.Rows(0)("LUONGBS") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("LUONGBS")) AndAlso CDec(INFO_SALARY.Rows(0)("LUONGBS")) > 0 Then

                    rnAddtionalSal.Value = Salary * CDec(INFO_SALARY.Rows(0)("LUONGBS")) / 100

                End If
                If INFO_SALARY.Rows(0)("THUONGYTCLCV") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("THUONGYTCLCV")) AndAlso CDec(INFO_SALARY.Rows(0)("THUONGYTCLCV")) > 0 Then

                    rnOtherSalary1.Value = Salary * CDec(INFO_SALARY.Rows(0)("THUONGYTCLCV")) / 100

                End If
                If INFO_SALARY.Rows(0)("OTHER1") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER1")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER1")) > 0 Then

                    rnPC1.Value = Salary * CDec(INFO_SALARY.Rows(0)("OTHER1")) / 100

                End If
                If INFO_SALARY.Rows(0)("OTHER2") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER2")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER2")) > 0 Then

                    rnPC2.Value = Salary * CDec(INFO_SALARY.Rows(0)("OTHER2")) / 100

                End If
                If INFO_SALARY.Rows(0)("OTHER3") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER3")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER3")) > 0 Then

                    rnPC3.Value = Salary * CDec(INFO_SALARY.Rows(0)("OTHER3")) / 100

                End If
                If INFO_SALARY.Rows(0)("OTHER4") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER4")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER4")) > 0 Then

                    rnPC4.Value = Salary * CDec(INFO_SALARY.Rows(0)("OTHER4")) / 100

                End If
                If INFO_SALARY.Rows(0)("OTHER5") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER5")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER5")) > 0 Then

                    rnPC5.Value = Salary * CDec(INFO_SALARY.Rows(0)("OTHER5")) / 100

                End If
            End If


        End If
    End Sub
    Private Sub ShowHideControlSal()

        Dim store As New ProfileStoreProcedure
        Dim INFO_SALARY = store.GET_INFO_SALARYITEMSPERCENT(hidOrg.Value, rdEffectDate.SelectedDate)
        Dim INFO_SALARY2 = store.GET_INFO_SALARYITEMSPERCENT2(hidOrg.Value, rdEffectDate.SelectedDate)
        If INFO_SALARY.Rows.Count > 0 Then
            If (INFO_SALARY.Rows(0)("XANGXE") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("XANGXE")) AndAlso CDec(INFO_SALARY.Rows(0)("XANGXE")) > 0) Or
                (INFO_SALARY2.Rows(0)("XANGXE") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("XANGXE")) AndAlso CDec(INFO_SALARY2.Rows(0)("XANGXE")) = 0) Then
                rnGas.Visible = True
                lbGas.Visible = True
                tdGas.Visible = True
                tdGas1.Visible = True
            Else
                lbGas.Visible = False
                rnGas.Visible = False
                tdGas.Visible = False
                tdGas1.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("DIENTHOAI") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("DIENTHOAI")) AndAlso CDec(INFO_SALARY.Rows(0)("DIENTHOAI")) > 0) Or
                (INFO_SALARY2.Rows(0)("DIENTHOAI") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("DIENTHOAI")) AndAlso CDec(INFO_SALARY2.Rows(0)("DIENTHOAI")) = 0) Then
                rnPhone.Visible = True
                lbPhone.Visible = True
                tdPhone.Visible = True
                tdPhone1.Visible = True

            Else
                rnPhone.Visible = False
                lbPhone.Visible = False
                tdPhone.Visible = False
                tdPhone1.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("LUONGBS") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("LUONGBS")) AndAlso CDec(INFO_SALARY.Rows(0)("LUONGBS")) > 0) Or
                (INFO_SALARY2.Rows(0)("LUONGBS") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("LUONGBS")) AndAlso CDec(INFO_SALARY2.Rows(0)("LUONGBS")) = 0) Then
                rnAddtionalSal.Visible = True
                lbAddtionalSal.Visible = True
                tdAddtionalSal.Visible = True
                tdAddtionalSal1.Visible = True
            Else
                rnAddtionalSal.Visible = False
                lbAddtionalSal.Visible = False
                tdAddtionalSal.Visible = False
                tdAddtionalSal1.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("THUONGYTCLCV") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("THUONGYTCLCV")) AndAlso CDec(INFO_SALARY.Rows(0)("THUONGYTCLCV")) > 0) Or
                (INFO_SALARY2.Rows(0)("THUONGYTCLCV") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("THUONGYTCLCV")) AndAlso CDec(INFO_SALARY2.Rows(0)("THUONGYTCLCV")) = 0) Then
                rnOtherSalary1.Visible = True
                lbOtherSalary1.Visible = True
                tdOtherSalary1.Visible = True
                tdOtherSalary11.Visible = True

            Else
                rnOtherSalary1.Visible = False
                lbOtherSalary1.Visible = False
                tdOtherSalary1.Visible = False
                tdOtherSalary11.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("OTHER1") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER1")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER1")) > 0) Or
                (INFO_SALARY2.Rows(0)("OTHER1") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("OTHER1")) AndAlso CDec(INFO_SALARY2.Rows(0)("OTHER1")) = 0) Then
                rnPC1.Visible = True
                lbPC1.Visible = True
                tdPC1.Visible = True
                tdPC11.Visible = True

            Else
                rnPC1.Visible = False
                lbPC1.Visible = False
                tdPC1.Visible = False
                tdPC11.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("OTHER2") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER2")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER2")) > 0) Or
                (INFO_SALARY2.Rows(0)("OTHER2") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("OTHER2")) AndAlso CDec(INFO_SALARY2.Rows(0)("OTHER2")) = 0) Then
                rnPC2.Visible = True
                lbPC2.Visible = True
                tdPC2.Visible = True
                tdPC21.Visible = True

            Else
                rnPC2.Visible = False
                lbPC2.Visible = False
                tdPC2.Visible = False
                tdPC21.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("OTHER3") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER3")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER3")) > 0) Or
                (INFO_SALARY2.Rows(0)("OTHER3") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("OTHER3")) AndAlso CDec(INFO_SALARY2.Rows(0)("OTHER3")) = 0) Then
                rnPC3.Visible = True
                lbPC3.Visible = True
                tdPC3.Visible = True
                tdPC31.Visible = True
            Else
                rnPC3.Visible = False
                lbPC3.Visible = False
                tdPC3.Visible = False
                tdPC31.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("OTHER4") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER4")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER4")) > 0) Or
                (INFO_SALARY2.Rows(0)("OTHER4") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("OTHER4")) AndAlso CDec(INFO_SALARY2.Rows(0)("OTHER4")) = 0) Then
                rnPC4.Visible = True
                lbPC4.Visible = True
                tdPC4.Visible = True
                tdPC41.Visible = True

            Else
                rnPC4.Visible = False
                lbPC4.Visible = False
                tdPC4.Visible = False
                tdPC41.Visible = False
            End If
            If (INFO_SALARY.Rows(0)("OTHER5") IsNot Nothing AndAlso IsNumeric(INFO_SALARY.Rows(0)("OTHER5")) AndAlso CDec(INFO_SALARY.Rows(0)("OTHER5")) > 0) Or
                (INFO_SALARY2.Rows(0)("OTHER5") IsNot Nothing AndAlso IsNumeric(INFO_SALARY2.Rows(0)("OTHER5")) AndAlso CDec(INFO_SALARY2.Rows(0)("OTHER5")) = 0) Then
                rnPC5.Visible = True
                lbPC5.Visible = True
                tdPC5.Visible = True
                tdPC51.Visible = True

            Else
                rnPC5.Visible = False
                lbPC5.Visible = False
                tdPC5.Visible = False
                tdPC51.Visible = False
            End If
        Else
            ShowMessage(Translate(" Đơn vị của nhân viên chưa được thiết lập cơ cấu lương, vui lòng thiết lập trước khi thêm Hồ sơ lương"), Utilities.NotifyType.Warning)
            HideControl()

        End If
    End Sub
End Class
Structure DATA_IN
    Public EMPLOYEE_ID As Decimal?
    Public EFFECT_DATE As String
    Public SALARYINSURANCE As Decimal?
    Public FACTORSALARY As Decimal?
    Public MUCLUONGCS As Decimal?
    Public TOTALSALARY As Decimal?
    Public BASICSALARY As Decimal?
    Public ALLOWANCE_TOTAL As Decimal?
    Public PERCENT_SALARY As Decimal?
    Public GROUP_SALARY As Decimal?
    Public RANK_SALARY As Decimal?
    Public LEVEL_SALARY As Decimal?
    Public OTHERSALARY1 As Decimal?
    Public OTHERSALARY2 As Decimal?
    Public OTHERSALARY3 As Decimal?
    Public OTHERSALARY4 As Decimal?
    Public OTHERSALARY5 As Decimal?
    Public CODE_CASE As String
    Public SALARY_BHXH As Decimal?
End Structure