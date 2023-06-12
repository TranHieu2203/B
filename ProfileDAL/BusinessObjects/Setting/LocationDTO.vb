Public Class LocationDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property ORG_ID As Decimal?
    Public Property ADDRESS As String
    Public Property CONTRACT_PLACE As String

    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property LOCATION_SHORT_NAME As String
    Public Property WORK_ADDRESS As String
    Public Property PHONE As String
    Public Property FAX As String
    Public Property WEBSITE As String
    Public Property ACCOUNT_NUMBER As String
    Public Property BANK_ID As Decimal?
    Public Property TAX_CODE As String
    Public Property TAX_DATE As Date?
    Public Property TAX_PLACE As String
    Public Property EMP_LAW_ID As Decimal?
    Public Property EMP_LAW_NAME As String
    Public Property EMP_SIGNCONTRACT_ID As Decimal?
    Public Property EMP_SIGNCONTRACT_NAME As String
    Public Property BUSINESS_NUMBER As String
    Public Property BUSINESS_NAME As String
    Public Property NOTE As String
    Public Property LOCATION_VN_NAME As String
    Public Property LOCATION_EN_NAME As String
    Public Property BUSINESS_REG_DATE As Date?
    Public Property BANK_BRANCH_ID As Decimal?

    Public Property PROVINCE_ID As Decimal?
    Public Property DISTRICT_ID As Decimal?
    Public Property WARD_ID As Decimal?
    Public Property IS_SIGN_CONTRACT As Decimal?
    Public Property FILE_LOGO As String
    Public Property FILE_HEADER As String
    Public Property FILE_FOOTER As String

    Public Property ATTACH_FILE_LOGO As String
    Public Property ATTACH_FILE_HEADER As String
    Public Property ATTACH_FILE_FOOTER As String

    Public Property CHANGE_TAX_CODE As String
    Public Property NAME_VN As String
    Public Property REGION As Decimal?
    Public Property REGION_NAME As String

    Public Property INS_LIST_ID As Decimal?
    Public Property INS_LIST_NAME As String

    Public Property DISTRICT_NAME As String
    Public Property PROVINCE_NAME As String
    Public Property WARD_NAME As String
    Public Property BANK_NAME As String
    Public Property BANK_BRANCH_NAME As String
End Class
