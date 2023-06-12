Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryType"

    Public Function GetSalaryType(ByVal _filter As SalaryTypeDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryTypeDTO)
        Dim lstSalaryType As List(Of SalaryTypeDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryType = rep.GetSalaryType(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryType
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetSalaryType(ByVal _filter As SalaryTypeDTO,
                                       Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryTypeDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryType(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryType(ByVal objSalaryType As SalaryTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryType(objSalaryType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryType(ByVal objSalaryType As SalaryTypeDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryType(objSalaryType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryType(ByVal objSalaryType As SalaryTypeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryType(objSalaryType, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryType(ByVal lstSalaryType As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryType(lstSalaryType)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryType(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryType(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class
