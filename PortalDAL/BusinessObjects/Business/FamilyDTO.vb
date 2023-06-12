Public Class FamilyDTO
    Public Property ID As Guid
    Public Property EMPLOYEE_ID As Guid
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME As String
    Public Property RELATION_ID As Guid
    Public Property RELATION_NAME As String
    Public Property BIRTHDAY As Date
    Public Property ID_NO As String
    Public Property JOB As String
    Public Property COMPANY As String
    Public Property IS_DEDUCT As Int16?
    Public Property DEDUCT_FROM As Date?
    Public Property DEDUCT_TO As Date?
    Public Property IS_DIED As Int16?
    Public Property IS_WORK_COMPANY As Int16?
    Public Property IS_WORK_OPPOSITE As Int16?
    Public Property RELATED_DOC As String
    Public Property IS_INSURANCE As Int16?
    Public Property INSURANCE_FROM As Date?
    Public Property INSURANCE_TO As Date?
    Public Property ADDRESS As String

    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property NATION_ID As Decimal?
    Public Property NATION_NAME As String
    Public Property ID_NO_DATE As Date?
    Public Property ID_NO_PLACE_NAME As String
    Public Property PHONE As String
    Public Property TAXTATION_DATE As Date?
    Public Property TAXTATION_PLACE As String
    Public Property BIRTH_CODE As String
    Public Property QUYEN As String
    Public Property BIRTH_NATION_ID As Decimal?
    Public Property BIRTH_NATION_NAME As String
    Public Property BIRTH_PROVINCE_ID As Decimal?
    Public Property BIRTH_PROVINCE_NAME As String
    Public Property BIRTH_DISTRICT_ID As Decimal?
    Public Property BIRTH_DISTRICT_NAME As String
    Public Property BIRTH_WARD_ID As Decimal?
    Public Property BIRTH_WARD_NAME As String


End Class
