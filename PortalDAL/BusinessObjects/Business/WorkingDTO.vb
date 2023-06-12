﻿Public Class WorkingDTO
    Public Property ID As Guid
    Public Property EMPLOYEE_ID As Guid
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property TITLE_ID As Guid
    Public Property TITLE_NAME As String
    Public Property ORG_ID As Guid
    Public Property ORG_NAME As String
    Public Property INDIRECT_MANAGER As Guid
    Public Property INDIRECT_MANAGER_NAME As String
    Public Property DIRECT_MANAGER As Guid
    Public Property DIRECT_MANAGER_NAME As String
    Public Property COST_CENTER_HR As Guid
    Public Property COST_CENTER_HR_NAME As String
    Public Property COST_CENTER_ACC As Guid
    Public Property COST_CENTER_ACC_NAME As String
    Public Property LOCATION_HR As Guid
    Public Property LOCATION_HR_NAME As String
    Public Property LOCATION_ACC As Guid
    Public Property LOCATION_ACC_NAME As String
    Public Property INDUSTRY As Guid
    Public Property INDUSTRY_NAME As String
    Public Property IS_DECISION As Boolean
    Public Property IS_CHANGE_CB As Boolean
    Public Property SALARY_CB As Double
    Public Property TRANSFER_TYPE As Guid
    Public Property TRANSFER_TYPE_NAME As String
    Public Property TRANSFER_REASON As String
    Public Property DECISION As DecisionDTO
    Public Property DECISION_NO As String
    Public Property STATUS As Guid
    Public Property STATUS_NAME As String
    Public Property REMARK As String
    Public Property WORKING_TYPE As Int16?
    Public Property EFFECT_DATE As Date?
    Public Property EXPIRE_DATE As Date?
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class