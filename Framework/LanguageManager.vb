Imports System.Globalization
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Xml.Serialization

Public Class LanguageManager
    Implements IDisposable
    ' To detect redundant calls
    Private disposed As Boolean = False
    Private _culture As CultureInfo
    Private _languagePath As String = "/Languages/"
    Private _CachePrefix As String = "DICT-"
    Private _CacheMinute As Integer = 10
    Private _dictionary As SerializableDictionary(Of String, String)

    Public ReadOnly Property Dictionary As SerializableDictionary(Of String, String)
        Get
            Return _dictionary
        End Get
    End Property
    Public Function HasNumber(ByVal input As String) As Boolean
        Return Regex.IsMatch(input, "\d")
    End Function


    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String)
        Try
            str = str.Trim
            'If str = "2. Danh sách cột dữ liệu" Then
            '    Dim a As String = "a"
            'End If
            Dim regex As String = "^[\w]*([,.][\w]*)?$"
            If _dictionary IsNot Nothing Then
                If _dictionary.ContainsKey(str.ToUpper) Then
                    Dim strFormat As String = _dictionary(str.ToUpper)
                    If strFormat.ToString.Contains("..") Or HasNumber(strFormat) Then
                        GoTo ab
                    End If
                    If strFormat.ToString.Contains(".") Then
                        strFormat = strFormat.ToString.Replace(".", ".<br/>")
                    End If
                    If strFormat.ToString.Contains("!") Then
                        strFormat = strFormat.ToString.Replace("!", "!<br/>")
                    End If
ab:
                    If args.Count = 0 Then
                        Return strFormat
                    End If
                    Return String.Format(strFormat, args)
                Else
                    _dictionary.Add(str.ToUpper, str)
                    CacheManager.Insert(_CachePrefix & _culture.Name, _dictionary, 10)
                End If
            End If
            Return String.Format(str, args)
        Catch ex As Exception
            Throw ex
        End Try
        Return str
    End Function

    Public Sub New()
        If HttpContext.Current.Session("SystemLanguage") Is Nothing Then
            HttpContext.Current.Session("SystemLanguage") = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN")
        End If
        Dim culture = HttpContext.Current.Session("SystemLanguage")
        Initialize(culture)
    End Sub

    Public Sub New(ByVal culture As CultureInfo)
        Initialize(culture)
    End Sub

    Private Sub Initialize(ByVal culture As CultureInfo)
        Try
            _culture = culture
            If CacheManager.GetValue(_CachePrefix & culture.Name) Is Nothing Then
                _dictionary = New SerializableDictionary(Of String, String)
                DeserializeFromFile()
                CacheManager.Insert(_CachePrefix & culture.Name, _dictionary, _CacheMinute)
            Else
                _dictionary = CacheManager.GetValue(_CachePrefix & culture.Name)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Property Item(ByVal key As String) As String
        Get
            Return _dictionary(key)
        End Get
        Set(ByVal value As String)
            _dictionary(key) = value
        End Set
    End Property

    Public Function RemoveItem(ByVal key As String) As Boolean
        _dictionary.Remove(key)
        Return True
    End Function


    Public Function SerializeToString() As String
        Dim strRet As String = ""
        Dim writer As New StringWriter
        Try
            Dim x As New System.Xml.Serialization.XmlSerializer(_dictionary.GetType)
            x.Serialize(writer, _dictionary)
            strRet = writer.ToString
        Catch ex As Exception
            Throw ex
        Finally
            writer.Close()
        End Try
        Return strRet
    End Function

    Public Sub SerializeToFile()
        Using writer As TextWriter = New StreamWriter(HttpContext.Current.Server.MapPath(_languagePath & _culture.Name & ".xml"))
            Dim bRet As Boolean = True
            Try
                Dim x As New System.Xml.Serialization.XmlSerializer(_dictionary.GetType)
                x.Serialize(writer, _dictionary)
            Catch ex As Exception
                writer.Close()
                writer.Dispose()
                Throw ex
            Finally
                writer.Close()
            End Try
        End Using
    End Sub

    Public Sub DeserializeFromFile()
        Using reader As TextReader = New StreamReader(HttpContext.Current.Server.MapPath(_languagePath & _culture.Name & ".xml"))
            Try

                Dim x As New XmlSerializer(_dictionary.GetType)
                _dictionary = x.Deserialize(reader)
            Catch ex As Exception
                reader.Close()
                reader.Dispose()
                'Throw ex
            Finally
                If reader IsNot Nothing Then reader.Close()
            End Try
        End Using

    End Sub

    ' IDisposable
    Protected Overridable Sub Dispose( _
       ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Free other state (managed objects).
            End If
            ' Free your own state (unmanaged objects).
            ' Set large fields to null.
        End If
        Me.disposed = True
    End Sub

    ' This code added by Visual Basic to 
    ' correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

End Class


