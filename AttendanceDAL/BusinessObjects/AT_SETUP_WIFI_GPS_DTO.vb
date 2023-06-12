Public Class AT_SETUP_WIFI_GPS_DTO
    Public Property ID As Decimal

    'FOR WIFI
    Public Property IP As String
    Public Property NAME_VN As String
    Public Property NAME_WIFI As String

    'FOR GPS
    Public Property NAME As String
    Public Property ADDRESS As String
    Public Property LAT_VD As String
    Public Property LONG_KD As String
    Public Property RADIUS As Decimal?


    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property IS_DISSOLVE As Boolean?
    Public Property USERNAME As String


End Class
