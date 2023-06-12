Imports System.IO
Imports Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Newtonsoft.Json
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRC_CandidateList

    Inherits Common.CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Recruitment\Modules\Business" + Me.GetType().Name.ToString()
    Public Property AjaxManagerId As String
    Protected WithEvents ctrlFindProgramDialog As New ctrlFindProgramPopupDialog
#Region "Properties"
    ' Không dùng nên khóa lại
    'Private Property CandidateList As List(Of CandidateDTO)
    '    Get
    '        Return ViewState(Me.ID & "_CandidateList")
    '    End Get
    '    Set(ByVal value As List(Of CandidateDTO))
    '        ViewState(Me.ID & "_CandidateList") = value
    '    End Set
    'End Property
    Private Property sender As String
        Get
            Return ViewState(Me.ID & "_sender")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_sender") = value
        End Set
    End Property
    Dim str As Integer
    ' Không dùng nên khóa lại
    'Public Property _filter As CandidateDTO
    '    Get
    '        If PageViewState(Me.ID & "_filter") Is Nothing Then
    '            PageViewState(Me.ID & "_filter") = New CandidateDTO
    '        End If
    '        Return PageViewState(Me.ID & "_filter")
    '    End Get
    '    Set(ByVal value As CandidateDTO)
    '        PageViewState(Me.ID & "_filter") = value
    '    End Set
    'End Property

    Private Property lstData As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_lstData") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region
    Public WithEvents AjaxManager As RadAjaxManager
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.ClientSettings.EnablePostBackOnRowClick = False
            rgCandidateList.SetFilter()
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
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Previous, ToolbarItem.Next)

            MainToolBar.Items(4).Text = Translate("Tạo vòng PV")
            MainToolBar.Items(5).Text = Translate("Lập lịch PV")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg.Value = objPro.ORG_ID
                lblTitleName.Text = objPro.TITLE_NAME
                hidTitle.Value = objPro.TITLE_ID
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
                CurrentState = CommonMessage.STATE_NORMAL
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.LoadAllOrganization = True
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub


#End Region

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Me.sender = "btnSearch"
            rgCandidateList.CurrentPageIndex = 0
            rgCandidateList.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgCandidateList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgCandidateList.ExportExcel(Server, Response, dtData, "Danh sách ứng viên")
                            Exit Sub
                        End If
                    End Using
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT');", True)



                Case TOOLBARITEM_PREVIOUS
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramExams&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")

                Case TOOLBARITEM_NEXT
                    Page.Response.Redirect("Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=" & hidProgramID.Value & "")


            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnBlacklist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlacklist.Click
        Dim strEmp As String = ""
        Dim status As String
        Dim EXISTS_EMP As String = ""
        'HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage("Vui lòng chọn 1 ứng viên", NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_ID")
            Select Case status
                Case RCContant.NHANVIEN
                    ' ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    EXISTS_EMP = "Tồn tại ứng viên đang ở trạng thái Đã là nhân viên, "
                    'Exit Sub
            End Select
            If strEmp = "" Then
                strEmp = dr.GetDataKeyValue("FULLNAME_VN")
            Else
                strEmp = strEmp + "," + dr.GetDataKeyValue("FULLNAME_VN")
            End If
        Next
        ctrlMessageBox.MessageText = Translate(EXISTS_EMP + "Bạn có chắc chắn muốn chuyển trạng thái Blacklist cho các ứng viên?")
        ctrlMessageBox.ActionName = "BLACKLIST"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnDuDieuKien_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDuDieuKien.Click

        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            Dim status_id As String = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = "DUDIEUKIEN"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnKhongDuDieuKien_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKhongDuDieuKien.Click
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            Dim status_id As String = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đủ điều kiện"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Không đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = "KHONGDUDIEUKIEN"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub



    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strID As String
                Dim strID_Program_Candidate As String
                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                Dim lstProgramCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("ID"))
                    lstProgramCanID.Add(dr.GetDataKeyValue("RC_PROGRAM_CANDIDATE_ID"))
                Next
                Using rep As New RecruitmentRepository
                    'Xóa nhân viên.
                    Select Case e.ActionName
                        Case CommonMessage.TOOLBARITEM_DELETE
                            Dim strError As String = ""
                            rep.DeleteCandidate(lstCanID, strError)
                            If strError = "" Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate("Tồn tại ứng viên đã tham gia thi tuyển") & strError.Substring(1, strError.Length - 1), Utilities.NotifyType.Error)
                            End If
                        Case "DUDIEUKIEN"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "KHONGDUDIEUKIEN"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGDUDIEUKIEN_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "BLACKLIST"
                            If rep.UpdateBlackListCandidate(lstCanID, True) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                rep.UpdateBlackListRc_Program_Candidate(lstProgramCanID)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "CHUYENUNGVIEN"
                            For Each dr As GridDataItem In rgCandidateList.SelectedItems
                                strID &= IIf(strID = vbNullString, dr.GetDataKeyValue("ID"), "," & dr.GetDataKeyValue("ID"))
                                strID_Program_Candidate &= IIf(strID_Program_Candidate = vbNullString, dr.GetDataKeyValue("RC_PROGRAM_CANDIDATE_ID"), "," & dr.GetDataKeyValue("RC_PROGRAM_CANDIDATE_ID"))
                            Next

                            'Session("ID_CANDIDATE") = strID
                            'Session("ID_PROGRAM_CANDIDATE") = strID_Program_Candidate
                            'Session("P_PROGRAM_ID_OLD") = hidProgramID.Value
                            Dim dic As New Dictionary(Of String, Object)
                            dic.Add("ID_CANDIDATE", strID)
                            dic.Add("ID_PROGRAM_CANDIDATE", strID_Program_Candidate)
                            dic.Add("P_PROGRAM_ID_OLD", hidProgramID.Value)
                            If Not FindOrgTitle.Controls.Contains(ctrlFindProgramDialog) Then
                                ctrlFindProgramDialog = Me.Register("ctrlFindProgramPopupDialog", "Recruitment", "ctrlFindProgramPopupDialog", "Shared", dic)
                                ctrlFindProgramDialog.LoadAllOrganization = True
                                FindOrgTitle.Controls.Add(ctrlFindProgramDialog)
                                ctrlFindProgramDialog.Show()
                            End If
                    End Select
                    rgCandidateList.Rebind()
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cmdYCTDKhac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYCTDKhac.Click
        Dim strEmp As String = ""
        Dim status As String
        Dim EXISTS_EMP As String = ""
        'HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage("Vui lòng chọn 1 ứng viên", NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_ID")
            Select Case status
                Case RCContant.NHANVIEN
                    ' ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    EXISTS_EMP = "Tồn tại ứng viên đang ở trạng thái Đã là nhân viên, "
                    'Exit Sub
            End Select
            If strEmp = "" Then
                strEmp = dr.GetDataKeyValue("FULLNAME_VN")
            Else
                strEmp = strEmp + "," + dr.GetDataKeyValue("FULLNAME_VN")
            End If
        Next

        ctrlMessageBox.MessageText = Translate(EXISTS_EMP + "Bạn có muốn chuyển ứng viên: " + strEmp + " sang vị trí khác không")
        ctrlMessageBox.ActionName = "CHUYENUNGVIEN"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCandidateList.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            Dim code = url.Split(";")(0)
            Select Case code
                Case "TRANSFER"
                    Dim lstCanID As New List(Of Decimal)
                    Using rep As New RecruitmentRepository
                        For Each item As GridDataItem In rgCandidateList.SelectedItems
                            lstCanID.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateProgramCandidate(lstCanID, url.Split(";")(1)) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            rgCandidateList.Rebind()
                        End If
                    End Using
                Case "PONTENTIAL_TRANSFER"
                    Dim lstCanID As New List(Of Decimal)
                    lstCanID.Add(url.Split(";")(1))
                    Using rep As New RecruitmentRepository
                        If rep.UpdateProgramCandidate(lstCanID, hidProgramID.Value) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            rgCandidateList.Rebind()
                        End If
                    End Using
            End Select
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
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function GetErrorCodeByKey(ByVal strKey As String)
        Dim pathJson As String = HttpContext.Current.Server.MapPath("JsonErrorCode\JsonLangs.json")
        Dim strValue As String = String.Empty
        Try
            Dim stsJson As String = File.ReadAllText(pathJson)
            Dim data As Dictionary(Of String, String) = readJson(stsJson)
            strValue = (From obj In data
                        Where obj.Key.ToUpper.Contains(strKey.ToUpper)
                        Select obj.Value).First
            If String.IsNullOrEmpty(strValue) Then
                Return strKey
            Else
                Return strValue
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetValidateCodeByKey(ByVal strKey As String)
        Dim pathJson As String = HttpContext.Current.Server.MapPath("JsonErrorCode\JsonValidateCode.json")
        Dim strValue As String = String.Empty
        Try
            Dim stsJson As String = File.ReadAllText(pathJson)
            Dim data As Dictionary(Of String, String) = readJson(stsJson)
            strValue = (From obj In data
                        Where obj.Key.ToUpper.Contains(strKey.ToUpper)
                        Select obj.Value).First
            If String.IsNullOrEmpty(strValue) Then
                Return strKey
            Else
                Return strValue
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function readJson(ByVal strJson As String)
        Try
            Dim read = Newtonsoft.Json.Linq.JObject.Parse(strJson)
            Dim data = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(strJson)
            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function CreateCanImport(ByVal dtData As DataTable) As List(Of CandidateImportDTO)
        Dim lst As New List(Of CandidateImportDTO)
        Try
            For Each dr In dtData.Rows
                Dim can_cv As CandidateCVDTO
                Dim can_other As CandidateOtherInfoDTO
                Dim can_edu As New CandidateEduDTO
                Dim can As New CandidateDTO
                Dim canimport As New CandidateImportDTO
                'Candidate
                can.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
                can.ORG_ID = Decimal.Parse(hidOrg.Value)
                can.TITLE_ID = Decimal.Parse(hidTitle.Value)
                can.FIRST_NAME_VN = dr("FIRST_NAME_VN").ToString
                can.LAST_NAME_VN = dr("LAST_NAME_VN").ToString

                'Candidate CV
                can_cv = New CandidateCVDTO
                can_cv.GENDER = dr("GENDER").ToString
                can_cv.NATIVE = dr("NATIVE").ToString
                can_cv.MARITAL_STATUS = dr("MARITAL_STATUS").ToString
                can_cv.RELIGION = dr("RELIGION").ToString
                can_cv.ID_NO = Decimal.Parse(dr("ID_NO").ToString)

                can_cv.ID_DATE = Date.Parse(dr("ID_DATE").ToString)

                If dr("ID_PLACE").ToString <> "" Then
                    can_cv.ID_PLACE = Decimal.Parse(dr("ID_PLACE").ToString)
                End If

                can_cv.PASSPORT_ID = dr("PASSPORT_ID").ToString
                can_cv.PASSPORT_PLACE_NAME = dr("PASSPORT_PLACE_NAME").ToString
                can_cv.BIRTH_DATE = Date.Parse(dr("BIRTH_DATE").ToString)

                If dr("BIRTH_NATION_ID").ToString <> "" Then
                    can_cv.BIRTH_NATION_ID = Decimal.Parse(dr("BIRTH_NATION_ID"))
                End If
                If dr("BIRTH_PROVINCE").ToString <> "" Then
                    can_cv.BIRTH_PROVINCE = Decimal.Parse(dr("BIRTH_PROVINCE"))
                End If
                If dr("NATIONALITY_ID").ToString <> "" Then
                    can_cv.NATIONALITY_ID = Decimal.Parse(dr("NATIONALITY_ID"))
                End If
                If dr("NAV_NATION_ID").ToString <> "" Then
                    can_cv.NAV_NATION_ID = Decimal.Parse(dr("NAV_NATION_ID"))
                End If
                If dr("NAV_PROVINCE").ToString <> "" Then
                    can_cv.NAV_PROVINCE = Decimal.Parse(dr("NAV_PROVINCE"))
                End If
                can_cv.PER_ADDRESS = dr("PER_ADDRESS").ToString
                If dr("PER_DISTRICT_ID").ToString <> "" Then
                    can_cv.PER_DISTRICT_ID = Decimal.Parse(dr("PER_DISTRICT_ID"))
                End If
                If dr("PER_NATION_ID").ToString <> "" Then
                    can_cv.PER_NATION_ID = Decimal.Parse(dr("PER_NATION_ID"))
                End If
                If dr("PER_PROVINCE").ToString <> "" Then
                    can_cv.PER_PROVINCE = Decimal.Parse(dr("PER_PROVINCE"))
                End If
                can_cv.CONTACT_ADDRESS = dr("CONTACT_ADDRESS")
                If dr("CONTACT_NATION_ID").ToString <> "" Then
                    can_cv.CONTACT_NATION_ID = Decimal.Parse(dr("CONTACT_NATION_ID"))
                End If
                If dr("CONTACT_PROVINCE").ToString <> "" Then
                    can_cv.CONTACT_PROVINCE = Decimal.Parse(dr("CONTACT_PROVINCE"))
                End If
                If dr("CONTACT_DISTRICT_ID").ToString <> "" Then
                    can_cv.CONTACT_DISTRICT_ID = Decimal.Parse(dr("CONTACT_DISTRICT_ID"))
                End If
                If dr("ID_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.ID_DATE_EXPIRATION = Date.Parse(dr("ID_DATE_EXPIRATION"))
                End If

                can_cv.IS_RESIDENT = 0
                If dr("IS_RESIDENT").ToString <> 0 Then
                    can_cv.IS_RESIDENT = Decimal.Parse(dr("IS_RESIDENT"))
                End If


                can_cv.CONTACT_MOBILE = dr("CONTACT_MOBILE").ToString
                can_cv.CONTACT_PHONE = dr("CONTACT_PHONE").ToString
                can_cv.PER_EMAIL = dr("PER_EMAIL").ToString
                can_cv.PERTAXCODE = dr("PERTAXCODE").ToString
                If dr("PER_TAX_DATE").ToString <> "" Then
                    can_cv.PER_TAX_DATE = Date.Parse(dr("PER_TAX_DATE"))
                End If
                can_cv.PER_TAX_PLACE = dr("PER_TAX_PLACE").ToString
                If dr("PASSPORT_DATE").ToString <> "" Then
                    can_cv.PASSPORT_DATE = Date.Parse(dr("PASSPORT_DATE"))
                End If
                If dr("PASSPORT_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.PASSPORT_DATE_EXPIRATION = Date.Parse(dr("PASSPORT_DATE_EXPIRATION"))
                End If
                can_cv.VISA_NUMBER = dr("VISA_NUMBER").ToString
                If dr("VISA_DATE").ToString <> "" Then
                    can_cv.VISA_DATE = Date.Parse(dr("VISA_DATE"))
                End If
                If dr("VISA_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.VISA_DATE_EXPIRATION = Date.Parse(dr("VISA_DATE_EXPIRATION"))
                End If
                can_cv.VISA_PLACE = dr("VISA_PLACE").ToString
                can_cv.VNAIRLINES_NUMBER = dr("VNAIRLINES_NUMBER").ToString
                If dr("VNAIRLINES_DATE").ToString <> "" Then
                    can_cv.VNAIRLINES_DATE = Date.Parse(dr("VNAIRLINES_DATE"))
                End If
                If dr("VNAIRLINES_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.VNAIRLINES_DATE_EXPIRATION = Date.Parse(dr("VNAIRLINES_DATE_EXPIRATION"))
                End If
                can_cv.VNAIRLINES_PLACE = dr("VNAIRLINES_PLACE").ToString
                can_cv.LABOUR_NUMBER = dr("LABOUR_NUMBER").ToString
                If dr("LABOUR_DATE").ToString <> "" Then
                    can_cv.LABOUR_DATE = Date.Parse(dr("LABOUR_DATE"))
                End If
                If dr("LABOUR_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.LABOUR_DATE_EXPIRATION = Date.Parse(dr("LABOUR_DATE_EXPIRATION"))
                End If

                can_cv.LABOUR_PLACE = dr("LABOUR_PLACE").ToString
                can_cv.WORK_PERMIT = dr("WORK_PERMIT").ToString
                If dr("WORK_PERMIT_END").ToString <> "" Then
                    can_cv.WORK_PERMIT_END = Date.Parse(dr("WORK_PERMIT_END"))
                End If
                If dr("WORK_PERMIT_START").ToString <> "" Then
                    can_cv.WORK_PERMIT_START = Date.Parse(dr("WORK_PERMIT_START"))
                End If

                can_cv.TEMP_RESIDENCE_CARD = dr("TEMP_RESIDENCE_CARD")
                If dr("TEMP_RESIDENCE_CARD_START").ToString <> "" Then
                    can_cv.TEMP_RESIDENCE_CARD_START = Date.Parse(dr("TEMP_RESIDENCE_CARD_START"))
                End If
                If dr("TEMP_RESIDENCE_CARD_END").ToString <> "" Then
                    can_cv.TEMP_RESIDENCE_CARD_END = Date.Parse(dr("TEMP_RESIDENCE_CARD_END"))
                End If

                'CanEducation

                If dr("MARK_EDU").ToString <> "" Then
                    can_edu.MARK_EDU = dr("MARK_EDU")
                End If

                If dr("ACADEMY").ToString <> "" Then
                    can_edu.ACADEMY = dr("ACADEMY")
                End If

                If dr("LEARNING_LEVEL").ToString <> "" Then
                    can_edu.LEARNING_LEVEL = dr("LEARNING_LEVEL")
                End If

                If dr("MAJOR").ToString <> "" Then
                    can_edu.MAJOR = dr("MAJOR")
                End If

                If dr("SCHOOL").ToString <> "" Then
                    can_edu.SCHOOL = dr("SCHOOL")
                End If

                If dr("FIELD").ToString <> "" Then
                    can_edu.FIELD = dr("FIELD")
                End If

                If dr("DEGREE").ToString <> "" Then
                    can_edu.DEGREE = dr("DEGREE")
                End If

                If dr("GPA").ToString <> "" Then
                    can_edu.GPA = Decimal.Parse(dr("GPA"))
                End If

                can_edu.IT_CERTIFICATE = dr("IT_CERTIFICATE")
                If dr("IT_LEVEL").ToString <> "" Then
                    can_edu.IT_LEVEL = dr("IT_LEVEL")
                End If
                can_edu.IT_MARK = dr("IT_MARK").ToString

                can_edu.IT_CERTIFICATE1 = dr("IT_CERTIFICATE1").ToString
                If dr("IT_LEVEL1").ToString <> "" Then
                    can_edu.IT_LEVEL1 = dr("IT_LEVEL1")
                End If
                can_edu.IT_MARK1 = dr("IT_MARK1").ToString

                can_edu.IT_CERTIFICATE2 = dr("IT_CERTIFICATE2").ToString
                If dr("IT_LEVEL2").ToString <> "" Then
                    can_edu.IT_LEVEL2 = dr("IT_LEVEL2").ToString
                End If
                can_edu.IT_MARK2 = dr("IT_MARK2").ToString

                can_edu.ENGLISH = dr("ENGLISH").ToString
                If dr("ENGLISH_LEVEL").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL = dr("ENGLISH_LEVEL")
                End If
                If dr("ENGLISH_MARK").ToString <> "" Then
                    can_edu.ENGLISH_MARK = dr("ENGLISH_MARK")
                End If
                can_edu.ENGLISH1 = dr("ENGLISH1").ToString
                If dr("ENGLISH_LEVEL1").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL1 = dr("ENGLISH_LEVEL1")
                End If
                If dr("ENGLISH_MARK1").ToString <> "" Then
                    can_edu.ENGLISH_MARK1 = dr("ENGLISH_MARK1")
                End If
                can_edu.ENGLISH2 = dr("ENGLISH2").ToString
                If dr("ENGLISH_LEVEL2").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL2 = dr("ENGLISH_LEVEL2")
                End If
                If dr("ENGLISH_MARK2").ToString <> "" Then
                    can_edu.ENGLISH_MARK2 = dr("ENGLISH_MARK2")
                End If

                'Candidate Other
                can_other = New CandidateOtherInfoDTO

                can_other.DOAN_PHI = 0
                If dr("CONGDOANPHI").ToString <> 0 Then
                    can_other.DOAN_PHI = -1
                End If
                If dr("NGAYVAOCONGDOAN").ToString <> "" Then
                    can_other.NGAY_VAO_DOAN = Date.Parse(dr("NGAYVAOCONGDOAN"))
                End If
                can_other.NOI_VAO_DOAN = dr("NOIVAOCONGDOAN")
                can_other.ACCOUNT_NAME = dr("ACCOUNT_NAME")
                If dr("ACCOUNT_NUMBER").ToString <> "" Then
                    can_other.ACCOUNT_NUMBER = Decimal.Parse(dr("ACCOUNT_NUMBER"))
                End If

                can_other.BANK = dr("BANK").ToString
                can_other.BANK_BRANCH = dr("BANK_BRANCH").ToString
                can_other.IS_PAYMENT_VIA_BANK = 0
                If dr("IS_PAYMENT_VIA_BANK").ToString <> 0 Then
                    can_other.IS_PAYMENT_VIA_BANK = 1
                End If

                If dr("ACCOUNT_EFFECT_DATE").ToString <> "" Then
                    can_other.ACCOUNT_EFFECT_DATE = Date.Parse(dr("ACCOUNT_EFFECT_DATE"))
                End If
                canimport.can = can
                canimport.can_cv = can_cv
                canimport.can_edu = can_edu
                canimport.can_other = can_other
                lst.Add(canimport)
            Next

            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Using rep As New RecruitmentRepository
                    If rep.TransferHSNVToCandidate(lstCommonEmployee(0).EMPLOYEE_ID, hidOrg.Value, hidTitle.Value, hidProgramID.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgCandidateList.Rebind()
                    End If
                End Using

            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

#End Region

#Region "Custom"
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New RecruitmentRepository
            Dim _filter As New CandidateDTO
            'Mã nhân viên
            ' Không dùng nên tạm khóa
            '_filter.CANDIDATE_CODE = txtCandidateCode.Text.Trim()
            SetValueObjectByRadGrid(rgCandidateList, _filter)
            If hidProgramID.Value <> "" Then
                _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)

            End If

            ' Set params filter
            If chkCandidateUnsatisfactory.Checked Then
                _filter.KHONGDUDIEUKIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGDUDIEUKIEN_ID
            Else
                _filter.KHONGDUDIEUKIEN_ID = ""
            End If
            If chkCandidateQualified.Checked Then
                _filter.DUDIEUKIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID
            Else
                _filter.DUDIEUKIEN_ID = ""
            End If
            If chkElectCandidate.Checked Then
                _filter.TRUNGTUYEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
            Else
                _filter.TRUNGTUYEN_ID = ""
            End If
            If chkCandidatePotential.Checked Then
                _filter.PONTENTIAL = RecruitmentCommon.RC_CANDIDATE_STATUS.PONTENTIAL
            Else
                _filter.PONTENTIAL = ""
            End If
            If chkCandidateCancel.Checked Then
                _filter.TUCHOI_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.TUCHOI_ID
            Else
                _filter.TUCHOI_ID = ""
            End If
            If chkCandidateHavesentmail.Checked Then
                _filter.GUITHU_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
            Else
                _filter.GUITHU_ID = ""
            End If
            If chkCandiateIsEmp.Checked Then
                _filter.LANHANVIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
            Else
                _filter.LANHANVIEN_ID = ""
            End If
            If chkCandidateIsLocaltion.Checked Then
                _filter.NOIBO_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.NOIBO_ID
            Else
                _filter.NOIBO_ID = ""
            End If

            Dim MaximumRows As Integer

            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of CandidateDTO)
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstData = rep.GetListCandidatePaging(rgCandidateList.CurrentPageIndex,
                                                                rgCandidateList.PageSize,
                                                                MaximumRows,
                                                                _filter,
                                                                Sorts)
                Else
                    lstData = rep.GetListCandidatePaging(rgCandidateList.CurrentPageIndex,
                                                                rgCandidateList.PageSize,
                                                                MaximumRows, _filter)
                End If
                rgCandidateList.VirtualItemCount = MaximumRows

                If lstData IsNot Nothing Then
                    rgCandidateList.DataSource = lstData
                Else
                    rgCandidateList.DataSource = New List(Of CandidateDTO)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetListCandidatePaging(_filter, Sorts).ToTable
                Else
                    Return rep.GetListCandidatePaging(_filter).ToTable
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region


End Class