Public Class fMain

    Private Sub cmdMeal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMeal.Click
        Dim fr As Form = New frmMain()
        Me.Hide()
        fr.ShowDialog()

    End Sub

    Private Sub cmdSDK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSDK.Click
        Dim fr As Form = New frmMain_MCC()
        Me.Hide()
        fr.ShowDialog()

    End Sub
End Class