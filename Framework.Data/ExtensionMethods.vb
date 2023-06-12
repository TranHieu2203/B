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

#Region "Collection Convert"
    <Extension()>
    Public Function ToList(Of T)(ByVal dt As DataTable) As IList(Of T)
        Using collecHelper As New CollectionHelper
            Return collecHelper.ConvertTo(Of T)(dt)
        End Using
    End Function

    <Extension()>
    Public Function ToTable(Of T)(ByVal lst As IList(Of T)) As DataTable
        Using uti As New Utilities
            Return uti.GetDataTableByList(lst)
        End Using
    End Function
#End Region
End Module

Public Class DateDifference
    ''' <summary>
    ''' defining Number of days in month; index 0=> january and 11=> December
    ''' february contain either 28 or 29 days, that's why here value is -1
    ''' which wil be calculate later.
    ''' </summary>
    Private monthDay() As Integer = {31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

    ''' <summary>
    ''' từ ngày
    ''' </summary>
    Private fromDate As Date

    ''' <summary>
    ''' đến ngày
    ''' </summary>
    Private toDate As Date

    ''' <summary>
    ''' 3 tham số trả về
    ''' </summary>
    Private year As Integer
    Private month As Integer
    Private day As Integer

    Public Sub New(ByVal d1 As Date, ByVal d2 As Date)
        Dim increment As Integer

        If d1 > d2 Then
            Me.fromDate = d2
            Me.toDate = d1
        Else
            Me.fromDate = d1
            Me.toDate = d2
        End If

        ' Tính toán ngày
        increment = 0

        If Me.fromDate.Day > Me.toDate.Day Then
            increment = Me.monthDay(Me.fromDate.Month - 1)
        End If

        ' Nếu là tháng 2
        If increment = -1 Then
            ' kiểm tra năm nhuận
            If Date.IsLeapYear(Me.fromDate.Year) Then
                ' nếu là năm nhuận tháng 2 có 29 ngày
                increment = 29
            Else
                increment = 28
            End If
        End If
        ' Nếu không phải tháng 2
        If increment <> 0 Then
            day = (Me.toDate.Day + increment) - Me.fromDate.Day
            increment = 1
        Else
            day = Me.toDate.Day - Me.fromDate.Day
        End If

        ' tính ra số tháng
        If (Me.fromDate.Month + increment) > Me.toDate.Month Then
            Me.month = (Me.toDate.Month + 12) - (Me.fromDate.Month + increment)
            increment = 1
        Else
            Me.month = (Me.toDate.Month) - (Me.fromDate.Month + increment)
            increment = 0
        End If

        ' tính ra số năm
        Me.year = Me.toDate.Year - (Me.fromDate.Year + increment)

    End Sub

    Public ReadOnly Property Years() As Integer
        Get
            Return Me.year
        End Get
    End Property

    Public ReadOnly Property Months() As Integer
        Get
            Return Me.month
        End Get
    End Property

    Public ReadOnly Property Days() As Integer
        Get
            Return Me.day
        End Get
    End Property

End Class
