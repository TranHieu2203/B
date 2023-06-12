Imports System
Imports System.ServiceModel
Imports MatrixBusiness.ServiceContracts
Imports ProfileBusiness.ServiceContracts
Public Class HU_WorkingClient
    Inherits ClientBase(Of IHU_WorkingBusiness)
    Implements IHU_WorkingBusiness
    Implements IMatrixBusiness
    Private Property hu_working_channel As IHU_WorkingBusiness
    Private Property matrix_channel As IMatrixBusiness
    Public Sub New()
        MyBase.New
        'Use "*" to use the first qualifying endpoint.
        hu_working_channel = New ChannelFactory(Of IHU_WorkingBusiness)("*").CreateChannel()
        matrix_channel = New ChannelFactory(Of IMatrixBusiness)("*").CreateChannel()
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String)
        MyBase.New(endpointConfigurationName)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(binding, remoteAddress)
    End Sub
    Public Function GetWorkings() As List(Of String) Implements IHU_WorkingBusiness.GetWorkings
        Return hu_working_channel.GetWorkings()
    End Function
    Public Function TestMatrix() As Decimal Implements IMatrixBusiness.GetWorkings
        Return matrix_channel.GetWorkings()
    End Function
End Class