Public Class SIGN_DTO
    Public Property ID As Guid
    Public Property CODE As String
    Public Property NAME As String
    Public Property STARTDATE As Date?
    Public Property ENDDATE As Date?
    Public Property ORDNO As Decimal?
    Public Property NOTE As String
    Public Property ACTFLG As Boolean
    Public Property GSIGNID As Guid
    Public Property SIGNTYPEID As Guid
    Public Property SIGNMODEID As Guid
    Public Property DISPLAY As Boolean
    Public Property IMPEXCEL As Boolean

    Public Property GSIGN_CODE As String
    Public Property SIGNTYPE_CODE As String
    Public Property SIGNMODE_CODE As String

    Public Property GSIGN_NAME As String
    Public Property SIGNTYPE_NAME As String
    Public Property SIGNMODE_NAME As String

    Public Property SIGNEXT As SIGNEXT_DTO

    Public Property NEXT1 As String
    Public Property NEXT2 As String
    Public Property NEXT3 As String
    Public Property SEXT1 As String
    Public Property SEXT2 As String
    Public Property SEXT3 As String
    Public Property DEXT1 As String
    Public Property DEXT2 As String
    Public Property DEXT3 As String

    Public Property LST_SIGNDEFAULT As IEnumerable(Of SIGNDEFAULTDTO)

    Public Property SHIFT As SHIFT_DTO
End Class
