Public Class JobDTO
    Public Property ID As Decimal
    Public Property CODE As String
    Public Property NAME_EN As String
    Public Property NAME_VN As String
    Public Property PHAN_LOAI_ID As Decimal?
    Public Property PHAN_LOAI_NAME As String
    Public Property REQUEST As String
    Public Property PURPOSE As String
    Public Property NOTE As String
    Public Property ACTFLG As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property NAMECBO As String

    Public Property JOB_BAND_ID As Decimal?
    Public Property JOB_BAND_NAME As String
    Public Property JOB_FAMILY_ID As Decimal?
    Public Property JOB_FAMILY_NAME As String
    Public Property FUNCTION_ID As Decimal?
    Public Property COLOR As String

    Public Property lstPosition As List(Of JobPositionDTO)
End Class
