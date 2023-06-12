Imports Framework.Data
Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness

#Region "Working"
        Public Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveListChangeInfoMng
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListChangeInfoMng(listID, acti, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckHasFile(ByVal id As List(Of Decimal)) As Decimal Implements IProfileBusiness.CheckHasFile
            Try
                Using rep As New ProfileRepository
                    Return rep.CheckHasFile(id)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ApproveWorkings(ByVal ids As List(Of Decimal), ByVal acti As Decimal, Optional ByVal log As UserLog = Nothing) As CommandResult Implements IProfileBusiness.ApproveWorkings
            Using rep As New ProfileRepository
                Return rep.ApproveWorkings(ids, acti, log)
            End Using
        End Function
        Public Function GetWorking_1(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO) Implements ServiceContracts.IProfileBusiness.GetWorking_1
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetWorking_1(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO) Implements ServiceContracts.IProfileBusiness.GetWorking
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetWorking(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_EMPDTL_HU_ALLOWAMCE(ByVal _filter As HUAllowanceDTO,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO) Implements ServiceContracts.IProfileBusiness.GET_EMPDTL_HU_ALLOWAMCE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_EMPDTL_HU_ALLOWAMCE(_filter, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                       ByVal _param As ParamDTO,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO) Implements ServiceContracts.IProfileBusiness.GetWorkingAllowance1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingAllowance1(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorkingAllowance(ByVal objWorkingAllowance As HUAllowanceDTO,
                                     ByVal log As UserLog,
                                     ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorkingAllowance
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertWorkingAllowance(objWorkingAllowance, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteWorkingAllowance(ByVal lstWorkingAllowance() As HUAllowanceDTO,
                                 ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWorkingAllowance
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteWorkingAllowance(lstWorkingAllowance, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyWorkingAllowanceNew(ByVal objWorkingAllowance As HUAllowanceDTO,
                                       ByVal log As UserLog,
                                       ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorkingAllowanceNew
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyWorkingAllowanceNew(objWorkingAllowance, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateNewEdit(ByVal objWorkingAllowance As HUAllowanceDTO) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateNewEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateNewEdit(objWorkingAllowance)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO) Implements ServiceContracts.IProfileBusiness.GetWorkingAllowance
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingAllowance(_filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO,
                                               ByVal log As UserLog,
                                               ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorkingAllowance
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorkingAllowance(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLastWorkingSalary(ByVal _filter As WorkingDTO) As WorkingDTO Implements ServiceContracts.IProfileBusiness.GetLastWorkingSalary
            Using rep As New ProfileRepository
                Try

                    Dim obj = rep.GetLastWorkingSalary(_filter)
                    Return obj
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorkingByID_1(ByVal _filter As WorkingDTO) As WorkingDTO _
           Implements ServiceContracts.IProfileBusiness.GetWorkingByID_1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingByID_1(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetWorkingByID(ByVal _filter As WorkingDTO) As WorkingDTO _
            Implements ServiceContracts.IProfileBusiness.GetWorkingByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorkingByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO _
            Implements ServiceContracts.IProfileBusiness.GetEmployeCurrentByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetEmployeCurrentByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertListWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertListWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertListWorking1(objWorking, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmpSaved(ByVal Id As Integer) As EmployeeDTO Implements ServiceContracts.IProfileBusiness.GetEmpSaved
            Try
                Using rep As New ProfileRepository
                    Dim dt = rep.GetEmpSaved(Id)
                    Return dt
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertMngProfileSaved(ByVal lstMngProSaved As MngProfileSavedDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.InsertMngProfileSaved
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertMngProfileSaved(lstMngProSaved, log)
                End Using
            Catch ex As Exception

            End Try
        End Function
        Public Function InsertWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking1(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyWorking1
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking1(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorking(ByVal objWorking As WorkingDTO) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWorking(objWorking)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateWorking(sType, obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValEffectdateByEmpCode(ByVal emp_code As String, ByVal effect_date As Date) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValEffectdateByEmpCode
            Using rep As New ProfileRepository
                Try

                    Return rep.ValEffectdateByEmpCode(emp_code, effect_date)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetAllowanceByDate
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAllowanceByDate(_filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO) _
            Implements ServiceContracts.IProfileBusiness.GetAllowanceByWorkingID
            Using rep As New ProfileRepository
                Try

                    Return rep.GetAllowanceByWorkingID(_filter)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteWorking3B(objWorking)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetChangeInfoImport(param As ParamDTO, log As UserLog) As DataSet _
            Implements ServiceContracts.IProfileBusiness.GetChangeInfoImport
            Using rep As New ProfileRepository
                Try

                    Return rep.GetChangeInfoImport(param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportChangeInfo(lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean _
                                 Implements ServiceContracts.IProfileBusiness.ImportChangeInfo
            Using rep As New ProfileRepository
                Try

                    Return rep.ImportChangeInfo(lstData, dtError, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function UnApproveWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.UnApproveWorking
            Using rep As New ProfileRepository
                Try

                    Return rep.UnApproveWorking(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDataExport() As DataSet Implements ServiceContracts.IProfileBusiness.GetDataExport
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDataExport()
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_REGION_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal Implements ServiceContracts.IProfileBusiness.GET_REGION_BY_DATE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_REGION_BY_DATE(empId, pDate)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_SALARY_BY_DATE(ByVal empId As Decimal, ByVal pDate As Date) As Decimal Implements ServiceContracts.IProfileBusiness.GET_SALARY_BY_DATE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_SALARY_BY_DATE(empId, pDate)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_CODE_EMP_TYPE(ByVal Id As Decimal) As String Implements ServiceContracts.IProfileBusiness.GET_CODE_EMP_TYPE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_CODE_EMP_TYPE(Id)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "3B"

        Public Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO) _
                           Implements ServiceContracts.IProfileBusiness.GetWorking3B
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWorking3B(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function InsertWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertWorking3B(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyWorking3b(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyWorking3B
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyWorking3B(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


#End Region
#Region "HU_SALARY"
        Public Function GetHu_Salary(ByVal _filter As HU_SALARYDTO,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of HU_SALARYDTO) Implements ServiceContracts.IProfileBusiness.GetHu_Salary
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetHu_Salary(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertHu_Salary(ByVal objWorking As HU_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertHu_Salary
            Using rep As New ProfileRepository
                Try

                    Return rep.InsertHu_Salary(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyHu_Salary(ByVal objWorking As HU_SALARYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyHu_Salary
            Using rep As New ProfileRepository
                Try

                    Return rep.ModifyHu_Salary(objWorking, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHuSalaryByID(ByVal _filter As HU_SALARYDTO) As HU_SALARYDTO _
           Implements ServiceContracts.IProfileBusiness.GetHuSalaryByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHuSalaryByID(_filter)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace
