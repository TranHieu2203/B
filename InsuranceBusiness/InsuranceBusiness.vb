Imports System.Reflection
Imports Framework.Data
Imports InsuranceBusiness.ServiceContracts
Imports InsuranceDAL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace InsuranceBusiness.ServiceImplementations

    Partial Public Class InsuranceBusiness
        Implements IInsuranceBusiness

        Public Shared OUT_CURSOR As String = "OUT_CURSOR"

#Region "Get data combobox"
        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean Implements IInsuranceBusiness.GetComboboxData
            Using rep As New InsuranceRepository
                Try
                    Return rep.GetComboboxData(cbxData)
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Insurance")
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements IInsuranceBusiness.GetOtherList
            Using rep As New InsuranceRepository
                Try
                    Return rep.GetOtherList(sType, sLang, isBlank)
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Insurance")
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetInsListChangeType() As DataTable _
              Implements ServiceContracts.IInsuranceBusiness.GetInsListChangeType
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_CHANGE_TYPE",
                                                      New With {.P_CUR = OUT_CURSOR})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetInsListWhereHealth() As DataTable _
              Implements ServiceContracts.IInsuranceBusiness.GetInsListWhereHealth
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_WHEREHEALTH",
                                                      New With {.P_CUR = OUT_CURSOR})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetInsListEntitledType() As DataTable _
              Implements ServiceContracts.IInsuranceBusiness.GetInsListEntitledType
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_ENTITLED_TYPE",
                                                      New With {.P_CUR = OUT_CURSOR})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "Function"
        Public Function GetEmpInfo(ByVal code As Decimal, Optional ByVal org As String = "") As DataTable _
                Implements ServiceContracts.IInsuranceBusiness.GetEmpInfo
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_LIST_EMPINFO", New With {.P_CUR = OUT_CURSOR, .P_CODE = code, .P_ORG = org})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function Check_Exist_Emp_Ins(ByVal strEmpId As String) As DataTable _
                Implements ServiceContracts.IInsuranceBusiness.Check_Exist_Emp_Ins
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.CHECK_EXIST_EMP_INS", New With {.P_CUR = OUT_CURSOR, .P_EMPCODE = strEmpId})
                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function Check_Arising_Type(ByVal arising_Type As Decimal) As DataTable _
                Implements ServiceContracts.IInsuranceBusiness.Check_Arising_Type
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.CHECK_ARISING_TYPE", New With {.P_ARISING_TYPE = arising_Type, .P_CUR = OUT_CURSOR})
                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function CheckDayCalculate(ByVal idRegime As Decimal, ByVal dayCal As Decimal) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.CheckDayCalculate
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_CHECK_DAY_CALCULATOR", New With {.P_CUR = OUT_CURSOR, .P_ID_REGIME = idRegime, .P_DAY_CALCULATOR = dayCal})
                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region
#Region "INS_MASTERLIST" 'Danh mục loại điều chỉnh Hồ sơ Bảo hiểm
        Public Function GetInsListMasterlist(ByVal username As String, ByVal type As String _
                                            , ByVal id As String _
                                            , ByVal sval1 As String _
                                            , ByVal sval2 As String _
                                            , ByVal nval1 As Double? _
                                            , ByVal nval2 As Double? _
                                            , ByVal dval1 As Date? _
                                            , ByVal dval2 As Date? _
                                            , ByVal status As Double?) As DataTable _
              Implements ServiceContracts.IInsuranceBusiness.GetInsListMasterlist
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_INS_LIST_MASTERLIST", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_TYPE = type _
                                                    , .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_SVAL1 = sval1 _
                                                    , .P_SVAL2 = sval2 _
                                                    , .P_NVAL1 = IIf(nval1 Is Nothing, System.DBNull.Value, nval1) _
                                                    , .P_NVAL2 = IIf(nval2 Is Nothing, System.DBNull.Value, nval2) _
                                                    , .P_DVAL1 = IIf(dval1 Is Nothing, System.DBNull.Value, dval1) _
                                                    , .P_DVAL2 = IIf(dval2 Is Nothing, System.DBNull.Value, dval2) _
                                                    , .P_STATUS = IIf(status Is Nothing, System.DBNull.Value, status)
                                                    })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsListMasterlist(ByVal username As String, ByVal type As String _
                                                , ByVal id As String _
                                                , ByVal name As String _
                                                , ByVal sval1 As String _
                                                , ByVal sval2 As String _
                                                , ByVal sval3 As String _
                                                , ByVal nval1 As Double? _
                                                , ByVal nval2 As Double? _
                                                , ByVal nval3 As Double? _
                                                , ByVal dval1 As Date? _
                                                , ByVal dval2 As Date? _
                                                , ByVal dval3 As Date? _
                                                , ByVal idx As Double? _
                                                , ByVal status As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsListMasterlist
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_LIST.SPU_INS_LIST_MASTERLIST", New With {.P_USERID = username, .P_TYPE = type _
                                                    , .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_NAME = name _
                                                    , .P_SVAL1 = sval1 _
                                                    , .P_SVAL2 = sval2 _
                                                    , .P_SVAL3 = sval3 _
                                                    , .P_NVAL1 = IIf(nval1 Is Nothing, System.DBNull.Value, nval1) _
                                                    , .P_NVAL2 = IIf(nval2 Is Nothing, System.DBNull.Value, nval2) _
                                                    , .P_NVAL3 = IIf(nval3 Is Nothing, System.DBNull.Value, nval3) _
                                                    , .P_DVAL1 = IIf(dval1 Is Nothing, System.DBNull.Value, dval1) _
                                                    , .P_DVAL2 = IIf(dval2 Is Nothing, System.DBNull.Value, dval2) _
                                                    , .P_DVAL3 = IIf(dval3 Is Nothing, System.DBNull.Value, dval3) _
                                                    , .P_IDX = IIf(idx Is Nothing, System.DBNull.Value, idx) _
                                                    , .P_STATUS = IIf(status Is Nothing, System.DBNull.Value, status)
                                                    })

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsListMasterlist(ByVal id As String) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsListMasterlist
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_LIST.SPD_INS_LIST_MASTERLIST",
                                                      New With {.P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function GetCheckExist(ByVal table As String, ByVal id As String) As DataTable _
            Implements ServiceContracts.IInsuranceBusiness.GetCheckExist
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_LIST.SPS_CHECK_EXIST", New With {.P_CUR = OUT_CURSOR, .P_TABLE = table, .P_ID = id})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function UpdateStatusForList(ByVal username As String, ByVal code As String, ByVal lstId As String _
                                            , ByVal status As Decimal) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateStatusForList
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS = rep.ExecuteStore("PKG_INS_LIST.SPU_UPDATE_STATUS_LIST", New With {.P_OUT = OUT_CURSOR, .P_USERID = username, .P_CODE = IIf(code Is Nothing, System.DBNull.Value, code),
                                                                                                       .P_LIST_ID = IIf(lstId Is Nothing, System.DBNull.Value, lstId) _
                                                                                                        , .P_STATUS = status
                                                                                                        })
                If objS.Rows(0)(0).ToString() = "1" Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

#Region "Quản lý khai báo biến động bảo hiểm"
        Public Function GetInsArising(ByVal username As String, ByVal fromdate As Date?, ByVal todate As Date?, ByVal arising_type_id As Double?, ByVal org_id As Decimal, ByVal insurance_id As Double?, ByVal is_Dissolve As Decimal) As DataTable _
                     Implements ServiceContracts.IInsuranceBusiness.GetInsArising
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_ARISING", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                                                                   , .P_ARISING_TYPE_ID = IIf(arising_type_id Is Nothing, System.DBNull.Value, arising_type_id) _
                                                                                                   , .P_FROMDATE = IIf(fromdate Is Nothing, System.DBNull.Value, fromdate) _
                                                                                                   , .P_TODATE = IIf(todate Is Nothing, System.DBNull.Value, todate) _
                                                                                                   , .P_ORG_ID = org_id _
                                                                                                   , .P_INSURANCE_ID = IIf(insurance_id Is Nothing, System.DBNull.Value, insurance_id),
                                                                                                    .P_IS_DISSOLVE = is_Dissolve})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsArising(ByVal username As String, ByVal effect_date As Date?, ByVal id As Double, ByVal empid As Double, ByVal arising_type_id As Double) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsArising
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_ARISING", New With {.P_USERID = username _
                                                                                                    , .P_ID = id _
                                                                                                    , .P_EFFECT_DATE = IIf(effect_date Is Nothing, System.DBNull.Value, effect_date) _
                                                                                                    , .P_EMPID = empid _
                                                                                                    , .P_ARISING_TYPE_ID = arising_type_id
                                                                                                   })

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Function UpdateInsArisingNote(ByVal username As String, ByVal id As Decimal, ByVal note As String) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsArisingNote
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_ARISING_NOTE", New With {.P_USERID = username _
                                                                                                    , .P_ID = id _
                                                                                                    , .P_NOTE = note
                                                                                                   })
                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Function DeleteInsArising(ByVal username As String, ByVal id As Double) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsArising
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_ARISING", New With {.P_USERID = username, .P_ID = id})

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function DELETE_INS_ARSING_LEAVE_SHEET(ByVal id As String) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DELETE_INS_ARSING_LEAVE_SHEET
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_LIST.DELETE_INS_ARSING_LEAVE_SHEET", New With {.P_ID = id})

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function

#End Region

#Region "Quản lý thông tin bảo hiểm"
        Public Function GetInsInfomation(ByVal username As String, ByVal id As Double? _
                                            , ByVal employee_id As String _
                                            , ByVal org_id As Double? _
                                            , ByVal isTer As Double? _
                                            , ByVal isDISSOLVE As Double?
                                            ) As DataTable _
             Implements ServiceContracts.IInsuranceBusiness.GetInsInfomation
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_INFORMATION", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_EMPLOYEE_ID = employee_id _
                                                    , .P_ORG_ID = IIf(org_id Is Nothing, System.DBNull.Value, org_id) _
                                                    , .P_ISTER = IIf(isTer Is Nothing, System.DBNull.Value, isTer) _
                                                    , .P_IS_DISS = IIf(isDISSOLVE Is Nothing, System.DBNull.Value, isDISSOLVE)
                                                    })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GET_EXPORT_BHXH() As DataTable _
             Implements ServiceContracts.IInsuranceBusiness.GET_EXPORT_BHXH
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.GET_EXPORT_BHXH", New With {.P_CUR = OUT_CURSOR})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function IMPORT_BHXH(ByVal P_DOCXML As String) As Boolean _
             Implements ServiceContracts.IInsuranceBusiness.IMPORT_BHXH
            Try
                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_INS_BUSINESS.IMPORT_BHXH",
                                     New With {.P_DOCXML = P_DOCXML,
                                               .P_USERNAME = Nothing})
                End Using
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
                Return False
            End Try
        End Function

        Public Function UpdateInsInfomation(ByVal username As String, ByVal id As Double? _
                                            , ByVal employee_id As Double? _
                                            , ByVal ins_org_name As String _
                                            , ByVal seniority_insurance As Double? _
                                            , ByVal seniority_insurance_company As String _
                                            , ByVal social_number As String _
                                            , ByVal social_status As Double? _
                                            , ByVal social_submit_date As Date? _
                                            , ByVal social_submit As String _
                                            , ByVal social_grant_date As Date? _
                                            , ByVal social_save_number As String _
                                            , ByVal social_deliver_date As Date? _
                                            , ByVal social_return_date As Date? _
                                            , ByVal social_receiver As String _
                                            , ByVal social_note As String _
                                            , ByVal health_number As String _
                                            , ByVal health_status As Double? _
                                            , ByVal health_effect_from_date As Date? _
                                            , ByVal health_effect_to_date As Date? _
                                            , ByVal health_area_ins_id As Double? _
                                            , ByVal health_receive_date As Date? _
                                            , ByVal health_receiver As String _
                                            , ByVal health_return_date As Date? _
                                            , ByVal unemp_from_moth As Date? _
                                            , ByVal unemp_to_month As Date? _
                                            , ByVal unemp_register_month As Date? _
                                            , ByVal si_from_month As Date? _
                                            , ByVal si_to_month As Date? _
                                            , ByVal hi_from_month As Date? _
                                            , ByVal hi_to_month As Date? _
                                            , ByVal si As Double? _
                                            , ByVal hi As Double? _
                                            , ByVal ui As Double? _
                                            , ByVal bhtnld_bnn As Double? _
                                            , ByVal is_hi_five_year As Double? _
                                            , ByVal bhtnld_bnn_from As Date? _
                                            , ByVal bhtnld_bnn_to As Date? _
                                            , ByVal register_place As Decimal?
                                            ) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsInfomation
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_INFORMATION", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                            , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                            , .P_INS_ORG_NAME = IIf(ins_org_name Is Nothing, System.DBNull.Value, ins_org_name) _
                                            , .P_SENIORITY_INSURANCE = IIf(seniority_insurance Is Nothing, System.DBNull.Value, seniority_insurance) _
                                            , .P_SENIORITY_INSURANCE_COMPANY = IIf(seniority_insurance_company Is Nothing, Nothing, seniority_insurance_company) _
                                            , .P_SOCIAL_NUMBER = IIf(social_number Is Nothing, System.DBNull.Value, social_number) _
                                            , .P_SOCIAL_STATUS = IIf(social_status Is Nothing, System.DBNull.Value, social_status) _
                                            , .P_SOCIAL_SUBMIT_DATE = IIf(social_submit_date Is Nothing, System.DBNull.Value, social_submit_date) _
                                            , .P_SOCIAL_SUBMIT = IIf(social_submit Is Nothing, System.DBNull.Value, social_submit) _
                                            , .P_SOCIAL_GRANT_DATE = IIf(social_grant_date Is Nothing, System.DBNull.Value, social_grant_date) _
                                            , .P_SOCIAL_SAVE_NUMBER = IIf(social_save_number Is Nothing, System.DBNull.Value, social_save_number) _
                                            , .P_SOCIAL_DELIVER_DATE = IIf(social_deliver_date Is Nothing, System.DBNull.Value, social_deliver_date) _
                                            , .P_SOCIAL_RETURN_DATE = IIf(social_return_date Is Nothing, System.DBNull.Value, social_return_date) _
                                            , .P_SOCIAL_RECEIVER = IIf(social_receiver Is Nothing, System.DBNull.Value, social_receiver) _
                                            , .P_SOCIAL_NOTE = IIf(social_note Is Nothing, System.DBNull.Value, social_note) _
                                            , .P_HEALTH_NUMBER = IIf(health_number Is Nothing, System.DBNull.Value, health_number) _
                                            , .P_HEALTH_STATUS = IIf(health_status Is Nothing, System.DBNull.Value, health_status) _
                                            , .P_HEALTH_EFFECT_FROM_DATE = IIf(health_effect_from_date Is Nothing, System.DBNull.Value, health_effect_from_date) _
                                            , .P_HEALTH_EFFECT_TO_DATE = IIf(health_effect_to_date Is Nothing, System.DBNull.Value, health_effect_to_date) _
                                            , .P_HEALTH_AREA_INS_ID = IIf(health_area_ins_id Is Nothing, System.DBNull.Value, health_area_ins_id) _
                                            , .P_HEALTH_RECEIVE_DATE = IIf(health_receive_date Is Nothing, System.DBNull.Value, health_receive_date) _
                                            , .P_HEALTH_RECEIVER = IIf(health_receiver Is Nothing, System.DBNull.Value, health_receiver) _
                                            , .P_HEALTH_RETURN_DATE = IIf(health_return_date Is Nothing, System.DBNull.Value, health_return_date) _
                                            , .P_UNEMP_FROM_MOTH = IIf(unemp_from_moth Is Nothing, System.DBNull.Value, unemp_from_moth) _
                                            , .P_UNEMP_TO_MONTH = IIf(unemp_to_month Is Nothing, System.DBNull.Value, unemp_to_month) _
                                            , .P_UNEMP_REGISTER_MONTH = IIf(unemp_register_month Is Nothing, System.DBNull.Value, unemp_register_month) _
                                            , .P_SI_FROM_MOTH = IIf(si_from_month Is Nothing, System.DBNull.Value, si_from_month) _
                                            , .P_SI_TO_MONTH = IIf(si_to_month Is Nothing, System.DBNull.Value, si_to_month) _
                                            , .P_HI_FROM_MOTH = IIf(hi_from_month Is Nothing, System.DBNull.Value, hi_from_month) _
                                            , .P_HI_TO_MONTH = IIf(hi_to_month Is Nothing, System.DBNull.Value, hi_to_month) _
                                            , .P_SI = IIf(si Is Nothing, System.DBNull.Value, si) _
                                            , .P_HI = IIf(hi Is Nothing, System.DBNull.Value, hi) _
                                            , .P_UI = IIf(ui Is Nothing, System.DBNull.Value, ui) _
                                            , .P_BHTNLD_BNN = IIf(bhtnld_bnn Is Nothing, System.DBNull.Value, bhtnld_bnn) _
                                            , .P_IS_HI_FIVE_YEAR = IIf(is_hi_five_year Is Nothing, System.DBNull.Value, is_hi_five_year) _
                                            , .P_BHTNLD_BNN_FROM_MONTH = IIf(bhtnld_bnn_from Is Nothing, System.DBNull.Value, bhtnld_bnn_from) _
                                            , .P_BHTNLD_BNN_TO_MONTH = IIf(bhtnld_bnn_to Is Nothing, System.DBNull.Value, bhtnld_bnn_to) _
                                            , .P_REGISTER_PLACE = IIf(register_place Is Nothing, System.DBNull.Value, register_place)
                                            })

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsInfomation(ByVal username As String, ByVal id As Double?) As Boolean _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsInfomation
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_INFORMATION", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function
#End Region

#Region "Quản lý biến động bảo hiểm"
        Public Function GetInsArisingManual(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As String _
                                        , ByVal org_id As Double? _
                                        , ByVal from_date As Date? _
                                        , ByVal to_date As Date? _
                                        , ByVal isTer As Double? _
                                        , ByVal isDISSOLVE As Double?
                                        ) As DataTable _
        Implements ServiceContracts.IInsuranceBusiness.GetInsArisingManual
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_ARISING_MANUAL", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_ORG_ID = IIf(org_id Is Nothing, System.DBNull.Value, org_id) _
                                                        , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                        , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                                        , .P_ISTER = IIf(isTer Is Nothing, System.DBNull.Value, isTer) _
                                                        , .P_IS_DISS = IIf(isDISSOLVE Is Nothing, System.DBNull.Value, isDISSOLVE)
                                                        })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsArisingManual(ByVal username As String, ByVal id As Double? _
                                            , ByVal employee_id As Double? _
                                            , ByVal ins_org_id As Double? _
                                            , ByVal ins_arising_type_id As Double? _
                                            , ByVal salary_pre_period As Double? _
                                            , ByVal salary_now_period As Double? _
                                            , ByVal from_health_ins_card As Double? _
                                            , ByVal effective_date As Date? _
                                            , ByVal expire_date As Date? _
                                            , ByVal declare_date As Date? _
                                            , ByVal arising_from_month As Date? _
                                            , ByVal arising_to_month As Date? _
                                            , ByVal note As String _
                                            , ByVal social_note As String _
                                            , ByVal health_number As String _
                                            , ByVal health_status As Double? _
                                            , ByVal health_effect_from_date As Date? _
                                            , ByVal health_effect_to_date As Date? _
                                            , ByVal health_area_ins_id As Double? _
                                            , ByVal health_receive_date As Date? _
                                            , ByVal health_receiver As String _
                                            , ByVal health_return_date As Date? _
                                            , ByVal unemp_from_moth As Date? _
                                            , ByVal unemp_to_month As Date? _
                                            , ByVal unemp_register_month As Date? _
                                            , ByVal r_from As Date? _
                                            , ByVal o_from As Date? _
                                            , ByVal r_to As Date? _
                                            , ByVal o_to As Date? _
                                            , ByVal r_si As Double? _
                                            , ByVal o_si As Double? _
                                            , ByVal r_hi As Double? _
                                            , ByVal o_hi As Double? _
                                            , ByVal r_ui As Double? _
                                            , ByVal o_ui As Double? _
                                            , ByVal a_from As Date? _
                                            , ByVal a_to As Date? _
                                            , ByVal a_si As Double? _
                                            , ByVal a_hi As Double? _
                                            , ByVal a_ui As Double? _
                                            , ByVal si As Double? _
                                            , ByVal hi As Double? _
                                            , ByVal ui As Double? _
                                            , ByVal tnld_bnn As Double? _
                                            , ByVal si_sal As Double? _
                                            , ByVal hi_sal As Double? _
                                            , ByVal ui_sal As Double? _
                                            , ByVal tnld_bnn_sal As Double? _
                                            , ByVal si_sal_old As Double? _
                                            , ByVal hi_sal_old As Double? _
                                            , ByVal ui_sal_old As Double? _
                                            , ByVal tnld_bnn_sal_old As Double? _
                                            , ByVal a_tnld_bnn As Double? _
                                            , ByVal r_tnld_bnn As Double? _
                                            , ByVal P_HI_DECLARED_DATE As Date? _
                                            , ByVal P_O_HI_COM As Decimal? _
                                            , ByVal P_O_HI_EMP As Decimal? _
                                            , ByVal P_HI_RATE_ARREARS As Decimal? _
                                            , ByVal P_SALARY_HD As Decimal? _
                                            , ByVal P_SALARY_PC As Decimal? _
                                            , ByVal P_FOREIGN As Decimal?) As Double _
                                             Implements ServiceContracts.IInsuranceBusiness.UpdateInsArisingManual
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_ARISING_MANUAL", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                            , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                            , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                            , .P_INS_ARISING_TYPE_ID = IIf(ins_arising_type_id Is Nothing, System.DBNull.Value, ins_arising_type_id) _
                                            , .P_SALARY_PRE_PERIOD = IIf(salary_pre_period Is Nothing, System.DBNull.Value, salary_pre_period) _
                                            , .P_SALARY_NOW_PERIOD = IIf(salary_now_period Is Nothing, System.DBNull.Value, salary_now_period) _
                                            , .P_FROM_HEALTH_INS_CARD = IIf(from_health_ins_card Is Nothing, System.DBNull.Value, from_health_ins_card) _
                                            , .P_EFFECTIVE_DATE = IIf(effective_date Is Nothing, System.DBNull.Value, effective_date) _
                                            , .txtEXPRIE_DATE = IIf(expire_date Is Nothing, System.DBNull.Value, expire_date) _
                                            , .P_DECLARE_DATE = IIf(declare_date Is Nothing, System.DBNull.Value, declare_date) _
                                            , .P_ARISING_FROM_MONTH = IIf(arising_from_month Is Nothing, System.DBNull.Value, arising_from_month) _
                                            , .P_ARISING_TO_MONTH = IIf(arising_to_month Is Nothing, System.DBNull.Value, arising_to_month) _
                                            , .P_NOTE = IIf(note Is Nothing, System.DBNull.Value, note) _
                                            , .P_SOCIAL_NOTE = IIf(social_note Is Nothing, System.DBNull.Value, social_note) _
                                            , .P_HEALTH_NUMBER = IIf(health_number Is Nothing, System.DBNull.Value, health_number) _
                                            , .P_HEALTH_STATUS = IIf(health_status Is Nothing, System.DBNull.Value, health_status) _
                                            , .P_HEALTH_EFFECT_FROM_DATE = IIf(health_effect_from_date Is Nothing, System.DBNull.Value, health_effect_from_date) _
                                            , .P_HEALTH_EFFECT_TO_DATE = IIf(health_effect_to_date Is Nothing, System.DBNull.Value, health_effect_to_date) _
                                            , .P_HEALTH_AREA_INS_ID = IIf(health_area_ins_id Is Nothing, System.DBNull.Value, health_area_ins_id) _
                                            , .P_HEALTH_RECEIVE_DATE = IIf(health_receive_date Is Nothing, System.DBNull.Value, health_receive_date) _
                                            , .P_HEALTH_RECEIVER = IIf(health_receiver Is Nothing, System.DBNull.Value, health_receiver) _
                                            , .P_HEALTH_RETURN_DATE = IIf(health_return_date Is Nothing, System.DBNull.Value, health_return_date) _
                                            , .P_UNEMP_FROM_MOTH = IIf(unemp_from_moth Is Nothing, System.DBNull.Value, unemp_from_moth) _
                                            , .P_UNEMP_TO_MONTH = IIf(unemp_to_month Is Nothing, System.DBNull.Value, unemp_to_month) _
                                            , .P_UNEMP_REGISTER_MONTH = IIf(unemp_register_month Is Nothing, System.DBNull.Value, unemp_register_month) _
                                            , .P_R_FROM = IIf(r_from Is Nothing, System.DBNull.Value, r_from) _
                                            , .P_O_FROM = IIf(o_from Is Nothing, System.DBNull.Value, o_from) _
                                            , .P_R_TO = IIf(r_to Is Nothing, System.DBNull.Value, r_to) _
                                            , .P_O_TO = IIf(o_to Is Nothing, System.DBNull.Value, o_to) _
                                            , .P_R_SI = IIf(r_si Is Nothing, System.DBNull.Value, r_si) _
                                            , .P_O_SI = IIf(o_si Is Nothing, System.DBNull.Value, o_si) _
                                            , .P_R_HI = IIf(r_hi Is Nothing, System.DBNull.Value, r_hi) _
                                            , .P_O_HI = IIf(o_hi Is Nothing, System.DBNull.Value, o_hi) _
                                            , .P_R_UI = IIf(r_ui Is Nothing, System.DBNull.Value, r_ui) _
                                            , .P_O_UI = IIf(o_ui Is Nothing, System.DBNull.Value, o_ui) _
                                            , .P_A_FROM = IIf(a_from Is Nothing, System.DBNull.Value, a_from) _
                                            , .P_A_TO = IIf(a_to Is Nothing, System.DBNull.Value, a_to) _
                                            , .P_A_SI = IIf(a_si Is Nothing, System.DBNull.Value, a_si) _
                                            , .P_A_HI = IIf(a_hi Is Nothing, System.DBNull.Value, a_hi) _
                                            , .P_A_UI = IIf(a_ui Is Nothing, System.DBNull.Value, a_ui) _
                                            , .P_SI = IIf(si Is Nothing, System.DBNull.Value, si) _
                                            , .P_HI = IIf(hi Is Nothing, System.DBNull.Value, hi) _
                                            , .P_UI = IIf(ui Is Nothing, System.DBNull.Value, ui) _
                                            , .P_TNLD_BNN = IIf(tnld_bnn Is Nothing, System.DBNull.Value, tnld_bnn) _
                                            , .P_SI_SAL = IIf(si_sal Is Nothing, System.DBNull.Value, si_sal) _
                                            , .P_HI_SAL = IIf(hi_sal Is Nothing, System.DBNull.Value, hi_sal) _
                                            , .P_UI_SAL = IIf(ui_sal Is Nothing, System.DBNull.Value, ui_sal) _
                                            , .P_TNLD_BNN_SAL = IIf(tnld_bnn_sal Is Nothing, System.DBNull.Value, tnld_bnn_sal) _
                                            , .P_SI_SAL_OLD = IIf(si_sal_old Is Nothing, System.DBNull.Value, si_sal_old) _
                                            , .P_HI_SAL_OLD = IIf(hi_sal_old Is Nothing, System.DBNull.Value, hi_sal_old) _
                                            , .P_UI_SAL_OLD = IIf(ui_sal_old Is Nothing, System.DBNull.Value, ui_sal_old) _
                                            , .P_TNLD_BNN_SAL_OLD = IIf(tnld_bnn_sal_old Is Nothing, System.DBNull.Value, tnld_bnn_sal_old) _
                                            , .P_A_TNLD_BNN = IIf(a_tnld_bnn Is Nothing, System.DBNull.Value, a_tnld_bnn) _
                                            , .P_R_TNLD_BNN = IIf(r_tnld_bnn Is Nothing, System.DBNull.Value, r_tnld_bnn) _
                                            , .P_HI_DECLARED_DATE = IIf(P_HI_DECLARED_DATE Is Nothing, System.DBNull.Value, P_HI_DECLARED_DATE) _
                                            , .P_O_HI_COM = IIf(P_O_HI_COM Is Nothing, System.DBNull.Value, P_O_HI_COM) _
                                            , .P_O_HI_EMP = IIf(P_O_HI_EMP Is Nothing, System.DBNull.Value, P_O_HI_EMP) _
                                            , .P_HI_RATE_ARREARS = IIf(P_HI_RATE_ARREARS Is Nothing, System.DBNull.Value, P_HI_RATE_ARREARS) _
                                            , .P_SALARY_HD = IIf(P_SALARY_HD Is Nothing, System.DBNull.Value, P_SALARY_HD) _
                                            , .P_SALARY_PC = IIf(P_SALARY_PC Is Nothing, System.DBNull.Value, P_SALARY_PC) _
                                            , .P_FOREIGN = IIf(P_FOREIGN Is Nothing, System.DBNull.Value, P_FOREIGN)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsArisingManual(ByVal username As String, ByVal lstid As String) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsArisingManual
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_ARISING_MANUAL", New With {.P_USERID = username, .P_ID = IIf(lstid Is Nothing, System.DBNull.Value, lstid)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function

        Public Function InsAraisingAuto(ByVal username As String, ByVal empid As Double?, ByVal ins_arising_type As Double? _
        , ByVal effective_date As Date?, ByVal declare_date As Date?, ByVal hi_date As Date?, ByVal is_foreign As Double?) As DataTable _
                   Implements ServiceContracts.IInsuranceBusiness.InsAraisingAuto
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_ARISING_MANUAL_LOAD", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                    , .P_EMPLOYEE_ID = IIf(empid Is Nothing, System.DBNull.Value, empid) _
                                                    , .P_INS_ARISING_TYPE_ID = IIf(ins_arising_type Is Nothing, System.DBNull.Value, ins_arising_type) _
                                                    , .P_EFFECTIVE_DATE = IIf(effective_date Is Nothing, System.DBNull.Value, effective_date) _
                                                    , .P_DECLARE_DATE = IIf(declare_date Is Nothing, System.DBNull.Value, declare_date) _
                                                    , .P_HEALTH_RETURN_DATE = IIf(hi_date Is Nothing, System.DBNull.Value, hi_date) _
                                                    , .P_FOREIGN = IIf(is_foreign Is Nothing, 0, is_foreign)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function InsAraisingAuto2(ByVal username As String, ByVal empid As Double?, ByVal ins_arising_type As Double? _
        , ByVal effective_date As Date?, ByVal declare_date As Date?, ByVal hi_date As Date?, ByVal truythu As String, ByVal is_foreign As Double?) As DataTable _
                   Implements ServiceContracts.IInsuranceBusiness.InsAraisingAuto2
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_ARISING_MANUAL_LOAD2", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                                                                                , .P_EMPLOYEE_ID = IIf(empid Is Nothing, System.DBNull.Value, empid) _
                                                                                                                , .P_INS_ARISING_TYPE_ID = IIf(ins_arising_type Is Nothing, System.DBNull.Value, ins_arising_type) _
                                                                                                                , .P_EFFECTIVE_DATE = IIf(effective_date Is Nothing, System.DBNull.Value, effective_date) _
                                                                                                                , .P_DECLARE_DATE = IIf(declare_date Is Nothing, System.DBNull.Value, declare_date) _
                                                                                                                , .P_HEALTH_RETURN_DATE = IIf(hi_date Is Nothing, System.DBNull.Value, hi_date) _
                                                                                                                , .P_TRUYTHU = IIf(truythu Is Nothing, System.DBNull.Value, truythu) _
                                                                                                                , .P_FOREIGN = IIf(is_foreign Is Nothing, 0, is_foreign)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "Quản lý thông tin hưởng chế độ"
        Public Function GetInsRegimes(ByVal username As String, ByVal id As Double? _
                                    , ByVal regime_id As Double? _
                                    , ByVal pay_form As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal org_id As Double? _
                                    , ByVal from_date As Date? _
                                    , ByVal to_date As Date? _
                                    , ByVal isTer As Double?
                                    ) As DataTable _
        Implements ServiceContracts.IInsuranceBusiness.GetInsRegimes
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_REGIMES", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_REGIME_ID = IIf(regime_id Is Nothing, System.DBNull.Value, regime_id) _
                                                        , .P_PAY_FORM = IIf(pay_form Is Nothing, System.DBNull.Value, pay_form) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_ORG_ID = IIf(org_id Is Nothing, System.DBNull.Value, org_id) _
                                                        , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                        , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                                        , .P_ISTER = IIf(isTer Is Nothing, System.DBNull.Value, isTer)
                                                        })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsRegimes(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As Double? _
                                        , ByVal regime_id As Double? _
                                        , ByVal pay_form As Double? _
                                        , ByVal from_date As Date? _
                                        , ByVal to_date As Date? _
                                        , ByVal day_calculator As Double? _
                                        , ByVal born_date As Date? _
                                        , ByVal name_children As String _
                                        , ByVal children_no As Double? _
                                        , ByVal accumulate_day As Double? _
                                        , ByVal subsidy_salary As Double? _
                                        , ByVal subsidy As Double? _
                                        , ByVal subsidy_amount As Double? _
                                        , ByVal payroll_date As Date? _
                                        , ByVal declare_date As Date? _
                                        , ByVal condition As String _
                                        , ByVal ins_pay_amount As Double? _
                                        , ByVal pay_approve_date As Date? _
                                        , ByVal approv_day_num As Double? _
                                        , ByVal note As String _
                                        , ByVal money_advance As Double? _
                                        , ByVal off_together As Double? _
                                        , ByVal off_in_house As Double? _
                                        , ByVal regimes_sal As Double? _
                                        , ByVal form_Allow As Decimal?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsRegimes
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_REGIMES", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                        , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                        , .P_REGIME_ID = IIf(regime_id Is Nothing, System.DBNull.Value, regime_id) _
                                        , .P_PAY_FORM = IIf(pay_form Is Nothing, System.DBNull.Value, pay_form) _
                                        , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                        , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                        , .P_DAY_CALCULATOR = IIf(day_calculator Is Nothing, System.DBNull.Value, day_calculator) _
                                        , .P_BORN_DATE = IIf(born_date Is Nothing, System.DBNull.Value, born_date) _
                                        , .P_NAME_CHILDREN = IIf(name_children Is Nothing, System.DBNull.Value, name_children) _
                                        , .P_CHILDREN_NO = IIf(children_no Is Nothing, System.DBNull.Value, children_no) _
                                        , .P_ACCUMULATE_DAY = IIf(accumulate_day Is Nothing, System.DBNull.Value, accumulate_day) _
                                        , .P_SUBSIDY_SALARY = IIf(subsidy_salary Is Nothing, System.DBNull.Value, subsidy_salary) _
                                         , .P_SUBSIDY = IIf(subsidy Is Nothing, System.DBNull.Value, subsidy) _
                                        , .P_SUBSIDY_AMOUNT = IIf(subsidy_amount Is Nothing, System.DBNull.Value, subsidy_amount) _
                                        , .P_PAYROLL_DATE = IIf(payroll_date Is Nothing, System.DBNull.Value, payroll_date) _
                                        , .P_DECLARE_DATE = IIf(declare_date Is Nothing, System.DBNull.Value, declare_date) _
                                        , .P_CONDITION = IIf(condition Is Nothing, System.DBNull.Value, condition) _
                                        , .P_INS_PAY_AMOUNT = IIf(ins_pay_amount Is Nothing, System.DBNull.Value, ins_pay_amount) _
                                        , .P_PAY_APPROVE_DATE = IIf(pay_approve_date Is Nothing, System.DBNull.Value, pay_approve_date) _
                                        , .P_APPROV_DAY_NUM = IIf(approv_day_num Is Nothing, System.DBNull.Value, approv_day_num) _
                                        , .P_NOTE = IIf(note Is Nothing, System.DBNull.Value, note) _
                                        , .P_MONEY_ADVANCE = IIf(money_advance Is Nothing, System.DBNull.Value, money_advance) _
                                        , .P_OFF_TOGETHER = IIf(off_together Is Nothing, System.DBNull.Value, off_together) _
                                        , .P_OFF_IN_HOUSE = IIf(off_in_house Is Nothing, System.DBNull.Value, off_in_house) _
                                        , .P_REGIMES_SAL = IIf(regimes_sal Is Nothing, System.DBNull.Value, regimes_sal) _
                                        , .P_FORM_ALLOW = IIf(form_Allow Is Nothing, System.DBNull.Value, form_Allow)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsRegimes(ByVal username As String, ByVal id As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsRegimes
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_REGIMES", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function
        Public Function GetInsRegimes_cal(ByVal employee_id As Double?, ByVal regime_id As Double? _
                                        , ByVal from_date As Date? _
                                        , ByVal to_date As Date?, Optional ByVal dayCal As Double = -1) As DataTable _
   Implements ServiceContracts.IInsuranceBusiness.GetInsRegimes_cal
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object
                If dayCal <> -1 Then
                    objS = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_REGIMES_CAL_2", New With {.P_CUR = OUT_CURSOR _
                                                    , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                                    , .P_REGIME_ID = IIf(regime_id Is Nothing, System.DBNull.Value, regime_id) _
                                                    , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                    , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                                    , .P_DAY_CAL = dayCal})
                Else
                    objS = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_REGIMES_CAL", New With {.P_CUR = OUT_CURSOR _
                                                    , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                                    , .P_REGIME_ID = IIf(regime_id Is Nothing, System.DBNull.Value, regime_id) _
                                                    , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                    , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date)
                                                    })
                End If


                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#End Region

#Region "Điều chỉnh Hồ sơ cấp BHXH, thẻ BHYT"
        Public Function GetInsModifier(ByVal username As String, ByVal id As Double? _
                                    , ByVal employee_id As String _
                                    , ByVal org_id As Double? _
                                    , ByVal ins_modifier_type_id As Double? _
                                    , ByVal from_date As Date? _
                                    , ByVal to_date As Date? _
                                    , ByVal isTer As Double?) As DataTable _
        Implements ServiceContracts.IInsuranceBusiness.GetInsModifier
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_MODIFIER", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_EMPLOYEE_ID = employee_id _
                                                    , .P_ORG_ID = IIf(org_id Is Nothing, System.DBNull.Value, org_id) _
                                                    , .P_INS_MODIFIER_TYPE_ID = IIf(ins_modifier_type_id Is Nothing, System.DBNull.Value, ins_modifier_type_id) _
                                                    , .P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                    , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                                    , .P_ISTER = IIf(isTer Is Nothing, System.DBNull.Value, isTer)
                                                    })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function UpdateInsModifier(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As Double? _
                                        , ByVal ins_modifier_type_id As Double? _
                                        , ByVal reason As String _
                                        , ByVal old_info As String _
                                        , ByVal new_info As String _
                                        , ByVal modifier_date As Date? _
                                        , ByVal note As String _
                                        , ByVal isUpdate As String _
                                        , ByVal birth_date As Date? _
                                        , ByVal areaid As Double? _
                                        , ByVal id_date As Date? _
                                        , ByVal id_place As Double? _
                                        , ByVal birth_place As Double? _
                                        , ByVal per_address As String _
                                        , ByVal per_coun As Double? _
                                        , ByVal per_prov As Double? _
                                        , ByVal per_dist As Double? _
                                        , ByVal per_ward As Double? _
                                        , ByVal con_address As String _
                                        , ByVal con_coun As Double? _
                                        , ByVal con_prov As Double? _
                                        , ByVal con_dist As Double? _
                                        , ByVal con_ward As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsModifier
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_MODIFIER", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                                                    , .P_INS_MODIFIER_TYPE_ID = IIf(ins_modifier_type_id Is Nothing, System.DBNull.Value, ins_modifier_type_id) _
                                                    , .P_REASON = IIf(reason Is Nothing, System.DBNull.Value, reason) _
                                                    , .P_OLD_INFO = IIf(old_info Is Nothing, System.DBNull.Value, old_info) _
                                                    , .P_NEW_INFO = IIf(new_info Is Nothing, System.DBNull.Value, new_info) _
                                                    , .P_MODIFIER_DATE = IIf(modifier_date Is Nothing, System.DBNull.Value, modifier_date) _
                                                    , .P_NOTE = IIf(note Is Nothing, System.DBNull.Value, note) _
                                                    , .P_ISUPDATE = IIf(isUpdate Is Nothing, System.DBNull.Value, isUpdate) _
                                                    , .P_BIRTH_DATE = IIf(birth_date Is Nothing, System.DBNull.Value, birth_date) _
                                                    , .P_HEALTH_AREA_INS_ID = IIf(areaid Is Nothing, System.DBNull.Value, areaid) _
                                                    , .P_ID_DATE = IIf(id_date Is Nothing, System.DBNull.Value, id_date) _
                                                    , .P_ID_PLACE = IIf(id_place Is Nothing, System.DBNull.Value, id_place) _
                                                    , .P_BIRTH_PLACE = IIf(birth_place Is Nothing, System.DBNull.Value, birth_place) _
                                                    , .P_PER_ADDRESS = IIf(per_address Is Nothing, System.DBNull.Value, per_address) _
                                                    , .P_PER_COUNTRY = IIf(per_coun Is Nothing, System.DBNull.Value, per_coun) _
                                                    , .P_PER_PROVINCE = IIf(per_prov Is Nothing, System.DBNull.Value, per_prov) _
                                                    , .P_PER_DISTRICT = IIf(per_dist Is Nothing, System.DBNull.Value, per_dist) _
                                                    , .P_PER_WARD = IIf(per_ward Is Nothing, System.DBNull.Value, per_ward) _
                                                    , .P_CON_ADDRESS = IIf(con_address Is Nothing, System.DBNull.Value, con_address) _
                                                    , .P_CON_COUNTRY = IIf(con_coun Is Nothing, System.DBNull.Value, con_coun) _
                                                    , .P_CON_PROVINCE = IIf(con_prov Is Nothing, System.DBNull.Value, con_prov) _
                                                    , .P_CON_DISTRICT = IIf(con_dist Is Nothing, System.DBNull.Value, con_dist) _
                                                    , .P_CON_WARD = IIf(con_ward Is Nothing, System.DBNull.Value, con_ward)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsModifier(ByVal username As String, ByVal id As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsModifier
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_MODIFIER", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function

#End Region

#Region "Tổng quỹ lương"
        Public Function CheckInvalidArising(ByVal ins_Org_Id As Decimal) As DataTable _
                  Implements ServiceContracts.IInsuranceBusiness.CheckInvalidArising
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPC_CHECK_INVALID_ARISING",
                                                      New With {.P_INS_ORG_ID = ins_Org_Id})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetInsTotalSalary(ByVal username As String, ByVal year As Double? _
        , ByVal month As Double? _
        , ByVal ins_org_id As Double? _
        , ByVal period As String
        ) As DataTable _
                   Implements ServiceContracts.IInsuranceBusiness.GetInsTotalSalary
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPI_INS_TOTALSALARY", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


        Public Function SPI_INS_TOTALSALARY_EXPORT(ByVal username As String, ByVal year As Double? _
        , ByVal month As Double? _
        , ByVal ins_org_id As Double? _
        , ByVal period As String
        ) As DataTable _
                   Implements ServiceContracts.IInsuranceBusiness.SPI_INS_TOTALSALARY_EXPORT
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPI_INS_TOTALSALARY_EXPORT", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GetInsTotalSalaryPeriod(ByVal username As String, ByVal year As Double? _
                                                , ByVal month As Double? _
                                                , ByVal ins_org_id As Double? _
                                                , ByVal period As String
                                                ) As DataTable _
                  Implements ServiceContracts.IInsuranceBusiness.GetInsTotalSalaryPeriod
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPI_INS_TOTALSALARY_PERIOD", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function GetInsTotalSalary_Summary(ByVal username As String, ByVal year As Double? _
       , ByVal month As Double? _
       , ByVal ins_org_id As Double? _
       , ByVal period As String _
       , ByVal isPre As String
       ) As DataTable _
                  Implements ServiceContracts.IInsuranceBusiness.GetInsTotalSalary_Summary
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPI_INS_TOTALSALARY_SUMMARY", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period) _
                                                        , .P_ISPRE = IIf(isPre Is Nothing, System.DBNull.Value, isPre)
                                                        })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function CalInsTotalSalary(ByVal username As String, ByVal year As Double? _
                                        , ByVal month As Double? _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal period As String
                                        ) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.CalInsTotalSalary
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_CAL", New With {.P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function

        Public Function CalInsTotalSalaryBatch(ByVal username As String, ByVal fromdate As Date _
                                                                        , ByVal todate As Date _
                                                                        , ByVal ins_org_id As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.CalInsTotalSalaryBatch
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_CAL_BATCH", New With {.P_USERID = username _
                                                                        , .P_FROM_DATE = fromdate _
                                                                        , .P_TO_DATE = todate _
                                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function

        Public Function LockInsTotalSalary(ByVal username As String, ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double? _
                                           , ByVal period As String
                                           ) As Double _
                Implements ServiceContracts.IInsuranceBusiness.LockInsTotalSalary
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_LOCK", New With {.P_CUR = OUT_CURSOR, .P_USERID = username _
                                                        , .P_YEAR = IIf(year Is Nothing, System.DBNull.Value, year) _
                                                        , .P_MONTH = IIf(month Is Nothing, System.DBNull.Value, month) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = IIf(period Is Nothing, System.DBNull.Value, period)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function
        Public Function LockInsTotalSalaryByDate(ByVal from_date As Date? _
                                               , ByVal to_date As Date? _
                                               , ByVal ins_org_id As Double? _
                                               , ByVal period As Double?
                                               ) As Double _
                Implements ServiceContracts.IInsuranceBusiness.LockInsTotalSalaryByDate
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_LOCK_BY_DATE", New With {.P_FROM_DATE = IIf(from_date Is Nothing, System.DBNull.Value, from_date) _
                                                        , .P_TO_DATE = IIf(to_date Is Nothing, System.DBNull.Value, to_date) _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_PERIOD = period})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function
        Public Function CHECK_INS_TOTALSALARY_UNLOCK(ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double?) As Boolean Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_TOTALSALARY_UNLOCK
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As DataTable = rep.ExecuteStore("PKG_INS_BUSINESS.CHECK_INS_TOTALSALARY_UNLOCK", New With {.P_YEAR = year,
                                                                                                                 .P_MONTH = month,
                                                                                                                 .P_INS_ORG_ID = ins_org_id,
                                                                                                               .P_OUT = OUT_CURSOR})
                If objS.Rows(0)("ID") = "1" Then
                    Return True
                Else
                    Return False
                End If

                Return True
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function CHECK_INS_TOTALSALARY_UNLOCK_BYDATE(ByVal from_date As Date? _
                                               , ByVal to_date As Date? _
                                           , ByVal ins_org_id As Double?) As Boolean Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_TOTALSALARY_UNLOCK_BYDATE
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As DataTable = rep.ExecuteStore("PKG_INS_BUSINESS.CHECK_INS_TOTALSALARY_UNLOCK_BYDATE", New With {.P_FROM_DATE = from_date,
                                                                                                                 .P_TO_DATE = to_date,
                                                                                                                 .P_INS_ORG_ID = ins_org_id,
                                                                                                               .P_OUT = OUT_CURSOR})
                If objS.Rows(0)("ID") = "1" Then
                    Return True
                Else
                    Return False
                End If

                Return True
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "Cap nhat thong tin BH"
        Public Function GetListDataImportHelth(ByVal id As Double?) As DataSet _
                Implements ServiceContracts.IInsuranceBusiness.GetListDataImportHelth
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As DataSet = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_GET_LIST_DATA_HEALTHIMPORT", New With {.P_ID = IIf(id Is Nothing, System.DBNull.Value, id), .P_CUR1 = OUT_CURSOR, .P_CUR2 = OUT_CURSOR, .P_CUR3 = OUT_CURSOR, .P_CUR4 = OUT_CURSOR, .P_CUR5 = OUT_CURSOR}, False)

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try

        End Function
        Public Function GET_INS_HEATH_IMPORT() As DataSet _
                Implements ServiceContracts.IInsuranceBusiness.GET_INS_HEATH_IMPORT
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As DataSet = rep.ExecuteStore("PKG_RECRUITMENT_EXPORT.GET_INS_HEATH_IMPORT", New With {.P_CUR1 = OUT_CURSOR, .P_CUR2 = OUT_CURSOR, .P_CUR3 = OUT_CURSOR}, False)

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try

        End Function
        Public Function GetInsHealthImport(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As String _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal insurance_id As Double? _
                                        , ByVal effective_from_date As Date? _
                                        , ByVal effective_to_date As Date?) As DataTable _
                                            Implements ServiceContracts.IInsuranceBusiness.GetInsHealthImport
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_HEALTH_IMPORT", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_INS_ORG_ID = ins_org_id _
                                                        , .P_INSURANCE_ID = IIf(insurance_id Is Nothing, System.DBNull.Value, insurance_id) _
                                                        , .P_EFFECTIVE_FROM_DATE = IIf(effective_from_date Is Nothing, System.DBNull.Value, effective_from_date) _
                                                        , .P_EFFECTIVE_TO_DATE = IIf(effective_to_date Is Nothing, System.DBNull.Value, effective_to_date)
                                                        })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


        Public Function GetInsHealthImportCheckOrg(ByVal username As String, ByVal id As Double? _
                                                , ByVal employee_id As String _
                                                , ByVal ins_org_id As String _
                                                , ByVal insurance_id As Double? _
                                                , ByVal effective_from_date As Date? _
                                                , ByVal effective_to_date As Date?) As DataTable _
   Implements ServiceContracts.IInsuranceBusiness.GetInsHealthImportCheckOrg
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_HEALTH_IMPORT_2", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_INS_ORG_ID = ins_org_id _
                                                        , .P_INSURANCE_ID = IIf(insurance_id Is Nothing, System.DBNull.Value, insurance_id) _
                                                        , .P_EFFECTIVE_FROM_DATE = IIf(effective_from_date Is Nothing, System.DBNull.Value, effective_from_date) _
                                                        , .P_EFFECTIVE_TO_DATE = IIf(effective_to_date Is Nothing, System.DBNull.Value, effective_to_date)
                                                        })

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsHealthImport(ByVal username As String, ByVal id As Double? _
                                            , ByVal employee_id As String _
                                            , ByVal ins_org_id As Double? _
                                            , ByVal insurance_id As Double? _
                                            , ByVal effective_from_date As Date? _
                                            , ByVal effective_to_date As Date?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsHealthImport
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_HEALTH_IMPORT", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_INS_ORG_ID = ins_org_id _
                                                        , .P_INSURANCE_ID = IIf(insurance_id Is Nothing, System.DBNull.Value, insurance_id) _
                                                        , .P_EFFECTIVE_FROM_DATE = IIf(effective_from_date Is Nothing, System.DBNull.Value, effective_from_date) _
                                                        , .P_EFFECTIVE_TO_DATE = IIf(effective_to_date Is Nothing, System.DBNull.Value, effective_to_date)
                                                        })

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsHealthImport(ByVal username As String, ByVal id As Double?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsHealthImport
            Try
                'Dim rep As New DataAccess.QueryData
                'Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_HEALTH_IMPORT", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function
        Public Function UpdateHealthImport(ByVal username As String, ByVal employee_id As String _
                                                                , ByVal ins_org_name As String _
                                                                , ByVal seniority_insurance As String _
                                                                , ByVal social_number As String _
                                                                , ByVal social_status As String _
                                                                , ByVal social_grant_date As String _
                                                                , ByVal social_save_number As String _
                                                                , ByVal health_number As String _
                                                                , ByVal health_status As String _
                                                                , ByVal health_effect_from_date As String _
                                                                , ByVal health_effect_to_date As String _
                                                                , ByVal health_receive_date As String _
                                                                , ByVal health_receiver As String _
                                                                , ByVal health_area_ins As String
                                                                      ) As Double _
               Implements ServiceContracts.IInsuranceBusiness.UpdateHealthImport
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_HEALTH_IMPORT", New With {.P_USERID = username _
                            , .P_EMPLOYEE_ID = IIf(employee_id Is Nothing, System.DBNull.Value, employee_id) _
                            , .P_INS_ORG_NAME = IIf(ins_org_name Is Nothing, System.DBNull.Value, ins_org_name) _
                            , .P_SENIORITY_INSURANCE = IIf(seniority_insurance Is Nothing, System.DBNull.Value, seniority_insurance) _
                            , .P_SOCIAL_NUMBER = IIf(social_number Is Nothing, System.DBNull.Value, social_number) _
                            , .P_SOCIAL_STATUS = IIf(social_status Is Nothing, System.DBNull.Value, social_status) _
                            , .P_SOCIAL_GRANT_DATE = IIf(social_grant_date Is Nothing, System.DBNull.Value, social_grant_date) _
                            , .P_SOCIAL_SAVE_NUMBER = IIf(social_save_number Is Nothing, System.DBNull.Value, social_save_number) _
                            , .P_HEALTH_NUMBER = IIf(health_number Is Nothing, System.DBNull.Value, health_number) _
                            , .P_HEALTH_STATUS = IIf(health_status Is Nothing, System.DBNull.Value, health_status) _
                            , .P_HEALTH_EFFECT_FROM_DATE = IIf(health_effect_from_date Is Nothing, System.DBNull.Value, health_effect_from_date) _
                            , .P_HEALTH_EFFECT_TO_DATE = IIf(health_effect_to_date Is Nothing, System.DBNull.Value, health_effect_to_date) _
                            , .P_HEALTH_RECEIVE_DATE = IIf(health_receive_date Is Nothing, System.DBNull.Value, health_receive_date) _
                            , .P_HEALTH_RECEIVER = IIf(health_receiver Is Nothing, System.DBNull.Value, health_receiver) _
                            , .P_HEALTH_AREA_INS = IIf(health_area_ins Is Nothing, System.DBNull.Value, health_area_ins)
                                })

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function


        'INS_INFOR              Cập nhật + sổ, thẻ BH
        Public Function GetInsHealthExt(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As String _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal health_ins_card As String _
                                        , ByVal effective_from_date As Date? _
                                        , ByVal effective_to_date As Date? _
                                        , ByVal health_from_date As Date? _
                                        , ByVal health_to_date As Date?
                                        ) As DataTable _
           Implements ServiceContracts.IInsuranceBusiness.GetInsHealthExt
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPS_INS_HEALTH_EXT", New With {.P_CUR = OUT_CURSOR, .P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                    , .P_EMPLOYEE_ID = employee_id _
                                                    , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                    , .P_HEALTH_INS_CARD = IIf(health_ins_card Is Nothing, System.DBNull.Value, health_ins_card) _
                                                    , .P_EFFECTIVE_FROM_DATE = IIf(effective_from_date Is Nothing, System.DBNull.Value, effective_from_date) _
                                                    , .P_EFFECTIVE_TO_DATE = IIf(effective_to_date Is Nothing, System.DBNull.Value, effective_to_date) _
                                                    , .P_HEALTH_FROM_DATE = IIf(health_from_date Is Nothing, System.DBNull.Value, health_from_date) _
                                                    , .P_HEALTH_TO_DATE = IIf(health_to_date Is Nothing, System.DBNull.Value, health_to_date)})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function UpdateInsHealthExt(ByVal username As String, ByVal id As Double? _
                                        , ByVal employee_id As String _
                                        , ByVal ins_org_id As Double? _
                                        , ByVal health_ins_card As String _
                                        , ByVal effective_from_date As Date? _
                                        , ByVal effective_to_date As Date? _
                                        , ByVal health_from_date As Date? _
                                        , ByVal health_to_date As Date?) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.UpdateInsHealthExt
            Try
                Dim rep As New DataAccess.QueryData
                Dim objU As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPU_INS_HEALTH_EXT", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id) _
                                                        , .P_EMPLOYEE_ID = employee_id _
                                                        , .P_INS_ORG_ID = IIf(ins_org_id Is Nothing, System.DBNull.Value, ins_org_id) _
                                                        , .P_HEALTH_INS_CARD = IIf(health_ins_card Is Nothing, System.DBNull.Value, health_ins_card) _
                                                        , .P_EFFECTIVE_FROM_DATE = IIf(effective_from_date Is Nothing, System.DBNull.Value, effective_from_date) _
                                                        , .P_EFFECTIVE_TO_DATE = IIf(effective_to_date Is Nothing, System.DBNull.Value, effective_to_date) _
                                                        , .P_HEALTH_FROM_DATE = IIf(health_from_date Is Nothing, System.DBNull.Value, health_from_date) _
                                                        , .P_HEALTH_TO_DATE = IIf(health_to_date Is Nothing, System.DBNull.Value, health_to_date)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function DeleteInsHealthExt(ByVal username As String, ByVal id As String) As Double _
                 Implements ServiceContracts.IInsuranceBusiness.DeleteInsHealthExt
            Try
                Dim rep As New DataAccess.QueryData
                Dim objD As Object = rep.ExecuteStore("PKG_INS_BUSINESS.SPD_INS_HEALTH_EXT", New With {.P_USERID = username, .P_ID = IIf(id Is Nothing, System.DBNull.Value, id)})

                Return 1
            Catch ex As Exception
                Return 0
            End Try

        End Function

#End Region

#Region "Chế độ bảo hiểm"
        Public Function GET_MLTTC(ByVal p_date As Date) As DataTable _
   Implements ServiceContracts.IInsuranceBusiness.GET_MLTTC
            Try
                Dim rep As New DataAccess.QueryData
                Dim objS As Object = rep.ExecuteStore("PKG_INS_BUSINESS.GET_MLTTC", New With {.P_DATE = p_date, .P_CUR = OUT_CURSOR})

                Return objS
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

        Public Function RATE_ARREARS_LIST() As DataTable _
           Implements ServiceContracts.IInsuranceBusiness.RATE_ARREARS_LIST
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_INS_LIST.RATE_ARREARS_LIST", New With {.P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function CHECK_INS_ORG_ID(ByVal P_INS_ORG_ID As Decimal) As Boolean _
            Implements ServiceContracts.IInsuranceBusiness.CHECK_INS_ORG_ID
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As DataTable = rep.ExecuteStore("PKG_INS_LIST.CHECK_INS_ORG_ID", New With {.P_INS_ORG_ID = P_INS_ORG_ID, .P_CUR = OUT_CURSOR})

                If Decimal.Parse(dtResult.Rows(0)("ID")) > 0 Then
                    Return False
                Else
                    Return True
                End If

                Return True
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function GET_BHYT_PP(ByVal P_EFFECTIVE_DATE As Date) As DataTable _
           Implements ServiceContracts.IInsuranceBusiness.GET_BHYT_PP
            Try

                Dim rep As New DataAccess.QueryData
                Dim dtResult As Object = rep.ExecuteStore("PKG_INS_LIST.GET_BHYT_PP", New With {.P_EFFECTIVE_DATE = P_EFFECTIVE_DATE, .P_CUR = OUT_CURSOR})
                Return dtResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
