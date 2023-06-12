Public Class AnnualLeaveDTO
    Public Property Year As Decimal
    Public Property Month As Decimal
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property EmployeeName As String
    Public Property OrgName As String
    Public Property OrgDesc As String
    Public Property PosName As String
    Public Property JoinDate As Date?
    Public Property Prev_Have As Decimal
    Public Property Prev_Used As Decimal
    Public Property Expiration_Date As Date
    Public Property Cur_Have As Decimal
    Public Property Cur_Used As Decimal
    Public Property ShowTerminate As Boolean
    Public Property TerminateDate As Date?
    Public Property Seniority As Decimal
    Public Property Calculate_Date As Date
    Public Property Fund As Decimal
    Public Property Remaining As Decimal
    Public Property OrgIds As List(Of Decimal)
    Public Property EmpIds As List(Of Decimal)
    Public Property isCalcu As Boolean
End Class

Public Class UpdateAnnualLeaveDTO
    Public Property STARTDATE As Date
    Public Property ENDDATE As Date
    Public Property LSTEMP As List(Of Decimal)
End Class

Public Class AnnualLeaveSummaryDTO
    Public Property Year As Decimal
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property EmployeeName As String
    Public Property OrgName As String
    Public Property OrgDesc As String
    Public Property PosName As String
    Public Property JoinDate As Date?
    Public Property Seniority As String
    Public Property SeniorityHave As Decimal
    Public Property Prev_Have As Decimal
    Public Property Prev_Used As Decimal
    Public Property Cur_Have As Decimal
    Public Property Cur_Used As Decimal
    Public Property Total_Used As Decimal
    Public Property Fund As Decimal
    Public Property Leave1 As Decimal
    Public Property Leave2 As Decimal
    Public Property Leave3 As Decimal
    Public Property Leave4 As Decimal
    Public Property Leave5 As Decimal
    Public Property Leave6 As Decimal
    Public Property Leave7 As Decimal
    Public Property Leave8 As Decimal
    Public Property Leave9 As Decimal
    Public Property Leave10 As Decimal
    Public Property Leave11 As Decimal
    Public Property Leave12 As Decimal
    Public Property Remaining As Decimal

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj.GetType() = Me.GetType Then
            Return (Me.EmployeeID = CType(obj, AnnualLeaveSummaryDTO).EmployeeID)
        End If
        Return MyBase.Equals(obj)
    End Function
End Class