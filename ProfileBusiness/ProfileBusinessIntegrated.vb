Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace ProfileBusiness.ServiceImplementations

    Public Class ProfileBusinessIntegrated
        Implements IProfileBusinessIntegrated

        Public Function GetEmpAll(ByVal lastDate As Date) As DataTable Implements ServiceContracts.IProfileBusinessIntegrated.GetEmpAll
            Using rep As New ProfileRepositoryIntegrated
                Try
                    Return rep.GetEmpAll(lastDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOrgAll(ByVal lastDate As Date) As DataTable Implements ServiceContracts.IProfileBusinessIntegrated.GetOrgAll
            Using rep As New ProfileRepositoryIntegrated
                Try
                    Return rep.GetOrgAll(lastDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitleAll(ByVal lastDate As Date) As DataTable Implements ServiceContracts.IProfileBusinessIntegrated.GetTitleAll
            Using rep As New ProfileRepositoryIntegrated
                Try
                    Return rep.GetTitleAll(lastDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitleGroupAll(ByVal lastDate As Date) As DataTable _
            Implements ServiceContracts.IProfileBusinessIntegrated.GetTitleGroupAll
            Using rep As New ProfileRepositoryIntegrated
                Try
                    Return rep.GetTitleGroupAll(lastDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStaffRankAll(ByVal lastDate As Date) As DataTable _
            Implements ServiceContracts.IProfileBusinessIntegrated.GetStaffRankAll
            Using rep As New ProfileRepositoryIntegrated
                Try
                    Return rep.GetStaffRankAll(lastDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace
