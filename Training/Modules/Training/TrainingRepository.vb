Imports Framework.UI
Imports Training.TrainingBusiness

Public Class TrainingRepository
    Inherits TrainingRepositoryBase

    Dim CacheMinusDataCombo As Integer = 30

#Region "Hoadm - List"
    Public Function ToDate(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        ElseIf item = "" Then
            Return Nothing
        Else
            Return DateTime.ParseExact(item, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
        End If
    End Function
    Public Function ToDecimal(ByVal item As Object)
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return CDec(item)
        End If
    End Function

#Region "Other List"

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
                End If
                CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrCertificateList(ByVal dGroupID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrCertificateList(dGroupID, Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrCenterList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrCenterList(Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrPlanByYearOrg(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrPlanByYearOrg(isBlank, dYear, dOrg, Me.Log)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrPlanByYearOrg2(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, Optional ByVal isIrregularly As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrPlanByYearOrg2(isBlank, dYear, dOrg, Me.Log, isIrregularly)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrLectureList(ByVal IsLocal As Boolean, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrLectureList(IsLocal, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHuProvinceList(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetHuProvinceList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHuDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetHuDistrictList(provinceID, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHuContractTypeList(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetHuContractTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrProgramByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrProgramByYear(isBlank, dYear)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrCriteriaGroupList(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrCriteriaGroupList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrAssFormList(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrAssFormList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrChooseProgramFormByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrChooseProgramFormByYear(isBlank, dYear)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrProgramById(ByVal id As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTrProgramById(id)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)
        Dim lstCostCenter As List(Of CostCenterDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCostCenter = rep.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCostCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCostCenter(objCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCostCenter(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCostCenter(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#End Region

#Region "NhatHV - List"
    Public Function GetTitleByList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetTitleByList(Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCourseByList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New TrainingBusinessClient
            Try
                dtData = rep.GetCourseByList(Common.Common.SystemLanguage.Name, isBlank)
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
