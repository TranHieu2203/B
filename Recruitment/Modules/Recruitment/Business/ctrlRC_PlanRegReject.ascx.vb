Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic

Public Class ctrlRC_PlanRegReject
    Inherits CommonView
    Protected WithEvents CurrentView As ViewBase
    Public WithEvents ViewImage As ViewBase
    Private rep As New HistaffFrameworkRepository
    Public Overrides Property MustAuthorize As Boolean = True


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)

    End Sub


    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

        txtRemarkReject.Focus()

    End Sub

    Public Overrides Sub UpdateControlState()


    End Sub


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click
        Try
            Dim rep_Recruitment As New RecruitmentRepository
            Dim lstDeletes As New List(Of Decimal)
            Dim aListRejectID() As String = Request.QueryString("RejectID").Split(",")
            Dim OUT_NUMBER As String = "0"
            For index = 0 To aListRejectID.Length - 1
                If aListRejectID(index).ToString() <> "" Then
                    Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_REMARK_REJECT", New List(Of Object)({aListRejectID(index).ToString(), txtRemarkReject.Text, OUT_NUMBER}))

                    lstDeletes.Add(aListRejectID(index).ToString())

                    OUT_NUMBER = obj(0).ToString()
                End If
            Next
            If 1 = Integer.Parse(OUT_NUMBER) Then
                rep_Recruitment.UpdateStatusPlanReg(lstDeletes, RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                Dim str As String = "getRadWindow().close('1');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)
            Else
                ShowMessage(Translate("Tác vụ thực hiện không thành công"), NotifyType.Error)
            End If



            'Dim aListRejectID() As String = Request.QueryString("RejectID").Split(",")
            'Dim OUT_NUMBER As String = "0"
            'For index = 0 To aListRejectID.Length - 1
            '    If aListRejectID(index).ToString() <> "" Then
            '        Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.UPDATE_REMARK_REJECT", New List(Of Object)({aListRejectID(index).ToString(), txtRemarkReject.Text, OUT_NUMBER}))
            '        OUT_NUMBER = obj(0).ToString()
            '    End If
            'Next
            'If 1 = Integer.Parse(OUT_NUMBER) Then
            '    Dim str As String = "getRadWindow().close('1');"
            '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)
            'Else
            '    ShowMessage(Translate("Tác vụ thực hiện không thành công"), NotifyType.Error)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class