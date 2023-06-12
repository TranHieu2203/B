Imports Common
Imports Common.CommonBusiness
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlRemindConfig
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
#End Region

#Region "Page"
    ''' <summary>
    ''' Create menu toolbar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            If Not IsPostBack Then
                ViewConfig(RadPane1)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao, load page, update state page, control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' update state luon la edit cho page, enable control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_EDIT
            Me.ChangeToolbarState()
            rntxtCONTRACT.Enabled = chkCONTRACT.Checked
            rntxtBIRTHDAY.Enabled = chkBIRTHDAY.Checked
            rntxtVISA.Enabled = chkVisa.Checked

            rntxtWORKING.Enabled = chkWorking.Checked
            rntxtTERMINATE.Enabled = chkTerminate.Checked
            rntxtTERMINATEDEBT.Enabled = chkTerminateDebt.Checked
            rntxtNOPAPER.Enabled = chkNoPaper.Checked
            rntxtCERTIFICATE.Enabled = chkCertificate.Checked
            rntxtLABOR.Enabled = chkLabor.Checked

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reset page, check value du lieu neu = 0 thi uncheck group tuong ung va disable textbox do
    ''' </summary>
    ''' <param name="Message">Ko su dung</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Dim _filter = New ReminderListDTO
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CommonConfig.GetReminderConfigFromDatabase()

            End If
            _filter.IS_LOAD_ALL = True

            Dim lstReminder = rep.GetReminderList(_filter)

            Dim ReminderInsArising = (From p In lstReminder Where p.ID = RemindConfigType.InsArising).FirstOrDefault
            If ReminderInsArising IsNot Nothing AndAlso ReminderInsArising.STATUS = 1 Then
                If CommonConfig.ReminderInsArising = 0 Then
                    chkInsArising.Checked = False
                Else
                    chkInsArising.Checked = True
                End If
                chkInsArising.Text = ReminderInsArising.REMINDER_NAME
            Else
                Panel4.Visible = False
            End If

            Dim ReminderSign = (From p In lstReminder Where p.ID = RemindConfigType.Sign).FirstOrDefault
            If ReminderSign IsNot Nothing AndAlso ReminderSign.STATUS = 1 Then
                If CommonConfig.ReminderSign = 0 Then
                    chkSign.Checked = False
                Else
                    chkSign.Checked = True
                End If
                chkSign.Text = ReminderSign.REMINDER_NAME
            Else
                Panel5.Visible = False
            End If

            Dim ReminderBHYT = (From p In lstReminder Where p.ID = RemindConfigType.BHYT).FirstOrDefault
            If ReminderBHYT IsNot Nothing AndAlso ReminderBHYT.STATUS = 1 Then
                If CommonConfig.ReminderBHYT = 0 Then
                    chkBHYT.Checked = False
                Else
                    chkBHYT.Checked = True
                End If
                chkBHYT.Text = ReminderBHYT.REMINDER_NAME
            Else
                Panel6.Visible = False
            End If

            Dim ReminderOverRegDate = (From p In lstReminder Where p.ID = RemindConfigType.OverRegDate).FirstOrDefault
            If ReminderOverRegDate IsNot Nothing AndAlso ReminderOverRegDate.STATUS = 1 Then
                If CommonConfig.ReminderOverRegDate = 0 Then
                    chkOverRegDate.Checked = False
                Else
                    chkOverRegDate.Checked = True
                End If
                chkOverRegDate.Text = ReminderOverRegDate.REMINDER_NAME
            Else
                Panel1.Visible = False
            End If

            Dim ReminderAge18 = (From p In lstReminder Where p.ID = RemindConfigType.Age18).FirstOrDefault
            If ReminderAge18 IsNot Nothing AndAlso ReminderAge18.STATUS = 1 Then
                If CommonConfig.ReminderAge18 = 0 Then
                    chkAge18.Checked = False
                    rnAge18.Enabled = False
                    rnAge18.Value = Nothing
                Else
                    chkAge18.Checked = True
                    rnAge18.Enabled = True
                    rnAge18.Value = CommonConfig.ReminderAge18
                End If
                chkAge18.Text = ReminderAge18.REMINDER_NAME
            Else
                Panel7.Visible = False
            End If

            Dim ReminderProbation = (From p In lstReminder Where p.ID = RemindConfigType.Probation).FirstOrDefault
            If ReminderProbation IsNot Nothing AndAlso ReminderProbation.STATUS = 1 Then
                If CommonConfig.ReminderProbation = 0 Then
                    chkProbation.Checked = False
                    rntxtProbation.Enabled = False
                    rntxtProbation.Value = Nothing
                Else
                    chkProbation.Checked = True
                    rntxtProbation.Enabled = True
                    rntxtProbation.Value = CommonConfig.ReminderProbation
                End If
                chkProbation.Text = ReminderProbation.REMINDER_NAME
            Else
                radProbation.Visible = False
            End If


            Dim ReminderContractDays = (From p In lstReminder Where p.ID = RemindConfigType.Contract).FirstOrDefault
            If ReminderContractDays IsNot Nothing AndAlso ReminderContractDays.STATUS = 1 Then
                If CommonConfig.ReminderContractDays = 0 Then
                    chkCONTRACT.Checked = False
                    rntxtCONTRACT.Enabled = False
                    rntxtCONTRACT.Value = Nothing
                Else
                    chkCONTRACT.Checked = True
                    rntxtCONTRACT.Enabled = True
                    rntxtCONTRACT.Value = CommonConfig.ReminderContractDays
                End If
                chkCONTRACT.Text = ReminderContractDays.REMINDER_NAME
            Else
                radCONTRACT.Visible = False
            End If

            Dim ReminderBirthdayDays = (From p In lstReminder Where p.ID = RemindConfigType.Birthday).FirstOrDefault
            If ReminderBirthdayDays IsNot Nothing AndAlso ReminderBirthdayDays.STATUS = 1 Then
                If CommonConfig.ReminderBirthdayDays = 0 Then
                    chkBIRTHDAY.Checked = False
                    rntxtBIRTHDAY.Enabled = False
                    rntxtBIRTHDAY.Value = Nothing
                Else
                    chkBIRTHDAY.Checked = True
                    rntxtBIRTHDAY.Enabled = True
                    rntxtBIRTHDAY.Value = CommonConfig.ReminderBirthdayDays
                End If
                chkBIRTHDAY.Text = ReminderBirthdayDays.REMINDER_NAME
            Else
                radBIRTHDAY.Visible = False
            End If

            Dim ReminderVisa = (From p In lstReminder Where p.ID = RemindConfigType.ExpireVisa).FirstOrDefault
            If ReminderVisa IsNot Nothing AndAlso ReminderVisa.STATUS = 1 Then
                If CommonConfig.ReminderVisa = 0 Then
                    chkVisa.Checked = False
                    rntxtVISA.Enabled = False
                    rntxtVISA.Value = Nothing
                Else
                    chkVisa.Checked = True
                    rntxtVISA.Enabled = True
                    rntxtVISA.Value = CommonConfig.ReminderVisa
                End If
                chkVisa.Text = ReminderVisa.REMINDER_NAME
            Else
                radVISA.Visible = False
            End If

            Dim ReminderWorking = (From p In lstReminder Where p.ID = RemindConfigType.ExpireWorking).FirstOrDefault
            If ReminderWorking IsNot Nothing AndAlso ReminderWorking.STATUS = 1 Then
                If CommonConfig.ReminderWorking = 0 Then
                    chkWorking.Checked = False
                    rntxtWORKING.Enabled = False
                    rntxtWORKING.Value = Nothing
                Else
                    chkWorking.Checked = True
                    rntxtWORKING.Enabled = True
                    rntxtWORKING.Value = CommonConfig.ReminderWorking
                End If
                chkWorking.Text = ReminderWorking.REMINDER_NAME
            Else
                radWORKING.Visible = False
            End If

            Dim ReminderTerminate = (From p In lstReminder Where p.ID = RemindConfigType.ExpireTerminate).FirstOrDefault
            If ReminderTerminate IsNot Nothing AndAlso ReminderTerminate.STATUS = 1 Then
                If CommonConfig.ReminderTerminate = 0 Then
                    chkTerminate.Checked = False
                    rntxtTERMINATE.Enabled = False
                    rntxtTERMINATE.Value = Nothing
                Else
                    chkTerminate.Checked = True
                    rntxtTERMINATE.Enabled = True
                    rntxtTERMINATE.Value = CommonConfig.ReminderTerminate
                End If
                chkTerminate.Text = ReminderTerminate.REMINDER_NAME
            Else
                radTERMINATE.Visible = False
            End If

            Dim ReminderTerminateDebt = (From p In lstReminder Where p.ID = RemindConfigType.ExpireTerminateDebt).FirstOrDefault
            If ReminderTerminateDebt IsNot Nothing AndAlso ReminderTerminateDebt.STATUS = 1 Then
                If CommonConfig.ReminderTerminateDebt = 0 Then
                    chkTerminateDebt.Checked = False
                    rntxtTERMINATEDEBT.Enabled = False
                    rntxtTERMINATEDEBT.Value = Nothing
                Else
                    chkTerminateDebt.Checked = True
                    rntxtTERMINATEDEBT.Enabled = True
                    rntxtTERMINATEDEBT.Value = CommonConfig.ReminderTerminateDebt
                End If
                chkTerminateDebt.Text = ReminderTerminateDebt.REMINDER_NAME
            Else
                radTERMINATEDEBT.Visible = False
            End If

            Dim ReminderNoPaper = (From p In lstReminder Where p.ID = RemindConfigType.ExpireNoPaper).FirstOrDefault
            If ReminderNoPaper IsNot Nothing AndAlso ReminderNoPaper.STATUS = 1 Then
                If CommonConfig.ReminderNoPaper = 0 Then
                    chkNoPaper.Checked = False
                    rntxtNOPAPER.Enabled = False
                    rntxtNOPAPER.Value = Nothing
                Else
                    chkNoPaper.Checked = True
                    rntxtNOPAPER.Enabled = True
                    rntxtNOPAPER.Value = CommonConfig.ReminderNoPaper
                End If
                chkNoPaper.Text = ReminderNoPaper.REMINDER_NAME
            Else
                radNOPAPER.Visible = False
            End If

            Dim ReminderCertificate = (From p In lstReminder Where p.ID = RemindConfigType.ExpireCertificate).FirstOrDefault
            If ReminderCertificate IsNot Nothing AndAlso ReminderCertificate.STATUS = 1 Then
                If CommonConfig.ReminderCertificate = 0 Then
                    chkCertificate.Checked = False
                    rntxtCERTIFICATE.Enabled = False
                    rntxtCERTIFICATE.Value = Nothing
                Else
                    chkCertificate.Checked = True
                    rntxtCERTIFICATE.Enabled = True
                    rntxtCERTIFICATE.Value = CommonConfig.ReminderCertificate
                End If
                chkCertificate.Text = ReminderCertificate.REMINDER_NAME
            Else
                radCERTIFICATE.Visible = False
            End If

            Dim ReminderLabor = (From p In lstReminder Where p.ID = RemindConfigType.ExpireLabor).FirstOrDefault
            If ReminderLabor IsNot Nothing AndAlso ReminderLabor.STATUS = 1 Then
                If CommonConfig.ReminderLabor = 0 Then
                    chkLabor.Checked = False
                    rntxtLABOR.Enabled = False
                    rntxtLABOR.Value = Nothing
                Else
                    chkLabor.Checked = True
                    rntxtLABOR.Enabled = True
                    rntxtLABOR.Value = CommonConfig.ReminderLabor
                End If
                chkLabor.Text = ReminderLabor.REMINDER_NAME
            Else
                radLABOR.Visible = False
            End If

            Dim ReminderApproveDays = (From p In lstReminder Where p.ID = RemindConfigType.Approve).FirstOrDefault
            If ReminderApproveDays IsNot Nothing AndAlso ReminderApproveDays.STATUS = 1 Then
                If CommonConfig.ReminderApproveDays = 0 Then
                    chkApprove.Checked = False
                    rntxtApprove.Enabled = False
                    rntxtApprove.Value = Nothing
                Else
                    chkApprove.Checked = True
                    rntxtApprove.Enabled = True
                    rntxtApprove.Value = CommonConfig.ReminderApproveDays
                End If
                chkApprove.Text = ReminderApproveDays.REMINDER_NAME
            Else
                radApprove.Visible = False
            End If

            Dim ReminderApproveHDLDDays = (From p In lstReminder Where p.ID = RemindConfigType.ApproveHDLD).FirstOrDefault
            If ReminderApproveHDLDDays IsNot Nothing AndAlso ReminderApproveHDLDDays.STATUS = 1 Then
                If CommonConfig.ReminderApproveHDLDDays = 0 Then
                    chkApproveHDLD.Checked = False
                    rntxtApproveHDLD.Enabled = False
                    rntxtApproveHDLD.Value = Nothing
                Else
                    chkApproveHDLD.Checked = True
                    rntxtApproveHDLD.Enabled = True
                    rntxtApproveHDLD.Value = CommonConfig.ReminderApproveHDLDDays
                End If
                chkApproveHDLD.Text = ReminderApproveHDLDDays.REMINDER_NAME
            Else
                radApproveHDLD.Visible = False
            End If

            Dim ReminderApproveTHHDDays = (From p In lstReminder Where p.ID = RemindConfigType.ApprovetTHHD).FirstOrDefault
            If ReminderApproveTHHDDays IsNot Nothing AndAlso ReminderApproveTHHDDays.STATUS = 1 Then
                If CommonConfig.ReminderApproveTHHDDays = 0 Then
                    chkApproveTHHD.Checked = False
                    rntxtApproveTHHD.Enabled = False
                    rntxtApproveTHHD.Value = Nothing
                Else
                    chkApproveTHHD.Checked = True
                    rntxtApproveTHHD.Enabled = True
                    rntxtApproveTHHD.Value = CommonConfig.ReminderApproveTHHDDays
                End If
                chkApproveTHHD.Text = ReminderApproveTHHDDays.REMINDER_NAME
            Else
                radApproveTHHD.Visible = False
            End If

            Dim ReminderMaternitiDays = (From p In lstReminder Where p.ID = RemindConfigType.Materniti).FirstOrDefault
            If ReminderMaternitiDays IsNot Nothing AndAlso ReminderMaternitiDays.STATUS = 1 Then
                If CommonConfig.ReminderMaternitiDays = 0 Then
                    chkMaterniti.Checked = False
                    rntxtMaterniti.Enabled = False
                    rntxtMaterniti.Value = Nothing
                Else
                    chkMaterniti.Checked = True
                    rntxtMaterniti.Enabled = True
                    rntxtMaterniti.Value = CommonConfig.ReminderMaternitiDays
                End If
                chkMaterniti.Text = ReminderMaternitiDays.REMINDER_NAME
            Else
                radMaterniti.Visible = False
            End If

            Dim ReminderRetirementDays = (From p In lstReminder Where p.ID = RemindConfigType.Retirement).FirstOrDefault
            If ReminderRetirementDays IsNot Nothing AndAlso ReminderRetirementDays.STATUS = 1 Then
                If CommonConfig.ReminderRetirementDays = 0 Then
                    chkRetirement.Checked = False
                    rntxtRetirement.Enabled = False
                    rntxtRetirement.Value = Nothing
                Else
                    chkRetirement.Checked = True
                    rntxtRetirement.Enabled = True
                    rntxtRetirement.Value = CommonConfig.ReminderRetirementDays
                End If
                chkRetirement.Text = ReminderRetirementDays.REMINDER_NAME
            Else
                radRetirement.Visible = False
            End If

            Dim ReminderNoneSalaryDays = (From p In lstReminder Where p.ID = RemindConfigType.NoneSalary).FirstOrDefault
            If ReminderNoneSalaryDays IsNot Nothing AndAlso ReminderNoneSalaryDays.STATUS = 1 Then
                If CommonConfig.ReminderNoneSalaryDays = 0 Then
                    chkNoneSalary.Checked = False
                    rntxtNoneSalary.Enabled = False
                    rntxtNoneSalary.Value = Nothing
                Else
                    chkNoneSalary.Checked = True
                    rntxtNoneSalary.Enabled = True
                    rntxtNoneSalary.Value = CommonConfig.ReminderNoneSalaryDays
                End If
                chkNoneSalary.Text = ReminderNoneSalaryDays.REMINDER_NAME
            Else
                radNoneSalary.Visible = False
            End If


            Dim ReminderExpiredCertificate = (From p In lstReminder Where p.ID = RemindConfigType.ExpiredCertificate).FirstOrDefault
            If ReminderExpiredCertificate IsNot Nothing AndAlso ReminderExpiredCertificate.STATUS = 1 Then
                If CommonConfig.ReminderExpiredCertificate = 0 Then
                    chkExpiredCertificate.Checked = False
                    rntxtExpiredCertificate.Enabled = False
                    rntxtExpiredCertificate.Value = Nothing
                Else
                    chkExpiredCertificate.Checked = True
                    rntxtExpiredCertificate.Enabled = True
                    rntxtExpiredCertificate.Value = CommonConfig.ReminderExpiredCertificate
                End If
                chkExpiredCertificate.Text = ReminderExpiredCertificate.REMINDER_NAME
            Else
                radExpiredCertificate.Visible = False
            End If

            Dim ReminderBIRTHDAY_LD = (From p In lstReminder Where p.ID = RemindConfigType.BIRTHDAY_LD).FirstOrDefault
            If ReminderBIRTHDAY_LD IsNot Nothing AndAlso ReminderBIRTHDAY_LD.STATUS = 1 Then
                If CommonConfig.ReminderBIRTHDAY_LD = 0 Then
                    chkBIRTHDAY_LD.Checked = False
                    rntxtBIRTHDAY_LD.Enabled = False
                    rntxtBIRTHDAY_LD.Value = Nothing
                Else
                    chkBIRTHDAY_LD.Checked = True
                    rntxtBIRTHDAY_LD.Enabled = True
                    rntxtBIRTHDAY_LD.Value = CommonConfig.ReminderBIRTHDAY_LD
                End If
                chkBIRTHDAY_LD.Text = ReminderBIRTHDAY_LD.REMINDER_NAME
            Else
                radBIRTHDAY_LD.Visible = False
            End If

            Dim ReminderConcurrently = (From p In lstReminder Where p.ID = RemindConfigType.Concurrently).FirstOrDefault
            If ReminderConcurrently IsNot Nothing AndAlso ReminderConcurrently.STATUS = 1 Then
                If CommonConfig.ReminderConcurrently = 0 Then
                    chkConcurrently.Checked = False
                    rntxtConcurrently.Enabled = False
                    rntxtConcurrently.Value = Nothing
                Else
                    chkConcurrently.Checked = True
                    rntxtConcurrently.Enabled = True
                    rntxtConcurrently.Value = CommonConfig.ReminderConcurrently
                End If
                chkConcurrently.Text = ReminderConcurrently.REMINDER_NAME
            Else
                radConcurrently.Visible = False
            End If

            Dim ReminderEmpDtlFamily = (From p In lstReminder Where p.ID = RemindConfigType.EmpDtlFamily).FirstOrDefault
            If ReminderEmpDtlFamily IsNot Nothing AndAlso ReminderEmpDtlFamily.STATUS = 1 Then
                If CommonConfig.ReminderEmpDtlFamily = 0 Then
                    chkEmpDtlFamily.Checked = False
                    rntxtEmpDtlFamily.Enabled = False
                    rntxtEmpDtlFamily.Value = Nothing
                Else
                    chkEmpDtlFamily.Checked = True
                    rntxtEmpDtlFamily.Enabled = True
                    rntxtEmpDtlFamily.Value = CommonConfig.ReminderEmpDtlFamily
                End If
                chkEmpDtlFamily.Text = ReminderEmpDtlFamily.REMINDER_NAME
            Else
                radEmpDtlFamily.Visible = False
            End If

            Dim ReminderExpContrat = (From p In lstReminder Where p.ID = RemindConfigType.ExpContrat).FirstOrDefault
            If ReminderExpContrat IsNot Nothing AndAlso ReminderExpContrat.STATUS = 1 Then
                If CommonConfig.ReminderExpContrat = 0 Then
                    chkExpContrat.Checked = False
                    rnExpContrat.Enabled = False
                    rnExpContrat.Value = Nothing
                Else
                    chkExpContrat.Checked = True
                    rnExpContrat.Enabled = True
                    rnExpContrat.Value = CommonConfig.ReminderExpContrat
                End If
                chkExpContrat.Text = ReminderExpContrat.REMINDER_NAME
            Else
                Panel2.Visible = False
            End If

            Dim ReminderExpDiscipline = (From p In lstReminder Where p.ID = RemindConfigType.ExpDiscipline).FirstOrDefault
            If ReminderExpDiscipline IsNot Nothing AndAlso ReminderExpDiscipline.STATUS = 1 Then
                If CommonConfig.ReminderExpDiscipline = 0 Then
                    chkExpDiscipline.Checked = False
                    rnExpDiscipline.Enabled = False
                    rnExpDiscipline.Value = Nothing
                Else
                    chkExpDiscipline.Checked = True
                    rnExpDiscipline.Enabled = True
                    rnExpDiscipline.Value = CommonConfig.ReminderExpDiscipline
                End If
                chkExpDiscipline.Text = ReminderExpDiscipline.REMINDER_NAME
            Else
                Panel3.Visible = False
            End If

            Dim ReminderExpAuthority = (From p In lstReminder Where p.ID = RemindConfigType.ExpAuthority).FirstOrDefault
            If ReminderExpAuthority IsNot Nothing AndAlso ReminderExpAuthority.STATUS = 1 Then
                If CommonConfig.ReminderExpAuthority = 0 Then
                    chkExpAuthority.Checked = False
                    rnExpAuthority.Enabled = False
                    rnExpAuthority.Value = Nothing
                Else
                    chkExpAuthority.Checked = True
                    rnExpAuthority.Enabled = True
                    rnExpAuthority.Value = CommonConfig.ReminderExpAuthority
                End If
                chkExpAuthority.Text = ReminderExpAuthority.REMINDER_NAME
            Else
                radExpAuthority.Visible = False
            End If

            Dim ReminderNoticePersonalIncomeTax = (From p In lstReminder Where p.ID = RemindConfigType.NoticePersonalIncomeTax).FirstOrDefault
            If ReminderNoticePersonalIncomeTax IsNot Nothing AndAlso ReminderNoticePersonalIncomeTax.STATUS = 1 Then
                If CommonConfig.ReminderNoticePersonalIncomeTax = 0 Then
                    chkNoticePersonalIncomeTax.Checked = False
                    rnNoticePersonalIncomeTax.Enabled = False
                    rnNoticePersonalIncomeTax.Value = Nothing
                Else
                    chkNoticePersonalIncomeTax.Checked = True
                    rnNoticePersonalIncomeTax.Enabled = True
                    rnNoticePersonalIncomeTax.Value = CommonConfig.ReminderNoticePersonalIncomeTax
                End If
                chkNoticePersonalIncomeTax.Text = ReminderNoticePersonalIncomeTax.REMINDER_NAME
            Else
                radNoticePersonalIncomeTax.Visible = False
            End If

            Dim ReminderNoticeStock = (From p In lstReminder Where p.ID = RemindConfigType.NoticeStock).FirstOrDefault
            If ReminderNoticeStock IsNot Nothing AndAlso ReminderNoticeStock.STATUS = 1 Then
                If CommonConfig.ReminderNoticeStock = 0 Then
                    chkNoticeStock.Checked = False
                    rnNoticeStock.Enabled = False
                    rnNoticeStock.Value = Nothing
                Else
                    chkNoticeStock.Checked = True
                    rnNoticeStock.Enabled = True
                    rnNoticeStock.Value = CommonConfig.ReminderNoticeStock
                End If
                chkNoticeStock.Text = ReminderNoticeStock.REMINDER_NAME

                If Common.Common.GetUsername.ToUpper <> "ADMIN" Then
                    Using reps As New CommonRepository
                        Dim permissions As List(Of PermissionDTO) = reps.GetUserPermissions(Common.Common.GetUsername)
                        If permissions IsNot Nothing Then
                            Dim isPermissions = (From p In permissions Where p.FID.ToLower = "ctrlHU_StocksTransaction".ToLower And p.IS_REPORT = False).Any
                            If Not isPermissions Then
                                radNoticeStock.Visible = False
                            End If
                        End If
                    End Using
                End If
            Else
                radNoticeStock.Visible = False
            End If

            Dim ReminderApproveHSNV = (From p In lstReminder Where p.ID = RemindConfigType.ApproveHSNV).FirstOrDefault
            If ReminderApproveHSNV IsNot Nothing AndAlso ReminderApproveHSNV.STATUS = 1 Then
                If CommonConfig.ReminderApproveHSNV = 0 Then
                    chkApproveHSNV.Checked = False
                Else
                    chkApproveHSNV.Checked = True
                End If
                chkApproveHSNV.Text = ReminderApproveHSNV.REMINDER_NAME
            Else
                radApproveHSNV.Visible = False
            End If

            Dim ReminderApproveWorkBefore = (From p In lstReminder Where p.ID = RemindConfigType.ApproveWorkBefore).FirstOrDefault
            If ReminderApproveWorkBefore IsNot Nothing AndAlso ReminderApproveWorkBefore.STATUS = 1 Then
                If CommonConfig.ReminderApproveWorkBefore = 0 Then
                    chkApproveWorkBefore.Checked = False
                Else
                    chkApproveWorkBefore.Checked = True
                End If
                chkApproveWorkBefore.Text = ReminderApproveWorkBefore.REMINDER_NAME
            Else
                radApproveWorkBefore.Visible = False
            End If

            Dim ReminderApproveCertificate = (From p In lstReminder Where p.ID = RemindConfigType.ApproveCertificate).FirstOrDefault
            If ReminderApproveCertificate IsNot Nothing AndAlso ReminderApproveCertificate.STATUS = 1 Then
                If CommonConfig.ReminderApproveCertificate = 0 Then
                    chkApproveCertificate.Checked = False
                Else
                    chkApproveCertificate.Checked = True
                End If
                chkApproveCertificate.Text = ReminderApproveCertificate.REMINDER_NAME
            Else
                radApproveCertificate.Visible = False
            End If

            Dim ReminderApproveFamily = (From p In lstReminder Where p.ID = RemindConfigType.ApproveFamily).FirstOrDefault
            If ReminderApproveFamily IsNot Nothing AndAlso ReminderApproveFamily.STATUS = 1 Then
                If CommonConfig.ReminderApproveCertificate = 0 Then
                    chkApproveFamily.Checked = False
                Else
                    chkApproveFamily.Checked = True
                End If
                chkApproveFamily.Text = ReminderApproveFamily.REMINDER_NAME
            Else
                radApproveFamily.Visible = False
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' event click nut luu, huy
    ''' update trang thai page, control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If chkApproveHSNV.Checked Then
                            CommonConfig.ReminderApproveHSNV = 2
                        Else
                            CommonConfig.ReminderApproveHSNV = 0
                        End If
                        If chkApproveWorkBefore.Checked Then
                            CommonConfig.ReminderApproveWorkBefore = 2
                        Else
                            CommonConfig.ReminderApproveWorkBefore = 0
                        End If
                        If chkApproveFamily.Checked Then
                            CommonConfig.ReminderApproveFamily = 2
                        Else
                            CommonConfig.ReminderApproveFamily = 0
                        End If
                        If chkApproveCertificate.Checked Then
                            CommonConfig.ReminderApproveCertificate = 2
                        Else
                            CommonConfig.ReminderApproveCertificate = 0
                        End If
                        If chkInsArising.Checked Then
                            CommonConfig.ReminderInsArising = 2
                        Else
                            CommonConfig.ReminderInsArising = 0
                        End If

                        If chkSign.Checked Then
                            CommonConfig.ReminderSign = 2
                        Else
                            CommonConfig.ReminderSign = 0
                        End If
                        If chkBHYT.Checked Then
                            CommonConfig.ReminderBHYT = 2
                        Else
                            CommonConfig.ReminderBHYT = 0
                        End If
                        ''-=======
                        If chkOverRegDate.Checked Then
                            CommonConfig.ReminderOverRegDate = 2
                        Else
                            CommonConfig.ReminderOverRegDate = 0
                        End If
                        If chkProbation.Checked Then
                            CommonConfig.ReminderProbation = rntxtProbation.Value
                            rntxtProbation.Enabled = True
                        Else
                            CommonConfig.ReminderProbation = 0
                            rntxtProbation.Enabled = False
                        End If
                        If chkCONTRACT.Checked Then
                            CommonConfig.ReminderContractDays = rntxtCONTRACT.Value
                            rntxtCONTRACT.Enabled = True
                        Else
                            CommonConfig.ReminderContractDays = 0
                            rntxtCONTRACT.Enabled = False
                        End If
                        If chkBIRTHDAY.Checked Then
                            CommonConfig.ReminderBirthdayDays = rntxtBIRTHDAY.Value
                            rntxtBIRTHDAY.Enabled = True
                        Else
                            CommonConfig.ReminderBirthdayDays = 0
                            rntxtBIRTHDAY.Enabled = True
                        End If


                        If chkVisa.Checked Then
                            If rntxtVISA.Value IsNot Nothing Then
                                CommonConfig.ReminderVisa = rntxtVISA.Value
                            End If

                            rntxtVISA.Enabled = True
                        Else
                            CommonConfig.ReminderVisa = 0
                            rntxtVISA.Enabled = True
                        End If
                        If chkWorking.Checked Then
                            If rntxtWORKING.Value IsNot Nothing Then
                                CommonConfig.ReminderWorking = rntxtWORKING.Value
                            End If
                            rntxtWORKING.Enabled = True
                        Else
                            CommonConfig.ReminderWorking = 0
                            rntxtWORKING.Enabled = True
                        End If
                        If chkTerminate.Checked Then
                            If rntxtTERMINATE.Value IsNot Nothing Then
                                CommonConfig.ReminderTerminate = rntxtTERMINATE.Value
                            End If

                            rntxtTERMINATE.Enabled = True
                        Else
                            CommonConfig.ReminderTerminate = 0
                            rntxtTERMINATE.Enabled = True
                        End If
                        If chkTerminateDebt.Checked Then
                            If rntxtTERMINATEDEBT.Value IsNot Nothing Then
                                CommonConfig.ReminderTerminateDebt = rntxtTERMINATEDEBT.Value
                            End If

                            rntxtTERMINATEDEBT.Enabled = True
                        Else
                            CommonConfig.ReminderTerminateDebt = 0
                            rntxtTERMINATEDEBT.Enabled = True
                        End If
                        If chkNoPaper.Checked Then
                            If rntxtNOPAPER.Value IsNot Nothing Then
                                CommonConfig.ReminderNoPaper = rntxtNOPAPER.Value
                            End If

                            rntxtNOPAPER.Enabled = True
                        Else
                            CommonConfig.ReminderNoPaper = 0
                            rntxtNOPAPER.Enabled = True
                        End If


                        If chkCertificate.Checked Then
                            If rntxtCERTIFICATE.Value IsNot Nothing Then
                                CommonConfig.ReminderCertificate = rntxtCERTIFICATE.Value
                            End If

                            rntxtCERTIFICATE.Enabled = True
                        Else
                            CommonConfig.ReminderCertificate = 0
                            rntxtCERTIFICATE.Enabled = True
                        End If

                        If chkLabor.Checked Then
                            If rntxtLABOR.Value IsNot Nothing Then
                                CommonConfig.ReminderLabor = rntxtLABOR.Value
                            End If

                            rntxtLABOR.Enabled = True
                        Else
                            CommonConfig.ReminderLabor = 0
                            rntxtLABOR.Enabled = True

                        End If
                        If chkApprove.Checked Then
                            CommonConfig.ReminderApproveDays = rntxtApprove.Value
                            rntxtApprove.Enabled = True
                        Else
                            CommonConfig.ReminderApproveDays = 0
                            rntxtApprove.Enabled = False
                        End If

                        If chkApproveHDLD.Checked Then
                            CommonConfig.ReminderApproveHDLDDays = rntxtApproveHDLD.Value
                            rntxtApproveHDLD.Enabled = True
                        Else
                            CommonConfig.ReminderApproveHDLDDays = 0
                            rntxtApproveHDLD.Enabled = False
                        End If

                        If chkApproveTHHD.Checked Then
                            CommonConfig.ReminderApproveTHHDDays = rntxtApproveTHHD.Value
                            rntxtApproveTHHD.Enabled = True
                        Else
                            CommonConfig.ReminderApproveTHHDDays = 0
                            rntxtApproveTHHD.Enabled = False
                        End If
                        If chkMaterniti.Checked Then
                            CommonConfig.ReminderMaternitiDays = rntxtMaterniti.Value
                            rntxtMaterniti.Enabled = True
                        Else
                            CommonConfig.ReminderMaternitiDays = 0
                            rntxtMaterniti.Enabled = False
                        End If
                        If chkRetirement.Checked Then
                            CommonConfig.ReminderRetirementDays = rntxtRetirement.Value
                            rntxtRetirement.Enabled = True
                        Else
                            CommonConfig.ReminderRetirementDays = 0
                            rntxtRetirement.Enabled = False
                        End If
                        If chkNoneSalary.Checked Then
                            CommonConfig.ReminderNoneSalaryDays = rntxtNoneSalary.Value
                            rntxtNoneSalary.Enabled = True
                        Else
                            CommonConfig.ReminderNoneSalaryDays = 0
                            rntxtNoneSalary.Enabled = False
                        End If

                        If chkExpiredCertificate.Checked Then
                            CommonConfig.ReminderExpiredCertificate = rntxtExpiredCertificate.Value
                            rntxtExpiredCertificate.Enabled = True
                        Else
                            CommonConfig.ReminderExpiredCertificate = 0
                            rntxtExpiredCertificate.Enabled = False
                        End If

                        If chkBIRTHDAY_LD.Checked Then
                            CommonConfig.ReminderBIRTHDAY_LD = rntxtBIRTHDAY_LD.Value
                            rntxtBIRTHDAY_LD.Enabled = True
                        Else
                            CommonConfig.ReminderBIRTHDAY_LD = 0
                            rntxtBIRTHDAY_LD.Enabled = False
                        End If

                        If chkConcurrently.Checked Then
                            CommonConfig.ReminderConcurrently = rntxtConcurrently.Value
                            rntxtConcurrently.Enabled = True
                        Else
                            CommonConfig.ReminderConcurrently = 0
                            rntxtConcurrently.Enabled = False
                        End If

                        If chkEmpDtlFamily.Checked Then
                            CommonConfig.ReminderEmpDtlFamily = rntxtEmpDtlFamily.Value
                            rntxtEmpDtlFamily.Enabled = True
                        Else
                            CommonConfig.ReminderEmpDtlFamily = 0
                            rntxtEmpDtlFamily.Enabled = False
                        End If
                        ''========
                        If chkExpContrat.Checked Then
                            CommonConfig.ReminderExpContrat = rnExpContrat.Value
                            rnExpContrat.Enabled = True
                        Else
                            CommonConfig.ReminderExpContrat = 0
                            rnExpContrat.Enabled = False
                        End If

                        If chkExpDiscipline.Checked Then
                            CommonConfig.ReminderExpDiscipline = rnExpDiscipline.Value
                            rnExpDiscipline.Enabled = True
                        Else
                            CommonConfig.ReminderExpDiscipline = 0
                            rnExpDiscipline.Enabled = False
                        End If


                        If chkAge18.Checked Then
                            If rnAge18.Value IsNot Nothing Then
                                CommonConfig.ReminderAge18 = rnAge18.Value
                            End If

                            rnAge18.Enabled = True
                        Else
                            CommonConfig.ReminderAge18 = 0
                            rnAge18.Enabled = True

                        End If
                        If chkExpAuthority.Checked Then
                            If rnExpAuthority.Value IsNot Nothing Then
                                CommonConfig.ReminderExpAuthority = rnExpAuthority.Value
                            End If

                            rnExpAuthority.Enabled = True
                        Else
                            CommonConfig.ReminderExpAuthority = 0
                            rnExpAuthority.Enabled = True

                        End If
                        If chkNoticePersonalIncomeTax.Checked Then
                            If rnNoticePersonalIncomeTax.Value IsNot Nothing Then
                                CommonConfig.ReminderNoticePersonalIncomeTax = rnNoticePersonalIncomeTax.Value
                            End If

                            rnNoticePersonalIncomeTax.Enabled = True
                        Else
                            CommonConfig.ReminderNoticePersonalIncomeTax = 0
                            rnNoticePersonalIncomeTax.Enabled = True

                        End If

                        If Common.Common.GetUsername.ToUpper <> "ADMIN" Then
                            Using reps As New CommonRepository
                                Dim permissions As List(Of PermissionDTO) = reps.GetUserPermissions(Common.Common.GetUsername)
                                If permissions IsNot Nothing Then
                                    Dim isPermissions = (From p In permissions Where p.FID.ToLower = "ctrlHU_StocksTransaction".ToLower And p.IS_REPORT = False).Any
                                    If isPermissions Then
                                        If chkNoticeStock.Checked Then
                                            If rnNoticeStock.Value IsNot Nothing Then
                                                CommonConfig.ReminderNoticeStock = rnNoticeStock.Value
                                            End If

                                            rnNoticeStock.Enabled = True
                                        Else
                                            CommonConfig.ReminderNoticeStock = 0
                                            rnNoticeStock.Enabled = True
                                        End If
                                        CommonConfig.SaveReminderPerUserWithPermission()
                                    End If
                                End If
                            End Using
                        Else
                            If chkNoticeStock.Checked Then
                                If rnNoticeStock.Value IsNot Nothing Then
                                    CommonConfig.ReminderNoticeStock = rnNoticeStock.Value
                                End If

                                rnNoticeStock.Enabled = True
                            Else
                                CommonConfig.ReminderNoticeStock = 0
                                rnNoticeStock.Enabled = True
                            End If
                            CommonConfig.SaveReminderPerUserWithPermission()
                        End If
                        CommonConfig.SaveReminderPerUser()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        Common.Common.Reminder = Nothing
                    End If
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Refresh()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' validate cua exception cvalBirthday
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalBIRTHDAY_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalBIRTHDAY.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkBIRTHDAY.Checked And (rntxtBIRTHDAY.Value Is Nothing OrElse rntxtBIRTHDAY.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' validate cua exception cvalCONTRACT
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCONTRACT_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCONTRACT.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkCONTRACT.Checked And (rntxtCONTRACT.Value Is Nothing OrElse rntxtCONTRACT.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalApprove_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalApprove.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkApprove.Checked And (rntxtApprove.Value Is Nothing OrElse rntxtApprove.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalApproveHDLD_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalApproveHDLD.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkApproveHDLD.Checked And (rntxtApproveHDLD.Value Is Nothing OrElse rntxtApproveHDLD.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalApproveTHHD_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalApproveTHHD.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkApproveTHHD.Checked And (rntxtApproveTHHD.Value Is Nothing OrElse rntxtApproveTHHD.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalMaterniti_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMaterniti.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkMaterniti.Checked And (rntxtMaterniti.Value Is Nothing OrElse rntxtMaterniti.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub cvalRetirement_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRetirement.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkRetirement.Checked And (rntxtRetirement.Value Is Nothing OrElse rntxtRetirement.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub cvalNoneSalary_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalNoneSalary.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkNoneSalary.Checked And (rntxtNoneSalary.Value Is Nothing OrElse rntxtNoneSalary.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub cvaExpiredCertificate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaExpiredCertificate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkExpiredCertificate.Checked And (rntxtExpiredCertificate.Value Is Nothing OrElse rntxtExpiredCertificate.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    'Private Sub cvalBIRTHDAY_LD_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalBIRTHDAY_LD.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If chkBIRTHDAY_LD.Checked And (rntxtBIRTHDAY_LD.Value Is Nothing OrElse rntxtBIRTHDAY_LD.Value = 0) Then
    '            args.IsValid = False
    '        Else
    '            args.IsValid = True
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

    '    End Try
    'End Sub
    Private Sub cvalConcurrently_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalConcurrently.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkConcurrently.Checked And (rntxtConcurrently.Value Is Nothing OrElse rntxtConcurrently.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub
    Private Sub cvalEmpDtlFamily_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalEmpDtlFamily.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkEmpDtlFamily.Checked And (rntxtEmpDtlFamily.Value Is Nothing OrElse rntxtEmpDtlFamily.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <summary>
    ''' Validate hết hạn visa
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub valnmVISA_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valnmVISA.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkVisa.Checked And (rntxtVISA.Value Is Nothing OrElse rntxtVISA.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub cvalAge18_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalAge18.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkAge18.Checked And (rnAge18.Value Is Nothing OrElse rnAge18.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalExpAuthority_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalExpAuthority.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkExpAuthority.Checked And (rnExpAuthority.Value Is Nothing OrElse rnExpAuthority.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalNoticePersonalIncomeTax_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalNoticePersonalIncomeTax.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkNoticePersonalIncomeTax.Checked And (rnNoticePersonalIncomeTax.Value Is Nothing OrElse rnNoticePersonalIncomeTax.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    Private Sub cvalNoticeStock_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalNoticeStock.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkNoticeStock.Checked And (rnNoticeStock.Value Is Nothing OrElse rnNoticeStock.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub cvalExpContrat_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvalExpContrat.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkExpContrat.Checked And (rnExpContrat.Value Is Nothing OrElse rnExpContrat.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    Private Sub cvalExpDiscipline_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvalExpDiscipline.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkExpDiscipline.Checked And (rnExpDiscipline.Value Is Nothing OrElse rnExpDiscipline.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    'Private Sub cvalInsArising_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles cvalInsArising.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If chkInsArising.Checked And (rnInsArising.Value Is Nothing OrElse rnInsArising.Value = 0) Then
    '            args.IsValid = False
    '        Else
    '            args.IsValid = True
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

    '    End Try
    'End Sub

#End Region

#Region "Custom"

#End Region
End Class