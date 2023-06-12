Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramSchedule
    Inherits Common.CommonView
    Private store As New RecruitmentStoreProcedure
    Private psp As New CommonRepository
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
#Region "Properties"
    Dim str As Integer
    Private Const FIELD_ORG As String = "{ORG}"
    Private Const FIELD_TITLE As String = "{TITLE}"
    Private Const FIELD_DAY As String = "{DAY}"
    Private Const FIELD_TIME As String = "{TIME}"
    Private Const FIELD_LOCATION As String = "{LOCATION}"
    Private Const FIELD_NOTE As String = "{NOTE}"
    Private Const FIELD_STT As String = "{STT}"
    Private Const FIELD_MNV As String = "{MNV}"
    Private Const FIELD_FULLNAME As String = "{FULLNAME}"
    Private Property ID_SCHEDULE As String
        Get
            Return ViewState(Me.ID & "_ID_SCHEDULE")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ID_SCHEDULE") = value
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
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            'If IsPostBack Then
            CreateDataFilter()
            rgData.Rebind()
            'End If
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            rgData.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Previous, ToolbarItem.Next)
            MainToolBar.Items(3).Text = Translate("Khai báo UV")
            MainToolBar.Items(4).Text = Translate("Kết quả PV")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg.Value = objPro.ORG_ID
                hidTitle.Value = objPro.TITLE_ID
                lblTitleName.Text = objPro.TITLE_NAME
                If objPro.SEND_DATE IsNot Nothing Then
                    lblSendDate.Text = objPro.SEND_DATE
                Else
                    lblSendDate.Text = ""
                End If
                lblCode.Text = objPro.CODE_YCTD
                lblJobName.Text = objPro.JOB_NAME
                lblRequestNumber.Text = objPro.REQUEST_NUMBER
                lblQuantityHasRecruitment.Text = objPro.CANDIDATE_RECEIVED
                lblStatusRequest.Text = objPro.STATUS_NAME
                lblReasonRecruitment.Text = objPro.RECRUIT_REASON
                lblOtherRequest.Text = objPro.REQUESTOTHER
                lblExperienceRequired.Text = objPro.REQUEST_EXPERIENCE
                If objPro.EXPECTED_JOIN_DATE IsNot Nothing Then
                    lblRespone.Text = objPro.EXPECTED_JOIN_DATE
                Else
                    lblRespone.Text = ""
                End If
                lblprofileReceive.Text = objPro.CANDIDATE_COUNT
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn ngày thi cần xóa!"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    For Each Item As GridDataItem In rgData.SelectedItems
                        ID_SCHEDULE = ID_SCHEDULE & Item.GetDataKeyValue("ID") & ","
                        Dim CANDIDATE_COUNT As Decimal = If(Item.GetDataKeyValue("CANDIDATE_COUNT").ToString IsNot Nothing, CDec(Item.GetDataKeyValue("CANDIDATE_COUNT").ToString), 0)
                        If CANDIDATE_COUNT > 0 Then
                            ShowMessage(Translate("UV trong lịch phỏng vấn đã có kết quả, không thể xóa!"), Utilities.NotifyType.Error)
                            ID_SCHEDULE = Nothing
                            Exit Sub
                        End If
                    Next
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_PREVIOUS
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_CandidateList&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

                Case CommonMessage.TOOLBARITEM_NEXT
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramUResult&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

            End Select
            UpdateControlState()
            CreateDataFilter()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim IsCompleted As Boolean
                If ID_SCHEDULE IsNot Nothing AndAlso ID_SCHEDULE <> "" Then
                    IsCompleted = store.DELETE_RC_PROGRAM_SCHEDULE(ID_SCHEDULE)
                    If IsCompleted Then
                        ShowMessage(Translate("Xóa thành công!"), Utilities.NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate("Xóa không thành công!"), Utilities.NotifyType.Error)
                    End If
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgData.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnDSPV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDSPV.Click
        Try
            For Each Item As GridDataItem In rgData.SelectedItems
                ID_SCHEDULE = ID_SCHEDULE & Item.GetDataKeyValue("ID") & ","
            Next
            Using xls As New ExcelCommon
                tabCandidateSchedule = store.GET_PROGRAM_SCHCEDULE_LIST_ALL(ID_SCHEDULE, Decimal.Parse(hidProgramID.Value))
                If tabCandidateSchedule.Rows.Count > 0 Then
                    xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/Common/DanhSachPhongVan.xls"), "DanhSachPhongVan", tabCandidateSchedule, Response)
                Else
                    ShowMessage(Translate("Không có dữ liệu để xuất danh sách"), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnEmailNPV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmailNPV.Click
        Dim dataMail As DataTable
        Dim dataMailTable As DataTable
        Dim dtValues As DataTable
        Dim dtValuesDetail As DataTable
        Dim dtNPVmail As DataTable

        Dim body_table As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = psp.GetConfig(0)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
            If defaultFrom = "" Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            End If
            If rgData.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dataItem = TryCast(rgData.SelectedItems(0), GridDataItem)
            If dataItem Is Nothing Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            ElseIf rgData.SelectedItems.Count >= 2 Then
                ShowMessage(Translate("Chỉ gửi email cho người phỏng vấn theo từng vòng tuyển dụng"), NotifyType.Warning)
                Exit Sub
            End If
            ' Lấy thông tin của người PV
            Dim itemSelect As GridDataItem = rgData.SelectedItems(0)
            dtValues = store.SCHEDULE_SENDMAIL_NPV(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))
            dtValuesDetail = store.GET_SCHCEDULE_CAN_BY_SCHCEDULEID(Decimal.Parse(hidProgramID.Value), Decimal.Parse(itemSelect.GetDataKeyValue("ID")))

            dtNPVmail = store.GET_PRO_SCHEDULE_MAIL(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))

            If dtNPVmail.Rows.Count = 0 Then
                ShowMessage(Translate("Người coi thi/phỏng vấn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            For index = 0 To dtNPVmail.Rows.Count - 1

                mail = dtNPVmail.Rows(index)("WORK_EMAIL")

                dataMail = store.GET_MAIL_TEMPLATE("INTERVIEW_NPV", "Recruitment")
                dataMailTable = store.GET_MAIL_TEMPLATE("INTERVIEW_NPV_TABLE", "Recruitment")



                titleMail = dataMail.Rows(0)("TITLE").ToString


                If dtValues.Rows.Count > 0 Then
                    Dim body As String = dataMail.Rows(0)("CONTENT").ToString

                    Dim Org As String = dtValues.Rows(0)("ORG").ToString()
                    Dim title As String = dtValues.Rows(0)("TITLE").ToString()
                    Dim day As String = dtValues.Rows(0)("DAY").ToString()
                    Dim time As String = dtValues.Rows(0)("TIME").ToString()
                    Dim location As String = dtValues.Rows(0)("LOCATION").ToString()
                    Dim note As String = dtValues.Rows(0)("NOTE").ToString()

                    body = Replace(body, FIELD_ORG, Org)
                    body = Replace(body, FIELD_TITLE, title)
                    body = Replace(body, FIELD_DAY, day)
                    body = Replace(body, FIELD_TIME, time)
                    body = Replace(body, FIELD_LOCATION, location)
                    body = Replace(body, FIELD_NOTE, note)

                    Dim bodytableAll As String = ""



                    For item = 0 To dtValuesDetail.Rows.Count - 1

                        Dim bodytable As String = dataMailTable.Rows(0)("CONTENT").ToString

                        Dim Stt As String = dtValuesDetail.Rows(item)("STT").ToString()
                        Dim empcode As String = dtValuesDetail.Rows(item)("CANDIDATE_CODE").ToString()
                        Dim fullname As String = dtValuesDetail.Rows(item)("FULLNAME_VN").ToString()

                        bodytable = Replace(bodytable, FIELD_STT, Stt)
                        bodytable = Replace(bodytable, FIELD_MNV, empcode)
                        bodytable = Replace(bodytable, FIELD_FULLNAME, fullname)

                        bodytableAll = bodytableAll + bodytable
                        bodytable = ""
                    Next
                    body = body + bodytableAll

                    If Not psp.InsertMail(defaultFrom, mail, titleMail, body, If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()), Nothing, "INTERVIEW_NPV") Then
                        ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                        Exit Sub
                    Else
                        ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If
                Else
                    ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnEmail1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmail1.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim dtNVmail As DataTable

        Dim body_table As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = psp.GetConfig(0)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
            If defaultFrom = "" Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            End If

            If rgData.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dataItem = TryCast(rgData.SelectedItems(0), GridDataItem)
            If dataItem Is Nothing Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            ElseIf rgData.SelectedItems.Count >= 2 Then
                ShowMessage(Translate("Chỉ gửi email cho người phỏng vấn theo từng vòng tuyển dụng"), NotifyType.Warning)
                Exit Sub
            End If
            Dim itemSelect As GridDataItem = rgData.SelectedItems(0)
            dtValues = store.SCHEDULE_SENDMAIL_NPV(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))
            dtNVmail = store.GET_PRO_SCHEDULE_MAIL_NV(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))
            If dtNVmail.Rows.Count = 0 Then
                ShowMessage(Translate("Người coi thi/phỏng vấn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            For index = 0 To dtNVmail.Rows.Count - 1

                mail = dtNVmail.Rows(index)("WORK_EMAIL")

                dataMail = store.GET_MAIL_TEMPLATE("INTERVIEW1", "Recruitment")

                titleMail = dataMail.Rows(0)("TITLE").ToString

                If dtValues.Rows.Count > 0 Then
                    Dim body As String = dataMail.Rows(0)("CONTENT").ToString

                    Dim Org As String = dtValues.Rows(0)("ORG").ToString()
                    Dim title As String = dtValues.Rows(0)("TITLE").ToString()
                    Dim day As String = dtValues.Rows(0)("DAY").ToString()
                    Dim time As String = dtValues.Rows(0)("TIME").ToString()
                    Dim location As String = dtValues.Rows(0)("LOCATION").ToString()
                    Dim note As String = dtValues.Rows(0)("NOTE").ToString()

                    body = Replace(body, FIELD_ORG, Org)
                    body = Replace(body, FIELD_TITLE, title)
                    body = Replace(body, FIELD_DAY, day)
                    body = Replace(body, FIELD_TIME, time)
                    body = Replace(body, FIELD_LOCATION, location)
                    body = Replace(body, FIELD_NOTE, note)

                    If Not psp.InsertMail(defaultFrom, mail, titleMail, body, If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()), Nothing, "INTERVIEW1") Then
                        ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                        Exit Sub
                    Else
                        ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If
                Else
                    ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub btnEmail2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmail2.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim dtNVmail As DataTable

        Dim body_table As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        Try
            Dim defaultFrom As String = ""
            Dim config As Dictionary(Of String, String)
            config = psp.GetConfig(0)
            defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
            If defaultFrom = "" Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            End If
            If rgData.SelectedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dataItem = TryCast(rgData.SelectedItems(0), GridDataItem)
            If dataItem Is Nothing Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            ElseIf rgData.SelectedItems.Count >= 2 Then
                ShowMessage(Translate("Chỉ gửi email cho người phỏng vấn theo từng vòng tuyển dụng"), NotifyType.Warning)
                Exit Sub
            End If
            Dim itemSelect As GridDataItem = rgData.SelectedItems(0)
            dtValues = store.SCHEDULE_SENDMAIL_NPV(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))
            dtNVmail = store.GET_PRO_SCHEDULE_MAIL_NV(Decimal.Parse(itemSelect.GetDataKeyValue("ID")))
            If dtNVmail.Rows.Count = 0 Then
                ShowMessage(Translate("Người coi thi/phỏng vấn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            For index = 0 To dtNVmail.Rows.Count - 1

                mail = dtNVmail.Rows(index)("WORK_EMAIL")

                dataMail = store.GET_MAIL_TEMPLATE("INTERVIEW2", "Recruitment")

                titleMail = dataMail.Rows(0)("TITLE").ToString


                If dtValues.Rows.Count > 0 Then
                    Dim body As String = dataMail.Rows(0)("CONTENT").ToString

                    Dim Org As String = dtValues.Rows(0)("ORG").ToString()
                    Dim title As String = dtValues.Rows(0)("TITLE").ToString()
                    Dim day As String = dtValues.Rows(0)("DAY").ToString()
                    Dim time As String = dtValues.Rows(0)("TIME").ToString()
                    Dim location As String = dtValues.Rows(0)("LOCATION").ToString()
                    Dim note As String = dtValues.Rows(0)("NOTE").ToString()

                    body = Replace(body, FIELD_ORG, Org)
                    body = Replace(body, FIELD_TITLE, title)
                    body = Replace(body, FIELD_DAY, day)
                    body = Replace(body, FIELD_TIME, time)
                    body = Replace(body, FIELD_LOCATION, location)
                    body = Replace(body, FIELD_NOTE, note)

                    If Not psp.InsertMail(defaultFrom, mail, titleMail, body, If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()), Nothing, "INTERVIEW2") Then
                        ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                        Exit Sub
                    Else
                        ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    End If
                Else
                    ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub CreateDataFilter()
        Try
            rgData.DataSource = Nothing
            rgData.DataSource = store.GET_PROGRAM_SCHCEDULE_LIST(Decimal.Parse(hidProgramID.Value))

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgData.CurrentPageIndex = 0
                rgData.Rebind()
                If rgData.Items IsNot Nothing AndAlso rgData.Items.Count > 0 Then
                    rgData.Items(0).Selected = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

End Class