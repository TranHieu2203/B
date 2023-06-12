Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryLevel"

    Public Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryLevel_Unilever(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryLevel_Unilever(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryLevelCombo(salGroupID As Decimal, ByVal isBlank As Boolean, Optional ByVal other_Use As String = "ALL") As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryLevelCombo(salGroupID, isBlank, other_Use)
                End If
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & objSalaryLevel.SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & objSalaryLevel.SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertSalaryLevel_Unilever(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryLevel_Unilever(objSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveSalaryLevel_Unilever(ByVal lstID As List(Of Decimal), ByVal sActive As String, salGroupID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveSalaryLevel_Unilever(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ModifySalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & objSalaryLevel.SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & objSalaryLevel.SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifySalaryLevel_Unilever(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryLevel_Unilever(ByVal lstSalaryLevel As List(Of SalaryLevelDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try

                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & lstSalaryLevel(0).SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_SALARY_LEVEL_LIST_" & lstSalaryLevel(0).SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteSalaryLevel_Unilever(lstSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class
