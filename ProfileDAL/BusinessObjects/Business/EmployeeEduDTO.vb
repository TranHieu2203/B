Public Class EmployeeEduDTO
    Public Property EMPLOYEE_ID As Decimal
    Public Property ACADEMY As Decimal?
    Public Property ACADEMY_NAME As String
    Public Property MAJOR As Decimal?
    Public Property MAJOR_NAME As String
    Public Property MAJOR_REMARK As String
    Public Property TRAINING_FORM As Decimal?
    Public Property TRAINING_FORM_NAME As String
    Public Property LEARNING_LEVEL As Decimal?
    Public Property LEARNING_LEVEL_NAME As String
    Public Property GRADUATE_SCHOOL_ID As Decimal?
    Public Property GRADUATE_SCHOOL_NAME As String
    Public Property LANGUAGE As Decimal?
    Public Property LANGUAGE_NAME As String
    Public Property LANGUAGE_LEVEL As Decimal?
    Public Property LANGUAGE_LEVEL_NAME As String
    Public Property LANGUAGE_MARK As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property GRADUATION_YEAR As Int16?
    Public Property QLNN As Decimal?
    Public Property LLCT As Decimal?
    Public Property TDTH As Decimal?
    Public Property DIEM_XLTH As String
    Public Property NOTE_TDTH1 As String

    Public Property LANGUAGE2 As Decimal?
    Public Property LANGUAGE_LEVEL2 As Decimal?
    Public Property LANGUAGE_LEVEL_NAME2 As String
    Public Property LANGUAGE_MARK2 As String

    Public Property TDTH2 As Decimal?
    Public Property DIEM_XLTH2 As String
    Public Property NOTE_TDTH2 As String
    'TRINH DO TIN HỌC ỨNG DUNG
    Public Property COMPUTER_CERTIFICATE As String
    'TRINH DO TIN HOC CO BAN
    Public Property COMPUTER_RANK As Decimal?
    Public Property COMPUTER_RANK_NAME As String
    'LOAI CHUNG CHI
    Public Property COMPUTER_MARK As Decimal?
    Public Property COMPUTER_MARK_NAME As String


    Public Property LEARNING_LEVEL_NAME_2 As String
    Public Property MAJOR_NAME_2 As String
    Public Property GRADUATE_SCHOOL_NAME_2 As String
    Public Property GRADUATION_YEAR_2 As Int16?

    Public Property CERTIFICATE_GROUP_ID As Decimal?
    Public Property CERTIFICATE_GROUP_NAME As String
    Public Property CERTIFICATE_TYPE_ID As Decimal?
    Public Property CERTIFICATE_TYPE_NAME As String
End Class
