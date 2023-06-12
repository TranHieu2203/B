Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryLevel"
        Public Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryLevel
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryLevel(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryLevel
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryLevel(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryLevel
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryLevel(objSalaryLevel)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryLevel
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryLevel(objSalaryLevel, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryLevel
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryLevel(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSalaryLevel(ByVal objSalaryLevel As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryLevel
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryLevel(objSalaryLevel)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace

