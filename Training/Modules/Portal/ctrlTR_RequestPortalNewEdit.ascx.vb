Imports Framework.UI
Imports Common
Imports Training.TrainingBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports System.IO

Public Class ctrlTR_RequestPortalNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property dtCourse As DataTable
        Get
            Return ViewState(Me.ID & "_dtCourse")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtCourse") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Dim Editable As Boolean

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

    Property AllowOverStudent As Integer
        Get
            Return ViewState(Me.ID & "_AllowOverStudent")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_AllowOverStudent") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)

        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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

                If Request.Params("success") IsNot Nothing Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End If

                Editable = (Request.Params("EDITABLE") IsNot Nothing)
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
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                   
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            Select Case isLoadPopup
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
            End Select

        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Dim tsp As New TrainingStoreProcedure
            Using rep As New TrainingRepository
                'Hình thức đào tạo
                dtData = rep.GetOtherList("TR_TRAIN_FORM", True)
                FillRadCombobox(cboTrainForm, dtData, "NAME", "ID")

                'Tính chất nhu cầu
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboPropertiesNeed, dtData, "NAME", "ID")

                'Trung tâm đào tạo
                dtData = tsp.GetCenters()
                lstCenter.CheckBoxes = True
                For Each dr As DataRow In dtData.Rows
                    lstCenter.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                Next

                'Đơn vị chủ trì đào tạo
                dtData = tsp.UnitGetList()
                FillRadCombobox(cboUnits, dtData, "NAME", "ID")

                'Đơn vị tiền tệ
                dtData = rep.GetOtherList("TR_CURRENCY", True)
                FillRadCombobox(cboCurrency, dtData, "NAME", "ID")

            End Using

            rdRequestDate.SelectedDate = Date.Now
            rntxtYear.Value = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
                        Dim obj As New RequestDTO

                        If rdRequestDate.SelectedDate >= rdExpectedDate.SelectedDate Then
                            ShowMessage(Translate("Thời gian dự kiến phải lớn hơn ngày gửi"), Utilities.NotifyType.Warning)
                            Exit Sub
                        ElseIf rdRequestDate.SelectedDate >= rdStartDate.SelectedDate Then
                            ShowMessage(Translate("Thời gian bắt đầu phải lớn hơn ngày gửi"), Utilities.NotifyType.Warning)
                            Exit Sub
                        ElseIf rdStartDate.SelectedDate IsNot Nothing Then
                            If rdExpectedDate.SelectedDate > rdStartDate.SelectedDate Then
                                ShowMessage(Translate("Thời gian bắt đầu phải lớn hơn hoặc bằng thời gian dự kiến"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        With obj
                            Dim cou_id = cboPlan.SelectedValue
                            Dim dtPlan As DataRow
                            If cbIrregularly.Checked Then
                                dtPlan = dtCourse.Select("ID = " + cou_id).FirstOrDefault
                            Else
                                dtPlan = dtCourse.Select("PLAN_ID = " + cou_id).FirstOrDefault
                            End If
                            If dtPlan Is Nothing Then
                                ShowMessage(Translate("Bạn phải chọn Khóa đào tạo"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If cbIrregularly.Checked Then
                                '
                            Else
                                If dtPlan.Table.Columns.Contains("PLAN_ID") Then
                                    .TR_PLAN_ID = dtPlan("PLAN_ID").ToString
                                End If
                            End If

                            If hidOrgID.Value IsNot Nothing Then
                                .ORG_ID = hidOrgID.Value
                            End If
                            .YEAR = rntxtYear.Value
                            .IS_IRREGULARLY = cbIrregularly.Checked
                            If dtPlan.Table.Columns.Contains("ID") Then
                                .COURSE_ID = dtPlan("ID").ToString
                            End If
                            If cboTrainForm.SelectedValue <> "" Then
                                .TRAIN_FORM_ID = cboTrainForm.SelectedValue
                            End If
                            If cboPropertiesNeed.SelectedValue <> "" Then
                                .PROPERTIES_NEED_ID = cboPropertiesNeed.SelectedValue
                            End If
                            .EXPECTED_DATE = rdExpectedDate.SelectedDate
                            .START_DATE = rdStartDate.SelectedDate
                            .CONTENT = txtContent.Text
                            If cboUnits.SelectedValue <> "" Then
                                .UNIT_ID = cboUnits.SelectedValue
                            End If
                            .EXPECTED_COST = rntxtExpectedCost.Value
                            If cboCurrency.SelectedValue <> "" Then
                                .TR_CURRENCY_ID = Decimal.Parse(cboCurrency.SelectedValue)
                            End If
                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            If hidSenderID.Value <> "" Then
                                .REQUEST_SENDER_ID = hidSenderID.Value
                            End If
                            .REQUEST_DATE = rdRequestDate.SelectedDate
                            .ATTACH_FILE = lblFilename.Text
                            .REMARK = txtRemark.Text

                            .lstCenters = (From item In lstCenter.CheckedItems Select New PlanCenterDTO With {.ID = item.Value}).ToList()
                            .lstTeachers = (From item In lstTeacher.CheckedItems Select New LectureDTO With {.ID = item.Value}).ToList()
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequest(obj, lstEmployee, gID) Then
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_RequestPortalNewEdit&success=1")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_RequestPortalNewEdit")

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                AllowOverStudent = 1
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonNo Then
                Dim cou_id = cboPlan.SelectedValue
                '
                If cbIrregularly.Checked Then
                    ' FillRadCombobox(cboPlan, dtCourse, "NAME", "ID")
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindOrg_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
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
                    GetPlanInYearOrg()
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub cbIrregularly_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cbIrregularly.Click
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        ctrlUpload2.MaxFileInput = 1
        ctrlUpload2.isMultiple = False
        ctrlUpload2.AllowedExtensions = "pdf,png,doc,docx,xls,xlsx,jpg,jpeg"
        ctrlUpload2.Show()
    End Sub
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Try
            If ctrlUpload2.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload2.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload/Request")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)
                    lblFilename.Text = file.FileName
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlan.SelectedIndexChanged
        Try
            If cboPlan.SelectedValue = "" Then
                ClearControl()
            ElseIf dtCourse Is Nothing Then
                ClearControl()
            ElseIf dtCourse.Rows.Count = 0 Then
                ClearControl()
            Else
                Dim cou_id = cboPlan.SelectedValue
                Dim dtPlan As DataRow
                If cbIrregularly.Checked Then
                    dtPlan = dtCourse.Select("ID = " + cou_id).FirstOrDefault()
                Else
                    dtPlan = dtCourse.Select("PLAN_ID = " + cou_id).FirstOrDefault()
                End If

                If dtPlan Is Nothing Then
                    ClearControl()
                ElseIf dtPlan.Table.Rows.Count = 0 Then
                    ClearControl()
                Else
                    '1
                    If dtPlan.Table.Columns.Contains("PROGRAM_GROUP") Then
                        txtProgramGroup.Text = dtPlan("PROGRAM_GROUP").ToString
                    End If

                    '2
                    If dtPlan.Table.Columns.Contains("TRAIN_FIELD") Then
                        txtTrainField.Text = dtPlan("TRAIN_FIELD").ToString
                    End If

                    '3
                    If dtPlan.Table.Columns.Contains("TRAIN_FORM_ID") Then
                        cboTrainForm.SelectedValue = dtPlan("TRAIN_FORM_ID").ToString
                    End If

                    '4
                    If dtPlan.Table.Columns.Contains("PROPERTIES_NEED_ID") Then
                        cboPropertiesNeed.SelectedValue = dtPlan("PROPERTIES_NEED_ID").ToString
                    End If

                    '5
              
                    '6
                    If dtPlan.Table.Columns.Contains("CENTERS") Then
                        Dim listCenter() As String = dtPlan("CENTERS").ToString().Split(New Char() {","})
                        For Each item As RadListBoxItem In lstCenter.Items
                            For Each cen As String In listCenter
                                If item.Value = cen Then
                                    item.Checked = True
                                    Exit For
                                End If
                            Next
                        Next

                        LoadTeacher()
                    End If

                    '6.1
                    If dtPlan.Table.Columns.Contains("UNIT_ID") Then
                        cboUnits.SelectedValue = dtPlan("UNIT_ID").ToString
                    End If
                    '6.2
                    If dtPlan.Table.Columns.Contains("COST_TOTAL") Then
                        rntxtExpectedCost.Value = dtPlan("COST_TOTAL").ToString
                    End If
                    '6.3
                    If dtPlan.Table.Columns.Contains("CURRENCY_ID") Then
                        cboCurrency.SelectedValue = dtPlan("CURRENCY_ID").ToString
                    End If

                    '7
                    If dtPlan.Table.Columns.Contains("TARGET_TRAIN") Then
                        txtTargetTrain.Text = dtPlan("TARGET_TRAIN").ToString
                    End If

                    '8
                    If dtPlan.Table.Columns.Contains("VENUE") Then
                        txtVenue.Text = dtPlan("VENUE").ToString
                    End If

                    '9
                    If cbIrregularly.Checked Then
                     
                    End If


                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub lstCenter_ItemCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles lstCenter.ItemCheck
        Try
            LoadTeacher()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Sub GetPlanInYearOrg()
        Try
            cboPlan.ClearSelection()
            cboPlan.Items.Clear()
            ClearControl()
            If hidOrgID.Value <> "" And rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    dtCourse = rep.GetTrPlanByYearOrg2(True, rntxtYear.Value, Decimal.Parse(hidOrgID.Value), cbIrregularly.Checked)
                    If cbIrregularly.Checked Then
                        FillRadCombobox(cboPlan, dtCourse, "NAME", "ID")
                    Else
                        FillRadCombobox(cboPlan, dtCourse, "NAME", "PLAN_ID")
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub LoadTeacher()
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim listCEN As String = ""
        Try
            lstTeacher.Items.Clear()

            For Each item As RadListBoxItem In lstCenter.CheckedItems
                listCEN += item.Value + ","
            Next

            If listCEN = "" Then Exit Sub

            dtData = tsp.GetLecture(listCEN)

            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        lstTeacher.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ClearControl()
        '1
        txtProgramGroup.Text = ""
        '2
        txtTrainField.Text = ""
        '3
        cboTrainForm.ClearSelection()
        '4
        cboPropertiesNeed.ClearSelection()
        '5
        '6
        For Each item As RadListBoxItem In lstCenter.Items
            item.Checked = False
        Next
        lstTeacher.Items.Clear()
        '7
        txtTargetTrain.Text = ""
        '8
        txtVenue.Text = ""

        '9
        lstEmployee = New List(Of RequestEmpDTO)
    End Sub

    Protected Sub SetControlState(ByVal state As Boolean)
        btnFindOrg.Enabled = state
        rntxtYear.ReadOnly = Not state
        cbIrregularly.Enabled = state
        cboPlan.Enabled = state
        txtProgramGroup.Enabled = state
        txtTrainField.Enabled = state
        cboTrainForm.Enabled = state
        cboPropertiesNeed.Enabled = state
        rdExpectedDate.Enabled = state
        rdStartDate.Enabled = state
        lstCenter.Enabled = state
        lstTeacher.Enabled = state
        cboUnits.Enabled = state
        rntxtExpectedCost.ReadOnly = Not state
        cboCurrency.Enabled = state
        txtTargetTrain.ReadOnly = Not state
        txtVenue.ReadOnly = Not state
        rdRequestDate.Enabled = state
        btnUploadFile.Enabled = state
        txtRemark.ReadOnly = Not state
    End Sub

    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class