Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase


#Region "Employee File"
    Public Function GetAttachFile_Manager(ByVal fileID As Decimal) As HuFileDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAttachFile_Manager(fileID)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function

    Public Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByVal ext As String, ByRef fileInfo As HuFileDTO) As Byte()
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DownloadAttachFile_Manager(fileid, ext, fileInfo)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function
    Public Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of HuFileDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAttachFiles_Manager(fileType, page, pageSize, totalPage, Employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw
            End Try
        End Using

    End Function
#End Region
End Class
