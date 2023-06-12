Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Profile Dashboard"
        Public Function GetEmployeeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As System.Collections.Generic.List(Of ProfileDAL.StatisticDTO) Implements ServiceContracts.IProfileBusiness.GetEmployeeStatistic
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetEmployeeStatistic(_type, _orgID, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListEmployeeStatistic() As System.Collections.Generic.List(Of ProfileDAL.OtherListDTO) Implements ServiceContracts.IProfileBusiness.GetListEmployeeStatistic
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetListEmployeeStatistic()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetChangeStatistic(ByVal _type As String, ByVal _orgID As Decimal, ByVal log As UserLog) As List(Of ProfileDAL.StatisticDTO) Implements ServiceContracts.IProfileBusiness.GetChangeStatistic
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetChangeStatistic(_type, _orgID, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListChangeStatistic() As System.Collections.Generic.List(Of ProfileDAL.OtherListDTO) Implements ServiceContracts.IProfileBusiness.GetListChangeStatistic
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetListChangeStatistic()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRemind(ByVal _dayRemind As String, ByVal log As UserLog, Optional ByVal _orgID As Decimal = 1) As System.Collections.Generic.List(Of ProfileDAL.ReminderLogDTO) Implements ServiceContracts.IProfileBusiness.GetRemind
            Using rep As New ProfileDashboardRepository
                Try
                    Return rep.GetRemind(_dayRemind, log, _orgID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetCompanyNewInfo(ByVal _orgID As Decimal, ByVal log As UserLog) As System.Collections.Generic.List(Of ProfileDAL.StatisticDTO) Implements ServiceContracts.IProfileBusiness.GetCompanyNewInfo
            Using rep As New ProfileDashboardRepository
                Try
                    Return rep.GetCompanyNewInfo(_orgID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetStatisticSeniority(ByVal _orgID As Decimal, ByVal log As UserLog) As System.Collections.Generic.List(Of ProfileDAL.StatisticDTO) Implements ServiceContracts.IProfileBusiness.GetStatisticSeniority
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetStatisticSeniority(_orgID, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "Competency Dashboard"
        Public Function GetStatisticTop5Competency(ByVal _year As Integer, ByVal log As UserLog) As List(Of ProfileDAL.StatisticDTO) Implements ServiceContracts.IProfileBusiness.GetStatisticTop5Competency
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetStatisticTop5Competency(_year, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStatisticTop5CopAvg(ByVal _year As Integer, ByVal log As UserLog) As List(Of CompetencyAvgEmplDTO) Implements ServiceContracts.IProfileBusiness.GetStatisticTop5CopAvg
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetStatisticTop5CopAvg(_year, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Portal Dashboard"
        Public Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable Implements ServiceContracts.IProfileBusiness.GetEmployeeReg
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetEmployeeReg(_employee_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                               Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO Implements ServiceContracts.IProfileBusiness.GetTotalDayOff
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetTotalDayOff(_filter)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable Implements ServiceContracts.IProfileBusiness.GetCertificateExpires
            Using rep As New ProfileDashboardRepository
                Try
                    Dim lst = rep.GetCertificateExpires(_employee_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace