Imports Recruitment.RecruitmentBusiness

Partial Class RecruitmentRepository
#Region "danh muc phuong xa"
    Public Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable
        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.GetWardList(districtID, isBlank)
                Return dtData
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Hoadm - List"

#Region "CostCenter"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)
        Dim lstCostCenter As List(Of CostCenterDTO)

        Using rep As New RecruitmentBusinessClient
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
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.InsertCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ValidateCostCenter(objCostCenter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByRef gID As Decimal) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ModifyCostCenter(objCostCenter, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCostCenter(ByVal lstCostCenter As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.ActiveCostCenter(lstCostCenter, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteCostCenter(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "ExamsDtl"

    Public Function GetExamsDtl(ByVal _filter As ExamsDtlDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO)
        Dim lstExamsDtl As List(Of ExamsDtlDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstExamsDtl = rep.GetExamsDtl(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstExamsDtl
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.UpdateExamsDtl(objExams, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean
        Using rep As New RecruitmentBusinessClient
            Try
                Return rep.DeleteExamsDtl(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#End Region
#Region "Yêu cầu tuyển dụng"
    Public Function PRI_PROCESS(ByVal employee_id_app As Decimal, ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal status As Decimal, ByVal process_type As String, ByVal notes As String, ByVal id_reggroup As Integer) As Int32
        Using rep As New RecruitmentBusinessClient
            Try

                Return rep.PRI_PROCESS(employee_id_app, employee_id, period_id, status, process_type, notes, id_reggroup, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Function PRI_PROCESS_APP(ByVal employee_id As Decimal, ByVal period_id As Integer, ByVal process_type As String, ByVal totalHours As Decimal, ByVal totalDay As Decimal, ByVal sign_id As Integer, ByVal id_reggroup As Integer, ByVal token As String) As Int32
        Using rep As New RecruitmentBusinessClient
            Try

                Return rep.PRI_PROCESS_APP(employee_id, period_id, process_type, totalHours, totalDay, sign_id, id_reggroup, token)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region
End Class
