Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
    Public Function Calculator_Salary(ByVal data_in As String) As DataTable

        Using rep As New ProfileBusinessClient
            Try
                Return rep.Calculator_Salary(data_in)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#Region "File"
    Public Function InsertAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertAttatch_Manager(fileInfo, fileBytes)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyEmployeeHuFile(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateAttatch_Manager(fileInfo, fileBytes)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function DeleteEmployeeHuFile(ByVal fileID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteAttatch_Manager(fileID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeHuFile(ByVal _filter As HuFileDTO) As List(Of HuFileDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeHuFile(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Quá trình đào tạo ngoài công ty"
    Public Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                   Optional ByRef PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Dim lstPro_train As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstPro_train = rep.GetProcessTraining(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstPro_train
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertProcessTraining(ByVal lstPro_train As HU_PRO_TRAIN_OUT_COMPANYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertProcessTraining(lstPro_train, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ModifyProcessTraining(ByVal lstPro_train As HU_PRO_TRAIN_OUT_COMPANYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyProcessTraining(lstPro_train, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteProcessTraining(ByVal lstPro_train As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteProcessTraining(lstPro_train)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistEmployeeCertificate_IsMain(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistEmployeeCertificate_IsMain(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckExistEmployeeCertificate_IsMajor(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistEmployeeCertificate_IsMajor(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "EmployeeTrain"
    Public Function InsertEmployeeTrain(ByVal objTrain As EmployeeTrainDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertEmployeeTrain(objTrain, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyEmployeeTrain(ByVal objTrain As EmployeeTrainDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyEmployeeTrain(objTrain, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteEmployeeTrain(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteEmployeeTrain(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeTrain(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeTrainForCompany(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeTrainByID(EmployeeID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateEmployeeTrain(objValidate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "WorkingBefore"
    Public Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWorkingBefore(objWorkingBefore, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWorkingBefore(objWorkingBefore, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteWorkingBefore(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWorkingBefore(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmpWorkingBefore(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Hoadm"

#Region "AssetMng"

    Public Function GetAssetMng(ByVal _filter As AssetMngDTO,
                                ByVal _param As ParamDTO,
                                ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetMngDTO)
        Dim lstAssetMng As List(Of AssetMngDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstAssetMng = rep.GetAssetMng(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lstAssetMng
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    'Public Function GetAssetMng(ByVal _filter As AssetMngDTO,
    '                          Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetMngDTO)
    '    Using rep As New ProfileBusinessClient
    '        Try
    '            Return rep.GetAssetMng(_filter, 0, Integer.MaxValue, 0, Sorts)
    '        Catch ex As Exception
    '            rep.Abort()
    '            Throw ex
    '        End Try
    '    End Using

    '    Return Nothing
    'End Function

    Public Function GetAssetMngById(ByVal _id As Integer) As AssetMngDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAssetMngById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAssetMng(ByVal objAssetMng As AssetMngDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertAssetMng(objAssetMng, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAssetMng(ByVal objAssetMng As AssetMngDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyAssetMng(objAssetMng, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAssetMng(ByVal lstAssetMng As List(Of AssetMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteAssetMng(lstAssetMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "WelfareMng"
    Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim dt = rep.GetWelfareListAuto(_filter, PageIndex, PageSize, Total, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GET_DETAILS_EMP(ByVal P_ID As Decimal, ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim dt = rep.GET_DETAILS_EMP(P_ID, P_WELFARE_ID, P_DATE, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_EXPORT_EMP(ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Dim dt = rep.GET_EXPORT_EMP(P_WELFARE_ID, P_DATE, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_INFO_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim dt = rep.GET_INFO_EMPLOYEE(P_EMP_CODE)
                Return dt
            Catch ex As Exception

            End Try
        End Using
    End Function
    Public Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO)
        Dim lstWelfareMng As List(Of WelfareMngDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstWelfareMng = rep.GetWelfareMng(_filter, IsDissolve, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstWelfareMng
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWelfareMng(_filter, IsDissolve, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetWelfareMngById(ByVal _id As Integer) As WelfareMngDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWelfareMngById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckWelfareMngEffect(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertWelfareMng(ByVal lstWelfareMng As WelfareMngDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertWelfareMng(lstWelfareMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetlistWelfareEMP(ByVal Id As Integer) As List(Of Welfatemng_empDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetlistWelfareEMP(Id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyWelfareMng(ByVal lstWelfareMng As WelfareMngDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyWelfareMng(lstWelfareMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWelfareMng(ByVal lstWelfareMng As List(Of WelfareMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteWelfareMng(lstWelfareMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#End Region

#Region "Quản lý bảo hộ lao động"

    Public Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO)
        Dim lstLabourProtectionMng As List(Of LabourProtectionMngDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstLabourProtectionMng = rep.GetLabourProtectionMng(_filter, IsDissolve, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstLabourProtectionMng
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO, ByVal IsDissolve As Integer,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLabourProtectionMng(_filter, IsDissolve, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetLabourProtectionMngById(ByVal _id As Integer) As LabourProtectionMngDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLabourProtectionMngById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertLabourProtectionMng(lstLabourProtectionMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyLabourProtectionMng(lstLabourProtectionMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteLabourProtectionMng(lstLabourProtectionMng, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Quản lý an toàn lao động"

    Public Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO)
        Dim lstOccupationalSafety As List(Of OccupationalSafetyDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOccupationalSafety = rep.GetOccupationalSafety(_filter, IsDissolve, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstOccupationalSafety
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer,
                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOccupationalSafety(_filter, IsDissolve, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function


    Public Function GetOccupationalSafetyById(ByVal _id As Integer) As OccupationalSafetyDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOccupationalSafetyById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOccupationalSafety(lstOccupationalSafety, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOccupationalSafety(lstOccupationalSafety, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOccupationalSafety(ByVal lstOccupationalSafety As List(Of OccupationalSafetyDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteOccupationalSafety(lstOccupationalSafety, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Danh mục thông tin lương"

#End Region

#Region "PLHD"
    Public Function GetContractForm(ByVal formID As Decimal) As OtherListDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractForm(formID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

    Public Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        Dim lstCommend As List(Of Temp_ConcurrentlyDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommend = rep.GET_LIST_CONCURRENTLY(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstCommend
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_LIST_CONCURRENTLY(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal EMPLOYEE_ID As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, PageIndex, PageSize, Total, Me.Log, EMPLOYEE_ID, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO,
                               ByVal EMPLOYEE_ID As Decimal,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, 0, Integer.MaxValue, 0, Me.Log, EMPLOYEE_ID, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_CONCURRENTLY_BY_ID(ByVal P_ID As Decimal) As DataTable
        Dim dtdata As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.GET_CONCURRENTLY_BY_ID(P_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer
        Dim dtdata As Integer

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.INSERT_CONCURRENTLY(concurrently)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UPDATE_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer
        Dim dtdata As Integer

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.UPDATE_CONCURRENTLY(concurrently)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_CONCURRENTLY_BY_EMP(ByVal P_ID As Decimal) As DataTable
        Dim dtdata As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.GET_CONCURRENTLY_BY_EMP(P_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function DELETE_CONCURRENTLY(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DELETE_CONCURRENTLY(lstID, Me.Log)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
    Public Function Validate_Concurrently(ByVal _validate As Temp_ConcurrentlyDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Validate_Concurrently(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GET_TITLE_ORG(ByVal P_ID As Decimal) As DataTable
        Dim dtdata As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.GET_TITLE_ORG(P_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function INSERT_EMPLOYEE_KN(ByVal P_EMPLOYEE_CODE As String,
                                       ByVal P_ORG_ID As Decimal,
                                       ByVal P_TITLE As Decimal,
                                       ByVal P_DATE As Date,
                                       ByVal P_ID_KN As Decimal,
                                       ByVal P_STAFF_RANK_ID As Decimal) As Boolean
        Dim dtdata As Boolean

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.INSERT_EMPLOYEE_KN(P_EMPLOYEE_CODE, P_ORG_ID, P_TITLE, P_DATE, P_ID_KN, P_STAFF_RANK_ID)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UPDATE_EMPLOYEE_KN(ByVal P_ID_KN As Decimal,
                                       ByVal P_DATE As Date) As Boolean
        Dim dtdata As Boolean

        Using rep As New ProfileBusinessClient
            Try
                dtdata = rep.UPDATE_EMPLOYEE_KN(P_ID_KN, P_DATE)
                Return dtdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetBCCCImport() As DataSet
        Dim dsdata As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dsdata = rep.GetBCCCImport()
                Return dsdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetQTCTImport() As DataSet
        Dim dsdata As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dsdata = rep.GetQTCTImport()
                Return dsdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetHopdongImport() As DataSet
        Dim dsdata As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dsdata = rep.GetHopdongImport()
                Return dsdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetHoSoLuongImport() As DataSet
        Dim dsdata As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dsdata = rep.GetHoSoLuongImport()
                Return dsdata
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCertificates(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal _param As ParamDTO,
                                    ByRef PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetCertificates(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCertificates(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal _param As ProfileBusiness.ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)

        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetCertificates(_filter, _param, 0, Integer.MaxValue, 0, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#Region "Quan ly công nợ"

    Public Function GetDebtMng(ByVal _filter As DebtDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DebtDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDebtMng(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetDebtMng(ByVal _filter As DebtDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DebtDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDebtMng(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Stocks"

    Public Function GetStocks(ByVal _filter As StocksDTO, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StocksDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStocks(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetStocks(ByVal _filter As StocksDTO, ByVal _param As ProfileBusiness.ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StocksDTO)
        Dim lst As List(Of StocksDTO)
        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetStocks(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetStocksByID(ByVal _filter As StocksDTO) As StocksDTO
        Using rep As New ProfileBusinessClient
            Try
                Dim obj = rep.GetStocksByID(_filter)
                Return obj
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertStocks(ByVal obj As StocksDTO, ByRef gID As Decimal)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertStocks(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyStocks(ByVal obj As StocksDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyStocks(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateStocks(ByVal obj As StocksDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateStocks(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateStocksGenerate(ByVal obj As StocksDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateStocksGenerate(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteStocks(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteStocks(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Stocks Transaction"

    Public Function GetStocksTransaction(ByVal _filter As StocksTransactionDTO, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer, ByVal _param As ParamDTO,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StocksTransactionDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetStocksTransaction(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function
    Public Function GetStocksTransaction(ByVal _filter As StocksTransactionDTO, ByVal _param As ProfileBusiness.ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StocksTransactionDTO)
        Dim lst As List(Of StocksTransactionDTO)
        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetStocksTransaction(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertStocksTransaction(ByVal obj As StocksTransactionDTO, ByRef gID As Decimal)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertStocksTransaction(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyStocksTransaction(ByVal obj As StocksTransactionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyStocksTransaction(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function ValidateStocksTransactionStatus(ByVal obj As StocksTransactionDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateStocksTransactionStatus(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateStocksTransactionBefore(ByVal obj As StocksTransactionDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateStocksTransactionBefore(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateStocksTransaction(ByVal obj As StocksTransactionDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateStocksTransaction(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteStocksTransaction(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteStocksTransaction(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
End Class
