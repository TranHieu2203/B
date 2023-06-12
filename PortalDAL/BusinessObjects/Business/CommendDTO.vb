Public Class CommendDTO
    Public Property ID As Guid
    Public Property EMPLOYEE_ID As Guid?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_ORG As Guid?
    Public Property EMPLOYEE_ORG_NAME As String
    Public Property ORG_ID As Guid?
    Public Property ORG_IDS As List(Of Guid)
    Public Property ORG_NAME As String
    Public Property ORG_CODE As String
    Public Property TITLE_ID As Guid
    Public Property TITLE_NAME As String
    Public Property COMMEND_OBJ As Guid
    Public Property COMMEND_OBJ_NAME As String
    Public Property COMMEND_OBJ_CODE As String
    Public Property COMMEND_TYPE As Guid
    Public Property COMMEND_TYPE_NAME As String
    Public Property COMMEND_LEVEL As Guid
    Public Property COMMEND_LEVEL_NAME As String
    Public Property MONEY As Double?
    Public Property REMARK As String
    Public Property DECISION_ID As Guid
    Public Property DECISION_NO As String
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property DECISION As DecisionDTO
End Class
