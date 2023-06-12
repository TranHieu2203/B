Public Class INS_HEALTH_INSURANCE_DTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property ORG_ID As String
    Public Property ORG_DESC As String
    Public Property YEAR As Decimal?
    Public Property INS_CONTRACT_ID As Decimal?
    Public Property INS_CONTRACT_DE_ID As Decimal?
    Public Property CHECK_BHNT As Decimal? '0:CHECKED,1:UNCHECK
    Public Property FAMYLI_ID As Decimal?
    Public Property DT_CHITRA As Decimal?
    Public Property DT_CHITRA_NAME As String
    Public Property JOIN_DATE As Date?

    Public Property EFFECT_DATE As Date?
    Public Property MONEY_INS As Decimal?
    Public Property REDUCE_DATE As Date?
    Public Property REFUND As Decimal?
    Public Property DATE_RECEIVE_MONEY As Date?
    Public Property EMP_RECEIVE_MONEY As String
    Public Property NOTES As String

    Public Property NAME_PROGRAM As String   'TEN CHUONG TRINH
    Public Property SOTIEN As Decimal? 'INS_LIST_CONTRACT_DETAIL->MONEY_INS

    Public Property ORG_INSURANCE As String  'TEN CHUONG TRINH BAO HIEM
    Public Property VAL_CO As Decimal? 'INS_LIST_CONTRACT->VAL_CO: GIA TRI HOP DONG
    Public Property START_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property CLAIM As String
    Public Property TOTAL As Decimal?

    Public Property FULLNAME As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As String
    Public Property PHONG_BAN As String
    Public Property HOTENNGUOITHAN As String
    Public Property MOIQUANHE As String
    Public Property NGAYSINHTN As Date?
    Public Property CMNDTN As String

    Public Property CONTRACT_INS_NO As String

    '------------------------------------------
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property IS_CLAIM As Decimal?
    Public Property IS_TERMINATE As Decimal?
    Public Property JOIN_DATE_END As Date?
    Public Property EFFECT_DATE_END As Date?
    Public Property REDUCE_DATE_END As Date?

End Class
