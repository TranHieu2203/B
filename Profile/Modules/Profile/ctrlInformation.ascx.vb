Imports Common

Public Class ctrlInformation
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

#End Region

#Region "Page"
    Public Overrides Sub ViewInit(e As System.EventArgs)
    End Sub
    Public Overrides Sub ViewLoad(e As System.EventArgs)
        Try
            Using rep As New CommonRepository
                Dim notyfi = rep.CHECK_SYSTEM_MAINTAIN()
                If notyfi <> "null" Then
                    Noti_Maintain.Text = notyfi
                Else
                    Noti_Maintain.Text = ""
                End If
            End Using
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"

#End Region

End Class