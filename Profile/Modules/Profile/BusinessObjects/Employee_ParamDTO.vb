Public Class Employee_ParamDTO
    Public Property EMPLOYEE As String
    Public Property USER_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property IS_DISSOLVE As Decimal?

    Public Property TITLE_ID As Decimal?
    Public Property FROMDATE As Date?
    Public Property TODATE As Date?
    Public Property EMP_TYPE As String
    Public Property IS_NOT_CONTRACT As Boolean?
    Public Property IS_TERMINATE As Boolean?
    Public Property IS_ALL As Boolean?

    Public Property DECISION_TYPE As Decimal?
    Public Property IS_TEMP_DECISION As Boolean?
    Public Property CONTRACT_TYPE As Decimal?
    Public Property IS_NEW_CONTRACT As Boolean?
    Public Property STATUS As String
    Public Property GROUPSALARY As Decimal?

    Public Property REASON_TYPE As String
    Public Property IS_INSTEAD As Boolean?
    Public Property IS_REEMPLOYE As Boolean?
    Public Property IS_BLACKLIST As Boolean?
    Public Property FROM_DATE_SEND As Date?
    Public Property TO_DATE_SEND As Date?
    Public Property FROM_DATE_LAST As Date?
    Public Property TO_DATE_LAST As Date?

    Public Property DISCIPLINE_OBJECT As String
    Public Property DISCIPLINE_FORM As String

    Public Property COMMEND_OBJECT As String
    Public Property COMMEND_FORM As String

    Public Property WELFACEMONEY As Decimal?
    Public Property SENIORITYFROM As Decimal?
    Public Property SENIORITYTO As Decimal?
    Public Property LSTGENDER As String
    Public Property LSTCONTRACTTYPE As String
    Public Property CHILD_OLD_FROM As Decimal?
    Public Property CHILD_OLD_TO As Decimal?

    Public Property PERIOD As Decimal?
    Public Property CONCURENTLY_TYPE As String
    Public Property PAGE As Decimal?
    Public Property SIZE As Decimal?
    Public Property EMPLOYEE_GROUP As String
    Public Property IS_TERMINATE_ALL As Boolean?



    ' Fill page hu_salary
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME_VN As String
    Public Property ORG_NAME As String
    Public Property TITLE_NAME As String
    Public Property GROUP_SALARY_NAME As String
    Public Property TAX_SALARY As String
    Public Property SCALE_SALARY_NAME As String
    Public Property NGACH As String
    Public Property RANK_NAME As String
    Public Property STANDARD_SALARY As String
    Public Property EXCHANGE_RATE As String
    Public Property INS_SALARY As String
    Public Property SUMALLOW_VND As String
    Public Property SUMALLOW_USD As String
    Public Property SUMALLOW_TOTAL As String
    Public Property EFFECT_DATE As String
    Public Property SALARY_NO As String
    Public Property SIGN_NAME1 As String
    Public Property SIGN_TITLE_NAME1 As String
    Public Property SIGN_NAME2 As String
    Public Property SIGN_TITLE_NAME2 As String
    Public Property STATUS_NAME As String


    'Fill HU_TERMINATE
    Public Property DECISION_TYPE_NAME As String

End Class
