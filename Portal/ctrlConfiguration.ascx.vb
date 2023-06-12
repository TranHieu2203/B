Imports Common
Imports Telerik.Web.UI

Public Class ctrlConfiguration
    Inherits CommonView

#Region "Property"
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Cancel)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub ViewLoad(e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional Message As String = "")
        Try
            rnSESSION_TIMEOUT.Text = CommonConfig.SessionTimeout
            rntxtPasswordLength.Text = CommonConfig.PasswordLength
            chkPasswordLower.Checked = CommonConfig.PasswordLowerChar
            chkPasswordNumber.Checked = CommonConfig.PasswordNumberChar
            chkPasswordSpecial.Checked = CommonConfig.PasswordSpecialChar
            chkPasswordUpper.Checked = CommonConfig.PasswordUpperChar
            rnMAX_LOGIN_FAIL.Value = CommonConfig.MaxNumberLoginFail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    'Lưu LDAP connection string
                    ctrlMessageBox.MessageText = Translate("Lưu ý: Toàn bộ user đang đăng nhập hệ thống sẽ phải đăng nhập lại. Bạn có muốn tiếp tục Lưu dữ liệu?")
                    ctrlMessageBox.MessageTitle = Translate("Thông báo")
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_SAVED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Refresh()
            End Select

        Catch ex As Exception
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.ACTION_SAVED Then
                CommonConfig.SessionTimeout = rnSESSION_TIMEOUT.Text.Trim
                CommonConfig.MaxNumberLoginFail = rnMAX_LOGIN_FAIL.Text.Trim
                CommonConfig.PasswordLength = rntxtPasswordLength.Text.Trim
                CommonConfig.PasswordLowerChar = chkPasswordLower.Checked
                CommonConfig.PasswordNumberChar = chkPasswordNumber.Checked
                CommonConfig.PasswordUpperChar = chkPasswordUpper.Checked
                CommonConfig.PasswordSpecialChar = chkPasswordSpecial.Checked
                CommonConfig.SaveConfig()
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class