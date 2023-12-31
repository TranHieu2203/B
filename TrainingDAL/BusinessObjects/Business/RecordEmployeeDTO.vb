﻿Public Class RecordEmployeeDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property FIRST_NAME_VN As String
    Public Property LAST_NAME_VN As String
    Public Property FULLNAME_VN As String
    Public Property FIRST_NAME_EN As String
    Public Property LAST_NAME_EN As String
    Public Property FULLNAME_EN As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property ORG_CODE As String
    Public Property WORK_STATUS As Decimal?
    Public Property WORK_STATUS_NAME As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property ITIME_ID As Decimal?
    Public Property PA_OBJECT_SALARY_ID As Decimal?
    Public Property PA_OBJECT_SALARY_NAME As String
    Public Property IMAGE As String                 'Ảnh đại diện ( Tên ảnh thôi ghi trong database)
    Public Property IMAGE_BINARY As Byte()          'Binary của Ảnh đại diện (Dùng service đọc ảnh ra binary)
    'Contract
    Public Property CONTRACT_ID As Decimal?             'Hop dong dang hieu luc
    Public Property CONTRACT_TYPE_ID As Decimal?        'Loại hợp đồng.
    Public Property CONTRACT_TYPE_NAME As String
    Public Property CONTRACT_NO As String
    Public Property CONTRACT_EFFECT_DATE As Date?    'Ngày hiệu lực của hợp đồng hiện tại
    Public Property CONTRACT_EXPIRE_DATE As Date?    'Ngày hết hiệu lực của hợp đồng hiện tại
    Public Property IS_NOT_CONTRACT As Boolean       'Liệt kê những nhân viên chưa có hợp đồng lao động (ThanhNT added 060602016)
    'Title
    Public Property TITLE_ID As Decimal?                'Chức danh
    Public Property TITLE_NAME_VN As String
    Public Property TITLE_NAME_EN As String

    Public Property TITLE_GROUP_ID As Decimal?
    Public Property TITLE_GROUP_NAME As String

    Public Property LAST_WORKING_ID As Decimal?
    'Start work
    Public Property JOIN_DATE As Date?
    Public Property JOIN_DATE_STATE As Date?
    Public Property DIRECT_MANAGER As Decimal?
    Public Property DIRECT_MANAGER_NAME As String
    Public Property LEVEL_MANAGER As Decimal?
    Public Property LEVEL_MANAGER_NAME As String

    Public Property EMPLOYEE_3B_ID As String
    Public Property EMPLOYEE_CODE_OLD As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String

    Public Property ID_NO As String
    Public Property MOBILE_PHONE As String
    Public Property WORK_EMAIL As String
    Public Property IS_HISTORY As Boolean
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property lstPaper As List(Of Decimal)


    Public Property IS_TER As Boolean
    Public Property IS_AND_TER As Boolean 'ThanhNT liet ke ca nhung nhan vien nghi viec (normal employee & terminate employee) (ThanhNT added 060602016)
    Public Property TER_EFFECT_DATE As Date?
    Public Property TER_LAST_DATE As Date?
    Public Property SAL_BASIC As Decimal?
    Public Property COST_SUPPORT As Decimal?
    Public Property EFFECT_DATE As Date?

    'course
    Public PROGRAME_ID As Decimal
    Public PROGRAME_NAME As String
    Public YEAR As Decimal?
    Public IS_LOCAL As Boolean?
    Public TR_COURSE_ID As Decimal?
    Public TR_COURSE_NAME As String
    Public TR_PROGRAM_ID As Decimal?
    Public TR_PROGRAM_NAME As String
    Public TR_PROGRAM_GROUP_ID As Decimal?
    Public TR_PROGRAM_GROUP_NAME As String
    Public TR_TRAIN_FORM_ID As Decimal?
    Public TR_TRAIN_FORM_NAME As String
    Public TR_TRAIN_ENTRIES_ID As Decimal?
    Public TR_TRAIN_ENTRIES_NAME As String
    Public DURATION As Decimal?
    Public TR_DURATION_UNIT_ID As Decimal?
    Public TR_DURATION_UNIT_NAME As String
    Public TR_CURRENCY_ID As Decimal?
    Public TR_CURRENCY_NAME As String
    Public CURRENCY As String
    Public TARGET_TRAIN As String
    Public VENUE As String
    Public CONTENT As String
    Public REMARK As String
    Public Units As List(Of ProgramOrgDTO)
    Public Titles As List(Of ProgramTitleDTO)
    Public Centers As List(Of ProgramCenterDTO)
    Public Lectures As List(Of ProgramLectureDTO)
    Public Departments_NAME As String
    Public Titles_NAME As String
    Public Centers_NAME As String
    Public Lectures_NAME As String
    Public CER_EXPIRED_DATE As Date?


    Public TR_UNIT_NAME As String 'trung tam dao tao
    Public FINAL_SCORE As Decimal? 'ngay het han chung chi



    Public TR_REQUEST_ID As Decimal?
    Public TR_PLAN_ID As Decimal?
    Public TR_PLAN_NAME As String
    Public DURATION_STUDY_ID As Decimal?
    Public DURATION_STUDY_NAME As String
    Public TR_LANGUAGE_ID As Decimal?
    Public TR_LANGUAGE_NAME As String
    Public IS_REIMBURSE As Boolean?
    Public DURATION_HC As Decimal?
    Public DURATION_OT As Decimal?
    Public START_DATE As Date?
    Public END_DATE As Date?

    Public Property COST_COM_SUPPORT As Decimal?
    Public Property COST_COMPANY As Decimal?
    Public Property STUDENT_NUMBER As Decimal?
    Public Property COST_TOTAL As Decimal?
    Public Property COST_OF_STUDENT As Decimal?
    Public Property COST_TOTAL_USD As Decimal?
    Public Property COST_OF_STUDENT_USD As Decimal?


    Public Property STATUS_ID As Decimal?
    Public Property FIELDS_ID As Decimal? 'Lĩnh vực
    Public Property TR_TRAIN_FIELD_NAME As String ' Lĩnh vực name
    Public Property NO_OF_STUDENT As Decimal?


    '' PROGRAM_RESULT
    Public Property TOEIC_FINAL_SCORE As Decimal? 'điểm toeic cuối cùng
    Public Property RANK_ID As Decimal? 'ID Xếp Loại
    Public Property RANK_NAME As String 'Tên xêp loại
    Public Property IS_REACH As String 'đạt hay ko đạt
    Public Property IS_REACHED As Boolean?
    Public Property IS_CERTIFICATE As Boolean? 'Có chính chỉ
    Public Property CERTIFICATE_NO As String 'Số chính chỉ
    Public Property IS_RESERVES As Boolean? ' Có trừ lương hay ko
    Public Property IS_END As Boolean?
    Public Property IS_EXAMS As String
    Public Property IS_RE_EXAMS As Boolean?
    Public Property CER_RECEIVE_DATE As Date?
    Public Property CERTIFICATE_DATE As Date?
    Public Property CERTIFICATE_DURATION As Decimal?
    Public Property TR_RANK_ID As Decimal?
    Public Property TR_RANK_NAME As String
    Public Property COMITMENT_TRAIN_NO As String ' so cam ket
    Public Property COMITMENT_START_DATE As Date? 'ngay bat dau cam ket
    Public Property COMITMENT_END_DATE As Date? 'ngay ket thuc cam ket
    Public Property COMITMENT_MONTH_WORK_NO As Date? ' Thời gian cam kết theo tháng
    Public Property PERIOD_RESERVES As String ' trừ vào kỳ lương nào

    Public Property REGISTER_TRAINING_STATUS As String

    Public Property EMP_TYPE As Decimal?
    Public Property EMP_TYPE_NAME As String

    Public Property COMMENT_1 As String
    Public Property COMMENT_2 As String
    Public Property COMMENT_3 As String
    Public Property CERTIFICATE_NAME As String
    Public Property COMMIT_NO As String
    Public Property MONEY_COMMIT As Decimal?
    Public Property COMMIT_WORK As Decimal?
    Public Property COMMIT_START As Date?
    Public Property COMMIT_END As Date?
    Public Property IS_TR_COMMIT As Boolean?


End Class
