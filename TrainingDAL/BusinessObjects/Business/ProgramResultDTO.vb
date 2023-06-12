Public Class ProgramResultDTO
    Public Property ID As Decimal?
    Public Property TR_PROGRAM_ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property DURATION As Decimal?
    Public Property IS_EXAMS As Boolean?
    Public Property IS_END As Boolean?
    Public Property IS_REACH As Boolean?
    Public Property TOIEC_BENCHMARK As Decimal?
    Public Property TOIEC_SCORE_IN As Decimal?
    Public Property TOIEC_SCORE_OUT As Decimal?
    Public Property INCREMENT_SCORE As Decimal?
    Public Property TR_RANK_ID As Decimal?
    Public Property TR_RANK_NAME As String
    Public Property RETEST_SCORE As Decimal?
    Public Property RETEST_RANK_ID As Decimal?
    Public Property RETEST_RANK_NAME As String
    Public Property RETEST_REMARK As String
    Public Property FINAL_SCORE As Decimal?
    Public Property ABSENT_REASON As Boolean?
    Public Property ABSENT_UNREASON As Boolean?
    Public Property IS_CERTIFICATE As Boolean?
    Public Property CERTIFICATE_DATE As Date?
    Public Property CERTIFICATE_NO As String
    Public Property CER_RECEIVE_DATE As Date?
    Public Property COMMIT_STARTDATE As Date?
    Public Property COMMIT_ENDDATE As Date?
    Public Property COMMIT_WORKMONTH As Decimal?
    Public Property IS_REFUND_FEE As Boolean?
    Public Property IS_RESERVES As Boolean?
    Public Property REMARK As String
    Public Property ATTACH_FILE As String

    Public Property IS_PLAN As Boolean?
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property CONTRACT_TYPE_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As Decimal?
    Public Property ID_DATE As Date?
    Public Property GENDER_NAME As String
    Public Property lstTitle As List(Of Decimal)
    Public Property lstContractType As List(Of Decimal)

    Public Property COMMENT_1 As String
    Public Property COMMENT_2 As String
    Public Property COMMENT_3 As String

    Public Property INSERT_HSNV As Decimal?
    Public Property INSERT_HSNV_STATUS As Boolean
    Public Property CERT_DATE As Date?
End Class
