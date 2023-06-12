Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class TrainingRepository

#Region "BAO CAO"
    Public Function ExportReport(ByVal sPkgName As String,
                             ByVal sStartDate As Date?,
                             ByVal sEndDate As Date?,
                             ByVal sOrg As String,
                             ByVal IsDissolve As Integer, ByVal sUserName As String, ByVal sLang As String) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore(sPkgName,
                                           New With {.P_ORG = sOrg,
                                                     .P_USERNAME = sUserName,
                                                     .P_STARTDATE = sStartDate,
                                                     .P_ENDDATE = sEndDate,
                                                     .P_ISDISSOLVE = IsDissolve,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Try
            Dim query As IQueryable(Of Se_ReportDTO)
            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" And log.Username.ToUpper <> "HR.ADMIN" Then
                'query = From u In Context.SE_USER
                '        From p In Context.SE_REPORT
                '        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                '        Select New Se_ReportDTO With {
                '            .ID = p.ID,
                '            .CODE = p.CODE,
                '            .NAME = p.NAME,
                '            .MODULE_ID = p.MODULE_ID}
                query = From re In Context.SE_REPORT
                        From p In Context.SE_USER_REPORT.Where(Function(f) f.SE_REPORT_ID = re.ID).DefaultIfEmpty
                        From u In Context.SE_USER.Where(Function(f) f.ID = p.SE_USER_ID).DefaultIfEmpty
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And re.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = re.ID,
                            .CODE = re.CODE,
                            .NAME = re.NAME,
                            .MODULE_ID = re.MODULE_ID}
            Else
                query = From p In Context.SE_REPORT
                        Where p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .MODULE_ID = p.MODULE_ID}

            End If

            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTraining")
            Throw ex
        End Try
    End Function

#End Region



End Class
