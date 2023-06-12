Public Class EmployeeAssessmentDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_NAME As String
    Public Property PE_PERIO_YEAR As Decimal?
    Public Property PE_PERIO_ID As Decimal?
    Public Property PE_PERIO_NAME As String
    Public Property PE_PERIO_TYPE_ASS As Decimal?
    Public Property PE_PERIO_TYPE_ASS_NAME As String
    Public Property PE_PERIO_START_DATE As Date?
    Public Property PE_PERIO_END_DATE As Date?
    Public Property RESULT As String
    Public Property PE_OBJECT_ID As Decimal?
    Public Property PE_CRITERIA_ID As Decimal? 'tieu chi
    Public Property PE_EMPLOYEE_ASSESSMENT_ID As Decimal?
    Public Property STATUS_EMP_ID As Decimal?
    Public Property STATUS_EMP_NAME As String
    Public Property DIRECT_ID As Decimal?
    Public Property UPDATE_DATE As Date?
    Public Property REMARK As String
    Public Property RESULT_DIRECT As String
    Public Property ASS_DATE As Date?
    Public Property REMARK_DIRECT As String
    Public Property RESULT_CONVERT As String 'diem danh gia cho tung tieu chi
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS_DIRECT As Decimal? 'danh gia.
    Public Property PE_STATUS_ID As Decimal? 'trang thai
    Public Property PE_STATUS_NAME As String
    Public Property STATUS_ACCEPT As String
    Public Property CLASSIFICATION_ID As Decimal?
    Public Property CLASSIFICATION_NAME As String 'Xep hang

End Class
