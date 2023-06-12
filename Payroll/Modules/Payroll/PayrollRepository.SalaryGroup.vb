Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "SalaryGroup"

    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO,
                                ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryGroupDTO)
        Dim lstSalaryGroup As List(Of SalaryGroupDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryGroup = rep.GetSalaryGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO,
                                       Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryGroupDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetEffectSalaryGroup() As SalaryGroupDTO
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetEffectSalaryGroup()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryGroupCombo(dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                dtData = rep.GetSalaryGroupCombo(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryGroup(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateSalaryGroup(objSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryGroup(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveSalaryGroup(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryGroup(ByVal lstSalaryGroup As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryGroup(lstSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GET_IMPORT_QUYLUONG() As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_IMPORT_QUYLUONG()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function IMPORT_QUYLUONG(ByVal P_DOCXML As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.IMPORT_QUYLUONG(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Don Vi Quy Luong"
    Public Function GetDonViQuyLuong(ByVal _filter As DonViQuyLuongDTO,
                            ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "NAME ASC") As List(Of DonViQuyLuongDTO)
        Dim lstSalaryGroup As List(Of DonViQuyLuongDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryGroup = rep.GetDonViQuyLuong(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetDonViQuyLuong(ByVal _filter As DonViQuyLuongDTO,
                                       Optional ByVal Sorts As String = "NAME ASC") As List(Of DonViQuyLuongDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetDonViQuyLuong(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertDonViQuyLuong(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifyDonViQuyLuong(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveDonViQuyLuong(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActiveDonViQuyLuong(lstID, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteDonViQuyLuong(ByVal lstSalaryGroup As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteDonViQuyLuong(lstSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ValidateDonViQuyLuong(objSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetSalaryQuyLuong(ByVal _filter As SalaryQuyLuongDTO,
                        ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of SalaryQuyLuongDTO)
        Dim lstSalaryGroup As List(Of SalaryQuyLuongDTO)
        Using rep As New PayrollBusinessClient

            Try
                lstSalaryGroup = rep.GetSalaryQuyLuong(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstSalaryGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetSalaryQuyLuong(ByVal _filter As SalaryQuyLuongDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of SalaryQuyLuongDTO)

        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryQuyLuong(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertSalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertSalaryQuyLuong(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifySalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO, ByRef gID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ModifySalaryQuyLuong(objSalaryGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteSalaryQuyLuong(ByVal lstSalaryGroup As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteSalaryQuyLuong(lstSalaryGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function LOAD_DONVI_QUYLUONG() As List(Of DonViQuyLuongDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.LOAD_DONVI_QUYLUONG()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmpNotQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetEmpNotQuyLuong(_filter, _param, Me.Log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmpQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetEmpQuyLuong(_filter, _param, Me.Log, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertEmpQuyLuong(ByVal obj As EmpQuyLuongDTO, ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.InsertEmpQuyLuong(obj, lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteEmpQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.DeleteEmpQuyLuong(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

End Class
