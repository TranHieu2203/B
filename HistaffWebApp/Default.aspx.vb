Imports System.Web.Services
Imports Common
Imports Framework.UI
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class _Default
    Inherits AjaxPage
    Protected WithEvents CurrentView As ViewBase
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = Me.GetType().Name.ToString()

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            MyBase.OnInit(e)
            Me.AjaxManager = RadAjaxManager1
            Me.AjaxLoading = LoadingPanel
            Me.ToolTipManager = Me.RadToolTipManager1
            Me.PopupWindow = rwMainPopup
            If Request.Params("EnableAjax") IsNot Nothing AndAlso Request.Params("EnableAjax") = False Then
                Me.AjaxManager.EnableAJAX = False
            End If
            Dim mid As String = Request.Params("mid")
            Dim fid As String = Request.Params("fid")
            Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))

            Try
                If mid IsNot Nothing AndAlso mid.Trim <> "" AndAlso fid IsNot Nothing AndAlso fid.Trim <> "" Then
                    CurrentView = Me.Register(fid, mid, fid, group, , True)
                    Me.Title = CurrentView.ViewDescription
                End If

            Catch ex As Exception
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                DisplayException("", "", ex)
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException("", "", ex)
        End Try


    End Sub

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))
        Dim tlbQuickLaunchToolbar As RadToolBar = Nothing
        Dim mnuMain As RadMenu = Nothing
        Try
            If Not IsPostBack Then
                Try
                    If Common.Common.GetUsername <> "" Then
                        tlbQuickLaunchToolbar = Me.Master.FindControl("QuickLaunchToolbar")
                        If tlbQuickLaunchToolbar IsNot Nothing AndAlso mid IsNot Nothing AndAlso mid.Trim <> "" Then
                            tlbQuickLaunchToolbar.LoadContentFile(String.Format(Utilities.ModulePath & "/{0}/QuickLaunch-" & Common.Common.SystemLanguage.Name & ".xml", mid))
                        End If
                        Using rep As New CommonRepository


                            Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)
                            If Not GroupAdmin Then
                                Dim lstPermission = rep.GetUserPermissions(Common.Common.GetUsername)
                                For Each dr In tlbQuickLaunchToolbar.GetAllItems
                                    If Not DirectCast(dr, RadToolBarButton).IsSeparator Then
                                        Dim query = (From p In lstPermission Where p.FID = dr.Value).Count
                                        If query = 0 Then
                                            dr.Visible = False
                                        End If
                                    End If
                                Next

                                If mid Is Nothing OrElse mid.Trim = "" OrElse fid Is Nothing OrElse fid.Trim = "" Then
                                    Utilities.Redirect("Profile", "ctrlInformation")
                                End If
                            Else
                                Dim user = LogHelper.CurrentUser

                                If mid Is Nothing OrElse mid.Trim = "" OrElse fid Is Nothing OrElse fid.Trim = "" Then
                                    Utilities.Redirect("Profile", "ctrlInformation")
                                End If

                                Dim bShowQuickLaunch As Boolean = False
                                If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(mid)) Then
                                    bShowQuickLaunch = True
                                End If
                                If Not bShowQuickLaunch Then
                                    For Each dr In tlbQuickLaunchToolbar.GetAllItems
                                        dr.Visible = False
                                    Next
                                End If

                            End If
                        End Using
                    End If
                    _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
                Catch ex As Exception
                    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                End Try
                Me.DataBind()

            End If

            'Kiểm tra new user đã thay đổi mật khẩu lần đầu
            If LogHelper.CurrentUser IsNot Nothing Then
                If LogHelper.CurrentUser.IS_LOGIN AndAlso Not LogHelper.CurrentUser.IS_AD Then
                    HttpContext.Current.Response.Redirect("/ChangePassword.aspx", False)
                    Exit Sub
                End If
            End If
            ''kIEM TRA HET HAN BAO TRI
            'Using rep As New CommonRepository
            '    Dim checks As Boolean = rep.CHECK_SYSTEM_MAINTAIN_IS_END()
            '    If checks = -1 AndAlso LogHelper.CurrentUser.MODULE_ADMIN <> "*" Then
            '        If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
            '            LogHelper.SaveAccessLog(Session.SessionID, "Logout")
            '            LogHelper.OnlineUsers.Remove(Session.SessionID)
            '            Session.Abandon()
            '            FormsAuthentication.SignOut()
            '            'FormsAuthentication.RedirectToLoginPage()
            '            Me.Response.Redirect("/Default.aspx")
            '        End If
            '    End If
            'End Using
            If CurrentView IsNot Nothing AndAlso Not PagePlaceHolder.Controls.Contains(CurrentView) Then
                Try
                    PagePlaceHolder.Controls.Add(CurrentView)
                Catch ex As Exception
                    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                End Try

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ' DisplayException("", "", ex)
        End Try
    End Sub

    <WebMethod()>
    Public Shared Function CheckExistsEmail(ByVal Email As String, ByVal Empcode As String) As Boolean
        Try
            Dim rep As New ProfileBusinessRepository
            Dim lstObj = rep.GetEmployeeByEmail(Email, 0, Integer.MaxValue, 0, New ParamDTO With {.ORG_ID = 1, .EMP_CODE = Empcode, .IS_DISSOLVE = False})
            Return lstObj.Any
        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Create by SONTV 03:01 AM - 09/07/2021
    ''' </summary>
    ''' <param name="data">collapse-10 (10: data of collapse)</param>
    ''' <returns> Code HTML fill Into Information</returns>
    ''' <remarks>Just write to show Remind Information near by Account Infor</remarks>
    <WebMethod()>
    Public Shared Function getInformationRemind(ByVal data As Decimal) As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim RemindList As New List(Of ReminderLogDTO)
        Dim result As String = ""

        Dim birthdayRemind As Integer
        Dim contractRemind As Integer
        Dim probationRemind As Integer
        Dim retireRemind As Integer

        Dim approveRemind As Integer
        Dim approveHDLDRemind As Integer
        Dim approveTHHDRemind As Integer
        Dim maternitiRemind As Integer
        Dim retirementRemind As Integer
        Dim noneSalaryRemind As Integer
        Dim noneExpiredCertificateRemind As Integer
        Dim noneBIRTHDAY_LD As Integer
        Dim noneConcurrently As Integer
        Dim noneEmpDtlFamily As Integer

        Dim nonOverRegDate As Integer

        Dim nonExpDiscipline As Integer
        Dim nonInsArising As Integer
        Dim nonSign As Integer
        Dim nonBHYT As Integer
        Dim age18 As Integer

        Dim workingRemind As Integer
        Dim terminateRemind As Integer
        Dim terminateDebtRemind As Integer
        Dim noPaperRemind As Integer
        Dim visaRemind As Integer
        Dim worPermitRemind As Integer
        Dim certificateRemind As Integer
        Dim authorityRemind As Integer
        Dim noticePersonalIncomeRemind As Integer
        Dim stockRemind As Integer
        Dim approveHSNV As Integer
        Dim approveWorkBefore As Integer
        Dim approveFamily As Integer
        Dim approveCertificate As Integer
        Using rep As New ProfileDashboardRepository

            Try
                If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                    contractRemind = CommonConfig.ReminderContractDays
                    birthdayRemind = CommonConfig.ReminderBirthdayDays
                    probationRemind = CommonConfig.ReminderProbation

                    workingRemind = CommonConfig.ReminderWorking
                    terminateRemind = CommonConfig.ReminderTerminate
                    terminateDebtRemind = CommonConfig.ReminderTerminateDebt
                    noPaperRemind = CommonConfig.ReminderNoPaper
                    visaRemind = CommonConfig.ReminderVisa

                    worPermitRemind = CommonConfig.ReminderLabor
                    certificateRemind = CommonConfig.ReminderCertificate

                    approveRemind = CommonConfig.ReminderApproveDays
                    approveHDLDRemind = CommonConfig.ReminderApproveHDLDDays
                    approveTHHDRemind = CommonConfig.ReminderApproveTHHDDays
                    maternitiRemind = CommonConfig.ReminderMaternitiDays
                    retirementRemind = CommonConfig.ReminderRetirementDays
                    noneSalaryRemind = CommonConfig.ReminderNoneSalaryDays
                    noneExpiredCertificateRemind = CommonConfig.ReminderExpiredCertificate
                    noneBIRTHDAY_LD = CommonConfig.ReminderBIRTHDAY_LD
                    noneConcurrently = CommonConfig.ReminderConcurrently
                    noneEmpDtlFamily = CommonConfig.ReminderEmpDtlFamily
                    nonOverRegDate = CommonConfig.ReminderOverRegDate

                    nonExpDiscipline = CommonConfig.ReminderExpDiscipline
                    nonInsArising = CommonConfig.ReminderInsArising
                    nonSign = CommonConfig.ReminderSign
                    nonBHYT = CommonConfig.ReminderBHYT
                    age18 = CommonConfig.ReminderAge18

                    authorityRemind = CommonConfig.ReminderExpAuthority
                    noticePersonalIncomeRemind = CommonConfig.ReminderNoticePersonalIncomeTax
                    stockRemind = CommonConfig.ReminderNoticeStock
                    approveHSNV = CommonConfig.ReminderApproveHSNV
                    approveWorkBefore = CommonConfig.ReminderApproveWorkBefore
                    approveFamily = CommonConfig.ReminderApproveFamily
                    approveCertificate = CommonConfig.ReminderApproveCertificate

                    RemindList = rep.GetRemind(probationRemind.ToString & "," &
                                               contractRemind.ToString & "," &
                                               birthdayRemind.ToString & "," &
                                               terminateRemind.ToString & "," &
                                               noPaperRemind.ToString & "," &
                                               approveRemind.ToString & "," &
                                               approveHDLDRemind.ToString & "," &
                                               approveTHHDRemind.ToString & "," &
                                               maternitiRemind.ToString & "," &
                                               retirementRemind.ToString & "," &
                                               noneSalaryRemind.ToString & "," &
                                               certificateRemind.ToString & "," &
                                               noneBIRTHDAY_LD.ToString & "," &
                                               noneConcurrently.ToString & "," &
                                               noneEmpDtlFamily.ToString & "," &
                                               nonOverRegDate.ToString & "," &
                                               nonExpDiscipline.ToString & "," &
                                               nonInsArising.ToString & "," &
                                               nonSign.ToString & "," &
                                               nonBHYT.ToString() & "," &
                                               age18.ToString() & "," &
                                               authorityRemind.ToString() & "," &
                                               noticePersonalIncomeRemind.ToString() & "," &
                                               stockRemind.ToString() & "," &
                                               approveHSNV.ToString() & "," &
                                               approveWorkBefore.ToString() & "," &
                                               approveFamily.ToString() & "," &
                                               approveCertificate.ToString()
                                               )

                End If
                Select Case data
                    Case 1
                        'Nhân viên sắp hết hạn hợp đồng
                        Dim listApproveDMVS = From p In RemindList Where p.REMIND_TYPE = 1
                        For Each item In listApproveDMVS
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&IDSelect=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 2
                        'Nhân viên sắp tới sinh nhật
                        Dim listApprove = From p In RemindList Where p.REMIND_TYPE = 2
                        For Each item In listApprove
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & item.EMPLOYEE_ID & "&state=Normal'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 3
                    Case 4
                    Case 5
                    Case 6
                    Case 7
                    Case 8
                    Case 9
                    Case 10
                        'Hết hạn Visa
                        Dim listProbation = From p In RemindList Where p.REMIND_TYPE = 10
                        For Each item In listProbation
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&empid=" & item.EMPLOYEE_ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 11
                    Case 12
                    Case 13
                        'Thay đổi thông tin nhân sự
                        Dim listApproveTHHD = From p In RemindList Where p.REMIND_TYPE = 23
                        For Each item In listApproveTHHD
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empid=" & item.EMPLOYEE_ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 14
                        'Nghỉ việc trong tháng
                        Dim listMaterniti = From p In RemindList Where p.REMIND_TYPE = 14
                        For Each item In listMaterniti
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteNewEdit&group=Business&empid=" & item.EMPLOYEE_ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 15
                    Case 16
                        'Nhân viên Chưa nộp đủ giấy tờ khi tiếp nhận
                        Dim listRetirement = From p In RemindList Where p.REMIND_TYPE = 16
                        For Each item In listRetirement
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & item.EMPLOYEE_ID & "&state=Normal'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 17
                    Case 18
                    Case 19
                        'Giấy phép lao động
                        Dim listEmpDtlFamily = From p In RemindList Where p.REMIND_TYPE = 19
                        For Each item In listEmpDtlFamily
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&empid=" & item.EMPLOYEE_ID & "&Place=ctrlHU_EmpDtlDeduct&state=Normal'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 20
                        'Nhân viên sắp hết hạn HĐ thử việc
                        Dim listEmpDtlFamily = From p In RemindList Where p.REMIND_TYPE = 20
                        For Each item In listEmpDtlFamily
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&IDSelect=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 21
                        'Nhân viên đến hạn bổ nhiệm
                        Dim listIdentify = From p In RemindList Where p.REMIND_TYPE = 21
                        For Each item In listIdentify
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&ID=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 22
                        'Nhân viên đến hạn ký lại HĐLĐ
                        Dim listPassport = From p In RemindList Where p.REMIND_TYPE = 22
                        For Each item In listPassport
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&ID=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 23
                        'Nhân viên hết hạn tạm hoãn HĐ
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 23
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&ID=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 24
                        'Nhân viên nghỉ thai sản đi làm lại
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 24
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Insurance&fid=ctrlInsMaternityDetail&group=Business&Status=1&IDSelect=" & item.ID & "'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 25
                        'Nhân viên đến tuổi nghỉ hưu
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 25
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & item.EMPLOYEE_ID & "state=Normal'  target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 26
                        'Nhân viên nghỉ không lương đi làm lại
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 26
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&VIEW=TRUE&FormType=0&ID=" & item.ID & "&periodid=91' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 27
                        'Nhân viên sắp hết hạn chứng chỉ
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 27
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & item.EMPLOYEE_ID & "&Place=ctrlHU_EmpDtlTrainingOutCompany&state=Normal' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 28
                    Case 29
                        'Hết hiệu lực kiêm nhiệm
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 29
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&Is_con=1&FormType=1&empID=" & item.EMPLOYEE_ID & "&IDSelect=" & item.ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 30
                        'Hết hạn giảm trừ gia cảnh
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 30
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & item.EMPLOYEE_ID & "&Place=ctrlHU_EmpDtlFamily&state=Normal' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 31
                        'Danh sách nhân viên nghỉ không lương
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 31
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Insurance&fid=ctrlInsArisingManual&group=Business&Status=0&EmployeeID=" & item.EMPLOYEE_ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 32
                    Case 33
                        'Danh sách nhân viên Hết thời hạn kỷ luật
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 33
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_DisciplineNewEdit&group=Business&noscroll=1&FormType=1&ID=" & item.ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 34
                        'Danh sách biến động bảo hiểm chưa được xử lý khai báo
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 34
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Insurance&fid=ctrlInsArising&group=Business' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 35
                        'Danh sách nhân viên bán hàng chưa ghi nhận ký quỹ
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 35
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='#' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 36
                        'Danh sách nhân viên chưa nhận thẻ BHYT
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 36
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Insurance&fid=ctrlInsInformations&group=Business&Status=1&IDSelect=" & item.ID & "&EmployeeID=" & item.EMPLOYEE_ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 37
                        'Danh sách người thân sắp 18 tuổi
                        Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 37
                        For Each item In listlicense
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='#' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 38
                    Case 39
                    Case 40
                        'Danh sách nhân viên đến hạn thanh toán cổ phiếu	
                        Dim listStock = From p In RemindList Where p.REMIND_TYPE = 40
                        For Each item In listStock
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_StocksTransactionNewEdit&group=Business&ID=" & item.EMPLOYEE_ID & "' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 41
                        'Phê duyệt HSNV	
                        Dim listApproveHSNV = From p In RemindList Where p.REMIND_TYPE = 41
                        For Each item In listApproveHSNV
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ApproveEmployee_Edit&group=Business' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 42
                        'Phê duyệt Kinh nghiệm làm việc
                        Dim listApproveWorkBefore = From p In RemindList Where p.REMIND_TYPE = 42
                        For Each item In listApproveWorkBefore
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ApproveWorkingBefore_Edit&group=Business' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 43
                        'Phê duyệt thông tin nhân thân
                        Dim listApproveFamily = From p In RemindList Where p.REMIND_TYPE = 43
                        For Each item In listApproveFamily
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ApproveFamily_Edit&group=Business' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                    Case 44
                        'Phê duyệt bằng cấp chứng chỉ
                        Dim listApproveCertificate = From p In RemindList Where p.REMIND_TYPE = 44
                        For Each item In listApproveCertificate
                            result &= "<div class='box25'>"
                            result &= "<i class='fa fa-exclamation-circle'></i><a href='/Default.aspx?mid=Profile&fid=ctrlHU_ApproveCertificateEmployee&group=Business' target='_blank'>" & item.EMPLOYEE_CODE & " - " & item.FULLNAME.Trim & "</a>"
                            result &= "</div>"
                        Next
                End Select
                Return result
            Catch ex As Exception

            End Try
        End Using

    End Function
End Class