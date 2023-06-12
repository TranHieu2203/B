Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDBInfoProfile
    Inherits Common.CommonView

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>MustAuthorize</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <summary>
    ''' birthdayRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim birthdayRemind As Integer

    ''' <summary>
    ''' contractRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim contractRemind As Integer

    ''' <summary>
    ''' workingRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim workingRemind As Integer

    ''' <summary>
    ''' terminateRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim terminateRemind As Integer

    ''' <summary>
    ''' terminateDebtRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim terminateDebtRemind As Integer

    ''' <summary>
    ''' noPaperRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim noPaperRemind As Integer

    ''' <summary>
    ''' visaRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim visaRemind As Integer

    ''' <summary>
    ''' worPermitRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim worPermitRemind As Integer

    ''' <summary>
    ''' certificateRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim certificateRemind As Integer

    ''' <summary>
    ''' authorityRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim authorityRemind As Integer

    ''' <summary>
    ''' probationRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim probationRemind As Integer

    ''' <summary>
    ''' retirementRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim retirementRemind As Integer

    ''' <summary>
    ''' maternitiRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim maternitiRemind As Integer

    ''' <summary>
    ''' noticePersonalIncomeTaxRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim noticePersonalIncomeTaxRemind As Integer

    ''' <summary>
    ''' width
    ''' </summary>
    ''' <remarks></remarks>
    Private width As Integer

    ''' <summary>
    ''' height
    ''' </summary>
    ''' <remarks></remarks>
    Private height As Integer

    ''' <summary>
    ''' title
    ''' </summary>
    ''' <remarks></remarks>
    Public title As String = ""

    ''' <summary>
    ''' name
    ''' </summary>
    ''' <remarks>Default : "Năm"</remarks>
    Public name As String = "Năm"

    ''' <summary>
    ''' data
    ''' </summary>
    ''' <remarks></remarks>
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Dashboard" + Me.GetType().Name.ToString()
    Dim _orgID As Decimal

#Region "Property"

    ''' <summary>
    ''' InfoList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InfoList As List(Of StatisticDTO)
        Get
            Return ViewState(Me.ID & "_InfoList")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            ViewState(Me.ID & "_InfoList") = value
        End Set
    End Property

    ''' <summary>
    ''' RemindList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemindList As List(Of ReminderLogDTO)
        Get
            Return PageViewState(Me.ID & "_RemindContractList")
        End Get

        Set(ByVal value As List(Of ReminderLogDTO))
            PageViewState(Me.ID & "_RemindContractList") = value
        End Set
    End Property

    ''' <summary>
    ''' StatisticData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property

    ''' <summary>
    ''' _filter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _filter As ReminderLogDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New ReminderLogDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get

        Set(ByVal value As ReminderLogDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            _orgID = 1
            If Request("orgid") IsNot Nothing Then
                _orgID = CInt(Request("orgid"))
            End If
            LoadConfig()

            If Not IsPostBack Or resize = 0 Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If

            GetInforRemind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Not IsPostBack Then
                ViewConfig(RadPane1)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter = New ReminderListDTO

        Try
            If InfoList Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                InfoList = rep.GetCompanyNewInfo(_orgID)
            End If
            rep.Dispose()
            lbtnEmpCount.Text = (From p In InfoList Where p.NAME = "EMP_COUNT" Select p.VALUE).FirstOrDefault
            lbtnEmpNew.Text = (From p In InfoList Where p.NAME = "EMP_NEW" Select p.VALUE).FirstOrDefault
            lbtnEmpTer.Text = (From p In InfoList Where p.NAME = "EMP_TER" Select p.VALUE).FirstOrDefault
            lbtnContractNew.Text = (From p In InfoList Where p.NAME = "CONTRACT_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferNew.Text = (From p In InfoList Where p.NAME = "TRANSFER_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferMove.Text = (From p In InfoList Where p.NAME = "TRANSFER_MOVE" Select p.VALUE).FirstOrDefault
            lbtnAgeAvg.Text = (From p In InfoList Where p.NAME = "AGE_AVG" Select p.VALUE).FirstOrDefault
            lbtnSeniority.Text = (From p In InfoList Where p.NAME = "SENIORITY_AVG" Select p.VALUE).FirstOrDefault
            Using reps As New ProfileRepository
                Dim lstReminder = reps.GetReminderList(_filter)

                Dim ReminderNoPaper = (From p In lstReminder Where p.ID = RemindConfigType.ExpireNoPaper).FirstOrDefault
                If ReminderNoPaper IsNot Nothing AndAlso ReminderNoPaper.STATUS = 1 Then
                    lbReminderPage.Text = ReminderNoPaper.REMINDER_NAME + ":"
                Else
                    lbReminderPage.Visible = False
                    lbReminder16.Visible = False
                    td16.Visible = False
                    td16_2.Visible = False
                    td16_3.Visible = False
                End If
                Dim ReminderWorking = (From p In lstReminder Where p.ID = RemindConfigType.ExpireWorking).FirstOrDefault
                If ReminderWorking IsNot Nothing AndAlso ReminderWorking.STATUS = 1 Then
                    lbToTrinh.Text = ReminderWorking.REMINDER_NAME + ":"
                Else
                    lbReminder13.Visible = False
                    lbToTrinh.Visible = False
                    td13.Visible = False
                    td13_2.Visible = False
                End If

                Dim ReminderExpAuthority = (From p In lstReminder Where p.ID = RemindConfigType.ExpAuthority).FirstOrDefault
                If ReminderExpAuthority IsNot Nothing AndAlso ReminderExpAuthority.STATUS = 1 Then
                    lbExpAuthority.Text = ReminderExpAuthority.REMINDER_NAME + ":"
                Else
                    lbnExpAuthority.Visible = False
                    lbExpAuthority.Visible = False
                    td38.Visible = False
                    td38_2.Visible = False
                    td38_3.Visible = False
                End If

                Dim ReminderVisa = (From p In lstReminder Where p.ID = RemindConfigType.ExpireVisa).FirstOrDefault
                If ReminderVisa IsNot Nothing AndAlso ReminderVisa.STATUS = 1 Then
                    lbVisa.Text = ReminderVisa.REMINDER_NAME + ":"
                Else
                    lbReminderVisa.Visible = False
                    lbVisa.Visible = False
                    td10.Visible = False
                    td10_2.Visible = False
                End If

                Dim ReminderContractDays = (From p In lstReminder Where p.ID = RemindConfigType.Contract).FirstOrDefault
                If ReminderContractDays IsNot Nothing AndAlso ReminderContractDays.STATUS = 1 Then
                    lbReminder.Text = ReminderContractDays.REMINDER_NAME + ":"
                Else
                    lbReminder1.Visible = False
                    lbReminder.Visible = False
                    td1.Visible = False
                    td1_2.Visible = False
                    td1_3.Visible = False
                End If

                Dim ReminderProbation = (From p In lstReminder Where p.ID = RemindConfigType.Probation).FirstOrDefault
                If ReminderProbation IsNot Nothing AndAlso ReminderProbation.STATUS = 1 Then
                    lbProbation.Text = ReminderProbation.REMINDER_NAME + ":"
                Else
                    lbnProbation.Visible = False
                    lbProbation.Visible = False
                    td20.Visible = False
                    td20_2.Visible = False
                End If

                Dim ReminderBirthdayDays = (From p In lstReminder Where p.ID = RemindConfigType.Birthday).FirstOrDefault
                If ReminderBirthdayDays IsNot Nothing AndAlso ReminderBirthdayDays.STATUS = 1 Then
                    lbReminderDay.Text = ReminderBirthdayDays.REMINDER_NAME + ":"
                Else
                    lbReminder2.Visible = False
                    lbReminderDay.Visible = False
                    td2.Visible = False
                    td2_2.Visible = False
                    td2_3.Visible = False
                End If

                Dim ReminderCertificate = (From p In lstReminder Where p.ID = RemindConfigType.ExpireCertificate).FirstOrDefault
                If ReminderCertificate IsNot Nothing AndAlso ReminderCertificate.STATUS = 1 Then
                    lbGPLD.Text = ReminderCertificate.REMINDER_NAME + ":"
                Else
                    lbReminder19.Visible = False
                    lbGPLD.Visible = False
                    td19.Visible = False
                    td19_2.Visible = False
                End If

                Dim ReminderMaternitiDays = (From p In lstReminder Where p.ID = RemindConfigType.Materniti).FirstOrDefault
                If ReminderMaternitiDays IsNot Nothing AndAlso ReminderMaternitiDays.STATUS = 1 Then
                    lbMaterniti.Text = ReminderMaternitiDays.REMINDER_NAME + ":"
                Else
                    lbnMaterniti.Visible = False
                    lbMaterniti.Visible = False
                    td24.Visible = False
                    td24_2.Visible = False
                    td24_3.Visible = False
                End If

                Dim ReminderRetirementDays = (From p In lstReminder Where p.ID = RemindConfigType.Retirement).FirstOrDefault
                If ReminderRetirementDays IsNot Nothing AndAlso ReminderRetirementDays.STATUS = 1 Then
                    lbRetirement.Text = ReminderRetirementDays.REMINDER_NAME + ":"
                Else
                    lbnRetirement.Visible = False
                    lbRetirement.Visible = False
                    td25.Visible = False
                    td25_2.Visible = False
                End If

                Dim ReminderNoticePersonalIncomeTax = (From p In lstReminder Where p.ID = RemindConfigType.NoticePersonalIncomeTax).FirstOrDefault
                If ReminderNoticePersonalIncomeTax IsNot Nothing AndAlso ReminderNoticePersonalIncomeTax.STATUS = 1 Then
                    lbNoticePersonalIncomeTax.Text = ReminderNoticePersonalIncomeTax.REMINDER_NAME + ":"
                Else
                    lbnNoticePersonalIncomeTax.Visible = False
                    lbNoticePersonalIncomeTax.Visible = False
                    td39.Visible = False
                    td39_2.Visible = False
                    td39_3.Visible = False
                End If

                Dim ReminderTerminate = (From p In lstReminder Where p.ID = RemindConfigType.ExpireTerminate).FirstOrDefault
                If ReminderTerminate IsNot Nothing AndAlso ReminderTerminate.STATUS = 1 Then
                    lbNVTT.Text = ReminderTerminate.REMINDER_NAME + ":"
                Else
                    lbReminder14.Visible = False
                    lbNVTT.Visible = False
                    td14.Visible = False
                    td14_2.Visible = False
                End If
            End Using
            'data = String.Empty
            'If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
            '    StatisticData = rep.GetStatisticSeniority()
            'End If
            'If (From p In StatisticData Where p.VALUE <> 0).Any Then
            '    For index As Integer = 0 To StatisticData.Count - 1
            '        If index = StatisticData.Count - 1 Then
            '            data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}"
            '        Else
            '            data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}," & vbNewLine
            '        End If
            '    Next
            'End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

#End Region

#Region "Custom"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Load data từ file config</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Sub LoadConfig()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            birthdayRemind = CommonConfig.ReminderBirthdayDays
            terminateRemind = CommonConfig.ReminderTerminate
            terminateDebtRemind = CommonConfig.ReminderTerminateDebt
            worPermitRemind = CommonConfig.ReminderLabor

            '-----
            noPaperRemind = CommonConfig.ReminderNoPaper
            workingRemind = CommonConfig.ReminderWorking
            authorityRemind = CommonConfig.ReminderExpAuthority
            visaRemind = CommonConfig.ReminderVisa
            contractRemind = CommonConfig.ReminderContractDays
            probationRemind = CommonConfig.ReminderProbation
            certificateRemind = CommonConfig.ReminderCertificate
            retirementRemind = CommonConfig.ReminderRetirementDays
            maternitiRemind = CommonConfig.ReminderMaternitiDays
            noticePersonalIncomeTaxRemind = CommonConfig.ReminderNoticePersonalIncomeTax

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Xuất ra file excel</summary>
    ''' <param name="grid"></param>
    ''' <remarks></remarks>
    Public Sub ExportToExcel(ByVal grid As RadGrid)
        Dim lstData As List(Of ReminderLogDTO)
        Dim _error As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using xls As New ExcelCommon
                Dim query = From p In RemindList
                If _filter.EMPLOYEE_CODE <> "" Then
                    query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
                End If

                If _filter.FULLNAME <> "" Then
                    query = query.Where(Function(f) f.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
                End If

                If _filter.REMIND_NAME <> "" Then
                    query = query.Where(Function(f) f.REMIND_NAME.ToUpper.Contains(_filter.REMIND_NAME.ToUpper))
                End If

                If _filter.TITLE_NAME <> "" Then
                    query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
                End If

                If _filter.ORG_NAME <> "" Then
                    query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If _filter.JOIN_DATE IsNot Nothing Then
                    query = query.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
                End If

                If _filter.REMIND_DATE IsNot Nothing Then
                    query = query.Where(Function(f) f.REMIND_DATE = _filter.REMIND_DATE)
                End If

                lstData = query.ToList
                Dim dtData = lstData.ToTable

                Dim bCheck = xls.ExportExcelTemplate(
                    Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                    "Reminder", dtData, Response, _error)

                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Lấy ra thông tin remind</summary>
    ''' <remarks></remarks>
    Protected Sub GetInforRemind()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New ProfileDashboardRepository
                Try
                    'If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                    RemindList = rep.GetRemind(probationRemind.ToString & "," &
                                                   contractRemind.ToString & "," &
                                                   "0," &
                                                   "0," &
                                                   noPaperRemind.ToString & "," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   maternitiRemind.ToString & "," &
                                                   retirementRemind.ToString & "," &
                                                   "0," &
                                                   certificateRemind.ToString & "," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   "0," &
                                                   visaRemind.ToString & "," &
                                                   workingRemind.ToString & "," &
                                                   authorityRemind.ToString & "," &
                                                   noticePersonalIncomeTaxRemind.ToString,
                                                   _orgID)

                        For Each item In RemindList
                            item.REMIND_NAME = Translate(item.REMIND_NAME)
                        Next
                    'End If
                    Dim query

                    query = (From p In RemindList Where p.REMIND_TYPE = 2).ToList
                    If query.count > 0 Then
                        lbReminder2.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 14).ToList
                    If query.count > 0 Then
                        lbReminder14.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 19).ToList
                    If query.count > 0 Then
                        lbReminder19.Text = query.count
                    End If

                    '----

                    query = (From p In RemindList Where p.REMIND_TYPE = 16).ToList
                    If query.count > 0 Then
                        lbReminder16.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 13).ToList
                    If query.count > 0 Then
                        lbReminder13.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 1).ToList
                    If query.count > 0 Then
                        lbReminder1.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 20).ToList
                    If query.count > 0 Then
                        lbnProbation.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 25).ToList
                    If query.count > 0 Then
                        lbnRetirement.Text = query.count
                    End If

                    query = (From p In RemindList Where p.REMIND_TYPE = 24).ToList
                    If query.count > 0 Then
                        lbnMaterniti.Text = query.count
                    End If
                Catch ex As Exception
                    rep.Dispose()
                    'DisplayException(Me.ViewName, Me.ID, ex)
                    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                End Try
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class