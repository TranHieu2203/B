Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryType"
        Public Function GetSalaryType(ByVal _filter As SalaryTypeDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryTypeDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryType
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryType(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPaymentSourcesbyYear(ByVal Year As Integer) As List(Of PaymentSourcesDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentSourcesbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPaymentSourcesbyYear(Year)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetListOrgBonus() As List(Of OrgBonusDTO) Implements ServiceContracts.IPayrollBusiness.GetListOrgBonus
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetListOrgBonus()
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryTypebyIncentive(ByVal incentive As Integer) As List(Of SalaryTypeDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryTypebyIncentive
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryTypebyIncentive(incentive)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function InsertSalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryType
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryType(objSalaryType, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryType(ByVal objSalaryType As SalaryTypeDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryType
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryType(objSalaryType)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryType
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryType(objSalaryType, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryType(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryType
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryType(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveSalaryType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveSalaryType
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryType(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace

