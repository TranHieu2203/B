Imports System.Activities

Public Class CommonWorkflow
    Public Shared Property WfApplication As WorkflowApplication
    Public Shared Property WfLongRunningIsRunning As Integer = 0
    Public Shared Property WfLongRunningCount As Integer = 0
End Class
