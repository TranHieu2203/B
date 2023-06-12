Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "Competency Course"

        Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetCompetencyCourse(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertCompetencyCourse(objCompetencyCourse, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyCompetencyCourse(objCompetencyCourse, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCompetencyCourse
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteCompetencyCourse(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region


#Region "Salary Items Percent"

        Public Function GetSalItemsPercent(ByVal _filter As SalaryItemsPercentDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of SalaryItemsPercentDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetSalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSalItemsPercent(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IProfileBusiness.GetPaymentListAll
            Using rep As New ProfileRepository
                Try
                    Return rep.GetPaymentListAll(Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertSalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertSalItemsPercent(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifySalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifySalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifySalItemsPercent(obj, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateSalItemsPercent(ByVal obj As SalaryItemsPercentDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateSalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateSalItemsPercent(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteSalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteSalItemsPercent(lstID, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal status As String, ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ActiveSalItemsPercent
            Using rep As New ProfileRepository
                Try

                    Return rep.ActiveSalItemsPercent(lstID, status, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace