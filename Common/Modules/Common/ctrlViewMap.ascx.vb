Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports System.IO
Imports System.Configuration
Public Class ctrlViewMap
    Inherits CommonView

#Region "Properties"
    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
        End Set
    End Property
    Public Property popupId As String
    Public WithEvents AjaxManager As RadAjaxManager
    Public AjaxLoading As RadAjaxLoadingPanel
    Public Property AjaxManagerId As String
    Public Property AjaxLoadingId As String
#End Region

#Region "Page"
    Public Overrides Property MustAuthorize As Boolean = False
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        UpdateControlState()
        Refresh()
    End Sub
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxLoading = CType(Me.Page, AjaxPage).AjaxLoading
            AjaxManagerId = AjaxManager.ClientID
            AjaxLoadingId = AjaxLoading.ClientID
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not isLoad Then
                DisplayMap()
                isLoad = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub DisplayMap()
        Try
            If Request.Params("Longitude") IsNot Nothing Then
                hidLng.Value = Request.Params("Longitude")
            End If
            If Request.Params("Latitude") IsNot Nothing Then
                hidLat.Value = Request.Params("Latitude")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function GetFileType(ByVal Extention As String) As String
        Return My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & Extention, "", Extention).ToString, "", Extention).ToString
    End Function
#End Region
End Class