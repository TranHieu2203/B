Imports System.Configuration.Provider

Public MustInherit Class ViewStateProviderBase
    Inherits ProviderBase

    Public MustOverride Sub SavePageState(ByVal name As String, ByVal viewState As Object)

    Public MustOverride Function LoadPageState(ByVal name As String) As Object


End Class

Public Class GlobalViewStateSingleton


    ' This is the single instance of this class
    Private Shared ReadOnly _instance As New GlobalViewStateSingleton()

    ''' <summary>
    ''' Private constructor for GlobalViewStateSingleton.
    ''' Prevents others from instantiating additional instances.
    ''' </summary>
    Private Sub New()
        ViewStates = New Dictionary(Of String, Object)()
    End Sub

    ''' <summary>
    ''' Gets the one instance of the GlobalViewStateSingleton class
    ''' </summary>
    Public Shared ReadOnly Property Instance() As GlobalViewStateSingleton
        Get
            Return _instance
        End Get
    End Property


    ''' <summary>
    ''' Gets a list of ViewStates.
    ''' </summary>
    Public Property ViewStates() As Dictionary(Of String, Object)
        Get
            Return m_ViewStates
        End Get
        Private Set(ByVal value As Dictionary(Of String, Object))
            m_ViewStates = value
        End Set
    End Property
    Private m_ViewStates As Dictionary(Of String, Object)
End Class


Public Class GlobalViewStateProvider
    Inherits ViewStateProviderBase

    Public Overrides Function LoadPageState(ByVal name As String) As Object
        If GlobalViewStateSingleton.Instance.ViewStates.ContainsKey(name) Then
            Return GlobalViewStateSingleton.Instance.ViewStates(name)
        End If
        Return Nothing
    End Function

    Public Overrides Sub SavePageState(ByVal name As String, ByVal viewState As Object)
        GlobalViewStateSingleton.Instance.ViewStates.Add(name, viewState)
    End Sub
End Class

Public Class ViewStateProviderCollection
    Inherits ProviderCollection
    ''' <summary>
    ''' Gets a viewState provider from a list given its name.
    ''' </summary>
    ''' <param name="name">Provider name.</param>
    ''' <returns>Viewstate provider.</returns>
    Default Public Shadows ReadOnly Property Item(ByVal name As String) As ViewStateProviderBase
        Get
            Return TryCast(MyBase.Item(name), ViewStateProviderBase)
        End Get
    End Property

    ''' <summary>
    ''' Adds a viewstate provider to a collection of providers.
    ''' </summary>
    ''' <param name="provider">Viewstate provider.</param>
    Public Overrides Sub Add(ByVal provider As ProviderBase)
        If provider IsNot Nothing AndAlso TypeOf provider Is ViewStateProviderBase Then
            MyBase.Add(provider)
        End If
    End Sub
End Class



