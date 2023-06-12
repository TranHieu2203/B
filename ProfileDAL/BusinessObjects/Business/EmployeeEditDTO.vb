Public Class EmployeeEditDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property STATUS As String
    Public Property STATUS_NAME As String
    Public Property REASON_UNAPROVE As String
    Public Property MARITAL_STATUS As Decimal?
    Public Property MARITAL_STATUS_NAME As String
    Public Property PER_ADDRESS As String
    Public Property PER_PROVINCE As Decimal?
    Public Property PER_PROVINCE_NAME As String
    Public Property PER_WARD As Decimal?
    Public Property PER_WARD_NAME As String
    Public Property PER_DISTRICT As Decimal?
    Public Property PER_DISTRICT_NAME As String
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property ID_PLACE As String
    Public Property ID_PLACE_NAME As String
    Public Property NAV_ADDRESS As String
    Public Property NAV_PROVINCE As Decimal?
    Public Property NAV_PROVINCE_NAME As String
    Public Property NAV_DISTRICT As Decimal?
    Public Property NAV_DISTRICT_NAME As String
    Public Property NAV_WARD As Decimal?
    Public Property NAV_WARD_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    'NGAY HET HAN CMND
    Public Property EXPIRE_DATE_IDNO As Date?

    ' Người liên hệ 
    Public Property CONTACT_PER As String
    Public Property RELATION_PER_CTR As Decimal?
    Public Property RELATION_PER_CTR_NAME As String
    Public Property CONTACT_PER_MBPHONE As String

    ' Ấp thôn xã
    Public Property VILLAGE As String

    Public Property HOME_PHONE As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIL As String
    Public Property PER_EMAIL As String

    Public Property PERSON_INHERITANCE As String

    Public Property BANK_NO As String
    Public Property BANK_ID As Decimal?
    Public Property BANK_NAME As String
    Public Property BANK_BRANCH_ID As Decimal?
    Public Property BANK_BRANCH_NAME As String

    Public Property CMND_NOTE_CHANGE As String
    Public Property ACADEMY As Decimal?
    Public Property ACADEMY_NAME As String
    Public Property LEARNING_LEVEL As Decimal?
    Public Property LEARNING_LEVEL_NAME As String
    Public Property MAJOR As Decimal?
    Public Property MAJOR_NAME As String
    Public Property GRADUATE_SCHOOL_ID As Decimal?
    Public Property GRADUATE_SCHOOL_NAME As String
    Public Property GRADUATION_YEAR As Decimal?
    Public Property COMPUTER_RANK As Decimal?
    Public Property COMPUTER_RANK_NAME As String
    Public Property COMPUTER_MARK As Decimal?
    Public Property COMPUTER_MARK_NAME As String
    Public Property COMPUTER_CERTIFICATE As String
    Public Property LANGUAGE As Decimal?
    Public Property LANGUAGE_NAME As String
    Public Property LANGUAGE_LEVEL As Decimal?
    Public Property LANGUAGE_LEVEL_NAME As String
    Public Property LANGUAGE_MARK As String
    Public Property NO_HOUSEHOLDS As String

    Public Property RELIGION As Decimal?
    Public Property RELIGION_NAME As String
    Public Property NATIVE As Decimal?
    Public Property NATIVE_NAME As String

    Public Property NOTE_CHANGE_CMND As String
    Public Property FILE_CMND As String
    Public Property FILE_CMND_BACK As String
    Public Property UPLOAD_FILE_CMND_BACK As String
    Public Property FILE_ADDRESS As String
    Public Property FILE_BANK As String
    Public Property UPLOAD_FILE_CMND As String
    Public Property UPLOAD_FILE_ADDRESS As String
    Public Property UPLOAD_FILE_BANK As String
    Public Property ADDRESS_PER_CTR As String
    Public Property IMAGE As String
    Public Property UPLOAD_IMAGE As String
    Public Property IS_SEND As Decimal?
    Public Property PASS_NO As String
    Public Property PASS_DATE As Date?
    Public Property PASS_EXPIRE As Date?
    Public Property PASS_PLACE As String
    Public Property PASS_PLACE_ID As Decimal?
    Public Property VISA As String
    Public Property VISA_DATE As Date?
    Public Property VISA_EXPIRE As Date?
    Public Property VISA_PLACE As String
    Public Property VISA_PLACE_ID As Decimal?
    Public Property BOOK_NO As String
    Public Property BOOK_DATE As Date?
    Public Property BOOK_EXPIRE As Date?
    Public Property SSLD_PLACE As String
    Public Property SSLD_PLACE_ID As Decimal?
    Public Property FILE_OTHER As String
    Public Property UPLOAD_FILE_OTHER As String
End Class
