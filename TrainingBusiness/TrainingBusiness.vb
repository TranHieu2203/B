Imports Framework.Data
Imports TrainingBusiness.ServiceContracts
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Public Class TrainingBusiness
        Implements ITrainingBusiness

        Public Function TestService(ByVal str As String) As String Implements ServiceContracts.ITrainingBusiness.TestService
            Return "Hello world " & str
        End Function
        Public Function test(ByVal a As CostDetailDTO) As CostDetailDTO Implements ServiceContracts.ITrainingBusiness.test
            Try
                Return TrainingRepositoryStatic.Instance.test(a)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#Region "Other List"

        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.ITrainingBusiness.GetOtherList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetOtherList(sType, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrCertificateList(dGroupID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.ITrainingBusiness.GetTrCertificateList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrCertificateList(dGroupID, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrCenterList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.ITrainingBusiness.GetTrCenterList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrCenterList(sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrPlanByYearOrg(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrPlanByYearOrg
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrPlanByYearOrg(isBlank, dYear, dOrg, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrPlanByYearOrg2(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog, Optional ByVal isIrregularly As Boolean = False) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrPlanByYearOrg2
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrPlanByYearOrg2(isBlank, dYear, dOrg, log, isIrregularly)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrLectureList(ByVal isLocal As Boolean, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrLectureList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrLectureList(isLocal, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHuProvinceList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetHuProvinceList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetHuProvinceList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHuDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetHuDistrictList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetHuDistrictList(provinceID, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetHuContractTypeList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetHuContractTypeList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetHuContractTypeList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrProgramByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrProgramByYear
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrProgramByYear(isBlank, dYear)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrCriteriaGroupList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrCriteriaGroupList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrCriteriaGroupList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTrAssFormList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrAssFormList
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrAssFormList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetTrProgramById(ByVal id As Decimal) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrProgramById
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrProgramById(id)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetTrChooseProgramFormByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable _
            Implements ServiceContracts.ITrainingBusiness.GetTrChooseProgramFormByYear
            Try
                Dim rep As New TrainingRepository
                Dim lst = rep.GetTrChooseProgramFormByYear(isBlank, dYear)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CostCenter"

        Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO) Implements ServiceContracts.ITrainingBusiness.GetCostCenter
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCostCenter
            Try
                Return TrainingRepositoryStatic.Instance.InsertCostCenter(objCostCenter, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCostCenter
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCostCenter(objCostCenter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCostCenter
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCostCenter(objCostCenter, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCostCenter
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCostCenter(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCostCenter
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCostCenter(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Get List"
        Public Function GetTitleByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.ITrainingBusiness.GetTitleByList
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetTitleByList(sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.ITrainingBusiness.GetCourseByList
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCourseByList(sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

    End Class
End Namespace
