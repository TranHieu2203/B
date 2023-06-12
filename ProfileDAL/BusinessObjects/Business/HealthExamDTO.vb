Public Class HealthExamDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String

    Public Property param As ParamDTO
    Public Property TITLE_NAME As String
    Public Property ORG_NAME As String
    Public Property IS_TERMINATE As Boolean

    Public Property YEAR As Decimal?
    Public Property EFFECT_DATE As Date?
    Public Property LOAISK_ID As Decimal?
    Public Property LOAISK_NAME As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property NHOM_MAU As String
    Public Property NHIP_TIM As String
    Public Property HUYET_AP As String
    Public Property MAT_TRAI As String
    Public Property MAT_PHAI As String
    Public Property CHIEU_CAO As String
    Public Property CAN_NANG As String
    Public Property NOTE As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
