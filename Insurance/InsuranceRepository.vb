Imports Insurance.InsuranceBusiness

Public Class InsuranceRepository
    Inherits InsuranceRepositoryBase
    Private _isAvailable As Boolean
    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetComboboxData(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New InsuranceBusinessClient
            Try
                Return rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
End Class