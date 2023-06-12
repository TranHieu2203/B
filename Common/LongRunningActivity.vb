Imports System.Activities

Public NotInheritable Class LongRunningActivity
    Inherits CodeActivity

    'Define an activity input argument of type String
    Property Ret() As OutArgument(Of String)

    ' If your activity returns a value, derive from CodeActivity(Of TResult)
    ' and return the value from the Execute method.
    Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
        'Obtain the runtime value of the Text input argument
        'Dim client As New ServiceCalculate.CalculateClient
        'Dim str = client.Calculate(New ServiceCalculate.Calculate With {.parameter1 = 1})
        Dim str As String = "Result is: "
        For i As Integer = 0 To 120
            Threading.Thread.Sleep(1000)
            CommonWorkflow.WfLongRunningCount = i
        Next
        Me.Ret.Set(context, str)
    End Sub
End Class
