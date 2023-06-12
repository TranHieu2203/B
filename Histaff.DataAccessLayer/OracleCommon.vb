Option Strict On
Option Explicit On

Imports Oracle.DataAccess.Client

Namespace DataAccess

    Public Class OracleCommon
        Implements IDisposable

        Dim arrOutType As Dictionary(Of String, Integer)
        Public OUT_CURSOR As String = "OUT_CURSOR"
        Public OUT_NUMBER As String = "OUT_NUMBER"
        Public OUT_STRING As String = "OUT_STRING"
        Public OUT_DATE As String = "OUT_DATE"

        Sub New()
            arrOutType = New Dictionary(Of String, Integer)
            arrOutType.Add("OUT_CURSOR", 0)
            arrOutType.Add("OUT_NUMBER", 1)
            arrOutType.Add("OUT_STRING", 2)
            arrOutType.Add("OUT_DATE", 3)
        End Sub

        ''' <summary>
        ''' Gán type database tương ứng với value
        ''' </summary>
        ''' <param name="sKey">Tên parameter</param>
        ''' <param name="sValue">Giá trị của parameter</param>
        ''' <returns></returns>
        ''' <remarks>Framework 1.0, 1.5</remarks>
        Public Function GetParameter(ByVal sKey As String, ByVal sValue As Object, Optional ByRef bOut As Boolean = False) As OracleParameter
            Dim para As New OracleParameter
            Try
                If (sValue IsNot Nothing) AndAlso Not arrOutType.ContainsKey(sValue.ToString()) Then
                    'If sValue IsNot Nothing Then
                    ' Kiểm tra type để gán thuộc tính tương ứng
                    Select Case sValue.GetType
                        Case GetType(Boolean)
                            para.DbType = DbType.Int32
                        Case GetType(Date)
                            para.DbType = DbType.Date
                        Case GetType(DateTime)
                            para.DbType = DbType.DateTime
                        Case GetType(Double)
                            para.DbType = DbType.Double
                        Case GetType(Decimal)
                            para.DbType = DbType.Decimal
                        Case GetType(Integer)
                            para.DbType = DbType.Int32
                        Case GetType(String)
                            para.DbType = DbType.String
                        Case GetType(Byte())
                            para.DbType = DbType.Binary
                    End Select
                    'End If
                    para.ParameterName = sKey
                    para.Value = sValue
                ElseIf sValue Is DBNull.Value Then
                    para.ParameterName = sKey
                    para.Size = 8
                    para.Value = sValue
                Else
                    para.ParameterName = sKey
                    para.Direction = ParameterDirection.Output
                    Select Case arrOutType(CStr(sValue))
                        Case 0
                            para.OracleDbType = OracleDbType.RefCursor
                        Case 1
                            bOut = True
                            para.OracleDbType = OracleDbType.Decimal
                        Case 2
                            bOut = True
                            para.OracleDbType = OracleDbType.NVarchar2
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
