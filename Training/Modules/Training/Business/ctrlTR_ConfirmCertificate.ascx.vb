Imports Common
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness
Public Class ctrlTR_ConfirmCertificate
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Property ListID As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_ListID")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_ListID") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Dim ListIDStr As String = Request.Params("LIST_ID")
            ListIDStr = ListIDStr.Remove(ListIDStr.Length - 1, 1)
            Dim StrArr As String() = ListIDStr.Split(New String() {","}, StringSplitOptions.None)
            ListID = New List(Of Decimal)
            For Each s As String In StrArr
                ListID.Add(Decimal.Parse(s))
            Next
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL

                Dim objProgram As ProgramDTO
                Using rep As New TrainingRepository
                    objProgram = rep.GetProgramById(hidProgramID.Value)
                End Using

                If objProgram IsNot Nothing Then
                    txtCERTIFICATE_NAME.Text = objProgram.CERTIFICATE_NAME
                    txtYEAR.Text = objProgram.YEAR
                    rdSTART_DATE.SelectedDate = objProgram.START_DATE
                    rdEND_DATE.SelectedDate = objProgram.END_DATE
                    txtCONTENT.Text = objProgram.CONTENT
                    rdIssuedDate.SelectedDate = Date.Now
                    txtCERTIFICATE_NAME.ReadOnly = True
                    txtYEAR.ReadOnly = True
                    rdSTART_DATE.Enabled = False
                    rdEND_DATE.Enabled = False
                    txtCONTENT.ReadOnly = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT

            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn xác nhận ?")
        ctrlMessageBox.ActionName = "CONFIRM_CERTIFICATE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "CONFIRM_CERTIFICATE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New TrainingRepository
                Dim obj = rep.ValidateCerificateConfirm(ListID)
                If obj IsNot Nothing Then
                    ShowMessage(Translate("Vui lòng cập nhật kết quả trước khi thực hiện thao tác này"), NotifyType.Warning)
                    Exit Sub
                End If
                If rep.SendTrainingToEmployeeProfile(ListID, rdIssuedDate.SelectedDate) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    btnConfirm.Enabled = False
                    Dim script As String = "function f(){CloseModal(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class