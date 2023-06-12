Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "AssetMng"
        Public Function GetAssetMng(ByVal _filter As AssetMngDTO,
                                    ByVal _param As ParamDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                    Optional ByVal log As UserLog = Nothing) As List(Of AssetMngDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetAssetMng
            Using rep As New ProfileRepository
                Try
                    Return rep.GetAssetMng(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAssetMngById(ByVal Id As Integer
                                        ) As AssetMngDTO Implements ServiceContracts.IProfileBusiness.GetAssetMngById
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetAssetMngById(Id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertAssetMng
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertAssetMng(objAssetMng, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyAssetMng
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyAssetMng(objAssetMng, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAssetMng(ByVal objAssetMng() As AssetMngDTO, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteAssetMng
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteAssetMng(objAssetMng, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace