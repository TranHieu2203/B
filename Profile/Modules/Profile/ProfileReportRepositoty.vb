Imports Profile.ProfileBusiness
Public Class ProfileReportRepositoty
    Inherits ProfileRepositoryBase

#Region "Hoadm"

#Region "Dynamic"


    Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetConditionColumn(_ConditionID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListReportName(_ViewId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SaveDynamicReport(_report, _col, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteDynamicReport(ByVal ID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteDynamicReport(ID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDynamicReportList() As Dictionary(Of Decimal, String)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDynamicReportList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDynamicReportColumn(_reportID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy dữ liệu báo cáo động
    ''' </summary>
    ''' <param name="column">Danh sách các cột cần lấy</param>
    ''' <param name="condition">Expression điều kiện</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDynamicReport(ByVal _reportID As Decimal,
                                     ByVal orgID As Decimal,
                                     ByVal isDissolve As Decimal,
                                     ByVal chkTerminate As Decimal,
                                     ByVal chkHasTerminate As Decimal,
                                     ByVal column As List(Of String),
                                     Optional ByVal condition As String = "") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDynamicReport(_reportID, orgID, isDissolve, chkTerminate, chkHasTerminate, column, condition, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#End Region

End Class
