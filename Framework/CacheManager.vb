Imports System.Web

Public Class CacheManager
    Public Shared Sub Insert(ByVal Key As String, ByVal Value As Object, ByVal CacheMinutes As Double)
        If HttpContext.Current.Cache(Key) IsNot Nothing Then
            HttpContext.Current.Cache.Remove(Key)
        End If
        If Value IsNot Nothing Then
            HttpContext.Current.Cache.Insert(Key, Value, Nothing, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero)
        End If
    End Sub

    Public Shared Function GetValue(ByVal Key As String) As Object
        Return HttpContext.Current.Cache(Key)

    End Function

    Public Shared Function ClearValue(ByVal Key As String) As Object
        Return HttpContext.Current.Cache.Remove(Key)
    End Function

    Public Shared Sub ClearAll()
        Dim dict As IDictionaryEnumerator = HttpContext.Current.Cache.GetEnumerator
        While dict.MoveNext
            HttpContext.Current.Cache.Remove(dict.Key)
        End While
    End Sub
    Public Shared Function GetAll() As DataTable
        Dim dict As IDictionaryEnumerator = HttpContext.Current.Cache.GetEnumerator
        Dim dtdata As New DataTable
        dtdata.Columns.Add("Key")
        While dict.MoveNext
            dtdata.Rows.Add(dict.Key.ToString)
        End While
        Return dtdata
    End Function

End Class