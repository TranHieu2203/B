Imports Training.TrainingBusiness

Partial Class TrainingRepository

#Region "Certificate"

    Public Function GetCertificate(ByVal _filter As CertificateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO)
        Dim lstCertificate As List(Of CertificateDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetCertificate(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCertificate(ByVal _filter As CertificateDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO)
        Dim lstCertificate As List(Of CertificateDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCertificate = rep.GetCertificate(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCertificate(ByVal objCertificate As CertificateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCertificate(objCertificate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCertificate(ByVal objCertificate As CertificateDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCertificate(objCertificate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCertificate(ByVal objCertificate As CertificateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCertificate(objCertificate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveCertificate(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCertificate(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCertificate(ByVal lstCertificate As List(Of CertificateDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCertificate(lstCertificate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Course"

    Public Function GetCourse(ByVal _filter As CourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO)
        Dim lstCourse As List(Of CourseDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCourse = rep.GetCourse(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCourse(ByVal _filter As CourseDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO)
        Dim lstCourse As List(Of CourseDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCourse = rep.GetCourse(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCourse(ByVal objCourse As CourseDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCourse(objCourse, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCourse(ByVal objCourse As CourseDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCourse(objCourse)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCourse(ByVal objCourse As CourseDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCourse(objCourse, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCourse(ByVal lstID As List(Of Decimal), ByVal bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCourse(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCourse(ByVal lstCourse As List(Of CourseDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCourse(lstCourse)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Center"

    Public Function GetCenter(ByVal _filter As CenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO)
        Dim lstCenter As List(Of CenterDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCenter = rep.GetCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCenter(ByVal _filter As CenterDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO)
        Dim lstCenter As List(Of CenterDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCenter = rep.GetCenter(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCenter
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCenter(ByVal objCenter As CenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCenter(objCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCenter(ByVal objCenter As CenterDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCenter(objCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCenter(ByVal objCenter As CenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCenter(objCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCenter(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCenter(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCenter(ByVal lstCenter As List(Of CenterDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCenter(lstCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Lecture"

    Public Function GetLecture(ByVal _filter As LectureDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO)
        Dim lstLecture As List(Of LectureDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstLecture = rep.GetLecture(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstLecture
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetLecture(ByVal _filter As LectureDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO)
        Dim lstLecture As List(Of LectureDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstLecture = rep.GetLecture(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstLecture
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertLecture(ByVal objLecture As LectureDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertLecture(objLecture, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateLecture(ByVal objLecture As LectureDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateLecture(objLecture)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyLecture(ByVal objLecture As LectureDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyLecture(objLecture, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveLecture(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveLecture(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteLecture(ByVal lstLecture As List(Of LectureDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteLecture(lstLecture)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CommitAfterTrain"

    Public Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO)
        Dim lstCommitAfterTrain As List(Of CommitAfterTrainDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCommitAfterTrain = rep.GetCommitAfterTrain(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCommitAfterTrain
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO)
        Dim lstCommitAfterTrain As List(Of CommitAfterTrainDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCommitAfterTrain = rep.GetCommitAfterTrain(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCommitAfterTrain
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCommitAfterTrain(objCommitAfterTrain, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCommitAfterTrain(objCommitAfterTrain, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCommitAfterTrain(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCommitAfterTrain(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCommitAfterTrain(ByVal lstCommitAfterTrain As List(Of CommitAfterTrainDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCommitAfterTrain(lstCommitAfterTrain)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Criteria"

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)
        Dim lstCriteria As List(Of CriteriaDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCriteria = rep.GetCriteria(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCriteria
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCriteria(objCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCriteria(objCriteria, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCriteria(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCriteria(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCriteria(ByVal lstCriteria As List(Of CriteriaDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCriteria(lstCriteria)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CriteriaGroup"

    Public Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO)
        Dim lstCriteriaGroup As List(Of CriteriaGroupDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCriteriaGroup = rep.GetCriteriaGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCriteriaGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO)
        Dim lstCriteriaGroup As List(Of CriteriaGroupDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstCriteriaGroup = rep.GetCriteriaGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCriteriaGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertCriteriaGroup(objCriteriaGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateCriteriaGroup(objCriteriaGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyCriteriaGroup(objCriteriaGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCriteriaGroup(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveCriteriaGroup(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCriteriaGroup(ByVal lstCriteriaGroup As List(Of CriteriaGroupDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteCriteriaGroup(lstCriteriaGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AssessmentRate"

    Public Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO)
        Dim lstAssessmentRate As List(Of AssessmentRateDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstAssessmentRate = rep.GetAssessmentRate(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAssessmentRate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO)
        Dim lstAssessmentRate As List(Of AssessmentRateDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstAssessmentRate = rep.GetAssessmentRate(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAssessmentRate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertAssessmentRate(objAssessmentRate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyAssessmentRate(objAssessmentRate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAssessmentRate(ByVal lstID As List(Of Decimal), bActive As Boolean) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveAssessmentRate(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAssessmentRate(ByVal lstAssessmentRate As List(Of AssessmentRateDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteAssessmentRate(lstAssessmentRate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AssessmentForm"

    Public Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO)
        Dim lstAssessmentForm As List(Of AssessmentFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstAssessmentForm = rep.GetAssessmentForm(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstAssessmentForm
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO)
        Dim lstAssessmentForm As List(Of AssessmentFormDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstAssessmentForm = rep.GetAssessmentForm(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstAssessmentForm
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertAssessmentForm(objAssessmentForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyAssessmentForm(objAssessmentForm, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAssessmentForm(ByVal lstAssessmentForm As List(Of AssessmentFormDTO)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteAssessmentForm(lstAssessmentForm)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Classification"

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetClassification(ByVal _filter As ClassificationDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)
        Dim lstClassification As List(Of ClassificationDTO)

        Using rep As New TrainingBusinessClient
            Try
                lstClassification = rep.GetClassification(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstClassification
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.InsertClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ValidateClassification(objClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ModifyClassification(objClassification, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal bActive As String) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.ActiveClassification(lstID, Me.Log, bActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function DeleteClassification(ByVal lstClassification As List(Of Decimal)) As Boolean
        Using rep As New TrainingBusinessClient
            Try
                Return rep.DeleteClassification(lstClassification)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
End Class
