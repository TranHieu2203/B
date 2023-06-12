Public Class ContractDTO
    Public Property ID As Decimal
    Public Property ORG_ID As Decimal
    Public Property ORG_IDS As List(Of Decimal)
    Public Property ORG_CODE As String
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACTTYPE_ID As Decimal?
    Public Property CONTRACTTYPE_CODE As String
    Public Property CONTRACTTYPE_NAME As String
    Public Property STAFF_RANK_NAME As String
    Public Property STAFF_RANK_ID As Decimal?

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property START_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property SIGN_ID As Decimal?
    Public Property SIGNER_TITLE As String
    Public Property SIGNER_NAME As String
    Public Property SIGN_DATE As Date?
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property WORK_STATUS As Decimal?
    Public Property IS_TER As Boolean
    Public Property TITLE_NAME As String
    Public Property REMARK As String
    Public Property STATUS_ID As Decimal?
    Public Property DIRECT_MANAGER As Decimal?
    Public Property STATUS_CODE As String
    Public Property STATUS_NAME As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String


    Public Property DECISION_NO As String
    Public Property SAL_GROUP_ID As Decimal?
    Public Property SAL_GROUP_NAME As String

    Public Property MORNING_START As Date?
    Public Property MORNING_STOP As Date?
    Public Property AFTERNOON_START As Date?
    Public Property AFTERNOON_STOP As Date?

    Public Property SAL_LEVEL_ID As Decimal?
    Public Property SAL_LEVEL_NAME As String

    Public Property SAL_RANK_ID As Decimal?
    Public Property SAL_RANK_NAME As String
    Public Property SAL_BASIC As Decimal?
    Public Property SAL_TOTAL As Decimal?
    Public Property PERCENT_SALARY As Decimal?
    Public Property WORKING_ID As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property Working As WorkingDTO
    Public Property ListAttachFiles As List(Of AttachFilesDTO)
    Public Property OBJECTTIMEKEEPING As Decimal?
    Public Property ID_SIGN_CONTRACT As Decimal?
    Public Property NAME_SIGN_CONTRACT As String
    Public Property OBJECT_LABOUR As Decimal?
    'id ten va chuc danh tai thoi diem ky hop dong cua ng ky 2
    Public Property SIGN_ID2 As Decimal?
    Public Property SIGNER_TITLE2 As String
    Public Property SIGNER_NAME2 As String

    'Dinh kem file
    Public Property ATTACH_FILE As String
    Public Property FILENAME As String
    'ngay thanh ly,tap tin dinh kem
    Public Property LIQUIDATION_DATE As Date?
    Public Property REMARK_LIQUIDATION As String

    Public Property WORK_PLACE_ID As Decimal?
    Public Property WORK_PLACE_NAME As String

    Public Property WORK_TIME As String
    Public Property WORK_DES As String

    Public Property OBJECT_EMPLOYEE_ID As Decimal?

    Public Property OBJECT_ATTENDANT_ID As Decimal?

    Public Property THEORY_PHASE_FROM As Date?
    Public Property THEORY_PHASE_TO As Date?
    Public Property PRACTICE_PHASE_FROM As Date?
    Public Property PRACTICE_PHASE_TO As Date?

    Public Property CONTRACTTYPE_TYPE_CODE As String

    Public Property IDs As List(Of Decimal)
    Public Property ORG_NAME2 As String
    Public Property ORG_NAME3 As String
    Public Property ORG_NAME4 As String
    Public Property ORG_NAME5 As String
    Public Property ORG_NAME6 As String
    Public Property ORG_NAME7 As String

    Public Property EXPIRE_FROM As Date?
    Public Property EXPIRE_TO As Date?
    Public Property IS_RECRUITMENT As Boolean?
    Public Property NUMOF_CONTRACT_SIGN As Int64?
    Public Property COMPANY_REG As Decimal?
    Public Property COMPANY_REG_NAME As String
End Class
