Imports Common
Imports Framework.UI
Imports Telerik.Web.UI

Public Class Dialog
    Inherits AjaxPage

    Protected WithEvents CurrentView As ViewBase

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        Me.AjaxManager = RadAjaxManager1
        Me.ToolTipManager = RadToolTipManager1
        Me.AjaxLoading = LoadingPanel
        Me.PopupWindow = rwMainPopup
        If Request.Params("EnableAjax") IsNot Nothing AndAlso Request.Params("EnableAjax") = False Then
            Me.AjaxManager.EnableAJAX = False
        End If

        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))

        Try
            If mid IsNot Nothing AndAlso mid.Trim <> "" AndAlso fid IsNot Nothing AndAlso fid.Trim <> "" Then
                CurrentView = Me.Register(fid, mid, fid, group, , True)

                Me.Title = CurrentView.ViewDescription
            End If
        Catch ex As Exception
            DisplayException("", "", ex)
        End Try

    End Sub

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        'CurrentView.Refresh()
        Dim noscroll As String = IIf(Request.Params("noscroll") Is Nothing, "", Request.Params("noscroll"))
        If noscroll = "1" Then
            RadPaneMain.Scrolling = SplitterPaneScrolling.None
        Else
            RadPaneMain.Scrolling = SplitterPaneScrolling.Both
        End If

        If CurrentView IsNot Nothing AndAlso Not PagePlaceHolder.Controls.Contains(CurrentView) Then
            PagePlaceHolder.Controls.Add(CurrentView)
        End If

    End Sub
End Class