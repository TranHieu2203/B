Imports System.IO
Imports System.Xml.Serialization

Public Class RdlGenerator
    Private m_allFields As List(Of String)
    Private m_selectedFields As List(Of String)
    Private m_selectedFieldTypes As List(Of String)
    Private m_FieldTexts As New Dictionary(Of String, String)

    Public Property AllFields() As List(Of String)
        Get
            Return m_allFields
        End Get
        Set(ByVal value As List(Of String))
            m_allFields = value
        End Set
    End Property

    Public Property SelectedFields() As List(Of String)
        Get
            Return m_selectedFields
        End Get
        Set(ByVal value As List(Of String))
            m_selectedFields = value
        End Set
    End Property

    Public Property SelectedFieldTypes() As List(Of String)
        Get
            Return m_selectedFieldTypes
        End Get
        Set(ByVal value As List(Of String))
            m_selectedFieldTypes = value
        End Set
    End Property

    Public Property FieldTexts() As Dictionary(Of String, String)
        Get
            Return m_FieldTexts
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            m_FieldTexts = value
        End Set
    End Property

    Private Function CreateReport() As Rdl.Report
        Dim report As New Rdl.Report()
        report.Items = New Object() {CreateDataSources(), CreateBody(), CreateDataSets(), "6.5in"}
        report.ItemsElementName = New Rdl.ItemsChoiceType37() {Rdl.ItemsChoiceType37.DataSources, Rdl.ItemsChoiceType37.Body, Rdl.ItemsChoiceType37.DataSets, Rdl.ItemsChoiceType37.Width}
        Return report
    End Function

    Private Function CreateDataSources() As Rdl.DataSourcesType
        Dim dataSources As New Rdl.DataSourcesType()
        dataSources.DataSource = New Rdl.DataSourceType() {CreateDataSource()}
        Return dataSources
    End Function

    Private Function CreateDataSource() As Rdl.DataSourceType
        Dim dataSource As New Rdl.DataSourceType()
        dataSource.Name = "DummyDataSource"
        dataSource.Items = New Object() {CreateConnectionProperties()}
        Return dataSource
    End Function

    Private Function CreateConnectionProperties() As Rdl.ConnectionPropertiesType
        Dim connectionProperties As New Rdl.ConnectionPropertiesType()
        connectionProperties.Items = New Object() {"", "SQL"}
        connectionProperties.ItemsElementName = New Rdl.ItemsChoiceType() {Rdl.ItemsChoiceType.ConnectString, Rdl.ItemsChoiceType.DataProvider}
        Return connectionProperties
    End Function

    Private Function CreateBody() As Rdl.BodyType
        Dim style As New Rdl.StyleType
        style.Items = New Object() {"2in"}
        style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontSize}
        Dim body As New Rdl.BodyType()
        body.Items = New Object() {CreateReportItems(), "1in", style}
        body.ItemsElementName = New Rdl.ItemsChoiceType30() {Rdl.ItemsChoiceType30.ReportItems, Rdl.ItemsChoiceType30.Height, Rdl.ItemsChoiceType30.Style}
        Return body
    End Function

    Private Function CreateReportItems() As Rdl.ReportItemsType
        Dim reportItems As New Rdl.ReportItemsType()
        Dim tableGen As New TableRdlGenerator()
        tableGen.Fields = m_selectedFields
        tableGen.FieldTypes = m_selectedFieldTypes
        tableGen.FieldTexts = m_FieldTexts
        reportItems.Items = New Object() {tableGen.CreateTable()}
        Return reportItems
    End Function

    Private Function CreateDataSets() As Rdl.DataSetsType
        Dim dataSets As New Rdl.DataSetsType()
        dataSets.DataSet = New Rdl.DataSetType() {CreateDataSet()}
        Return dataSets
    End Function

    Private Function CreateDataSet() As Rdl.DataSetType
        Dim dataSet As New Rdl.DataSetType()
        dataSet.Name = "MyData"
        dataSet.Items = New Object() {CreateQuery(), CreateFields()}
        Return dataSet
    End Function

    Private Function CreateQuery() As Rdl.QueryType
        Dim query As New Rdl.QueryType()
        query.Items = New Object() {"DummyDataSource", ""}
        query.ItemsElementName = New Rdl.ItemsChoiceType2() {Rdl.ItemsChoiceType2.DataSourceName, Rdl.ItemsChoiceType2.CommandText}
        Return query
    End Function

    Private Function CreateFields() As Rdl.FieldsType
        Dim fields As New Rdl.FieldsType()
        fields.Field = New Rdl.FieldType(m_allFields.Count) {}
        Dim i As Integer
        For i = 0 To m_allFields.Count - 1
            fields.Field(i) = CreateField(m_allFields(i))
        Next i
        Return fields
    End Function

    Private Function CreateField(ByVal fieldName As String) As Rdl.FieldType
        Dim field As New Rdl.FieldType()
        field.Name = fieldName
        field.Items = New Object() {fieldName}
        field.ItemsElementName = New Rdl.ItemsChoiceType1() {Rdl.ItemsChoiceType1.DataField}
        Return field
    End Function

    Public Sub WriteXml(ByVal stream As Stream)
        Dim serializer As New XmlSerializer(GetType(Rdl.Report))
        serializer.Serialize(stream, CreateReport())
    End Sub
End Class