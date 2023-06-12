Imports Profile.ProfileBusiness

Partial Public Class ProfileRepository

#Region "Lists"


#Region "CompetencyGroup"

    Public Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)
        Dim lstCompetencyGroup As List(Of CompetencyGroupDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyGroup = rep.GetCompetencyGroup(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)
        Dim lstCompetencyGroup As List(Of CompetencyGroupDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyGroup = rep.GetCompetencyGroup(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyGroup
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyGroup(objCompetencyGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCompetencyGroup(objCompetencyGroup)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyGroup(objCompetencyGroup, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveCompetencyGroup(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyGroup(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region


#Region "Competency"

    Public Function GetCompetency(ByVal _filter As CompetencyDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)
        Dim lstCompetency As List(Of CompetencyDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetency = rep.GetCompetency(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetency
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetency(ByVal _filter As CompetencyDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)
        Dim lstCompetency As List(Of CompetencyDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetency = rep.GetCompetency(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetency
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetency(ByVal objCompetency As CompetencyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetency(objCompetency, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateCompetency(ByVal objCompetency As CompetencyDTO) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ValidateCompetency(objCompetency)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetency(ByVal objCompetency As CompetencyDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetency(objCompetency, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ActiveCompetency(lstID, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetency(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetency(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyBuild"

    Public Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)
        Dim lstCompetencyBuild As List(Of CompetencyBuildDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyBuild = rep.GetCompetencyBuild(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyBuild
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)
        Dim lstCompetencyBuild As List(Of CompetencyBuildDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyBuild = rep.GetCompetencyBuild(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyBuild
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyBuild(objCompetencyBuild, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyBuild(objCompetencyBuild, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyBuild(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyStandard"

    Public Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)
        Dim lstCompetencyStandard As List(Of CompetencyStandardDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyStandard = rep.GetCompetencyStandard(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyStandard
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)
        Dim lstCompetencyStandard As List(Of CompetencyStandardDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyStandard = rep.GetCompetencyStandard(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyStandard
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyStandard(objCompetencyStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyStandard(objCompetencyStandard, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyStandard(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyAppendix"

    Public Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)
        Dim lstCompetencyAppendix As List(Of CompetencyAppendixDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyAppendix = rep.GetCompetencyAppendix(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyAppendix
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)
        Dim lstCompetencyAppendix As List(Of CompetencyAppendixDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyAppendix = rep.GetCompetencyAppendix(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyAppendix
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyAppendix(objCompetencyAppendix, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyAppendix(objCompetencyAppendix, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyAppendix(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyEmp"

    Public Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO)
        Dim lstCompetencyEmp As List(Of CompetencyEmpDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyEmp = rep.GetCompetencyEmp(_filter)
                Return lstCompetencyEmp
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCompetencyEmp(ByVal lstEmp As List(Of CompetencyEmpDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateCompetencyEmp(lstEmp, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyPeriod"

    Public Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)
        Dim lstCompetencyPeriod As List(Of CompetencyPeriodDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyPeriod = rep.GetCompetencyPeriod(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)
        Dim lstCompetencyPeriod As List(Of CompetencyPeriodDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyPeriod = rep.GetCompetencyPeriod(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyPeriod
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyPeriod(objCompetencyPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyPeriod(objCompetencyPeriod, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyPeriod(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "CompetencyAssDtl"

    Public Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO)
        Dim lstCompetencyAssDtl As List(Of CompetencyAssDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyAssDtl = rep.GetCompetencyAss(_filter)
                Return lstCompetencyAssDtl
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO)
        Dim lstCompetencyAssDtl As List(Of CompetencyAssDtlDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyAssDtl = rep.GetCompetencyAssDtl(_filter)
                Return lstCompetencyAssDtl
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstEmp As List(Of CompetencyAssDtlDTO)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.UpdateCompetencyAssDtl(objAss, lstEmp, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyAss(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "Khoa dao tao Course"
    Public Function GetCourseByList(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dtData As DataTable

        Using rep As New ProfileBusinessClient
            Try
                dtData = rep.GetCourseByList(Common.Common.SystemLanguage.Name, isBlank)
                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#End Region

#Region "Setting"
#Region "Competency Course"

    Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                             ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)
        Dim lstCompetencyCourse As List(Of CompetencyCourseDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyCourse = rep.GetCompetencyCourse(_filter, PageIndex, PageSize, Total, Sorts)
                Return lstCompetencyCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)
        Dim lstCompetencyCourse As List(Of CompetencyCourseDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCompetencyCourse = rep.GetCompetencyCourse(_filter, 0, Integer.MaxValue, 0, Sorts)
                Return lstCompetencyCourse
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.InsertCompetencyCourse(objCompetencyCourse, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByRef gID As Decimal) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.ModifyCompetencyCourse(objCompetencyCourse, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New ProfileBusinessClient
            Try
                Return rep.DeleteCompetencyCourse(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region
#End Region

#Region "Business"
    Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeCriteriaRecordDTO)
        Dim lstCertificate As List(Of EmployeeCriteriaRecordDTO)

        Using rep As New ProfileBusinessClient
            Try
                lstCertificate = rep.EmployeeCriteriaRecord(_filter, 0, Integer.MaxValue, 0, _param, Sorts, Log)
                Return lstCertificate
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer, ByVal _param As ParamDTO,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of EmployeeCriteriaRecordDTO)
        Using rep As New ProfileBusinessClient
            Try
                Return rep.EmployeeCriteriaRecord(_filter, PageIndex, PageSize, Total, _param, Sorts, Log)
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


End Class
