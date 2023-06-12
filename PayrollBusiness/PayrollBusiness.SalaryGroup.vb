Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "SalaryGroup"
        Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryGroup
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetEffectSalaryGroup() As SalaryGroupDTO Implements ServiceContracts.IPayrollBusiness.GetEffectSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.GetEffectSalaryGroup()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSalaryGroupCombo(dateValue As Date, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryGroupCombo
            Using rep As New PayrollRepository
                Try

                    Dim lst = rep.GetSalaryGroupCombo(dateValue, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryGroup(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.ValidateSalaryGroup(objSalaryGroup)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryGroup(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSalaryGroup
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSalaryGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryGroup(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_IMPORT_QUYLUONG() As DataSet Implements ServiceContracts.IPayrollBusiness.GET_IMPORT_QUYLUONG
            Try
                Return PayrollRepositoryStatic.Instance.GET_IMPORT_QUYLUONG()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IMPORT_QUYLUONG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean Implements ServiceContracts.IPayrollBusiness.IMPORT_QUYLUONG
            Try
                Return PayrollRepositoryStatic.Instance.IMPORT_QUYLUONG(P_DOCXML, P_USER)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmpNotQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO) Implements ServiceContracts.IPayrollBusiness.GetEmpNotQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.GetEmpNotQuyLuong(_filter, _param, log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEmpQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO) Implements ServiceContracts.IPayrollBusiness.GetEmpQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.GetEmpQuyLuong(_filter, _param, log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Don Vi Quy Luong"
        Public Function GetDonViQuyLuong(ByVal _filter As DonViQuyLuongDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "NAME ASC") As List(Of DonViQuyLuongDTO) Implements ServiceContracts.IPayrollBusiness.GetDonViQuyLuong
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetDonViQuyLuong(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertDonViQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.InsertDonViQuyLuong(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyDonViQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.ModifyDonViQuyLuong(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Function ActiveDonViQuyLuong(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveDonViQuyLuong
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveDonViQuyLuong(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteDonViQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteDonViQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.DeleteDonViQuyLuong(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateDonViQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.ValidateDonViQuyLuong(objSalaryGroup)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetSalaryQuyLuong(ByVal _filter As SalaryQuyLuongDTO, ByVal PageIndex As Integer,
                        ByVal PageSize As Integer,
                        ByRef Total As Integer,
                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of SalaryQuyLuongDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryQuyLuong
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSalaryQuyLuong(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSalaryQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.InsertSalaryQuyLuong(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifySalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySalaryQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.ModifySalaryQuyLuong(objSalaryGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteSalaryQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSalaryQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSalaryQuyLuong(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function LOAD_DONVI_QUYLUONG() As List(Of DonViQuyLuongDTO) Implements ServiceContracts.IPayrollBusiness.LOAD_DONVI_QUYLUONG
            Try
                Return PayrollRepositoryStatic.Instance.LOAD_DONVI_QUYLUONG()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertEmpQuyLuong(ByVal obj As EmpQuyLuongDTO, ByVal lstID As List(Of Decimal),
                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertEmpQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.InsertEmpQuyLuong(obj, lstID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteEmpQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteEmpQuyLuong
            Try
                Return PayrollRepositoryStatic.Instance.DeleteEmpQuyLuong(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
    End Class

End Namespace

