Imports Common.CommonBusiness
Imports Portal.PortalBusiness

Public Class PortalRepository
    Inherits PortalRepositoryBase

#Region "Event Information"
    ''' <summary>
    ''' Lấy thông tin sự kiện
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEventInformation() As EventDTO
        Using rep As New PortalBusinessClient
            Try
                Return rep.GetEventInformation(Nothing)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Lấy danh sách thông tin sự kiện
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEventInformation() As List(Of EventDTO)
        Using rep As New PortalBusinessClient
            Try
                Return rep.GetListEventInformation(Nothing)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Thêm mới thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertEventInformation(ByVal _event As EventDTO) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.InsertEventInformation(_event, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Sửa thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateEventInformation(ByVal _event As EventDTO) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.UpdateEventInformation(_event, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Xóa thông tin sự kiện
    ''' </summary>
    ''' <param name="_listId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEventInformation(ByVal _listId As List(Of Decimal)) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.DeleteEventInformation(_listId, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    ''' <summary>
    ''' Mở hiện sự kiện
    ''' </summary>
    ''' <param name="_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveEventInformation(ByVal _id As Decimal) As Boolean
        Using rep As New PortalBusinessClient
            Try
                Return rep.ActiveEventInformation(_id, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetListBackgroud() As List(Of SE_BACKGROUND_PORTALDTO)
        Using rep As New CommonBusinessClient
            Try
                Return rep.GetSE_BACKGROUND_PORTALList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyBACKGROUND_PORTAL(ByVal _obj As SE_BACKGROUND_PORTALDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.ModifyBACKGROUND_PORTAL(_obj, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertBACKGROUND_PORTAL(ByVal _obj As SE_BACKGROUND_PORTALDTO) As Boolean
        Using rep As New CommonBusinessClient
            Try
                Return rep.InsertBACKGROUND_PORTAL(_obj, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

End Class
