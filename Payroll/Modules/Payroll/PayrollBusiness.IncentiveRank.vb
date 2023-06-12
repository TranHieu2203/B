Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "IncentiveRank"

    Public Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetIncentiveRank(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRankIdIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As IncentiveRankDTO
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetIncentiveRankIdIncludeDetail(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TO_TARGET, CREATED_DATE desc") As List(Of IncentiveRankDetailDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetIncentiveRankDetail(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetIncentiveRankIncludeDetail(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetIncentiveRank(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDetailDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetIncentiveRankDetail(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetIncentiveRankIncludeDetail(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertIncentiveRankIncludeDetail(objIncentiveRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertIncentiveRank(objIncentiveRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRankDetail.INCENTIVE_RANK_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRankDetail.INCENTIVE_RANK_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.InsertIncentiveRankDetail(objIncentiveRankDetail, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertIncentiveListRankDetail(ByVal objIncentiveRankDetail As List(Of IncentiveRankDetailDTO), ByRef gID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertIncentiveListRankDetail(objIncentiveRankDetail, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyIncentiveRank(objIncentiveRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRankDetail.INCENTIVE_RANK_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRankDetail.INCENTIVE_RANK_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyIncentiveRankDetail(objIncentiveRankDetail, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & objIncentiveRank.ID & IIf(False, "Blank", "NoBlank"))
                Return rep.ModifyIncentiveRankIncludeDetail(objIncentiveRank, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateIncentiveRank(objIncentiveRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveIncentiveRank(ByVal lstID As List(Of Decimal), ByVal sActive As String, salGroupID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & salGroupID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & salGroupID & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveIncentiveRank(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveIncentiveRankDetail(ByVal lstID As List(Of Decimal), ByVal sActive As String, salIncentiveID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & salIncentiveID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & salIncentiveID & IIf(False, "Blank", "NoBlank"))
                Return rep.ActiveIncentiveRankDetail(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteIncentiveRank(ByVal lstIncentiveRank As List(Of IncentiveRankDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try

                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & lstIncentiveRank(0).SAL_GROUP_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & lstIncentiveRank(0).SAL_GROUP_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteIncentiveRank(lstIncentiveRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteIncentiveRankDetail(ByVal lstIncentiveRank As List(Of IncentiveRankDetailDTO)) As Boolean
        Using rep As New PayrollBusinessClient
            Try

                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & lstIncentiveRank(0).INCENTIVE_RANK_ID & IIf(True, "Blank", "NoBlank"))
                CacheManager.ClearValue("OT_PA_INCENTIVE_RANK_LIST_" & lstIncentiveRank(0).INCENTIVE_RANK_ID & IIf(False, "Blank", "NoBlank"))
                Return rep.DeleteIncentiveRankDetail(lstIncentiveRank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

End Class
