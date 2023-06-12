Imports Framework.UI
Imports Recruitment.RecruitmentBusiness

Public Class RecruitmentRepository
    Inherits RecruitmentRepositoryBase

#Region "Common"
    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
                End If
                CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, 60)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetComboList(ByRef _combolist As ComboBoxDataDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.GetComboList(_combolist)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Get_HU_WORK_PLACE(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable

        Using rep As New RecruitmentBusinessClient

            Return rep.Get_HU_WORK_PLACE(sType, Common.Common.SystemLanguage.Name, isBlank)
        End Using

        Return Nothing
    End Function
    Public Function GetProvinceList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetProvinceList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetContractTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetContractTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetTitleByOrgList(orgID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleByOrgListInPlan(ByVal orgID As Decimal, _year As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetTitleByOrgListInPlan(orgID, _year, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramExamsList(ByVal programID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetProgramExamsList(programID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProgramList(ByVal orgID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetProgramList(orgID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function



#End Region

End Class
