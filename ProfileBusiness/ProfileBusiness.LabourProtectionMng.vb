Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness


        Public Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO) Implements ServiceContracts.IProfileBusiness.GetLabourProtectionMng
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetLabourProtectionMng(_filter, IsDissolve, PageIndex, PageSize, Total, UserLog, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLabourProtectionMngById(ByVal Id As Integer
                                        ) As LabourProtectionMngDTO Implements ServiceContracts.IProfileBusiness.GetLabourProtectionMngById
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetLabourProtectionMngById(Id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertLabourProtectionMng
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertLabourProtectionMng(lstLabourProtectionMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyLabourProtectionMng
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyLabourProtectionMng(lstLabourProtectionMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteLabourProtectionMng(ByVal objLabourProtectionMng() As LabourProtectionMngDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteLabourProtectionMng
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteLabourProtectionMng(objLabourProtectionMng, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


    End Class
End Namespace
