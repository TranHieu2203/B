Public Class ATConstant
    Public Const ACTFLG_ACTIVE As String = "A"
    Public Const ACTFLG_DEACTIVE As String = "I"

    Public Const PERIODSTATUS_OPEN As String = "O"
    Public Const PERIODSTATUS_CLOSE As String = "C"
    Public Const PERIODSTATUS_FINALCLOSE As String = "FC"
    Public Const PERIODSTATUS_REOPEN As String = "RO"

    Public Const GSIGNCODE_SHIFT As String = "SHIFT"
    Public Const GSIGNCODE_LEAVE As String = "LEAVE"
    Public Const GSIGNCODE_LB As String = "LB"
    Public Const GSIGNCODE_SUMMARY As String = "SUMMARY"
    Public Const GSIGNCODE_OVERTIME As String = "OVERTIME"
    Public Const GSIGNCODE_INOUT As String = "INOUT"
    Public Const GSIGNCODE_WLEO As String = "WLEO"
    Public Const GSIGNCODE_WORK As String = "WORK"
    Public Const GSIGNCODE_AL As String = "AL"
    Public Const GSIGNCODE_OTHERS As String = "OTHERS"

    Public Const GSIGNCODE_TIME As String = "TIME" ' BangDV: Thêm để phục vụ cho phần phê duyệt

    Public Const SIGNTYPECODE_STRING As String = "STRING"
    Public Const SIGNTYPECODE_DATETIME As String = "DATETIME"
    Public Const SIGNTYPECODE_NUMBER As String = "NUMBER"

    Public Const FORMAT_DATE As String = "dd/MM/yyyy"

    Public Const SIGNMODECODE_REGISTER As String = "RGT"
    Public Const SIGNMODECODE_SUMMARY As String = "SUM"

    Public Const APPROVE As Decimal = 471
    Public Const NOT_APPROVE As Decimal = 472

    ' Tham số định nghĩa đọc từ cột nào trong file import công
    Public Const colStart As Decimal = 3
    Public Const rowStart As Decimal = 8
    Public Const SUMCOL_IMPORTMONTH As Decimal = 17
    Public Const SUMCOL_IMPORTHOUR As Decimal = 16
    Public Const SIGINCODE_P As String = "P"

    'Tham số loại import
    Public Const IMPORT_MONTH As String = "IMPORT_MONTH"
    Public Const IMPORT_HOUR As String = "IMPORT_HOUR"
    Public Const HALF As String = "/2"

    'Programid template Import
    Public Const PROGRAMID_IMPORT_MONTH As Decimal = 145
    Public Const PROGRAMID_IMPORT_HOUR As Decimal = 146

    Public Const N As String = "A"
    Public Const D As String = "M"

    'Manning
    Public Const IMPORT_MANNING As String = "IMPORT_MANNING"
    Public Const PROGRAMID_IMPORT_MANNING As Decimal = 147
End Class




