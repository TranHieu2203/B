Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramUResult
    Inherits Common.CommonView
    Private store As New RecruitmentStoreProcedure()
    Private rep As New HistaffFrameworkRepository
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private userlog As UserLog

#Region "Property"

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
        End Set
    End Property

    Public Property lstCanNotSchedule As List(Of ProgramScheduleCanDTO)
        Get
            Return ViewState(Me.ID & "_lstCanNotSchedule")
        End Get
        Set(ByVal value As List(Of ProgramScheduleCanDTO))
            ViewState(Me.ID & "_lstCanNotSchedule") = value
        End Set
    End Property

    Public Property lstCanSchedule As List(Of ProgramScheduleCanDTO)
        Get
            Return ViewState(Me.ID & "_lstCanSchedule")
        End Get
        Set(ByVal value As List(Of ProgramScheduleCanDTO))
            ViewState(Me.ID & "_lstCanSchedule") = value
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

    Property tabCandidateSchedule As DataTable
        Get
            Return ViewState(Me.ID & "_tabCandidateSchedule")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_tabCandidateSchedule") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'rgCanNotSchedule.PageSize = 50
            'rgCanNotSchedule.AllowCustomPaging = True
            rgCanSchedule.PageSize = 50
            rgCanSchedule.AllowCustomPaging = True
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            hidID.Value = Request.Params("SCHEDULE_ID")
            If hidID.Value = "" Then
                hidID.Value = 0
            End If
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Using rep As New RecruitmentRepository
                dtData = rep.GetProgramExamsList(hidProgramID.Value, True)
                'FillRadCombobox(cboExams, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Previous, ToolbarItem.Next)
            MainToolBar.Items(1).Text = Translate("Lập lịch PV")
            MainToolBar.Items(2).Text = Translate("Chuyển HSNV")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

            If Not IsPostBack Then
                LoadExamsList()
                ' rdScheduleDate.SelectedDate = DateTime.Now
                store.DELETE_PRO_SCHEDULE_CAN_ISNULL()
                Dim FULLNAME As String = ""
                Dim employee_ID As Decimal?

                Using rep As New RecruitmentRepository
                    Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                    lblJobName.Text = objPro.ORG_NAME
                    'lblTitleName.Text = objPro.TITLE_NAME
                    'lblRequestNo.Text = objPro.REQUEST_NUMBER
                    lblSendDate.Text = objPro.SEND_DATE.Value.ToString("dd/MM/yyyy")
                    lblCode.Text = objPro.CODE
                    lblJobName.Text = objPro.JOB_NAME
                    lblOrgName.Text = objPro.ORG_NAME
                    lblTitleName.Text = objPro.TITLE_NAME
                    'hidTitle.Value = objPro.TITLE_ID
                    lblSendDate.Text = objPro.SEND_DATE
                    lblCode.Text = objPro.CODE_YCTD
                    lblJobName.Text = objPro.JOB_NAME
                    lblRequestNumber.Text = objPro.REQUEST_NUMBER
                    lblQuantityHasRecruitment.Text = objPro.CANDIDATE_RECEIVED
                    lblStatusRequest.Text = objPro.STATUS_NAME
                    lblReasonRecruitment.Text = objPro.RECRUIT_REASON
                    lblOtherRequest.Text = objPro.REQUESTOTHER
                    lblExperienceRequired.Text = objPro.REQUEST_EXPERIENCE
                    lblRespone.Text = objPro.EXPECTED_JOIN_DATE
                    lblprofileReceive.Text = objPro.CANDIDATE_COUNT
                    If hidID.Value <> "" And Decimal.Parse(hidID.Value) > 0 Then


                        Dim tab As DataTable
                        tab = store.GET_PRO_SCHEDULE_BYID(Decimal.Parse(hidID.Value))
                        If tab.Rows.Count > 0 Then
                            'rdScheduleDate.SelectedDate = tab.Rows(0)("SCHEDULE_DATE").ToString()
                            'txtExamsPlace.Text = tab.Rows(0)("EXAMS_PLACE").ToString()
                            'txtNote.Text = tab.Rows(0)("NOTE").ToString()
                            FULLNAME = tab.Rows(0)("FULLNAME").ToString()
                            employee_ID = If(tab.Rows(0)("EMPLOYEE_ID").ToString() = "", Nothing, Decimal.Parse(tab.Rows(0)("EMPLOYEE_ID").ToString()))
                            If employee_ID <> 0 Then
                                'cboUsher.CheckBoxes = True
                                'cboUsher.SelectedValue = employee_ID
                                'cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
                                Dim item1 As New RadComboBoxItem()
                                item1.Text = FULLNAME
                                item1.Value = employee_ID
                                'cboUsher.Items.Add(item1)
                                'cboUsher.ClearCheckedItems()
                                'For Each chk As RadComboBoxItem In cboUsher.Items
                                'If cboUsher.SelectedValue.Contains(chk.Value) Then
                                'chk.Checked = True
                                ' End If
                                'Next
                            End If
                        End If
                        'Dim obj = rep.GetProgramScheduleByID(New ProgramScheduleDTO With {.ID = Decimal.Parse(hidID.Value)})
                        'rdScheduleDate.SelectedDate = obj.SCHEDULE_DATE
                        'txtExamsPlace.Text = obj.EXAMS_PLACE
                        'txtNote.Text = obj.NOTE

                        'cboUsher.CheckBoxes = True
                        'cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
                        'For Each itm In obj.lstScheduleUsher
                        '    Dim item As New RadComboBoxItem
                        '    item.Value = itm.EMPLOYEE_ID
                        '    item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        '    item.Checked = True
                        '    cboUsher.Items.Add(item)
                        'Next

                        'rdScheduleDate.Enabled = True
                        'txtExamsPlace.Enabled = True
                        'txtNote.Enabled = True
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        If phFindUsher.Controls.Contains(ctrlFindEmployeePopup) Then
            phFindUsher.Controls.Remove(ctrlFindEmployeePopup)
        End If
        Select Case isLoadPopup
            Case 1
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                ctrlFindEmployeePopup.MustHaveContract = False
                phFindUsher.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
        End Select

        'Using rep As New RecruitmentRepository
        '    'Dim lstCanNotID As New List(Of Decimal)
        '    'For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.SelectedItems
        '    '    lstCanNotID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
        '    'Next
        '    'Dim lstCanID As New List(Of Decimal)
        '    'For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.SelectedItems
        '    '    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
        '    'Next
        '    Select Case CurrentState
        '        Case CommonMessage.STATE_NEW
        '            CurrentState = CommonMessage.STATE_NORMAL
        '            For Each studentID In SelectedItemCanNotSchedule
        '                Dim objNot = (From p In lstCanNotSchedule Where p.CANDIDATE_ID = studentID).FirstOrDefault
        '                If objNot IsNot Nothing Then
        '                    objNot.CANDIDATE_ID = studentID
        '                    'Dim startHour As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day,
        '                    '                          rtStartHour.SelectedDate.Value.Hour,
        '                    '                          rtStartHour.SelectedDate.Value.Minute,
        '                    '                          rtStartHour.SelectedDate.Value.Second)
        '                    'Dim endHour As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day,
        '                    '                          rtEndHour.SelectedDate.Value.Hour,
        '                    '                          rtEndHour.SelectedDate.Value.Minute,
        '                    '                          rtEndHour.SelectedDate.Value.Second)
        '                    'objNot.START_HOUR = startHour
        '                    'objNot.END_HOUR = endHour
        '                    objNot.STATUS_ID = "DATLICH"
        '                    'If objNot IsNot Nothing Then
        '                    '    lstCanSchedule.Add(objNot)
        '                    '    lstCanNotSchedule.Remove(objNot)
        '                    '    rep.UpdateStatusCandidate(lstCanNotID, objNot.STATUS_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DATLICH)
        '                    'End If
        '                End If
        '            Next
        '            rgCanNotSchedule.Rebind()
        '            rgCanSchedule.Rebind()

        '            'Case CommonMessage.STATE_DELETE
        '            '    CurrentState = CommonMessage.STATE_NORMAL
        '            '    For Each studentID In SelectedItemCanSchedule
        '            '        Dim obj As ProgramScheduleCanDTO = (From p In lstCanSchedule Where p.CANDIDATE_ID = studentID).FirstOrDefault
        '            '        obj.CANDIDATE_ID = studentID
        '            '        obj.STATUS_ID = "DUDK"
        '            '        If obj IsNot Nothing Then
        '            '            lstCanNotSchedule.Add(obj)
        '            '            lstCanSchedule.Remove(obj)
        '            '            rep.UpdateStatusCandidate(lstCanNotID, obj.STATUS_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID)
        '            '        End If
        '            '    Next
        '            '    rgCanNotSchedule.Rebind()
        '            '    rgCanSchedule.Rebind()

        '    End Select

        'End Using
    End Sub

    Private Sub LoadExamsList()
        'If Not IsPostBack Then
        rlbExams.DataSource = store.GET_AllExams_ByProgram(Decimal.Parse(hidProgramID.Value), hidID.Value)
        rlbExams.DataTextField = "NAME_SORT"
        rlbExams.DataValueField = "ID"
        rlbExams.DataBind()

        If hidID.Value <> 0 Then
            rlbExams.Enabled = False
            rlbExams.SelectedIndex = "0"
        Else
            rlbExams.Enabled = True
        End If

        'Dim collection As IList(Of RadListBoxItem) = rlbExams.Items
        'For Each item As RadListBoxItem In collection
        '    item.Checked = True
        'Next
        'rlbExams.Enabled = False

        'End If
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New RecruitmentRepository
        Try

            Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim status As String
                    Dim obj As PROGRAM_SCHEDULE_CAN_DTO
                    Dim IsSaveCompleted As Boolean
                    Dim SCHEDULE As Int32
                    If rgCanSchedule.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Chọn record muốn lưu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each Item As GridDataItem In rgCanSchedule.SelectedItems

                        obj = New PROGRAM_SCHEDULE_CAN_DTO
                        obj.ID = Int32.Parse(Item("PSC_ID").Text)
                        Dim EXAMS_ORDER = Int32.Parse(Item("EXAMS_ORDER").Text)
                        Dim PRO_CAN_ID = Int32.Parse(Item("PRO_CAN_ID").Text)

                        obj.IS_PASS = cbbStatus.SelectedValue
                        If obj.IS_PASS = 0 Then
                            status = "ROTVONG" + EXAMS_ORDER.ToString
                            SCHEDULE = EXAMS_ORDER - 1
                            'store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "KDAT")
                        ElseIf obj.IS_PASS = 1 Then

                            status = "DAUVONG" + EXAMS_ORDER.ToString
                            SCHEDULE = EXAMS_ORDER
                            'store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "DAT")
                        Else
                            status = ""
                            SCHEDULE = EXAMS_ORDER - 1
                        End If
                        Dim IS_PV = Int32.Parse(Item("IS_PV").Text)
                        If IS_PV = 0 AndAlso obj.IS_PASS = 1 Then
                            obj.POINT_RESULT = Int32.Parse(Item("POINT_PASS").Text)
                        Else
                            obj.POINT_RESULT = 0
                        End If
                        IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                            obj.ID,
                                                            obj.POINT_RESULT,
                                                            obj.COMMENT_INFO,
                                                            obj.ASSESSMENT_INFO,
                                                            obj.IS_PASS)
                        If IsSaveCompleted Then
                            store.UPDATE_PROGRAM_CANDIDATE_STATUS_BY_EXAMS_ORDER(PRO_CAN_ID, status, EXAMS_ORDER, hidProgramID.Value, SCHEDULE)

                            ShowMessage(Translate("Lưu thành công"), NotifyType.Success)
                            'rgCanSchedule.Rebind()
                        End If
                    Next
                    rgCanSchedule.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Dim PROGRAM_ID As String = Request.QueryString("PROGRAM_ID")
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=" & PROGRAM_ID)
                    ' Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business")
                Case CommonMessage.TOOLBARITEM_PREVIOUS
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

                Case CommonMessage.TOOLBARITEM_NEXT
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_CandidateTransferList&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

            End Select
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    'Private Sub rgCanNotSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanNotSchedule.PageIndexChanged
    '    'GetCanNotScheduleSelected()
    'End Sub

    'Private Sub rgCanNotSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanNotSchedule.PageSizeChanged
    '    'GetCanNotScheduleSelected()
    'End Sub

    'Private Sub rgCanNotSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanNotSchedule.SortCommand
    '    'GetCanNotScheduleSelected()
    'End Sub

    'Private Sub rgCanNotSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanNotSchedule.NeedDataSource
    '    getCanNotSchedule()
    'End Sub






    Private Sub rgCanSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanSchedule.PageIndexChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanSchedule.PageSizeChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanSchedule.SortCommand
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanSchedule.NeedDataSource
        getCanSchedule()
    End Sub
    Protected Sub rgCanSchedule_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim reps As New RecruitmentRepository
        Try
            For Each dataItem As GridDataItem In rgCanSchedule.Items
                Dim STATUS_CODE As String = dataItem("STATUS_CODE").Text.ToUpper
                Dim CAN_ID As Decimal = Decimal.Parse(dataItem("ID").Text)
                Dim EXAMS_ORDER As Decimal = Decimal.Parse(dataItem("EXAMS_ORDER").Text)
                If STATUS_CODE = "NHANVIEN" Or reps.CheckExist_Program_Schedule_Can(CAN_ID, If(IsNumeric(hidProgramID.Value), hidProgramID.Value, 0), EXAMS_ORDER) Then
                    dataItem.SelectableMode = GridItemSelectableMode.None
                Else
                    dataItem.SelectableMode = GridItemSelectableMode.ServerAndClientSide
                End If
            Next
        Catch ex As Exception


        End Try
    End Sub
    'Protected Sub rgCanSchedule_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs)
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If TypeOf e.Item Is GridHeaderItem Then
    '            Dim headerItem As GridHeaderItem = CType(e.Item, GridHeaderItem)
    '            Dim headerChkBox As CheckBox = CType(headerItem("cbStatus").Controls(0), CheckBox)
    '            headerChkBox.AutoPostBack = True
    '            AddHandler headerChkBox.CheckedChanged, AddressOf headerChkBox_CheckedChanged
    '        End If
    '    Catch ex As Exception

    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub headerChkBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) 
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try

    '    Catch ex As Exception

    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    'Protected Sub btnFindUsher_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindUsher.Click
    '    Try
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            'If lstCommonEmployee.ToList.Count > 1 Then
            '    ShowMessage(Translate("Chọn duy nhất một nhân viên"), NotifyType.Warning)
            '    Exit Sub
            'End If
            'cboUsher.Items.Clear()
            'cboUsher.CheckBoxes = True
            'cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
            For Each itm In lstCommonEmployee
                Dim item As New RadComboBoxItem
                item.Value = itm.EMPLOYEE_ID
                '  item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                item.Text = itm.FULLNAME_VN
                item.Checked = True
                'cboUsher.Items.Add(item)
            Next
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub




    'Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    'GetCanScheduleSelected()
    '    CurrentState = CommonMessage.STATE_DELETE
    '    UpdateControlState()

    'End Sub

    'Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
    '    If Page.IsValid Then
    '        'GetCanNotScheduleSelected()
    '        CurrentState = CommonMessage.STATE_NEW
    '        UpdateControlState()
    '    End If
    'End Sub

    Private Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim body As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        Dim dataItem = TryCast(rgCanSchedule.SelectedItems(0), GridDataItem)
        If dataItem Is Nothing Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        For index = 0 To rgCanSchedule.SelectedItems.Count - 1
            Dim item As GridDataItem = rgCanSchedule.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            If mail = "" Then
                ShowMessage(Translate("Ứng viên được chọn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
        Next
        For index = 0 To rgCanSchedule.SelectedItems.Count - 1
            Dim item As GridDataItem = rgCanSchedule.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            dataMail = store.GET_MAIL_TEMPLATE("TMPV", "Recruitment")
            body = dataMail.Rows(0)("CONTENT").ToString
            titleMail = "THƯ MỜI PHỎNG VẤN"
            'mailCC = If(dataMail.Rows(0)("MAIL_CC").ToString <> "", dataMail.Rows(0)("MAIL_CC").ToString, Nothing)
            'mailCC = If(LogHelper.CurrentUser.EMAIL IsNot Nothing, LogHelper.CurrentUser.EMAIL.ToString, Nothing)
            mail = store.Get_Email_Employee(If(LogHelper.CurrentUser.EMPLOYEE_ID IsNot Nothing, LogHelper.CurrentUser.EMPLOYEE_ID.ToString, Nothing))
            dtValues = store.GET_INFO_CADIDATE(item.GetDataKeyValue("ID"))
            Dim values(dtValues.Columns.Count) As String
            If dtValues.Rows.Count > 0 Then
                For i As Integer = 0 To dtValues.Columns.Count - 1
                    values(i) = If(dtValues.Rows(0)(i).ToString() <> "", dtValues.Rows(0)(i), String.Empty)
                Next
            Else
                ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            bodyNew = String.Format(body, values)
            If Not Common.Common.sendEmailByServerMail(mail,
                                                     If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()),
                                                      titleMail, bodyNew, String.Empty) Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            Else
                ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
        Next
    End Sub

    Private Sub btnInterviewList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInterviewList.Click
        Dim template_URL As String = String.Format("~/ReportTemplates/Recruitment/Report/Danh sach phong van.xlxs")
        Dim fileName As String = String.Format("Danh sách phỏng vấn - {0}", lblJobName.Text)
        Dim _error As String = ""
        'Dim dt = lstCanSchedule.ToTable
        'Dim dt As New DataTable()
        'dt = DirectCast(rgCanSchedule.DataSource, DataTable)
        'dt.TableName = "Data"
        'Dim ds As New DataSet
        'ds.Tables.Add(dt)

        'tabCandidateSchedule = store.GET_PROGRAM_SCHCEDULE_LIST(Decimal.Parse(hidProgramID.Value), Decimal.Parse(hidID.Value))

        Using xls As New ExcelCommon
            'If dt.Rows.Count > 0 Then
            rgCanSchedule.ExportExcel(Server, Response, tabCandidateSchedule, "data")
            'End If
        End Using
    End Sub

    Private Sub btnRecLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecLetter.Click
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'Dim item As GridDataItem = rgCanSchedule.SelectedItems(0)
        'Dim dtData As DataTable = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        Dim folderName As String = ""
        Dim filePath As String = ""
        Dim extension As String = ""
        Dim iError As Integer = 0
        ExportFileWord(tabCandidateSchedule, folderName, filePath, extension, iError, rgCanSchedule.SelectedItems(0))
    End Sub

    Private Sub btnRecInterviewLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecInterviewLetter.Click
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'Dim dtData As DataTable
        Dim folderName As String = ""
        Dim filePath As String = ""
        Dim extension As String = ""
        Dim iError As Integer = 0
        'Dim item As GridDataItem = rgCanSchedule.SelectedItems(0)
        'dtData = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        ExportFileWord(tabCandidateSchedule, folderName, filePath, extension, iError, rgCanSchedule.SelectedItems(0))
    End Sub

#End Region

#Region "Custom"

    'Protected Function getCanNotSchedule(Optional ByVal isFull As Boolean = False) As DataTable
    '    Try
    '        If IsNumeric(rlbExams.SelectedValue) Then
    '            Dim idPro_Exams As Int32 = Decimal.Parse(rlbExams.SelectedValue)
    '            rgCanNotSchedule.DataSource = store.GET_CANDIDATE_NOT_SCHEDULE_1(Decimal.Parse(hidProgramID.Value), hidID.Value, idPro_Exams)
    '        End If

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Function

    Protected Function getCanSchedule(Optional ByVal isFull As Boolean = False) As DataTable
        Try

            tabCandidateSchedule = store.CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS(Decimal.Parse(hidProgramID.Value), If(IsNumeric(rlbExams.SelectedValue), Decimal.Parse(rlbExams.SelectedValue), 0))
            rgCanSchedule.DataSource = tabCandidateSchedule

            If tabCandidateSchedule.Rows.Count = 0 Then
                rlbExams.Enabled = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function
    Private Sub rgCanSchedule_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgCanSchedule.ItemCommand
        Try
            Select Case e.CommandName
                Case "DETAIL"
                    Dim item = CType(e.Item, GridDataItem)
                    Dim str As String
                    Dim EXAMS_ORDER As Decimal = If(IsNumeric(item.GetDataKeyValue("EXAMS_ORDER")), item.GetDataKeyValue("EXAMS_ORDER"), 0)
                    Dim PRO_CAN_ID As Decimal = item.GetDataKeyValue("PRO_CAN_ID")
                    If item.GetDataKeyValue("IS_PV") = 0 Then
                        str = "window.open('Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramExamsResult_Dialog&group=Business&PSC_ID=" & item.GetDataKeyValue("PSC_ID") & "&PRO_CAN_ID=" & PRO_CAN_ID & "&EXAMS_ORDER=" & EXAMS_ORDER & "&PROGRAM_ID=" & hidProgramID.Value & "');"
                    Else
                        str = "window.open('Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramInterviewResult_Dialog&group=Business&PSC_ID=" & item.GetDataKeyValue("PSC_ID") & "&PRO_CAN_ID=" & PRO_CAN_ID & "&EXAMS_ORDER=" & EXAMS_ORDER & "&PROGRAM_ID=" & hidProgramID.Value & "');"
                    End If
                    ' Page.Response.Redirect(String.Format("Dialog.aspx?mid=Recruitment&fid=ctrlRC_ProgramExamsResult&group=Business&PROGRAM_ID={0}&state=Normal&noscroll=1&isdone=1", item.GetDataKeyValue("ID")))
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ExportFileWord(ByVal dtData As DataTable, ByVal folderName As String, ByVal filePath As String,
                               ByVal extension As String, ByVal iError As Integer, ByVal item As GridDataItem)
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        If rgCanSchedule.SelectedItems.Count > 1 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'folderName = ""
        'filePath = ""
        'extension = ""
        'iError = 0
        'item = rgCanSchedule.SelectedItems(0)
        ' Kiểm tra + lấy thông tin trong database
        'Using rep As New ProfileRepository
        '    dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
        '                                   ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID,
        '                                   folderName)
        '    If dtData.Rows.Count = 0 Then
        '        ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
        '        Exit Sub
        '    End If
        '    If folderName = "" Then
        '        ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
        '        Exit Sub
        '    End If
        'End Using

        'dtData = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        ' Kiểm tra file theo thông tin trong database
        If Not Utilities.GetTemplateLinkFile(item.GetDataKeyValue("ID"),
                                         folderName,
                                         filePath,
                                         extension,
                                         iError) Then
            Select Case iError
                Case 1
                    ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                    Exit Sub
            End Select
        End If
        ' Export file mẫu
        Using word As New WordCommon
            word.ExportMailMerge(filePath,
                                 "AAAA",
                                 dtData,
                                 Response)
        End Using
    End Sub

#End Region

    'Protected Sub rlbExams_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles rlbExams.ItemDataBound
    '    For Each item As RadListBoxItem In rlbExams.Items
    '        'item.Checked = If(store.Check_Exams_IsExist(Decimal.Parse(item.Value)) > 0, True, False)
    '        item.Checked = True

    '    Next
    'End Sub
    Private Sub rlbExams_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rlbExams.SelectedIndexChanged
        'getCanNotSchedule()
        'rgCanNotSchedule.Rebind()
        getCanSchedule()
        rgCanSchedule.Rebind()
    End Sub
    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)
        userlog = LogHelper.GetUserLog
        Dim check As Integer = 0
        Dim str As String
        Dim arr() As String
        arr = e.Argument.Split("_")
        str = arr(arr.Count - 1)

        'If hidID.Value = 0 Then
        '    store.ADDNEW_PRO_SCHEDULE(hidProgramID.Value, If(cboUsher.SelectedValue = "", Nothing, cboUsher.SelectedValue), rdScheduleDate.SelectedDate, txtExamsPlace.Text, txtNote.Text, userlog.Username, String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))
        '    'store.ADDNEW_PRO_SCHEDULE(hidProgramID.Value, rdScheduleDate.SelectedDate, txtExamsPlace.Text, txtNote.Text, String.Empty, String.Empty)
        '    hidID.Value = store.GET_TOPID_PRO_SCHEDULE(hidProgramID.Value)
        'End If

        Select Case str
            'Case "btnInsert"
            '    For idx = 0 To rgCanNotSchedule.SelectedItems.Count - 1
            '        Dim item As GridDataItem = rgCanNotSchedule.SelectedItems(idx)
            '        'insert candidate --> program schedule candidate
            '        Dim idCandidate As Int32 = Int32.Parse(item.GetDataKeyValue("ID").ToString())
            '        Dim idPro_Candidate As Int32 = Int32.Parse(item.GetDataKeyValue("PRO_CAN").ToString())
            '        'For Each itemExams As RadListBoxItem In rlbExams.Items
            '        '    If itemExams.Checked = True Then
            '        '        Dim idPro_Exams As Int32 = Decimal.Parse(itemExams.Value)
            '        '        store.ADDNEW_CAN_PRO_SCHEDULE(idCandidate, Decimal.Parse(hidID.Value), idPro_Exams)
            '        '        check = 2
            '        '    Else
            '        '        Continue For
            '        '    End If
            '        'Next
            '        If IsNumeric(rlbExams.SelectedValue) Then
            '            Dim idPro_Exams As Int32 = Decimal.Parse(rlbExams.SelectedValue)
            '            store.ADDNEW_CAN_PRO_SCHEDULE(idCandidate, Decimal.Parse(hidID.Value), idPro_Exams)
            '            check = 2
            '        End If
            '        If check = 2 Then
            '            store.UPDATE_CANDIDATE_STATUS(idCandidate, "PROCESS")
            '            store.UPDATE_PROGRAM_CANDIDATE_STATUS(idPro_Candidate, "PROCESS")
            '        End If
            '    Next

            'Case "btnDelete"
            '    For idx = 0 To rgCanSchedule.SelectedItems.Count - 1
            '        Dim item As GridDataItem = rgCanSchedule.SelectedItems(idx)
            '        Dim idCandidate As Int32 = Int32.Parse(item.GetDataKeyValue("ID").ToString())
            '        If store.Check_Exams_IsExist(Decimal.Parse(hidID.Value), idCandidate) > 0 Then
            '            ShowMessage("Thông tin kết quả điểm thi/pv của ứng viên đã tồn tại. Vui lòng kiểm tra lại!", NotifyType.Warning)
            '            Exit Sub
            '        End If

            '        'delete candidate --> program schedule candidate
            '        store.DELETE_PRO_SCHEDULE_CAN(Decimal.Parse(hidID.Value), idCandidate)
            '    Next
        End Select
        'rgCanNotSchedule.Rebind()
        rgCanSchedule.Rebind()

        If rgCanSchedule.Items.Count = 0 Then
            LoadExamsList()
            rlbExams.Enabled = True
        Else
            rlbExams.Enabled = False
        End If

    End Sub

End Class