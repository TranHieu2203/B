Imports ProfileBusiness.ServiceContracts
Namespace ProfileBusiness.ServiceImplementations
    Public Class HU_WorkingBusiness
        Implements IHU_WorkingBusiness
        Public Function GetWorkings() As List(Of String) Implements IHU_WorkingBusiness.GetWorkings
            Dim result = New List(Of String)
            result.Add("A")
            result.Add("A")
            result.Add("A")
            result.Add("A")
            result.Add("A")
            Return result
        End Function
    End Class
End Namespace

