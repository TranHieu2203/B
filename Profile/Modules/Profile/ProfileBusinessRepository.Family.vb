Imports Profile.ProfileBusiness

Partial Public Class ProfileBusinessRepository
    Inherits ProfileRepositoryBase
#Region "hu_certificate_edit"
    Public Function SendCertificateEdit(ByVal lstID As List(Of Decimal)
                                          ) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SendCertificateEdit(lstID, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistCertificateEdit(pk_key)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                          ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCertificateEdit(objCertificateEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try

        End Using

    End Function
    Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCertificateEdit(objCertificateEdit, Me.Log, gID)

            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificateEdit(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "HU_CERTIFICATE"
    Public Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetCertificate(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region
#Region "Family -Quan hệ nhân thân"
    Public Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertEmployeeFamily(objFamily, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyEmployeeFamily(objFamily, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteEmployeeFamily(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteEmployeeFamily(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetFamilyByID(ByVal id As Decimal) As FamilyDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetFamilyByID(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetWorkingBeforeByID(ByVal id As Decimal) As WorkingBeforeDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetWorkingBeforeByID(id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckChuho(ByVal emp_id As Decimal, ByVal fa_id As Decimal) As Decimal
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckChuho(emp_id, fa_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeFamily_1(ByVal _filter As FamilyDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of FamilyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeFamily_1(_filter, PageIndex, PageSize, Total, _param, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeFamily(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function ValidateFamily(ByVal _validate As FamilyDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateFamily(_validate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "FamilyEdit -Quan hệ nhân thân"
    Public Function GetChangedFamilyList(ByVal lstFamilyEdit As List(Of FamilyEditDTO)) As Dictionary(Of String, String)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangedFamilyList(lstFamilyEdit)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertEmployeeFamilyEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyEmployeeFamilyEdit(objFamilyEdit, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteEmployeeFamilyEdit(ByVal lstDecimal As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteEmployeeFamilyEdit(lstDecimal, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetEmployeeFamilyEdit(_filter)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckExistFamilyEdit(ByVal pk_key As Decimal, Optional ByVal tab As Decimal = 0) As FamilyEditDTO
        Using rep As New ProfileBusinessClient
            Try
                Return rep.CheckExistFamilyEdit(pk_key, tab)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.SendEmployeeFamilyEdit(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateStatusEmployeeFamilyEdit(lstID, status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                               status As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateStatusEmployeeCetificateEdit(lstID, status, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function



    Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of FamilyEditDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveFamilyEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer, ByVal _param As ParamDTO,
                                   Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveEmployeeCertificateEdit(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveEmployeeCertificateEdit(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetChangedEmployeeCertificateList(ByVal lstEmpEdit As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)) As Dictionary(Of String, String)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetChangedEmployeeCertificateList(lstEmpEdit)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of FamilyEditDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.GetApproveFamilyEdit(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


End Class
