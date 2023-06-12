Imports Framework.Data
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Class TrainingBusiness

#Region "SettingCriteriaGroup"

        Public Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetCriteriaNotByGroupID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteriaNotByGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetCriteriaByGroupID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteriaByGroupID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertSettingCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.InsertSettingCriteriaGroup(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSettingCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.DeleteSettingCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.DeleteSettingCriteriaGroup(lstID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "SettingAssForm"

        Public Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetCriteriaGroupNotByFormID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteriaGroupNotByFormID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO) _
            Implements ServiceContracts.ITrainingBusiness.GetCriteriaGroupByFormID
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteriaGroupByFormID(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.InsertSettingAssForm
            Try
                Return TrainingRepositoryStatic.Instance.InsertSettingAssForm(lst, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteSettingAssForm(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.DeleteSettingAssForm
            Try
                Return TrainingRepositoryStatic.Instance.DeleteSettingAssForm(lstID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Setting Title Course"
        Public Function GetTitleCourse(ByVal _filter As TitleCourseDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO) Implements ServiceContracts.ITrainingBusiness.GetTitleCourse
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetTitleCourse(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateTitleCourse(ByVal objExams As TitleCourseDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ITrainingBusiness.UpdateTitleCourse
            Try
                Return TrainingRepositoryStatic.Instance.UpdateTitleCourse(objExams, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteTitleCourse
            Try
                Return TrainingRepositoryStatic.Instance.DeleteTitleCourse(obj)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "SettingCriteriaCourse"
        Public Function GET_SETTING_CRITERIA_DETAIL_BY_COURSE(ByVal _filter As SettingCriteriaDetailDTO,
                                 Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaDetailDTO) _
                                                Implements ServiceContracts.ITrainingBusiness.GET_SETTING_CRITERIA_DETAIL_BY_COURSE
            Try
                Return TrainingRepositoryStatic.Instance.GET_SETTING_CRITERIA_DETAIL_BY_COURSE(_filter, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GET_SETTING_CRITERIA_COURSE(ByVal _filter As SettingCriteriaCourseDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "ID desc") As List(Of SettingCriteriaCourseDTO) _
                                                Implements ServiceContracts.ITrainingBusiness.GET_SETTING_CRITERIA_COURSE
            Try
                Return TrainingRepositoryStatic.Instance.GET_SETTING_CRITERIA_COURSE(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function INSERT_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.INSERT_SETTING_CRITERIA_COURSE
            Try
                Return TrainingRepositoryStatic.Instance.INSERT_SETTING_CRITERIA_COURSE(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function MODIFY_SETTING_CRITERIA_COURSE(ByVal obj As SettingCriteriaCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.MODIFY_SETTING_CRITERIA_COURSE
            Try
                Return TrainingRepositoryStatic.Instance.MODIFY_SETTING_CRITERIA_COURSE(obj, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DELETE_SETTING_CRITERIA_COURSE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.ITrainingBusiness.DELETE_SETTING_CRITERIA_COURSE
            Try
                Return TrainingRepositoryStatic.Instance.DELETE_SETTING_CRITERIA_COURSE(lstID, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace
