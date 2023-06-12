Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
    Public Function CheckHasFileComend(ByVal id As List(Of Decimal)) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckHasFileComend(id)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#Region "health exam"

    Public Function DeleteHealthExam(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteHealthExam(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetHealthExam(ByVal _filter As HealthExamDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HealthExamDTO)
        Dim lstCommend As List(Of HealthExamDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommend = rep.GetHealthExam(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstCommend
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetHealthExam(ByVal _filter As HealthExamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HealthExamDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetHealthExam(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region
    Public Function GetCommend(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)
        Dim lstCommend As List(Of CommendDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommend = rep.GetCommend(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstCommend
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCommend(ByVal _filter As CommendDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommend(_filter, 0, Integer.MaxValue, 0, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetEmployeeCommendByID(ByVal ComId As Decimal) As List(Of CommendEmpDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeCommendByID(ComId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetOrgCommendByID(ByVal ComId As Decimal) As List(Of CommendOrgDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetOrgCommendByID(ComId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendByID(ByVal _filter As CommendDTO) As CommendDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendByID(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCommend(ByVal objCommend As CommendDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCommend(objCommend, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCommend(ByVal objCommend As CommendDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCommend(objCommend, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCommend(ByVal sType As String, ByVal obj As CommendDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCommend(sType, obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCommend(ByVal objCommend As CommendDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCommend(objCommend)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ApproveCommend(ByVal objCommend As CommendDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveCommend(objCommend)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ApproveListCommend(ByVal listID As List(Of Decimal), ByVal acti As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ApproveListCommend(listID, acti, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertImportCommend(lstImport, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetImportCommend(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#Region "VALIDATE BUSINESS"
    Public Function ValidateBusiness(ByVal Table_Name As String, ByVal Column_Name As String, ByVal ListID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateBusiness(Table_Name, Column_Name, ListID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region
#Region "Công thức khen thưởng (Commend-Formula)"
    Public Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)
        Dim lstCommend As List(Of CommendFormulaDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCommend = rep.GetCommendFormula(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCommend
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendFormula(ByVal _filter As CommendFormulaDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendFormula(_filter, 0, Integer.MaxValue, 0, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCommendFormulaID(ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
    Public Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                    ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCommendFormula(objCommendFormula, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                  ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCommendFormula(objCommendFormula, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveCommendFormula(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByRef strError As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCommendFormula(lstDecimals, Me.Log, strError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

    Function GetOT_OTHER_LIST(p1 As Boolean) As Object
        Throw New NotImplementedException
    End Function

    Public Function EXPORT_QLKT() As DataSet
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EXPORT_QLKT()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GET_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GET_EMPLOYEE(P_EMP_CODE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

End Class
