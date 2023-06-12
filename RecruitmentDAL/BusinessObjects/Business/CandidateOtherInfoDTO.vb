Public Class CandidateOtherInfoDTO
    Public Property CANDIDATE_ID As Decimal
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    ' Thông tin tài khoản ngân hàng
    Public Property ACCOUNT_NAME As String
    Public Property ACCOUNT_NUMBER As Decimal?
    Public Property BANK As String
    Public Property BANK_BRANCH As String
    Public Property IS_PAYMENT_VIA_BANK As Decimal?
    Public Property ACCOUNT_EFFECT_DATE As Date?
    Public Property BANK_BRANCH_Name As String
    'Thông tin tổ chức chính trị ...
    'Đoàn
    Public Property IS_DOANVIEN As Boolean?
    Public Property NGAY_VAO_DOAN As Date?
    Public Property NOI_VAO_DOAN As String
    Public Property CHUC_VU_DOAN As Decimal?
    Public Property CHUC_VU_DOAN_NAME As String
    Public Property DOAN_PHI As Boolean?
    'Đảng
    Public Property IS_DANGVIEN As Boolean?
    Public Property NGAY_VAO_DANG As Date?
    Public Property NOI_VAO_DANG As String
    Public Property CHUC_VU_DANG As Decimal?
    Public Property CHUC_VU_DANG_NAME As String
    Public Property DANG_PHI As Boolean?
    Public Property DANG_KIEMNHIEM As Decimal?
    Public Property DANG_KIEMNHIEM_NAME As String
    Public Property CAPUY_HIENTAI As String
    Public Property CAPUY_KIEMNHIEM As String

    'Công đoàn
    Public Property IS_CONGDOANPHI As Boolean?
    Public Property CDP_NGAYVAO As Date?
    Public Property CDP_NOIVAO As String

    'Sổ lao động
    Public Property LABOR_BOOK As String
    Public Property LABOR_PLACE As String
    Public Property LABOR_SAFETY_NUM As String
    Public Property LABOR_DATE As Date?
    Public Property LABOR_DATE_EXPIRE As Date?
    Public Property LABOR_SAFETY_DATE As Date?
    Public Property LABOR_SAFETY_DATE_EXPIRE As Date?

    'Cựu chiến binh
    Public Property IS_CCB As Boolean?
    Public Property CCB_NOIVAO As String
    Public Property CCB_QUANHAM As String
    Public Property CCB_NGAYVAO As Date?
    Public Property CCB_NGAYNHAPNGU As Date?
    Public Property CCB_NGAYXUATNGU As Date?
    Public Property CAREER As String
    Public Property TPGD As String
    Public Property DANHHIEU As String
    Public Property SOTRUONGCONGTAC As String
    Public Property CONGTAC_LAUNHAT As String
    Public Property CCB_CHUCVU As Decimal?
    Public Property CCB_CHUCVU_NAME As String
    Public Property LYLUANCHINHTRI As Decimal?
    Public Property LYLUANCHINHTRI_NAME As String
    Public Property QUANLYNHANUOC As Decimal?
    Public Property QUANLYNHANUOC_NAME As String
    Public Property THUONGBINH As Decimal?
    Public Property THUONGBINH_NAME As String
    Public Property GDCS As Decimal?
    Public Property GDCS_NAME As String
End Class
