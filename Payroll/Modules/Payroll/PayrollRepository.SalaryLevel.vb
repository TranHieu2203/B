Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryLevel"

    Public Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        Using rep As New PayrollBusinessClient

            Try
                Return rep.GetSalaryLevel(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryLevel(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryLevel(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryLevel(objSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryLevel(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryLevel(objSalaryLevel, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryLevel(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryLevel(lstSalaryLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

End Class
