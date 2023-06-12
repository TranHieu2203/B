Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Labour Protection Manager "
        Public Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO) Implements ServiceContracts.IProfileBusiness.GetOccupationalSafety
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOccupationalSafety(_filter, IsDissolve, PageIndex, PageSize, Total, UserLog, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOccupationalSafetyById(ByVal Id As Integer
                                        ) As OccupationalSafetyDTO Implements ServiceContracts.IProfileBusiness.GetOccupationalSafetyById
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOccupationalSafetyById(Id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertOccupationalSafety
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertOccupationalSafety(lstOccupationalSafety, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyOccupationalSafety
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyOccupationalSafety(lstOccupationalSafety, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteOccupationalSafety(ByVal objOccupationalSafety() As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteOccupationalSafety
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteOccupationalSafety(objOccupationalSafety, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace
