Public Class TotalDayOffDTO
    Public Property EMPLOYEE_ID As Decimal?
    Public Property DATE_REGISTER As Date?
    Public Property LEAVE_TYPE As Decimal? 'KIEU NGHI
    Public Property TOTAL_DAY As Decimal? 'TONG SO NGAY DUOC NGHI
    Public Property USED_DAY As Decimal? 'TONG SO NGAY DA NGHI
    Public Property REST_DAY As Decimal? ' TONG SO NGAY CON LAI
    Public Property LIMIT_DAY As String ' SO NGAY GIOI HAN LAN
    Public Property LIMIT_YEAR As String ' SO NGAY GIOI HAN NAM
    Public Property TIME_MANUAL_ID As Decimal? 'ID DANH MUC NGHI

    Public Property PREV_HAVE As Decimal? 'Phép cũ chuyển qua
    Public Property PREV_USED As Decimal? ' Phép cũ đã nghỉ
    Public Property PREVTOTAL_HAVE As Decimal? 'Phép cũ còn lại
    Public Property SENIORITYHAVE As Decimal? 'Phép thâm niên
    Public Property TOTAL_HAVE1 As Decimal? 'hép chuẩn năm nay
    Public Property TIME_OUTSIDE_COMPANY As Decimal? 'hép chuẩn năm nay

    Public Property ID_PORTAL_REG As Decimal?

End Class