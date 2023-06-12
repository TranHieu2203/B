Public Class PermissionDTO
    Public Property ID As Decimal
    Public Property GroupID As Decimal
    Public Property OrgID As Decimal?
    Public Property WorkLevelID As Decimal?
    Public Property FunctionID As Decimal
    Public Property FID As String
    Public Property MID As String
    Public Property AllowCreate As Int16
    Public Property AllowModify As Int16
    Public Property AllowDelete As Int16
    Public Property AllowPrint As Int16
    Public Property AllowImport As Int16
    Public Property AllowExport As Int16
    Public Property AllowSpecial1 As Int16
    Public Property AllowSpecial2 As Int16
    Public Property AllowSpecial3 As Int16
    Public Property AllowSpecial4 As Int16
    Public Property AllowSpecial5 As Int16
    Public Property IS_REPORT As Boolean
    Public Property FunctionName As String
End Class
