Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Module ExtensionMethods

#Region "Date"
    <Extension()>
    Public Function FirstDateOfMonth(ByVal currentDate As DateTime) As DateTime

        Return currentDate.ToString("01/MM/yyyy").ToDate().Value

    End Function

    <Extension()>
    Public Function LastDateOfMonth(ByVal currentDate As DateTime) As DateTime

        Return currentDate.FirstDateOfMonth().AddMonths(1).AddDays(-1)

    End Function

    <Extension()>
    Public Function FirstDateOfWeek(ByVal currentDate As DateTime) As DateTime

        Return currentDate.AddDays(DayOfWeek.Monday - If(currentDate.DayOfWeek = DayOfWeek.Sunday, DayOfWeek.Saturday + 1, currentDate.DayOfWeek))

    End Function

    <Extension()>
    Public Function LastDateOfWeek(ByVal currentDate As DateTime) As DateTime

        Return currentDate.FirstDateOfWeek().AddDays(6)

    End Function
#End Region

#Region "String"

    <Extension()>
    Public Function ToDate(ByVal input As String) As DateTime?
        Dim retValue As DateTime

        If Date.TryParse(input, CultureInfo.CreateSpecificCulture("vi-VN"), Globalization.DateTimeStyles.None, retValue) Then
            Return retValue
        Else
            Return CType(Nothing, DateTime?)
        End If

    End Function
#End Region
End Module
