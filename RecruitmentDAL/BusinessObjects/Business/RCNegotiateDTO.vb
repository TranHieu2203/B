Public Class RCNegotiateDTO
    Public Property Candidate As CandidateDTO
    Public Property Program As ProgramDTO
    Public Property ID As Decimal?
    Public Property RC_PROGRAM_ID As Decimal?
    Public Property CODE_YCTD As String
    Public Property NAME_YCTD As String

    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String

    Public Property RC_CANDIDATE_ID As Decimal?
    Public Property RC_CANDIDATE_NAME As String
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property CONTRACT_TYPE_NAME As String
    Public Property CONTRACT_FROMDATE As Date?
    Public Property CONTRACT_TODATE As Date?

    Public Property SAL_TYPE_ID As Decimal?
    Public Property SAL_TYPE_NAME As String
    Public Property TAX_TABLE_ID As Decimal?
    Public Property TAX_TABLE_NAME As String
    Public Property EMPLOYEE_TYPE_ID As Decimal?
    Public Property EMPLOYEE_TYPE_NAME As String
    Public Property OTHERSALARY1 As Decimal?
    Public Property PERCENT_SAL As Decimal?

    Public Property SALARY_PROBATION As Decimal?
    Public Property SALARY_OFFICIAL As Decimal?
    Public Property NOTE As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
