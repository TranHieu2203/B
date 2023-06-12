Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_CandidateTransferList
    Inherits Common.CommonView
    Private Property psp As New RecruitmentRepository
    Dim _mylog As New MyLog()
    Dim _classPath As String = "Recruitment\Modules\Recruitment\Business" + Me.GetType().Name.ToString()
    Protected WithEvents ctrlFindProgramDialog As New ctrlFindProgramPopupDialog

#Region "Properties"
    Private Property CandidateList As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_CandidateList")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_CandidateList") = value
        End Set
    End Property
    Private Property sender As String
        Get
            Return ViewState(Me.ID & "_sender")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_sender") = value
        End Set
    End Property
    Dim str As Integer
    Public Property _filter As CandidateDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New CandidateDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    Private Property lstData As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_lstData") = value
        End Set
    End Property

    Property ListComboData As Recruitment.RecruitmentBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Recruitment.RecruitmentBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Private Property strStatus As String
        Get
            Return ViewState(Me.ID & "_strStatus")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strStatus") = value
        End Set
    End Property

    Private Property strIDCandidate As String
        Get
            Return ViewState(Me.ID & "_strIDCandidate")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strIDCandidate") = value
        End Set
    End Property
    Private Property strID_Pro_Candidate As String
        Get
            Return ViewState(Me.ID & "_strID_Pro_Candidate")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strID_Pro_Candidate") = value
        End Set
    End Property

    Property dtData_Import As DataTable
        Get
            If ViewState(Me.ID & "_dtData_Import") Is Nothing Then
                Dim dt As New DataTable("DATA")
                'dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("CODE_RC", GetType(String))
                dt.Columns.Add("CANDIDATE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_NAME", GetType(String))
                dt.Columns.Add("CONTRACT_FROMDATE", GetType(String))
                dt.Columns.Add("CONTRACT_TODATE", GetType(String))
                dt.Columns.Add("SAL_TYPE_NAME", GetType(String))
                dt.Columns.Add("TAX_TABLE_NAME", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_NAME", GetType(String))
                dt.Columns.Add("SALARY_PROBATION1", GetType(String))
                dt.Columns.Add("OTHERSALARY1_1", GetType(String))
                dt.Columns.Add("PERCENT_SAL1", GetType(String))
                dt.Columns.Add("SALARY_OFFICIAL1", GetType(String))
                dt.Columns.Add("DK_LUONGCB2", GetType(String))
                dt.Columns.Add("EFFECT_DATE2", GetType(String))
                dt.Columns.Add("SALARY_PROBATION2", GetType(String))
                dt.Columns.Add("OTHERSALARY1_2", GetType(String))
                dt.Columns.Add("PERCENT_SAL2", GetType(String))
                dt.Columns.Add("SALARY_OFFICIAL2", GetType(String))
                dt.Columns.Add("PC1", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME1", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT1", GetType(String))
                dt.Columns.Add("MONEY1", GetType(String))
                dt.Columns.Add("EFFECT_FROM1", GetType(String))
                dt.Columns.Add("EFFECT_TO1", GetType(String))
                dt.Columns.Add("PC2", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME2", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT2", GetType(String))
                dt.Columns.Add("MONEY2", GetType(String))
                dt.Columns.Add("EFFECT_FROM2", GetType(String))
                dt.Columns.Add("EFFECT_TO2", GetType(String))

                dt.Columns.Add("PC3", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME3", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT3", GetType(String))
                dt.Columns.Add("MONEY3", GetType(String))
                dt.Columns.Add("EFFECT_FROM3", GetType(String))
                dt.Columns.Add("EFFECT_TO3", GetType(String))
                dt.Columns.Add("PC4", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME4", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT4", GetType(String))
                dt.Columns.Add("MONEY4", GetType(String))
                dt.Columns.Add("EFFECT_FROM4", GetType(String))
                dt.Columns.Add("EFFECT_TO4", GetType(String))
                dt.Columns.Add("PC5", GetType(String))
                dt.Columns.Add("ALLOWANCE_NAME5", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT5", GetType(String))
                dt.Columns.Add("MONEY5", GetType(String))
                dt.Columns.Add("EFFECT_FROM5", GetType(String))
                dt.Columns.Add("EFFECT_TO5", GetType(String))

                'ID
                dt.Columns.Add("RC_PROGRAM_ID", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_ID", GetType(String))
                dt.Columns.Add("SAL_TYPE_ID", GetType(String))
                dt.Columns.Add("TAX_TABLE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_TYPE_ID", GetType(String))
                dt.Columns.Add("PC_ID1", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID1", GetType(String))
                dt.Columns.Add("PC_ID2", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID2", GetType(String))
                dt.Columns.Add("PC_ID3", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID3", GetType(String))
                dt.Columns.Add("PC_ID4", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID4", GetType(String))
                dt.Columns.Add("PC_ID5", GetType(String))
                dt.Columns.Add("ALLOWANCE_UNIT_ID5", GetType(String))
                dt.Columns.Add("RC_CANDIDATE_ID", GetType(String))

                ViewState(Me.ID & "_dtData_Import") = dt
            End If
            Return ViewState(Me.ID & "_dtData_Import")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData_Import") = value
        End Set
    End Property
#End Region
    Public WithEvents AjaxManager As RadAjaxManager
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.SetFilter()

            rgResult.AllowCustomPaging = True
            rgResult.PageSize = Common.Common.DefaultPageSize
            rgResult.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            'tbarMainToolBar.Visible = False
            'rgCandidateList.ClientSettings.EnablePostBackOnRowClick = True
            'rgAspiration.ClientSettings.EnablePostBackOnRowClick = True
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Delete)
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Previous, ToolbarItem.Save)
            CType(MainToolBar.Items(1), RadToolBarButton).Text = "Lưu thông tin thỏa thuận"
            CType(MainToolBar.Items(1), RadToolBarButton).CssClass = "Hidden"
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Kết quả PV"
            'CType(MainToolBar.Items(1), RadToolBarButton).Text = "Xóa Đàm phán UV"
            'CType(MainToolBar.Items(1), RadToolBarButton).Visible = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'Load danh sach trang thai ung vien len rad listbox
            LoadDataRadlistBox()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                For Each item As RadListBoxItem In rlbStatus.Items
                    item.Checked = True
                    strStatus = strStatus & item.Value & ","
                Next

                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg.Value = objPro.ORG_ID
                hidTitle.Value = objPro.TITLE_ID
                lblTitleName.Text = objPro.TITLE_NAME
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
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            strStatus = ""
            For Each item As RadListBoxItem In rlbStatus.Items
                If item.Checked Then
                    strStatus = strStatus & item.Value & ","
                End If
            Next
            rgCandidateList.Rebind()
            rgAspiration.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CountNumberHaveRecruit(ByVal id As Integer)
        lblQuantityHasRecruitment.Text = psp.COUNT_NUMBER_RC(id)
    End Sub
    Private Sub LoadDataRadlistBox()
        If Not IsPostBack Then
            rlbStatus.DataSource = psp.GET_STATUS_CANDIDATE()
            rlbStatus.DataTextField = "NAME"
            rlbStatus.DataValueField = "CODE"
            rlbStatus.DataBind()

        End If
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgAspiration.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Chọn record muốn lưu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each Item As GridDataItem In rgAspiration.SelectedItems
                        Dim ID_CANDIDATE = Item("ID_CANDIDATE").Text
                        Dim PLACE_WORK = DirectCast(Item.FindControl("PLACE_WORK"), RadTextBox).Text
                        Dim RECEIVE_FROM As Date? = DirectCast(Item.FindControl("RECEIVE_FROM"), RadDatePicker).SelectedDate
                        Dim RECEIVE_TO As Date? = DirectCast(Item.FindControl("RECEIVE_TO"), RadDatePicker).SelectedDate
                        Dim PROBATION_FROM As Date? = DirectCast(Item.FindControl("PROBATION_FROM"), RadDatePicker).SelectedDate
                        Dim PROBATION_TO As Date? = DirectCast(Item.FindControl("PROBATION_TO"), RadDatePicker).SelectedDate
                        If psp.UPDATE_ASPIRATION(ID_CANDIDATE, PLACE_WORK, RECEIVE_FROM, RECEIVE_TO, PROBATION_FROM, PROBATION_TO) = 1 Then
                            rgAspiration.Rebind()
                            ShowMessage(Translate("Lưu thành công"), NotifyType.Success)
                        End If
                    Next
                Case CommonMessage.TOOLBARITEM_DELETE
                    'Dim StrErr As String = ""
                    'Dim statusDel As Decimal
                    'For Each items As GridDataItem In rgCandidateList.SelectedItems
                    '    Dim STATUS_CODE As String = String.Empty
                    '    STATUS_CODE = items.GetDataKeyValue("STATUS_CODE").ToString
                    '    If STATUS_CODE = "TRUNGTUYEN" Then
                    '        Dim lst = New RCNegotiateDTO
                    '        lst.RC_CANDIDATE_ID = items.GetDataKeyValue("ID_CANDIDATE")
                    '        lst.RC_PROGRAM_ID = Request.Params("PROGRAM_ID")
                    '        Dim funcDel As Decimal = rep.DeleteRCNegotiate(lst)
                    '        If funcDel = 0 Then
                    '            statusDel = 0
                    '            StrErr += items.GetDataKeyValue("CANDIDATE_CODE") + ","
                    '        ElseIf funcDel <> 1 Then
                    '            statusDel = 2
                    '            StrErr += items.GetDataKeyValue("CANDIDATE_CODE") + ","
                    '        End If
                    '    End If
                    'Next
                    'If statusDel = 2 Then
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate("Nhân viên " + StrErr + " xóa không thành công."), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If StrErr.Split(",").ToString <> "" Then
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate("Nhân viên " + StrErr + " không có thông tin đàm phán."), NotifyType.Warning)
                    '    rgCandidateList.Rebind()
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    rgCandidateList.Rebind()
                    'End If
                    btnDeleteNegotiate_Click(Nothing, Nothing)
                Case CommonMessage.TOOLBARITEM_PREVIOUS
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramUResult&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub btnThankLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnThankLetter.Click
    '    Dim status As String
    '    For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
    '        status = dr.GetDataKeyValue("STATUS_CODE").ToString
    '        Select Case status
    '            Case RCContant.TRUNGTUYEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.BLACKLIST
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.PONTENTIAL
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.THUMOI
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.NHANVIEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.TIEPNHANLD
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
    '                Exit Sub
    '        End Select
    '    Next
    '    Dim dataItem = TryCast(rgCandidateList.SelectedItems(0), GridDataItem)
    '    If dataItem Is Nothing Then
    '        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
    '        Exit Sub
    '    End If
    '    ' format email
    '    'Dim receiver As String = dataItem("Email").Text
    '    Dim receiver As String = "tanvn@tinhvan.com"
    '    Dim cc As String = String.Empty
    '    Dim subject As String = "Thư cám ơn"
    '    Dim body As String = String.Empty
    '    Dim fileAttachments As String = String.Empty
    '    'format body by html template
    '    Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/TiepNhanThuViec.htm"))
    '    body = reader.ReadToEnd
    '    'body = body.Replace("{ngày}", DateTime.Now.Day)
    '    'body = body.Replace("{tháng}", DateTime.Now.Month)
    '    'body = body.Replace("{năm}", DateTime.Now.Year)
    '    body = body.Replace("{họ tên}", dataItem("FULLNAME_VN").Text.ToUpper())

    '    If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, "") Then
    '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
    '        'Update Candidate Status
    '        psp.UPDATE_CANDIDATE_STATUS(dataItem("ID").Text, RCContant.TUCHOI)
    '        CurrentState = CommonMessage.STATE_NORMAL
    '        UpdateControlState()
    '    Else
    '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub cmdYCTDKhac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYCTDKhac.Click
        Dim strEmp As String = ""
        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage("Vui lòng chọn 1 ứng viên", NotifyType.Warning)
            Exit Sub
        End If

        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TUCHOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở từ chối trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
            End Select
            If strEmp = "" Then
                strEmp = dr.GetDataKeyValue("FULLNAME_VN")
            Else
                strEmp = strEmp + "," + dr.GetDataKeyValue("FULLNAME_VN")
            End If
        Next
        ctrlMessageBoxTransferProgram.MessageText = Translate("Bạn có muốn chuyển ứng viên: " + strEmp + " sang vị trí khác không")
        ctrlMessageBoxTransferProgram.ActionName = "CHUYENUNGVIEN"
        ctrlMessageBoxTransferProgram.DataBind()
        ctrlMessageBoxTransferProgram.Show()
    End Sub


    Private Sub btnBlacklist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlacklist.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Blacklist cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.BLACKLIST
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub
    Private Sub btnRejectOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRejectOffer.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TRUNGTUYEN
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                    'Case RCContant.TIEPNHANLD
                    '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    '    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái từ chối offer cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.TUCHOIOFFER
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub
    Private Sub btnPontential_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPontential.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TRUNGTUYEN
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.PONTENTIAL
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái ứng viên tiềm năng"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                    'Case RCContant.TIEPNHANLD
                    '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    '    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn lưu danh sách tiềm năng?")
        ctrlMessageBox.ActionName = RCContant.PONTENTIAL
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim rep As New RecruitmentRepository
        'Dim status As String
        'For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
        '    status = dr.GetDataKeyValue("STATUS_CODE").ToString
        '    Select Case status
        '        Case RCContant.THUMOI
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.NHANVIEN
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.KHONGDAT
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Không thi đạt"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.TUCHOI
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Từ chối trúng tuyển"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.BLACKLIST
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.PONTENTIAL
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Ứng viên tìêm năng"), NotifyType.Warning)
        '            Exit Sub
        '        Case RCContant.TUCHOIOFFER
        '            ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái từ chối offer"), NotifyType.Warning)
        '            Exit Sub
        '    End Select
        'Next
        ''Hiển thị Confirm delete.

        'ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển sang HSNV?")
        'ctrlMessageBox.ActionName = RCContant.NHANVIEN
        'ctrlMessageBox.DataBind()
        'ctrlMessageBox.Show()
        Try
            Dim IS_EMP As Integer = 0
            If rgCandidateList.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn ứng viên nào"), NotifyType.Warning)
                Exit Sub
            ElseIf rgCandidateList.SelectedItems.Count > 1 Then
                ShowMessage(Translate("Chỉ được chọn 1 ứng viên"), NotifyType.Warning)
                Exit Sub
            Else
                Dim status As String
                Dim items As GridDataItem = DirectCast(rgCandidateList.SelectedItems(0), GridDataItem)
                status = items.GetDataKeyValue("STATUS_CODE").ToString
                If status <> RCContant.TRUNGTUYEN Then
                    'ShowMessage(Translate("Vui lòng chọn ứng viên có trạng thái là Trúng tuyển"), NotifyType.Warning)
                    'Exit Sub
                    IS_EMP = 1
                End If
                Dim _filter = New RC_TransferCAN_ToEmployeeDTO
                _filter.CANDIDATE_ID = items.GetDataKeyValue("ID_CANDIDATE")
                _filter.RC_PROGRAM_ID = hidProgramID.Value
                Dim objtran = rep.GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(_filter)
                rwPopup.VisibleOnPageLoad = True
                If objtran IsNot Nothing Then
                    rwPopup.NavigateUrl = "/Dialog.aspx?mid=Recruitment&fid=ctrlRC_HU&group=Business&ProgramID=" & hidProgramID.Value & "&CandidateID=" & items.GetDataKeyValue("ID_CANDIDATE") & "&ID=" & objtran.ID & "&IS_EMP=" & IS_EMP
                Else
                    rwPopup.NavigateUrl = "/Dialog.aspx?mid=Recruitment&fid=ctrlRC_HU&group=Business&ProgramID=" & hidProgramID.Value & "&CandidateID=" & items.GetDataKeyValue("ID_CANDIDATE") & "&IS_EMP=" & IS_EMP
                End If
                rwPopup.InitialBehaviors = WindowBehaviors.Maximize
                'rwPopup.Width = 500


            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnTrungTuyen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrungTuyen.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TRUNGTUYEN
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                    'Case RCContant.TIEPNHANLD
                    '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    '    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.TRUNGTUYEN
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnKhongTrungTuyen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKhongTrungTuyen.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TRUNGTUYEN
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Không đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.TUCHOI
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnExportContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportContract.Click
        Dim strID As String
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        For Each dr As GridDataItem In rgCandidateList.SelectedItems
            strID = strID & dr.GetDataKeyValue("ID_CANDIDATE") & ","
        Next
        Dim dtData = psp.CONTRACT_RECIEVE(strID)
        If dtData.Rows.Count > 0 Then
            ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Recruitment/CONTRACT_RECIEVE.doc"),
                 "CONTRACT_RECIEVE_" & dtData.Rows(0)("NAME") & ".doc",
                 dtData,
                 Response)
        End If
    End Sub

    Private Sub btnReceive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReceive.Click
        Dim status As String
        Dim strID As String
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim filePath As String = ""
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                'Case RCContant.TUCHOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở từ chối trúng tuyển"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.BLACKLIST
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                '    Exit Sub
                'Case RCContant.THUMOI
                '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                '    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                    'Case RCContant.TIEPNHANLD
                    '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    '    Exit Sub
            End Select
        Next

        For Each dr As GridDataItem In rgCandidateList.SelectedItems
            strID = strID & dr.GetDataKeyValue("ID_CANDIDATE") & ","
        Next
        Dim dtData = psp.LETTER_RECIEVE(strID)
        If dtData.Rows.Count > 0 Then
            Using word As New WordCommon
                Dim sourcePath = Server.MapPath("~/TemplateDynamic/LetterRecieveSupport/")
                word.ExportMailMerge(System.IO.Path.Combine(Server.MapPath("~/TemplateDynamic/LetterRecieveSupport/"),
                                    "TMNV.doc"),
                                    "LETTER_RECIEVE_" & dtData.Rows(0)("FULLNAME_VN") & ".doc" & "_" &
                                    Format(Date.Now, "yyyyMMddHHmmss"),
                                    dtData,
                                   sourcePath,
                                    Response)
                'word.ExportMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath),
                '                                      "Recruitment/LETTER_RECIEVE.doc"),
                '                                     "LETTER_RECIEVE_" & dtData.Rows(0)("NAME") & "HOTEN.doc",
                '                                      dtData,
                '                                      Response)
            End Using

        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
            Exit Sub
        End If
    End Sub

    Private Sub btnLĐ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLĐ.Click
        Dim status As String
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TUCHOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.PONTENTIAL
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Dim status As String
        For Each dr As GridDataItem In rgCandidateList.SelectedItems
            'status = dr.GetDataKeyValue("STATUS_CODE").ToString
            'Select Case status
            '    Case RCContant.NHANVIEN
            '        ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
            '        Exit Sub
            'End Select
            Dim ID = dr.GetDataKeyValue("ID_CANDIDATE")
            Dim dataItem = psp.EMAIL_RECIEVE(ID)
            If dataItem.Rows.Count > 0 Then

                Dim receiver As String = "tanvn@tinhvan.com"
                Dim cc As String = String.Empty
                Dim subject As String = "Gửi email thông báo tiếp nhận LĐ thử việc"
                Dim body As String = String.Empty
                Dim fileAttachments As String = String.Empty
                'format body by html template
                Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/TiepNhanThuViec.htm"))
                body = reader.ReadToEnd
                body = body.Replace("{DAY}", dataItem(0)("DAY").ToString)
                body = body.Replace("{TITLE}", dataItem(0)("TITLE").ToString)
                body = body.Replace("{NAME}", dataItem(0)("NAME").ToString)
                body = body.Replace("{GENDER}", dataItem(0)("GENDER").ToString)
                body = body.Replace("{BIRTHDAY}", dataItem(0)("BIRTHDAY").ToString)
                body = body.Replace("{PLACE}", dataItem(0)("PLACE").ToString)
                body = body.Replace("{IDNO}", dataItem(0)("IDNO").ToString)
                body = body.Replace("{PHONE}", dataItem(0)("PHONE").ToString)
                body = body.Replace("{ADDRESS_CONTRACT}", dataItem(0)("ADDRESS_CONTRACT").ToString)
                body = body.Replace("{EDUCATION_TIME}", dataItem(0)("EDUCATION_TIME").ToString)
                body = body.Replace("{CERTIFICATE}", dataItem(0)("CERTIFICATE").ToString)
                body = body.Replace("{MAJORS}", dataItem(0)("MAJORS").ToString)
                body = body.Replace("{SCHOOL}", dataItem(0)("SCHOOL").ToString)
                body = body.Replace("{EXPERIENCE_TIME}", dataItem(0)("EXPERIENCE_TIME").ToString)
                body = body.Replace("{EXPERIENCE_TITILE}", dataItem(0)("EXPERIENCE_TITILE").ToString)
                body = body.Replace("{EXPERIENCE_COMPANY}", dataItem(0)("EXPERIENCE_COMPANY").ToString)
                body = body.Replace("{TITILE_PROBATION}", dataItem(0)("TITILE_PROBATION").ToString)
                body = body.Replace("{ORG_PROBATION}", dataItem(0)("ORG_PROBATION").ToString)
                body = body.Replace("{CONTRACT_TIME}", dataItem(0)("CONTRACT_TIME").ToString)
                body = body.Replace("{TASK}", dataItem(0)("TASK").ToString)
                If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, "") Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
                    'Update Candidate Status
                    psp.UPDATE_CANDIDATE_STATUS(ID, RCContant.NHANVIEN)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
                    Exit Sub
                End If
            End If

        Next

    End Sub


    Private Sub ctrlMessageBoxTransferProgram_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBoxTransferProgram.ButtonCommand
        Dim log As New UserLog
        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strID As String
                Dim strID_Program_Candidate As String
                For Each dr As GridDataItem In rgCandidateList.SelectedItems
                    strID &= IIf(strID = vbNullString, dr.GetDataKeyValue("ID_CANDIDATE"), "," & dr.GetDataKeyValue("ID_CANDIDATE"))
                    strID_Program_Candidate &= IIf(strID_Program_Candidate = vbNullString, dr.GetDataKeyValue("ID_PRO_CAN"), "," & dr.GetDataKeyValue("ID_PRO_CAN"))
                Next
                'Session("ID_CANDIDATE") = strID
                Dim dic As New Dictionary(Of String, Object)
                dic.Add("ID_CANDIDATE", strID)
                dic.Add("ID_PROGRAM_CANDIDATE", strID_Program_Candidate)
                dic.Add("P_PROGRAM_ID_OLD", hidProgramID.Value)
                If Not FindOrgTitle.Controls.Contains(ctrlFindProgramDialog) Then
                    'HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
                    ctrlFindProgramDialog = Me.Register("ctrlFindProgramPopupDialog", "Recruitment", "ctrlFindProgramPopupDialog", "Shared", dic)
                    ctrlFindProgramDialog.LoadAllOrganization = True
                    FindOrgTitle.Controls.Add(ctrlFindProgramDialog)
                    ctrlFindProgramDialog.Show()
                End If
            Else
                Return
            End If
            rgCandidateList.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim log As New UserLog
        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                strIDCandidate = ""
                strID_Pro_Candidate = ""
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
                    strIDCandidate = strIDCandidate & dr.GetDataKeyValue("ID_CANDIDATE") & ","
                    strID_Pro_Candidate = strID_Pro_Candidate & dr.GetDataKeyValue("ID_PRO_CAN") & ","
                Next
                ' Kiem tra neu la trang thai nhan vien thi insert du lieu moi vao cac table nhan vien
                If e.ActionName = RCContant.NHANVIEN Then
                    Dim REP As Int32 = psp.INSERT_CADIDATE_EMPLOYEE(strIDCandidate, log.Username, log.Ip + log.ComputerName)
                    If REP = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        Dim MESS As String = String.Empty
                        Select Case REP
                            Case -2
                                MESS = "Cập nhật dữ liệu hồ sơ nhân viên lỗi"
                            Case -3
                                MESS = "Cập nhật dữ liệu hồ sơ nhân viên lỗi (CV)"
                            Case -4
                                MESS = "Cập nhật dữ liệu trình độ học vấn lỗi"
                            Case -5
                                MESS = "Cập nhật dữ liệu quá trình công tác trước đây lỗi (CV)"
                            Case -6
                                MESS = "Cập nhật dữ liệu thông tin công ty trước đây lỗi"
                            Case -7
                                MESS = "Cập nhật trạng thái lỗi"
                            Case -8
                                MESS = "Cập nhật thông tin sức khỏe lỗi"
                            Case -9
                                MESS = "Cập nhật thông tin người thân lỗi"
                        End Select
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) + Chr(10) + MESS _
                            , Utilities.NotifyType.Error)
                    End If
                Else
                    If psp.UPDATE_CANDIDATE_STATUS(strIDCandidate, e.ActionName) = 1 Then
                        psp.UPDATE_PROGRAM_CANDIDATE_STATUS(strID_Pro_Candidate, e.ActionName)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                End If
                rgCandidateList.Rebind()
                rgResult.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            If hidProgramID.Value <> "" Then
                rgCandidateList.DataSource = psp.GET_LIST_EMPLOYEE_ELECT(Decimal.Parse(hidProgramID.Value), strStatus)
                rgCandidateList.VirtualItemCount = psp.GET_LIST_EMPLOYEE_ELECT(Decimal.Parse(hidProgramID.Value), strStatus).Rows.Count
                CountNumberHaveRecruit(hidProgramID.Value)
            End If
            rwPopup.VisibleOnPageLoad = False

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgAspiration_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAspiration.NeedDataSource
        Try
            If hidProgramID.Value <> "" Then
                rgAspiration.DataSource = psp.GET_LIST_EMPLOYEE_ASPIRATION(Decimal.Parse(hidProgramID.Value), strStatus)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgResult_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgResult.NeedDataSource
        Try
            If rgCandidateList.SelectedValues IsNot Nothing Then
                rgResult.DataSource = Nothing
                Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
                Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            Else
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(0, "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgCandidateList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCandidateList.SelectedIndexChanged
        Try
            'If rgCandidateList.SelectedValues IsNot Nothing Then
            '    Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
            '    Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
            '    rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            '    rgResult.DataBind()
            'End If
            rgResult.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ' Cập nhật nguyện vọng
    'Protected Sub rgAspiration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgAspiration.SelectedIndexChanged
    '    For Each Item As GridDataItem In rgAspiration.SelectedItems
    '        Dim ID_CANDIDATE = Item("ID_CANDIDATE").Text
    '        Dim PLACE_WORK = DirectCast(Item.FindControl("PLACE_WORK"), RadTextBox).Text
    '        Dim RECEIVE_FROM As Date? = DirectCast(Item.FindControl("RECEIVE_FROM"), RadDatePicker).SelectedDate
    '        Dim RECEIVE_TO As Date? = DirectCast(Item.FindControl("RECEIVE_TO"), RadDatePicker).SelectedDate
    '        Dim PROBATION_FROM As Date? = DirectCast(Item.FindControl("PROBATION_FROM"), RadDatePicker).SelectedDate
    '        Dim PROBATION_TO As Date? = DirectCast(Item.FindControl("PROBATION_TO"), RadDatePicker).SelectedDate
    '        If psp.UPDATE_ASPIRATION(ID_CANDIDATE, PLACE_WORK, RECEIVE_FROM, RECEIVE_TO, PROBATION_FROM, PROBATION_TO) = 1 Then
    '            rgAspiration.Rebind()
    '        End If
    '    Next

    'End Sub
    Private Sub btnExportFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportFile.Click

        'Dim dsData = New DataSet()

        'Using xls As New ExcelCommon
        '    Dim sourcePath = Server.MapPath("~/ReportTemplates/Recruitment/Import/Import_OfferLetter.xls")
        '    xls.ExportExcelTemplate(sourcePath,
        '                "Teamplate_Offerletter",
        '                dsData, Response)
        'End Using

        Template_Export()
    End Sub
    Private Sub btnInputFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInputFile.Click
        ctrlUpload1.Show()
    End Sub
    Private Sub Template_Export()
        Try
            Dim log As New UserLog
            log = LogHelper.GetUserLog
            Dim dt As New DataSet
            'psp.LETTER_RECIEVE(strID)
            dt = psp.GET_OFFERLETTER_IMPORT(hidProgramID.Value)

            ExportTemplate("Recruitment\Import\Import_OfferLetter.xls",
                            dt, Nothing, "Import_Offerletter" & Format(Date.Now, "yyyyMMdd"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData_Import = dtData_Import.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("CANDIDATE_CODE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("CANDIDATE_CODE")) OrElse rows("CANDIDATE_CODE") = "" Then Continue For
                Dim newRow As DataRow = dtData_Import.NewRow
                newRow("CODE_RC") = rows("CODE_RC")
                newRow("CANDIDATE_CODE") = rows("CANDIDATE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("CONTRACT_TYPE_NAME") = rows("CONTRACT_TYPE_NAME")
                newRow("CONTRACT_FROMDATE") = rows("CONTRACT_FROMDATE")
                newRow("CONTRACT_TODATE") = rows("CONTRACT_TODATE")
                newRow("SAL_TYPE_NAME") = rows("SAL_TYPE_NAME")
                newRow("TAX_TABLE_NAME") = rows("TAX_TABLE_NAME")
                newRow("EMPLOYEE_TYPE_NAME") = rows("EMPLOYEE_TYPE_NAME")


                newRow("SALARY_PROBATION1") = rows("SALARY_PROBATION1")
                newRow("OTHERSALARY1_1") = rows("OTHERSALARY1_1")
                newRow("PERCENT_SAL1") = rows("PERCENT_SAL1")
                newRow("SALARY_OFFICIAL1") = rows("SALARY_OFFICIAL1")

                newRow("DK_LUONGCB2") = rows("DK_LUONGCB2")
                newRow("EFFECT_DATE2") = rows("EFFECT_DATE2")
                newRow("SALARY_PROBATION2") = rows("SALARY_PROBATION2")

                newRow("OTHERSALARY1_2") = rows("OTHERSALARY1_2")
                newRow("PERCENT_SAL2") = rows("PERCENT_SAL2")
                newRow("SALARY_OFFICIAL2") = rows("SALARY_OFFICIAL2")
                newRow("PC1") = rows("PC1")
                newRow("ALLOWANCE_NAME1") = rows("ALLOWANCE_NAME1")
                newRow("ALLOWANCE_UNIT1") = rows("ALLOWANCE_UNIT1")
                newRow("MONEY1") = rows("MONEY1")
                newRow("EFFECT_FROM1") = rows("EFFECT_FROM1")
                newRow("EFFECT_TO1") = rows("EFFECT_TO1")
                newRow("PC2") = rows("PC2")
                newRow("ALLOWANCE_NAME2") = rows("ALLOWANCE_NAME2")
                newRow("ALLOWANCE_UNIT2") = rows("ALLOWANCE_UNIT2")
                newRow("MONEY2") = rows("MONEY2")
                newRow("EFFECT_FROM2") = rows("EFFECT_FROM2")
                newRow("EFFECT_TO2") = rows("EFFECT_TO2")

                newRow("PC3") = rows("PC3")
                newRow("ALLOWANCE_NAME3") = rows("ALLOWANCE_NAME3")
                newRow("ALLOWANCE_UNIT3") = rows("ALLOWANCE_UNIT3")
                newRow("MONEY3") = rows("MONEY3")
                newRow("EFFECT_FROM3") = rows("EFFECT_FROM3")
                newRow("EFFECT_TO3") = rows("EFFECT_TO3")
                newRow("PC4") = rows("PC4")
                newRow("ALLOWANCE_NAME4") = rows("ALLOWANCE_NAME4")
                newRow("ALLOWANCE_UNIT4") = rows("ALLOWANCE_UNIT4")
                newRow("MONEY4") = rows("MONEY4")
                newRow("EFFECT_FROM4") = rows("EFFECT_FROM4")
                newRow("EFFECT_TO4") = rows("EFFECT_TO4")
                newRow("PC5") = rows("PC5")
                newRow("ALLOWANCE_NAME5") = rows("ALLOWANCE_NAME5")
                newRow("ALLOWANCE_UNIT5") = rows("ALLOWANCE_UNIT5")
                newRow("MONEY5") = rows("MONEY5")
                newRow("EFFECT_FROM5") = rows("EFFECT_FROM5")
                newRow("EFFECT_TO5") = rows("EFFECT_TO5")
                'id
                newRow("RC_PROGRAM_ID") = If(IsNumeric(rows("RC_PROGRAM_ID")), Decimal.Parse(rows("RC_PROGRAM_ID")), Nothing)
                newRow("CONTRACT_TYPE_ID") = If(IsNumeric(rows("CONTRACT_TYPE_ID")), Decimal.Parse(rows("CONTRACT_TYPE_ID")), Nothing)
                newRow("SAL_TYPE_ID") = If(IsNumeric(rows("SAL_TYPE_ID")), Decimal.Parse(rows("SAL_TYPE_ID")), Nothing)
                newRow("TAX_TABLE_ID") = If(IsNumeric(rows("TAX_TABLE_ID")), Decimal.Parse(rows("TAX_TABLE_ID")), Nothing)
                newRow("EMPLOYEE_TYPE_ID") = If(IsNumeric(rows("EMPLOYEE_TYPE_ID")), Decimal.Parse(rows("EMPLOYEE_TYPE_ID")), Nothing)
                newRow("PC_ID1") = If(IsNumeric(rows("PC_ID1")), Decimal.Parse(rows("PC_ID1")), Nothing)
                newRow("ALLOWANCE_UNIT_ID1") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID1")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID1")), Nothing)
                newRow("PC_ID2") = If(IsNumeric(rows("PC_ID2")), Decimal.Parse(rows("PC_ID2")), Nothing)
                newRow("ALLOWANCE_UNIT_ID2") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID2")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID2")), Nothing)
                newRow("PC_ID3") = If(IsNumeric(rows("PC_ID3")), Decimal.Parse(rows("PC_ID3")), Nothing)
                newRow("ALLOWANCE_UNIT_ID3") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID3")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID3")), Nothing)
                newRow("PC_ID4") = If(IsNumeric(rows("PC_ID4")), Decimal.Parse(rows("PC_ID4")), Nothing)
                newRow("ALLOWANCE_UNIT_ID4") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID4")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID4")), Nothing)
                newRow("PC_ID5") = If(IsNumeric(rows("PC_ID5")), Decimal.Parse(rows("PC_ID5")), Nothing)
                newRow("ALLOWANCE_UNIT_ID5") = If(IsNumeric(rows("ALLOWANCE_UNIT_ID5")), Decimal.Parse(rows("ALLOWANCE_UNIT_ID5")), Nothing)
                newRow("RC_CANDIDATE_ID") = If(IsNumeric(rows("RC_CANDIDATE_ID")), Decimal.Parse(rows("RC_CANDIDATE_ID")), Nothing)

                dtData_Import.Rows.Add(newRow)
            Next
            dtData_Import.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData_Import.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New RecruitmentRepository()
                If sp.IMPORT_OFFERLETTER(DocXml, LogHelper.GetUserLog().Username.ToUpper) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                'rgWorking.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData_Import.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim dtDataImportLetter = dtData_Import.Clone
            dtError = dtData_Import.Clone
            Dim iRow = 1
            For Each row As DataRow In dtData_Import.Rows
                rowError = dtError.NewRow
                isError = False
                'sError = "Chưa nhập dữ liệu"
                ImportValidate.IsValidList("RC_PROGRAM_ID", "RC_PROGRAM_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("CONTRACT_TYPE_ID", "CONTRACT_TYPE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("RC_CANDIDATE_ID", "RC_CANDIDATE_ID", row, rowError, isError, "")
                ImportValidate.EmptyValue("CONTRACT_FROMDATE", row, rowError, isError, "Chưa nhập dữ liệu")
                If row("CONTRACT_FROMDATE") Is Nothing Then
                    ImportValidate.IsValidDate("CONTRACT_FROMDATE", row, rowError, isError, "Ngày sai định dạng")
                End If
                ImportValidate.IsValidList("SAL_TYPE_ID", "SAL_TYPE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("TAX_TABLE_ID", "TAX_TABLE_ID", row, rowError, isError, "")
                ImportValidate.IsValidList("EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_ID", row, rowError, isError, "")
                ImportValidate.EmptyValue("SALARY_PROBATION1", row, rowError, isError, " ")
                ImportValidate.IsValidList("SALARY_PROBATION1", "SALARY_PROBATION1", row, rowError, isError, "")
                ImportValidate.EmptyValue("OTHERSALARY1_1", row, rowError, isError, " ")
                ImportValidate.IsValidList("OTHERSALARY1_1", "OTHERSALARY1_1", row, rowError, isError, "")
                ImportValidate.EmptyValue("PERCENT_SAL1", row, rowError, isError, " ")
                ImportValidate.IsValidList("PERCENT_SAL1", "PERCENT_SAL1", row, rowError, isError, "")
                ImportValidate.EmptyValue("SALARY_OFFICIAL1", row, rowError, isError, " ")
                ImportValidate.IsValidList("SALARY_OFFICIAL1", "SALARY_OFFICIAL1", row, rowError, isError, "")
                If IsNumeric(row("DK_LUONGCB2")) AndAlso CDec(row("DK_LUONGCB2").ToString()) = 1 Then
                    ImportValidate.EmptyValue("EFFECT_DATE2", row, rowError, isError, "Chưa nhập dữ liệu")

                    If row("EFFECT_DATE2") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_DATE2", row, rowError, isError, "Ngày sai định dạng")
                    End If
                    ImportValidate.EmptyValue("SALARY_PROBATION2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("SALARY_PROBATION2", "SALARY_PROBATION2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("OTHERSALARY1_2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("OTHERSALARY1_2", "OTHERSALARY1_2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("PERCENT_SAL2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("PERCENT_SAL2", "PERCENT_SAL2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("SALARY_OFFICIAL2", row, rowError, isError, " ")
                    ImportValidate.IsValidList("SALARY_OFFICIAL2", "SALARY_OFFICIAL2", row, rowError, isError, "")
                End If
                If IsNumeric(row("PC1")) AndAlso CDec(row("PC1").ToString()) = 1 Then
                    ImportValidate.EmptyValue("PC_ID1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("PC_ID1", "PC_ID1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("ALLOWANCE_UNIT_ID1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID1", "ALLOWANCE_UNIT_ID1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("MONEY1", row, rowError, isError, " ")
                    ImportValidate.IsValidList("MONEY1", "MONEY1", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM1", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM1") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM1", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If

                If IsNumeric(row("PC2")) AndAlso CDec(row("PC2").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID2", "PC_ID2", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID2", "ALLOWANCE_UNIT_ID2", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY2", "MONEY2", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM2", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM2") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM2", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC3")) AndAlso CDec(row("PC3").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID3", "PC_ID3", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID3", "ALLOWANCE_UNIT_ID3", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY3", "MONEY3", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM3", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM3") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM3", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC4")) AndAlso CDec(row("PC4").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID4", "PC_ID4", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID4", "ALLOWANCE_UNIT_ID4", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY4", "MONEY4", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM4", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM4") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM4", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If
                If IsNumeric(row("PC5")) AndAlso CDec(row("PC5").ToString()) = 1 Then
                    ImportValidate.IsValidList("PC_ID5", "PC_ID5", row, rowError, isError, "")
                    ImportValidate.IsValidList("ALLOWANCE_UNIT_ID5", "ALLOWANCE_UNIT_ID5", row, rowError, isError, "")
                    ImportValidate.IsValidList("MONEY5", "MONEY5", row, rowError, isError, "")
                    ImportValidate.EmptyValue("EFFECT_FROM5", row, rowError, isError, "Chưa nhập dữ liệu")
                    If row("EFFECT_FROM5") Is Nothing Then
                        ImportValidate.IsValidDate("EFFECT_FROM5", row, rowError, isError, "Ngày sai định dạng")
                    End If
                End If


                If isError Then
                    rowError("CODE_RC") = row("CODE_RC").ToString
                    rowError("CANDIDATE_CODE") = row("CANDIDATE_CODE").ToString
                    rowError("FULLNAME_VN") = row("FULLNAME_VN").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportLetter.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TEMP_IMPORT_OFFERLETER');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub TableMapping(ByVal dtData_Import As DataTable)
        Dim row As DataRow = dtData_Import.Rows(1)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtData_Import.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtData_Import.Rows(0).Delete()
        dtData_Import.Rows(0).Delete()
        dtData_Import.AcceptChanges()
    End Sub
#End Region

#Region "Custom"
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New RecruitmentRepository

            'Mã nhân viên
            If hidProgramID.Value <> "" Then
                _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)

            End If
            Dim MaximumRows As Integer

            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _
                                                                _filter, _
                                                                Sorts)
                Else
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _filter)
                End If

                rgCandidateList.VirtualItemCount = MaximumRows

                If CandidateList IsNot Nothing Then
                    rgCandidateList.DataSource = CandidateList
                Else
                    rgCandidateList.DataSource = New List(Of CandidateDTO)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetListCandidate(_filter, Sorts).ToTable
                Else
                    Return rep.GetListCandidate(_filter).ToTable
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            dsData.Tables(0).TableName = "Table"
            designer.SetDataSource(dsData)

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
#End Region

    'Protected Sub chkPassed_CheckedChanged(sender As Object, e As EventArgs) Handles chkPassed.CheckedChanged

    'End Sub
    'Protected Sub chkElect_CheckedChanged(sender As Object, e As EventArgs) Handles chkElect.CheckedChanged

    'End Sub
    'Protected Sub chkInternal_CheckedChanged(sender As Object, e As EventArgs) Handles chkInternal.CheckedChanged

    'End Sub
    'Protected Sub chkPotential_CheckedChanged(sender As Object, e As EventArgs) Handles chkPotential.CheckedChanged

    'End Sub
    'Protected Sub chkInvitation_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvitation.CheckedChanged

    'End Sub
    'Protected Sub chkEmployee_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmployee.CheckedChanged

    'End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim eventArg As String = e.Argument  'Request("__EVENTARGUMENT")
            If Left(eventArg, 13) <> "PopupPostback" Then
                Exit Sub
            End If
            If eventArg <> "" Then
                eventArg = Right(eventArg, eventArg.Length - 14)
                If eventArg = "Cancel" Then
                ElseIf eventArg = "OK" Then
                    'For Each dr As GridDataItem In rgCandidateList.SelectedItems
                    '    If dr.GetDataKeyValue("ID_CANDIDATE") = Session("ID_CANDIDATE") Then
                    '        dr("STATUS_NAME").Text = "Ứng viên đã chuyển sang vị trí khác"
                    '        dr("STATUS_CODE").Text = "CHUYENVITRI"
                    '        Exit For
                    '    End If
                    'Next
                    ShowMessage("Thao tác thành công", NotifyType.Success)
                    rgCandidateList.Rebind()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub btnNegotiate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNegotiate.Click
    '    Try
    '        Dim IS_EMP As Integer = 0
    '        If rgCandidateList.SelectedItems.Count = 0 Then
    '            ShowMessage(Translate("Bạn chưa chọn ứng viên nào"), NotifyType.Warning)
    '            Exit Sub
    '        ElseIf rgCandidateList.SelectedItems.Count > 1 Then
    '            ShowMessage(Translate("Chỉ được chọn 1 ứng viên"), NotifyType.Warning)
    '            Exit Sub
    '        Else
    '            Dim status As String
    '            Dim items As GridDataItem = DirectCast(rgCandidateList.SelectedItems(0), GridDataItem)
    '            status = items.GetDataKeyValue("STATUS_CODE").ToString
    '            'If status <> RCContant.TRUNGTUYEN Then
    '            '    ShowMessage(Translate("Vui lòng chọn ứng viên có trạng thái là Trúng tuyển"), NotifyType.Warning)
    '            '    Exit Sub
    '            'End If
    '            If status <> RCContant.TRUNGTUYEN Then
    '                IS_EMP = 1
    '            End If
    '            rwPopup.VisibleOnPageLoad = True
    '            rwPopup.NavigateUrl = "/Dialog.aspx?mid=Recruitment&fid=ctrlRC_CandidateNegotiate&group=Business&ProgramID=" & hidProgramID.Value & "&CandidateID=" & items.GetDataKeyValue("ID_CANDIDATE") & "&IS_EMP=" & IS_EMP
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub


    Private Sub btnDeleteNegotiate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteNegotiate.Click
        Try
            Dim rep As New RecruitmentRepository
            Dim psp As New RecruitmentStoreProcedure
            Dim StrErr As String = ""
            Dim statusDel As Decimal
            For Each items As GridDataItem In rgCandidateList.SelectedItems
                Dim STATUS_CODE As String = String.Empty
                STATUS_CODE = items.GetDataKeyValue("STATUS_CODE").ToString
                If STATUS_CODE = "NHANVIEN" Then
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                End If
                'If STATUS_CODE = "TRUNGTUYEN" Then
                Dim lst = New RCNegotiateDTO
                lst.RC_CANDIDATE_ID = items.GetDataKeyValue("ID_CANDIDATE")
                lst.RC_PROGRAM_ID = Request.Params("PROGRAM_ID")
                Dim funcDel As Decimal = rep.DeleteRCNegotiate(lst)
                psp.DELETE_SALARY_ALLOWANCE_CANDIDATE(Request.Params("PROGRAM_ID"), items.GetDataKeyValue("ID_CANDIDATE"))
                Dim Offer_ID As String = ""
                rep.UPDATE_PROGRAM_CANDIDATE_OFFER_ID(items.GetDataKeyValue("ID_CANDIDATE"), Request.Params("PROGRAM_ID"), Offer_ID)
                If funcDel = 0 Then
                    statusDel = 0
                    StrErr += items.GetDataKeyValue("CANDIDATE_CODE") + ","
                ElseIf funcDel <> 1 Then
                    statusDel = 2
                    StrErr += items.GetDataKeyValue("CANDIDATE_CODE") + ","
                End If
                'End If
            Next
            If statusDel = 2 Then
                CurrentState = CommonMessage.STATE_NORMAL
                ShowMessage(Translate("Nhân viên " + StrErr + " xóa không thành công."), NotifyType.Warning)
                Exit Sub
            End If
            If StrErr <> "" Then
                CurrentState = CommonMessage.STATE_NORMAL
                ShowMessage(Translate("Nhân viên " + StrErr + " không có thông tin đàm phán."), NotifyType.Warning)
                rgCandidateList.Rebind()
            Else
                CurrentState = CommonMessage.STATE_NORMAL
                rgCandidateList.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class