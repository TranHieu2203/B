Imports System.Web.Configuration
Imports Common.CommonBusiness

Public Class CommonConfig
    Private Shared Property AppConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")

    Public Shared ReadOnly Property CurrentUser As String
        Get
            Return LogHelper.CurrentUser.USERNAME
        End Get
    End Property

    Public Shared Property ModuleID As SystemConfigModuleID = SystemConfigModuleID.All

#Region "iSecure"
    'anhvn, dung session luu gia tri ban dau, khong load lai du lieu trong khi luu (chi su dung cho nut luu man hinh thiet lap)
    Public Shared ReadOnly Property dicConfig_save As Dictionary(Of String, String)
        Get
            If HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) = rep.GetConfig(ModuleID)
                End Using
            End If
            Return HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID)
        End Get
    End Property
    Public Shared ReadOnly Property dicConfig As Dictionary(Of String, String)
        Get

            Dim config As Dictionary(Of String, String)
            Using rep As New CommonRepository
                config = rep.GetConfig(ModuleID)
            End Using
            Return config
        End Get
    End Property
    Public Shared ReadOnly Property ListLDAP As List(Of LdapDTO)
        Get
            If HttpContext.Current.Session("ListLDAPCache") Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ListLDAPCache") = rep.GetLdap(New LdapDTO)
                End Using
            End If
            Return HttpContext.Current.Session("ListLDAPCache")
        End Get
    End Property

    Public Shared Property SessionTimeout() As Integer
        Get
            Dim web As SessionStateSection
            web = CommonConfig.AppConfig.GetSection("system.web/sessionState")
            Return web.Timeout.TotalMinutes
        End Get
        Set(ByVal value As Integer)
            Dim web As SessionStateSection
            web = CommonConfig.AppConfig.GetSection("system.web/sessionState")
            web.Timeout = New System.TimeSpan(Int(value / 60), value Mod 60, 0)

            'Lưu Timeout cho Form Authentication.
            Dim auth As AuthenticationSection
            auth = CommonConfig.AppConfig.GetSection("system.web/authentication")
            auth.Forms.Timeout = New System.TimeSpan(Int((value * 2) / 60), (value * 2) Mod 60, 0)
        End Set
    End Property

    Public Shared Property SessionWarning() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SessionWarning") Then
                Return 5
            End If
            Return AppConfig.AppSettings.Settings("SessionWarning").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SessionWarning") Then
                AppConfig.AppSettings.Settings("SessionWarning").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SessionWarning", value)
            End If
        End Set
    End Property

    Public Shared Property ActiveTimeout() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("ActiveTimeout") Then
                Return 1
            End If
            Return AppConfig.AppSettings.Settings("ActiveTimeout").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("ActiveTimeout") Then
                AppConfig.AppSettings.Settings("ActiveTimeout").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("ActiveTimeout", value)
            End If
        End Set

        'Get
        '    If Not dicConfig.ContainsKey("ActiveTimeout") Then
        '        Return 2
        '    End If
        '    Return dicConfig("ActiveTimeout")
        'End Get
        'Set(ByVal value As Integer)
        '    If dicConfig.ContainsKey("ActiveTimeout") Then
        '        dicConfig("ActiveTimeout") = value
        '    Else
        '        dicConfig.Add("ActiveTimeout", value)
        '    End If
        'End Set
    End Property

    Public Shared Property MailIsSSL() As Integer
        Get
            If Not dicConfig.ContainsKey("MailIsSSL") Then
                Return 0
            End If
            If dicConfig("MailIsSSL") = "" Then
                Return 0
            End If
            Return dicConfig("MailIsSSL")
        End Get
        Set(ByVal value As Integer)
            If dicConfig.ContainsKey("MailIsSSL") Then
                dicConfig("MailIsSSL") = value
            Else
                dicConfig.Add("MailIsSSL", value)
            End If
        End Set
    End Property

    Public Shared Property MailIsAuthen() As Integer
        Get
            If Not dicConfig.ContainsKey("MailIsAuthen") Then
                Return 0
            End If
            If dicConfig("MailIsAuthen") = "" Then
                Return 0
            End If
            Return dicConfig("MailIsAuthen")
        End Get
        Set(ByVal value As Integer)
            If dicConfig.ContainsKey("MailIsAuthen") Then
                dicConfig("MailIsAuthen") = value
            Else
                dicConfig.Add("MailIsAuthen", value)
            End If
        End Set
    End Property

    Public Shared Property MailFrom() As String
        Get
            If Not dicConfig.ContainsKey("MailFrom") Then
                Return ""
            End If
            Return dicConfig("MailFrom")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailFrom") Then
                dicConfig("MailFrom") = value
            Else
                dicConfig.Add("MailFrom", value)
            End If
        End Set
    End Property

    Public Shared Property MailServer() As String
        Get
            If Not dicConfig.ContainsKey("MailServer") Then
                Return ""
            End If
            Return dicConfig("MailServer")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailServer") Then
                dicConfig("MailServer") = value
            Else
                dicConfig.Add("MailServer", value)
            End If
        End Set
    End Property

    Public Shared Property MailPort() As String
        Get
            If Not dicConfig.ContainsKey("MailPort") Then
                Return ""
            End If
            Return dicConfig("MailPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailPort") Then
                dicConfig("MailPort") = value
            Else
                dicConfig.Add("MailPort", value)
            End If
        End Set
    End Property

    Public Shared Property MailAccount() As String
        Get
            If Not dicConfig.ContainsKey("MailAccount") Then
                Return ""
            End If
            Return dicConfig("MailAccount")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailAccount") Then
                dicConfig("MailAccount") = value
            Else
                dicConfig.Add("MailAccount", value)
            End If
        End Set
    End Property

    Public Shared Property MailAccountPassword() As String
        Get
            If Not dicConfig.ContainsKey("MailAccountPassword") Then
                Return ""
            End If
            Return dicConfig("MailAccountPassword")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailAccountPassword") Then
                dicConfig("MailAccountPassword") = value
            Else
                dicConfig.Add("MailAccountPassword", value)
            End If
        End Set
    End Property

    Public Shared Property PortalPort() As String
        Get
            If Not dicConfig.ContainsKey("PortalPort") Then
                Return ""
            End If
            Return dicConfig("PortalPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("PortalPort") Then
                dicConfig("PortalPort") = value
            Else
                dicConfig.Add("PortalPort", value)
            End If
        End Set
    End Property

    Public Shared Property AppPort() As String
        Get
            If Not dicConfig.ContainsKey("AppPort") Then
                Return ""
            End If
            Return dicConfig("AppPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AppPort") Then
                dicConfig("AppPort") = value
            Else
                dicConfig.Add("AppPort", value)
            End If
        End Set
    End Property

    Public Shared Property SendMailPasswordSubject() As String
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordSubject") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("SendMailPasswordSubject").Value
        End Get
        Set(ByVal value As String)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordSubject") Then
                AppConfig.AppSettings.Settings("SendMailPasswordSubject").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SendMailPasswordSubject", value)
            End If
        End Set
    End Property

    Public Shared Property SendMailPasswordContent() As String
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordContent") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("SendMailPasswordContent").Value
        End Get
        Set(ByVal value As String)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordContent") Then
                AppConfig.AppSettings.Settings("SendMailPasswordContent").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SendMailPasswordContent", value)
            End If
        End Set
    End Property

    Public Shared Property MaxNumberLoginFail() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("MaxNumberLoginFail") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("MaxNumberLoginFail").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("MaxNumberLoginFail") Then
                AppConfig.AppSettings.Settings("MaxNumberLoginFail").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("MaxNumberLoginFail", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordLength() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLength") Then
                Return 4
            End If
            Return AppConfig.AppSettings.Settings("PasswordLength").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLength") Then
                AppConfig.AppSettings.Settings("PasswordLength").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordLength", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordUpperChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordUpperChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordUpperChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordUpperChar") Then
                AppConfig.AppSettings.Settings("PasswordUpperChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordUpperChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordLowerChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLowerChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordLowerChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLowerChar") Then
                AppConfig.AppSettings.Settings("PasswordLowerChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordLowerChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNumberChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNumberChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordNumberChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNumberChar") Then
                AppConfig.AppSettings.Settings("PasswordNumberChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNumberChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordSpecialChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordSpecialChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordSpecialChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordSpecialChar") Then
                AppConfig.AppSettings.Settings("PasswordSpecialChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordSpecialChar", value)
            End If
        End Set
    End Property

    Public Shared Property EffectTimeForCodeResetPassword() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("EffectTimeForCodeResetPassword") Then
                Return 60
            End If
            Return AppConfig.AppSettings.Settings("EffectTimeForCodeResetPassword").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("EffectTimeForCodeResetPassword") Then
                AppConfig.AppSettings.Settings("EffectTimeForCodeResetPassword").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("EffectTimeForCodeResetPassword", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNotifyDays() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyDays") Then
                Return 10
            End If
            Return AppConfig.AppSettings.Settings("PasswordNotifyDays").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyDays") Then
                AppConfig.AppSettings.Settings("PasswordNotifyDays").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNotifyDays", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNotifyCount() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyCount") Then
                Return 1
            End If
            Return AppConfig.AppSettings.Settings("PasswordNotifyCount").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyCount") Then
                AppConfig.AppSettings.Settings("PasswordNotifyCount").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNotifyCount", value)
            End If
        End Set
    End Property
    Public Shared Property PasswordConfig() As Boolean
        Get
            If Not dicConfig.ContainsKey("PasswordConfig") Then
                Return False
            End If
            Return dicConfig("PasswordConfig")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PasswordConfig") Then
                dicConfig("PasswordConfig") = value
            Else
                dicConfig.Add("PasswordConfig", value)
            End If
        End Set
    End Property
    Public Shared Property PasswordDefault() As Boolean
        Get
            If Not dicConfig.ContainsKey("PasswordDefault") Then
                Return False
            End If
            Return dicConfig("PasswordDefault")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PasswordDefault") Then
                dicConfig("PasswordDefault") = value
            Else
                dicConfig.Add("PasswordDefault", value)
            End If
        End Set
    End Property
    Public Shared Property PasswordDefaultText() As String
        Get
            If Not dicConfig.ContainsKey("PasswordDefaultText") Then
                Return ""
            End If
            Return dicConfig("PasswordDefaultText")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("PasswordDefaultText") Then
                dicConfig("PasswordDefaultText") = value
            Else
                dicConfig.Add("PasswordDefaultText", value)
            End If
        End Set
    End Property
#End Region

#Region "iProfile"
    Public Shared Property ReminderEmail() As String
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Email, Integer)) Then
                Return 0
            End If
            Return dicReminderConfig(CType(RemindConfigType.Email, Integer))
        End Get
        Set(ByVal value As String)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Email, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Email, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Email, Integer), value)
            End If
        End Set
    End Property

    Public Shared ReadOnly Property dicReminderConfig As Dictionary(Of Integer, String)
        Get
            If HttpContext.Current.Session("ConfigReminderDictionaryCache") Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ConfigReminderDictionaryCache") = rep.GetReminderConfig(CurrentUser)
                End Using
            End If
            Return HttpContext.Current.Session("ConfigReminderDictionaryCache")
        End Get
    End Property
    Public Shared Property ReminderApproveHDLDDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHDLD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveHDLD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHDLD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveHDLD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveHDLD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderApproveTHHDDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApprovetTHHD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApprovetTHHD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApprovetTHHD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApprovetTHHD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApprovetTHHD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderMaternitiDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Materniti, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Materniti, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Materniti, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Materniti, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Materniti, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderRetirementDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Retirement, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Retirement, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Retirement, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Retirement, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Retirement, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderNoneSalaryDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.NoneSalary, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.NoneSalary, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.NoneSalary, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.NoneSalary, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.NoneSalary, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderExpiredCertificate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpiredCertificate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpiredCertificate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpiredCertificate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpiredCertificate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpiredCertificate, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderBIRTHDAY_LD() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.BIRTHDAY_LD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.BIRTHDAY_LD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.BIRTHDAY_LD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.BIRTHDAY_LD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.BIRTHDAY_LD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderConcurrently() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Concurrently, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Concurrently, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Concurrently, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Concurrently, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Concurrently, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderEmpDtlFamily() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.EmpDtlFamily, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.EmpDtlFamily, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.EmpDtlFamily, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.EmpDtlFamily, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.EmpDtlFamily, Integer), value)
            End If

        End Set
    End Property

    Public Shared Property ReminderExpContrat() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpContrat, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpContrat, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpContrat, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpContrat, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpContrat, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderExpDiscipline() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpDiscipline, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpDiscipline, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpDiscipline, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpDiscipline, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpDiscipline, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderInsArising() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.InsArising, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.InsArising, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.InsArising, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.InsArising, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.InsArising, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderOverRegDate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.OverRegDate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.OverRegDate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.OverRegDate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.OverRegDate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.OverRegDate, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderSign() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Sign, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Sign, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Sign, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Sign, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Sign, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderBHYT() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.BHYT, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.BHYT, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.BHYT, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.BHYT, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.BHYT, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderApproveDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Approve, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Approve, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Approve, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Approve, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Approve, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderContractDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Contract, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Contract, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Contract, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Contract, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Contract, Integer), value)
            End If

        End Set
    End Property

    Public Shared Property ReminderBirthdayDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Birthday, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Birthday, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Birthday, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Birthday, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Birthday, Integer), value)
            End If

        End Set
    End Property

    Public Shared Property ReminderVisa() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireVisa, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireVisa, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireVisa, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireVisa, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireVisa, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderWorking() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorking, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireWorking, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorking, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireWorking, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireWorking, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderTerminate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireTerminate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireTerminate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireTerminate, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderTerminateDebt() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminateDebt, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireTerminateDebt, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminateDebt, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireTerminateDebt, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireTerminateDebt, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderNoPaper() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireNoPaper, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireNoPaper, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireNoPaper, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireNoPaper, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireNoPaper, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderLabor() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireLabor, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireLabor, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireLabor, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireLabor, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireLabor, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderCertificate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireCertificate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireCertificate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireCertificate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireCertificate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireCertificate, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderProbation() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Probation, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Probation, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Probation, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Probation, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Probation, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderAge18() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Age18, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Age18, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Age18, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Age18, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Age18, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderExpAuthority() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpAuthority, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpAuthority, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpAuthority, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpAuthority, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpAuthority, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderNoticePersonalIncomeTax() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.NoticePersonalIncomeTax, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.NoticePersonalIncomeTax, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.NoticePersonalIncomeTax, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.NoticePersonalIncomeTax, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.NoticePersonalIncomeTax, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderNoticeStock() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.NoticeStock, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.NoticeStock, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.NoticeStock, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.NoticeStock, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.NoticeStock, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderApproveHSNV() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHSNV, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveHSNV, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHSNV, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveHSNV, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveHSNV, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderApproveWorkBefore() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveWorkBefore, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveWorkBefore, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveWorkBefore, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveWorkBefore, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveWorkBefore, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderApproveFamily() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveFamily, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveFamily, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveFamily, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveFamily, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveFamily, Integer), value)
            End If
        End Set
    End Property
    Public Shared Property ReminderApproveCertificate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveCertificate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveCertificate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveCertificate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveCertificate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveCertificate, Integer), value)
            End If
        End Set
    End Property
    Public Shared Sub GetReminderConfigFromDatabase()
        Using rep As New CommonRepository
            HttpContext.Current.Session("ConfigReminderDictionaryCache") = rep.GetReminderConfig(CurrentUser)
        End Using
    End Sub
#End Region

#Region "iPayroll"

    Public Shared Property PA_BASIC_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_BASIC_SAL") Then
                Return True
            End If
            Return dicConfig("PA_BASIC_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_BASIC_SAL") Then
                dicConfig("PA_BASIC_SAL") = value
            Else
                dicConfig.Add("PA_BASIC_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_SOFT_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_SOFT_SAL") Then
                Return False
            End If
            Return dicConfig("PA_SOFT_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_SOFT_SAL") Then
                dicConfig("PA_SOFT_SAL") = value
            Else
                dicConfig.Add("PA_SOFT_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_COEFFICIENT() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_COEFFICIENT") Then
                Return False
            End If
            Return dicConfig("PA_COEFFICIENT")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_COEFFICIENT") Then
                dicConfig("PA_COEFFICIENT") = value
            Else
                dicConfig.Add("PA_COEFFICIENT", value)
            End If
        End Set
    End Property

    Public Shared Property PA_OTHER_SAL As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_OTHER_SAL") Then
                Return False
            End If
            Return dicConfig("PA_OTHER_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_OTHER_SAL") Then
                dicConfig("PA_OTHER_SAL") = value
            Else
                dicConfig.Add("PA_OTHER_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_BASIC_UP_STEP() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_BASIC_UP_STEP") Then
                Return False
            End If
            Return dicConfig("PA_BASIC_UP_STEP")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_BASIC_UP_STEP") Then
                dicConfig("PA_BASIC_UP_STEP") = value
            Else
                dicConfig.Add("PA_BASIC_UP_STEP", value)
            End If
        End Set
    End Property

    Public Shared Property PA_SOFT_UP_STEP() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_SOFT_UP_STEP") Then
                Return False
            End If
            Return dicConfig("PA_SOFT_UP_STEP")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_SOFT_UP_STEP") Then
                dicConfig("PA_SOFT_UP_STEP") = value
            Else
                dicConfig.Add("PA_SOFT_UP_STEP", value)
            End If
        End Set
    End Property

    Public Shared Property PA_MIN_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_MIN_SAL") Then
                Return False
            End If
            Return dicConfig("PA_MIN_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_MIN_SAL") Then
                dicConfig("PA_MIN_SAL") = value
            Else
                dicConfig.Add("PA_MIN_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_MAX_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_MAX_SAL") Then
                Return False
            End If
            Return dicConfig("PA_MAX_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_MAX_SAL") Then
                dicConfig("PA_MAX_SAL") = value
            Else
                dicConfig.Add("PA_MAX_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_CURRENCY() As String
        Get
            If Not dicConfig.ContainsKey("PA_CURRENCY") Then
                Return ""
            End If
            Return dicConfig("PA_CURRENCY")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("PA_CURRENCY") Then
                dicConfig("PA_CURRENCY") = value
            Else
                dicConfig.Add("PA_CURRENCY", value)
            End If
        End Set
    End Property

#End Region

#Region "iTime"

    ' Ngày làm mới phép năm
    Public Shared Property AT_AL_DATE_RESET() As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_DATE_RESET") Then
                Return "31/03/2013"
            End If
            Return dicConfig("AT_AL_DATE_RESET")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_DATE_RESET") Then
                dicConfig("AT_AL_DATE_RESET") = value
            Else
                dicConfig.Add("AT_AL_DATE_RESET", value)
            End If
        End Set
    End Property

    'Số phép năm được hưởng 1 năm
    Public Shared Property AT_AL_IN_YEAR As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_IN_YEAR") Then
                Return 12
            End If
            Return dicConfig("AT_AL_IN_YEAR")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_IN_YEAR") Then
                dicConfig("AT_AL_IN_YEAR") = value
            Else
                dicConfig.Add("AT_AL_IN_YEAR", value)
            End If
        End Set
    End Property

    'Số phép được phép ứng trước
    Public Shared Property AT_AL_ADVANCE_TAKEN As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_ADVANCE_TAKEN") Then
                Return 0
            End If
            Return dicConfig("AT_AL_ADVANCE_TAKEN")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_ADVANCE_TAKEN") Then
                dicConfig("AT_AL_ADVANCE_TAKEN") = value
            Else
                dicConfig.Add("AT_AL_ADVANCE_TAKEN", value)
            End If
        End Set
    End Property

    'Số năm thâm niên được tăng phép
    Public Shared Property AT_AL_SENIORITY As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_SENIORITY") Then
                Return 1
            End If
            Return dicConfig("AT_AL_SENIORITY")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_SENIORITY") Then
                dicConfig("AT_AL_SENIORITY") = value
            Else
                dicConfig.Add("AT_AL_SENIORITY", value)
            End If
        End Set
    End Property

    'Số phép tăng theo thâm niên
    Public Shared Property AT_AL_SENIORITY_UP As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_SENIORITY_UP") Then
                Return 1
            End If
            Return dicConfig("AT_AL_SENIORITY_UP")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_SENIORITY_UP") Then
                dicConfig("AT_AL_SENIORITY_UP") = value
            Else
                dicConfig.Add("AT_AL_SENIORITY_UP", value)
            End If
        End Set
    End Property

    'Đăng ký phép trong thời gian thử việc
    Public Shared Property AT_AL_REG_PROBA As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_AL_REG_PROBA") Then
                Return False
            End If
            Return dicConfig("AT_AL_REG_PROBA")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_AL_REG_PROBA") Then
                dicConfig("AT_AL_REG_PROBA") = value
            Else
                dicConfig.Add("AT_AL_REG_PROBA", value)
            End If
        End Set
    End Property

    ' Tính theo tháng
    Public Shared Property AT_AL_CAL_MONTH As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_AL_CAL_MONTH") Then
                Return False
            End If
            Return dicConfig("AT_AL_CAL_MONTH")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_AL_CAL_MONTH") Then
                dicConfig("AT_AL_CAL_MONTH") = value
            Else
                dicConfig.Add("AT_AL_CAL_MONTH", value)
            End If
        End Set
    End Property

    ' Tính theo tháng
    Public Shared Property AT_AL_CTRACT_TYPE As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_CTRACT_TYPE") Then
                Return ""
            End If
            Return dicConfig("AT_AL_CTRACT_TYPE")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_CTRACT_TYPE") Then
                dicConfig("AT_AL_CTRACT_TYPE") = value
            Else
                dicConfig.Add("AT_AL_CTRACT_TYPE", value)
            End If
        End Set
    End Property

    ' Số giờ OT 1 năm
    Public Shared Property AT_AL_OT_YEAR_NUMBER As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_OT_YEAR_NUMBER") Then
                Return 0
            End If
            Return dicConfig("AT_AL_OT_YEAR_NUMBER")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_OT_YEAR_NUMBER") Then
                dicConfig("AT_AL_OT_YEAR_NUMBER") = value
            Else
                dicConfig.Add("AT_AL_OT_YEAR_NUMBER", value)
            End If
        End Set
    End Property


    ' Tính OT nghỉ bù theo hệ số nhà nước
    Public Shared Property GOVERMENT_COM_OT As Boolean
        Get
            If Not dicConfig.ContainsKey("GOVERMENT_COM_OT") Then
                Return 0
            End If
            Return dicConfig("GOVERMENT_COM_OT")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("GOVERMENT_COM_OT") Then
                dicConfig("AT_AL_OT_YEAR_NUMBER") = value
            Else
                dicConfig.Add("GOVERMENT_COM_OT", value)
            End If
        End Set
    End Property

    'Bỏ xử lý quy trình phê duyệt ca làm việc
    Public Shared Property PORTALSIGNWORK As Boolean
        Get
            If Not dicConfig.ContainsKey("PORTALSIGNWORK") Then
                Return 0
            End If
            Return dicConfig("PORTALSIGNWORK")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PORTALSIGNWORK") Then
                dicConfig("PORTALSIGNWORK") = value
            Else
                dicConfig.Add("PORTALSIGNWORK", value)
            End If
        End Set
    End Property

    'Bỏ qua phân quyền ca làm việc theo phòng ban
    Public Shared Property PORTALSIGNWORKDMCA As Boolean
        Get
            If Not dicConfig.ContainsKey("PORTALSIGNWORKDMCA") Then
                Return 0
            End If
            Return dicConfig("PORTALSIGNWORKDMCA")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PORTALSIGNWORKDMCA") Then
                dicConfig("PORTALSIGNWORKDMCA") = value
            Else
                dicConfig.Add("PORTALSIGNWORKDMCA", value)
            End If
        End Set
    End Property
#End Region

#Region "iPortal"
    Public Shared Property APP_SETTING() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING") Then
                Return True
            End If
            Return dicConfig("APP_SETTING")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING") Then
                dicConfig("APP_SETTING") = value
            Else
                dicConfig.Add("APP_SETTING", value)
            End If
        End Set
    End Property
    Public Shared Property CODE_ACCURACY() As String
        Get
            If Not dicConfig.ContainsKey("CODE_ACCURACY") Then
                Return ""
            End If
            Return dicConfig("CODE_ACCURACY")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("CODE_ACCURACY") Then
                dicConfig("CODE_ACCURACY") = value
            Else
                dicConfig.Add("CODE_ACCURACY", value)
            End If
        End Set
    End Property
    Public Shared Property SETUP_NUM_ORG() As String
        Get
            If Not dicConfig.ContainsKey("SETUP_NUM_ORG") Then
                Return ""
            End If
            Return dicConfig("SETUP_NUM_ORG")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("SETUP_NUM_ORG") Then
                dicConfig("SETUP_NUM_ORG") = value
            Else
                dicConfig.Add("SETUP_NUM_ORG", value)
            End If
        End Set
    End Property
    Public Shared Property NAME_MYPAGE_PORTAL() As String
        Get
            If Not dicConfig.ContainsKey("NAME_MYPAGE_PORTAL") Then
                Return ""
            End If
            Return dicConfig("NAME_MYPAGE_PORTAL")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("NAME_MYPAGE_PORTAL") Then
                dicConfig("NAME_MYPAGE_PORTAL") = value
            Else
                dicConfig.Add("NAME_MYPAGE_PORTAL", value)
            End If
        End Set
    End Property
    Public Shared Property MAINTENANCECOMINGEND() As String
        Get
            If Not dicConfig.ContainsKey("MAINTENANCECOMINGEND") Then
                Return ""
            End If
            Return dicConfig("MAINTENANCECOMINGEND")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MAINTENANCECOMINGEND") Then
                dicConfig("MAINTENANCECOMINGEND") = value
            Else
                dicConfig.Add("MAINTENANCECOMINGEND", value)
            End If
        End Set
    End Property
    Public Shared Property MAINTENANCEEND() As String
        Get
            If Not dicConfig.ContainsKey("MAINTENANCEEND") Then
                Return ""
            End If
            Return dicConfig("MAINTENANCEEND")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MAINTENANCEEND") Then
                dicConfig("MAINTENANCEEND") = value
            Else
                dicConfig.Add("MAINTENANCEEND", value)
            End If
        End Set
    End Property
    Public Shared Property NOTIFYLOGIN() As String
        Get
            If Not dicConfig.ContainsKey("NOTIFYLOGIN") Then
                Return ""
            End If
            Return dicConfig("NOTIFYLOGIN")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("NOTIFYLOGIN") Then
                dicConfig("NOTIFYLOGIN") = value
            Else
                dicConfig.Add("NOTIFYLOGIN", value)
            End If
        End Set
    End Property
    Public Shared Property ISNOTLOGINMAINTENAN() As Boolean
        Get
            If Not dicConfig.ContainsKey("ISNOTLOGINMAINTENAN") Then
                Return True
            End If
            Return dicConfig("ISNOTLOGINMAINTENAN")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("ISNOTLOGINMAINTENAN") Then
                dicConfig("ISNOTLOGINMAINTENAN") = value
            Else
                dicConfig.Add("ISNOTLOGINMAINTENAN", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_1() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_1") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_1")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_1") Then
                dicConfig("APP_SETTING_1") = value
            Else
                dicConfig.Add("APP_SETTING_1", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_2() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_2") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_2")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_2") Then
                dicConfig("APP_SETTING_2") = value
            Else
                dicConfig.Add("APP_SETTING_2", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_3() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_3") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_3")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_3") Then
                dicConfig("APP_SETTING_3") = value
            Else
                dicConfig.Add("APP_SETTING_3", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_4() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_4") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_4")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_4") Then
                dicConfig("APP_SETTING_4") = value
            Else
                dicConfig.Add("APP_SETTING_4", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_5() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_5") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_5")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_5") Then
                dicConfig("APP_SETTING_5") = value
            Else
                dicConfig.Add("APP_SETTING_5", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_6() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_6") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_6")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_6") Then
                dicConfig("APP_SETTING_6") = value
            Else
                dicConfig.Add("APP_SETTING_6", value)
            End If
        End Set
    End Property


    Public Shared Property APP_SETTING_7() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_7") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_7")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_7") Then
                dicConfig("APP_SETTING_7") = value
            Else
                dicConfig.Add("APP_SETTING_7", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_8() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_8") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_8")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_8") Then
                dicConfig("APP_SETTING_8") = value
            Else
                dicConfig.Add("APP_SETTING_8", value)
            End If
        End Set
    End Property
    ''=====================================================

    Public Shared Property APP_SETTING_9() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_9") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_9")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_9") Then
                dicConfig("APP_SETTING_9") = value
            Else
                dicConfig.Add("APP_SETTING_9", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_10() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_10") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_10")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_10") Then
                dicConfig("APP_SETTING_10") = value
            Else
                dicConfig.Add("APP_SETTING_10", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_11() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_11") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_11")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_11") Then
                dicConfig("APP_SETTING_11") = value
            Else
                dicConfig.Add("APP_SETTING_11", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_12() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_12") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_12")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_12") Then
                dicConfig("APP_SETTING_12") = value
            Else
                dicConfig.Add("APP_SETTING_12", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_13() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_13") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_13")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_13") Then
                dicConfig("APP_SETTING_13") = value
            Else
                dicConfig.Add("APP_SETTING_13", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_14() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_14") Then
                Return True
            End If
            Return dicConfig("APP_SETTING_14")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_14") Then
                dicConfig("APP_SETTING_14") = value
            Else
                dicConfig.Add("APP_SETTING_14", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_15() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_15") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_15")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_15") Then
                dicConfig("APP_SETTING_15") = value
            Else
                dicConfig.Add("APP_SETTING_15", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_16() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_16") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_16")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_16") Then
                dicConfig("APP_SETTING_16") = value
            Else
                dicConfig.Add("APP_SETTING_16", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_17() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_17") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_17")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_17") Then
                dicConfig("APP_SETTING_17") = value
            Else
                dicConfig.Add("APP_SETTING_17", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_18() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_18") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_18")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_18") Then
                dicConfig("APP_SETTING_18") = value
            Else
                dicConfig.Add("APP_SETTING_18", value)
            End If
        End Set
    End Property

    Public Shared Property APP_SETTING_19() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_19") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_19")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_19") Then
                dicConfig("APP_SETTING_19") = value
            Else
                dicConfig.Add("APP_SETTING_19", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_20() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_20") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_20")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_20") Then
                dicConfig("APP_SETTING_20") = value
            Else
                dicConfig.Add("APP_SETTING_20", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_21() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_21") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_21")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_21") Then
                dicConfig("APP_SETTING_21") = value
            Else
                dicConfig.Add("APP_SETTING_21", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_22() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_22") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_22")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_22") Then
                dicConfig("APP_SETTING_22") = value
            Else
                dicConfig.Add("APP_SETTING_22", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_23() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_23") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_23")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_23") Then
                dicConfig("APP_SETTING_23") = value
            Else
                dicConfig.Add("APP_SETTING_23", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_24() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_24") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_24")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_24") Then
                dicConfig("APP_SETTING_24") = value
            Else
                dicConfig.Add("APP_SETTING_24", value)
            End If
        End Set
    End Property
    Public Shared Property APP_SETTING_25() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_SETTING_25") Then
                Return False
            End If
            Return dicConfig("APP_SETTING_25")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_SETTING_25") Then
                dicConfig("APP_SETTING_25") = value
            Else
                dicConfig.Add("APP_SETTING_25", value)
            End If
        End Set
    End Property
    Public Shared Property APP_ISHIDE_IMAGE() As Boolean
        Get
            If Not dicConfig.ContainsKey("APP_ISHIDE_IMAGE") Then
                Return False
            End If
            Return dicConfig("APP_ISHIDE_IMAGE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("APP_ISHIDE_IMAGE") Then
                dicConfig("APP_ISHIDE_IMAGE") = value
            Else
                dicConfig.Add("APP_ISHIDE_IMAGE", value)
            End If
        End Set
    End Property
    Public Shared Property CONFIGTITLE() As Boolean
        Get
            If Not dicConfig.ContainsKey("CONFIGTITLE") Then
                Return False
            End If
            Return dicConfig("CONFIGTITLE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("CONFIGTITLE") Then
                dicConfig("CONFIGTITLE") = value
            Else
                dicConfig.Add("CONFIGTITLE", value)
            End If
        End Set
    End Property
    Public Shared Property ISHIDE_OBJECTLABORNEW() As Boolean
        Get
            If Not dicConfig.ContainsKey("ISHIDE_OBJECTLABORNEW") Then
                Return False
            End If
            Return dicConfig("ISHIDE_OBJECTLABORNEW")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("ISHIDE_OBJECTLABORNEW") Then
                dicConfig("ISHIDE_OBJECTLABORNEW") = value
            Else
                dicConfig.Add("ISHIDE_OBJECTLABORNEW", value)
            End If
        End Set
    End Property
    Public Shared Property HU_QUTT_PERMISION() As Boolean
        Get
            If Not dicConfig.ContainsKey("HU_QUTT_PERMISION") Then
                Return False
            End If
            Return dicConfig("HU_QUTT_PERMISION")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("HU_QUTT_PERMISION") Then
                dicConfig("HU_QUTT_PERMISION") = value
            Else
                dicConfig.Add("HU_QUTT_PERMISION", value)
            End If
        End Set
    End Property
    Public Shared Property RC_REQUEST_PORTALPERORG() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_REQUEST_PORTALPERORG") Then
                Return False
            End If
            Return dicConfig("RC_REQUEST_PORTALPERORG")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_REQUEST_PORTALPERORG") Then
                dicConfig("RC_REQUEST_PORTALPERORG") = value
            Else
                dicConfig.Add("RC_REQUEST_PORTALPERORG", value)
            End If
        End Set
    End Property
    '' Cho phép chỉnh sửa và gửi phê duyệt thông tin nhân viên và nhân thân trên portal
    Public Shared Property PORTAL_ALLOW_CHANGE() As Boolean
        Get
            If Not dicConfig.ContainsKey("PORTAL_ALLOW_CHANGE") Then
                Return True
            End If
            Return dicConfig("PORTAL_ALLOW_CHANGE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PORTAL_ALLOW_CHANGE") Then
                dicConfig("PORTAL_ALLOW_CHANGE") = value
            Else
                dicConfig.Add("PORTAL_ALLOW_CHANGE", value)
            End If
        End Set
    End Property
    'Cảnh báo vượt rank lương theo vị trí định biên
    Public Shared Property IS_HIDE_AD_USER() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_HIDE_AD_USER") Then
                Return True
            End If
            Return dicConfig("IS_HIDE_AD_USER")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_HIDE_AD_USER") Then
                dicConfig("IS_HIDE_AD_USER") = value
            Else
                dicConfig.Add("IS_HIDE_AD_USER", value)
            End If
        End Set
    End Property
    'thiết lập Ẩn thông tin tài khoản liên kết AD User
    Public Shared Property IS_HIDE_BUTTON_RESET() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_HIDE_BUTTON_RESET") Then
                Return True
            End If
            Return dicConfig("IS_HIDE_BUTTON_RESET")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_HIDE_BUTTON_RESET") Then
                dicConfig("IS_HIDE_BUTTON_RESET") = value
            Else
                dicConfig.Add("IS_HIDE_BUTTON_RESET", value)
            End If
        End Set
    End Property
    'thiết lập Ẩn tính năng reset (nút reset)
    Public Shared Property RC_OVER_RANK() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_OVER_RANK") Then
                Return True
            End If
            Return dicConfig("RC_OVER_RANK")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_OVER_RANK") Then
                dicConfig("RC_OVER_RANK") = value
            Else
                dicConfig.Add("RC_OVER_RANK", value)
            End If
        End Set
    End Property
    Public Shared Property AT_HS_OT() As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_HS_OT") Then
                Return True
            End If
            Return dicConfig("AT_HS_OT")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_HS_OT") Then
                dicConfig("AT_HS_OT") = value
            Else
                dicConfig.Add("AT_HS_OT", value)
            End If
        End Set
    End Property
    'Cảnh báo vượt ngân sách lương theo bộ phận
    Public Shared Property RC_SAL_BUDGET_EXCEEDED() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_SAL_BUDGET_EXCEEDED") Then
                Return True
            End If
            Return dicConfig("RC_SAL_BUDGET_EXCEEDED")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_SAL_BUDGET_EXCEEDED") Then
                dicConfig("RC_SAL_BUDGET_EXCEEDED") = value
            Else
                dicConfig.Add("RC_SAL_BUDGET_EXCEEDED", value)
            End If
        End Set
    End Property
    Public Shared Property PERSONRE_TCTD() As Boolean
        Get
            If Not dicConfig.ContainsKey("PERSONRE_TCTD") Then
                Return True
            End If
            Return dicConfig("PERSONRE_TCTD")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PERSONRE_TCTD") Then
                dicConfig("PERSONRE_TCTD") = value
            Else
                dicConfig.Add("PERSONRE_TCTD", value)
            End If
        End Set
    End Property

    'Cảnh báo vượt phep ứng
    Public Shared Property AT_ADVANCELEAVE() As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_ADVANCELEAVE") Then
                Return False
            End If
            Return dicConfig("AT_ADVANCELEAVE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_ADVANCELEAVE") Then
                dicConfig("AT_ADVANCELEAVE") = value
            Else
                dicConfig.Add("AT_ADVANCELEAVE", value)
            End If
        End Set
    End Property

    'Cảnh báo vượt phep ứng
    Public Shared Property AT_ADVANCELEAVE_VALUE() As String
        Get
            If Not dicConfig.ContainsKey("AT_ADVANCELEAVE_VALUE") Then
                Return ""
            End If
            Return dicConfig("AT_ADVANCELEAVE_VALUE")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_ADVANCELEAVE_VALUE") Then
                dicConfig("AT_ADVANCELEAVE_VALUE") = value
            Else
                dicConfig.Add("AT_ADVANCELEAVE_VALUE", value)
            End If
        End Set
    End Property

    Public Shared Property IS_ORG_EXPAND() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_ORG_EXPAND") Then
                Return False
            End If
            Return dicConfig("IS_ORG_EXPAND")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_ORG_EXPAND") Then
                dicConfig("IS_ORG_EXPAND") = value
            Else
                dicConfig.Add("IS_ORG_EXPAND", value)
            End If
        End Set
    End Property
    Public Shared Property ORG_EXPAND_LEVEL() As String
        Get
            If Not dicConfig.ContainsKey("ORG_EXPAND_LEVEL") Then
                Return ""
            End If
            Return dicConfig("ORG_EXPAND_LEVEL")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("ORG_EXPAND_LEVEL") Then
                dicConfig("ORG_EXPAND_LEVEL") = value
            Else
                dicConfig.Add("ORG_EXPAND_LEVEL", value)
            End If
        End Set
    End Property
    Public Shared Property AT_ADVANCELEAVETEMP_VALUE() As String
        Get
            If Not dicConfig.ContainsKey("AT_ADVANCELEAVETEMP_VALUE") Then
                Return ""
            End If
            Return dicConfig("AT_ADVANCELEAVETEMP_VALUE")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_ADVANCELEAVETEMP_VALUE") Then
                dicConfig("AT_ADVANCELEAVETEMP_VALUE") = value
            Else
                dicConfig.Add("AT_ADVANCELEAVETEMP_VALUE", value)
            End If
        End Set
    End Property
    Public Shared Property HIDE_MANAGERHEATHINS() As Boolean
        Get
            If Not dicConfig.ContainsKey("HIDE_MANAGERHEATHINS") Then
                Return True
            End If
            Return dicConfig("HIDE_MANAGERHEATHINS")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("HIDE_MANAGERHEATHINS") Then
                dicConfig("HIDE_MANAGERHEATHINS") = value
            Else
                dicConfig.Add("HIDE_MANAGERHEATHINS", value)
            End If
        End Set
    End Property
    Public Shared Property CAL_SENIORITY_BY_HSV() As Boolean
        Get
            If Not dicConfig.ContainsKey("CAL_SENIORITY_BY_HSV") Then
                Return False
            End If
            Return dicConfig("CAL_SENIORITY_BY_HSV")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("CAL_SENIORITY_BY_HSV") Then
                dicConfig("CAL_SENIORITY_BY_HSV") = value
            Else
                dicConfig.Add("CAL_SENIORITY_BY_HSV", value)
            End If
        End Set
    End Property
    Public Shared Property IS_LOAD_DIRECTMNG() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_LOAD_DIRECTMNG") Then
                Return False
            End If
            Return dicConfig("IS_LOAD_DIRECTMNG")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_LOAD_DIRECTMNG") Then
                dicConfig("IS_LOAD_DIRECTMNG") = value
            Else
                dicConfig.Add("IS_LOAD_DIRECTMNG", value)
            End If
        End Set
    End Property

    Public Shared Property IS_AUTO() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_AUTO") Then
                Return False
            End If
            Return dicConfig("IS_AUTO")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_AUTO") Then
                dicConfig("IS_AUTO") = value
            Else
                dicConfig.Add("IS_AUTO", value)
            End If
        End Set
    End Property
    Public Shared Property IS_WORKING_LESSEQUAL_WORKINGSTANDARD() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_WORKING_LESSEQUAL_WORKINGSTANDARD") Then
                Return False
            End If
            Return dicConfig("IS_WORKING_LESSEQUAL_WORKINGSTANDARD")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_WORKING_LESSEQUAL_WORKINGSTANDARD") Then
                dicConfig("IS_WORKING_LESSEQUAL_WORKINGSTANDARD") = value
            Else
                dicConfig.Add("IS_WORKING_LESSEQUAL_WORKINGSTANDARD", value)
            End If
        End Set
    End Property
    Public Shared Property IS_BOUND_SHIFT() As Boolean
        Get
            If Not dicConfig.ContainsKey("IS_BOUND_SHIFT") Then
                Return False
            End If
            Return dicConfig("IS_BOUND_SHIFT")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("IS_BOUND_SHIFT") Then
                dicConfig("IS_BOUND_SHIFT") = value
            Else
                dicConfig.Add("IS_BOUND_SHIFT", value)
            End If
        End Set
    End Property
    Public Shared Property AT_BOUND_SHIFT_VALUE() As String
        Get
            If Not dicConfig.ContainsKey("AT_BOUND_SHIFT_VALUE") Then
                Return False
            End If
            Return dicConfig("AT_BOUND_SHIFT_VALUE")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_BOUND_SHIFT_VALUE") Then
                dicConfig("AT_BOUND_SHIFT_VALUE") = value
            Else
                dicConfig.Add("AT_BOUND_SHIFT_VALUE", value)
            End If
        End Set
    End Property
    Public Shared Property AT_BOUND_SHIFT_OPERATOR() As String
        Get
            If Not dicConfig.ContainsKey("AT_BOUND_SHIFT_OPERATOR") Then
                Return False
            End If
            Return dicConfig("AT_BOUND_SHIFT_OPERATOR")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_BOUND_SHIFT_OPERATOR") Then
                dicConfig("AT_BOUND_SHIFT_OPERATOR") = value
            Else
                dicConfig.Add("AT_BOUND_SHIFT_OPERATOR", value)
            End If
        End Set
    End Property


    Public Shared Property RC_ID_NO_REQUIRE() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_ID_NO_REQUIRE") Then
                Return True
            End If
            Return dicConfig("RC_ID_NO_REQUIRE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_ID_NO_REQUIRE") Then
                dicConfig("RC_ID_NO_REQUIRE") = value
            Else
                dicConfig.Add("RC_ID_NO_REQUIRE", value)
            End If
        End Set
    End Property

    Public Shared Property RC_PERSONAL_EMAIL_REQUIRE() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_PERSONAL_EMAIL_REQUIRE") Then
                Return True
            End If
            Return dicConfig("RC_PERSONAL_EMAIL_REQUIRE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_PERSONAL_EMAIL_REQUIRE") Then
                dicConfig("RC_PERSONAL_EMAIL_REQUIRE") = value
            Else
                dicConfig.Add("RC_PERSONAL_EMAIL_REQUIRE", value)
            End If
        End Set
    End Property

    Public Shared Property RC_MOBILE_PHONE_REQUIRE() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_MOBILE_PHONE_REQUIRE") Then
                Return True
            End If
            Return dicConfig("RC_MOBILE_PHONE_REQUIRE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_MOBILE_PHONE_REQUIRE") Then
                dicConfig("RC_MOBILE_PHONE_REQUIRE") = value
            Else
                dicConfig.Add("RC_MOBILE_PHONE_REQUIRE", value)
            End If
        End Set
    End Property

    Public Shared Property RC_LITERACY_REQUIRE() As Boolean
        Get
            If Not dicConfig.ContainsKey("RC_LITERACY_REQUIRE") Then
                Return True
            End If
            Return dicConfig("RC_LITERACY_REQUIRE")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("RC_LITERACY_REQUIRE") Then
                dicConfig("RC_LITERACY_REQUIRE") = value
            Else
                dicConfig.Add("RC_LITERACY_REQUIRE", value)
            End If
        End Set
    End Property
#End Region

    Public Shared Function SaveChanges(Optional ByVal isSaveConfig As Boolean = True)
        Try
            Using rep As New CommonRepository
                rep.UpdateConfig(dicConfig, ModuleID)
                If isSaveConfig Then
                    AppConfig.Save()
                End If
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveConfig()
        Try
            AppConfig.Save()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveConfigHost()
        Try
            Using rep As New CommonRepository
                rep.UpdateConfig(dicConfig, ModuleID)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function SetSE_Configuration() As Boolean
        Try
            Using rep As New CommonRepository
                Dim dic As New Dictionary(Of String, String)
                dic.Add("PasswordDefault", dicConfig_save("PasswordDefault"))
                dic.Add("PasswordConfig", dicConfig_save("PasswordConfig"))
                dic.Add("PasswordDefaultText", dicConfig_save("PasswordDefaultText"))
                dic.Add("ActiveTimeout", dicConfig_save("ActiveTimeout"))
                rep.SetGeneralConfig(dic, SystemConfigModuleID.iSecure)
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function SetGeneralConfig() As Boolean
        Try
            Using rep As New CommonRepository
                Dim dic As New Dictionary(Of String, String)
                dic.Add("PORTAL_ALLOW_CHANGE", dicConfig_save("PORTAL_ALLOW_CHANGE"))
                rep.SetGeneralConfig(dic, SystemConfigModuleID.iPortal)
            End Using
            Using rep As New CommonRepository
                Dim dic As New Dictionary(Of String, String)
                dic.Add("APP_SETTING", dicConfig_save("APP_SETTING"))
                dic.Add("APP_SETTING_1", dicConfig_save("APP_SETTING_1"))
                dic.Add("APP_SETTING_2", dicConfig_save("APP_SETTING_2"))
                dic.Add("APP_SETTING_3", dicConfig_save("APP_SETTING_3"))
                dic.Add("APP_SETTING_4", dicConfig_save("APP_SETTING_4"))
                dic.Add("APP_SETTING_5", dicConfig_save("APP_SETTING_5"))
                dic.Add("APP_SETTING_6", dicConfig_save("APP_SETTING_6"))

                dic.Add("APP_SETTING_7", dicConfig_save("APP_SETTING_7"))
                dic.Add("APP_SETTING_8", dicConfig_save("APP_SETTING_8"))
                dic.Add("APP_SETTING_9", dicConfig_save("APP_SETTING_9"))
                dic.Add("APP_SETTING_10", dicConfig_save("APP_SETTING_10"))
                dic.Add("APP_SETTING_11", dicConfig_save("APP_SETTING_11"))
                dic.Add("APP_SETTING_12", dicConfig_save("APP_SETTING_12"))
                dic.Add("APP_SETTING_13", dicConfig_save("APP_SETTING_13"))
                dic.Add("APP_SETTING_14", dicConfig_save("APP_SETTING_14"))

                dic.Add("APP_SETTING_15", dicConfig_save("APP_SETTING_15"))
                dic.Add("APP_SETTING_16", dicConfig_save("APP_SETTING_16"))
                dic.Add("APP_SETTING_17", dicConfig_save("APP_SETTING_17"))
                dic.Add("APP_SETTING_18", dicConfig_save("APP_SETTING_18"))
                dic.Add("APP_SETTING_19", dicConfig_save("APP_SETTING_19"))

                dic.Add("APP_SETTING_20", dicConfig_save("APP_SETTING_20"))
                dic.Add("APP_SETTING_21", dicConfig_save("APP_SETTING_21"))
                dic.Add("APP_SETTING_22", dicConfig_save("APP_SETTING_22"))
                dic.Add("APP_SETTING_23", dicConfig_save("APP_SETTING_23"))
                dic.Add("APP_SETTING_24", dicConfig_save("APP_SETTING_24"))
                dic.Add("APP_SETTING_25", dicConfig_save("APP_SETTING_25"))
                dic.Add("IS_ORG_EXPAND", dicConfig_save("IS_ORG_EXPAND"))
                dic.Add("ORG_EXPAND_LEVEL", dicConfig_save("ORG_EXPAND_LEVEL"))


                dic.Add("CODE_ACCURACY", dicConfig_save("CODE_ACCURACY"))

                'anhvn
                dic.Add("RC_OVER_RANK", dicConfig_save("RC_OVER_RANK"))
                dic.Add("RC_SAL_BUDGET_EXCEEDED", dicConfig_save("RC_SAL_BUDGET_EXCEEDED"))
                dic.Add("PERSONRE_TCTD", dicConfig_save("PERSONRE_TCTD"))

                dic.Add("AT_ADVANCELEAVE", dicConfig_save("AT_ADVANCELEAVE"))
                dic.Add("AT_ADVANCELEAVE_VALUE", dicConfig_save("AT_ADVANCELEAVE_VALUE"))

                dic.Add("AT_ADVANCELEAVETEMP_VALUE", dicConfig_save("AT_ADVANCELEAVETEMP_VALUE"))

                dic.Add("IS_HIDE_AD_USER", dicConfig_save("IS_HIDE_AD_USER"))
                dic.Add("IS_HIDE_BUTTON_RESET", dicConfig_save("IS_HIDE_BUTTON_RESET"))
                dic.Add("HIDE_MANAGERHEATHINS", dicConfig_save("HIDE_MANAGERHEATHINS"))
                dic.Add("AT_HS_OT", dicConfig_save("AT_HS_OT"))

                dic.Add("SETUP_NUM_ORG", dicConfig_save("SETUP_NUM_ORG"))
                dic.Add("NAME_MYPAGE_PORTAL", dicConfig_save("NAME_MYPAGE_PORTAL"))
                dic.Add("CAL_SENIORITY_BY_HSV", dicConfig_save("CAL_SENIORITY_BY_HSV"))
                dic.Add("IS_LOAD_DIRECTMNG", dicConfig_save("IS_LOAD_DIRECTMNG"))
                dic.Add("IS_AUTO", dicConfig_save("IS_AUTO"))

                dic.Add("IS_WORKING_LESSEQUAL_WORKINGSTANDARD", dicConfig_save("IS_WORKING_LESSEQUAL_WORKINGSTANDARD"))
                dic.Add("IS_BOUND_SHIFT", dicConfig_save("IS_BOUND_SHIFT"))
                dic.Add("AT_BOUND_SHIFT_VALUE", dicConfig_save("AT_BOUND_SHIFT_VALUE"))
                dic.Add("AT_BOUND_SHIFT_OPERATOR", dicConfig_save("AT_BOUND_SHIFT_OPERATOR"))

                dic.Add("APP_ISHIDE_IMAGE", dicConfig_save("APP_ISHIDE_IMAGE"))
                dic.Add("CONFIGTITLE", dicConfig_save("CONFIGTITLE"))
                dic.Add("ISHIDE_OBJECTLABORNEW", dicConfig_save("ISHIDE_OBJECTLABORNEW"))
                dic.Add("MAINTENANCECOMINGEND", dicConfig_save("MAINTENANCECOMINGEND"))
                dic.Add("MAINTENANCEEND", dicConfig_save("MAINTENANCEEND"))
                dic.Add("NOTIFYLOGIN", dicConfig_save("NOTIFYLOGIN"))
                dic.Add("ISNOTLOGINMAINTENAN", dicConfig_save("ISNOTLOGINMAINTENAN"))
                dic.Add("HU_QUTT_PERMISION", dicConfig_save("HU_QUTT_PERMISION"))
                dic.Add("RC_REQUEST_PORTALPERORG", dicConfig_save("RC_REQUEST_PORTALPERORG"))
                rep.SetGeneralConfig(dic, SystemConfigModuleID.iProfile)
            End Using

            Using rep As New CommonRepository
                Dim dic As New Dictionary(Of String, String)
                dic.Add("RC_ID_NO_REQUIRE", dicConfig_save("RC_ID_NO_REQUIRE"))
                dic.Add("RC_PERSONAL_EMAIL_REQUIRE", dicConfig_save("RC_PERSONAL_EMAIL_REQUIRE"))
                dic.Add("RC_MOBILE_PHONE_REQUIRE", dicConfig_save("RC_MOBILE_PHONE_REQUIRE"))
                dic.Add("RC_LITERACY_REQUIRE", dicConfig_save("RC_LITERACY_REQUIRE"))
                rep.SetGeneralConfig(dic, SystemConfigModuleID.iRecruitment)
            End Using

            Using rep As New CommonRepository
                Dim dic As New Dictionary(Of String, String)
                dic.Add("GOVERMENT_COM_OT", dicConfig_save("GOVERMENT_COM_OT"))
                dic.Add("PORTALSIGNWORK", dicConfig_save("PORTALSIGNWORK"))
                dic.Add("PORTALSIGNWORKDMCA", dicConfig_save("PORTALSIGNWORKDMCA"))
                rep.SetGeneralConfig(dic, SystemConfigModuleID.iTime)
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveReminderPerUserWithPermission() As Boolean
        Try
            Using rep As New CommonRepository
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.NoticeStock, Integer), ReminderNoticeStock)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function SaveReminderPerUser() As Boolean
        Try
            Using rep As New CommonRepository
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Contract, Integer), ReminderContractDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Birthday, Integer), ReminderBirthdayDays)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireVisa, Integer), ReminderVisa)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireTerminate, Integer), ReminderTerminate)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireTerminateDebt, Integer), ReminderTerminateDebt)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireWorking, Integer), ReminderWorking)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireNoPaper, Integer), ReminderNoPaper)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Probation, Integer), ReminderProbation)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireCertificate, Integer), ReminderCertificate)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireLabor, Integer), ReminderLabor)

                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Approve, Integer), ReminderApproveDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveHDLD, Integer), ReminderApproveHDLDDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApprovetTHHD, Integer), ReminderApproveTHHDDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Materniti, Integer), ReminderMaternitiDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Retirement, Integer), ReminderRetirementDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.NoneSalary, Integer), ReminderNoneSalaryDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpiredCertificate, Integer), ReminderExpiredCertificate)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.BIRTHDAY_LD, Integer), ReminderBIRTHDAY_LD)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Concurrently, Integer), ReminderConcurrently)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.EmpDtlFamily, Integer), ReminderEmpDtlFamily)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.OverRegDate, Integer), ReminderOverRegDate)

                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpContrat, Integer), ReminderExpContrat)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpDiscipline, Integer), ReminderExpDiscipline)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.InsArising, Integer), ReminderInsArising)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Sign, Integer), ReminderSign)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.BHYT, Integer), ReminderBHYT)
                rep.SetReminderConfig(CurrentUser, RemindConfigType.Email, ReminderEmail)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Age18, Integer), ReminderAge18)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpAuthority, Integer), ReminderExpAuthority)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.NoticePersonalIncomeTax, Integer), ReminderNoticePersonalIncomeTax)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveHSNV, Integer), ReminderApproveHSNV)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveWorkBefore, Integer), ReminderApproveWorkBefore)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveCertificate, Integer), ReminderApproveCertificate)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveFamily, Integer), ReminderApproveFamily)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireVisa, Integer), ReminderVisa)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireWorking, Integer), ReminderWorking)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireLabor, Integer), ReminderLabor)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireCertificate, Integer), ReminderCertificate)
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub GetConfigFromDatabase()
        Using rep As New CommonRepository
            HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) = rep.GetConfig(ModuleID)
        End Using
    End Sub

End Class
