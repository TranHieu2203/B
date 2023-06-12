Imports Attendance.AttendanceBusiness
Imports Framework.UI
Imports Profile.ProfileBusiness

Public Class AttendanceRepository
    Inherits AttendanceRepositoryBase
    Private _isAvailable As Boolean

    Dim CacheMinusDataCombo As Integer = 30

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
                End If
                CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProjectList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProjectTitleList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectTitleList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetProjectWorkList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New AttendanceBusinessClient
            Try

                dtData = rep.GetProjectWorkList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetComboboxData(ByRef cbxData As AttendanceBusiness.ComboBoxDataDTO, Optional ByVal strUser As String = "ADMIN") As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                'cbxData.USER = Me.Log.Username
                Return rep.GetComboboxData(cbxData, Me.Log.Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#Region "STORE PROCEDURE"
    Public Function GET_MANUAL_BY_ID(ByVal id As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_MANUAL_BY_ID(id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_INFO_PHEPNAM(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_INFO_PHEPNAM(id, fromDate)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_INFO_PHEPNAM_IMPORT_CTT(ByVal id As Decimal, ByVal fromDate As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_INFO_PHEPNAM_IMPORT_CTT(id, fromDate)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region
    Public Function CheckOverMonth(ByVal employeeId As Decimal, ByVal orgId As Decimal _
                                                                , ByVal FromDate As DateTime,
                                                                 ByVal ToDate As DateTime) As Decimal
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckOverMonth(employeeId, orgId, FromDate, ToDate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetEmpId(ByVal employeeID As Decimal) As EmployeeDTO

        Using profile As New ProfileBusinessClient
            Try
                Return profile.GetEmployeeByEmployeeID(employeeID)
            Catch ex As Exception
                profile.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class