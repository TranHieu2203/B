Imports Common
Imports Portal.PortalBusiness

Public Class ctrlEvent
    Inherits CommonView

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional Message As String = "")
        Try
            Dim rep As New PortalRepository
            Dim info As EventDTO
            info = rep.GetEventInformation()
            If info IsNot Nothing Then
                rEditor.Content = "<b>" & info.TITLE & "</b> - " & _
                    Format(info.ADD_TIME, "dd/MM/yyyy") & "<br />" & info.DETAIL
            End If
        Catch ex As Exception
            Throw ex
        End Try
        MyBase.Refresh(Message)
    End Sub
#End Region
End Class