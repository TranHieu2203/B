Imports System.Configuration
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Framework.Data.DataAccess
Imports Framework.Data.System.Linq.Dynamic
Imports Oracle.DataAccess.Client

Public Class AuditLog
    Public Property ObjectId As Decimal
    Public Property ObjectName As String
    Public Property ObjectState As String
    Public Property lstLogDtl As List(Of AuditLogDtl)
End Class

Public Class AuditLogDtl
    Public Property COL_NAME As String
    Public Property OLD_VALUE As String
    Public Property NEW_VALUE As String
End Class

Public Class AuditLogHelper
    Shared _ctx As AuditLogContext

    Public Shared ReadOnly Property Context As AuditLogContext
        Get

            If _ctx Is Nothing Then
                _ctx = New AuditLogContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property

    Public Shared Function IsAuditLog(ByVal EntityName As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim regex As New Regex("\*", RegexOptions.IgnoreCase)

            Dim str As String = If(ConfigurationManager.AppSettings("AuditLogs") IsNot Nothing, ConfigurationManager.AppSettings("AuditLogs"), "")
            If regex.IsMatch(str) Then
                ret = True
            End If
            Dim regex1 As New Text.RegularExpressions.Regex("\+[\s]*" & EntityName, RegexOptions.IgnoreCase)
            If regex1.IsMatch(str) Then
                ret = True
            End If

            Dim regex2 As New Text.RegularExpressions.Regex("\-[\s]*" & EntityName, RegexOptions.IgnoreCase)
            If regex2.IsMatch(str) Then
                ret = False
            End If
            Return ret
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Shared Sub TryAuditLog(ByVal AuditLogs As List(Of AuditLog),
                               ByVal log As UserLog)

        Try

            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            Using resource As New DataAccess.OracleCommon()
                                For Each item In AuditLogs
                                    Dim strAction As String = "SAVE"
                                    Dim strPosOut As String = ""
                                    If log.ActionName IsNot Nothing AndAlso log.ActionName.Trim <> "" Then
                                        strAction = log.ActionName.Trim
                                    End If

                                    cmd.CommandText = "PKG_COMMON_BUSINESS.INSERT_LOG"
                                    cmd.Parameters.Clear()
                                    Dim emp_info = (From p In Context.SE_USER Where p.USERNAME.ToUpper = log.Username.ToUpper).FirstOrDefault
                                    Dim actionLog As New SE_ACTION_LOG With {
                                        .ID = Utilities.GetNextSequence(Context, Context.SE_ACTION_LOG.EntitySet.Name),
                                        .USERNAME = log.Username,
                                        .FULLNAME = If(log.Fullname Is Nothing, emp_info.FULLNAME, log.Fullname),
                                        .EMAIL = If(log.Email Is Nothing, emp_info.EMAIL, log.Email),
                                        .MOBILE = If(log.Mobile Is Nothing, emp_info.TELEPHONE, log.Mobile),
                                        .VIEW_NAME = log.ViewName,
                                        .VIEW_DESCRIPTION = If(log.ViewDescription <> "", log.ViewDescription, " "),
                                        .VIEW_GROUP = If(log.Fullname Is Nothing, "PORTAL", If(log.ViewGroup <> "", log.ViewGroup, " ")),
                                        .ACTION_NAME = strAction,
                                        .ACTION_TYPE = item.ObjectState,
                                        .ACTION_DATE = DateTime.Now,
                                        .OBJECT_ID = item.ObjectId,
                                        .OBJECT_NAME = item.ObjectName,
                                        .IP = log.Ip,
                                        .PLATFORM = If(log.Fullname Is Nothing, "PORTAL", "APP"),
                                        .COMPUTER_NAME = log.ComputerName}
                                    Context.SE_ACTION_LOG.AddObject(actionLog)

                                    Dim objParam = New With {.P_USERNAME = log.Username,
                                                             .P_FULLNAME = If(log.Fullname Is Nothing, emp_info.FULLNAME, log.Fullname),
                                                             .P_EMAIL = If(log.Email Is Nothing, emp_info.EMAIL, log.Email),
                                                             .P_MOBILE = If(log.Mobile Is Nothing, emp_info.TELEPHONE, log.Mobile),
                                                             .P_VIEW_NAME = log.ViewName,
                                                             .P_VIEW_DESCRIPTION = If(log.ViewDescription <> "", log.ViewDescription, " "),
                                                             .P_VIEW_GROUP = If(log.Fullname Is Nothing, "PORTAL", If(log.ViewGroup <> "", log.ViewGroup, " ")),
                                                             .P_ACTION_NAME = strAction,
                                                             .P_ACTION_TYPE = item.ObjectState,
                                                             .P_OBJECT_ID = item.ObjectId,
                                                             .P_OBJECT_NAME = item.ObjectName,
                                                             .P_IP = log.Ip,
                                                             .P_COMPUTER_NAME = log.ComputerName,
                                                             .PO_ID = resource.OUT_NUMBER}

                                    If objParam IsNot Nothing Then
                                        Dim idx As Integer = 0
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                If bOut Then
                                                    strPosOut = idx.ToString + ";"
                                                End If
                                                cmd.Parameters.Add(para)
                                                idx += 1
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()

                                    ' Lấy dữ liệu kiểu out để trả về
                                    If strPosOut <> "" Then
                                        strPosOut = strPosOut.Substring(0, strPosOut.Length - 1)
                                        If objParam IsNot Nothing Then
                                            For Each str As String In strPosOut.Split(";")
                                                Dim key = cmd.Parameters(Integer.Parse(str)).ParameterName
                                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                                    If info.Name = key Then
                                                        Select Case cmd.Parameters(Integer.Parse(str)).OracleDbType
                                                            Case OracleDbType.NVarchar2
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                            Case OracleDbType.Date
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString("dd/MM/yyyy"), Nothing)
                                                            Case OracleDbType.Decimal
                                                                info.SetValue(objParam, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                        End Select
                                                        Exit For
                                                    End If
                                                Next
                                            Next
                                        End If
                                    End If


                                    cmd.CommandText = "PKG_COMMON_BUSINESS.INSERT_LOG_DTL"
                                    For Each item1 In item.lstLogDtl
                                        cmd.Parameters.Clear()
                                        Dim objParam1 = New With {.P_COLNAME = item1.COL_NAME,
                                                                 .P_OLDVALUE = item1.OLD_VALUE,
                                                                 .P_NEWVALUE = item1.NEW_VALUE,
                                                                 .P_ACTIONLOG_ID = objParam.PO_ID}

                                        If objParam1 IsNot Nothing Then
                                            For Each info As PropertyInfo In objParam1.GetType().GetProperties()
                                                Dim bOut As Boolean = False
                                                Dim para = resource.GetParameter(info.Name, info.GetValue(objParam1, Nothing), bOut)
                                                If para IsNot Nothing Then
                                                    cmd.Parameters.Add(para)
                                                End If
                                            Next
                                        End If
                                        cmd.ExecuteNonQuery()
                                    Next
                                Next
                                cmd.Transaction.Commit()
                            End Using
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function GetAccessLog(ByVal filter As AccessLogFilter,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "LoginDate desc") As List(Of AccessLog)

        Try
            Dim query = (From p In Context.SE_ACCESS_LOG
                         Select New AccessLog With {.Id = p.ID,
                                                  .Username = p.USERNAME,
                                                  .Fullname = p.FULLNAME,
                                                  .Email = p.EMAIL,
                                                  .Mobile = p.MOBILE,
                                                  .AccessFunctions = p.ACCESS_FUNCTIONS,
                                                  .LoginDate = p.LOGIN_DATE,
                                                  .LogoutDate = p.LOGOUT_DATE,
                                                  .LogoutStatus = p.LOGOUT_STATUS,
                                                  .IP = p.IP,
                                                  .ComputerName = p.COMPUTER_NAME,
                                                  .GroupName = p.GROUP_NAMES})

            If filter.Status IsNot Nothing AndAlso filter.Status.Trim <> "" Then
                query = query.Where(Function(p) p.LogoutStatus.ToUpper.Contains(filter.Status.ToUpper))
            End If
            If filter.Username IsNot Nothing AndAlso filter.Username.Trim <> "" Then
                query = query.Where(Function(p) p.Username.ToUpper.Contains(filter.Username.ToUpper))
            End If

            If filter.username_login <> "ADMIN" Then
                query = query.Where(Function(p) p.Username.ToUpper <> "ADMIN")
            End If
            If filter.IP IsNot Nothing AndAlso filter.IP.Trim <> "" Then
                query = query.Where(Function(p) p.IP.ToUpper.Contains(filter.IP.ToUpper))
            End If
            If filter.FromDate IsNot Nothing Then
                query = query.Where(Function(p) p.LoginDate >= filter.FromDate)
            End If
            If filter.ToDate IsNot Nothing Then
                query = query.Where(Function(p) p.LoginDate <= filter.ToDate)
            End If
            If filter.ComputerName IsNot Nothing AndAlso filter.ComputerName.Trim <> "" Then
                query = query.Where(Function(p) p.ComputerName.ToUpper.Contains(filter.ComputerName.ToUpper))
            End If
            query = query.OrderBy(Sorts)

            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            'Logger.LogInfo(query)

            Return (query.ToList)
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Public Shared Function GetReportAccessLog(ByVal _fromDate As Date?, ByVal _toDate As Date?) As List(Of AccessLog)

        Try
            Dim query = (From p In Context.SE_ACCESS_LOG
                         Order By p.LOGIN_DATE Descending
                         Select New AccessLog With {.Id = p.ID,
                                                  .Username = p.USERNAME,
                                                  .Fullname = p.FULLNAME,
                                                  .Email = p.EMAIL,
                                                  .Mobile = p.MOBILE,
                                                  .AccessFunctions = p.ACCESS_FUNCTIONS,
                                                  .LoginDate = p.LOGIN_DATE,
                                                  .LogoutDate = p.LOGOUT_DATE,
                                                  .LogoutStatus = p.LOGOUT_STATUS,
                                                  .IP = p.IP,
                                                  .ComputerName = p.COMPUTER_NAME})


            If _fromDate IsNot Nothing Then
                query = query.Where(Function(p) p.LoginDate >= _fromDate)
            End If
            If _toDate IsNot Nothing Then
                query = query.Where(Function(p) p.LoginDate <= _toDate)
            End If
            Return (query.ToList)
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Public Shared Function InsertAccessLog(ByVal _accesslog As AccessLog) As Boolean


        Dim objAccessLog As New SE_ACCESS_LOG
        Try
            objAccessLog.ID = Utilities.GetNextSequence(Context, Context.SE_ACCESS_LOG.EntitySet.Name)
            objAccessLog.USERNAME = _accesslog.Username
            objAccessLog.FULLNAME = _accesslog.Fullname
            objAccessLog.EMAIL = _accesslog.Email
            objAccessLog.MOBILE = _accesslog.Mobile
            objAccessLog.ACCESS_FUNCTIONS = _accesslog.AccessFunctions
            objAccessLog.LOGIN_DATE = _accesslog.LoginDate
            objAccessLog.LOGOUT_DATE = _accesslog.LogoutDate
            objAccessLog.LOGOUT_STATUS = _accesslog.LogoutStatus
            objAccessLog.IP = _accesslog.IP
            objAccessLog.COMPUTER_NAME = _accesslog.ComputerName
            Context.SE_ACCESS_LOG.AddObject(objAccessLog)
            Context.SaveChanges()
            Return True
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Public Shared Function GetActionLog(ByVal ObjectId As Decimal) As List(Of ActionLog)

        Try
            Dim query = (From p In Context.SE_ACTION_LOG
                         Order By p.ACTION_DATE Descending
                         Select New ActionLog With {
                             .Id = p.ID,
                             .ObjectName = p.OBJECT_NAME,
                             .Username = p.USERNAME,
                             .Fullname = p.FULLNAME,
                             .Email = p.EMAIL,
                             .Mobile = p.MOBILE,
                             .ViewGroup = p.VIEW_GROUP,
                             .ViewName = p.VIEW_NAME,
                             .ViewDescription = p.VIEW_DESCRIPTION,
                             .ActionName = p.ACTION_NAME,
                             .ActionDate = p.ACTION_DATE,
                             .IP = p.IP,
                             .ComputerName = p.COMPUTER_NAME})

            Return query.ToList
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Public Shared Function GetActionLogByID(ByVal gID As Decimal) As List(Of AuditLogDtl)

        Try
            Dim obj = (From p In Context.SE_LOG_DTL
                       Where p.SE_ACTION_LOG_ID = gID
                       Select New AuditLogDtl With {
                       .COL_NAME = p.COL_NAME,
                       .OLD_VALUE = p.OLD_VALUE,
                       .NEW_VALUE = p.NEW_VALUE}).ToList

            Return obj
        Catch ex As Exception

            Throw ex
        End Try

    End Function


    Public Shared Function GetActionLog(ByVal filter As ActionLogFilter,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ActionDate desc") As List(Of ActionLog)

        Try
            Dim query = (From p In Context.SE_ACTION_LOG
                         Order By p.ACTION_DATE Descending
                         Select New ActionLog With {
                             .Id = p.ID,
                             .ObjectName = p.OBJECT_NAME,
                             .Username = p.USERNAME,
                             .Fullname = p.FULLNAME,
                             .Email = p.EMAIL,
                             .Mobile = p.MOBILE,
                             .ViewGroup = p.VIEW_GROUP,
                             .ViewName = p.VIEW_NAME,
                             .ViewDescription = p.VIEW_DESCRIPTION,
                             .ActionName = p.ACTION_NAME,
                             .ActionDate = p.ACTION_DATE,
                             .IP = p.IP,
                             .PLATFORM = p.PLATFORM,
                             .ComputerName = p.COMPUTER_NAME})

            If filter.Platform = "2" Then
                query = query.Where(Function(p) p.PLATFORM = "PORTAL")
            ElseIf filter.Platform = "1" Then
                query = query.Where(Function(p) p.PLATFORM = "APP")
            ElseIf filter.Platform = "3" Then
                query = query.Where(Function(p) p.PLATFORM = "MOBILE")
            End If

            If filter.ViewGroup IsNot Nothing AndAlso filter.ViewGroup.Trim <> "" Then
                query = query.Where(Function(p) p.ViewGroup.ToUpper.Contains(filter.ViewGroup.ToUpper))
            End If
            If filter.ViewName IsNot Nothing AndAlso filter.ViewName.Trim <> "" Then
                query = query.Where(Function(p) p.ViewName.ToUpper.Contains(filter.ViewName.ToUpper))
            End If
            If filter.ActionName IsNot Nothing AndAlso filter.ActionName.Trim <> "" Then
                query = query.Where(Function(p) p.ActionName.ToUpper.Contains(filter.ActionName.ToUpper))
            End If
            If filter.UserName IsNot Nothing AndAlso filter.UserName.Trim <> "" Then
                query = query.Where(Function(p) p.Username.ToUpper.Contains(filter.UserName.ToUpper))
            End If

            If filter.username_login <> "ADMIN" Then
                query = query.Where(Function(p) p.Username.ToUpper <> "ADMIN")
            End If
            If filter.IP IsNot Nothing AndAlso filter.IP.Trim <> "" Then
                query = query.Where(Function(p) p.IP.ToUpper.Contains(filter.IP))
            End If
            If filter.FromDate IsNot Nothing Then
                query = query.Where(Function(p) p.ActionDate >= filter.FromDate)
            End If
            If filter.ToDate IsNot Nothing Then
                query = query.Where(Function(p) p.ActionDate <= filter.ToDate)
            End If
            'If filter.ActionDate IsNot Nothing Then
            '    Dim date1 = Format(CDate(filter.ActionDate), "dd/MM/yyyy")
            '    query = query.Where(Function(p) p.ActionDate = filter.ActionDate)
            'End If
            If filter.ComputerName IsNot Nothing AndAlso filter.ComputerName.Trim <> "" Then
                query = query.Where(Function(p) p.ComputerName.ToUpper.Contains(filter.ComputerName.ToUpper))
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            'Logger.LogInfo(query)
            Return query.ToList
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Public Shared Function GetReportActionLog(ByVal _fromDate As Date?, ByVal _toDate As Date?) As List(Of ActionLog)
        Try
            Dim Context As New AuditLogContext
            Dim query = (From p In Context.SE_ACTION_LOG
                         Order By p.ACTION_DATE Descending
                         Select New ActionLog With {
                             .Id = p.ID,
                             .ObjectName = p.OBJECT_NAME,
                             .Username = p.USERNAME,
                             .Fullname = p.FULLNAME,
                             .Email = p.EMAIL,
                             .Mobile = p.MOBILE,
                             .ViewGroup = p.VIEW_GROUP,
                             .ViewName = p.VIEW_NAME,
                             .ViewDescription = p.VIEW_DESCRIPTION,
                             .ActionName = p.ACTION_NAME,
                             .ActionDate = p.ACTION_DATE,
                             .IP = p.IP,
                             .ComputerName = p.COMPUTER_NAME})

            If _fromDate IsNot Nothing Then
                query = query.Where(Function(p) p.ActionDate >= _fromDate)
            End If
            If _toDate IsNot Nothing Then
                query = query.Where(Function(p) p.ActionDate <= _toDate)
            End If
            Return query.ToList
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function DeleteActionLogs(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    conn.Open()
                    Using cmd As New OracleCommand()
                        cmd.Connection = conn
                        cmd.Transaction = cmd.Connection.BeginTransaction()
                        Try
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_COMMON_BUSINESS.DELETE_LOG"
                            For i As Integer = 0 To lstDeleteIds.Count - 1
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon
                                    Dim objParam = New With {.P_ID = lstDeleteIds(i)}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception

            Throw ex
        End Try

    End Function

End Class
