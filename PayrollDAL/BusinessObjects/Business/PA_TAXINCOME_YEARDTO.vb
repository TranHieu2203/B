Public Class PA_TAXINCOME_YEARDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String
    Public Property YEAR As Decimal?
    Public Property PIT_CODE As String
    Public Property BHXH As Decimal?
    Public Property NPT As Decimal?
    Public Property NPT_MONTH As Decimal?
    Public Property DM_GTBT As Decimal?
    Public Property DM_NPT As Decimal?
    Public Property GTGC As Decimal?
    Public Property CHIUTHUE_YEAR As Decimal?
    Public Property THUETNCN_YEAR As Decimal?
    Public Property CHIUTHUE_BOSUNG As Decimal?
    Public Property THUETNCN_BOSUNG As Decimal?
    Public Property THUNHAP_CHIUTHUE As Decimal?
    Public Property THUNHAP_QTT As Decimal?
    Public Property THUETNCN_QTT As Decimal?
    Public Property THUETNCN_PAY As Decimal?
    Public Property THUETNCN_REFUND As Decimal?
    Public Property TOTAL_THUE_TNCN As Decimal?
    Public Property IS_LOCK As Boolean?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

Public Class PA_DOCUMENT_PITDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_CODE_SEARCH As String
    Public Property SERIAL_NO As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property YEAR As Decimal?
    Public Property PIT_CODE As String
    Public Property PIT_NO As String
    Public Property PIT_NO_SEARCH As String
    Public Property CUTRU As Decimal?
    Public Property LIEN1 As Decimal?
    Public Property LIEN1_STATUS As String
    Public Property LIEN1_DATE As Date?
    Public Property LIEN1_FROMDATE As Date?
    Public Property LIEN1_TODATE As Date?
    Public Property LIEN2 As Decimal?
    Public Property LIEN2_STATUS As String
    Public Property LIEN2_DATE As Date?
    Public Property LIEN2_FROMDATE As Date?
    Public Property LIEN2_TODATE As Date?
    Public Property TYPE_INCOME As String
    Public Property PERIOD_REPLY As String
    Public Property TAXABLE_INCOME As Decimal?
    Public Property MONEY_PIT As Decimal?
    Public Property REST_INCOME As Decimal?
    Public Property BIRTH_DATE As Date?
    Public Property GENDER As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

