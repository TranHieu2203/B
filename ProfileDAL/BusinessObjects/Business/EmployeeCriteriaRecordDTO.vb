Public Class EmployeeCriteriaRecordDTO
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property COMPETENCY_GROUP_ID As Decimal?
    Public Property COMPETENCY_GROUP_NAME As String
    Public Property COMPETENCY_ID As Decimal?
    Public Property COMPETENCY_NAME As String
    Public Property LST_COMPETENCY_ID As List(Of Decimal?)
    Public Property LEVEL_NUMBER As Int16?
    Public Property LEVEL_NUMBER_ASS As Int16?
    Public Property LEVEL_NUMBER_ASS_NAME As String ' diem danh gia hien tai cua NV
    Public Property LEVEL_NUMBER_STANDARD As Int16?
    Public Property LEVEL_NUMBER_STANDARD_NAME As String ' diem chuan theo chuc danh
    Public Property TR_COURSE_ID As Decimal?
    Public Property TR_COURSE_NAME As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    ''    
    Public Property COMPETENCY_PERIOD_ID As Decimal? 'DOT DANH GIA NANG LUC
    Public Property COMPETENCY_PERIOD_YEAR As Decimal?
    Public Property COMPETENCY_PERIOD_NAME As String



End Class
