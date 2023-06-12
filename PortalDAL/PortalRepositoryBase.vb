Public Class PortalRepositoryBase
    Private _ctx As PortalContext

    Public ReadOnly Property Context As PortalContext
        Get
            If _ctx Is Nothing Then
                _ctx = New PortalContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property
End Class
