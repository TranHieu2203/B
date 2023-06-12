Imports System.Web
Imports System.Web.UI

Public Interface IViewListener(Of T)
    Function Register(ByVal viewID As String, ByVal mid As String, ByVal fid As String,
                      Optional ByVal group As String = "",
                      Optional ByVal params As Dictionary(Of String, Object) = Nothing,
                      Optional ByVal IsShowDenyMessage As Boolean = False,
                             Optional ByRef DescriptionMain As String = "") As ViewBase
    Sub Listen(ByVal FromViewID As String, ByVal data As Object)

End Interface

<Telerik.Web.UI.RadCompressionSettings(HttpCompression:=Telerik.Web.UI.CompressionType.None)>
Public Class PageBase
    Inherits System.Web.UI.Page
    Implements IViewListener(Of ViewBase)

    Public Overridable Property MustAuthenticate As Boolean = True
    Public Overridable Property IsShowDenyMessage As Boolean = False

    Public Overridable Function IsAuthenticated() As Boolean
        Return False
    End Function

    Public Function Register(ByVal viewID As String, ByVal mid As String, ByVal fid As String,
                             Optional ByVal group As String = "",
                             Optional ByVal params As Dictionary(Of String, Object) = Nothing,
                             Optional ByVal IsShowDenyMessage As Boolean = False,
                             Optional ByRef DescriptionMain As String = "") As ViewBase Implements IViewListener(Of ViewBase).Register
        Try
            Dim view As ViewBase
            Dim brackcrum As String = ""
            If group.Trim <> "" Then
                view = Me.LoadControl(String.Format(Utilities.ModulePath & "/{0}/{1}/{2}.ascx", mid, group, fid))
            Else
                view = Me.LoadControl(String.Format(Utilities.ModulePath & "/{0}/{1}.ascx", mid, fid))
            End If

            view.ID = viewID
            view.ModuleName = mid
            view.IsShowDenyMessage = IsShowDenyMessage
            If params IsNot Nothing Then
                For Each p In params
                    view.SetProperty(p.Key, p.Value)
                Next
            End If
            view.Listener = Me
            If group = "" Then
                brackcrum = "<i class=""fa fa-sitemap"" aria-hidden=""true""></i> " & Translate(mid) & " <i class=""fa fa-angle-double-right"" aria-hidden=""true""></i> <span class=""brackcrum-active"">" & Translate(fid) & "</span>"
            Else
                brackcrum = "<i class=""fa fa-sitemap"" aria-hidden=""true""></i> " & Translate(mid) & " <i class=""fa fa-angle-double-right"" aria-hidden=""true""></i> " & Translate(group) & " <i class=""fa fa-angle-double-right"" aria-hidden=""true""></i> <span class=""brackcrum-active"">" & Translate(fid) & "</span>"
            End If
            ScriptManager.RegisterStartupScript(view.Page, view.Page.GetType, "getTitle", "$('div.brackcrum').html('" + brackcrum + "');", True)

            Return view
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Delegate Sub ReceiveDataDelegate(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs)
    Public Event OnReceiveData As ReceiveDataDelegate

    Public Sub Listen(ByVal fromID As String, ByVal message As Object) Implements IViewListener(Of ViewBase).Listen
        Try
            Dim e As New ViewCommunicationEventArgs(fromID, message)
            RaiseEvent OnReceiveData(Me, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected WithEvents PagePlaceHolder As Global.System.Web.UI.WebControls.PlaceHolder

    Public Overridable Sub PageLoad(ByVal e As System.EventArgs)

    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Try
            MyBase.OnLoad(e)
            If MustAuthenticate() AndAlso Not IsAuthenticated() Then
                '"window.onbeforeunload = top.location.reload();"
                Session.Abandon()
                Security.FormsAuthentication.SignOut()
                Security.FormsAuthentication.RedirectToLoginPage()
                Exit Sub
            End If
            If Request.Params("Error") IsNot Nothing Then
                Dim strErr As String = HttpUtility.UrlDecode(Request.Params("Error"))
                ShowMessage(strErr, Utilities.NotifyType.Error)
            End If
            PageLoad(e)
        Catch ex As Exception
            DisplayException(Me.Title, "", ex)
        End Try

    End Sub


    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String) As String
        Using langMgr As New LanguageManager
            Try

                Return langMgr.Translate(str, args)
            Catch ex As Exception
                langMgr.Dispose()
            End Try
            Return str
        End Using

    End Function


    Public Overridable Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")


    End Sub

    Public Sub ShowMessage(ByVal Message As String, ByVal notity As Utilities.NotifyType)
        Utilities.ShowMessage(Me, Message, notity)
    End Sub

#Region "ViewState Provider Service Access"

    ' Random number generator 
    Private Shared _random As New Random(Environment.TickCount)

    ''' <summary>
    ''' Saves any view and control state to appropriate viewstate provider.
    ''' This method shields the client from viewstate key generation issues.
    ''' </summary>
    ''' <param name="viewState"></param>
    Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        ' Make up a unique name
        Dim random As String = _random.[Next](0, Integer.MaxValue).ToString()
        Dim name As String = "ACTION_" + random + "_" + Request.UserHostAddress + "_" + DateTime.Now.Ticks.ToString()

        ViewStateProviderService.SavePageState(name, viewState)
        ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", name)
    End Sub

    ''' <summary>
    ''' Retrieves viewstate from appropriate viewstate provider.
    ''' This method shields the client from viewstate key retrieval issues.
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Dim name As String = Request.Form("__VIEWSTATE_KEY")
        Return ViewStateProviderService.LoadPageState(name)
    End Function


    Public Property PageViewState(ByVal key As String)
        Get
            Return ViewState(key)
        End Get
        Set(ByVal value)
            ViewState(key) = value
        End Set
    End Property

#End Region
End Class
