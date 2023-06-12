Imports System.Reflection
Imports System.Web
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports Framework.Data.SystemConfig

Public Class ProfileRepository
    Inherits ProfileRepositoryBase

    Public Function GetOrgsTree() As List(Of OrganizationDTO)
        Return (From p In Context.HU_ORGANIZATION Select New OrganizationDTO With {.ID = p.ID, .NAME_VN = p.NAME_VN, .PARENT_ID = p.PARENT_ID}).ToList
    End Function

#Region "VALIDATE BUSINESS"
    Public Function ValidateBusiness(ByVal Table_Name As String, ByVal Column_Name As String, ByVal ListID As List(Of Decimal)) As Boolean
        Dim IsExists As Boolean
        Dim strListID As String
        Try
            strListID = ListID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
            IsExists = Execute_ExistInDatabase(Table_Name, Column_Name, strListID)
        Catch ex As Exception

        End Try
        Return IsExists
    End Function
    Public Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Return CHECK_ExistInDatabase(table, column, strListID)
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Lấy data danh mục Combo"

    Public Function GetEmployeeGroup() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_EMPLOYEE_GROUP",
                                           New With {.P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OTHER_LIST",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GET_DEFAULT_OBJECT_ATTENDANCE() As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_DEFAULT_OBJECT_ATTENDANCE",
                                           New With {.P_CUR = cls.OUT_CURSOR})

                Return dtData(0)("P_OUT")
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetInsListWhereHealth() As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim objS As DataTable = cls.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_WHEREHEALTH_IS_NULL",
                                                          New With {.P_CUR = cls.OUT_CURSOR})

                Return objS
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetINS_Contract_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtResult As DataTable = cls.ExecuteStore("PKG_INS_LIST.GET_INS_LIST_CONTRACT", New With {.P_LOADFULL = If(Is_Full, "1", "0"),
                                                                                                           .P_CUR = cls.OUT_CURSOR})
                Return dtResult
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ByVal id As Decimal?) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtResult As Object = cls.ExecuteStore("PKG_INS_LIST.GetINS_LIST_CONTRACT_DETAIL_BY_ID",
                                                 New With {.P_ID = IIf(id Is Nothing, System.DBNull.Value, id),
                                                 .P_CUR = cls.OUT_CURSOR})
                Return dtResult
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.Get_HU_WORK_PLACE",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal, ByVal sLang As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.HU_PAPER_LIST",
                                           New With {.P_EMP_ID = P_EMP_ID,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetBankList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_BANK",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetBankBranchList(ByVal bankID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_BANK_BRANCH",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_BANK_ID = bankID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    'Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
    '    Try
    '        Using cls As New DataAccess.QueryData
    '            Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE_BYORG",
    '                                       New With {.P_ORGID = orgID,
    '                                                 .P_ISBLANK = isBlank,
    '                                                 .P_LANG = sLang,
    '                                                 .P_CUR = cls.OUT_CURSOR})

    '            Return dtData
    '        End Using
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
    '        Throw ex
    '    End Try
    'End Function
    Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean, ByVal Employee_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE_BYORG",
                                           New With {.P_ORGID = orgID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_EMPLOYEEID = Employee_ID,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_WARD",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DISTRICT_ID = districtID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DISTRICT",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_PROVINCE_ID = provinceID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvinceList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE_1",
                                           New With {.P_NATIVE = P_NATIVE,
                                                     .P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetProvinceList2(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE_2",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetNationList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_NATION",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetStaffRankList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_STAFF_RANK",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GET_INS_HEALTH_BY_EMPID(ByVal emp_ID As Decimal?) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_INS_HEALTH_BY_EMPID",
                                           New With {.P_EMP_ID = emp_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetSalaryTypeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GET_Hu_Allowance_List(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_Allowance_List",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SALE_COMMISION",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP_ID = salGroupID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_RANK",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_LEVEL_ID = salLevelID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_AllowanceList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_ALLOWANCE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetPA_ObjectSalary(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_OBJ_SALARY",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOT_WageTypeList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OT_WAGE_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOT_MissionTypeList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OT_MISSION_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOT_TripartiteTypeList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OT_TRIPARTITE_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_TEMPLATE_TYPE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                          ByVal isTemplateType As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_MERGE_FIELD",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_TEMPLATE_TYPE_ID = isTemplateType,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                          ByVal isTemplateType As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_TEMPLATE",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_TEMPLATE_TYPE_ID = isTemplateType,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHU_DataDynamic_Muti_1(ByVal dID As String,
                                             ByVal de As String,
                                      ByVal tempID As Decimal,
                                      ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = dID,
                                    .P_EMP = de,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DATA_DYNAMIC_MUTI_1", obj)
                folderName = dtData(0)("FOLDERNAME")
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHU_DataDynamic_Muti(ByVal dID As String,
                                      ByVal tempID As Decimal,
                                      ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = dID,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DATA_DYNAMIC_MUTI", obj)
                folderName = dtData(0)("FOLDERNAME")
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = dID,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DATA_DYNAMIC", obj)
                folderName = dtData(0)("FOLDERNAME")
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = strID,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_FOLDERNAME = cls.OUT_STRING,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_Multy_DATA_DYNAMIC", obj)
                folderName = obj.P_FOLDERNAME
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_DataDynamicContract(ByVal dID As String,
                                   ByVal tempID As Decimal,
                                   ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = dID,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_FOLDERNAME = cls.OUT_STRING,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DATA_DYNAMIC_CONTRACT", obj)
                folderName = obj.P_FOLDERNAME
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                                   ByVal tempID As Decimal,
                                   ByRef folderName As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_ID = dID,
                                    .P_TEMPLATE_TYPE_ID = tempID,
                                    .P_FOLDERNAME = cls.OUT_STRING,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_DATA_DYNAMIC_CONTRACT", obj)
                folderName = obj.P_FOLDERNAME
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO), ByVal log As UserLog) As Boolean
        Try
            For Each obj In lstData
                If obj.ID <> 0 Then
                    Dim objData = (From p In Context.HU_MERGE_FIELD
                                   Where p.ID = obj.ID).FirstOrDefault
                    objData.NAME = obj.NAME
                Else
                    Dim objData As New HU_MERGE_FIELD
                    objData.ID = Utilities.GetNextSequence(Context, Context.HU_MERGE_FIELD.EntitySet.Name)
                    objData.HU_TEMPLATE_TYPE_ID = obj.HU_TEMPLATE_TYPE_ID
                    objData.CODE = obj.CODE
                    objData.NAME = obj.NAME
                    Context.HU_MERGE_FIELD.AddObject(objData)
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetInsRegionList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_INS_REGION",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_CompetencyGroupList(ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_COMPETENCY_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_CompetencyList(ByVal groupID As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_COMPETENCY",
                                           New With {.P_GROUP_ID = groupID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetHU_CompetencyPeriodList(ByVal year As Decimal, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_COMPETENCY_PERIOD",
                                           New With {.P_YEAR = year,
                                                     .P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Get Combobox Data"

    ''' <summary>
    ''' Lấy dữ liệu cho combobox
    ''' </summary>
    ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
    ''' <returns>TRUE: Success</returns>
    ''' <remarks></remarksGET_TER_STATUS
    ''' 
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO, Optional log As UserLog = Nothing) As Boolean
        Dim query
        'trinh do chuyen mon
        If _combolistDTO.GET_TDCM Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "MAJOR")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TDCM = query
        End If
        'truong dao tao
        If _combolistDTO.GET_TDT Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "HU_GRADUATE_SCHOOL")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TDT = query
        End If

        'cap ki luat
        If _combolistDTO.GET_LEVEL_DISCIPLINE Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "DISCIPLINE_LEVEL")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEVEL_DISCIPLINE = query
        End If
        'DIA DIEM KY HOP DONG HU_LOCATION
        If _combolistDTO.GET_LOCATION Then

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = 0})
            End Using

            query = (From p In Context.HU_LOCATION Where p.ACTFLG = "A" AndAlso p.CODE IsNot Nothing
                     From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From t In Context.OT_OTHER_LIST.Where(Function(t) t.ID = p.REGION).DefaultIfEmpty
                     From se In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = o.ID And f.USERNAME = log.Username)
                     Select New LocationDTO With {
                        .ID = p.ID,
                        .LOCATION_VN_NAME = o.SHORT_NAME + " - " + p.NAME_VN,
                        .LOCATION_EN_NAME = p.LOCATION_EN_NAME,
                        .REGION = p.REGION,
                        .REGION_NAME = t.NAME_VN,
                        .CODE = p.CODE}).ToList
            _combolistDTO.LIST_LOCATION = query
        End If
        'NHOM CHUC DANH
        If _combolistDTO.GET_TITLE_GROUP Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "HU_TITLE_GROUP")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TITLE_GROUP = query
        End If
        'TRINH DO
        If _combolistDTO.GET_LEVEL_TRAIN Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "LEARNING_LEVEL")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEVEL_TRAIN = query
        End If
        'CHUYEN NGANH
        If _combolistDTO.GET_MAJOR_TRAIN Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "MAJOR")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MAJOR_TRAIN = query
        End If
        'LINH VUC DAO TAO
        If _combolistDTO.GET_FIELD_TRAIN Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "TR_TRAIN_FIELD")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_FIELD_TRAIN = query
        End If

        If _combolistDTO.GET_UNIT_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "UNIT_LEVEL")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_UNIT_LEVEL = query
        End If

        If _combolistDTO.GET_HANDOVER_CONTENT Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "HANDOVER_CONTENT")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_HANDOVER_CONTENT = query
        End If

        If _combolistDTO.GET_REASON Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "REASON")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_REASON = query
        End If
        If _combolistDTO.GET_TYPE_WORK Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "TYPE_WORK")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TYPE_WORK = query
        End If
        If _combolistDTO.GET_RANK Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "RANK")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_RANK = query
        End If
        If _combolistDTO.GET_CAPACITY Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "CAPACITY")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME_VN,
                        .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_CAPACITY = query
        End If

        If _combolistDTO.GET_EVALUATE Then
            query = (From p In Context.PE_PERIOD Where p.ACTFLG = "A"
                     From t In Context.OT_OTHER_LIST.Where(Function(t) t.ID = p.TYPE_ASS)
                     Order By p.NAME
                     Select New OtherListDTO With {
                        .ID = p.ID,
                        .NAME_VN = p.NAME,
                        .YEAR = p.YEAR}).ToList
            _combolistDTO.LIST_EVALUATE = query
        End If
        'Loại NGHĨ
        'list ALLOWANCE
        If _combolistDTO.GET_WORKING_ALLOWANCE_LIST Then
            query = (From p In Context.HU_ALLOWANCE_LIST Where p.ACTFLG = "A"
                     Order By p.ORDERS
                     Select New AllowanceListDTO With {
                         .ID = p.ID,
                         .NAME = p.NAME,
                         .IS_DEDUCT = p.IS_DEDUCT
                         }).ToList
            _combolistDTO.LIST_WORKING_ALLOWANCE_LIST = query
        End If

        'Loại bảo hộ lao động
        If _combolistDTO.GET_LABOURPROTECTION Then
            query = (From p In Context.HU_LABOURPROTECTION Where p.ACTFLG = "A" Order By p.NAME
                     Select New LabourProtectionDTO With {
                         .ID = p.ID,
                         .NAME = p.NAME,
                         .UNIT_PRICE = p.UNIT_PRICE,
                         .SDESC = p.SDESC}).ToList
            _combolistDTO.LIST_LABOURPROTECTION = query
        End If
        'Size của loại bảo hộ lao động
        If _combolistDTO.GET_SIZE_LABOURPROTECTION Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A"
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "SIZE_LABOUR")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_SIZE_LABOURPROTECTION = query
        End If

        'Giới tính
        If _combolistDTO.GET_GENDER Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "GENDER")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_GENDER = query
        End If

        If _combolistDTO.GET_WORK_STATUS Then
            query = (From p In Context.OT_OTHER_LIST Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID And t.CODE = "WORKSTATUS")
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_WORK_STATUS = query
        End If

        If _combolistDTO.GET_TITLE Then
            query = (From p In Context.HU_TITLE Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     Order By p.NAME_VN
                     Select New TitleDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TITLE = query
        End If

        If _combolistDTO.GET_TITLE Then
            query = (From p In Context.HU_TITLE Where p.ACTFLG = "A" Order By p.NAME_VN.ToUpper
                     Order By p.NAME_VN
                     Select New TitleDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TITLE = query
        End If

        'WorkDescription
        If _combolistDTO.GET_WORK_DESCRIPTION Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "WORK_DESCRIPTION" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_WORK_DESCRIPTION = query
        End If

        'Ngân hàng
        If _combolistDTO.GET_BANK Then
            query = (From p In Context.HU_BANK Where p.ACTFLG = "A"
                     Order By p.NAME.ToUpper
                     Select New BankDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME = p.NAME}).ToList
            _combolistDTO.LIST_BANK = query
        End If
        'Ngân hàng hiển thị combobox có Name = Code + Name
        If _combolistDTO.GET_BANK_CODE Then
            query = (From p In Context.HU_BANK Where p.ACTFLG = "A"
                     Order By p.CODE
                     Select New BankDTO With {
                         .ID = p.ID,
                         .NAME = p.CODE & " - " & p.NAME}).ToList
            _combolistDTO.LIST_BANK_CODE = query
        End If

        'Lý do tuyển dụng
        If _combolistDTO.GET_RECRUIMENT_REASON Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "RECRUITMENT_REASON" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_RECRUIMENT_REASON = query
        End If

        'Loại sức khỏe
        If _combolistDTO.GET_HEALTH_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "HEALTH_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_HEALTH_TYPE = query
        End If

        If _combolistDTO.GET_DISTRICT Then
            query = (From p In Context.HU_DISTRICT
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New DistrictDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .PROVINCE_ID = p.PROVINCE_ID}).ToList
            _combolistDTO.LIST_DISTRICT = query
        End If
        If _combolistDTO.GET_NATION Then
            query = (From p In Context.HU_NATION
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New NationDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_NATION = query
        End If
        If _combolistDTO.GET_PROVINCE Then
            query = (From p In Context.HU_PROVINCE
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New ProvinceDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NATION_ID = p.NATION_ID,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_PROVINCE = query
        End If

        If _combolistDTO.GET_WARD Then
            query = (From p In Context.HU_WARD
                     Where p.ACTFLG = "A"
                     Order By p.NAME_VN.ToUpper
                     Select New Ward_DTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_WARD = query
        End If


        ' Danh mục phụ cấp chi tiết
        If _combolistDTO.GET_ALLOWANCE_DETAIL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ALLOWANCE_DETAIL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN}).ToList
            _combolistDTO.LIST_ALLOWANCE_DETAIL = query
        End If

        'Danh mục nhóm tài sản
        If _combolistDTO.GET_ASSET_GROUP Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ASSET_GROUP" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN}).ToList
            _combolistDTO.LIST_ASSET_GROUP = query
        End If

        'Danh mục cấp đơn vị
        If _combolistDTO.GET_ORG_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ORG_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.CODE
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.CODE + " - " + p.NAME_EN,
                         .NAME_VN = p.CODE + " - " + p.NAME_VN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_ORG_LEVEL = query
        End If

        'Danh mục tình trạng tài sản
        If _combolistDTO.GET_ASSET_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "ASSET_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN}).ToList
            _combolistDTO.LIST_ASSET_STATUS = query
        End If

        'Danh mục tài sản
        If _combolistDTO.GET_ASSET Then
            query = (From p In Context.HU_ASSET
                     Where p.ACTFLG = "A"
                     Order By p.NAME.ToUpper
                     Select New AssetDTO With {
                         .ID = p.ID,
                         .NAME = p.NAME,
                         .GROUP_ID = p.OT_OTHER_LIST.ID}).ToList
            _combolistDTO.LIST_ASSET = query
        End If

        'Danh mục đơn vị tính
        If _combolistDTO.GET_UNIT Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "UNIT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN}).ToList
            _combolistDTO.LIST_UNIT = query
        End If

        'Danh mục loại hợp đồng
        If _combolistDTO.GET_CONTRACTTYPE_POPUP Then
            query = (From p In Context.HU_CONTRACT_TYPE
                     From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                     Where p.ACTFLG = "A" And p.TYPE_CODE Is Nothing And p.IS_REQUIREMENT = -1
                     Order By p.NAME
                     Select New ContractTypeDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME = p.NAME,
                         .TYPE_CODE = ot.CODE,
                         .IS_HOCVIEC = p.IS_HOCVIEC,
                         .IS_HSL = p.IS_HSL,
                         .PERIOD = p.PERIOD}).ToList
            _combolistDTO.LIST_CONTRACTTYPE_POPUP = query
        End If
        'Danh mục loại hợp đồng
        If _combolistDTO.GET_CONTRACTTYPE Then
            query = (From p In Context.HU_CONTRACT_TYPE
                     From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                     Where p.ACTFLG = "A" And p.TYPE_CODE Is Nothing
                     Order By p.NAME
                     Select New ContractTypeDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME = p.NAME,
                         .TYPE_CODE = ot.CODE,
                         .IS_HOCVIEC = p.IS_HOCVIEC,
                         .IS_HSL = p.IS_HSL,
                         .PERIOD = p.PERIOD}).ToList
            _combolistDTO.LIST_CONTRACTTYPE = query
        End If

        'Trạng thái hợp đồng
        If _combolistDTO.GET_CONTRACT_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = ProfileCommon.OT_CONTRACT_STATUS.Name _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                                    .ID = p.ID,
                                    .NAME_EN = p.NAME_EN,
                                    .NAME_VN = p.NAME_VN,
                                    .CODE = p.CODE}).ToList
            _combolistDTO.LIST_CONTRACT_STATUS = query
        End If

        'Danh mục phúc lợi
        If _combolistDTO.GET_WELFARE Then
            Dim welfare = (From l In Context.HU_WELFARE_LIST
                           Where l.ACTFLG = "A" _
                           And l.IS_AUTO = 0 _
                           And l.START_DATE <= Date.Now And (l.END_DATE >= Date.Now Or l.END_DATE Is Nothing)
                           Select New WelfareListDTO With {
                                .NAME = l.NAME,
                                .ID = l.ID,
                                .MONEY = l.MONEY}).ToList
            _combolistDTO.LIST_WELFARE = welfare
        End If

        If _combolistDTO.GET_WELFARE_AUTO Then
            Dim dateEndCompare = New Date(Date.Now.Year, 1, 1)
            Dim dateStartCompare = New Date(Date.Now.Year, 12, 31)
            Dim welfare = (From l In Context.HU_WELFARE_LIST
                           Where l.ACTFLG = "A" _
                           And l.IS_AUTO = -1 _
                           And l.START_DATE <= dateStartCompare And (l.END_DATE >= dateEndCompare Or l.END_DATE Is Nothing)
                           Select New WelfareListDTO With {
                               .ID = l.ID,
                               .NAME = l.NAME,
                               .CODE = l.CODE,
                               .CONTRACT_TYPE = l.CONTRACT_TYPE,
                               .CONTRACT_TYPE_NAME = l.CONTRACT_TYPE_NAME,
                               .GENDER = l.GENDER,
                               .GENDER_NAME = l.GENDER_NAME,
                               .SENIORITY = l.SENIORITY,
                               .CHILD_OLD_FROM = l.CHILD_OLD_FROM,
                               .CHILD_OLD_TO = l.CHILD_OLD_TO,
                               .MONEY = l.MONEY,
                               .START_DATE = l.START_DATE,
                               .END_DATE = l.END_DATE,
                               .IS_AUTO = l.IS_AUTO
                                }).ToList
            _combolistDTO.LIST_WELFARE_AUTO = welfare
        End If


        'Danh mục trình độ văn hóa( Danh mục trường Đào tạo, vd: ĐH Quốc Gia,...
        If _combolistDTO.GET_ACADEMY Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "ACADEMY" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_ACADEMY = query
        End If

        'Danh mục ngành đào tạo:(Chuyên môn: Kỹ sư CNTT,...)
        If _combolistDTO.GET_MAJOR Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "MAJOR" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MAJOR = query
        End If

        'Danh mục bằng cấp: Đại học, Cao Đẳng, Kỹ sư,...
        If _combolistDTO.GET_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEVEL = query
        End If
        'Danh mục trình độ tin học: 
        If _combolistDTO.GET_COMPUTER_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMPUTER_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMPUTER_LEVEL = query
        End If
        'Danh mục ngoại ngữ :  
        If _combolistDTO.GET_LANGUAGE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LANGUAGE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LANGUAGE = query
        End If
        'Danh mục hình thức đào tạo :  
        If _combolistDTO.GET_TRAINING_FORM Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TRAINING_FORM" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TRAINING_FORM = query
        End If
        'loai bang cap/chung chi
        If _combolistDTO.GET_CERTIFICATE_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "CERTIFICATE_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_CERTIFICATE_TYPE = query
        End If

        If _combolistDTO.GET_CERTIFICATE_TYPE_2 Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "CERTIFICATE_TYPE_2" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_CERTIFICATE_TYPE_2 = query
        End If

        'Danh mục loại hình đào tạo :  
        If _combolistDTO.GET_TRAINING_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TRAINING_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TRAINING_TYPE = query
        End If
        'Danh mục trình độ học vấn :  
        If _combolistDTO.GET_LEARNING_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LEARNING_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LEARNING_LEVEL = query
        End If
        'Danh mục xếp loại học vấn:  
        If _combolistDTO.GET_MARK_EDU Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "MARK_EDU" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_MARK_EDU = query
        End If
        'Danh mục trình độ ngoại ngữ :  
        If _combolistDTO.GET_LANGUAGE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "LANGUAGE_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_LANGUAGE_LEVEL = query
        End If

        'Danh mục loại quyết định
        If _combolistDTO.GET_DECISION_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DECISION_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_DECISION_TYPE = query
        End If

        'Danh mục đối tượng kỷ luật
        If _combolistDTO.GET_DISCIPLINE_OBJ Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_OBJECT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_OBJ = query
        End If
        'Danh mục chế độ sữa 
        If _combolistDTO.GET_MILK_MODE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "MILK_MODE" _
                     And p.ACTFLG = "A" Order By p.NAME_VN Descending
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_EN = p.NAME_EN,
                         .NAME_VN = p.NAME_VN}).ToList
            _combolistDTO.LIST_MILK_MODE = query
        End If
        'Danh mục cấp kỷ luật
        If _combolistDTO.GET_DISCIPLINE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_LEVEL = query
        End If

        'Danh mục hình thức kỷ luật
        If _combolistDTO.GET_DISCIPLINE_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .ATTRIBUTE1 = p.ATTRIBUTE1,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_TYPE = query
        End If

        'Danh mục lý do kỷ luật
        If _combolistDTO.GET_DISCIPLINE_REASON Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DISCIPLINE_REASON" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DISCIPLINE_REASON = query
        End If

        'Danh mục đối tượng khen thưởng
        If _combolistDTO.GET_COMMEND_OBJ Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_OBJECT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_OBJ = query
        End If

        'Danh mục cấp khen thưởng
        If _combolistDTO.GET_COMMEND_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_LEVEL" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_LEVEL = query
        End If
        'Danh mucj loai khen thuong
        If _combolistDTO.GET_COMMEND_LIST Then
            query = (From p In Context.HU_COMMEND_LIST
                     Where p.ACTFLG = "A"
                     Order By p.NAME
                     Select New CommendListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME = p.NAME
                         }).ToList
            _combolistDTO.LIST_COMMEND_LIST = query
        End If

        'Danh mục hình thức khen thưởng
        If _combolistDTO.GET_COMMEND_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "COMMEND_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_COMMEND_TYPE = query
        End If

        'Danh mục hình thức chấm dứt hợp đồng
        If _combolistDTO.GET_TER_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TER_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TER_TYPE = query
        End If
        'Loại Nghỉ
        If _combolistDTO.GET_TYPE_NGHI Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TERMINATE_PRINT" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN
                         }).ToList
            _combolistDTO.LIST_TYPE_NGHI = query
        End If
        'Trang thai cong no
        If _combolistDTO.GET_DEBT_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DEBT_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DEBT_STATUS = query
        End If
        'Danh muc loai cong no
        If _combolistDTO.GET_DEBT_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "DEBT_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_DEBT_TYPE = query
        End If
        'Danh mục lý do nghỉ việc
        If _combolistDTO.GET_TER_REASON Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "TER_REASON" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN}).ToList
            _combolistDTO.LIST_TER_REASON = query
        End If

        'Danh mục trạng thái nghỉ việc
        If _combolistDTO.GET_TER_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = ProfileCommon.OT_TER_STATUS.Name _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_TER_STATUS = query
        End If
        'Danh mục trạng thái khen thưởng
        If _combolistDTO.GET_COMMEND_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = ProfileCommon.COMMEND_STATUS.Name _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_COMMEND_STATUS = query
        End If
        'Danh muc trang thai so
        If _combolistDTO.GET_TYPE_INS_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "INS_STATUS" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_INS_STATUS = query
        End If
        'Danh mục trạng thái nghỉ việc
        If _combolistDTO.GET_DISCIPLINE_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = ProfileCommon.OT_DISCIPLINE_STATUS.Name _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_DISCIPLINE_STATUS = query
        End If
        'Danh mục mối quan hệ
        If _combolistDTO.GET_RELATION Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.TYPE_ID = 48 And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New RelationshipListDTO With {
                         .ID = p.ID,
                         .NAME = p.NAME_VN}).ToList
            _combolistDTO.LIST_RELATION = query
        End If
        'TransferType = New_Hire -- Dùng khi thêm mới quyết định khi thêm nhân viên trong hồ sơ.
        If _combolistDTO.GET_TRANSFER_TYPE_NEW_HIRE Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.CODE = "NEW_HIRE" _
                     And p.ACTFLG = "A" _
                    And p.OT_OTHER_LIST_TYPE.CODE = "TRANSFER_TYPE"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).SingleOrDefault
            _combolistDTO.LIST_TRANSFER_TYPE_NEW_HIRE = query
        End If

        'TRANSFER_STATUS = APPROVE -- Dùng khi thêm mới quyết định khi thêm nhân viên trong hồ sơ.
        If _combolistDTO.GET_TRANSFER_STATUS_APPROVE Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.CODE = "1" _
                     And p.ACTFLG = "A" _
                    And p.OT_OTHER_LIST_TYPE.CODE = "TRANSFER_STATUS"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .CODE = p.CODE,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).SingleOrDefault
            _combolistDTO.LIST_TRANSFER_STATUS_APPROVE = query
        End If

        'Danh mục căn cứ quyết định
        If _combolistDTO.GET_DECISION_REMARK Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "HU_CCQD" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                          .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_DECISION_REMARK = query
        End If

        'Danh mục căn cứ quyết định
        If _combolistDTO.GET_ATTATCH_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = "ATTATCH_TYPE" _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_ATTATCH_TYPE = query
        End If

        'Danh mục căn cứ quyết định
        If _combolistDTO.GET_WORK_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.ACTFLG = "A" And p.TYPE_ID = ProfileCommon.OT_WORK_STATUS.TYPE_ID
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_WORK_STATUS = query
        End If

        'Danh mục căn cứ quyết định
        If _combolistDTO.GET_TITLE_LEVEL Then
            query = (From p In Context.OT_OTHER_LIST
                     Where p.ACTFLG = "A" And p.TYPE_ID = 160
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .ATTRIBUTE1 = p.ATTRIBUTE1}).ToList
            _combolistDTO.LIST_TITLE_LEVEL = query
        End If
        'Trạng thái quyết định
        If _combolistDTO.GET_DECISION_STATUS Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID Where q.CODE = ProfileCommon.DECISION_STATUS.Name _
                     And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_DECISION_STATUS = query
        End If

        'Loại quyết định của Nghỉ việc
        If _combolistDTO.GET_TER_DECISION_TYPE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "TERMINATE_PRINT" And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_TER_DECISION_TYPE = query
        End If

        ' moi quan he
        If _combolistDTO.GET_RELATE_OWNER Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "RELATION" And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_RELATE_OWNER = query
        End If
        'dan toc
        If _combolistDTO.GET_NATIVE Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "NATIVE" And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_NATIVE = query
        End If
        'vung mien
        If _combolistDTO.GET_ORG_REG Then
            query = (From p In Context.OT_OTHER_LIST
                     Join q In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals q.ID
                     Where q.CODE = "REGION_CH" And p.ACTFLG = "A"
                     Order By p.NAME_VN
                     Select New OtherListDTO With {
                         .ID = p.ID,
                         .NAME_VN = p.NAME_VN,
                         .NAME_EN = p.NAME_EN,
                         .CODE = p.CODE}).ToList
            _combolistDTO.LIST_ORG_REG = query
        End If
        Return True
    End Function

#End Region
#Region "Validate Combobox"
    Public Function ValidateSelectedComboList(ByRef _combolistDTO As ComboBoxDataDTO, ByVal _validate As Object) As Boolean

    End Function
#End Region
#Region "Service Send Mail Reminder"

    Public Function GetConfig(ByVal eModule As ModuleID) As Dictionary(Of String, String)
        Using config As New SystemConfig
            Try
                Return config.GetConfig(eModule)
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Using
    End Function


    'Public Function CheckAndSendMailReminder() As Boolean
    '    Using cfig As New SystemConfig
    '        Using rep As New ProfileDashboardRepository
    '            Dim defaultFrom As String = ""
    '            Dim configreminder As Dictionary(Of String, Dictionary(Of Integer, String))
    '            Try
    '                configreminder = cfig.GetReminderConfig()
    '                Dim config As Dictionary(Of String, String)
    '                config = GetConfig(ModuleID.All)
    '                defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

    '                If defaultFrom = "" Then
    '                    Return False
    '                End If
    '                Dim iCount As Integer
    '                For Each item In configreminder
    '                    iCount = 0
    '                    Dim toMail As String = (From p In Context.SE_USER
    '                                            Where p.USERNAME = item.Key
    '                                            Select p.EMAIL).FirstOrDefault

    '                    'Dim birthdayRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Birthday), _
    '                    '                                   CInt(item.Value(SystemConfig.RemindConfigType.Birthday)), 0)
    '                    'Dim contractRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Contract), _
    '                    '                                   CInt(item.Value(SystemConfig.RemindConfigType.Contract)), 0)
    '                    'Dim probationRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Probation), _
    '                    '                                   CInt(item.Value(SystemConfig.RemindConfigType.Probation)), 0)
    '                    'Dim retireRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Retire), _
    '                    '                                   CInt(item.Value(SystemConfig.RemindConfigType.Retire)), 0)
    '                    'Dim visaRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Visa), _
    '                    '                                   CInt(item.Value(SystemConfig.RemindConfigType.Visa)), 0)
    '                    'Dim toMailConfig As String = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Email), _
    '                    '                                   item.Value(SystemConfig.RemindConfigType.Email), defaultFrom)
    '                    'If toMailConfig IsNot Nothing AndAlso Not toMailConfig.ToUpper.Contains(toMail.ToUpper) Then
    '                    '    toMailConfig &= ";" & toMail
    '                    'End If
    '                    'Dim lst = rep.GetRemind(contractRemind.ToString & "," & _
    '                    '                       birthdayRemind.ToString & "," & _
    '                    '                       probationRemind.ToString & "," & _
    '                    '                       retireRemind.ToString & "," & _
    '                    '                       visaRemind.ToString,
    '                    '                       New UserLog With {.Username = item.Key})

    '                    'Dim lstReminder = (From p In Context.HU_REMINDER_LOG
    '                    '                   Where p.USERNAME.ToUpper = item.Key.ToUpper
    '                    '                    Select New ReminderLogDTO With {
    '                    '                        .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
    '                    '                        .USERNAME = p.USERNAME,
    '                    '                        .REMIND_DATE = p.REMINDER_DATE,
    '                    '                        .REMIND_TYPE = p.REMINDER_TYPE}).ToList


    '                    'Dim bCheck As Boolean = False
    '                    'Dim sContent As String = "Thư nhắc nhở: <br />"
    '                    'For i = 0 To lst.Count - 1
    '                    '    Dim check = (From p In lstReminder
    '                    '        Where p.EMPLOYEE_CODE = lst(i).EMPLOYEE_CODE And
    '                    '            p.REMIND_DATE.Value.ToShortDateString =
    '                    '                lst(i).REMIND_DATE.Value.ToShortDateString And
    '                    '            p.REMIND_TYPE = lst(i).REMIND_TYPE And
    '                    '            p.USERNAME = item.Key
    '                    '        Select p.EMPLOYEE_CODE).Count
    '                    '    If check = 0 Then
    '                    '        bCheck = True
    '                    '        sContent &= (iCount + 1) & ". " & lst(i).EMPLOYEE_CODE & " - " & lst(i).FULLNAME & ": "
    '                    '        If lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Birthday Then
    '                    '            sContent &= "Ngày sinh nhật "
    '                    '        ElseIf lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Contract Then
    '                    '            sContent &= "Ngày hết hạn hợp đồng "
    '                    '        ElseIf lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Probation Then
    '                    '            sContent &= "Ngày hết hạn thử việc "
    '                    '        ElseIf lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Retire Then
    '                    '            sContent &= "Ngày đến tuổi về hưu "
    '                    '        ElseIf lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Visa Then
    '                    '            sContent &= "Ngày hết hạn visa, hộ chiếu "
    '                    '        End If
    '                    '        sContent &= lst(i).REMIND_DATE.Value.ToString("dd/MM/yyyy") & "<br />"
    '                    '        Dim _newReminder As New HU_REMINDER_LOG
    '                    '        _newReminder.ID = Utilities.GetNextSequence(Context, Context.HU_REMINDER_LOG.EntitySet.Name)
    '                    '        _newReminder.EMPLOYEE_CODE = lst(i).EMPLOYEE_CODE
    '                    '        _newReminder.USERNAME = item.Key
    '                    '        _newReminder.REMINDER_DATE = lst(i).REMIND_DATE
    '                    '        _newReminder.REMINDER_TYPE = lst(i).REMIND_TYPE
    '                    '        Context.HU_REMINDER_LOG.AddObject(_newReminder)
    '                    '    End If
    '                    'Next

    '                    'If bCheck And toMailConfig IsNot Nothing Then
    '                    '    Dim _newMail As New SE_MAIL
    '                    '    _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
    '                    '    _newMail.MAIL_FROM = defaultFrom
    '                    '    _newMail.MAIL_TO = toMailConfig
    '                    '    _newMail.MAIL_CC = ""
    '                    '    _newMail.MAIL_BCC = ""
    '                    '    _newMail.SUBJECT = "[Histaff-Reminder]"
    '                    '    _newMail.CONTENT = sContent
    '                    '    _newMail.VIEW_NAME = "REMINDER"
    '                    '    Context.SE_MAIL.AddObject(_newMail)

    '                    '    Context.SaveChanges()
    '                    'End If
    '                Next
    '                Return True
    '            Catch ex As Exception
    '                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
    '                Return False
    '            End Try

    '        End Using
    '    End Using
    'End Function

    ''' <summary>
    ''' LOIVT - gửi mail nhắc nhở hợp đồng đến hạn
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckAndSendMailReminder() As Boolean
        Using cfig As New SystemConfig
            Using rep As New ProfileDashboardRepository
                Dim defaultFrom As String = ""
                Dim configreminder As Dictionary(Of String, Dictionary(Of Integer, String))
                Try
                    configreminder = cfig.GetReminderConfig()
                    Dim config As Dictionary(Of String, String)
                    config = GetConfig(ModuleID.All)

                    ' bắt buộc phải thiết lập mail để gửi
                    defaultFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")

                    If defaultFrom = "" Then
                        Return False
                    End If
                    Dim iCount As Integer
                    For Each item In configreminder
                        iCount = 0
                        Dim toMail As String = (From p In Context.SE_USER
                                                Where p.USERNAME = item.Key
                                                Select p.EMAIL).FirstOrDefault

                        Dim birthdayRemind As Integer = 0
                        Dim contractRemind As Integer = If(item.Value.ContainsKey(SystemConfig.RemindConfigType.Contract), _
                                                           CInt(item.Value(SystemConfig.RemindConfigType.Contract)), 0)
                        Dim probationRemind As Integer = 0
                        Dim retireRemind As Integer = 0
                        Dim visaRemind As Integer = 0
                        Dim toMailConfig As String = ""

                        Dim lst = rep.GetRemind(contractRemind.ToString & "," & _
                                               birthdayRemind.ToString & "," & _
                                               probationRemind.ToString & "," & _
                                               retireRemind.ToString & "," & _
                                               visaRemind.ToString,
                                               New UserLog With {.Username = item.Key})

                        Dim lstReminder = (From p In Context.HU_REMINDER_LOG
                                           Where p.USERNAME.ToUpper = item.Key.ToUpper
                                           Select New ReminderLogDTO With {
                                               .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                               .USERNAME = p.USERNAME,
                                               .REMIND_DATE = p.REMINDER_DATE,
                                               .REMIND_TYPE = p.REMINDER_TYPE}).ToList

                        ' - Lấy thông tin của người quản lý
                        Dim directManagerInfo = (From p In Context.HU_EMPLOYEE
                                                 From c In Context.HU_EMPLOYEE_CV.Where(Function(F) F.EMPLOYEE_ID = p.ID)
                                                 From dir In Context.HU_EMPLOYEE.Where(Function(f) p.DIRECT_MANAGER = f.ID).DefaultIfEmpty
                                                 From dirc In Context.HU_EMPLOYEE_CV.Where(Function(f) dir.ID = f.EMPLOYEE_ID).DefaultIfEmpty
                                                 Select New EmployeeDTO With {
                                                 .ID = p.ID,
                                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                 .WORK_EMAIL = dirc.WORK_EMAIL}).ToList

                        Dim bCheck As Boolean = False
                        Dim sContent As String = "Thư nhắc nhở: <br />"

                        For i = 0 To lst.Count - 1
                            Dim check = (From p In lstReminder
                                         Where p.EMPLOYEE_CODE = lst(i).EMPLOYEE_CODE And
                                             p.REMIND_DATE.Value.ToShortDateString =
                                                 lst(i).REMIND_DATE.Value.ToShortDateString And
                                             p.REMIND_TYPE = lst(i).REMIND_TYPE And
                                             p.USERNAME = item.Key
                                         Select p.EMPLOYEE_CODE).Count

                            If check = 0 Then
                                bCheck = True
                                sContent = ""
                                sContent &= "Thư nhắc nhở: <br />"
                                sContent &= (iCount + 1) & ". " & lst(i).EMPLOYEE_CODE & " - " & lst(i).FULLNAME & ": "

                                If lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Contract Then
                                    sContent &= "Ngày hết hạn hợp đồng "
                                End If

                                sContent &= lst(i).REMIND_DATE.Value.ToString("dd/MM/yyyy") & "<br />"

                                Dim _newReminder As New HU_REMINDER_LOG
                                _newReminder.ID = Utilities.GetNextSequence(Context, Context.HU_REMINDER_LOG.EntitySet.Name)
                                _newReminder.EMPLOYEE_CODE = lst(i).EMPLOYEE_CODE
                                _newReminder.USERNAME = item.Key
                                _newReminder.REMINDER_DATE = lst(i).REMIND_DATE
                                _newReminder.REMINDER_TYPE = lst(i).REMIND_TYPE
                                Context.HU_REMINDER_LOG.AddObject(_newReminder)
                            End If

                            ' lấy mail người quản lý
                            toMailConfig = (From p In directManagerInfo
                                            Where p.ID = lst(i).EMPLOYEE_ID
                                            Select p.WORK_EMAIL).FirstOrDefault

                            If toMailConfig = "" Or toMailConfig Is Nothing Then
                                toMailConfig = toMail
                            End If

                            toMailConfig &= ";loivt@tinhvan.com;anhdtp@tinhvan.com"
                            '''' END '''''''

                            ' kiểm tra xem trong bảng gửi mail có bản ghi để gửi chưa
                            Dim checkSE_MAIL = (From p In Context.SE_MAIL
                                                Where p.MAIL_TO = toMailConfig And
                                                    p.SUBJECT = "[Histaff-Reminder]"
                                                Select p.ID).Count

                            If checkSE_MAIL = 0 And bCheck Then
                                Dim _newMail As New SE_MAIL
                                _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
                                _newMail.MAIL_FROM = defaultFrom
                                _newMail.MAIL_TO = toMailConfig
                                _newMail.MAIL_CC = ""
                                _newMail.MAIL_BCC = ""
                                _newMail.SUBJECT = "[Histaff-Reminder]"
                                _newMail.CONTENT = sContent
                                _newMail.VIEW_NAME = "REMINDER"
                                Context.SE_MAIL.AddObject(_newMail)

                                Context.SaveChanges()

                            ElseIf checkSE_MAIL = 0 And check > 0 Then
                                sContent = ""
                                sContent &= "Thư nhắc nhở: <br />"
                                sContent &= (iCount + 1) & ". " & lst(i).EMPLOYEE_CODE & " - " & lst(i).FULLNAME & ": "

                                If lst(i).REMIND_TYPE = SystemConfig.RemindConfigType.Contract Then
                                    sContent &= "Ngày hết hạn hợp đồng "
                                End If

                                sContent &= lst(i).REMIND_DATE.Value.ToString("dd/MM/yyyy") & "<br />"

                                Dim _newMail As New SE_MAIL
                                _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
                                _newMail.MAIL_FROM = defaultFrom
                                _newMail.MAIL_TO = toMailConfig
                                _newMail.MAIL_CC = ""
                                _newMail.MAIL_BCC = ""
                                _newMail.SUBJECT = "[Histaff-Reminder]"
                                _newMail.CONTENT = sContent
                                _newMail.VIEW_NAME = "REMINDER"
                                Context.SE_MAIL.AddObject(_newMail)

                                Context.SaveChanges()
                            End If

                        Next

                    Next
                    Return True
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                    Return False
                End Try

            End Using
        End Using
    End Function
#End Region

#Region "Báo cáo"
    Public Function ProfileReport(ByVal sPkgName As String, ByVal sStartDate As Date?, ByVal sEndDate As Date?, ByVal sOrg As Integer, ByVal sUserName As String, ByVal sLang As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrg,
                                                     .P_USERNAME = sUserName,
                                                     .P_STARTDATE = sStartDate,
                                                     .P_ENDDATE = sEndDate,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Try
            Dim query As IQueryable(Of Se_ReportDTO)
            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" And log.Username.ToUpper <> "HR.ADMIN" Then
                query = From u In Context.SE_USER
                        From p In u.SE_REPORT
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .MODULE_ID = p.MODULE_ID}
            Else
                query = From p In Context.SE_REPORT
                        Where p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .MODULE_ID = p.MODULE_ID}

            End If

            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.SEMPLOYEE_ID = sEmployee_id,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR,
                                                     .P_CUR4 = cls.OUT_CURSOR,
                                                     .P_CUR5 = cls.OUT_CURSOR,
                                                     .P_CUR6 = cls.OUT_CURSOR,
                                                     .P_CUR7 = cls.OUT_CURSOR,
                                                     .P_CUR8 = cls.OUT_CURSOR,
                                                     .P_CUR9 = cls.OUT_CURSOR,
                                                     .P_CUR10 = cls.OUT_CURSOR,
                                                     .P_CUR11 = cls.OUT_CURSOR,
                                                     .P_CUR12 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Xuat file bao cao theo sPkgName
    ''' PKG_PROFILE_REPORT.HU0101 => Bao cao danh sach CBNV
    ''' PKG_PROFILE_REPORT.HU0102 => Baos cao danh sach hop dong co hieu luc
    ''' PKG_PROFILE_REPORT.HU0103 => Bao cao danh sach hop dong het han
    ''' PKG_PROFILE_REPORT.HU0104 => Bao cao danh sach cham dut hop dong
    ''' PKG_PROFILE_REPORT.HU0105 => Bao cao danh sach khen thuong
    ''' PKG_PROFILE_REPORT.HU0106 => Bao cao danh sach ho so luong, phu cap
    ''' PKG_PROFILE_REPORT.HU0107 => Bao cao danh sach ky luat
    ''' PKG_PROFILE_REPORT.HU0108 => Bao cao danh sach nhan vien dieu chuyen
    ''' PKG_PROFILE_REPORT.HU0109 => Bao cao nhan vien kiem nhiem
    ''' PKG_PROFILE_REPORT.HU0110 => Bao cao danh sach thay doi ho so luong
    ''' PKG_PROFILE_REPORT.HU0111 => Bao cao danh sach thay doi chuc danh
    ''' PKG_PROFILE_REPORT.HU0112 => Bao cao danh sach nhan vien dang co don xin cham dut HDLD
    ''' PKG_PROFILE_REPORT.HU0113 => Bao cao tong hop bien dong nhan su
    ''' PKG_PROFILE_REPORT.HU0114 => Bao cao qua trinh luong, phu cap
    ''' </summary>
    ''' <param name="sPkgName"></param>
    ''' <param name="sStartDate"></param>
    ''' <param name="sEndDate"></param> 
    ''' <param name="sOrg"></param>
    ''' <param name="IsDissolve"></param>
    ''' <param name="sUserName"></param>
    ''' <param name="sLang"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExportReport(ByVal sPkgName As String,
                                 ByVal sStartDate As Date?,
                                 ByVal sEndDate As Date?,
                                 ByVal sOrg As String,
                                 ByVal IsDissolve As Integer, ByVal sUserName As String, ByVal sLang As String) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrg,
                                                     .P_USERNAME = sUserName,
                                                     .P_STARTDATE = sStartDate,
                                                     .P_ENDDATE = sEndDate,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "AUTOGENCODE"
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName & " WHERE " & colName & " LIKE '%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "0001"
            End If
            Dim number_str As String = str.Substring(str.IndexOf(firstChar) + firstChar.Length)
            If Not IsNumeric(number_str) Then
                Return firstChar & "0001"
            End If
            Dim number = Decimal.Parse(number_str)
            number = number + 1
            Dim lastChar = Format(number, "0000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "AUTOGENID"

    Public Function AutoGenID(ByVal tableName As String) As Decimal
        Dim decRelExcute As Decimal = 0

        Try
            decRelExcute = Context.ExecuteStoreQuery(Of Decimal)("SELECT MAX(ID) FROM " & tableName).FirstOrDefault
            If decRelExcute = 0 Then
                Return 1
            Else
                Return decRelExcute + 1
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "PLHD"
    Public Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer
        Try
            Dim maxSTT As Integer
            Dim query As List(Of HU_FILECONTRACT)
            If id > 0 Then 'edit
                Dim fileContract = Context.HU_FILECONTRACT.Where(Function(f) f.ID = id).FirstOrDefault
                If fileContract.ID_CONTRACT = contract_id Then
                    Return fileContract.STT
                Else
                    query = Context.HU_FILECONTRACT.Where(Function(f) f.ID_CONTRACT = contract_id And f.EMP_ID = emp_id).ToList
                End If
            Else 'insert
                query = Context.HU_FILECONTRACT.Where(Function(f) f.ID_CONTRACT = contract_id And f.EMP_ID = emp_id).ToList
            End If
            If query.Count > 0 Then
                maxSTT = IIf(IsNumeric(query.Max(Function(f) f.STT)), query.Max(Function(f) f.STT), 0)
                Return maxSTT + 1
            End If
            Return 1
        Catch ex As Exception
            'Throw ex
            Return 1
        End Try
    End Function
    ''' <summary>
    ''' in phu luc hợp đồng lao động
    ''' </summary>
    ''' <param name="emp_ID">mã nhân viên</param>
    ''' <param name="fileContract_ID">id của phụ lục hợp đồng</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PrintFileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE.RP_IPROFILE_PLHDLD",
                                          New With {.P_EMP_ID = emp_code,
                                                    .P_ID_PHULUC = fileContract_ID,
                                                    .P_OUT = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO
        Try

            Dim query = (From p In Context.HU_FILECONTRACT
                         From u In Context.HU_EMPLOYEE.Where(Function(x) p.EMP_ID = x.ID).DefaultIfEmpty
                         From ho In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = u.ORG_ID).DefaultIfEmpty
                         From o1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = ho.ORG_ID2).DefaultIfEmpty
                         From e1 In Context.HU_EMPLOYEE.Where(Function(x) x.ID = p.SIGN_ID).DefaultIfEmpty
                         From e2 In Context.HU_EMPLOYEE.Where(Function(x) x.ID = p.SIGN_ID2).DefaultIfEmpty
                         From t1 In Context.HU_TITLE.Where(Function(x) x.ID = e1.TITLE_ID).DefaultIfEmpty
                         From t2 In Context.HU_TITLE.Where(Function(x) x.ID = e2.TITLE_ID).DefaultIfEmpty
                         From t In Context.HU_TITLE.Where(Function(x) x.ID = u.TITLE_ID).DefaultIfEmpty
                         From c In Context.HU_CONTRACT.Where(Function(x) x.ID = p.ID_CONTRACT).DefaultIfEmpty
                         From type In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                         From org In Context.HU_ORGANIZATION.Where(Function(x) x.ID = u.ORG_ID).DefaultIfEmpty
                         From f In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty
                         From staffrank In Context.HU_STAFF_RANK.Where(Function(x) u.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                         From w In Context.HU_WORKING.Where(Function(x) p.WORKING_ID = f.ID).DefaultIfEmpty
                         From sal_group In Context.PA_SALARY_GROUP.Where(Function(x) w.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                         From sal_level In Context.PA_SALARY_LEVEL.Where(Function(x) w.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                         From wp In Context.HU_WORK_PLACE.Where(Function(x) x.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                         Where p.ID = ID
                         Select New FileContractDTO With {.ID = p.ID,
                                                            .EMPLOYEE_ID = p.EMP_ID,
                                                             .SHORT_NAME = o1.SHORT_NAME,
                                                            .REMARK = p.REMARK,
                                                            .EMPLOYEE_CODE = u.EMPLOYEE_CODE,
                                                            .EMPLOYEE_NAME = u.FULLNAME_VN,
                                                            .TITLE_NAME = t.NAME_VN,
                                                             .TITLE_ID = p.TITLE_ID,
                                                             .ORG_ID = u.ORG_ID,
                                                             .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                                             .WORK_PLACE_NAME = wp.NAME_VN,
                                                            .ORG_NAME = org.NAME_VN,
                                                            .CONTRACT_NO = p.CONTRACT_NO,
                                                            .ID_CONTRACT = p.ID_CONTRACT,
                                                            .CONTRACTTYPE_ID = p.CONTRACT_TYPE_ID,
                                                            .CONTRACTTYPE_NAME = type.NAME,
                                                            .APPEND_NUMBER = p.APPEND_NUMBER,
                                                            .APPEND_TYPEID = p.APPEND_TYPEID,
                                                            .CONTENT_APPEND = p.CONTENT_APPEND,
                                                            .START_DATE = p.START_DATE,
                                                            .EXPIRE_DATE = p.EXPIRE_DATE,
                                                            .FILEUPLOAD = p.FILEUPLOAD,
                                                            .STATUS_ID = p.STATUS_ID,
                                                             .DECISION_NO = w.DECISION_NO,
                                                             .EFFECT_DATE = w.EFFECT_DATE,
                                                            .FORM_ID = p.FORM_ID,
                                                            .LOCATION_ID = p.LOCATION_ID,
                                                            .SIGN_ID = p.SIGN_ID,
                                                            .SIGNER_NAME = e1.FULLNAME_VN,
                                                            .SIGNER_TITLE = t1.NAME_VN,
                                                            .SIGN_ID2 = p.SIGN_ID2,
                                                            .SIGNER_NAME2 = e2.FULLNAME_VN,
                                                            .SIGNER_TITLE2 = t2.NAME_VN,
                                                            .LOCATION_NAME = "",
                                                            .FORM_NAME = f.NAME_VN,
                                                            .SIGN_DATE = p.SIGN_DATE,
                                                            .AUTHORITY = p.AUTHOR,
                                                            .FILENAME = p.FILENAME,
                                                            .UPLOADFILE = p.UPLOADFILE,
                                                            .AUTHOR_CHAIRMAIN = p.AUTHOR_CHAIRMAN,
                                                            .AUTHORITY_NUMBER = p.AUTHOR_NUMBER,
                                                            .WORKING_ID = p.WORKING_ID,
                                                            .SAL_BASIC = w.SAL_BASIC,
                                                            .SAL_GROUP_ID = w.SAL_GROUP_ID,
                                                            .SAL_GROUP_NAME = sal_group.NAME,
                                                            .SAL_LEVEL_ID = w.SAL_LEVEL_ID,
                                                            .SAL_LEVEL_NAME = sal_level.NAME,
                                                            .SAL_RANK_ID = w.SAL_RANK_ID,
                                                            .SAL_RANK_NAME = sal_level.NAME,
                                                            .BONUS = Nothing,
                                                            .SAL_TOTAL = w.SAL_TOTAL,
                                                            .PERCENT_YEAR = Nothing,
                                                            .SAL_YEAR = Nothing,
                                                            .SAL_BHXH = Nothing,
                                                            .SAL_BHYT = Nothing,
                                                            .SAL_BHTN = Nothing,
                                                            .CONTENT = p.CONTENT,
                                                            .AUTHORITY_CHECKBOX = If(p.AUTHOR = "-1", True, False),
                                                            .AUTHOR_CHAIRMAIN_CHECKBOX = If(p.AUTHOR_CHAIRMAN = "-1", True, False)}).FirstOrDefault

            If query IsNot Nothing Then
                Dim lstType = (From p In Context.HU_FILECONTRACT_TYPE Where p.FILE_CONTRACT_ID = query.ID Select p.TYPE_ID).ToList
                query.APPEND_TYPE_LIST = lstType
                query.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                      From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                      Where p.HU_WORKING_ID = query.WORKING_ID
                                      Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                           .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                           .AMOUNT = p.AMOUNT,
                                                                           .EFFECT_DATE = p.EFFECT_DATE,
                                                                           .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                           .IS_INSURRANCE = p.IS_INSURRANCE}).ToList

                'Lay Chuc Danh, Phong Ban theo quyet dinh 
                'Làm biếng thêm cột vào entity quá nên làm cách này cho nhanh
                Using cls As New DataAccess.QueryData
                    Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE.GET_ORG_TITLE_BY_FC",
                                               New With {.P_ID = query.ID,
                                                         .P_CUR = cls.OUT_CURSOR}, False)
                    If dsData.Tables(0).Rows.Count > 0 Then
                        query.ORG_NAME = dsData.Tables(0).Rows(0)("ORG_NAME").ToString
                        query.TITLE_NAME = dsData.Tables(0).Rows(0)("TITLE_NAME").ToString
                    End If
                End Using
            End If
            Return query
            'contract = contract.Skip(PageIndex * PageSize).Take(PageSize)


        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetContractPaging")
            Throw ex
        End Try
    End Function
    Public Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                                 ByVal PageSize As Integer,
                                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                 Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Lay Danh sach PLHD di kem vs QĐ
            Dim dtQD As New DataTable
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE.GET_PLHD_QD", New With {.P_CUR = cls.OUT_CURSOR}, False)
                If dsData.Tables(0).Rows.Count > 0 Then
                    dtQD = dsData.Tables(0)
                End If
            End Using
            Dim QD = dtQD.AsEnumerable.Select(Function(f) New FileContractDTO With {.ID = f.Field(Of Decimal)("ID"),
                                                                                    .EMPLOYEE_ID = f.Field(Of Decimal)("EMP_ID"),
                                                                                     .ORG_NAME = f.Field(Of String)("ORG_NAME"),
                                                                                     .ORG_NAME2 = f.Field(Of String)("ORG_NAME2"),
                                                                                     .ORG_NAME3 = f.Field(Of String)("ORG_NAME3"),
                                                                                     .ORG_NAME4 = f.Field(Of String)("ORG_NAME4"),
                                                                                     .ORG_NAME5 = f.Field(Of String)("ORG_NAME5"),
                                                                                     .ORG_NAME6 = f.Field(Of String)("ORG_NAME6"),
                                                                                     .ORG_NAME7 = f.Field(Of String)("ORG_NAME7"),
                                                                                     .TITLE_NAME = f.Field(Of String)("TITLE_NAME"),
                                                                                     .APPEND_TYPE_NAME = f.Field(Of String)("TYPE_NAME"),
                                                                                    .ORG_ID = f.Field(Of Decimal)("ORG_ID"),
                                                                                    .ORG_DESC = f.Field(Of String)("ORG_DESC"),
                                                                                    .TITLE_ID = f.Field(Of Decimal)("TITLE_ID")}).ToList()
            Dim query = From p In Context.HU_FILECONTRACT
                        From u In Context.HU_EMPLOYEE.Where(Function(x) p.EMP_ID = x.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.STATUS_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT.Where(Function(x) x.ID = p.ID_CONTRACT).DefaultIfEmpty
                        From type In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From other_ctr In Context.OT_OTHER_LIST.Where(Function(x) x.ID = type.TYPE_ID)
                        From form In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty
                        From appendtype In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = p.APPEND_TYPEID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(x) x.ID = u.TITLE_ID).DefaultIfEmpty()
                        From org In Context.HU_ORGANIZATION.Where(Function(x) x.ID = u.ORG_ID).DefaultIfEmpty()
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = u.ORG_ID And f.USERNAME = log.Username.ToUpper)

            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.u.WORK_STATUS.HasValue Or
                                    (p.u.WORK_STATUS.HasValue And
                                     ((p.u.WORK_STATUS <> terID) Or (p.u.WORK_STATUS = terID And p.u.TER_EFFECT_DATE > dateNow))))
            End If


            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.u.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or p.u.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If


            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.u.ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(x) x.org.NAME_VN.Contains(_filter.ORG_NAME))
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(x) x.t.NAME_VN.Contains(_filter.TITLE_NAME))
            End If

            If _filter.ID_CONTRACT <> 0 Then
                query = query.Where(Function(p) p.p.ID_CONTRACT = _filter.ID_CONTRACT)
            End If
            If _filter.STATUS_ID <> 0 Then
                query = query.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.APPEND_NUMBER IsNot Nothing Then
                query = query.Where(Function(p) p.p.APPEND_NUMBER = _filter.APPEND_NUMBER)
            End If

            If _filter.APPEND_TYPEID <> 0 Then
                query = query.Where(Function(x) x.p.APPEND_TYPEID = _filter.APPEND_TYPEID)
            End If

            If _filter.CONTRACTTYPE_ID <> 0 Then
                query = query.Where(Function(p) p.type.ID = _filter.CONTRACTTYPE_ID)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE <= _filter.EXPIRE_DATE)
            End If
            If _filter.CONTRACT_NO IsNot Nothing Then
                query = query.Where(Function(x) x.p.CONTRACT_NO = _filter.CONTRACT_NO)
            End If

            Dim contract = query.Select(Function(x) New FileContractDTO With {.ID = x.p.ID,
                                                                              .EMPLOYEE_ID = x.p.EMP_ID,
                                                                              .EMPLOYEE_CODE = x.u.EMPLOYEE_CODE,
                                                                              .EMPLOYEE_NAME = x.u.FULLNAME_VN,
                                                                              .TITLE_NAME = x.t.NAME_VN,
                                                                              .TITLE_ID = x.t.ID,
                                                                              .ORG_NAME = x.org.NAME_VN,
                                                                              .ORG_ID = x.org.ID,
                                                                              .CONTRACT_NO = x.p.CONTRACT_NO,
                                                                              .ID_CONTRACT = x.p.ID_CONTRACT,
                                                                              .CONTRACTTYPE_ID = x.p.CONTRACT_TYPE_ID,
                                                                              .CONTRACTTYPE_NAME = x.type.NAME,
                                                                              .APPEND_NUMBER = x.p.APPEND_NUMBER,
                                                                              .CONTENT_APPEND = x.p.CONTENT_APPEND,
                                                                              .START_DATE = x.p.START_DATE,
                                                                              .EXPIRE_DATE = x.p.EXPIRE_DATE,
                                                                              .FILEUPLOAD = x.p.FILEUPLOAD,
                                                                              .CREATED_DATE = x.p.CREATED_DATE,
                                                                              .STATUS_ID = x.p.STATUS_ID,
                                                                              .STATUS_NAME = x.ot.NAME_VN,
                                                                              .APPEND_TYPEID = x.p.APPEND_TYPEID,
                                                                              .APPEND_TYPE_NAME = x.appendtype.NAME,
                                                                              .CONTRACTTYPE_CODE = x.appendtype.CODE,
                                                                              .FORM_ID = x.p.FORM_ID,
                                                                              .SIGN_DATE = x.p.SIGN_DATE,
                                                                              .LOCATION_ID = x.p.LOCATION_ID,
                                                                              .LOCATION_NAME = "",
                                                                              .FORM_NAME = x.form.NAME_VN,
                                                                              .SIGN_ID = x.p.SIGN_ID,
                                                                              .SIGNER_NAME = x.p.SIGNER_NAME,
                                                                              .SIGNER_TITLE = x.p.SIGNER_TITLE,
                                                                              .WORKING_ID = x.p.WORKING_ID})
            contract = contract.OrderBy(Sorts)
            Total = contract.Count
            contract = contract.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstResult = contract.ToList()
            lstResult = (From a In lstResult
                         From b In QD.Where(Function(f) f.ID = a.ID)
                         Select New FileContractDTO With {.ID = a.ID,
                                                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                                                            .EMPLOYEE_CODE = a.EMPLOYEE_CODE,
                                                            .EMPLOYEE_NAME = a.EMPLOYEE_NAME,
                                                        .TITLE_NAME = b.TITLE_NAME,
                                                        .TITLE_ID = b.TITLE_ID,
                                                        .ORG_NAME = b.ORG_NAME,
                                                        .ORG_NAME2 = b.ORG_NAME2,
                                                        .ORG_NAME3 = b.ORG_NAME3,
                                                        .ORG_NAME4 = b.ORG_NAME4,
                                                        .ORG_NAME5 = b.ORG_NAME5,
                                                        .ORG_NAME6 = b.ORG_NAME6,
                                                        .ORG_NAME7 = b.ORG_NAME7,
                                                        .ORG_ID = b.ORG_ID,
                                                        .ORG_DESC = b.ORG_DESC,
                                                        .CONTRACT_NO = a.CONTRACT_NO,
                                                            .ID_CONTRACT = a.ID_CONTRACT,
                                                        .CONTRACTTYPE_ID = a.CONTRACTTYPE_ID,
                                                            .CONTRACTTYPE_NAME = a.CONTRACTTYPE_NAME,
                                                        .APPEND_NUMBER = a.APPEND_NUMBER,
                                                            .CONTENT_APPEND = a.CONTENT_APPEND,
                                                            .START_DATE = a.START_DATE,
                                                        .EXPIRE_DATE = a.EXPIRE_DATE,
                                                            .FILEUPLOAD = a.FILEUPLOAD,
                                                        .CREATED_DATE = a.CREATED_DATE,
                                                        .STATUS_ID = a.STATUS_ID,
                                                        .STATUS_NAME = a.STATUS_NAME,
                                                        .APPEND_TYPEID = a.APPEND_TYPEID,
                                                        .APPEND_TYPE_NAME = b.APPEND_TYPE_NAME,
                                                             .CONTRACTTYPE_CODE = a.CONTRACTTYPE_CODE,
                                                            .FORM_ID = a.FORM_ID,
                                                             .SIGN_DATE = a.SIGN_DATE,
                                                            .LOCATION_ID = a.LOCATION_ID,
                                                        .LOCATION_NAME = a.LOCATION_NAME,
                                                        .FORM_NAME = a.FORM_NAME,
                                                            .SIGN_ID = a.SIGN_ID,
                                                            .SIGNER_NAME = a.SIGNER_NAME,
                                                            .SIGNER_TITLE = a.SIGNER_TITLE,
                                                        .WORKING_ID = a.WORKING_ID}).ToList
            Return lstResult
            'Return contract.ToList()
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetContractPaging")
            Throw ex
        End Try


    End Function
    Public Function GetEmpDtlContractAppendixPaging(ByVal _filter As FileContractDTO,
                                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                 Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO)
        Try
            'Lay Danh sach PLHD di kem vs QĐ
            Dim dtQD As New DataTable
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_PROFILE.GET_PLHD_QD", New With {.P_CUR = cls.OUT_CURSOR}, False)
                If dsData.Tables(0).Rows.Count > 0 Then
                    dtQD = dsData.Tables(0)
                End If
            End Using
            Dim QD = dtQD.AsEnumerable.Select(Function(f) New FileContractDTO With {.ID = f.Field(Of Decimal)("ID"),
                                                                                    .EMPLOYEE_ID = f.Field(Of Decimal)("EMP_ID"),
                                                                                     .ORG_NAME = f.Field(Of String)("ORG_NAME"),
                                                                                     .ORG_NAME2 = f.Field(Of String)("ORG_NAME2"),
                                                                                     .ORG_NAME3 = f.Field(Of String)("ORG_NAME3"),
                                                                                     .ORG_NAME4 = f.Field(Of String)("ORG_NAME4"),
                                                                                     .ORG_NAME5 = f.Field(Of String)("ORG_NAME5"),
                                                                                     .ORG_NAME6 = f.Field(Of String)("ORG_NAME6"),
                                                                                     .ORG_NAME7 = f.Field(Of String)("ORG_NAME7"),
                                                                                     .TITLE_NAME = f.Field(Of String)("TITLE_NAME"),
                                                                                     .APPEND_TYPE_NAME = f.Field(Of String)("TYPE_NAME"),
                                                                                    .ORG_ID = f.Field(Of Decimal)("ORG_ID"),
                                                                                    .ORG_DESC = f.Field(Of String)("ORG_DESC"),
                                                                                    .TITLE_ID = f.Field(Of Decimal)("TITLE_ID")}).ToList()
            Dim query = From p In Context.HU_FILECONTRACT
                        From u In Context.HU_EMPLOYEE.Where(Function(x) p.EMP_ID = x.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.STATUS_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT.Where(Function(x) x.ID = p.ID_CONTRACT).DefaultIfEmpty
                        From type In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From other_ctr In Context.OT_OTHER_LIST.Where(Function(x) x.ID = type.TYPE_ID)
                        From form In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty
                        From appendtype In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = p.APPEND_TYPEID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(x) x.ID = u.TITLE_ID).DefaultIfEmpty()
                        From org In Context.HU_ORGANIZATION.Where(Function(x) x.ID = u.ORG_ID).DefaultIfEmpty()

            '' lọc điều kiện
            'Dim dateNow = Date.Now.Date
            'Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            'If Not _filter.IS_TER Then
            '    query = query.Where(Function(p) Not p.u.WORK_STATUS.HasValue Or
            '                        (p.u.WORK_STATUS.HasValue And
            '                         ((p.u.WORK_STATUS <> terID) Or (p.u.WORK_STATUS = terID And p.u.TER_EFFECT_DATE > dateNow))))
            'End If


            'If _filter.EMPLOYEE_CODE IsNot Nothing Then
            '    query = query.Where(Function(p) p.u.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or p.u.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            'End If


            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.u.ID = _filter.EMPLOYEE_ID)
            End If

            'If _filter.ORG_NAME IsNot Nothing Then
            '    query = query.Where(Function(x) x.org.NAME_VN.Contains(_filter.ORG_NAME))
            'End If

            'If _filter.TITLE_NAME IsNot Nothing Then
            '    query = query.Where(Function(x) x.t.NAME_VN.Contains(_filter.TITLE_NAME))
            'End If

            'If _filter.ID_CONTRACT <> 0 Then
            '    query = query.Where(Function(p) p.p.ID_CONTRACT = _filter.ID_CONTRACT)
            'End If
            If _filter.STATUS_ID <> 0 Then
                query = query.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If

            'If _filter.APPEND_NUMBER IsNot Nothing Then
            '    query = query.Where(Function(p) p.p.APPEND_NUMBER = _filter.APPEND_NUMBER)
            'End If

            'If _filter.APPEND_TYPEID <> 0 Then
            '    query = query.Where(Function(x) x.p.APPEND_TYPEID = _filter.APPEND_TYPEID)
            'End If

            'If _filter.CONTRACTTYPE_ID <> 0 Then
            '    query = query.Where(Function(p) p.type.ID = _filter.CONTRACTTYPE_ID)
            'End If
            'If _filter.START_DATE IsNot Nothing Then
            '    query = query.Where(Function(p) p.p.START_DATE >= _filter.START_DATE)
            'End If
            'If _filter.EXPIRE_DATE IsNot Nothing Then
            '    query = query.Where(Function(p) p.p.EXPIRE_DATE <= _filter.EXPIRE_DATE)
            'End If
            'If _filter.CONTRACT_NO IsNot Nothing Then
            '    query = query.Where(Function(x) x.p.CONTRACT_NO = _filter.CONTRACT_NO)
            'End If

            Dim contract = query.Select(Function(x) New FileContractDTO With {.ID = x.p.ID,
                                                                              .EMPLOYEE_ID = x.p.EMP_ID,
                                                                              .EMPLOYEE_CODE = x.u.EMPLOYEE_CODE,
                                                                              .EMPLOYEE_NAME = x.u.FULLNAME_VN,
                                                                              .TITLE_NAME = x.t.NAME_VN,
                                                                              .TITLE_ID = x.t.ID,
                                                                              .ORG_NAME = x.org.NAME_VN,
                                                                              .ORG_ID = x.org.ID,
                                                                              .CONTRACT_NO = x.p.CONTRACT_NO,
                                                                              .ID_CONTRACT = x.p.ID_CONTRACT,
                                                                              .CONTRACTTYPE_ID = x.p.CONTRACT_TYPE_ID,
                                                                              .CONTRACTTYPE_NAME = x.type.NAME,
                                                                              .APPEND_NUMBER = x.p.APPEND_NUMBER,
                                                                              .CONTENT_APPEND = x.p.CONTENT_APPEND,
                                                                              .START_DATE = x.p.START_DATE,
                                                                              .EXPIRE_DATE = x.p.EXPIRE_DATE,
                                                                              .FILEUPLOAD = x.p.FILEUPLOAD,
                                                                              .CREATED_DATE = x.p.CREATED_DATE,
                                                                              .STATUS_ID = x.p.STATUS_ID,
                                                                              .STATUS_NAME = x.ot.NAME_VN,
                                                                              .APPEND_TYPEID = x.p.APPEND_TYPEID,
                                                                              .APPEND_TYPE_NAME = x.appendtype.NAME,
                                                                              .CONTRACTTYPE_CODE = x.appendtype.CODE,
                                                                              .FORM_ID = x.p.FORM_ID,
                                                                              .SIGN_DATE = x.p.SIGN_DATE,
                                                                              .LOCATION_ID = x.p.LOCATION_ID,
                                                                              .LOCATION_NAME = "",
                                                                              .FORM_NAME = x.form.NAME_VN,
                                                                              .SIGN_ID = x.p.SIGN_ID,
                                                                              .SIGNER_NAME = x.p.SIGNER_NAME,
                                                                              .SIGNER_TITLE = x.p.SIGNER_TITLE,
                                                                              .WORKING_ID = x.p.WORKING_ID})
            contract = contract.OrderBy(Sorts)
            'Total = contract.Count
            'contract = contract.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstResult = contract.ToList()
            lstResult = (From a In lstResult
                         From b In QD.Where(Function(f) f.ID = a.ID)
                         Select New FileContractDTO With {.ID = a.ID,
                                                            .EMPLOYEE_ID = a.EMPLOYEE_ID,
                                                            .EMPLOYEE_CODE = a.EMPLOYEE_CODE,
                                                            .EMPLOYEE_NAME = a.EMPLOYEE_NAME,
                                                        .TITLE_NAME = b.TITLE_NAME,
                                                        .TITLE_ID = b.TITLE_ID,
                                                        .ORG_NAME = b.ORG_NAME,
                                                        .ORG_NAME2 = b.ORG_NAME2,
                                                        .ORG_NAME3 = b.ORG_NAME3,
                                                        .ORG_NAME4 = b.ORG_NAME4,
                                                        .ORG_NAME5 = b.ORG_NAME5,
                                                        .ORG_NAME6 = b.ORG_NAME6,
                                                        .ORG_NAME7 = b.ORG_NAME7,
                                                        .ORG_ID = b.ORG_ID,
                                                        .ORG_DESC = b.ORG_DESC,
                                                        .CONTRACT_NO = a.CONTRACT_NO,
                                                            .ID_CONTRACT = a.ID_CONTRACT,
                                                        .CONTRACTTYPE_ID = a.CONTRACTTYPE_ID,
                                                            .CONTRACTTYPE_NAME = a.CONTRACTTYPE_NAME,
                                                        .APPEND_NUMBER = a.APPEND_NUMBER,
                                                            .CONTENT_APPEND = a.CONTENT_APPEND,
                                                            .START_DATE = a.START_DATE,
                                                        .EXPIRE_DATE = a.EXPIRE_DATE,
                                                            .FILEUPLOAD = a.FILEUPLOAD,
                                                        .CREATED_DATE = a.CREATED_DATE,
                                                        .STATUS_ID = a.STATUS_ID,
                                                        .STATUS_NAME = a.STATUS_NAME,
                                                        .APPEND_TYPEID = a.APPEND_TYPEID,
                                                        .APPEND_TYPE_NAME = b.APPEND_TYPE_NAME,
                                                             .CONTRACTTYPE_CODE = a.CONTRACTTYPE_CODE,
                                                            .FORM_ID = a.FORM_ID,
                                                             .SIGN_DATE = a.SIGN_DATE,
                                                            .LOCATION_ID = a.LOCATION_ID,
                                                        .LOCATION_NAME = a.LOCATION_NAME,
                                                        .FORM_NAME = a.FORM_NAME,
                                                            .SIGN_ID = a.SIGN_ID,
                                                            .SIGNER_NAME = a.SIGNER_NAME,
                                                            .SIGNER_TITLE = a.SIGNER_TITLE,
                                                        .WORKING_ID = a.WORKING_ID}).ToList
            Return lstResult
            'Return contract.ToList()
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetContractPaging")
            Throw ex
        End Try


    End Function
    Public Function DeleteFileContract(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Try
            For Each item In lstID
                Dim objContractData As HU_FILECONTRACT
                ' Xóa phụ lục hợp đồng
                objContractData = (From p In Context.HU_FILECONTRACT Where p.ID = item).FirstOrDefault

                'tao 1 dong moi trong table file_contract da xoa
                Dim attFile As New HU_FILECONTRACT_DELETED

                Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.HU_FILECONTRACT_DELETED.EntitySet.Name)
                attFile = New HU_FILECONTRACT_DELETED With {
                    .ID = fileID,
                    .REMARK = objContractData.REMARK,
                    .START_DATE = objContractData.START_DATE,
                    .EXPIRE_DATE = objContractData.EXPIRE_DATE,
                    .CONTRACT_NO = objContractData.CONTRACT_NO,
                    .SIGN_DATE = objContractData.SIGN_DATE,
                    .SIGN_ID = objContractData.SIGN_ID,
                    .SIGN_ORG_ID = objContractData.SIGN_ORG_ID,
                    .JOB = objContractData.JOB,
                    .SALARY = objContractData.SALARY,
                    .EMP_ID = objContractData.EMP_ID,
                     .ID_CONTRACT = objContractData.ID_CONTRACT,
                    .CONTRACT_TYPE_ID = objContractData.CONTRACT_TYPE_ID,
                    .CONTENT_APPEND = objContractData.CONTENT_APPEND,
                    .FILEUPLOAD = objContractData.FILEUPLOAD,
                    .APPEND_NUMBER = objContractData.APPEND_NUMBER,
                    .STATUS_ID = objContractData.STATUS_ID,
                    .APPEND_TYPEID = objContractData.APPEND_TYPEID,
                    .FORM_ID = objContractData.FORM_ID,
                    .LOCATION_ID = objContractData.LOCATION_ID,
                    .AUTHOR = objContractData.AUTHOR,
                    .AUTHOR_NUMBER = objContractData.AUTHOR_NUMBER,
                    .AUTHOR_CHAIRMAN = objContractData.AUTHOR_CHAIRMAN,
                    .SIGNER_NAME = objContractData.SIGNER_NAME,
                    .SIGNER_TITLE = objContractData.SIGNER_TITLE,
                    .WORKING_ID = objContractData.WORKING_ID,
                    .STT = objContractData.STT,
                    .CREATED_BY = log.Username,
                    .CREATED_DATE = Date.Now,
                    .CREATED_LOG = log.Ip & "-" & log.ComputerName,
                    .MODIFIED_BY = log.Username,
                    .MODIFIED_DATE = Date.Now,
                    .MODIFIED_LOG = log.Ip & "-" & log.ComputerName
                }

                Context.HU_FILECONTRACT_DELETED.AddObject(attFile)

                Context.HU_FILECONTRACT.DeleteObject(objContractData)
                DeleteFileType(item)

                Context.SaveChanges()
            Next

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteContract")
            Throw ex
        End Try
    End Function

    Public Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean
        Try
            Dim numDay As Integer
            Dim period As Integer = 0
            Dim inforContract = (From p In Context.HU_CONTRACT Where p.ID = ID Select p).FirstOrDefault
            If inforContract IsNot Nothing Then
                period = Context.HU_CONTRACT_TYPE.First(Function(x) x.ID = inforContract.CONTRACT_TYPE_ID).PERIOD
                If period <> 0 Then
                    numDay = (EndDate - inforContract.START_DATE.Value).Days
                    numDay = Math.Round(numDay / 365 * 12, 0)
                    If numDay > period Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal ID As Decimal) As Boolean
        Try
            If ID <> 0 Then
                Dim query = (From p In Context.HU_FILECONTRACT
                             Where p.EMP_ID = empID AndAlso p.ID <> ID Select p).ToList()
                If query IsNot Nothing AndAlso query.Count <> 0 Then
                    If query.Any(Function(x) x.EMP_ID = empID AndAlso x.START_DATE.Value.Date = StartDate.Date) Then
                        Return False
                    End If
                End If
            Else
                Dim query = (From p In Context.HU_FILECONTRACT
                             Where p.EMP_ID = empID Select p).ToList()
                If query IsNot Nothing AndAlso query.Count <> 0 Then
                    If query.Any(Function(x) x.EMP_ID = empID AndAlso x.START_DATE.Value.Date = StartDate.Date) Then
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef appenNum As String) As Boolean
        Try

            Dim attFile As HU_FILECONTRACT
            Dim lstContract As New List(Of HU_FILECONTRACT)
            Dim MaxSTT As Integer = 0
            Dim STTHD As Integer = 0

            'lay HĐLĐ thứ bao nhiêu của nhân viên !
            Dim listContract As List(Of HU_FILECONTRACT) = Context.HU_FILECONTRACT.ToList.FindAll(Function(x) x.EMP_ID IsNot Nothing And x.ID_CONTRACT IsNot Nothing And x.EMP_ID = FileInfo.EMPLOYEE_ID And x.ID_CONTRACT <> FileInfo.ID_CONTRACT)
            If listContract IsNot Nothing Then
                If lstContract.Count = 0 Then
                    STTHD = 1
                Else
                    For Each hf As HU_FILECONTRACT In listContract
                        If hf.ID_CONTRACT <> FileInfo.ID_CONTRACT Then
                            STTHD = STTHD + 1
                        End If
                    Next
                End If
            End If

            Dim inforContract = (From p In Context.HU_CONTRACT.Where(Function(x) x.ID = FileInfo.ID_CONTRACT) Select p).FirstOrDefault


            'Lấy phụ lục HĐLĐ lần thứ bao nhiêu
            lstContract = Context.HU_FILECONTRACT.ToList().FindAll(Function(x) x.EMP_ID IsNot Nothing And x.ID_CONTRACT IsNot Nothing And x.EMP_ID = FileInfo.EMPLOYEE_ID And x.ID_CONTRACT = FileInfo.ID_CONTRACT)

            If lstContract IsNot Nothing Then
                If lstContract.Count = 0 Then
                    MaxSTT = 1
                Else
                    MaxSTT = IIf(IsNumeric(lstContract.Max(Function(x) x.STT)), lstContract.Max(Function(x) x.STT), 1)
                End If
            End If


            'Dim APPEND_NUMBER = MaxSTT.ToString() + "/" + FileInfo.EMPLOYEE_CODE + "/" + STTHD.ToString() + "/" + Date.Now.Year.ToString() + "/" + "/Sasco – HĐLĐ"
            'Dim APPEND_NUMBER = MaxSTT.ToString() + "/" + Replace(inforContract.CONTRACT_NO, "HDLD", "PLHDLD")

            Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.HU_FILECONTRACT.EntitySet.Name)
            attFile = New HU_FILECONTRACT With {
                .ID = fileID,
                .REMARK = FileInfo.REMARK,
                .START_DATE = FileInfo.START_DATE,
                .EXPIRE_DATE = FileInfo.EXPIRE_DATE,
                .CONTRACT_NO = FileInfo.CONTRACT_NO,
                .SIGN_DATE = FileInfo.SIGN_DATE,
                .SIGN_ID = FileInfo.SIGN_ID,
                .SIGN_ID2 = FileInfo.SIGN_ID2,
                .SIGNER_NAME2 = FileInfo.SIGNER_NAME2,
                .SIGNER_TITLE2 = FileInfo.SIGNER_TITLE2,
                .SIGN_ORG_ID = FileInfo.SIGN_ORG_ID,
                .JOB = FileInfo.JOB,
                .SALARY = FileInfo.SALARY,
                .EMP_ID = FileInfo.EMPLOYEE_ID,
                .ID_CONTRACT = FileInfo.ID_CONTRACT,
                .CONTRACT_TYPE_ID = FileInfo.CONTRACTTYPE_ID,
                .CONTENT_APPEND = FileInfo.CONTENT_APPEND,
                .FILEUPLOAD = FileInfo.FILEUPLOAD,
                .APPEND_NUMBER = appenNum,
                .STATUS_ID = FileInfo.STATUS_ID,
                .FORM_ID = FileInfo.FORM_ID,
            .LOCATION_ID = FileInfo.LOCATION_ID,
            .AUTHOR = FileInfo.AUTHORITY,
            .AUTHOR_NUMBER = FileInfo.AUTHORITY_NUMBER,
            .AUTHOR_CHAIRMAN = FileInfo.AUTHOR_CHAIRMAIN,
            .SIGNER_NAME = FileInfo.SIGNER_NAME,
            .SIGNER_TITLE = FileInfo.SIGNER_TITLE,
             .FILENAME = FileInfo.FILENAME,
                .ORG_ID = FileInfo.ORG_ID,
                .TITLE_ID = FileInfo.TITLE_ID,
                .WORK_PLACE_ID = FileInfo.WORK_PLACE_ID,
            .UPLOADFILE = FileInfo.UPLOADFILE,
             .WORKING_ID = FileInfo.WORKING_ID,
            .STT = FileInfo.STT,
            .CONTENT = FileInfo.CONTENT
            }
            Context.HU_FILECONTRACT.AddObject(attFile)
            If FileInfo.APPEND_TYPE_LIST.Count > 0 Then
                For Each item In FileInfo.APPEND_TYPE_LIST
                    Dim objFileType As New HU_FILECONTRACT_TYPE
                    objFileType.FILE_CONTRACT_ID = fileID
                    objFileType.TYPE_ID = item
                    Context.HU_FILECONTRACT_TYPE.AddObject(objFileType)
                Next
            End If
            If Context.SaveChanges(log) Then
                'appenNum = APPEND_NUMBER
                gID = fileID
                'If FileInfo.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                '    UpdateExtend_DateContract(FileInfo, log)
                'End If

                Return True
            End If
            Return False

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objUpdate As New HU_FILECONTRACT With {.ID = FileInfo.ID}
            Context.HU_FILECONTRACT.Attach(objUpdate)
            With objUpdate
                .REMARK = FileInfo.REMARK
                .START_DATE = FileInfo.START_DATE
                .EXPIRE_DATE = FileInfo.EXPIRE_DATE
                .CONTRACT_NO = FileInfo.CONTRACT_NO
                .SIGN_DATE = FileInfo.SIGN_DATE
                .SIGN_ID = FileInfo.SIGN_ID
                .SIGN_ID2 = FileInfo.SIGN_ID2
                .SIGNER_NAME2 = FileInfo.SIGNER_NAME2
                .SIGNER_TITLE2 = FileInfo.SIGNER_TITLE2
                .APPEND_NUMBER = FileInfo.APPEND_NUMBER
                .SIGN_ORG_ID = FileInfo.SIGN_ORG_ID
                .JOB = FileInfo.JOB
                .SALARY = FileInfo.SALARY
                .EMP_ID = FileInfo.EMPLOYEE_ID
                .ID_CONTRACT = FileInfo.ID_CONTRACT
                .CONTRACT_TYPE_ID = FileInfo.CONTRACTTYPE_ID
                .CONTENT_APPEND = FileInfo.CONTENT_APPEND
                .FILEUPLOAD = FileInfo.FILEUPLOAD
                .STATUS_ID = FileInfo.STATUS_ID
                .APPEND_TYPEID = FileInfo.APPEND_TYPEID
                .FORM_ID = FileInfo.FORM_ID
                .LOCATION_ID = FileInfo.LOCATION_ID
                .AUTHOR = FileInfo.AUTHORITY
                .AUTHOR_NUMBER = FileInfo.AUTHORITY_NUMBER
                .AUTHOR_CHAIRMAN = FileInfo.AUTHOR_CHAIRMAIN
                .SIGNER_NAME = FileInfo.SIGNER_NAME
                .SIGNER_TITLE = FileInfo.SIGNER_TITLE
                .WORKING_ID = FileInfo.WORKING_ID
                .FILENAME = FileInfo.FILENAME
                .ORG_ID = FileInfo.ORG_ID
                .TITLE_ID = FileInfo.TITLE_ID
                .WORK_PLACE_ID = FileInfo.WORK_PLACE_ID
                .UPLOADFILE = FileInfo.UPLOADFILE
                .CONTENT = FileInfo.CONTENT
            End With
            DeleteFileType(objUpdate.ID)
            If FileInfo.APPEND_TYPE_LIST.Count > 0 Then
                For Each item In FileInfo.APPEND_TYPE_LIST
                    Dim objFileType As New HU_FILECONTRACT_TYPE
                    objFileType.FILE_CONTRACT_ID = objUpdate.ID
                    objFileType.TYPE_ID = item
                    Context.HU_FILECONTRACT_TYPE.AddObject(objFileType)
                Next
            End If
            'If Context.SaveChanges(log) Then
            '    gID = FileInfo.ID
            '    'If FileInfo.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
            '    '    UpdateExtend_DateContract(FileInfo, log)
            '    'End If
            '    Return True
            'End If
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub DeleteFileType(ByVal _fileContractId As Decimal)
        Try
            Dim lstType = (From p In Context.HU_FILECONTRACT_TYPE Where p.FILE_CONTRACT_ID = _fileContractId)
            For Each item In lstType
                Context.HU_FILECONTRACT_TYPE.DeleteObject(item)
            Next
            Context.SaveChanges()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Sub

    Public Sub UpdateExtend_DateContract(ByVal objFileContract As FileContractDTO, ByVal log As UserLog)
        Try
            'lay thong tin loai hop dong
            Dim objType = GetContractTypeID(objFileContract.APPEND_TYPEID)
            'kiem tra loai hop dong co dau check gia han hay khong '''''And objType.EXTEND_CHECK = -1
            If objType IsNot Nothing Then
                '------------Phần này DuyNL làm mình comment lại--------------
                ''lay thong tin hop dong 
                'Dim objContract = (From p In Context.HU_CONTRACT
                '                   Where p.ID = objFileContract.ID_CONTRACT
                '                     Select New ContractDTO With {.ID = p.ID}).FirstOrDefault

                ''cap nhat ngay ket thuc cua phu luc hop dong cho ngày gia hạn của hợp đồng
                'If objContract IsNot Nothing Then
                '    Dim objContractData As New HU_CONTRACT With {.ID = objContract.ID}

                '    Context.HU_CONTRACT.Attach(objContractData)

                '    objContractData.ID = objContract.ID

                '    objContractData.EXTEND_DATE = objFileContract.EXPIRE_DATE

                '    Context.SaveChanges(log)
                'End If
                'ThanhNT sửa lại 29/05/2017 - Vì DuyNL làm attach z sẽ bị lỗi the same key entity
                Dim inforContract = (From p In Context.HU_CONTRACT.Where(Function(x) x.ID = objFileContract.ID_CONTRACT) Select p).FirstOrDefault
                'inforContract.EXTEND_DATE = objFileContract.EXPIRE_DATE
                Context.SaveChanges(log)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Sub

    Public Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO
        Try
            Dim query = (From p In Context.HU_CONTRACT_TYPE
                         From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                         From fl In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.FLOWING_MD).DefaultIfEmpty
                         From c In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CODE_GET_ENDDATE).DefaultIfEmpty
                         Where p.ID = ID
                         Select New ContractTypeDTO With {
                                                       .ID = p.ID,
                                                      .CODE = p.CODE,
                                                      .PERIOD = p.PERIOD,
                                                      .REMARK = p.REMARK,
                                                      .NAME = p.NAME,
                                                      .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                      .CREATED_DATE = p.CREATED_DATE,
                                                      .TYPE_ID = p.TYPE_ID,
                                                      .FLOWING_MD = fl.CODE,
                                                      .CODE_GET_ENDDATE = c.CODE}).FirstOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO)
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.EMPLOYEE_ID = empID AndAlso
                         p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso p.CONTRACT_NO IsNot Nothing
                         Select New ContractDTO With {.ID = p.ID,
                                                      .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                      .CONTRACT_NO = p.CONTRACT_NO,
                                                      .START_DATE = p.START_DATE,
                                                      .EXPIRE_DATE = p.EXPIRE_DATE,
                                                      .CONTRACTTYPE_ID = p.CONTRACT_TYPE_ID,
                                                      .STATUS_ID = p.STATUS_ID}).OrderBy("START_DATE DESC").ToList()
            'If query IsNot Nothing Then
            '    query.RemoveAll(Function(x) x.START_DATE.Value.Date > Date.Now.Date)
            '    query.RemoveAll(Function(x) x.EXPIRE_DATE IsNot Nothing AndAlso x.EXPIRE_DATE.Value.Date < Date.Now.Date)
            'End If
            'AndAlso p.CONTRACT_TYPE_ID <> 6 AndAlso p.CONTRACT_TYPE_ID <> 35 AndAlso p.CONTRACT_TYPE_ID <> 32
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTitileBaseOnEmp(ByVal ID As Decimal) As TitleDTO
        Try
            Dim query = (From p In Context.HU_TITLE.ToList Where p.ID = ID
                         Select New TitleDTO With {.ID = p.ID,
                                           .CODE = p.CODE,
                                           .NAME_EN = p.NAME_EN,
                                            .NAME_VN = p.NAME_VN,
                                            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")}).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String
        Try
            Dim result As String = ""
            Dim lstFileContract = Context.HU_FILECONTRACT.ToList.FindAll(Function(x) x.EMP_ID IsNot Nothing And x.ID_CONTRACT IsNot Nothing And x.EMP_ID = Contract.EMPLOYEE_ID And x.ID_CONTRACT = Contract.ID)
            If lstFileContract IsNot Nothing Then
                If lstFileContract.Count = 0 Then
                    STT = 1
                Else
                    STT = lstFileContract.Max(Function(x) x.STT)
                End If
            End If

            Return STT.ToString() + "/" + Replace(Contract.CONTRACT_NO, "HDLD", "PLHDLD")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO)
        Try


            Dim query = From p In Context.HU_FILECONTRACT
                        From u In Context.HU_EMPLOYEE.Where(Function(x) p.EMP_ID = x.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.STATUS_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(x) x.ID = u.TITLE_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT.Where(Function(x) x.ID = p.ID_CONTRACT).DefaultIfEmpty
                        From type In Context.HU_CONTRACT_TYPE.Where(Function(x) x.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(x) x.ID = u.ORG_ID).DefaultIfEmpty
            'Select New FileContractDTO With {.ID = p.ID,
            '                                 .EMPLOYEE_ID = p.EMP_ID,
            '                                 .EMPLOYEE_CODE = u.EMPLOYEE_CODE,
            '                                 .EMPLOYEE_NAME = u.FULLNAME_VN,
            '                                .TITLE_NAME = t.NAME_VN,
            '                                .ORG_NAME = org.NAME_VN,
            '                                .CONTRACT_NO = c.CONTRACT_NO,
            '                                 .ID_CONTRACT = c.ID,
            '                                .CONTRACTTYPE_ID = c.CONTRACT_TYPE_ID,
            '                                 .CONTRACTTYPE_NAME = type.NAME,
            '                                .APPEND_NUMBER = p.APPEND_NUMBER,
            '                                 .CONTENT_APPEND = p.CONTENT_APPEND,
            '                                 .EFFECT_DATE = p.EFFECT_DATE,
            '                                .EXPIRE_DATE = p.EXPIRE_DATE,
            '                                 .FILEUPLOAD = p.FILEUPLOAD}).ToList()

            ' lọc điều kiện


            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.u.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) Or p.u.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If


            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.u.ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(x) x.org.NAME_VN.Contains(_filter.ORG_NAME))
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(x) x.t.NAME_VN.Contains(_filter.TITLE_NAME))
            End If
            If _filter.ID_CONTRACT <> 0 Then
                query = query.Where(Function(p) p.p.ID_CONTRACT = _filter.ID_CONTRACT)
            End If
            If _filter.STATUS_ID <> 0 Then
                query = query.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.APPEND_NUMBER IsNot Nothing Then
                query = query.Where(Function(p) p.p.APPEND_NUMBER = _filter.APPEND_NUMBER)
            End If

            If _filter.APPEND_TYPEID <> 0 Then
                query = query.Where(Function(x) x.p.APPEND_TYPEID = _filter.APPEND_TYPEID)
            End If

            If _filter.CONTRACTTYPE_ID <> 0 Then
                query = query.Where(Function(p) p.type.ID = _filter.CONTRACTTYPE_ID)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE <= _filter.EXPIRE_DATE)
            End If
            If _filter.CONTRACT_NO IsNot Nothing Then
                query = query.Where(Function(x) x.p.CONTRACT_NO = _filter.CONTRACT_NO)
            End If
            Dim contract = query.Select(Function(x) New FileContractDTO With {.ID = x.p.ID,
                                                                                 .EMPLOYEE_ID = x.p.EMP_ID,
                                                                                 .EMPLOYEE_CODE = x.u.EMPLOYEE_CODE,
                                                                                 .EMPLOYEE_NAME = x.u.FULLNAME_VN,
                                                                                .TITLE_NAME = x.t.NAME_VN,
                                                                              .TITLE_ID = x.t.ID,
                                                                                .ORG_NAME = x.org.NAME_VN,
                                                                              .ORG_ID = x.org.ID,
                                                                                .CONTRACT_NO = x.p.CONTRACT_NO,
                                                                                 .ID_CONTRACT = x.p.ID_CONTRACT,
                                                                                .CONTRACTTYPE_ID = x.p.CONTRACT_TYPE_ID,
                                                                                 .CONTRACTTYPE_NAME = x.type.NAME,
                                                                                .APPEND_NUMBER = x.p.APPEND_NUMBER,
                                                                                 .CONTENT_APPEND = x.p.CONTENT_APPEND,
                                                                                 .START_DATE = x.p.START_DATE,
                                                                                .EXPIRE_DATE = x.p.EXPIRE_DATE,
                                                                                 .FILEUPLOAD = x.p.FILEUPLOAD,
                                                                              .STATUS_ID = x.p.STATUS_ID,
                                                                              .STATUS_NAME = x.ot.NAME_VN,
                                                                              .APPEND_TYPEID = x.p.APPEND_TYPEID,
                                                                              .WORKING_ID = x.p.WORKING_ID})
            Return contract.ToList()



            'contract = contract.Skip(PageIndex * PageSize).Take(PageSize)


        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetContractPaging")
            Throw ex
        End Try


    End Function

    Public Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO)
        Try
            If type <> "" Then
                Dim query = (From p In Context.HU_CONTRACT_TYPE
                             From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                             Where group.CODE = type And p.ACTFLG = "A"
                             Select New ContractTypeDTO With {
                                                           .ID = p.ID,
                                                          .CODE = group.CODE,
                                                          .PERIOD = p.PERIOD,
                                                          .REMARK = p.REMARK,
                                                          .NAME = p.NAME,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                          .CREATED_DATE = p.CREATED_DATE}).ToList
                Return query
            Else
                Dim query = (From p In Context.HU_CONTRACT_TYPE
                             From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                             Where p.ACTFLG = "A"
                             Select New ContractTypeDTO With {
                                                           .ID = p.ID,
                                                          .CODE = group.CODE,
                                                          .PERIOD = p.PERIOD,
                                                          .REMARK = p.REMARK,
                                                          .NAME = p.NAME,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                          .CREATED_DATE = p.CREATED_DATE,
                                                          .TYPE_ID = p.TYPE_ID}).ToList
                Return query
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO)
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.EMPLOYEE_ID = ID
                         Order By p.START_DATE Descending
                         Select New ContractDTO With {
                           .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                           .CONTRACT_NO = p.CONTRACT_NO,
                           .CONTRACTTYPE_ID = p.CONTRACT_TYPE_ID}).ToList()
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetListContract(ByVal ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_CONTRACT.GET_LIST_HU_CONTRACT",
                                          New With {.P_EMPID = ID,
                                                    .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


#End Region

    Public Function GET_CONCURRENTLY_BY_ID(ByVal P_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.GET_CONCURRENTLY_BY_ID",
                                           New With {.P_ID = P_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function INSERT_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable
                dtData = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.INSERT_CONCURRENTLY",
                                           New With {.P_EMPLOYEE_ID = concurrently.EMPLOYEE_ID,
                                                     .P_ORG_ID = concurrently.ORG_ID,
                                                     .P_TITLE_ID = concurrently.TITLE_ID,
                                                     .P_ORG_CON = concurrently.ORG_CON,
                                                     .P_TITLE_CON = concurrently.TITLE_CON,
                                                     .P_EFFECT_DATE_CON = concurrently.EFFECT_DATE_CON,
                                                     .P_EXPIRE_DATE_CON = concurrently.EXPIRE_DATE_CON,
                                                     .P_ALLOW_MONEY = concurrently.ALLOW_MONEY,
                                                     .P_CON_NO = concurrently.CON_NO,
                                                     .P_STATUS = concurrently.STATUS,
                                                     .P_SIGN_DATE = concurrently.SIGN_DATE,
                                                     .P_SIGN_ID = concurrently.SIGN_ID,
                                                     .P_SIGN_TITLE = concurrently.SIGN_TITLE_NAME,
                                                     .P_SIGN_ID_2 = concurrently.SIGN_ID_2,
                                                     .P_REMARK = concurrently.REMARK,
                                                     .P_CON_NO_STOP = concurrently.CON_NO_STOP,
                                                     .P_SIGN_DATE_STOP = concurrently.SIGN_DATE_STOP,
                                                     .P_EFFECT_DATE_STOP = concurrently.EFFECT_DATE_STOP,
                                                     .P_STATUS_STOP = concurrently.STATUS_STOP,
                                                     .P_SIGN_ID_STOP = concurrently.SIGN_ID_STOP,
                                                     .P_SIGN_STOP_TITLE = concurrently.SIGN_TITLE_NAME_STOP,
                                                     .P_SIGN_ID_STOP_2 = concurrently.SIGN_ID_STOP_2,
                                                     .P_REMARK_STOP = concurrently.REMARK_STOP,
                                                     .P_CONCURENTLY_TYPE = concurrently.CONCURENTLY_TYPE,
                                                     .P_CONCURENTLY_POSITION = concurrently.CONCURENTLY_POSITION,
                                                     .P_CONCURENTLY_TYPE_CANCEL = concurrently.CONCURENTLY_TYPE_CANCEL,
                                                     .P_CREATED_BY = concurrently.CREATED_BY,
                                                     .P_CREATED_LOG = concurrently.CREATED_LOG,
                                                     .P_IS_ALLOW = concurrently.IS_ALLOW,
                                                     .P_FILE_BYTE = concurrently.FILE_BYTE,
                                                     .P_FILE_BYTE1 = concurrently.FILE_BYTE1,
                                                     .P_ATTACH_FOLDER_BYTE = concurrently.ATTACH_FOLDER_BYTE,
                                                     .P_ATTACH_FOLDER_BYTE1 = concurrently.ATTACH_FOLDER_BYTE1,
                                                     .P_IS_CHUYEN = concurrently.IS_CHUYEN,
                                                     .P_CAP_TREN_TRUC_TIEP = concurrently.CAP_QUAN_LY_TT,
                                                     .P_MONEY = concurrently.MONEY,
                                                     .P_CHUCVU_CON = concurrently.CHUCVU_CON,
                                                     .BASIC1 = concurrently.BASIC1,
                                                     .P_STAFF_RANK_ID = concurrently.STAFF_RANK_ID,
                                                     .P_OUT = cls.OUT_CURSOR})

                Return Integer.Parse(dtData(0)("ID"))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UPDATE_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable
                dtData = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.UPDATE_CONCURRENTLY",
                                          New With {.P_ID = concurrently.ID,
                                                    .P_EMPLOYEE_ID = concurrently.EMPLOYEE_ID,
                                                    .P_ORG_ID = concurrently.ORG_ID,
                                                    .P_TITLE_ID = concurrently.TITLE_ID,
                                                    .P_ORG_CON = concurrently.ORG_CON,
                                                    .P_TITLE_CON = concurrently.TITLE_CON,
                                                    .P_EFFECT_DATE_CON = concurrently.EFFECT_DATE_CON,
                                                    .P_EXPIRE_DATE_CON = concurrently.EXPIRE_DATE_CON,
                                                    .P_ALLOW_MONEY = concurrently.ALLOW_MONEY,
                                                    .P_CON_NO = concurrently.CON_NO,
                                                    .P_STATUS = concurrently.STATUS,
                                                    .P_SIGN_DATE = concurrently.SIGN_DATE,
                                                    .P_SIGN_ID = concurrently.SIGN_ID,
                                                    .P_SIGN_TITLE = concurrently.SIGN_TITLE_NAME,
                                                    .P_SIGN_ID_2 = concurrently.SIGN_ID_2,
                                                    .P_REMARK = concurrently.REMARK,
                                                    .P_CON_NO_STOP = concurrently.CON_NO_STOP,
                                                    .P_SIGN_DATE_STOP = concurrently.SIGN_DATE_STOP,
                                                    .P_EFFECT_DATE_STOP = concurrently.EFFECT_DATE_STOP,
                                                    .P_STATUS_STOP = concurrently.STATUS_STOP,
                                                    .P_SIGN_ID_STOP = concurrently.SIGN_ID_STOP,
                                                    .P_SIGN_STOP_TITLE = concurrently.SIGN_TITLE_NAME_STOP,
                                                    .P_SIGN_ID_STOP_2 = concurrently.SIGN_ID_STOP_2,
                                                    .P_REMARK_STOP = concurrently.REMARK_STOP,
                                                    .P_CONCURENTLY_TYPE = concurrently.CONCURENTLY_TYPE,
                                                    .P_CONCURENTLY_POSITION = concurrently.CONCURENTLY_POSITION,
                                                    .P_CONCURENTLY_TYPE_CANCEL = concurrently.CONCURENTLY_TYPE_CANCEL,
                                                    .P_CREATED_BY = concurrently.CREATED_BY,
                                                    .P_CREATED_LOG = concurrently.CREATED_LOG,
                                                    .P_IS_ALLOW = concurrently.IS_ALLOW,
                                                    .P_FILE_BYTE = concurrently.FILE_BYTE,
                                                    .P_FILE_BYTE1 = concurrently.FILE_BYTE1,
                                                    .P_ATTACH_FOLDER_BYTE = concurrently.ATTACH_FOLDER_BYTE,
                                                    .P_ATTACH_FOLDER_BYTE1 = concurrently.ATTACH_FOLDER_BYTE1,
                                                    .P_IS_CHUYEN = concurrently.IS_CHUYEN,
                                                    .P_CAP_TREN_TRUC_TIEP = concurrently.CAP_QUAN_LY_TT,
                                                    .P_MONEY = concurrently.MONEY,
                                                    .P_CHUCVU_CON = concurrently.CHUCVU_CON,
                                                    .BASIC1 = concurrently.BASIC1,
                                                    .P_STAFF_RANK_ID = concurrently.STAFF_RANK_ID,
                                                    .P_OUT = cls.OUT_CURSOR})

                Return Integer.Parse(dtData(0)("ID"))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function GET_CONCURRENTLY_BY_EMP(ByVal P_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.GET_CONCURRENTLY_BY_EMP",
                                           New With {.P_EMPID = P_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GET_TITLE_ORG(ByVal P_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.GET_TITLE_ORG",
                                           New With {.P_ORGID = P_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DELETE_CONCURRENTLY(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstConcurrentlyData As List(Of HU_CONCURRENTLY)
        Try

            lstConcurrentlyData = (From p In Context.HU_CONCURRENTLY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstConcurrentlyData.Count - 1
                Context.HU_CONCURRENTLY.DeleteObject(lstConcurrentlyData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)

        Try
            Dim listCommend As New List(Of Temp_ConcurrentlyDTO)

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using

            ' Lấy toàn bộ dữ liệu theo employee
            Dim queryEmp = From p In Context.HU_CONCURRENTLY
                           From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID)
                           From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) e.ID = cv.EMPLOYEE_ID)
                           From org In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                           From huv_org_id In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                           From cty In Context.HU_ORGANIZATION.Where(Function(f) f.ID = huv_org_id.ID2).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = p.ORG_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                           From tc In Context.HU_TITLE.Where(Function(tc) tc.ID = p.TITLE_CON).DefaultIfEmpty
                           From org_con In Context.HU_ORGANIZATION.Where(Function(org_con) org_con.ID = p.ORG_CON).DefaultIfEmpty
                           From huv_con_org_id In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = p.ORG_CON).DefaultIfEmpty
                           From cty_con In Context.HU_ORGANIZATION.Where(Function(f) f.ID = huv_con_org_id.ID2).DefaultIfEmpty
                           From sign In Context.HU_EMPLOYEE.Where(Function(sign) sign.ID = p.SIGN_ID).DefaultIfEmpty
                           From sign_title In Context.HU_TITLE.Where(Function(sign_title) sign_title.ID = sign.TITLE_ID).DefaultIfEmpty
                           From sign_stop In Context.HU_EMPLOYEE.Where(Function(sign_stop) sign_stop.ID = p.SIGN_ID_STOP).DefaultIfEmpty
                           From sign_stop_title In Context.HU_TITLE.Where(Function(sign_stop_title) sign_stop_title.ID = sign_stop.TITLE_ID).DefaultIfEmpty
                           From sign2 In Context.HU_EMPLOYEE.Where(Function(sign2) sign2.ID = p.SIGN_ID_2).DefaultIfEmpty
                           From sign_title2 In Context.HU_TITLE.Where(Function(sign_title2) sign_title2.ID = sign2.TITLE_ID).DefaultIfEmpty
                           From cvkn In Context.OT_OTHER_LIST.Where(Function(cvkn) cvkn.ID = p.CHUCVU_CON).DefaultIfEmpty

            If Not _filter.IS_TERMINATE Then
                queryEmp = queryEmp.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing AndAlso _filter.EMPLOYEE_CODE <> "" Then
                queryEmp = queryEmp.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.EMPLOYEE_ID.HasValue Then
                queryEmp = queryEmp.Where(Function(p) p.e.ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.FULLNAME_VN IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.FULLNAME_VN.ToUpper))
            End If
            If _filter.STATUS IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.STATUS = _filter.STATUS)
            End If
            If _filter.TITLE_CON_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.tc.NAME_VN.ToUpper.Contains(_filter.TITLE_CON_NAME.ToUpper))
            End If
            If _filter.ORG_CON_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.org_con.NAME_VN.ToUpper.Contains(_filter.ORG_CON_NAME.ToUpper))
            End If
            If _filter.ALLOW_MONEY_NUMBER IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.ALLOW_MONEY = _filter.ALLOW_MONEY_NUMBER)
            End If

            ' danh sách thông tin khen thưởng nhân viên
            Dim lstEmp = queryEmp.Select(Function(p) New Temp_ConcurrentlyDTO With {
                                                       .ID = p.p.ID,
                                                       .CBSTATUS = Nothing,
                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                                       .ORG_ID = p.e.ORG_ID,
                                                       .ORG_NAME = p.o.NAME_VN,
                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                       .COM_ORG_NAME = p.cty.NAME_VN,
                                                       .TITLE_ID = p.e.TITLE_ID,
                                                       .TITLE_NAME = p.t.CODE & " - " & p.t.NAME_VN,
                                                       .ORG_CON_NAME = p.org_con.NAME_VN,
                                                       .ORG_CON = p.p.ORG_CON,
                                                       .TITLE_CON = p.p.TITLE_CON,
                                                       .TITLE_CON_NAME = p.tc.CODE & " - " & p.tc.NAME_VN,
                                                       .ALLOW_MONEY_NUMBER = p.p.ALLOW_MONEY,
                                                       .EFFECT_DATE_CON = p.p.EFFECT_DATE_CON,
                                                       .EXPIRE_DATE_CON = p.p.EXPIRE_DATE_CON,
                                                       .STATUS_NAME = If(p.p.STATUS Is Nothing, "", If(p.p.STATUS = 447, "Đã phê duyệt", "Chờ phê duyệt")),
                                                       .EFFECT_DATE_STOP = p.p.EFFECT_DATE_STOP,
                                                       .STATUS_STOP_NAME = If(p.p.STATUS_STOP Is Nothing, "", If(p.p.STATUS_STOP = 447, "Đã phê duyệt", "Chờ phê duyệt")),
                                                       .ORG_ID_DESC = p.org_con.DESCRIPTION_PATH,
                                                       .STATUS = p.p.STATUS,
                                                       .STATUS_STOP = p.p.STATUS_STOP,
                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                       .SIGN_NAME = p.sign.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME = p.sign_title.NAME_VN,
                                                       .SIGN_DATE = p.p.SIGN_DATE,
                                                       .SIGN_NAME2 = p.sign2.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME2 = p.sign_title2.NAME_VN,
                                                       .CHUCVU_CON_NAME = p.cvkn.NAME_VN,
                                                       .CON_NO = p.p.CON_NO
                                                       })

            lstEmp = lstEmp.OrderBy(Sorts)
            Total = lstEmp.Count
            lstEmp = lstEmp.Skip(PageIndex * PageSize).Take(PageSize)
            listCommend = lstEmp.ToList()
            For Each item In listCommend
                If IsNumeric(item.ORG_CON) Then
                    item.COM_ORG_CON_NAME = GetCompanyLevel(item.ORG_CON)
                End If
            Next
            Return listCommend
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        ByVal EMPLOYEE_ID As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)

        Try
            Dim listCommend As New List(Of Temp_ConcurrentlyDTO)

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = _filter.IS_DISSOLVE})
            End Using
            ' Lấy toàn bộ dữ liệu theo employee
            Dim queryEmp = From p In Context.HU_CONCURRENTLY
                           From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID)
                           From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) e.ID = cv.EMPLOYEE_ID)
                           From org In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                           From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                           From tc In Context.HU_TITLE.Where(Function(tc) tc.ID = p.TITLE_CON).DefaultIfEmpty
                           From org_con In Context.HU_ORGANIZATION.Where(Function(org_con) org_con.ID = p.ORG_CON).DefaultIfEmpty
                           From sign In Context.HU_EMPLOYEE.Where(Function(sign) sign.ID = p.SIGN_ID).DefaultIfEmpty
                           From sign_title In Context.HU_TITLE.Where(Function(sign_title) sign_title.ID = sign.TITLE_ID).DefaultIfEmpty
                           From sign_stop In Context.HU_EMPLOYEE.Where(Function(sign_stop) sign_stop.ID = p.SIGN_ID_STOP).DefaultIfEmpty
                           From sign_stop_title In Context.HU_TITLE.Where(Function(sign_stop_title) sign_stop_title.ID = sign_stop.TITLE_ID).DefaultIfEmpty
                           From sign2 In Context.HU_EMPLOYEE.Where(Function(sign2) sign2.ID = p.SIGN_ID_2).DefaultIfEmpty
                           From sign_title2 In Context.HU_TITLE.Where(Function(sign_title2) sign_title2.ID = sign2.TITLE_ID).DefaultIfEmpty
                           From sign_stop2 In Context.HU_EMPLOYEE.Where(Function(sign_stop2) sign_stop2.ID = p.SIGN_ID_STOP_2).DefaultIfEmpty
                           From sign_stop_title2 In Context.HU_TITLE.Where(Function(sign_stop_title2) sign_stop_title2.ID = sign_stop2.TITLE_ID).DefaultIfEmpty
                           Where p.EMPLOYEE_ID = EMPLOYEE_ID And p.STATUS = ProfileCommon.DECISION_STATUS.APPROVE_ID

            If Not _filter.IS_TERMINATE Then
                queryEmp = queryEmp.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing AndAlso _filter.EMPLOYEE_CODE <> "" Then
                queryEmp = queryEmp.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.FULLNAME_VN IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.FULLNAME_VN.ToUpper))
            End If
            If _filter.TITLE_CON_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.tc.NAME_VN.ToUpper.Contains(_filter.TITLE_CON_NAME.ToUpper))
            End If
            If _filter.ORG_CON_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.org_con.NAME_VN.ToUpper.Contains(_filter.ORG_CON_NAME.ToUpper))
            End If
            If _filter.ALLOW_MONEY_NUMBER IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.ALLOW_MONEY = _filter.ALLOW_MONEY_NUMBER)
            End If

            ' danh sách thông tin khen thưởng nhân viên
            Dim lstEmp = queryEmp.Select(Function(p) New Temp_ConcurrentlyDTO With {
                                                       .ID = p.p.ID,
                                                       .CBSTATUS = Nothing,
                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                                       .ORG_CON_NAME = p.org_con.NAME_VN,
                                                       .ORG_CON = p.p.ORG_CON,
                                                       .TITLE_CON = p.p.TITLE_CON,
                                                       .TITLE_CON_NAME = p.tc.CODE & " - " & p.tc.NAME_VN,
                                                       .ALLOW_MONEY_NUMBER = p.p.ALLOW_MONEY,
                                                       .EFFECT_DATE_CON = p.p.EFFECT_DATE_CON,
                                                       .EXPIRE_DATE_CON = p.p.EXPIRE_DATE_CON,
                                                       .STATUS_NAME = If(p.p.STATUS = 1, "Đã phê duyệt", "Chưa phê duyệt"),
                                                       .EFFECT_DATE_STOP = p.p.EFFECT_DATE_STOP,
                                                       .STATUS_STOP_NAME = If(p.p.STATUS_STOP = 1, "Đã phê duyệt", "Chưa phê duyệt"),
                                                       .ORG_ID_DESC = p.org_con.DESCRIPTION_PATH,
                                                       .STATUS = p.p.STATUS,
                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                       .SIGN_NAME = p.sign.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME = p.sign_title.NAME_VN,
                                                       .SIGN_NAME2 = p.sign2.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME2 = p.sign_title2.NAME_VN,
                                                       .SIGN_NAME_STOP = p.sign_stop.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME_STOP = p.sign_stop_title.NAME_VN,
                                                       .SIGN_NAME_STOP2 = p.sign_stop2.FULLNAME_VN,
                                                       .SIGN_TITLE_NAME_STOP2 = p.sign_stop_title2.NAME_VN,
                                                       .REMARK = p.p.REMARK
                                                       })

            lstEmp = lstEmp.OrderBy(Sorts)
            Total = lstEmp.Count
            lstEmp = lstEmp.Skip(PageIndex * PageSize).Take(PageSize)



            listCommend = lstEmp.ToList()

            Return listCommend
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function Validate_Concurrently(ByVal _validate As Temp_ConcurrentlyDTO)
        Dim query
        Try
            If _validate.ID <> 0 Then
                query = (From p In Context.HU_CONCURRENTLY
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                         Where p.EFFECT_DATE_CON = _validate.EFFECT_DATE_CON _
                         Where p.ORG_CON = _validate.ORG_CON _
                         Where p.TITLE_CON = _validate.TITLE_CON _
                         And p.ID <> _validate.ID).FirstOrDefault
            Else
                query = (From p In Context.HU_CONCURRENTLY
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                         Where p.EFFECT_DATE_CON = _validate.EFFECT_DATE_CON _
                         Where p.ORG_CON = _validate.ORG_CON _
                         Where p.TITLE_CON = _validate.TITLE_CON).FirstOrDefault
            End If
            If query IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function INSERT_EMPLOYEE_KN(ByVal P_EMPLOYEE_CODE As String,
                                       ByVal P_ORG_ID As Decimal,
                                       ByVal P_TITLE As Decimal,
                                       ByVal P_DATE As Date,
                                       ByVal P_ID_KN As Decimal,
                                       ByVal P_STAFF_RANK_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.INSERT_EMPLOYEE_KN",
                                           New With {.P_EMPLOYEE_CODE = P_EMPLOYEE_CODE,
                                                     .P_ORG_ID = P_ORG_ID,
                                                     .P_TITLE = P_TITLE,
                                                     .P_DATE = P_DATE,
                                                     .P_ID_KN = P_ID_KN,
                                                     .P_STAFF_RANK_ID = P_STAFF_RANK_ID
                                                     })

                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UPDATE_EMPLOYEE_KN(ByVal P_ID_KN As Decimal,
                                       ByVal P_DATE As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData = cls.ExecuteStore("PKG_HU_IPROFILE_CONCURRENTLY.UPDATE_EMPLOYEE_KN",
                                           New With {.P_ID_KN = P_ID_KN,
                                                     .P_DATE = P_DATE
                                                     })

                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetHU_OrgTreeview(ByVal isBlank As Boolean, ByVal userName As String, Optional ByVal allLevel As Decimal = 0) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                If allLevel = 0 Then
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_ORG_TREEVIEW",
                                           New With {.P_USERNAME = userName,
                                                     .P_ISBLANK = isBlank,
                                                     .P_CUR = cls.OUT_CURSOR})
                    Return dtData
                Else
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_ORG_TREEVIEW_ALL_LVL",
                                          New With {.P_USERNAME = userName,
                                                    .P_ISBLANK = isBlank,
                                                    .P_CUR = cls.OUT_CURSOR})
                    Return dtData
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "Quan ly file"
    Public Function GetFoldersAll() As List(Of FolderDTO)
        Dim lstFolders As New List(Of FolderDTO)
        lstFolders = (From p In Context.HU_FOLDERS
                      From parent In Context.HU_FOLDERS.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                      Order By p.ID Descending
                      Select New FolderDTO With {
                          .ID = p.ID,
                          .LINK = p.LINK,
                          .FOLDER_NAME = p.FOLDER_NAME,
                          .PARENT_ID = p.PARENT_ID,
                          .PARENT_NAME = parent.FOLDER_NAME}).ToList

        Return lstFolders
    End Function

    Public Function GetFoldersStructureInfo(ByVal _folderId As Decimal) As List(Of FolderDTO)
        Dim query As New FolderDTO
        Dim list As New List(Of FolderDTO)
        query.PARENT_ID = _folderId

        Do While query.PARENT_ID IsNot Nothing
            query = (From p In Context.HU_FOLDERS Where p.ID = query.PARENT_ID
                     Order By p.FOLDER_NAME
                     Select New FolderDTO With {
                     .ID = p.ID,
                     .FOLDER_NAME = p.FOLDER_NAME,
                     .PARENT_ID = p.PARENT_ID}).FirstOrDefault
            list.Add(query)
        Loop
        Return list
    End Function

    Public Function AddFolder(ByVal _folder As FolderDTO) As Integer
        Try
            If _folder.ID = 0 Then
                Dim check = (From p In Context.HU_FOLDERS Where p.FOLDER_NAME.ToUpper.Equals(_folder.FOLDER_NAME.ToUpper) And p.PARENT_ID = _folder.PARENT_ID).Count
                If check > 0 Then
                    Return 1
                Else
                    Dim objFolder As New HU_FOLDERS
                    objFolder.ID = Utilities.GetNextSequence(Context, Context.HU_FOLDERS.EntitySet.Name)
                    objFolder.FOLDER_NAME = _folder.FOLDER_NAME
                    objFolder.PARENT_ID = _folder.PARENT_ID
                    objFolder.LINK = _folder.LINK
                    Context.HU_FOLDERS.AddObject(objFolder)
                End If
            Else
                Dim check = (From p In Context.HU_FOLDERS Where p.FOLDER_NAME.ToUpper.Equals(_folder.FOLDER_NAME.ToUpper) And p.ID <> _folder.ID).Count
                If check > 0 Then
                    Return 1
                Else
                    Dim objFolder = (From p In Context.HU_FOLDERS Where p.ID = _folder.ID).FirstOrDefault
                    objFolder.FOLDER_NAME = _folder.FOLDER_NAME
                    objFolder.LINK = _folder.LINK
                End If
            End If
            Context.SaveChanges()
            Return 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function DeleteFolder(ByVal _id As Decimal) As Boolean
        Try
            Dim folder = (From p In Context.HU_FOLDERS Where p.ID = _id).FirstOrDefault
            Context.HU_FOLDERS.DeleteObject(folder)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetFolderByID(ByVal _id As Decimal) As FolderDTO
        Try
            Dim obj = (From p In Context.HU_FOLDERS Where p.ID = _id
                       Select New FolderDTO With {
                            .ID = p.ID,
                            .FOLDER_NAME = p.FOLDER_NAME,
                            .PARENT_ID = p.PARENT_ID,
                            .LINK = p.LINK
                        }).FirstOrDefault
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetFileOfFolder(ByVal _filter As UserFileDTO,
                                    ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal log As UserLog = Nothing,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc"
                                    ) As List(Of UserFileDTO)
        Try
            Dim query = (From p In Context.HU_USERFILES Where p.FOLDER_ID = _filter.FOLDER_ID.Value
                         Select New UserFileDTO With {
                             .ID = p.ID,
                             .NAME = p.NAME,
                             .FILE_NAME = p.FILE_NAME,
                             .FOLDER_ID = p.FOLDER_ID,
                             .CREATED_BY = p.CREATED_BY,
                             .CREATED_DATE = p.CREATED_DATE,
                             .DESCRIPTION = p.DESCRIPTION,
                             .CREATED_LOG = p.CREATED_LOG})

            If _filter.NAME IsNot Nothing Then
                query = query.Where(Function(f) f.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.DESCRIPTION IsNot Nothing Then
                query = query.Where(Function(f) f.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
            End If
            If IsDate(_filter.CREATED_DATE) Then
                query = query.Where(Function(f) f.CREATED_DATE = _filter.CREATED_DATE)
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)

            Dim listFiles As New List(Of UserFileDTO)
            listFiles = query.ToList()
            Return listFiles
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function AddUserFile(ByVal _userFile As UserFileDTO) As Decimal
        Try
            If _userFile.ID > 0 Then
                Dim objData = (From p In Context.HU_USERFILES
                               Where p.ID = _userFile.ID).FirstOrDefault
                objData.NAME = _userFile.NAME
                objData.FOLDER_ID = _userFile.FOLDER_ID
                objData.FILE_NAME = _userFile.FILE_NAME
                objData.DESCRIPTION = _userFile.DESCRIPTION
                objData.CREATED_BY = _userFile.CREATED_BY
                objData.LINK = _userFile.LINK
            Else
                Dim check = (From p In Context.HU_USERFILES Where p.NAME.ToUpper.Equals(_userFile.NAME.ToUpper) And p.FOLDER_ID = _userFile.FOLDER_ID).Count
                If check > 0 Then
                    Return 1
                Else
                    Dim objUserFile As New HU_USERFILES
                    objUserFile.ID = Utilities.GetNextSequence(Context, Context.HU_USERFILES.EntitySet.Name)
                    objUserFile.NAME = _userFile.NAME
                    objUserFile.FOLDER_ID = _userFile.FOLDER_ID
                    objUserFile.FILE_NAME = _userFile.FILE_NAME
                    objUserFile.DESCRIPTION = _userFile.DESCRIPTION
                    objUserFile.CREATED_BY = _userFile.CREATED_BY
                    objUserFile.LINK = _userFile.LINK
                    objUserFile.CREATED_DATE = DateTime.Now()
                    Context.HU_USERFILES.AddObject(objUserFile)
                End If
            End If

            Context.SaveChanges()
            Return 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteUserFile(ByVal _id As Decimal) As Boolean
        Try
            Dim UserFile = (From p In Context.HU_USERFILES Where p.ID = _id).FirstOrDefault
            Context.HU_USERFILES.DeleteObject(UserFile)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetUserFileByID(ByVal _id As Decimal) As UserFileDTO
        Try
            Dim obj = (From p In Context.HU_USERFILES Where p.ID = _id
                       Select New UserFileDTO With {
                            .ID = p.ID,
                            .NAME = p.NAME,
                            .FOLDER_ID = p.FOLDER_ID,
                            .DESCRIPTION = p.DESCRIPTION,
                            .LINK = p.LINK,
                            .FILE_NAME = p.FILE_NAME,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE
                        }).FirstOrDefault
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

    Public Function CheckExistApproval(ByVal EmpID As Decimal, ByVal Table As String, ByVal Fk_Pkey As Decimal) As Boolean
        Try
            If Table = "HU_WORKING_BEFORE_EDIT" Then
                Return (From p In Context.HU_WORKING_BEFORE_EDIT Where p.EMPLOYEE_ID = EmpID And (p.STATUS = "0" Or p.STATUS = "1") And p.FK_PKEY = Fk_Pkey).Any
            End If

            If Table = "HU_FAMILY_EDIT" Then
                Return (From p In Context.HU_FAMILY_EDIT Where p.EMPLOYEE_ID = EmpID And (p.STATUS = "0" Or p.STATUS = "1") And p.FK_PKEY = Fk_Pkey).Any
            End If

            If Table = "HU_PRO_TRAIN_OUT_COMPANY_EDIT" Then
                Return (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.EMPLOYEE_ID = EmpID And (p.STATUS = "0" Or p.STATUS = "1") And p.FK_PKEY = Fk_Pkey).Any
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
End Class
