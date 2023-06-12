Imports System.Globalization
Imports Common.CommonBusiness
Imports Framework.UI
Imports Mailer
Imports Telerik.Web.UI
Public Class Common

#Region "Configurate Cache Minute"

#End Region

#Region "Other configurate"

    Public Shared ReadOnly Property ShowMessageException As Boolean
        Get
            Return ConfigurationManager.AppSettings("ShowMessageException")
        End Get
    End Property
    Public Shared ReadOnly Property DefaultPageSize As Integer
        Get
            Return If(ConfigurationManager.AppSettings("DefaultPageSize") IsNot Nothing, ConfigurationManager.AppSettings("DefaultPageSize"), 20)
        End Get
    End Property
    Public Shared ReadOnly Property CacheMinusGetRemind As Integer
        Get
            Return If(ConfigurationManager.AppSettings("CacheMinusGetRemind") IsNot Nothing, ConfigurationManager.AppSettings("CacheMinusGetRemind"), 5)
        End Get
    End Property
    Public Shared ReadOnly Property CacheMinusMenu As Integer
        Get
            Return If(ConfigurationManager.AppSettings("CacheMinusMenu") IsNot Nothing, ConfigurationManager.AppSettings("CacheMinusMenu"), 60)
        End Get
    End Property
    Public Shared ReadOnly Property CacheConfigGrid As Integer
        Get
            Return If(ConfigurationManager.AppSettings("CacheConfigGrid") IsNot Nothing, ConfigurationManager.AppSettings("CacheConfigGrid"), 60)
        End Get
    End Property
#End Region

#Region "Session"
    Public Shared Property SystemLanguage() As CultureInfo
        Get
            If HttpContext.Current.Session("SystemLanguage") Is Nothing Then
                HttpContext.Current.Session("SystemLanguage") = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN")
            End If
            Return HttpContext.Current.Session("SystemLanguage")
        End Get
        Set(ByVal value As CultureInfo)
            HttpContext.Current.Session("SystemLanguage") = value
        End Set
    End Property

    Public Shared Property CheckUserAdmin()
        Get
            Return HttpContext.Current.Session("CheckUserAdmin")
        End Get
        Set(ByVal value)
            HttpContext.Current.Session.Add("CheckUserAdmin", value)
        End Set
    End Property

    Public Shared Property OrganizationOMLocationDataSession As List(Of OrganizationDTO)
        Get
            Return HttpContext.Current.Session("OrganizationOMLocationDataCache")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            HttpContext.Current.Session.Add("OrganizationOMLocationDataCache", value)
        End Set
    End Property
    Public Shared Property OrganizationLocationDataSession As List(Of OrganizationDTO)
        Get
            Return HttpContext.Current.Session("OrganizationLocationDataCache")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            HttpContext.Current.Session.Add("OrganizationLocationDataCache", value)
        End Set
    End Property

    Public Shared Property GetUserPermissionsDataSession() As List(Of PermissionDTO)
        Get
            Return HttpContext.Current.Session("UserPermissionsDataSession")
        End Get
        Set(ByVal value As List(Of PermissionDTO))
            HttpContext.Current.Session.Add("UserPermissionsDataSession", value)
        End Set
    End Property

    Public Shared Property DynamicReportDataSession() As DynamicReportDTO
        Get
            Return HttpContext.Current.Session("GetDynamicReportDataSession")
        End Get
        Set(ByVal value As DynamicReportDTO)
            HttpContext.Current.Session.Add("GetDynamicReportDataSession", value)
        End Set
    End Property

    Public Shared Property ReportDataSession() As ReportPreviewDTO

    Public Shared Property UserAccessMenuModuleDataSession() As String
        Get
            Return HttpContext.Current.Session("UserAccessMenuModuleDataSession")
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("UserAccessMenuModuleDataSession") = value
        End Set
    End Property

    Public Shared Property Reminder As String
        Get
            Return HttpContext.Current.Session("Reminder")
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("Reminder") = value
        End Set
    End Property

    'Public Shared Property Languages As Object
    '    Get
    '        Return HttpContext.Current.Session("Languages")
    '    End Get
    '    Set(ByVal value As Object)
    '        HttpContext.Current.Session("Languages") = value
    '    End Set
    'End Property

    'Public Shared Property ApprovePermiss As DataSet
    '    Get
    '        Return HttpContext.Current.Session("ApprovePermiss")
    '    End Get
    '    Set(ByVal value As DataSet)
    '        HttpContext.Current.Session("ApprovePermiss") = value
    '    End Set
    'End Property

#End Region

#Region "Cache"

    Public Shared Property FunctionListDataCache As List(Of FunctionDTO)
        Get
            Return HttpContext.Current.Session("FunctionListDataCache")
        End Get
        Set(ByVal value As List(Of FunctionDTO))
            HttpContext.Current.Session("FunctionListDataCache") = value
        End Set
    End Property

    Public Shared Property AccessLimit As String
        Get
            Return CacheManager.GetValue("SE_AccessLimit")
        End Get
        Set(ByVal value As String)
            CacheManager.Insert("SE_AccessLimit", value, 60 * 24)
        End Set
    End Property

#End Region

#Region "Toolbar"
    Public Shared Function CreateToolbarItem(ByVal _command As String,
                                             ByVal _icon As ToolbarIcons,
                                             Optional ByVal _authorize As ToolbarAuthorize = ToolbarAuthorize.None,
                                             Optional ByVal Name As String = "") As RadToolBarItem
        Using langMgr As New LanguageManager
            Try
                Dim newitem As RadToolBarButton
                If Name.Trim <> "" Then
                    newitem = New RadToolBarButton(Name)
                Else
                    newitem = New RadToolBarButton(langMgr.Translate(_command))
                End If
                newitem.CommandName = _command
                newitem.CausesValidation = False
                Dim strAuthorize As String = ""
                Select Case _authorize
                    Case ToolbarAuthorize.Create
                        strAuthorize = CommonMessage.AUTHORIZE_CREATE
                    Case ToolbarAuthorize.Modify
                        strAuthorize = CommonMessage.AUTHORIZE_MODIFY
                    Case ToolbarAuthorize.Delete
                        strAuthorize = CommonMessage.AUTHORIZE_DELETE
                        newitem.OuterCssClass = "RadToolbarDelete"
                    Case ToolbarAuthorize.Kill
                        'strAuthorize = CommonMessage.AUTHORIZE_KILL
                        newitem.OuterCssClass = "RadToolbarDelete"
                    Case ToolbarAuthorize.Print
                        strAuthorize = CommonMessage.AUTHORIZE_PRINT
                    Case ToolbarAuthorize.Import
                        strAuthorize = CommonMessage.AUTHORIZE_IMPORT
                    Case ToolbarAuthorize.Export
                        strAuthorize = CommonMessage.AUTHORIZE_EXPORT
                    Case ToolbarAuthorize.Special1
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL1
                    Case ToolbarAuthorize.Special2
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL2
                    Case ToolbarAuthorize.Special3
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL3
                    Case ToolbarAuthorize.Special4
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL4
                    Case ToolbarAuthorize.Special5
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL5
                    Case ToolbarAuthorize.Special5
                        strAuthorize = CommonMessage.AUTHORIZE_SPECIAL5
                    Case ToolbarAuthorize.Reset
                        strAuthorize = CommonMessage.AUTHORIZE_RESET
                End Select

                Dim strImageUrl As String = ""
                Select Case _icon
                    Case ToolbarIcons.Add
                        strImageUrl = "~/Static/Images/Toolbar/add.png"
                    Case ToolbarIcons.Calculator
                        strImageUrl = "~/Static/Images/Toolbar/calculator.png"
                    Case ToolbarIcons.Calender
                        strImageUrl = "~/Static/Images/Toolbar/calendar.png"
                    Case ToolbarIcons.Cancel
                        strImageUrl = "~/Static/Images/Toolbar/disk_blue_error.png"
                    Case ToolbarIcons.Checkmark
                        strImageUrl = ""
                    Case ToolbarIcons.Delete
                        strImageUrl = "~/Static/Images/Toolbar/delete.png"
                    Case ToolbarIcons.Kill
                        strImageUrl = "~/Static/Images/Toolbar/kill.png"
                    Case ToolbarIcons.Edit
                        strImageUrl = "~/Static/Images/Toolbar/edit.png"
                    Case ToolbarIcons.Export
                        strImageUrl = "~/Static/Images/Toolbar/export1.png"
                    Case ToolbarIcons.Find
                        strImageUrl = "~/Static/Images/Toolbar/refresh.png"
                    Case ToolbarIcons.Import
                        strImageUrl = "~/Static/Images/Toolbar/import1.png"
                    Case ToolbarIcons.Print
                        strImageUrl = "~/Static/Images/Toolbar/printer.png"
                    Case ToolbarIcons.Save
                        strImageUrl = "~/Static/Images/Toolbar/disk_blue.png"
                    Case ToolbarIcons.Active
                        strImageUrl = "~/Static/Images/Toolbar/lock_open.png"
                    Case ToolbarIcons.DeActive
                        strImageUrl = "~/Static/Images/Toolbar/lock.png"
                    Case ToolbarIcons.Approve
                        strImageUrl = "~/Static/Images/Toolbar/document_ok.png"
                    Case ToolbarIcons.Reject
                        strImageUrl = "~/Static/Images/Toolbar/document_error.png"
                    Case ToolbarIcons.Next
                        strImageUrl = "~/Static/Images/Toolbar/next.png"
                    Case ToolbarIcons.Previous
                        strImageUrl = "~/Static/Images/Toolbar/previous.png"
                    Case ToolbarIcons.Unlock
                        strImageUrl = "~/Static/Images/Toolbar/lock_open.png"
                    Case ToolbarIcons.Lock
                        strImageUrl = "~/Static/Images/Toolbar/lock.png"
                    Case ToolbarIcons.Refresh
                        strImageUrl = "~/Static/Images/Toolbar/refresh.png"
                    Case ToolbarIcons.Sync
                        strImageUrl = "~/Static/Images/Toolbar/refresh.png"
                    Case ToolbarIcons.Reset
                        strImageUrl = "~/Static/Images/Toolbar/reset.png"
                    Case ToolbarIcons.SendMail
                        strImageUrl = "~/Static/Images/Toolbar/send_email.png"
                    Case ToolbarIcons.Submit
                        strImageUrl = "~/Static/Images/Toolbar/next.png"
                    Case ToolbarIcons.Vacant
                        strImageUrl = "~/Static/Images/Toolbar/vacant.png"
                End Select
                newitem.ImageUrl = strImageUrl
                If strAuthorize.Trim <> "" Then
                    newitem.Attributes("Authorize") = strAuthorize
                End If
                Return newitem
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Shared Function CreateToolbarSeperator() As RadToolBarItem
        Dim newitem As New RadToolBarButton()
        newitem.IsSeparator = True
        Return newitem
    End Function
    ''' <summary>
    ''' Ham thuc hien xay dung list cac nut xu ly nhu: them, sua, xoa, huy, luu, export,... => giong nhu group button
    ''' </summary>
    ''' <param name="toolbar">ten control RadToolBar</param>
    ''' <param name="items">list cac nut duoc enable</param>
    ''' <remarks></remarks>
    Public Shared Sub BuildToolbar(ByVal toolbar As RadToolBar, ByVal ParamArray items() As ToolbarItem)
        Try
            toolbar.Items.Clear()
            Dim _hasAddEdit As Boolean = False
            For Each item In items
                Dim strCommand As String = ""
                Dim Authorize As ToolbarAuthorize = ToolbarAuthorize.None
                Dim icon As ToolbarIcons
                If item = ToolbarItem.Seperator Then
                    toolbar.Items.Add(CreateToolbarSeperator)
                Else
                    Select Case item
                        Case ToolbarItem.Search
                            strCommand = CommonMessage.TOOLBARITEM_SEARCH
                            icon = ToolbarIcons.Find
                        Case ToolbarItem.Create
                            strCommand = CommonMessage.TOOLBARITEM_CREATE
                            Authorize = ToolbarAuthorize.Create
                            icon = ToolbarIcons.Add
                            _hasAddEdit = True
                        Case ToolbarItem.Edit
                            strCommand = CommonMessage.TOOLBARITEM_EDIT
                            Authorize = ToolbarAuthorize.Modify
                            icon = ToolbarIcons.Edit
                            _hasAddEdit = True
                        Case ToolbarItem.Delete
                            strCommand = CommonMessage.TOOLBARITEM_DELETE
                            Authorize = ToolbarAuthorize.Delete
                            icon = ToolbarIcons.Delete
                        Case ToolbarItem.Kill
                            strCommand = CommonMessage.TOOLBARITEM_KILL
                            Authorize = ToolbarAuthorize.Kill
                            icon = ToolbarIcons.Kill
                        Case ToolbarItem.Save
                            strCommand = CommonMessage.TOOLBARITEM_SAVE
                            icon = ToolbarIcons.Save
                            Authorize = ToolbarAuthorize.Create
                        Case ToolbarItem.Cancel
                            strCommand = CommonMessage.TOOLBARITEM_CANCEL
                            icon = ToolbarIcons.Cancel
                        Case ToolbarItem.Print
                            strCommand = CommonMessage.TOOLBARITEM_PRINT
                            Authorize = ToolbarAuthorize.Print
                            icon = ToolbarIcons.Print
                        Case ToolbarItem.Import
                            strCommand = CommonMessage.TOOLBARITEM_IMPORT
                            Authorize = ToolbarAuthorize.Import
                            icon = ToolbarIcons.Import
                        Case ToolbarItem.Export
                            strCommand = CommonMessage.TOOLBARITEM_EXPORT
                            Authorize = ToolbarAuthorize.Export
                            icon = ToolbarIcons.Export
                        Case ToolbarItem.Calculate
                            strCommand = CommonMessage.TOOLBARTIEM_CALCULATE
                            Authorize = ToolbarAuthorize.Special1
                            icon = ToolbarIcons.Calculator
                        Case ToolbarItem.Approve
                            strCommand = CommonMessage.TOOLBARITEM_APPROVE
                            Authorize = ToolbarAuthorize.Special1
                            icon = ToolbarIcons.Approve
                        Case ToolbarItem.Reject
                            strCommand = CommonMessage.TOOLBARITEM_REJECT
                            Authorize = ToolbarAuthorize.Special1
                            icon = ToolbarIcons.Reject
                        Case ToolbarItem.Active
                            strCommand = CommonMessage.TOOLBARITEM_ACTIVE
                            Authorize = ToolbarAuthorize.Special2
                            icon = ToolbarIcons.Active
                        Case ToolbarItem.Deactive
                            strCommand = CommonMessage.TOOLBARITEM_DEACTIVE
                            Authorize = ToolbarAuthorize.Special2
                            icon = ToolbarIcons.DeActive
                        Case ToolbarItem.Next
                            strCommand = CommonMessage.TOOLBARITEM_NEXT
                            icon = ToolbarIcons.Next
                            Authorize = ToolbarAuthorize.Special1
                        Case ToolbarItem.Previous
                            strCommand = CommonMessage.TOOLBARITEM_PREVIOUS
                            icon = ToolbarIcons.Previous
                        Case ToolbarItem.Attach
                            strCommand = CommonMessage.TOOLBARITEM_ATTACH
                            Authorize = ToolbarAuthorize.Special3
                            icon = ToolbarIcons.Attach
                        Case ToolbarItem.Submit
                            strCommand = CommonMessage.TOOLBARITEM_SUBMIT
                            Authorize = ToolbarAuthorize.Special4
                            icon = ToolbarIcons.Submit
                        Case ToolbarItem.Refresh
                            strCommand = CommonMessage.TOOLBARITEM_REFRESH
                            icon = ToolbarIcons.Refresh
                            Authorize = ToolbarAuthorize.Special1
                        Case ToolbarItem.Unlock
                            strCommand = CommonMessage.TOOLBARITEM_UNLOCK
                            Authorize = ToolbarAuthorize.Special2
                            icon = ToolbarIcons.Unlock
                        Case ToolbarItem.Lock
                            strCommand = CommonMessage.TOOLBARITEM_LOCK
                            Authorize = ToolbarAuthorize.Special2
                            icon = ToolbarIcons.Lock
                        Case ToolbarItem.Sync
                            strCommand = CommonMessage.TOOLBARITEM_SYNC
                            Authorize = ToolbarAuthorize.Special5
                            icon = ToolbarIcons.Sync
                        Case ToolbarItem.Reset
                            strCommand = CommonMessage.TOOLBARITEM_RESET
                            Authorize = ToolbarAuthorize.Reset
                            icon = ToolbarIcons.Reset
                        Case ToolbarItem.SendMail
                            strCommand = CommonMessage.TOOLBARITEM_SENDMAIL
                            Authorize = ToolbarAuthorize.Special5
                            icon = ToolbarIcons.SendMail
                        Case ToolbarItem.HIExt
                            'ThanhNT added 12/05/2016
                            strCommand = CommonMessage.TOOLBARITEM_HIEXT
                            'Authorize = ToolbarAuthorize.HIExt
                            'icon = ToolbarIcons.Import
                        Case ToolbarItem.HIUpdateInfo
                            strCommand = CommonMessage.TOOLBARITEM_HIUPDATEINFO
                            'Authorize = ToolbarAuthorize.HIUpdateInfo
                            'icon = ToolbarIcons.Import
                        Case ToolbarItem.CreateBatch
                            strCommand = CommonMessage.TOOLBARITEM_CREATE_BATCH
                            Authorize = ToolbarAuthorize.Special5
                            icon = ToolbarIcons.Add
                        Case ToolbarItem.ExportTemplate 'ThanhNT added 04072016
                            strCommand = CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                            Authorize = ToolbarAuthorize.ExportTemplate
                            icon = ToolbarIcons.Export
                        Case ToolbarItem.ApproveBatch 'ThanhNT added 04072016
                            strCommand = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                            Authorize = ToolbarAuthorize.Special1
                            icon = ToolbarIcons.Approve
                        Case ToolbarItem.View 'ThanhNT added 04072016
                            strCommand = CommonMessage.TOOLBARITEM_VIEW
                            Authorize = ToolbarAuthorize.View
                            icon = ToolbarIcons.Approve
                        Case -99
                            strCommand = CommonMessage.TOOLBARITEM_RESET
                            Authorize = ToolbarAuthorize.Special5
                            icon = ToolbarIcons.Reset
                    End Select
                    toolbar.Items.Add(CreateToolbarItem(strCommand, icon, Authorize))
                End If
            Next
            If _hasAddEdit Then
                For i = 0 To toolbar.Items.Count - 1
                    If CType(toolbar.Items(i), RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_SAVE Or _
                       CType(toolbar.Items(i), RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_CANCEL Then
                        CType(toolbar.Items(i), RadToolBarButton).Enabled = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Custom"
    Public Shared Sub DisplayException(ByVal control As Control, ByVal ex As System.Exception, Optional ByVal DisplayText As String = "", Optional ByVal ExtraInfo As String = "")
        Using langMgr As New LanguageManager
            If Not ShowMessageException Then
                Utilities.ShowMessage(control, ex.Message, Utilities.NotifyType.Error)
            Else
                Utilities.DisplayException(control, ex, ExtraInfo)
            End If
        End Using

    End Sub
    Public Shared Function GetShortDatePattern() As String
        Try
            Return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region


    Public Shared Function IsAllow(ByVal ViewName As String) As Boolean
        Dim rep As New CommonRepository
        Dim user = LogHelper.CurrentUser
        Dim bAllow As Boolean = False
        'If Utilities.IsAuthenticated Then
        If LogHelper.CurrentUser IsNot Nothing Then
            Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(GetUsername)
            If Not GroupAdmin Then
                Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(GetUsername)
                If permissions IsNot Nothing Then
                    Dim ViewPermissions As List(Of PermissionDTO)
                    ViewPermissions = (From p In permissions Where p.FID = ViewName And p.IS_REPORT = False).ToList
                    If ViewPermissions IsNot Nothing AndAlso ViewPermissions.Count > 0 Then
                        Return True
                    End If
                End If
            Else
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function sendEmailByServerMail(ByVal strEmailReceiver As String,
                                                 ByVal strEmailCC As String,
                                                 ByVal strSubject As String,
                                                 ByVal strBody As String,
                                                 ByVal fileAttachments As String) As Boolean
        Try
            Dim strLog = String.Empty
            Using commonRep As New CommonRepository
                Dim arrAttachFile As ArrayList
                Dim config As Dictionary(Of String, String)
                Try
                    config = commonRep.GetConfig(0)
                    Dim _server = If(config.ContainsKey("MailServer"), config("MailServer"), "")
                    Dim _port = If(config.ContainsKey("MailPort"), config("MailPort"), "")
                    Dim _account = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
                    Dim _password = If(config.ContainsKey("MailAccountPassword"), config("MailAccountPassword"), "")
                    Dim _ssl = If(config.ContainsKey("MailIsSSL"), config("MailIsSSL"), 0)
                    Dim _authen = If(config.ContainsKey("MailIsAuthen"), config("MailIsAuthen"), 0)
                    Dim isEnableSSL As Boolean = False

                    If fileAttachments <> "" Then
                        arrAttachFile = New ArrayList
                        arrAttachFile.Add(fileAttachments)
                    End If
                    If CBool(_ssl) Then isEnableSSL = True Else isEnableSSL = False
                    clsSendMail20.Setup(_port, False, False, _server, isEnableSSL)
                    If _password <> "" Then
                        Using encry As New EncryptData()
                            _password = encry.DecryptString(_password)
                        End Using
                        strLog = clsSendMail20.SendMail_Err(_account, _password, strEmailReceiver, strEmailCC, "", strSubject, strBody, _server, arrAttachFile)
                    Else
                        strLog = clsSendMail.SendMail(_account, strEmailReceiver, strEmailCC, "", strSubject, strBody, _server, arrAttachFile)
                    End If
                    If strLog = "" Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                Finally
                    config = Nothing
                    arrAttachFile = Nothing
                End Try
            End Using

            '' Lấy thông tin config mail từ DB
            'CommonConfig.GetConfigFromDatabase()
            'Dim mail As New MailMessage()
            ''Dim SmtpServer As New SmtpClient(CommonConfig.MailServer)
            'Dim SmtpServer As New SmtpClient(CommonConfig.MailServer, CommonConfig.MailPort)
            'Dim EncryptData As New Framework.UI.EncryptData
            'Dim pass As String = EncryptData.DecryptString(CommonConfig.MailAccountPassword)
            'Dim NetworkCred As New NetworkCredential(CommonConfig.MailAccount, pass)
            'SmtpServer.EnableSsl = True
            'SmtpServer.UseDefaultCredentials = False
            'SmtpServer.Credentials = NetworkCred
            'SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network
            'mail.From = New MailAddress(CommonConfig.MailFrom)
            'mail.[To].Add(strEmailReceiver)
            'If strEmailCC <> String.Empty Then
            '    mail.CC.Add(strEmailCC)
            'End If
            'If fileAttachments <> String.Empty Then
            '    mail.Attachments.Add(New Attachment(fileAttachments))
            'End If
            'mail.Subject = strSubject
            'mail.Body = strBody
            'mail.BodyEncoding = System.Text.Encoding.UTF8
            'mail.SubjectEncoding = System.Text.Encoding.UTF8
            'mail.IsBodyHtml = True
            'SmtpServer.Send(mail)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function GetUsername() As String
        Try
            If LogHelper.CurrentUser IsNot Nothing Then
                Return LogHelper.CurrentUser.USERNAME.ToUpper
            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Public Shared Function sendEmailByServerMail(ByVal strEmailReceiver As String,
                                                 ByVal strEmailCC As String,
                                                 ByVal strSubject As String,
                                                 ByVal strBody As String,
                                                 ByVal fileAttachments As String,
                                                 ByVal view As String) As Boolean
        Try
            CommonConfig.GetConfigFromDatabase()
            Dim rep As New CommonRepository
            rep.InsertMail(CommonConfig.MailFrom,
                           strEmailReceiver, strSubject, strBody, strEmailCC, "", view)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class

Public Enum RemindConfigType
    Contract = 1
    Birthday = 2
    Email = 5
    UpSalaryBefore = 6
    EmployeeAppoint = 7
    UpSalaryAfter = 8
    ExpireDriverLicense = 9
    ExpireVisa = 10
    ExpireLabor = 11 ' hết hạn giấy phép lao động 
    ExpireAppoint = 12
    ExpireWorking = 13 ' hết hạn tờ trình
    ExpireTerminate = 14 ' Nghỉ việc
    ExpireTerminateDebt = 15 ' Nghỉ việc chưa bàn giao hết hoặc còn công nơ
    ExpireNoPaper = 16 ' chưa nộp đủ giấy tờ khi tiếp nhận NV mới
    ExpireCertificate = 19 'hết hạn chứng chỉ lao động 
    Probation = 20 'het han hd thu viec
    Approve = 21 ' Quyết định bổ nhiệm
    ApproveHDLD = 22 ' Quyết định ký hợp đồng lao động
    ApprovetTHHD = 23 ' Quyết định tạm hoãn hợp đồng
    Materniti = 24 ' nghỉ thai sản đi làm lại
    Retirement = 25 ' Nhân viên đến tuổi nghỉ hưu
    NoneSalary = 26 '  Nhân viên nghỉ không lương đi làm lại
    ExpiredCertificate = 27 ' Nhân viên sắp hết hạn chứng chỉ
    BIRTHDAY_LD = 28 ' Sắp đến sinh nhật lãnh đạo
    Concurrently = 29 ' Nhân viên sắp hết hạn kiêm nhiêm
    EmpDtlFamily = 30 ' Nhân viên có người thân sắp hết giảm trừ gia cảnh
    OverRegDate = 31 'nhan vien dang ki nghi khong luong >=14 ngay

    ExpContrat = 32 'het han hop dong
    ExpDiscipline = 33 'het han ky luat
    InsArising = 34 'Danh sách biến động bảo hiểm chưa được xử lý khai báo
    Sign = 35 'Danh sách nhân viên bán hàng chưa ghi nhận ký quỹ
    BHYT = 36 'Danh sách nhân viên chưa nhận thẻ BHYT
    Age18 = 37 'Danh sách người thân sắp 18 tuổi
    ExpAuthority = 38 'Danh sách nhân viên Ủy quyền sắp hết hạn
    NoticePersonalIncomeTax = 39 'Thông báo quyết toán thuế thu nhập cá nhân
    NoticeStock = 40 'Thông báo đến hạn thanh toán cổ phiếu
    ApproveHSNV = 41 'Phê duyệt HSNV
    ApproveWorkBefore = 42 'Phê duyệt Kinh nghiệm làm việc
    ApproveFamily = 43 'Phê duyệt thông tin nhân thân
    ApproveCertificate = 44 'Phê duyệt bằng cấp chứng chỉ
End Enum

Public Enum ToolbarItem
    Search
    Create
    Edit
    Delete
    Save
    Cancel
    Print
    Import
    Export
    Calculate
    Approve
    Reject
    Active
    Deactive
    Seperator
    [Next]
    Previous
    Submit
    Attach
    Refresh
    Sync
    Unlock
    Lock
    Reset
    SendMail
    HIExt
    HIUpdateInfo
    CreateBatch
    ExportTemplate
    ApproveBatch
    View
    Kill
End Enum

Public Enum ToolbarIcons
    Add
    Calculator
    Calender
    Cancel
    Checkmark
    Delete
    Save
    Edit
    Find
    Print
    Import
    Export
    Active
    DeActive
    Approve
    Reject
    [Next]
    Previous
    Submit
    Attach
    Refresh
    Sync
    Unlock
    Lock
    Reset
    SendMail
    Kill
    Vacant
End Enum

Public Enum ToolbarAuthorize
    Create
    Modify
    Delete
    Print
    Import
    Export
    Special1
    Special2
    Special3
    Special4
    Special5
    None
    Reset
    ExportTemplate
    ApproveBatch
    View
    Kill
End Enum

Public Enum LoginError
    LDAP_SERVER_NOT_FOUND
    WRONG_USERNAME_OR_PASSWORD
    USERNAME_NOT_EXISTS
    WRONG_PASSWORD
    USERNAME_EXPIRED
    NO_PERMISSION
    USER_LOCKED
    USER_NOT_EMPLOYEE
    USER_NOT_ORG
    EMPLOYEE_WORK_STATUS
    LOGINED
End Enum