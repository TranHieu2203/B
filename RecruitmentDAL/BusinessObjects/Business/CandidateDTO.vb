Public Class CandidateDTO
    Public Property ID As Decimal?
    Public Property CANDIDATE_CODE As String
    Public Property EMPLOYEE_CODE As String
    Public Property FIRST_NAME_VN As String
    Public Property LAST_NAME_VN As String
    Public Property FULLNAME_VN As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_ABBR As String
    Public Property IMAGE As String                 'Ảnh đại diện ( Tên ảnh thôi ghi trong database)
    Public Property IMAGE_BINARY As Byte()          'Binary của Ảnh đại diện (Dùng service đọc ảnh ra binary)

    Public Property TITLE_ID As Decimal?         'Cấp Chức danh
    Public Property TITLE_NAME_VN As String ' Chức danh
    Public Property TITLE_NAME_EN As String

    Public Property JOIN_DATE As Date?
    Public Property EFFECT_DATE As Date?
    Public Property CTRACT_NO As String
    Public Property CTRACT_START As Date?
    Public Property CTRACT_END As Date?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    ' Bo sung 2/3/2015
    Public Property TITLE_NAME As String
    Public Property FILE_NAME As String
    Public Property FILE_SIZE As Byte()
    Public Property WORK As String

    'Cost center of Org
    Public Property ORG_COSTCENTER_ID As Decimal?
    Public Property ORG_COSTCENTER_NAME As String
    Public Property RC_PROGRAM_ID As Decimal?
    Public Property STATUS_ID As String
    Public Property STATUS_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property GENDER_NAME As String
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property IS_PONTENTIAL As Boolean?
    Public Property IS_BLACKLIST As Boolean?
    Public Property IS_REHIRE As Boolean?

    Public Property WORK_STATUS As Decimal?

    ' Trạng thái ứng viên
    Public Property DUDIEUKIEN_ID As String
    Public Property KHONGDUDIEUKIEN_ID As String
    Public Property THIDAT_ID As String = "DAT"
    Public Property TRUNGTUYEN_ID As String
    Public Property GUITHU_ID As String
    Public Property LANHANVIEN_ID As String
    Public Property KHONGTHIDAT_ID As String
    Public Property TUCHOI_ID As String
    Public Property NOIBO_ID As String
    Public Property PONTENTIAL As String

    'Chức danh quan tâm
    Public Property CARE_TITLE_NAME As String

    'Trang tuyển dụng
    Public Property RECRUIMENT_WEBSITE As String

    Public Property RC_SOURCE_REC_ID As Decimal?
    Public Property RC_SOURCE_REC_NAME As String

    'anhvn
    Public Property RC_PROGRAM_CANDIDATE_ID As Decimal?
    Public Property CONTRACT_MOBILE As String
    Public Property PER_EMAIL As String
End Class
