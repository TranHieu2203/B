Imports Framework.Data
Imports PortalBusiness.ServiceContracts
Imports PortalDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PortalBusiness.ServiceImplementations

    Public Class PortalBusiness
        Implements IPortalBusiness

        Public Function TestService(ByVal str As String) As String Implements ServiceContracts.IPortalBusiness.TestService
            Return "Hello world " & str
        End Function

#Region "Event Information"
        Public Function ActiveEventInformation(ByVal _id As System.Decimal, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPortalBusiness.ActiveEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.ActiveEventInformation(_id, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteEventInformation(ByVal _listId As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IPortalBusiness.DeleteEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.DeleteEventInformation(_listId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEventInformation(log As UserLog) As PortalDAL.EventDTO Implements ServiceContracts.IPortalBusiness.GetEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.GetEventInformation(log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListEventInformation(log As UserLog) As List(Of PortalDAL.EventDTO) Implements ServiceContracts.IPortalBusiness.GetListEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.GetListEventInformation(log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertEventInformation(_event As PortalDAL.EventDTO, log As UserLog) As Boolean Implements ServiceContracts.IPortalBusiness.InsertEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.InsertEventInformation(_event, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateEventInformation(_event As PortalDAL.EventDTO, log As UserLog) As Boolean Implements ServiceContracts.IPortalBusiness.UpdateEventInformation
            Try
                Dim rep As New PortalRepository
                Return rep.UpdateEventInformation(_event, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region


#Region "Online Registering"

#End Region
    End Class
End Namespace
