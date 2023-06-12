Imports RecruitmentBusiness.ServiceContracts
Imports RecruitmentDAL


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Public Class RecruitmentBusiness
        Implements IRecruitmentBusiness

#Region "Test Service"
        Public Function TestService(ByVal str As String) As String Implements ServiceContracts.IRecruitmentBusiness.TestService
            Return "Hello world " & str
        End Function
#End Region

#Region "Common"
        Public Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean Implements ServiceContracts.IRecruitmentBusiness.GetComboList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetComboList(_combolistDTO)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetOtherList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetOtherList(sType, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Get_HU_WORK_PLACE(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.Get_HU_WORK_PLACE

            Try

                Dim lst = RecruitmentRepositoryStatic.Instance.Get_HU_WORK_PLACE(sType, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetProvinceList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetProvinceList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProvinceList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetContractTypeList(ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetContractTypeList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetContractTypeList(isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetTitleByOrgList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetTitleByOrgList(orgID, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTitleByOrgListInPlan(ByVal orgID As Decimal, _year As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetTitleByOrgListInPlan
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetTitleByOrgListInPlan(orgID, _year, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetProgramExamsList(ByVal programID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable Implements ServiceContracts.IRecruitmentBusiness.GetProgramExamsList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramExamsList(programID, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProgramList(ByVal OrgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable _
            Implements ServiceContracts.IRecruitmentBusiness.GetProgramList
            Try
                Dim lst = RecruitmentRepositoryStatic.Instance.GetProgramList(OrgID, sLang, isBlank)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class
End Namespace