Public Class CandidateCVDTO
    Public Property FULLNAME_VN As String
    Public Property MODIFY_TYPE As String '0: not modify, 1: modify image, 2: modify info

    Public Property CANDIDATE_ID As Decimal?
    Public Property BIRTH_DATE As Date?
    Public Property GENDER As String
    Public Property GENDER_NAME As String
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property ID_PLACE As String
    Public Property ID_PLACE_NAME As String
    Public Property NAV_ADDRESS As String
    Public Property NAV_PROVINCE As Decimal?
    Public Property BIRTH_ADDRESS As String
    Public Property BIRTH_PROVINCE As Decimal?
    Public Property NATIVE As String
    Public Property NATIVE_NAME As String
    Public Property RELIGION As String
    Public Property RELIGION_NAME As String
    Public Property PER_ADDRESS As String
    Public Property PER_PROVINCE As Decimal?
    Public Property CONTACT_ADDRESS As String
    Public Property CONTACT_PROVINCE As Decimal?
    Public Property MARITAL_STATUS As String
    Public Property MARITAL_STATUS_NAME As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIl As String
    Public Property PIT_CODE As String
    Public Property BHXH_NO As String
    Public Property HOSPITAL_ADDRESS As String

    Public Property WORK_FORTE As String
    Public Property IMAGE As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property NATIONALITY_ID As Decimal?
    Public Property NATIONALITY_NAME As String
    Public Property INSURANCE_DATE As Date?

    ' Bo sung 2/3/2015
    Public Property PASSPORT_ID As String
    Public Property PASSPORT_DATE As Date?
    Public Property PASSPORT_PLACE_ID As String
    Public Property PASSPORT_PLACE_NAME As String

    Public Property BIRTH_NATION_ID As Decimal?
    Public Property BIRTH_NATION_NAME As String

    Public Property PERTAXCODE As String
    Public Property EDUCATION_MAJORS As String

    Public Property EDUCATION_ID As Decimal?
    Public Property EDUCATION_ID_NAME As String

    Public Property CONTACT_NATION_ID As Decimal?
    Public Property CONTACT_NATION_NAME As String

    Public Property PER_NATION_ID As Decimal?
    Public Property PER_NATION As String

    Public Property NAV_NATION_ID As Decimal?
    Public Property NAV_NATION_NAME As String

    Public Property PER_DISTRICT_ID As Decimal?
    Public Property PER_DISTRICT As String

    Public Property CONTACT_DISTRICT_ID As Decimal?
    Public Property CONTACT_DISTRICT As String

    Public Property ID_DATE_EXPIRATION As Date?
    Public Property IS_RESIDENT As Decimal?
    Public Property CONTACT_ADDRESS_TEMP As String
    Public Property CONTACT_NATION_TEMP As Decimal?
    Public Property CONTACT_PROVINCE_TEMP As Decimal?
    Public Property CONTACT_DISTRICT_TEMP As Decimal?
    Public Property CONTACT_MOBILE As String
    Public Property CONTACT_PHONE As String
    Public Property PER_EMAIL As String
    Public Property PER_TAX_DATE As Date?
    Public Property PER_TAX_PLACE As String
    Public Property PASSPORT_DATE_EXPIRATION As Date?

    Public Property VISA_NUMBER As String
    Public Property VISA_DATE As Date?
    Public Property VISA_DATE_EXPIRATION As Date?
    Public Property VISA_PLACE As String

    Public Property VNAIRLINES_NUMBER As String
    Public Property VNAIRLINES_DATE As Date?
    Public Property VNAIRLINES_DATE_EXPIRATION As Date?
    Public Property VNAIRLINES_PLACE As String

    Public Property LABOUR_NUMBER As String
    Public Property LABOUR_DATE As Date?
    Public Property LABOUR_DATE_EXPIRATION As Date?
    Public Property LABOUR_PLACE As String

    Public Property WORK_PERMIT As String
    Public Property WORK_PERMIT_START As Date?
    Public Property WORK_PERMIT_END As Date?

    Public Property TEMP_RESIDENCE_CARD As String
    Public Property TEMP_RESIDENCE_CARD_START As Date?
    Public Property TEMP_RESIDENCE_CARD_END As Date?
    ' Thông tin liên hệ khẩn cấp
    Public Property CONTACT_PER As String
    Public Property CONTACT_REL_ADDRESS As String
    Public Property CONTACT_PER_PHONE As String
    'Liện lạc
    Public Property CON_ADDRESS As String
    Public Property CON_COUNTRY As Decimal?
    Public Property CON_COUNTRY_NAME As String
    Public Property CON_PROVINCE As Decimal?
    Public Property CON_PROVINCE_NAME As String
    Public Property CON_DISTRICT As Decimal?
    Public Property CON_DISTRICT_NAME As String
    Public Property CON_WARD As Decimal?
    Public Property CON_WARD_NAME As String
    'THEM TRUONG PHUONG XA THUONG TRU PER_WARD
    Public Property PER_WARD As Decimal?
    Public Property PER_WARD_NAME As String

    Public Property FINDER_NAME As String
    Public Property FINDER_SDT As String
    Public Property FINDER_ADDRESS As String
    Public Property URGENT_PER_NAME As String
    Public Property URGENT_PER_RELATION As Decimal
    Public Property URGENT_PER_SDT As String
    Public Property URGENT_ADDRESS As String
    Public Property STRANGER As String
    Public Property WEAKNESS As String
    Public Property IS_WORK_PERMIT As Decimal?

End Class
