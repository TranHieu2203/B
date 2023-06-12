Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSE_MailConfiguration
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Obj ComboBoxData
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

    ''' <lastupdate>
    ''' 26/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Khởi tạo các command trên toolbar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(rtbMain.Items(0), RadToolBarButton).CausesValidation = True
            'CommonConfig.ModuleID = SystemConfigModuleID.iSecure
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Gọi hàm chuyển đổi trạng thái control
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Or Message = "Reload" Then
                Dim EncryptData As New Framework.UI.EncryptData
                CommonConfig.GetConfigFromDatabase()
                txtMailServer.Text = CommonConfig.MailServer
                rntxtPort.Text = CommonConfig.MailPort
                txtMailAccount.Text = CommonConfig.MailAccount
                txtMailAddress.Text = CommonConfig.MailFrom
                If CommonConfig.MailAccountPassword <> "" Then
                    txtMailPass.Text = EncryptData.DecryptString(CommonConfig.MailAccountPassword)
                End If
                cbIsAuthen.Checked = CommonConfig.MailIsAuthen
                cboIsSSL.Checked = CommonConfig.MailIsSSL
                cbIsAuthen_CheckedChanged(Nothing, Nothing)
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

    ''' <lastupdate>
    ''' 26/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command lưu, hủy
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
                        If txtMailPass.Text.Trim <> "" Then
                            Dim EncryptData As New Framework.UI.EncryptData
                            CommonConfig.MailAccountPassword = EncryptData.EncryptString(txtMailPass.Text.Trim)
                        End If

                        CommonConfig.MailIsAuthen = cbIsAuthen.Checked
                        CommonConfig.MailServer = txtMailServer.Text.Trim
                        CommonConfig.MailPort = rntxtPort.Text.Trim
                        CommonConfig.MailAccount = txtMailAccount.Text.Trim
                        CommonConfig.MailFrom = txtMailAddress.Text.Trim
                        CommonConfig.MailIsSSL = cboIsSSL.Checked
                        CommonConfig.SaveChanges()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Refresh("Reload")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 15:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu lys su kien checkedChanged cho control cbIsAuthen
    ''' Enable hay disable cac textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbIsAuthen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbIsAuthen.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtMailAccount.Enabled = cbIsAuthen.Checked
            txtMailPass.Enabled = cbIsAuthen.Checked
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class