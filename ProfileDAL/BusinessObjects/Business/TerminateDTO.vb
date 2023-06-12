Public Class TerminateDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_SENIORITY As String
    Public Property JOIN_DATE As Date?
    Public Property ID_NO As String
    Public Property IS_IMPACT As Boolean
    Public Property IS_NOHIRE As Boolean
    'Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property UPLOADFILE As String
    Public Property FILENAME As String
    Public Property ORG_DESC As String
    Public Property ORG_ABBR As String
    'Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property SEND_DATE As Date?
    Public Property TO_SEND_DATE As Date?
    Public Property FROM_SEND_DATE As Date?
    Public Property WRITE_DATE As Date?
    Public Property lstReason As List(Of TerminateReasonDTO)
    Public Property lstHandoverContent As List(Of HandoverContentDTO)
    Public Property TER_REASON_DETAIL As String
    Public Property LAST_DATE As Date?
    Public Property TO_LAST_DATE As Date?
    Public Property FROM_LAST_DATE As Date?
    Public Property STATUS_ID As Decimal
    Public Property STATUS_NAME As String
    Public Property STATUS_CODE As String
    Public Property REMARK As String
    Public Property DECISION_ID As Decimal
    Public Property DECISION_NO As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property APPROVAL_DATE As Date? 'ngay duoc phe duyet nghi
    Public Property IDENTIFI_CARD As Decimal? 'check chon the nv, the gui xe
    Public Property IDENTIFI_RDATE As Date? 'ngay tra the nv, the gui xe
    Public Property IDENTIFI_STATUS As String 'tinh trang the nv, the gui xe
    Public Property SUN_CARD As Decimal? 'check chon the sungroup
    Public Property SUN_RDATE As Date? 'ngay tra the sungroup
    Public Property SUN_STATUS As String 'tinh trang the sungroup
    Public Property INSURANCE_CARD As Decimal? 'check chon the bao hiem y te
    Public Property INSURANCE_RDATE As Date? 'ngay tra the bao hiem y te
    Public Property INSURANCE_STATUS As String 'tinh trang the bao hiem y te
    Public Property INSURANCE_STATUS_NAME As String 'tinh trang the bao hiem y te
    Public Property REMAINING_LEAVE As Decimal? 'so phep con lai
    Public Property PAYMENT_LEAVE As Decimal? 'tien thanh toan phep
    Public Property AMOUNT_VIOLATIONS As Decimal? 'so tien vi pham thoi han bao truoc
    Public Property AMOUNT_WRONGFUL As Decimal? 'so tien vi pham do cham dut sai luat
    Public Property ALLOWANCE_TERMINATE As Decimal? 'tro cap thoi viec
    Public Property TRAINING_COSTS As Decimal? 'chi phi dao tao
    Public Property OTHER_COMPENSATION As Decimal? 'boi thuong khac
    Public Property COMPENSATORY_LEAVE As Decimal? 'so ngay nghi bu
    Public Property COMPENSATORY_PAYMENT As Decimal? 'so ngay nghi bu thanh toan

    Public Property EFFECT_DATE As Date?
    Public Property SIGN_DATE As Date?
    Public Property SIGN_ID As Decimal?
    Public Property SIGN_NAME As String
    Public Property SIGN_TITLE As String

    Public Property IDENTIFI_MONEY As Decimal?
    Public Property SUN_MONEY As Decimal?
    Public Property INSURANCE_MONEY As Decimal?

    Public Property WORK_STATUS As Decimal?
    Public Property IS_NOHIRE_SHORT As Short?
    Public Property TYPE_TERMINATE As Decimal?

    Public Property SALARYMEDIUM As Decimal?
    Public Property YEARFORALLOW As Decimal?
    Public Property MONEYALLOW As Decimal?
    Public Property REMAINING_LEAVE_MONEY As Decimal?
    Public Property IDENTIFI_VIOLATE_MONEY As Decimal?
    Public Property MONEY_RETURN As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property SUM_DEBT As Decimal?
    Public Property SALARYMEDIUM_LOSS As String
    Public Property EXPIRE_DATE As Date?
    Public Property AssetMngs As List(Of AssetMngDTO)
    Public Property CODE As String

    Public Property TER_REASON As Decimal?
    Public Property TER_REASON_NAME As String
    Public Property DECISION_TYPE As Decimal?
    Public Property DECISION_TYPE_NAME As String
    Public Property SUM_COLLECT_DEBT As Decimal?
    Public Property AMOUNT_PAYMENT_CASH As Decimal?
    Public Property AMOUNT_DEDUCT_FROM_SAL As Decimal?
    Public Property PERIOD_ID As Decimal?
    Public Property IS_ALLOW As Boolean
    Public Property ALLOW_NAME As String
    Public Property IS_REPLACE_POS As Boolean
    Public Property REVERSE_SENIORITY As Decimal?
    Public Property lstDebts As List(Of DebtDTO)

    'HSV ADD PROERTIES
    Public Property NOTIFY_NO As String
    Public Property TER_DATE As Date?
    Public Property IS_BLACK_LIST As Decimal?
    Public Property REASON_BLACK_LIST As String
    Public Property TRUYTHU_BHYT As Decimal?
    Public Property O_HI_EMP As Decimal?
    Public Property O_HI_COM As Decimal?
    Public Property IS_JOB_LOSS_ALLOWANCE As Decimal?
    Public Property JOB_LOSS_ALLOWANCE_NAME As String
    Public Property YEAR_JOB_LOSS_ALLOWANCE As Decimal?
    Public Property MONTH_JOB_LOSS_ALLOWANCE As String
    Public Property MONEY_JOB_LOSS_ALLOWANCE As Decimal?
    Public Property PAYPACK_UNIFORM As Decimal?

    Public Property IS_RETURN_INSAL As Decimal?
    Public Property PERIOD_TEXT As String
    Public Property ORG_NAME2 As String
    Public Property ORG_NAME3 As String
    Public Property ORG_NAME4 As String
    Public Property ORG_NAME5 As String
    Public Property ORG_NAME6 As String
    Public Property ORG_NAME7 As String

    Public Property IS_COPY As Decimal?
    Public Property COPY_STATUS_NAME As String
    Public Property UNIFORM As Decimal?
    Public Property REMARK_QT As String
    Public Property MOBILE_PHONE As String
    Public Property GENDER_NAME As String
    Public Property BIRTH_DATE As Date?
    Public Property PER_ADDRESS As String
    Public Property NAV_ADDRESS As String
    Public Property IS_DAMAT_NAME As String
End Class
