Imports Oracle.DataAccess.Client

Namespace DataAccess

    Partial Public Class QueryData
        Inherits OracleCommon

        ''' <summary>
        ''' Update/Insert 1 DataTable lên database 
        ''' có transaction
        ''' </summary>
        ''' <param name="storeProcedure"></param>
        ''' <param name="dtb">Datatable chứa dữ liệu update/insert, Tên Cột là tên tham số, giá trị từng Cell trên 1 row là value tương ứng</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteBatchCommand(ByVal storeProcedure As String, ByVal dtb As DataTable) As Integer
            Using cmd As New OracleCommand(storeProcedure)
                Try
                    Using conMng As New ConnectionManager
                        Using conn As New OracleConnection(conMng.GetConnectionString())
                            'cmd.CommandText = storeProcedure
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Connection = conn
                            'Open connection
                            conn.Open()

                            Dim re As Integer = 0

                            'Duyệt từng row để update
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each dr As DataRow In dtb.Rows
                                cmd.Parameters.Clear()
                                'Truyền parameter (Tên cột là tên tham số, từng Cell là Value
                                For index = 0 To dtb.Columns.Count - 1 'Lấy parameter name
                                    Dim para = GetParameter(dtb.Columns(index).ColumnName, dr(index), False)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next

                                re = re + cmd.ExecuteNonQuery()
                            Next

                            If (-1) * re = dtb.Rows.Count Then
                                cmd.Transaction.Commit()
                            Else
                                cmd.Transaction.Rollback()
                            End If

                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                            Return re * (-1)
                        End Using
                    End Using

                Catch ex As Exception
                    cmd.Transaction.Rollback()
                    cmd.Dispose()
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Out param will return in parameters
        ''' Phải truyền vào List gồm Name và Value của Parameter
        ''' Không dùng Cache
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks></remarks>
        Public Function ExecuteStoreProcedure(ByVal storeName As String, ByRef parameters As List(Of ArrayList)) As DataSet
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
                                    Dim para = GetParameter(parameters(i)(0), parameters(i)(1), bOut)
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
                                        For Each str As String In strPosOut.Split(";")

                                            Dim index = Integer.Parse(str)
                                            Dim key = cmd.Parameters(index).ParameterName
                                            For i = 0 To parameters.Count - 1
                                                If parameters(i)(0) = key Then
                                                    Select Case cmd.Parameters(index).OracleDbType
                                                        Case OracleDbType.NVarchar2, OracleDbType.Decimal
                                                            parameters(i)(1) = cmd.Parameters(index).Value
                                                        Case OracleDbType.Date
                                                            parameters(i)(1) = cmd.Parameters(index).Value.ToString("dd/MM/yyyy")
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

                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Execute Store Procedure chỉ truyền vào List Value của Parameters
        ''' Không dùng cache
        ''' </summary>
        ''' <param name="storeName"></param>
        ''' <param name="parameters"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteStoreProcedure(ByVal storeName As String,
                                              ByVal parameters As List(Of Object)) As DataSet
            Try
                Using conMng As New ConnectionManager
                    Return Histaff.DataAccessLayer.OracleHelper.ExecuteDataset(conMng.GetConnectionString(),
                                                                               storeName,
                                                                               parameters.ToArray())
                End Using
            Catch ex As Exception
                Throw ex
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
                                    Dim para = GetParameter(parameters(i)(0), parameters(i)(1), False)
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

                            Return re
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        ''' <summary>
        ''' Execute Store Insert, Update, Delete bằng Store Procedure
        ''' Có dùng Cache
        ''' </summary>
        ''' <param name="storeName"></param>
        ''' <param name="parameterNames"></param>
        ''' <param name="parameterValues"></param>
        ''' <param name="parameterTypes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteStoreProcedureNonQuery(ByVal storeName As String,
                                                      ByVal parameterNames As List(Of Object),
                                                      ByVal parameterValues As List(Of Object),
                                                      ByVal parameterTypes As List(Of Object)) As Integer
            Try
                Using conMng As New ConnectionManager
                    Return Histaff.DataAccessLayer.OracleHelper.ExecuteNonQuery(conMng.GetConnectionString(),
                                                                                storeName,
                                                                                parameterNames.ToArray(),
                                                                                parameterValues.ToArray(),
                                                                                parameterTypes.ToArray())
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Thực thi store trả về mảng cái value OUTPUT
        ''' Có dùng Cache
        ''' </summary>
        ''' <param name="storeName">Tên store procedure</param>
        ''' <param name="parameters">Mảng Value của các tham số</param>
        ''' <returns>Mảng các giá trị output</returns>
        ''' <remarks></remarks>
        Public Function ExecuteStoreScalar(ByVal storeName As String,
                                           ByVal parameters As List(Of Object)) As Object()
            Try
                Using conMng As New ConnectionManager

                    Return Histaff.DataAccessLayer.OracleHelper.ExecuteScalar(conMng.GetConnectionString(),
                                                                              storeName,
                                                                              parameters.ToArray())

                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

End Namespace