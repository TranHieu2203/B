Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramInterviewResult
    Inherits Common.CommonView
    Protected WithEvents StageView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    Private userlog As UserLog

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Public Property tabSource As DataTable
        Get
            Return PageViewState(Me.ID & "_tabSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_tabSource") = value
        End Set
    End Property

    Property lstItemDecimals As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_lstDeleteDecimals")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_lstDeleteDecimals") = value
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

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()

            gridCadidate.SetFilter()
            gridCadidate.AllowCustomPaging = True
            gridCadidate.PageSize = Common.Common.DefaultPageSize

            rgDataInterview.SetFilter()
            rgDataInterview.AllowCustomPaging = True
            rgDataInterview.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            gridCadidate.SetFilter()
            gridCadidate.AllowCustomPaging = True
            gridCadidate.PageSize = Common.Common.DefaultPageSize

            rgDataInterview.SetFilter()
            rgDataInterview.AllowCustomPaging = True
            rgDataInterview.PageSize = Common.Common.DefaultPageSize

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGrid(gridCadidate, True)
                    Utilities.EnabledGridNotPostback(rgDataInterview, True)
                    txtAssessment.Enabled = False
                    txtComment.Enabled = False
                    cbbStatus.Enabled = False
                    ResetText()
                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgDataInterview, False)
                    txtAssessment.Enabled = True
                    txtComment.Enabled = True
                    cbbStatus.Enabled = True
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            lblExamName_Interview.Text = String.Empty
            cbbStatus.SelectedIndex = 0
            txtAssessment.Text = String.Empty
            txtComment.Text = String.Empty
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hdProgramID.Value = Request.Params("PROGRAM_ID")
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDataInterview.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgDataInterview.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("EXAM_NAME", lblExamName_Interview)
        dic.Add("IS_PASS", cbbStatus)
        dic.Add("COMMENT_INFO", txtComment)
        dic.Add("ASSESSMENT_INFO", txtAssessment)
        Utilities.OnClientRowSelectedChanged(rgDataInterview, dic)
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As PROGRAM_SCHEDULE_CAN_DTO
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDataInterview.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDataInterview.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj = New PROGRAM_SCHEDULE_CAN_DTO
                        obj.COMMENT_INFO = txtComment.Text
                        obj.ASSESSMENT_INFO = txtAssessment.Text
                        obj.IS_PASS = cbbStatus.SelectedValue


                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgDataInterview.SelectedValue
                                IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                            obj.ID,
                                                            0,
                                                            obj.COMMENT_INFO,
                                                            obj.ASSESSMENT_INFO,
                                                            obj.IS_PASS)
                        End Select

                        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)
                        'If obj.ISPASS = 1 And dataItem("STATUS").Text = "Đủ điều kiện" Then
                        '    store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "DAT")
                        'End If

                        'Ràng buộc khi thay đổi status chỗ này ?
                        If obj.IS_PASS = 0 Then
                            store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "KDAT")
                        ElseIf obj.IS_PASS = 1 Then
                            store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "DAT")
                        End If

                        If IsSaveCompleted Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            rgDataInterview.Rebind()
                            ResetText()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles gridCadidate.NeedDataSource
        Try
            GetCadidateList()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDataInterview.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub gridCadidate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridCadidate.SelectedIndexChanged
        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            CreateDataFilter()
            rgDataInterview.Rebind()
            ResetText()
        End If
    End Sub

    Private Sub cmdSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendEmail.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim body As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        If gridCadidate.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)
        If dataItem Is Nothing Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            Mail = store.Get_Email_Candidate(ID)
            If Mail = "" Then
                ShowMessage(Translate("Ứng viên được chọn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
        Next
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            Mail = store.Get_Email_Candidate(ID)
            dataMail = store.GET_MAIL_TEMPLATE("TCO", "Recruitment")
            body = dataMail.Rows(0)("CONTENT").ToString
            titleMail = "THƯ CẢM ƠN"
            ' mailCC = If(dataMail.Rows(0)("MAIL_CC").ToString <> "", dataMail.Rows(0)("MAIL_CC").ToString, Nothing)
            'mailCC = If(LogHelper.CurrentUser.EMAIL IsNot Nothing, LogHelper.CurrentUser.EMAIL.ToString, Nothing)
            dtValues = store.GET_INFO_CADIDATE(item.GetDataKeyValue("ID"))
            mailCC = store.GET_EMAIL_COMPANY(LogHelper.CurrentUser.EMPLOYEE_ID)
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
            If Not Common.Common.sendEmailByServerMail(Mail,
                                                     If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()),
                                                      titleMail, bodyNew, String.Empty) Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            Else
                ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                ' Update Candidate Status
                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), RCContant.TUCHOI)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
        Next
        ' format email
        'Dim receiver As String = dataItem("Email").Text
        'Dim receiver As String = "tanvn@tinhvan.com"
        'Dim subject As String = "Thư cám ơn"
        'Dim body As String = String.Empty
        ''format body by html template
        'Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/ThuCamOn.htm"))
        'body = reader.ReadToEnd
        'body = body.Replace("{ngày}", DateTime.Now.Day)
        'body = body.Replace("{tháng}", DateTime.Now.Month)
        'body = body.Replace("{năm}", DateTime.Now.Year)
        'body = body.Replace("{họ tên}", dataItem("FullName").Text.ToUpper())

        'If Common.Common.sendEmailByServerMail(receiver, String.Empty, subject, body, String.Empty, "") Then
        '    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)

        '    'Update Candidate Status
        '    store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), RCContant.TUCHOI)

        '    CurrentState = CommonMessage.STATE_NORMAL
        '    UpdateControlState()
        'Else
        '    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
        '    Exit Sub
        'End If
    End Sub

#End Region

#Region "Custom"

    Protected Function GetCadidateList()
        Try
            If hdProgramID.Value IsNot Nothing Then
                tabSource = store.CANDIDATE_LIST_GETBYPROGRAM(hdProgramID.Value)
                If tabSource IsNot Nothing Then
                    gridCadidate.VirtualItemCount = tabSource.Rows.Count
                    gridCadidate.DataSource = tabSource
                Else
                    gridCadidate.DataSource = New List(Of COSTALLOCATE_DTO)
                End If
            Else
                gridCadidate.DataSource = New List(Of COSTALLOCATE_DTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

    Protected Function CreateDataFilter()
        Try
            If gridCadidate.Items.Count > 0 Then
                If hdProgramID.Value IsNot Nothing Then

                    If Not IsPostBack Then
                        ' set default first row selected
                        gridCadidate.MasterTableView.Items(0).Selected = True
                    End If

                    Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)

                    If dataItem IsNot Nothing Then
                        tabSource = store.EXAMS_GETBYCANDIDATE(hdProgramID.Value, Int32.Parse(dataItem("ID").Text), -1)
                        If tabSource IsNot Nothing And tabSource.Rows.Count > 0 Then
                            rgDataInterview.VirtualItemCount = tabSource.Rows.Count
                            rgDataInterview.DataSource = tabSource
                        Else
                            rgDataInterview.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
                        End If
                    End If
                Else
                    rgDataInterview.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

    Protected Sub gridCadidate_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridCadidate.ItemDataBound
        If TypeOf e.Item Is GridHeaderItem Then
            Dim chkbx As CheckBox = DirectCast(TryCast(e.Item, GridHeaderItem)("cbStatus").Controls(0), CheckBox)
            'to disable the chekbox 
            chkbx.Enabled = False
            'or to hide the checkbox 
            chkbx.Visible = False
        End If
    End Sub
End Class