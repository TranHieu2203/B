Imports Framework.Data
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "IncentiveRank"
        Public Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO) Implements ServiceContracts.IPayrollBusiness.GetIncentiveRank
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetIncentiveRank(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetIncentiveRankIdIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As IncentiveRankDTO Implements ServiceContracts.IPayrollBusiness.GetIncentiveRankIdIncludeDetail
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetIncentiveRankIdIncludeDetail(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TO_TARGET, CREATED_DATE desc") As List(Of IncentiveRankDetailDTO) Implements ServiceContracts.IPayrollBusiness.GetIncentiveRankDetail
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetIncentiveRankDetail(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO) Implements ServiceContracts.IPayrollBusiness.GetIncentiveRankIncludeDetail
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetIncentiveRankIncludeDetail(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertIncentiveRankIncludeDetail
            Try
                Return PayrollRepositoryStatic.Instance.InsertIncentiveRankIncludeDetail(objIncentiveRank, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertIncentiveRank
            Try
                Return PayrollRepositoryStatic.Instance.InsertIncentiveRank(objIncentiveRank, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertIncentiveRankDetail
            Try
                Return PayrollRepositoryStatic.Instance.InsertIncentiveRankDetail(objIncentiveRankDetail, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertIncentiveListRankDetail(ByVal objIncentiveRankDetail As List(Of IncentiveRankDetailDTO),
                                   ByVal log As UserLog, ByRef gID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertIncentiveListRankDetail
            Try
                Return PayrollRepositoryStatic.Instance.InsertIncentiveListRankDetail(objIncentiveRankDetail, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyIncentiveRank
            Try
                Return PayrollRepositoryStatic.Instance.ModifyIncentiveRank(objIncentiveRank, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyIncentiveRankDetail
            Try
                Return PayrollRepositoryStatic.Instance.ModifyIncentiveRankDetail(objIncentiveRankDetail, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyIncentiveRankIncludeDetail(ByVal objIncentive As IncentiveRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyIncentiveRankIncludeDetail
            Try
                Return PayrollRepositoryStatic.Instance.ModifyIncentiveRankIncludeDetail(objIncentive, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateIncentiveRank(ByVal _validate As IncentiveRankDTO) Implements ServiceContracts.IPayrollBusiness.ValidateIncentiveRank
            Try
                Return PayrollRepositoryStatic.Instance.ValidateIncentiveRank(_validate)
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function ActiveIncentiveRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveIncentiveRank
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveIncentiveRank(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ActiveIncentiveRankDetail(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ActiveIncentiveRankDetail
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveIncentiveRankDetail(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteIncentiveRank(ByVal lstIncentiveRank As List(Of IncentiveRankDTO)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteIncentiveRank
            Try
                Return PayrollRepositoryStatic.Instance.DeleteIncentiveRank(lstIncentiveRank)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteIncentiveRankDetail(ByVal lstIncentiveRank() As IncentiveRankDetailDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteIncentiveRankDetail
            Try
                Return PayrollRepositoryStatic.Instance.DeleteIncentiveRankDetail(lstIncentiveRank)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace

