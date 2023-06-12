Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Test Service"
        Public Function TestService(ByVal str As String) As String Implements IPayrollBusiness.TestService
            Return "Hello world " & str
        End Function
#End Region
        Public Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements IPayrollBusiness.GetTitleList
            Using rep As New PayrollRepository
                Try
                    Return rep.GetTitleList(sLang, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements IPayrollBusiness.GetOtherList
            Using rep As New PayrollRepository
                Try
                    Return rep.GetOtherList(sType, sLang, isBlank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean Implements IPayrollBusiness.GetComboboxData
            Using rep As New PayrollRepository
                Try
                    Return rep.GetComboboxData(cbxData)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean _
            Implements IPayrollBusiness.CheckExistInDatabase
            Using rep As New PayrollRepository
                Try
                    Return rep.CheckExistInDatabase(lstID, table)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

    End Class
End Namespace

