Imports Performance.PerformanceBusiness

Partial Class PerformanceRepository

#Region "ObjectGroupPeriod"

    Public Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As _
                                         List(Of ObjectGroupPeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupNotByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE desc") As _
                                         List(Of ObjectGroupPeriodDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetObjectGroupByPeriodID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertObjectGroupByPeriod(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteObjectGroupByPeriod(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "CriteriaObjectGroup"

    Public Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As _
                                         List(Of CriteriaObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As _
                                         List(Of CriteriaObjectGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteriaByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteriaByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO)) As Boolean
        Using rep As New PerformanceBusinessClient
            Try
                Return rep.UpdateCriteriaObjectGroup(lst, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "EmployeeAssessment"

    Public Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As _
                                         List(Of EmployeeAssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeeNotByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As _
                                         List(Of EmployeeAssessmentDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetEmployeeByObjectGroupID(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertEmployeeByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteEmployeeByObjectGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "THIẾT LẬP NHÓM CHỨC DANH THEO TIÊU CHÍ ĐÁNH GIÁ"
    Public Function getCriteriaTitleGroup(ByVal _filter As CriteriaTitleGroupDTO,
                                              ByVal PageIndex As Integer,
                                              ByVal PageSize As Integer,
                                              ByRef Total As Integer,
                                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.getCriteriaTitleGroup(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteCriteriaTitleGroup(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteriaTitleGroup(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteriaTitleGroup(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyCriteriaTitleGroup(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateCriteriaTitleGroup(ByVal objUpdate As CriteriaTitleGroupDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateCriteriaTitleGroup(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateCriteriaTitleGroup_Detail(ByVal objUpdate As CriteriaTitleGroupDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ValidateCriteriaTitleGroup_Detail(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCriteriaTitleGroupbyID(ByVal objUpdate As CriteriaTitleGroupDTO) As CriteriaTitleGroupDTO

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaTitleGroupbyID(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "Thang điểm theo nhóm chức danh và tiêu chí"
    Public Function getCriteriaTitleGroupRank(ByVal _filter As CriteriaTitleGroupRankDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaTitleGroupRankDTO)

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.getCriteriaTitleGroupRank(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteCriteriaTitleGroupRank(ByVal lst As List(Of Decimal)) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.DeleteCriteriaTitleGroupRank(lst, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaTitleGroupRankbyID(ByVal objUpdate As CriteriaTitleGroupRankDTO) As CriteriaTitleGroupRankDTO

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.GetCriteriaTitleGroupRankbyID(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.ModifyCriteriaTitleGroupRank(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteriaTitleGroupRank(ByVal objUpdate As CriteriaTitleGroupRankDTO) As Boolean

        Using rep As New PerformanceBusinessClient
            Try
                Return rep.InsertCriteriaTitleGroupRank(objUpdate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region


End Class
