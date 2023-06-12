Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

Public Class ctrlRC_ProgramExamsResult
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

            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            gridCadidate.SetFilter()
            gridCadidate.AllowCustomPaging = True
            gridCadidate.PageSize = Common.Common.DefaultPageSize

            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize

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
                    Utilities.EnabledGridNotPostback(rgData, True)
                    txtAssessment.Enabled = False
                    txtComment.Enabled = False
                    txtMarks.Enabled = False
                    ResetText()
                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgData, False)
                    txtMarks.Focus()
                    txtAssessment.Enabled = True
                    txtComment.Enabled = True
                    txtMarks.Enabled = True
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            lblExamName.Text = String.Empty
            lblPointLadder.Text = String.Empty
            lblPointPass.Text = String.Empty
            txtMarks.Value = "0"
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
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("EXAM_NAME", lblExamName)
        dic.Add("POINT_LADDER", lblPointLadder)
        dic.Add("POINT_PASS", lblPointPass)
        dic.Add("POINT_RESULT", txtMarks)
        dic.Add("COMMENT_INFO", txtComment)
        dic.Add("ASSESSMENT_INFO", txtAssessment)
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As PROGRAM_SCHEDULE_CAN_DTO
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        obj = New PROGRAM_SCHEDULE_CAN_DTO
                        obj.POINT_RESULT = txtMarks.Value
                        obj.COMMENT_INFO = txtComment.Text
                        obj.ASSESSMENT_INFO = txtAssessment.Text
                        obj.IS_PASS = If(obj.POINT_RESULT >= Int32.Parse(lblPointPass.Text) = True, 1, 0)

                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgData.SelectedValue
                                IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                            obj.ID,
                                                            obj.POINT_RESULT,
                                                            obj.COMMENT_INFO,
                                                            obj.ASSESSMENT_INFO,
                                                            obj.IS_PASS)
                        End Select
                        If IsSaveCompleted Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            rgData.Rebind()
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
        'Try
        '    Dim IsCompleted As Boolean
        '    If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

        '        For Each item As Int32 In lstItemDecimals
        '            IsCompleted = store.DELETE_CANDIDATE_RESULT(item)
        '        Next

        '        If IsCompleted Then
        '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
        '            rgData.Rebind()
        '        Else
        '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
        '        End If

        '        CurrentState = CommonMessage.STATE_NORMAL
        '        rgData.Rebind()
        '        UpdateControlState()
        '    End If
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles gridCadidate.NeedDataSource
        Try
            GetCadidateList()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
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
            rgData.Rebind()
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
            'Else
            '    If dataItem("Email").Text = String.Empty OrElse Not dataItem("Email").Text.Contains("@") Then
            '        ShowMessage(Translate(CommonMessage.MESSAGE_EMAIL_ISEMPTY), NotifyType.Warning)
            '        Exit Sub
            '    End If
        End If
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            If mail = "" Then
                ShowMessage(Translate("Ứng viên được chọn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
        Next
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            dataMail = store.GET_MAIL_TEMPLATE("TCO", "Recruitment")
            body = dataMail.Rows(0)("CONTENT").ToString
            titleMail = "THƯ CẢM ƠN"
            'mailCC = If(dataMail.Rows(0)("MAIL_CC").ToString <> "", dataMail.Rows(0)("MAIL_CC").ToString, Nothing)
            'mailCC = If(LogHelper.CurrentUser. IsNot Nothing, LogHelper.CurrentUser.EMAIL.ToString, Nothing)
            mailCC = store.GET_EMAIL_COMPANY(LogHelper.CurrentUser.EMPLOYEE_ID)
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
                ' Update Candidate Status
                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), RCContant.TUCHOI)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
        Next

        ' format email
        'Dim receiver As String = dataItem("Email").Text
        'Dim receiver As String = "tanvn@tinhvan.com"
        'Dim cc As String = String.Empty
        'Dim subject As String = "Thư cám ơn"
        'Dim body As String = String.Empty
        'Dim fileAttachments As String = String.Empty
        ''format body by html template
        'Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/ThuCamOn.htm"))
        'body = reader.ReadToEnd
        'body = body.Replace("{ngày}", DateTime.Now.Day)
        'body = body.Replace("{tháng}", DateTime.Now.Month)
        'body = body.Replace("{năm}", DateTime.Now.Year)
        'body = body.Replace("{họ tên}", dataItem("FullName").Text.ToUpper())


        'If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, "") Then
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
                        tabSource = store.EXAMS_GETBYCANDIDATE(hdProgramID.Value, Int32.Parse(dataItem("ID").Text), 0)
                        If tabSource IsNot Nothing And tabSource.Rows.Count > 0 Then
                            rgData.VirtualItemCount = tabSource.Rows.Count
                            rgData.DataSource = tabSource

                            Dim avg As Decimal
                            avg = store.CANDIDATE_GET_AVERAGE_MARKS(hdProgramID.Value, Int32.Parse(dataItem("ID").Text))
                            If avg > 0 Then
                                lblAVG.Text = String.Format("Điểm trung bình: {0}", avg.ToString("#.#"))
                            Else
                                lblAVG.Text = "Điểm trung bình: 0"
                            End If

                        Else
                            rgData.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
                            lblAVG.Text = String.Format("Điểm trung bình: {0}", 0)
                        End If
                    End If
                Else
                    rgData.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
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