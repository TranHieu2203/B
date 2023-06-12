Imports System.Data.Objects
Imports Framework.Data


Partial Public Class ProfileContext
    Public Overloads Function SaveChanges(ByVal log As UserLog) As Integer
        Dim entries = Me.ObjectStateManager.GetObjectStateEntries(EntityState.Added Or EntityState.Modified Or EntityState.Deleted)
        Dim intRet As Integer = -1
        Dim lstAuditLogs As New List(Of AuditLog)
        For Each entry As ObjectStateEntry In entries
            Dim Auditable = entry.Entity
            Dim ObjectId As Decimal
            Dim ObjectState As String = ""
            Dim IsAudit As Boolean = False
            Dim EntityHelper As New EntityHelper
            If Auditable IsNot Nothing Then
                Dim ObjectName As String = entry.EntitySet.Name

                If entry.State = EntityState.Modified OrElse entry.State = EntityState.Deleted Then
                    ObjectId = entry.EntityKey.EntityKeyValues(0).Value
                Else
                    ObjectId = EntityHelper.GetProperty(Auditable, "ID")
                End If

                If entry.State = EntityState.Added Then
                    EntityHelper.SetProperty("CREATED_BY", log.Username, Auditable)
                    EntityHelper.SetProperty("CREATED_DATE", DateTime.Now, Auditable)
                    EntityHelper.SetProperty("CREATED_LOG", log.Ip & "-" & log.ComputerName, Auditable)
                    EntityHelper.SetProperty("MODIFIED_BY", log.Username, Auditable)
                    EntityHelper.SetProperty("MODIFIED_DATE", DateTime.Now, Auditable)
                    EntityHelper.SetProperty("MODIFIED_LOG", log.Ip & "-" & log.ComputerName, Auditable)
                    ObjectState = "I"
                    IsAudit = True
                ElseIf entry.State = EntityState.Modified Then
                    EntityHelper.SetProperty("MODIFIED_BY", log.Username, Auditable)
                    EntityHelper.SetProperty("MODIFIED_DATE", DateTime.Now, Auditable)
                    EntityHelper.SetProperty("MODIFIED_LOG", log.Ip & "-" & log.ComputerName, Auditable)
                    ObjectState = "U"
                    IsAudit = True
                ElseIf entry.State = EntityState.Deleted Then
                    ObjectState = "D"
                    IsAudit = True
                End If


                If IsAudit AndAlso AuditLogHelper.IsAuditLog(ObjectName) Then

                    Dim lstLogDtl = EntityHelper.GetEntryValueInString(entry)
                    lstAuditLogs.Add(New AuditLog With {.ObjectId = ObjectId,
                                                    .ObjectName = ObjectName,
                                                    .ObjectState = ObjectState,
                                                    .lstLogDtl = lstLogDtl})
                End If
            End If
        Next
        intRet = Me.SaveChanges()
        AuditLogHelper.TryAuditLog(lstAuditLogs, log)

        Return intRet
    End Function


End Class
