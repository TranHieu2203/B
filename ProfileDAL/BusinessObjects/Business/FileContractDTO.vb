Public Class FileContractDTO
    Public Property ID As Decimal
    Public Property FILE_NAME As String
    Public Property FILE_SIZE As Integer?
    Public Property NUMBER_CONTRACT As String
    Public Property EFFECT_DATE As Date?
    Public Property ID_CONTRACT As Decimal?
    Public Property NUMBER_CONTRACT_INS As String
    Public Property SIGN_TITLE_NAME As String 'THnahNT added 14/01/2016
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACTTYPE_ID As Decimal?
    Public Property CONTRACTTYPE_CODE As String
    Public Property CONTRACTTYPE_NAME As String
    Public Property CONTRACTTMP_ID As Decimal?
    Public Property CONTRACTTMP_NAME As String
    Public Property AUTHORIZE_NO As String
    Public Property SIGN_ORG_ID As Decimal?
    Public Property SIGN_ORG_NAME As String
    Public Property JOB As String
    Public Property WORK_TIME As String
    Public Property BREAK_TIME As String
    Public Property START_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property SIGN_ID As Decimal?
    Public Property SIGNER_TITLE As String
    Public Property SIGNER_NAME As String
    Public Property SIGN_DATE As Date?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_CODE_OLD As String
    Public Property EMPLOYEE_NAME As String
    Public Property WORK_STATUS As Decimal?
    Public Property IS_TERMINATE As Boolean
    Public Property TITLE_NAME_LEVEL As String
    Public Property TITLE_NAME As String
    Public Property REMARK As String
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_CODE As String
    Public Property STATUS_NAME As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property SALARY As Decimal?
    Public Property LOCATION_ID As Decimal?
    Public Property LOCATION_NAME As String
    Public Property APPEND_NUMBER As String
    Public Property FILEUPLOAD As String
    Public Property CONTENT_APPEND As String
    Public Property STT As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property APPEND_TYPEID As Decimal?
    Public Property APPEND_TYPE_NAME As String
    Public Property FORM_ID As Decimal?
    Public Property FORM_NAME As String
    Public Property AUTHORITY As Decimal?
    Public Property AUTHORITY_CHECKBOX As Boolean
    Public Property AUTHORITY_NUMBER As String
    Public Property AUTHOR_CHAIRMAIN As Decimal?
    Public Property AUTHOR_CHAIRMAIN_CHECKBOX As Boolean

    Public Property WORKING_ID As Decimal?

    Public Property DECISION_NO As String
    Public Property SAL_GROUP_ID As Decimal?
    Public Property SAL_GROUP_NAME As String

    Public Property SAL_LEVEL_ID As Decimal?
    Public Property SAL_LEVEL_NAME As String

    Public Property SAL_RANK_ID As Decimal?
    Public Property SAL_RANK_NAME As String
    Public Property SAL_BASIC As Decimal?
    Public Property SAL_TOTAL As Decimal?

    Public Property SAL_BHXH As Decimal?
    Public Property SAL_BHYT As Decimal?
    Public Property SAL_BHTN As Decimal?
    Public Property PERCENT_YEAR As Decimal?
    Public Property SAL_YEAR As Decimal?
    Public Property BONUS_FACTOR As Decimal?
    Public Property BONUS As Decimal?
    Public Property lstAllowance As List(Of WorkingAllowanceDTO)

    Public Property IS_TER As Boolean
    Public Property FILENAME As String
    Public Property UPLOADFILE As String

    ' Thêm người ký 2
    Public Property SIGN_ID2 As Decimal?
    Public Property SIGNER_TITLE2 As String
    Public Property SIGNER_NAME2 As String
    Public Property SALARY_BHXH As Decimal?

    Public Property APPEND_TYPE_LIST As List(Of Decimal)
    Public Property WORK_PLACE_ID As Decimal?
    Public Property WORK_PLACE_NAME As String
    Public Property SHORT_NAME As String
    Public Property ORG_NAME2 As String
    Public Property ORG_NAME3 As String
    Public Property ORG_NAME4 As String
    Public Property ORG_NAME5 As String
    Public Property ORG_NAME6 As String
    Public Property ORG_NAME7 As String
    Public Property CONTENT As String
End Class
