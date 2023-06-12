Imports System.Reflection
Imports System.Threading.Tasks
Imports Oracle.DataAccess.Client

Namespace DataAccess
    Public Class QueryData
        Inherits OracleCommon

        ''' <summary>
        ''' Query 1 câu lệnh SQL
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteSQL(ByVal sql As String, Optional ByVal isDataTable As Boolean = True) As Object
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand(sql, conn)
                        Try
                            'Open connection
                            conn.Open()

                            Dim da As New OracleDataAdapter(cmd)
                            Dim oReturn As Object
                            If isDataTable Then
                                oReturn = New DataTable("Table")
                                da.Fill(oReturn)
                            Else
                                oReturn = New DataSet()
                                da.Fill(oReturn)
                            End If
                            'Dispose all resource
                            da.Dispose()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                            Return oReturn

                        Catch ex As Exception
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
        '''  Query 1 câu lệnh SQL có chứa tham số
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="sql"></param>
        ''' <param name="obj"></param>
        ''' <param name="isDataTable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteSQL(Of T)(ByVal sql As String, ByVal obj As T, Optional ByVal isDataTable As Boolean = True) As Object
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand(sql, conn)
                        Try
                            'Open connection
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

                            Dim da As New OracleDataAdapter(cmd)
                            Dim oReturn As Object
                            If isDataTable Then
                                oReturn = New DataTable("Table")
                                da.Fill(oReturn)
                            Else
                                oReturn = New DataSet()
                                da.Fill(oReturn)
                            End If

                            'Dispose all resource
                            da.Dispose()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                            Return oReturn

                        Catch ex As Exception
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
        ''' Query procedure không chứa tham số
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStore(ByVal sql As String, Optional ByVal isDataTable As Boolean = True)
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try
                                'Open connection
                                conn.Open()
                                cmd.CommandType = CommandType.StoredProcedure

                                Dim da As New OracleDataAdapter(cmd)
                                Dim oReturn As Object
                                If isDataTable Then
                                    oReturn = New DataTable("Table")
                                    da.Fill(oReturn)
                                Else
                                    oReturn = New DataSet()
                                    da.Fill(oReturn)
                                End If
                                'Dispose all resource
                                da.Dispose()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return oReturn

                            Catch ex As Exception
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

        Public Function ExecuteStore(ByVal sql As String, ByVal listParam As Dictionary(Of String, Object), Optional ByVal isDataTable As Boolean = True)
            Dim strPosOut As String = ""
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(sql, conn)
                            Try
                                'Open connection
                                conn.Open()
                                cmd.CommandType = CommandType.StoredProcedure

                                'Add parameter
                                If listParam.Count > 0 Then
                                    For Each item In listParam
                                        cmd.Parameters.Add(New OracleParameter(item.Key, item.Value))
                                    Next
                                End If

                                Dim da As New OracleDataAdapter(cmd)
                                Dim oReturn As Object
                                If isDataTable Then
                                    oReturn = New DataTable("Table")
                                    da.Fill(oReturn)
                                Else
                                    oReturn = New DataSet()
                                    da.Fill(oReturn)
                                End If

                                'Dispose all resource
                                da.Dispose()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return oReturn

                            Catch ex As Exception
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
        ''' Query procedure có chứa tham số
        ''' </summary>
        ''' <param name="sql">Câu lệnh SQL</param>
        ''' <param name="obj">Đối tượng chưa parameter</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStore(Of T)(ByVal sql As String, ByRef obj As T, Optional ByVal isDataTable As Boolean = True)
            Dim strPosOut As String = ""
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

                                Dim da As New OracleDataAdapter(cmd)
                                Dim oReturn As Object
                                If isDataTable Then
                                    oReturn = New DataTable("Table")
                                    da.Fill(oReturn)
                                Else
                                    oReturn = New DataSet()
                                    da.Fill(oReturn)
                                End If

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

                                'Dispose all resource
                                da.Dispose()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()

                                Return oReturn

                            Catch ex As Exception
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
        ''' Thực thi gọi thủ tục không kiểm soát giá trị trả về
        ''' </summary>
        Public Sub ExecuteStoreProcedureNonQueryTask(ByVal sql As String, ByVal obj As Dictionary(Of String, Object))
            Task.Factory.StartNew(Sub()
                                      Dim strPosOut As String = ""
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
                                                              Dim idx As Integer = 0
                                                              For Each item In obj
                                                                  Dim bOut As Boolean = False
                                                                  Dim para = GetParameter(item.Key, item.Value, bOut)
                                                                  If para IsNot Nothing Then
                                                                      If bOut Then
                                                                          strPosOut += idx.ToString + ";"
                                                                      End If
                                                                      cmd.Parameters.Add(para)
                                                                      idx += 1
                                                                  End If
                                                              Next
                                                          End If

                                                          Dim da As New OracleDataAdapter(cmd)
                                                          Dim oReturn As Object

                                                          oReturn = New DataSet()
                                                          da.Fill(oReturn)

                                                          'Dispose all resource
                                                          da.Dispose()
                                                          cmd.Dispose()
                                                          conn.Close()
                                                          conn.Dispose()

                                                          'Return oReturn

                                                      Catch ex As Exception
                                                          cmd.Dispose()
                                                          conn.Close()
                                                          conn.Dispose()
                                                          Utilities.WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "DataAccess")
                                                          'Throw ex

                                                      End Try
                                                  End Using
                                              End Using
                                          End Using

                                      Catch ex As Exception
                                          'Throw ex
                                      End Try
                                  End Sub)
        End Sub
    End Class

End Namespace