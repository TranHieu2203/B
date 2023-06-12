Public Class AssetMngDTO
    Public Property ID As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property ORG_IDS As List(Of Decimal)
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property STAFF_RANK_NAME As String
    Public Property ASSET_ID As Decimal?
    Public Property ASSET_CODE As String
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_NAME As String
    Public Property ASSET_NAME As String
    Public Property ASSET_GROUP_CODE As String
    Public Property ASSET_GROUP_NAME As String
    Public Property ASSET_VALUE As Decimal?
    Public Property ORG_TRANFER As String
    Public Property ORG_RECEIVE As String
    Public Property ASSET_BARCODE As String
    Public Property ASSET_SERIAL As String
    Public Property DEPOSITS As Decimal?
    Public Property DESC As String
    Public Property ISSUE_DATE As Date?
    Public Property RETURN_DATE As Date?
    Public Property ASSET_DECLARE_ID As Decimal?
    Public Property UNIT_PRICE As Double?
    Public Property DEPRECIATE As Double?
    Public Property STATUS_ID As Decimal?
    Public Property STATUS_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property IS_TERMINATE As Boolean?
    Public Property WORK_STATUS As Decimal?
    Public Property TER_LAST_DATE As Date?
    Public Property FROM_DATE_SEARCH As Date?
    Public Property TO_DATE_SEARCH As Date?

    Public Property param As ParamDTO
    Public Property DATE_USE As Decimal?
    Public Property REMARK As String
    Public Property QUANTITY As Decimal?

End Class
