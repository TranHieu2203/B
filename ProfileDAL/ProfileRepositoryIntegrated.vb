Imports System.Reflection
Imports System.Web
Imports Framework.Data

Public Class ProfileRepositoryIntegrated
    Inherits ProfileRepositoryBase


#Region "Lấy data danh mục Combo"

    Public Function GetEmpAll(ByVal lastDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_EMPLOYEE_INFO",
                                           New With {.P_DATE = lastDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrgAll(ByVal lastDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_ORG_INFO",
                                           New With {.P_DATE = lastDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTitleAll(ByVal lastDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_TITLE_INFO",
                                           New With {.P_DATE = lastDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTitleGroupAll(ByVal lastDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_TITLE_GROUP_INFO",
                                           New With {.P_DATE = lastDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetStaffRankAll(ByVal lastDate As Date) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_INTEGRATED.GET_STAFF_RANK_INFO",
                                           New With {.P_DATE = lastDate,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

End Class
