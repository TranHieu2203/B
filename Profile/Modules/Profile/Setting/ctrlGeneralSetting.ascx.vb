Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlGeneralSetting
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Setting/" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = True
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

            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            'End If

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
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CommonConfig.GetReminderConfigFromDatabase()
            End If

            chkPortalAllowChange.Checked = CommonConfig.PORTAL_ALLOW_CHANGE()
            chkApp.Checked = CommonConfig.APP_SETTING()

            chkApp_1.Checked = CommonConfig.APP_SETTING_1()
            chkApp_2.Checked = CommonConfig.APP_SETTING_2()
            chkApp_3.Checked = CommonConfig.APP_SETTING_3()
            chkApp_4.Checked = CommonConfig.APP_SETTING_4()
            chkApp_5.Checked = CommonConfig.APP_SETTING_5()
            chkApp_6.Checked = CommonConfig.APP_SETTING_6()

            chkApp_7.Checked = CommonConfig.APP_SETTING_7()
            chkApp_8.Checked = CommonConfig.APP_SETTING_8()
            chkApp_9.Checked = CommonConfig.APP_SETTING_9()
            chkApp_10.Checked = CommonConfig.APP_SETTING_10()
            chkApp_11.Checked = CommonConfig.APP_SETTING_11()
            chkApp_12.Checked = CommonConfig.APP_SETTING_12()
            chkApp_13.Checked = CommonConfig.APP_SETTING_13()
            chkApp_14.Checked = CommonConfig.APP_SETTING_14()

            chkApp_15.Checked = CommonConfig.APP_SETTING_15()
            chkApp_16.Checked = CommonConfig.APP_SETTING_16()
            chkApp_17.Checked = CommonConfig.APP_SETTING_17()
            chkApp_18.Checked = CommonConfig.APP_SETTING_18()
            chkApp_19.Checked = CommonConfig.APP_SETTING_19()

            chkApp_20.Checked = CommonConfig.APP_SETTING_20()
            chkApp_21.Checked = CommonConfig.APP_SETTING_21()
            chkApp_22.Checked = CommonConfig.APP_SETTING_22()
            chkApp_23.Checked = CommonConfig.APP_SETTING_23()
            chkApp_24.Checked = CommonConfig.APP_SETTING_24()
            chkApp_25.Checked = CommonConfig.APP_SETTING_25()

            chkIsOrgExpand.Checked = CommonConfig.IS_ORG_EXPAND()
            If CommonConfig.ORG_EXPAND_LEVEL() IsNot Nothing Then
                rnOrgExpandLevel.Value = CDec(Val(CommonConfig.ORG_EXPAND_LEVEL()))
            End If
            chkApp_ISHide_Image.Checked = CommonConfig.APP_ISHIDE_IMAGE()
            chkConfigTitle.Checked = CommonConfig.CONFIGTITLE()
            chk_IsHide_ObjectLaborNew.Checked = CommonConfig.ISHIDE_OBJECTLABORNEW()
            chkQLTT.Checked = CommonConfig.HU_QUTT_PERMISION()
            chkYCTDPERORG.Checked = CommonConfig.RC_REQUEST_PORTALPERORG()
            txtCodeXacThuc.Text = CommonConfig.CODE_ACCURACY()
            If IsNumeric(CommonConfig.SETUP_NUM_ORG()) Then
                txtSetup_Num_Org.Value = CDec(Val(CommonConfig.SETUP_NUM_ORG()))
            End If
            txtNamePage.Text = CommonConfig.NAME_MYPAGE_PORTAL()

            txtMaintenanceComingEnd.Text = CommonConfig.MAINTENANCECOMINGEND()
            txtMaintenanceEnd.Text = CommonConfig.MAINTENANCEEND()
            txtNotifyLogin.Text = CommonConfig.NOTIFYLOGIN()
            chkIsNotLoginMaintenance.Checked = CommonConfig.ISNOTLOGINMAINTENAN()

            ckh_RC_Over_Rank.Checked = CommonConfig.RC_OVER_RANK()
            chk_hide_AD_User.Checked = CommonConfig.IS_HIDE_AD_USER()
            chk_Hide_button_reset.Checked = CommonConfig.IS_HIDE_BUTTON_RESET()
            ckh_RC_Sal_Budget_Exceeded.Checked = CommonConfig.RC_SAL_BUDGET_EXCEEDED()
            ckh_PersonRe_TCTD.Checked = CommonConfig.PERSONRE_TCTD()

            chk_Hide_ManagerHeathIns.Checked = CommonConfig.HIDE_MANAGERHEATHINS()
            chkAdvanceLeave.Checked = CommonConfig.AT_ADVANCELEAVE()
            Chk_hs_ot.Checked = CommonConfig.AT_HS_OT()
            If CommonConfig.AT_ADVANCELEAVE_VALUE() IsNot Nothing Then
                ntxtAdvanceLeave.Value = CDec(Val(CommonConfig.AT_ADVANCELEAVE_VALUE()))
            End If

            If CommonConfig.AT_ADVANCELEAVE_VALUE() IsNot Nothing Then
                ntxtAdvanceLeaveTemp.Value = CDec(Val(CommonConfig.AT_ADVANCELEAVETEMP_VALUE()))
            Else
                ntxtAdvanceLeaveTemp.Value = 12
            End If
            chkCal_SENIORITY_By_HSV.Checked = CommonConfig.CAL_SENIORITY_BY_HSV()
            chk_Is_Load_DirectMng.Checked = CommonConfig.IS_LOAD_DIRECTMNG()
            chkIsAuto.Checked = CommonConfig.IS_AUTO()

            chkWorkingLessEqualWorkingStandard.Checked = CommonConfig.IS_WORKING_LESSEQUAL_WORKINGSTANDARD()
            chkBoundShift.Checked = CommonConfig.IS_BOUND_SHIFT()
            If CommonConfig.AT_BOUND_SHIFT_OPERATOR() <> "" Then
                txtCompareOperatorBoundShift.Text = CommonConfig.AT_BOUND_SHIFT_OPERATOR()
            End If
            If IsNumeric(CommonConfig.AT_BOUND_SHIFT_VALUE()) Then
                rntxtBoundShift.Value = CDec(Val(CommonConfig.AT_BOUND_SHIFT_VALUE()))
            End If

            chkRC_ID_NO.Checked = CommonConfig.RC_ID_NO_REQUIRE()
            chkRC_PER_MAIL.Checked = CommonConfig.RC_PERSONAL_EMAIL_REQUIRE()
            chkRC_MOB_PHONE.Checked = CommonConfig.RC_MOBILE_PHONE_REQUIRE()
            chkRC_LITERACY.Checked = CommonConfig.RC_LITERACY_REQUIRE()
            chkGovermentComOT.Checked = CommonConfig.GOVERMENT_COM_OT()
            chkPortalSignWork.Checked = CommonConfig.PORTALSIGNWORK()
            chkPortalSignWorkDMCA.Checked = CommonConfig.PORTALSIGNWORKDMCA()
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
                        If txtCodeXacThuc.Text = "" Then
                            ShowMessage("Chưa nhập mã xác thực đăng ký User", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtNamePage.Text = "" Then
                            ShowMessage("Chưa nhập tiêu đề mypage", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If txtSetup_Num_Org.Value Is Nothing Then
                            ShowMessage("Chưa nhập số lượng client công ty hoặc chi nhánh", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If chkAdvanceLeave.Checked AndAlso ntxtAdvanceLeave.Value Is Nothing Then
                            ShowMessage("Chưa nhập Số phép ứng tối đa", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If chkBoundShift.Checked Then
                            If txtCompareOperatorBoundShift.Text = "" Then
                                ShowMessage("Mời nhập giá trị so sánh", Framework.UI.Utilities.NotifyType.Warning)
                                txtCompareOperatorBoundShift.Focus()
                                Exit Sub
                            End If
                            If rntxtBoundShift.Value Is Nothing Then
                                ShowMessage("Mời nhập giá trị DK", Framework.UI.Utilities.NotifyType.Warning)
                                rntxtBoundShift.Focus()
                                Exit Sub
                            End If
                        End If
                        If chkIsOrgExpand.Checked Then
                            If rnOrgExpandLevel.Value Is Nothing Then
                                ShowMessage("Mời nhập Org level expand", Framework.UI.Utilities.NotifyType.Warning)
                                rnOrgExpandLevel.Focus()
                                Exit Sub
                            Else
                                If rnOrgExpandLevel.Value < 1 Then
                                    ShowMessage("Org level expand phải lớn hơn 1", Framework.UI.Utilities.NotifyType.Warning)
                                    rnOrgExpandLevel.Focus()
                                    Exit Sub
                                End If
                            End If
                        End If
                        CommonConfig.dicConfig_save("AT_ADVANCELEAVE") = chkAdvanceLeave.Checked
                        CommonConfig.dicConfig_save("AT_ADVANCELEAVE_VALUE") = If(ntxtAdvanceLeave.Value IsNot Nothing, ntxtAdvanceLeave.Value, "")

                        CommonConfig.dicConfig_save("AT_ADVANCELEAVETEMP_VALUE") = If(ntxtAdvanceLeaveTemp.Value IsNot Nothing, ntxtAdvanceLeaveTemp.Value, "")

                        CommonConfig.dicConfig_save("PORTAL_ALLOW_CHANGE") = chkPortalAllowChange.Checked
                        CommonConfig.dicConfig_save("APP_SETTING") = chkApp.Checked

                        CommonConfig.dicConfig_save("APP_SETTING_1") = chkApp_1.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_2") = chkApp_2.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_3") = chkApp_3.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_4") = chkApp_4.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_5") = chkApp_5.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_6") = chkApp_6.Checked

                        CommonConfig.dicConfig_save("APP_SETTING_7") = chkApp_7.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_8") = chkApp_8.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_9") = chkApp_9.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_10") = chkApp_10.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_11") = chkApp_11.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_12") = chkApp_12.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_13") = chkApp_13.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_14") = chkApp_14.Checked

                        CommonConfig.dicConfig_save("APP_SETTING_15") = chkApp_15.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_16") = chkApp_16.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_17") = chkApp_17.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_18") = chkApp_18.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_19") = chkApp_19.Checked

                        CommonConfig.dicConfig_save("APP_SETTING_20") = chkApp_20.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_21") = chkApp_21.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_22") = chkApp_22.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_23") = chkApp_23.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_24") = chkApp_24.Checked
                        CommonConfig.dicConfig_save("APP_SETTING_25") = chkApp_25.Checked

                        CommonConfig.dicConfig_save("IS_ORG_EXPAND") = chkIsOrgExpand.Checked
                        CommonConfig.dicConfig_save("ORG_EXPAND_LEVEL") = If(rnOrgExpandLevel.Value IsNot Nothing, rnOrgExpandLevel.Value, "")

                        CommonConfig.dicConfig_save("CODE_ACCURACY") = txtCodeXacThuc.Text.Replace(" ", "")

                        'anhvn
                        CommonConfig.dicConfig_save("RC_OVER_RANK") = ckh_RC_Over_Rank.Checked
                        CommonConfig.dicConfig_save("RC_SAL_BUDGET_EXCEEDED") = ckh_RC_Sal_Budget_Exceeded.Checked
                        CommonConfig.dicConfig_save("PERSONRE_TCTD") = ckh_PersonRe_TCTD.Checked
                        'CommonConfig.dicConfig_save("RC_OVER_RANK") = chk_hide_AD_User.Checked
                        'CommonConfig.dicConfig_save("RC_OVER_RANK") = chk_Hide_button_reset.Checked

                        CommonConfig.dicConfig_save("IS_HIDE_AD_USER") = chk_hide_AD_User.Checked
                        CommonConfig.dicConfig_save("IS_HIDE_BUTTON_RESET") = chk_Hide_button_reset.Checked
                        CommonConfig.dicConfig_save("HIDE_MANAGERHEATHINS") = chk_Hide_ManagerHeathIns.Checked
                        CommonConfig.dicConfig_save("AT_HS_OT") = Chk_hs_ot.Checked

                        If IsNumeric(txtSetup_Num_Org.Value) Then
                            CommonConfig.dicConfig_save("SETUP_NUM_ORG") = CDec(Val(txtSetup_Num_Org.Value))
                        End If
                        CommonConfig.dicConfig_save("NAME_MYPAGE_PORTAL") = txtNamePage.Text
                        CommonConfig.dicConfig_save("CAL_SENIORITY_BY_HSV") = chkCal_SENIORITY_By_HSV.Checked
                        CommonConfig.dicConfig_save("IS_LOAD_DIRECTMNG") = chk_Is_Load_DirectMng.Checked
                        CommonConfig.dicConfig_save("IS_AUTO") = chkIsAuto.Checked
                        CommonConfig.dicConfig_save("APP_ISHIDE_IMAGE") = chkApp_ISHide_Image.Checked
                        CommonConfig.dicConfig_save("CONFIGTITLE") = chkConfigTitle.Checked
                        CommonConfig.dicConfig_save("ISHIDE_OBJECTLABORNEW") = chk_IsHide_ObjectLaborNew.Checked
                        CommonConfig.dicConfig_save("MAINTENANCECOMINGEND") = txtMaintenanceComingEnd.Text
                        CommonConfig.dicConfig_save("MAINTENANCEEND") = txtMaintenanceEnd.Text
                        CommonConfig.dicConfig_save("NOTIFYLOGIN") = txtNotifyLogin.Text
                        CommonConfig.dicConfig_save("ISNOTLOGINMAINTENAN") = chkIsNotLoginMaintenance.Checked

                        CommonConfig.dicConfig_save("RC_ID_NO_REQUIRE") = chkRC_ID_NO.Checked
                        CommonConfig.dicConfig_save("RC_PERSONAL_EMAIL_REQUIRE") = chkRC_PER_MAIL.Checked
                        CommonConfig.dicConfig_save("RC_MOBILE_PHONE_REQUIRE") = chkRC_MOB_PHONE.Checked
                        CommonConfig.dicConfig_save("RC_LITERACY_REQUIRE") = chkRC_LITERACY.Checked
                        CommonConfig.dicConfig_save("HU_QUTT_PERMISION") = chkQLTT.Checked
                        CommonConfig.dicConfig_save("RC_REQUEST_PORTALPERORG") = chkYCTDPERORG.Checked

                        CommonConfig.dicConfig_save("IS_WORKING_LESSEQUAL_WORKINGSTANDARD") = chkWorkingLessEqualWorkingStandard.Checked
                        CommonConfig.dicConfig_save("IS_BOUND_SHIFT") = chkBoundShift.Checked
                        CommonConfig.dicConfig_save("AT_BOUND_SHIFT_VALUE") = If(rntxtBoundShift.Value IsNot Nothing, rntxtBoundShift.Value, "")
                        CommonConfig.dicConfig_save("AT_BOUND_SHIFT_OPERATOR") = If(txtCompareOperatorBoundShift.Text IsNot Nothing, txtCompareOperatorBoundShift.Text, "")
                        CommonConfig.dicConfig_save("GOVERMENT_COM_OT") = chkGovermentComOT.Checked
                        CommonConfig.dicConfig_save("PORTALSIGNWORK") = chkPortalSignWork.Checked
                        CommonConfig.dicConfig_save("PORTALSIGNWORKDMCA") = chkPortalSignWorkDMCA.Checked
                        '----
                        CommonConfig.SetGeneralConfig()
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
#End Region

#Region "Custom"

#End Region

End Class