Imports System.Xml
Imports Common
Imports Framework.UI
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class Site
    Inherits System.Web.UI.MasterPage
    Protected Property DatabaseName As String
    Protected Property Username As String
    Dim bAdmin As Boolean
    Dim lstFuncId As New List(Of String)

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
    Dim noticeStock As Integer
    Dim approveHSNV As Integer
    Dim approveWorkBefore As Integer
    Dim approveFamily As Integer
    Dim approveCertificate As Integer
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = Me.GetType().Name.ToString()

    Public Property RemindList As List(Of ReminderLogDTO)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginStatus As LoginStatus = HeadLoginStatus
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                Session.Abandon()
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
                Exit Sub
            End If
            'PhongDV
            If loginStatus IsNot Nothing Then
                AddHandler loginStatus.LoggingOut, AddressOf LoggingOut
            End If
            'End PhongDV

            Using rep As New Common.CommonRepository
                Username = Common.Common.GetUsername.Trim
                If Username = "" Then Exit Sub
                'PhongDV
                If LogHelper.CurrentUser IsNot Nothing AndAlso LogHelper.CurrentUser.EMPLOYEE_ID IsNot Nothing Then
                    lblUserName.Text = LogHelper.CurrentUser.FULLNAME
                    lblMNV.Text = "MNV: " + LogHelper.CurrentUser.EMPLOYEE_CODE

                    Dim lastname As String
                    Dim result As String() = LogHelper.CurrentUser.FULLNAME.Split(" ")
                    For Each s As String In result
                        lastname = s
                    Next
                    lblUserName2.Text = lastname
                    lblEmail.Text = LogHelper.CurrentUser.EMAIL
                    Try
                        Dim repP As New ProfileBusinessRepository
                        Dim sError As String = ""

                        If LogHelper.CurrentUser.EMPLOYEE_CODE <> "" Then
                            If LogHelper.ImageUser Is Nothing Then
                                LogHelper.ImageUser = repP.GetEmployeeImage(LogHelper.CurrentUser.EMPLOYEE_ID, sError)
                            End If
                            rbiEmployeeImage.DataValue = LogHelper.ImageUser
                            rbiEmployeeImage1.DataValue = LogHelper.ImageUser
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If
                'End PhongDV
                'Kiểm tra phân quyền Menu
                Dim lstFunc = rep.GetUserPermissions(Common.Common.GetUsername)
                bAdmin = rep.CheckGroupAdmin(Common.Common.GetUsername)
                lstFuncId = (From p In lstFunc Select p.FID).ToList
            End Using
            If Not IsPostBack Then
                LoadConfig()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String) As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Using langMgr As New LanguageManager
            Try
                Return langMgr.Translate(str, args)
                _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End Using

        Return str
    End Function


    Public Sub LoggingOut(ByVal sender As Object, ByVal e As LoginCancelEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                LogHelper.SaveAccessLog(Session.SessionID, "Logout")
                LogHelper.OnlineUsers.Remove(Session.SessionID)
                Session.Abandon()
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Utilities.ShowMessage(Me.Page, ex.Message, Utilities.NotifyType.Error)
        End Try

    End Sub
    ' CreatBy: nhungdt
    ' CreateDate: 22/06/2017
    ' LastUpdate: 23/06/2017
    ' Name: BuildSubMenu
    ' Description: Xây dựng danh sách cây thư mục con của menu
    ' Input:
    ' Output: String
    Protected Function BuildSubMenu() As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim mid As String = Request.Params("mid")
            Dim xmlFile As String = String.Format(Utilities.ModulePath & "/{0}/Menu-" & Common.Common.SystemLanguage.Name & ".xml", mid)
            Return BuildMenu(xmlFile)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try
    End Function

    Protected Function BuildMainMenu() As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'CacheManager.Insert("MainMenu" + Common.Common.GetUsername.ToString() + Common.Common.SystemLanguage.Name, "", Common.Common.CacheMinusGetRemind)
            'If CacheManager.GetValue("MainMenu" + Common.Common.GetUsername.ToString() + Common.Common.SystemLanguage.Name) IsNot Nothing Then
            '    Return CacheManager.GetValue("MainMenu" + Common.Common.GetUsername.ToString() + Common.Common.SystemLanguage.Name).ToString()
            'End If
            Dim startTime As DateTime = DateTime.UtcNow
            If Common.Common.GetUsername = "" Then Return "<li><a class=""fNiv"" href='#' onclick=""mnuHelpClick()""><span>Trợ giúp</span> </a></li>"
            Dim mid As String = Request.Params("mid")
            Dim xmlFile As String = String.Format(Utilities.ModulePath & "/Menu-" & Common.Common.SystemLanguage.Name & ".xml")
            ' Nếu tồn tại file có tên xmlFile thì hàm trả về giá trị rỗng
            If Not IO.File.Exists(Server.MapPath(xmlFile)) Then
                Return ""
            End If
            Dim rep As New CommonRepository
            Dim stringAccessModule As String = rep.GetUserAccessMenuModule(Common.Common.GetUsername)
            Dim stringReturn As String = ""
            Dim stringTemp As String = ""
            Dim stringModule As String = ""
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(Server.MapPath(xmlFile))
            Dim xElement As XmlElement = xmlDoc.DocumentElement.FirstChild
            Dim curView = Request.Params("fid").ToString
            Dim curMid = rep.GetCurMIDByFID(curView)

            Dim xChildElement As XmlElement = xElement.ChildNodes(0)
            stringModule = xChildElement.GetAttribute("Text")
            For j As Integer = 0 To xChildElement.FirstChild.ChildNodes.Count - 1
                Dim xChild As XmlElement = xChildElement.FirstChild.ChildNodes(j)
                If stringAccessModule = "*" OrElse xChild.GetAttribute("Value").ToUpper = "HELP" OrElse
                     xChild.GetAttribute("Value").ToUpper = "HOME" OrElse
                    stringAccessModule.ToUpper.Contains(xChild.GetAttribute("Value").ToUpper) Then
                    stringReturn &= String.Format("<li class='menuItem'><a class='menuLink" _
                                                  & If(xChild.GetAttribute("Value").ToUpper.Equals(curMid.ToUpper), " menu-selected-item ", "") & " ' {2} {1} {3}>{0}</a>",
                                                    xChild.Attributes("Text").Value,
                                                    If(xChild.HasAttribute("NavigateUrl"),
                                                        String.Format("href=""{0}""", xChild.Attributes("NavigateUrl").Value),
                                                        "href=""#"""),
                                                    If(xChild.GetAttribute("Value").ToString() = mid, "class='selected'", ""),
                         If(xChild.HasAttribute("OnClick"), String.Format("OnClick='{0}'", xChild.Attributes("OnClick").Value), "")
                                                    )
                    Dim xmlFileSub As String = String.Format(Utilities.ModulePath & "/{0}/Menu-" & Common.Common.SystemLanguage.Name & ".xml", xChild.GetAttribute("Value"))
                    stringReturn &= "<ul>" & BuildMenu(xmlFileSub) & "</ul>"
                    stringReturn &= "</li>"
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            CacheManager.Insert("MainMenu" + Common.Common.GetUsername.ToString() + Common.Common.SystemLanguage.Name, stringReturn, Common.Common.CacheMinusMenu)
            Return stringReturn
        Catch ex As Exception
            _mylog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

    Protected Function BuildMenu(ByVal xmlFile As String) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IO.File.Exists(Server.MapPath(xmlFile)) Then
                Return ""
            End If
            If Common.Common.GetUsername = "" Then Return ""
            Dim bCheckPermission As Boolean
            Dim stringReturn As String = ""

            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(Server.MapPath(xmlFile))
            Dim xElement As XmlElement = xmlDoc.DocumentElement.FirstChild

            For i As Integer = 0 To xElement.ChildNodes.Count - 1
                bCheckPermission = True
                Dim xChildElement As XmlElement = xElement.ChildNodes(i)
                If Not bAdmin Then
                    If xChildElement.HasAttribute("CheckPermission") AndAlso xChildElement.HasAttribute("Value") Then
                        If xChildElement.GetAttribute("CheckPermission").ToUpper = "TRUE" Then
                            If lstFuncId.Contains(xChildElement.GetAttribute("Value")) Then
                                bCheckPermission = True
                            Else
                                bCheckPermission = False
                            End If
                        End If
                    End If
                End If
                Dim childString As String
                If bCheckPermission Then
                    If xChildElement.HasChildNodes Then
                        childString = getChildElement(xChildElement)
                        If childString <> "" Then
                            stringReturn &= String.Format("<li><a {2} href=""{1}"" >{0}</a>",
                                                          xChildElement.GetAttribute("Text"),
                                                          If(xChildElement.HasAttribute("NavigateUrl"),
                                                             String.Format("href=""{0}""", xChildElement.GetAttribute("NavigateUrl")), ""),
                                                          If(xChildElement.HasChildNodes, "class='hsub'", ""))
                            If xChildElement.HasChildNodes Then
                                stringReturn &= childString
                            End If
                            stringReturn &= "</li>"
                        End If
                    Else
                        stringReturn &= String.Format("<li><a href=""{1}"" {2}>{0}</a>",
                                                          xChildElement.GetAttribute("Text"),
                                                          If(xChildElement.HasAttribute("NavigateUrl"),
                                                              xChildElement.GetAttribute("NavigateUrl"), ""),
                                                          If(xChildElement.HasAttribute("OnClick"), String.Format("OnClick='{0}'", xChildElement.Attributes("OnClick").Value), ""))
                        stringReturn &= "</li>"
                    End If
                End If

            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return stringReturn
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

    Function getChildElement(ByVal xElement As XmlElement) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            xElement = xElement.FirstChild
            Dim stringReturn As String = "<ul>"
            Dim bCheckPermission As Boolean
            Dim childString As String

            For i As Integer = 0 To xElement.ChildNodes.Count - 1
                bCheckPermission = True
                Dim xChildElement As XmlElement = xElement.ChildNodes(i)

                If xChildElement.HasAttribute("IsSeparator") Then
                    stringReturn &= "<li class=""separator""></li>"

                    Continue For
                End If
                If Not bAdmin Then
                    If xChildElement.HasAttribute("CheckPermission") AndAlso xChildElement.HasAttribute("Value") Then
                        If xChildElement.GetAttribute("CheckPermission").ToUpper = "TRUE" Then
                            If lstFuncId.Contains(xChildElement.GetAttribute("Value")) Then
                                bCheckPermission = True
                            Else
                                bCheckPermission = False
                            End If
                        End If
                    End If
                End If
                If bCheckPermission Then

                    If xChildElement.HasChildNodes Then
                        childString = getChildElement(xChildElement)
                        If childString <> "" Then
                            stringReturn &= String.Format("<li><a class='hsub' {1}>{2}{0}</a>",
                                                          xChildElement.Attributes("Text").Value, _
                                                          If(xChildElement.HasAttribute("NavigateUrl"), _
                                                             String.Format("href=""{0}""", xChildElement.Attributes("NavigateUrl").Value), _
                                                             "href=""#"""), _
                                                          If(xChildElement.HasAttribute("ImageUrl"), _
                                                             String.Format("<img src=""{0}"" />", xChildElement.Attributes("ImageUrl").Value), _
                                                             ""))
                            stringReturn &= childString & "</li>"
                        End If
                    Else
                        stringReturn &= String.Format("<li><a {1}>{2}{0}</a></li>",
                                                      xChildElement.Attributes("Text").Value, _
                                                      If(xChildElement.HasAttribute("NavigateUrl"), _
                                                         String.Format("href=""{0}""", xChildElement.Attributes("NavigateUrl").Value), _
                                                         "href=""#"""), _
                                                      If(xChildElement.HasAttribute("ImageUrl"), _
                                                         String.Format("<img src=""{0}"" />", xChildElement.Attributes("ImageUrl").Value), _
                                                         "") _
                                                     )
                    End If
                End If
            Next

            stringReturn &= vbNewLine & "</ul>"
            If stringReturn = "<ul>" & vbNewLine & "</ul>" Then
                stringReturn = ""
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return stringReturn
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

#Region "Notify"
    'Public Sub Load_Noty()
    '    Dim db As New AttendanceRepository
    '    Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
    '        .EmployeeIdName = String.Empty,
    '        .FromDate = Date.Now.FirstDateOfMonth(),
    '        .ToDate = Date.Now.LastDateOfMonth(),
    '        .Status = 0
    '    }
    '    GetDataNotify()
    '    'Dim listApprove = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_LEAVE, filter)
    '    'listApprove = listApprove.Where(Function(p) p.STATUS = 1).ToList()
    '    'ltrTime_LEAVE.DataSource = listApprove
    '    'ltrTime_LEAVE.DataBind()

    '    'Dim listApproveDMVS = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_WLEO, filter)
    '    'listApproveDMVS = listApproveDMVS.Where(Function(p) p.STATUS = 1).ToList()
    '    'ltrWLEO.DataSource = listApproveDMVS
    '    'ltrWLEO.DataBind()

    '    'ltrNumberRocord.Text = listApprove.Count + listApproveDMVS.Count
    'End Sub

    'Private Sub btnNotiTimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotiTimer.Click
    '    'Load_Noty()
    'End Sub

    Private Sub LoadConfig()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            noticeStock = CommonConfig.ReminderNoticeStock
            approveHSNV = CommonConfig.ReminderApproveHSNV
            approveWorkBefore = CommonConfig.ReminderApproveWorkBefore
            approveCertificate = CommonConfig.ReminderApproveCertificate
            approveFamily = CommonConfig.ReminderApproveFamily
            If approveRemind <> 0 Then
                dApproveRemind.Visible = True
            Else
                dApproveRemind.Visible = False
            End If

            If approveHDLDRemind <> 0 Then
                dApproveHDLDRemind.Visible = True
            Else
                dApproveHDLDRemind.Visible = False
            End If
            If approveTHHDRemind <> 0 Then
                dApproveTHHDRemind.Visible = True
            Else
                dApproveTHHDRemind.Visible = False
            End If
            If maternitiRemind <> 0 Then
                dMaternitiRemind.Visible = True
            Else
                dMaternitiRemind.Visible = False
            End If
            If retirementRemind <> 0 Then
                dRetirementRemind.Visible = True
            Else
                dRetirementRemind.Visible = False
            End If
            If noneSalaryRemind <> 0 Then
                dNoneSalaryRemind.Visible = True
            Else
                dNoneSalaryRemind.Visible = False
            End If
            'If noneBIRTHDAY_LD <> 0 Then
            '    dBIRTHDAY_LD.Visible = True
            'Else
            '    dBIRTHDAY_LD.Visible = False
            'End If
            If noneConcurrently <> 0 Then
                dConcurrently.Visible = True
            Else
                dConcurrently.Visible = False
            End If
            If noneEmpDtlFamily <> 0 Then
                dEmpDtlFamily.Visible = True
            Else
                dEmpDtlFamily.Visible = False
            End If
            If nonOverRegDate <> 0 Then
                dOverRegDate.Visible = True
            Else
                dOverRegDate.Visible = False
            End If
            If noneEmpDtlFamily <> 0 Then
                dEmpDtlFamily.Visible = True
            Else
                dEmpDtlFamily.Visible = False
            End If
            ''====

            If nonSign <> 0 Then
                dSign.Visible = True
            Else
                dSign.Visible = False
            End If
            If nonBHYT <> 0 Then
                dBHYT.Visible = True
            Else
                dBHYT.Visible = False
            End If
            If nonExpDiscipline <> 0 Then
                dExpDiscipline.Visible = True
            Else
                dExpDiscipline.Visible = False
            End If
            If nonInsArising <> 0 Then
                dInsArising.Visible = True
            Else
                dInsArising.Visible = False
            End If
            ''====


            If probationRemind <> 0 Then
                dProbationRemind.Visible = True
            Else
                dProbationRemind.Visible = False
            End If

            If terminateDebtRemind <> 0 Then
                dExpireTerminateDebt.Visible = True
            Else
                dExpireTerminateDebt.Visible = False
            End If

            If contractRemind <> 0 Then
                dcontractRemind.Visible = True
            Else
                dcontractRemind.Visible = False
            End If

            If birthdayRemind <> 0 Then
                dbirthdayRemind.Visible = True
            Else
                dbirthdayRemind.Visible = False
            End If

            If terminateRemind <> 0 Then
                dterminateRemind.Visible = True
            Else
                dterminateRemind.Visible = False
            End If

            If noPaperRemind <> 0 Then
                dnoPaperRemind.Visible = True
            Else
                dnoPaperRemind.Visible = False
            End If

            If age18 <> 0 Then
                dnoAge18.Visible = True
            Else
                dnoAge18.Visible = False
            End If
            'If authorityRemind <> 0 Then
            '    dauthority.Visible = True
            'Else
            '    dauthority.Visible = False
            'End If
            'If noticePersonalIncomeRemind <> 0 Then
            '    dnoticePersonalIncome.Visible = True
            'Else
            '    dnoticePersonalIncome.Visible = False
            'End If
            If visaRemind <> 0 Then
                dvisaRemind.Visible = True
            Else
                dvisaRemind.Visible = False
            End If

            If noticeStock <> 0 Then
                DivNoticeStock.Visible = True
            Else
                DivNoticeStock.Visible = False
            End If
            If workingRemind <> 0 Then
                dvisaRemind1.Visible = True
            Else
                dvisaRemind1.Visible = False
            End If
            If certificateRemind <> 0 Then
                dworPermitRemind.Visible = True
            Else
                dworPermitRemind.Visible = False
            End If

            If approveHSNV <> 0 Then
                dApproveHSNV.Visible = True
            Else
                dApproveHSNV.Visible = False
            End If

            If approveWorkBefore <> 0 Then
                dApproveWorkBefore.Visible = True
            Else
                dApproveWorkBefore.Visible = False
            End If

            If approveCertificate <> 0 Then
                dApproveCertificate.Visible = True
            Else
                dApproveCertificate.Visible = False
            End If

            If approveFamily <> 0 Then
                dApproveFamily.Visible = True
            Else
                dApproveFamily.Visible = False
            End If
            'If certificateRemind <> 0 Then
            '    dcertificateRemind.Visible = True
            'Else
            '    dcertificateRemind.Visible = False
            'End If
            ''dcertificateRemind.Visible = False
            'dworkingRemind1.Visible = False

            GetDataNotify()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub GetDataNotify()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim reps As New ProfileRepository
        Dim _filter = New ReminderListDTO
        Using rep As New ProfileDashboardRepository
            Try
                If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                    'RemindList = rep.GetRemind(contractRemind.ToString & "," & _
                    '                           birthdayRemind.ToString & "," & _
                    '                           visaRemind.ToString & "," & _
                    '                           workingRemind.ToString & "," & _
                    '                           terminateRemind.ToString & "," & _
                    '                           terminateDebtRemind.ToString & "," & _
                    '                           noPaperRemind.ToString & "," & _
                    '                           "0," & _
                    '                           "0," & _
                    '                           worPermitRemind.ToString & "," & _
                    '                           certificateRemind.ToString
                    '                           )

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
                                               noticeStock.ToString() & "," &
                                               approveHSNV.ToString() & "," &
                                               approveWorkBefore.ToString() & "," &
                                               approveFamily.ToString() & "," &
                                               approveCertificate.ToString()
                                               )
                    'For Each item In RemindList
                    '    item.REMIND_NAME = Translate(item.REMIND_NAME)
                    'Next
                End If
                Dim lstReminder = reps.GetReminderList(_filter)

                ltrNumberRocord.Text = "0"
                ltrNumberRocordApproveProfile.Text = "0"
                'Nhân viên đến hạn bổ nhiệm lại chức vụ
                Dim listApproveNVBN = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Approve
                'ltrApprove.DataSource = listApproveNVBN
                'ltrApprove.DataBind()
                lblApprove.Text = Utilities.ObjToInt(listApproveNVBN.Count)
                If approveRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listApproveNVBN.Count
                    Dim Approve = lstReminder.Where(Function(f) f.ID = RemindConfigType.Approve).FirstOrDefault
                    If Approve IsNot Nothing Then
                        lb21.Text = Approve.REMINDER_NAME
                    End If
                End If

                'Nhân viên đến hạn ký lại HDLD
                Dim listApproveHDLD = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApproveHDLD
                'ltrApproveHDLD.DataSource = listApproveHDLD
                'ltrApproveHDLD.DataBind()
                lblApproveHDLD.Text = Utilities.ObjToInt(listApproveHDLD.Count)
                If approveHDLDRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listApproveHDLD.Count
                    Dim ApproveHDLD = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApproveHDLD).FirstOrDefault
                    If ApproveHDLD IsNot Nothing Then
                        lb22.Text = ApproveHDLD.REMINDER_NAME
                    End If
                End If

                'Nhân viên hết hạn tạm hoãn HD
                Dim listApproveTHHD = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApprovetTHHD
                'ltrApproveTHHD.DataSource = listApproveTHHD
                'ltrApproveTHHD.DataBind()
                lblApproveTHHD.Text = Utilities.ObjToInt(listApproveTHHD.Count)
                If approveTHHDRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listApproveTHHD.Count
                    Dim ApprovetTHHD = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApprovetTHHD).FirstOrDefault
                    If ApprovetTHHD IsNot Nothing Then
                        lb23.Text = ApprovetTHHD.REMINDER_NAME
                    End If
                End If

                'Nhân viên nghỉ thai sản đi làm lại
                Dim listMaterniti = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Materniti
                'ltrMaterniti.DataSource = listMaterniti
                'ltrMaterniti.DataBind()
                lblMaterniti.Text = Utilities.ObjToInt(listMaterniti.Count)
                If maternitiRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listMaterniti.Count
                    Dim Materniti = lstReminder.Where(Function(f) f.ID = RemindConfigType.Materniti).FirstOrDefault
                    If Materniti IsNot Nothing Then
                        lb24.Text = Materniti.REMINDER_NAME
                    End If
                End If

                'Nhân viên đến tuổi nghỉ hưu
                Dim listRetirement = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Retirement
                'ltrRetirement.DataSource = listRetirement
                'ltrRetirement.DataBind()
                lblRetirement.Text = Utilities.ObjToInt(listRetirement.Count)
                If retirementRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listRetirement.Count
                    Dim Retirement = lstReminder.Where(Function(f) f.ID = RemindConfigType.Retirement).FirstOrDefault
                    If Retirement IsNot Nothing Then
                        lb25.Text = Retirement.REMINDER_NAME
                    End If
                End If

                'Nhân viên nghỉ không lương đi làm lại
                Dim listNoneSalary = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.NoneSalary
                'ltrNoneSalary.DataSource = listNoneSalary
                'ltrNoneSalary.DataBind()
                lblNoneSalary.Text = Utilities.ObjToInt(listNoneSalary.Count)
                If noneSalaryRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listNoneSalary.Count
                    Dim NoneSalary = lstReminder.Where(Function(f) f.ID = RemindConfigType.NoneSalary).FirstOrDefault
                    If NoneSalary IsNot Nothing Then
                        lb26.Text = NoneSalary.REMINDER_NAME
                    End If
                End If

                'Sinh nhật lãnh đạo
                'Dim listBIRTHDAY_LD = From p In RemindList Where p.REMIND_TYPE = 28
                'ltrBIRTHDAY_LD.DataSource = listBIRTHDAY_LD
                'ltrBIRTHDAY_LD.DataBind()
                'lbBIRTHDAY_LD.Text = Utilities.ObjToInt(listBIRTHDAY_LD.Count)

                'Hết hiệu lực kiêm nhiệm
                Dim listConcurrently = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Concurrently
                'ltrConcurrently.DataSource = listConcurrently
                'ltrConcurrently.DataBind()
                lbConcurrently.Text = Utilities.ObjToInt(listConcurrently.Count)
                If noneConcurrently <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listConcurrently.Count
                    Dim Concurrently = lstReminder.Where(Function(f) f.ID = RemindConfigType.Concurrently).FirstOrDefault
                    If Concurrently IsNot Nothing Then
                        lb29.Text = Concurrently.REMINDER_NAME
                    End If
                End If

                'Hết hạn giảm trừ gia cảnh
                Dim listEmpDtlFamily = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.EmpDtlFamily
                'ltrEmpDtlFamily.DataSource = listEmpDtlFamily
                'ltrEmpDtlFamily.DataBind()
                lbEmpDtlFamily.Text = Utilities.ObjToInt(listEmpDtlFamily.Count)
                If noneEmpDtlFamily <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listEmpDtlFamily.Count
                    Dim EmpDtlFamily = lstReminder.Where(Function(f) f.ID = RemindConfigType.EmpDtlFamily).FirstOrDefault
                    If EmpDtlFamily IsNot Nothing Then
                        lb30.Text = EmpDtlFamily.REMINDER_NAME
                    End If
                End If

                'nhan vien dang ky nghi khong luong >= 14 ngay
                Dim listOverRegDate = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.OverRegDate
                'ltrOverRegDate.DataSource = listOverRegDate
                'ltrOverRegDate.DataBind()
                lbOverRegDate.Text = Utilities.ObjToInt(listOverRegDate.Count)
                If nonOverRegDate <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listOverRegDate.Count
                    Dim OverRegDate = lstReminder.Where(Function(f) f.ID = RemindConfigType.OverRegDate).FirstOrDefault
                    If OverRegDate IsNot Nothing Then
                        lb31.Text = OverRegDate.REMINDER_NAME
                    End If
                End If

                'het han ky luat
                Dim listExpDiscipline = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpDiscipline
                'ltrExpDiscipline.DataSource = listExpDiscipline
                'ltrExpDiscipline.DataBind()
                lbExpDiscipline.Text = Utilities.ObjToInt(listExpDiscipline.Count)
                If nonExpDiscipline <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listExpDiscipline.Count
                    Dim ExpDiscipline = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpDiscipline).FirstOrDefault
                    If ExpDiscipline IsNot Nothing Then
                        lb33.Text = ExpDiscipline.REMINDER_NAME
                    End If
                End If

                'Danh sách biến động bảo hiểm chưa được xử lý khai báo
                Dim listInsArising = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.InsArising
                'ltrInsArising.DataSource = listInsArising
                'ltrInsArising.DataBind()
                lbInsArising.Text = Utilities.ObjToInt(listInsArising.Count)
                If nonInsArising <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listInsArising.Count
                    Dim InsArising = lstReminder.Where(Function(f) f.ID = RemindConfigType.InsArising).FirstOrDefault
                    If InsArising IsNot Nothing Then
                        lb34.Text = InsArising.REMINDER_NAME
                    End If
                End If

                'Danh sách nhân viên bán hàng chưa ghi nhận ký quỹ
                Dim listSign = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Sign
                'ltrSign.DataSource = listSign
                'ltrSign.DataBind()
                lbSign.Text = Utilities.ObjToInt(listSign.Count)
                If nonSign <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listSign.Count
                    Dim Sign = lstReminder.Where(Function(f) f.ID = RemindConfigType.Sign).FirstOrDefault
                    If Sign IsNot Nothing Then
                        lb35.Text = Sign.REMINDER_NAME
                    End If
                End If

                'Danh sách nhân viên chưa nhận thẻ BHYT
                Dim listBHYT = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.BHYT
                'ltrBHYT.DataSource = listBHYT
                'ltrBHYT.DataBind()
                lbBHYT.Text = Utilities.ObjToInt(listBHYT.Count)
                If nonBHYT <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listBHYT.Count
                    Dim BHYT = lstReminder.Where(Function(f) f.ID = RemindConfigType.BHYT).FirstOrDefault
                    If BHYT IsNot Nothing Then
                        lb36.Text = BHYT.REMINDER_NAME
                    End If
                End If

                'Nhân viên sắp hết hạn hợp đồng
                Dim listApproveDMVS = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Contract
                'ltrWLEO.DataSource = listApproveDMVS
                'ltrWLEO.DataBind()
                lblWLEO.Text = Utilities.ObjToInt(listApproveDMVS.Count)
                If contractRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listApproveDMVS.Count
                    Dim Contract = lstReminder.Where(Function(f) f.ID = RemindConfigType.Contract).FirstOrDefault
                    If Contract IsNot Nothing Then
                        lb1.Text = Contract.REMINDER_NAME
                    End If
                End If

                'Nhân viên sắp tới sinh nhật
                Dim listApprove = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Birthday
                'ltrTime_LEAVE.DataSource = listApprove
                'ltrTime_LEAVE.DataBind()
                lblTime_LEAVE.Text = Utilities.ObjToInt(listApprove.Count)
                If birthdayRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listApprove.Count
                    Dim Birthday = lstReminder.Where(Function(f) f.ID = RemindConfigType.Birthday).FirstOrDefault
                    If Birthday IsNot Nothing Then
                        lb2.Text = Birthday.REMINDER_NAME
                    End If
                End If

                'Chưa nộp đủ giấy tờ khi tiếp nhận
                Dim listGiayTo = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireNoPaper
                'ltrTime_GiayTo.DataSource = listGiayTo
                'ltrTime_GiayTo.DataBind()
                lblTime_GiayTo.Text = Utilities.ObjToInt(listGiayTo.Count)
                If noPaperRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listGiayTo.Count
                    Dim ExpireNoPaper = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireNoPaper).FirstOrDefault
                    If ExpireNoPaper IsNot Nothing Then
                        lb16.Text = ExpireNoPaper.REMINDER_NAME
                    End If
                End If

                'Hết hạn Visa
                Dim listVisa = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireVisa
                'ltrTime_Visa.DataSource = listVisa
                'ltrTime_Visa.DataBind()
                lblTime_Visa.Text = Utilities.ObjToInt(listVisa.Count)
                If visaRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listVisa.Count
                    Dim ExpireVisa = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireVisa).FirstOrDefault
                    If ExpireVisa IsNot Nothing Then
                        lb10.Text = ExpireVisa.REMINDER_NAME
                    End If
                End If

                'Het han to trinh - Thay đổi thông tin nhân sự
                Dim listToTrinh = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireWorking
                'ltrTime_ToTrinh.DataSource = listToTrinh
                'ltrTime_ToTrinh.DataBind()
                lblTime_ToTrinh.Text = Utilities.ObjToInt(listToTrinh.Count)
                If workingRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listToTrinh.Count
                    Dim ExpireWorking = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireWorking).FirstOrDefault
                    If ExpireWorking IsNot Nothing Then
                        lb13.Text = ExpireWorking.REMINDER_NAME
                    End If
                End If

                'Tờ trình chưa phê duyệt
                'Dim listToTrinhPheDuyet = From p In RemindList Where p.REMIND_TYPE = 13 And p.REMIND_NAME = "Tờ trình chưa phê duyệt"
                'ltrTime_ToTrinhPheDuyet.DataSource = listToTrinhPheDuyet
                'ltrTime_ToTrinhPheDuyet.DataBind()
                'lblTime_ToTrinhPheDuyet.Text = Utilities.ObjToInt(listToTrinhPheDuyet.Count)
                'Giấy phép lao động
                Dim listGPLD = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireCertificate
                'ltrTime_GiayPhepLaoDong.DataSource = listGPLD
                'ltrTime_GiayPhepLaoDong.DataBind()
                lblTime_GiayPhepLaoDong.Text = Utilities.ObjToInt(listGPLD.Count)
                If certificateRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listGPLD.Count
                    Dim ExpireCertificate = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireCertificate).FirstOrDefault
                    If ExpireCertificate IsNot Nothing Then
                        lb19.Text = ExpireCertificate.REMINDER_NAME
                    End If
                End If

                'Chung chi lao dong
                'Dim listChungChi = From p In RemindList Where p.REMIND_TYPE = 20
                'ltrTime_ChungChi.DataSource = listChungChi
                'ltrTime_ChungChi.DataBind()
                'lblTime_ChungChi.Text = Utilities.ObjToInt(listChungChi.Count)
                'nghi viec trong thang
                Dim listNghiViec = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireTerminate
                'ltrTime_NghiViec.DataSource = listNghiViec
                'ltrTime_NghiViec.DataBind()
                lblTime_NghiViec.Text = Utilities.ObjToInt(listNghiViec.Count)
                If terminateRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listNghiViec.Count
                    Dim ExpireTerminate = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireTerminate).FirstOrDefault
                    If ExpireTerminate IsNot Nothing Then
                        lb14.Text = ExpireTerminate.REMINDER_NAME
                    End If
                End If

                'het han thu viec
                Dim listProbation = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Probation
                'ltrProbation.DataSource = listProbation
                'ltrProbation.DataBind()
                lblProbation.Text = Utilities.ObjToInt(listProbation.Count)
                If probationRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listProbation.Count
                    Dim Probation = lstReminder.Where(Function(f) f.ID = RemindConfigType.Probation).FirstOrDefault
                    If Probation IsNot Nothing Then
                        lb20.Text = Probation.REMINDER_NAME
                    End If
                End If

                'nv nghi viec chua ban giao hoac thieu cong no
                Dim listExpireTerminateDebt = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ExpireTerminateDebt
                lbExpireTerminateDebt.Text = Utilities.ObjToInt(listProbation.Count)
                If terminateDebtRemind <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + listExpireTerminateDebt.Count
                    Dim ExpireTerminateDebt = lstReminder.Where(Function(f) f.ID = RemindConfigType.ExpireTerminateDebt).FirstOrDefault
                    If ExpireTerminateDebt IsNot Nothing Then
                        lb15.Text = ExpireTerminateDebt.REMINDER_NAME
                    End If
                End If

                'Danh sách người thân sắp 18 tuổi
                Dim lstAge18 = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.Age18
                'listAge18.DataSource = lstAge18
                'listAge18.DataBind()
                lbAge18.Text = Utilities.ObjToInt(lstAge18.Count)
                If age18 <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + lstAge18.Count
                    Dim oAge18 = lstReminder.Where(Function(f) f.ID = RemindConfigType.Age18).FirstOrDefault
                    If oAge18 IsNot Nothing Then
                        lb37.Text = oAge18.REMINDER_NAME
                    End If
                End If

                'Danh sách Nhân viên đến hạn thanh toán cổ phiếu
                Dim lstNoticeStock = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.NoticeStock
                lbNoticeStock.Text = Utilities.ObjToInt(lstNoticeStock.Count)
                If noticeStock <> 0 Then
                    ltrNumberRocord.Text = CDec(ltrNumberRocord.Text) + lstNoticeStock.Count
                    Dim oNoticeStock = lstReminder.Where(Function(f) f.ID = RemindConfigType.NoticeStock).FirstOrDefault
                    If oNoticeStock IsNot Nothing Then
                        lb40.Text = oNoticeStock.REMINDER_NAME
                    End If
                End If

                'Phê duyệt HSNV
                Dim lstApproveHSNV = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApproveHSNV
                lbApproveHSNV.Text = Utilities.ObjToInt(lstApproveHSNV.Count)
                If approveHSNV <> 0 Then
                    ltrNumberRocordApproveProfile.Text = CDec(ltrNumberRocordApproveProfile.Text) + lstApproveHSNV.Count
                    Dim oApproveHSNV = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApproveHSNV).FirstOrDefault
                    If oApproveHSNV IsNot Nothing Then
                        lb41.Text = oApproveHSNV.REMINDER_NAME
                    End If
                End If

                'Phê duyệt Kinh nghiệm làm việc
                Dim lstApproveWorkBefore = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApproveWorkBefore
                lbApproveWorkBefore.Text = Utilities.ObjToInt(lstApproveWorkBefore.Count)
                If approveWorkBefore <> 0 Then
                    ltrNumberRocordApproveProfile.Text = CDec(ltrNumberRocordApproveProfile.Text) + lstApproveWorkBefore.Count
                    Dim oApproveWorkBefore = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApproveWorkBefore).FirstOrDefault
                    If oApproveWorkBefore IsNot Nothing Then
                        lb42.Text = oApproveWorkBefore.REMINDER_NAME
                    End If
                End If

                'Phê duyệt thông tin nhân thân
                Dim lstApproveFamily = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApproveFamily
                lbApproveFamily.Text = Utilities.ObjToInt(lstApproveFamily.Count)
                If approveFamily <> 0 Then
                    ltrNumberRocordApproveProfile.Text = CDec(ltrNumberRocordApproveProfile.Text) + lstApproveFamily.Count
                    Dim oApproveFamily = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApproveFamily).FirstOrDefault
                    If oApproveFamily IsNot Nothing Then
                        lb43.Text = oApproveFamily.REMINDER_NAME
                    End If
                End If

                'Phê duyệt bằng cấp chứng chỉ
                Dim lstApproveCertificate = From p In RemindList Where p.REMIND_TYPE = RemindConfigType.ApproveCertificate
                lbApproveCertificate.Text = Utilities.ObjToInt(lstApproveCertificate.Count)
                If approveCertificate <> 0 Then
                    ltrNumberRocordApproveProfile.Text = CDec(ltrNumberRocordApproveProfile.Text) + lstApproveCertificate.Count
                    Dim oApproveCertificate = lstReminder.Where(Function(f) f.ID = RemindConfigType.ApproveCertificate).FirstOrDefault
                    If oApproveCertificate IsNot Nothing Then
                        lb44.Text = oApproveCertificate.REMINDER_NAME
                    End If
                End If

                _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End Using
        reps.Dispose()
    End Sub


#End Region

End Class