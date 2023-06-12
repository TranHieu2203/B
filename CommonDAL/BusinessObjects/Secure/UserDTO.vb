Public Class UserDTO
    Public Property ID As Decimal
    Public Property USERNAME As String
    Public Property PASSWORD As String
    Public Property FULLNAME As String
    Public Property EMAIL As String
    Public Property TELEPHONE As String
    Public Property IS_APP As Boolean?
    Public Property IS_PORTAL As Boolean?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_FULLNAME_VN As String
    Public Property IS_AD As Boolean?
    Public Property EFFECT_DATE As DateTime?
    Public Property EXPIRE_DATE As DateTime?
    Public Property IS_USER_PERMISSION As Boolean
    Public Property ACTFLG As String
    Public Property IS_CHANGE_PASS As Integer
    Public Property CREATED_DATE As Date?
    Public Property MODULE_ADMIN As String
    Public Property WORK_STATUS As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property EFFECTDATE_TERMINATION As Date?
    Public Property GROUP_USER_ID As List(Of Decimal)
    Public Property OBJECT_EMPLOYEE_ID As Decimal?

    Public Property LST_ORG As List(Of Decimal)
    Public Property IS_LOGIN As Boolean?
    Public Property IS_RC As Boolean?
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String

    Public Property IS_HR As Boolean?

    Public Property SESSION_OUT As Decimal?
    Public Property SESSION_WARNING As Decimal?
    Public Property IS_SHOW_INACTIVE_USER As Boolean?
End Class

Public Class ProcessDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property EMP_APP_ID As Decimal?
    Public Property APP_TYPE As Decimal?
    Public Property APP_STATUS As Decimal?
    Public Property APP_STATUS_STR As String
    Public Property APP_LEVEL As Decimal?
    Public Property APP_DATE As String
    Public Property APP_NOTES As String
    Public Property NODE_VIEW As String
    Public Property NODE_NAME As String
End Class