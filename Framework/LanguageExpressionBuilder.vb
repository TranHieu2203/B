Imports System.CodeDom
Imports System.Web.Compilation
Imports System.Web.UI

<ExpressionPrefix("Translate")> _
<ExpressionEditor("TranslateEditor")> _
Public Class LanguageExpressionBuilder
    Inherits ExpressionBuilder


    Public Overrides Function GetCodeExpression(ByVal entry As System.Web.UI.BoundPropertyEntry, ByVal parsedData As Object, ByVal context As System.Web.Compilation.ExpressionBuilderContext) As System.CodeDom.CodeExpression

        Dim methodParams() As CodeExpression = New CodeExpression() {New CodePrimitiveExpression(entry.Expression.Trim())}
        Return New CodeMethodInvokeExpression(New CodeTypeReferenceExpression(MyBase.GetType()), "Translate", methodParams)
    End Function


    Public Shared Function Translate(ByVal expression As String)
        Using langMgr As New LanguageManager
            Try
                Dim lst As List(Of String) = expression.Split(";").ToList
                If lst.Count > 1 Then
                    Dim strFormat As String = lst(0)
                    lst.RemoveAt(0)
                    Dim arr As String() = lst.ToArray
                    Return (langMgr.Translate(strFormat, arr))
                End If
                Return langMgr.Translate(lst(0))
            Catch ex As Exception
                langMgr.Dispose()
            End Try
        End Using

        Return expression
    End Function

    Public Overrides ReadOnly Property SupportsEvaluate As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function EvaluateExpression(ByVal target As Object, _
       ByVal entry As BoundPropertyEntry, ByVal parsedData As Object, _
       ByVal context As ExpressionBuilderContext) As Object

        Return Translate(entry.Expression)
    End Function


End Class
