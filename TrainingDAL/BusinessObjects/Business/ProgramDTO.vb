Public Class ProgramDTO
    Public ID As Decimal
    Public TR_PLAN_ID As Decimal?
    Public TR_PLAN_NAME As String
    Public TR_REQUEST_ID As Decimal?
    Public YEAR As Decimal?
    Public ORG_ID As Decimal?
    Public ORG_NAME As String
    Public TR_COURSE_ID As Decimal?
    Public TR_COURSE_CODE As String
    Public TR_COURSE_NAME As String
    Public NAME As String
    Public TRAIN_FORM_ID As Decimal?
    Public TRAIN_FORM_NAME As String
    Public PROPERTIES_NEED_ID As Decimal?
    Public PROPERTIES_NEED_NAME As String
    Property PROGRAM_GROUP As String
    Property TRAIN_FIELD As String
    Public DURATION As Decimal?
    Public TR_DURATION_UNIT_ID As Decimal?
    Public TR_DURATION_UNIT_NAME As String
    Public START_DATE As Date?
    Public END_DATE As Date?
    Public DURATION_STUDY_ID As Decimal?
    Public DURATION_STUDY_NAME As String
    Public DURATION_HC As Decimal?
    Public DURATION_OT As Decimal?
    Public IS_REIMBURSE As Boolean?
    Public IS_RETEST As Boolean?
    Public TR_CURRENCY_ID As Decimal?
    Public TR_CURRENCY_NAME As String
    Public Property PLAN_STUDENT_NUMBER As Decimal?
    Public Property PLAN_COST_TOTAL_US As Decimal?
    Public Property PLAN_COST_TOTAL As Decimal?
    Public Property STUDENT_NUMBER As Decimal?
    Public Property COST_TOTAL_US As Decimal?
    Public Property COST_STUDENT_US As Decimal?
    Public Property COST_TOTAL As Decimal?
    Public Property COST_STUDENT As Decimal?
    Public Costs As List(Of ProgramCostDTO)
    Public Departments_NAME As String
    Public Units As List(Of ProgramOrgDTO)
    Public Titles_NAME As String
    Public Titles As List(Of ProgramTitleDTO)
    Public Employees As List(Of RecordEmployeeDTO)
    Public GroupTitle As List(Of ProgramTitleDTO)
    Public WI_NAME As String
    Public WIs As List(Of ProgramTitleDTO)
    Public TR_LANGUAGE_ID As Decimal?
    Public TR_LANGUAGE_NAME As String
    Public TR_UNIT_ID As Decimal?
    Public TR_UNIT_NAME As String
    Public Centers_NAME As String
    Public Centers As List(Of ProgramCenterDTO)
    Public Lectures_NAME As String
    Public Lectures As List(Of ProgramLectureDTO)
    Public CONTENT As String
    Public TARGET_TRAIN As String
    Public VENUE As String
    Public REMARK As String

    Public CREATED_BY As String
    Public CREATED_DATE As DateTime?
    Public CREATED_LOG As String
    Public MODIFIED_BY As String
    Public MODIFIED_DATE As DateTime?
    Public MODIFIED_LOG As String

    Public TR_TRAIN_ENTRIES_ID As Decimal?
    Public TR_TRAIN_ENTRIES_NAME As String
    Public CURRENCY As String
    Public Property COMMIT_WORK As Decimal?

    Public TR_PROGRAM_CODE As String
    Public TR_TYPE_ID As Decimal?
    Public TR_TYPE_NAME As String

    Public Property CERTIFICATE As Decimal?
    Public Property CERTIFICATE_BOOL As Boolean?
    Public TR_TRAIN_FIELD_ID As Decimal?
    Public EXPECT_CLASS As Decimal?
    Public FILE_ATTACH As String
    Public UPLOAD_FILE As String
    Public TR_AFTER_TRAIN As Decimal?
    Public TR_AFTER_TRAIN_BOOL As Boolean?
    Public TR_COMMIT As Decimal?
    Public TR_COMMIT_BOOL As Boolean?
    Public DAY_REVIEW_1 As DateTime?
    Public DAY_REVIEW_2 As DateTime?
    Public DAY_REVIEW_3 As DateTime?
    Public CERTIFICATE_NAME As String

    Public IS_PUBLIC As Boolean
    Public PUBLIC_STATUS_ID As Decimal?
    Public PUBLIC_STATUS_NAME As String

    Public STATUS As Decimal?
    Public STATUS_NAME As String
    Public EMPLOYEE_ID As Decimal?
    Public Property STUDENT_NUMBER_JOIN As Decimal?
    Public PROGRAM_EMP_ID As Decimal?
    Public ATTACHFILE As String
    Public EXPECT_TR_FROM As DateTime?
    Public EXPECT_TR_TO As DateTime?
    Public TR_TRAIN_FORM_ID As Decimal?
    Public TR_TRAIN_FORM_NAME As String

    Public IS_PLAN As Decimal?
    Public PROGRAM_TYPE As String
    Public PORTAL_REGIST_FROM As DateTime?
    Public PORTAL_REGIST_TO As DateTime?
    Public ASS_EMP1_ID As Decimal?
    Public ASS_EMP2_ID As Decimal?
    Public ASS_EMP3_ID As Decimal?
    Public ASS_EMP1_NAME As String
    Public ASS_EMP2_NAME As String
    Public ASS_EMP3_NAME As String
    Public SUM_RATIO As Decimal?
    Public ASS_DATE As Date?

    Public Property IS_LOCK As Decimal?
    Public Property IS_LOCK_TEXT As String
    Public Property ASSESMENT_ID As Decimal?
    Public Property STATUS_ASSESMENT As String
End Class
