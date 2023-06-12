Imports System.Configuration

''' <summary>
''' Represents the custom viewstate provider section in web.config.
''' </summary>
''' <remarks>
''' GoF Design Patterns: Factory.
''' 
''' The Factory Design Pattern implementation is hidden from us, but is implemented
''' by the ConfigurationSection base class. This class reads the configuration 
''' file (web.config) and manufactures the viewstate providers accordingly.
''' </remarks>
Public Class ViewStateProviderConfigSection
    Inherits ConfigurationSection
    ''' <summary>
    ''' Gets a collection of viewstate providers from web.config.
    ''' </summary>
    <ConfigurationProperty("providers")> _
    Public ReadOnly Property Providers() As ProviderSettingsCollection
        Get
            Return DirectCast(MyBase.Item("providers"), ProviderSettingsCollection)
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the default viewstate provider.
    ''' </summary>
    <StringValidator(MinLength:=1)> _
    <ConfigurationProperty("defaultProvider", DefaultValue:="GlobalViewStateProvider")> _
    Public Property DefaultProvider() As String
        Get
            Return DirectCast(MyBase.Item("defaultProvider"), String)
        End Get
        Set(ByVal value As String)
            MyBase.Item("defaultProvider") = value
        End Set
    End Property
End Class

