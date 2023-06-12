Imports Common
Imports Common.CommonBusiness

Public Class PortalRepositoryBase

    Public ReadOnly Property Log As UserLog
        Get
            Return LogHelper.GetUserLog
        End Get
    End Property


End Class
