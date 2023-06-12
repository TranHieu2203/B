Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Serialization

<Serializable()> _
Public Class SerializableDictionary(Of TKey, TVal)
    Inherits Dictionary(Of TKey, TVal)
    Implements IXmlSerializable
    Implements ISerializable
#Region "Constants"
    Private Const DictionaryNodeName As String = "Dictionary"
    Private Const ItemNodeName As String = "Item"
    Private Const KeyNodeName As String = "Key"
    Private Const ValueNodeName As String = "Value"
#End Region
#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal dictionary As IDictionary(Of TKey, TVal))
        MyBase.New(dictionary)
    End Sub

    Public Sub New(ByVal comparer As IEqualityComparer(Of TKey))
        MyBase.New(comparer)
    End Sub

    Public Sub New(ByVal capacity As Integer)
        MyBase.New(capacity)
    End Sub

    Public Sub New(ByVal dictionary As IDictionary(Of TKey, TVal), ByVal comparer As IEqualityComparer(Of TKey))
        MyBase.New(dictionary, comparer)
    End Sub

    Public Sub New(ByVal capacity As Integer, ByVal comparer As IEqualityComparer(Of TKey))
        MyBase.New(capacity, comparer)
    End Sub

#End Region
#Region "ISerializable Members"

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        Dim itemCount As Integer = info.GetInt32("ItemCount")
        For i As Integer = 0 To itemCount - 1
            Dim kvp As KeyValuePair(Of TKey, TVal) = DirectCast(info.GetValue([String].Format("Item{0}", i), GetType(KeyValuePair(Of TKey, TVal))), KeyValuePair(Of TKey, TVal))
            Me.Add(kvp.Key, kvp.Value)
        Next
    End Sub



    Private Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
        info.AddValue("ItemCount", Me.Count)
        Dim itemIdx As Integer = 0
        For Each kvp As KeyValuePair(Of TKey, TVal) In Me
            info.AddValue([String].Format("Item{0}", itemIdx), kvp, GetType(KeyValuePair(Of TKey, TVal)))
            itemIdx += 1
        Next
    End Sub

#End Region
#Region "IXmlSerializable Members"

    Private Sub WriteXml(ByVal writer As Xml.XmlWriter) Implements IXmlSerializable.WriteXml
        'writer.WriteStartElement(DictionaryNodeName);
        For Each kvp As KeyValuePair(Of TKey, TVal) In Me
            writer.WriteStartElement(ItemNodeName)
            writer.WriteStartElement(KeyNodeName)
            KeySerializer.Serialize(writer, kvp.Key)
            writer.WriteEndElement()
            writer.WriteStartElement(ValueNodeName)
            If Not IsDBNull(kvp.Value) Then
                ValueSerializer.Serialize(writer, kvp.Value)
            Else
                ValueSerializer.Serialize(writer, "null")
            End If

            writer.WriteEndElement()
            writer.WriteEndElement()
        Next
        'writer.WriteEndElement();
    End Sub

    Private Sub ReadXml(ByVal reader As Xml.XmlReader) Implements IXmlSerializable.ReadXml
        If reader.IsEmptyElement Then
            Return
        End If

        ' Move past container
        If Not reader.Read() Then
            Throw New XmlException("Error in Deserialization of Dictionary")
        End If

        'reader.ReadStartElement(DictionaryNodeName);
        While reader.NodeType <> XmlNodeType.EndElement
            reader.ReadStartElement(ItemNodeName)
            reader.ReadStartElement(KeyNodeName)
            Dim key As TKey = DirectCast(KeySerializer.Deserialize(reader), TKey)
            reader.ReadEndElement()
            reader.ReadStartElement(ValueNodeName)
            Dim value As TVal = DirectCast(ValueSerializer.Deserialize(reader), TVal)
            reader.ReadEndElement()
            reader.ReadEndElement()
            Me.Add(key, value)
            reader.MoveToContent()
        End While
        'reader.ReadEndElement();

        reader.ReadEndElement()
        ' Read End Element to close Read of containing node
    End Sub

    Private Function GetSchema() As Xml.Schema.XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function

#End Region
#Region "Private Properties"
    Protected ReadOnly Property ValueSerializer() As XmlSerializer
        Get
            If m_valueSerializer Is Nothing Then
                m_valueSerializer = New XmlSerializer(GetType(TVal))
            End If
            Return m_valueSerializer
        End Get
    End Property

    Private ReadOnly Property KeySerializer() As XmlSerializer
        Get
            If m_keySerializer Is Nothing Then
                m_keySerializer = New XmlSerializer(GetType(TKey))
            End If
            Return m_keySerializer
        End Get
    End Property
#End Region
#Region "Private Members"
    Private m_keySerializer As XmlSerializer = Nothing
    Private m_valueSerializer As XmlSerializer = Nothing
#End Region
End Class