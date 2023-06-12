Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_PositionNewEdit
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindPositionPopup As ctrlFindPositionPopup
    Protected WithEvents ctrlFindQLPD As ctrlFindPositionPopup
    Protected repHF As HistaffFrameworkRepository

    Property PosSelect As Decimal
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()


    ''' <summary>
    ''' Obj Organizations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Organizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property


    Public Property IDSelected As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelected")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelected") = value
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
    Property Down_FileJD As String
        Get
            Return ViewState(Me.ID & "_Down_FileJD")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_FileJD") = value
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
    Property FileOldNameJD As String
        Get
            Return ViewState(Me.ID & "_FileOldNameJD")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldNameJD") = value
        End Set
    End Property
    Public Property IS_UYBAN As Boolean
        Get
            Return ViewState(Me.ID & "_IS_UYBAN")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IS_UYBAN") = value
        End Set
    End Property
    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelected = Request.Params("ID")
                End If
                If IDSelected <> 0 Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Property"
    Protected WithEvents ctrlFindOrgPopupOM As ctrlFindOrgPopup

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

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

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim starttime As DateTime = DateTime.UtcNow

        Try
            'If Request.Params("isView") IsNot Nothing AndAlso Decimal.Parse(Request.Params("isView")) = 1 Then
            '    Me.MainToolBar.Enabled = False
            'End If
            LoadComboTitle()
            LoadComboTitleGroup()
            GetParams()
            'isLoadPopup = -1
            'If Not IsPostBack Then
            UpdateControlState()
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(starttime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'displayexception(me.viewname, me.id, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'rgMain.AllowCustomPaging = True
            'ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'CType(Me.MainToolBar.Items(2), RadToolBarButton).Text = "Swap"
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("SWAP", ToolbarIcons.Export,
                                                                 ToolbarAuthorize.Export, Translate("Hoán đổi")))
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub LoadComboTitleGroup()
        Try
            Using rep As New ProfileRepository
                Dim DataTable As DataTable = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboTitleGroup, DataTable, "NAME", "ID")
                DataTable = rep.GetOtherList("OBJECT_ATTENDANT", True)
                FillRadCombobox(cboWorkingTime, DataTable, "NAME", "ID")
                DataTable = rep.GetOtherList("MAJOR", True)
                FillRadCombobox(cboTDCM, DataTable, "NAME", "ID")
                DataTable = rep.GetOtherList("LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLANGUAGE, DataTable, "NAME", "ID")
                DataTable = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboCOMPUTER, DataTable, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub LoadComboTitle()
        Dim rep As New ProfileRepository
        Dim lst As New List(Of JobDTO)
        Dim obj As New JobDTO
        lst = rep.Getjob(obj, "CREATED_DATE desc")
        Dim lstnew = lst.Where(Function(f) f.ACTFLG = "Áp dụng")
        Dim ds = lstnew.ToList.ToTable
        FillRadCombobox(cboJobFamily, ds, "NAMECBO", "ID")
    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            'ctrlFindOrgPopupOM.IsOM = True
            ctrlFindOrgPopupOM.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case Message
                Case "UpdateView"
                    'cbTDV.Enabled = False
                    'CType(MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetPositionID(Decimal.Parse(Me.IDSelected))
                    If obj IsNot Nothing Then
                        hidPos.Value = obj.ID
                        'If obj.ACTFLG = "A" Then
                        '    MainToolBar.Items(0).Enabled = False
                        '    MainToolBar.Items(1).Enabled = False
                        '    MainToolBar.Items(2).Enabled = False
                        '    MainToolBar.Items(3).Enabled = False
                        'End If
                        If obj.JOB_ID IsNot Nothing Then
                            cboJobFamily.SelectedValue = obj.JOB_ID
                        End If

                        txtMaVT.Text = obj.CODE
                        txtTenVT.Text = obj.NAME_VN
                        txtTCVE.Text = obj.NAME_EN
                        hidOrgID.Value = obj.ORG_ID
                        hidOrgIdOld.Value = obj.ORG_ID
                        txtOrgName.Text = obj.ORG_NAME
                        If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                            txtOrgName.ToolTip = Utilities.DrawTreeByString(obj.ORG_DESC)
                        End If
                        If IsNumeric(obj.TITLE_GROUP_ID) Then
                            cboTitleGroup.SelectedValue = obj.TITLE_GROUP_ID
                        End If
                        txtEmpQltt.Text = obj.EMP_LM
                        txtEmpQlgt.Text = obj.EMP_CSM
                        'Using repOrg As New ProfileRepository
                        '    Dim objOrg = repOrg.GetOrganizationByID(Decimal.Parse(obj.ORG_ID))
                        '    If objOrg IsNot Nothing Then
                        '        txtOrgName.Text = objOrg.NAME_VN
                        '    End If
                        'End Using
                        'Dim repOrg As ProfileRepository

                        cbTDV.Checked = obj.IS_OWNER
                        If obj.IS_UYBAN IsNot Nothing Then
                            IS_UYBAN = obj.IS_UYBAN
                            If obj.IS_UYBAN = True Then
                                cbNonPhysical.Checked = True
                                cbNonPhysical.Enabled = False
                            Else
                                cbNonPhysical.Checked = False
                                cbNonPhysical.Enabled = False
                            End If
                        End If
                        'cbNonPhysical_CheckedChanged(Nothing, Nothing)
                        If obj.IS_PLAN = True Then
                            hidIsPlanBefore.Value = -1
                        Else
                            hidIsPlanBefore.Value = 0
                        End If
                        cbPlan.Checked = obj.IS_PLAN

                        If obj.LM IsNot Nothing Then
                            hidEmp.Value = obj.LM
                        End If
                        txtQLTT.Text = obj.LM_NAME
                        If obj.CSM IsNot Nothing Then
                            hidQLPD.Value = obj.CSM
                        End If
                        txtQLPD.Text = obj.CSM_NAME
                        'txtMaCP.Text = obj.COST_CENTER
                        'If obj.WORK_LOCATION IsNot Nothing Then
                        '    cbbDDLV.SelectedValue = obj.WORK_LOCATION
                        'End If
                        If obj.ACTFLG = "A" Then
                            cboStatus.SelectedValue = -1
                        Else
                            cboStatus.SelectedValue = 0
                        End If

                        If obj.REMARK IsNot Nothing Then
                            txtSpec.Content = obj.REMARK
                        End If

                        If obj.JOB_SPEC IsNot Nothing Then
                            txtYC.Content = obj.JOB_SPEC
                        End If

                        'txtTCV.Text = obj.JOB_BAND_NAME
                        txtMaster.Text = obj.MASTER_NAME
                        txtInterim.Text = obj.INTERIM_NAME
                        If obj.EFFECTIVE_DATE IsNot Nothing Then
                            txtEffective_Date.SelectedDate = obj.EFFECTIVE_DATE
                        End If
                        If obj.WORKING_TIME IsNot Nothing Then
                            cboWorkingTime.SelectedValue = obj.WORKING_TIME
                        End If
                        txtUploadFile.Text = obj.FILENAME
                        txtRemindLink.Text = If(obj.UPLOADFILE Is Nothing, "", obj.UPLOADFILE)
                        loadDatasource(txtUploadFile.Text)
                        FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                        If obj.MASTER_NAME <> " - " Or txtInterim.Text <> " - " Then
                            btnFindOrg.Enabled = False
                            cboJobFamily.Enabled = False
                        End If
                        If obj.JobDescription IsNot Nothing Then
                            txtUploadFileJD.Text = obj.JobDescription.FILE_NAME
                            txtRemindLinkJD.Text = If(obj.JobDescription.UPLOAD_FILE Is Nothing, "", obj.JobDescription.UPLOAD_FILE)
                            loadDatasourceJD(txtUploadFileJD.Text)
                            FileOldNameJD = If(FileOldNameJD = "", txtUploadJD.Text, FileOldNameJD)
                            If obj.JobDescription.COMPUTER IsNot Nothing Then
                                cboCOMPUTER.SelectedValue = obj.JobDescription.COMPUTER
                            End If
                            txtDETAIL_RESPONSIBILITY_1.Text = obj.JobDescription.DETAIL_RESPONSIBILITY_1
                            txtDETAIL_RESPONSIBILITY_2.Text = obj.JobDescription.DETAIL_RESPONSIBILITY_2
                            txtDETAIL_RESPONSIBILITY_3.Text = obj.JobDescription.DETAIL_RESPONSIBILITY_3
                            txtDETAIL_RESPONSIBILITY_4.Text = obj.JobDescription.DETAIL_RESPONSIBILITY_4
                            txtDETAIL_RESPONSIBILITY_5.Text = obj.JobDescription.DETAIL_RESPONSIBILITY_5
                            txtINTERNAL_1.Text = obj.JobDescription.INTERNAL_1
                            txtINTERNAL_2.Text = obj.JobDescription.INTERNAL_2
                            txtINTERNAL_3.Text = obj.JobDescription.INTERNAL_2
                            txtJOB_TARGET_1.Text = obj.JobDescription.JOB_TARGET_1
                            txtJOB_TARGET_2.Text = obj.JobDescription.JOB_TARGET_2
                            txtJOB_TARGET_3.Text = obj.JobDescription.JOB_TARGET_3
                            txtJOB_TARGET_4.Text = obj.JobDescription.JOB_TARGET_4
                            txtJOB_TARGET_5.Text = obj.JobDescription.JOB_TARGET_5
                            txtJOB_TARGET_6.Text = obj.JobDescription.JOB_TARGET_6
                            If obj.JobDescription.LANGUAGE IsNot Nothing Then
                                cboLANGUAGE.SelectedValue = obj.JobDescription.LANGUAGE
                            End If
                            txtMAJOR.Text = obj.JobDescription.MAJOR_NAME
                            txtOUTSIDE_1.Text = obj.JobDescription.OUTSIDE_1
                            txtOUTSIDE_2.Text = obj.JobDescription.OUTSIDE_2
                            txtOUTSIDE_3.Text = obj.JobDescription.OUTSIDE_3
                            txtOUT_RESULT_1.Text = obj.JobDescription.OUT_RESULT_1
                            txtOUT_RESULT_2.Text = obj.JobDescription.OUT_RESULT_2
                            txtOUT_RESULT_3.Text = obj.JobDescription.OUT_RESULT_3
                            txtOUT_RESULT_4.Text = obj.JobDescription.OUT_RESULT_4
                            txtOUT_RESULT_5.Text = obj.JobDescription.OUT_RESULT_5
                            txtPERMISSION_1.Text = obj.JobDescription.PERMISSION_1
                            txtPERMISSION_2.Text = obj.JobDescription.PERMISSION_2
                            txtPERMISSION_3.Text = obj.JobDescription.PERMISSION_3
                            txtPERMISSION_4.Text = obj.JobDescription.PERMISSION_4
                            txtPERMISSION_5.Text = obj.JobDescription.PERMISSION_5
                            txtPERMISSION_6.Text = obj.JobDescription.PERMISSION_6
                            txtRESPONSIBILITY_1.Text = obj.JobDescription.RESPONSIBILITY_1
                            txtRESPONSIBILITY_2.Text = obj.JobDescription.RESPONSIBILITY_2
                            txtRESPONSIBILITY_3.Text = obj.JobDescription.RESPONSIBILITY_3
                            txtRESPONSIBILITY_4.Text = obj.JobDescription.RESPONSIBILITY_4
                            txtRESPONSIBILITY_5.Text = obj.JobDescription.RESPONSIBILITY_5
                            txtSUPPORT_SKILL.Text = obj.JobDescription.SUPPORT_SKILL
                            If obj.JobDescription.TDCM IsNot Nothing Then
                                cboTDCM.SelectedValue = obj.JobDescription.TDCM
                            End If
                            txtWORK_EXP.Text = obj.JobDescription.WORK_EXP
                            'MainToolBar.Items(0).Enabled = False
                            'MainToolBar.Items(1).Enabled = False
                        End If
                    Else
                        CurrentState = CommonMessage.STATE_REJECT
                    End If
                Case "InsertView"
                    Dim repe As New ProfileRepository
                    CType(MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    txtMaVT.Text = repe.AutoGenCodeHuTile("HU_TITLE", "CODE")
                    If Request.Params("OrgID") IsNot Nothing Then
                        hidOrgID.Value = Request.Params("OrgID")
                        loadData()
                    End If
                    If Request.Params("OrgName") IsNot Nothing Then
                        txtOrgName.Text = Request.Params("OrgName")
                    End If
                    cbPlan.Checked = True
                    CurrentState = CommonMessage.STATE_NEW

            End Select

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
        Dim rep As New ProfileRepository
        Dim _filter As New TitleDTO
        Dim dtData As New DataTable
        Dim objjob As New TitleDTO
        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case "SWAP"
                    ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn đổi Master <-> Interim không?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    Exit Sub
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboJobFamily.SelectedValue <> "" Then
                            objjob.JOB_ID = cboJobFamily.SelectedValue
                        Else
                            ShowMessage(Translate("Bạn phải chọn công việc"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtMaVT.Text.Trim <> "" Then
                            objjob.CODE = txtMaVT.Text
                        End If

                        If txtEffective_Date.SelectedDate IsNot Nothing Then
                            objjob.EFFECTIVE_DATE = txtEffective_Date.SelectedDate
                        End If

                        If txtTenVT.Text.Trim <> "" Then
                            objjob.NAME_VN = txtTenVT.Text
                        End If

                        If hidOrgID.Value <> "" Then
                            objjob.ORG_NAME = txtOrgName.Text
                            objjob.ORG_ID = hidOrgID.Value
                        End If

                        If txtQLPD.Text.Trim <> "" And IsNumeric(hidQLPD.Value) Then
                            objjob.CSM = Decimal.Parse(hidQLPD.Value)  ' txtQLPD.Text
                        Else
                            objjob.CSM = Nothing
                        End If

                        If txtQLTT.Text.Trim <> "" AndAlso IsNumeric(hidEmp.Value) Then
                            objjob.LM = Decimal.Parse(hidEmp.Value)
                        End If
                        If cboWorkingTime.SelectedValue <> "" Then
                            objjob.WORKING_TIME = cboWorkingTime.SelectedValue
                        End If
                        objjob.IS_PLAN = cbPlan.Checked
                        objjob.IS_OWNER = cbTDV.Checked
                        objjob.IS_NONPHYSICAL = cbNonPhysical.Checked
                        objjob.NAME_EN = txtTCVE.Text
                        objjob.REMARK = txtYC.Content
                        objjob.JOB_SPEC = txtSpec.Content
                        objjob.ACTFLG = "A"
                        objjob.FILENAME = txtUpload.Text.Trim
                        'If IsNumeric(cboTitleGroup.SelectedValue) Then
                        '    objjob.TITLE_GROUP_ID = cboTitleGroup.SelectedValue
                        'End If
                        objjob.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objjob.UPLOADFILE = "" Then
                            objjob.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objjob.UPLOADFILE = If(objjob.UPLOADFILE Is Nothing, "", objjob.UPLOADFILE)
                        End If
                        objjob.JobDescription = New JobDescriptionDTO
                        objjob.JobDescription.JOB_TARGET_1 = txtJOB_TARGET_1.Text
                        objjob.JobDescription.JOB_TARGET_2 = txtJOB_TARGET_2.Text
                        objjob.JobDescription.JOB_TARGET_3 = txtJOB_TARGET_3.Text
                        objjob.JobDescription.JOB_TARGET_4 = txtJOB_TARGET_4.Text
                        objjob.JobDescription.JOB_TARGET_5 = txtJOB_TARGET_5.Text
                        objjob.JobDescription.JOB_TARGET_6 = txtJOB_TARGET_6.Text
                        If cboTDCM.SelectedValue <> "" Then
                            objjob.JobDescription.TDCM = cboTDCM.SelectedValue
                        End If
                        objjob.JobDescription.MAJOR_NAME = txtMAJOR.Text
                        objjob.JobDescription.WORK_EXP = txtWORK_EXP.Text
                        If cboLANGUAGE.SelectedValue <> "" Then
                            objjob.JobDescription.LANGUAGE = cboLANGUAGE.SelectedValue
                        End If
                        If cboCOMPUTER.SelectedValue <> "" Then
                            objjob.JobDescription.COMPUTER = cboCOMPUTER.SelectedValue
                        End If

                        objjob.JobDescription.SUPPORT_SKILL = txtSUPPORT_SKILL.Text
                        objjob.JobDescription.INTERNAL_1 = txtINTERNAL_1.Text
                        objjob.JobDescription.INTERNAL_2 = txtINTERNAL_2.Text
                        objjob.JobDescription.INTERNAL_3 = txtINTERNAL_3.Text
                        objjob.JobDescription.OUTSIDE_1 = txtOUTSIDE_1.Text
                        objjob.JobDescription.OUTSIDE_2 = txtOUTSIDE_2.Text
                        objjob.JobDescription.OUTSIDE_3 = txtOUTSIDE_3.Text
                        objjob.JobDescription.RESPONSIBILITY_1 = txtRESPONSIBILITY_1.Text
                        objjob.JobDescription.RESPONSIBILITY_2 = txtRESPONSIBILITY_2.Text
                        objjob.JobDescription.RESPONSIBILITY_3 = txtRESPONSIBILITY_3.Text
                        objjob.JobDescription.RESPONSIBILITY_4 = txtRESPONSIBILITY_4.Text
                        objjob.JobDescription.RESPONSIBILITY_5 = txtRESPONSIBILITY_5.Text
                        objjob.JobDescription.DETAIL_RESPONSIBILITY_1 = txtDETAIL_RESPONSIBILITY_1.Text
                        objjob.JobDescription.DETAIL_RESPONSIBILITY_2 = txtDETAIL_RESPONSIBILITY_2.Text
                        objjob.JobDescription.DETAIL_RESPONSIBILITY_3 = txtDETAIL_RESPONSIBILITY_3.Text
                        objjob.JobDescription.DETAIL_RESPONSIBILITY_4 = txtDETAIL_RESPONSIBILITY_4.Text
                        objjob.JobDescription.DETAIL_RESPONSIBILITY_5 = txtDETAIL_RESPONSIBILITY_5.Text
                        objjob.JobDescription.OUT_RESULT_1 = txtOUT_RESULT_1.Text
                        objjob.JobDescription.OUT_RESULT_2 = txtOUT_RESULT_2.Text
                        objjob.JobDescription.OUT_RESULT_3 = txtOUT_RESULT_3.Text
                        objjob.JobDescription.OUT_RESULT_4 = txtOUT_RESULT_4.Text
                        objjob.JobDescription.OUT_RESULT_5 = txtOUT_RESULT_5.Text
                        objjob.JobDescription.PERMISSION_1 = txtPERMISSION_1.Text
                        objjob.JobDescription.PERMISSION_2 = txtPERMISSION_2.Text
                        objjob.JobDescription.PERMISSION_3 = txtPERMISSION_3.Text
                        objjob.JobDescription.PERMISSION_4 = txtPERMISSION_4.Text
                        objjob.JobDescription.PERMISSION_5 = txtPERMISSION_5.Text
                        objjob.JobDescription.PERMISSION_5 = txtPERMISSION_5.Text
                        objjob.JobDescription.PERMISSION_6 = txtPERMISSION_6.Text

                        objjob.JobDescription.FILE_NAME = txtUploadJD.Text.Trim
                        objjob.JobDescription.UPLOAD_FILE = If(Down_FileJD Is Nothing, "", Down_FileJD)
                        If objjob.JobDescription.UPLOAD_FILE = "" Then
                            objjob.JobDescription.UPLOAD_FILE = If(txtRemindLinkJD.Text Is Nothing, "", txtRemindLinkJD.Text)
                        Else
                            objjob.JobDescription.UPLOAD_FILE = If(objjob.JobDescription.UPLOAD_FILE Is Nothing, "", objjob.JobDescription.UPLOAD_FILE)
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'If rep.CheckIsOwnerActive(objjob.ORG_ID) And objjob.IS_PLAN = False And objjob.IS_OWNER = True Then
                                '    Me.ShowMessage(Translate("Đơn vị đích đến đã có vị trí trưởng."), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                If cbTDV.Checked Then
                                    If rep.CheckOwnerExist(objjob) Then
                                        Me.ShowMessage(txtOrgName.Text & " đã có trưởng đơn vị, vui lòng kiểm tra lại", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If rep.InsertPossition(objjob, gid) Then
                                    'Remove old cache
                                    CacheManager.ClearValue("OT_HU_BANK_LIST_Blank")
                                    CacheManager.ClearValue("OT_HU_BANK_LIST_NoBlank")
                                    'return
                                    'Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_Position&group=Business")
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objjob.ID = hidPos.Value

                                If cbTDV.Checked Then
                                    If rep.CheckOwnerExist(objjob) Then
                                        Me.ShowMessage(txtOrgName.Text & " đã có trưởng đơn vị, vui lòng kiểm tra lại", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                End If
                                'check org has master
                                repHF = New HistaffFrameworkRepository
                                Dim objOrg As New dataDTO
                                Dim dtData1 = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_CHECK_ORG_EXIST", New List(Of Object)({objOrg.ID})).Tables(0)
                                Dim isExist = dtData1.Rows(0)(0)

                                'If isExist <> 0 Then
                                '    ShowMessage(Translate("Phòng đã có trưởng phòng, không được cập nhật trưởng phòng"), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                'If rep.CheckIsOwnerActive(objjob.ORG_ID) And objjob.IS_PLAN = False And objjob.IS_OWNER = True Then
                                '    Me.ShowMessage(Translate("Phòng đã có trưởng phòng, không được cập nhật trưởng phòng."), NotifyType.Warning)
                                '    Exit Sub
                                'End If
                                'If rep.CheckIsOwnerActive(objjob.ORG_ID) And objjob.IS_PLAN = False And objjob.IS_OWNER = False Then
                                '    Me.ShowMessage(Translate("Đơn vị đích đến đã có vị trí trưởng."), NotifyType.Warning)
                                '    Exit Sub
                                'End If

                                If rep.ModifyPossition(objjob, objjob.ID) Then
                                    objjob.IS_PLAN_LEFT = If(hidIsPlanBefore.Value = -1, True, False)
                                    'rep.ModifyPositionById(objjob, objjob.ORG_ID)
                                    If objjob.IS_PLAN = True Then
                                        Dim dtDataB = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_POSITION_BY_ORG", New List(Of Object)({objjob.ORG_ID}))
                                        Dim dtDataA As New DataTable
                                        If dtDataB IsNot Nothing AndAlso dtDataB.Tables.Count > 0 Then
                                            dtDataA = dtDataB.Tables(0)
                                        End If
                                        If dtDataA IsNot Nothing Then
                                            If dtDataA.Rows.Count > 0 Then
                                                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(objjob.ORG_ID),
                                               .IS_DISSOLVE = False}
                                                Dim objTitle As New TitleDTO
                                                Dim Titles As List(Of TitleDTO)
                                                Dim MaximumRows As Integer
                                                Titles = rep.GetPossition(_filter, _param, 0, 50, MaximumRows, True)
                                                Dim lstTitle = (From t In Titles Where t.IS_OWNER = 0 And t.ACTFLG = "Áp dụng").ToList()
                                                If lstTitle.Count > 0 Then
                                                    For Each item In lstTitle
                                                        Dim lstTitle1 = (From x In lstTitle Where x.ID = Decimal.Parse(item.ID)).FirstOrDefault
                                                        lstTitle1.LM = Decimal.Parse(dtDataA.Rows(0)("ID"))
                                                        lstTitle1.CSM = Decimal.Parse(dtDataA.Rows(0)("ID"))
                                                        Dim obj = rep.GetPositionID(Decimal.Parse(item.ID))
                                                        If obj IsNot Nothing Then
                                                            If obj.JOB_ID IsNot Nothing Then
                                                                lstTitle1.JOB_ID = obj.JOB_ID
                                                            End If
                                                        End If
                                                        lstTitle1.EFFECTIVE_DATE = DateTime.Now
                                                        If rep.ModifyPossition(lstTitle1, Decimal.Parse(item.ID)) Then

                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    End If

                                    'Remove old cache
                                    CacheManager.ClearValue("OT_HU_BANK_LIST_Blank")
                                    CacheManager.ClearValue("OT_HU_BANK_LIST_NoBlank")
                                    'Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_Position&group=Business")
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Check sự kiện validate cho combobox tồn tại hoặc ngừng áp dụng</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalMAVT_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMaVT.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New TitleDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'If CurrentState = CommonMessage.STATE_EDIT Then
            'Else
            '    _validate.CODE = txtMaVT.Text.Trim
            '    args.IsValid = rep.ValidatePosition_ACTIVITIES(_validate)
            'End If

            'If Not args.IsValid Then
            '    txtMaVT.Text = rep.AutoGenCode("00", "HU_TITLE_ACTIVITIES", "CODE")
            'End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If rep.SwapMasterInterim(IDSelected, "APP") Then
                    Dim Master = txtMaster.Text
                    Dim Interim = txtInterim.Text
                    txtMaster.Text = Interim
                    txtInterim.Text = Master
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub ctrlFindOrgPopupOM_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopupOM.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopupOM.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN

                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If

                loadData()
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Class dataDTO
        Public Property ID As Decimal
        Public Property IS_OWNER As Decimal
    End Class


    Protected Function loadData()
        Dim objOrg As New dataDTO

        Dim org_id = 0
        If hidOrgID.Value <> "" Then
            org_id = Decimal.Parse(hidOrgID.Value)
        End If
        IS_UYBAN = If(Request.Params("Committee") Is Nothing, False, Request.Params("Committee"))
        If IS_UYBAN = True Then
            cbNonPhysical.Checked = True
            cbNonPhysical.Enabled = False
        Else
            cbNonPhysical.Checked = False
            cbNonPhysical.Enabled = False
        End If

        If cbNonPhysical.Checked Then
            sStarQLTT.Visible = False
            reqQLTT.Visible = False
        Else
            sStarQLTT.Visible = True
            reqQLTT.Visible = True
        End If
        'cbNonPhysical_CheckedChanged(Nothing, Nothing)
        Dim is_owner As Decimal
        If cbTDV.Checked Then
            is_owner = -1
        Else
            is_owner = 0
        End If

        If org_id > 0 Then
            objOrg.ID = org_id
        Else
            objOrg.ID = 0
        End If

        objOrg.IS_OWNER = is_owner

        'repHF = New HistaffFrameworkRepository

        'Dim dtData1 = New DataTable
        'Dim ds = repHF.ExecuteToDataSet("PKG_OMS_BUSINESS.GET_POSITION_BY_ORG", New List(Of Object)({objOrg.ID}))
        'If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
        '    dtData1 = ds.Tables(0)
        'End If

        'txtQLTT.Text = ""
        'txtQLPD.Text = ""
        'hidQLPD.Value = ""
        'hidEmp.Value = ""
        ''txtMaCP.Text = ""

        'If dtData1 IsNot Nothing Then
        '    If dtData1.Rows.Count > 0 Then
        '        txtQLTT.Text = dtData1.Rows(0)("NAME_VN")
        '        txtQLPD.Text = dtData1.Rows(0)("NAME_VN")
        '        hidQLPD.Value = Decimal.Parse(dtData1.Rows(0)("ID"))
        '        hidEmp.Value = Decimal.Parse(dtData1.Rows(0)("ID"))
        '        'If Not IsDBNull(dtData1.Rows(0)("COST_CENTER_CODE")) Then
        '        '    txtMaCP.Text = dtData1.Rows(0)("COST_CENTER_CODE")
        '        'End If
        '    End If
        'End If

    End Function

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
            isLoadPopup = 1
            Me.UpdateControlState()
            ctrlFindPositionPopup.Show()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnQLPD_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnQLPD.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 3
            Me.UpdateControlState()
            ctrlFindQLPD.Show()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub btnDeleteQLGT_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnDeleteQLGT.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ClearControlValue(txtQLPD, txtEmpQlgt, hidQLPD)
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
                                 ctrlFindQLPD.CancelClicked,
                                 ctrlFindOrgPopupOM.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    ''' 
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If

                Select Case isLoadPopup
                    Case 1
                        hidEmp.Value = obj.EMPLOYEE_ID
                        txtQLTT.Text = obj.EMPLOYEE_NAME
                    Case 3
                        hidQLPD.Value = obj.EMPLOYEE_ID
                        txtQLPD.Text = obj.EMPLOYEE_NAME

                End Select


            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
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
    Private Sub ctrlFindPositionPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindPositionPopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindPositionPopup.SelectedEmployeeID.Count > 0 Then
                Dim empID = ctrlFindPositionPopup.SelectedEmployeeID(0)
                Dim nameQLTT = ctrlFindPositionPopup.SelectedEmployeeID(1)
                Dim codeQLTT = ctrlFindPositionPopup.SelectedEmployeeID(2)
                hidEmp.Value = empID
                txtQLTT.Text = codeQLTT & " - " & nameQLTT
                txtEmpQltt.Text = If(ctrlFindPositionPopup.SelectedEmployeeID.Count > 4, ctrlFindPositionPopup.SelectedEmployeeID(4), "")
            End If
            'FillData(empID)
            isLoadPopup = 0
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
    Private Sub ctrlFindQLPD_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindQLPD.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim empID = ctrlFindQLPD.SelectedEmployeeID(0)
            'FillData(empID)

            If ctrlFindQLPD.SelectedEmployeeID.Count > 0 Then
                Dim empID = ctrlFindQLPD.SelectedEmployeeID(0)
                txtQLPD.Text = ctrlFindQLPD.SelectedEmployeeID(2) & " - " & ctrlFindQLPD.SelectedEmployeeID(1)
                hidQLPD.Value = empID
                txtEmpQlgt.Text = If(ctrlFindQLPD.SelectedEmployeeID.Count > 4, ctrlFindQLPD.SelectedEmployeeID(4), "")
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Protected Sub cboJobFamily_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If cboJobFamily.SelectedValue <> "" Then
                Dim rep As New ProfileRepository
                Dim objJob As JobDTO = rep.GetjobID(cboJobFamily.SelectedValue)
                'txtTCV.Text = objJob.JOB_BAND_NAME
                txtTenVT.Text = objJob.NAME_VN
                txtTCVE.Text = objJob.NAME_EN
                txtSpec.Content = objJob.PURPOSE
                txtYC.Content = objJob.REQUEST
                'cboTitleGroup.SelectedValue = objJob.PHAN_LOAI_ID
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region


    Public Overrides Sub UpdateControlState()
        Try
            If phFindOrg.Controls.Contains(ctrlFindOrgPopupOM) Then
                'ctrlFindOrgPopupOM.IsOM = True
                phFindOrg.Controls.Remove(ctrlFindOrgPopupOM)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If phFindEmp.Controls.Contains(ctrlFindPositionPopup) Then
                phFindEmp.Controls.Remove(ctrlFindPositionPopup)
            End If
            If phFindQLPD.Controls.Contains(ctrlFindQLPD) Then
                phFindQLPD.Controls.Remove(ctrlFindQLPD)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindPositionPopup = Me.Register("ctrlFindPositionPopup", "Common", "ctrlFindPositionPopup")
                        phFindEmp.Controls.Add(ctrlFindPositionPopup)
                        ctrlFindPositionPopup.MultiSelect = False
                        ctrlFindPositionPopup.LoadAllOrganization = False
                        ctrlFindPositionPopup.MustHaveContract = False
                        ctrlFindPositionPopup.IS_COMMITEE = If(IS_UYBAN, "1", "0")
                    End If
                Case 2
                    ' ctrlFindOrgPopupOM
                    ' ctrlFindOrgPopup
                    If Not phFindOrg.Controls.Contains(ctrlFindOrgPopupOM) Then
                        ctrlFindOrgPopupOM = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")

                        'ctrlFindOrgPopupOM.IsOM = True
                        ctrlFindOrgPopupOM.OrganizationType = OrganizationType.OrganizationLocation
                        If IS_UYBAN Then
                            ctrlFindOrgPopupOM.ShowCommitee_static_check = 1
                        End If

                        phFindOrg.Controls.Add(ctrlFindOrgPopupOM)
                        'ctrlFindOrgPopupOM.Show()
                    End If

                Case 3
                    If Not phFindQLPD.Controls.Contains(ctrlFindQLPD) Then
                        ctrlFindQLPD = Me.Register("ctrlFindQLPD", "Common", "ctrlFindPositionPopup")
                        phFindEmp.Controls.Add(ctrlFindQLPD)
                        ctrlFindQLPD.MultiSelect = False
                        ctrlFindQLPD.LoadAllOrganization = False
                        ctrlFindQLPD.MustHaveContract = False
                        ctrlFindQLPD.IS_COMMITEE = If(IS_UYBAN, "1", "0")
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

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
    Private Sub btnUploadFileJD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileJD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtUploadFileJD.Text = ""
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
            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Organization/")
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFileJD.Text = file.FileName
                        Down_FileJD = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasourceJD(txtUploadFileJD.Text)
            End If
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
            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Organization/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
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

    Private Sub loadDatasourceJD(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFileJD.Text = strUpload
                FileOldNameJD = txtUploadJD.Text
                txtUploadJD.Text = strUpload
            Else
                strUpload = String.Empty
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
            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownloadJD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadJD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try
            If txtUploadJD.Text <> "" Then
                Dim strPath_Down As String
                If FileOldNameJD = txtUploadJD.Text.Trim Or FileOldNameJD Is Nothing Then
                    If txtRemindLinkJD.Text IsNot Nothing Then
                        If txtRemindLinkJD.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/Organization/" + txtRemindLinkJD.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_FileJD <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/Organization/" + Down_FileJD)
                        'bCheck = True
                        ZipFilesJD(strPath_Down)
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
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/Organization/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/Organization/" + Down_File)
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

    Private Sub ZipFilesJD(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            Dim fileNameZip As String = txtUploadFileJD.Text.Trim
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
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
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            Dim fileNameZip As String = txtUploadFile.Text.Trim
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
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

    'Private Sub cbNonPhysical_CheckedChanged(sender As Object, e As EventArgs) Handles cbNonPhysical.CheckedChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        'If cbNonPhysical.Checked Then
    '        '    sStarQLTT.Visible = False
    '        '    reqQLTT.Visible = False
    '        'Else
    '        '    sStarQLTT.Visible = True
    '        '    reqQLTT.Visible = True
    '        'End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        HttpContext.Current.Trace.Warn(ex.ToString())
    '    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
End Class