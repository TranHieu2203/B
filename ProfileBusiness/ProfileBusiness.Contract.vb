Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "TRANINGEVALUATE"
        Public Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO) _
                             Implements ServiceContracts.IProfileBusiness.GetTrainingEvaluateEmp
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingEvaluateEmp(_empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                         ByVal PageSize As Integer,
                         ByRef Total As Integer, ByVal _param As ParamDTO,
                         Optional ByVal Sorts As String = "CREATED_DATE desc",
                         Optional ByVal log As UserLog = Nothing) As List(Of TrainningEvaluateDTO) _
                             Implements ServiceContracts.IProfileBusiness.GetTrainingEvaluate
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingEvaluate(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.InsertTrainingEvaluate
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertTrainingEvaluate(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.ModifyTrainingEvaluate
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyTrainingEvaluate(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTrainingEvaluateByID(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO _
                                  Implements ServiceContracts.IProfileBusiness.GetTrainingEvaluateByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingEvaluateById(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteTrainingEvaluate
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteTrainingEvaluate(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Manage annual leave plans (ALP)"
        Public Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetListALPByEmpID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListALPByEmpID(_filter, _param)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetALP
            Using rep As New ProfileRepository
                Try
                    Return rep.GetALP(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.InsertALP
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertALP(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyALP
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyALP(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetALPByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO _
                                  Implements ServiceContracts.IProfileBusiness.GetALPByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetALPById(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteALP(ByVal objContract As TrainningManageDTO) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteALP
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteALP(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer _
           Implements ServiceContracts.IProfileBusiness.CheckEmployee_Exits
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckEmployee_Exits(empCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean _
          Implements ServiceContracts.IProfileBusiness.ImportAnnualLeave
            Using rep As New ProfileRepository
                Try
                    Return rep.ImportAnnualLeave(P_DOCXML, P_USER, P_YEAR)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "training manage"
        Public Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetListTrainingManageByEmpID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListTrainingManageByEmpID(_filter, _param)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetTrainingManage
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingManage(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.InsertTrainingManage
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertTrainingManage(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyTrainingManage
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyTrainingManage(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTrainingManageByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO _
                                  Implements ServiceContracts.IProfileBusiness.GetTrainingManageByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingManageById(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTrainingManage(ByVal objContract As TrainningManageDTO) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteTrainingManage
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteTrainingManage(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "traningforeign"
        Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningForeignDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetTrainingForeign
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingForeign(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.InsertTrainingForeign
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertTrainingForeign(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
           Implements ServiceContracts.IProfileBusiness.ModifyTrainingForeign
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyTrainingForeign(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTrainingForeignByID(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO _
                                  Implements ServiceContracts.IProfileBusiness.GetTrainingForeignByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetTrainingForeignById(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteTrainingForeign(ByVal objContract As TrainningForeignDTO) As Boolean _
           Implements ServiceContracts.IProfileBusiness.DeleteTrainingForeign
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteTrainingForeign(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "emp bank"
        Public Function saveEmpBank(ByVal dt As DataTable, Optional ByVal log As UserLog = Nothing) As Decimal _
                                   Implements ServiceContracts.IProfileBusiness.saveEmpBank
            Using rep As New ProfileRepository
                Try
                    Return rep.saveEmpBank(dt, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmpBank(ByVal _filter As EmpBankDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of EmpBankDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetEmpBank
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmpBank(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "contract appendix"
        Public Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer Implements ServiceContracts.IProfileBusiness.GET_NEXT_APPENDIX_ORDER
            Using rep As New ProfileRepository
                Try
                    Return rep.GET_NEXT_APPENDIX_ORDER(id, contract_id, emp_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
        Public Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean Implements ServiceContracts.IProfileBusiness.UpdateDateToContract
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateDateToContract(id, day, remark)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetContract
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContract(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO _
                                    Implements ServiceContracts.IProfileBusiness.GetContractByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractByID(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateContract
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateContract(sType, obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckStartDate1(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckStartDate1
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckStartDate1(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckStartDate(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckStartDate
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckStartDate(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertContract
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal Implements ServiceContracts.IProfileBusiness.CheckHasFileFileContract
            Try
                Using rep As New ProfileRepository
                    Try
                        Return rep.CheckHasFileFileContract(id)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal Implements ServiceContracts.IProfileBusiness.CheckHasFileContract
            Try
                Using rep As New ProfileRepository
                    Try
                        Return rep.CheckHasFileContract(id)
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyContract
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteContract
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteContract(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CreateContractNo(ByVal objContract As ContractDTO) As String _
            Implements ServiceContracts.IProfileBusiness.CreateContractNo
            Using rep As New ProfileRepository
                Try
                    Return rep.CreateContractNo(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckContractNo
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckContractNo(objContract)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO _
            Implements ServiceContracts.IProfileBusiness.GetContractEmployeeByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetContractEmployeeByID(gEmployeeID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetMaxConId(empId) As Decimal Implements ServiceContracts.IProfileBusiness.GetMaxConId
            Using rep As New ProfileRepository
                Try
                    Return rep.GetMaxConId(empId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UnApproveContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UnApproveContract
            Using rep As New ProfileRepository
                Try
                    Return rep.UnApproveContract(objContract, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCheckContractTypeID(ByVal listID As String) As DataTable _
                          Implements ServiceContracts.IProfileBusiness.GetCheckContractTypeID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCheckContractTypeID(listID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetContractForm(ByVal formID As Decimal) As OtherListDTO Implements ServiceContracts.IProfileBusiness.GetContractForm
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractForm(formID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.ApproveListContract
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListContract(listID, acti, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable _
          Implements ServiceContracts.IProfileBusiness.GET_PROCESS_PLCONTRACT
            Using rep As New ProfileRepository
                Try
                    Return rep.GET_PROCESS_PLCONTRACT(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_PLHD(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet _
         Implements ServiceContracts.IProfileBusiness.EXPORT_PLHD
            Using rep As New ProfileRepository
                Try
                    Return rep.EXPORT_PLHD(_param, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function EXPORT_CONGNO(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet _
         Implements ServiceContracts.IProfileBusiness.EXPORT_CONGNO
            Using rep As New ProfileRepository
                Try
                    Return rep.EXPORT_CONGNO(_param, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_CV(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet _
         Implements ServiceContracts.IProfileBusiness.EXPORT_CV
            Using rep As New ProfileRepository
                Try
                    Return rep.EXPORT_CV(_param, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_EMPLOYEE
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_CONTRACT
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_CONTRACT(P_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_CONTRACT_BY_EMP_CODE(ByVal P_ID As Decimal, ByVal P_EMPID As Decimal) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_CONTRACT_BY_EMP_CODE
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_CONTRACT_BY_EMP_CODE(P_ID, P_EMPID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_SALARY
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_SALARY(P_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_CONTRACT_EXITS
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_CONTRACT_EXITS(P_CONTRACT, P_EMP_CODE, P_DATE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IProfileBusiness.CHECK_SIGN
            Using rep As New ProfileRepository
                Try

                    Return rep.CHECK_SIGN(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_PLHD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.INPORT_PLHD
            Using rep As New ProfileRepository
                Try

                    Return rep.INPORT_PLHD(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IMPORT_CONGNO(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.IMPORT_CONGNO
            Using rep As New ProfileRepository
                Try

                    Return rep.IMPORT_CONGNO(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function IMPORT_NV(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.IMPORT_NV
            Using rep As New ProfileRepository
                Try

                    Return rep.IMPORT_NV(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable _
          Implements ServiceContracts.IProfileBusiness.GET_PROCESS_PLCONTRACT_PORTAL
            Using rep As New ProfileRepository
                Try
                    Return rep.GET_PROCESS_PLCONTRACT_PORTAL(P_EMP_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace
