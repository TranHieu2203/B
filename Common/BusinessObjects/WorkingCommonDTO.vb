Public Class WorkingCommonDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_CODE_OLD As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property TITLE_NAME_EN As String
    Public Property TITLE_CONCURRENT_ID As Decimal?
    Public Property TITLE_CONCURRENT_NAME As String


    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String

    Public Property ORG_SULP_ID As Decimal?
    Public Property ORG_SULP_NAME As String
    Public Property ORG_SULP_DESC As String


    Public Property DECISION_TYPE As Decimal?
    Public Property DECISION_TYPE_NAME As String
    Public Property DECISION_ABBR As String

    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String
    Public Property DECISION_NO As String
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property EXPIRE_SYSTEM As Date?
    Public Property DECISION_REMARK As String
    Public Property TITLE_PER As Decimal?
    Public Property IS_SALARY_INCOME As Boolean?
    Public Property INC_SAL_GROUP_ID As Decimal?
    Public Property INC_SAL_GROUP_NAME As String
    Public Property INC_SAL_LEVEL_ID As Decimal?
    Public Property INC_SAL_LEVEL_NAME As String
    Public Property INC_SAL_RANK As Decimal?
    Public Property INC_SAL_COEFFICIENT_ID As Decimal?
    Public Property INC_SAL_COEFFICIENT_NAME As String
    Public Property INC_PER As Integer?
    Public Property IS_SALARY_COEFFICIENT As Boolean?
    Public Property COE_SAL_GROUP_ID As Decimal?
    Public Property COE_SAL_GROUP_NAME As String
    Public Property COE_SAL_LEVEL_ID As Decimal?
    Public Property COE_SAL_LEVEL_NAME As String
    Public Property COE_SAL_RANK As Decimal?
    Public Property COE_SAL_COEFFICIENT_ID As Decimal?
    Public Property COE_SAL_COEFFICIENT_NAME As String
    Public Property HOLD_RANK As Date?
    Public Property COE_PER As Integer?
    Public Property STATUS As Decimal?
    Public Property STATUS_CODE As String
    Public Property STATUS_NAME As String
    Public Property WORKING_TYPE As String
    Public Property SIGNER_CODE As String
    Public Property SIGNER_NAME As String
    Public Property SIGNER_TITLE As String
    Public Property SIGN_DATE As Date?
    Public Property REMARK As String

    Public Property SALARY_ID As Decimal?
    Public Property ORGINS_ID As Decimal?
    Public Property ORGINS_NAME As String
    Public Property ORGINS_DESC As String
    Public Property OBJECTLEVEL_ID As Decimal?
    Public Property OBJECTLEVEL_NAME As String
    Public Property FUNCTIONLEVEL_ID As Decimal?
    Public Property FUNCTIONLEVEL_NAME As String
    Public Property TITLE As String
    Public Property DIRECTMANAGER_ID As String
    Public Property DIRECTMANAGER_NAME As String
    Public Property FROMDATE As Date?
    Public Property TODATE As Date?
    Public Property IS_CHALLENGE As Boolean?
    Public Property CHALLENGE_MONTH As Decimal?
    Public Property FILENAME As String
    Public Property FILEBYTE As Byte()
    Public Property OBJECT_TYPE As String

    Public Property OLD_ORG_ID As Decimal?
    Public Property OLD_ORG_NAME As String
    Public Property OLD_ORG_DESC As String
    Public Property OLD_TITLE As String
    Public Property OLD_EFFECT_DATE As Date?

    Public Property F_EMPLOYEE As String
    Public Property F_TEREMP As Boolean
    Public Property F_ORGID As Decimal?
    Public Property F_ISDISSOLVE As Boolean

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

End Class
