Public Class PlanDTO
    Public ID As Decimal
    Public NAME As String
    Public YEAR As Decimal?
    Public ORG_ID As Decimal?
    Public ORG_NAME As String
    Public TR_COURSE_ID As Decimal?
    Public TR_COURSE_NAME As String
    Public TR_TRAIN_FORM_ID As Decimal?
    Public TR_TRAIN_FORM_NAME As String
    Public TR_TRAIN_ENTRIES_ID As Decimal?
    Public TR_TRAIN_ENTRIES_NAME As String
    Public PLAN_T1 As Boolean?
    Public PLAN_T2 As Boolean?
    Public PLAN_T3 As Boolean?
    Public PLAN_T4 As Boolean?
    Public PLAN_T5 As Boolean?
    Public PLAN_T6 As Boolean?
    Public PLAN_T7 As Boolean?
    Public PLAN_T8 As Boolean?
    Public PLAN_T9 As Boolean?
    Public PLAN_T10 As Boolean?
    Public PLAN_T11 As Boolean?
    Public PLAN_T12 As Boolean?
    Public DURATION As Decimal?
    Public TR_DURATION_UNIT_ID As Decimal?
    Public TR_DURATION_UNIT_NAME As String
    Public TEACHER_NUMBER As Decimal?
    Public STUDENT_NUMBER As Decimal?
    Public COST_TRAIN As Decimal?
    Public COST_INCURRED As Decimal?
    Public COST_TRAVEL As Decimal?
    Public COST_TOTAL As Decimal?
    Public COST_TOTAL_USD As Decimal?
    Public COST_OTHER As Decimal?    ' Chi phi cua mot hoc vien
    Public COST_OF_STUDENT As Decimal?
    Public COST_OF_STUDENT_USD As Decimal?
    Public TR_CURRENCY_ID As Decimal?
    Public TR_CURRENCY_NAME As String
    Public CURRENCY As String
    Public TARGET_TRAIN As String    ' Dia diem to chuc
    Public VENUE As String    'Ghi Chu
    Public REMARK As String
    Public Units As List(Of PlanOrgDTO)
    Public Titles As List(Of PlanTitleDTO)
    Public GroupTitle As List(Of PlanTitleDTO)
    Public Work_inv As List(Of PlanTitleDTO)
    Public Centers As List(Of PlanCenterDTO)
    Public CostDetail As List(Of CostDetailDTO)
    Public Plan_Emp As List(Of RequestEmpDTO)
    Public ProgramCenter As List(Of ProgramCenterDTO)
    Public ProgramLecture As List(Of ProgramLectureDTO)
    Public Departments_NAME As String
    Public Titles_NAME As String
    Public Centers_NAME As String
    Public Months_NAME As String
    Public Work_inv_NAME As String
    Public CREATED_BY As String
    Public CREATED_DATE As DateTime?
    Public CREATED_LOG As String
    Public MODIFIED_BY As String
    Public MODIFIED_DATE As DateTime?
    Public MODIFIED_LOG As String
    Public PROPERTIES_NEED_ID As Decimal?
    Public PROPERTIES_NEED_NAME As String
    Public UNIT_ID As Decimal?
    Public ATTACHFILE As String
    Public TR_TRAIN_FIELD_ID As Decimal?
    Public TR_TRAIN_FIELD_NAME As String
    Public TR_PROGRAM_GROUP_NAME As String
    Public UNIT_NAME As String

    Public TR_PLAN_CODE As String
    Public PLAN_TYPE As Boolean?
    Public PLAN_TYPE_TEXT As String
    Public EXPECT_TR_FROM As DateTime?
    Public EXPECT_TR_TO As DateTime?
    Public CONTENT As String
    Public EXPECT_CLASS As Decimal?
    Public TR_AFTER_TRAIN As Boolean?
    Public TR_AFTER_TRAIN_TEXT As String
    Public CERTIFICATE As Boolean?
    Public CERTIFICATE_TEXT As String
    Public CERTIFICATE_NAME As String
    Public TR_COMMIT As Boolean?
    Public TR_COMMIT_TEXT As String
    Public TR_TYPE_ID As Decimal?
    Public TR_TYPE_NAME As String
    Public DAY_REVIEW_1 As DateTime?
    Public DAY_REVIEW_2 As DateTime?
    Public DAY_REVIEW_3 As DateTime?
    Public TR_PROGRAM_CODE As String
    Public TR_REQUEST_ID As Decimal?
    Public ASS_EMP1_ID As Decimal?
    Public ASS_EMP2_ID As Decimal?
    Public ASS_EMP3_ID As Decimal?
    Public ASS_EMP1_NAME As String
    Public ASS_EMP2_NAME As String
    Public ASS_EMP3_NAME As String

End Class
