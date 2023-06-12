Public Class CVPoolEmpDTO
    Public Property CANDIDATE_ID As Decimal
    Public Property CANDIDATE_CODE As String
    Public Property FULL_NAME_VN As String
    Public Property GENDER As String
    Public Property GENDER_NAME As String
    Public Property MARITAL_STATUS As String 'tinh trang hon nhan
    Public Property MARITAL_STATUS_NAME As String
    Public Property PER_COUNTRY As Decimal? ' quoc gia
    Public Property PER_COUNTRY_NAME As String
    Public Property BIRTH_PLACE As Decimal? ' noi sinh
    Public Property BIRTH_PLACE_NAME As String
    Public Property BIRTH_DATE As Date? 'ngay sinh
    Public Property BIRTH_FROM_DATE As Date?
    Public Property BIRTH_TO_DATE As Date?
    Public Property NATIONALITY As Decimal? ' quoc tich
    Public Property NATIONALITY_NAME As String
    Public Property PER_PROVINCE As Decimal?    'tinh
    Public Property PER_PROVINCE_NAME As String
    Public Property ACADEMY As String  'trinh do van hoa
    Public Property ACADEMY_NAME As String
    Public Property LEARNING_LEVEL As String 'Trinh do hoc van
    Public Property LEARNING_LEVEL_NAME As String
    Public Property MAJOR As String 'Trinh do chuyen mon/hinh thuc dao tao/chuyen nganh
    Public Property MAJOR_NAME As String
    Public Property GRADUATE_SCHOOL As String 'Truong hoc
    Public Property GRADUATE_SCHOOL_NAME As String
    'Public Property MAJOR As Decimal? 'Chuyen nganh
    'Public Property MAJOR_NAME As String
    Public Property DEGREE As String 'Bang cap
    Public Property DEGREE_NAME As String
    Public Property MARK_EDU As String 'Xep loai
    Public Property MARK_EDU_NAME As String
    Public Property COMPUTER_RANK As String 'Trinh do tin hoc
    Public Property COMPUTER_RANK_NAME As String
    Public Property LANGUAGE As String 'Trinh do ngoai ngu
    Public Property LANGUAGE_NAME As String
    Public Property CHIEU_CAO As String
    Public Property CHIEU_CAO_TU As Decimal?
    Public Property CHIEU_CAO_DEN As Decimal?
    Public Property CAN_NANG As String
    Public Property CAN_NANG_TU As Decimal?
    Public Property CAN_NANG_DEN As Decimal?
    Public Property LOAI_SUC_KHOE As String
    Public Property LOAI_SUC_KHOE_NAME As String
    Public Property CANDIDATE_STATUS As String 'trang thai
    Public Property CANDIDATE_STATUS_NAME As String
    Public Property CREATED_DATE As Date?
    Public Property ORG_NAME As String
    Public Property MODIFIED_DATE As Date?
    Public Property TITLE_NAME_VN As String
    Public Property TITLE_GROUP_ID As Decimal?

    Public Property POS_1 As String
    Public Property POS_2 As String
    Public Property SAL_MM As Decimal?

    Public Property STATUS_ID As String

    Public Property RC_SOURCE_REC_ID As Decimal?
    Public Property RC_SOURCE_REC_NAME As String
    Public Property WORK_PERMIT As String
    Public Property IS_WORK_PERMIT As Decimal?
    Public Property WORK_PERMIT_NAME As String
    Public Property WORK_PERMIT_START As Date?
    Public Property WORK_PERMIT_END As Date?
End Class
