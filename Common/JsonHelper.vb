Imports System.IO
Imports System.Runtime.Serialization.json
''' <summary>
''' JSON Serialization and Deserialization Assistant Class
''' </summary>
Public Class JsonHelper
    ''' <summary>
    ''' JSON Serialization
    ''' </summary>
    Public Shared Function JsonSerializer(Of T)(obj As T) As String
        Dim ser As New DataContractJsonSerializer(GetType(T))
        Dim ms As New MemoryStream()
        ser.WriteObject(ms, obj)
        Dim jsonString As String = Encoding.UTF8.GetString(ms.ToArray())
        ms.Close()
        Return jsonString
    End Function
    ''' <summary>
    ''' JSON Deserialization
    ''' </summary>
    Public Shared Function JsonDeserialize(Of T)(jsonString As String) As T
        Dim ser As New DataContractJsonSerializer(GetType(T))
        Dim ms As New MemoryStream(Encoding.UTF8.GetBytes(jsonString))
        Dim obj As T = DirectCast(ser.ReadObject(ms), T)
        Return obj
    End Function
End Class
