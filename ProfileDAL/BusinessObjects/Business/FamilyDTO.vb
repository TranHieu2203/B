﻿Public Class FamilyDTO
    Public Property ID As Decimal
    Public Property FK_PKEY As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property STATUS_TAB As Decimal?
    Public Property STATUS As String
    Public Property EMPLOYEE_CODE As String
    Public Property FULLNAME As String
    Public Property RELATION_ID As Decimal?
    Public Property RELATION_NAME As String
    Public Property PROVINCE_ID As Decimal?
    Public Property PROVINCE_NAME As String
    Public Property TAXTATION As String
    Public Property BIRTH_DATE As Date?
    Public Property ID_NO As String
    Public Property IS_DEDUCT As Boolean?
    Public Property DEDUCT_REG As Date?
    Public Property DEDUCT_FROM As Date?
    Public Property DEDUCT_TO As Date?
    Public Property ADDRESS As String
    Public Property ADDRESS_TT As String
    Public Property AD_PROVINCE_ID As Decimal?
    Public Property AD_DISTRICT_ID As Decimal?
    Public Property AD_WARD_ID As Decimal?
    Public Property TT_PROVINCE_ID As Decimal?
    Public Property TT_DISTRICT_ID As Decimal?
    Public Property TT_WARD_ID As Decimal?
    Public Property CERTIFICATE_CODE As String
    Public Property CERTIFICATE_NUM As String
    Public Property IS_PASS As Boolean
    Public Property IS_OWNER As Boolean
    Public Property REMARK As String
    Public Property AD_VILLAGE As String

    Public Property AD_PROVINCE_NAME As String
    Public Property AD_DISTRICT_NAME As String
    Public Property AD_WARD_NAME As String
    Public Property TT_PROVINCE_NAME As String
    Public Property TT_DISTRICT_NAME As String
    Public Property TT_WARD_NAME As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String


    Public Property TITLE_NAME As String
    Public Property CAREER As String
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
    Public Property GENDER As Decimal?
    Public Property GENDER_NAME As String

    Public Property IS_SAME_COMPANY As Boolean?
    Public Property IS_TER As Boolean?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String

    Public Property EMPLOYEE_NAME As String
    Public Property WORK_STATUS As Decimal?
    Public Property TER_EFFECT_DATE As Date?

    Public Property RELATE_OWNER As Decimal?
    Public Property RELATE_OWNER_NAME As String

    Public Property NATIVE As Decimal?
    Public Property NATIVE_NAME As String

    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?

    Public Property BANK_ID As Decimal?
    Public Property BANK_NAME As String
    Public Property BANK_NO As String
    Public Property PERSON_INHERITANCE As String
    Public Property FILE_FAMILY As String
    Public Property UPLOAD_FILE_FAMILY As String
    Public Property FILE_NPT As String
    Public Property UPLOAD_FILE_NPT As String
End Class