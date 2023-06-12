Public Class WorkingDTO
    Public Property ID As Decimal
    Public Property OBJECT_ATTENDANCE As Decimal?
    Public Property OBJECT_ATTENDANCE_NAME As String
    Public Property FILING_DATE As Date?

    Public Property OBJECT_ATTENDANCE_OLD As Decimal?
    Public Property OBJECT_ATTENDANCE_NAME_OLD As String
    Public Property FILING_DATE_OLD As Date?

    Public Property EMPLOYEE_3B_ID As Decimal?
    Public Property CODE As String
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String

    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property TITLE_MOVE_NAME As String
    Public Property TITLE_NAME_OLD As String

    Public Property TITLE_GROUP_ID As Decimal?
    Public Property TITLE_GROUP_NAME As String

    Public Property SAL_PAYMENT_ID As Decimal?
    Public Property SAL_PAYMENT_NAME As String

    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property STAFF_RANK_MOVE_NAME As String

    Public Property DIRECT_MANAGER As Decimal?
    Public Property DIRECT_MANAGER_NAME As String
    Public Property DIRECT_MANAGER_NAME_OLD As String

    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_NAME2 As String
    Public Property ORG_NAME3 As String
    Public Property ORG_DESC As String
    Public Property ORG_MOVE_NAME As String
    Public Property ORG_MOVE_DESC As String
    Public Property ORG_NAME_OLD As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?

    Public Property EFFECT_DATE_OLD As Date?
    Public Property EXPIRE_DATE_OLD As Date?

    Public Property DECISION_TYPE_ID As Decimal?
    Public Property DECISION_TYPE_CODE As String
    Public Property DECISION_TYPE_NAME As String
    Public Property DECISION_TYPE_NAME_OLD As String

    Public Property OBJECT_LABOR As Decimal?
    Public Property OBJECT_LABORNAME As String
    Public Property OBJECT_LABORNAME_OLD As String

    Public Property DECISION_NO As String
    Public Property DECISION_NO_OLD As String

    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String

    Public Property SAL_GROUP_ID As Decimal?
    Public Property SAL_GROUP_NAME As String

    Public Property SAL_LEVEL_ID As Decimal?
    Public Property SAL_LEVEL_NAME As String

    Public Property SAL_RANK_ID As Decimal?
    Public Property SAL_RANK_NAME As String
    Public Property SAL_BASIC As Decimal?
    Public Property SAL_TOTAL As Decimal?

    Public Property COST_SUPPORT As Decimal?

    Public Property SIGN_DATE As Date?
    Public Property SIGN_ID As Decimal?
    Public Property SIGN_CODE As String
    Public Property SIGN_NAME As String
    Public Property SIGN_TITLE As String

    Public Property SIGN_DATE_OLD As Date?
    Public Property SIGN_NAME_OLD As String
    Public Property SIGN_TITLE_OLD As String

    Public Property REMARK As String
    Public Property REMARK_OLD As String
    Public Property PERCENT_SALARY As Decimal?
    Public Property ATTACH_FILE As String
    Public Property FILENAME As String
    Public Property IS_PROCESS As Boolean?
    Public Property IS_MISSION As Boolean?
    Public Property IS_WAGE As Boolean?
    Public Property IS_3B As Boolean?
    Public Property IS_PROCESS_SHORT As Short?
    Public Property IS_MISSION_SHORT As Short?
    Public Property IS_WAGE_SHORT As Short?
    Public Property IS_3B_SHORT As Short?
    Public Property LST_EMP_WORKING As List(Of WorkingDTO)

    Public Property lstAllowance As List(Of WorkingAllowanceDTO)
    Public Property WORKING_OLD As WorkingDTO

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String
    Public Property IS_TER As Boolean
    Public Property TER_EFFECT_DATE As Date?
    Public Property SAL_TYPE_ID As Decimal?
    Public Property SALE_COMMISION_ID As Decimal?
    Public Property SAL_TYPE_NAME As String
    Public Property SALE_COMMISION_NAME As String
    Public Property SAL_INS As Decimal?
    Public Property ALLOWANCE_TOTAL As Decimal?
    Public Property TAX_TABLE_ID As Decimal?
    Public Property TAX_TABLE_Name As String
    Public Property ResponsibilityAllowances As Decimal?
    Public Property WorkAllowances As Decimal?
    Public Property AttendanceAllowances As Decimal?
    Public Property HousingAllowances As Decimal?
    Public Property CarRentalAllowances As Decimal?
    Public Property Ids As List(Of Decimal)
    Public Property UPLOADFILE As String
    Public Property FILENAME1 As String
    Public Property PERCENTSALARY As Decimal?
    Public Property FACTORSALARY As Decimal?
    Public Property OTHERSALARY1 As Decimal?
    Public Property OTHERSALARY2 As Decimal?
    Public Property OTHERSALARY3 As Decimal?
    Public Property OTHERSALARY4 As Decimal?
    Public Property OTHERSALARY5 As Decimal?
    Public Property CODE_ATTENDANCE As Decimal?

    Public Property EMPLOYEE_TYPE As Decimal?
    Public Property EMPLOYEE_TYPE_NAME As String
    Public Property SHORT_NAME As String

    Public Property OBJECT_EMPLOYEE_ID As Decimal?
    Public Property OBJECT_EMPLOYEE_NAME As String
    Public Property OBJECT_ATTENDANT_ID As Decimal?
    Public Property OBJECT_ATTENDANT_NAME As String
    Public Property WORK_PLACE_ID As Decimal?
    Public Property WORK_PLACE_NAME As String

    Public Property SALARY_BHXH As Decimal?
    Public Property ORG_NAME4 As String
    Public Property ORG_NAME5 As String
    Public Property ORG_NAME6 As String
    Public Property ORG_NAME7 As String
    Public Property DM_ID As Decimal?

    Public Property TOXIC_RATE As Decimal?
    Public Property COEFFICIENT As Decimal?
    Public Property TOXIC_SALARY As Decimal?
    Public Property NEXTSALARY_DATE As Date?
    Public Property REGION_SAL As Decimal?
    Public Property GAS_SAL As Decimal?
    Public Property ADDITIONAL_SAL As Decimal?
    Public Property PHONE_SAL As Decimal?
    Public Property SHARE_SAL As Decimal?

    Public Property PC1 As Decimal?
    Public Property PC2 As Decimal?
    Public Property PC3 As Decimal?
    Public Property PC4 As Decimal?
    Public Property PC5 As Decimal?
    Public Property IS_ALLOW_SALARY_LESS_THAN As Boolean
End Class
