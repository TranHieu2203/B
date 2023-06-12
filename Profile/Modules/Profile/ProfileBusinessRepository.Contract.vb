Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
#Region "evaluate"
    Public Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluateEmp(_empId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningEvaluateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluate(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningEvaluateDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluate(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingEvaluate(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingEvaluate(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingEvaluateByID(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingEvaluateByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingEvaluate(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#Region "traning manage"
    Public Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListTrainingManageByEmpID(_filter, _param, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                           ByVal PageSize As Integer,
                           ByRef Total As Integer, ByVal _param As ParamDTO,
                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManage(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManage(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingManage(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingManage(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingManage(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingManageByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingManageByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingManage(ByVal objContract As TrainningManageDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingManage(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Manage annual leave plans (ALP)"
    Public Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListALPByEmpID(_filter, _param, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                           ByVal PageSize As Integer,
                           ByRef Total As Integer, ByVal _param As ParamDTO,
                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetALP(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetALP(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetALP(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertALP(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertALP(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyALP(ByVal objContract As TrainningManageDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyALP(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetALPByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetALPByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteALP(ByVal objContract As TrainningManageDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteALP(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckEmployee_Exits(empCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ImportAnnualLeave(P_DOCXML, P_USER, P_YEAR)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "emp bank"
    Public Function saveEmpBank(ByVal dt As DataTable) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.saveEmpBank(dt, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetEmpBank(ByVal _filter As EmpBankDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmpBankDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmpBank(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetEmpBank(ByVal _filter As EmpBankDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmpBankDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmpBank(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "trainingforeign"
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningForeignDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeign(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningForeignDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeign(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTrainingForeign(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTrainingForeign(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetTrainingForeignByID(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTrainingForeignByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DeleteTrainingForeign(ByVal objContract As TrainningForeignDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTrainingForeign(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
    Public Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateDateToContract(id, day, remark)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContract(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Using rep As New ProfileBusinessClient
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
    Public Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Using rep As New ProfileBusinessClient
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

    Public Function GetContract(ByVal _filter As ContractDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContract(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateContract(sType, obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckStartDate1(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckStartDate1(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function CheckStartDate(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckStartDate(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteContract(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CreateContractNo(ByVal objContract As ContractDTO) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CreateContractNo(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckContractNo(objContract)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractEmployeeByID(gEmployeeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetMaxConId(ByVal empId As Decimal) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return GetMaxConId(empId)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function UnApproveContract(ByVal objContract As ContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UnApproveContract(objContract, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal acti As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveListContract(listID, acti, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class
