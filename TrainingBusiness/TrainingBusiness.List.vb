Imports Framework.Data
Imports TrainingDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace TrainingBusiness.ServiceImplementations
    Partial Class TrainingBusiness

#Region "Certificate"

        Public Function GetCertificate(ByVal _filter As CertificateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO) Implements ServiceContracts.ITrainingBusiness.GetCertificate
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCertificate(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCertificate
            Try
                Return TrainingRepositoryStatic.Instance.InsertCertificate(objCertificate, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCertificate(ByVal objCertificate As CertificateDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCertificate
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCertificate(objCertificate)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCertificate
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCertificate(objCertificate, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCertificate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCertificate
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCertificate(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCertificate(ByVal lstCertificate() As CertificateDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCertificate
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCertificate(lstCertificate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Course"

        Public Function GetCourse(ByVal _filter As CourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO) Implements ServiceContracts.ITrainingBusiness.GetCourse
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCourse(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCourse
            Try
                Return TrainingRepositoryStatic.Instance.InsertCourse(objCourse, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCourse(ByVal objCourse As CourseDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCourse
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCourse(objCourse)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCourse
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCourse(objCourse, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCourse
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCourse(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCourse(ByVal lstCourse() As CourseDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCourse
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCourse(lstCourse)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Center"

        Public Function GetCenter(ByVal _filter As CenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO) Implements ServiceContracts.ITrainingBusiness.GetCenter
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCenter
            Try
                Return TrainingRepositoryStatic.Instance.InsertCenter(objCenter, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCenter(ByVal objCenter As CenterDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCenter
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCenter(objCenter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCenter
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCenter(objCenter, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCenter
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCenter(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCenter(ByVal lstCenter() As CenterDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCenter
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCenter(lstCenter)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Lecture"

        Public Function GetLecture(ByVal _filter As LectureDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO) Implements ServiceContracts.ITrainingBusiness.GetLecture
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetLecture(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertLecture
            Try
                Return TrainingRepositoryStatic.Instance.InsertLecture(objLecture, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateLecture(ByVal objLecture As LectureDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateLecture
            Try
                Return TrainingRepositoryStatic.Instance.ValidateLecture(objLecture)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyLecture
            Try
                Return TrainingRepositoryStatic.Instance.ModifyLecture(objLecture, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveLecture(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveLecture
            Try
                Return TrainingRepositoryStatic.Instance.ActiveLecture(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteLecture(ByVal lstLecture() As LectureDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteLecture
            Try
                Return TrainingRepositoryStatic.Instance.DeleteLecture(lstLecture)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCenters() As List(Of CenterDTO) Implements ServiceContracts.ITrainingBusiness.GetCenters
            Try
                Return TrainingRepositoryStatic.Instance.GetCenters()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "CommitAfterTrain"

        Public Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO) Implements ServiceContracts.ITrainingBusiness.GetCommitAfterTrain
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCommitAfterTrain(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCommitAfterTrain
            Try
                Return TrainingRepositoryStatic.Instance.InsertCommitAfterTrain(objCommitAfterTrain, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCommitAfterTrain
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCommitAfterTrain(objCommitAfterTrain, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCommitAfterTrain(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCommitAfterTrain
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCommitAfterTrain(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCommitAfterTrain(ByVal lstCommitAfterTrain() As CommitAfterTrainDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCommitAfterTrain
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCommitAfterTrain(lstCommitAfterTrain)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "Criteria"

        Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO) Implements ServiceContracts.ITrainingBusiness.GetCriteria
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCriteria
            Try
                Return TrainingRepositoryStatic.Instance.InsertCriteria(objCriteria, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCriteria
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCriteria(objCriteria)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCriteria
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCriteria(objCriteria, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCriteria
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCriteria(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCriteria(ByVal lstCriteria() As CriteriaDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCriteria
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCriteria(lstCriteria)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "CriteriaGroup"

        Public Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO) Implements ServiceContracts.ITrainingBusiness.GetCriteriaGroup
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetCriteriaGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.InsertCriteriaGroup(objCriteriaGroup, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.ValidateCriteriaGroup(objCriteriaGroup)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.ModifyCriteriaGroup(objCriteriaGroup, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.ActiveCriteriaGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteCriteriaGroup(ByVal lstCriteriaGroup() As CriteriaGroupDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteCriteriaGroup
            Try
                Return TrainingRepositoryStatic.Instance.DeleteCriteriaGroup(lstCriteriaGroup)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "AssessmentRate"

        Public Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO) Implements ServiceContracts.ITrainingBusiness.GetAssessmentRate
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetAssessmentRate(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertAssessmentRate
            Try
                Return TrainingRepositoryStatic.Instance.InsertAssessmentRate(objAssessmentRate, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyAssessmentRate
            Try
                Return TrainingRepositoryStatic.Instance.ModifyAssessmentRate(objAssessmentRate, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveAssessmentRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveAssessmentRate
            Try
                Return TrainingRepositoryStatic.Instance.ActiveAssessmentRate(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteAssessmentRate(ByVal lstAssessmentRate() As AssessmentRateDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteAssessmentRate
            Try
                Return TrainingRepositoryStatic.Instance.DeleteAssessmentRate(lstAssessmentRate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

#Region "AssessmentForm"

        Public Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO) Implements ServiceContracts.ITrainingBusiness.GetAssessmentForm
            Try
                Dim lst = TrainingRepositoryStatic.Instance.GetAssessmentForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertAssessmentForm
            Try
                Return TrainingRepositoryStatic.Instance.InsertAssessmentForm(objAssessmentForm, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyAssessmentForm
            Try
                Return TrainingRepositoryStatic.Instance.ModifyAssessmentForm(objAssessmentForm, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAssessmentForm(ByVal lstAssessmentForm() As AssessmentFormDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteAssessmentForm
            Try
                Return TrainingRepositoryStatic.Instance.DeleteAssessmentForm(lstAssessmentForm)
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
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO) Implements ServiceContracts.ITrainingBusiness.GetClassification
            Try
                Return TrainingRepositoryStatic.Instance.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.InsertClassification
            Try
                Return TrainingRepositoryStatic.Instance.InsertClassification(objClassification, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean Implements ServiceContracts.ITrainingBusiness.ValidateClassification
            Try
                Return TrainingRepositoryStatic.Instance.ValidateClassification(objClassification)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.ModifyClassification
            Try
                Return TrainingRepositoryStatic.Instance.ModifyClassification(objClassification, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.ITrainingBusiness.ActiveClassification
            Try
                Return TrainingRepositoryStatic.Instance.ActiveClassification(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean Implements ServiceContracts.ITrainingBusiness.DeleteClassification
            Try
                Return TrainingRepositoryStatic.Instance.DeleteClassification(lstClassification)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

    End Class
End Namespace
