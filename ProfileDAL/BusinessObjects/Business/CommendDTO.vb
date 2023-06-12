Public Class CommendDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_IDS As List(Of Decimal)
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_CODE As String
    Public Property TITLE_ID As Decimal?
    Public Property FILENAME As String
    Public Property TITLE_NAME As String
    Public Property COMMEND_OBJ As Decimal?
    Public Property COMMEND_OBJ_NAME As String
    Public Property COMMEND_OBJ_CODE As String
    Public Property COMMEND_TYPE As Decimal?
    Public Property COMMEND_TYPE_NAME As String
    Public Property COMMEND_LEVEL As Decimal?
    Public Property COMMEND_LEVEL_NAME As String
    Public Property MONEY As Double?
    Public Property REMARK As String
    Public Property DECISION_ID As Decimal?
    Public Property DECISION_NO As String
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_ORG As Boolean
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
    Public Property STATUS_CODE As String
    Public Property YEAR_PERIOD As Decimal?
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String
    Public Property NO As String
    Public Property NAME As String
    Public Property TYPE As Decimal?
    Public Property TYPE_CODE As String
    Public Property TYPE_NAME As String
    Public Property SIGN_ID As Decimal?
    Public Property SIGNER_NAME As String
    Public Property SIGNER_TITLE As String
    Public Property SIGN_DATE As Date?
    Public Property ISSUE_DATE As Date?
    Public Property DEDUCT_FROM_SALARY As Boolean

    Public Property COMMEND_EMP As List(Of CommendEmpDTO)
    Public Property LIST_COMMEND_ORG As List(Of CommendOrgDTO)
    Public Property param As ParamDTO
    Public Property IS_TERMINATE As Boolean

    Public Property LEVEL_NAME As String
    Public Property COMMEND_DATE As Date?
    Public Property COMMEND_TITLE_ID As Decimal?
    Public Property COMMEND_TITLE_NAME As String 'Danh hieu khen thuong

    Public Property COMMEND_LIST_ID As Decimal?
    Public Property COMMEND_LIST_NAME As String
    Public Property COMMEND_DETAIL As String
    Public Property COMMEND_PAY As Decimal?
    Public Property COMMEND_PAY_NAME As String
    Public Property POWER_PAY_ID As Decimal?
    Public Property POWER_PAY_NAME As String
    Public Property UPLOADFILE As String
    Public Property NOTE As String
    Public Property IS_TAX As Decimal?
    Public Property IS_TAX_BOOLEN As Boolean
    Public Property PERIOD_TAX As Decimal?
    Public Property PERIOD_TAX_NAME As String

    Public Property OBJ_ORG_ID As Decimal? 'ID CUA PHONG BAN ( TAP THE )  -> LAY TU TABLE HU_COMMEND_ORG
    Public Property OBJ_ORG_NAME As String 'NAME CUA PHONG BAN ( TAP THE ) -> LAY TU TABLE HU_COMMEND_ORG
    Public Property HU_COMMEND_ORG_ID As Decimal? 'LAY TU ID TABLE HU_COMMEND_ORG

    Public Property FORM_ID As Decimal?
    Public Property FORM_NAME As String
    Public Property ORDER As Decimal? 'ThanhNT added 22/09/2016 - Sort theo thứ tự của danh mục khen thưởng
    Public Property YEAR As Decimal?

    Public Property FID As String
    Public Property FULLNAME_TEXT As String
    Public Property ORG_TEXT As String
    Public Property TITLE_TEXT As String
    Public Property NotEmp As Decimal?
End Class
