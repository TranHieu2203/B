Imports Recruitment.RecruitmentBusiness

Partial Class RecruitmentRepository

#Region "BAO CAO"
    Public Function ExportReport(ByVal sPkgName As String,
                             ByVal sStartDate As Date?,
                             ByVal sEndDate As Date?,
                             ByVal sOrg As String,
                             ByVal IsDissolve As Integer,
                             ByVal sLang As String) As DataSet
        Dim dtData As DataSet

        Using rep As New RecruitmentBusinessClient
            Try
                dtData = rep.ExportReport(sPkgName, sStartDate, sEndDate, sOrg, IsDissolve, Log.Username.ToUpper, Common.Common.SystemLanguage.Name)

                Return dtData
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                    ByRef Total As Integer,
                                    Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Dim lstTitle As List(Of Se_ReportDTO)

        Using rep As New RecruitmentBusinessClient
            Try
                lstTitle = rep.GetReportById(_filter, PageIndex, PageSize, Total, Me.Log, Sorts)
                Return lstTitle
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

#End Region
End Class
