Public Class HUAllowanceDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property HU_WORKING_ID As Decimal?
    Public Property ALLOWANCE_LIST_ID As Decimal?
    Public Property ALLOWANCE_TYPE As Decimal?
    Public Property ALLOWANCE_LIST_NAME As String
    Public Property AMOUNT As Decimal?
    Public Property IS_INSURRANCE As Boolean?
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property REMARK As String
    Public Property FACTOR As Decimal?
    Public Property EMPLOYEE_SIGNER_ID As Decimal?
    Public Property EMPLOYEE_SIGNER_NAME As String
    Public Property EMPLOYEE_SIGNER_SR_NAME As String
    Public Property EMPLOYEE_SIGNER_TITLE_NAME As String
    Public Property SIGNER_LEVEL_NAME As String
    Public Property SIGNER_NAME As String
    Public Property SIGNER_TITLE_NAME As String
    Public Property DATE_SIGNER As Date?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property IS_NEW As Boolean?
    Public Property IS_TER As Boolean?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_EFFECT_DATE As Date?

    Public Property ORG_DESC As String
    Public Property ORG_NAME1 As String
    Public Property ORG_NAME2 As String
    Public Property ORG_NAME3 As String
    Public Property PAYROLL As String
    Public Property STAFF_RANK_NAME As String
    Public Property TITLE_NAME_VN As String
    Public Property TITLE_JOB_NAME As String
    Public Property LEARNING_LEVEL_NAME As String

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property ACTFLG As String
    Public Property IS_INSUR As Decimal?

    Public Property DETAIL_ADD As Decimal?
    Public Property DETAIL_ADD_NAME As String
    Public Property IS_DEDUCT As Boolean?
    Public Property PERCENT_PIT As Decimal?
    Public Property AMOUNT_PIT As Decimal?
    Public Property FILE_NAME As String
    Public Property UPLOAD_FILE As String

End Class
