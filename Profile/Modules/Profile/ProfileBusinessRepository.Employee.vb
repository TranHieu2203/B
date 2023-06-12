Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase

#Region "EmployeeBusiness"

    Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                    ByVal _filter As EmployeeDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmpInfomations(orgIDs, _filter, PageIndex, PageSize, Total, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO
        Dim lstEmployee As New EmployeeDTO

        Using rep As New ProfileBusinessClient
            Try
                lstEmployee = rep.GetEmployeeByEmployeeID(empID)
                Return lstEmployee
            Catch ex As Exception

                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function AddFileUpload(ByVal _File As FileUploadDTO, ByVal _imageBinary As Byte()) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Dim obj As Decimal
                obj = rep.AddFileUpload(_File, _imageBinary)
                Return obj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetFileForView(ByVal fileUpload As String) As FileUploadDTO
        Using rep As New ProfileBusinessClient
            Try
                Dim obj As FileUploadDTO
                obj = rep.GetFileForView(fileUpload)
                Return obj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()
        Using rep As New ProfileBusinessClient
            Try
                Dim _binaryImage As Byte()
                _binaryImage = rep.GetEmployeeImage(gEmpID, sError)
                Return _binaryImage
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeImage_PrintCV(ByVal gEmpID As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Dim _Image As String
                _Image = rep.GetEmployeeImage_PrintCV(gEmpID)
                Return _Image
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CreateNewEMPLOYEECode() As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CreateNewEMPLOYEECode()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByRef gID As Decimal, _
                                  ByRef _strEmpCode As String, _
                                  ByVal _imageBinary As Byte(), _
                                  Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                  Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                  Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertEmployee(objEmp, Me.Log, gID, _strEmpCode, _imageBinary, IIf(objEmpCV IsNot Nothing, objEmpCV, Nothing), _
                                                IIf(objEmpEdu IsNot Nothing, objEmpEdu, Nothing), _
                                                IIf(objEmpHealth IsNot Nothing, objEmpHealth, Nothing))
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByRef gID As Decimal, _
                                     ByVal _imageBinary As Byte(), _
                                     Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                     Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                     Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyEmployee(objEmp, Me.Log, gID, _imageBinary, IIf(objEmpCV IsNot Nothing, objEmpCV, Nothing), _
                                                IIf(objEmpEdu IsNot Nothing, objEmpEdu, Nothing), _
                                                IIf(objEmpHealth IsNot Nothing, objEmpHealth, Nothing))
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByRef sError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteEmployee(lstEmpID, Me.Log, sError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Sub GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO)
        Using rep As New ProfileBusinessClient
            Try
                rep.GetEmployeeAllByID(sEmployeeID, empCV, empEdu, empHealth)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Sub

    Public Function GET_INS_HEALTH_BY_EMPID(ByVal empID As Decimal?) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GET_INS_HEALTH_BY_EMPID(empID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using



        Return Nothing
    End Function

    Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployee(_orgIds, _filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeOrgChart() As List(Of OrgChartDTO)
        Using rep As New ProfileBusinessClient
            Try

                If Common.Common.OrganizationLocationDataSession Is Nothing Then
                    Using rep1 As New Common.CommonRepository
                        rep1.GetOrganizationLocationTreeView()
                    End Using
                End If

                Dim lstID = (From p In Common.Common.OrganizationLocationDataSession
                             Select p.ID).ToList

                Return rep.GetEmployeeOrgChart(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployeePaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListWorkingBefore(ByVal _filter As WorkingBeforeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_ID") As List(Of WorkingBeforeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListWorkingBefore(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListWorkingBefore(ByVal _filter As WorkingBeforeDTO,
                              ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "EMPLOYEE_ID") As List(Of WorkingBeforeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListWorkingBefore(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployeePortal(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                              ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListEmployeePaging(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                              ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChartEmployee(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateEmployee(ByVal sType As String, ByVal sEmpCode As String, ByVal value As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateEmployee(sType, sEmpCode, value)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeByEmail(ByVal _email As String,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeByEmail(_email, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



    Public Function GetEmployeeCuriculumVitae(ByVal empID As Decimal, ByRef empCV As EmployeeCVDTO, ByRef empHealth As EmployeeHealthDTO) As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeCuriculumVitae(empID, empCV, empHealth)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateEmpWorkEmail(ByVal _email As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateEmpWorkEmail(_email)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Hàm kiểm tra nhân viên đã có hợp đồng hay chưa.
    ''' </summary>
    ''' <param name="strEmpCode"></param>
    ''' <returns>True: Nếu đã có</returns>
    ''' <remarks></remarks>
    Public Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckEmpHasContract(strEmpCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Employee Proccess"
    ''' <summary>
    ''' Lấy danh sách nhân thân
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFamily(ByVal _empId As Decimal) As List(Of FamilyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFamily(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy quá trình công tác trước khi vào công ty
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingBefore(ByVal _empId As Decimal) As List(Of WorkingBeforeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingBefore(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy quá trình công tác trong công ty
    ''' </summary>
    ''' <param name="_empCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingProccess(ByVal _empId As Decimal?) As List(Of WorkingDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingProccess(_empId, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy quá trình lương
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryProccess(ByVal _empId As Decimal) As List(Of WorkingDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalaryProccess(_empId, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lấy quá trình phúc lợi
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWelfareProccess(ByVal _empId As Decimal) As List(Of WelfareMngDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWelfareProccess(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy quá trình hợp đồng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractProccess(ByVal _empId As Decimal) As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractProccess(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    ''' <summary>
    ''' Lấy quá trình khen thưởng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommendProccess(ByVal _empId As Decimal) As List(Of CommendDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendProccess(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#Region "quá trình kiêm nhiệm"
    Public Function GetConcurrentlyProccess(ByVal _empId As Decimal) As List(Of TitleConcurrentDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetConcurrentlyProccess(_empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region


    ''' <summary>
    ''' Lấy quá trình kỷ luật
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisciplineProccess(ByVal _empId As Decimal) As List(Of DisciplineDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDisciplineProccess(_empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Lấy quá trình kỷ luật
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsuranceProccess(ByVal _empId As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInsuranceProccess(_empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeHistory(ByVal _empId As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeHistory(_empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function

    'QUA TRINH DANH GIA KPI
    Public Function GetAssessKPIEmployee(ByVal _empId As Decimal) As List(Of EmployeeAssessmentDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAssessKPIEmployee(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    ' QUA TRINH NANG LUC
    Public Function GetCompetencyEmployee(ByVal _empId As Decimal) As List(Of EmployeeCompetencyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCompetencyEmployee(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function GetCertificateById(ByVal _id As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificateById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


#End Region

#Region "EmployeeEdit"
    Public Function GetChangedCVList(ByVal lstEmpEdit As List(Of EmployeeEditDTO)) As Dictionary(Of String, String)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangedCVList(lstEmpEdit)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertEmployeeEdit(objEmployeeEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyEmployeeEdit(objEmployeeEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteNVBlackList(ByVal id_no As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteNVBlackList(id_no, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteEmployeeEdit(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteEmployeeEdit(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeEditByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendEmployeeEdit(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SendEmployeeEdit(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateStatusEmployeeEdit(lstID, status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeEditDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveEmployeeEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeEditDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveEmployeeEdit(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


#Region "IPORFILE - Quá trình đào tạo ngoài công ty"

    Public Function GetCertificateType() As List(Of OtherListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificateType()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertProcessTrainingEdit(ByVal objFamilyEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertProcessTrainingEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyProcessTrainingEdit(ByVal objFamilyEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyProcessTrainingEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProcessTrainingEdit(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteProcessTrainingEdit(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetProcessTrainingEdit(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistProcessTrainingEdit(pk_key)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SendProcessTrainingEdit(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateStatusProcessTrainingEdit(lstID, status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveProcessTrainingEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveProcessTrainingEdit(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "IPORFILE - Quá trình công tác ngoài công ty"

    Public Function InsertWorkingBeforeEdit(ByVal objFamilyEdit As WorkingBeforeDTOEdit, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorkingBeforeEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWorkingBeforeEdit(ByVal objFamilyEdit As WorkingBeforeDTOEdit, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorkingBeforeEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWorkingBeforeEdit(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorkingBeforeEdit(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingBeforeEdit(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistWorkingBeforeEdit(pk_key)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SendWorkingBeforeEdit(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateStatusWorkingBeforeEdit(lstID, status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of WorkingBeforeDTOEdit)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveWorkingBeforeEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChangedWorkingBeforeList(ByVal lstWorkingBeforeEdit As List(Of WorkingBeforeDTOEdit)) As Dictionary(Of String, String)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangedWorkingBeforeList(lstWorkingBeforeEdit)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc") As List(Of WorkingBeforeDTOEdit)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveWorkingBeforeEdit(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
    Public Function GetPortalCompetencyCourse(ByVal _empId As Decimal) As List(Of EmployeeCriteriaRecordDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPortalCompetencyCourse(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

    'Public Function EmployeeImage_mb(ByVal userID As Decimal, ByRef sError As String) As Byte()
    '    Using rep As New ProfileBusinessClient
    '        Try
    '            Dim _binaryImage As Byte()
    '            _binaryImage = rep.EmployeeImage_mb(userID, sError)
    '            Return _binaryImage
    '        Catch ex As Exception
    '            rep.Abort()
    '            Throw ex
    '        End Try
    '    End Using

    '    Return Nothing
    'End Function
    Public Function ChangeImage(ByVal _EmpID As Decimal, ByVal _SavePath As String, ByVal _ImageName As String, ByVal _imageBinary As Byte()) As Boolean

        Using rep As New ProfileBusinessClient
            Try
                Return rep.ChangeImage(_EmpID, _SavePath, _ImageName, _imageBinary, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Get_Month_Work_Before(ByVal _EmpID As Decimal) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Get_Month_Work_Before(_EmpID)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UpdateEmployeeHoldingCode(ByVal lstEmpID As List(Of Decimal), ByVal HoldingCode As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateEmployeeHoldingCode(lstEmpID, HoldingCode)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
End Class
