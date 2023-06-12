Public Class ProgramDTO
    Public Property ID As Decimal
    Public Property SEND_DATE As Date?
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property IS_IN_PLAN As Boolean?
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property PIC_ID As Decimal?
    Public Property PIC_NAME As String
    Public Property GROUP_WORK_ID As String
    Public Property GROUP_WORK_NAME As String
    Public Property STAGE_ID As Decimal?
    Public Property TITLE As String 'đợt tuyển dụng
    Public Property REQUEST_SALARY_FROM As Decimal?
    Public Property PRIORITY_LEVEL_ID As String
    Public Property PRIORITY_LEVEL_NAME As String
    Public Property REQUEST_NUMBER As String 'Decimal?
    Public Property IS_PORTAL As Boolean?
    Public Property WORK_TIME_ID As Decimal?
    Public Property WORK_TIME_NAME As String
    Public Property CODE As String
    Public Property JOB_NAME As String
    Public Property RECRUIT_TYPE_ID As String
    Public Property RECRUIT_TYPE_NAME As String
    Public Property RECRUIT_REASON As String
    Public Property RECRUIT_REASON_ID As String
    Public Property RECRUIT_REASON_NAME As String
    Public Property LEARNING_LEVEL_ID As String
    Public Property LEARNING_LEVEL_NAME As String
    Public Property RECRUIT_START As Date?
    Public Property RECEIVE_END As Date?
    Public Property TRAINING_LEVEL_MAIN_ID As String
    Public Property TRAINING_LEVEL_MAIN_NAME As String
    Public Property TRAINING_SCHOOL_MAIN_ID As String
    Public Property TRAINING_SCHOOL_MAIN_NAME As String
    Public Property GRADUATION_TYPE_MAIN_ID As String
    Public Property GRADUATION_TYPE_MAIN_NAME As String
    Public Property MAJOR_MAIN_ID As String
    Public Property MAJOR_MAIN_NAME As String
    Public Property TRAINING_LEVEL_SUB_ID As String
    Public Property TRAINING_LEVEL_SUB_NAME As String
    Public Property TRAINING_SCHOOL_SUB_ID As String
    Public Property TRAINING_SCHOOL_SUB_NAME As String
    Public Property GRADUATION_TYPE_SUB_ID As String
    Public Property GRADUATION_TYPE_SUB_NAME As String
    Public Property MAJOR_SUB_ID As String
    Public Property MAJOR_SUB_NAME As String
    Public Property LANGUAGE1_ID As String
    Public Property LANGUAGE1_NAME As String
    Public Property LANGUAGE1_LEVEL_ID As String
    Public Property LANGUAGE1_LEVEL_NAME As String
    Public Property LANGUAGE1_POINT As String
    Public Property LANGUAGE2_ID As String
    Public Property LANGUAGE2_NAME As String
    Public Property LANGUAGE2_LEVEL_ID As String
    Public Property LANGUAGE2_LEVEL_NAME As String
    Public Property LANGUAGE2_POINT As String
    Public Property LANGUAGE3_ID As String
    Public Property LANGUAGE3_NAME As String
    Public Property LANGUAGE3_LEVEL_ID As String
    Public Property LANGUAGE3_LEVEL_NAME As String
    Public Property LANGUAGE3_POINT As String
    Public Property COMPUTER_LEVEL_ID As String
    Public Property COMPUTER_LEVEL_NAME As String
    Public Property AGE_FROM As Decimal?
    Public Property AGE_TO As Decimal?
    Public Property GENDER_ID As String
    Public Property GENDER_NAME As String
    Public Property CANNANG As Decimal?
    Public Property CHIEUCAO As String
    Public Property THILUCTRAI As String
    Public Property THILUCPHAI As String
    Public Property APPEARANCE_ID As String
    Public Property APPEARANCE_NAME As String
    Public Property HEALTH_STATUS_ID As String
    Public Property HEALTH_STATUS_NAME As String
    Public Property EXPERIENCE_NUMBER As Decimal?
    Public Property DESCRIPTION As String
    Public Property REQUEST_EXPERIENCE As String
    Public Property REMARK As String
    Public Property STATUS_ID As String
    Public Property STATUS_NAME As String
    Public Property RECRUIT_SCOPE_ID As Decimal?
    Public Property lstEmp As List(Of ProgramEmpDTO)
    Public Property lstSoft As List(Of ProgramSoftSkillDTO)
    Public Property lstCharac As List(Of ProgramCharacterDTO)
    Public Property lstScope As List(Of ProgramScopeDTO)
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property CANDIDATE_COUNT As String 'Decimal?
    Public Property CANDIDATE_REQUEST As Decimal?
    Public Property CANDIDATE_RECEIVED As Decimal?
    Public Property SCOPE_COUNT As Decimal?
    Public Property FOLLOWERS_EMP_ID As Decimal?
    Public Property FOLLOWERS_EMP_NAME As String
    Public Property STAGERECRUITMENT_ID As Decimal?
    Public Property REQUEST_SALARY_TO As Decimal?
    Public Property NEGOTIABLESALARY As Decimal?
    Public Property POSITIONRECRUITMENT As String
    Public Property REQUESTOTHER As String
    Public Property REQUESTOTHER_DEFAULT As String
    Public Property SPECIALSKILLS As Decimal?
    Public Property EXPECTED_JOIN_DATE As Date?
    Public Property NUMBERRECRUITMENT As String
    Public Property IS_SUPPORT As Boolean?
    Public Property IS_OVER_LIMIT As Boolean?
    Public Property CONTRACT_TYPE_ID As Decimal?
    Public Property RC_RECRUIT_PROPERTY As Decimal?
    Public Property RECRUIT_NUMBER As Decimal?
    Public Property ORG_NAME_CTY As String
    Public Property LOCATION_ID As Decimal?
    Public Property SUPPORT_NAME As String
    Public Property EXAMP_COUNT As Decimal?

    Public Property CODE_YCTD As String
    Public Property NAME_YCTD As String
    Public Property PER_REQUEST As String

    Public Property RC_REQUEST_ID As Decimal?
    Public Property RC_REQUEST_NAME As String
    Public Property IS_INSERT_PRO As Boolean?

    Public Property PERSON_PT_RC As Decimal?
    Public Property WORKING_PLACE_ID As Decimal?

End Class

