Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryRank"
        Public Function GetSalaryRank(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryRank
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryRank(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetSalaryRank_Unilever(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryRank_Unilever
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryRank_Unilever(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryRankCombo(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryRankCombo
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetSalaryRankCombo(salLevelID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryRank
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryRank(objSalaryRank, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryRank
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryRank(objSalaryRank, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryRank(ByVal objSalaryRank As SalaryRankDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryRank
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryRank(objSalaryRank)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryRank
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryRank(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryRank(ByVal objSalaryRank() As SalaryRankDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryRank
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryRank(objSalaryRank)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace

