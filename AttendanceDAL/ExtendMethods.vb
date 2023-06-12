Public Module ExtendMethods
    <System.Runtime.CompilerServices.Extension()> _
    Public Function ReplaceField(ByVal inputString As String, ByVal listValue As Dictionary(Of String, String)) As String
        Dim outputString As String = inputString

        For Each item As KeyValuePair(Of String, String) In listValue
            outputString = outputString.Replace(item.Key, item.Value)
        Next

        Return outputString
    End Function
End Module
