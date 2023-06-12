Public Class CostDTO
    Public Property ID As Decimal
    Public Property RC_PROGRAM_ID As Decimal?
    Public Property RC_PROGRAM_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property COST_EXPECTED As Decimal?
    Public Property COST_ACTUAL As Decimal?
    Public Property RC_FORMs As List(Of CostFormDTO)
    Public Property RC_FORM_NAMES As String
    Public Property RC_FORM_IDS As String
    Public Property COST_DESCRIPTION As String
    Public Property REMARK As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    'TanVN: Add fields - 07/06/2016
    Public Property RC_STAGE_ID As Decimal?
    Public Property SOURCEOFREC_ID As Decimal?
    Public Property COSTESTIMATE As Decimal?
    Public Property COSTREALITY As Decimal?

End Class
