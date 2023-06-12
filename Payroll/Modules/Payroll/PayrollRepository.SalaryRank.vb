Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryRank"

    Public Function GetSalaryRank(ByVal _filter As SalaryRankDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        Dim lstSalaryRank As List(Of SalaryRankDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryRank = rep.GetSalaryRank(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryRank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetSalaryRank_Unilever(ByVal _filter As SalaryRankDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        Dim lstSalaryRank As List(Of SalaryRankDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryRank = rep.GetSalaryRank_Unilever(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryRank
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryRank(ByVal _filter As SalaryRankDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryRank(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryRank_Unilever(ByVal _filter As SalaryRankDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryRank_Unilever(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryRankCombo(salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryRankCombo(salLevelID, isBlank)
                End If
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & objSalaryRank.SAL_LEVEL_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & objSalaryRank.SAL_LEVEL_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertSalaryRank(objSalaryRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & objSalaryRank.SAL_LEVEL_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & objSalaryRank.SAL_LEVEL_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifySalaryRank(objSalaryRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal sActive As String, salLevelID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveSalaryRank(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryRank(ByVal objSalaryRank As SalaryRankDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryRank(objSalaryRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryRank(ByVal lstSalaryRank As List(Of SalaryRankDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & lstSalaryRank(0).SAL_LEVEL_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_RANK_LIST_" & lstSalaryRank(0).SAL_LEVEL_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteSalaryRank(lstSalaryRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class
