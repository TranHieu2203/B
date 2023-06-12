Imports Framework.Data
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "Criteria"

        Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO) Implements ServiceContracts.IPerformanceBusiness.GetCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertCriteria(objCriteria, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateCriteria(objCriteria)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyCriteria(objCriteria, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveCriteria(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteCriteria
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteCriteria(lstCriteria)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPE_Criteria_HTCH(ByVal _filter As PE_Criteria_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Criteria_HTCHDTO) Implements ServiceContracts.IPerformanceBusiness.GetPE_Criteria_HTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPE_Criteria_HTCH(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPE_Criteria_HTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPE_Criteria_HTCH(objCriteria, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidatePE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidatePE_Criteria_HTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidatePE_Criteria_HTCH(objCriteria)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPE_Criteria_HTCH(ByVal objCriteria As PE_Criteria_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPE_Criteria_HTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPE_Criteria_HTCH(objCriteria, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePE_Criteria_HTCH(ByVal lstCriteria() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePE_Criteria_HTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePE_Criteria_HTCH(lstCriteria)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_CRITERIAL_DATA_IMPORT() As DataSet Implements ServiceContracts.IPerformanceBusiness.GET_CRITERIAL_DATA_IMPORT
            Try
                Using rep As New PerformanceRepository
                    Return rep.GET_CRITERIAL_DATA_IMPORT()
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function IMPORT_CRITERIAL_DATA(ByVal log As UserLog, ByVal DATA_IN As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.IMPORT_CRITERIAL_DATA
            Try
                Using rep As New PerformanceRepository
                    Return rep.IMPORT_CRITERIAL_DATA(log, DATA_IN)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region
#Region "Classification"

        Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO) Implements ServiceContracts.IPerformanceBusiness.GetClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertClassification(objClassification, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateClassification(objClassification)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyClassification(objClassification, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveClassification(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CalKPI(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPerformanceBusiness.CalKPI
            Try
                Using rep As New PerformanceRepository
                    Return rep.CalKPI(param, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CalKPI_Result(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPerformanceBusiness.CalKPI_Result
            Try
                Using rep As New PerformanceRepository
                    Return rep.CalKPI_Result(param, log)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteClassification
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteClassification(lstClassification)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
#Region "ClassificationHTCH"

        Public Function GetClassificationHTCH(ByVal _filter As ClassificationHTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationHTCHDTO) Implements ServiceContracts.IPerformanceBusiness.GetClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetClassificationHTCH(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertClassificationHTCH(objClassification, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateClassificationHTCH(objClassification)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassificationHTCH(ByVal objClassification As ClassificationHTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyClassificationHTCH(objClassification, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveClassificationHTCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveClassificationHTCH(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteClassificationHTCH(ByVal lstClassification() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteClassificationHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteClassificationHTCH(lstClassification)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
#Region "ObjectGroup"

        Public Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO) Implements ServiceContracts.IPerformanceBusiness.GetObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroup(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertObjectGroup(objObjectGroup, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateObjectGroup(objObjectGroup)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyObjectGroup(objObjectGroup, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActiveObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActiveObjectGroup(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeleteObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteObjectGroup(lstObjectGroup)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Period"

        Public Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPeriodById(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriodById
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriodById(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPeriod(objPeriod, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidatePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidatePeriod(objPeriod)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPeriod(objPeriod, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPerformanceBusiness.ActivePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.ActivePeriod(lstID, log, bActive)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePeriod(lstPeriod)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
#Region "Period HTCH"

        Public Function GetPeriodHTCH(ByVal _filter As PE_Period_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriodHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriodHTCH(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetPeriodHTCHById(ByVal _filter As PE_Period_HTCHDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PE_Period_HTCHDTO) Implements ServiceContracts.IPerformanceBusiness.GetPeriodHTCHById
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetPeriodHTCHById(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertPeriodHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertPeriodHTCH(objPeriod, log, gID)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidatePeriod(ByVal objPeriod As PE_Period_HTCHDTO) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidatePeriodHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidatePeriodHTCH(objPeriod)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPeriodHTCH(ByVal objPeriod As PE_Period_HTCHDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyPeriodHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyPeriodHTCH(objPeriod, log, gID)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePeriodHTCH(ByVal lstPeriod() As Decimal) As Boolean Implements ServiceContracts.IPerformanceBusiness.DeletePeriodHTCH
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeletePeriodHTCH(lstPeriod)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region
    End Class
End Namespace
