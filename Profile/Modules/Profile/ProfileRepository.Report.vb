
Imports Profile.ProfileBusiness

Partial Public Class ProfileRepository


    Public Function GetReportList(ByVal _filter As WorkingDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WorkingDTO)
        Dim lstWorking As List(Of WorkingDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWorking = rep.GetWorking(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lstWorking
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Chart_HDLD(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_HDLD(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_Age(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_Age(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_TRINHDO_HOCVAN(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_TRINHDO_HOCVAN(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_GENDER(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_GENDER(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_TNCT(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_TNCT(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_HANHCHINH(ByVal param As ParamDTO) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_HANHCHINH(param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_TRINHDO_NGOAINGU(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_TRINHDO_NGOAINGU(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_EmpObj(ByVal param As ParamDTO) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_EmpObj(param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Chart_BAC_LAO_DONG(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_BAC_LAO_DONG(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Chart_NEW_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_NEW_EMPLOYEE(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Chart_TER_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_TER_EMPLOYEE(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Chart_BO_NHIEM(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_BO_NHIEM(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_Employee_Num(ByVal param As ParamDTO, ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_Employee_Num(param, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Chart_WorkPlace(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, Optional isExport As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Chart_WorkPlace(param, _lstOrg, _strFilter, Me.Log, isExport)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
End Class
