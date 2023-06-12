Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Discipline"
        Public Function CheckHasFileDiscipline(ByVal id As List(Of Decimal)) As Decimal Implements ServiceContracts.IProfileBusiness.CheckHasFileDiscipline
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckHasFileDiscipline(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function

        Public Function ApproveListDiscipline(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveListDiscipline
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListDiscipline(listID, acti, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineEmpDTO) Implements ServiceContracts.IProfileBusiness.GetEmployeeDesciplineID
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetEmployeeDesciplineID(DesId)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetOrgDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineOrgDTO) Implements ServiceContracts.IProfileBusiness.GetOrgDesciplineID
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetOrgDesciplineID(DesId)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetDiscipline(ByVal _filter As DisciplineDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO) Implements ServiceContracts.IProfileBusiness.GetDiscipline
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetDiscipline(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function GetDisciplineByID(ByVal _filter As DisciplineDTO) As DisciplineDTO Implements ServiceContracts.IProfileBusiness.GetDisciplineByID
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetDisciplineByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try

        End Function

        Public Function InsertDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertDiscipline
            Try
                Dim rep As New ProfileRepository
                Return rep.InsertDiscipline(objDiscipline, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyDiscipline
            Try
                Dim rep As New ProfileRepository
                Return rep.ModifyDiscipline(objDiscipline, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateDiscipline(ByVal sType As String, ByVal obj As DisciplineDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateDiscipline
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateDiscipline(sType, obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteDiscipline(ByVal objDiscipline() As DisciplineDTO) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteDiscipline
            Try
                Dim rep As New ProfileRepository
                Return rep.DeleteDiscipline(objDiscipline)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ApproveDiscipline(ByVal objDiscipline As DisciplineDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveDiscipline
            Try
                Dim rep As New ProfileRepository
                Return rep.ApproveDiscipline(objDiscipline)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function Open_ApproveDiscipline(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.Open_ApproveDiscipline
            Try
                Dim rep As New ProfileRepository
                Return rep.Open_ApproveDiscipline(listID, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "DisciplineSalary"

        Public Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO,
                                      ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "YEAR,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetDisciplineSalary
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetDisciplineSalary(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function GetDisciplineSalaryByID(ByVal _filter As DisciplineSalaryDTO) As DisciplineSalaryDTO _
            Implements ServiceContracts.IProfileBusiness.GetDisciplineSalaryByID
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetDisciplineSalaryByID(_filter)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function EditDisciplineSalary(obj As DisciplineSalaryDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.EditDisciplineSalary
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.EditDisciplineSalary(obj)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function ValidateDisciplineSalary(ByVal obj As DisciplineSalaryDTO, ByRef sError As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateDisciplineSalary
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.ValidateDisciplineSalary(obj, sError)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ApproveDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean _
                                    Implements ServiceContracts.IProfileBusiness.ApproveDisciplineSalary
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.ApproveDisciplineSalary(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function StopDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean _
                                    Implements ServiceContracts.IProfileBusiness.StopDisciplineSalary
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.StopDisciplineSalary(lstID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace
