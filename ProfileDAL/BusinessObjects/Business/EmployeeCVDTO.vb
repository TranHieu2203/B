Public Class EmployeeCVDTO
    Public Property EMPLOYEE_ID As Decimal
    Public Property FULL_NAME_VN As String
    Public Property GENDER As Decimal?
    Public Property GENDER_NAME As String
    Public Property IMAGE As String
    Public Property BIRTH_DATE As Date?
    Public Property BIRTH_PLACE As Decimal?
    Public Property MARITAL_STATUS As Decimal?
    Public Property MARITAL_STATUS_NAME As String
    Public Property RELIGION As Decimal?
    Public Property RELIGION_NAME As String
    Public Property NATIVE As Decimal?
    Public Property NATIVE_NAME As String
    Public Property NATIONALITY As Decimal?
    Public Property NATIONALITY_NAME As String
    Public Property PER_ADDRESS As String
    Public Property PER_PROVINCE As Decimal?
    Public Property PER_PROVINCE_NAME As String
    Public Property WORKPLACE_ID As Decimal?
    Public Property WORKPLACE_NAME As String
    Public Property INS_REGION_ID As Decimal?
    Public Property INS_REGION_NAME As String
    Public Property PER_WARD As Decimal?
    Public Property PER_WARD_NAME As String
    Public Property PER_DISTRICT As Decimal?
    Public Property PER_DISTRICT_NAME As String
    Public Property HOME_PHONE As String
    Public Property MOBILE_PHONE As String
    Public Property ID_NO As String
    Public Property ID_DATE As Date?
    Public Property ID_PLACE As Decimal?
    Public Property PLACE_NAME As String
    Public Property ID_REMARK As String
    Public Property PASS_NO As String
    Public Property PASS_DATE As Date?
    Public Property PASS_EXPIRE As Date?
    Public Property PASS_PLACE As String
    Public Property VISA As String
    Public Property VISA_DATE As Date?
    Public Property VISA_EXPIRE As Date?
    Public Property VISA_PLACE As String
    Public Property WORK_PERMIT As String
    Public Property WORK_PERMIT_DATE As Date?
    Public Property WORK_PERMIT_EXPIRE As Date?
    Public Property WORK_PERMIT_PLACE As String
    Public Property WORK_EMAIL As String
    Public Property NAV_ADDRESS As String
    Public Property NAV_PROVINCE As Decimal?
    Public Property NAV_PROVINCE_NAME As String
    Public Property NAV_DISTRICT As Decimal?
    Public Property NAV_DISTRICT_NAME As String
    Public Property NAV_WARD As Decimal?
    Public Property NAV_WARD_NAME As String
    Public Property PIT_CODE As String
    Public Property PER_EMAIL As String
    Public Property CONTACT_PER As String
    Public Property CONTACT_PER_PHONE As String
    Public Property CAREER As String


    Public Property NGAY_VAO_DOAN As Date?
    Public Property NOI_VAO_DOAN As String
    Public Property CHUC_VU_DOAN As String
    Public Property DOAN_PHI As Boolean?
    Public Property NGAY_VAO_DANG As Date?
    Public Property NOI_VAO_DANG As String
    Public Property CHUC_VU_DANG As String
    Public Property DANG_PHI As Boolean?
    Public Property BANK_ID As Decimal?
    Public Property BANK_NAME As String
    Public Property BANK_BRANCH_ID As Decimal?
    Public Property BANK_BRANCH_NAME As String
    Public Property BANK_NO As String
    Public Property IS_PERMISSION As Boolean?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_PAY_BANK As Boolean?

    Public Property MODIFY_TYPE As String '0: not modify, 1: modify image, 2: modify info
    Public Property PROVINCEEMP_ID As Decimal?
    Public Property PROVINCEEMP_NAME As String
    Public Property DISTRICTEMP_ID As Decimal?
    Public Property DISTRICTEMP_NAME As String
    Public Property WARDEMP_ID As Decimal?
    Public Property WARDEMP_NAME As String
    Public Property PROVINCENQ_ID As Decimal?
    Public Property PROVINCENQ_NAME As String
    Public Property OPPTION1 As String
    Public Property OPPTION2 As String
    Public Property OPPTION3 As String
    Public Property OPPTION4 As String
    Public Property OPPTION5 As String
    Public Property OPPTION6 As Date?
    Public Property OPPTION7 As Date?
    Public Property OPPTION8 As Date?
    Public Property OPPTION9 As Date?
    Public Property OPPTION10 As Date?
    Public Property GD_CHINH_SACH As Decimal?
    Public Property GD_CHINH_SACH_NAME As String
    Public Property HANG_THUONG_BINH As Decimal?
    Public Property HANG_THUONG_BINH_NAME As String
    Public Property THUONG_BINH As Boolean?
    Public Property DV_XUAT_NGU_QD As String
    Public Property NGAY_XUAT_NGU_QD As Date?
    Public Property NGAY_NHAP_NGU_QD As Date?
    Public Property QD As Boolean?
    Public Property DV_XUAT_NGU_CA As String
    Public Property NGAY_XUAT_NGU_CA As Date?
    Public Property NGAY_NHAP_NGU_CA As Date?
    Public Property NGAY_TG_BAN_NU_CONG As Date?
    Public Property CV_BAN_NU_CONG As String
    Public Property NU_CONG As Boolean?
    Public Property NGAY_TG_BANTT As Date?
    Public Property CV_BANTT As String
    Public Property CONG_DOAN As Boolean?
    Public Property CA As Boolean?
    Public Property DANG As Boolean?
    Public Property SKILL As String
    Public Property NGAY_VAO_DANG_DB As Date?
    Public Property BANTT As Boolean?

    Public Property PROVINCEEMP_BRITH As Decimal?
    Public Property PROVINCEEMP_BRITH_NAME As String
    Public Property DISTRICTEMP_BRITH As Decimal?
    Public Property DISTRICTEMP_BRITH_NAME As String
    Public Property WARDEMP_BRITH As Decimal?
    Public Property WARDEMP_BRITH_NAME As String
    Public Property OBJECT_INS As Decimal?
    Public Property OBJECT_INS_NAME As String
    Public Property IS_CHUHO As Boolean?
    Public Property NO_HOUSEHOLDS As String
    Public Property CODE_HOUSEHOLDS As String
    Public Property RELATION_PER_CTR As Decimal?
    Public Property RELATION_PER_CTR_NAME As String
    Public Property ADDRESS_PER_CTR As String
    'ap thon khu
    Public Property VILLAGE As String
    'SO DT DI DONG NG LIEN HE
    Public Property CONTACT_PER_MBPHONE As String
    'ngay cap va noi cap ma so thue
    Public Property PIT_CODE_DATE As Date?
    Public Property PIT_CODE_PLACE As String
    'TEN NGUOI THU HUONG VA NGAY HIEU LUC NGAN HAN
    Public Property EFFECTDATE_BANK As Date?
    Public Property PERSON_INHERITANCE As String
    'NGAY HET HAN CMND
    Public Property EXPIRE_DATE_IDNO As Date?
    'noi sinh
    Public Property BIRTH_PLACEID As Decimal?
    Public Property BIRTH_PLACENAME As String

    Public Property ATTACH_FILE As String
    Public Property FILENAME As String

    Public Property OTHER_GENDER As String
    Public Property BIRTH_PLACE_DETAIL As String
    Public Property COPY_ADDRESS As Boolean?
    Public Property CHECK_NAV As Boolean?
    Public Property BOOK_NO As String
    Public Property BOOK_DATE As Date?
    Public Property BOOK_EXPIRE As Date?

    Public Property PASS_PLACE_ID As Decimal?
    Public Property VISA_PLACE_ID As Decimal?
    Public Property SSLD_PLACE_ID As Decimal?
    Public Property SSLD_PLACE_NAME As String

    Public Property HEALTH_AREA_INS_ID As Decimal?
    Public Property HEALTH_AREA_INS_NAME As String

    Public Property CONTACT_PER_IDNO As String
    Public Property CONTACT_PER_EFFECT_DATE_IDNO As Date?
    Public Property CONTACT_PER_EXPIRE_DATE_IDNO As Date?
    Public Property CONTACT_PER_PLACE_IDNO As Decimal?
    Public Property CONTACT_PER_PLACE_NAME As String

    Public Property PIT_ID_PLACE As Decimal?
    Public Property PIT_ID_PLACE_NAME As String

    Public Property RELATE_OWNER As Decimal?
    Public Property RELATE_OWNER_NAME As String

    Public Property HEALTH_NO As String
    Public Property NGAY_VAO_DTN As Date?
    Public Property NOI_VAO_DTN As String
    Public Property CHUC_VU_DTN As String
    Public Property TD_CHINHTRI As String
    Public Property CBO_SINHHOAT As String
    Public Property SO_LYLICH As String
    Public Property SOTHE_DANG As String
End Class
