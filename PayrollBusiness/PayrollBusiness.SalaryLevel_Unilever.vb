Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryLevel"
        Public Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryLevel_Unilever
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryLevel_Unilever(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryLevelCombo(salGroupID As Decimal, ByVal isBlank As Boolean, Optional ByVal other_Use As String = "ALL") As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryLevelCombo
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetSalaryLevelCombo(salGroupID, isBlank, other_Use)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryLevel_Unilever
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryLevel_Unilever(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryLevel_Unilever
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryLevel_Unilever(objSalaryLevel)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryLevel_Unilever
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryLevel_Unilever(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryLevel_Unilever(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryLevel_Unilever
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryLevel_Unilever(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSalaryLevel_Unilever(ByVal objSalaryLevel() As SalaryLevelDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryLevel_Unilever
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryLevel_Unilever(objSalaryLevel)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace

