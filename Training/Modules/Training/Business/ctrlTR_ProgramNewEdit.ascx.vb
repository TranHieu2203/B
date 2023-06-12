Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup1 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeePopup2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeePopup3 As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlFindPlanPopup As ctrlFindPlanPopup
    Protected WithEvents ctrlFindEmployeeGridPopup As ctrlFindEmployeePopup
#Region "Property"

    Public Property Employee_list As List(Of RecordEmployeeDTO)
        Get
            Return PageViewState(Me.ID & "_Employee_list")
        End Get
        Set(ByVal value As List(Of RecordEmployeeDTO))
            PageViewState(Me.ID & "_Employee_list") = value
        End Set
    End Property
    '0 - normal
    '1 - Employee
    '2 - Org
    Public Property List_Oganization_ID As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_List_Oganization_ID") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_List_Oganization_ID")
        End Get
    End Property
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property
    Property IsDelete As Decimal
        Get
            Return ViewState(Me.ID & "_ISDELETE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ISDELETE") = value
        End Set
    End Property
    Property IsAdd As Decimal
        Get
            Return ViewState(Me.ID & "_ISADD")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ISADD") = value
        End Set
    End Property
    Property IsFull As Decimal
        Get
            Return ViewState(Me.ID & "_IsFull")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsFull") = value
        End Set
    End Property

    Public Property dtRequest As DataTable
        Get
            If ViewState(Me.ID & "_dtRequest") Is Nothing Then
                ViewState(Me.ID & "_dtRequest") = New DataTable
            End If
            Return ViewState(Me.ID & "_dtRequest")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtRequest") = value
        End Set
    End Property

    Public Property lstOrgCost As List(Of ProgramCostDTO)
        Get
            If ViewState(Me.ID & "_lstOrgCost") Is Nothing Then
                ViewState(Me.ID & "_lstOrgCost") = New List(Of ProgramCostDTO)
            End If
            Return ViewState(Me.ID & "_lstOrgCost")
        End Get
        Set(ByVal value As List(Of ProgramCostDTO))
            ViewState(Me.ID & "_lstOrgCost") = value
        End Set
    End Property

    Property CourseID As Decimal
        Get
            Return ViewState(Me.ID & "_CourseID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_CourseID") = value
        End Set
    End Property

    Property dtType As DataTable
        Get
            Return ViewState(Me.ID & "_dtType")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtType") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            'ToolbarItem.Edit,
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As New DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim rep As New TrainingRepository
        Dim trainingID As Decimal = 0
        Try
            dtData = rep.GetOtherList("PUBLIC_STATUS_TRAIN", True)
            FillRadCombobox(cboPublicStatus, dtData, "NAME", "ID")
            cboPublicStatus.SelectedValue = 788616
            cboPublicStatus.Text = "Đóng"

            dtData = rep.GetOtherList("TR_TRAIN_FIELD", True)
            FillRadCombobox(cboTrainField, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("TR_CURRENCY", True)
            FillRadCombobox(cboUnitPrice, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("TR_TRAINING_TYPE", True)
            FillRadCombobox(cboTrainType, dtData, "NAME", "ID")

            dtType = rep.GetOtherList("TR_TRAINING_TYPE")

            dtData = rep.GetOtherList("HU_TITLE_GROUP", False)
            FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

            dtData = tsp.GetCenters()
            FillRadCombobox(cboCenters, dtData, "NAME", "ID")

            dtData = rep.GetTrPlanByYearOrg2(True, 0, 0, True)
            FillRadCombobox(cboPlan, dtData, "NAME", "ID")

            'chkIsPlan.Checked = True
            GetDataCombo()
            rntxtYear.Value = Date.Now.Year
            GetPlanInYear()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Dim tsp As New TrainingStoreProcedure
            Using rep As New TrainingRepository
                'Hình thức đào tạo
                dtData = rep.GetOtherList("TR_TRAIN_FORM", True)
                FillRadCombobox(cboHinhThuc, dtData, "NAME", "ID")

                'Tính chất nhu cầu
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboTinhchatnhucau, dtData, "NAME", "ID")

                'Thời gian học
                dtData = rep.GetOtherList("TR_DURATION_STUDY", True)
                FillRadCombobox(cboDurationStudy, dtData, "NAME", "ID")

                'Thời lượng - đơn vị
                dtData = rep.GetOtherList("TR_DURATION_UNIT", True)
                FillRadCombobox(cboDurationType, dtData, "NAME", "ID")
                cboDurationType.SelectedValue = 4005 'Giờ

                'Ngôn ngữ giảng dạy
                dtData = rep.GetOtherList("TR_LANGUAGE", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID")

                'Đơn vị chủ trì đào tạo
                dtData = tsp.UnitGetList()
                FillRadCombobox(cboUnit, dtData, "NAME", "ID")

                'Dim lstCenters As List(Of CenterDTO) = rep.GetCenters()
                'lstCenter.DataSource = lstCenters
                'lstCenter.DataValueField = "ID"
                'If (Common.Common.SystemLanguage.Name = "vi-VN") Then
                '    lstCenter.DataTextField = "Name_VN"
                'Else
                '    lstCenter.DataTextField = "Name_EN"
                'End If

                'Trung tâm đào tạo
                dtData = tsp.GetCenters()
                lstCenter.CheckBoxes = True
                lstCenter.AutoPostBack = True
                For Each dr As DataRow In dtData.Rows
                    lstCenter.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing And IDSelect <> -1 Then
                    Refresh("UpdateView")
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Else
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Dim store As New TrainingStoreProcedure
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_NORMAL
                    Dim plan = rep.GetProgramById(IDSelect)
                    hidID.Value = plan.ID
                    rntxtYear.Value = plan.YEAR
                    hidOrgID.Value = plan.ORG_ID
                    txtOrgName.Text = plan.ORG_NAME
                    txtProgramCode.Text = plan.TR_PROGRAM_CODE
                    txtContent.Text = plan.CONTENT
                    txtTargetTrain.Text = plan.TARGET_TRAIN
                    txtVenue.Text = plan.VENUE
                    txtRemark.Text = plan.REMARK
                    rntxtNumberStudent.Value = plan.STUDENT_NUMBER
                    rnExpectClass.Value = plan.EXPECT_CLASS
                    rntxtCostCompany.Value = plan.COST_TOTAL
                    lblFilename.Text = plan.ATTACHFILE
                    'If plan.CERTIFICATE IsNot Nothing Then
                    '    chkAfterTrain.Checked = plan.CERTIFICATE
                    'End If
                    If plan.TR_AFTER_TRAIN IsNot Nothing Then
                        chkAfterTrain.Checked = plan.TR_AFTER_TRAIN
                        chkAfterTrain_CheckedChanged(Nothing, Nothing)
                        rdASS_DATE.SelectedDate = plan.ASS_DATE
                    End If
                    If plan.CERTIFICATE IsNot Nothing Then
                        cboCertificate.Checked = plan.CERTIFICATE
                        cboCertificate_CheckedChanged(Nothing, Nothing)
                    End If
                    txtCertificateName.Text = plan.CERTIFICATE_NAME
                    chkIsPublic.Checked = plan.IS_PUBLIC
                    If chkIsPublic.Checked Then
                        rdPortalFrom.Visible = True
                        rdPortalTo.Visible = True
                        lbPortalFrom.Visible = True
                        lbPortalTo.Visible = True
                    Else
                        rdPortalFrom.Visible = False
                        rdPortalTo.Visible = False
                        lbPortalFrom.Visible = False
                        lbPortalTo.Visible = False
                        rdPortalFrom.Clear()
                        rdPortalTo.Clear()
                    End If
                    If plan.PUBLIC_STATUS_ID IsNot Nothing Then
                        cboPublicStatus.SelectedValue = plan.PUBLIC_STATUS_ID
                    End If
                    If plan.TR_COMMIT IsNot Nothing Then
                        chkTrainCommit.Checked = plan.TR_COMMIT
                    End If
                    If plan.IS_PLAN IsNot Nothing AndAlso plan.IS_PLAN = 0 Then
                        chkIsPlan.Checked = True
                    End If
                    If plan.PORTAL_REGIST_FROM IsNot Nothing Then
                        rdPortalFrom.SelectedDate = plan.PORTAL_REGIST_FROM
                    End If
                    If plan.PORTAL_REGIST_TO IsNot Nothing Then
                        rdPortalTo.SelectedDate = plan.PORTAL_REGIST_TO
                    End If
                    'If plan.GroupTitle.Count > 0 Then
                    '    For Each items As RadComboBoxItem In cboTitleGroup.Items
                    '        For Each titleGroup As ProgramTitleDTO In plan.GroupTitle
                    '            If titleGroup.ID = If(items.Value = "", 0, items.Value) Then
                    '                items.Checked = True
                    '            End If
                    '        Next
                    '    Next
                    '    LoadTitle()
                    'End If
                    'If plan.Titles.Count > 0 Then
                    '    For Each items As RadComboBoxItem In cboTitle.Items
                    '        For Each title As ProgramTitleDTO In plan.Titles
                    '            If title.ID = If(items.Value = "", 0, items.Value) Then
                    '                items.Checked = True
                    '            End If
                    '        Next
                    '    Next
                    'End If

                    If plan.Centers.Count > 0 Then
                        For Each cen As ProgramCenterDTO In plan.Centers
                            Dim item = cboCenters.FindItemByValue(cen.ID)
                            If item IsNot Nothing Then
                                item.Checked = True
                            End If
                        Next
                        LoadTeacher()
                    End If
                    If plan.Lectures.Count > 0 Then
                        For Each tec As ProgramLectureDTO In plan.Lectures
                            Dim item = cboTeachers.FindItemByValue(tec.ID)
                            If item IsNot Nothing Then
                                item.Checked = True
                            End If
                        Next
                    End If
                    If plan.TR_TYPE_ID IsNot Nothing Then
                        cboTrainType.SelectedValue = plan.TR_TYPE_ID
                        cboTrainType_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If plan.DAY_REVIEW_1 IsNot Nothing Then
                        rdReviewDate1.SelectedDate = plan.DAY_REVIEW_1
                    End If
                    If plan.DAY_REVIEW_2 IsNot Nothing Then
                        rdReviewDate2.SelectedDate = plan.DAY_REVIEW_2
                    End If
                    If plan.DAY_REVIEW_3 IsNot Nothing Then
                        rdReviewDate3.SelectedDate = plan.DAY_REVIEW_3
                    End If
                    If plan.ASS_EMP1_ID IsNot Nothing Then
                        HidEmpId1.Value = plan.ASS_EMP1_ID
                        txtEmployeeCode1.Text = plan.ASS_EMP1_NAME
                    End If
                    If plan.ASS_EMP2_ID IsNot Nothing Then
                        HidEmpId2.Value = plan.ASS_EMP2_ID
                        txtEmployeeCode2.Text = plan.ASS_EMP2_NAME
                    End If
                    If plan.ASS_EMP3_ID IsNot Nothing Then
                        HidEmpId3.Value = plan.ASS_EMP3_ID
                        txtEmployeeCode3.Text = plan.ASS_EMP3_NAME
                    End If
                    If plan.TR_CURRENCY_ID IsNot Nothing Then
                        cboUnitPrice.SelectedValue = plan.TR_CURRENCY_ID
                    End If
                    If plan.START_DATE IsNot Nothing Then
                        rdStartDate.SelectedDate = plan.START_DATE
                    End If
                    If plan.END_DATE IsNot Nothing Then
                        rdEndDate.SelectedDate = plan.END_DATE
                    End If
                    If plan.TR_COURSE_ID IsNot Nothing Then
                        cboPlan.SelectedValue = plan.TR_COURSE_ID
                        Using repo As New TrainingRepository
                            Dim course = repo.GetEntryAndFormByCourseID(cboPlan.SelectedValue, Common.Common.SystemLanguage.Name)
                            If course.TR_TRAIN_FIELD_ID IsNot Nothing Then
                                cboTrainField.SelectedValue = course.TR_TRAIN_FIELD_ID
                                txtTrainField.Text = cboTrainField.Text
                            End If
                        End Using
                    End If
                    If plan.PROPERTIES_NEED_ID IsNot Nothing Then
                        cboTinhchatnhucau.SelectedValue = plan.PROPERTIES_NEED_ID
                    End If
                    If plan.TR_TRAIN_FORM_ID IsNot Nothing Then
                        cboHinhThuc.SelectedValue = plan.TR_TRAIN_FORM_ID
                    End If

                    Employee_list = plan.Employees
                    rgEmployee.Rebind()

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    lstOrgCost = New List(Of ProgramCostDTO)
                    rgChiPhi.Rebind()
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            If isLoadPopup = 1 Or isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                    ctrlFindOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID
                End If
                ctrlFindOrgPopup.IS_HadLoad = False
                Select Case isLoadPopup
                    Case 1 'Chọn phòng ban tham gia
                    Case 2 'Chọn phòng ban cấp chi phí
                        ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                        ctrlFindOrgPopup.CheckChildNodes = True
                End Select
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            ElseIf isLoadPopup = 3 Then
                If Not FindPlan.Controls.Contains(ctrlFindPlanPopup) Then
                    Dim dic As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
                    dic.Add("Year", Convert.ToDecimal(rntxtYear.Value))
                    dic.Add("OrgId", Convert.ToDecimal(If(hidOrgID.Value.ToString = "", 1, hidOrgID.Value)))
                    ctrlFindPlanPopup = Me.Register("ctrlFindPlanPopup", "Training", "ctrlFindPlanPopup", "Shared", dic)
                    ctrlFindPlanPopup.MultiSelect = False
                    FindPlan.Controls.Add(ctrlFindPlanPopup)
                    ctrlFindPlanPopup.Show()
                End If
            ElseIf isLoadPopup = 4 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeeGridPopup) Then
                    ctrlFindEmployeeGridPopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeeGridPopup.MustHaveContract = True
                    ctrlFindEmployeeGridPopup.MultiSelect = True
                    FindEmployee.Controls.Add(ctrlFindEmployeeGridPopup)
                End If
            ElseIf isLoadPopup = 5 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup1) Then
                    ctrlFindEmployeePopup1 = Me.Register("ctrlFindEmployeePopup1", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup1.MustHaveContract = True
                    ctrlFindEmployeePopup1.MultiSelect = False
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup1)
                End If
            ElseIf isLoadPopup = 6 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup2) Then
                    ctrlFindEmployeePopup2 = Me.Register("ctrlFindEmployeePopup2", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup2.MustHaveContract = True
                    ctrlFindEmployeePopup2.MultiSelect = False
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup2)
                End If
            ElseIf isLoadPopup = 7 Then
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup3) Then
                    ctrlFindEmployeePopup3 = Me.Register("ctrlFindEmployeePopup3", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup3.MustHaveContract = True
                    ctrlFindEmployeePopup3.MultiSelect = False
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup3)
                End If
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    SetControlState(True)
                    cboPublicStatus.Enabled = False

                Case CommonMessage.STATE_EDIT
                    SetControlState(True)
                    If chkIsPublic.Checked Then
                        cboPublicStatus.Enabled = True
                    Else
                        cboPublicStatus.Enabled = False
                    End If

                Case CommonMessage.STATE_NORMAL
                    SetControlState(False)
                    If chkIsPublic.Checked Then
                        cboPublicStatus.Enabled = True
                    Else
                        cboPublicStatus.Enabled = False
                    End If

            End Select

            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SetControlState(ByVal state As Boolean)
        Try
            rntxtYear.ReadOnly = Not state
            btnFindOrg.Enabled = state
            EnableRadCombo(cboPlan, state)
            txtName.ReadOnly = Not state
            EnableRadCombo(cboHinhThuc, state)
            EnableRadCombo(cboTinhchatnhucau, state)

            rntxtDuration.ReadOnly = Not state
            EnableRadCombo(cboDurationType, state)
            rdStartDate.Enabled = state
            rdEndDate.Enabled = state
            chkIsPublic.Enabled = state
            EnableRadCombo(cboDurationStudy, state)
            rntxtDurationHC.ReadOnly = Not state
            rntxtDurationOT.ReadOnly = Not state

            chkThilai.Enabled = state
            chkIsReimburse.Enabled = state
            btnFindOrgCost.Enabled = state
            btnDel.Enabled = state
            btnCalCost.Enabled = state

            lstPartDepts.Enabled = False
            lstPositions.Enabled = False
            lstWork.Enabled = False

            EnableRadCombo(cboLanguage, state)
            EnableRadCombo(cboUnit, state)
            lstCenter.Enabled = state
            lstLecture.Enabled = state
            txtContent.ReadOnly = Not state
            txtTargetTrain.ReadOnly = Not state
            txtVenue.ReadOnly = Not state
            txtRemark.ReadOnly = Not state
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim gID As Decimal
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New ProgramDTO
                        With obj
                            .IS_PLAN = If(chkIsPlan.Checked, 0, -1)
                            If cboCertificate.Checked AndAlso String.IsNullOrEmpty(txtCertificateName.Text.Trim) Then
                                ShowMessage(Translate("Chưa nhập tên bằng cấp chứng chỉ"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            .YEAR = rntxtYear.Value
                            .ORG_ID = If(String.IsNullOrEmpty(hidOrgID.Value), Nothing, hidOrgID.Value)
                            .TR_PROGRAM_CODE = txtProgramCode.Text
                            If cboPlan.SelectedValue <> "" Then
                                .TR_COURSE_ID = cboPlan.SelectedValue
                            End If
                            If cboTinhchatnhucau.SelectedValue <> "" Then
                                .PROPERTIES_NEED_ID = cboTinhchatnhucau.SelectedValue
                            End If
                            If cboHinhThuc.SelectedValue <> "" Then
                                .TRAIN_FORM_ID = cboHinhThuc.SelectedValue
                            End If
                            If cboTrainField.SelectedValue <> "" Then
                                .TR_TRAIN_FIELD_ID = cboTrainField.SelectedValue
                            End If
                            .CONTENT = txtContent.Text
                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            .REMARK = txtRemark.Text
                            If rdStartDate.SelectedDate IsNot Nothing Then
                                .START_DATE = rdStartDate.SelectedDate
                            End If
                            If rdEndDate.SelectedDate IsNot Nothing Then
                                .END_DATE = rdEndDate.SelectedDate
                            End If

                            If rdPortalFrom.SelectedDate IsNot Nothing Then
                                .PORTAL_REGIST_FROM = rdPortalFrom.SelectedDate
                            End If

                            If rdPortalTo.SelectedDate IsNot Nothing Then
                                .PORTAL_REGIST_TO = rdPortalTo.SelectedDate
                            End If

                            .IS_PUBLIC = chkIsPublic.Checked
                            If cboPublicStatus.SelectedValue <> "" Then
                                .PUBLIC_STATUS_ID = cboPublicStatus.SelectedValue
                            End If
                            If cboHinhThuc.SelectedValue <> "" Then
                                .TRAIN_FORM_ID = cboHinhThuc.SelectedValue
                            End If
                            If IsNumeric(rntxtNumberStudent.Value) Then
                                .STUDENT_NUMBER = rntxtNumberStudent.Value
                            End If
                            If IsNumeric(rnExpectClass.Value) Then
                                .EXPECT_CLASS = rnExpectClass.Value
                            End If
                            If IsNumeric(rntxtCostCompany.Value) Then
                                .COST_TOTAL = rntxtCostCompany.Value
                            End If
                            If cboUnitPrice.SelectedValue <> "" Then
                                .TR_CURRENCY_ID = cboUnitPrice.SelectedValue
                            End If
                            .FILE_ATTACH = lblFilename.Text
                            .UPLOAD_FILE = hidUploadFile.Value.ToString
                            .TR_AFTER_TRAIN = chkAfterTrain.Checked
                            .CERTIFICATE = cboCertificate.Checked
                            .CERTIFICATE_NAME = txtCertificateName.Text
                            .TR_COMMIT = chkTrainCommit.Checked
                            If cboTrainType.SelectedValue <> "" Then
                                .TR_TYPE_ID = cboTrainType.SelectedValue
                            End If
                            If rdReviewDate1.SelectedDate IsNot Nothing Then
                                .DAY_REVIEW_1 = rdReviewDate1.SelectedDate
                            End If
                            If rdReviewDate2.SelectedDate IsNot Nothing Then
                                .DAY_REVIEW_2 = rdReviewDate2.SelectedDate
                            End If
                            If rdReviewDate3.SelectedDate IsNot Nothing Then
                                .DAY_REVIEW_3 = rdReviewDate3.SelectedDate
                            End If
                            .ASS_EMP1_ID = If(HidEmpId1.Value <> "", HidEmpId1.Value, Nothing)
                            .ASS_EMP2_ID = If(HidEmpId2.Value <> "", HidEmpId2.Value, Nothing)
                            .ASS_EMP3_ID = If(HidEmpId3.Value <> "", HidEmpId3.Value, Nothing)
                            .Centers = (From item In cboCenters.CheckedItems Select New ProgramCenterDTO With {.ID = item.Value}).ToList()

                            .Lectures = (From item In cboTeachers.CheckedItems Select New ProgramLectureDTO With {.ID = item.Value}).ToList()

                            .Employees = Employee_list
                            If rdASS_DATE.SelectedDate IsNot Nothing Then
                                .ASS_DATE = rdASS_DATE.SelectedDate
                            End If
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertProgram(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyProgram(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgChiPhi.NeedDataSource, rgEmployee.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Try
            rgChiPhi.VirtualItemCount = lstOrgCost.Count
            rgChiPhi.DataSource = lstOrgCost
            If IsDelete = 1 Then
                rgEmployee.DataSource = Employee_list
                If rntxtNumberStudent.Value IsNot Nothing AndAlso Employee_list.Count > rntxtNumberStudent.Value Then
                    ShowMessage(Translate("Số lượng học viên quá giới hạn cho phép"), Utilities.NotifyType.Warning)
                End If
                IsDelete = 0
                Exit Sub
            End If
            If IsAdd = 1 Then
                If rntxtNumberStudent.Value IsNot Nothing AndAlso Employee_list.Count > rntxtNumberStudent.Value Then
                    ShowMessage(Translate("Số lượng học viên quá giới hạn cho phép"), Utilities.NotifyType.Warning)
                End If
                rgEmployee.DataSource = Employee_list
                IsAdd = 0
                Exit Sub
            End If
            'If Request.Params("ID") IsNot Nothing AndAlso Request.Params("ID") <> -1 Then
            '    rgEmployee.DataSource = Employee_list
            '    Exit Sub
            'End If
            If (cboTitle.CheckedItems.Count > 0 OrElse cboTitleGroup.CheckedItems.Count > 0) AndAlso isFull = 0 Then
                Dim _filter As New RecordEmployeeDTO
                Dim rep As New TrainingRepository
                Dim lstTitleId As New List(Of Decimal)
                Dim total As Integer = 0
                SetValueObjectByRadGrid(rgEmployee, _filter)
                _filter.ORG_ID = Decimal.Parse(If(hidOrgID.Value = "", 1, hidOrgID.Value))
                Dim _param = New ParamDTO With {.ORG_ID = If(hidOrgID.Value = "", 1, hidOrgID.Value),
                                                .IS_DISSOLVE = 0}

                For Each items As RadComboBoxItem In cboTitle.CheckedItems
                    lstTitleId.Add(items.Value)
                Next
                Dim lstTitleGR As New List(Of Decimal)
                For Each items As RadComboBoxItem In cboTitleGroup.CheckedItems
                    lstTitleGR.Add(items.Value)
                Next
                Dim MaximumRows As Integer
                Dim Sorts As String = rgEmployee.MasterTableView.SortExpressions.GetSortString()

                If Sorts IsNot Nothing Then
                    Employee_list = rep.GetPlanEmployee(_filter, _param, rgEmployee.CurrentPageIndex, lstTitleId, lstTitleGR, rgEmployee.PageSize, MaximumRows, Sorts)
                Else
                    Employee_list = rep.GetPlanEmployee(_filter, _param, rgEmployee.CurrentPageIndex, lstTitleId, lstTitleGR, rgEmployee.PageSize, MaximumRows)
                End If
                rgEmployee.VirtualItemCount = MaximumRows
                rgEmployee.DataSource = Employee_list
                If rntxtNumberStudent.Value IsNot Nothing AndAlso MaximumRows > rntxtNumberStudent.Value Then
                    ShowMessage(Translate("Số lượng học viên quá giới hạn cho phép"), Utilities.NotifyType.Warning)
                End If
            Else
                If Employee_list Is Nothing Then
                    Employee_list = New List(Of RecordEmployeeDTO)
                End If
                rgEmployee.DataSource = Employee_list
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboTitleGroup_ItemChecked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTitleGroup.ItemChecked
        Try
            LoadTitle()
            If cboTitleGroup.CheckedItems.Count > 0 Then
                IsFull = 1
                rgEmployee.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lblFilename.Text = ""
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Training/TrainingInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        lblFilename.Text = file.FileName
                        hidUploadFile.Value = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS, XLSX, TXT, CTR, DOC, DOCX, XML, PNG, JPG, BITMAP, JPEG, PDF"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboTitle_ItemChecked(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTitle.ItemChecked
        Try
            If cboTitle.CheckedItems.Count > 0 Then
                IsFull = 1
                rgEmployee.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindOrg_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 1
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindEmployee1_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee1.Click
        Try
            isLoadPopup = 5
            UpdateControlState()
            ctrlFindEmployeePopup1.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindEmployee2_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee2.Click
        Try
            isLoadPopup = 6
            UpdateControlState()
            ctrlFindEmployeePopup2.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindEmployee3_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee3.Click
        Try
            isLoadPopup = 7
            UpdateControlState()
            ctrlFindEmployeePopup3.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindOrgCost_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrgCost.Click
        Try
            ReadCost()

            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnDel_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDel.Click
        Try
            ReadCost()

            For Each item As GridDataItem In rgChiPhi.SelectedItems
                If item.GetDataKeyValue("ORG_ID") IsNot Nothing Then
                    Dim obj = (From p In lstOrgCost Where p.ORG_ID = item.GetDataKeyValue("ORG_ID")).FirstOrDefault
                    lstOrgCost.Remove(obj)
                End If
            Next
            If rgChiPhi.SelectedItems.Count > 0 Then
                rgChiPhi.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ReadCost()
        For Each org As ProgramCostDTO In lstOrgCost
            For Each item As GridDataItem In rgChiPhi.MasterTableView.Items
                If org.ORG_ID = item.GetDataKeyValue("ORG_ID") Then
                    Dim rntxtCost As RadNumericTextBox = CType(item.FindControl("ValueTS"), RadNumericTextBox)
                    org.COST_COMPANY = rntxtCost.Value
                End If
            Next
        Next
    End Sub
    Protected Sub btnCalCost_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalCost.Click
        Try
            If lblDVT.ToolTip = "" Then
                ShowMessage(Translate("Chưa chọn khóa đào tạo"), Utilities.NotifyType.Warning)
                Exit Sub
            ElseIf rntxtNumberStudent.Value Is Nothing OrElse rntxtNumberStudent.Value = 0 Then
                ShowMessage(Translate("Khóa đào tạo này chưa chọn học viên"), Utilities.NotifyType.Warning)
                Exit Sub
            Else
                Dim total As Decimal = 0
                For Each item As GridDataItem In rgChiPhi.MasterTableView.Items
                    Dim rntxtCost As RadNumericTextBox = CType(item.FindControl("ValueTS"), RadNumericTextBox)
                    total += rntxtCost.Value 'cost
                Next

                If total = 0 Then
                    ShowMessage(Translate("Chưa khai báo chi phí đào tạo cho phòng ban."), Utilities.NotifyType.Warning, 8)
                    Exit Sub
                ElseIf total < 0 Then
                    ShowMessage(Translate("Tổng chi phí đào tạo quá bé. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning, 8)
                    Exit Sub
                Else
                    Select Case lblDVT.ToolTip.ToUpper
                        Case "USD"
                            rntxtCostCompanyUS.Value = total
                            rntxtCostOfStudentUS.Value = (total / CDec(rntxtNumberStudent.Value))
                        Case Else
                            rntxtCostCompany.Value = total
                            rntxtCostOfStudent.Value = (total / CDec(rntxtNumberStudent.Value))
                    End Select
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPlanPopup_PlanSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindPlanPopup.PlanSelected
        Dim lstPlan As New List(Of PlanDTO)
        Dim plan As New PlanDTO
        Dim rep As New TrainingRepository
        Dim store As New TrainingStoreProcedure
        Try
            lstPlan = CType(ctrlFindPlanPopup.SelectedPlan, List(Of PlanDTO))
            If lstPlan.Count <> 0 Then
                Dim item = lstPlan(0)
                Dim id As Decimal = item.ID
                plan = rep.GetPlanById(id)
                rntxtYear.Value = plan.YEAR
                hidOrgID.Value = plan.ORG_ID
                txtOrgName.Text = plan.ORG_NAME
                txtProgramCode.Text = plan.TR_PLAN_CODE
                txtContent.Text = plan.CONTENT
                txtTargetTrain.Text = plan.TARGET_TRAIN
                txtVenue.Text = plan.VENUE
                txtRemark.Text = plan.REMARK
                rntxtNumberStudent.Value = plan.STUDENT_NUMBER
                rnExpectClass.Value = plan.EXPECT_CLASS
                rntxtCostCompany.Value = plan.COST_TOTAL
                lblFilename.Text = plan.ATTACHFILE
                If plan.CERTIFICATE IsNot Nothing Then
                    chkAfterTrain.Checked = plan.CERTIFICATE
                    chkAfterTrain_CheckedChanged(Nothing, Nothing)
                End If
                If plan.TR_AFTER_TRAIN IsNot Nothing Then
                    chkAfterTrain.Checked = plan.TR_AFTER_TRAIN
                    chkAfterTrain_CheckedChanged(Nothing, Nothing)
                End If
                If plan.CERTIFICATE IsNot Nothing Then
                    cboCertificate.Checked = plan.CERTIFICATE
                    cboCertificate_CheckedChanged(Nothing, Nothing)
                End If
                'rdASS_DATE.SelectedDate=plan.ass_da
                txtCertificateName.Text = plan.CERTIFICATE_NAME
                If plan.TR_TRAIN_FIELD_ID IsNot Nothing Then
                    cboTrainField.SelectedValue = plan.TR_TRAIN_FIELD_ID
                    txtTrainField.Text = cboTrainField.Text
                End If
                If plan.TR_COMMIT IsNot Nothing Then
                    chkTrainCommit.Checked = plan.TR_COMMIT
                End If

                'If plan.GroupTitle.Count > 0 Then
                '    For Each titleGroup As PlanTitleDTO In plan.GroupTitle
                '        Dim xItem = cboTitleGroup.FindItemByValue(titleGroup.ID)
                '        If xItem IsNot Nothing Then
                '            xItem.Checked = True
                '        End If
                '    Next
                '    LoadTitle()
                'End If
                'If plan.Titles.Count > 0 Then
                '    For Each title As PlanTitleDTO In plan.Titles
                '        Dim xItem = cboTitle.FindItemByValue(title.ID)
                '        If xItem IsNot Nothing Then
                '            xItem.Checked = True
                '        End If
                '    Next
                'End If
                Dim ds = store.GET_TITLE_GROUP_BY_PLAN_AND_YEAR(plan.TR_COURSE_ID, rntxtYear.Value)
                If ds IsNot Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim xitem = cboTitleGroup.FindItemByValue(row("TITLE_GROUP_ID"))
                            If xitem IsNot Nothing Then
                                xitem.Checked = True
                            End If
                        Next row
                    End If
                    LoadTitle()
                    If ds.Tables(1).Rows.Count > 0 Then
                        For Each row As DataRow In ds.Tables(1).Rows
                            Dim xitem = cboTitle.FindItemByValue(row("HU_TITLE_ID"))
                            If xitem IsNot Nothing Then
                                xitem.Checked = True
                            End If
                        Next row
                    End If
                End If

                If plan.Centers.Count > 0 Then
                    For Each cen As PlanCenterDTO In plan.Centers
                        Dim xItem = cboCenters.FindItemByValue(cen.ID)
                        If xItem IsNot Nothing Then
                            xItem.Checked = True
                        End If
                    Next
                    LoadTeacher()
                End If
                If plan.ProgramLecture.Count > 0 Then
                    For Each tec As ProgramLectureDTO In plan.ProgramLecture
                        Dim xItem = cboTeachers.FindItemByValue(tec.ID)
                        If xItem IsNot Nothing Then
                            xItem.Checked = True
                        End If
                    Next
                End If
                If plan.DAY_REVIEW_1 IsNot Nothing Then
                    rdReviewDate1.SelectedDate = plan.DAY_REVIEW_1
                End If
                If plan.DAY_REVIEW_2 IsNot Nothing Then
                    rdReviewDate2.SelectedDate = plan.DAY_REVIEW_2
                End If
                If plan.DAY_REVIEW_3 IsNot Nothing Then
                    rdReviewDate3.SelectedDate = plan.DAY_REVIEW_3
                End If
                If plan.TR_TYPE_ID IsNot Nothing Then
                    cboTrainType.SelectedValue = plan.TR_TYPE_ID
                End If
                If plan.TR_CURRENCY_ID IsNot Nothing Then
                    cboUnitPrice.SelectedValue = plan.TR_CURRENCY_ID
                End If
                If plan.EXPECT_TR_FROM IsNot Nothing Then
                    rdStartDate.SelectedDate = plan.EXPECT_TR_FROM
                End If
                If plan.EXPECT_TR_TO IsNot Nothing Then
                    rdEndDate.SelectedDate = plan.EXPECT_TR_TO
                End If
                If plan.TR_COURSE_ID IsNot Nothing Then
                    cboPlan.SelectedValue = plan.TR_COURSE_ID
                End If
                If plan.PROPERTIES_NEED_ID IsNot Nothing Then
                    cboTinhchatnhucau.SelectedValue = plan.PROPERTIES_NEED_ID
                End If
                If plan.TR_TRAIN_FORM_ID IsNot Nothing Then
                    cboHinhThuc.SelectedValue = plan.TR_TRAIN_FORM_ID
                End If
                rgEmployee.Rebind()
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Select Case isLoadPopup
                Case 1
                    Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
                    If orgItem IsNot Nothing Then
                        hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                        txtOrgName.Text = orgItem.name_vn
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.description_path)
                    End If
                    If hidOrgID.Value <> "" AndAlso cboTitle.CheckedItems.Count > 0 Then
                        rgEmployee.Rebind()
                    End If
                    GetPlanInYear()
                Case 2
                    Dim lstIds = e.SelectedValues
                    If lstIds IsNot Nothing AndAlso lstIds.Count > 0 AndAlso e.SelectedTexts.Count = e.SelectedValues.Count Then
                        For i = 0 To e.SelectedValues.Count - 1
                            Dim orgID = e.SelectedValues(i)
                            If Not lstOrgCost.Exists(Function(x) x.ORG_ID.ToString = orgID) Then
                                lstOrgCost.Add(New ProgramCostDTO With {.ORG_ID = e.SelectedValues(i),
                                                                        .ORG_NAME = e.SelectedTexts(i),
                                                                        .COST_COMPANY = 0})
                            End If
                        Next
                        rgChiPhi.Rebind()
                    End If

            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindPlanPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindPlanPopup.CancelClicked
        isLoadPopup = 0
        'chkIsPlan.Checked = True
    End Sub
    Protected Sub GetPlanInYear()
        Try
            'If rntxtYear.Value IsNot Nothing And hidOrgID.Value <> "" Then
            '    Using rep As New TrainingRepository
            '        dtRequest = rep.GetTrPlanByYearOrg(True, rntxtYear.Value, hidOrgID.Value)
            '        FillRadCombobox(cboPlan, dtRequest, "NAME", "REQUEST_ID")
            '    End Using
            'Else
            '    cboPlan.ClearSelection()
            '    cboPlan.Items.Clear()
            '    hidOrgID.Value = Nothing
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlan.SelectedIndexChanged
        Dim rep As New TrainingStoreProcedure
        Dim ds As New DataSet
        Try
            If cboPlan.SelectedValue <> "" And IsNumeric(rntxtYear.Value) Then
                Dim bb = 0
                If cboPlan.SelectedValue <> "" Then
                    bb = rep.CHECK_BB(cboPlan.SelectedValue)
                End If
                If bb <> 0 Then
                    Dim dtData As DataTable
                    Using rep1 As New TrainingRepository
                        dtData = rep1.GetOtherList("TR_PROPERTIES_NEED", True)
                    End Using
                    For Each Item In dtData.Rows
                        If Item("CODE").ToString = "BB" Then
                            cboTinhchatnhucau.Text = Item("NAME")
                            cboTinhchatnhucau.SelectedValue = Item("ID")
                        End If
                    Next
                End If


                ds = rep.GET_TITLE_GROUP_BY_PLAN_AND_YEAR(cboPlan.SelectedValue, rntxtYear.Value)
                cboTitleGroup.ClearCheckedItems()
                cboTrainField.ClearSelection()
                If ds IsNot Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim item = cboTitleGroup.FindItemByValue(row("TITLE_GROUP_ID"))
                            If item IsNot Nothing Then
                                item.Checked = True
                            End If
                        Next row
                        LoadTitle()
                    End If
                    If ds.Tables(1).Rows.Count > 0 Then
                        For Each row As DataRow In ds.Tables(1).Rows
                            Dim item = cboTitle.FindItemByValue(row("HU_TITLE_ID"))
                            If item IsNot Nothing Then
                                item.Checked = True
                            End If
                        Next row
                    End If
                    Using repo As New TrainingRepository
                        Dim course = repo.GetEntryAndFormByCourseID(cboPlan.SelectedValue, Common.Common.SystemLanguage.Name)
                        If course.TR_TRAIN_FIELD_ID IsNot Nothing Then
                            cboTrainField.SelectedValue = course.TR_TRAIN_FIELD_ID
                            txtTrainField.Text = cboTrainField.Text
                        End If
                    End Using
                End If
            End If
            rgEmployee.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub lstCenter_ItemCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles lstCenter.ItemCheck
        Try
            Dim Centers_ID As String = ""
            For Each item As RadListBoxItem In lstCenter.CheckedItems
                Centers_ID &= item.Value & ","
            Next
            LoadTeacher(Centers_ID)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup1_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup1.EmployeeSelected
        Dim Employee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Employee = CType(ctrlFindEmployeePopup1.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If Employee.Count > 0 Then
                Dim item = Employee(0)
                txtEmployeeCode1.Text = item.EMPLOYEE_CODE + " - " + item.FULLNAME_VN
                HidEmpId1.Value = item.ID
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup2_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup2.EmployeeSelected
        Dim Employee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Employee = CType(ctrlFindEmployeePopup2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If Employee.Count > 0 Then
                Dim item = Employee(0)
                txtEmployeeCode2.Text = item.EMPLOYEE_CODE + " - " + item.FULLNAME_VN
                HidEmpId2.Value = item.ID
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup3_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup3.EmployeeSelected
        Dim Employee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Employee = CType(ctrlFindEmployeePopup3.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If Employee.Count > 0 Then
                Dim item = Employee(0)
                txtEmployeeCode3.Text = item.EMPLOYEE_CODE + " - " + item.FULLNAME_VN
                HidEmpId3.Value = item.ID
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEmployeeCode1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode1.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtEmployeeCode1.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEmployeeCode1.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEmployeeCode1.Text = ""
                    HidEmpId1.Value = ""
                ElseIf Count = 1 Then
                    Dim emp = EmployeeList(0)
                    txtEmployeeCode1.Text = emp.EMPLOYEE_CODE + " - " + emp.FULLNAME_VN
                    HidEmpId1.Value = emp.ID
                End If
            Else
                txtEmployeeCode1.Text = ""
                HidEmpId1.Value = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEmployeeCode2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode2.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtEmployeeCode2.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEmployeeCode2.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEmployeeCode2.Text = ""
                    HidEmpId2.Value = ""
                ElseIf Count = 1 Then
                    Dim emp = EmployeeList(0)
                    txtEmployeeCode2.Text = emp.EMPLOYEE_CODE + " - " + emp.FULLNAME_VN
                    HidEmpId2.Value = emp.ID
                End If
            Else
                txtEmployeeCode2.Text = ""
                HidEmpId2.Value = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub txtEmployeeCode3_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEmployeeCode3.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtEmployeeCode3.Text <> "" Then
                Dim Count = 0
                Dim EmployeeList As List(Of CommonBusiness.EmployeePopupFindListDTO)
                Dim _filter As New CommonBusiness.EmployeePopupFindListDTO
                _filter.EMPLOYEE_CODE = txtEmployeeCode3.Text
                EmployeeList = rep.GetEmployeeFind(_filter, Count, "EMPLOYEE_CODE asc", Nothing)
                If Count <= 0 Then
                    ShowMessage(Translate("Nhân viên vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    txtEmployeeCode3.Text = ""
                    HidEmpId3.Value = ""
                ElseIf Count = 1 Then
                    Dim emp = EmployeeList(0)
                    txtEmployeeCode3.Text = emp.EMPLOYEE_CODE + " - " + emp.FULLNAME_VN
                    HidEmpId3.Value = emp.ID
                End If
            Else
                txtEmployeeCode3.Text = ""
                HidEmpId3.Value = ""
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeeGridPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeGridPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            lstCommonEmployee = CType(ctrlFindEmployeeGridPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                If Employee_list Is Nothing Then
                    Employee_list = New List(Of RecordEmployeeDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_list.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        Continue For
                    End If
                    Dim employee As New RecordEmployeeDTO
                    employee.ID = emp.ID
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME_VN = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    employee.JOIN_DATE = emp.JOIN_DATE
                    employee.TITLE_GROUP_ID = emp.TITLE_GROUP_ID
                    employee.TITLE_GROUP_NAME = emp.TITLE_GROUP_NAME
                    employee.EMP_TYPE = -1
                    employee.STATUS_ID = 0
                    employee.EMP_TYPE_NAME = "Ngoại lệ"
                    Employee_list.Add(employee)
                Next
            End If
            isLoadPopup = 0
            IsAdd = 1
            rgEmployee.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 4
                    UpdateControlState()
                    ctrlFindEmployeeGridPopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_list Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_list.Remove(s)
                    Next
                    IsDelete = 1
                    rgEmployee.Rebind()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub chkIsPlan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsPlan.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If chkIsPlan.Checked Then
                Dim startTime As DateTime = DateTime.UtcNow
                isLoadPopup = 3
                UpdateControlState()
                ctrlFindPlanPopup.Show()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub cboCertificate_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCertificate.CheckedChanged
        Try
            txtCertificateName.ClearValue()
            lbCertificate.Visible = cboCertificate.Checked
            txtCertificateName.Visible = cboCertificate.Checked
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub chkAfterTrain_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAfterTrain.CheckedChanged
        Try
            rdASS_DATE.ClearValue()
            blbAss_Date.Visible = chkAfterTrain.Checked
            rdASS_DATE.Visible = chkAfterTrain.Checked
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub cboTrainType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTrainType.SelectedIndexChanged

        Try
            ClearControlValue(rdReviewDate1, rdReviewDate2, rdReviewDate3, HidEmpId1, HidEmpId2, HidEmpId3, txtEmployeeCode1, txtEmployeeCode2, txtEmployeeCode3)
            If cboTrainType.SelectedValue <> "" Then
                Dim _check = (From p In dtType Where p("ID") = cboTrainType.SelectedValue And p("CODE").ToString.ToUpper.Equals("DG")).Any
                lbDateReview1.Visible = _check
                lbDateReview2.Visible = _check
                lbDateReview3.Visible = _check
                rdReviewDate1.Visible = _check
                rdReviewDate2.Visible = _check
                rdReviewDate3.Visible = _check
                lbEmployeeCode1.Visible = _check
                lbEmployeeCode2.Visible = _check
                lbEmployeeCode3.Visible = _check
                txtEmployeeCode1.Visible = _check
                txtEmployeeCode2.Visible = _check
                txtEmployeeCode3.Visible = _check
                btnFindEmployee1.Visible = _check
                btnFindEmployee2.Visible = _check
                btnFindEmployee3.Visible = _check
            Else
                lbDateReview1.Visible = False
                lbDateReview2.Visible = False
                lbDateReview3.Visible = False
                rdReviewDate1.Visible = False
                rdReviewDate2.Visible = False
                rdReviewDate3.Visible = False
                lbEmployeeCode1.Visible = False
                lbEmployeeCode2.Visible = False
                lbEmployeeCode3.Visible = False
                txtEmployeeCode1.Visible = False
                txtEmployeeCode2.Visible = False
                txtEmployeeCode3.Visible = False
                btnFindEmployee1.Visible = False
                btnFindEmployee2.Visible = False
                btnFindEmployee3.Visible = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cboCenters_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCenters.SelectedIndexChanged
        Try
            LoadTeacher()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If lblFilename.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Training/TrainingInfo/" + hidUploadFile.Value + lblFilename.Text)
                ZipFiles(strPath_Down, 2)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dtData As New DataTable
            dtData.TableName = "DATA"
            ExportTemplate("Training\Import\Program_Employee.xlsx",
                                      dtData, Nothing,
                                      "TemplateImport_Program_Employee_" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        ctrlUpload2.Show()
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try

            Dim tempPath As String = "Excel"
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload2.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)

                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                If workbook.Worksheets.GetSheetByCodeName("Sheet1") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            ImportData(dsDataPrepare.Tables(0).Select("STT <>'""' ").CopyToDataTable)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub LoadTeacher(ByVal sCenter As List(Of PlanCenterDTO), ByVal sLecture As List(Of LectureDTO))
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim listCEN As String = ""
        Try
            'Giảng viên
            sCenter.ForEach(Sub(x) listCEN &= x.ID & ",")
            dtData = tsp.GetLecture(listCEN)
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    lstLecture.Items.Clear()
                    lstLecture.CheckBoxes = True
                    For Each dr As DataRow In dtData.Rows
                        Dim lbi As RadListBoxItem = New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString())
                        If sLecture.Count > 0 Then
                            For Each tea As LectureDTO In sLecture
                                If dr("ID").ToString() = tea.ID.ToString() Then
                                    lbi.Checked = True
                                    Exit For
                                End If
                            Next
                        End If
                        lstLecture.Items.Add(lbi)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub LoadTeacher(ByVal sCenter As String)
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Try
            'Giảng viên
            lstLecture.Items.Clear()
            lstLecture.CheckBoxes = True
            dtData = tsp.GetLecture(sCenter)
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        Dim lbi As RadListBoxItem = New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString())
                        lstLecture.Items.Add(lbi)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim fileNameZip As String
            If _ID = 1 Then
                fileNameZip = lblFilename.Text.Trim
            Else
                fileNameZip = lblFilename.Text.Trim
            End If
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

    Protected Sub LoadTitle()
        cboTitle.Items.Clear()
        Try
            Dim rep As New TrainingRepository
            Dim lstTitleGroup As New List(Of Decimal)
            For Each item In cboTitleGroup.CheckedItems
                lstTitleGroup.Add(item.Value)
            Next
            Dim lstTitle = rep.GetTitleByGroupID(lstTitleGroup)
            FillRadCombobox(cboTitle, lstTitle, "NAME_VN", "ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub LoadTeacher()
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim listCEN As String = ""
        Try
            Dim cboCheck As IList(Of RadComboBoxItem) = cboCenters.CheckedItems
            For Each item As RadComboBoxItem In cboCheck
                listCEN += item.Value + ","
            Next

            If listCEN = "" Then
                cboTeachers.DataSource = Nothing
                cboTeachers.Items.Clear()
                Exit Sub
            End If
            dtData = tsp.GetLecture(listCEN)
            FillRadCombobox(cboTeachers, dtData, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtOrgName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtOrgName.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Try
            If txtOrgName.Text.Trim <> "" Then
                Dim List_org = rep.GetOrganizationLocationTreeView()
                Dim orgList = (From p In List_org Where p.NAME_VN.ToUpper.Contains(txtOrgName.Text.Trim.ToUpper)).ToList
                If orgList.Count <= 0 Then
                    ShowMessage(Translate("Phòng ban vừa tìm chưa được phân quyền hoặc không tồn tại."), Utilities.NotifyType.Warning)
                    ClearControlValue(hidOrgID, txtOrgName)
                ElseIf orgList.Count = 1 Then
                    hidOrgID.Value = orgList(0).ID
                    txtOrgName.Text = orgList(0).NAME_VN
                Else
                    List_Oganization_ID = (From p In orgList Select p.ID).ToList
                    btnFindOrg_click(Nothing, Nothing)
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub chkIsPublic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsPublic.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If chkIsPublic.Checked Then
                cboPublicStatus.Enabled = True
                rdPortalFrom.Visible = True
                rdPortalTo.Visible = True
                lbPortalFrom.Visible = True
                lbPortalTo.Visible = True
            Else
                cboPublicStatus.Enabled = False
                rdPortalFrom.Visible = False
                rdPortalTo.Visible = False
                lbPortalFrom.Visible = False
                lbPortalTo.Visible = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dtData As DataTable,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dtData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Sub ImportData(ByVal dtData As DataTable)
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim rep As New TrainingRepository
            Dim commonRep As New Common.CommonRepository
            Dim dtError As DataTable = dtData.Clone
            dtError.TableName = "DATA"
            Dim IsError As Boolean = False
            Dim lstDtl As New List(Of RecordEmployeeDTO)
            For Each row As DataRow In dtData.Rows
                Dim sError As String = ""
                Dim rowError = dtError.NewRow
                Dim obj As New RecordEmployeeDTO

                sError = "Chưa nhập Mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, IsError, sError)

                sError = "Chưa chọn loại học viên"
                ImportValidate.EmptyValue("EMP_TYPE_NAME", row, rowError, IsError, sError)

                If Not IsDBNull(row("EMPLOYEE_CODE").ToString) OrElse Not String.IsNullOrEmpty(row("EMPLOYEE_CODE").ToString) Then
                    Dim emp = rep.GetEmployeeByCode(row("EMPLOYEE_CODE"))
                    If emp = 0 Then
                        IsError = True
                        rowError("EMPLOYEE_CODE") = "Mã nhân viên " & row("EMPLOYEE_CODE").ToString & " không tồn tại trong hệ thống"
                    Else
                        obj.ID = emp
                        If (From p In Employee_list Where p.ID = emp).Any Then
                            IsError = True
                            rowError("EMPLOYEE_CODE") = "Nhân viên đã tồn tại"
                        Else
                            Dim objEmp = commonRep.GetEmployeeID(emp)
                            obj.JOIN_DATE = objEmp.JOIN_DATE
                            obj.ORG_NAME = objEmp.ORG_NAME
                            obj.TITLE_NAME_VN = objEmp.TITLE_NAME
                            obj.TITLE_GROUP_NAME = objEmp.TITLE_GROUP_NAME
                            obj.TITLE_NAME_VN = objEmp.TITLE_NAME
                            obj.FULLNAME_VN = objEmp.FULLNAME_VN
                            obj.EMP_TYPE_NAME = row("EMP_TYPE_NAME")
                            obj.EMP_TYPE = CDec(If(Not IsDBNull(row("EMP_TYPE")) AndAlso Not String.IsNullOrEmpty(row("EMP_TYPE")), row("EMP_TYPE").ToString, Nothing))
                        End If
                    End If
                End If
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString

                lstDtl.Add(obj)

                If IsError Then
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    If IsDBNull(rowError("EMPLOYEE_CODE")) Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("STT") = row("STT").ToString
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Session("TR_PROGRAM_EMPLOYEE_ERROR") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_Program_Employee_Error')", True)
                ShowMessage(Translate("Import không thành công, chi tiết lỗi tệp tin đính kèm"), NotifyType.Warning)
            Else
                For Each item In lstDtl
                    Employee_list.Add(item)
                Next
                IsAdd = 1
                rgEmployee.Rebind()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class