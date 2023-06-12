Imports System.Dynamic

' The class derived from DynamicObject.
Public Class DynamicReportClass
    Inherits DynamicObject

    ' The inner dictionary.
    Dim dictionary As New Dictionary(Of String, Object)

    ' This property returns the number of elements
    ' in the inner dictionary.
    ReadOnly Property Count As Integer
        Get
            Return dictionary.Count
        End Get
    End Property


    ' If you try to get a value of a property that is
    ' not defined in the class, this method is called.

    Public Overrides Function TryGetMember(
        ByVal binder As System.Dynamic.GetMemberBinder,
        ByRef result As Object) As Boolean

        ' Converting the property name to lowercase
        ' so that property names become case-insensitive.
        Dim name As String = binder.Name.ToLower()

        ' If the property name is found in a dictionary,
        ' set the result parameter to the property value and return true.
        ' Otherwise, return false.
        Return dictionary.TryGetValue(name, result)
    End Function

    Public Overrides Function TrySetMember(
        ByVal binder As System.Dynamic.SetMemberBinder,
        ByVal value As Object) As Boolean

        ' Converting the property name to lowercase
        ' so that property names become case-insensitive.
        dictionary(binder.Name.ToLower()) = value

        ' You can always add a value to a dictionary,
        ' so this method always returns true.
        Return True
    End Function
End Class
