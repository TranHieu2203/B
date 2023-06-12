Imports System.Reflection
Imports Oracle.DataAccess.Client

Namespace DataAccess
    Public Class NonQueryData
        Inherits OracleCommon

        ''' <summary>
        ''' NonQuery 1 câu lệnh SQL, bao gồm transaction
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteSQL(ByVal sql As String) As Integer
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand(sql, conn)
                        Try
                            'Open connection
                            conn.Open()

                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            Dim re As Integer = cmd.ExecuteNonQuery()
                            cmd.Transaction.Commit()

                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                            Return re

                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                            Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                            Throw ex
                        End Try
                    End Using
                End Using
            End Using
        End Function

        ''' <summary>
        ''' NonQuery 1 câu lệnh SQL có chứa tham số, bao gồm transaction
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <param name="orclParameter">Mảng các tham số truyền vào</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteSQL(Of T)(ByVal sql As String, ByVal obj As T) As Integer
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand(sql, conn)
                        Try
                            'Open connection
                            conn.Open()

                            'Add parameter
                            If obj IsNot Nothing Then
                                Dim idx As Integer = 0
                                For Each info As PropertyInfo In obj.GetType().GetProperties()
                                    Dim para = GetParameter(info.Name, info.GetValue(obj, Nothing))
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                        idx += 1
                                    End If
                                Next
                            End If

                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            Dim re As Integer = cmd.ExecuteNonQuery()
                            cmd.Transaction.Commit()

                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                            Return re
                        Catch ex As Exception
                            cmd.Transaction.Rollback()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                            Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                            Throw ex

                        End Try

                    End Using
                End Using
            End Using
        End Function

        Public Function ExecuteNonQuery(Of T)(ByVal cmd As OracleCommand, ByVal obj As T) As Integer
            cmd.Parameters.Clear()
            'Add parameter
            If obj IsNot Nothing Then
                Dim oraCom As New OracleCommon
                For Each info As PropertyInfo In obj.GetType().GetProperties()
                    Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing))
                    If para IsNot Nothing Then
                        cmd.Parameters.Add(para)
                    End If
                Next
            End If

            Return cmd.ExecuteNonQuery

        End Function

        ''' <summary>
        '''Get value không chứa tham số
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <param name="obj">Đối tượng chưa parameter</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteSQLScalar(Of T)(ByVal sql As String, ByVal obj As T)
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try 'Open connection
                                conn.Open()

                                'Add parameter
                                If obj IsNot Nothing Then
                                    Dim oraCom As New OracleCommon
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing))
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next

                                End If

                                Dim oReturn = cmd.ExecuteScalar

                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return oReturn

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                                Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                                Throw ex
                            End Try

                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' NonQuery procedure có chứa tham số, bao gồm transaction
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStore(ByVal sql As String) As Integer
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try
                                cmd.CommandType = CommandType.StoredProcedure
                                'Open connection
                                conn.Open()

                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                Dim re As Integer = cmd.ExecuteNonQuery()
                                cmd.Transaction.Commit()


                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return re

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                                Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                                Throw ex

                            End Try
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' NonQuery procedure có chứa tham số, bao gồm transaction
        ''' </summary> 
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <param name="obj">Mảng các tham số truyền vào</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStore(Of T)(ByVal sql As String, ByVal obj As T) As Integer
            Dim strPosOut As String = ""
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try
                                cmd.CommandType = CommandType.StoredProcedure
                                'Open connection
                                conn.Open()

                                'Add parameter
                                If obj IsNot Nothing Then
                                    Dim idx As Integer = 0
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = GetParameter(info.Name, info.GetValue(obj, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            If bOut Then
                                                strPosOut += idx.ToString + ";"
                                            End If
                                            cmd.Parameters.Add(para)
                                            idx += 1
                                        End If
                                    Next
                                End If

                                cmd.Transaction = cmd.Connection.BeginTransaction()

                                Dim re As Integer = cmd.ExecuteNonQuery()
                                cmd.Transaction.Commit()

                                ' Lấy dữ liệu kiểu out để trả về
                                If strPosOut <> "" Then
                                    strPosOut = strPosOut.Substring(0, strPosOut.Length - 1)
                                    If obj IsNot Nothing Then
                                        For Each str As String In strPosOut.Split(";")
                                            Dim key = cmd.Parameters(Integer.Parse(str)).ParameterName
                                            For Each info As PropertyInfo In obj.GetType().GetProperties()
                                                If info.Name = key Then
                                                    Select Case cmd.Parameters(Integer.Parse(str)).OracleDbType
                                                        Case OracleDbType.NVarchar2
                                                            info.SetValue(obj, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                        Case OracleDbType.Date
                                                            info.SetValue(obj, cmd.Parameters(Integer.Parse(str)).Value.ToString("dd/MM/yyyy"), Nothing)
                                                        Case OracleDbType.Decimal
                                                            info.SetValue(obj, cmd.Parameters(Integer.Parse(str)).Value.ToString, Nothing)
                                                    End Select
                                                    Exit For
                                                End If
                                            Next
                                        Next
                                    End If
                                End If

                                Return re
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                                Throw ex
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
                Throw ex
            End Try

        End Function

        ''' <summary>
        ''' Get value procedure có chứa tham số
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <param name="obj">Đối tượng chưa parameter</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStoreScalar(Of T)(ByVal sql As String, ByVal obj As T)
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try
                                'Open connection
                                conn.Open()
                                cmd.CommandType = CommandType.StoredProcedure

                                'Add parameter
                                If obj IsNot Nothing Then
                                    Dim oraCom As New OracleCommon
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing))
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If

                                Dim oReturn = cmd.ExecuteScalar

                                'Dispose all resource
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return oReturn

                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                                Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                                Throw ex

                            End Try
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace