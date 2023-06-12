Imports Framework.Data
Imports RecruitmentDAL


' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceImplementations
    Partial Class RecruitmentBusiness

#Region "Dashboard"
        Public Function GetStatisticGender(ByVal _year As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.StatisticDTO) Implements ServiceContracts.IRecruitmentBusiness.GetStatisticGender
            Try
                Return RecruitmentRepositoryStatic.Instance.GetStatisticGender(_year, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStatisticEduacation(ByVal _year As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.StatisticDTO) Implements ServiceContracts.IRecruitmentBusiness.GetStatisticEduacation
            Try
                Return RecruitmentRepositoryStatic.Instance.GetStatisticEduacation(_year, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetStatisticCanToEmp(ByVal _year As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.StatisticDTO) Implements ServiceContracts.IRecruitmentBusiness.GetStatisticCanToEmp
            Try
                Return RecruitmentRepositoryStatic.Instance.GetStatisticCanToEmp(_year, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetStatisticEstimateReality(ByVal _year As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.StatisticDTO) Implements ServiceContracts.IRecruitmentBusiness.GetStatisticEstimateReality
            Try
                Return RecruitmentRepositoryStatic.Instance.GetStatisticEstimateReality(_year, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function REPORT_RANK_SAL_BY_MONTH(ByVal _year As Integer, ByVal _month As Integer, ByVal _org As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.HRPlaningDetailDTO) Implements ServiceContracts.IRecruitmentBusiness.REPORT_RANK_SAL_BY_MONTH
            Try
                Return RecruitmentRepositoryStatic.Instance.REPORT_RANK_SAL_BY_MONTH(_year, _month, _org, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function REPORT_RANK_SAL_BY_YEAR_SAL(ByVal _year As Integer, ByVal _org As Integer, ByVal log As UserLog) As System.Collections.Generic.List(Of RecruitmentDAL.ReportDTO) Implements ServiceContracts.IRecruitmentBusiness.REPORT_RANK_SAL_BY_YEAR_SAL
            Try
                Return RecruitmentRepositoryStatic.Instance.REPORT_RANK_SAL_BY_YEAR_SAL(_year, _org, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region



    End Class
End Namespace