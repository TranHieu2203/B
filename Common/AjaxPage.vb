Imports Framework.UI
Imports Telerik.Web.UI

Public Class AjaxPage
    Inherits PageBase

    Public Property AjaxManager As RadAjaxManager
    Public Property ToolTipManager As RadToolTipManager
    Public Property AjaxLoading As RadAjaxLoadingPanel
    Public WithEvents PopupWindow As RadWindow

    Public Overrides Function IsAuthenticated() As Boolean
        'If Utilities.IsAuthenticated AndAlso LogHelper.CurrentUser IsNot Nothing Then
        If LogHelper.CurrentUser IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Overrides Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")
        Common.DisplayException(Me, ex, "")
    End Sub
End Class
