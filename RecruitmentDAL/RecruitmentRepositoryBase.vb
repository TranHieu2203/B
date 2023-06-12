Public Class RecruitmentRepositoryBase
    Private _ctx As RecruitmentContext

    Public ReadOnly Property Context As RecruitmentContext
        Get
            If _ctx Is Nothing Then
                _ctx = New RecruitmentContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property

End Class
