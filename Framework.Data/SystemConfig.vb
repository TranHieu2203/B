Public Class SystemConfig
    Implements IDisposable

    Dim _ctx As AuditLogContext

    Public ReadOnly Property Context As AuditLogContext
        Get
            If _ctx Is Nothing Then
                _ctx = New AuditLogContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property

#Region "System Configurate"
    Public Function GetConfig(Optional ByVal eModule As ModuleID = ModuleID.All) As Dictionary(Of String, String)
        Try
            Dim query As List(Of ConfigDTO)
            Dim r As New Dictionary(Of String, String)
            Dim iMod As Integer? = eModule
            query = (From p In Context.SE_CONFIG
                     Where (iMod = 0) Or (iMod <> 0 And p.MODULE = iMod)
                     Select New ConfigDTO With {
                      .ID = p.ID,
                      .CODE = p.CODE,
                      .NAME = p.NAME,
                      .VALUE = p.VALUE}).ToList
            For i = 0 To query.Count - 1 Step 1
                r.Add(query(i).CODE, query(i).VALUE)
            Next
            Return r
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateConfig(ByVal _lstConfig As Dictionary(Of String, String), ByVal eModule As ModuleID) As Boolean

        Try
            For i = 0 To _lstConfig.Count - 1
                Dim code As String = _lstConfig.Keys.ElementAt(i)
                Dim iMod As Integer? = eModule
                Dim Value As String
                If String.IsNullOrEmpty(_lstConfig(code)) Then
                    Continue For
                End If
                If _lstConfig(code).ToUpper = "TRUE" Then
                    Value = "-1"
                ElseIf _lstConfig(code).ToUpper = "FALSE" Then
                    Value = "0"
                Else
                    Value = _lstConfig(code)
                End If
                Dim query As SE_CONFIG = (From p In Context.SE_CONFIG
                                          Where p.CODE = code And ((iMod = 0) Or (iMod <> 0 And p.MODULE = iMod))
                                          Select p).FirstOrDefault
                If query IsNot Nothing Then
                    query.VALUE = Value
                Else
                    Dim _new As New SE_CONFIG
                    _new.ID = Utilities.GetNextSequence(Context, Context.SE_CONFIG.EntitySet.Name)
                    _new.CODE = code
                    _new.NAME = code
                    _new.VALUE = Value
                    _new.MODULE = eModule
                    Context.SE_CONFIG.AddObject(_new)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "Reminder per user"

    Public Function GetReminderConfig() As Dictionary(Of String, Dictionary(Of Integer, String))

        Try
            Dim rmdCfg As New Dictionary(Of String, Dictionary(Of Integer, String))
            Dim cfgList As List(Of String) = (From p In Context.SE_REMINDER Select p.USERNAME).Distinct.ToList

            For Each item In cfgList
                Dim lst = Context.SE_REMINDER.Where(Function(p) p.USERNAME = item).ToList
                Dim config As New Dictionary(Of Integer, String)
                For Each itemConfig In lst
                    config.Add(itemConfig.TYPE, itemConfig.VALUE)
                Next
                rmdCfg.Add(item, config)
            Next
            Return rmdCfg

        Catch ex As Exception

            Throw
        End Try

    End Function

    Public Function GetReminderConfig(ByVal username As String) As Dictionary(Of Integer, String)

        Try
            Dim rmdCfg As New Dictionary(Of Integer, String)

            Dim cfgList = Context.SE_REMINDER.Where(Function(p) p.USERNAME = username).ToList

            For Each item In cfgList
                rmdCfg.Add(item.TYPE, item.VALUE)
            Next

            Return rmdCfg
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Function SetReminderConfig(ByVal username As String, ByVal type As Integer, ByVal value As String) As Boolean

        Try
            Dim itemUpdate = Context.SE_REMINDER.SingleOrDefault(Function(p) p.USERNAME = username AndAlso p.TYPE = type)

            If itemUpdate Is Nothing Then
                itemUpdate = New SE_REMINDER With {
                    .ID = Utilities.GetNextSequence(Context, Context.SE_REMINDER.EntitySet.Name),
                    .TYPE = type,
                    .USERNAME = username,
                    .VALUE = value
                }

                Context.SE_REMINDER.AddObject(itemUpdate)

            Else
                itemUpdate.VALUE = value
            End If

            Context.SaveChanges()

            Return True
        Catch ex As Exception

            Throw ex
        End Try


    End Function
#End Region

    Public Enum RemindConfigType
        Contract = 1
        Birthday = 2
        Probation = 3
        Retire = 4
        Email = 5
        Visa = 6
    End Enum

    Public Enum ModuleID
        All = 0
        iProfile = 1
        iTime = 2
        iPayroll = 3
        iPortal = 4
        iSecure = 5
        iRecruitment = 6
    End Enum

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

