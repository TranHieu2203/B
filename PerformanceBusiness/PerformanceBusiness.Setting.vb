Imports Framework.Data
Imports PerformanceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace PerformanceBusiness.ServiceImplementations
    Partial Class PerformanceBusiness

#Region "ObjectGroupPeriod"
        Public Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As _
                                         List(Of ObjectGroupPeriodDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetObjectGroupNotByPeriodID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroupNotByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As _
                                         List(Of ObjectGroupPeriodDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetObjectGroupByPeriodID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetObjectGroupByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO),
                                              ByVal log As UserLog) As Boolean _
                                          Implements ServiceContracts.IPerformanceBusiness.InsertObjectGroupByPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertObjectGroupByPeriod(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.DeleteObjectGroupByPeriod
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteObjectGroupByPeriod(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "CriteriaObjectGroup"
        Public Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As _
                                         List(Of CriteriaObjectGroupDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetCriteriaNotByObjectGroupID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As _
                                         List(Of CriteriaObjectGroupDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetCriteriaByObjectGroupID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                              ByVal log As UserLog) As Boolean _
                                          Implements ServiceContracts.IPerformanceBusiness.InsertCriteriaByObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertCriteriaByObjectGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.DeleteCriteriaByObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteCriteriaByObjectGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.UpdateCriteriaObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.UpdateCriteriaObjectGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function


#End Region

#Region "EmployeeAssessment"
        Public Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE",
                                                Optional ByVal log As UserLog = Nothing) As _
                                         List(Of EmployeeAssessmentDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetEmployeeNotByObjectGroupID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function


        Public Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As _
                                         List(Of EmployeeAssessmentDTO) _
                                         Implements ServiceContracts.IPerformanceBusiness.GetEmployeeByObjectGroupID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetEmployeeByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO),
                                              ByVal log As UserLog) As Boolean _
                                          Implements ServiceContracts.IPerformanceBusiness.InsertEmployeeByObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertEmployeeByObjectGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.DeleteEmployeeByObjectGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteEmployeeByObjectGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "THIẾT LẬP NHÓM CHỨC DANH THEO TIÊU CHÍ ĐÁNH GIÁ"
        Public Function getCriteriaTitleGroup(ByVal _filter As CriteriaTitleGroupDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupDTO) Implements ServiceContracts.IPerformanceBusiness.getCriteriaTitleGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.getCriteriaTitleGroup(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function DeleteCriteriaTitleGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.DeleteCriteriaTitleGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteCriteriaTitleGroup(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetCriteriaTitleGroupbyID(ByVal objUpdate As CriteriaTitleGroupDTO,
                                            ByVal log As UserLog) As CriteriaTitleGroupDTO Implements ServiceContracts.IPerformanceBusiness.GetCriteriaTitleGroupbyID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaTitleGroupbyID(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertCriteriaTitleGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertCriteriaTitleGroup(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ModifyCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyCriteriaTitleGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyCriteriaTitleGroup(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ValidateCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO,
                                                ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateCriteriaTitleGroup
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateCriteriaTitleGroup(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ValidateCriteriaTitleGroup_Detail(ByVal objUpdate As CriteriaTitleGroupDTO,
                                                ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ValidateCriteriaTitleGroup_Detail
            Try
                Using rep As New PerformanceRepository
                    Return rep.ValidateCriteriaTitleGroup_Detail(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "Thang điểm theo nhóm chức danh và tiêu chí"
        Public Function getCriteriaTitleGroupRank(ByVal _filter As CriteriaTitleGroupRankDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupRankDTO) Implements ServiceContracts.IPerformanceBusiness.getCriteriaTitleGroupRank
            Try
                Using rep As New PerformanceRepository
                    Return rep.getCriteriaTitleGroupRank(_filter, PageIndex, PageSize, Total, Sorts)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteCriteriaTitleGroupRank(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean _
                               Implements ServiceContracts.IPerformanceBusiness.DeleteCriteriaTitleGroupRank
            Try
                Using rep As New PerformanceRepository
                    Return rep.DeleteCriteriaTitleGroupRank(lst, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCriteriaTitleGroupRankbyID(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                              ByVal log As UserLog) As CriteriaTitleGroupRankDTO Implements ServiceContracts.IPerformanceBusiness.GetCriteriaTitleGroupRankbyID
            Try
                Using rep As New PerformanceRepository
                    Return rep.GetCriteriaTitleGroupRankbyID(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.ModifyCriteriaTitleGroupRank
            Try
                Using rep As New PerformanceRepository
                    Return rep.ModifyCriteriaTitleGroupRank(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO,
                                                ByVal log As UserLog) As Boolean Implements ServiceContracts.IPerformanceBusiness.InsertCriteriaTitleGroupRank
            Try
                Using rep As New PerformanceRepository
                    Return rep.InsertCriteriaTitleGroupRank(objUpdate, log)
                End Using
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

    End Class
End Namespace
