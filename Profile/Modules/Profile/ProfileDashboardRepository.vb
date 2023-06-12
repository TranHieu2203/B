Imports Framework.UI
Imports Profile.ProfileBusiness

Public Class ProfileDashboardRepository
    Inherits ProfileRepositoryBase
#Region "Profile dashboard"

    Public Function GetEmployeeStatistic(ByVal _orgID As Decimal, ByVal _type As String) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeStatistic(_type, _orgID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListEmployeeStatistic() As List(Of OtherListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployeeStatistic()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListChangeStatistic() As List(Of OtherListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListChangeStatistic()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChangeStatistic(ByVal _orgID As Decimal, ByVal _type As String) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangeStatistic(_type,_orgID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#Region "Reminder"
    Public Function GetRemind(ByVal _dayRemind As String, Optional ByVal _orgID As Decimal = 1) As List(Of ReminderLogDTO)
        Using rep As New ProfileBusinessClient
            Dim dtdata = New List(Of ReminderLogDTO)
            'Dim ORG_ID_CACHE As Decimal
            Try
                ' chạy theo quyền của từng tài khoản nên chậm tính tiếp :v 
                'dtdata = CacheManager.GetValue("GetRemind")
                'ORG_ID_CACHE = CacheManager.GetValue("GetRemind_orgID")
                'If dtdata Is Nothing Then
                dtdata = rep.GetRemind(_dayRemind, Log, _orgID)
                'CacheManager.Insert("GetRemind_orgID", _orgID, Common.Common.CacheMinusGetRemind)
                'CacheManager.Insert("GetRemind", dtdata, Common.Common.CacheMinusGetRemind)
                'End If

                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

    Public Function GetCompanyNewInfo(ByVal _orgID As Decimal) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCompanyNewInfo(_orgID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetStatisticSeniority(ByVal _orgID As Decimal) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticSeniority(_orgID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Competency Dashboard"
    Public Function GetStatisticTop5Competency(ByVal _year As Integer) As List(Of StatisticDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticTop5Competency(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetStatisticTop5CopAvg(ByVal _year As Integer) As List(Of CompetencyAvgEmplDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStatisticTop5CopAvg(_year, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "Portal Dashboard"
    Public Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeReg(_employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO) As TotalDayOffDTO
        Using rep As New ProfileBusinessClient
            Try
                Dim lst = rep.GetTotalDayOff(_filter, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificateExpires(_employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


End Class
