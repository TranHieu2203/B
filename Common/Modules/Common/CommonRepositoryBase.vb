﻿Imports Common.CommonBusiness

Public Class CommonRepositoryBase
    Implements IDisposable
    ' To detect redundant calls
    Private disposed As Boolean = False

    Public ReadOnly Property Log As UserLog
        Get
            Return LogHelper.GetUserLog
        End Get
    End Property

    ' IDisposable
    Protected Overridable Sub Dispose( _
       ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Free other state (managed objects).
            End If
            ' Free your own state (unmanaged objects).
            ' Set large fields to null.
        End If
        Me.disposed = True
    End Sub

    ' This code added by Visual Basic to 
    ' correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
End Class
