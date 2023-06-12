Imports Framework.Data
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
#Region "HU_CERTIFICATE_EDIT"
        Public Function SendCertificateEdit(ByVal lstID As List(Of Decimal),
                                          ByVal log As UserLog) As Boolean Implements ServiceContracts.IProfileBusiness.SendCertificateEdit
            Try
                Using rep As New ProfileRepository
                    Return rep.SendCertificateEdit(lstID, log)
                End Using
            Catch ex As Exception

            End Try
        End Function
        Public Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) Implements ServiceContracts.IProfileBusiness.GetCertificateEdit

            Try
                Using rep As New ProfileRepository
                    Return rep.GetCertificateEdit(_filter)
                End Using
            Catch ex As Exception

            End Try
        End Function
        Public Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByVal log As UserLog,
                                            ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.InsertCertificateEdit
            Try
                Using rep As New ProfileRepository
                    Return rep.InsertCertificateEdit(objCertificateEdit, log, gID)
                End Using

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean Implements ServiceContracts.IProfileBusiness.ModifyCertificateEdit
            Try
                Using rep As New ProfileRepository
                    Return rep.ModifyCertificateEdit(objCertificateEdit, log, gID)
                End Using

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT Implements ServiceContracts.IProfileBusiness.CheckExistCertificateEdit
            Try
                Using rep As New ProfileRepository
                    Return rep.CheckExistCertificateEdit(pk_key)
                End Using

            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "HU_CERTIFICATE"
        Public Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO) Implements ServiceContracts.IProfileBusiness.GetCertificate
            Try
                Using rep As New ProfileRepository
                    Return rep.GetCertificate(_filter)
                End Using
            Catch ex As Exception

            End Try
        End Function
#End Region
#Region "EmployeeFamily"
        Public Function CheckChuho(ByVal emp_id As Decimal, ByVal fa_id As Decimal) As Decimal _
            Implements ServiceContracts.IProfileBusiness.CheckChuho
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckChuho(emp_id, fa_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetFamilyByID(ByVal id As Decimal) As FamilyDTO _
            Implements ServiceContracts.IProfileBusiness.GetFamilyByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetFamilyByID(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetWorkingBeforeByID(ByVal id As Decimal) As WorkingBeforeDTO _
            Implements ServiceContracts.IProfileBusiness.GetWorkingBeforeByID
            Using rep As New ProfileRepository
                Try
                    Return rep.GetWorkingBeforeByID(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeFamily_1(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of FamilyDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeFamily_1
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeFamily_1(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeFamily(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertEmployeeFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertEmployeeFamily(objFamily, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyEmployeeFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyEmployeeFamily(objFamily, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteEmployeeFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteEmployeeFamily(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateFamily(ByVal objFamily As FamilyDTO) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ValidateFamily
            Using rep As New ProfileRepository
                Try
                    Return rep.ValidateFamily(objFamily)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "EmployeeFamilyEdit"
        Public Function GetChangedFamilyList(ByVal lstFamilyEdit As List(Of FamilyEditDTO)) As Dictionary(Of String, String) _
            Implements ServiceContracts.IProfileBusiness.GetChangedFamilyList
            Using rep As New ProfileRepository
                Try
                    Return rep.GetChangedFamilyList(lstFamilyEdit)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO) _
            Implements ServiceContracts.IProfileBusiness.GetEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetEmployeeFamilyEdit(_filter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.InsertEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.InsertEmployeeFamilyEdit(objFamilyEdit, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean _
            Implements ServiceContracts.IProfileBusiness.ModifyEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.ModifyEmployeeFamilyEdit(objFamilyEdit, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.DeleteEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.DeleteEmployeeFamilyEdit(lstDecimals, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckExistFamilyEdit(ByVal pk_key As Decimal, ByVal tab As Decimal) As FamilyEditDTO _
            Implements ServiceContracts.IProfileBusiness.CheckExistFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.CheckExistFamilyEdit(pk_key, tab)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.SendEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.SendEmployeeFamilyEdit(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ReadNotifi(ByVal id As Decimal) As Decimal _
            Implements ServiceContracts.IProfileBusiness.ReadNotifi
            Using rep As New ProfileRepository
                Try
                    Return rep.ReadNotifi(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   status As String,
                                                   ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IProfileBusiness.UpdateStatusEmployeeFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateStatusEmployeeFamilyEdit(lstID, status, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                                  status As String,
                                                  ByVal log As UserLog) As Boolean _
                                               Implements ServiceContracts.IProfileBusiness.UpdateStatusEmployeeCetificateEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.UpdateStatusEmployeeCetificateEdit(lstID, status, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO) _
            Implements ServiceContracts.IProfileBusiness.GetApproveFamilyEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetApproveFamilyEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) _
             Implements ServiceContracts.IProfileBusiness.GetApproveEmployeeCertificateEdit
            Using rep As New ProfileRepository
                Try
                    Return rep.GetApproveEmployeeCertificateEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetChangedEmployeeCertificateList(ByVal lstEmpEdit As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)) As Dictionary(Of String, String) _
             Implements ServiceContracts.IProfileBusiness.GetChangedEmployeeCertificateList
            Using rep As New ProfileRepository
                Try
                    Return rep.GetChangedEmployeeCertificateList(lstEmpEdit)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function copyAddress(ByVal emp_id As Decimal) As FamilyEditDTO _
             Implements ServiceContracts.IProfileBusiness.copyAddress
            Using rep As New ProfileRepository
                Try
                    Return rep.copyAddress(emp_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace
