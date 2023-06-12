Imports Oracle.DataAccess.Client
Namespace DataAccess
    Public Class OracleCommon
        Implements IDisposable

        ''' <summary>
        ''' Gán type database tương ứng với value
        ''' </summary>
        ''' <param name="sKey">Tên parameter</param>
        ''' <param name="sValue">Giá trị của parameter</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParameter(ByVal sKey As String, ByVal sValue As Object, Optional ByRef bOut As Boolean = False) As OracleParameter
            Dim para As New OracleParameter
            Try
                If sValue Is Nothing Or ((sValue IsNot Nothing And sValue IsNot DBNull.Value) AndAlso Not arrOutType.ContainsKey(sValue)) Then
                    'If sValue Is Nothing Or ((sValue IsNot Nothing) AndAlso Not arrOutType.ContainsKey(sValue)) Then
                    If sValue IsNot Nothing Then
                        ' Kiểm tra type để gán thuộc tính tương ứng
                        Select Case sValue.GetType
                            Case GetType(Boolean)
                                para.DbType = DbType.Int16
                                If sValue Then
                                    para.Value = -1
                                Else
                                    para.Value = 0
                                End If
                            Case GetType(Date)
                                para.DbType = DbType.Date
                                para.Value = sValue
                            Case GetType(DateTime)
                                para.DbType = DbType.DateTime
                                para.Value = sValue
                            Case GetType(Double)
                                para.DbType = DbType.Double
                                para.Value = sValue
                            Case GetType(Decimal)
                                para.DbType = DbType.Decimal
                                para.Value = sValue
                            Case GetType(Integer)
                                para.DbType = DbType.Int32
                                para.Value = sValue
                            Case GetType(String)
                                If sValue.ToString.Length > 225 Then
                                    para.OracleDbType = OracleDbType.Clob
                                Else
                                    para.DbType = DbType.String
                                End If

                                para.Value = sValue
                            Case GetType(Byte())
                                para.DbType = DbType.Binary
                                If sValue Is DBNull.Value Then
                                    para.Size = 8
                                End If
                                para.Value = sValue
                        End Select
                    End If
                    para.ParameterName = sKey
                ElseIf sValue Is DBNull.Value Then
                    para.ParameterName = sKey
                    para.Size = 8
                    para.Value = sValue
                Else
                    para.ParameterName = sKey
                    para.Direction = ParameterDirection.Output
                    Select Case arrOutType(sValue)
                        Case 0
                            para.OracleDbType = OracleDbType.RefCursor
                        Case 1
                            bOut = True
                            para.OracleDbType = OracleDbType.Decimal
                        Case 2
                            bOut = True
                            para.OracleDbType = OracleDbType.NVarchar2
                            para.Size = 2000
                        Case 3
                            bOut = True
                            para.OracleDbType = OracleDbType.Date
                    End Select
                End If
                Return para
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Sub New()
            arrOutType = New Dictionary(Of String, Integer)
            arrOutType.Add("OUT_CURSOR", 0)
            arrOutType.Add("OUT_NUMBER", 1)
            arrOutType.Add("OUT_STRING", 2)
            arrOutType.Add("OUT_DATE", 3)
        End Sub
        Dim arrOutType As Dictionary(Of String, Integer)
        Public OUT_CURSOR As String = "OUT_CURSOR"
        Public OUT_NUMBER As String = "OUT_NUMBER"
        Public OUT_STRING As String = "OUT_STRING"
        Public OUT_DATE As String = "OUT_DATE"

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


End Namespace
