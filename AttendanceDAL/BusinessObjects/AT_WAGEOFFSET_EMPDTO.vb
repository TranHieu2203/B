﻿Public Class AT_WAGEOFFSET_EMPDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_NAME As String
    Public Property EMPLOYEE_CODE As String
    Public Property ORG_ID As Decimal?
    Public Property ORG_NAME As String
    Public Property ORG_DESC As String
    Public Property TITLE_ID As Decimal?
    Public Property TITLE_NAME As String
    Public Property YEAR As Decimal?
    Public Property PERIOD_ID As Decimal?
    Public Property PERIOD_NAME As String

    Public Property WAGE_OFFSET As Decimal?
    Public Property NOTE As String

    Public Property CREATED_DATE As Date?
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date?
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class
