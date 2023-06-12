Public Class RequestDTO
    Property ID As Decimal
    Property TR_PLAN_ID As Decimal?
    Property TR_PLAN_NAME As String
    Property ORG_ID As Decimal?
    Property ORG_NAME As String
    Property ORG_DESC As String
    Property REQUEST_DATE As Date?
    Property YEAR As Decimal?
    Property TR_CURRENCY_ID As Decimal?
    Property TR_CURRENCY_NAME As String
    Property STATUS_ID As Decimal?
    Property STATUS_NAME As String
    Property EXPECTED_COST As Decimal?
    Property EXPECTED_DATE As Date?
    Property START_DATE As Date?
    Property CONTENT As String
    Property CENTERS As String
    Property CENTERS_ID As String
    Property TEACHERS As String
    Property TEACHERS_ID As String
    Property VENUE As String
    Property ADDRESS As String
    Property REMARK As String
    Property CREATED_DATE As Date?
    Property REJECT_REASON As String
    Property lstEmp As List(Of RequestEmpDTO)

    Property REQUEST_SENDER_ID As Decimal? 'EMPLOYEE_ID
    Property SENDER_CODE As String
    Property SENDER_NAME As String
    Property SENDER_EMAIL As String
    Property SENDER_MOBILE As String
    Property COURSE_ID As Decimal?
    Property COURSE_NAME As String
    Property TRAIN_FORM_ID As Decimal?
    Property TRAIN_FORM As String
    Property TARGET_TRAIN As String
    Property IS_IRREGULARLY As Boolean?
    Property PROGRAM_GROUP As String
    Property TRAIN_FIELD As String
    Property PROPERTIES_NEED_ID As Decimal?
    Property PROPERTIES_NEED As String
    Property UNIT_ID As Decimal?
    Property UNIT_NAME As String
    Property ATTACH_FILE As String
    Property lstCenters As List(Of PlanCenterDTO)
    Property lstTeachers As List(Of LectureDTO)
    Property COM_ID As Decimal?
    Property COM_NAME As String
    Property COM_DESC As String
    Property NUM_EMP As Decimal?
    Property ORG_GRO As String
    Property ORG_ID_GRO As String
    Property TIT_GRO As String
    Property TIT_ID_GRO As String
    Property WI_GRO As String
    Property lstOrgs As List(Of PlanOrgDTO)
    Property lstTits As List(Of PlanTitleDTO)
    Property lstWIs As List(Of PlanTitleDTO)

    Property EMPLOYEE_ID As Decimal?
    Property EXPECT_DATE_TO As Date?
    Property SENDER_TITLE_ID As Decimal?
    Property SENDER_TITLE_NAME As String
    Property OTHER_COURSE As String
    Property TRAINER_NUMBER As Decimal?
    Property TR_COMMIT As Boolean?
    Property CERTIFICATE As Boolean?
    Property TR_PLACE As String
    Property REASON_PORTAL As String
    Property EMPLOYEE_APPROVED As String
    Property EMPLOYEE_APPROVED_ID As Decimal?
    Property IS_APPROVE As Decimal?
    Property IS_PORTAL As Boolean?
    Property REQUEST_CODE As String
End Class