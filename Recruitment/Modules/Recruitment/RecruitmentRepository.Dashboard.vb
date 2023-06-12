Imports Recruitment.RecruitmentBusiness

Partial Class RecruitmentRepository

#Region "Dashboard"

    Public Function GetStatisticGender(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticGender(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticEduacation(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticEduacation(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticCanToEmp(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticCanToEmp(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStatisticEstimateReality(ByVal _year As Integer) As List(Of StatisticDTO)

        Dim lstStatistic As List(Of StatisticDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.GetStatisticEstimateReality(_year, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function REPORT_RANK_SAL_BY_MONTH(ByVal _year As Integer, ByVal _month As Integer, ByVal _org As Integer) As List(Of HRPlaningDetailDTO)

        Dim lstStatistic As List(Of HRPlaningDetailDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.REPORT_RANK_SAL_BY_MONTH(_year, _month, _org, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function REPORT_RANK_SAL_BY_YEAR_SAL(ByVal _year As Integer, ByVal _org As Integer) As List(Of ReportDTO)

        Dim lstStatistic As List(Of ReportDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstStatistic = rep.REPORT_RANK_SAL_BY_YEAR_SAL(_year, _org, Me.Log)
                Return lstStatistic
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
End Class
