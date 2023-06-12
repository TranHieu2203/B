Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "health exam"

        Public Function DeleteHealthExam(ByVal lstID As List(Of Decimal)) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteHealthExam
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteHealthExam(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetHealthExam(ByVal _filter As HealthExamDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HealthExamDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetHealthExam
            Using rep As New ProfileRepository
                Try
                    Return rep.GetHealthExam(_filter, PageIndex, PageSize, Total, log, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Commend"
        Public Function CheckHasFileComend(ByVal id As List(Of Decimal)) As Decimal Implements ServiceContracts.IProfileBusiness.CheckHasFileComend
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckHasFileComend(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCommend(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO) _
                                    Implements ServiceContracts.IProfileBusiness.GetCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCommend(_filter, PageIndex, PageSize, Total, log, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeCommendByID(ByVal ComId As Decimal) As List(Of CommendEmpDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeCommendByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeCommendByID(ComId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetOrgCommendByID(ByVal ComId As Decimal) As List(Of CommendOrgDTO) _
           Implements ServiceContracts.IProfileBusiness.GetOrgCommendByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetOrgCommendByID(ComId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCommendByID(ByVal _filter As CommendDTO) As CommendDTO _
            Implements ServiceContracts.IProfileBusiness.GetCommendByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCommendByID(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertCommend(objCommend, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyCommend(objCommend, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateCommend(ByVal sType As String, ByVal obj As CommendDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateCommend
            Using rep As New ProfileRepository
                Try

                    Return rep.ValidateCommend(sType, obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteCommend(ByVal objCommend As CommendDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteCommend(objCommend)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ApproveCommend(ByVal objCommend As CommendDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ApproveCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveCommend(objCommend)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean _
          Implements ServiceContracts.IProfileBusiness.InsertImportCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertImportCommend(lstImport, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO) _
        Implements ServiceContracts.IProfileBusiness.GetImportCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.GetImportCommend(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ApproveListCommend(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean _
          Implements ServiceContracts.IProfileBusiness.ApproveListCommend
            Using rep As New ProfileRepository
                Try
                    Return rep.ApproveListCommend(listID, acti, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Công thức khen thưởng (Commend_formula)"
        Public Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO) _
                                   Implements ServiceContracts.IProfileBusiness.GetCommendFormula
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCommendFormula(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO _
           Implements ServiceContracts.IProfileBusiness.GetCommendFormulaID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetCommendFormulaID(ID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
          Implements ServiceContracts.IProfileBusiness.InsertCommendFormula
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertCommendFormula(objCommendFormula, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
         Implements ServiceContracts.IProfileBusiness.ModifyCommendFormula
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyCommendFormula(objCommendFormula, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean _
         Implements ServiceContracts.IProfileBusiness.ActiveCommendFormula
            Using rep As New ProfileRepository
                Try
                    Return rep.ActiveCommendFormula(lstID, sActive, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean _
         Implements ServiceContracts.IProfileBusiness.DeleteCommendFormula
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteCommendFormula(lstDecimals, log, strError)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function EXPORT_QLKT() As DataSet _
         Implements ServiceContracts.IProfileBusiness.EXPORT_QLKT
            Using rep As New ProfileRepository
                Try
                    Return rep.EXPORT_QLKT()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GET_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable _
         Implements ServiceContracts.IProfileBusiness.GET_EMPLOYEE
            Using rep As New ProfileRepository
                Try
                    Return rep.GET_EMPLOYEE(P_EMP_CODE)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

    End Class
End Namespace