Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
    Public Function CheckHasFileDiscipline(ByVal id As List(Of Decimal)) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckHasFileDiscipline(id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ApproveListDiscipline(ByVal listID As List(Of Decimal), ByVal acti As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveListDiscipline(listID, acti, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmployeeDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineEmpDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeDesciplineID(DesId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineOrgDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgDesciplineID(DesId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetDiscipline(ByVal _filter As DisciplineDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)
        Dim lstDiscipline As List(Of DisciplineDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstDiscipline = rep.GetDiscipline(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstDiscipline
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDiscipline(ByVal _filter As DisciplineDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDiscipline(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetDisciplineByID(ByVal _filter As DisciplineDTO) As DisciplineDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertDiscipline(ByVal objDiscipline As DisciplineDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertDiscipline(objDiscipline, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyDiscipline(ByVal objDiscipline As DisciplineDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyDiscipline(objDiscipline, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateDiscipline(ByVal sType As String, ByVal obj As DisciplineDTO) As Boolean

        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateDiscipline(sType, obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteDiscipline(ByVal objDiscipline As List(Of DisciplineDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteDiscipline(objDiscipline)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ApproveDiscipline(ByVal objDiscipline As DisciplineDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveDiscipline(objDiscipline)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Open_ApproveDiscipline(ByVal listID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Open_ApproveDiscipline(listID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ApproveDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveDisciplineSalary(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function StopDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.StopDisciplineSalary(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "YEAR ,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineSalary(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO,
                                        Optional ByVal Sorts As String = "YEAR ,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineSalary(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDisciplineSalaryByID(ByVal _filter As DisciplineSalaryDTO) As DisciplineSalaryDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineSalaryByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function EditDisciplineSalary(obj As DisciplineSalaryDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EditDisciplineSalary(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateDisciplineSalary(ByVal obj As DisciplineSalaryDTO, ByRef sError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateDisciplineSalary(obj, sError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

End Class
