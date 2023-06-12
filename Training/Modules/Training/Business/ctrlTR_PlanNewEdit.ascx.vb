Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports ProfileDAL
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_PlanNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindMEmployeePopup As ctrlFindMEmployeePopup
    Protected WithEvents ctrlFindTRRequestPopup As ctrlFindTRRequestPopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected repHF As HistaffFrameworkRepository
    Public Class lstOrgDTO
        Public Property ORG_ID As Decimal
        Public Property ORG_NAME As String
    End Class
    Public Class lstTitleDTO
        Public Property TITLE_ID As Decimal
        Public Property TITLE_NAME As String
    End Class
    Public Class lstWORKINVOLVEDTO
        Public Property WI_ID As Decimal
        Public Property WI_NAME As String
    End Class
#Region "Property"

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
    Public Property lstcostDT As List(Of CostDetailDTO)
        Get
            If ViewState(Me.ID & "_lstcostDT") Is Nothing Then
                ViewState(Me.ID & "_lstcostDT") = New List(Of CostDetailDTO)
            End If
            Return ViewState(Me.ID & "_lstcostDT")
        End Get
        Set(ByVal value As List(Of CostDetailDTO))
            ViewState(Me.ID & "_lstcostDT") = value
        End Set
    End Property
    Public Property lstEmployee As List(Of RequestEmpDTO)
        Get
            If ViewState(Me.ID & "_lstEmployee") Is Nothing Then
                ViewState(Me.ID & "_lstEmployee") = New List(Of RequestEmpDTO)
            End If
            Return ViewState(Me.ID & "_lstEmployee")
        End Get
        Set(ByVal value As List(Of RequestEmpDTO))
            ViewState(Me.ID & "_lstEmployee") = value
        End Set
    End Property
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property

    Property ParticipcatedDetpNames As List(Of String)
        Get
            Return ViewState(Me.ID & "_ParticipcatedDetpNames")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_ParticipcatedDetpNames") = value
        End Set
    End Property

    Property ParticipcatedDetpIds As List(Of String)
        Get
            Return ViewState(Me.ID & "_ParticipcatedDetpIds")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_ParticipcatedDetpIds") = value
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

    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

#End Region

#Region "Page"

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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim rep As New TrainingRepository()
            GetDataCombo()
            Dim lstCenters As List(Of CenterDTO) = rep.GetCenters()
            FillRadCombobox(cboCenter, lstCenters, "NAME_VN", "ID")

            rntxtYear.Value = Date.Now.Year

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try

            Select Case Message

                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetPlanById(IDSelect)
                    hidID.Value = obj.ID
                    If obj.PLAN_TYPE IsNot Nothing Then
                        chkPlanType.Checked = obj.PLAN_TYPE
                    End If
                    txtOrgName.Text = obj.ORG_NAME
                    hidOrgID.Value = obj.ORG_ID.ToString
                    txtRemark.Text = obj.REMARK
                    txtPlanCode.Text = obj.TR_PLAN_CODE
                    If obj.TR_TYPE_ID.HasValue Then
                        cboType.SelectedValue = obj.TR_TYPE_ID
                        cboType_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    If obj.TR_COURSE_ID IsNot Nothing Then cboCourse.SelectedValue = obj.TR_COURSE_ID
                    cboCourse_SelectedIndexChanged(Nothing, Nothing)
                    txtContent.Text = obj.CONTENT
                    If obj.EXPECT_TR_FROM.HasValue Then
                        rdExpectFrom.SelectedDate = obj.EXPECT_TR_FROM
                    End If
                    If obj.EXPECT_TR_TO.HasValue Then
                        rdExpectTo.SelectedDate = obj.EXPECT_TR_TO
                    End If
                    If obj.DAY_REVIEW_1.HasValue Then
                        rdDateReview1.SelectedDate = obj.DAY_REVIEW_1
                    End If
                    If obj.DAY_REVIEW_2.HasValue Then
                        rdDateReview2.SelectedDate = obj.DAY_REVIEW_2
                    End If
                    If obj.DAY_REVIEW_3.HasValue Then
                        rdDateReview3.SelectedDate = obj.DAY_REVIEW_3
                    End If

                    If obj.TR_COMMIT IsNot Nothing Then
                        chkCommit.Checked = obj.TR_COMMIT
                    End If
                    If obj.EXPECT_CLASS.HasValue Then
                        rnExpectClass.Value = obj.EXPECT_CLASS
                    End If
                    If obj.TR_AFTER_TRAIN IsNot Nothing Then
                        chkAfterTrain.Checked = obj.TR_AFTER_TRAIN
                    End If
                    If obj.CERTIFICATE IsNot Nothing Then
                        chkCertificate.Checked = obj.CERTIFICATE
                        chkCertificate_CheckedChanged(Nothing, Nothing)
                    End If
                    txtCertificateName.Text = obj.CERTIFICATE_NAME

                    If obj.TR_REQUEST_ID.HasValue Then
                        hidRequestID.Value = obj.TR_REQUEST_ID
                    End If
                    If obj.TR_CURRENCY_ID.HasValue Then
                        cboCurrency.SelectedValue = obj.TR_CURRENCY_ID
                    End If

                    For Each value In obj.Centers
                        Dim item = cboCenter.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next

                    For Each value In obj.GroupTitle
                        Dim item = cboTitleGroup.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    cboTitleGroup_SelectedIndexChanged(Nothing, Nothing)
                    For Each value In obj.Titles
                        Dim item = cboTitles.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next

                    cboHinhThuc.SelectedValue = obj.TR_TRAIN_FORM_ID
                    cboTinhchatnhucau.SelectedValue = obj.PROPERTIES_NEED_ID

                    txtTargetTrain.Text = obj.TARGET_TRAIN
                    txtVenue.Text = obj.VENUE
                    rntxtTotal.Value = obj.COST_TOTAL
                    rntxtStudents.Value = obj.STUDENT_NUMBER
                    'lstcostDT.Clear()
                    'lstcostDT = rep.GetPlan_Cost_Detail(obj.ID)
                    'If obj.ATTACHFILE IsNot Nothing Then
                    '    lblFilename.Text = "..." & Right(obj.ATTACHFILE, 10)
                    '    lblFilename.NavigateUrl = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Training/Upload"), obj.ATTACHFILE)
                    'End If

                    If obj.ATTACHFILE IsNot Nothing Then
                        txtUpload.Text = obj.ATTACHFILE
                    End If

                    repHF = New HistaffFrameworkRepository
                    Dim dtData1 = repHF.ExecuteToDataSet("PKG_TRAINING.PLAN_CHECK_REQUEST", New List(Of Object)({obj.ID})).Tables(0)

                    If dtData1 IsNot Nothing Then
                        If dtData1.Rows.Count >= 1 Then
                            RadPane2.Enabled = False
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

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

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New PlanDTO

                        If chkCertificate.Checked And String.IsNullOrEmpty(txtCertificateName.Text) Then
                            ShowMessage(Translate("Phải nhập tên bằng cấp/chứng chỉ khi check chọn"), Utilities.NotifyType.Warning)
                        End If
                        With obj
                            .YEAR = rntxtYear.Value
                            .TR_PLAN_CODE = txtPlanCode.Text
                            'Có check thì lưu = 0 và không check thì lưu = -1
                            .PLAN_TYPE = If(chkPlanType.Checked, False, True)
                            If cboType.SelectedValue <> "" Then
                                .TR_TYPE_ID = cboType.SelectedValue
                            End If
                            .CONTENT = txtContent.Text
                            If IsDate(rdExpectFrom.SelectedDate) Then
                                .EXPECT_TR_FROM = rdExpectFrom.SelectedDate
                            End If
                            If IsDate(rdExpectTo.SelectedDate) Then
                                .EXPECT_TR_TO = rdExpectTo.SelectedDate
                            End If
                            .TR_COMMIT = chkCommit.Checked
                            If IsNumeric(rnExpectClass.Value) Then
                                .EXPECT_CLASS = rnExpectClass.Value
                            End If
                            If IsDate(rdDateReview1.SelectedDate) Then
                                .DAY_REVIEW_1 = rdDateReview1.SelectedDate
                            End If
                            If IsDate(rdDateReview2.SelectedDate) Then
                                .DAY_REVIEW_2 = rdDateReview2.SelectedDate
                            End If
                            If IsDate(rdDateReview3.SelectedDate) Then
                                .DAY_REVIEW_3 = rdDateReview3.SelectedDate
                            End If

                            If cboCurrency.SelectedValue <> "" Then
                                .TR_CURRENCY_ID = cboCurrency.SelectedValue
                            End If
                            If IsNumeric(hidRequestID.Value) Then
                                .TR_REQUEST_ID = hidRequestID.Value
                            End If
                            .TR_AFTER_TRAIN = chkAfterTrain.Checked
                            .CERTIFICATE = chkCertificate.Checked
                            .CERTIFICATE_NAME = txtCertificateName.Text
                            If hidOrgID.Value IsNot Nothing Then .ORG_ID = hidOrgID.Value
                            If cboCourse.SelectedValue <> "" Then
                                .TR_COURSE_ID = Decimal.Parse(cboCourse.SelectedValue)
                            End If

                            If .Months_NAME <> "" Then
                                .Months_NAME = .Months_NAME.Substring(0, .Months_NAME.Length - 2)
                            End If
                            .PROPERTIES_NEED_ID = Decimal.Parse(cboTinhchatnhucau.SelectedValue)
                            .TR_TRAIN_FORM_ID = Decimal.Parse(cboHinhThuc.SelectedValue)

                            .STUDENT_NUMBER = rntxtStudents.Value

                            .COST_TOTAL = rntxtTotal.Value

                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            .REMARK = txtRemark.Text
                            .Titles = (From item In cboTitles.CheckedItems Select New PlanTitleDTO With {.ID = item.Value}).ToList()
                            .Centers = (From item In cboCenter.CheckedItems Select New PlanCenterDTO With {.ID = item.Value}).ToList()
                            If cboCenter.CheckedItems.Count > 0 Then
                                .Centers_NAME = cboCenter.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            End If

                            If cboTitles.CheckedItems.Count > 0 Then
                                .Titles_NAME = cboTitles.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            End If

                            .ATTACHFILE = txtUpload.Text
                            .Plan_Emp = lstEmployee
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertPlan(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyPlan(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub btnFindOrg_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            If sender IsNot Nothing Then
                List_Oganization_ID = New List(Of Decimal)
            End If
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Select Case isLoadPopup
                Case 2
                    Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
                    If orgItem IsNot Nothing Then
                        hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                        txtOrgName.Text = orgItem.name_vn
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.description_path)
                    End If
                Case 1
                    Dim lstIds = e.SelectedValues
                    If lstIds IsNot Nothing AndAlso lstIds.Count > 0 AndAlso e.SelectedTexts.Count = e.SelectedValues.Count Then
                        Me.ParticipcatedDetpIds = e.SelectedValues
                        Me.ParticipcatedDetpNames = e.SelectedTexts
                    End If
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindMEmployeePopup.CancelClicked, ctrlFindTRRequestPopup.CancelClicked
        isLoadPopup = 0

    End Sub

    Private Sub cboCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        txtLinhvuc.ClearValue()
        cboTitleGroup.ClearCheckedItems()
        cboTitles.Items.Clear()
        Dim store As New TrainingStoreProcedure

        If cboCourse.SelectedValue <> "" Then
            Dim bb = 0
            If cboCourse.SelectedValue <> "" Then
                bb = store.CHECK_BB(cboCourse.SelectedValue)
            End If
            If bb <> 0 Then
                Dim dtData As DataTable
                Using rep As New TrainingRepository
                    dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                End Using
                For Each Item In dtData.Rows
                    If Item("CODE").ToString = "BB" Then
                        cboTinhchatnhucau.Text = Item("NAME")
                        cboTinhchatnhucau.SelectedValue = Item("ID")
                    End If
                Next
            End If

            Using rep As New TrainingRepository
                Try
                    Dim course = rep.GetEntryAndFormByCourseID(cboCourse.SelectedValue, Common.Common.SystemLanguage.Name)
                    txtLinhvuc.Text = course.TR_TRAIN_FIELD_NAME
                    'If course.LST_TITLE_GROUP IsNot Nothing Then
                    '    FillRadCombobox(cboTitleGroup, course.LST_TITLE_GROUP, "NAME_VN", "ID")
                    'End If
                    'If course.LST_TITLE IsNot Nothing Then
                    '    FillRadCombobox(cboTitles, course.LST_TITLE, "NAME_VN", "ID")
                    'End If
                    For Each value In course.LST_TITLE_GROUP
                        Dim item = cboTitleGroup.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    cboTitleGroup_SelectedIndexChanged(Nothing, Nothing)
                    For Each value In course.LST_TITLE
                        Dim item = cboTitles.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                Catch ex As Exception
                    DisplayException(Me.ViewName, Me.ID, ex)
                End Try
            End Using
        End If
    End Sub

    Private Sub chkPlanType_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPlanType.CheckedChanged

        ClearValue()
        ''ControlChangeStatus(chkPlanType.Checked)
        If chkPlanType.Checked Then
            If Not IsNumeric(rntxtYear.Value) Then
                ShowMessage(Translate("Phải nhập năm"), Utilities.NotifyType.Warning)
                Exit Sub
            End If
            isLoadPopup = 4
            UpdateControlState()
            ctrlFindTRRequestPopup.Show()
        End If
    End Sub

    Private Sub chkCertificate_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCertificate.CheckedChanged
        txtCertificateName.ClearValue()
        lbCertificateName.Visible = chkCertificate.Checked
        txtCertificateName.Visible = chkCertificate.Checked
    End Sub

    Private Sub ctrlFindTRRequestPopup_TRRquestSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindTRRequestPopup.TRRequestSelected

        Dim rep As New TrainingRepository
        Try
            Dim objRequest = rep.GetTrainingRequestsByID(New RequestDTO With {.ID = ctrlFindTRRequestPopup.SelectedTRRequestID(0)})
            If objRequest IsNot Nothing Then
                If objRequest.COURSE_ID.HasValue Then
                    cboCourse.SelectedValue = objRequest.COURSE_ID
                End If
                hidRequestID.Value = objRequest.ID
                cboCourse_SelectedIndexChanged(Nothing, Nothing)
                If objRequest.ORG_ID IsNot Nothing Then
                    hidOrgID.Value = objRequest.ORG_ID
                End If
                txtOrgName.Text = objRequest.ORG_NAME
                txtOrgName.ToolTip = Utilities.DrawTreeByString(objRequest.COM_DESC)

                If objRequest.TRAIN_FORM_ID.HasValue Then
                    cboHinhThuc.SelectedValue = objRequest.TRAIN_FORM_ID
                End If
                If objRequest.PROPERTIES_NEED_ID.HasValue Then
                    cboTinhchatnhucau.SelectedValue = objRequest.PROPERTIES_NEED_ID
                End If
                txtLinhvuc.Text = objRequest.TRAIN_FIELD
                txtVenue.Text = objRequest.TR_PLACE
                txtContent.Text = objRequest.CONTENT
                txtTargetTrain.Text = objRequest.TARGET_TRAIN
                If objRequest.EXPECTED_DATE.HasValue Then
                    rdExpectFrom.SelectedDate = objRequest.EXPECTED_DATE
                End If
                If objRequest.EXPECT_DATE_TO.HasValue Then
                    rdExpectTo.SelectedDate = objRequest.EXPECT_DATE_TO
                End If
                If objRequest.TR_COMMIT.HasValue Then
                    chkCommit.Checked = objRequest.TR_COMMIT
                End If
                If objRequest.TRAINER_NUMBER.HasValue Then
                    rntxtStudents.Value = objRequest.TRAINER_NUMBER
                End If
                If objRequest.EXPECTED_COST.HasValue Then
                    rntxtTotal.Value = objRequest.EXPECTED_COST
                End If
                If objRequest.TR_CURRENCY_ID.HasValue Then
                    cboCurrency.SelectedValue = objRequest.TR_CURRENCY_ID
                End If
                chkAfterTrain.Checked = False
                'If objRequest.lstCenters IsNot Nothing And objRequest.lstCenters.Count > 0 Then
                '    FillRadCombobox(cboCenter, objRequest.lstCenters, "NAME_VN", "ID")
                'End If
                For Each value In objRequest.lstCenters
                    Dim item = cboCenter.FindItemByValue(value.ID.ToString)
                    If item IsNot Nothing Then
                        item.Checked = True
                    End If
                Next

                If objRequest.CERTIFICATE.HasValue Then
                    chkCertificate.Checked = objRequest.CERTIFICATE
                End If
                chkCertificate_CheckedChanged(Nothing, Nothing)
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboType.SelectedIndexChanged
        ClearControlValue(rdDateReview1, rdDateReview2, rdDateReview3)
        If cboType.SelectedValue <> "" Then
            Dim _check = (From p In dtType Where p("ID") = cboType.SelectedValue And p("CODE").ToString.ToUpper.Equals("DG")).Any
            lbDateReview1.Visible = _check
            lbDateReview2.Visible = _check
            lbDateReview3.Visible = _check
            rdDateReview1.Visible = _check
            rdDateReview2.Visible = _check
            rdDateReview3.Visible = _check
            rdDateReview1.Enabled = _check
            rdDateReview2.Enabled = _check
            rdDateReview3.Enabled = _check
        Else
            lbDateReview1.Visible = False
            lbDateReview2.Visible = False
            lbDateReview3.Visible = False
            rdDateReview1.Visible = False
            rdDateReview2.Visible = False
            rdDateReview3.Visible = False
        End If
    End Sub

    Private Sub cboTitleGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitleGroup.SelectedIndexChanged
        cboTitles.Items.Clear()
        ''Dim x = cboTitles.Items.Count
        Try
            Dim rep As New TrainingRepository
            Dim lstTitleGroup As New List(Of Decimal)
            For Each item In cboTitleGroup.CheckedItems
                lstTitleGroup.Add(item.Value)
            Next
            Dim lstTitle = rep.GetTitleByGroupID(lstTitleGroup)
            FillRadCombobox(cboTitles, lstTitle, "NAME_VN", "ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()

        Try
            If pgFindMultiEmp.Controls.Contains(ctrlFindMEmployeePopup) Then
                pgFindMultiEmp.Controls.Remove(ctrlFindMEmployeePopup)
            End If
            If phFindRequest.Controls.Contains(ctrlFindTRRequestPopup) Then
                phFindRequest.Controls.Remove(ctrlFindTRRequestPopup)
            End If
            If isLoadPopup = 1 OrElse isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlfindorgpopup", "common", "ctrlfindorgpopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                If List_Oganization_ID IsNot Nothing AndAlso List_Oganization_ID.Count > 0 Then
                    ctrlFindOrgPopup.Bind_Find_ValueKeys = List_Oganization_ID

                End If
                ctrlFindOrgPopup.IS_HadLoad = False
                Select Case isLoadPopup
                    Case 2 'Chọn phòng ban tổ chức
                    Case 1 'Chọn phòng ban tham gia
                        ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                        ctrlFindOrgPopup.CheckChildNodes = True
                End Select
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            ElseIf isLoadPopup = 3 Then
                ctrlFindMEmployeePopup = Me.Register("ctrlFindMEmployeePopup", "Common", "ctrlFindMEmployeePopup")
                ctrlFindMEmployeePopup.MustHaveContract = True
                ctrlFindMEmployeePopup.IS_3B = 2
                pgFindMultiEmp.Controls.Add(ctrlFindMEmployeePopup)
                ctrlFindMEmployeePopup.MultiSelect = True
            ElseIf isLoadPopup = 4 Then
                ctrlFindTRRequestPopup = Me.Register("ctrlFindTRRequestPopup", "Common", "ctrlFindTRRequestPopup")
                ctrlFindTRRequestPopup.Year = rntxtYear.Value
                phFindRequest.Controls.Add(ctrlFindTRRequestPopup)
            End If

        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository
                dtData = rep.GetOtherList("TR_CURRENCY", True)
                Dim lst As List(Of CourseDTO)
                lst = rep.GetCourseList()
                FillRadCombobox(cboCourse, lst, "NAME", "ID")
                repHF = New HistaffFrameworkRepository
                dtData = repHF.ExecuteToDataSet("PKG_TRAINING.HINH_THUC_DAO_TAO", New List(Of Object)({})).Tables(0)
                FillRadCombobox(cboHinhThuc, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboTinhchatnhucau, dtData, "NAME", "ID")
                dtType = rep.GetOtherList("TR_TRAINING_TYPE", True)
                FillRadCombobox(cboType, dtType, "NAME", "ID")
                dtType = rep.GetOtherList("TR_TRAINING_TYPE")
                dtData = rep.GetOtherList("TR_CURRENCY", True)
                FillRadCombobox(cboCurrency, dtData, "NAME", "ID")

                dtData = rep.GetOtherList("HU_TITLE_GROUP")
                FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
    'Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
    '    ctrlUpload2.MaxFileInput = 1
    '    ctrlUpload2.isMultiple = False
    '    ctrlUpload2.AllowedExtensions = "pdf,png,doc,docx,xls,xlsx,jpg,jpeg"
    '    ctrlUpload2.Show()
    'End Sub
    'Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
    '    Dim fileName As String
    '    Try
    '        If ctrlUpload2.UploadedFiles.Count > 1 Then
    '            ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
    '            Exit Sub
    '        Else
    '            If ctrlUpload2.UploadedFiles.Count > 0 Then
    '                Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
    '                fileName = Server.MapPath("~/ReportTemplates/Training/Upload")
    '                If Not Directory.Exists(fileName) Then
    '                    Directory.CreateDirectory(fileName)
    '                End If
    '                fileName = System.IO.Path.Combine(fileName, file.FileName)
    '                file.SaveAs(fileName, True)
    '                lblFilename.Text = file.FileName
    '                lblFilename.NavigateUrl = fileName

    '            Else
    '                ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
    '            End If

    '        End If
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
    '    End Try
    'End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUpload.Text = ""
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Training/Upload/")

            If Not Directory.Exists(Server.MapPath("~/ReportTemplates/Training/Upload")) Then
                Directory.CreateDirectory(Server.MapPath("~/ReportTemplates/Training/Upload"))
            End If

            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                Dim finfo As New AttachFilesDTO
                ListAttachFile = New List(Of AttachFilesDTO)
                Dim file As UploadedFile = ctrlUpload2.UploadedFiles(ctrlUpload2.UploadedFiles.Count - 1)
                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                    System.IO.Directory.CreateDirectory(strPath)
                    strPath = strPath
                    fileName = System.IO.Path.Combine(strPath, file.FileName)
                    file.SaveAs(fileName, True)
                    txtUpload.Text = file.FileName
                    finfo.FILE_PATH = strPath + file.FileName
                    finfo.ATTACHFILE_NAME = file.FileName
                    finfo.CONTROL_NAME = "ctrlTR_PlanNewEdit"
                    finfo.FILE_TYPE = file.GetExtension
                    ListAttachFile.Add(finfo)
                Else
                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Training/Upload/" + txtUploadFile.Text + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            Else
                ShowMessage(Translate("Tài liệu đào tạo chưa được đính kèm."), NotifyType.Warning)
                Exit Sub

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim fileNameZip As String
            If _ID = 1 Then
                fileNameZip = txtUpload.Text.Trim
            Else
                fileNameZip = txtUpload.Text.Trim
            End If
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
        End Try
    End Sub

    Private Sub cusCourse_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCourse.ServerValidate
        Try
            If cboCourse.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusHinhThuc_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusHinhThuc.ServerValidate
        Try
            If cboHinhThuc.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusTinhchatnhucau_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTinhchatnhucau.ServerValidate
        Try
            If cboTinhchatnhucau.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtOrgName_TextChanged(sender As Object, e As EventArgs) Handles txtOrgName.TextChanged
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

    Private Sub ControlChangeStatus(ByVal _status As Boolean)
        cboCourse.Enabled = _status
        cboHinhThuc.Enabled = _status
        cboTinhchatnhucau.Enabled = _status
        txtLinhvuc.Enabled = _status
        txtVenue.Enabled = _status
        txtContent.Enabled = _status
        txtTargetTrain.Enabled = _status
        rdExpectFrom.Enabled = _status
        rdExpectTo.Enabled = _status
        chkCommit.Enabled = _status
        rntxtStudents.Enabled = _status
        rntxtTotal.Enabled = _status
        cboCurrency.Enabled = _status
        rnExpectClass.Enabled = _status
        cboCenter.Enabled = _status
        chkAfterTrain.Enabled = _status
        chkCertificate.Enabled = _status
        txtCertificateName.Enabled = _status
        cboType.Enabled = _status
        rdDateReview1.Enabled = _status
        rdDateReview2.Enabled = _status
        rdDateReview3.Enabled = _status
        cboTitleGroup.Enabled = _status
        cboTitles.Enabled = _status
        btnUpload.Enabled = _status
        btnDownload.Enabled = _status
        txtRemark.Enabled = _status
    End Sub

    Private Sub ClearValue()
        ClearControlValue(cboCourse, cboHinhThuc, cboTinhchatnhucau, txtLinhvuc, txtVenue, txtContent, txtTargetTrain,
                          rdExpectFrom, rdExpectTo, chkCommit, rntxtStudents, rntxtTotal, cboCurrency, rnExpectClass, cboCenter,
                          chkAfterTrain, chkCertificate, txtCertificateName, _cboType, rdDateReview1, rdDateReview2, rdDateReview3,
                          cboTitles, txtUpload, txtUploadFile, txtRemark)

    End Sub

End Class