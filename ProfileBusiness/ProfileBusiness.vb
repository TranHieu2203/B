Imports System.IO
Imports Framework.Data
Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace ProfileBusiness.ServiceImplementations

    Public Class ProfileBusiness
        Implements IProfileBusiness


#Region "Other List"
        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetOtherList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOtherList(sType, sLang, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_DEFAULT_OBJECT_ATTENDANCE() As Integer Implements ServiceContracts.IProfileBusiness.GET_DEFAULT_OBJECT_ATTENDANCE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_DEFAULT_OBJECT_ATTENDANCE()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetInsListWhereHealth() As DataTable Implements ServiceContracts.IProfileBusiness.GetInsListWhereHealth
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetInsListWhereHealth()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_Contract_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable Implements ServiceContracts.IProfileBusiness.GetINS_Contract_Combobox
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetINS_Contract_Combobox(Is_Full)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ByVal id As Decimal?) As DataTable Implements ServiceContracts.IProfileBusiness.GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.Get_HU_WORK_PLACE
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.Get_HU_WORK_PLACE(sType, sLang, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal, ByVal sLang As String) As DataTable Implements ServiceContracts.IProfileBusiness.HU_PAPER_LIST
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.HU_PAPER_LIST(P_EMP_ID, sLang)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetBankList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetBankList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetBankList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetBankBranchList(ByVal bankID As Decimal, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetBankBranchList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetBankBranchList(bankID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        'Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetTitleByOrgID
        '    Using rep As New ProfileRepository
        '        Try

        '            Dim lst = rep.GetTitleByOrgID(orgID, sLang, isBlank)
        '            Return lst
        '        Catch ex As Exception
        '            Throw ex
        '        End Try
        '    End Using
        'End Function
        Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean, ByVal Employee_ID As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetTitleByOrgID
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetTitleByOrgID(orgID, sLang, isBlank, Employee_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetTitleList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitleList(sLang, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetWardList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetWardList(districtID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetDistrictList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDistrictList(provinceID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProvinceList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetProvinceList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvinceList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProvinceList2(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetProvinceList2
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvinceList2(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetProvinceList1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetProvinceList1(P_NATIVE, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetNationList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetNationList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetNationList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetStaffRankList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetStaffRankList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetStaffRankList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_INS_HEALTH_BY_EMPID(ByVal emp_ID As Decimal?) As DataTable Implements ServiceContracts.IProfileBusiness.GET_INS_HEALTH_BY_EMPID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_INS_HEALTH_BY_EMPID(emp_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetSalaryGroupList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSalaryGroupList(dateValue, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSalaryTupeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetSalaryTypeList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSalaryTypeList(dateValue, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_Hu_Allowance_List(ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IProfileBusiness.GET_Hu_Allowance_List
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GET_Hu_Allowance_List(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable _
           Implements ServiceContracts.IProfileBusiness.GetSaleCommisionList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSaleCommisionList(dateValue, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        'Public Function GetSalaryTypes(ByVal query As PA_SALARY_TYPEQuery) As PA_SALARY_TYPEResult _
        '    Implements ServiceContracts.IProfileBusiness.GetSalaryTypes
        '    Using rep As New ProfileRepository
        '        Return rep.GetPA_SALARY_TYPE(query)
        '    End Using
        'End Function
        Public Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetSalaryLevelList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSalaryLevelList(salGroupID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetSalaryRankList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetSalaryRankList(salLevelID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_AllowanceList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_AllowanceList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_AllowanceList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPA_ObjectSalary(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetPA_ObjectSalary
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetPA_ObjectSalary(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOT_WageTypeList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetOT_WageTypeList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOT_WageTypeList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOT_MissionTypeList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetOT_MissionTypeList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOT_MissionTypeList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOT_TripartiteTypeList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetOT_TripartiteTypeList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOT_TripartiteTypeList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_TemplateType
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_TemplateType(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SaveTemplateFileHost(ByVal fByte As Byte(), ByVal folder As String, ByVal filename As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.SaveTemplateFileHost
            Using rep As New ProfileRepository
                Try

                    Dim filePath = AppDomain.CurrentDomain.BaseDirectory & folder
                    If Not System.IO.Directory.Exists(filePath) Then
                        System.IO.Directory.CreateDirectory(filePath)
                    End If
                    Dim fPath = System.IO.Path.Combine(filePath, filename)

                    System.IO.File.WriteAllBytes(fPath, fByte)
                    Return True
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTemplateFileHost(ByVal path As String) As Byte() _
            Implements ServiceContracts.IProfileBusiness.GetTemplateFileHost
            Using rep As New ProfileRepository
                Try
                    Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & path
                    If Not System.IO.File.Exists(rootPath) Then
                        Return Nothing
                    End If
                    Return System.IO.File.ReadAllBytes(rootPath)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExistsTemplateFileHost(ByVal path As String) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ExistsTemplateFileHost
            Using rep As New ProfileRepository
                Try
                    Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\" & path
                    Return System.IO.File.Exists(rootPath)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                          ByVal isTemplateType As Decimal) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_MergeFieldList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_MergeFieldList(isBlank, isTemplateType)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                    ByVal isTemplateType As Decimal) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_TemplateList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_TemplateList(isBlank, isTemplateType)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHU_DataDynamic_Muti_1(ByVal dID As String,
                                                 ByVal de As String,
                                      ByVal tempID As Decimal,
                                      ByRef folderName As String) As DataTable _
           Implements ServiceContracts.IProfileBusiness.GetHU_DataDynamic_Muti_1
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_DataDynamic_Muti_1(dID, de, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHU_DataDynamic_Muti(ByVal dID As String,
                                      ByVal tempID As Decimal,
                                      ByRef folderName As String) As DataTable _
           Implements ServiceContracts.IProfileBusiness.GetHU_DataDynamic_Muti
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_DataDynamic_Muti(dID, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_DataDynamic
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_DataDynamic(dID, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_MultyDataDynamic
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_MultyDataDynamic(strID, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function GetHU_DataDynamicContract(ByVal dID As String,
                       ByVal tempID As Decimal,
                       ByRef folderName As String) As DataTable _
Implements ServiceContracts.IProfileBusiness.GetHU_DataDynamicContract
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_DataDynamicContract(dID, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                       ByVal tempID As Decimal,
                       ByRef folderName As String) As DataTable _
Implements ServiceContracts.IProfileBusiness.GetHU_DataDynamicContractAppendix
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_DataDynamicContractAppendix(dID, tempID, folderName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommon.TABLE_NAME) As Boolean _
            Implements ServiceContracts.IProfileBusiness.CheckExistInDatabase
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.CheckExistInDatabase(lstID, table)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String _
            Implements ServiceContracts.IProfileBusiness.AutoGenCode
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.AutoGenCode(firstChar, tableName, colName)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UpdateMergeField
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.UpdateMergeField(lstData, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet _
            Implements ServiceContracts.IProfileBusiness.GetDataPrintBBBR
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDataPrintBBBR(id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet _
            Implements ServiceContracts.IProfileBusiness.GetDataPrintBBBR3B
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetDataPrintBBBR3B(id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetInsRegionList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IProfileBusiness.GetInsRegionList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetInsRegionList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_CompetencyGroupList(ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_CompetencyGroupList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_CompetencyGroupList(isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_CompetencyList(ByVal groupID As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_CompetencyList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_CompetencyList(groupID, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHU_CompetencyPeriodList(ByVal year As Decimal, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IProfileBusiness.GetHU_CompetencyPeriodList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetHU_CompetencyPeriodList(year, isBlank)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Nghiệp vụ"

        Public Function GetAllOrgDisplays() As List(Of OrganizationDTO) Implements ServiceContracts.IProfileBusiness.GetOrgsTree
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetOrgsTree
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using

        End Function

#End Region

#Region "Hoadm"

        Public Function GetComboList(ByRef _combolistDTO As ProfileDAL.ComboBoxDataDTO, Optional log As UserLog = Nothing) As Boolean Implements ServiceContracts.IProfileBusiness.GetComboList
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetComboList(_combolistDTO, log)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#Region "Service Auto Update Employee Information"
        Public Function CheckAndUpdateEmployeeInformation() As Boolean Implements ServiceContracts.IProfileBusiness.CheckAndUpdateEmployeeInformation
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckAndUpdateEmployeeInformation
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Service Send Mail Reminder"
        Public Function CheckAndSendMailReminder() As Boolean Implements ServiceContracts.IProfileBusiness.CheckAndSendMailReminder
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckAndSendMailReminder
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#End Region

#Region "Báo cáo"
        Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO) Implements ServiceContracts.IProfileBusiness.GetReportById
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetReportById(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ProfileReport(ByVal sPkgName As String, ByVal sStartDate As Date?, ByVal sEndDate As Date?, ByVal sOrg As Integer, ByVal sUserName As String, ByVal sLang As String) As DataTable Implements ServiceContracts.IProfileBusiness.ProfileReport
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.ProfileReport(sPkgName, sStartDate, sEndDate, sOrg, sUserName, sLang)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet Implements ServiceContracts.IProfileBusiness.GetEmployeeCVByID
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetEmployeeCVByID(sPkgName, sEmployee_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ExportReport(ByVal sPkgName As String,
                                     ByVal sStartDate As Date?,
                                     ByVal sEndDate As Date?,
                                     ByVal sOrg As String,
                                     ByVal IsDissolve As Integer,
                                     ByVal sUserName As String,
                                     ByVal sLang As String) As DataSet Implements ServiceContracts.IProfileBusiness.ExportReport
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.ExportReport(sPkgName, sStartDate, sEndDate, sOrg, IsDissolve, sUserName, sLang)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#Region "VALIDATE BUSINESS"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Table_Name"></param>
        ''' <param name="Column_Name"></param>
        ''' <param name="Value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ValidateBusiness(ByVal Table_Name As String, ByVal Column_Name As String, ByVal ListID As List(Of Decimal)) As Boolean Implements ServiceContracts.IProfileBusiness.ValidateBusiness
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateBusiness(Table_Name, Column_Name, ListID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using

        End Function
        Public Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean Implements ServiceContracts.IProfileBusiness.CheckExistID
            Using rep As New ProfileRepository
                Try

                    Return rep.CheckExistID(lstID, table, column)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "phu luc hop dong"
        Public Function PrintFileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable Implements ServiceContracts.IProfileBusiness.PrintFileContract
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.PrintFileContract(emp_code, fileContract_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO Implements ServiceContracts.IProfileBusiness.GetFileConTractID
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetFileConTractID(ID)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer, ByVal _param As ParamDTO,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                                             Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO) Implements ServiceContracts.IProfileBusiness.GetContractAppendixPaging
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetContractAppendixPaging(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetEmpDtlContractAppendixPaging(ByVal _filter As FileContractDTO,
                                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                        Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO) Implements ServiceContracts.IProfileBusiness.GetEmpDtlContractAppendixPaging
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetEmpDtlContractAppendixPaging(_filter, Sorts, log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteFileContract(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements IProfileBusiness.DeleteFileContract
            Try
                Dim rep As New ProfileRepository
                Return rep.DeleteFileContract(lstID, log)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO) Implements ServiceContracts.IProfileBusiness.GetListContractType
            Using rep As New ProfileRepository
                Try

                    Return rep.GetListContractType(type)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean Implements IProfileBusiness.CheckExpireFileContract
            Try
                Dim rep As New ProfileRepository
                Return rep.CheckExpireFileContract(StartDate, EndDate, ID)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal ID As Decimal) As Boolean Implements IProfileBusiness.CheckExistFileContract
            Try
                Dim rep As New ProfileRepository
                Return rep.CheckExistFileContract(empID, StartDate, ID)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function InsertFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef appenNum As String) As Boolean Implements IProfileBusiness.InsertFileContract
            Try
                Dim rep As New ProfileRepository
                Return rep.InsertFileContract(FileInfo, log, gID, appenNum)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function UpdateFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements IProfileBusiness.UpdateFileContract
            Try
                Dim rep As New ProfileRepository
                Return rep.UpdateFileContract(FileInfo, log, gID)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO) Implements ServiceContracts.IProfileBusiness.GetContractList
            Using rep As New ProfileRepository
                Try
                    Return rep.GetContractList(empID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitileBaseOnEmp(ByVal ID As Decimal) As TitleDTO _
          Implements ServiceContracts.IProfileBusiness.GetTitileBaseOnEmp
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetTitileBaseOnEmp(ID)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String Implements IProfileBusiness.GetFileContract_No
            Try
                Dim rep As New ProfileRepository
                Return rep.GetFileContract_No(Contract, STT)
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO) Implements ServiceContracts.IProfileBusiness.GetContractAppendix
            Try
                Dim rep As New ProfileRepository
                Dim lst = rep.GetContractAppendix(_filter)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO Implements ServiceContracts.IProfileBusiness.GetContractTypeID
            Using rep As New ProfileRepository
                Try

                    Return rep.GetContractTypeID(ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO) Implements ServiceContracts.IProfileBusiness.GetListContractBaseOnEmp
            Using rep As New ProfileRepository
                Try
                    Return rep.GetListContractBaseOnEmp(ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListContract(ByVal ID As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GetListContract
            Using rep As New ProfileRepository
                Try

                    Dim lst = rep.GetListContract(ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

        Public Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO) Implements ServiceContracts.IProfileBusiness.GET_LIST_CONCURRENTLY
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GET_LIST_CONCURRENTLY(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        ByVal EMPLOYEE_ID As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO) Implements ServiceContracts.IProfileBusiness.GET_LIST_CONCURRENTLY_BY_ID
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GET_LIST_CONCURRENTLY_BY_ID(_filter, PageIndex, PageSize, Total, log, EMPLOYEE_ID, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_CONCURRENTLY_BY_ID(ByVal P_ID As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GET_CONCURRENTLY_BY_ID
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GET_CONCURRENTLY_BY_ID(P_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer Implements ServiceContracts.IProfileBusiness.INSERT_CONCURRENTLY
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.INSERT_CONCURRENTLY(concurrently)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer Implements ServiceContracts.IProfileBusiness.UPDATE_CONCURRENTLY
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.UPDATE_CONCURRENTLY(concurrently)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_CONCURRENTLY_BY_EMP(ByVal P_ID As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GET_CONCURRENTLY_BY_EMP
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GET_CONCURRENTLY_BY_EMP(P_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DELETE_CONCURRENTLY(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.DELETE_CONCURRENTLY
            Using rep As New ProfileRepository
                Try
                    Return rep.DELETE_CONCURRENTLY(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Validate_Concurrently(ByVal _validate As Temp_ConcurrentlyDTO) As Boolean Implements ServiceContracts.IProfileBusiness.Validate_Concurrently
            Using rep As New ProfileRepository
                Try
                    Return rep.Validate_Concurrently(_validate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GET_TITLE_ORG(ByVal P_ID As Decimal) As DataTable Implements ServiceContracts.IProfileBusiness.GET_TITLE_ORG
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GET_TITLE_ORG(P_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function INSERT_EMPLOYEE_KN(ByVal P_EMPLOYEE_CODE As String,
                                           ByVal P_ORG_ID As Decimal,
                                           ByVal P_TITLE As Decimal,
                                           ByVal P_DATE As Date,
                                           ByVal P_ID_KN As Decimal,
                                           ByVal P_STAFF_RANK_ID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.INSERT_EMPLOYEE_KN
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.INSERT_EMPLOYEE_KN(P_EMPLOYEE_CODE, P_ORG_ID, P_TITLE, P_DATE, P_ID_KN, P_STAFF_RANK_ID)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UPDATE_EMPLOYEE_KN(ByVal P_ID_KN As Decimal,
                                       ByVal P_DATE As Date) As Boolean Implements ServiceContracts.IProfileBusiness.UPDATE_EMPLOYEE_KN
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.UPDATE_EMPLOYEE_KN(P_ID_KN, P_DATE)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetHU_OrgTreeview(ByVal isBlank As Boolean, ByVal userName As String, Optional ByVal allLevel As Decimal = 0) As DataTable _
         Implements ServiceContracts.IProfileBusiness.GetHU_OrgTreeview
            Using rep As New ProfileRepository
                Try

                    Return rep.GetHU_OrgTreeview(isBlank, userName, allLevel)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "quan ly file"

        Public Function GetFoldersAll() As List(Of FolderDTO) Implements ServiceContracts.IProfileBusiness.GetFoldersAll
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetFoldersAll()
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFoldersStructureInfo(ByVal _folderId As Decimal) As List(Of FolderDTO) Implements ServiceContracts.IProfileBusiness.GetFoldersStructureInfo
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetFoldersStructureInfo(_folderId)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function AddFolder(ByVal _folder As FolderDTO) As Integer Implements ServiceContracts.IProfileBusiness.AddFolder
            Using rep As New ProfileRepository
                Try
                    Dim oldlink As String = ""
                    If _folder.ID <> 0 Then
                        Dim oldFolder = rep.GetFolderByID(_folder.ID)
                        oldlink = oldFolder.LINK
                    End If
                    Dim lst = rep.AddFolder(_folder)
                    If lst = 0 Then
                        Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & _folder.LINK
                        Dim oldPath = AppDomain.CurrentDomain.BaseDirectory & oldlink
                        If _folder.ID <> 0 Then
                            If Not Directory.Exists(oldPath) Then
                                Directory.CreateDirectory(rootPath)
                            Else
                                FileIO.FileSystem.RenameDirectory(oldPath, _folder.FOLDER_NAME)
                            End If
                        Else
                            If Not Directory.Exists(rootPath) Then
                                Directory.CreateDirectory(rootPath)
                            End If
                        End If
                    End If
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteFolder(ByVal _id As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteFolder
            Using rep As New ProfileRepository
                Try
                    Dim oldFolder = rep.GetFolderByID(_id)
                    Dim oldLink = oldFolder.LINK
                    Dim lst = rep.DeleteFolder(_id)
                    If lst Then
                        Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & oldLink
                        If Directory.Exists(rootPath) Then
                            Directory.Delete(rootPath, True)
                        End If
                    End If
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFileOfFolder(ByVal _filter As UserFileDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal log As UserLog = Nothing,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc"
                                    ) As List(Of UserFileDTO) Implements ServiceContracts.IProfileBusiness.GetFileOfFolder
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetFileOfFolder(_filter, PageIndex, PageSize, Total, log, Sorts)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetFolderByID(ByVal _id As Decimal) As FolderDTO Implements ServiceContracts.IProfileBusiness.GetFolderByID
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetFolderByID(_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function AddUserFile(ByVal _userFile As UserFileDTO, ByVal _imageBinary As Byte()) As Decimal Implements ServiceContracts.IProfileBusiness.AddUserFile
            Using rep As New ProfileRepository
                Try
                    Dim folder = rep.GetFolderByID(_userFile.FOLDER_ID)
                    _userFile.LINK = folder.LINK
                    Dim lst = rep.AddUserFile(_userFile)
                    If lst <> 1 AndAlso _imageBinary IsNot Nothing Then
                        Dim savepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _userFile.LINK)
                        If Not Directory.Exists(savepath) Then
                            Directory.CreateDirectory(savepath)
                        End If

                        Dim ms As New MemoryStream(_imageBinary)
                        ' đọc lại từ base lưu sang file vật lý
                        ' mệt vl
                        Dim fs As New FileStream(savepath & "\" & _userFile.FILE_NAME, FileMode.Create)
                        ms.WriteTo(fs)

                        ' clean up
                        ms.Close()
                        fs.Close()
                        fs.Dispose()
                    End If
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteUserFile(ByVal _id As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.DeleteUserFile
            Using rep As New ProfileRepository
                Try
                    Dim file = rep.GetUserFileByID(_id)
                    Dim lst = rep.DeleteUserFile(_id)
                    If lst Then
                        Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & file.LINK & "\" & file.FILE_NAME
                        If My.Computer.FileSystem.FileExists(rootPath) Then
                            My.Computer.FileSystem.DeleteFile(rootPath)
                        End If
                    End If
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserFileByID(ByVal _id As Decimal) As UserFileDTO Implements ServiceContracts.IProfileBusiness.GetUserFileByID
            Using rep As New ProfileRepository
                Try
                    Dim lst = rep.GetUserFileByID(_id)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetUserFileByte(ByVal _id As Decimal) As Byte() Implements ServiceContracts.IProfileBusiness.GetUserFileByte
            Using rep As New ProfileRepository
                Try
                    Dim UserFile = rep.GetUserFileByID(_id)
                    Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & UserFile.LINK & "\" & UserFile.FILE_NAME

                    Dim bytes = System.IO.File.ReadAllBytes(rootPath)
                    Return bytes
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

        Public Function CheckExistApproval(ByVal EmpID As Decimal, ByVal Table As String, ByVal Fk_Pkey As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.CheckExistApproval
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckExistApproval(EmpID, Table, Fk_Pkey)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
    End Class
End Namespace
