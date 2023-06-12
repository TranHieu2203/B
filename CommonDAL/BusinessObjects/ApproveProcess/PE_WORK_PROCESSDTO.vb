Public Class PE_WORK_PROCESSDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property JOIN_DATE As Date?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property PROBLEMS As String
    Public Property REQUESTS As String
    Public Property TYPE_ID As Decimal?
    Public Property TYPE_NAME As String
    Public Property IS_SEND_APP As Decimal
    Public Property MANAGER_ID As Decimal?
    Public Property MANAGER_CODE As String
    Public Property MANAGER_NAME As String
    Public Property IS_CHANGE_SAL As Decimal?
    Public Property IS_CHANGE_TITLE As Decimal?
    Public Property IS_BONHIEM As Decimal?
    Public Property IS_THOINHIEM As Decimal?
    Public Property NOTE As String
    Public lstWorkEmp As List(Of PE_WORK_EMPLOYEEDTO)
End Class

Public Class PE_WORK_EMPLOYEEDTO
    Public Property ID As Decimal?
    Public Property PROCESS_ID As Decimal?
    Public Property WORK_NAME As String
    Public Property RESULT As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

Public Class HR_PROCESS_DTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property PROCESS_TYPE_ID As Decimal?
    Public Property PROCESS_TYPE_NAME As String
    Public Property PROCESS_TYPE_CODE As String
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property NOTY_CONTENT As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property IS_READ As Decimal?
    Public Property NOTY_DATE As Date?
    Public Property PROCESS_TYPE As String
End Class

Public Class PE_WORK_MANAGER_CONCLUDEDTO
    Public Property ID As Decimal?
    Public Property PROCESS_ID As Decimal?
    Public Property CONCLUDE_TYPE As Decimal?
    Public Property DATE_PROBATION As Date?
    Public Property CONTRACT_TYPE As Decimal?
    Public Property APOINT_TITLE As Decimal?
    Public Property APOINT_OTHER As String
    Public Property MOVE_TITLE As Decimal?
    Public Property MOVE_OTHER As String
    Public Property IS_SAL_CHANGE As Decimal?
    Public Property IS_JOBBAND_CHANGE As Decimal?
    Public Property SAL_SUGGET As Decimal?
    Public Property IS_CHANGE_TITLE As Decimal?
    Public Property CHANGE_TITLE As Decimal?
    Public Property CHANGE_OTHER As String
    Public Property STAFF_RANK As String
    Public Property STATUS As Decimal?
    Public Property IS_CONFIRM As Decimal?

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property NOTE As String
    Public lstWorkMan As List(Of PE_WORK_MANAGERDTO)
End Class

Public Class PE_WORK_MANAGERDTO
    Public Property ID As Decimal?
    Public Property PROCESS_ID As Decimal?
    Public Property WORK_ID As Decimal?
    Public Property WORK_NAME As String
    Public Property RESULT As Decimal?
    Public Property REMARK As String
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class

Public Class HU_TERMINATION_PROCESSDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property STAFF_RANK_ID As Decimal?
    Public Property STAFF_RANK_NAME As String
    Public Property JOIN_DATE As Date?
    Public Property TIME_OVER As Decimal?
    Public Property TIME_OVER_OTHER As Decimal?
    Public Property TER_LASTDATE As Date?
    Public Property REASON_DETAIL As String
    Public Property IS_COMMIT As Decimal?
    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
    Public Property STATUS As Decimal?
    Public Property STATUS_NAME As String
    Public Property IS_SEND_APP As Decimal
    Public Property REASON_ID As Decimal?
    Public Property REASON_NAME As String
End Class



Public Class PE_MANAGER_OFFER_DTO
    Public Property processDTO As PE_WORK_PROCESSDTO
    Public Property manConcludeDTO As PE_WORK_MANAGER_CONCLUDEDTO
End Class