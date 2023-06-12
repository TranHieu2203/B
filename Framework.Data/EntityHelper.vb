Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Xml
Imports System.Xml.Serialization

Public Class EntityHelper
    Implements IDisposable

    Public Function SetProperty(ByVal Name As String, ByVal Value As Object, ByRef Entity As Object) As Boolean

        Try
            Dim infos As PropertyInfo() = Entity.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanWrite And p.Name = Name Select p).SingleOrDefault
            If item IsNot Nothing Then
                item.SetValue(Entity, Value, Nothing)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False

    End Function



    Public Function GetProperty(ByVal Entity As Object, ByVal Name As String) As Object
        Try
            Dim infos As PropertyInfo() = Entity.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanRead And p.Name = Name Select p).SingleOrDefault
            If item IsNot Nothing Then
                Return item.GetValue(Entity, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function


    Public Function XmlSerialize(ByVal obj As Object) As String
        If obj IsNot Nothing Then
            ' Assuming obj is an instance of an object
            Dim ser As New XmlSerializer(obj.[GetType]())
            Dim sb As New Text.StringBuilder()
            Dim writer As New IO.StringWriter(sb)
            ser.Serialize(writer, obj)
            Return sb.ToString()
        End If
        Return String.Empty

    End Function

    Public Function XmlDeserialize(ByVal objType As Type, ByVal xmlDoc As String) As Object
        If xmlDoc IsNot Nothing AndAlso objType IsNot Nothing Then
            Dim doc As New XmlDocument()
            doc.LoadXml(xmlDoc)
            'Assuming doc is an XML document containing a serialized object and objType is a System.Type set to the type of the object.
            Dim reader As New XmlNodeReader(doc.DocumentElement)
            Dim ser As New XmlSerializer(objType)
            Return ser.Deserialize(reader)
        End If
        Return Nothing
    End Function

    Public Function GetEntryValueInString(ByVal entry As ObjectStateEntry) As List(Of AuditLogDtl)
        Dim dict As New List(Of AuditLogDtl)
        Dim setterValue As Object = Nothing
        If TypeOf entry.Entity Is EntityObject Then
            Dim strXml As String = ""
            If entry.State = EntityState.Added Then
                For Each r In entry.CurrentValues.DataRecordInfo.FieldMetadata
                    setterValue = IIf(IsDBNull(entry.CurrentValues(r.FieldType.Name)), "", entry.CurrentValues(r.FieldType.Name))
                    dict.Add(New AuditLogDtl With
                             {.COL_NAME = r.FieldType.Name,
                              .OLD_VALUE = "",
                              .NEW_VALUE = setterValue})
                Next
            ElseIf entry.State = EntityState.Modified Then
                For Each propName As String In entry.GetModifiedProperties()
                    Dim setterOldValue As Object = Nothing
                    Dim setterNewValue As Object = Nothing
                    setterOldValue = IIf(IsDBNull(entry.OriginalValues(propName)), "", entry.OriginalValues(propName))
                    setterNewValue = IIf(IsDBNull(entry.CurrentValues(propName)), "", entry.CurrentValues(propName))
                    If Not setterOldValue.Equals(setterNewValue) Then
                        dict.Add(New AuditLogDtl With
                                 {.COL_NAME = propName,
                                  .OLD_VALUE = setterOldValue,
                                  .NEW_VALUE = setterNewValue})

                    End If
                Next
            ElseIf entry.State = EntityState.Deleted Then
                For Each propName As String In entry.GetModifiedProperties()
                    setterValue = IIf(IsDBNull(entry.OriginalValues(propName)), "", entry.OriginalValues(propName))
                    dict.Add(New AuditLogDtl With
                             {.COL_NAME = propName,
                              .OLD_VALUE = setterValue,
                              .NEW_VALUE = ""})
                Next
            End If
            Return dict
        End If
        Return New List(Of AuditLogDtl)
    End Function

    Public Function GetEntryValueInString(ByVal entry As Entity.Infrastructure.DbEntityEntry) As List(Of AuditLogDtl)
        Dim dict As New List(Of AuditLogDtl)
        Dim setterValue As Object = Nothing
        Dim strXml As String = ""
        If entry.State = EntityState.Added Then
            ' For Each r In entry.CurrentValues.DataRecordInfo.FieldMetadata
            For Each propertyName In entry.CurrentValues.PropertyNames
                setterValue = IIf(IsDBNull(entry.CurrentValues(propertyName)), "", entry.CurrentValues(propertyName))
                dict.Add(New AuditLogDtl With
                             {.COL_NAME = propertyName,
                              .OLD_VALUE = "",
                              .NEW_VALUE = setterValue})
            Next
            'entry.CurrentValues.PropertyNames.Where(propertyName => entry.Property(propertyName).IsModified).ToList()
        ElseIf entry.State = EntityState.Modified Then
            For Each propName As String In entry.CurrentValues.PropertyNames.Where(Function(f) entry.Property(f).IsModified)
                Dim setterOldValue As Object = Nothing
                Dim setterNewValue As Object = Nothing
                setterOldValue = IIf(IsDBNull(entry.OriginalValues(propName)), "", entry.OriginalValues(propName))
                setterNewValue = IIf(IsDBNull(entry.CurrentValues(propName)), "", entry.CurrentValues(propName))
                If Not setterOldValue.Equals(setterNewValue) Then
                    dict.Add(New AuditLogDtl With
                                 {.COL_NAME = propName,
                                  .OLD_VALUE = setterOldValue,
                                  .NEW_VALUE = setterNewValue})

                End If
            Next
        ElseIf entry.State = EntityState.Deleted Then
            For Each propName As String In entry.CurrentValues.PropertyNames.Where(Function(f) entry.Property(f).IsModified)
                setterValue = IIf(IsDBNull(entry.OriginalValues(propName)), "", entry.OriginalValues(propName))
                dict.Add(New AuditLogDtl With
                             {.COL_NAME = propName,
                              .OLD_VALUE = setterValue,
                              .NEW_VALUE = ""})
            Next
        End If
        Return dict
        Return New List(Of AuditLogDtl)
    End Function

    Public Function CloneEntity(ByVal obj As EntityObject) As EntityObject
        Dim Type = obj.[GetType]()
        Dim Clone = Activator.CreateInstance(Type)

        For Each [Property] In Type.GetProperties(BindingFlags.GetProperty _
                                                  Or BindingFlags.[Public] _
                                                  Or BindingFlags.Instance _
                                                  Or BindingFlags.DeclaredOnly _
                                                  Or BindingFlags.SetProperty)
            If [Property].PropertyType.IsGenericType _
                AndAlso [Property].PropertyType.GetGenericTypeDefinition() = GetType(EntityReference(Of )) Then
                Continue For
            End If
            If [Property].PropertyType.IsGenericType AndAlso [Property].PropertyType.GetGenericTypeDefinition() = GetType(EntityCollection(Of )) Then
                Continue For
            End If
            If [Property].PropertyType.IsSubclassOf(GetType(EntityObject)) Then
                Continue For
            End If

            If [Property].CanWrite Then
                [Property].SetValue(Clone, [Property].GetValue(obj, Nothing), Nothing)
            End If
        Next

        Return DirectCast(Clone, EntityObject)

    End Function

    Public Function CreateTypeBuilder(ByVal assemblyName As String, ByVal moduleName As String, ByVal typeName As String) As TypeBuilder
        Dim typeBuilder As TypeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(New AssemblyName(assemblyName), AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName, TypeAttributes.[Public])
        typeBuilder.DefineDefaultConstructor(MethodAttributes.[Public])
        Return typeBuilder
    End Function

    Public Sub CreateAutoImplementedProperty(ByVal builder As TypeBuilder, ByVal propertyName As String, ByVal propertyType As Type)
        Const PrivateFieldPrefix As String = "m_"
        Const GetterPrefix As String = "get_"
        Const SetterPrefix As String = "set_"

        ' Generate the field.
        Dim fieldBuilder As FieldBuilder = builder.DefineField(String.Concat(PrivateFieldPrefix, propertyName), propertyType, FieldAttributes.[Private])

        ' Generate the property
        Dim propertyBuilder As PropertyBuilder = builder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, Nothing)

        ' Property getter and setter attributes.
        Dim propertyMethodAttributes As MethodAttributes = MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig

        ' Define the getter method.
        Dim getterMethod As MethodBuilder = builder.DefineMethod(String.Concat(GetterPrefix, propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes)

        ' Emit the IL code.
        ' ldarg.0
        ' ldfld,_field
        ' ret
        Dim getterILCode As ILGenerator = getterMethod.GetILGenerator()
        getterILCode.Emit(OpCodes.Ldarg_0)
        getterILCode.Emit(OpCodes.Ldfld, fieldBuilder)
        getterILCode.Emit(OpCodes.Ret)

        ' Define the setter method.
        Dim setterMethod As MethodBuilder = builder.DefineMethod(String.Concat(SetterPrefix, propertyName), propertyMethodAttributes, Nothing, New Type() {propertyType})

        ' Emit the IL code.
        ' ldarg.0
        ' ldarg.1
        ' stfld,_field
        ' ret
        Dim setterILCode As ILGenerator = setterMethod.GetILGenerator()
        setterILCode.Emit(OpCodes.Ldarg_0)
        setterILCode.Emit(OpCodes.Ldarg_1)
        setterILCode.Emit(OpCodes.Stfld, fieldBuilder)
        setterILCode.Emit(OpCodes.Ret)

        propertyBuilder.SetGetMethod(getterMethod)
        propertyBuilder.SetSetMethod(setterMethod)
    End Sub

    Public Function CreateType() As Type
        Dim builder As TypeBuilder = CreateTypeBuilder("MyDynamicAssembly", "MyModule", "MyType")
        CreateAutoImplementedProperty(builder, "Name", GetType(String))
        CreateAutoImplementedProperty(builder, "code", GetType(String))
        CreateAutoImplementedProperty(builder, "id", GetType(Byte()))

        Dim resultType As Type = builder.CreateType()
        Return resultType
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
