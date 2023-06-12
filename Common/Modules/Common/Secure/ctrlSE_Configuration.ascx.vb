Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSE_Configuration
    Inherits CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ComboBoxData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComboBoxData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxData")
        End Get

        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxData") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar,
                                ToolbarItem.Save,
                                ToolbarItem.Cancel)

            CType(rtbMain.Items(0), RadToolBarButton).CausesValidation = True
            'CommonConfig.ModuleID = SystemConfigModuleID.iSecure

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Or Message = "Reload" Then
                Dim EncryptData As New Framework.UI.EncryptData
                CommonConfig.GetConfigFromDatabase()

                rntxtSessionTimeout.Text = CommonConfig.SessionTimeout
                rntxtSessionWarning.Text = CommonConfig.SessionWarning
                rntxtActiveTimeout.Text = CommonConfig.ActiveTimeout
                rntxtPasswordLength.Text = CommonConfig.PasswordLength
                cbPasswordLower.Checked = CommonConfig.PasswordLowerChar
                cbPasswordNumber.Checked = CommonConfig.PasswordNumberChar
                cbPasswordSpecial.Checked = CommonConfig.PasswordSpecialChar
                rnEffectTimeForCodeResetPassword.Text = CommonConfig.EffectTimeForCodeResetPassword
                chkPasswordUpper.Checked = CommonConfig.PasswordUpperChar
                rntxtMaxLoginFail.Value = CommonConfig.MaxNumberLoginFail
                rntxtAppPort.Text = CommonConfig.AppPort
                rntxtPortalPort.Text = CommonConfig.PortalPort

                cbPasswordConfig.Checked = CommonConfig.PasswordConfig
                cbPasswordDefault.Checked = CommonConfig.PasswordDefault
                rtPasswordDefault.Text = CommonConfig.PasswordDefaultText
                If cbPasswordConfig.Checked Then
                    rvPasswordDefault.Enabled = True
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Not cbPasswordConfig.Checked AndAlso Not cbPasswordDefault.Checked Then
                            ShowMessage("Bạn phải chọn loại Phát sinh mật khẩu", Framework.UI.Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        ctrlMessageBox.MessageText = Translate("Lưu ý: Toàn bộ user đang đăng nhập hệ thống sẽ phải đăng nhập lại. Bạn có muốn tiếp tục Lưu dữ liệu?")
                        ctrlMessageBox.MessageTitle = Translate("Thông báo")
                        ctrlMessageBox.ActionName = CommonMessage.ACTION_SAVED
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Refresh("Reload")
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_SAVED Then
                Dim store = New CommonProcedureNew

                'Lưu LDAP connection string
                CommonConfig.AppPort = rntxtAppPort.Text
                CommonConfig.PortalPort = rntxtPortalPort.Text
                CommonConfig.SessionTimeout = rntxtSessionTimeout.Text.Trim
                CommonConfig.ActiveTimeout = rntxtActiveTimeout.Text.Trim
                CommonConfig.SessionWarning = rntxtSessionWarning.Text.Trim
                CommonConfig.MaxNumberLoginFail = rntxtMaxLoginFail.Text.Trim

                CommonConfig.PasswordLength = rntxtPasswordLength.Text.Trim
                CommonConfig.PasswordLowerChar = cbPasswordLower.Checked
                CommonConfig.PasswordNumberChar = cbPasswordNumber.Checked
                CommonConfig.PasswordUpperChar = chkPasswordUpper.Checked
                CommonConfig.PasswordSpecialChar = cbPasswordSpecial.Checked
                CommonConfig.EffectTimeForCodeResetPassword = rnEffectTimeForCodeResetPassword.Text.Trim
                'save db xai cho mobile ne
                Dim ex = store.ADD_CONFIG_PASSWORD(rntxtPasswordLength.Text.Trim, chkPasswordUpper.Checked, cbPasswordNumber.Checked, cbPasswordLower.Checked, cbPasswordSpecial.Checked, rnEffectTimeForCodeResetPassword.Text.Trim)
                Dim re = store.ADD_CONFIG_SESSION_TIME(CDec(Val(rntxtSessionTimeout.Text.Trim)), CDec(Val(rntxtSessionWarning.Text.Trim)))



                'CommonConfig.PasswordConfig = cbPasswordConfig.Checked
                'CommonConfig.PasswordDefault = cbPasswordDefault.Checked
                'CommonConfig.PasswordDefaultText = rtPasswordDefault.Text
                CommonConfig.dicConfig_save("PasswordConfig") = cbPasswordConfig.Checked
                CommonConfig.dicConfig_save("PasswordDefault") = cbPasswordDefault.Checked
                CommonConfig.dicConfig_save("PasswordDefaultText") = rtPasswordDefault.Text

                CommonConfig.dicConfig_save("ActiveTimeout") = rntxtActiveTimeout.Text.Trim
                CommonConfig.SetSE_Configuration()
                CommonConfig.SaveChanges()

                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Kiểm tra ít nhất 1 checkbox được check</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub valCharacter_ServerValidate(ByVal source As Object, ByVal args As ServerValidateEventArgs) Handles valCharacter.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not cbPasswordLower.Checked AndAlso Not cbPasswordNumber.Checked AndAlso Not cbPasswordSpecial.Checked AndAlso Not chkPasswordUpper.Checked Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub cbPasswor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPasswordDefault.CheckedChanged, cbPasswordConfig.CheckedChanged
        If Me.cbPasswordDefault.Checked Then
            rvPasswordDefault.Enabled = True
        End If
        If Me.cbPasswordConfig.Checked Then
            rvPasswordDefault.Enabled = False
        End If
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>26/07/2017</lastupdate>
    ''' <summary> Kiểm tra Session Warning có nhở hơn Session Timeout hay không</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cvalWarning_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs) Handles cvalWarning.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not rntxtSessionTimeout.Value.HasValue OrElse Not rntxtSessionWarning.Value.HasValue Then
                e.IsValid = True
                Exit Sub
            End If

            e.IsValid = rntxtSessionTimeout.Value.Value > rntxtSessionWarning.Value.Value

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class