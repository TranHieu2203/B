Option Strict On
Option Explicit On

Imports System.Reflection

Public Class FrameworkUtilities
#Region "Tham số  khi thực thi store procedure(Framework 1.0 và Framework 1.5)"
    Public Shared OUT_CURSOR As String = "OUT_CURSOR"
    Public Shared OUT_NUMBER As String = "OUT_NUMBER"
    Public Shared OUT_STRING As String = "OUT_STRING"
    Public Shared OUT_STRINGBUDER As New StringBuilder
    Public Shared OUT_DATE As String = "OUT_DATE"
    Public Shared TABLE_PARAMERTERS As String = "parameters"

    ''' <summary>
    ''' hàm tạo List Parameter để chuyền vào StoreProcedure
    ''' sử dụng: New With{.[Tên tham số trong Store1] = [Value1],
    '''                   .[Tên tham số trong Store2] = [Value2]}
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateParameterList(Of T)(ByVal parameters As T) As List(Of List(Of Object))
        Dim lstParameter As New List(Of List(Of Object))

        For Each info As PropertyInfo In parameters.GetType().GetProperties()
            Dim param As New List(Of Object)

            param.Add(info.Name)
            param.Add(info.GetValue(parameters, Nothing))

            lstParameter.Add(param)
        Next

        Return lstParameter
    End Function
#End Region
End Class
