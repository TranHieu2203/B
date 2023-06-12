Imports System.Xml
Imports Attendance
Imports Common
Imports Framework.UI
Imports Profile
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class Site
    Inherits System.Web.UI.MasterPage
    Private WithEvents btnVN As RadButton
    Private WithEvents btnEN As RadButton
    Protected Property Username As String
    Public Property EmployeeID As Decimal
    Public ReadOnly Property ProcessApprove As String
        Get
            Return ATConstant.GSIGNCODE_WLEO
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginStatus As LoginStatus = HeadLoginStatus
        If loginStatus IsNot Nothing Then
            AddHandler loginStatus.LoggingOut, AddressOf LoggingOut
            'Dim Pepository As New ProfileRepository
            'Dim lst = Pepository.GetOtherList("SYSTEM_LANGUAGE")
            'rcboLanguage.DataTextField = "NAME"
            'rcboLanguage.DataValueField = "CODE"
            'rcboLanguage.DataSource = lst
            'rcboLanguage.DataBind()
            'If rcboLanguage.Items.Count > 0 Then
            '    If (Common.Common.SystemLanguage.Name <> rcboLanguage.SelectedValue) And (rcboLanguage.SelectedValue <> "") Then
            '        Common.Common.SystemLanguage = System.Globalization.CultureInfo.CreateSpecificCulture(rcboLanguage.SelectedValue)
            '        rcboLanguage.SelectedValue = Common.Common.SystemLanguage.Name
            '    End If
            '    If rcboLanguage.SelectedValue = "" Then
            '        rcboLanguage.SelectedValue = Common.Common.SystemLanguage.Name
            '    End If
            'End If
        End If

        Dim rep As New Common.CommonRepository
        Username = Common.Common.GetUsername.Trim
        If Username = "" Then Exit Sub
        'Kiểm tra phân quyền Menu
        Dim lstFunc = rep.GetUserPermissions(Common.Common.GetUsername)
        If LogHelper.CurrentUser IsNot Nothing AndAlso _
            LogHelper.CurrentUser.EMPLOYEE_ID IsNot Nothing Then

            lblUserName.Text = LogHelper.CurrentUser.FULLNAME
            lblMNV.Text = "MNV: " + LogHelper.CurrentUser.EMPLOYEE_CODE
            lblUserName2.Text = LogHelper.CurrentUser.FULLNAME
            lblEmail.Text = LogHelper.CurrentUser.EMAIL
            Try
                Dim repP As New ProfileBusinessRepository
                Dim sError As String = ""
                'Cookie avatar
                'Dim obj = repP.GetEmployeeCV(LogHelper.CurrentUser.EMPLOYEE_ID)
                'Dim fileDirectory = ""
                'Dim filepath = ""
                'fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImage"
                'fileDirectory = fileDirectory.Replace("HistaffWebApp", "HistaffServiceHost")
                'If obj.IMAGE IsNot Nothing And obj.IMAGE <> "" Then
                '    filepath = fileDirectory & "\" & obj.IMAGE
                '    Dim cookie As HttpCookie = New HttpCookie("useravatar")
                '    If File.Exists(filepath) = True Then
                '        Dim dir As String
                '        Dim pathImg As String
                '        dir = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImage"
                '        If (Directory.Exists(dir) = False) Then
                '            Directory.CreateDirectory(dir)
                '        End If
                '        pathImg = dir + "\" + obj.IMAGE
                '        If File.Exists(pathImg) = False Then
                '            My.Computer.FileSystem.CopyFile(filepath, pathImg)
                '        End If

                '        cookie.Value = "../EmployeeImage/" + obj.IMAGE
                '    Else
                '        cookie.Value = "../Static/Images/user_login.png"
                '    End If
                '    cookie.Expires = Now.AddDays(7)
                '    Response.Cookies.Add(cookie)
                'End If

                If LogHelper.CurrentUser.EMPLOYEE_CODE <> "" Then

                    If LogHelper.ImageUser Is Nothing Then
                        LogHelper.ImageUser = repP.GetEmployeeImage(LogHelper.CurrentUser.EMPLOYEE_ID, sError)
                    End If

                    rbiEmployeeImage.DataValue = LogHelper.ImageUser
                    rbiEmployeeImage1.DataValue = LogHelper.ImageUser
                End If
                Load_Noty()
                Load_Info_Support()
                Dim reps As New CommonRepository
                Dim strImg = rep.GET_SE_BACKGROUND_PORTAL()
                Dim background As String = "background-image:url('Static/Images/" + strImg + "');"
                'Dim background As String = " background-image:url('Static/Images/background.png');"
                divContent.Attributes.Add("style", background)
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub
    Public Sub Load_Noty()
        Dim db As New AttendanceRepository
        Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
            .EmployeeIdName = String.Empty,
            .FromDate = Date.Now.FirstDateOfMonth(),
            .ToDate = Date.Now.LastDateOfMonth(),
            .Status = 0
        }
        Dim rep As New AttendanceRepository
        Dim _filter As New Attendance.AttendanceBusiness.AT_PORTAL_REG_DTO
        _filter.ID_EMPLOYEE = LogHelper.CurrentUser.EMPLOYEE_ID
        _filter.STATUS = 0
        Dim listApprove = rep.PRS_GETLEAVE_BY_APPROVE1(_filter).ToList(Of Attendance.AttendanceBusiness.AT_PORTAL_REG_DTO)
        'Dim listApprove = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_LEAVE, filter)
        'listApprove = listApprove.Where(Function(p) p.STATUS = 1).ToList()
        ltrTime_LEAVE.DataSource = listApprove
        ltrTime_LEAVE.DataBind()

        'Dim listApproveOV = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_OVERTIME, filter)
        'listApproveOV = listApproveOV.Where(Function(p) p.STATUS = 1).ToList()
        'ltrTime_OverTime.DataSource = listApproveOV
        'ltrTime_OverTime.DataBind()

        'Dim listApproveDMVS = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_WLEO, filter)
        'listApproveDMVS = listApproveDMVS.Where(Function(p) p.STATUS = 1).ToList()
        'ltrWLEO.DataSource = listApproveDMVS
        'ltrWLEO.DataBind()

        'Dim listAssess As DataTable = db.GET_PE_ASSESS_MESS(LogHelper.CurrentUser.EMPLOYEE_ID)
        'ltrASSESS.DataSource = listAssess
        'ltrASSESS.DataBind()


        Dim listRC = rep.PRS_Get_TREvaluateCourse_Nofi(_filter).ToList(Of Attendance.AttendanceBusiness.AT_PORTAL_REG_DTO)

        lsTrEvaluateCourse.DataSource = listRC
        lsTrEvaluateCourse.DataBind()

        ltrNumberRocord.Text = listApprove.Count + listRC.Count
    End Sub

    Public Sub Load_Info_Support()
        Dim rep As New ProfileRepository
        Dim lstOrganization As List(Of OrganizationDTO)
        lstOrganization = rep.GetOrganization()
        Dim lst = (From p In lstOrganization Where p.PARENT_ID Is Nothing).FirstOrDefault
        lblSupport.Text = lst.REMARK
    End Sub
    Private Sub btnNotiTimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotiTimer.Click
        Load_Noty()
    End Sub
    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String) As String
        Using langMgr As New LanguageManager
            Try
                Return langMgr.Translate(str, args)
            Catch ex As Exception
                langMgr.Dispose()
            End Try
            Return str
        End Using

    End Function


    Public Sub LoggingOut(ByVal sender As Object, ByVal e As LoginCancelEventArgs)
        Try

            If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                Dim SystemLanguage = Common.Common.SystemLanguage
                LogHelper.OnlineUsers.Remove(Session.SessionID)
                LogHelper.SaveAccessLog(Session.SessionID, "Logout")
                Session.Clear()
                Session.Abandon()
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
                Common.Common.SystemLanguage = SystemLanguage
            End If
        Catch ex As Exception
            Utilities.ShowMessage(Me.Page, ex.Message, Utilities.NotifyType.Error)
        End Try

    End Sub


    Protected Sub btnLang_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVN.Click, btnEN.Click
        Common.Common.SystemLanguage = System.Globalization.CultureInfo.CreateSpecificCulture(sender.Value)
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

#Region "PhongDV"
    'Protected Sub rcboLanguage_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcboLanguage.SelectedIndexChanged
    '    Common.Common.SystemLanguage = System.Globalization.CultureInfo.CreateSpecificCulture(e.Value)
    '    Response.Redirect(Request.Url.AbsoluteUri)
    'End Sub

    Protected Function BuildMainMenu() As String
        If Common.Common.GetUsername = "" Then Return "<li><a class=""fNiv"" href='#' onclick=""mnuHelpClick()""><span>Trợ giúp</span> </a></li>"
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim xmlFile As String = String.Format("Static/PanelBar_" & Common.Common.SystemLanguage.Name & ".xml")

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
        '''''''''''''''''''''''''''''''''''''''
        Try
            Try
                If LogHelper.CurrentUser IsNot Nothing Then
                    Dim repCommon As New CommonRepository
                    Dim lstPermission = rep.GetUserPermissions(Common.Common.GetUsername)
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)

                    For j As Integer = 0 To xmlDoc.DocumentElement.ChildNodes.Count - 1
                        Dim xChild As XmlElement = xmlDoc.DocumentElement.ChildNodes(j)
                        Dim lstValue() As String
                        ' TH1: nếu là admin --> chỉ có quyền ở phần quản trị hệ thống ( trong trường ADMIN không được gán cho bất kỳ nhân viên nào )
                        ' TH2: nếu là user --> cho vào tất cả các form có attribute  trạng thái "NoPer" + các form có trạng thái "Per" khi có quyền
                        If xChild.HasAttribute("Value") Then
                            lstValue = xChild.GetAttribute("Value").Split(";")
                            ''''''''''''''''''''''''''''''''
                            Dim subMenu As String = ""
                            If (xChild.HasChildNodes) Then
                                subMenu &= "<ul>"
                                For k As Integer = 0 To xChild.ChildNodes.Count - 1
                                    Dim xChildElement As XmlElement = xChild.ChildNodes(k)
                                    Dim lstValueOfElement = xChildElement.GetAttribute("Value").Split(";")
                                    'Nếu là admin
                                    If GroupAdmin OrElse (LogHelper.CurrentUser.EMPLOYEE_CODE = "" Or LogHelper.CurrentUser.EMPLOYEE_CODE Is Nothing) Then
                                        'If lstValue(0).ToUpper = "PORTAL" Or lstValue(0).ToUpper = "DASHBOARD" Then
                                        If lstValueOfElement(3) = "Edit" Then
                                            subMenu &= BulidSubMenuBold(xChildElement, lstValueOfElement)
                                        Else
                                            subMenu &= BulidSubMenu(xChildElement, lstValueOfElement)
                                        End If
                                        'End If
                                        'Nếu ko phải admin
                                    ElseIf GroupAdmin = False OrElse (GroupAdmin And LogHelper.CurrentUser.EMPLOYEE_CODE <> "") Then
                                        If lstValueOfElement(2) = "NoPer" Then
                                            If lstValueOfElement(3) = "Edit" Then
                                                subMenu &= BulidSubMenuBold(xChildElement, lstValueOfElement)
                                            Else
                                                subMenu &= BulidSubMenu(xChildElement, lstValueOfElement)
                                            End If
                                        ElseIf lstValueOfElement(2) = "Per" Then
                                            Dim query = (From p In lstPermission Where p.FID = lstValueOfElement(1)).Count
                                            If query <> 0 Then
                                                If lstValueOfElement(3) = "Edit" Then
                                                    subMenu &= BulidSubMenuBold(xChildElement, lstValueOfElement)
                                                Else
                                                    subMenu &= BulidSubMenu(xChildElement, lstValueOfElement)
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                                subMenu &= "</ul>"
                            End If
                            'If stringAccessModule = "*" OrElse stringAccessModule.ToUpper.Contains(lstValue(0)) Then
                            If subMenu <> "<ul></ul>" Then
                                If LogHelper.CurrentUser.EMPLOYEE_CODE = "" Or LogHelper.CurrentUser.EMPLOYEE_CODE Is Nothing Then
                                    If lstValue(0).ToUpper = "PORTAL" Or lstValue(0).ToUpper = "DASHBOARD" Then
                                        stringReturn &= String.Format("<li><a {2} {1}>{0}</a>",
                                                                xChild.Attributes("Text").Value, _
                                                                If(lstValue.Count > 1, String.Format("href='Default.aspx?mid={0}&fid={1}'", lstValue(0), lstValue(1)), "href='#'"),
                                                                If(xChild.GetAttribute("Value").ToString().Contains(mid), "class='selected'", "")
                                                                )
                                    End If
                                Else
                                    stringReturn &= String.Format("<li><a {2} {1}>{0}</a>",
                                                                xChild.Attributes("Text").Value, _
                                                                If(lstValue.Count > 1, String.Format("href='Default.aspx?mid={0}&fid={1}'", lstValue(0), lstValue(1)), "href='#'"),
                                                                If(xChild.GetAttribute("Value").ToString().Contains(mid), "class='selected'", "")
                                                                )
                                End If
                                stringReturn &= subMenu
                                stringReturn &= "</li>"
                                'End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            'DisplayException(Me.Title, "", ex)
        End Try
        Return stringReturn
    End Function

    Function BulidSubMenu(ByVal xChildElement As XmlElement, ByVal lstValueOfElement() As String) As String
        Return String.Format("<li><a {2} href=""{1}"" >{0}</a>",
                    xChildElement.GetAttribute("Text"),
                    If(xChildElement.HasAttribute("Value"),
                        String.Format("Default.aspx?mid={0}&fid={1}", lstValueOfElement(0), lstValueOfElement(1)), ""),
                    If(xChildElement.HasChildNodes, "class='hsub'", ""))
    End Function

    Function BulidSubMenuBold(ByVal xChildElement As XmlElement, ByVal lstValueOfElement() As String) As String
        Return String.Format("<li><b><a {2} href=""{1}"" >{0}</a></b>",
                    xChildElement.GetAttribute("Text"),
                    If(xChildElement.HasAttribute("Value"),
                        String.Format("Default.aspx?mid={0}&fid={1}", lstValueOfElement(0), lstValueOfElement(1)), ""),
                    If(xChildElement.HasChildNodes, "class='hsub'", ""))
    End Function
#End Region
End Class