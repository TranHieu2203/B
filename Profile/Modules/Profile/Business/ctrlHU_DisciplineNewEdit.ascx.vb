Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports ICSharpCode.SharpZipLib.Checksums
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_DisciplineNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Object Disciptline
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Discipline As DisciplineDTO
        Get
            Return ViewState(Me.ID & "_Discipline")
        End Get
        Set(ByVal value As DisciplineDTO)
            ViewState(Me.ID & "_Discipline") = value
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
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' List combo data
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
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sach nhan vien bi ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_Discipline As List(Of DisciplineEmpDTO)
        Get
            Return ViewState(Me.ID & "_Employee_Discipline")
        End Get
        Set(ByVal value As List(Of DisciplineEmpDTO))
            ViewState(Me.ID & "_Employee_Discipline") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sach phong ban bi ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Org_Discipline As List(Of DisciplineOrgDTO)
        Get
            Return ViewState(Me.ID & "_Org_Discipline")
        End Get
        Set(ByVal value As List(Of DisciplineOrgDTO))
            ViewState(Me.ID & "_Org_Discipline") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' '0 - normal
    '1 - Employee
    '2 - Org
    '3 - Sign
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
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' 0 - Declare
    ''' 1 - Extent
    ''' 2 - Details
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DesciplineID As Decimal
        Get
            Return ViewState(Me.ID & "_DesciplineID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_DesciplineID") = value
        End Set
    End Property

    Property State_Id As Decimal
        Get
            Return ViewState(Me.ID & "_State_Id")
        End Get
        Set(value As Decimal)
            ViewState(Me.ID & "_State_Id") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("FULLNAME_TEXT", GetType(String))
                dt.Columns.Add("ORG_TEXT", GetType(String))
                dt.Columns.Add("TITLE_TEXT", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If

            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad
    ''' Lay ve cac parameter, lam moi lai trang, Cap nhat trang thai cac control tren page
    ''' 0 - Declare
    ''' 1 - Extent
    ''' 2 - Details
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
            rgEmployee.AllowSorting = False
            rgOrg.AllowSorting = False
            'rntxtMoney.Enabled = False
            If CType(CommonConfig.dicConfig("APP_SETTING_12"), Boolean) Then
                lbSignerName.Visible = False
                txtSignerName.Display = False
                btnFindSinger.Visible = False
                lbSignerTitle.Visible = False
                txtSignerTitle.Visible = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarDiscipline
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
                Select Case FormType
                    Case 0
                        Me.ViewDescription = Translate("Tạo mới kỷ luật")
                    Case 1
                        Me.ViewDescription = Translate("Sửa kỷ luật")
                End Select
            End If
            If Not IsPostBack Then
                'ViewConfig(RadPane2)
                'GirdConfig(rgEmployee)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc bind du lieu nguoi dung
    ''' Bind dua lieu cho data combobox
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cacs trang thai theo update, insert cua view
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim objOther As OtherListDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Discipline = rep.GetDisciplineByID(New DisciplineDTO With {.ID = DesciplineID})
                    txtDecisionNo.Text = Discipline.NO
                    txtSignerName.Text = Discipline.SIGNER_NAME
                    txtSignerTitle.Text = Discipline.SIGNER_TITLE
                    If IsDate(Discipline.EFFECT_DATE) Then
                        rdEffectDate.SelectedDate = Discipline.EFFECT_DATE
                    End If
                    If IsDate(Discipline.EXPIRE_DATE) Then
                        rdExpireDate.SelectedDate = Discipline.EXPIRE_DATE
                    End If
                    If IsDate(Discipline.SIGN_DATE) Then
                        rdSignDate.SelectedDate = Discipline.SIGN_DATE
                    End If

                    If IsNumeric(Discipline.STATUS_ID) Then
                        cboStatus.SelectedValue = Discipline.STATUS_ID
                        If Discipline.STATUS_ID = 447 Then
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If
                    End If
                    If IsNumeric(Discipline.DISCIPLINE_OBJ) Then
                        cboDisciplineObj.SelectedValue = Discipline.DISCIPLINE_OBJ
                    End If
                    If IsNumeric(Discipline.DISCIPLINE_TYPE) Then
                        cboDisciplineType.SelectedValue = Discipline.DISCIPLINE_TYPE
                    End If

                    If Discipline.LEVEL_ID IsNot Nothing Then
                        cbolevel.SelectedValue = Discipline.LEVEL_ID.ToString
                        cbolevel.Text = Discipline.LEVEL_NAME
                    End If
                    If Discipline.YEAR IsNot Nothing Then
                        cboYear.SelectedValue = Discipline.YEAR.ToString
                    End If
                    If IsDate(Discipline.DEL_DISCIPLINE_DATE) Then
                        rdDelDisciplineDate.SelectedDate = Discipline.DEL_DISCIPLINE_DATE
                    End If
                    txtNoteDiscipline.Text = Discipline.NOTE_DISCIPLINE
                    txtDisciplineReasonDetail.Text = Discipline.DISCIPLINE_REASON_DETAIL
                    If IsDate(Discipline.VIOLATION_DATE) Then
                        rdViolationDate.SelectedDate = Discipline.VIOLATION_DATE
                    End If
                    If IsDate(Discipline.DECTECT_VIOLATION_DATE) Then
                        rdDetectViolationDate.SelectedDate = Discipline.DECTECT_VIOLATION_DATE
                    End If
                    txtRemarkDiscipline.Text = Discipline.RERARK_DISCIPLINE
                    State_Id = Discipline.STATUS_ID

                    hidID.Value = Discipline.ID.ToString
                    txtUploadFile.Text = Discipline.FILENAME
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                    GetEmployeeDiscipline()
                    GetOrgDiscipline()
                    rgEmployee.Rebind()
                    rgOrg.Rebind()
                    If Discipline.STATUS_ID = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID Then

                        EnableControlAll_Cus(False, RadPane2)

                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True

                        Dim btnEmployee As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEmployee")
                        btnEmployee.Enabled = False
                        Dim btnQD As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnQD")
                        btnQD.Enabled = True
                        Dim btnHSL As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnHSL")
                        btnHSL.Enabled = True
                        Dim btnDeleteEmp As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnDeleteEmp")
                        btnDeleteEmp.Enabled = False

                        rgEmployee.Items(0).Enabled = False

                        CurrentState = CommonMessage.STATE_EDIT

                    Else

                        Dim btnEmployee As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnEmployee")
                        btnEmployee.Enabled = True

                        Dim btnQD As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnQD")
                        btnQD.Enabled = False

                        Dim btnHSL As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnHSL")
                        btnHSL.Enabled = False

                        Dim btnDeleteEmp As RadButton = rgEmployee.MasterTableView.GetItems(GridItemType.CommandItem)(0).FindControl("btnDeleteEmp")
                        btnDeleteEmp.Enabled = True

                    End If
                    If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
                        Org_Discipline.Clear()
                        rgOrg.Visible = False
                        rgEmployee.Visible = True

                    Else
                        Employee_Discipline.Clear()
                        rgEmployee.Visible = False
                        rgOrg.Visible = True
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    Employee_Discipline = New List(Of DisciplineEmpDTO)
                    Org_Discipline = New List(Of DisciplineOrgDTO)
                    'If LogHelper.CurrentUser.USERNAME.ToUpper <> "ADMIN" Then
                    '    cboStatus.Visible = False
                    '    lbStatus.Visible = False
                    'End If
            End Select

            rgEmployee.Rebind()
            rgOrg.Rebind()
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
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Commandd khi click control OnMainToolbar
    ''' Cac trang thai command: luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objDiscipline As New DisciplineDTO
        Dim lstDisciplineEmp As New List(Of DisciplineEmpDTO)
        'Dim objOther As OtherListDTO
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If (cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân") And rgEmployee.Items.Count = 0 Then
                            ShowMessage(Translate("Bạn chưa chọn nhân viên"), NotifyType.Warning)
                            Exit Sub
                        ElseIf cboDisciplineObj.SelectedValue <> 401 And cboDisciplineObj.Text <> "Kỷ luật cá nhân" And rgOrg.Items.Count = 0 Then
                            ShowMessage(Translate("Bạn chưa chọn phòng ban"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If cboDisciplineObj.SelectedValue <> "" Then
                            objDiscipline.DISCIPLINE_OBJ = Decimal.Parse(cboDisciplineObj.SelectedValue)
                        End If
                        If cbolevel.SelectedValue <> "" Then
                            objDiscipline.LEVEL_ID = cbolevel.SelectedValue
                        End If

                        If cboYear.SelectedValue <> "" Then
                            objDiscipline.YEAR = cboYear.SelectedValue
                        End If

                        If cboDisciplineType.SelectedValue <> "" Then
                            objDiscipline.DISCIPLINE_TYPE = cboDisciplineType.SelectedValue
                        End If

                        If cboStatus.SelectedValue <> "" Then
                            objDiscipline.STATUS_ID = cboStatus.SelectedValue
                        End If
                        objDiscipline.DISCIPLINE_EMP = Employee_Discipline
                        objDiscipline.DISCIPLINE_ORG = Org_Discipline

                        If (rdEffectDate.SelectedDate.HasValue And rdExpireDate.SelectedDate.HasValue) Then
                            If (CLng(rdExpireDate.SelectedDate.Value.Subtract(rdEffectDate.SelectedDate.Value).TotalSeconds()) < 0) Then
                                ShowMessage(Translate("Ngày hết hiệu lực phải sau ngày có hiệu lực"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        If (rdViolationDate.SelectedDate.HasValue And rdDetectViolationDate.SelectedDate.HasValue) Then
                            If (CLng(rdDetectViolationDate.SelectedDate.Value.Subtract(rdViolationDate.SelectedDate.Value).TotalSeconds()) < 0) Then
                                ShowMessage(Translate("Ngày phát hiện vi phạm phải sau ngày vi phạm"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        If (rdSignDate.SelectedDate.HasValue And rdEffectDate.SelectedDate.HasValue) Then
                            If (CLng(rdSignDate.SelectedDate.Value.Subtract(rdEffectDate.SelectedDate.Value).TotalSeconds()) > 0) Then
                                ShowMessage(Translate("Ngày có hiệu lực phải sau ngày ký"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        If (rdSignDate.SelectedDate.HasValue And rdExpireDate.SelectedDate.HasValue) Then
                            If (CLng(rdSignDate.SelectedDate.Value.Subtract(rdExpireDate.SelectedDate.Value).TotalSeconds()) > 0) Then
                                ShowMessage(Translate("Ngày ký phải trước ngày hết hiệu lực"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        objDiscipline.FILENAME = txtUpload.Text.Trim
                        objDiscipline.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objDiscipline.UPLOADFILE = "" Then
                            objDiscipline.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objDiscipline.UPLOADFILE = If(objDiscipline.UPLOADFILE Is Nothing, "", objDiscipline.UPLOADFILE)
                        End If
                        If IsDate(rdEffectDate.SelectedDate) Then
                            objDiscipline.EFFECT_DATE = rdEffectDate.SelectedDate
                        End If
                        If IsDate(rdExpireDate.SelectedDate) Then
                            objDiscipline.EXPIRE_DATE = rdExpireDate.SelectedDate
                        End If
                        objDiscipline.SIGNER_NAME = txtSignerName.Text.Trim
                        If IsDate(rdSignDate.SelectedDate) Then
                            objDiscipline.SIGN_DATE = rdSignDate.SelectedDate
                        End If
                        objDiscipline.NO = txtDecisionNo.Text.Trim
                        If IsDate(rdDelDisciplineDate.SelectedDate) Then
                            objDiscipline.DEL_DISCIPLINE_DATE = rdDelDisciplineDate.SelectedDate
                        End If
                        objDiscipline.NOTE_DISCIPLINE = txtNoteDiscipline.Text
                        objDiscipline.DISCIPLINE_REASON_DETAIL = txtDisciplineReasonDetail.Text
                        If IsDate(rdViolationDate.SelectedDate) Then
                            objDiscipline.VIOLATION_DATE = rdViolationDate.SelectedDate
                        End If
                        If IsDate(rdDetectViolationDate.SelectedDate) Then
                            objDiscipline.DECTECT_VIOLATION_DATE = rdDetectViolationDate.SelectedDate
                        End If
                        objDiscipline.RERARK_DISCIPLINE = txtRemarkDiscipline.Text

                        objDiscipline.SIGNER_TITLE = txtSignerTitle.Text
                        objDiscipline.IS_PORTAL = 0
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertDiscipline(objDiscipline, gID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    'Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_DisciplineNewEdit&group=Business&FormType=0&noscroll=1")
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objDiscipline.ID = Decimal.Parse(DesciplineID)
                                If rep.ModifyDiscipline(objDiscipline, gID) Then
                                    'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                        If objDiscipline.DISCIPLINE_TYPE = ProfileCommon.DISCIPLINE_TYPE.LAYOFF_ID And
                        cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" And
                        rgEmployee.Items.Count > 0 And
                        cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then

                        Else
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/")
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
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/DisciplineInfo/" + Down_File)
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
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button khi click vao control ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = "CALL_TERMINATE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim empID = CType(rgEmployee.Items(0), GridDataItem).GetDataKeyValue("HU_EMPLOYEE_ID").ToString
                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_TerminateNewEdit&group=Business&FormType=3&empid=" & empID)
            Else
                'Dim str As String = "getRadWindow().close('1');"
                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                ''POPUPTOLINK
                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Discipline&group=Business")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click khi click vao btnFindSigner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindSinger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindSinger.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindSigner.MustHaveContract = True
            ctrlFindSigner.LoadAllOrganization = False
            ctrlFindSigner.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Selected cua control ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ClearControlValue(txtSignerTitle, txtSignerName)
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                If Employee_Discipline Is Nothing Then
                    Employee_Discipline = New List(Of DisciplineEmpDTO)
                End If
                If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                    Employee_Discipline.Clear()
                    'chkDeductFromSalary.Enabled = True
                End If

                hidEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID

                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New DisciplineEmpDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.HU_EMPLOYEE_ID = emp.ID
                    employee.FULLNAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME

                    Dim checkEmployeeCode As DisciplineEmpDTO = Employee_Discipline.Find(Function(p) p.EMPLOYEE_CODE = emp.EMPLOYEE_CODE)
                    If (Not checkEmployeeCode Is Nothing) Then
                        Continue For
                    End If

                    If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                        'Employee_Discipline.Clear()
                        hidEmpCode.Value = emp.EMPLOYEE_CODE
                        hidOrg_Name_2.Value = emp.ORG_NAME_2
                        Employee_Discipline.Add(employee)
                    Else
                        Employee_Discipline.Add(employee)
                    End If
                Next

                'Dim intMoney As Decimal = 0
                'Dim intAmountToPaid As Decimal = 0
                'If (Employee_Discipline.Count > 0) Then
                '    If rnAmountToPaid.Value IsNot Nothing Then
                '        intAmountToPaid += rnAmountToPaid.Value
                '    End If
                '    intMoney = Utilities.ObjToDecima(intAmountToPaid / Employee_Discipline.Count)
                'End If

                'For index = 0 To Employee_Discipline.Count - 1
                '    Employee_Discipline.Item(index).INDEMNIFY_MONEY = intMoney
                'Next

                rgEmployee.Rebind()
                'For Each i As GridItem In rgEmployee.Items
                '    i.Edit = True
                'Next
                'rgEmployee.Rebind()
            End If

            rep.Dispose()
            isLoadPopup = 0
            'AutoCreate_DecisionNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Selected cua control ctrlFindSigner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                txtSignerName.Text = item.FULLNAME_VN
                txtSignerTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click Cancel cua ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineObj
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalDisciplineObj_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineObj.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New OtherListDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDisciplineObj.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList(validate.ID)
                FillRadCombobox(cboDisciplineObj, dtData, "NAME", "ID")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalDisciplineType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineType.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New OtherListDTO
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboDisciplineType.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                Dim dtData = rep.GetOtherList(validate.ID)
                FillRadCombobox(cboDisciplineType, dtData, "NAME", "ID")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cvalDisciplineReason_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalDisciplineReason.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New ProfileRepository
    '        Dim validate As New OtherListDTO
    '        If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
    '            validate.ID = cboDisciplineReason.SelectedValue
    '            validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateOtherList(validate)
    '        End If
    '        If Not args.IsValid Then
    '            Dim dtData = rep.GetOtherList(validate.ID)
    '            FillRadCombobox(cboDisciplineReason, dtData, "NAME", "ID")
    '        End If
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien item command cua item rgEmployee
    ''' Xu ly tinh toan tien ky luat theo tong nhan vien: Cong tong phat so sanh voi tong phat trc do;
    ''' Chia deu so phat cho tat ca cac nhan vien
    ''' Tim, xoa nhan vien tren luoi ky luat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "CalcEmployee"
                    Dim totalSumIndemMoney As Decimal = 0

                    For Each item As GridDataItem In rgEmployee.Items
                        Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY = CType(item("INDEMNIFY_MONEY").Controls(0), RadNumericTextBox).Value
                        If Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY IsNot Nothing Then
                            totalSumIndemMoney += Employee_Discipline(item.ItemIndex).INDEMNIFY_MONEY
                        End If
                    Next
                    If Employee_Discipline.Count > 0 Then
                        Dim totalMoneyIndemEmp As Decimal = 0

                        Dim colMoneyIndem = CType(rgEmployee.Columns.FindByUniqueName("INDEMNIFY_MONEY"), GridNumericColumn)
                        colMoneyIndem.FooterText = ""

                        If totalSumIndemMoney <> totalMoneyIndemEmp Then
                            colMoneyIndem.Aggregate = GridAggregateFunction.None
                            colMoneyIndem.FooterText = "Lệch: " & Format((totalSumIndemMoney - totalMoneyIndemEmp), "n0")
                        Else
                            colMoneyIndem.Aggregate = GridAggregateFunction.Sum
                        End If
                    End If
                    rgEmployee.Rebind()
                Case "ShareEmployee"
                    Dim countEmp = rgEmployee.Items.Count
                    If countEmp = 0 Then
                        Exit Sub
                    End If

                    rgEmployee.Rebind()
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_Discipline Where (q.HU_EMPLOYEE_ID = i.GetDataKeyValue("HU_EMPLOYEE_ID")) OrElse
                                                                   (q.HU_EMPLOYEE_ID Is Nothing AndAlso q.FULLNAME_TEXT.ToUpper.Equals(i.GetDataKeyValue("FULLNAME_TEXT")))).FirstOrDefault
                        Employee_Discipline.Remove(s)
                    Next

                    rgEmployee.Rebind()
                Case "CreateQD"
                    Dim EmpID As Decimal
                    If State_Id = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
                            'Ca nhan
                            For Each dr As GridDataItem In rgEmployee.Items
                                EmpID = dr.GetDataKeyValue("HU_EMPLOYEE_ID")
                            Next
                        End If
                        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "openQDtab('" & EmpID & "')", True)
                    Else
                        ShowMessage("Quyết định Kỷ luật chưa được phê duyệt. Vui lòng kiểm tra lại !", NotifyType.Warning)
                        Exit Sub
                    End If
                Case "CreateHSL"
                    Dim EmpID As Decimal
                    If State_Id = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
                            'Ca nhan
                            For Each dr As GridDataItem In rgEmployee.Items
                                EmpID = dr.GetDataKeyValue("HU_EMPLOYEE_ID")
                            Next
                        End If
                        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "openHSLtab('" & EmpID & "')", True)
                    Else
                        ShowMessage("Quyết định Kỷ luật chưa được phê duyệt. Vui lòng kiểm tra lại !", NotifyType.Warning)
                        Exit Sub
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Try
            rgEmployee.DataSource = Employee_Discipline

        Catch ex As Exception

        End Try

    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgOrg_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgOrg.NeedDataSource
        Try
            rgOrg.DataSource = Org_Discipline

        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien server validate cho control cval_EffectDate_EpireDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cval_EffectDate_ExpireDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdExpireDate.SelectedDate IsNot Nothing Then
                If rdExpireDate.SelectedDate < rdEffectDate.SelectedDate Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' cusStatus server validate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusStatus.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboStatus.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "DECISION_STATUS"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub btnAddEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEmp.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        If String.IsNullOrEmpty(txtFullnameText.Text.Trim) Then
    '            ShowMessage("Chưa nhập tên nhân viên ngoài hệ thống !", NotifyType.Warning)
    '            txtFullnameText.Focus()
    '            Exit Sub
    '        End If

    '        If String.IsNullOrEmpty(txtOrgText.Text.Trim) Then
    '            ShowMessage("Chưa nhập Phòng ban !", NotifyType.Warning)
    '            txtOrgText.Focus()
    '            Exit Sub
    '        End If

    '        If String.IsNullOrEmpty(txtTitleText.Text.Trim) Then
    '            ShowMessage("Chưa nhập Chức danh !", NotifyType.Warning)
    '            txtTitleText.Focus()
    '            Exit Sub
    '        End If
    '        Dim employee As New DisciplineEmpDTO
    '        employee.FULLNAME = txtFullnameText.Text.Trim
    '        employee.ORG_NAME = txtOrgText.Text.Trim
    '        employee.TITLE_NAME = txtTitleText.Text.Trim
    '        employee.FULLNAME_TEXT = txtFullnameText.Text.Trim
    '        employee.ORG_TEXT = txtOrgText.Text.Trim
    '        employee.TITLE_TEXT = txtTitleText.Text.Trim

    '        Dim checkEmployeeCode = From p In Employee_Discipline Where Not String.IsNullOrEmpty(p.FULLNAME_TEXT) AndAlso p.FULLNAME_TEXT.ToUpper.Equals(txtFullnameText.Text.Trim.ToUpper)
    '        If checkEmployeeCode.Any Then
    '            ShowMessage("Nhân viên đã tồn tại trên lưới !", NotifyType.Warning)
    '            txtFullnameText.Focus()
    '            Exit Sub
    '        End If
    '        Employee_Discipline.Add(employee)
    '        rgEmployee.Rebind()
    '        ClearControlValue(txtFullnameText, txtOrgText, txtTitleText)
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        ExportTemplate("Profile/Import/Template_Import_Discipline_Emp.xls",
    '                              Nothing, Nothing, "Template_Import_Discipline_Emp" & Format(Date.Now, "yyyymmdd"))
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        ctrlUpload.Show()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileRepository
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("STT<>'""'").CopyToDataTable.Rows
                Dim newRow As DataRow = dtData.NewRow
                newRow("STT") = rows("STT")
                newRow("FULLNAME_TEXT") = rows("FULLNAME_TEXT")
                newRow("ORG_TEXT") = rows("ORG_TEXT")
                newRow("TITLE_TEXT") = rows("TITLE_TEXT")
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                For Each item In dtData.Rows
                    Dim employee As New DisciplineEmpDTO
                    employee.FULLNAME = item("FULLNAME_TEXT")
                    employee.ORG_NAME = item("ORG_TEXT")
                    employee.TITLE_NAME = item("TITLE_TEXT")
                    employee.FULLNAME_TEXT = item("FULLNAME_TEXT")
                    employee.ORG_TEXT = item("ORG_TEXT")
                    employee.TITLE_TEXT = item("TITLE_TEXT")
                    Employee_Discipline.Add(employee)
                Next
                rgEmployee.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKyLuat.zip"
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
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
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
#Region "Custom"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay thong tin nhung nhan vien bi ky luat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetEmployeeDiscipline()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileBusinessRepository
                Employee_Discipline = rep.GetEmployeeDesciplineID(Utilities.ObjToDecima(DesciplineID))
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay thong tin nhung phong ban bi ky luat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetOrgDiscipline()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileBusinessRepository
                Org_Discipline = rep.GetOrgDesciplineID(Utilities.ObjToDecima(DesciplineID))
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren paged
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            If phFindSign.Controls.Contains(ctrlFindSigner) Then
                phFindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = True
                    'ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    ctrlFindEmployeePopup.IsHideTerminate = False
                    ctrlFindEmployeePopup.LoadAllOrganization = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then ' Ca nhan
                        ctrlFindEmployeePopup.MultiSelect = True
                        'chkDeductFromSalary.Enabled = False
                    ElseIf cboDisciplineObj.SelectedValue = 400 Then ' tap the
                        ctrlFindEmployeePopup.MultiSelect = True
                    End If
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                    ctrlFindOrgPopup.CheckChildNodes = True
                    ctrlFindOrgPopup.LoadAllOrganization = False

                    phFindOrg.Controls.Add(ctrlFindOrgPopup)

                Case 3
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                    ctrlFindSigner.FunctionName = "ctrlHU_DisciplineNewEdit"
                    ctrlFindSigner.EmployeeOrg = If(LogHelper.CurrentUser.ORG_ID IsNot Nothing, LogHelper.CurrentUser.ORG_ID, 0)
                    ctrlFindSigner.EffectDate = If(rdEffectDate.SelectedDate IsNot Nothing, CDbl(rdEffectDate.SelectedDate.Value.ToOADate), CDbl(New Date().ToOADate))
                    phFindSign.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho combobox cboStatus, cboDisciplineObj, cboDisciplineType, cboDisciplineLevel, cboDisciplineReason
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_DISCIPLINE_STATUS = True
                ListComboData.GET_DISCIPLINE_OBJ = True
                ListComboData.GET_DISCIPLINE_TYPE = True
                ListComboData.GET_LEVEL_DISCIPLINE = True
                rep.GetComboList(ListComboData)
            End If

            'FillDropDownList(cboStatus, ListComboData.LIST_DISCIPLINE_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            Dim dtData As New DataTable
            dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            FillDropDownList(cboDisciplineObj, ListComboData.LIST_DISCIPLINE_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineObj.SelectedValue)
            'FillDropDownList(cboDisciplineType, ListComboData.LIST_DISCIPLINE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cboDisciplineType.SelectedValue)
            FillDropDownList(cbolevel, ListComboData.LIST_LEVEL_DISCIPLINE, "NAME_VN", "ID", Common.Common.SystemLanguage, False, cbolevel.SelectedValue)

            cboDisciplineObj.SelectedIndex = 0
            Dim _filter As New DisciplineListDTO
            Dim dataType = rep.GetDisciplineList(_filter)
            Dim query = (From p In dataType Where p.OBJECT_ID = cboDisciplineObj.SelectedValue)
            If query IsNot Nothing Then
                FillRadCombobox(cboDisciplineType, query.ToList.ToTable, "NAME", "ID", False)
            End If
            'txtDecisionNo.ReadOnly = True
            'txtDecisionNo.Text = "/QĐ-TLSG"
            'cboDisciplineType.SelectedIndex = 0
            cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
            cbolevel.SelectedIndex = 1

            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2018 To Date.Now.Year + 2
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay params 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                    DesciplineID = IDSelect
                    hidID.Value = IDSelect
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Sub GetSigner()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            ClearControlValue(txtSignerName, txtSignerTitle)
            If IsDate(rdEffectDate.SelectedDate) AndAlso cboDisciplineObj.SelectedValue <> "" Then
                Dim signer = store.GET_SIGNER_BY_FUNC(Me.ViewName, rdEffectDate.SelectedDate, cboDisciplineObj.SelectedValue)
                If signer.Rows.Count > 0 Then
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

#End Region

    Private Sub cboDisciplineObj_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDisciplineObj.SelectedIndexChanged
        GetSigner()
        If cboDisciplineObj.SelectedValue = 401 Or cboDisciplineObj.Text = "Kỷ luật cá nhân" Then
            Org_Discipline.Clear()
            rgOrg.Visible = False
            rgEmployee.Visible = True

        Else
            Employee_Discipline.Clear()
            rgEmployee.Visible = False
            rgOrg.Visible = True
        End If
        Dim rep As New ProfileRepository
        Dim _filter As New DisciplineListDTO
        Dim dataType = rep.GetDisciplineList(_filter)
        Dim query = (From p In dataType Where p.OBJECT_ID = cboDisciplineObj.SelectedValue)
        If query IsNot Nothing Then
            FillRadCombobox(cboDisciplineType, query.ToList.ToTable, "NAME", "ID", False)
        End If
    End Sub

    Private Sub rdEffectDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        'GetSigner()
        ClearControlValue(txtSignerTitle, txtSignerName)
        If IsDate(rdEffectDate.SelectedDate) Then
            rdSignDate.SelectedDate = rdEffectDate.SelectedDate
            'If hidEmp.Value <> "" Then
            '    AutoCreate_DecisionNo()
            'End If
        End If
    End Sub

    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            Dim dsData As DataSet = rep.GetHopdongImport()
            'Dim dt_work As New DataTable
            'dt_work = dsData.Tables(4)
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập Tên nhân viên"
                ImportValidate.EmptyValue("FULLNAME_TEXT", row, rowError, isError, sError)

                sError = "Chưa nhập Phòng ban"
                ImportValidate.EmptyValue("ORG_TEXT", row, rowError, isError, sError)

                sError = "Chưa nhập Chức danh"
                ImportValidate.EmptyValue("TITLE_TEXT", row, rowError, isError, sError)


                Dim checkEmployeeCode = From p In Employee_Discipline Where Not String.IsNullOrEmpty(p.FULLNAME_TEXT) AndAlso p.FULLNAME_TEXT.ToUpper.Equals(row("FULLNAME_TEXT").ToString.Trim.ToUpper)
                If checkEmployeeCode.Any Then
                    rowError("FULLNAME_TEXT") = "Nhân viên đã tồn tại trên lưới !"
                    isError = True
                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    If rowError("FULLNAME_TEXT").ToString = "" Then
                        rowError("FULLNAME_TEXT") = row("FULLNAME_TEXT").ToString
                    End If
                    If rowError("ORG_TEXT").ToString = "" Then
                        rowError("ORG_TEXT") = row("ORG_TEXT").ToString
                    End If
                    If rowError("TITLE_TEXT").ToString = "" Then
                        rowError("TITLE_TEXT") = row("TITLE_TEXT").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Dim dsErr = New DataSet
                dsErr.Tables.Add(dtError)
                Session("DIS_EMP") = dsErr
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_Discipline_Emp_Error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack();", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    'Private Sub AutoCreate_DecisionNo()
    '    Dim store As New ProfileStoreProcedure
    '    Try
    '        If IsDBNull(hidEmp.Value) Then
    '            Exit Sub
    '        End If

    '        If rdEffectDate.SelectedDate Is Nothing Then
    '            Exit Sub
    '        End If

    '        If cboYear.SelectedValue = "" Then
    '            Exit Sub
    '        End If

    '        ClearControlValue(txtDecisionNo)
    '        Dim contract_no = store.AUTOCREATE_DISCIPLINENO(Decimal.Parse(hidEmp.Value),
    '                                                        LogHelper.CurrentUser.EMPLOYEE_ID,
    '                                                        rdEffectDate.SelectedDate)

    '        txtDecisionNo.Text = contract_no
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    ''' <lastupdate>13/07/2022</lastupdate>
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

                Case "DeleteOrg"
                    For Each i As GridDataItem In rgOrg.SelectedItems
                        Dim s = (From q In Org_Discipline Where q.ORG_ID = i.GetDataKeyValue("ORG_ID")).FirstOrDefault
                        Org_Discipline.Remove(s)
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
            If Org_Discipline Is Nothing Then
                Org_Discipline = New List(Of DisciplineOrgDTO)
            End If

            For Each org_Check As Common.CommonBusiness.OrganizationDTO In lstOrg
                If Org_Discipline.Any(Function(f) f.ORG_ID = org_Check.ID) Then
                    ShowMessage(Translate("Phòng ban đã tồn tại"), NotifyType.Warning)
                    Exit Sub
                End If
                Dim org As New DisciplineOrgDTO
                org.ORG_ID = org_Check.ID
                org.ORG_NAME = org_Check.NAME_VN
                Org_Discipline.Add(org)
            Next

            If lstOrg.Count > 0 Then
                hidOrgID.Value = lstOrg(0).ID
            End If

            rgOrg.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class