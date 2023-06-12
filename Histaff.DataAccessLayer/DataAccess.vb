Option Strict On
Option Explicit On

Imports System.Text
Imports Oracle.DataAccess.Client

Namespace DataAccess

    ''' <summary>
    ''' Thực thi Store Procdure Oracle
    ''' </summary>
    ''' <remarks></remarks>
    Partial Public Class QueryData
        Inherits OracleCommon

#Region "Histaff Framework Version 2.0"
        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Get danh sách tham số của 1 StoreProcedure
        ''' </summary>
        ''' <param name="storeName">Tên store Procedure</param>
        ''' <param name="isInParameterOnly">True: Chỉ lấy danh sách tham số In, False: lấy tất cả tham số</param>
        ''' <returns>Datatable danh sách tham số</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Public Function GetSpParameterSet(ByVal storeName As String, Optional isInParameterOnly As Boolean = False) As DataTable
            Try
                Using conMng As New ConnectionManager
                    Dim connectionString As String = conMng.GetConnectionString()

                    '1. Lấy danh sách Parameters từ Database => CommandParameters
                    Dim CommandParameters As OracleParameter()
                    CommandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, storeName, True)

                    If CommandParameters IsNot Nothing Then
                        Dim dtb As New DataTable("CommandParameters")
                        dtb.Columns.Add("ParametersName")
                        dtb.Columns.Add("DpType")
                        dtb.Columns.Add("Direction")

                        For i = 0 To CommandParameters.Length - 1
                            Dim dr As DataRow = dtb.NewRow

                            If isInParameterOnly AndAlso (CommandParameters(i).Direction = ParameterDirection.Input OrElse CommandParameters(i).Direction = ParameterDirection.InputOutput) Then
                                dr("ParametersName") = CommandParameters(i).ParameterName
                                dr("DpType") = CommandParameters(i).OracleDbType
                                dr("Direction") = CommandParameters(i).Direction
                                dtb.Rows.Add(dr)
                            ElseIf isInParameterOnly = False Then
                                dr("ParametersName") = CommandParameters(i).ParameterName
                                dr("DpType") = CommandParameters(i).OracleDbType
                                dr("Direction") = CommandParameters(i).Direction
                                dtb.Rows.Add(dr)
                            End If
                        Next
                        Return dtb
                    Else : Return Nothing
                    End If
                End Using
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => GetSpParameterSet", String.Format("{0} " & vbNewLine & "isInParameterOnly: {1}", storeName, isInParameterOnly))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Thực thi Store Procedure trả về 1 DataSet (1 hoặc nhiều datatable) 
        ''' Thứ tự value trong parameterValue phải đúng với thứ tự tham số Input (không quan tâm vị trí tham số Output) được khái báo trong Store Procedure
        ''' Không truyền biến Out Cursor
        ''' Không Output biến Scalar
        ''' Không cần khai báo ParameterName 
        ''' Có sử dụng Cache
        ''' </summary>
        ''' <param name="storeName">Tên Store Procedure Oracle</param>
        ''' <param name="parameterValue">Danh sách value truyền vào để thực thi Store Procedure</param>
        ''' <returns>Dataset (không return biến Output)</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Public Function ExecuteToDataSet(ByVal storeName As String, Optional ByVal parameterValue As List(Of Object) = Nothing) As DataSet
            Try
                Using conMng As New ConnectionManager

                    Dim connectionString As String = conMng.GetConnectionString()

                    '1. Lấy danh sách Parameters từ Database => CommandParameters
                    Dim CommandParameters As OracleParameter()
                    CommandParameters = OracleHelperParameterCache.GetSpParameterSet(connectionString, storeName, True)

                    '2. Add parameterValue cho CommandParameters là Input nếu có CommandParameters
                    If CommandParameters IsNot Nothing Then
                        Dim idx As Integer = 0 'Duyệt list Parameter Value

                        'Duyệt danh sách CommandParameters lấy được từ Database
                        'Check nều là biến In thì gán Value cho CommandParameters(i)
                        For i = 0 To CommandParameters.Length - 1
                            'Check nếu là biến Input thì mới gán value
                            If CommandParameters(i).Direction <> ParameterDirection.Output And CommandParameters(i).Direction <> ParameterDirection.ReturnValue Then
                                CommandParameters(i).Value = parameterValue(idx) 'Gán value
                                idx = idx + 1
                            End If
                        Next
                    End If

                    '3. Thực thi Store Procedure với 2 dạng có tham số hoặc không có tham số
                    If CommandParameters IsNot Nothing Then
                        Return OracleHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, storeName, CommandParameters)
                    Else '???
                        Return OracleHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, storeName)
                    End If
                End Using
            Catch ex As Exception
                Dim sb As New StringBuilder
                sb.AppendLine("{")
                If parameterValue Is Nothing Then
                    sb.AppendLine("Nothing")
                Else
                    For Each param As Object In parameterValue
                        If param Is Nothing Then
                            sb.AppendLine("Nothing,")
                        Else
                            sb.AppendLine(param.ToString() + ",")
                        End If
                    Next
                End If
                sb.AppendLine("}")
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteToDataSet", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Thực thi Store Procedure Oracle trả về mảng cái tham số Output là Scalar (không phải Cursor)
        ''' Có sử dụng Cache
        ''' </summary>
        ''' <param name="storeName">Tên store procedure</param>
        ''' <param name="parameters">List value các tham số Input để thực thi Store</param>
        ''' <returns>Mảng các giá trị output</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Public Function ExecuteStoreScalar(ByVal storeName As String,
                                           ByVal parameters As List(Of Object)) As Object()
            Try
                Using conMng As New ConnectionManager

                    Return OracleHelper.ExecuteScalar(conMng.GetConnectionString(),
                                                    storeName,
                                                    parameters.ToArray())

                End Using
            Catch ex As Exception
                Dim sb As New StringBuilder
                sb.AppendLine("{")
                If parameters Is Nothing Then
                    sb.AppendLine("Nothing")
                Else
                    For Each param As Object In parameters
                        If param Is Nothing Then
                            sb.AppendLine("Nothing,")
                        Else
                            sb.AppendLine(param.ToString() + ",")
                        End If
                    Next
                End If
                sb.AppendLine("}")
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteStoreScalar", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Update/Insert 1 DataTable lên database Mở kết nối => duyệt từng rows cho đến khi hết row thì => đóng kết nối
        ''' Có transaction
        ''' </summary>
        ''' <param name="storeName">Tên Store Produre</param>
        ''' <param name="dtb">Datatable chứa dữ liệu update/insert, Tên Cột là tên tham số, giá trị từng Cell trên 1 row là value tương ứng</param>
        ''' <returns>Số dùng transaction thành công</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Public Function ExecuteBatchCommand(ByVal storeName As String, ByVal dtb As DataTable) As Integer

            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Dim connectionString As String = conMng.GetConnectionString()
                        Using cmd As New OracleCommand(storeName, conn)
                            cmd.CommandType = CommandType.StoredProcedure

                            '1. Open connection
                            conn.Open()

                            Dim re As Integer = 0

                            Try
                                '2. Begin Transaction
                                cmd.Transaction = cmd.Connection.BeginTransaction()

                                '3. Duyệt từng row để insert/update/delete
                                For Each dr As DataRow In dtb.Rows
                                    cmd.Parameters.Clear()
                                    '3.1 Truyền parameter (Tên cột là tên tham số, từng Cell là Value
                                    For index = 0 To dtb.Columns.Count - 1 'Lấy parameter name
                                        Dim para = GetParameter(dtb.Columns(index).ColumnName, dr(index), False)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next

                                    '3.2 Thực thi Store với Parameters được truyền vào
                                    re = re + cmd.ExecuteNonQuery()
                                Next

                                '4. Check nếu insert/update/delete thành công toàn bộ thì mới Commit còn không thì Rollback
                                If (-1) * re = dtb.Rows.Count Then
                                    cmd.Transaction.Commit()
                                Else
                                    cmd.Transaction.Rollback()
                                End If
                            Catch ex As Exception
                                'ghi log rows thực hiện thất bại
                                Dim sb As New StringBuilder
                                sb.AppendLine("{")
                                If cmd.Parameters Is Nothing Then
                                    sb.AppendLine("Nothing")
                                Else
                                    For jj = 0 To cmd.Parameters.Count - 1
                                        If cmd.Parameters(jj) Is Nothing Then
                                            sb.AppendLine("Nothing,")
                                        Else
                                            sb.AppendLine(cmd.Parameters(jj).Value.ToString() + ",")
                                        End If
                                    Next
                                End If
                                sb.AppendLine("}")

                                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteBatchCommand", String.Format("{0} ({1})", storeName, sb.ToString()))
                                cmd.Transaction.Rollback()
                                cmd.Dispose()
                                Throw ex
                            End Try

                            '5. Dispose all resource và Close Connection
                            cmd.Dispose()
                            conn.Close() 'Close
                            conn.Dispose()

                            '6. Return số dòng execute thành công
                            Return re * (-1)
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteBatchCommand", String.Format("{0} " & vbNewLine & "DataTableName: {1}" & vbNewLine & "Row(s): {2}", storeName, dtb.TableName, dtb.Rows.Count))
                Throw
            End Try

        End Function
#End Region

#Region "Histaff Framework Version 1.5"
        ''' <summary>
        ''' Execute Store Procedure chỉ truyền vào List Value của Parameters
        ''' Không dùng cache
        ''' </summary>
        ''' <param name="storeName">Tên store procedure</param>
        ''' <param name="parameters">List Value tham số In hoặc Out truyền vào</param>
        ''' <returns></returns>
        ''' <remarks>Histaff Framework 1.5</remarks>
        Public Function ExecuteStore1_5(ByVal storeName As String,
                                              ByVal parameters As List(Of Object)) As DataSet
            Try
                Using conMng As New ConnectionManager
                    Return OracleHelper.ExecuteDataset(conMng.GetConnectionString(),
                                                                               storeName,
                                                                               parameters.ToArray())
                End Using
            Catch ex As Exception
                Dim sb As New StringBuilder
                sb.AppendLine("{")
                If parameters Is Nothing Then
                    sb.AppendLine("Nothing")
                Else
                    For Each param As Object In parameters
                        If param Is Nothing Then
                            sb.AppendLine("Nothing,")
                        Else
                            sb.AppendLine(param.ToString() + ",")
                        End If
                    Next
                End If
                sb.AppendLine("}")
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteStore1_5", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                Throw
            End Try
        End Function
#End Region

#Region "Histaff Framework Version 1.0"
        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Out param will return in parameters
        ''' Phải truyền vào List gồm Name và Value của Parameter
        ''' Không dùng Cache
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks></remarks>
        Public Function ExecuteStore(ByVal storeName As String, ByRef parameters As List(Of ArrayList)) As DataSet
            Dim strPosOut As String = ""
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(storeName, conn)
                            'Open connection
                            conn.Open()

                            cmd.CommandType = CommandType.StoredProcedure

                            'Add parameter
                            If parameters IsNot Nothing Then
                                Dim idx As Integer = 0

                                For i = 0 To parameters.Count - 1
                                    Dim bOut As Boolean = False
                                    Dim para = GetParameter(parameters(i)(0).ToString(), parameters(i)(1), bOut)
                                    If para IsNot Nothing Then
                                        If bOut Then
                                            strPosOut = idx.ToString + ";"
                                        End If
                                        cmd.Parameters.Add(para)
                                        idx += 1
                                    End If
                                Next
                            End If

                            Dim da As New OracleDataAdapter(cmd)
                            Dim oReturn As New DataSet()
                            Try
                                cmd.Transaction = cmd.Connection.BeginTransaction()
                                da.Fill(oReturn)

                                ' Lấy dữ liệu kiểu out để trả về
                                If strPosOut <> String.Empty Then
                                    strPosOut = strPosOut.Substring(0, strPosOut.Length - 1)
                                    If parameters IsNot Nothing Then
                                        For Each str As String In strPosOut.Split(CChar(";"))

                                            Dim index = Integer.Parse(str)
                                            Dim key = cmd.Parameters(index).ParameterName
                                            For i = 0 To parameters.Count - 1
                                                If parameters(i)(0) Is key Then
                                                    Select Case cmd.Parameters(index).OracleDbType
                                                        Case OracleDbType.NVarchar2, OracleDbType.Decimal
                                                            parameters(i)(1) = cmd.Parameters(index).Value
                                                        Case OracleDbType.Date
                                                            parameters(i)(1) = String.Format("{0:dd/MM/yyyy", cmd.Parameters(index).Value)
                                                    End Select
                                                    Exit For
                                                End If
                                            Next
                                        Next
                                    End If
                                End If
                                cmd.Transaction.Commit()
                                'Dispose all resource
                                da.Dispose()
                                cmd.Dispose()
                                conn.Close()
                                conn.Dispose()
                                Return oReturn
                            Catch ex As Exception
                                cmd.Transaction.Rollback()
                                Throw ex
                            End Try
                        End Using
                    End Using
                End Using

            Catch ex As Exception
                Dim sb As New StringBuilder
                sb.AppendLine("{")
                If parameters Is Nothing Then
                    sb.AppendLine("Nothing")
                Else
                    For Each param As ArrayList In parameters
                        If param Is Nothing Then
                            sb.AppendLine("Nothing,")
                        Else
                            sb.AppendLine(param(0).ToString() & "=> " & param(1).ToString())
                        End If
                    Next
                End If
                sb.AppendLine("}")

                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteStore", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                Throw
            End Try
        End Function

        ''' <summary>
        ''' NonQuery procedure có chứa tham số, bao gồm transaction (insert/update/delete)
        ''' Không dùng cache
        ''' </summary> 
        ''' <param name="storeName">StoreName procedure</param>
        ''' <param name="parameters">Mảng các tham số truyền vào</param>
        ''' <returns>Dữ liệu trả về</returns>
        Public Function ExecuteStoreProcedureNonQuery(ByVal storeName As String, ByVal parameters As List(Of ArrayList)) As Integer
            Dim strPosOut As String = ""
            Try
                Using conMng As New ConnectionManager
                    Using conn As New OracleConnection(conMng.GetConnectionString())
                        Using cmd As New OracleCommand(storeName, conn)
                            cmd.CommandType = CommandType.StoredProcedure
                            'Open connection
                            conn.Open()

                            'Add parameter
                            If parameters IsNot Nothing Then
                                For i = 0 To parameters.Count - 1
                                    Dim para = GetParameter(parameters(i)(0).ToString(), parameters(i)(1), False)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
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

                            Return (-1) * re
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Dim sb As New StringBuilder
                sb.AppendLine("{")
                If parameters Is Nothing Then
                    sb.AppendLine("Nothing")
                Else
                    For Each param As ArrayList In parameters
                        If param Is Nothing Then
                            sb.AppendLine("Nothing,")
                        Else
                            sb.AppendLine(param(0).ToString() & "=> " & param(1).ToString())
                        End If
                    Next
                End If
                sb.AppendLine("}")
                Utility.WriteExceptionLog(ex, Me.ToString() & ".DataAccess => ExecuteStoreNonQuery", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                Throw
            End Try

        End Function
#End Region

    End Class
End Namespace