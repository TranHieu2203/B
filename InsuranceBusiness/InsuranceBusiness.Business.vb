Imports Framework.Data
Imports InsuranceBusiness.ServiceContracts
Imports InsuranceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace InsuranceBusiness.ServiceImplementations
    Partial Public Class InsuranceBusiness
        Implements IInsuranceBusiness
#Region "Đóng mới bảo hiểm"
        Public Function GetINS_ARISING(ByVal _filter As INS_ARISINGDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_ARISINGDTO) Implements IInsuranceBusiness.GetINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_ARISING(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertINS_ARISING(ByVal objLeave As List(Of INS_ARISINGDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IInsuranceBusiness.InsertINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.InsertINS_ARISING(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyINS_ARISING(ByVal objLeave As INS_ARISINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IInsuranceBusiness.ModifyINS_ARISING

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.ModifyINS_ARISING(objLeave, log, gID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetINS_ARISINGyById(ByVal _id As Decimal?) As INS_ARISINGDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_ARISINGyById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_ARISINGyById(_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quản lý thông tin bảo hiểm"
        Function GetINS_INFO(ByVal _filter As INS_INFORMATIONDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFORMATIONDTO) Implements ServiceContracts.IInsuranceBusiness.GetINS_INFO

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_INFO(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_INFOById(ByVal _id As Decimal?) As INS_INFORMATIONDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_INFOById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_INFOById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_INFO(objIns_Info, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_INFO(ByVal objIns_Info As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_INFO(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_INFO(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_INFO
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_INFO(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetInfoPrint(ByVal LISTID As String) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetInfoPrint

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetInfoPrint(LISTID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetLuongBH(ByVal p_EMPLOYEE_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLuongBH

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetLuongBH(p_EMPLOYEE_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetEmployeeID(ByVal p_EMPLOYEE_ID As String) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetEmployeeID

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetEmployeeID(p_EMPLOYEE_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetAllowanceTotalByDate(ByVal EMPLOYEE_ID As Decimal) As Decimal? Implements ServiceContracts.IInsuranceBusiness.GetAllowanceTotalByDate

            Using rep As New InsuranceRepository
                Try
                    Dim dInsuran As Decimal?
                    dInsuran = rep.GetAllowanceTotalByDate(EMPLOYEE_ID)
                    Return dInsuran
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quan ly thong tin bao hiem cu"
        Function GetINS_INFOOLD(ByVal _filter As INS_INFOOLDDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFOOLDDTO) Implements IInsuranceBusiness.GetINS_INFOOLD

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_INFOOLD(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetINS_INFOOLDById(ByVal _id As Decimal?) As INS_INFOOLDDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_INFOOLDById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_INFOOLDById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertINS_INFOOLD(ByVal objRegisterOT As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_INFOOLD(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyINS_INFOOLD(ByVal objRegisterOT As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_INFOOLD(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteINS_INFOOLD(ByVal lstID As List(Of INS_INFOOLDDTO)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_INFOOLD
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_INFOOLD(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeById(ByVal _id As Decimal?) As EmployeeDTO Implements ServiceContracts.IInsuranceBusiness.GetEmployeeById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetEmployeeById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeByIdProcess(ByRef P_EMPLOYEEID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetEmployeeByIdProcess
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetEmployeeByIdProcess(P_EMPLOYEEID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Quan ly bien dong bao hiem"
        Function GetINS_CHANGE(ByVal _filter As INS_CHANGEDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGEDTO) Implements IInsuranceBusiness.GetINS_CHANGE

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetINS_CHANGE(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_CHANGEById(ByVal _id As Decimal?) As INS_CHANGEDTO Implements ServiceContracts.IInsuranceBusiness.GetINS_CHANGEById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetINS_CHANGEById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertINS_CHANGE(ByVal objRegisterOT As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertINS_CHANGE(objRegisterOT, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyINS_CHANGE(ByVal objRegisterOT As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyINS_CHANGE(objRegisterOT, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteINS_CHANGE(ByVal lstID As List(Of INS_CHANGEDTO)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteINS_CHANGE
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteINS_CHANGE(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTiLeDong() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetTiLeDong
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetTiLeDong()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETLUONGBIENDONG(ByRef P_EMPLOYEEID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GETLUONGBIENDONG
            Using rep As New InsuranceRepository
                Try
                    Return rep.GETLUONGBIENDONG(P_EMPLOYEEID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "thay doi thong tin tham gia bao hiem"
        Public Function InsertOrModifyChangeInfo(ByVal objTitle As INS_CHANGE_INFO_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertOrModifyChangeInfo
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertOrModifyChangeInfo(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetChangeInfoById(ByVal _id As Decimal?) As INS_CHANGE_INFO_DTO Implements ServiceContracts.IInsuranceBusiness.GetChangeInfoById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetChangeInfoById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteChangeInfo(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteChangeInfo
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteChangeInfo(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetChangeInfo(ByVal _filter As INS_CHANGE_INFO_DTO,
                                    ByVal OrgId As Integer,
                                    ByVal Fillter As String,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGE_INFO_DTO) Implements ServiceContracts.IInsuranceBusiness.GetChangeInfo
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetChangeInfo(_filter, OrgId, Fillter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function CheckDouble(ByVal id As Decimal, ByVal emp_id As Decimal, ByVal type_id As Decimal, ByVal date_change As Date) As Decimal Implements ServiceContracts.IInsuranceBusiness.CheckDouble
            Using rep As New InsuranceRepository
                Try

                    Return rep.CheckDouble(id, emp_id, type_id, date_change)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Quản lý Sun Care"
        Public Function GetSunCare(ByVal _filter As INS_SUN_CARE_DTO,
                                    ByVal OrgId As Integer,
                                    ByVal Fillter As String,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_SUN_CARE_DTO) Implements ServiceContracts.IInsuranceBusiness.GetSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetSunCare(_filter, OrgId, Fillter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSunCareById(ByVal _id As Decimal?) As INS_SUN_CARE_DTO Implements ServiceContracts.IInsuranceBusiness.GetSunCareById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetSunCareById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetIns_Cost_LeverByID(ByVal _id As Decimal?) As INS_COST_FOLLOW_LEVERDTO Implements ServiceContracts.IInsuranceBusiness.GetIns_Cost_LeverByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetIns_Cost_LeverByID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLevelImport() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLevelImport
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetLevelImport()
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertSunCare(ByVal objTitle As INS_SUN_CARE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertSunCare(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifySunCare(ByVal objTitle As INS_SUN_CARE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifySunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifySunCare(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_MANAGER_SUN_CARE(ByVal P_EMP_CODE As String, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_LEVEL_ID As Decimal) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_MANAGER_SUN_CARE
            Using rep As New InsuranceRepository
                Try

                    Return rep.CHECK_MANAGER_SUN_CARE(P_EMP_CODE, P_START_DATE, P_END_DATE, P_LEVEL_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveSunCare(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ActiveSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ActiveSunCare(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteSunCare(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteSunCare(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_EMPLOYEE
            Using rep As New InsuranceRepository
                Try

                    Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INPORT_MANAGER_SUN_CARE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.INPORT_MANAGER_SUN_CARE
            Using rep As New InsuranceRepository
                Try

                    Return rep.INPORT_MANAGER_SUN_CARE(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Quản lý bảo hiểm sức khỏe cá nhân, người thân"
        Public Function InsertHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertHEALTH_INSURANCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertHEALTH_INSURANCE(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyHEALTH_INSURANCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyHEALTH_INSURANCE(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCLAIME_INSURANCE(ByVal objTitle As INS_CLAIME_INSURANCEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertCLAIME_INSURANCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertCLAIME_INSURANCE(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateHealth_Insurance(ByVal _validate As INS_HEALTH_INSURANCE_DTO) Implements ServiceContracts.IInsuranceBusiness.ValidateHealth_Insurance
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateHealth_Insurance(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_INS_HEALTH_INSURANCE(
                                             ByVal ORGID As Integer?,
                                             ByVal IS_TERMINATE As Integer?,
                                             ByVal IS_CLAIM As Integer?,
                                             ByVal ISDISSOLVE As Integer?,
                                             ByVal DJOIN_START As Date?,
                                             ByVal DJOIN_END As Date?,
                                             ByVal D_EFFECT_START As Date?,
                                             ByVal D_EFFECT_END As Date?,
                                             ByVal DREDUCE_START As Date?,
                                             ByVal DREDUCE_END As Date?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_HEALTH_INSURANCE_DTO) Implements ServiceContracts.IInsuranceBusiness.GET_INS_HEALTH_INSURANCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.GET_INS_HEALTH_INSURANCE(ORGID, IS_TERMINATE, IS_CLAIM, ISDISSOLVE, DJOIN_START, DJOIN_END, D_EFFECT_START, D_EFFECT_END, DREDUCE_START, DREDUCE_END, PAGE, SIZE, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Delete_INS_HEALTH_INSURANCE(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.Delete_INS_HEALTH_INSURANCE
            Using rep As New InsuranceRepository
                Try

                    Return rep.Delete_INS_HEALTH_INSURANCE(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "Quản lý chế độ bảo hiểm"
        Public Function GetInfoInsByEmpID(ByVal employee_id As Integer) As INS_INFORMATIONDTO Implements ServiceContracts.IInsuranceBusiness.GetInfoInsByEmpID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetInfoInsByEmpID(employee_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetLuyKe(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date,
                                      ByRef P_EMPLOYEEID As Integer,
                                      ByVal P_ENTITLED_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetLuyKe
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetLuyKe(P_TUNGAY, P_DENNGAY, P_EMPLOYEEID, P_ENTITLED_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CALCULATOR_DAY(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date) As DataTable Implements ServiceContracts.IInsuranceBusiness.CALCULATOR_DAY
            Using rep As New InsuranceRepository
                Try

                    Return rep.CALCULATOR_DAY(P_TUNGAY, P_DENNGAY)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTienHuong(ByVal P_NUMOFF As Integer,
                                    ByVal P_ATHOME As Integer,
                                    ByRef P_EMPLOYEEID As Integer,
                                    ByVal P_INSENTILEDKEY As Integer,
                                    ByVal P_SALARY_ADJACENT As Decimal,
                                    ByVal P_FROMDATE As Date,
                                    ByVal P_SOCON As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetTienHuong
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetTienHuong(P_NUMOFF, P_ATHOME, P_EMPLOYEEID, P_INSENTILEDKEY, P_SALARY_ADJACENT, P_FROMDATE, P_SOCON)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetMaxDayByID(ByVal P_ENTITLED_ID As Integer) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetMaxDayByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetMaxDayByID(P_ENTITLED_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRegimeManager(ByVal _filter As INS_REMIGE_MANAGER_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                        ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REMIGE_MANAGER_DTO) Implements ServiceContracts.IInsuranceBusiness.GetRegimeManager
            Using rep As New InsuranceRepository
                Try
                    Return rep.GetRegimeManager(_filter, PageIndex, PageSize, Total, OrgId, IsDissolve, EntiledID, Fillter, log, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetRegimeManagerByID(ByVal _id As Decimal?) As INS_REMIGE_MANAGER_DTO Implements ServiceContracts.IInsuranceBusiness.GetRegimeManagerByID
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetRegimeManagerByID(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertRegimeManager(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyRegimeManager(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Validate_KhamThai(ByVal _validate As INS_REMIGE_MANAGER_DTO) As DataTable Implements ServiceContracts.IInsuranceBusiness.Validate_KhamThai
            Using rep As New InsuranceRepository
                Try

                    Return rep.Validate_KhamThai(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateGroupRegime(ByVal _validate As INS_GROUP_REGIMESDTO) Implements ServiceContracts.IInsuranceBusiness.ValidateGroupRegime
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateGroupRegime(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateListProgram(ByVal _validate As INS_LIST_PROGRAMDTO) Implements ServiceContracts.IInsuranceBusiness.ValidateListProgram
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateListProgram(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateListContract(ByVal _validate As INS_LIST_CONTRACTDTO) Implements ServiceContracts.IInsuranceBusiness.ValidateListContract
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateListContract(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateRegimeManager(ByVal _validate As INS_REMIGE_MANAGER_DTO) Implements ServiceContracts.IInsuranceBusiness.ValidateRegimeManager
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateRegimeManager(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteRegimeManager(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteRegimeManager
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteRegimeManager(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Khai báo điều chỉnh nhóm bảo hiểm sun care"
        Function GetGroup_SunCare(ByVal _filter As INS_GROUP_SUN_CAREDTO,
                                  ByVal _param As PARAMDTO,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_GROUP_SUN_CAREDTO) Implements ServiceContracts.IInsuranceBusiness.GetGroup_SunCare

            Using rep As New InsuranceRepository
                Try
                    Dim lst = rep.GetGroup_SunCare(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetGroup_SunCareById(ByVal _id As Decimal?) As INS_GROUP_SUN_CAREDTO Implements ServiceContracts.IInsuranceBusiness.GetGroup_SunCareById
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetGroup_SunCareById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertGroup_SunCare(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyGroup_SunCare(ByVal objIns_Info As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyGroup_SunCare(objIns_Info, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteGroup_SunCare(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.DeleteGroup_SunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.DeleteGroup_SunCare(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        'anhvn
        Public Function GetINS_CLAIME_INSURANCE_BY_ID(ByVal ID As Decimal?) As List(Of INS_CLAIME_INSURANCEDTO) _
                 Implements ServiceContracts.IInsuranceBusiness.GetINS_CLAIME_INSURANCE_BY_ID
            Try
                Dim rep As New DataAccess.QueryData
                Dim dt As New DataTable
                Dim lst As New List(Of INS_CLAIME_INSURANCEDTO)
                Dim ds As Object = rep.ExecuteStore("PKG_INS_BUSINESS.GetINS_CLAIME_INSURANCE_BY_ID",
                                                 New With {.P_ID = IIf(ID Is Nothing, System.DBNull.Value, ID),
                                                 .P_CUR = OUT_CURSOR})
                dt = ds
                'If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                '    dt = ds.Tables(0)
                'End If
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim itm As New INS_CLAIME_INSURANCEDTO
                        itm.ID = dr("ID")
                        itm.INS_HEALTH_ID = dr("INS_HEALTH_ID")
                        itm.EXAMINE_DATE = dr("EXAMINE_DATE")
                        itm.DISEASE_NAME = dr("DISEASE_NAME")
                        itm.AMOUNT_OF_CLAIMS = dr("AMOUNT_OF_CLAIMS")
                        itm.AMOUNT_OF_COMPENSATION = dr("AMOUNT_OF_COMPENSATION")
                        itm.COMPENSATION_DATE = dr("COMPENSATION_DATE")
                        itm.NOTE = dr("NOTE")
                        lst.Add(itm)
                    Next
                End If

                Return lst
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function DELETE_INS_CLAIME_INSURANCE(ByVal ID As Decimal?) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DELETE_INS_CLAIME_INSURANCE
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.DELETE_INS_CLAIME_INSURANCE",
                                                      New With {.P_ID = IIf(ID Is Nothing, System.DBNull.Value, ID)})
                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function
        Public Function Get_LIST_FAMILY_BY_ID_EMP(ByVal id As Decimal?) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.Get_LIST_FAMILY_BY_ID_EMP
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_HU_IPROFILE.Get_LIST_FAMILY_BY_ID_EMP",
                                                 New With {.P_USERID = IIf(id Is Nothing, System.DBNull.Value, id),
                                                 .P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GET_INS_HEALTH_INSURANCE_BY_ID(ByVal id As Decimal?) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.GET_INS_HEALTH_INSURANCE_BY_ID
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_INS_BUSINESS.GET_INS_HEALTH_INSURANCE_BY_ID",
                                                 New With {.P_ID = IIf(id Is Nothing, System.DBNull.Value, id),
                                                 .P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

#Region "Báo cáo bảo hiểm"
        Public Function GetReportList() As DataTable Implements ServiceContracts.IInsuranceBusiness.GetReportList
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetReportList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       log As UserLog,
                                       Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IInsuranceBusiness.GetReportById
            Using rep As New InsuranceRepository
                Try

                    Dim lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetD02Tang(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetD02Tang
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetD02Tang(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetD02Giam(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetD02Giam
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetD02Giam(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetC70_HD(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetC70_HD
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetC70_HD(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetQuyLuongBH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetQuyLuongBH
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetQuyLuongBH(p_MONTH, p_YEAR, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDsBHSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetDsBHSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetDsBHSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetDsDieuChinhSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetDsDieuChinhSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetDsDieuChinhSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetChiPhiSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetChiPhiSunCare
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetChiPhiSunCare(p_Tungay, p_Toingay, p_Username, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrgInfo(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetOrgInfo
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetOrgInfo(p_Tungay, p_Toingay, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrgInfoMONTH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Org_ID As Decimal) As DataTable Implements ServiceContracts.IInsuranceBusiness.GetOrgInfoMONTH
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetOrgInfoMONTH(p_MONTH, p_YEAR, p_Org_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region " Tổng hợp loại nghỉ bảo hiểm"
        Public Function GetInsArisingLeaveSheet(ByVal _filter As INS_ARISING_LEAVESHEET_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                        ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE asc") As List(Of INS_ARISING_LEAVESHEET_DTO) Implements ServiceContracts.IInsuranceBusiness.GetInsArisingLeaveSheet
            Using rep As New InsuranceRepository
                Try
                    Return rep.GetInsArisingLeaveSheet(_filter, PageIndex, PageSize, Total, OrgId, IsDissolve, EntiledID, Fillter, log, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_INS_ARSING_LEAVE_SHEET(ByVal OrgId As Integer,
                                               ByVal IsDissolve As Integer,
                                               ByVal startDate As Date,
                                               ByVal endDate As Date,
                                               ByVal LeaveType As String,
                                               ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.GET_INS_ARSING_LEAVE_SHEET
            Using rep As New InsuranceRepository
                Try
                    Return rep.GET_INS_ARSING_LEAVE_SHEET(OrgId, IsDissolve, startDate, endDate, LeaveType, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_INS_ARSING_LEAVE_SHEET(ByVal P_LIST_ID As String,
                                                  ByVal P_FROM_MONTH As Date,
                                                  ByVal P_TO_MONTH As Date,
                                                  ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.UPDATE_INS_ARSING_LEAVE_SHEET
            Using rep As New InsuranceRepository
                Try
                    Return rep.UPDATE_INS_ARSING_LEAVE_SHEET(P_LIST_ID, P_FROM_MONTH, P_TO_MONTH, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_INS_ARSING_TANG(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.INSERT_INS_ARSING_TANG
            Using rep As New InsuranceRepository
                Try
                    Return rep.INSERT_INS_ARSING_TANG(P_FROM_MONTH, P_TO_MONTH, OrgId, IsDissolve, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_INS_ARSING_GIAM(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.INSERT_INS_ARSING_GIAM
            Using rep As New InsuranceRepository
                Try
                    Return rep.INSERT_INS_ARSING_GIAM(P_FROM_MONTH, P_TO_MONTH, OrgId, IsDissolve, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_ORG(ByVal P_EMP_ID As Decimal,
                                  ByVal P_INS_ORG_ID As Decimal,
                                  ByVal P_ARISING_FROM_MONTH As Date,
                                  ByRef pDate As String) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_ORG
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_ORG(P_EMP_ID, P_INS_ORG_ID, P_ARISING_FROM_MONTH, pDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_ORG_TANG_GIAM(ByVal P_INS_ORG_ID As Decimal) As String Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_ORG_TANG_GIAM
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_ORG_TANG_GIAM(P_INS_ORG_ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CHECK_INS_MANUAL(ByVal P_EMP_ID As Decimal,
                                          ByVal P_INS_ORG_ID As Decimal,
                                          ByVal P_ARISING_FROM_MONTH As Date,
                                          ByRef pDate As String) As Integer Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_MANUAL
            Using rep As New InsuranceRepository
                Try
                    Return rep.CHECK_INS_MANUAL(P_EMP_ID, P_INS_ORG_ID, P_ARISING_FROM_MONTH, pDate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_INS_INFORMATION(ByVal P_EMP_ID As Decimal,
                                           ByVal P_ARISING_FROM_MONTH As Date) As Boolean Implements ServiceContracts.IInsuranceBusiness.UPDATE_INS_INFORMATION
            Using rep As New InsuranceRepository
                Try
                    Return rep.UPDATE_INS_INFORMATION(P_EMP_ID, P_ARISING_FROM_MONTH)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region
#Region "Quá trình bảo hiểm trước khi vào công ty"
        Public Function GetInsWorkingBefore(ByVal _filter As INS_WORKING_BEFOREDTO,
                                    ByVal OrgId As Integer,
                                    ByVal Fillter As String,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_WORKING_BEFOREDTO) Implements ServiceContracts.IInsuranceBusiness.GetInsWorkingBefore
            Using rep As New InsuranceRepository
                Try

                    Return rep.GetInsWorkingBefore(_filter, OrgId, Fillter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertInsWorkingBefore(ByVal obj As INS_WORKING_BEFOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.InsertInsWorkingBefore
            Using rep As New InsuranceRepository
                Try

                    Return rep.InsertInsWorkingBefore(obj, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyInsWorkingBefore(ByVal objTitle As INS_WORKING_BEFOREDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IInsuranceBusiness.ModifyInsWorkingBefore
            Using rep As New InsuranceRepository
                Try

                    Return rep.ModifyInsWorkingBefore(objTitle, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Delete_InsWorkingBefore(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IInsuranceBusiness.Delete_InsWorkingBefore
            Using rep As New InsuranceRepository
                Try

                    Return rep.Delete_InsWorkingBefore(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function INPORT_InsWorkingBefore(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IInsuranceBusiness.INPORT_InsWorkingBefore
            Using rep As New InsuranceRepository
                Try

                    Return rep.INPORT_InsWorkingBefore(P_DOCXML, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ValidateWorkingBefore(ByVal _validate As INS_WORKING_BEFOREDTO) Implements ServiceContracts.IInsuranceBusiness.ValidateWorkingBefore
            Using rep As New InsuranceRepository
                Try
                    Return rep.ValidateWorkingBefore(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace
