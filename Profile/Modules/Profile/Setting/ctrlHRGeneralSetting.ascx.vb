Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHRGeneralSetting
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
                                       ToolbarItem.Seperator,
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
            txtCodeXacThuc.Text = CommonConfig.CODE_ACCURACY()

            ckh_RC_Over_Rank.Checked = CommonConfig.RC_OVER_RANK()
            ckh_RC_Sal_Budget_Exceeded.Checked = CommonConfig.RC_SAL_BUDGET_EXCEEDED()
            ckh_PersonRe_TCTD.Checked = CommonConfig.PERSONRE_TCTD()


            chkAdvanceLeave.Checked = CommonConfig.AT_ADVANCELEAVE()
            If CommonConfig.AT_ADVANCELEAVE_VALUE() IsNot Nothing Then
                ntxtAdvanceLeave.Value = CommonConfig.AT_ADVANCELEAVE_VALUE()
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
                        If txtCodeXacThuc.Text = "" Then
                            ShowMessage("Chưa nhập mã xác thực đăng ký User", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If chkAdvanceLeave.Checked AndAlso ntxtAdvanceLeave.Value Is Nothing Then
                            ShowMessage("Chưa nhập Số phép ứng tối đa", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        CommonConfig.dicConfig("AT_ADVANCELEAVE") = chkAdvanceLeave.Checked
                        CommonConfig.dicConfig("AT_ADVANCELEAVE_VALUE") = If(ntxtAdvanceLeave.Value IsNot Nothing, ntxtAdvanceLeave.Value, Nothing)

                        CommonConfig.dicConfig("PORTAL_ALLOW_CHANGE") = chkPortalAllowChange.Checked
                        CommonConfig.dicConfig("APP_SETTING") = chkApp.Checked

                        CommonConfig.dicConfig("APP_SETTING_1") = chkApp_1.Checked
                        CommonConfig.dicConfig("APP_SETTING_2") = chkApp_2.Checked

                        CommonConfig.dicConfig("CODE_ACCURACY") = txtCodeXacThuc.Text.Replace(" ", "")

                        'anhvn
                        CommonConfig.dicConfig("RC_OVER_RANK") = ckh_RC_Over_Rank.Checked
                        CommonConfig.dicConfig("RC_SAL_BUDGET_EXCEEDED") = ckh_RC_Sal_Budget_Exceeded.Checked
                        CommonConfig.dicConfig("PERSONRE_TCTD") = ckh_PersonRe_TCTD.Checked

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