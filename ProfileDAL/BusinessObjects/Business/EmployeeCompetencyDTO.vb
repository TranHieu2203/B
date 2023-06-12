Public Class EmployeeCompetencyDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String

    Public Property COMPETENCY_PERIOD_ID As Decimal? 'DOT DANH GIA NANG LUC
    Public Property COMPETENCY_PERIOD_YEAR As Decimal?
    Public Property COMPETENCY_PERIOD_NAME As String

    Public Property COMPETENCY_GROUP_ID As Decimal? 'NHOM NANG LUC
    Public Property COMPETENCY_GROUP_NAME As String
    Public Property COMPETENCY_ID As Decimal? 'NANG LUC
    Public Property COMPETENCY_NAME As String
    Public Property LEVEL_NUMBER_STANDARD As Decimal? ' MUC NANG LUC CHUAN
    Public Property LEVEL_NUMBER_EMP As Decimal? ' MUC NANG LUC CA NHAN
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
End Class
