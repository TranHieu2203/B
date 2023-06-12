' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Imports System.ServiceModel
Imports Framework.Data
Imports PortalDAL

Namespace PortalBusiness.ServiceContracts

    <ServiceContract()>
    Public Interface IPortalBusiness

        <OperationContract()>
        Function TestService(ByVal str As String) As String
#Region "Event Information"
        ''' <summary>
        ''' Lấy thông tin sự kiện
        ''' </summary>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEventInformation(ByVal log As UserLog) As EventDTO
        ''' <summary>
        ''' Lấy thông tin sự kiện
        ''' </summary>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEventInformation(ByVal log As UserLog) As List(Of EventDTO)
        ''' <summary>
        ''' Thêm mới thông tin sự kiện
        ''' </summary>
        ''' <param name="_event"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEventInformation(ByVal _event As EventDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Sửa thông tin sự kiện
        ''' </summary>
        ''' <param name="_event"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function UpdateEventInformation(ByVal _event As EventDTO, ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Xóa thông tin sự kiện
        ''' </summary>
        ''' <param name="_listId"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEventInformation(ByVal _listId As List(Of Decimal), ByVal log As UserLog) As Boolean
        ''' <summary>
        ''' Mở hiện sự kiện
        ''' </summary>
        ''' <param name="_id"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveEventInformation(ByVal _id As Decimal, ByVal log As UserLog) As Boolean
#End Region

    End Interface

End Namespace
