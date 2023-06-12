Imports Framework.UI
Imports Payroll.PayrollBusiness


Public Class PayrollRepository
    Inherits PayrollRepositoryBase
    Dim CacheMinusDataCombo As Integer = 30
    Public Function TestServices(ByVal Username As String) As String
        Using rep As New PayrollBusinessClient
            Try
                Return rep.TestService(Username)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return False
    End Function

    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetComboboxData(cbxData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckExistInDatabase(lstID, table)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetOtherList(ByVal sType As String, Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New PayrollBusinessClient
            Try
                dtData = CacheManager.GetValue("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"))
                If dtData Is Nothing Then
                    dtData = rep.GetOtherList(sType, Common.Common.SystemLanguage.Name, isBlank)
                End If
                CacheManager.Insert("OT_" & sType & "_" & Common.Common.SystemLanguage.Name & IIf(isBlank, "Blank", "NoBlank"), dtData, CacheMinusDataCombo)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActionSendPayslip(lstEmployee, orgID, isDissolve, periodID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal),
                                     ByVal orgID As Decimal?,
                                     ByVal isDissolve As Decimal?,
                                     ByVal periodID As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.ActionSendBonusslip(lstEmployee, orgID, isDissolve, periodID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetTitleList(Optional ByVal isBlank As Boolean = False) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetTitleList(Common.Common.SystemLanguage.Name, isBlank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
End Class
