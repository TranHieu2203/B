Imports Framework.Data

Partial Class RecruitmentRepository

#Region "Dashboard"

    Public Function GetStatisticGender(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_GENDER", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticEduacation(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_EDUCATION", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME") & " (" & dr("VALUE") & ")",
                                   .VALUE = Decimal.Parse(dr("VALUE"))})
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticCanToEmp(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_CAN_TO_EMP", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME"),
                                   .VALUE = Decimal.Parse(dr("VALUE")),
                                   .VALUE_SECOND = 0
                                   })
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStatisticEstimateReality(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        Try
            Dim result As New List(Of StatisticDTO)

            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.GET_STATISTIC_ESTIMATE_REALITY", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        result.Add(New StatisticDTO With {
                                   .NAME = dr("NAME"),
                                   .VALUE = Decimal.Parse(dr("VALUE")),
                                   .VALUE_SECOND = Decimal.Parse(dr("VALUE_SECOND"))
                               })
                    Next
                End If
            End Using

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function REPORT_RANK_SAL_BY_MONTH(ByVal _year As Integer, ByVal _month As Integer, ByVal _org As Integer, ByVal log As UserLog) As List(Of HRPlaningDetailDTO)
        Try
            Dim result As New List(Of HRPlaningDetailDTO)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _org,
                                           .P_ISDISSOLVE = 0})
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_OGR_ID = _org,
                                    .P_MONTH = _month,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.REPORT_RANK_SAL_BY_MONTH", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each Row As DataRow In dtData.Rows
                        Dim objWA As New HRPlaningDetailDTO
                        objWA.TITLE_NAME = IIf(IsDBNull(Row("TITLE_NAME")), Nothing, Row("TITLE_NAME").ToString)
                        If Row("TOTAL_EMP").ToString <> "" Then
                            objWA.EMP_COUNT = Decimal.Parse(Row("TOTAL_EMP"))
                        End If
                        If Row("ACTUAL_WAGE_FUND").ToString <> "" Then
                            objWA.ACTUAL_WAGE_FUND = Decimal.Parse(Row("ACTUAL_WAGE_FUND"))
                        End If
                        If _month = 1 Then
                            If Row("MONTH_1").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_1"))
                            End If
                        ElseIf _month = 2 Then
                            If Row("MONTH_2").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_2"))
                            End If
                        ElseIf _month = 3 Then
                            If Row("MONTH_3").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_3"))
                            End If
                        ElseIf _month = 4 Then
                            If Row("MONTH_4").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_4"))
                            End If
                        ElseIf _month = 5 Then
                            If Row("MONTH_5").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_5"))
                            End If
                        ElseIf _month = 6 Then
                            If Row("MONTH_6").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_6"))
                            End If
                        ElseIf _month = 7 Then
                            If Row("MONTH_7").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_7"))
                            End If
                        ElseIf _month = 8 Then
                            If Row("MONTH_8").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_8"))
                            End If
                        ElseIf _month = 9 Then
                            If Row("MONTH_9").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_9"))
                            End If
                        ElseIf _month = 10 Then
                            If Row("MONTH_10").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_10"))
                            End If
                        ElseIf _month = 11 Then
                            If Row("MONTH_11").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_11"))
                            End If
                        Else
                            If Row("MONTH_12").ToString <> "" Then
                                objWA.MONTH_HRP_DETAIL = Decimal.Parse(Row("MONTH_12"))
                            End If
                        End If
                        If Row("RANK_SAL").ToString <> "" Then
                            objWA.RANK_SAL = Decimal.Parse(Row("RANK_SAL"))
                        End If
                        objWA.WAGE_FUNDS_PLAN = objWA.MONTH_HRP_DETAIL * objWA.RANK_SAL
                        objWA.WAGE_FUNDS = objWA.WAGE_FUNDS_PLAN - objWA.ACTUAL_WAGE_FUND
                        result.Add(objWA)
                    Next
                End If
            End Using

            Return result.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function REPORT_RANK_SAL_BY_YEAR_SAL(ByVal _year As Integer, ByVal _org As Integer, ByVal log As UserLog) As List(Of ReportDTO)
        Try
            Dim result As New List(Of ReportDTO)

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _org,
                                           .P_ISDISSOLVE = 0})
                Dim obj = New With {.P_USERNAME = log.Username,
                                    .P_OGR_ID = _org,
                                    .P_YEAR = _year,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_DASHBOARD.REPORT_RANK_SAL_BY_YEAR_SAL", obj)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each Row As DataRow In dtData.Rows
                        Dim objWA As New ReportDTO
                        If Row("MONTH_1").ToString <> "" Then
                            objWA.MONTH_1 = Decimal.Parse(Row("MONTH_1"))
                        End If
                        If Row("MONTH_2").ToString <> "" Then
                            objWA.MONTH_2 = Decimal.Parse(Row("MONTH_2"))
                        End If
                        If Row("MONTH_3").ToString <> "" Then
                            objWA.MONTH_3 = Decimal.Parse(Row("MONTH_3"))
                        End If
                        If Row("MONTH_4").ToString <> "" Then
                            objWA.MONTH_4 = Decimal.Parse(Row("MONTH_4"))
                        End If
                        If Row("MONTH_5").ToString <> "" Then
                            objWA.MONTH_5 = Decimal.Parse(Row("MONTH_5"))
                        End If
                        If Row("MONTH_6").ToString <> "" Then
                            objWA.MONTH_6 = Decimal.Parse(Row("MONTH_6"))
                        End If
                        If Row("MONTH_7").ToString <> "" Then
                            objWA.MONTH_7 = Decimal.Parse(Row("MONTH_7"))
                        End If
                        If Row("MONTH_8").ToString <> "" Then
                            objWA.MONTH_8 = Decimal.Parse(Row("MONTH_8"))
                        End If
                        If Row("MONTH_9").ToString <> "" Then
                            objWA.MONTH_9 = Decimal.Parse(Row("MONTH_9"))
                        End If
                        If Row("MONTH_10").ToString <> "" Then
                            objWA.MONTH_10 = Decimal.Parse(Row("MONTH_10"))
                        End If
                        If Row("MONTH_11").ToString <> "" Then
                            objWA.MONTH_11 = Decimal.Parse(Row("MONTH_11"))
                        End If
                        If Row("MONTH_12").ToString <> "" Then
                            objWA.MONTH_12 = Decimal.Parse(Row("MONTH_12"))
                        End If
                        result.Add(objWA)
                    Next
                End If
            End Using

            Return result.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
