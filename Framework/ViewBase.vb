Imports System.Reflection
Imports System.Web.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI

<Telerik.Web.UI.RadCompressionSettings(HttpCompression:=Telerik.Web.UI.CompressionType.GZip)>
Public MustInherit Class ViewBase
    Inherits UserControl
    Implements IViewListener(Of ViewBase)

    Public Property Listener As IViewListener(Of ViewBase)

    Public Property ViewType As ViewType = UI.ViewType.Functional

    Public Overridable Property ViewName As String = Me.GetType.BaseType.Name

    Public Overridable Property ViewDescription As String = Translate(Me.GetType.BaseType.Name)

    Public Property ViewGroup As String = ""

    Public Property ModuleName As String = ""

    Public Function GetProperty(ByVal Name As String) As Object
        Try
            Dim infos As PropertyInfo() = Me.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanRead And p.Name = Name Select p).SingleOrDefault
            If item IsNot Nothing Then
                Return item.GetValue(Me, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function SetProperty(ByVal Name As String, ByVal Value As Object) As Boolean

        Try
            Dim infos As PropertyInfo() = Me.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanWrite And p.Name = Name Select p).SingleOrDefault
            If item IsNot Nothing Then
                item.SetValue(Me, Value, Nothing)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False

    End Function
    Public Property FormInit As Boolean
        Get
            If ViewState(Me.ID & "_FormInit") Is Nothing Then
                Return True
            Else
                Return ViewState(Me.ID & "_FormInit")
            End If

        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_FormInit") = value
        End Set
    End Property

    Public Property CurrentState As String
        Get
            Return PageViewState(Me.ID & "_CurrentState")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentState") = value
        End Set
    End Property


    Public Property PageViewState(ByVal key As String) As Object
        Get
            Try
                Return CType(Me.Page, PageBase).PageViewState(key)
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Get
        Set(ByVal value As Object)
            Try
                CType(Me.Page, PageBase).PageViewState(key) = value
            Catch ex As Exception
                Throw ex
            End Try

        End Set
    End Property


    Public Overridable Property Allow As Boolean = False
    Public Overridable Property AllowCreate As Boolean = False
    Public Overridable Property AllowModify As Boolean = False
    Public Overridable Property AllowDelete As Boolean = False
    Public Overridable Property AllowPrint As Boolean = False
    Public Overridable Property AllowImport As Boolean = False
    Public Overridable Property AllowExport As Boolean = False
    Public Overridable Property AllowSpecial1 As Boolean = False
    Public Overridable Property AllowSpecial2 As Boolean = False
    Public Overridable Property AllowSpecial3 As Boolean = False
    Public Overridable Property AllowSpecial4 As Boolean = False
    Public Overridable Property AllowSpecial5 As Boolean = False
    Public Overridable Property AllowReset As Boolean = False
    Public Overridable Property MustAuthorize As Boolean = True

    Public Overridable Function IsAuthenticated() As Boolean
        Return False
    End Function

    Public Overridable Property IsShowDenyMessage As Boolean = False
    Public Overridable Property EnableLogAccess As Boolean = True

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Try
            If Not ((Not MustAuthorize()) OrElse (MustAuthorize() AndAlso IsAuthenticated() AndAlso Allow)) Then
                writer.Flush()
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                writer.RenderEndTag()
                If IsShowDenyMessage() Then
                    Utilities.ShowMessage(Me, Me.Translate("ACCESS DENIED", Me.ViewDescription), NotifyType.Error)
                End If
            End If
            MyBase.Render(writer)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Function Register(ByVal viewID As String, ByVal mid As String,
                             ByVal fid As String,
                             Optional ByVal group As String = "",
                             Optional ByVal params As System.Collections.Generic.Dictionary(Of String, Object) = Nothing,
                             Optional ByVal IsShowDenyMessage As Boolean = False,
                             Optional ByRef DescriptionMain As String = "") As ViewBase Implements IViewListener(Of ViewBase).Register
        Try
            Dim view As ViewBase
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

            Return view
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Sub Send(ByVal message As Object)

        Try
            'If Me.PluginManager.BeforeSend(Me, message) Then
            If Me.Listener IsNot Nothing Then
                Me.Listener.Listen(Me.ID, message)
            End If
            'Me.PluginManager.AfterSend(Me, message)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Delegate Sub ReceiveDataDelegate(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs)
    Public Event OnReceiveData As ReceiveDataDelegate

    Public Sub Listen(ByVal fromID As String, ByVal message As Object) Implements IViewListener(Of ViewBase).Listen
        Try
            'If Me.PluginManager.BeforeListen(Me, fromID, message) Then
            Dim e As New ViewCommunicationEventArgs(fromID, message)
            RaiseEvent OnReceiveData(Me, e)
            'Me.PluginManager.AfterListen(Me, fromID, message)
            'End If
        Catch ex As Exception
            Throw ex
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


    Public Overridable Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As Exception, Optional ByVal ExtraInfo As String = "")
        Utilities.DisplayException(Me, ex)
    End Sub

    Public Sub ShowMessage(ByVal Message As String, ByVal notity As NotifyType, Optional ByVal autohidesecond As Integer = 10)
        Utilities.ShowMessage(Me, Message, notity, autohidesecond)
    End Sub

    Public Sub ExcuteScript(ByVal Key As String, ByVal Script As String)
        Utilities.ExcuteScript(Me, Key, Script)
    End Sub


    Public Overridable Sub Refresh(Optional ByVal Message As String = "")

    End Sub
    ''' <createby>HongDX</createby>
    ''' <Date>04/08/2017</Date>
    ''' <summary>
    ''' Resize panel sau khi lưu fix bug #888
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub ReSize()

    End Sub

    Public Overridable Sub ReSizeSpliter()

    End Sub


    Public Overridable Sub UpdateControlState()

    End Sub

    Public Overridable Sub CheckAuthorization()

    End Sub

    Public Overridable Sub ViewInit(ByVal e As System.EventArgs)

    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Try
            MyBase.OnInit(e)
            'If PluginManager.BeforeInit(Me) Then
            ViewInit(e)
            '    PluginManager.AfterInit(Me)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overridable Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub
    Public Overridable Sub BindData()

    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Try
            MyBase.OnLoad(e)
            'If PluginManager.BeforeCheckAuthorization(Me) Then
            CheckAuthorization()
            '    PluginManager.AfterCheckAuthorization(Me)
            'End If
            If Not Me.Allow Then
                Me.Controls.Clear()
            End If
            'If PluginManager.BeforeLoad(Me) Then
            If Me.Allow Then
                If FormInit Then
                    BindData()
                    Me.DataBind()
                    FormInit = False
                End If
                ViewLoad(e)
            End If
            'PluginManager.AfterLoad(Me)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

End Class


Public Class ViewCommunicationEventArgs
    Inherits EventArgs

    Public Sub New(ByVal _fromViewID As String, ByVal _data As Object)
        FromViewID = _fromViewID
        EventData = _data
    End Sub
    Public FromViewID As String
    Public EventData As Object

End Class


Public Enum ViewType
    Functional
    Report
End Enum
