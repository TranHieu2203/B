Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "Hold Salary"

    Public Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Dim query = From p In Context.PA_HOLD_SALARY
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID)

            query = query.Where(Function(f) f.p.PERIOD_ID = PeriodId)
            Dim lst = query.Select(Function(p) New PAHoldSalaryDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .FULLNAME_VN = p.e.FULLNAME_VN,
                                       .FULLNAME_EN = p.e.FULLNAME_EN,
                                       .ORG_NAME = p.org.NAME_VN,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.pe.PERIOD_NAME,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_BY = p.p.CREATED_BY})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

            'Using cls As New DataAccess.QueryData
            '    Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.GET_HOLDSALARY_LIST",
            '                               New With {.P_ORG_ID = OrgId,
            '                                         .P_PERIOD_ID = PeriodId,
            '                                         .P_USERNAME = log.Username,
            '                                         .P_CUR = cls.OUT_CURSOR})
            '    Total = dtData.ToList(Of PAHoldSalaryDTO)().Count
            '    Return dtData.AsEnumerable().ToList().Skip(PageIndex * PageSize).Take(PageSize)
            'End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Try
            For Each item As PAHoldSalaryDTO In objPeriod
                Dim exists = (From p In Context.PA_HOLD_SALARY Where p.PERIOD_ID = item.PERIOD_ID And p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                If exists IsNot Nothing AndAlso exists.ID > 0 Then
                    exists.EMPLOYEE_ID = item.EMPLOYEE_ID
                    exists.PERIOD_ID = item.PERIOD_ID
                Else
                    Dim objHoldSalaryData As New PA_HOLD_SALARY
                    objHoldSalaryData.ID = Utilities.GetNextSequence(Context, Context.PA_HOLD_SALARY.EntitySet.Name)
                    objHoldSalaryData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    objHoldSalaryData.PERIOD_ID = item.PERIOD_ID
                    Context.PA_HOLD_SALARY.AddObject(objHoldSalaryData)
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
        Try

            For Each item As Decimal In lstDelete
                Dim DeleteData = (From d In Context.PA_HOLD_SALARY Where d.ID = item).SingleOrDefault
                If DeleteData.ID > 0 Then
                    Context.PA_HOLD_SALARY.DeleteObject(DeleteData)
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "SALARYTYPE_GROUP"

    Public Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO)

        Try
            Dim query = From p In Context.PA_SALARYTYPE_GROUP
                        Join t In Context.PA_SALARY_TYPE On p.SAL_TYPE_ID Equals t.ID
                        From o In Context.PA_SALARY_GROUP.Where(Function(e) e.ID = p.SAL_GROUP_ID).DefaultIfEmpty()
                        Where (p.IS_DELETED = 0)

            Dim lst = query.Select(Function(e) New SalaryType_GroupDTO With {
                                       .ID = e.p.ID,
                                       .SAL_TYPE_ID = e.p.SAL_TYPE_ID,
                                       .SAL_TYPE_NAME = e.t.NAME,
                                       .SAL_GROUP_ID = e.p.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = e.o.NAME,
                                       .IS_DIRECT_SALARY = e.p.IS_DIRECT_SALARY,
                                       .REMARK = e.p.REMARK,
                                       .ACTFLG = If(e.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .ORDERS = e.p.ORDERS,
                                       .CREATED_DATE = e.p.CREATED_DATE})
            If _filter.SAL_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.SAL_TYPE_NAME.ToUpper.Contains(_filter.SAL_TYPE_NAME.ToUpper))
            End If
            If _filter.SAL_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.SAL_GROUP_NAME.ToUpper.Contains(_filter.SAL_GROUP_NAME.ToUpper))
            End If

            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryType_GroupData As New PA_SALARYTYPE_GROUP
        Try
            objSalaryType_GroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARYTYPE_GROUP.EntitySet.Name)
            objSalaryType_GroupData.SAL_TYPE_ID = objSalaryType_Group.SAL_TYPE_ID
            objSalaryType_GroupData.SAL_GROUP_ID = objSalaryType_Group.SAL_GROUP_ID
            objSalaryType_GroupData.REMARK = objSalaryType_Group.REMARK.Trim
            objSalaryType_GroupData.IS_DIRECT_SALARY = objSalaryType_Group.IS_DIRECT_SALARY
            objSalaryType_GroupData.ACTFLG = objSalaryType_Group.ACTFLG
            objSalaryType_GroupData.ORDERS = objSalaryType_Group.ORDERS
            objSalaryType_GroupData.IS_DELETED = objSalaryType_Group.IS_DELETED
            Context.PA_SALARYTYPE_GROUP.AddObject(objSalaryType_GroupData)
            Context.SaveChanges(log)
            gID = objSalaryType_GroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryType_GroupData As New PA_SALARYTYPE_GROUP With {.ID = objSalaryType_Group.ID}
        Try
            Context.PA_SALARYTYPE_GROUP.Attach(objSalaryType_GroupData)
            objSalaryType_GroupData.SAL_TYPE_ID = objSalaryType_Group.SAL_TYPE_ID
            objSalaryType_GroupData.SAL_GROUP_ID = objSalaryType_Group.SAL_GROUP_ID
            objSalaryType_GroupData.REMARK = objSalaryType_Group.REMARK.Trim
            objSalaryType_GroupData.IS_DIRECT_SALARY = objSalaryType_Group.IS_DIRECT_SALARY
            objSalaryType_GroupData.ORDERS = objSalaryType_Group.ORDERS
            Context.SaveChanges(log)
            gID = objSalaryType_GroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO)
        Dim query
        Try
            If Not _validate.IS_DIRECT_SALARY Then     'THeo ngach bac
                query = (From p In Context.PA_SALARYTYPE_GROUP
                         Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                         And p.IS_DELETED = 0 _
                         And p.IS_DIRECT_SALARY = True).FirstOrDefault
                If query IsNot Nothing Then
                    Return False
                End If
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARYTYPE_GROUP
                             Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                             And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARYTYPE_GROUP
                             Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                             And p.IS_DELETED = 0 _
                             And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                query = (From p In Context.PA_SALARYTYPE_GROUP
                         Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                         And p.IS_DIRECT_SALARY <> True _
                         And p.IS_DELETED = 0
                             ).FirstOrDefault
                If query IsNot Nothing Then
                    Return False
                End If
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARYTYPE_GROUP
                             Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                             And p.IS_DIRECT_SALARY = _validate.IS_DIRECT_SALARY _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARYTYPE_GROUP
                             Where p.SAL_TYPE_ID = _validate.SAL_TYPE_ID _
                             And p.IS_DELETED = 0 _
                             And p.IS_DIRECT_SALARY = _validate.IS_DIRECT_SALARY).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARYTYPE_GROUP)
        Try
            lstData = (From p In Context.PA_SALARYTYPE_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of PA_SALARYTYPE_GROUP)
        Try
            lstData = (From p In Context.PA_SALARYTYPE_GROUP Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_DELETED = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryType_Group(ByVal lstSalaryType_Group() As SalaryType_GroupDTO) As Boolean
        Dim lstSalaryType_GroupData As List(Of PA_SALARYTYPE_GROUP)
        Dim lstIDSalaryType_Group As List(Of Decimal) = (From p In lstSalaryType_Group.ToList Select p.ID).ToList
        Try
            lstSalaryType_GroupData = (From p In Context.PA_SALARYTYPE_GROUP Where lstIDSalaryType_Group.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryType_GroupData.Count - 1
                Context.PA_SALARYTYPE_GROUP.DeleteObject(lstSalaryType_GroupData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "SalaryPlanning"

    Public Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_PA_SALARY_FUND.GET_PA_SALARY_PLANNING",
                                 New With {.PV_USERNAME = log.Username,
                                           .PV_ORG_ID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                           .P_YEAR = _filter.YEAR,
                                           .P_MANNING_ORG_ID = _filter.MANNING_ORG_ID,
                                           .PV_CUR = cls.OUT_CURSOR})
                'dtData = cls.ExecuteStore("PKG_RECRUITMENT.GET_PA_SALARY_PLANNING_BY_ID",
                '                New With {.P_MANNING_ORG_ID = _filter.MANNING_ORG_ID,
                '                          .P_ORG_ID = _param.ORG_ID,
                '                          .P_YEAR = _filter.YEAR,
                '                          .P_CUR = cls.OUT_CURSOR})
            End Using

            Return dtData
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanning")
            Throw ex
        End Try

    End Function

    Public Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO

        Try

            Dim query = From p In Context.PA_SALARY_PLANNING
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New PASalaryPlanningDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = o.NAME_VN,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = t.NAME_VN,
                                       .EMP_NUMBER = p.EMP_NUMBER,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim obj = query.FirstOrDefault
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanningByID")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryPlanningData As New PA_SALARY_PLANNING
        Try
            Dim exists = (From r In Context.PA_SALARY_PLANNING _
                          Where r.YEAR = objSalaryPlanning.YEAR And
                          r.ORG_ID = objSalaryPlanning.ORG_ID And
                          r.TITLE_ID = objSalaryPlanning.TITLE_ID).Any
            If exists Then
                Dim obj = (From r In Context.PA_SALARY_PLANNING _
                           Where r.YEAR = objSalaryPlanning.YEAR And
                           r.ORG_ID = objSalaryPlanning.ORG_ID And
                           r.TITLE_ID = objSalaryPlanning.TITLE_ID).FirstOrDefault
                obj.EMP_NUMBER = objSalaryPlanning.EMP_NUMBER
                gID = obj.ID
            Else

                objSalaryPlanningData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_PLANNING.EntitySet.Name)
                objSalaryPlanningData.YEAR = objSalaryPlanning.YEAR
                objSalaryPlanningData.ORG_ID = objSalaryPlanning.ORG_ID
                objSalaryPlanningData.TITLE_ID = objSalaryPlanning.TITLE_ID
                objSalaryPlanningData.EMP_NUMBER = objSalaryPlanning.EMP_NUMBER
                objSalaryPlanningData.MONTH = objSalaryPlanning.MONTH

                Context.PA_SALARY_PLANNING.AddObject(objSalaryPlanningData)
                gID = objSalaryPlanningData.ID
            End If
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertSalaryPlanning")
            Throw ex
        End Try
    End Function

    Public Function ImportSalaryPlanning(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryPlanningData As PA_SALARY_PLANNING
        Try
            For Each row As DataRow In dtData.Rows
                Dim year = Utilities.Obj2Decima(row("YEAR"), Nothing)
                Dim orgID = Utilities.Obj2Decima(row("ORG_ID"), Nothing)
                Dim titleID = Utilities.Obj2Decima(row("TITLE_ID"), Nothing)
                Dim exists = (From r In Context.PA_SALARY_PLANNING _
                              Where r.YEAR = year And
                              r.ORG_ID = orgID And
                              r.TITLE_ID = titleID).Any
                If exists Then
                    Dim obj = (From r In Context.PA_SALARY_PLANNING _
                               Where r.YEAR = year And
                               r.ORG_ID = orgID And
                               r.TITLE_ID = titleID).FirstOrDefault

                    obj.EMP_NUMBER = Utilities.Obj2Decima(row("EMP_NUMBER"), Nothing)
                    gID = obj.ID
                Else
                    objSalaryPlanningData = New PA_SALARY_PLANNING
                    objSalaryPlanningData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_PLANNING.EntitySet.Name)
                    objSalaryPlanningData.YEAR = Utilities.Obj2Decima(row("YEAR"), Nothing)
                    objSalaryPlanningData.MONTH = Utilities.Obj2Decima(row("MONTH"), Nothing)
                    objSalaryPlanningData.ORG_ID = Utilities.Obj2Decima(row("ORG_ID"), Nothing)
                    objSalaryPlanningData.TITLE_ID = Utilities.Obj2Decima(row("TITLE_ID"), Nothing)
                    objSalaryPlanningData.EMP_NUMBER = Utilities.Obj2Decima(row("EMP_NUMBER"), Nothing)
                    Context.PA_SALARY_PLANNING.AddObject(objSalaryPlanningData)
                    gID = objSalaryPlanningData.ID
                End If
                Context.SaveChanges(log)
            Next
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertSalaryPlanning")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryPlanningData As New PA_SALARY_PLANNING With {.ID = objSalaryPlanning.ID}
        Try
            objSalaryPlanningData = (From p In Context.PA_SALARY_PLANNING Where p.ID = objSalaryPlanning.ID).SingleOrDefault

            objSalaryPlanningData.YEAR = objSalaryPlanning.YEAR
            objSalaryPlanningData.ORG_ID = objSalaryPlanning.ORG_ID
            objSalaryPlanningData.TITLE_ID = objSalaryPlanning.TITLE_ID
            objSalaryPlanningData.MONTH = objSalaryPlanning.MONTH
            objSalaryPlanningData.EMP_NUMBER = objSalaryPlanning.EMP_NUMBER
            Context.SaveChanges(log)
            gID = objSalaryPlanningData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifySalaryPlanning")
            Throw ex
        End Try

    End Function

    Public Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryPlanningData As List(Of PA_SALARY_PLANNING)
        Try
            lstSalaryPlanningData = (From p In Context.PA_SALARY_PLANNING Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstSalaryPlanningData.Count - 1
                Context.PA_SALARY_PLANNING.DeleteObject(lstSalaryPlanningData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteSalaryPlanning")
            Throw ex
        End Try

    End Function

    Public Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_TITLE_BYORG",
                                           New With {.P_ORGID = orgID,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSalaryPlanningImport(ByVal org_id As Decimal, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dsData As DataSet = cls.ExecuteStore("PKG_PA_SALARY_FUND.GET_PA_SALARY_PLANNING_IMPORT",
                                           New With {.PV_ORG_ID = org_id,
                                                     .PV_USERNAME = log.Username,
                                                     .PV_CUR = cls.OUT_CURSOR,
                                                     .PV_CUR1 = cls.OUT_CURSOR}, False)
            Return dsData
        End Using
        Return Nothing
    End Function

    Public Function GET_EXPORT_PA_EMP_FORMULER(ByVal org_id As Decimal, ByVal IS_DISSOLVE As Decimal, ByVal log As UserLog) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dsData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_EXPORT_PA_EMP_FORMULER",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORG_ID = org_id,
                                                     .P_IS_DISSOLVE = IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR,
                                                     .P_CUR4 = cls.OUT_CURSOR,
                                                     .P_CUR5 = cls.OUT_CURSOR}, False)
            Return dsData
        End Using
        Return Nothing
    End Function

#End Region

#Region "SalaryTracker"

    Public Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

        Try
            Dim dsData As DataSet
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_PA_SALARY_FUND.GET_PA_SALARY_TRACKER",
                                 New With {.PV_USERNAME = log.Username,
                                           .PV_ORG_ID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                           .P_YEAR = _filter.YEAR,
                                           .P_MONTH = _filter.MONTH,
                                           .PV_CUR = cls.OUT_CURSOR,
                                           .PV_CUR1 = cls.OUT_CURSOR}, False)
            End Using

            Return dsData
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanning")
            Throw ex
        End Try

    End Function

    Public Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

        Try
            Dim dsData As DataSet
            Using cls As New DataAccess.QueryData
                dsData = cls.ExecuteStore("PKG_PA_SALARY_FUND.GET_PA_SALARY_EMP_TRACKER",
                                 New With {.PV_USERNAME = log.Username,
                                           .PV_ORG_ID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                           .P_YEAR = _filter.YEAR,
                                           .PV_CUR = cls.OUT_CURSOR,
                                           .PV_CUR1 = cls.OUT_CURSOR}, False)
            End Using

            Return dsData
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetSalaryPlanning")
            Throw ex
        End Try

    End Function

#End Region

#Region "PA_SETUP_LDTT_NV_QLCH"
    Public Function GET_PA_SETUP_LDTT_NV_QLCH(ByVal _filter As PA_SETUP_LDTTT_NV_QLCH_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_LDTTT_NV_QLCH_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_LDTTT_NV_QLCH
                        From brand In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                        From staff In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STAFF_OBJECT).DefaultIfEmpty
                        From grade In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADE).DefaultIfEmpty
                        Select New PA_SETUP_LDTTT_NV_QLCH_DTO With {
                                   .ID = p.ID,
                                   .BRAND = p.BRAND,
                                   .BRAND_NAME = brand.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .STAFF_OBJECT = p.STAFF_OBJECT,
                                   .STAFF_OBJECT_NAME = staff.NAME_VN,
                                   .FROM_RATE = p.FROM_RATE,
                                   .TO_RATE = p.TO_RATE,
                                   .GRADE = p.GRADE,
                                   .GRADE_NAME = grade.NAME_VN,
                                   .LDTTT = p.LDTTT,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_LDTTT_NV_QLCH
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_LDTTT_NV_QLCH.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_OBJECT = obj.STAFF_OBJECT
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.GRADE = obj.GRADE
            objData.LDTTT = obj.LDTTT
            objData.NOTE = obj.NOTE
            Context.PA_SETUP_LDTTT_NV_QLCH.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_LDTTT_NV_QLCH
        Try
            objData = (From p In Context.PA_SETUP_LDTTT_NV_QLCH Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_OBJECT = obj.STAFF_OBJECT
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.GRADE = obj.GRADE
            objData.LDTTT = obj.LDTTT
            objData.NOTE = obj.NOTE
            objData.BRAND = obj.BRAND
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePA_SETUP_LDTT_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_LDTTT_NV_QLCH)
        Try

            lstData = (From p In Context.PA_SETUP_LDTTT_NV_QLCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_LDTTT_NV_QLCH.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidatePA_SETUP_LDTT_NV_QLCH(ByVal obj As PA_SETUP_LDTTT_NV_QLCH_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_LDTTT_NV_QLCH Where f.ID <> obj.ID AndAlso f.STAFF_OBJECT = obj.STAFF_OBJECT _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.BRAND = obj.BRAND _
                                                                           AndAlso f.GRADE = obj.GRADE _
                                                                           AndAlso f.FROM_RATE = obj.FROM_RATE _
                                                                           AndAlso f.TO_RATE = obj.TO_RATE)
            Return query.Any

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function GET_PA_SETUP_LDTT_NV_QLCH_DATA_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_PA_SETUP_LDTT_NV_QLCH",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR,
                                                                   .P_CUR3 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function IMPORT_PA_SETUP_LDTT_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_SETUP_LDTT_NV_QLCH",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "PA_Setup_HeSoMR_NV_QLCH"
    Public Function GET_PA_SETUP_HESOMR_NV_QLCH(ByVal _filter As PA_SETUP_HESOMR_NV_QLCH_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HESOMR_NV_QLCH_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_HESOMR_NV_QLCH
                        From brand In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                        From staff In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STAFF_OBJECT).DefaultIfEmpty
                        Select New PA_SETUP_HESOMR_NV_QLCH_DTO With {
                                   .ID = p.ID,
                                   .BRAND = p.BRAND,
                                   .BRAND_NAME = brand.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .STAFF_OBJECT = p.STAFF_OBJECT,
                                   .STAFF_OBJECT_NAME = staff.NAME_VN,
                                   .FROM_MRA = p.FROM_MRA,
                                   .TO_MRA = p.TO_MRA,
                                   .HESO_MR = p.HESO_MR,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_HESOMR_NV_QLCH
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_HESOMR_NV_QLCH.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_OBJECT = obj.STAFF_OBJECT
            objData.FROM_MRA = obj.FROM_MRA
            objData.TO_MRA = obj.TO_MRA
            objData.HESO_MR = obj.HESO_MR
            objData.NOTE = obj.NOTE
            Context.PA_SETUP_HESOMR_NV_QLCH.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_HESOMR_NV_QLCH
        Try
            objData = (From p In Context.PA_SETUP_HESOMR_NV_QLCH Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_OBJECT = obj.STAFF_OBJECT
            objData.FROM_MRA = obj.FROM_MRA
            objData.TO_MRA = obj.TO_MRA
            objData.HESO_MR = obj.HESO_MR
            objData.NOTE = obj.NOTE
            objData.BRAND = obj.BRAND
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SETUP_HESOMR_NV_QLCH(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_HESOMR_NV_QLCH)
        Try

            lstData = (From p In Context.PA_SETUP_HESOMR_NV_QLCH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_HESOMR_NV_QLCH.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_SETUP_HESOMR_NV_QLCH(ByVal obj As PA_SETUP_HESOMR_NV_QLCH_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_HESOMR_NV_QLCH Where f.ID <> obj.ID AndAlso f.STAFF_OBJECT = obj.STAFF_OBJECT _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.BRAND = obj.BRAND _
                                                                           AndAlso f.FROM_MRA = obj.FROM_MRA _
                                                                           AndAlso f.TO_MRA = obj.TO_MRA).Any
            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region
#Region "PA_SETUP_INDEX"
    Public Function GET_PA_SETUP_INDEX(ByVal _filter As PA_SETUP_INDEX_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_INDEX_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_INDEX
                        From brand In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                        From index In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INDEX_TYPE).DefaultIfEmpty
                        Select New PA_SETUP_INDEX_DTO With {
                                   .ID = p.ID,
                                   .BRAND = p.BRAND,
                                   .BRAND_NAME = brand.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .INDEX_TYPE = p.INDEX_TYPE,
                                   .INDEX_TYPE_NAME = index.NAME_VN,
                                   .FROM_COMPLETION_RATE = p.FROM_COMPLETION_RATE,
                                   .TO_COMPLETION_RATE = p.TO_COMPLETION_RATE,
                                   .FACTOR = p.FACTOR,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE,
                                   .IS_GET_TLHT = p.IS_GET_TLHT}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_INDEX
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_INDEX.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.INDEX_TYPE = obj.INDEX_TYPE
            objData.FROM_COMPLETION_RATE = obj.FROM_COMPLETION_RATE
            objData.TO_COMPLETION_RATE = obj.TO_COMPLETION_RATE
            objData.FACTOR = obj.FACTOR
            objData.NOTE = obj.NOTE
            objData.IS_GET_TLHT = obj.IS_GET_TLHT
            Context.PA_SETUP_INDEX.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_INDEX
        Try
            objData = (From p In Context.PA_SETUP_INDEX Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.INDEX_TYPE = obj.INDEX_TYPE
            objData.FROM_COMPLETION_RATE = obj.FROM_COMPLETION_RATE
            objData.TO_COMPLETION_RATE = obj.TO_COMPLETION_RATE
            objData.FACTOR = obj.FACTOR
            objData.NOTE = obj.NOTE
            objData.IS_GET_TLHT = obj.IS_GET_TLHT
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SETUP_INDEX(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_INDEX)
        Try
            lstData = (From p In Context.PA_SETUP_INDEX Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_INDEX.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_SETUP_INDEX(ByVal obj As PA_SETUP_INDEX_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_INDEX Where f.ID <> obj.ID AndAlso f.INDEX_TYPE = obj.INDEX_TYPE _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.BRAND = obj.BRAND _
                                                                           AndAlso f.FACTOR = obj.FACTOR).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function EXPORT_PA_SETUP_INDEX(ByVal sLang As String) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dsData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.EXPORT_PA_SETUP_INDEX",
                                           New With {.P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dsData
        End Using
        Return Nothing
    End Function
    Public Function IMPORT_PA_SETUP_INDEX(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_SETUP_INDEX",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "PA_SETUP_HSTDT"
    Public Function GET_PA_SETUP_HSTDT(ByVal _filter As PA_SETUP_HSTDT_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_HSTDT_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_HSTDT
                        From brand In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                        From modelShop In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MODELSHOP).DefaultIfEmpty
                        From staffGroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STAFF_GROUP).DefaultIfEmpty
                        Select New PA_SETUP_HSTDT_DTO With {
                                   .ID = p.ID,
                                   .BRAND = p.BRAND,
                                   .BRAND_NAME = brand.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .STAFF_GROUP = p.STAFF_GROUP,
                                   .STAFF_GROUP_NAME = staffGroup.NAME_VN,
                                   .FROM_RATE = p.FROM_RATE,
                                   .TO_RATE = p.TO_RATE,
                                   .MODELSHOP = p.MODELSHOP,
                                   .MODELSHOP_NAME = modelShop.NAME_VN,
                                   .NOTE = p.NOTE,
                                   .COEFFICIENT = p.COEFFICIENT,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_HSTDT
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_HSTDT.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_GROUP = obj.STAFF_GROUP
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.MODELSHOP = obj.MODELSHOP
            objData.COEFFICIENT = obj.COEFFICIENT
            objData.NOTE = obj.NOTE
            Context.PA_SETUP_HSTDT.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_HSTDT
        Try
            objData = (From p In Context.PA_SETUP_HSTDT Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.STAFF_GROUP = obj.STAFF_GROUP
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.MODELSHOP = obj.MODELSHOP
            objData.COEFFICIENT = obj.COEFFICIENT
            objData.NOTE = obj.NOTE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SETUP_HSTDT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_HSTDT)
        Try
            lstData = (From p In Context.PA_SETUP_HSTDT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_HSTDT.DeleteObject(lstData(index))
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_SETUP_HSTDT(ByVal obj As PA_SETUP_HSTDT_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_HSTDT Where f.ID <> obj.ID AndAlso f.MODELSHOP = obj.MODELSHOP _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.BRAND = obj.BRAND _
                                                                           AndAlso f.STAFF_GROUP = obj.STAFF_GROUP _
                                                                           AndAlso f.COEFFICIENT = obj.COEFFICIENT).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region

#Region "PA_SETUP_MR_BQN"
    Public Function GET_PA_SETUP_MR_BQN(ByVal _filter As PA_SETUP_MR_BQN_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_MR_BQN_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_MR_BQN
                        From brand In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                        Select New PA_SETUP_MR_BQN_DTO With {
                                   .ID = p.ID,
                                   .BRAND = p.BRAND,
                                   .BRAND_NAME = brand.NAME_VN,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .AVG_MR = p.AVG_MR,
                                   .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_MR_BQN
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_MR_BQN.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.AVG_MR = obj.AVG_MR
            Context.PA_SETUP_MR_BQN.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_MR_BQN
        Try
            objData = (From p In Context.PA_SETUP_MR_BQN Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.AVG_MR = obj.AVG_MR
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SETUP_MR_BQN(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_MR_BQN)
        Try

            lstData = (From p In Context.PA_SETUP_MR_BQN Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_MR_BQN.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_SETUP_MR_BQN(ByVal obj As PA_SETUP_MR_BQN_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_MR_BQN Where f.ID <> obj.ID AndAlso f.BRAND = obj.BRAND _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.AVG_MR = obj.AVG_MR).Any
            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region
#Region "PA_SETUP_RATE_DTTT"
    Public Function GET_PA_SETUP_RATE_DTTT(ByVal _filter As PA_SETUP_RATE_DTTT_DTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTTT_DTO)

        Try
            Dim query = From p In Context.PA_SETUP_RATE_DTTT
                        From branch In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRANCH).DefaultIfEmpty
                        From shop In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SHOP_TYPE).DefaultIfEmpty
                        From store In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.STORE_CODE).DefaultIfEmpty
                        Select New PA_SETUP_RATE_DTTT_DTO With {
                                                               .ID = p.ID,
                                                               .BRANCH = p.BRANCH,
                                                               .BRANCH_NAME = branch.NAME_VN,
                                                               .EFFECT_DATE = p.EFFECT_DATE,
                                                               .SHOP_TYPE = p.SHOP_TYPE,
                                                               .SHOP_TYPE_NAME = shop.NAME_VN,
                                                               .STORE_CODE = store.ID,
                                                               .STORE_CODE_NAME = store.NAME_VN,
                                                               .RATE = p.RATE,
                                                               .NOTE = p.NOTE,
                                                               .CREATED_DATE = p.CREATED_DATE,
                                                               .IS_DOANHTHU = p.IS_DOANHTHU,
                                                               .IS_DOANHTHU_TEXT = If(p.IS_DOANHTHU = -1, "X", "")}

            Dim lst = query

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE.Value)
            End If
            If Not String.IsNullOrEmpty(_filter.BRANCH_NAME) Then
                lst = lst.Where(Function(f) f.BRANCH_NAME.ToUpper.Contains(_filter.BRANCH_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.SHOP_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.SHOP_TYPE_NAME.ToUpper.Contains(_filter.SHOP_TYPE_NAME.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.STORE_CODE_NAME) Then
                lst = lst.Where(Function(f) f.STORE_CODE_NAME.ToUpper.Contains(_filter.STORE_CODE_NAME.ToUpper))
            End If
            If _filter.RATE.HasValue Then
                lst = lst.Where(Function(f) f.RATE = _filter.RATE.Value)
            End If
            If Not String.IsNullOrEmpty(_filter.IS_DOANHTHU_TEXT) Then
                lst = lst.Where(Function(f) f.IS_DOANHTHU_TEXT.ToUpper.Contains(_filter.IS_DOANHTHU_TEXT.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function INSERT_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_RATE_DTTT
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_RATE_DTTT.EntitySet.Name)
            objData.BRANCH = obj.BRANCH
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.RATE = obj.RATE
            objData.SHOP_TYPE = obj.SHOP_TYPE
            objData.STORE_CODE = obj.STORE_CODE
            objData.NOTE = obj.NOTE
            objData.IS_DOANHTHU = obj.IS_DOANHTHU
            Context.PA_SETUP_RATE_DTTT.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function MODIFY_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As PA_SETUP_RATE_DTTT
        Try
            objData = (From p In Context.PA_SETUP_RATE_DTTT Where p.ID = obj.ID).FirstOrDefault
            objData.ID = obj.ID
            objData.BRANCH = obj.BRANCH
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.RATE = obj.RATE
            objData.SHOP_TYPE = obj.SHOP_TYPE
            objData.STORE_CODE = obj.STORE_CODE
            objData.NOTE = obj.NOTE
            objData.IS_DOANHTHU = obj.IS_DOANHTHU
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DELETE_PA_SETUP_RATE_DTTT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of PA_SETUP_RATE_DTTT)
        Try

            lstData = (From p In Context.PA_SETUP_RATE_DTTT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_RATE_DTTT.DeleteObject(lstData(index))
            Next

            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function VALIDATE_PA_SETUP_RATE_DTTT(ByVal obj As PA_SETUP_RATE_DTTT_DTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_RATE_DTTT Where f.ID <> obj.ID AndAlso f.BRANCH = obj.BRANCH _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.STORE_CODE = obj.STORE_CODE _
                                                                           AndAlso f.RATE = obj.RATE).Any
            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region

#Region "Setup NKL"
    Public Function GetListPaSetupNKL(ByVal _filter As PA_SETUP_NKLDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_NKLDTO)

        Try
            Dim query = From p In Context.PA_SETUP_NKL
                        Select New PA_SETUP_NKLDTO With {.ID = p.ID,
                                                         .VALUE_FROM = p.VALUE_FROM,
                                                         .VALUE_TO = p.VALUE_TO,
                                                         .MONTH = p.MONTH,
                                                         .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.VALUE_TO.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_TO = _filter.VALUE_TO.Value)
            End If

            If _filter.VALUE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_FROM = _filter.VALUE_FROM.Value)
            End If

            If _filter.MONTH.HasValue Then
                lst = lst.Where(Function(f) f.MONTH = _filter.MONTH.Value)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean
        Dim objData As New PA_SETUP_NKL
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_NKL.EntitySet.Name)
            objData.VALUE_FROM = obj.VALUE_FROM
            objData.VALUE_TO = obj.VALUE_TO
            objData.MONTH = obj.MONTH
            Context.PA_SETUP_NKL.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPaSetupNKL(ByVal obj As PA_SETUP_NKLDTO, ByVal log As UserLog) As Boolean
        Dim objData As PA_SETUP_NKL
        Try
            objData = (From p In Context.PA_SETUP_NKL Where p.ID = obj.ID).FirstOrDefault
            objData.VALUE_FROM = obj.VALUE_FROM
            objData.VALUE_TO = obj.VALUE_TO
            objData.MONTH = obj.MONTH
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePaSetupNKL(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_NKL)
        Try

            lstData = (From p In Context.PA_SETUP_NKL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_NKL.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidatePaSetupNKL(ByVal obj As PA_SETUP_NKLDTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_NKL Where f.ID <> obj.ID AndAlso _
                         ((f.VALUE_FROM <= obj.VALUE_FROM AndAlso f.VALUE_TO >= obj.VALUE_FROM) OrElse _
                          (f.VALUE_FROM <= obj.VALUE_TO AndAlso f.VALUE_TO >= obj.VALUE_TO) OrElse _
                          (obj.VALUE_FROM <= f.VALUE_FROM AndAlso obj.VALUE_TO >= f.VALUE_FROM) OrElse _
                          (obj.VALUE_FROM <= f.VALUE_TO AndAlso obj.VALUE_TO >= f.VALUE_TO)))
            Return query.Any

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region

#Region "Classification"
    Public Function GetPaClassifications(ByVal _filter As PA_CLASSIFICATIONDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_CLASSIFICATIONDTO)

        Try
            Dim query = From p In Context.PA_CLASSIFICATION
                        Select New PA_CLASSIFICATIONDTO With {.ID = p.ID,
                                                              .VALUE_FROM = p.VALUE_FROM,
                                                              .VALUE_TO = p.VALUE_TO,
                                                              .CODE = p.CODE,
                                                              .NAME = p.NAME,
                                                              .EFFECT_DATE = p.EFFECT_DATE,
                                                              .FACTOR = p.FACTOR,
                                                              .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.VALUE_TO.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_TO = _filter.VALUE_TO.Value)
            End If

            If _filter.VALUE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.VALUE_FROM = _filter.VALUE_FROM.Value)
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE.Value)
            End If
            If _filter.FACTOR.HasValue Then
                lst = lst.Where(Function(f) f.FACTOR = _filter.FACTOR.Value)
            End If
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean
        Dim objData As New PA_CLASSIFICATION
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_CLASSIFICATION.EntitySet.Name)
            objData.VALUE_FROM = obj.VALUE_FROM
            objData.VALUE_TO = obj.VALUE_TO
            objData.CODE = obj.CODE
            objData.NAME = obj.NAME
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.FACTOR = obj.FACTOR
            Context.PA_CLASSIFICATION.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPaClassification(ByVal obj As PA_CLASSIFICATIONDTO, ByVal log As UserLog) As Boolean
        Dim objData As PA_CLASSIFICATION
        Try
            objData = (From p In Context.PA_CLASSIFICATION Where p.ID = obj.ID).FirstOrDefault
            objData.VALUE_FROM = obj.VALUE_FROM
            objData.VALUE_TO = obj.VALUE_TO
            objData.CODE = obj.CODE
            objData.NAME = obj.NAME
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.FACTOR = obj.FACTOR
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePaClassification(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_CLASSIFICATION)
        Try

            lstData = (From p In Context.PA_CLASSIFICATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.PA_CLASSIFICATION.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateClassification(ByVal obj As PA_CLASSIFICATIONDTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_NKL Where f.ID <> obj.ID AndAlso
                         ((f.VALUE_FROM <= obj.VALUE_FROM AndAlso f.VALUE_TO >= obj.VALUE_FROM) OrElse
                          (f.VALUE_FROM <= obj.VALUE_TO AndAlso f.VALUE_TO >= obj.VALUE_TO) OrElse
                          (obj.VALUE_FROM <= f.VALUE_FROM AndAlso obj.VALUE_TO >= f.VALUE_FROM) OrElse
                          (obj.VALUE_FROM <= f.VALUE_TO AndAlso obj.VALUE_TO >= f.VALUE_TO)))
            Return query.Any

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
#End Region
End Class

