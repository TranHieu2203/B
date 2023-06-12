Public Class DisciplineDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_IDS As List(Of Decimal)
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_CODE As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property DISCIPLINE_OBJ As Decimal?
    Public Property DISCIPLINE_OBJ_NAME As String
    Public Property DISCIPLINE_OBJ_CODE As String
    Public Property DISCIPLINE_TYPE As Decimal?
    Public Property DISCIPLINE_TYPE_NAME As String
    Public Property DISCIPLINE_LEVEL As Decimal?
    Public Property DISCIPLINE_LEVEL_NAME As String
    Public Property DISCIPLINE_REASON As Decimal?
    Public Property DISCIPLINE_REASON_NAME As String
    Public Property MONEY As Decimal?
    Public Property REMARK As String
    Public Property DECISION_ID As Decimal?
    Public Property DECISION_NO As String
    Public Property DISCIPLINE_DATE As Date?
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS_ID As Decimal
    Public Property STATUS_NAME As String
    Public Property STATUS_CODE As String
    Public Property DEDUCT_FROM_SALARY As Boolean
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String
    Public Property YEAR_PERIOD As Decimal?
    Public Property INDEMNIFY_MONEY As Decimal?
    Public Property PERFORM_TIME As String

    Public Property DEL_DISCIPLINE_DATE As Date?
    Public Property NOTE_DISCIPLINE As String
    Public Property DISCIPLINE_REASON_DETAIL As String
    Public Property VIOLATION_DATE As Date?
    Public Property DECTECT_VIOLATION_DATE As Date?
    Public Property EXPLAIN As String
    Public Property RERARK_DISCIPLINE As String
    Public Property PAIDMONEY As Decimal?
    Public Property AMOUNT_PAID_CASH As Decimal?
    Public Property AMOUNT_TO_PAID As Decimal?
    Public Property AMOUNT_SAL_MONTH As Decimal?
    Public Property AMOUNT_IN_MONTH As Decimal?
    Public Property AMOUNT_DEDUCT_AMOUNT As Decimal?
    Public Property NO_DISCIPLINE As String

    Public Property DISCIPLINE_EMP As List(Of DisciplineEmpDTO)
    Public Property DISCIPLINE_ORG As List(Of DisciplineOrgDTO)

    Public Property NO As String
    Public Property NAME As String
    Public Property TYPE As Decimal?
    Public Property TYPE_CODE As String
    Public Property TYPE_NAME As String
    Public Property SIGNER_NAME As String
    Public Property SIGNER_TITLE As String
    Public Property SIGN_DATE As Date?
    Public Property ISSUE_DATE As Date?
    Public Property UPLOADFILE As String
    Public Property FILENAME As String
    Public Property param As ParamDTO
    Public Property IS_TERMINATE As Boolean
    Public Property EFFECT_FROM As Date?
    Public Property EFFECT_TO As Date?

    Public Property IS_AMOUNT_IN_MONTH As Boolean
    Public Property YEAR As Decimal?
    Public Property LEVEL_ID As Decimal?
    Public Property LEVEL_NAME As String
    Public Property IS_PORTAL As Decimal?
    Public Property IS_PORTAL_NAME As String
    Public Property FULLNAME_TEXT As String
    Public Property ORG_TEXT As String
    Public Property TITLE_TEXT As String
End Class
