Public Class CandidateFamilyDTO
    Public Property ID As Decimal
    Public Property CANDIDATE_ID As Decimal
    Public Property CANDIDATE_CODE As String
    Public Property FULLNAME As String
    Public Property BIRTH_DAY As Int32?
    Public Property BIRTH_MONTH As Int32?
    Public Property BIRTH_YEAR As Int32?
    Public Property BIRTH_DATE As String
    Public Property ID_NO As String
    Public Property RELATION_ID As Decimal
    Public Property RELATION_NAME As String
    Public Property JOB As String
    Public Property COMPANY As String
    Public Property ADDRESS As String
    Public Property IS_MLG As Boolean?
    Public Property IS_DIED As Boolean?
    Public Property DIED_DATE As Date?
    Public Property REMARK As String

    Public Property RC_FAMILY_ID As Decimal
    Public Property IS_DEDUCT As Decimal
    Public Property DEDUCT_FROM As Date?
    Public Property DEDUCT_TO As Date?
    Public Property PIT_CODE As String
    Public Property PIT_DATE As DateTime?
    Public Property ID_DATE As DateTime?
    Public Property ID_PLACE As Decimal?
    Public Property PER_ADDRESS As String
    Public Property BIRTH_NO As Decimal?
    Public Property BIRTH_BOOK As Decimal?
    Public Property BIRTH_NAT As Decimal?
    Public Property BIRTH_PRO As Decimal?
    Public Property BIRTH_DIS As Decimal?
    Public Property BIRTH_NAT2 As Decimal?

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property PHONE_NUMBER As String
End Class
