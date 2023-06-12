Option Strict On
Option Explicit On

Imports System.Text
Imports Histaff.DataAccessLayer
Imports HistaffFramework.ServiceContracts

Namespace HistaffFramework.ServiceImplementations

    Public Class HistaffFramework
        Implements IHistaffFramework

#Region "Histaff Framework Version 2.0"

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Get danh sách tham số của 1 StoreProcedure
        ''' </summary>
        ''' <param name="storeName">Tên store Procedure</param>
        ''' <param name="isInParameterOnly">True: Chỉ lấy danh sách tham số In, False: lấy tất cả tham số</param>
        ''' <returns>Datatable danh sách tham số</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Public Function GetSpParameterSet(ByVal storeName As String, Optional isInParameterOnly As Boolean = False) As DataTable Implements IHistaffFramework.GetSpParameterSet
            Dim obj As New DataTable
            Try
                Using da As New DataAccess.QueryData
                    obj = da.GetSpParameterSet(storeName, isInParameterOnly)
                End Using
            Catch ex As Exception
                obj = Nothing
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => GetSpParameterSet", String.Format("{0} " & vbNewLine & "isInParameterOnly: {1}", storeName, isInParameterOnly))
            End Try
            Return obj
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
        Public Function ExecuteToDataSet(ByVal storeName As String, Optional ByVal parameterValue As List(Of Object) = Nothing) As DataSet Implements IHistaffFramework.ExecuteToDataSet
            Dim obj As New DataSet
            Try
                Using da As New DataAccess.QueryData
                    obj = da.ExecuteToDataSet(storeName, parameterValue)
                End Using
            Catch ex As Exception
                obj = Nothing
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteToDataSet", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
            End Try
            Return obj
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
        Function ExecuteStoreScalar(ByVal storeName As String, ByVal parameters As List(Of Object)) As Object() Implements IHistaffFramework.ExecuteStoreScalar
            Dim i() As Object
            Try
                Using da As New DataAccess.QueryData
                    i = da.ExecuteStoreScalar(storeName, parameters)
                End Using

                If i.Count <= 0 OrElse i(0).ToString().Equals(String.Empty) Then
                    i = Nothing
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
                    Utility.WriteExceptionLog(New Exception("No data found"), Me.ToString() & ".HistaffFramework => ExecuteStoreScalar", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                End If
            Catch ex As Exception
                i = Nothing
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteStoreScalar", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
            End Try
            Return i
        End Function

        ''' <summary>
        ''' Update/Insert 1 DataTable lên database Mở kết nối => duyệt từng rows cho đến khi hết row thì => đóng kết nối
        ''' Có transaction
        ''' </summary>
        ''' <param name="storeName">Tên Store Produre</param>
        ''' <param name="dtb">Datatable chứa dữ liệu update/insert, Tên Cột là tên tham số, giá trị từng Cell trên 1 row là value tương ứng</param>
        ''' <returns>Số dùng transaction thành công</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        Function ExecuteBatchCommand(ByVal storeName As String, ByVal dtb As DataTable) As Integer Implements IHistaffFramework.ExecuteBatchCommand
            Dim i As Integer
            Try
                Using da As New DataAccess.QueryData
                    i = da.ExecuteBatchCommand(storeName, dtb)
                End Using

            Catch ex As Exception
                i = 0
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteBatchCommand", String.Format("{0} " & vbNewLine & "DataTableName: {1}" & vbNewLine & "Row(s): {2}", storeName, dtb.TableName, dtb.Rows.Count))
            End Try
            Return i
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
        Public Function ExecuteStore1_5(ByVal storeName As String, ByVal parameters As List(Of Object)) As DataSet Implements IHistaffFramework.ExecuteStore1_5
            Dim obj As New DataSet
            Try
                Using da As New DataAccess.QueryData
                    obj = da.ExecuteStore1_5(storeName, parameters)
                End Using

            Catch ex As Exception
                obj = Nothing
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteStore1_5", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
            End Try
            Return obj
        End Function

        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Fix bug Out parameters in Function ExecuteStore 
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <param name="isHasOutParameter">Có tham số OUTPUT trả về hay không, Trừ OUT_CUROR </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks>Histaff Framework 1.5</remarks>
        Public Function ExecuteStoreOutParameters(ByVal storeName As String, ByVal parameters As List(Of ArrayList), Optional ByVal isHasOutParameter As Boolean = False) As DataSet Implements IHistaffFramework.ExecuteStoreOutParameters
            Dim ds As New DataSet
            Try
                Using da As New DataAccess.QueryData

                    ds = da.ExecuteStore(storeName, parameters)

                    'Lấy danh sách các parameter vào datatable (Tất nhiên trừ OUT_CURSOR, code tự control)
                    If isHasOutParameter Then
                        Dim dtb As New DataTable("parameters")
                        For Each item As ArrayList In parameters
                            dtb.Columns.Add(item(0).ToString())
                        Next
                        Dim dr As DataRow = dtb.NewRow
                        For i = 0 To dtb.Columns.Count - 1
                            dr(i) = parameters(i)(1)
                        Next
                        dtb.Rows.Add(dr)
                        ds.Tables.Add(dtb)
                    End If
                End Using
            Catch ex As Exception
                ds = Nothing
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteStoreOutParameters", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
            End Try
            Return ds
        End Function
#End Region

#Region "Histaff Framework Version 1.0"
        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Out param will return in parameters
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks>Histaff Framework 1.0</remarks>
        Public Function ExecuteStore(ByVal storeName As String, ByVal parameters As List(Of ArrayList)) As DataSet Implements IHistaffFramework.ExecuteStore
            Dim obj As New DataSet
            Try
                Using da As New DataAccess.QueryData
                    obj = da.ExecuteStore(storeName, parameters)
                End Using
            Catch ex As Exception
                obj = Nothing
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteStore", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
            End Try
            Return obj
        End Function

        ''' <summary>
        ''' Execute Store Procedure Insert/Update/Delete
        ''' </summary>
        ''' <param name="storeName">StoreName</param>
        ''' <param name="parameters">List Parameters</param>
        ''' <returns>Row(s) commited</returns>
        ''' <remarks>Histaff Framework 1.0</remarks>
        Function ExecuteStoreNonQuery(ByVal storeName As String, ByVal parameters As List(Of ArrayList)) As Integer Implements IHistaffFramework.ExecuteStoreNonQuery
            Dim i As Integer
            Try
                Using da As New DataAccess.QueryData
                    i = da.ExecuteStoreProcedureNonQuery(storeName, parameters)
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
                Utility.WriteExceptionLog(ex, Me.ToString() & ".HistaffFramework => ExecuteStoreNonQuery", String.Format("{0} " & vbNewLine & "{1}", storeName, sb.ToString()))
                i = 0
            End Try
            Return i
        End Function
#End Region

    End Class

End Namespace
