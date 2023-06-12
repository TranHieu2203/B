Public Class REQUEST_DTO
    Public Property REQUEST_ID As Decimal
    Public Property PROGRAM_ID As Decimal
    Public Property PHASE_CODE As String
    Public Property STATUS_CODE As String
    Public Property START_DATE As Date
    Public Property END_DATE As Date
    Public Property ACTUAL_START_DATE As Date
    Public Property ACTUAL_COMPLETE_DATE As Date
    Public Property NLS_LANGUAGE As String
    Public Property NLS_TERRITORY As String
    Public Property PRINTER As String
    Public Property SIZE As Decimal
    Public Property ORIENTATION As Decimal
    Public Property PERMISSION As Decimal
    Public Property LST_PARAMETERS As List(Of PARAMETER_DTO)
    Public Property CREATED_BY As String
    Public Property CREATED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_LOG As String
    Public Property CREATED_LOG As String
    Public Property STORE_EXECUTE_IN As String
    Public Property STORE_EXECUTE_OUT As String
End Class

Public Class PARAMETER_DTO
    Public Property REQUEST_ID As Decimal
    Public Property PARAMETER_NAME As String
    Public Property VALUE As String
    Public Property SEQUENCE As Decimal
    Public Property REPORT_FIELD As String
    Public Property IS_REQUIRE As Decimal
    Public Property CODE As String
End Class

