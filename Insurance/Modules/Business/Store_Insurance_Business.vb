Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class Store_Insurance_Business
    Private rep As New HistaffFrameworkRepository

#Region "Nghiệp vụ"

#Region "Khai báo biến động bảo hiểm"
    'Load danh sách
    Public Function GET_INS_ARISING(ByVal P_USER_NAME As String,
                                   ByVal P_FROM_DATE As Date,
                                    ByVal P_TO_DATE As Date,
                                    ByVal P_ORG_ID As String,
                                    ByVal P_INS_GROUP_ARISING As String,
                                    ByVal P_INS_LIST_ARISING_TYPE As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_ARISING", New List(Of Object)(New Object() {P_USER_NAME, P_FROM_DATE, P_TO_DATE, P_ORG_ID, P_INS_GROUP_ARISING, P_INS_LIST_ARISING_TYPE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function


    Public Function SPU_INS_ARISING(ByVal P_USER_NAME As String,
                                    ByVal P_ID As Int32,
                                    ByVal P_EFFECT_DATE As String,
                                    ByVal P_EMPID As Int32,
                                    ByVal P_INS_LIST_ARISING_TYPE As Int32) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.SPU_INS_ARISING", New List(Of Object)(New Object() {P_USER_NAME, P_ID, P_EFFECT_DATE, P_EMPID, P_INS_LIST_ARISING_TYPE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function
#End Region

    Public Function GET_INS_MATERNITY_MNG(ByVal P_USER_NAME As String,
                                          ByVal P_EMPLOYEE_ID As String,
                                   ByVal P_FROM_DATE As Date?,
                                    ByVal P_TO_DATE As Date?,
                                    ByVal P_ORG_ID As String,
                                    ByVal P_ISTER As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_MATERNITY_MNG", New List(Of Object)(New Object() {P_USER_NAME, P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE, P_ORG_ID, P_ISTER}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_INS_MATERNITY_MNG_BYID(ByVal P_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_MATERNITY_MNG_BYID", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function


    Public Function PRI_INS_ARISING_MATERNITY(ByVal P_ID As Int32,
                                    ByVal P_EMPID As Int32,
                                    ByVal P_EFFECT_DATE As Date?,
                                    ByVal P_INS_ARISING_TYPE As Int32,
                                    ByVal P_USER As String) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_HU_INS.PRI_INS_ARISING_MATERNITY", New List(Of Object)(New Object() {P_ID, P_EMPID, P_EFFECT_DATE, P_INS_ARISING_TYPE, P_USER, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function SAVE_INS_MATERNITY_MNG(ByVal P_ID As Int32,
                                    ByVal P_EMPID As Int32,
                                    ByVal P_NGAY_DU_SINH As Date?,
                                    ByVal P_NGHI_THAI_SAN As Int32,
                                    ByVal P_FROM_DATE As Date?,
                                    ByVal P_TO_DATE As Date?,
                                    ByVal P_FROM_DATE_ENJOY As Date?,
                                    ByVal P_TO_DATE_ENJOY As Date?,
                                    ByVal P_NGAY_SINH As Date?,
                                    ByVal P_SO_CON As Int32,
                                    ByVal P_TIEN_TAM_UNG As Int32,
                                    ByVal P_NgayDiLamSom As Date?,
                                    ByVal P_REMARK As String,
                                    ByVal P_USER As String,
                                    ByVal P_LOG As String) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.SAVE_INS_MATERNITY_MNG", New List(Of Object)(New Object() {P_ID, P_EMPID, P_NGAY_DU_SINH, P_NGHI_THAI_SAN, P_FROM_DATE, P_TO_DATE, P_FROM_DATE_ENJOY, P_TO_DATE_ENJOY, P_NGAY_SINH, P_SO_CON, P_TIEN_TAM_UNG, P_NgayDiLamSom, P_REMARK, P_USER, P_LOG, OUT_NUMBER}))

        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function GET_INS_AON_MNG(ByVal P_USER_NAME As String,
                                   ByVal P_FROM_DATE As Date?,
                                    ByVal P_TO_DATE As Date?,
                                    ByVal P_ORG_ID As String,
                                    ByVal P_YEAR As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_AON_MNG", New List(Of Object)(New Object() {P_USER_NAME, P_FROM_DATE, P_TO_DATE, P_ORG_ID, P_YEAR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function SAVE_INS_AON_MNG(ByVal P_ID As Int32,
                                     ByVal P_EMPLOYEE_ID As Int32,
                                     ByVal P_INS_POSITION_ID As Int32,
                                     ByVal P_FROM_DATE As Date?,
                                     ByVal P_TO_DATE As Date?,
                                     ByVal P_YEAR As Int32,
                                     ByVal P_REMARK As String,
                                     ByVal P_USER As String,
                                     ByVal P_LOG As String) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.SAVE_INS_AON_MNG", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_ID, P_INS_POSITION_ID, P_FROM_DATE, P_TO_DATE, P_YEAR, P_REMARK, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_EMPL_AON(ByVal P_USER_NAME As String,
                                 ByVal P_ORG_ID As String,
                                 ByVal P_APPLY_DATE As Date?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_EMPL_AON", New List(Of Object)(New Object() {P_USER_NAME, P_ORG_ID, P_APPLY_DATE}))
        If Not ds Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_INS_ACCIDENT_MNG(ByVal P_USER_NAME As String,
                                  ByVal P_FROM_DATE As Date?,
                                   ByVal P_TO_DATE As Date?,
                                   ByVal P_ORG_ID As String,
                                   ByVal P_YEAR As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_ACCIDENT_MNG", New List(Of Object)(New Object() {P_USER_NAME, P_FROM_DATE, P_TO_DATE, P_ORG_ID, P_YEAR}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function SAVE_INS_ACCIDENT_MNG(ByVal P_ID As Int32,
                                     ByVal P_EMPLOYEE_ID As Int32,
                                     ByVal P_FROM_DATE As Date?,
                                     ByVal P_TO_DATE As Date?,
                                     ByVal P_YEAR As Int32,
                                     ByVal P_ACC_AMOUNT As Int32,
                                     ByVal P_REMARK As String,
                                     ByVal P_USER As String,
                                     ByVal P_LOG As String) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.SAVE_INS_ACCIDENT_MNG", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE, P_YEAR, P_ACC_AMOUNT, P_REMARK, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_EMPL_ACCIDENT(ByVal P_USER_NAME As String,
                                 ByVal P_ORG_ID As String,
                                 ByVal P_APPLY_DATE As Date?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_EMPL_ACCIDENT", New List(Of Object)(New Object() {P_USER_NAME, P_ORG_ID, P_APPLY_DATE}))
        If Not ds Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_SALARY_NOW_PERIOD(ByVal P_EMPLOYEE_ID As Int32) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.GET_SALARY_NOW_PERIOD", New List(Of Object)(New Object() {P_EMPLOYEE_ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Lương bình quân 6T
    Public Function GET_INS_THAISAN_6T(ByVal P_EMPLOYEE_ID As Int32, ByVal P_DATE As DateTime) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.GET_INS_THAISAN_6", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_DATE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Lương bình quân lương BHXH: Tạm thời lấy từ HU_Salary.STANDARD_SALARY
    Public Function GET_LBQ_BHXH(ByVal P_EMPLOYEE_ID As Int32, ByVal P_DATE As DateTime) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_FUNCTION.SALARY_BASIC_BYDATE", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_DATE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

#Region "Truy thu bảo hiểm sức khẻo"
    Public Function GET_EMPLOYEE_ARR_DETAIL(ByVal ID As Decimal?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_EMPLOYEE_ARR_DETAIL", _
                                                 New List(Of Object)(New Object() {ID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt

    End Function

    Public Function GET_INFO_EMPLOYEE_ARR(ByVal ID As Decimal?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_INFO_EMPLOYEE_ARR", _
                                                 New List(Of Object)(New Object() {ID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt

    End Function

    'Public Function GET_INS_HEALTH_ARR(ByVal _filter As Employee_ParamDTO) As DataTable
    '    Dim dt As New DataTable
    '    Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_INS_HEALTH_ARR", _
    '                                             New List(Of Object)(New Object() {_filter.USER_NAME,
    '                                                                               _filter.ORG_ID,
    '                                                                               _filter.IS_DISSOLVE,
    '                                                                               _filter.EMPLOYEE,
    '                                                                                _filter.FROMDATE,
    '                                                                                _filter.TODATE}))
    '    If ds IsNot Nothing Then
    '        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
    '            dt = ds.Tables(0)
    '        End If
    '    End If
    '    Return dt
    'End Function

    'Public Function INSERT_INS_HEALTH_ARR(ByVal BAIL As INS_HEALTH_ARR_DTO) As Integer
    '    Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_LIST_NEW.INSERT_INS_HEALTH_ARR", New List(Of Object)(New Object() {
    '                                                                                                                          BAIL.EMPLOYEE_ID, _
    '                                                                                                                          BAIL.EFFECT_DATE, _
    '                                                                                                                          BAIL.TER_DATE, _
    '                                                                                                                          BAIL.MONTH_REMMAING, _
    '                                                                                                                          BAIL.MONEY_RETURN, _
    '                                                                                                                          BAIL.AON_GROUP_NAME, _
    '                                                                                                                          BAIL.AON_AMOUNT, _
    '                                                                                                                          BAIL.IS_COMP_PAYMENT, _
    '                                                                                                                          BAIL.REMARK,
    '                                                                                                                          BAIL.CREATED_BY, _
    '                                                                                                                          BAIL.CREATED_LOG, _
    '                                                                                                                          OUT_NUMBER}))
    '    Return Integer.Parse(obj(0).ToString())
    'End Function

    'Public Function UPDATE_INS_HEALTH_ARR(ByVal BAIL As INS_HEALTH_ARR_DTO) As Integer
    '    Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_LIST_NEW.UPDATE_INS_HEALTH_ARR", New List(Of Object)(New Object() {BAIL.ID,
    '                                                                                                                      BAIL.EMPLOYEE_ID, _
    '                                                                                                                      BAIL.EFFECT_DATE, _
    '                                                                                                                      BAIL.TER_DATE, _
    '                                                                                                                      BAIL.MONTH_REMMAING, _
    '                                                                                                                      BAIL.MONEY_RETURN, _
    '                                                                                                                      BAIL.AON_GROUP_NAME, _
    '                                                                                                                      BAIL.AON_AMOUNT, _
    '                                                                                                                      BAIL.IS_COMP_PAYMENT, _
    '                                                                                                                      BAIL.REMARK,
    '                                                                                                                      BAIL.CREATED_BY, _
    '                                                                                                                      BAIL.CREATED_LOG, _
    '                                                                                                                       OUT_NUMBER}))
    '    Return Integer.Parse(obj(0).ToString())
    'End Function

#End Region

#Region "Truy thu bảo hiểm tai nạn"
    Public Function GET_EMPLOYEE_ACC_DETAIL(ByVal ID As Decimal?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_EMPLOYEE_ACC_DETAIL", _
                                                 New List(Of Object)(New Object() {ID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt

    End Function

    Public Function GET_INFO_EMPLOYEE_ACC(ByVal ID As Decimal?) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_INFO_EMPLOYEE_ACC", _
                                                 New List(Of Object)(New Object() {ID}))
        If ds IsNot Nothing Then
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
        End If
        Return dt

    End Function

    'Public Function GET_INS_HEALTH_ACC(ByVal _filter As Employee_ParamDTO) As DataTable
    '    Dim dt As New DataTable
    '    Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_LIST_NEW.GET_INS_HEALTH_ACC", _
    '                                             New List(Of Object)(New Object() {_filter.USER_NAME,
    '                                                                               _filter.ORG_ID,
    '                                                                               _filter.IS_DISSOLVE,
    '                                                                               _filter.EMPLOYEE,
    '                                                                                _filter.FROMDATE,
    '                                                                                _filter.TODATE}))
    '    If ds IsNot Nothing Then
    '        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
    '            dt = ds.Tables(0)
    '        End If
    '    End If
    '    Return dt
    'End Function

    'Public Function INSERT_INS_HEALTH_ACC(ByVal BAIL As INS_HEALTH_ACC_DTO) As Integer
    '    Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_LIST_NEW.INSERT_INS_HEALTH_ACC", New List(Of Object)(New Object() {
    '                                                                                                                          BAIL.EMPLOYEE_ID, _
    '                                                                                                                          BAIL.EFFECT_DATE, _
    '                                                                                                                          BAIL.TER_DATE, _
    '                                                                                                                          BAIL.MONTH_REMMAING, _
    '                                                                                                                          BAIL.MONEY_RETURN, _
    '                                                                                                                          BAIL.ACC_AMOUNT, _
    '                                                                                                                          BAIL.IS_COMP_PAYMENT, _
    '                                                                                                                          BAIL.REMARK,
    '                                                                                                                          BAIL.CREATED_BY, _
    '                                                                                                                          BAIL.CREATED_LOG, _
    '                                                                                                                          OUT_NUMBER}))
    '    Return Integer.Parse(obj(0).ToString())
    'End Function

    'Public Function UPDATE_INS_HEALTH_ACC(ByVal BAIL As INS_HEALTH_ACC_DTO) As Integer
    '    Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_LIST_NEW.UPDATE_INS_HEALTH_ACC", New List(Of Object)(New Object() {BAIL.ID,
    '                                                                                                                      BAIL.EMPLOYEE_ID, _
    '                                                                                                                      BAIL.EFFECT_DATE, _
    '                                                                                                                      BAIL.TER_DATE, _
    '                                                                                                                      BAIL.MONTH_REMMAING, _
    '                                                                                                                      BAIL.MONEY_RETURN, _
    '                                                                                                                      BAIL.ACC_AMOUNT, _
    '                                                                                                                      BAIL.IS_COMP_PAYMENT, _
    '                                                                                                                      BAIL.REMARK,
    '                                                                                                                      BAIL.CREATED_BY, _
    '                                                                                                                      BAIL.CREATED_LOG, _
    '                                                                                                                       OUT_NUMBER}))
    '    Return Integer.Parse(obj(0).ToString())
    'End Function

#End Region

    Public Function GET_INS_AON_FAMILY_MNG(ByVal P_EMPL_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_AON_FAMILY_MNG", New List(Of Object)(New Object() {P_EMPL_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_EMPL_AON_INFO(ByVal P_EMPL_ID As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_EMPL_AON_INFO", New List(Of Object)(New Object() {P_EMPL_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function SAVE_INS_AON_FAMILY_MNG(ByVal P_ID As Int32,
                                    ByVal P_EMPLOYEE_ID As Int32,
                                    ByVal P_INS_POSITION_ID As Int32?,
                                    ByVal P_HU_FAMILY_ID As Int32,
                                     ByVal P_APPLY_YEAR As Int32,
                                    ByVal P_FROM_DATE As Date?,
                                    ByVal P_TO_DATE As Date?,
                                    ByVal P_USER As String,
                                    ByVal P_LOG As String) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.SAVE_INS_AON_FAMILY_MNG", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_ID, P_INS_POSITION_ID, P_HU_FAMILY_ID, P_APPLY_YEAR, P_FROM_DATE, P_TO_DATE, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

#End Region



    Public Function GET_LIST_PAGING(ByVal P_CURRENT_PAGE As Int32, ByVal P_NEXT_PAGE As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_PAGING", New List(Of Object)(New Object() {"ins_information", String.Empty, -1, P_CURRENT_PAGE, P_NEXT_PAGE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function


    Public Function GET_SALARY_BY_EFFECT_DATE(ByVal P_EMPLOYEE_ID As Int32, ByVal P_EFFECT_DATE As Date) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.GET_SALARY_BY_EFFECT_DATE", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_EFFECT_DATE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_SALARY_OLD_BY_EFFECT_DATE(ByVal P_EMPLOYEE_ID As Int32, ByVal P_EFFECT_DATE As Date) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.GET_SALARY_OLD_BY_EFFECT_DATE", New List(Of Object)(New Object() {P_EMPLOYEE_ID, P_EFFECT_DATE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function


    Public Function GET_SAL_BY_AREA(ByVal P_EMPL_ID As Int32, ByVal P_NEW_SALARY As Int32, ByVal P_OLD_SALARY As Int32, ByVal P_EFFECTIVE_DATE As Date) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_BUSINESS.GET_SAL_BY_AREA", New List(Of Object)(New Object() {P_EMPL_ID, P_NEW_SALARY, P_OLD_SALARY, P_EFFECTIVE_DATE}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SPS_INS_ARISING_MANUAL_LOAD(ByVal P_USER As String, ByVal P_EMPLOYEE_ID As Int32, ByVal P_INS_ARISING_TYPE_ID As Int32, ByVal P_datetEffective As Date, ByVal P_ARISING_FROM_MONTH As Date, ByVal P_DECLARE_DATE As Date,
                                                ByVal P_SI_SAL_OLD As Int32, ByVal P_HI_SAL_OLD As Int32, ByVal P_UI_SAL_OLD As Int32, ByVal P_SI_SAL_NEW As Int32, ByVal P_HI_SAL_NEW As Int32, ByVal P_UI_SAL_NEW As Int32, ByVal P_BHTNLD_BNN_SAL_OLD As Int32, ByValP_BHTNLD_BNN_SAL_NEW As Int32) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INS_BUSINESS.SPS_INS_ARISING_MANUAL_LOAD", New List(Of Object)(New Object() {P_USER, P_EMPLOYEE_ID, P_INS_ARISING_TYPE_ID, P_datetEffective, P_ARISING_FROM_MONTH, P_DECLARE_DATE, P_SI_SAL_OLD, P_SI_SAL_NEW, P_HI_SAL_OLD, P_HI_SAL_NEW, P_UI_SAL_OLD, P_UI_SAL_NEW, P_BHTNLD_BNN_SAL_OLD, ByValP_BHTNLD_BNN_SAL_NEW}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SPU_INS_ARISING_MANUAL(ByVal username As String, ByVal id As Double? _
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
                                            , ByVal si_sal_old As Double? _
                                            , ByVal hi_sal_old As Double? _
                                            , ByVal ui_sal_old As Double? _
                                            , ByVal si_sal_new As Double? _
                                            , ByVal hi_sal_new As Double? _
                                            , ByVal ui_sal_new As Double? _
                                            , ByVal P_BHTNLD_BNN As Double? _
                                            , ByVal P_A_BHTNLD_BNN As Double? _
                                            , ByVal P_R_BHTNLD_BNN As Double? _
                                            , ByVal P_BHTNLD_BNN_SAL_NEW As Double? _
                                            , ByVal P_BHTNLD_BNN_SAL_OLD As Double?) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.SPU_INS_ARISING_MANUAL", New List(Of Object)(New Object() {username, IIf(id Is Nothing, System.DBNull.Value, id) _
                                            , employee_id _
                                            , ins_org_id _
                                            , ins_arising_type_id _
                                            , salary_pre_period _
                                            , salary_now_period _
                                            , from_health_ins_card _
                                            , effective_date _
                                            , expire_date _
                                            , declare_date _
                                            , arising_from_month _
                                            , arising_to_month _
                                            , note _
                                            , social_note _
                                            , health_number _
                                            , health_status _
                                            , health_effect_from_date _
                                            , health_effect_to_date _
                                            , health_area_ins_id _
                                            , health_receive_date _
                                            , health_receiver _
                                            , health_return_date _
                                            , unemp_from_moth _
                                            , unemp_to_month _
                                            , unemp_register_month _
                                            , r_from _
                                            , o_from _
                                            , r_to _
                                            , o_to _
                                            , r_si _
                                            , o_si _
                                            , r_hi _
                                            , o_hi _
                                            , r_ui _
                                            , o_ui _
                                            , a_from _
                                            , a_to _
                                            , a_si _
                                            , a_hi _
                                            , a_ui _
                                            , si _
                                            , hi _
                                            , ui _
                                            , si_sal_old, hi_sal_old, ui_sal_old, si_sal_new, hi_sal_new, ui_sal_new _
                                            , P_BHTNLD_BNN, P_A_BHTNLD_BNN, P_R_BHTNLD_BNN, P_BHTNLD_BNN_SAL_NEW, P_BHTNLD_BNN_SAL_OLD}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GET_ARISING_MANUAL(ByVal P_USERID As String,
                                       ByVal p_orgid As Decimal?,
                                       ByVal p_frdate As Date?,
                                       ByVal p_todate As Date?,
                                       ByVal p_empcode As String,
                                       ByVal P_ISTER As Double?,
                                       ByVal p_page As Integer,
                                       ByVal p_size As Integer
                                       ) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_ARISING_MANUAL_NEW",
                                  New List(Of Object)(New Object() {P_USERID,
                                                                    p_orgid,
                                                                    p_frdate,
                                                                    p_todate,
                                                                    p_empcode,
                                                                    P_ISTER,
                                                                    p_page,
                                                                    p_size}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function COUNT_ARISING_MANUAL(ByVal P_USERID As String,
                                         ByVal p_orgid As Decimal?,
                                         ByVal p_frdate As Date?,
                                         ByVal p_todate As Date?,
                                         ByVal p_empcode As String,
                                         ByVal P_ISTER As Double?) As Int32
        Dim objects = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.COUNT_ARISING_MANUAL",
                                             New List(Of Object)(New Object() {P_USERID,
                                                                               p_orgid,
                                                                               p_frdate,
                                                                               p_todate,
                                                                               p_empcode,
                                                                               P_ISTER,
                                                                               OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function

    Public Function GET_INS_INFO(ByVal P_USERID As String,
                                 ByVal P_ID As Decimal?,
                                 ByVal P_EMPLOYEE_ID As String,
                                 ByVal P_CMND As String,
                                 ByVal P_ORG_ID As Decimal?,
                                 ByVal P_FROM_DATE As Date?,
                                 ByVal P_TO_DATE As Date?,
                                 ByVal P_ISTER As Double?,
                                 ByVal p_page As Integer,
                                 ByVal p_size As Integer
                                 ) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_INSURANCE_BUSINESS.GET_INS_INFO",
                                  New List(Of Object)(New Object() {P_USERID,
                                                                    P_ID,
                                                                    P_EMPLOYEE_ID,
                                                                    P_CMND,
                                                                    P_ORG_ID,
                                                                    P_FROM_DATE,
                                                                    P_TO_DATE,
                                                                    P_ISTER,
                                                                    p_page,
                                                                    p_size}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GET_INS_INFO_COUNT(ByVal P_USERID As String,
                                 ByVal P_ID As Decimal?,
                                 ByVal P_EMPLOYEE_ID As String,
                                 ByVal P_CMND As String,
                                 ByVal P_ORG_ID As Decimal?,
                                 ByVal P_FROM_DATE As Date?,
                                 ByVal P_TO_DATE As Date?,
                                 ByVal P_ISTER As Double?
                                 ) As Int32
        Dim objects = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.GET_INS_INFO_COUNT",
                                             New List(Of Object)(New Object() {P_USERID,
                                                                P_ID,
                                                                P_EMPLOYEE_ID,
                                                                P_CMND,
                                                                P_ORG_ID,
                                                                P_FROM_DATE,
                                                                P_TO_DATE,
                                                                P_ISTER,
                                                                OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function

    Public Function DELETE_INS_THAISAN(ByVal P_EMPLOYEE_CODE As String, ByVal P_FROM_DATE As Date?, ByVal P_TO_DATE As Date?) As Int32
        Dim objects = rep.ExecuteStoreScalar("PKG_INSURANCE_BUSINESS.DELETE_INS_THAISAN",
                                             New List(Of Object)(New Object() {P_EMPLOYEE_CODE,
                                                                P_FROM_DATE,
                                                                P_TO_DATE,
                                                                OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function

    Public Function SPU_INS_TOTALSALARY_LOCK(ByVal username As String, ByVal year As Double? _
                                          , ByVal month As Double? _
                                          , ByVal ins_org_id As Double?) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_LOCK_1", New List(Of Object)(New Object() {username, year, month, ins_org_id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function SPU_INS_TOTALSALARY_UNLOCK(ByVal username As String, ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double?) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_BUSINESS.SPU_INS_TOTALSALARY_UNLOCK", New List(Of Object)(New Object() {username, year, month, ins_org_id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function GET_INS_TOTALSALARY_UNLOCK(ByVal username As String, ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double?) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_BUSINESS.GET_INS_TOTALSALARY_UNLOCK", New List(Of Object)(New Object() {username, year, month, ins_org_id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function GET_INS_TOTALSALARY_ALL_UNLOCK(ByVal username As String, ByVal year As Double? _
                                          , ByVal startdate As Date? _
                                          , ByVal enddate As Date? _
                                          , ByVal ins_org_id As Double?) As Int32

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_BUSINESS.GET_INS_TOTALSALARY_ALL_UNLOCK", New List(Of Object)(New Object() {username, year, startdate, enddate, ins_org_id, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())

    End Function

    Public Function CHECK_INS_TOTALSALARY_UNLOCK(ByVal year As Double? _
                                           , ByVal month As Double? _
                                           , ByVal ins_org_id As Double?) As String

        Dim obj As Object = rep.ExecuteStoreScalar("PKG_INS_BUSINESS.CHECK_INS_TOTALSALARY_UNLOCK", New List(Of Object)(New Object() {year, month, ins_org_id, OUT_NUMBER}))
        Return obj(0).ToString()

    End Function


End Class
