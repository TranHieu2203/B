Public Class HistoryLeaveDTO
    Public Property ID As Decimal?
    Public Property EMPLOYEE_ID As Decimal?
    Public Property EMPLOYEE_CODE As String
    Public Property EMPLOYEE_NAME As String
    Public Property FROM_DATE As Date?
    Public Property TO_DATE As Date?
    Public Property FROM_HOUR As Date?
    Public Property TO_HOUR As Date?
    Public Property REGDATE As Date?
    Public Property NOTE As String ' ghi chu
    Public Property SIGN_ID As Decimal? 'ID loai dk nghi
    Public Property SIGN_CODE As String
    Public Property SIGN_NAME As String ' loai dang ky nghi
    Public Property APPROVE_DATE As Date? ' THOI GIAN DUYET
    Public Property APPROVE_STATUS As Decimal? 'trang thai
End Class
