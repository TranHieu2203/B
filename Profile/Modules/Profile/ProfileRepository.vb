Imports System.IO
Imports Framework.UI
Imports Profile.ProfileBusiness
'Imports Framework.Data

Partial Public Class ProfileRepository
    Inherits ProfileRepositoryBase

    Dim CacheMinusDataCombo As Integer = 30
#Region "VALIDATE BUSINESS"
    Public Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Try
                    Return rep.ValidateBusiness(table, column, lstID)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Other"
    Public Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCurrentPeriod(_year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Contract appendix"
    Public Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_NEXT_APPENDIX_ORDER(id, contract_id, emp_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Common"

    Public Function GetEmployeeGroup() As DataTable
        Using rep As New ProfileBusinessClient
            Return rep.GetEmployeeGroup()
        End Using
        Return Nothing
    End Function

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        'Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            'Try
            '    dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
            '    If dtData Is Nothing Then
            '        dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
            '    End If
            '    CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
            '    Return dtData
            'Catch ex As Exception
            '    rep.Abort()
            '    Throw ex
            'End Try
            Return rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
        End Using

        Return Nothing
    End Function

    Public Function GET_DEFAULT_OBJECT_ATTENDANCE() As Integer
        Using rep As New ProfileBusinessClient
            Return rep.GET_DEFAULT_OBJECT_ATTENDANCE()
        End Using

        Return Nothing
    End Function

    Public Function GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ByVal ID As Decimal?) As DataTable


        Using rep As New ProfileBusinessClient

            Return rep.GetINS_LIST_CONTRACT_DETAIL_BY_ID_COMBOBOX(ID)
        End Using

        Return Nothing
    End Function
    Public Function GetINS_Contract_Combobox(Optional ByVal Is_Full As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient

            Return rep.GetINS_Contract_Combobox(Is_Full)
        End Using

        Return Nothing
    End Function
    Public Function GetInsListWhereHealth() As DataTable
        Using rep As New ProfileBusinessClient

            Return rep.GetInsListWhereHealth()
        End Using

        Return Nothing
    End Function

    Public Function Get_HU_WORK_PLACE(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        'Dim dtData As DataTable

        Using rep As New ProfileBusinessClient

            Return rep.Get_HU_WORK_PLACE(sType, Common.Common.SystemLanguage.Name, isBlank)
        End Using

        Return Nothing
    End Function

    Public Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal) As DataTable
        'Dim dtData As DataTable

        Using rep As New ProfileBusinessClient

            Return rep.HU_PAPER_LIST(P_EMP_ID, Common.Common.SystemLanguage.Name)
        End Using

        Return Nothing
    End Function

    Public Function GetBankList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_BANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetBankList(isBlank)
                End If
                CacheManager.Insert("OT_HU_BANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetBankBranchList(ByVal bankID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetBankBranchList(bankID, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    'Public Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, Optional ByVal isBlank As Boolean = False) As DataTable
    '    Dim dtData As DataTable

    '    Using rep As New ProfileBusinessClient
    '        Try
    '            dtData = rep.GetTitleByOrgID(orgID, Common.Common.SystemLanguage.Name, True)
    '            Return dtData
    '        Catch ex As Exception
    '            rep.Abort()
    '            Throw ex
    '        End Try
    '    End Using

    '    Return Nothing
    'End Function
    Public Function GetTitleByOrgID(ByVal orgID As Decimal, Optional ByVal isBlank As Boolean = False, Optional ByVal Employee_ID As Decimal = 0) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetTitleByOrgID(orgID, Common.Common.SystemLanguage.Name, isBlank, Employee_ID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTitleList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetTitleList(Common.Common.SystemLanguage.Name, True)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetWardList(ByVal districtID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_WARD_LIST_" & districtID & "_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetWardList(districtID, isBlank)
                End If
                CacheManager.Insert("OT_HU_WARD_LIST_" & districtID & "_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDistrictList(ByVal provinceID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetDistrictList(provinceID, isBlank)
                End If
                CacheManager.Insert("OT_HU_DISTRICT_LIST_" & provinceID & "_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvinceList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetProvinceList(isBlank)
                End If
                CacheManager.Insert("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                'dtData = CacheManager.GetValue("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                dtData = rep.GetProvinceList1(P_NATIVE, isBlank)
                'CacheManager.Insert("OT_HU_PROVINCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetProvinceList2(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetProvinceList2(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetNationList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_NATION_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetNationList(isBlank)
                End If
                CacheManager.Insert("OT_HU_NATION_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetStaffRankList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_STAFF_RANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetStaffRankList(isBlank)
                End If
                CacheManager.Insert("OT_HU_STAFF_RANK_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

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

    Public Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSalaryGroupList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetSalaryTypeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSalaryTypeList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_Hu_Allowance_List(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GET_Hu_Allowance_List(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetSaleCommisionList(dateValue, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryLevelList(salGroupID, isBlank)
                End If
                CacheManager.Insert("OT_PA_SALARY_LEVEL_LIST_" & salGroupID & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetSalaryRankList(salLevelID, isBlank)
                End If
                CacheManager.Insert("OT_PA_SALARY_RANK_LIST_" & salLevelID & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_AllowanceList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_HU_ALLOWANCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetHU_AllowanceList(isBlank)
                End If
                CacheManager.Insert("OT_HU_ALLOWANCE_LIST_" & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetPA_ObjectSalary(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPA_ObjectSalary(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_WageTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_WageTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_MissionTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_MissionTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOT_TripartiteTypeList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetOT_TripartiteTypeList(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_TemplateType(isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function SaveTemplateFileHost(ByVal fByte As Byte(), ByVal folder As String, ByVal filename As String) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Dim result = rep.SaveTemplateFileHost(fByte, folder, filename)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetTemplateFileHost(ByVal path As String) As Byte()


        Using rep As New ProfileBusinessClient
            Try
                Dim result = rep.GetTemplateFileHost(path)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ExistsTemplateFileHost(ByVal path As String) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Dim result = rep.ExistsTemplateFileHost(path)
                Return result
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                           ByVal isTemplateType As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_MergeFieldList(isBlank, isTemplateType)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                           ByVal isTemplateType As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_TemplateList(isBlank, isTemplateType)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetHU_DataDynamic_Muti_1(ByVal dID As String,
                                             ByVal de As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamic_Muti_1(dID, de, tempID, folderName)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetHU_DataDynamic_Muti(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamic_Muti(dID, tempID, folderName)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamic(dID, tempID, folderName)

                If Not dtData.Columns.Contains("IMAGE") Then
                    dtData.Columns.Add("IMAGE", GetType(Byte()))
                Else
                    If dtData.Rows(0)("IMAGE").ToString <> "" Then
                        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                        dtData.Columns.Remove("IMAGE")
                        dtData.Columns.Add("IMAGE", GetType(Byte()))
                        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                        If Not Directory.Exists(logoPath) Then
                            Directory.CreateDirectory(logoPath)
                        End If
                        Dim logo = org_id2 & "_*"
                        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                        If dirs.Length > 0 Then
                            Using FileStream = File.Open(dirs(0), FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        Else
                            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                            Using FileStream = File.Open(logoPath, FileMode.Open)
                                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                                Using ms As New MemoryStream
                                    FileStream.CopyTo(ms)
                                    dtData.Rows(0)("IMAGE") = ms.ToArray
                                End Using
                            End Using
                        End If
                    End If
                End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_MultyDataDynamic(strID, tempID, folderName)

                'If Not dtData.Columns.Contains("IMAGE") Then
                '    dtData.Columns.Add("IMAGE", GetType(Byte()))
                'Else
                '    If dtData.Rows(0)("IMAGE").ToString <> "" Then
                '        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                '        dtData.Columns.Remove("IMAGE")
                '        dtData.Columns.Add("IMAGE", GetType(Byte()))
                '        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                '        If Not Directory.Exists(logoPath) Then
                '            Directory.CreateDirectory(logoPath)
                '        End If
                '        Dim logo = org_id2 & "_*"
                '        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                '        If dirs.Length > 0 Then
                '            Using FileStream = File.Open(dirs(0), FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        Else
                '            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                '            Using FileStream = File.Open(logoPath, FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        End If
                '    End If
                'End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    '' In hop dong hang loat
    Public Function GetHU_DataDynamicContract(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamicContract(dID, tempID, folderName)
                'If Not dtData.Columns.Contains("IMAGE") Then
                '    dtData.Columns.Add("IMAGE", GetType(Byte()))
                'Else
                '    If dtData.Rows(0)("IMAGE").ToString <> "" AndAlso IsNumeric(dtData.Rows(0)("IMAGE")) Then
                '        Dim org_id2 As Decimal = dtData.Rows(0)("IMAGE")
                '        dtData.Columns.Remove("IMAGE")
                '        dtData.Columns.Add("IMAGE", GetType(Byte()))
                '        Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                '        If Not Directory.Exists(logoPath) Then
                '            Directory.CreateDirectory(logoPath)
                '        End If
                '        Dim logo = org_id2 & "_*"
                '        Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                '        If dirs.Length > 0 Then
                '            Using FileStream = File.Open(dirs(0), FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        Else
                '            logoPath = AppDomain.CurrentDomain.BaseDirectory & "Static\logo_default.png"
                '            Using FileStream = File.Open(logoPath, FileMode.Open)
                '                Dim fileContent As Byte() = New Byte(FileStream.Length) {}
                '                Using ms As New MemoryStream
                '                    FileStream.CopyTo(ms)
                '                    dtData.Rows(0)("IMAGE") = ms.ToArray
                '                End Using
                '            End Using
                '        End If
                '    End If
                'End If

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String,
                                       Optional ByVal isLogo As Boolean = True) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetHU_DataDynamicContractAppendix(dID, tempID, folderName)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommonTABLE_NAME) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.AutoGenCode(firstChar, tableName, colName)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateMergeField(lstData, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDataPrintBBBR(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetDataPrintBBBR3B(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetInsRegionList(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInsRegionList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_CompetencyGroupList(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyGroupList(isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetHU_CompetencyList(ByVal groupID As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyList(groupID, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHU_CompetencyPeriodList(ByVal year As Decimal, Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_CompetencyPeriodList(year, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function



#Region "Get combobox Data"
    Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try

                Return rep.GetComboList(_combolistDTO, Me.Log)

            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Service Auto Update Employee Information"
    Public Function CheckAndUpdateEmployeeInformation() As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckAndUpdateEmployeeInformation
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "Service Send Mail Reminder"
    Public Function CheckAndSendMailReminder() As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckAndSendMailReminder
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#End Region

#Region "Setting"

#Region "Organization"

    Public Function GetOrganization(Optional ByVal sACT As String = "") As List(Of OrganizationDTO)
        Dim lstOrganization As List(Of OrganizationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrganization = rep.GetOrganization(sACT)
                Return lstOrganization
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO
        Using rep As New ProfileBusinessClient
            Try

                Return rep.GetOrganizationByID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOrganization(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrganization(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateOrganization(ByVal objOrganization As OrganizationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateOrganization(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCostCenterCode(ByVal objOrganization As OrganizationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCostCenterCode(objOrganization)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckEmployeeInOrganization(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOrganization(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrganization(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrganizationPath(lstPath)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function ActiveOrganization(ByVal lstOrganization As List(Of OrganizationDTO), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveOrganization(lstOrganization, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateUy_Ban_Organization(ByVal lstOrganization As List(Of OrganizationDTO), ByVal sValue As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateUy_Ban_Organization(lstOrganization, sValue, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "OrgTitle"

    Public Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)
        Dim lstOrgTitle As List(Of OrgTitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrgTitle = rep.GetOrgTitle(filter, PageIndex, PageSize, Total, Sorts)
                Return lstOrgTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOrgTitle(ByVal lstOrgTitle As List(Of OrgTitleDTO), Optional ByRef gID As Decimal = Nothing) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertOrgTitle(lstOrgTitle, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckTitleInEmployee(lstID, orgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteOrgTitle(ByVal lstOrgTitle As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteOrgTitle(lstOrgTitle, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveOrgTitle(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveOrgTitle(lstOrgTitle, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveTalentPool(ByVal lst As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveTalentPool(lst, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Function GetTalentPool(ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TalentPoolDTO)

        Dim lst As List(Of TalentPoolDTO)

        Using rep As New ProfileBusinessClient
            Try
                lst = rep.GetTalentPool(PageIndex, PageSize, Total, _param, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#End Region

#Region "Hoadm"
    Public Function GetOrgFromUsername(ByVal username As String) As Decimal?
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgFromUsername(username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetLineManager(username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "BÁO CÁO"
    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Dim lstTitle As List(Of Se_ReportDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetReportById(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet
        Dim dtData As DataSet

        Using rep As New ProfileBusinessClient
            Try

                'If dtData Is Nothing Then
                dtData = rep.GetEmployeeCVByID(sPkgName, sEmployee_id)
                'End If
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ExportReport(ByVal sPkgName As String,
                                 ByVal sStartDate As Date?,
                                 ByVal sEndDate As Date?,
                                 ByVal sOrg As String,
                                 ByVal IsDissolve As Integer,
                                 ByVal sLang As String) As DataSet
        Dim dtData As DataSet

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.ExportReport(sPkgName, sStartDate, sEndDate, sOrg, IsDissolve, Log.Username.ToUpper, Common.Common.SystemLanguage.Name)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "FILTER TALENT POOL HONGDX"
    Public Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO) As DataTable
        Dim dt As DataTable
        Try
            Using rep As New ProfileBusinessClient
                Try
                    dt = rep.FILTER_TALENT_POOL(obj, Me.Log)
                    Return dt
                Catch ex As Exception
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO)) As Boolean
        Try
            Using rep As New ProfileBusinessClient
                Try
                    Return rep.InsertTalentPool(lstTalentPool, Me.Log)
                Catch ex As Exception
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "in phụ lục hợp đồng, hợp đồng"
    Public Function PrintFileContract(ByVal emp_Code As String, ByVal fileContract_ID As String) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.PrintFileContract(emp_Code, fileContract_ID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFileConTractID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of FileContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractAppendixPaging(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmpDtlContractAppendixPaging(ByVal _filter As FileContractDTO,
                                                    Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of FileContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmpDtlContractAppendixPaging(_filter, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteFileContract(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteFileContract(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListContractType(type)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExpireFileContract(StartDate, EndDate, ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal ID As Decimal) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistFileContract(empID, StartDate, ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertFileContract(ByVal fileInfo As FileContractDTO, ByRef gID As Decimal, ByRef appNum As String) As Boolean


        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertFileContract(fileInfo, Me.Log, gID, appNum)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateFileContract(ByVal fileInfo As FileContractDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateFileContract(fileInfo, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractList(empID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetTitileBaseOnEmp(ByVal ID As Decimal) As Profile.ProfileBusiness.TitleDTO
        Dim lstTitle As Profile.ProfileBusiness.TitleDTO
        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetTitileBaseOnEmp(ID)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String


        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFileContract_No(Contract, STT)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractAppendix(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetContractTypeID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetListContractBaseOnEmp(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetListContract(ByVal ID As Decimal) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetListContract(ID)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_PROCESS_PLCONTRACT(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function EXPORT_PLHD(ByVal _param As ParamDTO) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EXPORT_PLHD(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function EXPORT_CV(ByVal _param As ParamDTO) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EXPORT_CV(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function EXPORT_CONGNO(ByVal _param As ParamDTO) As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EXPORT_CONGNO(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_EMPLOYEE(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_CONTRACT(P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_CONTRACT_BY_EMP_CODE(ByVal P_ID As Decimal, ByVal P_EMPID As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_CONTRACT_BY_EMP_CODE(P_ID, P_EMPID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_SALARY(P_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_CONTRACT_EXITS(P_CONTRACT, P_EMP_CODE, P_DATE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CHECK_SIGN(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function INPORT_PLHD(ByVal P_DOCXML As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.INPORT_PLHD(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_CONGNO(ByVal P_DOCXML As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.IMPORT_CONGNO(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function IMPORT_NV(ByVal P_DOCXML As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.IMPORT_NV(P_DOCXML, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_PROCESS_PLCONTRACT_PORTAL(P_EMP_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "CTNN"

    Public Function GetAbroads(ByVal _filter As HUAbroadDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUAbroadDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAbroads(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetAbroads(ByVal _filter As HUAbroadDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUAbroadDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetAbroads(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertAbroad(objAbroad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyAbroad(objAbroad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateAbroad(ByVal objAbroad As HUAbroadDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateAbroad(objAbroad)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteAbroad(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteAbroad(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_ABROAD_DATA_IMPORT() As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_ABROAD_DATA_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function IMPORT_ABROAD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.IMPORT_ABROAD(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
#Region "bhld"
    Public Function getOrgName(ByVal id As Decimal) As String
        Using rep As New ProfileBusinessClient
            Try
                Return rep.getOrgName(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function excel_BHLD_portal(ByVal year As Decimal, ByVal dts As String, ByVal log As Common.CommonBusiness.UserLog, ByVal status As String) As Byte()
        Using rep As New ProfileBusinessClient
            Try

                Return rep.excel_BHLD_portal(year, dts, log, status)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function saveBdldPortal(ByVal year As Decimal, ByVal dt As String, ByVal log As Common.CommonBusiness.UserLog, ByVal is_send As Decimal) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.saveBdldPortal(year, dt, log, is_send)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function saveBHLD(ByVal year As Decimal, ByVal lst_col As List(Of String), ByVal dt As DataTable, ByVal is_import As Boolean) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.saveBHLD(year, lst_col, dt, is_import, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function getListCol() As List(Of BHLDDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.getListCol()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Excel_DK_PD(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO, ByVal type As String,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As Common.CommonBusiness.UserLog = Nothing) As Byte()
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Excel_DK_PD(year, empcode, statusId, PageIndex, PageSize, Total, _param, type, Sorts, log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetBHLD_Approve(ByVal year As Integer, ByVal empcode As String, ByVal statusId As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetBHLD_Approve(year, empcode, statusId, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetBHLD_Register(ByVal year As Integer, ByVal empcode As String, ByVal statusId As Decimal, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetBHLD_Register(year, empcode, statusId, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetBHLD(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetBHLD(year, empcode, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetBHLD1(ByVal year As Integer, ByVal empcode As String, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetBHLD1(year, empcode, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CalculateBHLD(ByVal year As Integer, ByVal _param As ParamDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CalculateBHLD(year, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Travel"

    Public Function GetTravels(ByVal _filter As HUTravelDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUTravelDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTravels(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetTravels(ByVal _filter As HUTravelDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUTravelDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetTravels(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertTravel(ByVal objTravel As HUTravelDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTravel(objTravel, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyTravel(ByVal objTravel As HUTravelDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyTravel(objTravel, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateTravel(ByVal objTravel As HUTravelDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateTravel(objTravel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteTravel(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteTravel(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_TRAVEL_DATA_IMPORT() As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_TRAVEL_DATA_IMPORT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function IMPORT_TRAVEL(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.IMPORT_TRAVEL(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "InfoConfirm"

    Public Function GetInfoConfirms(ByVal _filter As HUInfoConfirmDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUInfoConfirmDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInfoConfirms(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetInfoConfirms(ByVal _filter As HUInfoConfirmDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HUInfoConfirmDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInfoConfirms(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertInfoConfirm(objInfoConfirm, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyInfoConfirm(ByVal objInfoConfirm As HUInfoConfirmDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyInfoConfirm(objInfoConfirm, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteInfoConfirm(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteInfoConfirm(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetInfoConfirmPrintData(ByVal _id As Decimal) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetInfoConfirmPrintData(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "CB Planning"

    Public Function GetCBPlannings(ByVal _filter As CBPlanningDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CBPlanningDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBPlannings(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetCBPlannings(ByVal _filter As CBPlanningDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CBPlanningDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBPlannings(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCBPlanning(ByVal _id As Decimal) As CBPlanningDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBPlanning(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCBPlanning(ByVal objCBPlanning As CBPlanningDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCBPlanning(objCBPlanning, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyCBPlanning(ByVal objCBPlanning As CBPlanningDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCBPlanning(objCBPlanning, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteCBPlanning(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCBPlanning(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function CopyCBPlanning(ByVal _id As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CopyCBPlanning(_id, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCBPlanningsHistory(ByVal _filter As CBPlanningEmpHisDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CBPlanningEmpHisDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBPlanningsHistory(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

#End Region

#Region "Commitee"

    Public Function GetCommitees(ByVal _filter As CommiteeDTO,
                                 ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommiteeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommitees(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetCommitees(ByVal _filter As CommiteeDTO, ByVal _param As ParamDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommiteeDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommitees(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetCommitee(ByVal _id As Decimal) As CommiteeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommitee(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertCommitee(ByVal objCommitee As CommiteeDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCommitee(objCommitee, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function ModifyCommitee(ByVal objCommitee As CommiteeDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCommitee(objCommitee, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function DeleteCommitee(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCommitee(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function CopyCommitee(ByVal _id As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CopyCommitee(_id, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCommiteesHistory(ByVal _filter As CommiteeEmpDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommiteeEmpDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommiteesHistory(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

#End Region

#Region "CB Assessment"

    Public Function GetCBAssessments(ByVal _filter As CBAssessmentDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CBAssessmentDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBAssessments(_filter, PageIndex, PageSize, Total, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetCBAssessments(ByVal _filter As CBAssessmentDTO, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CBAssessmentDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBAssessments(_filter, 0, Integer.MaxValue, 0, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCBAssessment(ByVal _id As Decimal) As CBAssessmentDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCBAssessment(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCBAssessment(ByVal objCBAssessment As CBAssessmentDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCBAssessment(objCBAssessment, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifyCBAssessment(ByVal objCBAssessment As CBAssessmentDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCBAssessment(objCBAssessment, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteCBAssessment(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCBAssessment(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Emp NPT"

    Public Function GetEmployeeNPTs(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer, ByVal _param As ParamDTO,
                                    Optional ByVal Sorts As String = "EMPLOYEE_CODE asc") As List(Of FamilyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeNPTs(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeNPTs(ByVal _filter As FamilyDTO, ByVal _param As ParamDTO, Optional ByVal Sorts As String = "EMPLOYEE_CODE asc") As List(Of FamilyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeNPTs(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function IMPORT_EMPPLOYEE_NPT(ByVal P_DOCXML As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.IMPORT_EMPPLOYEE_NPT(P_DOCXML, Me.Log.Username.ToUpper)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
#Region "OM"
    Public Function Deletejob(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Deletejob(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Activejob(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Activejob(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckJobExistInTitle(JobId As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckJobExistInTitle(JobId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Getjob(ByVal _filter As JobDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal Language As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobDTO)
        Dim lstjob As List(Of JobDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstjob = rep.Getjob(_filter, PageIndex, PageSize, Total, Common.Common.SystemLanguage.Name, Sorts)
                Return lstjob
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function Getjob(ByVal _filter As JobDTO,
                                        ByVal Language As String,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of JobDTO)
        Dim lstjob As List(Of JobDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstjob = rep.Getjob(_filter, 0, Integer.MaxValue, 0, Common.Common.SystemLanguage.Name, Sorts)
                Return lstjob
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetOrgTreeList(Optional ByVal sACT As String = "") As List(Of OrganizationDTO)
        Dim lstOrganization As List(Of OrganizationDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrganization = rep.GetOrgTreeList(Me.Log.Username, sACT)
                'lstOrganization = rep.GetOrgTreeList(sACT)
                Return lstOrganization
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgTreeEmp(Optional ByVal sACT As String = "") As List(Of OrganizationDTO)
        Dim lstOrganization As List(Of OrganizationDTO)

        Using rep As New ProfileBusinessClient
            Try
                '// Lay ten class/function
                Dim FunctionName As String = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name.ToString() & "_" &
                                            System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
                '// Generate token
                'Dim Token = TokenHelper.GenerateToken(LogHelper.GetUserLog.SessionID, LogHelper.GetUserLog.USERNAME, LogHelper.GetUserLog.EMPLOYEE_ID, LogHelper.GetUserLog.EMPLOYEE_CODE, FunctionName, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString())
                lstOrganization = rep.GetOrgTreeEmp(sACT)
                Return lstOrganization
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ModifyOrgTreeEmp(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrgTreeEmp(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveOrgTreeList(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                '// Lay ten class/function
                Dim FunctionName As String = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name.ToString() & "_" &
                                            System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
                '// Generate token
                ' Dim Token = TokenHelper.GenerateToken(LogHelper.GetUserLog.SessionID, LogHelper.GetUserLog.USERNAME, LogHelper.GetUserLog.EMPLOYEE_ID, LogHelper.GetUserLog.EMPLOYEE_CODE, FunctionName, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString())
                Return rep.ActiveOrgTreeList(lstOrgTitle, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetDataByProcedures(ByVal isBank As Decimal, Optional ByVal ID As Decimal = 0, Optional ByVal Name As String = "", Optional ByVal sLang As String = "vi-VN") As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Dim FunctionName As String = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name.ToString() & "_" &
                                            System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

                Return rep.GetDataByProcedures(isBank, ID, Name, sLang)
            Catch ex As Exception

                Throw ex
            End Try
        End Using
    End Function
    Public Function ModifyOrgTreeList(ByVal objOrganization As OrganizationDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyOrgTreeList(objOrganization, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetPossition(ByVal _filter As TitleDTO,
                                 ByVal _param As ParamDTO,
                                 ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                Optional ByVal IsReadWrite As Boolean = False,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPossition(_filter, _param, Common.Common.SystemLanguage.Name, PageIndex, PageSize, Total, IsReadWrite, Sorts, Me.Log)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function GetPossition2(ByVal _filter As TitleDTO,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal IsReadWrite As Boolean = False,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPossition2(_filter, _param, Common.Common.SystemLanguage.Name, 0, Integer.MaxValue, 0, IsReadWrite, Sorts, Me.Log)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetPossition(ByVal _filter As TitleDTO,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal IsReadWrite As Boolean = False,
                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)
        Dim lstTitle As List(Of TitleDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstTitle = rep.GetPossition(_filter, _param, Common.Common.SystemLanguage.Name, 0, Integer.MaxValue, 0, IsReadWrite, Sorts, Me.Log)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertOrgTreeList(ByVal objOrganization As OrganizationDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                '// Lay ten class/function
                Return rep.InsertOrgTreeList(objOrganization, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Insertjob(ByVal objjob As JobDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Insertjob(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Validatejob(ByVal objjob As JobDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Validatejob(objjob)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Modifyjob(ByVal objjob As JobDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.Modifyjob(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetjobID(ByVal ID As Decimal) As Profile.ProfileBusiness.JobDTO
        Dim lstjob As Profile.ProfileBusiness.JobDTO

        Using rep As New ProfileBusinessClient
            Try
                lstjob = rep.GetjobID(ID)
                Return lstjob
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ValidateJobCode(ByVal objTitle As JobDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateJobCode(objTitle)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetjobFunctionByJobID(ByVal ID As Decimal) As List(Of JobFunctionDTO)
        Dim lstjob As List(Of JobFunctionDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstjob = rep.GetjobFunctionByJobID(ID)
                Return lstjob
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertjobFunction(ByVal objjob As JobFunctionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertjobFunction(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyjobFunction(ByVal objjob As JobFunctionDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyjobFunction(objjob, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeletejobFunction(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeletejobFunction(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function SwapMasterInterim(ByVal _Id As Decimal, ByVal type As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SwapMasterInterim(_Id, Me.Log, type)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertTitleNB(ByVal obj As TitleDTO, ByVal OrgRight As Decimal, ByVal Address As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertTitleNB(obj, OrgRight, Address, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function ActiveOrgEmp(ByVal lstOrgTitle As List(Of Decimal), ByVal sActive As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveOrgEmp(lstOrgTitle, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyJobPosTreeList(ByVal objJobPositinTree As JobPositinTreeDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyJobPosTreeList(objJobPositinTree, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function JobPossitionRptHist(ByVal RptMonth As Date) As List(Of JobPositinTreeDTO)
        Dim lstJobPosTree As List(Of JobPositinTreeDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstJobPosTree = rep.JobPossitionRptHist(RptMonth, Common.Common.SystemLanguage.Name, Me.Log)
                Return lstJobPosTree
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetJobChileTree(ByVal job_Id As Decimal) As List(Of JobChildTreeDTO)
        Dim lstJobChildTree As List(Of JobChildTreeDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstJobChildTree = rep.GetJobChileTree(job_Id, Common.Common.SystemLanguage.Name)
                Return lstJobChildTree
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function OrgChartRpt(ByVal Language As String) As List(Of OrgChartRptDTO)
        Dim lstOrganization As List(Of OrgChartRptDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstOrganization = rep.OrgChartRpt(Language, Me.Log)
                Return lstOrganization
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetJobPosTree() As List(Of JobPositinTreeDTO)
        Dim lstJobPosTree As List(Of JobPositinTreeDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstJobPosTree = rep.GetJobPosTree(Common.Common.SystemLanguage.Name, Me.Log)
                Return lstJobPosTree
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "Salary Items Percent"

    Public Function GetSalItemsPercent(ByVal _filter As SalaryItemsPercentDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryItemsPercentDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetSalItemsPercent(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetPaymentListAll(Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertSalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertSalItemsPercent(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ModifySalItemsPercent(ByVal obj As SalaryItemsPercentDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifySalItemsPercent(obj, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ValidateSalItemsPercent(ByVal obj As SalaryItemsPercentDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateSalItemsPercent(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function DeleteSalItemsPercent(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteSalItemsPercent(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function ActiveSalItemsPercent(ByVal lstID As List(Of Decimal), ByVal status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveSalItemsPercent(lstID, status, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
    Public Function GetHU_OrgTreeview(ByVal isBlank As Boolean, Optional ByVal allLevel As Decimal = 0) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHU_OrgTreeview(isBlank, Log.Username.ToUpper, allLevel)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
End Class
