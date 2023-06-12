Imports System.Web.Configuration


Public NotInheritable Class ViewStateProviderService
    Private Sub New()
    End Sub
    Private Shared _provider As ViewStateProviderBase = Nothing
    Private Shared _providers As ViewStateProviderCollection = Nothing
    Private Shared _syncLock As New Object()

    ''' <summary>
    ''' Retrieves the viewstate information from the appropriate viewstate provider. 
    ''' Implements Lazy Load Design Pattern.
    ''' </summary>
    ''' <param name="name">Name of provider.</param>
    ''' <returns>Viewstate.</returns>
    Public Shared Function LoadPageState(ByVal name As String) As Object
        ' Make sure a provider is loaded. Lazy Load Design Pattern.
        LoadProviders()

        ' Delegate to the provider
        Return _provider.LoadPageState(name)
    End Function

    ''' <summary>
    ''' Saves any view or control state information to the appropriate 
    ''' viewstate provider. 
    ''' </summary>
    ''' <param name="name">Name of viewstate.</param>
    ''' <param name="viewState">Viewstate.</param>
    Public Shared Sub SavePageState(ByVal name As String, ByVal viewState As Object)
        ' Make sure a provider is loaded
        LoadProviders()

        ' Delegate to the provider
        _provider.SavePageState(name, viewState)
    End Sub

    ''' <summary>
    ''' Instantiates and manages the viewstate providers according to the 
    ''' registered providers in the "viewStateServices" section in web.config.
    ''' </summary>
    Private Shared Sub LoadProviders()
        ' providers are loaded just once
        If _provider Is Nothing Then
            ' Synchronize the process of loading the providers
            SyncLock _syncLock
                ' Confirm that _provider is still null
                If _provider Is Nothing Then
                    ' Get a reference to the <viewstateService> section
                    Dim section = DirectCast(WebConfigurationManager.GetSection("CustomViewStateSection/ViewStateConfig"), ViewStateProviderConfigSection)

                    ' Load all registered providers
                    _providers = New ViewStateProviderCollection()

                    ProvidersHelper.InstantiateProviders(section.Providers, _providers, GetType(ViewStateProviderBase))

                    ' Set _provider to the default provider
                    _provider = _providers(section.DefaultProvider)
                End If
            End SyncLock
        End If
    End Sub
End Class

