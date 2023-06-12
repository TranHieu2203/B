Option Strict On
Option Explicit On
Imports HistaffFrameworkPublic.FrameworkUtilities


Public Class EmailCommon
    Implements IDisposable

#Region "ThanhNT - Email"
    Dim rep As New HistaffFrameworkRepository
    'hàm lấy thông tin chi tiết của 1 email
    Public Function GetEmailInfo(ByVal listId As String, ByVal type As Decimal, ByVal status As Decimal, ByVal directLinkCode As String, ByVal idTypeEmail As Decimal, ByVal org As String, ByVal employee As String, ByVal approve As String) As Boolean
        Try
            Dim rs As New DataTable
            Dim body As String = ""
            Dim subject As String = ""
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_SV_EMAIL_REQUEST.READ_ALL_EMAIL_INFO", New List(Of Object)({idTypeEmail, org, employee, approve}))
            If ds Is Nothing Or ds.Tables.Count = 0 Then
                Return False
            Else
                rs = ds.Tables(0)
                subject = UpdateSubject(listId, type, status, directLinkCode, CStr(rs.Rows(0).Item("SUBJECT")))
                body = UpdateTemplate(listId, type, status, directLinkCode, CStr(rs.Rows(0).Item("HEADER")), CStr(rs.Rows(0).Item("CONTENT")), CStr(rs.Rows(0).Item("FOOTER")), CStr(rs.Rows(0).Item("SIGNATURE")))
            End If
            'insert vao table SV_EMAIL_REQUEST
            Dim obj = rep.ExecuteStoreScalar("PKG_SV_EMAIL_REQUEST.INSERT_EMAIL_REQUEST", _
                   New List(Of Object)(New Object() {idTypeEmail, subject, CStr(rs.Rows(0).Item("EMAIL_TO").ToString()), _
                                                     CStr(rs.Rows(0).Item("EMAIL_CC").ToString()), body, "", DateTime.Now, OUT_NUMBER}))
            If CDec(Decimal.Parse(obj(0).ToString)) = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'hàm xử lý thông tin cho template
    Public Function UpdateTemplate(ByVal listId As String, ByVal type As Decimal, ByVal status As Decimal, ByVal directLinkCode As String, ByVal header As String, ByVal content As String, ByVal footer As String, ByVal signature As String) As String
        Try
            'Lấy tất cả dữ liệu của thông tin trong template    
            'Dim url As Uri
            'Dim link = url.Authority
            'Dim linkUrl = "<a href=" & link
            Dim ds = rep.ExecuteToDataSet("PKG_SV_EMAIL_REQUEST.READ_ALL_INFO_IN_TEMPLATE", New List(Of Object)(New Object() {listId, type, directLinkCode, status}))
            '1 - Tiến hành cập nhật lại template theo đúng dữ liệu được lấy lên
            'Replace những thông tin trong template với dữ liệu được lấy lên            

            Dim oldStr As String = ""
            Dim newStr As String = ""
            Dim newContent As String = content
            Dim dt As New DataTable
            dt = ds.Tables(0)
            For t As Integer = 0 To dt.Rows.Count - 1  'duyet qua tung dong` du~ lieu
                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1 'duyet qua tat ca ca cot va replace
                    oldStr = ds.Tables(0).Columns(i).ColumnName.ToString
                    newStr = CStr(ds.Tables(0).Rows(t).Item(ds.Tables(0).Columns(i).ColumnName.ToString))
                    'replace header + footer 1 lan ma thoi
                    header = header.Replace(oldStr, newStr)
                    footer = footer.Replace(oldStr, newStr)
                    'replace content co the la nhieu lan
                    content = content.Replace(oldStr, newStr)
                Next
                If t < dt.Rows.Count - 1 Then
                    newContent = content & "<br/>" & newContent
                Else
                    newContent = content
                End If
            Next
            Return header & "<br/><br/>" & newContent & "<br/>" & footer & "<br/><br/>" & signature
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'hàm xử lý thông tin cho template
    Public Function UpdateSubject(ByVal listId As String, ByVal type As Decimal, ByVal status As Decimal, ByVal directLinkCode As String, ByVal subject As String) As String
        Try
            'Lấy tất cả dữ liệu của thông tin trong template    
            'Dim url As Uri
            'Dim link = url.Authority
            'Dim linkUrl = "<a href=" & link
            Dim ds = rep.ExecuteToDataSet("PKG_SV_EMAIL_REQUEST.READ_ALL_INFO_IN_TEMPLATE", New List(Of Object)(New Object() {listId, type, directLinkCode, status}))
            '1 - Tiến hành cập nhật lại template theo đúng dữ liệu được lấy lên
            'Replace những thông tin trong template với dữ liệu được lấy lên            

            Dim oldStr As String = ""
            Dim newStr As String = ""
            Dim newContent As String = subject
            Dim dt As New DataTable
            dt = ds.Tables(0)
            For t As Integer = 0 To dt.Rows.Count - 1  'duyet qua tung dong` du~ lieu
                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1 'duyet qua tat ca ca cot va replace
                    oldStr = ds.Tables(0).Columns(i).ColumnName.ToString
                    newStr = CStr(ds.Tables(0).Rows(t).Item(ds.Tables(0).Columns(i).ColumnName.ToString))
                    'replace header + footer 1 lan ma thoi
                    subject = subject.Replace(oldStr, newStr)
                Next
            Next
            Return subject
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

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
