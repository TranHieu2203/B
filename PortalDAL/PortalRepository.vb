Imports Framework.Data

Public Class PortalRepository
    Inherits PortalRepositoryBase

#Region "Event Information"
    ''' <summary>
    ''' Lấy thông tin sự kiện
    ''' </summary>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEventInformation(ByVal log As UserLog) As EventDTO
        Try
            Dim query As EventDTO
            query = (From p In Context.PO_EVENT
                     Where p.IS_SHOW <> 0
                     Select New EventDTO With {
                     .ID = p.ID,
                     .ADD_TIME = p.ADD_TIME,
                     .TITLE = p.TITLE,
                     .DETAIL = p.DETAIL}).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Lấy danh sách thông tin sự kiện
    ''' </summary>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEventInformation(ByVal log As UserLog) As List(Of EventDTO)
        Try
            Dim query As List(Of EventDTO)
            query = (From p In Context.PO_EVENT
                     Select New EventDTO With {
                     .ID = p.ID,
                     .ADD_TIME = p.ADD_TIME,
                     .TITLE = p.TITLE,
                     .DETAIL = p.DETAIL,
                     .IS_SHOW = p.IS_SHOW}).ToList
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Thêm mới thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertEventInformation(ByVal _event As EventDTO, ByVal log As UserLog) As Boolean
        Try
            Dim obj As New PO_EVENT
            obj.ID = Utilities.GetNextSequence(Context, Context.PO_EVENT.EntitySet.Name)
            obj.TITLE = _event.TITLE
            obj.DETAIL = _event.DETAIL
            obj.IS_SHOW = _event.IS_SHOW
            obj.ADD_TIME = _event.ADD_TIME
            obj.CREATED_BY = log.Username
            obj.CREATED_DATE = DateTime.Now
            obj.CREATED_LOG = log.ComputerName
            obj.MODIFIED_BY = log.Username
            obj.MODIFIED_DATE = DateTime.Now
            obj.MODIFIED_LOG = log.ComputerName
            Context.AddToPO_EVENT(obj)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Sửa thông tin sự kiện
    ''' </summary>
    ''' <param name="_event"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateEventInformation(ByVal _event As EventDTO, ByVal log As UserLog) As Boolean
        Try
            Dim obj As PO_EVENT = (From p In Context.PO_EVENT Where p.ID = _event.ID Select p).SingleOrDefault
            If obj IsNot Nothing Then
                obj.TITLE = _event.TITLE
                obj.DETAIL = _event.DETAIL
                obj.IS_SHOW = _event.IS_SHOW
                obj.ADD_TIME = _event.ADD_TIME
                obj.MODIFIED_BY = log.Username
                obj.MODIFIED_DATE = DateTime.Now
                obj.MODIFIED_LOG = log.ComputerName
                Context.SaveChanges()
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Xóa thông tin sự kiện
    ''' </summary>
    ''' <param name="_listId"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEventInformation(ByVal _listId As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            Dim lst As List(Of PO_EVENT) = (From p In Context.PO_EVENT Where _listId.Contains(p.ID) Select p).ToList
            If lst IsNot Nothing Then
                Dim i As Integer
                For i = 0 To lst.Count - 1
                    Context.PO_EVENT.DeleteObject(lst(i))
                Next
                Context.SaveChanges()
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Mở hiện sự kiện
    ''' </summary>
    ''' <param name="_id"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveEventInformation(ByVal _id As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim lst As List(Of PO_EVENT) = (From p In Context.PO_EVENT Select p).ToList
            If lst IsNot Nothing Then
                Dim i As Integer
                Dim id As Decimal
                For i = 0 To lst.Count - 1
                    id = lst(i).ID
                    If (id = _id) Then
                        lst(i).IS_SHOW = True
                    Else
                        lst(i).IS_SHOW = False
                    End If
                    lst(i).MODIFIED_BY = log.Username
                    lst(i).MODIFIED_DATE = DateTime.Now
                    lst(i).MODIFIED_LOG = log.ComputerName
                Next
                Context.SaveChanges()
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "Online Registering"
    'Public Function GetLeaveRegistered(byval_empID As Guid) As List(Of 
#End Region
End Class
