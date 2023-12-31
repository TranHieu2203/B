﻿Imports System.IO
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class RecruitmentRepository
    Function ConvertStringToDate(ByVal strDate As String) As Date
        Try
            Dim year As Integer = Now.Year
            Dim month As Integer = Now.Month
            Dim day As Integer = Now.Day
            If strDate.Split("/").Count <> 3 Then Return Nothing
            year = Integer.Parse(strDate.Split("/")(2))
            month = Integer.Parse(strDate.Split("/")(1))
            day = Integer.Parse(strDate.Split("/")(0))
            Return New Date(year, month, day)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function SetValueObject(ByVal pi As PropertyInfo, ByVal item As Object) As Object
        Dim value As Object
        Try
            Select Case pi.PropertyType
                Case GetType(DateTime), GetType(DateTime?)
                    value = ConvertStringToDate(item.ToString())
                Case GetType(Double), GetType(Double?)
                    Double.TryParse(item, value)
                Case GetType(Decimal), GetType(Decimal?)
                    Decimal.TryParse(item, value)
                Case GetType(Integer), GetType(Integer?)
                    Integer.TryParse(item, value)
                Case GetType(Boolean), GetType(Boolean?)
                    Boolean.TryParse(item, value)
                Case Else
                    value = item
            End Select
            Return value
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Create ChienNV 
    'Create date: 13-NOV-2019
    'Import hồ sơ ứng viên
    Public Function ImportRC(ByVal Data As DataTable, ByVal ProGramID As Decimal, ByVal log As UserLog) As Boolean
        Dim oPropsRC_CANDIDATE_EXPECT() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE_CV() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE_HEALTH() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE_EDUCATION() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE_TRAINSINGER() As PropertyInfo = Nothing
        Dim oPropsRC_CANDIDATE_BEFOREWT() As PropertyInfo = Nothing
        'Dim oProps() As PropertyInfo = Nothing
        Dim objRC_CANDIDATE_EXPECT As New RC_CANDIDATE_EXPECT
        Dim objRC_CANDIDATE As New RC_CANDIDATE
        Dim objRC_CANDIDATE_CV As New RC_CANDIDATE_CV
        Dim objRC_CANDIDATE_HEALTH As New RC_CANDIDATE_HEALTH
        Dim objRC_CANDIDATE_EDUCATION As New RC_CANDIDATE_EDUCATION

        Dim programData As New ProgramDTO With {.ID = ProGramID}
        programData = GetProgramByID(programData)

        objRC_CANDIDATE.ORG_ID = programData.ORG_ID
        objRC_CANDIDATE.TITLE_ID = programData.TITLE_ID

        Try
            'ID THAM CHIEU 
            Dim CANDIDATE_ID As Decimal = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE.EntitySet.Name)
            objRC_CANDIDATE_EXPECT.CANDIDATE_ID = CANDIDATE_ID
            objRC_CANDIDATE.ID = CANDIDATE_ID
            objRC_CANDIDATE_CV.CANDIDATE_ID = CANDIDATE_ID
            objRC_CANDIDATE_HEALTH.CANDIDATE_ID = CANDIDATE_ID
            objRC_CANDIDATE_EDUCATION.CANDIDATE_ID = CANDIDATE_ID
            For Each Col As DataColumn In Data.Columns
                Try
                    Select Case Col.ColumnName.Split("#")(0).ToUpper
                        Case "RC_CANDIDATE_EXPECT"
                            oPropsRC_CANDIDATE_EXPECT = objRC_CANDIDATE_EXPECT.GetType().GetProperties()
                            Dim pi As PropertyInfo = (From l In oPropsRC_CANDIDATE_EXPECT Where l.Name.ToUpper.Equals(Col.ColumnName.Split("#")(1).ToUpper) Select l).FirstOrDefault()
                            If pi IsNot Nothing Then
                                Dim value As Object
                                value = SetValueObject(pi, Data(0)(Col.ColumnName))
                                pi.SetValue(objRC_CANDIDATE_EXPECT, value, Nothing)
                            End If
                        Case "RC_CANDIDATE"
                            oPropsRC_CANDIDATE = objRC_CANDIDATE.GetType().GetProperties()
                            Dim pi As PropertyInfo = (From l In oPropsRC_CANDIDATE Where l.Name.ToUpper.Equals(Col.ColumnName.Split("#")(1).ToUpper) Select l).FirstOrDefault()
                            If pi IsNot Nothing Then
                                Dim value As Object
                                value = SetValueObject(pi, Data(0)(Col.ColumnName))
                                pi.SetValue(objRC_CANDIDATE, value, Nothing)
                            End If
                        Case "RC_CANDIDATE_CV"
                            oPropsRC_CANDIDATE_CV = objRC_CANDIDATE_CV.GetType().GetProperties()
                            Dim pi As PropertyInfo = (From l In oPropsRC_CANDIDATE_CV Where l.Name.ToUpper.Equals(Col.ColumnName.Split("#")(1).ToUpper) Select l).FirstOrDefault()
                            If pi IsNot Nothing Then
                                Dim value As Object
                                value = SetValueObject(pi, Data(0)(Col.ColumnName))
                                pi.SetValue(objRC_CANDIDATE_CV, value, Nothing)
                            End If
                        Case "RC_CANDIDATE_HEALTH"
                            oPropsRC_CANDIDATE_HEALTH = objRC_CANDIDATE_HEALTH.GetType().GetProperties()
                            Dim pi As PropertyInfo = (From l In oPropsRC_CANDIDATE_HEALTH Where l.Name.ToUpper.Equals(Col.ColumnName.Split("#")(1).ToUpper) Select l).FirstOrDefault()
                            pi.SetValue(objRC_CANDIDATE_HEALTH, Data(0)(Col.ColumnName), Nothing)
                        Case "RC_CANDIDATE_EDUCATION"
                            oPropsRC_CANDIDATE_EDUCATION = objRC_CANDIDATE_EDUCATION.GetType().GetProperties()
                            Dim pi As PropertyInfo = (From l In oPropsRC_CANDIDATE_EDUCATION Where l.Name.ToUpper.Equals(Col.ColumnName.Split("#")(1).ToUpper) Select l).FirstOrDefault()
                            If pi IsNot Nothing Then
                                Dim value As Object
                                value = SetValueObject(pi, Data(0)(Col.ColumnName))
                                pi.SetValue(objRC_CANDIDATE_EDUCATION, value, Nothing)
                            End If
                        Case "RC_CANDIDATE_TRAINSINGER"
                            Continue For
                            'XU LY RIENG TH NAY
                            'oPropsRC_CANDIDATE_TRAINSINGER = objRC_CANDIDATE_TRAINSINGER.GetType().GetProperties()
                            'Dim pi As PropertyInfo = oPropsRC_CANDIDATE_TRAINSINGER(Col.ColumnName.Split("#")(1))
                            'pi.SetValue(objRC_CANDIDATE_TRAINSINGER, Data(0)(Col.ColumnName.Split("#")(1)), Nothing)
                        Case "RC_CANDIDATE_BEFOREWT"
                            Continue For
                            'XU LY RIENG TH NAY 
                            'oPropsRC_CANDIDATE_BEFOREWT = objRC_CANDIDATE_BEFOREWT.GetType().GetProperties()
                            'Dim pi As PropertyInfo = oPropsRC_CANDIDATE_BEFOREWT(Col.ColumnName.Split("#")(1))
                            'pi.SetValue(objRC_CANDIDATE_BEFOREWT, Data(0)(Col.ColumnName.Split("#")(1)), Nothing)
                    End Select
                Catch ex As Exception
                    Utility.WriteExceptionLog(ex, Me.ToString() & ".ImportRC: " + Col.ColumnName)
                End Try
            Next
            Context.RC_CANDIDATE_EDUCATION.AddObject(objRC_CANDIDATE_EDUCATION)
            Context.RC_CANDIDATE_HEALTH.AddObject(objRC_CANDIDATE_HEALTH)
            Context.RC_CANDIDATE_CV.AddObject(objRC_CANDIDATE_CV)
            objRC_CANDIDATE.RC_PROGRAM_ID = ProGramID

            'Sinh mã ứng viên động
            Dim checkEMP As Integer = 0
            Dim empCodeDB As Decimal = 0
            Dim EMPCODE As String
            Using query As New DataAccess.NonQueryData
                Dim temp = query.ExecuteSQLScalar("select Candidate_CODE from RC_Candidate order by Candidate_CODE DESC", New Object)
                If temp IsNot Nothing Then
                    empCodeDB = Decimal.Parse(temp)
                End If
            End Using
            Do
                empCodeDB += 1
                EMPCODE = String.Format("{0}", Format(empCodeDB, "000000"))
                checkEMP = (From p In Context.RC_CANDIDATE Where p.CANDIDATE_CODE = EMPCODE Select p.ID).Count
            Loop Until checkEMP = 0
            objRC_CANDIDATE.CANDIDATE_CODE = EMPCODE
            Context.RC_CANDIDATE.AddObject(objRC_CANDIDATE)
            objRC_CANDIDATE_EXPECT.DATE_START = Nothing
            Context.RC_CANDIDATE_EXPECT.AddObject(objRC_CANDIDATE_EXPECT)
            'Dim objRC_CANDIDATE_TRAINSINGER As New RC_CANDIDATE_TRAINSINGER
            For I As Integer = 1 To 5
                Try
                    Dim objRC_CANDIDATE_TRAINSINGER As New RC_CANDIDATE_TRAINNING
                    objRC_CANDIDATE_TRAINSINGER.CANDIDATE_ID = CANDIDATE_ID
                    objRC_CANDIDATE_TRAINSINGER.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_TRAINNING.EntitySet.Name)
                    If IsDate(Data(0)("rc_candidate_trainning#FROM_DATE" + I.ToString())) Then
                        objRC_CANDIDATE_TRAINSINGER.FROM_DATE = ConvertStringToDate("1/" + Data(0)("rc_candidate_trainning#FROM_DATE" + I.ToString()))
                    End If
                    If IsDate(Data(0)("rc_candidate_trainning#TO_DATE" + I.ToString())) Then
                        objRC_CANDIDATE_TRAINSINGER.TO_DATE = ConvertStringToDate("1/" + Data(0)("rc_candidate_trainning#TO_DATE" + I.ToString()))
                    End If
                    If IsDBNull(Data(0)("rc_candidate_trainning#NAME_SHOOLS" + I.ToString())) OrElse String.IsNullOrEmpty(Data(0)("rc_candidate_trainning#NAME_SHOOLS" + I.ToString())) Then
                        Continue For
                    End If
                    objRC_CANDIDATE_TRAINSINGER.NAME_SHOOLS = Data(0)("rc_candidate_trainning#NAME_SHOOLS" + I.ToString())
                    objRC_CANDIDATE_TRAINSINGER.SPECIALIZED_TRAIN = Data(0)("rc_candidate_trainning#SPECIALIZED_TRAIN" + I.ToString())
                    objRC_CANDIDATE_TRAINSINGER.LEVEL_ID = CDec(Data(0)("rc_candidate_trainning#LEVEL_ID" + I.ToString()))
                    objRC_CANDIDATE_TRAINSINGER.RESULT_TRAIN = Data(0)("rc_candidate_trainning#RESULT_TRAIN" + I.ToString())
                    Context.RC_CANDIDATE_TRAINNING.AddObject(objRC_CANDIDATE_TRAINSINGER)
                Catch ex As Exception
                    Continue For
                End Try
            Next
            ' Dim objRC_CANDIDATE_BEFOREWT As New RC_CANDIDATE_BEFOREWT
            For I As Integer = 1 To 3
                Try
                    Dim objRC_CANDIDATE_BEFOREWT As New RC_CANDIDATE_BEFOREWT
                    objRC_CANDIDATE_BEFOREWT.CANDIDATE_ID = CANDIDATE_ID
                    objRC_CANDIDATE_BEFOREWT.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_BEFOREWT.EntitySet.Name)

                    If IsDBNull(Data(0)("rc_candidate_beforewt#ORG_NAME" + I.ToString())) OrElse String.IsNullOrEmpty(Data(0)("rc_candidate_beforewt#ORG_NAME" + I.ToString())) Then
                        Continue For
                    End If

                    objRC_CANDIDATE_BEFOREWT.ORG_NAME = Data(0)("rc_candidate_beforewt#ORG_NAME" + I.ToString())
                    objRC_CANDIDATE_BEFOREWT.TITLE_NAME = Data(0)("rc_candidate_beforewt#TITLE_NAME" + I.ToString())
                    'If IsDate(Data(0)("rc_candidate_beforewt#FROMDATE" + I.ToString())) Then
                    objRC_CANDIDATE_BEFOREWT.FROMDATE = ConvertStringToDate("1/" + Data(0)("rc_candidate_beforewt#FROMDATE" + I.ToString()))
                    'End If
                    'If IsDate(Data(0)("rc_candidate_beforewt#TODATE" + I.ToString())) Then
                    objRC_CANDIDATE_BEFOREWT.TODATE = ConvertStringToDate("1/" + Data(0)("rc_candidate_beforewt#TODATE" + I.ToString()))
                    'End If
                    objRC_CANDIDATE_BEFOREWT.DIRECT_MANAGER = Data(0)("rc_candidate_beforewt#DIRECT_MANAGER" + I.ToString())
                    objRC_CANDIDATE_BEFOREWT.WORK = Data(0)("rc_candidate_beforewt#WORK" + I.ToString())
                    If IsNumeric(Data(0)("rc_candidate_beforewt#SALARY" + I.ToString())) Then
                        objRC_CANDIDATE_BEFOREWT.SALARY = CDec(Data(0)("rc_candidate_beforewt#SALARY" + I.ToString()))
                    End If
                    objRC_CANDIDATE_BEFOREWT.REASON_LEAVE = Data(0)("rc_candidate_beforewt#REASON_LEAVE" + I.ToString())
                    Context.RC_CANDIDATE_BEFOREWT.AddObject(objRC_CANDIDATE_BEFOREWT)
                Catch ex As Exception
                    Continue For
                End Try
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Return False
        Finally
            objRC_CANDIDATE_EXPECT = Nothing
            objRC_CANDIDATE = Nothing
            objRC_CANDIDATE_CV = Nothing
            objRC_CANDIDATE_HEALTH = Nothing
            objRC_CANDIDATE_EDUCATION = Nothing
        End Try
    End Function

#Region "PlanReg"

    Public Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanRegDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.RC_PLAN_REG
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New PlanRegDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .REMARK_REJECT = p.REMARK_REJECT}

            Dim lst = query
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.RECRUIT_REASON_NAME <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.RECRUIT_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.RECRUIT_NUMBER = _filter.RECRUIT_NUMBER)
            End If
            If _filter.EXPECTED_JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPECTED_JOIN_DATE = _filter.EXPECTED_JOIN_DATE)
            End If
            If _filter.SEND_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            End If
            If _param.YEAR > 0 And _param.YEAR.ToString() <> "" Then
                lst = lst.Where(Function(p) p.SEND_DATE.Value.Year = _param.YEAR)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            If _param.YEAR > 0 And _param.YEAR.ToString() <> "" Then
                lst = lst.Where(Function(p) p.SEND_DATE.Value.Year = _param.YEAR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanReg")
            Throw ex
        End Try

    End Function

    Public Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO

        Try

            Dim query = From p In Context.RC_PLAN_REG
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Where p.ID = _filter.ID
                        Select New PlanRegDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .EDUCATIONLEVEL = p.EDUCATIONLEVEL,
                                       .AGESFROM = p.AGESFROM,
                                       .AGESTO = p.AGESTO,
                                       .LANGUAGE = p.LANGUAGE,
                                       .LANGUAGELEVEL = p.LANGUAGELEVEL,
                                       .LANGUAGESCORES = p.LANGUAGESCORES,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .MAINTASK = p.MAINTASK,
                                       .QUALIFICATION = p.QUALIFICATION,
                                       .QUALIFICATIONREQUEST = p.QUALIFICATIONREQUEST,
                                       .COMPUTER_LEVEL = p.COMPUTER_LEVEL}

            Dim obj = query.FirstOrDefault
            Dim lstEmp = (From p In Context.RC_PLAN_REG_EMP
                          From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                          Where p.RC_PLAN_REG_ID = obj.ID
                          Select New PlanRegEmpDTO With {
                              .EMPLOYEE_ID = p.EMPLOYEE_ID,
                              .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                              .EMPLOYEE_NAME = emp.FULLNAME_VN}).ToList

            obj.lstEmp = lstEmp
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanRegByID")
            Throw ex
        End Try


    End Function

    Public Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New RC_PLAN_REG
        Try
            objPlanRegData.ID = Utilities.GetNextSequence(Context, Context.RC_PLAN_REG.EntitySet.Name)
            objPlanRegData.SEND_DATE = objPlanReg.SEND_DATE
            objPlanRegData.ORG_ID = objPlanReg.ORG_ID
            objPlanRegData.TITLE_ID = objPlanReg.TITLE_ID
            objPlanRegData.JOBGRADE_ID = objPlanReg.JOBGRADE_ID
            objPlanRegData.RECRUIT_NUMBER = objPlanReg.RECRUIT_NUMBER
            objPlanRegData.EXPECTED_JOIN_DATE = objPlanReg.EXPECTED_JOIN_DATE
            objPlanRegData.RECRUIT_REASON_ID = objPlanReg.RECRUIT_REASON_ID
            objPlanRegData.REMARK = objPlanReg.REMARK
            objPlanRegData.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.WAIT_ID
            objPlanRegData.EDUCATIONLEVEL = objPlanReg.EDUCATIONLEVEL
            objPlanRegData.AGESFROM = objPlanReg.AGESFROM
            objPlanRegData.AGESTO = objPlanReg.AGESTO
            objPlanRegData.LANGUAGE = objPlanReg.LANGUAGE
            objPlanRegData.LANGUAGELEVEL = objPlanReg.LANGUAGELEVEL
            objPlanRegData.LANGUAGESCORES = objPlanReg.LANGUAGESCORES
            objPlanRegData.SPECIALSKILLS = objPlanReg.SPECIALSKILLS
            objPlanRegData.MAINTASK = objPlanReg.MAINTASK
            objPlanRegData.QUALIFICATION = objPlanReg.QUALIFICATION
            objPlanRegData.QUALIFICATIONREQUEST = objPlanReg.QUALIFICATIONREQUEST
            objPlanRegData.COMPUTER_LEVEL = objPlanReg.COMPUTER_LEVEL
            Context.RC_PLAN_REG.AddObject(objPlanRegData)
            If objPlanReg.lstEmp IsNot Nothing Then
                For Each item In objPlanReg.lstEmp
                    Dim objPlanRegEmpData As New RC_PLAN_REG_EMP
                    objPlanRegEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_PLAN_REG_EMP.EntitySet.Name)
                    objPlanRegEmpData.RC_PLAN_REG_ID = objPlanRegData.ID
                    objPlanRegEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_PLAN_REG_EMP.AddObject(objPlanRegEmpData)
                Next
            End If
            Context.SaveChanges(log)
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertPlanReg")
            Throw ex
        End Try
    End Function

    Public Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPlanRegData As New RC_PLAN_REG With {.ID = objPlanReg.ID}
        Try
            Context.RC_PLAN_REG.Attach(objPlanRegData)
            objPlanRegData.SEND_DATE = objPlanReg.SEND_DATE
            objPlanRegData.ORG_ID = objPlanReg.ORG_ID
            objPlanRegData.TITLE_ID = objPlanReg.TITLE_ID
            objPlanRegData.JOBGRADE_ID = objPlanReg.JOBGRADE_ID
            objPlanRegData.RECRUIT_NUMBER = objPlanReg.RECRUIT_NUMBER
            objPlanRegData.EXPECTED_JOIN_DATE = objPlanReg.EXPECTED_JOIN_DATE
            objPlanRegData.RECRUIT_REASON_ID = objPlanReg.RECRUIT_REASON_ID
            objPlanRegData.REMARK = objPlanReg.REMARK
            objPlanRegData.EDUCATIONLEVEL = objPlanReg.EDUCATIONLEVEL
            objPlanRegData.AGESFROM = objPlanReg.AGESFROM
            objPlanRegData.AGESTO = objPlanReg.AGESTO
            objPlanRegData.LANGUAGE = objPlanReg.LANGUAGE
            objPlanRegData.LANGUAGELEVEL = objPlanReg.LANGUAGELEVEL
            objPlanRegData.LANGUAGESCORES = objPlanReg.LANGUAGESCORES
            objPlanRegData.SPECIALSKILLS = objPlanReg.SPECIALSKILLS
            objPlanRegData.MAINTASK = objPlanReg.MAINTASK
            objPlanRegData.QUALIFICATION = objPlanReg.QUALIFICATION
            objPlanRegData.QUALIFICATIONREQUEST = objPlanReg.QUALIFICATIONREQUEST
            objPlanRegData.COMPUTER_LEVEL = objPlanReg.COMPUTER_LEVEL
            Dim lstRegEmp = (From p In Context.RC_PLAN_REG_EMP Where p.RC_PLAN_REG_ID = objPlanRegData.ID).ToList
            For Each item In lstRegEmp
                Context.RC_PLAN_REG_EMP.DeleteObject(item)
            Next
            Context.SaveChanges(log)
            If objPlanReg.lstEmp IsNot Nothing Then
                For Each item In objPlanReg.lstEmp
                    Dim objPlanRegEmpData As New RC_PLAN_REG_EMP
                    objPlanRegEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_PLAN_REG_EMP.EntitySet.Name)
                    objPlanRegEmpData.RC_PLAN_REG_ID = objPlanRegData.ID
                    objPlanRegEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_PLAN_REG_EMP.AddObject(objPlanRegEmpData)
                Next
                Context.SaveChanges(log)
            End If
            gID = objPlanRegData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyPlanReg")
            Throw ex
        End Try

    End Function

    Public Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPlanRegData As List(Of RC_PLAN_REG)
        Try
            lstPlanRegData = (From p In Context.RC_PLAN_REG Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstPlanRegData.Count - 1
                Dim id = lstPlanRegData(index).ID
                Dim lstRegEmp = (From p In Context.RC_PLAN_REG_EMP Where p.RC_PLAN_REG_ID = id).ToList
                For Each item In lstRegEmp
                    Context.RC_PLAN_REG_EMP.DeleteObject(item)
                Next
                Context.RC_PLAN_REG.DeleteObject(lstPlanRegData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeletePlanReg")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean
        Dim lstPlanRegData As List(Of RC_PLAN_REG)
        Try
            lstPlanRegData = (From p In Context.RC_PLAN_REG Where lstID.Contains(p.ID)).ToList
            For Each item In lstPlanRegData
                item.STATUS_ID = status
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusPlanReg")
            Throw ex
        End Try

    End Function

#End Region

#Region "PlanSummary"

    Public Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable

        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT.GET_PLAN_SUMMARY",
                                           New With {.P_YEAR = _year,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanSummary")
            Throw ex
        End Try

    End Function

#End Region

#Region "Request"

    Public Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            '
            Dim query As IQueryable(Of RequestDTO)
            'Dim query
            If _filter.IS_INSERT_PRO AndAlso log.Username.ToUpper <> "ADMIN" Then
                query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From rc_prop In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RC_RECRUIT_PROPERTY).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GENDER_PRIORITY).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From req_name In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUIRER).DefaultIfEmpty
                        From p_pt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PERSON_PT_RC).DefaultIfEmpty
                        From wp_name In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORKING_PLACE_ID).DefaultIfEmpty
                        Where (p.IS_CONFIRM Is Nothing OrElse p.IS_CONFIRM <> 0) And p.PERSON_PT_RC = _filter.PERSON_PT_RC
                        Select New RequestDTO With {
                                           .ID = p.ID,
                                           .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                           .ORG_ID = p.ORG_ID,
                                           .ORG_NAME = org.NAME_VN,
                                           .ORG_DESC = org.DESCRIPTION_PATH,
                                           .TITLE_ID = p.TITLE_ID,
                                           .TITLE_NAME = title.NAME_VN,
                                           .AGE_FROM = p.AGE_FROM,
                                           .AGE_TO = p.AGE_TO,
                                           .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                           .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                           .DESCRIPTION = p.DESCRIPTION,
                                           .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                           .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                           .IS_IN_PLAN = p.IS_IN_PLAN,
                                           .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                           .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                           .MALE_NUMBER = p.MALE_NUMBER,
                                           .RECRUIT_REASON = p.RECRUIT_REASON,
                                           .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                           .RECRUIT_REASON_NAME = reason.NAME_VN,
                                           .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                           .REQUEST_OTHER = p.REQUEST_OTHER,
                                           .REMARK = p.REMARK,
                                           .SEND_DATE = p.SEND_DATE,
                                           .STATUS_ID = p.STATUS_ID,
                                           .STATUS_NAME = status.NAME_VN,
                                           .STATUS_CODE = status.CODE,
                                           .CREATED_DATE = p.CREATED_DATE,
                                           .REMARK_REJECT = p.REMARK_REJECT,
                                           .RC_RECRUIT_PROPERTY = p.RC_RECRUIT_PROPERTY,
                                           .RC_RECRUIT_PROPERTY_NAME = rc_prop.NAME_VN,
                                           .IS_OVER_LIMIT = p.IS_OVER_LIMIT,
                                           .IS_REQUEST_COMPUTER = p.IS_REQUEST_COMPUTER,
                                           .FOREIGN_ABILITY = p.FOREIGN_ABILITY,
                                           .COMPUTER_APP_LEVEL = p.COMPUTER_APP_LEVEL,
                                           .GENDER_PRIORITY = p.GENDER_PRIORITY,
                                           .GENDER_PRIORITY_NAME = gender.NAME_VN,
                                           .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                           .UPLOAD_FILE = p.UPLOAD_FILE,
                                           .FILE_NAME = p.FILE_NAME,
                                           .CODE_RC = p.CODE_RC,
                                           .NAME_RC = p.NAME_RC,
                                           .REQUIRER = p.REQUIRER,
                                           .REQUIRER_NAME = req_name.FULLNAME_VN,
                                           .WORKING_PLACE_ID = p.WORKING_PLACE_ID,
                                           .WORKING_PLACE_NAME = wp_name.NAME_VN,
                                           .RC_GRPOS = p.RC_GRPOS,
                                           .SAL_OFFER = p.SAL_OFFER,
                                           .IS_APPROVED = p.IS_APPROVED,
                                           .PERSON_PT_RC = p.PERSON_PT_RC,
                                           .PERSON_PT_RC_NAME = p_pt.FULLNAME_VN,
                                           .OTHER_QUALIFICATION = p.OTHER_QUALIFICATION,
                                            .IS_CLOCK = p.IS_CLOCK,
                                           .IS_CLOCK_NAME = If(p.IS_CLOCK Is Nothing OrElse p.IS_CLOCK = 0, "", "Đã khóa"),
                                           .IS_PORTAL = p.IS_PORTAL,
                                           .REMARK_CANCEL = p.REMARK_CANCEL}
            Else
                query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From rc_prop In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RC_RECRUIT_PROPERTY).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GENDER_PRIORITY).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From req_name In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUIRER).DefaultIfEmpty
                        From p_pt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PERSON_PT_RC).DefaultIfEmpty
                        From wp_name In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORKING_PLACE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.IS_CONFIRM Is Nothing OrElse p.IS_CONFIRM <> 0
                        Select New RequestDTO With {
                                           .ID = p.ID,
                                           .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                           .ORG_ID = p.ORG_ID,
                                           .ORG_NAME = org.NAME_VN,
                                           .ORG_DESC = org.DESCRIPTION_PATH,
                                           .TITLE_ID = p.TITLE_ID,
                                           .TITLE_NAME = title.NAME_VN,
                                           .AGE_FROM = p.AGE_FROM,
                                           .AGE_TO = p.AGE_TO,
                                           .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                           .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                           .DESCRIPTION = p.DESCRIPTION,
                                           .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                           .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                           .IS_IN_PLAN = p.IS_IN_PLAN,
                                           .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                           .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                           .MALE_NUMBER = p.MALE_NUMBER,
                                           .RECRUIT_REASON = p.RECRUIT_REASON,
                                           .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                           .RECRUIT_REASON_NAME = reason.NAME_VN,
                                           .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                           .REQUEST_OTHER = p.REQUEST_OTHER,
                                           .REMARK = p.REMARK,
                                           .SEND_DATE = p.SEND_DATE,
                                           .STATUS_ID = p.STATUS_ID,
                                           .STATUS_NAME = status.NAME_VN,
                                           .STATUS_CODE = status.CODE,
                                           .CREATED_DATE = p.CREATED_DATE,
                                           .REMARK_REJECT = p.REMARK_REJECT,
                                           .RC_RECRUIT_PROPERTY = p.RC_RECRUIT_PROPERTY,
                                           .RC_RECRUIT_PROPERTY_NAME = rc_prop.NAME_VN,
                                           .IS_OVER_LIMIT = p.IS_OVER_LIMIT,
                                           .IS_REQUEST_COMPUTER = p.IS_REQUEST_COMPUTER,
                                           .FOREIGN_ABILITY = p.FOREIGN_ABILITY,
                                           .COMPUTER_APP_LEVEL = p.COMPUTER_APP_LEVEL,
                                           .GENDER_PRIORITY = p.GENDER_PRIORITY,
                                           .GENDER_PRIORITY_NAME = gender.NAME_VN,
                                           .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                           .UPLOAD_FILE = p.UPLOAD_FILE,
                                           .FILE_NAME = p.FILE_NAME,
                                           .CODE_RC = p.CODE_RC,
                                           .NAME_RC = p.NAME_RC,
                                           .REQUIRER = p.REQUIRER,
                                           .REQUIRER_NAME = req_name.FULLNAME_VN,
                                           .WORKING_PLACE_ID = p.WORKING_PLACE_ID,
                                           .WORKING_PLACE_NAME = wp_name.NAME_VN,
                                           .RC_GRPOS = p.RC_GRPOS,
                                           .SAL_OFFER = p.SAL_OFFER,
                                           .IS_APPROVED = p.IS_APPROVED,
                                           .PERSON_PT_RC = p.PERSON_PT_RC,
                                           .PERSON_PT_RC_NAME = p_pt.FULLNAME_VN,
                                           .OTHER_QUALIFICATION = p.OTHER_QUALIFICATION,
                                            .IS_CLOCK = p.IS_CLOCK,
                                           .IS_CLOCK_NAME = If(p.IS_CLOCK Is Nothing OrElse p.IS_CLOCK = 0, "", "Đã khóa"),
                                           .IS_PORTAL = p.IS_PORTAL,
                                           .REMARK_CANCEL = p.REMARK_CANCEL}
            End If

            Dim lst = query
            lst = lst.Where(Function(p) p.STATUS_ID IsNot Nothing)
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE <= _filter.FROM_DATE)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.LEARNING_LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.LEARNING_LEVEL_NAME.ToUpper.Contains(_filter.LEARNING_LEVEL_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.RECRUIT_REASON <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON.ToUpper.Contains(_filter.RECRUIT_REASON.ToUpper))
            End If
            If _filter.REMARK_CANCEL <> "" Then
                lst = lst.Where(Function(p) p.REMARK_CANCEL.ToUpper.Contains(_filter.REMARK_CANCEL.ToUpper))
            End If
            If _filter.REQUEST_EXPERIENCE <> "" Then
                lst = lst.Where(Function(p) p.REQUEST_EXPERIENCE.ToUpper.Contains(_filter.REQUEST_EXPERIENCE.ToUpper))
            End If
            If _filter.REQUEST_OTHER <> "" Then
                lst = lst.Where(Function(p) p.REQUEST_OTHER.ToUpper.Contains(_filter.REQUEST_OTHER.ToUpper))
            End If
            If _filter.RECRUIT_REASON_NAME <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
            End If
            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.DESCRIPTION <> "" Then
                lst = lst.Where(Function(p) p.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.MALE_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.MALE_NUMBER = _filter.MALE_NUMBER)
            End If
            If _filter.AGE_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.AGE_FROM = _filter.AGE_FROM)
            End If
            If _filter.EXPERIENCE_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPERIENCE_NUMBER = _filter.EXPERIENCE_NUMBER)
            End If
            If _filter.IS_IN_PLAN IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_IN_PLAN = _filter.IS_IN_PLAN)
            End If
            If _filter.AGE_TO IsNot Nothing Then
                lst = lst.Where(Function(p) p.AGE_TO = _filter.AGE_TO)
            End If
            If _filter.SEND_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequest")
            Throw ex
        End Try

    End Function

    Public Function GetListOfferCandidate(ByVal _filter As ListOfferCandidateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ListOfferCandidateDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim listStatus As New List(Of String)({"NHANVIEN", "TUCHOIOFFER", "TRUNGTUYEN", "TUCHOINV", "PONTENTIAL"})

            Dim query As IQueryable(Of ListOfferCandidateDTO)
            query = From pro_can In Context.RC_PROGRAM_CANDIDATE
                    From can In Context.RC_CANDIDATE.Where(Function(f) f.ID = pro_can.CANDIDATE_ID)
                    From offer In Context.OT_OTHER_LIST.Where(Function(f) f.TYPE_ID = 1029 And f.CODE = pro_can.OFFER_ID).DefaultIfEmpty
                    From status In Context.OT_OTHER_LIST.Where(Function(f) f.TYPE_ID = 1029 And f.CODE = pro_can.STATUS_ID).DefaultIfEmpty
                    From program In Context.RC_PROGRAM.Where(Function(f) f.ID = pro_can.RC_PROGRAM_ID)
                    From request In Context.RC_REQUEST.Where(Function(f) f.ID = program.RC_REQUEST_ID).DefaultIfEmpty
                    From requier In Context.HU_EMPLOYEE.Where(Function(f) f.ID = request.REQUIRER).DefaultIfEmpty
                    From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = request.ORG_ID).DefaultIfEmpty
                    From title In Context.HU_TITLE.Where(Function(f) f.ID = request.TITLE_ID).DefaultIfEmpty
                    From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = request.RECRUIT_REASON_ID).DefaultIfEmpty
                    From person In Context.HU_EMPLOYEE.Where(Function(f) f.ID = request.PERSON_PT_RC).DefaultIfEmpty
                    From k In Context.SE_CHOSEN_ORG.Where(Function(f) request.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                    Where listStatus.Contains(pro_can.STATUS_ID)
                    Select New ListOfferCandidateDTO With {
                                    .ID = pro_can.ID,
                                    .CREATED_DATE = pro_can.CREATED_DATE,
                                    .CANDIDATE_ID = pro_can.CANDIDATE_ID,
                                    .CANDIDATE_CODE = can.CANDIDATE_CODE,
                                    .CANDIDATE_NAME = can.FULLNAME_VN,
                                    .OFFER_ID_STR = pro_can.OFFER_ID,
                                    .OFFER_ID_DEC = offer.ID,
                                    .OFFER_NAME = offer.NAME_VN,
                                    .STATUS_ID_STR = pro_can.STATUS_ID,
                                    .STATUS_ID_DEC = status.ID,
                                    .STATUS_NAME = status.NAME_VN,
                                    .CODE_RC = request.CODE_RC,
                                    .REQUIRER_ID = request.REQUIRER,
                                    .REQUIRER_NAME = requier.FULLNAME_VN,
                                    .SEND_DATE = request.SEND_DATE,
                                    .EXPECTED_JOIN_DATE = request.EXPECTED_JOIN_DATE,
                                    .ORG_ID = request.ORG_ID,
                                    .ORG_DESC = org.DESCRIPTION_PATH,
                                    .ORG_NAME = org.NAME_VN,
                                    .TITLE_ID = request.TITLE_ID,
                                    .TITLE_NAME = title.NAME_VN,
                                    .RECRUIT_REASON_ID = request.RECRUIT_REASON_ID,
                                    .RECRUIT_REASON_NAME = reason.NAME_VN,
                                    .PERSON_PT_RC_ID = request.PERSON_PT_RC,
                                    .PERSON_PT_RC_NAME = person.FULLNAME_VN,
                                    .RC_PROGRAM_ID = pro_can.RC_PROGRAM_ID
                               }

            Dim lst = query
            If _filter.SEND_FROM IsNot Nothing Then
                lst = lst.Where(Function(f) f.SEND_DATE >= _filter.SEND_FROM)
            End If

            If _filter.SEND_TO IsNot Nothing Then
                lst = lst.Where(Function(f) f.SEND_DATE <= _filter.SEND_TO)
            End If

            If _filter.EXPECTED_FROM IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXPECTED_JOIN_DATE >= _filter.EXPECTED_FROM)
            End If

            If _filter.EXPECTED_TO IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXPECTED_JOIN_DATE <= _filter.EXPECTED_TO)
            End If

            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(f) f.TITLE_ID = _filter.TITLE_ID)
            End If

            If _filter.STATUS_ID_DEC IsNot Nothing Then
                lst = lst.Where(Function(f) f.STATUS_ID_DEC = _filter.STATUS_ID_DEC)
            End If

            If _filter.OFFER_ID_STR IsNot Nothing Then
                lst = lst.Where(Function(f) f.OFFER_ID_STR = _filter.OFFER_ID_STR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetListOfferCandidate")
            Throw ex
        End Try

    End Function

    Public Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO

        Try

            Dim query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From req In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUIRER).DefaultIfEmpty
                        From pt_rc In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PERSON_PT_RC).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New RequestDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                       .MALE_NUMBER = p.MALE_NUMBER,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_OTHER = p.REQUEST_OTHER,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .STATUS_CODE = status.CODE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .LANGUAGE = p.LANGUAGE,
                                       .WORKING_PLACE_ID = p.WORKING_PLACE_ID,
                                       .LANGUAGELEVEL = p.LANGUAGELEVEL,
                                       .LANGUAGESCORES = p.LANGUAGESCORES,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .MAINTASK = p.MAINTASK,
                                       .QUALIFICATION = p.QUALIFICATION,
                                       .DESCRIPTIONATTACHFILE = p.DESCRIPTIONATTACHFILE,
                                       .COMPUTER_LEVEL = p.COMPUTER_LEVEL,
                                       .RC_PLAN_ID = p.RC_PLAN_ID,
                                       .RC_RECRUIT_PROPERTY = p.RC_RECRUIT_PROPERTY,
                                       .IS_OVER_LIMIT = p.IS_OVER_LIMIT,
                                       .IS_REQUEST_COMPUTER = p.IS_REQUEST_COMPUTER,
                                       .LOCATION_ID = p.LOCATION_ID,
                                       .FOREIGN_ABILITY = p.FOREIGN_ABILITY,
                                       .COMPUTER_APP_LEVEL = p.COMPUTER_APP_LEVEL,
                                       .GENDER_PRIORITY = p.GENDER_PRIORITY,
                                       .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                       .FILE_NAME = p.FILE_NAME,
                                       .UPLOAD_FILE = p.UPLOAD_FILE,
                                       .CODE_RC = p.CODE_RC,
                                       .NAME_RC = p.NAME_RC,
                                        .REQUIRER = p.REQUIRER,
                                        .REQUIRER_NAME = req.EMPLOYEE_CODE + " - " + req.FULLNAME_VN,
                                        .RC_GRPOS = p.RC_GRPOS,
                                        .SAL_OFFER = p.SAL_OFFER,
                                        .PERSON_PT_RC = p.PERSON_PT_RC,
                                        .PERSON_PT_RC_NAME = pt_rc.EMPLOYEE_CODE + " - " + pt_rc.FULLNAME_VN,
                                        .IS_APPROVED = p.IS_APPROVED,
                                        .OTHER_QUALIFICATION = p.OTHER_QUALIFICATION,
                                        .IS_PORTAL = p.IS_PORTAL}

            Dim obj = query.FirstOrDefault
            Dim lstEmp = (From p In Context.RC_REQUEST_EMP
                          From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                          From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                          From t In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID)
                          Where p.RC_REQUEST_ID = obj.ID
                          Select New RequestEmpDTO With {
                               .EMPLOYEE_ID = p.EMPLOYEE_ID,
                               .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                               .EMPLOYEE_NAME = emp.FULLNAME_VN,
                               .ORG_NAME = o.NAME_VN,
                               .TITLE_NAME = t.NAME_VN,
                               .JOIN_DATE = emp.JOIN_DATE}).ToList
            obj.lstEmp = lstEmp
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestByID")
            Throw ex
        End Try


    End Function

    Public Function GetRequestByID_ReturnList(ByVal _filter As RequestDTO) As List(Of RequestDTO)

        Try

            Dim query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New RequestDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                       .MALE_NUMBER = p.MALE_NUMBER,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_OTHER = p.REQUEST_OTHER,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .LANGUAGE = p.LANGUAGE,
                                       .LANGUAGELEVEL = p.LANGUAGELEVEL,
                                       .LANGUAGESCORES = p.LANGUAGESCORES,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .MAINTASK = p.MAINTASK,
                                       .QUALIFICATION = p.QUALIFICATION,
                                       .DESCRIPTIONATTACHFILE = p.DESCRIPTIONATTACHFILE,
                                       .COMPUTER_LEVEL = p.COMPUTER_LEVEL,
                                       .IS_PORTAL = p.IS_PORTAL}

            'Dim obj = query.FirstOrDefault
            Dim obj = query
            'Dim lstEmp = (From p In Context.RC_REQUEST_EMP
            '               From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '               Where p.RC_REQUEST_ID = obj.ID
            '               Select New RequestEmpDTO With {
            '                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
            '                   .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
            '                   .EMPLOYEE_NAME = emp.FULLNAME_VN}).ToList

            'obj.lstEmp = lstEmp
            Return obj.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestByID")
            Throw ex
        End Try


    End Function

    Public Function CheckExistRequest(ByVal objRequest As RequestDTO) As Boolean
        Try
            Dim query = (From p In Context.RC_REQUEST
                         Where p.ORG_ID = objRequest.ORG_ID _
                        And p.TITLE_ID = objRequest.TITLE_ID _
                        And p.SEND_DATE = objRequest.SEND_DATE _
                        And p.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
                         Select p.ID).ToList()
            Return query.Count > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequestData As New RC_REQUEST
        Try
            If CheckExistRequest(objRequest) Then
                Return False
            End If
            objRequestData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST.EntitySet.Name)
            objRequestData.IS_IN_PLAN = objRequest.IS_IN_PLAN
            objRequestData.ORG_ID = objRequest.ORG_ID
            objRequestData.TITLE_ID = objRequest.TITLE_ID
            objRequestData.JOBGRADE_ID = objRequest.JOBGRADE_ID
            objRequestData.CONTRACT_TYPE_ID = objRequest.CONTRACT_TYPE_ID
            objRequestData.RECRUIT_REASON_ID = objRequest.RECRUIT_REASON_ID
            objRequestData.RECRUIT_REASON = objRequest.RECRUIT_REASON
            objRequestData.LEARNING_LEVEL_ID = objRequest.LEARNING_LEVEL_ID
            objRequestData.AGE_FROM = objRequest.AGE_FROM
            objRequestData.AGE_TO = objRequest.AGE_TO
            objRequestData.SEND_DATE = objRequest.SEND_DATE
            objRequestData.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
            objRequestData.EXPERIENCE_NUMBER = objRequest.EXPERIENCE_NUMBER
            objRequestData.MALE_NUMBER = objRequest.MALE_NUMBER
            objRequestData.FEMALE_NUMBER = objRequest.FEMALE_NUMBER
            objRequestData.DESCRIPTION = objRequest.DESCRIPTION
            objRequestData.REQUEST_EXPERIENCE = objRequest.REQUEST_EXPERIENCE
            objRequestData.REQUEST_OTHER = objRequest.REQUEST_OTHER
            objRequestData.REMARK = objRequest.REMARK
            objRequestData.LANGUAGE = objRequest.LANGUAGE
            objRequestData.LANGUAGELEVEL = objRequest.LANGUAGELEVEL
            objRequestData.LANGUAGESCORES = objRequest.LANGUAGESCORES
            objRequestData.SPECIALSKILLS = objRequest.SPECIALSKILLS
            objRequestData.QUALIFICATION = objRequest.QUALIFICATION
            objRequestData.MAINTASK = objRequest.MAINTASK
            objRequestData.DESCRIPTIONATTACHFILE = objRequest.DESCRIPTIONATTACHFILE
            objRequestData.COMPUTER_LEVEL = objRequest.COMPUTER_LEVEL
            objRequestData.STATUS_ID = objRequest.STATUS_ID
            objRequestData.RC_PLAN_ID = objRequest.RC_PLAN_ID
            objRequestData.RC_RECRUIT_PROPERTY = objRequest.RC_RECRUIT_PROPERTY
            objRequestData.IS_OVER_LIMIT = objRequest.IS_OVER_LIMIT
            objRequestData.IS_REQUEST_COMPUTER = objRequest.IS_REQUEST_COMPUTER
            objRequestData.FOREIGN_ABILITY = objRequest.FOREIGN_ABILITY
            objRequestData.COMPUTER_APP_LEVEL = objRequest.COMPUTER_APP_LEVEL
            objRequestData.GENDER_PRIORITY = objRequest.GENDER_PRIORITY
            objRequestData.RECRUIT_NUMBER = objRequest.RECRUIT_NUMBER
            objRequestData.FILE_NAME = objRequest.FILE_NAME
            objRequestData.UPLOAD_FILE = objRequest.UPLOAD_FILE
            objRequestData.LOCATION_ID = objRequest.LOCATION_ID

            objRequestData.CODE_RC = objRequest.CODE_RC
            objRequestData.NAME_RC = objRequest.NAME_RC
            objRequestData.REQUIRER = objRequest.REQUIRER
            objRequestData.RC_GRPOS = objRequest.RC_GRPOS
            objRequestData.WORKING_PLACE_ID = objRequest.WORKING_PLACE_ID
            objRequestData.SAL_OFFER = objRequest.SAL_OFFER
            objRequestData.PERSON_PT_RC = objRequest.PERSON_PT_RC
            objRequestData.IS_APPROVED = objRequest.IS_APPROVED
            objRequestData.OTHER_QUALIFICATION = objRequest.OTHER_QUALIFICATION

            Context.RC_REQUEST.AddObject(objRequestData)
            If objRequest.lstEmp IsNot Nothing Then
                For Each item In objRequest.lstEmp
                    Dim objRequestEmpData As New RC_REQUEST_EMP
                    objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                    objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                    objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                Next
            End If
            Context.SaveChanges(log)
            If objRequest.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                Dim lstDecimal As New List(Of Decimal)
                lstDecimal.Add(objRequestData.ID)
                UpdateStatusRequest(lstDecimal, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, log, objRequest.IS_INSERT_PRO)
            End If

            gID = objRequestData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRequest")
            Throw ex
        End Try
    End Function

    Public Function ModifyRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequestData As New RC_REQUEST With {.ID = objRequest.ID}
        Try
            Context.RC_REQUEST.Attach(objRequestData)
            objRequestData.IS_IN_PLAN = objRequest.IS_IN_PLAN
            objRequestData.ORG_ID = objRequest.ORG_ID
            objRequestData.TITLE_ID = objRequest.TITLE_ID
            objRequestData.JOBGRADE_ID = objRequest.JOBGRADE_ID
            objRequestData.CONTRACT_TYPE_ID = objRequest.CONTRACT_TYPE_ID
            objRequestData.RECRUIT_REASON_ID = objRequest.RECRUIT_REASON_ID
            objRequestData.RECRUIT_REASON = objRequest.RECRUIT_REASON
            objRequestData.LEARNING_LEVEL_ID = objRequest.LEARNING_LEVEL_ID
            objRequestData.AGE_FROM = objRequest.AGE_FROM
            objRequestData.AGE_TO = objRequest.AGE_TO
            objRequestData.SEND_DATE = objRequest.SEND_DATE
            objRequestData.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
            objRequestData.EXPERIENCE_NUMBER = objRequest.EXPERIENCE_NUMBER
            objRequestData.MALE_NUMBER = objRequest.MALE_NUMBER
            objRequestData.FEMALE_NUMBER = objRequest.FEMALE_NUMBER
            objRequestData.DESCRIPTION = objRequest.DESCRIPTION
            objRequestData.REQUEST_EXPERIENCE = objRequest.REQUEST_EXPERIENCE
            objRequestData.REQUEST_OTHER = objRequest.REQUEST_OTHER
            objRequestData.REMARK = objRequest.REMARK
            objRequestData.LANGUAGE = objRequest.LANGUAGE
            objRequestData.LANGUAGELEVEL = objRequest.LANGUAGELEVEL
            objRequestData.LANGUAGESCORES = objRequest.LANGUAGESCORES
            objRequestData.SPECIALSKILLS = objRequest.SPECIALSKILLS
            objRequestData.QUALIFICATION = objRequest.QUALIFICATION
            objRequestData.MAINTASK = objRequest.MAINTASK
            objRequestData.DESCRIPTIONATTACHFILE = objRequest.DESCRIPTIONATTACHFILE
            objRequestData.COMPUTER_LEVEL = objRequest.COMPUTER_LEVEL
            objRequestData.RC_RECRUIT_PROPERTY = objRequest.RC_RECRUIT_PROPERTY
            objRequestData.IS_OVER_LIMIT = objRequest.IS_OVER_LIMIT
            objRequestData.IS_REQUEST_COMPUTER = objRequest.IS_REQUEST_COMPUTER
            objRequestData.FOREIGN_ABILITY = objRequest.FOREIGN_ABILITY
            objRequestData.COMPUTER_APP_LEVEL = objRequest.COMPUTER_APP_LEVEL
            objRequestData.GENDER_PRIORITY = objRequest.GENDER_PRIORITY
            objRequestData.RECRUIT_NUMBER = objRequest.RECRUIT_NUMBER
            objRequestData.FILE_NAME = objRequest.FILE_NAME
            objRequestData.UPLOAD_FILE = objRequest.UPLOAD_FILE
            objRequestData.LOCATION_ID = objRequest.LOCATION_ID
            objRequestData.STATUS_ID = objRequest.STATUS_ID
            objRequestData.CODE_RC = objRequest.CODE_RC
            objRequestData.NAME_RC = objRequest.NAME_RC
            objRequestData.REQUIRER = objRequest.REQUIRER
            objRequestData.RC_GRPOS = objRequest.RC_GRPOS
            objRequestData.WORKING_PLACE_ID = objRequest.WORKING_PLACE_ID
            objRequestData.SAL_OFFER = objRequest.SAL_OFFER
            objRequestData.PERSON_PT_RC = objRequest.PERSON_PT_RC
            objRequestData.OTHER_QUALIFICATION = objRequest.OTHER_QUALIFICATION

            If objRequest.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                If objRequest.IS_APPROVED = 1 Then
                    Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = objRequestData.ID).ToList
                    For Each item In lstRegEmp
                        Context.RC_REQUEST_EMP.DeleteObject(item)
                    Next
                    'Context.SaveChanges(log)
                    If objRequest.lstEmp IsNot Nothing Then
                        For Each item In objRequest.lstEmp
                            Dim objRequestEmpData As New RC_REQUEST_EMP
                            objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                            objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                            objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                            Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                        Next

                    End If

                    objRequestData.IS_APPROVED = objRequest.IS_APPROVED
                    objRequestData.IS_PORTAL = 0
                    Dim lstDecimal As New List(Of Decimal)
                    lstDecimal.Add(objRequestData.ID)
                    UpdateStatusRequest(lstDecimal, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, log, objRequest.IS_INSERT_PRO)
                Else
                    Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = objRequestData.ID).ToList
                    For Each item In lstRegEmp
                        Context.RC_REQUEST_EMP.DeleteObject(item)
                    Next
                    'Context.SaveChanges(log)
                    If objRequest.lstEmp IsNot Nothing Then
                        For Each item In objRequest.lstEmp
                            Dim objRequestEmpData As New RC_REQUEST_EMP
                            objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                            objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                            objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                            Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                        Next
                    End If
                    If CheckExistIDRequestIn_RC_Program(objRequestData.ID) Then
                        Dim lstDecimal As New List(Of Decimal)
                        lstDecimal.Add(objRequestData.ID)
                        UpdateStatusRequest(lstDecimal, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, log, objRequest.IS_INSERT_PRO)
                    End If
                End If
            Else
                Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = objRequestData.ID).ToList
                For Each item In lstRegEmp
                    Context.RC_REQUEST_EMP.DeleteObject(item)
                Next
                'Context.SaveChanges(log)
                If objRequest.lstEmp IsNot Nothing Then
                    For Each item In objRequest.lstEmp
                        Dim objRequestEmpData As New RC_REQUEST_EMP
                        objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                        objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                        objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                    Next
                End If
            End If

            Context.SaveChanges(log)
            gID = objRequestData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRequest")
            Throw ex
        End Try

    End Function

    Public Function CheckExistIDRequestIn_RC_Program(ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_PROGRAM Where p.RC_REQUEST_ID = _id).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistData")
            Throw ex
        End Try
    End Function

    Public Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRequestData As List(Of RC_REQUEST)
        Try
            lstRequestData = (From p In Context.RC_REQUEST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRequestData.Count - 1
                Dim id = lstRequestData(index).ID
                Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = id).ToList
                For Each item In lstRegEmp
                    Context.RC_REQUEST_EMP.DeleteObject(item)
                Next
                Context.RC_REQUEST.DeleteObject(lstRequestData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteRequest")
            Throw ex
        End Try

    End Function

    Public Function DeleteOfferCandidate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstOfferCandidate As List(Of RC_PROGRAM_CANDIDATE)
        Try
            lstOfferCandidate = (From p In Context.RC_PROGRAM_CANDIDATE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstOfferCandidate.Count - 1
                Dim program_id = lstOfferCandidate(index).RC_PROGRAM_ID
                Dim candidate_id = lstOfferCandidate(index).CANDIDATE_ID

                Dim lstSalaryCandidate = (From p In Context.RC_SALARY_CANDIDATE Where p.RC_PROGRAM_ID = program_id And p.RC_CANDIDATE_ID = candidate_id).ToList
                For Each item In lstSalaryCandidate
                    Context.RC_SALARY_CANDIDATE.DeleteObject(item)
                Next

                Dim lstAllowanceCandidate = (From p In Context.RC_ALLOWANCE_CANDIDATE Where p.RC_PROGRAM_ID = program_id And p.RC_CANDIDATE_ID = candidate_id).ToList
                For Each item In lstAllowanceCandidate
                    Context.RC_ALLOWANCE_CANDIDATE.DeleteObject(item)
                Next

                Dim lstContractCandidate = (From p In Context.RC_CONTRACT_CANDIDATE Where p.RC_PROGRAM_ID = program_id And p.RC_CANDIDATE_ID = candidate_id).ToList
                For Each item In lstContractCandidate
                    Context.RC_CONTRACT_CANDIDATE.DeleteObject(item)
                Next

                lstOfferCandidate(index).OFFER_ID = Nothing
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteOfferCandidate")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusRequest(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog, ByVal Insert_Pro As Boolean, Optional ByVal IS_CONFIRM As Boolean = False) As Boolean
        Dim lstRequestData As List(Of RC_REQUEST)
        Dim lstProgramData As List(Of RC_PROGRAM)
        Try
            lstRequestData = (From p In Context.RC_REQUEST Where lstID.Contains(p.ID)).ToList
            For Each item In lstRequestData
                item.STATUS_ID = status
                If IS_CONFIRM Then
                    item.IS_APPROVED = 1
                    item.IS_CONFIRM = 1
                    'item.IS_PORTAL = 0
                End If
                If status = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                    Dim lstEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = item.ID).ToList
                    lstProgramData = (From p In Context.RC_PROGRAM Where p.RC_REQUEST_ID = item.ID).ToList
                    If lstProgramData.Count = 0 AndAlso Insert_Pro = True Then
                        InsertProgram(item, lstEmp)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusRequest")
            Throw ex
        End Try

    End Function

    Public Function CancelRequest(ByVal lstID As List(Of Decimal), ByVal reason As String, ByVal log As UserLog) As Boolean
        Dim lstRequestData As List(Of RC_REQUEST)
        Try
            lstRequestData = (From p In Context.RC_REQUEST Where lstID.Contains(p.ID)).ToList
            For Each item In lstRequestData
                item.IS_APPROVED = 4
                item.STATUS_ID = 4103
                item.REMARK_CANCEL = "Ngày thực hiện hủy: " & Date.Now.ToString("dd/MM/yyyy") & " : " & reason
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusRequest")
            Throw ex
        End Try

    End Function


    Public Function AutoGenCodeRequestRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName
            '& " WHERE " & colName & " LIKE '" & firstChar & "%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "001"
            End If
            Dim number = Decimal.Parse(str.Substring(str.IndexOf(firstChar) + firstChar.Length))
            number = number + 1
            Dim lastChar = Format(number, "000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "Request_Portal"

    Public Function GetRequestPortal(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG3",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim emptyValue As String = "-"
            Dim query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From pas In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 1 And f.PROCESS_TYPE.ToUpper = "RECRUITMENT" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 1 And h.PROCESS_TYPE.ToUpper = "RECRUITMENT").Max(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From ee In Context.HU_EMPLOYEE.Where(Function(f) f.ID = pas.EMPLOYEE_APPROVED).DefaultIfEmpty()
                        From w_p In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 0 _
                           And f.PROCESS_TYPE.ToUpper = "RECRUITMENT" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 0 And h.PROCESS_TYPE.ToUpper = "RECRUITMENT").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From w_e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = w_p.EMPLOYEE_APPROVED).DefaultIfEmpty()
                        From rj In Context.PROCESS_APPROVED_STATUS.Where(Function(f) f.ID_REGGROUP = p.ID And f.APP_STATUS = 2 _
                           And f.PROCESS_TYPE.ToUpper = "RECRUITMENT" And f.APP_LEVEL = (Context.PROCESS_APPROVED_STATUS.Where(Function(h) h.ID_REGGROUP = p.ID And h.APP_STATUS = 2 And h.PROCESS_TYPE.ToUpper = "RECRUITMENT").Min(Function(k) k.APP_LEVEL))).DefaultIfEmpty()
                        From rj_e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = rj.EMPLOYEE_APPROVED).DefaultIfEmpty()
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From approve In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.IS_APPROVED).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From rc_prop In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RC_RECRUIT_PROPERTY).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GENDER_PRIORITY).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From req_name In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUIRER).DefaultIfEmpty
                        From p_pt In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PERSON_PT_RC).DefaultIfEmpty
                        From wp_name In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORKING_PLACE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.IS_PORTAL IsNot Nothing And p.IS_PORTAL <> 0
                        Select New RequestPortalDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                       .MALE_NUMBER = p.MALE_NUMBER,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_OTHER = p.REQUEST_OTHER,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .REMARK_REJECT = p.REMARK_REJECT,
                                       .RC_RECRUIT_PROPERTY = p.RC_RECRUIT_PROPERTY,
                                       .RC_RECRUIT_PROPERTY_NAME = rc_prop.NAME_VN,
                                       .IS_OVER_LIMIT = p.IS_OVER_LIMIT,
                                       .IS_REQUEST_COMPUTER = p.IS_REQUEST_COMPUTER,
                                       .FOREIGN_ABILITY = p.FOREIGN_ABILITY,
                                       .COMPUTER_APP_LEVEL = p.COMPUTER_APP_LEVEL,
                                       .GENDER_PRIORITY = p.GENDER_PRIORITY,
                                       .GENDER_PRIORITY_NAME = gender.NAME_VN,
                                       .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                       .UPLOAD_FILE = p.UPLOAD_FILE,
                                       .FILE_NAME = p.FILE_NAME,
                                       .CODE_RC = p.CODE_RC,
                                       .NAME_RC = p.NAME_RC,
                                       .REQUIRER = p.REQUIRER,
                                       .REQUIRER_NAME = req_name.FULLNAME_VN,
                                       .WORKING_PLACE_ID = p.WORKING_PLACE_ID,
                                       .WORKING_PLACE_NAME = wp_name.NAME_VN,
                                       .RC_GRPOS = p.RC_GRPOS,
                                       .SAL_OFFER = p.SAL_OFFER,
                                       .PERSON_PT_RC = p.PERSON_PT_RC,
                                       .PERSON_PT_RC_NAME = p_pt.FULLNAME_VN,
                                       .OTHER_QUALIFICATION = p.OTHER_QUALIFICATION,
                                       .IS_CLOCK = p.IS_CLOCK,
                                       .IS_APPROVED = p.IS_APPROVED,
                                       .IS_APPROVED_CODE = approve.CODE,
                                       .IS_APPROVED_NAME = approve.NAME_VN,
                                       .IS_PORTAL = p.IS_PORTAL,
                                       .EMPLOYEE_APPROVED_NAME = If(p.IS_APPROVED.Value = 0, w_e.EMPLOYEE_CODE & " - " & w_e.FULLNAME_VN, If(p.IS_APPROVED.Value = 1, ee.EMPLOYEE_CODE & " - " & ee.FULLNAME_VN, If(p.IS_APPROVED.Value = 2, rj_e.EMPLOYEE_CODE & " - " & rj_e.FULLNAME_VN, Nothing))),
                                       .IS_CONFIRM = p.IS_CONFIRM,
                                       .REMARK_CANCEL = p.REMARK_CANCEL}

            Dim lst = query
            'lst = lst.Where(Function(p) p.STATUS_ID IsNot Nothing)
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE <= _filter.FROM_DATE)
            End If
            If _filter.STATUS_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.REMARK_CANCEL <> "" Then
                lst = lst.Where(Function(p) p.REMARK_CANCEL.ToUpper.Contains(_filter.REMARK_CANCEL.ToUpper))
            End If
            If _filter.LEARNING_LEVEL_NAME <> "" Then
                lst = lst.Where(Function(p) p.LEARNING_LEVEL_NAME.ToUpper.Contains(_filter.LEARNING_LEVEL_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.RECRUIT_REASON <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON.ToUpper.Contains(_filter.RECRUIT_REASON.ToUpper))
            End If
            If _filter.REQUEST_EXPERIENCE <> "" Then
                lst = lst.Where(Function(p) p.REQUEST_EXPERIENCE.ToUpper.Contains(_filter.REQUEST_EXPERIENCE.ToUpper))
            End If
            If _filter.REQUEST_OTHER <> "" Then
                lst = lst.Where(Function(p) p.REQUEST_OTHER.ToUpper.Contains(_filter.REQUEST_OTHER.ToUpper))
            End If
            If _filter.RECRUIT_REASON_NAME <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
            End If
            If _filter.CONTRACT_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
            End If
            If _filter.IS_APPROVED_NAME <> "" Then
                lst = lst.Where(Function(p) p.IS_APPROVED_NAME.ToUpper.Contains(_filter.IS_APPROVED_NAME.ToUpper))
            End If
            If _filter.DESCRIPTION <> "" Then
                lst = lst.Where(Function(p) p.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.MALE_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.MALE_NUMBER = _filter.MALE_NUMBER)
            End If
            If _filter.REQUIRER IsNot Nothing Then
                lst = lst.Where(Function(p) p.REQUIRER = _filter.REQUIRER)
            End If
            If _filter.AGE_FROM IsNot Nothing Then
                lst = lst.Where(Function(p) p.AGE_FROM = _filter.AGE_FROM)
            End If
            If _filter.EXPERIENCE_NUMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPERIENCE_NUMBER = _filter.EXPERIENCE_NUMBER)
            End If
            If _filter.IS_IN_PLAN IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_IN_PLAN = _filter.IS_IN_PLAN)
            End If
            If _filter.AGE_TO IsNot Nothing Then
                lst = lst.Where(Function(p) p.AGE_TO = _filter.AGE_TO)
            End If
            If _filter.SEND_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            End If
            If IsNumeric(_filter.IS_APPROVED) Then
                lst = lst.Where(Function(p) p.IS_APPROVED = _filter.IS_APPROVED)
            End If
            If IsNumeric(_filter.IS_CONFIRM) Then
                If _filter.IS_CONFIRM = 0 Then
                    lst = lst.Where(Function(p) p.IS_CONFIRM Is Nothing OrElse p.IS_CONFIRM = _filter.IS_CONFIRM)
                Else
                    lst = lst.Where(Function(p) p.IS_CONFIRM = _filter.IS_CONFIRM)
                End If
            End If
            If _filter.EMPLOYEE_APPROVED_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_APPROVED_NAME.ToUpper.Contains(_filter.EMPLOYEE_APPROVED_NAME.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestPortal")
            Throw ex
        End Try

    End Function
    Public Function GetRequestPortal_Approve(ByVal _filter As RequestPortalDTO, ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer, ByVal _param As ParamDTO,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of RequestPortalDTO)

        Try
            Dim dtData As DataTable

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})

                dtData = cls.ExecuteStore("PKG_RECRUITMENT.GET_REQUESTPORTAL_APPROVE",
                                           New With {.P_EMPLOYEE_APPROVED = _filter.EMPLOYEE_APPROVED,
                                                     .P_IS_APPROVED = _filter.IS_APPROVED,
                                                     .P_USERNAME = log.Username,
                                                     .P_ORG_ID = _filter.ORG_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            End Using
            If dtData.Rows.Count > 0 Then

                Dim query = From row As DataRow In dtData.Rows
                            Select New RequestPortalDTO With {.ID = If(Not IsNumeric(row("ID")), Nothing, Decimal.Parse(row("ID").ToString)),
                                                           .EXPECTED_JOIN_DATE = If(row("EXPECTED_JOIN_DATE") Is Nothing, Nothing, row("EXPECTED_JOIN_DATE")),
                                                           .ORG_ID = If(Not IsNumeric(row("ORG_ID")), Nothing, Decimal.Parse(row("ORG_ID").ToString)),
                                                           .ORG_NAME = If(IsDBNull(row("ORG_NAME")), "", row("ORG_NAME").ToString),
                                                           .ORG_DESC = If(IsDBNull(row("ORG_DESC")), "", row("ORG_DESC").ToString),
                                                           .TITLE_ID = If(Not IsNumeric(row("TITLE_ID")), Nothing, Decimal.Parse(row("TITLE_ID").ToString)),
                                                           .TITLE_NAME = If(IsDBNull(row("TITLE_NAME")), "", row("TITLE_NAME").ToString),
                                                           .AGE_FROM = If(Not IsNumeric(row("AGE_FROM")), Nothing, Decimal.Parse(row("AGE_FROM").ToString)),
                                                           .AGE_TO = If(Not IsNumeric(row("AGE_TO")), Nothing, Decimal.Parse(row("AGE_TO").ToString)),
                                                           .CONTRACT_TYPE_ID = If(Not IsNumeric(row("CONTRACT_TYPE_ID")), Nothing, Decimal.Parse(row("CONTRACT_TYPE_ID").ToString)),
                                                           .CONTRACT_TYPE_NAME = If(IsDBNull(row("CONTRACT_TYPE_NAME")), "", row("CONTRACT_TYPE_NAME").ToString),
                                                           .DESCRIPTION = If(IsDBNull(row("DESCRIPTION")), "", row("DESCRIPTION").ToString),
                                                           .EXPERIENCE_NUMBER = If(Not IsNumeric(row("EXPERIENCE_NUMBER")), Nothing, Decimal.Parse(row("EXPERIENCE_NUMBER").ToString)),
                                                           .FEMALE_NUMBER = If(Not IsNumeric(row("FEMALE_NUMBER")), Nothing, Decimal.Parse(row("FEMALE_NUMBER").ToString)),
                                                           .IS_IN_PLAN = If(Not IsNumeric(row("IS_IN_PLAN")), Nothing, Decimal.Parse(row("IS_IN_PLAN").ToString)),
                                                           .LEARNING_LEVEL_ID = If(Not IsNumeric(row("LEARNING_LEVEL_ID")), Nothing, Decimal.Parse(row("LEARNING_LEVEL_ID").ToString)),
                                                           .LEARNING_LEVEL_NAME = If(IsDBNull(row("LEARNING_LEVEL_NAME")), "", row("LEARNING_LEVEL_NAME").ToString),
                                                           .MALE_NUMBER = If(Not IsNumeric(row("MALE_NUMBER")), Nothing, Decimal.Parse(row("MALE_NUMBER").ToString)),
                                                           .RECRUIT_REASON = If(IsDBNull(row("RECRUIT_REASON")), "", row("RECRUIT_REASON").ToString),
                                                           .RECRUIT_REASON_ID = If(Not IsNumeric(row("RECRUIT_REASON_ID")), Nothing, Decimal.Parse(row("RECRUIT_REASON_ID").ToString)),
                                                           .RECRUIT_REASON_NAME = If(IsDBNull(row("RECRUIT_REASON_NAME")), "", row("RECRUIT_REASON_NAME").ToString),
                                                           .REQUEST_EXPERIENCE = If(IsDBNull(row("REQUEST_EXPERIENCE")), "", row("REQUEST_EXPERIENCE").ToString),
                                                           .REQUEST_OTHER = If(IsDBNull(row("REQUEST_OTHER")), "", row("REQUEST_OTHER").ToString),
                                                           .REMARK = If(IsDBNull(row("REMARK")), "", row("REMARK").ToString),
                                                           .SEND_DATE = If(IsDBNull(row("SEND_DATE")), Nothing, row("SEND_DATE")),
                                                           .STATUS_ID = If(Not IsNumeric(row("STATUS_ID")), Nothing, Decimal.Parse(row("STATUS_ID").ToString)),
                                                           .STATUS_NAME = If(IsDBNull(row("STATUS_NAME")), "", row("STATUS_NAME").ToString),
                                                           .CREATED_DATE = If(IsDBNull(row("CREATED_DATE")), Nothing, row("CREATED_DATE")),
                                                           .REMARK_REJECT = If(IsDBNull(row("REMARK_REJECT")), "", row("REMARK_REJECT").ToString),
                                                           .RC_RECRUIT_PROPERTY = If(Not IsNumeric(row("RC_RECRUIT_PROPERTY")), Nothing, Decimal.Parse(row("RC_RECRUIT_PROPERTY").ToString)),
                                                           .RC_RECRUIT_PROPERTY_NAME = If(IsDBNull(row("RC_RECRUIT_PROPERTY_NAME")), "", row("RC_RECRUIT_PROPERTY_NAME").ToString),
                                                           .IS_OVER_LIMIT = If(Not IsNumeric(row("IS_OVER_LIMIT")), Nothing, Decimal.Parse(row("IS_OVER_LIMIT").ToString)),
                                                           .IS_REQUEST_COMPUTER = If(Not IsNumeric(row("IS_REQUEST_COMPUTER")), Nothing, Decimal.Parse(row("IS_REQUEST_COMPUTER").ToString)),
                                                           .FOREIGN_ABILITY = If(IsDBNull(row("FOREIGN_ABILITY")), "", row("FOREIGN_ABILITY").ToString),
                                                           .COMPUTER_APP_LEVEL = If(IsDBNull(row("COMPUTER_APP_LEVEL")), "", row("COMPUTER_APP_LEVEL").ToString),
                                                           .GENDER_PRIORITY = If(Not IsNumeric(row("GENDER_PRIORITY")), Nothing, Decimal.Parse(row("GENDER_PRIORITY").ToString)),
                                                           .GENDER_PRIORITY_NAME = If(IsDBNull(row("GENDER_PRIORITY_NAME")), "", row("GENDER_PRIORITY_NAME").ToString),
                                                           .RECRUIT_NUMBER = If(Not IsNumeric(row("RECRUIT_NUMBER")), Nothing, Decimal.Parse(row("RECRUIT_NUMBER").ToString)),
                                                           .UPLOAD_FILE = If(IsDBNull(row("UPLOAD_FILE")), "", row("UPLOAD_FILE").ToString),
                                                           .FILE_NAME = If(IsDBNull(row("FILE_NAME")), "", row("FILE_NAME").ToString),
                                                           .CODE_RC = If(IsDBNull(row("CODE_RC")), "", row("CODE_RC").ToString),
                                                           .NAME_RC = If(IsDBNull(row("NAME_RC")), "", row("NAME_RC").ToString),
                                                           .REQUIRER = If(Not IsNumeric(row("REQUIRER")), Nothing, Decimal.Parse(row("REQUIRER").ToString)),
                                                           .REQUIRER_NAME = If(IsDBNull(row("REQUIRER_NAME")), "", row("REQUIRER_NAME").ToString),
                                                           .WORKING_PLACE_ID = If(Not IsNumeric(row("WORKING_PLACE_ID")), Nothing, Decimal.Parse(row("WORKING_PLACE_ID").ToString)),
                                                           .WORKING_PLACE_NAME = If(IsDBNull(row("WORKING_PLACE_NAME")), "", row("WORKING_PLACE_NAME").ToString),
                                                           .RC_GRPOS = If(IsDBNull(row("RC_GRPOS")), "", row("RC_GRPOS").ToString),
                                                           .SAL_OFFER = If(Not IsNumeric(row("SAL_OFFER")), Nothing, Decimal.Parse(row("SAL_OFFER").ToString)),
                                                           .PERSON_PT_RC = If(Not IsNumeric(row("PERSON_PT_RC")), Nothing, Decimal.Parse(row("PERSON_PT_RC").ToString)),
                                                           .PERSON_PT_RC_NAME = If(IsDBNull(row("PERSON_PT_RC_NAME")), "", row("PERSON_PT_RC_NAME").ToString),
                                                           .OTHER_QUALIFICATION = If(IsDBNull(row("OTHER_QUALIFICATION")), "", row("OTHER_QUALIFICATION").ToString),
                                                           .IS_CLOCK = If(Not IsNumeric(row("IS_CLOCK")), Nothing, Decimal.Parse(row("IS_CLOCK").ToString)),
                                                           .IS_APPROVED = If(Not IsNumeric(row("IS_APPROVED")), Nothing, Decimal.Parse(row("IS_APPROVED").ToString)),
                                                           .IS_APPROVED_CODE = If(IsDBNull(row("IS_APPROVED_CODE")), "", row("IS_APPROVED_CODE").ToString),
                                                           .IS_APPROVED_NAME = If(IsDBNull(row("IS_APPROVED_NAME")), "", row("IS_APPROVED_NAME").ToString)}

                Dim lst = (From P In query.AsQueryable)
                'lst = lst.Where(Function(p) p.STATUS_ID IsNot Nothing)
                If _filter.FROM_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.SEND_DATE >= _filter.FROM_DATE)
                End If
                If _filter.TO_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.SEND_DATE <= _filter.FROM_DATE)
                End If
                If _filter.STATUS_ID IsNot Nothing Then
                    lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
                End If
                If _filter.ORG_NAME <> "" Then
                    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If
                If _filter.LEARNING_LEVEL_NAME <> "" Then
                    lst = lst.Where(Function(p) p.LEARNING_LEVEL_NAME.ToUpper.Contains(_filter.LEARNING_LEVEL_NAME.ToUpper))
                End If
                If _filter.TITLE_NAME <> "" Then
                    lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
                End If
                If _filter.RECRUIT_REASON <> "" Then
                    lst = lst.Where(Function(p) p.RECRUIT_REASON.ToUpper.Contains(_filter.RECRUIT_REASON.ToUpper))
                End If
                If _filter.REQUEST_EXPERIENCE <> "" Then
                    lst = lst.Where(Function(p) p.REQUEST_EXPERIENCE.ToUpper.Contains(_filter.REQUEST_EXPERIENCE.ToUpper))
                End If
                If _filter.REQUEST_OTHER <> "" Then
                    lst = lst.Where(Function(p) p.REQUEST_OTHER.ToUpper.Contains(_filter.REQUEST_OTHER.ToUpper))
                End If
                If _filter.RECRUIT_REASON_NAME <> "" Then
                    lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
                End If
                If _filter.CONTRACT_TYPE_NAME <> "" Then
                    lst = lst.Where(Function(p) p.CONTRACT_TYPE_NAME.ToUpper.Contains(_filter.CONTRACT_TYPE_NAME.ToUpper))
                End If
                If _filter.IS_APPROVED_NAME <> "" Then
                    lst = lst.Where(Function(p) p.IS_APPROVED_NAME.ToUpper.Contains(_filter.IS_APPROVED_NAME.ToUpper))
                End If
                If _filter.DESCRIPTION <> "" Then
                    lst = lst.Where(Function(p) p.DESCRIPTION.ToUpper.Contains(_filter.DESCRIPTION.ToUpper))
                End If
                If _filter.REMARK <> "" Then
                    lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
                End If
                If _filter.MALE_NUMBER IsNot Nothing Then
                    lst = lst.Where(Function(p) p.MALE_NUMBER = _filter.MALE_NUMBER)
                End If
                If _filter.AGE_FROM IsNot Nothing Then
                    lst = lst.Where(Function(p) p.AGE_FROM = _filter.AGE_FROM)
                End If
                If _filter.EXPERIENCE_NUMBER IsNot Nothing Then
                    lst = lst.Where(Function(p) p.EXPERIENCE_NUMBER = _filter.EXPERIENCE_NUMBER)
                End If
                If _filter.IS_IN_PLAN IsNot Nothing Then
                    lst = lst.Where(Function(p) p.IS_IN_PLAN = _filter.IS_IN_PLAN)
                End If
                If _filter.AGE_TO IsNot Nothing Then
                    lst = lst.Where(Function(p) p.AGE_TO = _filter.AGE_TO)
                End If
                If _filter.SEND_DATE IsNot Nothing Then
                    lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
                End If
                If IsNumeric(_filter.IS_APPROVED) Then
                    lst = lst.Where(Function(p) p.IS_APPROVED = _filter.IS_APPROVED)
                End If
                lst = lst.OrderBy(Sorts)
                Total = lst.Count
                lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
                Return lst.ToList
            End If
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestPortal")
            Throw ex
        End Try

    End Function

    Public Function GetRequestPortalByID(ByVal _filter As RequestPortalDTO) As RequestPortalDTO

        Try

            Dim query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From approve In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.IS_APPROVED).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From req In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.REQUIRER).DefaultIfEmpty
                        From pt_rc In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PERSON_PT_RC).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New RequestPortalDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                       .MALE_NUMBER = p.MALE_NUMBER,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_OTHER = p.REQUEST_OTHER,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .LANGUAGE = p.LANGUAGE,
                                       .WORKING_PLACE_ID = p.WORKING_PLACE_ID,
                                       .LANGUAGELEVEL = p.LANGUAGELEVEL,
                                       .LANGUAGESCORES = p.LANGUAGESCORES,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .MAINTASK = p.MAINTASK,
                                       .QUALIFICATION = p.QUALIFICATION,
                                       .DESCRIPTIONATTACHFILE = p.DESCRIPTIONATTACHFILE,
                                       .COMPUTER_LEVEL = p.COMPUTER_LEVEL,
                                       .RC_PLAN_ID = p.RC_PLAN_ID,
                                       .RC_RECRUIT_PROPERTY = p.RC_RECRUIT_PROPERTY,
                                       .IS_OVER_LIMIT = p.IS_OVER_LIMIT,
                                       .IS_REQUEST_COMPUTER = p.IS_REQUEST_COMPUTER,
                                       .LOCATION_ID = p.LOCATION_ID,
                                       .FOREIGN_ABILITY = p.FOREIGN_ABILITY,
                                       .COMPUTER_APP_LEVEL = p.COMPUTER_APP_LEVEL,
                                       .GENDER_PRIORITY = p.GENDER_PRIORITY,
                                       .RECRUIT_NUMBER = p.RECRUIT_NUMBER,
                                       .FILE_NAME = p.FILE_NAME,
                                       .UPLOAD_FILE = p.UPLOAD_FILE,
                                       .CODE_RC = p.CODE_RC,
                                       .NAME_RC = p.NAME_RC,
                                        .REQUIRER = p.REQUIRER,
                                        .REQUIRER_NAME = req.EMPLOYEE_CODE + " - " + req.FULLNAME_VN,
                                        .RC_GRPOS = p.RC_GRPOS,
                                        .SAL_OFFER = p.SAL_OFFER,
                                        .PERSON_PT_RC = p.PERSON_PT_RC,
                                        .PERSON_PT_RC_NAME = pt_rc.EMPLOYEE_CODE + " - " + pt_rc.FULLNAME_VN,
                                        .IS_APPROVED = p.IS_APPROVED,
                                        .IS_APPROVED_CODE = approve.CODE,
                                        .IS_APPROVED_NAME = approve.NAME_VN,
                                        .OTHER_QUALIFICATION = p.OTHER_QUALIFICATION,
                                       .IS_PORTAL = p.IS_PORTAL}

            Dim obj = query.FirstOrDefault
            Dim lstEmp = (From p In Context.RC_REQUEST_EMP
                          From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                          From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                          From t In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID)
                          Where p.RC_REQUEST_ID = obj.ID
                          Select New RequestEmpPortalDTO With {
                               .EMPLOYEE_ID = p.EMPLOYEE_ID,
                               .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                               .EMPLOYEE_NAME = emp.FULLNAME_VN,
                               .ORG_NAME = o.NAME_VN,
                               .TITLE_NAME = t.NAME_VN,
                               .JOIN_DATE = emp.JOIN_DATE}).ToList
            obj.lstEmp = lstEmp
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestByID")
            Throw ex
        End Try


    End Function

    Public Function GetRequestPortalByID_ReturnList(ByVal _filter As RequestPortalDTO) As List(Of RequestPortalDTO)

        Try

            Dim query = From p In Context.RC_REQUEST
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From learning In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        From ctracttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.LEARNING_LEVEL_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New RequestPortalDTO With {
                                       .ID = p.ID,
                                       .EXPECTED_JOIN_DATE = p.EXPECTED_JOIN_DATE,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ctracttype.NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .FEMALE_NUMBER = p.FEMALE_NUMBER,
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .LEARNING_LEVEL_NAME = learning.NAME_VN,
                                       .MALE_NUMBER = p.MALE_NUMBER,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON_NAME = reason.NAME_VN,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_OTHER = p.REQUEST_OTHER,
                                       .REMARK = p.REMARK,
                                       .SEND_DATE = p.SEND_DATE,
                                       .STATUS_ID = p.STATUS_ID,
                                       .STATUS_NAME = status.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .LANGUAGE = p.LANGUAGE,
                                       .LANGUAGELEVEL = p.LANGUAGELEVEL,
                                       .LANGUAGESCORES = p.LANGUAGESCORES,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .MAINTASK = p.MAINTASK,
                                       .QUALIFICATION = p.QUALIFICATION,
                                       .DESCRIPTIONATTACHFILE = p.DESCRIPTIONATTACHFILE,
                                       .COMPUTER_LEVEL = p.COMPUTER_LEVEL,
                                       .IS_PORTAL = p.IS_PORTAL}

            'Dim obj = query.FirstOrDefault
            Dim obj = query
            'Dim lstEmp = (From p In Context.RC_REQUEST_EMP
            '               From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '               Where p.RC_REQUEST_ID = obj.ID
            '               Select New RequestEmpDTO With {
            '                   .EMPLOYEE_ID = p.EMPLOYEE_ID,
            '                   .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
            '                   .EMPLOYEE_NAME = emp.FULLNAME_VN}).ToList

            'obj.lstEmp = lstEmp
            Return obj.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRequestByID")
            Throw ex
        End Try


    End Function

    Public Function CheckExistRequestPortal(ByVal objRequest As RequestPortalDTO) As Boolean
        Try
            Dim query = (From p In Context.RC_REQUEST
                         Where p.ORG_ID = objRequest.ORG_ID _
                        And p.TITLE_ID = objRequest.TITLE_ID _
                        And p.SEND_DATE = objRequest.SEND_DATE _
                        And p.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
                         Select p.ID).ToList()
            Return query.Count > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequestData As New RC_REQUEST
        Try
            'If CheckExistRequestPortal(objRequest) Then
            '    Return False
            'End If
            objRequestData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST.EntitySet.Name)
            objRequestData.IS_IN_PLAN = objRequest.IS_IN_PLAN
            objRequestData.ORG_ID = objRequest.ORG_ID
            objRequestData.TITLE_ID = objRequest.TITLE_ID
            objRequestData.JOBGRADE_ID = objRequest.JOBGRADE_ID
            objRequestData.CONTRACT_TYPE_ID = objRequest.CONTRACT_TYPE_ID
            objRequestData.RECRUIT_REASON_ID = objRequest.RECRUIT_REASON_ID
            objRequestData.RECRUIT_REASON = objRequest.RECRUIT_REASON
            objRequestData.LEARNING_LEVEL_ID = objRequest.LEARNING_LEVEL_ID
            objRequestData.AGE_FROM = objRequest.AGE_FROM
            objRequestData.AGE_TO = objRequest.AGE_TO
            objRequestData.SEND_DATE = objRequest.SEND_DATE
            objRequestData.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
            objRequestData.EXPERIENCE_NUMBER = objRequest.EXPERIENCE_NUMBER
            objRequestData.MALE_NUMBER = objRequest.MALE_NUMBER
            objRequestData.FEMALE_NUMBER = objRequest.FEMALE_NUMBER
            objRequestData.DESCRIPTION = objRequest.DESCRIPTION
            objRequestData.REQUEST_EXPERIENCE = objRequest.REQUEST_EXPERIENCE
            objRequestData.REQUEST_OTHER = objRequest.REQUEST_OTHER
            objRequestData.REMARK = objRequest.REMARK
            objRequestData.LANGUAGE = objRequest.LANGUAGE
            objRequestData.LANGUAGELEVEL = objRequest.LANGUAGELEVEL
            objRequestData.LANGUAGESCORES = objRequest.LANGUAGESCORES
            objRequestData.SPECIALSKILLS = objRequest.SPECIALSKILLS
            objRequestData.QUALIFICATION = objRequest.QUALIFICATION
            objRequestData.MAINTASK = objRequest.MAINTASK
            objRequestData.DESCRIPTIONATTACHFILE = objRequest.DESCRIPTIONATTACHFILE
            objRequestData.COMPUTER_LEVEL = objRequest.COMPUTER_LEVEL
            objRequestData.STATUS_ID = objRequest.STATUS_ID
            objRequestData.RC_PLAN_ID = objRequest.RC_PLAN_ID
            objRequestData.RC_RECRUIT_PROPERTY = objRequest.RC_RECRUIT_PROPERTY
            objRequestData.IS_OVER_LIMIT = objRequest.IS_OVER_LIMIT
            objRequestData.IS_REQUEST_COMPUTER = objRequest.IS_REQUEST_COMPUTER
            objRequestData.FOREIGN_ABILITY = objRequest.FOREIGN_ABILITY
            objRequestData.COMPUTER_APP_LEVEL = objRequest.COMPUTER_APP_LEVEL
            objRequestData.GENDER_PRIORITY = objRequest.GENDER_PRIORITY
            objRequestData.RECRUIT_NUMBER = objRequest.RECRUIT_NUMBER
            objRequestData.FILE_NAME = objRequest.FILE_NAME
            objRequestData.UPLOAD_FILE = objRequest.UPLOAD_FILE
            objRequestData.LOCATION_ID = objRequest.LOCATION_ID

            objRequestData.CODE_RC = objRequest.CODE_RC
            objRequestData.NAME_RC = objRequest.NAME_RC
            objRequestData.REQUIRER = objRequest.REQUIRER
            objRequestData.RC_GRPOS = objRequest.RC_GRPOS
            objRequestData.WORKING_PLACE_ID = objRequest.WORKING_PLACE_ID
            objRequestData.SAL_OFFER = objRequest.SAL_OFFER
            objRequestData.PERSON_PT_RC = objRequest.PERSON_PT_RC
            'objRequestData.IS_APPROVED = (From p In Context.OT_OTHER_LIST
            '                              From OTTYPE In Context.OT_OTHER_LIST_TYPE.Where(Function(F) F.ID = p.TYPE_ID)
            '                              Where p.CODE.Trim.ToUpper = "R" Select p.ID).FirstOrDefault
            objRequestData.IS_APPROVED = 3
            objRequestData.OTHER_QUALIFICATION = objRequest.OTHER_QUALIFICATION
            objRequestData.IS_PORTAL = -1
            Context.RC_REQUEST.AddObject(objRequestData)
            If objRequest.lstEmp IsNot Nothing Then
                For Each item In objRequest.lstEmp
                    Dim objRequestEmpData As New RC_REQUEST_EMP
                    objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                    objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                    objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                Next
            End If
            Context.SaveChanges(log)
            'If objRequest.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
            '    Dim lstDecimal As New List(Of Decimal)
            '    lstDecimal.Add(objRequestData.ID)
            '    UpdateStatusRequestPortal(lstDecimal, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, log, objRequest.IS_INSERT_PRO)
            'End If

            gID = objRequestData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRequest")
            Throw ex
        End Try
    End Function

    Public Function ModifyRequestPortal(ByVal objRequest As RequestPortalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRequestData As New RC_REQUEST With {.ID = objRequest.ID}
        Try
            Context.RC_REQUEST.Attach(objRequestData)
            objRequestData.IS_IN_PLAN = objRequest.IS_IN_PLAN
            objRequestData.ORG_ID = objRequest.ORG_ID
            objRequestData.TITLE_ID = objRequest.TITLE_ID
            objRequestData.JOBGRADE_ID = objRequest.JOBGRADE_ID
            objRequestData.CONTRACT_TYPE_ID = objRequest.CONTRACT_TYPE_ID
            objRequestData.RECRUIT_REASON_ID = objRequest.RECRUIT_REASON_ID
            objRequestData.RECRUIT_REASON = objRequest.RECRUIT_REASON
            objRequestData.LEARNING_LEVEL_ID = objRequest.LEARNING_LEVEL_ID
            objRequestData.AGE_FROM = objRequest.AGE_FROM
            objRequestData.AGE_TO = objRequest.AGE_TO
            objRequestData.SEND_DATE = objRequest.SEND_DATE
            objRequestData.EXPECTED_JOIN_DATE = objRequest.EXPECTED_JOIN_DATE
            objRequestData.EXPERIENCE_NUMBER = objRequest.EXPERIENCE_NUMBER
            objRequestData.MALE_NUMBER = objRequest.MALE_NUMBER
            objRequestData.FEMALE_NUMBER = objRequest.FEMALE_NUMBER
            objRequestData.DESCRIPTION = objRequest.DESCRIPTION
            objRequestData.REQUEST_EXPERIENCE = objRequest.REQUEST_EXPERIENCE
            objRequestData.REQUEST_OTHER = objRequest.REQUEST_OTHER
            objRequestData.REMARK = objRequest.REMARK
            objRequestData.LANGUAGE = objRequest.LANGUAGE
            objRequestData.LANGUAGELEVEL = objRequest.LANGUAGELEVEL
            objRequestData.LANGUAGESCORES = objRequest.LANGUAGESCORES
            objRequestData.SPECIALSKILLS = objRequest.SPECIALSKILLS
            objRequestData.QUALIFICATION = objRequest.QUALIFICATION
            objRequestData.MAINTASK = objRequest.MAINTASK
            objRequestData.DESCRIPTIONATTACHFILE = objRequest.DESCRIPTIONATTACHFILE
            objRequestData.COMPUTER_LEVEL = objRequest.COMPUTER_LEVEL
            objRequestData.RC_RECRUIT_PROPERTY = objRequest.RC_RECRUIT_PROPERTY
            objRequestData.IS_OVER_LIMIT = objRequest.IS_OVER_LIMIT
            objRequestData.IS_REQUEST_COMPUTER = objRequest.IS_REQUEST_COMPUTER
            objRequestData.FOREIGN_ABILITY = objRequest.FOREIGN_ABILITY
            objRequestData.COMPUTER_APP_LEVEL = objRequest.COMPUTER_APP_LEVEL
            objRequestData.GENDER_PRIORITY = objRequest.GENDER_PRIORITY
            objRequestData.RECRUIT_NUMBER = objRequest.RECRUIT_NUMBER
            objRequestData.FILE_NAME = objRequest.FILE_NAME
            objRequestData.UPLOAD_FILE = objRequest.UPLOAD_FILE
            objRequestData.LOCATION_ID = objRequest.LOCATION_ID
            objRequestData.STATUS_ID = objRequest.STATUS_ID
            objRequestData.CODE_RC = objRequest.CODE_RC
            objRequestData.NAME_RC = objRequest.NAME_RC
            objRequestData.REQUIRER = objRequest.REQUIRER
            objRequestData.RC_GRPOS = objRequest.RC_GRPOS
            objRequestData.WORKING_PLACE_ID = objRequest.WORKING_PLACE_ID
            objRequestData.SAL_OFFER = objRequest.SAL_OFFER
            objRequestData.PERSON_PT_RC = objRequest.PERSON_PT_RC
            'objRequestData.IS_APPROVED = objRequest.IS_APPROVED
            objRequestData.OTHER_QUALIFICATION = objRequest.OTHER_QUALIFICATION
            objRequestData.IS_PORTAL = -1
            Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = objRequestData.ID).ToList
            For Each item In lstRegEmp
                Context.RC_REQUEST_EMP.DeleteObject(item)
            Next
            'Context.SaveChanges(log)
            If objRequest.lstEmp IsNot Nothing Then
                For Each item In objRequest.lstEmp
                    Dim objRequestEmpData As New RC_REQUEST_EMP
                    objRequestEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_REQUEST_EMP.EntitySet.Name)
                    objRequestEmpData.RC_REQUEST_ID = objRequestData.ID
                    objRequestEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_REQUEST_EMP.AddObject(objRequestEmpData)
                Next

            End If

            'If objRequest.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
            '    If CheckExistIDRequestPortalIn_RC_Program(objRequestData.ID) Then
            '        Dim lstDecimal As New List(Of Decimal)
            '        lstDecimal.Add(objRequestData.ID)
            '        UpdateStatusRequestPortal(lstDecimal, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID, log, objRequest.IS_INSERT_PRO)
            '    End If
            'End If
            Context.SaveChanges(log)
            gID = objRequestData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyRequest")
            Throw ex
        End Try

    End Function

    Public Function CheckExistIDRequestPortalIn_RC_Program(ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_PROGRAM Where p.RC_REQUEST_ID = _id).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistData")
            Throw ex
        End Try
    End Function

    Public Function DeleteRequestPortal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRequestData As List(Of RC_REQUEST)
        Try
            lstRequestData = (From p In Context.RC_REQUEST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRequestData.Count - 1
                Dim id = lstRequestData(index).ID
                Dim lstRegEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = id).ToList
                For Each item In lstRegEmp
                    Context.RC_REQUEST_EMP.DeleteObject(item)
                Next

                Dim process = (From p In Context.PROCESS_APPROVED_STATUS Where p.ID_REGGROUP = id And p.PROCESS_TYPE = "RECRUITMENT").ToList
                For Each item In process
                    Context.PROCESS_APPROVED_STATUS.DeleteObject(item)
                Next

                Context.RC_REQUEST.DeleteObject(lstRequestData(index))

            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteRequest")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusRequestPortal(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog, ByVal Insert_Pro As Boolean) As Boolean
        Dim lstRequestData As List(Of RC_REQUEST)
        Dim lstProgramData As List(Of RC_PROGRAM)
        Try
            lstRequestData = (From p In Context.RC_REQUEST Where lstID.Contains(p.ID)).ToList
            For Each item In lstRequestData
                item.STATUS_ID = status
                If status = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                    item.IS_PORTAL = 0
                    Dim lstEmp = (From p In Context.RC_REQUEST_EMP Where p.RC_REQUEST_ID = item.ID).ToList
                    lstProgramData = (From p In Context.RC_PROGRAM Where p.RC_REQUEST_ID = item.ID).ToList
                    If lstProgramData.Count = 0 AndAlso Insert_Pro = True Then
                        InsertProgram(item, lstEmp)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusRequest")
            Throw ex
        End Try

    End Function

    Public Function AutoGenCodeRequestPortalRC(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName
            '& " WHERE " & colName & " LIKE '" & firstChar & "%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "001"
            End If
            Dim number = Decimal.Parse(str.Substring(str.IndexOf(firstChar) + firstChar.Length))
            number = number + 1
            Dim lastChar = Format(number, "000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function
#End Region
#Region "Program"

    Public Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        Try
            Dim query As IQueryable(Of ProgramDTO)
            If _filter.IS_INSERT_PRO = False Or log.Username.ToUpper = "ADMIN" Then

                Using cls As New DataAccess.QueryData
                    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = _param.ORG_ID,
                                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
                End Using
                query = From p In Context.RC_PROGRAM
                        From PRE In Context.RC_REQUEST.Where(Function(f) f.ID = p.RC_REQUEST_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = PRE.REQUIRER).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From huv_org_id In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From cty In Context.HU_ORGANIZATION.Where(Function(f) f.ID = huv_org_id.ID2).DefaultIfEmpty
                        From sta In Context.RC_STAGE.Where(Function(f) f.ID = p.STAGE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From rectype In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_TYPE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Select New ProgramDTO With {
                                               .ID = p.ID,
                                               .CODE = p.CODE,
                                               .CODE_YCTD = PRE.CODE_RC,
                                               .PER_REQUEST = If(PRE.REQUIRER IsNot Nothing, e.EMPLOYEE_CODE + "-" + e.FULLNAME_VN, e.EMPLOYEE_CODE + "" + e.FULLNAME_VN),
                                               .ORG_ID = p.ORG_ID,
                                               .ORG_NAME = org.NAME_VN,
                                               .ORG_DESC = org.DESCRIPTION_PATH,
                                               .TITLE_NAME = title.NAME_VN,
                                               .IS_IN_PLAN = If(p.IS_IN_PLAN Is Nothing Or p.IS_IN_PLAN = 0, False, True),
                                               .SEND_DATE = p.SEND_DATE,
                                               .REMARK = p.REMARK,
                                               .RECRUIT_REASON_NAME = reason.NAME_VN,
                                               .RECRUIT_REASON = p.RECRUIT_REASON,
                                               .REQUEST_NUMBER = p.REQUEST_NUMBER,
                                               .RECRUIT_START = p.RECRUIT_START,
                                               .RECEIVE_END = p.RECEIVE_END,
                                               .STATUS_ID = p.STATUS_ID,
                                               .STATUS_NAME = status.NAME_VN,
                                               .CREATED_DATE = p.CREATED_DATE,
                                               .STAGE_ID = p.STAGE_ID,
                                               .ORG_NAME_CTY = cty.NAME_VN,
                                               .RECRUIT_TYPE_ID = rectype.CODE,
                                               .EXPECTED_JOIN_DATE = PRE.EXPECTED_JOIN_DATE,
                                               .CANDIDATE_COUNT = (From can In Context.RC_PROGRAM_CANDIDATE
                                                                   Where can.RC_PROGRAM_ID = p.ID).Count,
                                           .CANDIDATE_RECEIVED = (From can In Context.RC_PROGRAM_CANDIDATE
                                                                  Where can.RC_PROGRAM_ID = p.ID And can.STATUS_ID = "NHANVIEN").Count,
                                               .EXAMP_COUNT = (From examp In Context.RC_PROGRAM_EXAMS
                                                               Where examp.RC_PROGRAM_ID = p.ID).Count}

                '.IS_REQUEST_COMPUTER = If(PRE.IS_REQUEST_COMPUTER = 0 Or PRE.IS_REQUEST_COMPUTER Is Nothing, False, True),
                '                               .SUPPORT_NAME = If(PRE.IS_REQUEST_COMPUTER = 0 Or PRE.IS_REQUEST_COMPUTER Is Nothing, "Không", "Có"),
                '.CANDIDATE_REQUEST = (From reg In Context.RC_PLAN_REG
                '                                                  Where reg.SEND_DATE.Value.Year = p.SEND_DATE.Value.Year And _
                '                                                  reg.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID And _
                '                                                  reg.ORG_ID = p.ORG_ID And reg.TITLE_ID = p.TITLE_ID Select reg.RECRUIT_NUMBER).Sum

                ' = x.RECRUIT_NUMBER, '
            Else
                query = From p In Context.RC_PROGRAM
                        From PRE In Context.RC_REQUEST.Where(Function(f) f.ID = p.RC_REQUEST_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = PRE.REQUIRER).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From huv_org_id In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From cty In Context.HU_ORGANIZATION.Where(Function(f) f.ID = huv_org_id.ID2).DefaultIfEmpty
                        From sta In Context.RC_STAGE.Where(Function(f) f.ID = p.STAGE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From rectype In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_TYPE_ID).DefaultIfEmpty
                        Where PRE.PERSON_PT_RC = _filter.PERSON_PT_RC
                        Select New ProgramDTO With {
                                               .ID = p.ID,
                                               .CODE = p.CODE,
                                               .CODE_YCTD = PRE.CODE_RC,
                                               .PER_REQUEST = If(PRE.REQUIRER IsNot Nothing, e.EMPLOYEE_CODE + "-" + e.FULLNAME_VN, e.EMPLOYEE_CODE + "" + e.FULLNAME_VN),
                                               .ORG_ID = p.ORG_ID,
                                               .ORG_NAME = org.NAME_VN,
                                               .ORG_DESC = org.DESCRIPTION_PATH,
                                               .TITLE_NAME = title.NAME_VN,
                                               .IS_IN_PLAN = If(p.IS_IN_PLAN Is Nothing Or p.IS_IN_PLAN = 0, False, True),
                                               .SEND_DATE = p.SEND_DATE,
                                               .REMARK = p.REMARK,
                                               .RECRUIT_REASON_NAME = reason.NAME_VN,
                                               .RECRUIT_REASON = p.RECRUIT_REASON,
                                               .REQUEST_NUMBER = p.REQUEST_NUMBER,
                                               .RECRUIT_START = p.RECRUIT_START,
                                               .RECEIVE_END = p.RECEIVE_END,
                                               .STATUS_ID = p.STATUS_ID,
                                               .STATUS_NAME = status.NAME_VN,
                                               .CREATED_DATE = p.CREATED_DATE,
                                               .STAGE_ID = p.STAGE_ID,
                                               .ORG_NAME_CTY = cty.NAME_VN,
                                               .RECRUIT_TYPE_ID = rectype.CODE,
                                               .EXPECTED_JOIN_DATE = PRE.EXPECTED_JOIN_DATE,
                                               .CANDIDATE_COUNT = (From can In Context.RC_PROGRAM_CANDIDATE
                                                                   Where can.RC_PROGRAM_ID = p.ID).Count,
                                           .CANDIDATE_RECEIVED = (From can In Context.RC_PROGRAM_CANDIDATE
                                                                  Where can.RC_PROGRAM_ID = p.ID And can.STATUS_ID = "NHANVIEN").Count,
                                               .EXAMP_COUNT = (From examp In Context.RC_PROGRAM_EXAMS
                                                               Where examp.RC_PROGRAM_ID = p.ID).Count}
            End If
            Dim lst = query
            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE <= _filter.TO_DATE)
            End If
            If _filter.SEND_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            End If
            If _filter.EXPECTED_JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPECTED_JOIN_DATE = _filter.EXPECTED_JOIN_DATE)
            End If
            'If _filter.RECRUIT_START IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECRUIT_START = _filter.RECRUIT_START)
            'End If
            'If _filter.RECEIVE_END IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECEIVE_END = _filter.RECEIVE_END)
            'End If
            If _filter.ORG_NAME_CTY IsNot Nothing Then
                lst = lst.Where(Function(p) p.ORG_NAME_CTY.ToUpper.Contains(_filter.ORG_NAME_CTY.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            'If _filter.SEND_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            'End If
            If _filter.STATUS_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            End If
            'If _filter.RECRUIT_SCOPE_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECRUIT_SCOPE_ID = _filter.RECRUIT_SCOPE_ID)
            'End If
            If _filter.IS_IN_PLAN IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_IN_PLAN = _filter.IS_IN_PLAN)
            End If
            If _filter.STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            'If _filter.RECRUIT_REASON_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
            'End If
            If _filter.SUPPORT_NAME <> "" Then
                lst = lst.Where(Function(p) p.SUPPORT_NAME.ToUpper.Contains(_filter.SUPPORT_NAME.ToUpper))
            End If
            'If _filter.CODE <> "" Then
            '    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            'End If
            If _filter.RECRUIT_REASON <> "" Then
                lst = lst.Where(Function(p) p.RECRUIT_REASON.ToUpper.Contains(_filter.RECRUIT_REASON.ToUpper))
            End If
            'If _filter.REQUEST_NUMBER IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.REQUEST_NUMBER = _filter.REQUEST_NUMBER)
            'End If
            If _filter.STAGE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.STAGE_ID = _filter.STAGE_ID)
            End If
            'If _filter.RECRUIT_TYPE_ID <> "" Then
            '    lst = lst.Where(Function(p) p.RECRUIT_TYPE_ID = _filter.RECRUIT_TYPE_ID)
            'End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgram")
            Throw ex
        End Try

    End Function

    Public Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.RC_PROGRAM
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From sta In Context.RC_STAGE.Where(Function(f) f.ID = p.STAGE_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_REASON_ID).DefaultIfEmpty
                        From rectype In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_TYPE_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where (p.IS_PORTAL Is Nothing OrElse p.IS_PORTAL = 0)
                        Select New ProgramDTO With {
                                           .ID = p.ID,
                                           .CODE = p.CODE,
                                           .ORG_ID = p.ORG_ID,
                                           .ORG_NAME = org.NAME_VN,
                                           .ORG_DESC = org.DESCRIPTION_PATH,
                                           .TITLE_NAME = title.NAME_VN,
                                           .IS_IN_PLAN = p.IS_IN_PLAN,
                                           .SEND_DATE = p.SEND_DATE,
                                           .REMARK = p.REMARK,
                                           .RECRUIT_REASON_NAME = reason.NAME_VN,
                                           .RECRUIT_REASON = p.RECRUIT_REASON,
                                           .REQUEST_NUMBER = p.REQUEST_NUMBER,
                                           .RECRUIT_START = p.RECRUIT_START,
                                           .RECEIVE_END = p.RECEIVE_END,
                                           .STATUS_ID = p.STATUS_ID,
                                           .STATUS_NAME = status.NAME_VN,
                                           .CREATED_DATE = p.CREATED_DATE,
                                           .STAGE_ID = p.STAGE_ID,
                                           .RECRUIT_TYPE_ID = rectype.CODE,
                                           .CANDIDATE_COUNT = (From can In Context.RC_CANDIDATE
                                                               Where can.RC_PROGRAM_ID = p.ID).Count,
                                           .CANDIDATE_REQUEST = (From reg In Context.RC_PLAN_REG
                                                                 Where reg.SEND_DATE.Value.Year = p.SEND_DATE.Value.Year And
                                                              reg.STATUS_ID = RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID And
                                                              reg.ORG_ID = p.ORG_ID And reg.TITLE_ID = p.TITLE_ID Select reg.RECRUIT_NUMBER).Sum}

            ' = x.RECRUIT_NUMBER, '
            Dim lst = query
            'If _filter.FROM_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.SEND_DATE >= _filter.FROM_DATE)
            'End If
            'If _filter.TO_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.SEND_DATE <= _filter.TO_DATE)
            'End If
            'If _filter.SEND_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            'End If
            'If _filter.RECRUIT_START IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECRUIT_START = _filter.RECRUIT_START)
            'End If
            'If _filter.RECEIVE_END IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECEIVE_END = _filter.RECEIVE_END)
            'End If
            'If _filter.ORG_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            'End If
            'If _filter.TITLE_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            'End If
            'If _filter.SEND_DATE IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.SEND_DATE = _filter.SEND_DATE)
            'End If
            'If _filter.STATUS_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.STATUS_ID = _filter.STATUS_ID)
            'End If
            'If _filter.RECRUIT_SCOPE_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.RECRUIT_SCOPE_ID = _filter.RECRUIT_SCOPE_ID)
            'End If
            'If _filter.IS_IN_PLAN IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.IS_IN_PLAN.Equals(True))
            'End If
            'If _filter.STATUS_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            'End If
            'If _filter.RECRUIT_REASON_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.RECRUIT_REASON_NAME.ToUpper.Contains(_filter.RECRUIT_REASON_NAME.ToUpper))
            'End If
            'If _filter.CODE <> "" Then
            '    lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            'End If
            'If _filter.RECRUIT_REASON <> "" Then
            '    lst = lst.Where(Function(p) p.RECRUIT_REASON.ToUpper.Contains(_filter.RECRUIT_REASON.ToUpper))
            'End If
            'If _filter.REQUEST_NUMBER IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.REQUEST_NUMBER = _filter.REQUEST_NUMBER)
            'End If
            'If _filter.STAGE_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.STAGE_ID = _filter.STAGE_ID)
            'End If
            'If _filter.RECRUIT_TYPE_ID <> "" Then
            '    lst = lst.Where(Function(p) p.RECRUIT_TYPE_ID = _filter.RECRUIT_TYPE_ID)
            'End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgram")
            Throw ex
        End Try

    End Function

    Public Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO
        Try
            Dim query = From p In Context.RC_PROGRAM
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From pic In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.PIC_ID).DefaultIfEmpty
                        From pic2 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.FOLLOWERS_EMP_ID).DefaultIfEmpty
                        From rec In Context.RC_REQUEST.Where(Function(f) f.ID = p.RC_REQUEST_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = rec.REQUIRER).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID And (p.IS_PORTAL Is Nothing OrElse p.IS_PORTAL = 0)
                        Select New ProgramDTO With {
                                       .ID = p.ID,
                                       .CODE_YCTD = rec.CODE_RC,
                                       .NAME_YCTD = rec.NAME_RC,
                                       .PER_REQUEST = If(e.EMPLOYEE_CODE IsNot Nothing, e.EMPLOYEE_CODE + "-" + e.FULLNAME_VN, e.EMPLOYEE_CODE + "" + e.FULLNAME_VN),
                                       .IS_IN_PLAN = p.IS_IN_PLAN,
                                       .WORK_TIME_ID = p.WORK_TIME_ID,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .ORG_NAME = org.NAME_VN,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .PIC_ID = p.PIC_ID,
                                       .PIC_NAME = pic.FULLNAME_VN,
                                       .SEND_DATE = p.SEND_DATE,
                                       .RECRUIT_TYPE_ID = p.RECRUIT_TYPE_ID,
                                       .RECRUIT_REASON_ID = p.RECRUIT_REASON_ID,
                                       .RECRUIT_REASON = p.RECRUIT_REASON,
                                       .GROUP_WORK_ID = p.GROUP_WORK_ID,
                                       .REQUEST_SALARY_FROM = p.REQUEST_SALARY_FROM,
                                       .PRIORITY_LEVEL_ID = p.PRIORITY_LEVEL_ID,
                                       .IS_PORTAL = p.IS_PORTAL,
                                       .CODE = p.CODE,
                                       .JOB_NAME = p.JOB_NAME,
                                       .DESCRIPTION = p.DESCRIPTION,
                                       .REQUEST_EXPERIENCE = p.REQUEST_EXPERIENCE,
                                       .REQUEST_NUMBER = p.REQUEST_NUMBER,
                                       .REMARK = p.REMARK,
                                       .RECRUIT_START = p.RECRUIT_START,
                                       .RECEIVE_END = p.RECEIVE_END,
                                       .LEARNING_LEVEL_ID = p.LEARNING_LEVEL_ID,
                                       .EXPERIENCE_NUMBER = p.EXPERIENCE_NUMBER,
                                       .TRAINING_LEVEL_MAIN_ID = p.TRAINING_LEVEL_MAIN_ID,
                                       .TRAINING_SCHOOL_MAIN_ID = p.TRAINING_SCHOOL_MAIN_ID,
                                       .MAJOR_MAIN_ID = p.MAJOR_MAIN_ID,
                                       .GRADUATION_TYPE_MAIN_ID = p.GRADUATION_TYPE_MAIN_ID,
                                       .TRAINING_LEVEL_SUB_ID = p.TRAINING_LEVEL_SUB_ID,
                                       .TRAINING_SCHOOL_SUB_ID = p.TRAINING_SCHOOL_SUB_ID,
                                       .MAJOR_SUB_ID = p.MAJOR_SUB_ID,
                                       .GRADUATION_TYPE_SUB_ID = p.GRADUATION_TYPE_SUB_ID,
                                       .LANGUAGE1_ID = p.LANGUAGE1_ID,
                                       .LANGUAGE1_LEVEL_ID = p.LANGUAGE1_LEVEL_ID,
                                       .LANGUAGE1_POINT = p.LANGUAGE1_POINT_ID,
                                       .LANGUAGE2_ID = p.LANGUAGE2_ID,
                                       .LANGUAGE2_LEVEL_ID = p.LANGUAGE2_LEVEL_ID,
                                       .LANGUAGE2_POINT = p.LANGUAGE2_POINT_ID,
                                       .LANGUAGE3_ID = p.LANGUAGE3_ID,
                                       .LANGUAGE3_LEVEL_ID = p.LANGUAGE3_LEVEL_ID,
                                       .LANGUAGE3_POINT = p.LANGUAGE3_POINT_ID,
                                       .COMPUTER_LEVEL_ID = p.COMPUTER_LEVEL_ID,
                                       .AGE_FROM = p.AGE_FROM,
                                       .AGE_TO = p.AGE_TO,
                                       .GENDER_ID = p.GENDER_ID,
                                       .CHIEUCAO = p.CHIEUCAO,
                                       .CANNANG = p.CANNANG,
                                       .APPEARANCE_ID = p.APPEARANCE_ID,
                                       .THILUCTRAI = p.THILUCTRAI,
                                       .THILUCPHAI = p.THILUCPHAI,
                                       .HEALTH_STATUS_ID = p.HEALTH_STATUS_ID,
                                       .STATUS_ID = p.STATUS_ID,
                                       .CANDIDATE_COUNT = (From can In Context.RC_PROGRAM_CANDIDATE
                                                           Where can.RC_PROGRAM_ID = p.ID).Count,
                                       .CANDIDATE_RECEIVED = (From can In Context.RC_PROGRAM_CANDIDATE
                                                              Where can.RC_PROGRAM_ID = p.ID And can.STATUS_ID = "NHANVIEN").Count,
                                       .EXPECTED_JOIN_DATE = rec.EXPECTED_JOIN_DATE,
                                       .NUMBERRECRUITMENT = p.REQUEST_NUMBER,
                                       .NEGOTIABLESALARY = p.NEGOTIABLESALARY,
                                       .REQUEST_SALARY_TO = p.REQUEST_SALARY_TO,
                                       .REQUESTOTHER = p.REQUESTOTHER,
                                       .REQUESTOTHER_DEFAULT = rec.REQUEST_OTHER,
                                       .STAGE_ID = rec.STATUS_ID,
                                       .IS_OVER_LIMIT = rec.IS_OVER_LIMIT,
                                       .CONTRACT_TYPE_ID = rec.CONTRACT_TYPE_ID,
                                       .LOCATION_ID = rec.LOCATION_ID,
                                       .RC_RECRUIT_PROPERTY = rec.RC_RECRUIT_PROPERTY,
                                       .RECRUIT_NUMBER = rec.RECRUIT_NUMBER,
                                       .FOLLOWERS_EMP_ID = p.FOLLOWERS_EMP_ID,
                                       .FOLLOWERS_EMP_NAME = pic2.FULLNAME_VN,
                                       .SPECIALSKILLS = p.SPECIALSKILLS,
                                       .STATUS_NAME = status.NAME_VN,
                                       .RC_REQUEST_NAME = rec.NAME_RC,
                                       .WORKING_PLACE_ID = rec.WORKING_PLACE_ID}
            '.IS_REQUEST_COMPUTER = rec.IS_REQUEST_COMPUTER,
            Dim obj = query.FirstOrDefault
            Dim lstEmp = (From p In Context.RC_PROGRAM_EMP
                          From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                          Where p.RC_PROGRAM_ID = obj.ID
                          Select New ProgramEmpDTO With {
                              .EMPLOYEE_ID = p.EMPLOYEE_ID,
                              .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                              .EMPLOYEE_NAME = emp.FULLNAME_VN}).ToList

            Dim lstScope = (From p In Context.RC_PROGRAM_SCOPE
                            From scope In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RECRUIT_SCOPE_ID)
                            Where p.RC_PROGRAM_ID = obj.ID
                            Select New ProgramScopeDTO With {
                                .RECRUIT_SCOPE_ID = p.RECRUIT_SCOPE_ID,
                                .RECRUIT_SCOPE_NAME = scope.NAME_VN}).ToList

            Dim lstSoft = (From p In Context.RC_PROGRAM_SOFT_SKILL
                           From soft In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOFT_SKILL_ID)
                           Where p.RC_PROGRAM_ID = obj.ID
                           Select New ProgramSoftSkillDTO With {
                               .SOFT_SKILL_ID = p.SOFT_SKILL_ID,
                               .SOFT_SKILL_NAME = soft.NAME_VN}).ToList

            Dim lstCharac = (From p In Context.RC_PROGRAM_CHARACTER
                             From charac In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CHARACTER_ID)
                             Where p.RC_PROGRAM_ID = obj.ID
                             Select New ProgramCharacterDTO With {
                                 .CHARACTER_ID = p.CHARACTER_ID,
                                 .CHARACTER_NAME = charac.NAME_VN}).ToList

            obj.lstEmp = lstEmp
            obj.lstScope = lstScope
            obj.lstSoft = lstSoft
            obj.lstCharac = lstCharac
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramByID")
            Throw ex
        End Try


    End Function

    Public Function InsertProgram(ByVal objProgram As RC_REQUEST,
                                  ByVal lstEmp As List(Of RC_REQUEST_EMP)) As Boolean
        Dim objProgramData As New RC_PROGRAM
        Try
            objProgramData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM.EntitySet.Name)
            objProgramData.IS_IN_PLAN = objProgram.IS_IN_PLAN
            objProgramData.ORG_ID = objProgram.ORG_ID
            objProgramData.TITLE_ID = objProgram.TITLE_ID
            objProgramData.RECRUIT_REASON_ID = objProgram.RECRUIT_REASON_ID
            objProgramData.RECRUIT_REASON = objProgram.RECRUIT_REASON
            If Not objProgram.LEARNING_LEVEL_ID Is Nothing Then
                objProgramData.LEARNING_LEVEL_ID = objProgram.LEARNING_LEVEL_ID
            End If
            objProgramData.AGE_FROM = objProgram.AGE_FROM
            objProgramData.AGE_TO = objProgram.AGE_TO
            objProgramData.SEND_DATE = objProgram.SEND_DATE
            objProgramData.EXPERIENCE_NUMBER = objProgram.EXPERIENCE_NUMBER
            objProgramData.DESCRIPTION = objProgram.DESCRIPTION
            objProgramData.REQUEST_EXPERIENCE = objProgram.REQUEST_EXPERIENCE
            objProgramData.REMARK = objProgram.REMARK
            objProgramData.STATUS_ID = objProgram.STATUS_ID

            'objProgramData.REQUEST_NUMBER = If(objProgram.FEMALE_NUMBER Is Nothing, 0, objProgram.FEMALE_NUMBER) +
            '    If(objProgram.MALE_NUMBER Is Nothing, 0, objProgram.MALE_NUMBER)
            objProgramData.REQUEST_NUMBER = If(objProgram.RECRUIT_NUMBER Is Nothing, 0, objProgram.RECRUIT_NUMBER)
            objProgramData.RC_REQUEST_ID = objProgram.ID
            objProgramData.IS_PORTAL = objProgram.IS_PORTAL
            Context.RC_PROGRAM.AddObject(objProgramData)
            If lstEmp IsNot Nothing Then
                For Each item In lstEmp
                    Dim objProgramEmpData As New RC_PROGRAM_EMP
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_EMP.EntitySet.Name)
                    objProgramEmpData.RC_PROGRAM_ID = objProgramData.ID
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_PROGRAM_EMP.AddObject(objProgramEmpData)
                Next
            End If
            Dim idProgram = objProgramData.ID
            Dim lstProgramExam = (From p In Context.RC_EXAMS
                                  From dtl In Context.RC_EXAMS_DTL.Where(Function(f) f.RC_EXAMS_ID = p.ID)
                                  Where p.ORG_ID = objProgram.ORG_ID And p.TITLE_ID = objProgram.TITLE_ID
                                  Select New ProgramExamsDTO With {
                                      .ORG_ID = p.ORG_ID,
                                      .TITLE_ID = p.TITLE_ID,
                                      .EXAMS_ORDER = dtl.EXAMS_ORDER,
                                      .NAME = dtl.NAME,
                                      .POINT_LADDER = dtl.POINT_LADDER,
                                      .POINT_PASS = dtl.POINT_PASS,
                                      .RC_PROGRAM_ID = idProgram,
                                      .IS_PV = dtl.IS_PV}).ToList


            For Each item In lstProgramExam
                UpdateProgramExams(item)
            Next

            '' TanVN add: Tự động add các môn thi tuyển trong danh mục vào "Thiết lập môn thi" (table RC_PROGRAM_EXAMS)
            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_RECRUITMENT.AUTO_GENERATE_PRO_EXAMS",
            '                     New With {.P_ORG_ID = objProgram.ORG_ID,
            '                               .P_TITLE_ID = objProgram.TITLE_ID,
            '                               .P_PROGRAM_ID = idProgram})
            'End Using

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgram")
            Throw ex
        End Try
    End Function

    Public Function ModifyProgram(ByVal objProgram As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objProgramData As New RC_PROGRAM With {.ID = objProgram.ID}
        Try
            Context.RC_PROGRAM.Attach(objProgramData)
            objProgramData.ID = objProgram.ID
            objProgramData.PIC_ID = objProgram.PIC_ID
            objProgramData.SEND_DATE = objProgram.SEND_DATE
            objProgramData.RECRUIT_TYPE_ID = objProgram.RECRUIT_TYPE_ID
            objProgramData.RECRUIT_REASON_ID = objProgram.RECRUIT_REASON_ID
            objProgramData.RECRUIT_REASON = objProgram.RECRUIT_REASON
            objProgramData.GROUP_WORK_ID = objProgram.GROUP_WORK_ID
            objProgramData.REQUEST_SALARY_FROM = objProgram.REQUEST_SALARY_FROM
            objProgramData.PRIORITY_LEVEL_ID = objProgram.PRIORITY_LEVEL_ID
            objProgramData.IS_PORTAL = objProgram.IS_PORTAL
            objProgramData.WORK_TIME_ID = objProgram.WORK_TIME_ID
            objProgramData.CODE = objProgram.CODE
            objProgramData.JOB_NAME = objProgram.JOB_NAME
            objProgramData.DESCRIPTION = objProgram.DESCRIPTION
            objProgramData.REQUEST_EXPERIENCE = objProgram.REQUEST_EXPERIENCE
            objProgramData.REMARK = objProgram.REMARK
            objProgramData.RECRUIT_START = objProgram.RECRUIT_START
            objProgramData.RECEIVE_END = objProgram.RECEIVE_END
            objProgramData.LEARNING_LEVEL_ID = objProgram.LEARNING_LEVEL_ID
            objProgramData.EXPERIENCE_NUMBER = objProgram.EXPERIENCE_NUMBER
            objProgramData.TRAINING_LEVEL_MAIN_ID = objProgram.TRAINING_LEVEL_MAIN_ID
            objProgramData.TRAINING_SCHOOL_MAIN_ID = objProgram.TRAINING_SCHOOL_MAIN_ID
            objProgramData.MAJOR_MAIN_ID = objProgram.MAJOR_MAIN_ID
            objProgramData.GRADUATION_TYPE_MAIN_ID = objProgram.GRADUATION_TYPE_MAIN_ID
            objProgramData.TRAINING_LEVEL_SUB_ID = objProgram.TRAINING_LEVEL_SUB_ID
            objProgramData.TRAINING_SCHOOL_SUB_ID = objProgram.TRAINING_SCHOOL_SUB_ID
            objProgramData.MAJOR_SUB_ID = objProgram.MAJOR_SUB_ID
            objProgramData.GRADUATION_TYPE_SUB_ID = objProgram.GRADUATION_TYPE_SUB_ID
            objProgramData.LANGUAGE1_ID = objProgram.LANGUAGE1_ID
            objProgramData.LANGUAGE1_LEVEL_ID = objProgram.LANGUAGE1_LEVEL_ID
            objProgramData.LANGUAGE1_POINT_ID = objProgram.LANGUAGE1_POINT
            objProgramData.LANGUAGE2_ID = objProgram.LANGUAGE2_ID
            objProgramData.LANGUAGE2_LEVEL_ID = objProgram.LANGUAGE2_LEVEL_ID
            objProgramData.LANGUAGE2_POINT_ID = objProgram.LANGUAGE2_POINT
            objProgramData.LANGUAGE3_ID = objProgram.LANGUAGE3_ID
            objProgramData.LANGUAGE3_LEVEL_ID = objProgram.LANGUAGE3_LEVEL_ID
            objProgramData.LANGUAGE3_POINT_ID = objProgram.LANGUAGE3_POINT
            objProgramData.COMPUTER_LEVEL_ID = objProgram.COMPUTER_LEVEL_ID
            objProgramData.AGE_FROM = objProgram.AGE_FROM
            objProgramData.AGE_TO = objProgram.AGE_TO
            objProgramData.GENDER_ID = objProgram.GENDER_ID
            objProgramData.CHIEUCAO = objProgram.CHIEUCAO
            objProgramData.CANNANG = objProgram.CANNANG
            objProgramData.APPEARANCE_ID = objProgram.APPEARANCE_ID
            objProgramData.THILUCTRAI = objProgram.THILUCTRAI
            objProgramData.THILUCPHAI = objProgram.THILUCPHAI
            objProgramData.HEALTH_STATUS_ID = objProgram.HEALTH_STATUS_ID
            objProgramData.STATUS_ID = objProgram.STATUS_ID

            objProgramData.FOLLOWERS_EMP_ID = objProgram.FOLLOWERS_EMP_ID
            objProgramData.STAGE_ID = objProgram.STAGE_ID
            objProgramData.NEGOTIABLESALARY = objProgram.NEGOTIABLESALARY
            objProgramData.REQUEST_SALARY_TO = objProgram.REQUEST_SALARY_TO
            objProgramData.REQUESTOTHER = objProgram.REQUESTOTHER
            objProgramData.SPECIALSKILLS = objProgram.SPECIALSKILLS
            objProgramData.IS_PORTAL = objProgram.IS_PORTAL

            Dim lstRegEmp = (From p In Context.RC_PROGRAM_EMP Where p.RC_PROGRAM_ID = objProgramData.ID).ToList
            For Each item In lstRegEmp
                Context.RC_PROGRAM_EMP.DeleteObject(item)
            Next
            Dim lstRegScore = (From p In Context.RC_PROGRAM_SCOPE Where p.RC_PROGRAM_ID = objProgramData.ID).ToList
            For Each item In lstRegScore
                Context.RC_PROGRAM_SCOPE.DeleteObject(item)
            Next
            Dim lstRegSoft = (From p In Context.RC_PROGRAM_SOFT_SKILL Where p.RC_PROGRAM_ID = objProgramData.ID).ToList
            For Each item In lstRegSoft
                Context.RC_PROGRAM_SOFT_SKILL.DeleteObject(item)
            Next
            Dim lstRegCharac = (From p In Context.RC_PROGRAM_CHARACTER Where p.RC_PROGRAM_ID = objProgramData.ID).ToList
            For Each item In lstRegCharac
                Context.RC_PROGRAM_CHARACTER.DeleteObject(item)
            Next
            Context.SaveChanges(log)
            If objProgram.lstEmp IsNot Nothing Then
                For Each item In objProgram.lstEmp
                    Dim objProgramEmpData As New RC_PROGRAM_EMP
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_EMP.EntitySet.Name)
                    objProgramEmpData.RC_PROGRAM_ID = objProgramData.ID
                    objProgramEmpData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_PROGRAM_EMP.AddObject(objProgramEmpData)
                Next
                Context.SaveChanges(log)
            End If
            If objProgram.lstCharac IsNot Nothing Then
                For Each item In objProgram.lstCharac
                    Dim objProgramCharacData As New RC_PROGRAM_CHARACTER
                    objProgramCharacData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_CHARACTER.EntitySet.Name)
                    objProgramCharacData.RC_PROGRAM_ID = objProgramData.ID
                    objProgramCharacData.CHARACTER_ID = item.CHARACTER_ID
                    Context.RC_PROGRAM_CHARACTER.AddObject(objProgramCharacData)
                Next
                Context.SaveChanges(log)
            End If
            If objProgram.lstScope IsNot Nothing Then
                For Each item In objProgram.lstScope
                    Dim objProgramCharacData As New RC_PROGRAM_SCOPE
                    objProgramCharacData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_SCOPE.EntitySet.Name)
                    objProgramCharacData.RC_PROGRAM_ID = objProgramData.ID
                    objProgramCharacData.RECRUIT_SCOPE_ID = item.RECRUIT_SCOPE_ID
                    Context.RC_PROGRAM_SCOPE.AddObject(objProgramCharacData)
                Next
                Context.SaveChanges(log)
            End If
            If objProgram.lstSoft IsNot Nothing Then
                For Each item In objProgram.lstSoft
                    Dim objProgramSoftData As New RC_PROGRAM_SOFT_SKILL
                    objProgramSoftData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_SOFT_SKILL.EntitySet.Name)
                    objProgramSoftData.RC_PROGRAM_ID = objProgramData.ID
                    objProgramSoftData.SOFT_SKILL_ID = item.SOFT_SKILL_ID
                    Context.RC_PROGRAM_SOFT_SKILL.AddObject(objProgramSoftData)
                Next
                Context.SaveChanges(log)
            End If
            gID = objProgramData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyProgram")
            Throw ex
        End Try

    End Function

    Public Function XuatToTrinh(ByVal sID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_STAGE_ID = sID,
                                    .P_CUR = cls.OUT_CURSOR}
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT.XUAT_TO_TRINH", obj)
                Return dtData
            End Using
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".XuatToTrinh")
            Throw ex
        End Try
    End Function

#Region "ProgramExams"

    Public Function GetProgramExams(ByVal _filter As ProgramExamsDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO)

        Try
            Dim query = From p In Context.RC_PROGRAM
                        From dtl In Context.RC_PROGRAM_EXAMS.Where(Function(f) f.RC_PROGRAM_ID = p.ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Where dtl.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID And (p.IS_PORTAL Is Nothing OrElse p.IS_PORTAL = 0)
                        Select New ProgramExamsDTO With {
                                       .ID = dtl.ID,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .NAME = dtl.NAME,
                                       .POINT_LADDER = dtl.POINT_LADDER,
                                       .POINT_PASS = dtl.POINT_PASS,
                                       .EXAMS_ORDER = dtl.EXAMS_ORDER,
                                       .IS_PV = dtl.IS_PV,
                                       .COEFFICIENT = dtl.COEFFICIENT,
                                       .REMARK = dtl.REMARK,
                                       .COUNT_SCAN_RESULT = (From scan In Context.RC_PROGRAM_SCHEDULE_CAN
                                                             Where scan.RC_PROGRAM_EXAMS_ID = dtl.ID And scan.IS_PASS <> -1).Count,
                                       .COUNT_SCAN = (From scan In Context.RC_PROGRAM_SCHEDULE_CAN
                                                      Where scan.RC_PROGRAM_EXAMS_ID = dtl.ID).Count}

            Dim lst = query

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.POINT_LADDER IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_LADDER = _filter.POINT_LADDER)
            End If

            If _filter.POINT_PASS IsNot Nothing Then
                lst = lst.Where(Function(p) p.POINT_PASS = _filter.POINT_PASS)
            End If

            If _filter.EXAMS_ORDER IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXAMS_ORDER = _filter.EXAMS_ORDER)
            End If

            If _filter.IS_PV IsNot Nothing Then
                lst = lst.Where(Function(p) p.IS_PV = _filter.IS_PV)
            End If

            If _filter.COEFFICIENT IsNot Nothing Then
                lst = lst.Where(Function(p) p.COEFFICIENT = _filter.COEFFICIENT)
            End If

            If _filter.REMARK IsNot Nothing Then
                lst = lst.Where(Function(p) p.REMARK = _filter.REMARK)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramExams")
            Throw ex
        End Try

    End Function
    Public Function Get_EXAMS_ORDER(ByVal RC_PROGRAM_ID) As Decimal

        Try
            Dim stt As Decimal = 1
            Using query As New DataAccess.NonQueryData
                Dim temp = query.ExecuteSQLScalar("SELECT E.EXAMS_ORDER FROM RC_PROGRAM_EXAMS E WHERE E.RC_PROGRAM_ID=" + RC_PROGRAM_ID + " ORDER BY E.EXAMS_ORDER DESC", New Object)
                If temp IsNot Nothing Then
                    stt = Decimal.Parse(temp) + 1
                End If
            End Using
            Return stt
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".Get_EXAMS_ORDER")
            Throw ex
        End Try

    End Function
    Public Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO

        Try
            Dim query = From dtl In Context.RC_PROGRAM_EXAMS
                        Where dtl.ID = _filter.ID
                        Select New ProgramExamsDTO With {
                                        .ID = dtl.ID,
                                        .NAME = dtl.NAME,
                                        .POINT_LADDER = dtl.POINT_LADDER,
                                        .POINT_PASS = dtl.POINT_PASS,
                                        .EXAMS_ORDER = dtl.EXAMS_ORDER,
                                        .IS_PV = dtl.IS_PV}

            Return query.FirstOrDefault
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramExamsByID")
            Throw ex
        End Try

    End Function

    Public Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO, Optional ByVal log As UserLog = Nothing) As Boolean
        Dim objProgramExamsData As RC_PROGRAM_EXAMS
        Try
            objProgramExamsData = (From p In Context.RC_PROGRAM_EXAMS Where p.ID = objExams.ID).FirstOrDefault
            If objProgramExamsData IsNot Nothing Then
                objProgramExamsData.NAME = objExams.NAME
                objProgramExamsData.EXAMS_ORDER = objExams.EXAMS_ORDER
                objProgramExamsData.POINT_LADDER = objExams.POINT_LADDER
                objProgramExamsData.POINT_PASS = objExams.POINT_PASS
                objProgramExamsData.RC_PROGRAM_ID = objExams.RC_PROGRAM_ID
                objProgramExamsData.IS_PV = objExams.IS_PV
                objProgramExamsData.COEFFICIENT = objExams.COEFFICIENT
                objProgramExamsData.REMARK = objExams.REMARK
            Else
                objProgramExamsData = New RC_PROGRAM_EXAMS
                objProgramExamsData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_EXAMS.EntitySet.Name)
                objProgramExamsData.NAME = objExams.NAME
                objProgramExamsData.EXAMS_ORDER = objExams.EXAMS_ORDER
                objProgramExamsData.POINT_LADDER = objExams.POINT_LADDER
                objProgramExamsData.POINT_PASS = objExams.POINT_PASS
                objProgramExamsData.RC_PROGRAM_ID = objExams.RC_PROGRAM_ID
                objProgramExamsData.IS_PV = objExams.IS_PV
                objProgramExamsData.COEFFICIENT = objExams.COEFFICIENT
                objProgramExamsData.REMARK = objExams.REMARK
                Context.RC_PROGRAM_EXAMS.AddObject(objProgramExamsData)
            End If
            If log IsNot Nothing Then
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateProgramExams")
            Throw ex
        End Try
    End Function

    Public Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean
        Dim objDel As RC_PROGRAM_EXAMS
        Try
            objDel = (From p In Context.RC_PROGRAM_EXAMS Where obj.ID = p.ID).FirstOrDefault
            Context.RC_PROGRAM_EXAMS.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteProgramExams")
            Throw ex
        End Try

    End Function

#End Region

#End Region
#Region "RC_User_Title_permision"
    Public Function GETUSER_TITLE_PERMISION(ByVal _filter As RC_USER_TITLE_PERMISIONDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of RC_USER_TITLE_PERMISIONDTO)

        Try
            Dim query = From p In Context.RC_USER_TITLE_PERMISION
                        From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                        From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = user.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID)
                        From l In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_TITLE_ID)
                        Select New RC_USER_TITLE_PERMISIONDTO With {
                                       .ID = p.ID,
                                       .USER_ID = p.USER_ID,
                                       .GROUP_TITLE_ID = p.GROUP_TITLE_ID,
                                       .GROUP_TITLE_NAME = l.NAME_VN,
                                       .USER_NAME = user.USERNAME,
                                       .CREATED_DATE = p.CREATED_DATE,
                            .EMP_NAME = emp.FULLNAME_VN,
                            .EMP_CODE = emp.EMPLOYEE_CODE,
                            .TITTLE_ID = t.ID,
                            .TITTLE_NAME = t.NAME_VN,
                            .ORG_ID = o.ID,
                            .ORG_NAME = o.NAME_VN}

            Dim lst = query

            If _filter.USER_NAME <> "" Then
                lst = lst.Where(Function(p) p.USER_NAME.ToUpper.Contains(_filter.USER_NAME.ToUpper))
            End If

            If _filter.GROUP_TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_TITLE_NAME.ToUpper.Contains(_filter.GROUP_TITLE_NAME.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GETUSER_TITLE_PERMISION")
            Throw ex
        End Try

    End Function
    Public Function GETUSER_TITLE_PERMISION_BY_USER(Optional ByVal log As UserLog = Nothing) As List(Of RC_USER_TITLE_PERMISIONDTO)

        Try
            Dim query = From p In Context.RC_USER_TITLE_PERMISION
                        From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                        From l In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_TITLE_ID)
                        Select New RC_USER_TITLE_PERMISIONDTO With {
                                       .ID = p.ID,
                                       .USER_ID = p.USER_ID,
                                       .GROUP_TITLE_ID = p.GROUP_TITLE_ID,
                                       .GROUP_TITLE_NAME = l.NAME_VN,
                                       .USER_NAME = user.USERNAME,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            lst = lst.Where(Function(p) p.USER_NAME.ToUpper.Contains(log.Username.ToUpper))


            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GETUSER_TITLE_PERMISION")
            Throw ex
        End Try

    End Function
    Public Function UpdateUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO, Optional ByVal log As UserLog = Nothing) As Boolean
        Dim objData As RC_USER_TITLE_PERMISION
        Try
            objData = (From p In Context.RC_USER_TITLE_PERMISION Where p.USER_ID = obj.USER_ID And p.GROUP_TITLE_ID = obj.GROUP_TITLE_ID).FirstOrDefault
            If objData Is Nothing Then
                objData = New RC_USER_TITLE_PERMISION
                objData.ID = Utilities.GetNextSequence(Context, Context.RC_USER_TITLE_PERMISION.EntitySet.Name)
                objData.GROUP_TITLE_ID = obj.GROUP_TITLE_ID
                objData.USER_ID = obj.USER_ID
                Context.RC_USER_TITLE_PERMISION.AddObject(objData)
            End If
            If log IsNot Nothing Then
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateUSER_TITLE_PERMISION")
            Throw ex
        End Try
    End Function

    Public Function DeleteUSER_TITLE_PERMISION(ByVal obj As RC_USER_TITLE_PERMISIONDTO) As Boolean
        Dim objDel As RC_USER_TITLE_PERMISION
        Try
            objDel = (From p In Context.RC_USER_TITLE_PERMISION Where obj.ID = p.ID).FirstOrDefault
            Context.RC_USER_TITLE_PERMISION.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteProgramExams")
            Throw ex
        End Try

    End Function
#End Region

#Region "Candidate"

    Public ReadOnly pathCandidate As String = "CANDIDATE"

    ''' <summary>
    ''' Kiểm tra ứng viên đó có trong hệ thống ko (trừ ứng viên nghỉ việc)
    ''' </summary>
    ''' <param name="strEmpCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckExistCandidate(ByVal strEmpCode As String) As Boolean
        Try
            Dim query = (From p In Context.RC_CANDIDATE
                         Where p.CANDIDATE_CODE = strEmpCode).Count
            If query > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistCandidate")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_CANDIDATE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT_EXPORT.IMPORT_CANDIDATE", New With {.P_DOCXML = P_DOCXML,
                                                                                                                .P_USERNAME = log.Username,
                                                                                                                .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
            Return False
        End Try
    End Function

    Public Function CheckExist_Program_Candidate(ByVal lstCandidateID As List(Of Decimal), ByVal ProgramID As Decimal) As Boolean
        Dim lstPCan As List(Of RC_PROGRAM_CANDIDATE)
        Try
            'Dim query = (From p In Context.RC_PROGRAM_CANDIDATE
            '             Where p.CANDIDATE_ID = strEmpCode).Count
            lstPCan = (From p In Context.RC_PROGRAM_CANDIDATE Where p.RC_PROGRAM_ID = ProgramID And lstCandidateID.Contains(p.CANDIDATE_ID)).ToList
            If lstPCan.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExist_Program_Candidate")
            Throw ex
        End Try
    End Function
    Public Function ValidateInsertCandidate(ByVal sEmpCode As String, ByVal sID_No As String, ByVal sFullName As String, ByVal dBirthDate As Date, ByVal sType As String) As Boolean
        Try
            Select Case sType
                Case "NO_ID"
                    If Not sID_No Is Nothing And sID_No <> "" Then
                        Return ((From e In Context.RC_CANDIDATE
                                 From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = e.ID)
                                 Where cv.ID_NO = sID_No).Count = 0)
                    End If
                Case "BLACK_LIST"
                    If Not sID_No Is Nothing And sID_No <> "" Then
                        Return ((From e In Context.RC_CANDIDATE
                                 From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = e.ID)
                                 Where cv.ID_NO = sID_No And e.IS_BLACKLIST = -1).Count = 0)
                    End If
                Case "DATE_FULLNAME"
                    If (sID_No = "" Or sID_No Is Nothing) And dBirthDate.ToString <> "" Then
                        Return ((From e In Context.RC_CANDIDATE
                                 From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = e.ID)
                                 Where cv.BIRTH_DATE = dBirthDate And e.FULLNAME_VN.ToUpper = sFullName.ToUpper).Count = 0)
                    End If
            End Select
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateInsertCandidate")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy sanh sách ứng viên có phân trang
    ''' </summary>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="_filter"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Try
            Dim query = From p In Context.RC_CANDIDATE
                        From rc In Context.RC_PROGRAM_CANDIDATE.Where(Function(f) p.ID = f.CANDIDATE_ID)
                        From ot In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From cv In Context.RC_CANDIDATE_CV.Where(Function(f) p.ID = f.CANDIDATE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = rc.STATUS_ID).DefaultIfEmpty
                        Order By p.CREATED_DATE Descending

            'If _filter.RC_PROGRAM_ID IsNot Nothing Then
            '    query = query.Where(Function(f) f.p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID)
            'End If
            'anhvn
            If _filter.RC_PROGRAM_ID IsNot Nothing Then
                query = query.Where(Function(f) f.rc.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID)
            End If
            '---------------

            Dim lst = query.Select(Function(p) New CandidateDTO With {
                         .CANDIDATE_CODE = p.p.CANDIDATE_CODE,
                         .ID = p.p.ID,
                         .FULLNAME_VN = p.p.FULLNAME_VN,
                         .ORG_ID = p.p.ORG_ID,
                         .ORG_NAME = p.org.NAME_VN,
                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                         .TITLE_ID = p.ot.ID,
                         .TITLE_NAME_VN = p.ot.NAME_VN,
                         .BIRTH_DATE = p.cv.BIRTH_DATE,
                         .ID_NO = p.cv.ID_NO,
                         .ID_DATE = p.cv.ID_DATE,
                         .IS_BLACKLIST = p.p.IS_BLACKLIST,
                         .IS_PONTENTIAL = p.p.IS_PONTENTIAL,
                         .IS_REHIRE = p.p.IS_REHIRE,
                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                         .STATUS_ID = p.rc.STATUS_ID,
                         .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                         .STATUS_NAME = p.status.NAME_VN,
                         .CONTRACT_MOBILE = p.cv.CONTACT_MOBILE,
                         .PER_EMAIL = p.cv.PER_EMAIL,
                         .RC_PROGRAM_CANDIDATE_ID = p.rc.ID})

            If _filter.CANDIDATE_CODE <> "" Then
                lst = lst.Where(Function(p) p.CANDIDATE_CODE.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                p.FULLNAME_VN.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                   p.ID_NO.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0)
            End If

            Dim lstStatus = New List(Of String)
            If _filter.KHONGDUDIEUKIEN_ID <> "" Then
                lstStatus.Add(_filter.KHONGDUDIEUKIEN_ID)
            End If
            If _filter.DUDIEUKIEN_ID <> "" Then
                lstStatus.Add(_filter.DUDIEUKIEN_ID)
            End If
            If _filter.TRUNGTUYEN_ID <> "" Then
                lstStatus.Add(_filter.TRUNGTUYEN_ID)
            End If
            If _filter.PONTENTIAL <> "" Then
                lstStatus.Add(_filter.PONTENTIAL)
            End If
            If _filter.TUCHOI_ID <> "" Then
                lstStatus.Add(_filter.TUCHOI_ID)
            End If
            If _filter.GUITHU_ID <> "" Then
                lstStatus.Add(_filter.GUITHU_ID)
            End If
            If _filter.LANHANVIEN_ID <> "" Then
                lstStatus.Add(_filter.LANHANVIEN_ID)
            End If
            If _filter.NOIBO_ID <> "" Then
                lstStatus.Add(_filter.NOIBO_ID)
            End If

            If lstStatus.Count > 0 Then
                lst = lst.Where(Function(p) lstStatus.Contains(p.STATUS_ID))
            End If

            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If

            If _filter.ID_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID_DATE = _filter.ID_DATE)
            End If

            If _filter.ID_NO IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            'If _filter.TITLE_ID IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            'End If

            If _filter.BIRTH_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            'If _filter.IS_PONTENTIAL IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.IS_PONTENTIAL = _filter.IS_PONTENTIAL)
            'End If

            'If _filter.STATUS_NAME <> "" Then
            '    lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper().IndexOf(_filter.STATUS_NAME.ToUpper) >= 0)
            'End If

            lst = lst.OrderBy(Sorts)

            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetListCandidatePaging")
            Throw ex
        End Try
    End Function

    Public Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Try
            Dim query = From p In Context.RC_CANDIDATE
                        From ot In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From cv In Context.RC_CANDIDATE_CV.Where(Function(f) p.ID = f.CANDIDATE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = p.STATUS_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_CODE Is Nothing And p.STATUS_ID = "PONTENTIAL"

            If _filter.RC_PROGRAM_ID IsNot Nothing Then
                query = query.Where(Function(f) f.p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID)
            End If

            Dim lst = query.Select(Function(p) New CandidateDTO With {
                         .CANDIDATE_CODE = p.p.CANDIDATE_CODE,
                         .ID = p.p.ID,
                         .FULLNAME_VN = p.p.FULLNAME_VN,
                         .ORG_ID = p.p.ORG_ID,
                         .ORG_NAME = p.org.NAME_VN,
                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                         .TITLE_ID = p.ot.ID,
                         .TITLE_NAME_VN = p.ot.NAME_VN,
                         .BIRTH_DATE = p.cv.BIRTH_DATE,
                         .ID_NO = p.cv.ID_NO,
                         .ID_DATE = p.cv.ID_DATE,
                         .IS_BLACKLIST = p.p.IS_BLACKLIST,
                         .IS_PONTENTIAL = p.p.IS_PONTENTIAL,
                         .IS_REHIRE = p.p.IS_REHIRE,
                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                         .STATUS_ID = p.p.STATUS_ID,
                         .STATUS_NAME = p.status.NAME_VN})

            If _filter.CANDIDATE_CODE <> "" Then
                lst = lst.Where(Function(p) p.CANDIDATE_CODE.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                p.FULLNAME_VN.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                   p.ID_NO.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If
            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ID_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID_DATE = _filter.ID_DATE)
            End If

            If _filter.ID_NO IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            'If _filter.IS_PONTENTIAL IsNot Nothing Then
            '    lst = lst.Where(Function(p) p.IS_PONTENTIAL = _filter.IS_PONTENTIAL)
            'End If

            If _filter.BIRTH_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            If _filter.STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper().IndexOf(_filter.STATUS_NAME.ToUpper) >= 0)
            End If
            lst = lst.OrderBy(Sorts)

            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetListCandidatePaging")
            Throw ex
        End Try
    End Function

    Public Function GetListCandidate(ByVal _filter As CandidateDTO,
                                   Optional ByVal Sorts As String = "Candidate_CODE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of CandidateDTO)
        Try
            Dim lst As IQueryable(Of CandidateDTO)
            Dim query = From p In Context.RC_CANDIDATE
                        From ot In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From cv In Context.RC_CANDIDATE_CV.Where(Function(f) p.ID = f.CANDIDATE_ID).DefaultIfEmpty
                        Order By p.CREATED_DATE Descending


            lst = query.Select(Function(p) New CandidateDTO With {
                         .CANDIDATE_CODE = p.p.CANDIDATE_CODE,
                         .ID = p.p.ID,
                         .FULLNAME_VN = p.p.FULLNAME_VN,
                         .ORG_ID = p.p.ORG_ID,
                         .ORG_NAME = p.org.NAME_VN,
                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                         .TITLE_ID = p.ot.ID,
                         .TITLE_NAME_VN = p.ot.NAME_VN,
                         .JOIN_DATE = p.p.JOIN_DATE,
                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                         .IMAGE = p.cv.IMAGE,
                         .ID_NO = p.cv.ID_NO})

            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If
            If _filter.TITLE_ID <> 0 Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If

            If _filter.CANDIDATE_CODE <> "" Then
                lst = lst.Where(Function(p) p.CANDIDATE_CODE.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                p.FULLNAME_VN.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
                                                 p.ID_NO.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0)

            End If
            lst = lst.OrderBy(Sorts)
            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetListCandidate")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Hàm đọc ảnh hồ sơ của ứng viên thành binary
    ''' </summary>
    ''' <param name="gEmpID"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String,
                                     Optional ByVal isOneCandidate As Boolean = True,
                                     Optional ByVal img_link As String = "") As Byte()
        Try


            Dim sCandidateImage As String = ""
            If Not isOneCandidate Then
                sCandidateImage = img_link
            Else
                sCandidateImage = (From p In Context.RC_CANDIDATE_CV Where p.CANDIDATE_ID = gEmpID _
                                   Select p.IMAGE).FirstOrDefault
            End If

            Dim _imageBinary() As Byte = Nothing
            Dim fileDirectory = ""
            Dim filepath = ""
            Dim filepathDefault = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\CandidateImage"
            Dim _fileInfo As IO.FileInfo
            If sCandidateImage IsNot Nothing AndAlso sCandidateImage.Trim().Length > 0 Then
                filepath = fileDirectory & "\" & sCandidateImage
            Else
                filepath = fileDirectory & "\NoImage.jpg"
            End If
            filepathDefault = fileDirectory & "\NoImage.jpg"
            'Kiểm tra file có tồn tại ko
            If File.Exists(filepath) Then
                _fileInfo = New FileInfo(filepath)
            Else
                _fileInfo = New FileInfo(filepathDefault) 'Nếu ko có thì lấy ảnh mặc định
            End If

            Dim _FStream As New IO.FileStream(_fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _imageBinary = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _imageBinary
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateImage")
            Throw ex
        End Try
    End Function

    Public Function CreateNewCandidateCode() As CandidateDTO
        Dim objEmpData As New RC_CANDIDATE
        Dim empData As New CandidateDTO
        ' thêm kỷ luật
        Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE.EntitySet.Name)
        'SaveCandidate(fileID, 222)

        'Sinh mã ứng viên động
        Dim checkEMP As Integer = 0
        Dim empCodeDB As Decimal = 0
        Dim EMPCODE As String

        Using query As New DataAccess.NonQueryData
            Dim temp = query.ExecuteSQLScalar("select Candidate_CODE from RC_Candidate " & _
                                   "order by Candidate_CODE DESC",
                                   New Object)
            If temp IsNot Nothing Then
                empCodeDB = Decimal.Parse(temp)
            End If
        End Using
        Do
            empCodeDB += 1
            EMPCODE = String.Format("{0}", Format(empCodeDB, "000000"))
            checkEMP = (From p In Context.RC_CANDIDATE Where p.CANDIDATE_CODE = EMPCODE Select p.ID).Count
        Loop Until checkEMP = 0

        Return (New CandidateDTO With {.ID = fileID, .CANDIDATE_CODE = EMPCODE})

    End Function
    Public Function InsertProgramCandidate(ByVal lstID As List(Of Decimal), ByVal RC_PROGRAM_ID As Decimal, ByVal log As UserLog) As Boolean
        Try
            For i As Int16 = 0 To lstID.Count - 1
                Dim objData As New RC_PROGRAM_CANDIDATE
                objData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_CANDIDATE.EntitySet.Name)
                objData.CANDIDATE_ID = lstID(i)
                objData.RC_PROGRAM_ID = RC_PROGRAM_ID
                objData.STATUS_ID = "DUDK"
                Context.RC_PROGRAM_CANDIDATE.AddObject(objData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertProgramCandidate")
            Throw ex
        End Try

    End Function
    Public Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                   ByRef _strEmpCode As String,
                                   ByVal _imageBinary As Byte(),
                                   ByVal objEmpCV As CandidateCVDTO,
                                         ByVal objEmpEdu As CandidateEduDTO,
                                         ByVal objEmpOther As CandidateOtherInfoDTO,
                                         ByVal objEmpHealth As CandidateHealthDTO,
                                         ByVal objEmpExpect As CandidateExpectDTO,
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean

        Try
            '---------- 1.0 Thêm vào bảng RC_Candidate ----------
            Dim objEmpData As New RC_CANDIDATE
            Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE.EntitySet.Name)
            'SaveCandidate(fileID, 222)
            objEmpData.ID = fileID

            'Sinh mã ứng viên động
            Dim checkEMP As Integer = 0
            Dim empCodeDB As Decimal = 0
            Dim EMPCODE As String

            Using query As New DataAccess.NonQueryData
                Dim temp = query.ExecuteSQLScalar("select Candidate_CODE from RC_Candidate order by Candidate_CODE DESC", New Object)
                If temp IsNot Nothing Then
                    empCodeDB = Decimal.Parse(temp)
                End If
            End Using
            Do
                empCodeDB += 1
                EMPCODE = String.Format("{0}", Format(empCodeDB, "000000"))
                checkEMP = (From p In Context.RC_CANDIDATE Where p.CANDIDATE_CODE = EMPCODE Select p.ID).Count
            Loop Until checkEMP = 0
            _strEmpCode = EMPCODE
            objEmpData.ID = fileID
            objEmpData.CANDIDATE_CODE = _strEmpCode
            objEmpData.RC_PROGRAM_ID = objEmp.RC_PROGRAM_ID
            objEmpData.FIRST_NAME_VN = objEmp.FIRST_NAME_VN
            objEmpData.LAST_NAME_VN = objEmp.LAST_NAME_VN
            objEmpData.FULLNAME_VN = objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.JOIN_DATE = objEmp.JOIN_DATE
            objEmpData.EFFECT_DATE = objEmp.EFFECT_DATE
            objEmpData.TITLE_NAME = objEmp.TITLE_NAME
            objEmpData.WORK = objEmp.WORK
            objEmpData.FILE_NAME = objEmp.FILE_NAME
            objEmpData.CARE_TITLE_NAME = objEmp.CARE_TITLE_NAME
            objEmpData.RECRUIMENT_WEBSITE = objEmp.RECRUIMENT_WEBSITE
            objEmpData.RC_SOURCE_REC_ID = objEmp.RC_SOURCE_REC_ID
            'objEmpData.STATUS_ID = "DUDK"
            If objEmp.FILE_SIZE IsNot Nothing Then
                FileInsert(objEmp.ID, pathCandidate, objEmp.FILE_SIZE)
            End If

            If objEmp.WORK_STATUS IsNot Nothing Then
                '10,11,12,13,14 - nghỉ việc
                If ",10,11,12,13,14,".Contains("," & objEmp.WORK_STATUS.Value.ToString & ",") Then
                    objEmpData.IS_REHIRE = True
                End If
            End If

            Context.RC_CANDIDATE.AddObject(objEmpData)

            '---------- 2.0 Thêm vào bảng RC_Candidate_CV ----------
            Dim objEmpCVData As New RC_CANDIDATE_CV
            objEmpCVData.IS_WORK_PERMIT = objEmpCV.IS_WORK_PERMIT
            objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
            objEmpCVData.WORK_PERMIT_END = objEmpCV.WORK_PERMIT_END
            objEmpCVData.WORK_PERMIT_START = objEmpCV.WORK_PERMIT_START
            objEmpCVData.CANDIDATE_ID = objEmpData.ID
            objEmpCVData.CON_WARD = objEmpCV.CON_WARD
            objEmpCVData.PER_WARD = objEmpCV.PER_WARD
            objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
            objEmpCVData.GENDER = objEmpCV.GENDER
            objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
            objEmpCVData.ID_NO = objEmpCV.ID_NO
            objEmpCVData.ID_DATE = objEmpCV.ID_DATE
            objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
            objEmpCVData.NATIVE = objEmpCV.NATIVE
            objEmpCVData.RELIGION = objEmpCV.RELIGION
            objEmpCVData.PASSPORT_ID = objEmpCV.PASSPORT_ID
            objEmpCVData.PASSPORT_DATE = objEmpCV.PASSPORT_DATE
            objEmpCVData.PASSPORT_PLACE = objEmpCV.PASSPORT_PLACE_NAME
            objEmpCVData.BIRTH_NATION = objEmpCV.BIRTH_NATION_ID
            objEmpCVData.BIRTH_PROVINCE = objEmpCV.BIRTH_PROVINCE
            objEmpCVData.NATIONALITY_ID = objEmpCV.NATIONALITY_ID
            objEmpCVData.NAV_NATION = objEmpCV.NAV_NATION_ID
            objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
            objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
            objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
            objEmpCVData.PER_NATION = objEmpCV.PER_NATION_ID
            objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT_ID
            objEmpCVData.CONTACT_ADDRESS = objEmpCV.CONTACT_ADDRESS
            objEmpCVData.CONTACT_PROVINCE = objEmpCV.CONTACT_PROVINCE
            objEmpCVData.CONTACT_NATION = objEmpCV.CONTACT_NATION_ID
            objEmpCVData.CONTACT_DISTRICT = objEmpCV.CONTACT_DISTRICT_ID
            objEmpCVData.PERTAXCODE = objEmpCV.PERTAXCODE
            objEmpCVData.EDUCATION_MAJORS = objEmpCV.EDUCATION_MAJORS
            objEmpCVData.EDUCATION_ID = objEmpCV.EDUCATION_ID
            objEmpCVData.ID_DATE_EXPIRATION = objEmpCV.ID_DATE_EXPIRATION
            objEmpCVData.IS_RESIDENT = objEmpCV.IS_RESIDENT
            objEmpCVData.CONTACT_ADDRESS_TEMP = objEmpCV.CONTACT_ADDRESS_TEMP
            objEmpCVData.CONTACT_NATION_TEMP = objEmpCV.CONTACT_NATION_TEMP
            objEmpCVData.CONTACT_PROVINCE_TEMP = objEmpCV.CONTACT_PROVINCE_TEMP
            objEmpCVData.CONTACT_DISTRICT_TEMP = objEmpCV.CONTACT_DISTRICT_TEMP
            objEmpCVData.CONTACT_MOBILE = objEmpCV.CONTACT_MOBILE
            objEmpCVData.CONTACT_PHONE = objEmpCV.CONTACT_PHONE
            objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
            objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIl
            objEmpCVData.PER_TAX_DATE = objEmpCV.PER_TAX_DATE
            objEmpCVData.PER_TAX_PLACE = objEmpCV.PER_TAX_PLACE
            objEmpCVData.PASSPORT_DATE_EXPIRATION = objEmpCV.PASSPORT_DATE_EXPIRATION
            objEmpCVData.VISA_NUMBER = objEmpCV.VISA_NUMBER
            objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
            objEmpCVData.VISA_DATE_EXPIRATION = objEmpCV.VISA_DATE_EXPIRATION
            objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
            objEmpCVData.VNAIRLINES_NUMBER = objEmpCV.VNAIRLINES_NUMBER
            objEmpCVData.VNAIRLINES_DATE = objEmpCV.VNAIRLINES_DATE
            objEmpCVData.VNAIRLINES_DATE_EXPIRATION = objEmpCV.VNAIRLINES_DATE_EXPIRATION
            objEmpCVData.VNAIRLINES_PLACE = objEmpCV.VNAIRLINES_PLACE
            objEmpCVData.LABOUR_NUMBER = objEmpCV.LABOUR_NUMBER
            objEmpCVData.LABOUR_DATE = objEmpCV.LABOUR_DATE
            objEmpCVData.LABOUR_DATE_EXPIRATION = objEmpCV.LABOUR_DATE_EXPIRATION
            objEmpCVData.LABOUR_PLACE = objEmpCV.LABOUR_PLACE
            objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
            objEmpCVData.WORK_PERMIT_START = objEmpCV.WORK_PERMIT_START
            objEmpCVData.WORK_PERMIT_END = objEmpCV.WORK_PERMIT_END
            objEmpCVData.TEMP_RESIDENCE_CARD = objEmpCV.TEMP_RESIDENCE_CARD
            objEmpCVData.TEMP_RESIDENCE_CARD_START = objEmpCV.TEMP_RESIDENCE_CARD_START
            objEmpCVData.TEMP_RESIDENCE_CARD_END = objEmpCV.TEMP_RESIDENCE_CARD_END

            objEmpCVData.FINDER_NAME = objEmpCV.FINDER_NAME
            objEmpCVData.FINDER_SDT = objEmpCV.FINDER_SDT
            objEmpCVData.FINDER_ADDRESS = objEmpCV.FINDER_ADDRESS

            objEmpCVData.URGENT_PER_NAME = objEmpCV.URGENT_PER_NAME
            If objEmpCV.URGENT_PER_RELATION = 0 Then
                objEmpCVData.URGENT_PER_RELATION = Nothing
            Else
                objEmpCVData.URGENT_PER_RELATION = objEmpCV.URGENT_PER_RELATION
            End If
            objEmpCVData.URGENT_PER_SDT = objEmpCV.URGENT_PER_SDT
            objEmpCVData.URGENT_ADDRESS = objEmpCV.URGENT_ADDRESS

            objEmpCVData.STRANGER = objEmpCV.STRANGER
            objEmpCVData.WEAKNESS = objEmpCV.WEAKNESS

            If objEmpCV.IMAGE <> "" Then
                objEmpCVData.IMAGE = objEmpData.CANDIDATE_CODE & objEmpCV.IMAGE
            End If
            Context.RC_CANDIDATE_CV.AddObject(objEmpCVData)

            '---------- 3.0 Thêm vào bảng RC_EDUCATION ----------
            Dim objEmpEduData As New RC_CANDIDATE_EDUCATION
            objEmpEduData.YEAR_GRADUATE = objEmpEdu.YEAR_GRADUATE
            objEmpEduData.LANGUAGE_ID = objEmpEdu.LANGUAGE_ID
            objEmpEduData.CERTIFICATE_ID = objEmpEdu.CERTIFICATE_ID
            objEmpEduData.CANDIDATE_ID = objEmpData.ID
            objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
            objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
            objEmpEduData.FIELD = objEmpEdu.FIELD
            objEmpEduData.SCHOOL = objEmpEdu.SCHOOL
            objEmpEduData.MAJOR = objEmpEdu.MAJOR
            objEmpEduData.DEGREE = objEmpEdu.DEGREE
            objEmpEduData.GPA = objEmpEdu.GPA
            objEmpEduData.IT_CERTIFICATE = objEmpEdu.IT_CERTIFICATE
            objEmpEduData.IT_LEVEL = objEmpEdu.IT_LEVEL
            objEmpEduData.IT_MARK = objEmpEdu.IT_MARK
            objEmpEduData.IT_CERTIFICATE1 = objEmpEdu.IT_CERTIFICATE1
            objEmpEduData.IT_LEVEL1 = objEmpEdu.IT_LEVEL1
            objEmpEduData.IT_MARK1 = objEmpEdu.IT_MARK1
            objEmpEduData.IT_CERTIFICATE2 = objEmpEdu.IT_CERTIFICATE2
            objEmpEduData.IT_LEVEL2 = objEmpEdu.IT_LEVEL2
            objEmpEduData.IT_MARK2 = objEmpEdu.IT_MARK2
            objEmpEduData.ENGLISH = objEmpEdu.ENGLISH
            objEmpEduData.ENGLISH_LEVEL = objEmpEdu.ENGLISH_LEVEL
            objEmpEduData.ENGLISH_MARK = objEmpEdu.ENGLISH_MARK
            objEmpEduData.ENGLISH1 = objEmpEdu.ENGLISH1
            objEmpEduData.ENGLISH_LEVEL1 = objEmpEdu.ENGLISH_LEVEL1
            objEmpEduData.ENGLISH_MARK1 = objEmpEdu.ENGLISH_MARK1
            objEmpEduData.ENGLISH2 = objEmpEdu.ENGLISH2
            objEmpEduData.ENGLISH_LEVEL2 = objEmpEdu.ENGLISH_LEVEL2
            objEmpEduData.ENGLISH_MARK2 = objEmpEdu.ENGLISH_MARK2
            objEmpEduData.RATE = objEmpEdu.RATE
            Context.RC_CANDIDATE_EDUCATION.AddObject(objEmpEduData)

            '---------- 4.0 Thêm vào bảng RC_Candidate_OTHER_INFO ----------
            Dim objEmpOtherData As New RC_CANDIDATE_OTHER_INFO
            objEmpOtherData.CANDIDATE_ID = objEmpData.ID
            objEmpOtherData.NGAYVAOCONGDOAN = objEmpOther.NGAY_VAO_DOAN
            objEmpOtherData.NOIVAOCONGDOAN = objEmpOther.NOI_VAO_DOAN
            objEmpOtherData.CONGDOANPHI = objEmpOther.DOAN_PHI
            objEmpOtherData.ACCOUNT_NAME = objEmpOther.ACCOUNT_NAME
            objEmpOtherData.ACCOUNT_NUMBER = objEmpOther.ACCOUNT_NUMBER
            objEmpOtherData.BANK = objEmpOther.BANK
            objEmpOtherData.BANK_BRANCH = objEmpOther.BANK_BRANCH
            objEmpOtherData.IS_PAYMENT_VIA_BANK = objEmpOther.IS_PAYMENT_VIA_BANK
            objEmpOtherData.ACCOUNT_EFFECT_DATE = objEmpOther.ACCOUNT_EFFECT_DATE
            Context.RC_CANDIDATE_OTHER_INFO.AddObject(objEmpOtherData)

            ' Thêm vào bảng CandidateExpect
            Dim objEmpExpectData As New RC_CANDIDATE_EXPECT
            objEmpExpectData.CANDIDATE_ID = objEmpData.ID
            If objEmpExpect IsNot Nothing Then
                objEmpExpectData.TIME_START = objEmpExpect.TIME_START
                objEmpExpectData.PROBATIONARY_SALARY = objEmpExpect.PROBATIONARY_SALARY
                objEmpExpectData.OFFICIAL_SALARY = objEmpExpect.OFFICIAL_SALARY
                objEmpExpectData.DATE_START = objEmpExpect.DATE_START
                objEmpExpectData.OTHER_REQUEST = objEmpExpect.OTHER_REQUEST
                objEmpExpectData.WORK_LOCATION = objEmpExpect.WORK_LOCATION

                objEmpExpectData.DESIRE_POSITION_1 = objEmpExpect.DESIRE_POSITION_1
                objEmpExpectData.DESIRE_POSITION_2 = objEmpExpect.DESIRE_POSITION_2
                objEmpExpectData.DESIRE_LEVEL = objEmpExpect.DESIRE_LEVEL
                objEmpExpectData.SENIORITY_EXPERIENCE = objEmpExpect.SENIORITY_EXPERIENCE
                objEmpExpectData.WORKED_HSV_HV = objEmpExpect.WORKED_HSV_HV
            End If
            Context.RC_CANDIDATE_EXPECT.AddObject(objEmpExpectData)

            ' Thêm vào bảng CandidateHealth
            Dim objEmpHealthData As New RC_CANDIDATE_HEALTH
            objEmpHealthData.CANDIDATE_ID = objEmpData.ID
            If objEmpHealth IsNot Nothing Then
                objEmpHealthData.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                objEmpHealthData.CAN_NANG = objEmpHealth.CAN_NANG
                objEmpHealthData.NHOM_MAU = objEmpHealth.NHOM_MAU
                objEmpHealthData.HUYET_AP = objEmpHealth.HUYET_AP
                objEmpHealthData.MAT_TRAI = objEmpHealth.MAT_TRAI
                objEmpHealthData.MAT_PHAI = objEmpHealth.MAT_PHAI
                objEmpHealthData.LOAI_SUC_KHOE = objEmpHealth.LOAI_SUC_KHOE
                objEmpHealthData.TAI_MUI_HONG = objEmpHealth.TAI_MUI_HONG
                objEmpHealthData.RANG_HAM_MAT = objEmpHealth.RANG_HAM_MAT
                objEmpHealthData.TIM = objEmpHealth.TIM
                objEmpHealthData.PHOI_NGUC = objEmpHealth.PHOI_NGUC
                objEmpHealthData.VIEM_GAN_B = objEmpHealth.VIEM_GAN_B
                objEmpHealthData.DA_HOA_LIEU = objEmpHealth.DA_HOA_LIEU
                objEmpHealthData.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE
                Context.RC_CANDIDATE_HEALTH.AddObject(objEmpHealthData)
            End If

            Context.SaveChanges(log)
            gID = objEmpData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidate")
            Throw ex
        End Try

    End Function

    Public Function INSERT_RC_PROGRAM_CANDIDATE(ByVal objDTO As RC_PROGRAM_CANDIDATEDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean
        Try

            Dim objData As New RC_PROGRAM_CANDIDATE
            objData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_CANDIDATE.EntitySet.Name)
            objData.RC_PROGRAM_ID = objDTO.RC_PROGRAM_ID
            objData.CANDIDATE_ID = objDTO.CANDIDATE_ID
            objData.STATUS_ID = objDTO.STATUS_ID
            Context.RC_PROGRAM_CANDIDATE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".INSERT_RC_PROGRAM_CANDIDATE")
            Throw ex
        End Try
    End Function

    Public Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                        ByVal _imageBinary As Byte(), _
                                         ByVal objEmpCV As CandidateCVDTO, _
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                         ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean

        Try
            Dim objEmpData As New RC_CANDIDATE With {.ID = objEmp.ID}

            '---------- 1.0 Cập nhật bảng RC_Candidate ----------
            Context.RC_CANDIDATE.Attach(objEmpData)
            objEmpData.CANDIDATE_CODE = objEmp.CANDIDATE_CODE
            objEmpData.FIRST_NAME_VN = objEmp.FIRST_NAME_VN
            objEmpData.LAST_NAME_VN = objEmp.LAST_NAME_VN
            objEmpData.FULLNAME_VN = objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.JOIN_DATE = objEmp.JOIN_DATE
            objEmpData.EFFECT_DATE = objEmp.EFFECT_DATE
            objEmpData.TITLE_NAME = objEmp.TITLE_NAME
            objEmpData.WORK = objEmp.WORK
            objEmpData.FILE_NAME = objEmp.FILE_NAME
            objEmpData.CARE_TITLE_NAME = objEmp.CARE_TITLE_NAME
            objEmpData.RECRUIMENT_WEBSITE = objEmp.RECRUIMENT_WEBSITE
            objEmpData.RC_SOURCE_REC_ID = objEmp.RC_SOURCE_REC_ID
            If objEmp.FILE_SIZE IsNot Nothing Then
                FileInsert(objEmp.ID, pathCandidate, objEmp.FILE_SIZE)
            End If

            '---------- 2.0 Thêm vào bảng RC_Candidate_CV ----------
            Dim objEmpCVData As RC_CANDIDATE_CV
            If objEmpCV IsNot Nothing Then
                Dim isInsert As Boolean = False
                objEmpCVData = (From p In Context.RC_CANDIDATE_CV Where p.CANDIDATE_ID = objEmp.ID).FirstOrDefault
                If objEmpCVData Is Nothing Then
                    objEmpCVData = New RC_CANDIDATE_CV
                    isInsert = True
                End If
                If objEmpCV.IMAGE <> "" Then
                    objEmpCVData.IMAGE = objEmp.CANDIDATE_CODE & objEmpCV.IMAGE 'Lưu Image thành dạng E10012.jpg.
                End If
                objEmpCVData.IS_WORK_PERMIT = objEmpCV.IS_WORK_PERMIT
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_END = objEmpCV.WORK_PERMIT_END
                objEmpCVData.WORK_PERMIT_START = objEmpCV.WORK_PERMIT_START
                objEmpCVData.CANDIDATE_ID = objEmpData.ID
                objEmpCVData.CON_WARD = objEmpCV.CON_WARD
                objEmpCVData.PER_WARD = objEmpCV.PER_WARD
                objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                objEmpCVData.GENDER = objEmpCV.GENDER
                objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                objEmpCVData.ID_NO = objEmpCV.ID_NO
                objEmpCVData.ID_DATE = objEmpCV.ID_DATE
                objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                objEmpCVData.NATIVE = objEmpCV.NATIVE
                objEmpCVData.RELIGION = objEmpCV.RELIGION
                objEmpCVData.PASSPORT_ID = objEmpCV.PASSPORT_ID
                objEmpCVData.PASSPORT_DATE = objEmpCV.PASSPORT_DATE
                objEmpCVData.PASSPORT_PLACE = objEmpCV.PASSPORT_PLACE_NAME
                objEmpCVData.BIRTH_NATION = objEmpCV.BIRTH_NATION_ID
                objEmpCVData.BIRTH_PROVINCE = objEmpCV.BIRTH_PROVINCE
                objEmpCVData.NATIONALITY_ID = objEmpCV.NATIONALITY_ID
                objEmpCVData.NAV_NATION = objEmpCV.NAV_NATION_ID
                objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                objEmpCVData.PER_NATION = objEmpCV.PER_NATION_ID
                objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT_ID
                objEmpCVData.CONTACT_ADDRESS = objEmpCV.CONTACT_ADDRESS
                objEmpCVData.CONTACT_PROVINCE = objEmpCV.CONTACT_PROVINCE
                objEmpCVData.CONTACT_NATION = objEmpCV.CONTACT_NATION_ID
                objEmpCVData.CONTACT_DISTRICT = objEmpCV.CONTACT_DISTRICT_ID
                objEmpCVData.PERTAXCODE = objEmpCV.PERTAXCODE
                objEmpCVData.EDUCATION_MAJORS = objEmpCV.EDUCATION_MAJORS
                objEmpCVData.EDUCATION_ID = objEmpCV.EDUCATION_ID
                objEmpCVData.ID_DATE_EXPIRATION = objEmpCV.ID_DATE_EXPIRATION
                objEmpCVData.IS_RESIDENT = objEmpCV.IS_RESIDENT
                objEmpCVData.CONTACT_ADDRESS_TEMP = objEmpCV.CONTACT_ADDRESS_TEMP
                objEmpCVData.CONTACT_NATION_TEMP = objEmpCV.CONTACT_NATION_TEMP
                objEmpCVData.CONTACT_PROVINCE_TEMP = objEmpCV.CONTACT_PROVINCE_TEMP
                objEmpCVData.CONTACT_DISTRICT_TEMP = objEmpCV.CONTACT_DISTRICT_TEMP
                objEmpCVData.CONTACT_MOBILE = objEmpCV.CONTACT_MOBILE
                objEmpCVData.CONTACT_PHONE = objEmpCV.CONTACT_PHONE
                objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIl
                objEmpCVData.PER_TAX_DATE = objEmpCV.PER_TAX_DATE
                objEmpCVData.PER_TAX_PLACE = objEmpCV.PER_TAX_PLACE
                objEmpCVData.PASSPORT_DATE_EXPIRATION = objEmpCV.PASSPORT_DATE_EXPIRATION
                objEmpCVData.VISA_NUMBER = objEmpCV.VISA_NUMBER
                objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                objEmpCVData.VISA_DATE_EXPIRATION = objEmpCV.VISA_DATE_EXPIRATION
                objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
                objEmpCVData.VNAIRLINES_NUMBER = objEmpCV.VNAIRLINES_NUMBER
                objEmpCVData.VNAIRLINES_DATE = objEmpCV.VNAIRLINES_DATE
                objEmpCVData.VNAIRLINES_DATE_EXPIRATION = objEmpCV.VNAIRLINES_DATE_EXPIRATION
                objEmpCVData.VNAIRLINES_PLACE = objEmpCV.VNAIRLINES_PLACE
                objEmpCVData.LABOUR_NUMBER = objEmpCV.LABOUR_NUMBER
                objEmpCVData.LABOUR_DATE = objEmpCV.LABOUR_DATE
                objEmpCVData.LABOUR_DATE_EXPIRATION = objEmpCV.LABOUR_DATE_EXPIRATION
                objEmpCVData.LABOUR_PLACE = objEmpCV.LABOUR_PLACE
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_START = objEmpCV.WORK_PERMIT_START
                objEmpCVData.WORK_PERMIT_END = objEmpCV.WORK_PERMIT_END
                objEmpCVData.TEMP_RESIDENCE_CARD = objEmpCV.TEMP_RESIDENCE_CARD
                objEmpCVData.TEMP_RESIDENCE_CARD_START = objEmpCV.TEMP_RESIDENCE_CARD_START
                objEmpCVData.TEMP_RESIDENCE_CARD_END = objEmpCV.TEMP_RESIDENCE_CARD_END

                objEmpCVData.FINDER_NAME = objEmpCV.FINDER_NAME
                objEmpCVData.FINDER_SDT = objEmpCV.FINDER_SDT
                objEmpCVData.FINDER_ADDRESS = objEmpCV.FINDER_ADDRESS

                objEmpCVData.URGENT_PER_NAME = objEmpCV.URGENT_PER_NAME
                If objEmpCV.URGENT_PER_RELATION = 0 Then
                    objEmpCVData.URGENT_PER_RELATION = Nothing
                Else
                    objEmpCVData.URGENT_PER_RELATION = objEmpCV.URGENT_PER_RELATION
                End If
                objEmpCVData.URGENT_PER_SDT = objEmpCV.URGENT_PER_SDT
                objEmpCVData.URGENT_ADDRESS = objEmpCV.URGENT_ADDRESS

                objEmpCVData.STRANGER = objEmpCV.STRANGER
                objEmpCVData.WEAKNESS = objEmpCV.WEAKNESS

                If isInsert Then
                    Context.RC_CANDIDATE_CV.AddObject(objEmpCVData)
                End If
            End If

            '---------- 3.0 Thêm vào bảng RC_EDUCATION ----------
            Dim objEmpEduData As New RC_CANDIDATE_EDUCATION
            If objEmpEduData IsNot Nothing Then
                Dim isInsert As Boolean = False
                objEmpEduData = (From p In Context.RC_CANDIDATE_EDUCATION Where p.CANDIDATE_ID = objEmp.ID).FirstOrDefault
                If objEmpEduData Is Nothing Then
                    objEmpEduData = New RC_CANDIDATE_EDUCATION
                    isInsert = True
                End If
                objEmpEduData.YEAR_GRADUATE = objEmpEdu.YEAR_GRADUATE
                objEmpEduData.LANGUAGE_ID = objEmpEdu.LANGUAGE_ID
                objEmpEduData.CERTIFICATE_ID = objEmpEdu.CERTIFICATE_ID
                objEmpEduData.CANDIDATE_ID = objEmpData.ID
                objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
                objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
                objEmpEduData.FIELD = objEmpEdu.FIELD
                objEmpEduData.SCHOOL = objEmpEdu.SCHOOL
                objEmpEduData.MAJOR = objEmpEdu.MAJOR
                objEmpEduData.MARK_EDU = objEmpEdu.MARK_EDU
                objEmpEduData.DEGREE = objEmpEdu.DEGREE
                objEmpEduData.GPA = objEmpEdu.GPA
                objEmpEduData.IT_CERTIFICATE = objEmpEdu.IT_CERTIFICATE
                objEmpEduData.IT_LEVEL = objEmpEdu.IT_LEVEL
                objEmpEduData.IT_MARK = objEmpEdu.IT_MARK
                objEmpEduData.IT_CERTIFICATE1 = objEmpEdu.IT_CERTIFICATE1
                objEmpEduData.IT_LEVEL1 = objEmpEdu.IT_LEVEL1
                objEmpEduData.IT_MARK1 = objEmpEdu.IT_MARK1
                objEmpEduData.IT_CERTIFICATE2 = objEmpEdu.IT_CERTIFICATE2
                objEmpEduData.IT_LEVEL2 = objEmpEdu.IT_LEVEL2
                objEmpEduData.IT_MARK2 = objEmpEdu.IT_MARK2
                objEmpEduData.ENGLISH = objEmpEdu.ENGLISH
                objEmpEduData.ENGLISH_LEVEL = objEmpEdu.ENGLISH_LEVEL
                objEmpEduData.ENGLISH_MARK = objEmpEdu.ENGLISH_MARK
                objEmpEduData.ENGLISH1 = objEmpEdu.ENGLISH1
                objEmpEduData.ENGLISH_LEVEL1 = objEmpEdu.ENGLISH_LEVEL1
                objEmpEduData.ENGLISH_MARK1 = objEmpEdu.ENGLISH_MARK1
                objEmpEduData.ENGLISH2 = objEmpEdu.ENGLISH2
                objEmpEduData.ENGLISH_LEVEL2 = objEmpEdu.ENGLISH_LEVEL2
                objEmpEduData.ENGLISH_MARK2 = objEmpEdu.ENGLISH_MARK2
                objEmpEduData.DATE_START = objEmpEdu.DATE_START
                objEmpEduData.DATE_END = objEmpEdu.DATE_END
                objEmpEduData.ENGLISH_SKILL = objEmpEdu.ENGLISH_SKILL
                objEmpEduData.RATE = objEmpEdu.RATE
                If isInsert Then
                    Context.RC_CANDIDATE_EDUCATION.AddObject(objEmpEduData)
                End If
            End If

            '---------- 4.0 Thêm vào bảng RC_Candidate_OTHER_INFO ----------
            If objEmpOther IsNot Nothing Then
                Dim isInsert As Boolean = False
                Dim objEmpOtherData As RC_CANDIDATE_OTHER_INFO
                objEmpOtherData = (From p In Context.RC_CANDIDATE_OTHER_INFO Where p.CANDIDATE_ID = objEmp.ID).FirstOrDefault
                If objEmpOtherData Is Nothing Then
                    objEmpOtherData = New RC_CANDIDATE_OTHER_INFO
                    isInsert = True
                End If

                objEmpOtherData.CANDIDATE_ID = objEmpData.ID
                objEmpOtherData.NGAYVAOCONGDOAN = objEmpOther.NGAY_VAO_DOAN
                objEmpOtherData.NOIVAOCONGDOAN = objEmpOther.NOI_VAO_DOAN
                objEmpOtherData.CONGDOANPHI = objEmpOther.DOAN_PHI
                objEmpOtherData.ACCOUNT_NAME = objEmpOther.ACCOUNT_NAME
                objEmpOtherData.ACCOUNT_NUMBER = objEmpOther.ACCOUNT_NUMBER
                objEmpOtherData.BANK = objEmpOther.BANK
                objEmpOtherData.BANK_BRANCH = objEmpOther.BANK_BRANCH
                objEmpOtherData.IS_PAYMENT_VIA_BANK = objEmpOther.IS_PAYMENT_VIA_BANK
                objEmpOtherData.ACCOUNT_EFFECT_DATE = objEmpOther.ACCOUNT_EFFECT_DATE

                If isInsert Then
                    Context.RC_CANDIDATE_OTHER_INFO.AddObject(objEmpOtherData)
                End If
            End If

            ' Thêm vào bảng CandidateExpect
            If objEmpExpect IsNot Nothing Then
                Dim isInsert As Boolean = False
                Dim objEmpExpectData As RC_CANDIDATE_EXPECT
                objEmpExpectData = (From p In Context.RC_CANDIDATE_EXPECT Where p.CANDIDATE_ID = objEmp.ID).FirstOrDefault
                If objEmpExpectData Is Nothing Then
                    objEmpExpectData = New RC_CANDIDATE_EXPECT
                    isInsert = True
                End If

                objEmpExpectData.CANDIDATE_ID = objEmpData.ID
                objEmpExpectData.TIME_START = objEmpExpect.TIME_START
                objEmpExpectData.PROBATIONARY_SALARY = objEmpExpect.PROBATIONARY_SALARY
                objEmpExpectData.OFFICIAL_SALARY = objEmpExpect.OFFICIAL_SALARY
                objEmpExpectData.DATE_START = objEmpExpect.DATE_START
                objEmpExpectData.OTHER_REQUEST = objEmpExpect.OTHER_REQUEST
                objEmpExpectData.WORK_LOCATION = objEmpExpect.WORK_LOCATION

                objEmpExpectData.DESIRE_POSITION_1 = objEmpExpect.DESIRE_POSITION_1
                objEmpExpectData.DESIRE_POSITION_2 = objEmpExpect.DESIRE_POSITION_2
                objEmpExpectData.DESIRE_LEVEL = objEmpExpect.DESIRE_LEVEL
                objEmpExpectData.SENIORITY_EXPERIENCE = objEmpExpect.SENIORITY_EXPERIENCE
                objEmpExpectData.WORKED_HSV_HV = objEmpExpect.WORKED_HSV_HV
                If isInsert Then
                    Context.RC_CANDIDATE_EXPECT.AddObject(objEmpExpectData)
                End If
            End If
            ' Thêm vào bảng CandidateHealth
            If objEmpHealth IsNot Nothing Then
                Dim isInsert As Boolean = False
                Dim objEmpHealthData As RC_CANDIDATE_HEALTH
                objEmpHealthData = (From p In Context.RC_CANDIDATE_HEALTH Where p.CANDIDATE_ID = objEmp.ID).FirstOrDefault
                If objEmpHealthData Is Nothing Then
                    objEmpHealthData = New RC_CANDIDATE_HEALTH
                    isInsert = True
                End If

                objEmpHealthData.CANDIDATE_ID = objEmpData.ID
                objEmpHealthData.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                objEmpHealthData.CAN_NANG = objEmpHealth.CAN_NANG
                objEmpHealthData.NHOM_MAU = objEmpHealth.NHOM_MAU
                objEmpHealthData.HUYET_AP = objEmpHealth.HUYET_AP
                objEmpHealthData.MAT_TRAI = objEmpHealth.MAT_TRAI
                objEmpHealthData.MAT_PHAI = objEmpHealth.MAT_PHAI
                objEmpHealthData.LOAI_SUC_KHOE = objEmpHealth.LOAI_SUC_KHOE
                objEmpHealthData.TAI_MUI_HONG = objEmpHealth.TAI_MUI_HONG
                objEmpHealthData.RANG_HAM_MAT = objEmpHealth.RANG_HAM_MAT
                objEmpHealthData.TIM = objEmpHealth.TIM
                objEmpHealthData.PHOI_NGUC = objEmpHealth.PHOI_NGUC
                objEmpHealthData.VIEM_GAN_B = objEmpHealth.VIEM_GAN_B
                objEmpHealthData.DA_HOA_LIEU = objEmpHealth.DA_HOA_LIEU
                objEmpHealthData.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE

                If isInsert Then
                    Context.RC_CANDIDATE_HEALTH.AddObject(objEmpHealthData)
                End If
            End If
            If objEmpCVData.IMAGE <> "" Then
                If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                    Dim savepath = ""
                    savepath = AppDomain.CurrentDomain.BaseDirectory & "\CandidateImage"
                    If Not Directory.Exists(savepath) Then
                        Directory.CreateDirectory(savepath)
                    End If
                    'Xóa ảnh cũ của ứng viên.
                    Dim sFile() As String = Directory.GetFiles(savepath, objEmpCVData.IMAGE)
                    For Each s In sFile
                        File.Delete(s)
                    Next
                    Dim ms As New MemoryStream(_imageBinary)
                    ' instance a filestream pointing to the
                    ' storatge folder, use the original file name
                    ' to name the resulting file

                    Dim fs As New FileStream(savepath & "\" & objEmpCVData.IMAGE, FileMode.Create)
                    ms.WriteTo(fs)

                    ' clean up
                    ms.Close()
                    fs.Close()
                    fs.Dispose()
                End If
            End If

            Context.SaveChanges(log)
            gID = objEmpData.ID

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCandidate")
            Throw ex
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Hàm xóa ứng viên
    ''' </summary>
    ''' <param name="lstEmpID"></param>
    ''' <param name="log"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean
        Dim lstEmpDelete As List(Of RC_CANDIDATE)
        Try
            lstEmpDelete = (From p In Context.RC_CANDIDATE Where lstEmpID.Contains(p.ID)).ToList
            For i As Int16 = 0 To lstEmpDelete.Count - 1
                'Kiểm tra ứng viên có tham gia thi tuyển hay không?
                Dim objDelete = lstEmpDelete(i)
                Dim canID As Decimal = objDelete.ID
                Dim programId As Decimal = objDelete.RC_PROGRAM_ID
                Dim isExam = (From p In Context.RC_PROGRAM_SCHEDULE_CAN
                              From sche In Context.RC_PROGRAM_SCHEDULE.Where(Function(f) f.ID = p.RC_PROGRAM_SCHEDULE_ID)
                              Where p.CANDIDATE_ID = canID And sche.RC_PROGRAM_ID = programId).Count
                If isExam > 0 Then
                    sError = sError & "," & objDelete.CANDIDATE_CODE
                Else
                    '1. Xóa Candidate_CV.
                    Dim lstEmpCVDelete = (From p In Context.RC_CANDIDATE_CV Where p.CANDIDATE_ID = canID).ToList
                    For idx As Int16 = 0 To lstEmpCVDelete.Count - 1
                        Context.RC_CANDIDATE_CV.DeleteObject(lstEmpCVDelete(idx))
                    Next

                    '2. Xóa RC_Candidate_OTHER_INFO
                    Dim lstEmpOtherInfoDelete = (From p In Context.RC_CANDIDATE_OTHER_INFO Where p.CANDIDATE_ID = canID).ToList
                    For idx As Int16 = 0 To lstEmpOtherInfoDelete.Count - 1
                        Context.RC_CANDIDATE_OTHER_INFO.DeleteObject(lstEmpOtherInfoDelete(idx))
                    Next

                    '3. Xóa RC_CANDIDATE_EDUCATION
                    Dim lstEmpEduDelete = (From p In Context.RC_CANDIDATE_EDUCATION Where p.CANDIDATE_ID = canID).ToList
                    For idx As Int16 = 0 To lstEmpEduDelete.Count - 1
                        Context.RC_CANDIDATE_EDUCATION.DeleteObject(lstEmpEduDelete(idx))
                    Next

                    '4. Xóa RC_CANDIDATE_HISTORY
                    Dim lstEmpHistoryDelete = (From p In Context.RC_CANDIDATE_HISTORY Where p.CANDIDATE_ID = canID).ToList
                    For idx As Int16 = 0 To lstEmpHistoryDelete.Count - 1
                        Context.RC_CANDIDATE_HISTORY.DeleteObject(lstEmpHistoryDelete(idx))
                    Next
                    Context.RC_CANDIDATE.DeleteObject(lstEmpDelete(i))

                    '5. Xóa RC_CANDIDATE_FAMILY
                    Dim lstEmpFamilyDelete = (From p In Context.RC_CANDIDATE_FAMILY Where p.CANDIDATE_ID = canID).ToList
                    For idx As Int16 = 0 To lstEmpHistoryDelete.Count - 1
                        Context.RC_CANDIDATE_FAMILY.DeleteObject(lstEmpFamilyDelete(idx))
                    Next
                    Context.RC_CANDIDATE.DeleteObject(lstEmpDelete(i))
                End If
            Next
            If sError = "" Then
                Context.SaveChanges()
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCandidate")
            Throw ex
        End Try
        Return True
    End Function


    Public Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), ByVal programID As Decimal, ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_CANDIDATE With {.ID = canID}
                Context.RC_CANDIDATE.Attach(objEmpData)
                objEmpData.RC_PROGRAM_ID = programID
            Next
            Context.SaveChanges(log)

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateProgramCandidate")
            Throw ex
        End Try

        Return True
    End Function

    Public Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), ByVal statusID As String, ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_CANDIDATE With {.ID = canID}
                Context.RC_CANDIDATE.Attach(objEmpData)
                objEmpData.STATUS_ID = statusID
                If statusID = "DUDK" Then
                    objEmpData.IS_BLACKLIST = Nothing
                End If
                If statusID = RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID Then
                    Dim empCode As String = ""
                    TransferCandidateToHSNV(canID, log, empCode)
                    objEmpData.EMPLOYEE_CODE = empCode
                End If
            Next


            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateStatusCandidate")
            Throw ex
        End Try

        Return True
    End Function

    Public Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_CANDIDATE With {.ID = canID}
                Context.RC_CANDIDATE.Attach(objEmpData)
                objEmpData.IS_PONTENTIAL = bCheck
            Next
            Context.SaveChanges(log)

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdatePontentialCandidate")
            Throw ex
        End Try

        Return True
    End Function

    Public Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_CANDIDATE With {.ID = canID}
                Context.RC_CANDIDATE.Attach(objEmpData)
                objEmpData.IS_BLACKLIST = bCheck
                objEmpData.STATUS_ID = "BLACKLIST"
            Next
            Context.SaveChanges(log)

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateBlackListCandidate")
            Throw ex
        End Try

        Return True
    End Function

    Public Function UpdateBlackListRc_Program_Candidate(ByVal lstCanID As List(Of Decimal), ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_PROGRAM_CANDIDATE With {.ID = canID}
                Context.RC_PROGRAM_CANDIDATE.Attach(objEmpData)
                objEmpData.STATUS_ID = "BLACKLIST"
            Next
            Context.SaveChanges(log)

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateBlackListRc_Program_Candidate")
            Throw ex
        End Try

        Return True
    End Function

    Public Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        Try
            For Each canID In lstCanID
                Dim objEmpData As New RC_CANDIDATE With {.ID = canID}
                Context.RC_CANDIDATE.Attach(objEmpData)
                objEmpData.IS_REHIRE = bCheck
            Next
            Context.SaveChanges(log)

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateBlackListCandidate")
            Throw ex
        End Try

        Return True
    End Function


    ''' <summary>
    ''' Lấy thông tin ứng viên từ CandidateCode
    ''' </summary>
    ''' <param name="sCandidateCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO
        Try
            Try
                '.IMAGE = e.RC_Candidate_CV.IMAGE,
                If sCandidateCode = "" Then Return Nothing
                Dim query As CandidateDTO = (From e In Context.RC_CANDIDATE
                                             From org In Context.HU_ORGANIZATION.Where(Function(f) e.ORG_ID = f.ID).DefaultIfEmpty
                                             From cv In Context.RC_CANDIDATE_CV.Where(Function(f) e.ID = f.CANDIDATE_ID).DefaultIfEmpty
                                             From OT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = e.RC_SOURCE_REC_ID).DefaultIfEmpty
                                             Where (e.CANDIDATE_CODE = sCandidateCode)
                                             Select New CandidateDTO With {
                                                .ID = e.ID,
                                                .FIRST_NAME_VN = e.FIRST_NAME_VN,
                                                .LAST_NAME_VN = e.LAST_NAME_VN,
                                                .FULLNAME_VN = e.FULLNAME_VN,
                                                .CANDIDATE_CODE = e.CANDIDATE_CODE,
                                                .TITLE_ID = e.TITLE_ID,
                                                .ORG_ID = e.ORG_ID,
                                                .ORG_NAME = org.NAME_VN,
                                                .ORG_DESC = org.DESCRIPTION_PATH,
                                                .JOIN_DATE = e.JOIN_DATE,
                                                .EFFECT_DATE = e.EFFECT_DATE,
                                                .FILE_NAME = e.FILE_NAME,
                                                .TITLE_NAME = e.TITLE_NAME,
                                                .WORK = e.WORK,
                                                .STATUS_ID = e.STATUS_ID,
                                                .CARE_TITLE_NAME = e.CARE_TITLE_NAME,
                                                .RECRUIMENT_WEBSITE = e.RECRUIMENT_WEBSITE,
                                                .IMAGE = cv.IMAGE,
                                                .RC_SOURCE_REC_ID = e.RC_SOURCE_REC_ID,
                                                .RC_SOURCE_REC_NAME = OT.NAME_VN
                                                }).FirstOrDefault

                Return query
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateInfo")
            Throw ex
        End Try
    End Function

    Public Function GETCANDIDATEINFO_BY_PSC_ID(ByVal PSC_ID As String) As CandidateDTO
        Try
            Try
                '.IMAGE = e.RC_Candidate_CV.IMAGE,
                If PSC_ID = "" Then Return Nothing
                Dim query As CandidateDTO = (From PSC In Context.RC_PROGRAM_SCHEDULE_CAN
                                             From RC In Context.RC_CANDIDATE.Where(Function(f) PSC.CANDIDATE_ID = f.ID).DefaultIfEmpty
                                             From RC_CV In Context.RC_CANDIDATE_CV.Where(Function(f) RC.ID = f.CANDIDATE_ID).DefaultIfEmpty
                                             From OT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = RC_CV.GENDER).DefaultIfEmpty
                                             Where (PSC.ID = PSC_ID)
                                             Select New CandidateDTO With {
                                                 .ID = PSC.CANDIDATE_ID,
                                                .CANDIDATE_CODE = RC.CANDIDATE_CODE,
                                                .FULLNAME_VN = RC.FULLNAME_VN,
                                                .BIRTH_DATE = RC_CV.BIRTH_DATE,
                                                .GENDER_NAME = OT.NAME_VN
                                                }).FirstOrDefault

                Return query
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GETCANDIDATEINFO_BY_PSC_ID")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy thông tin CV của ứng viên
    ''' </summary>
    ''' <param name="sCandidateID">ID(Decimal) của ứng viên</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_CV
                         From ot1 In Context.HU_WARD.Where(Function(f) f.ID = e.CON_WARD).DefaultIfEmpty
                         From ot2 In Context.HU_WARD.Where(Function(f) f.ID = e.PER_WARD).DefaultIfEmpty
                         Where (e.CANDIDATE_ID = sCandidateID)
                         Select New CandidateCVDTO With {
                     .CANDIDATE_ID = e.CANDIDATE_ID,
                     .GENDER = e.GENDER,
                     .CON_WARD = e.CON_WARD,
                     .CON_WARD_NAME = ot1.NAME_VN,
                     .PER_WARD = e.PER_WARD,
                     .PER_WARD_NAME = ot2.NAME_VN,
                     .MARITAL_STATUS = e.MARITAL_STATUS,
                     .NATIVE = e.NATIVE,
                     .RELIGION = e.RELIGION,
                     .ID_NO = e.ID_NO,
                     .ID_DATE = e.ID_DATE,
                     .ID_PLACE = e.ID_PLACE,
                     .PASSPORT_ID = e.PASSPORT_ID,
                     .PASSPORT_DATE = e.PASSPORT_DATE,
                     .PASSPORT_PLACE_NAME = e.PASSPORT_PLACE,
                     .BIRTH_DATE = e.BIRTH_DATE,
                     .BIRTH_NATION_ID = e.BIRTH_NATION,
                     .BIRTH_PROVINCE = e.BIRTH_PROVINCE,
                     .NATIONALITY_ID = e.NATIONALITY_ID,
                     .NAV_NATION_ID = e.NAV_NATION,
                     .NAV_PROVINCE = e.NAV_PROVINCE,
                     .PER_ADDRESS = e.PER_ADDRESS,
                     .PER_PROVINCE = e.PER_PROVINCE,
                     .PER_NATION_ID = e.PER_NATION,
                     .PER_DISTRICT_ID = e.PER_DISTRICT,
                     .CONTACT_ADDRESS = e.CONTACT_ADDRESS,
                     .CONTACT_PROVINCE = e.CONTACT_PROVINCE,
                     .CONTACT_NATION_ID = e.CONTACT_NATION,
                     .CONTACT_DISTRICT_ID = e.CONTACT_DISTRICT,
                     .EDUCATION_ID = e.EDUCATION_ID,
                     .EDUCATION_MAJORS = e.EDUCATION_MAJORS,
                     .PERTAXCODE = e.PERTAXCODE,
                     .ID_DATE_EXPIRATION = e.ID_DATE_EXPIRATION,
                     .IS_RESIDENT = e.IS_RESIDENT,
                     .CONTACT_ADDRESS_TEMP = e.CONTACT_ADDRESS_TEMP,
                     .CONTACT_NATION_TEMP = e.CONTACT_NATION_TEMP,
                     .CONTACT_PROVINCE_TEMP = e.CONTACT_PROVINCE_TEMP,
                     .CONTACT_DISTRICT_TEMP = e.CONTACT_DISTRICT_TEMP,
                     .CONTACT_MOBILE = e.CONTACT_MOBILE,
                     .CONTACT_PHONE = e.CONTACT_PHONE,
                     .PER_EMAIL = e.PER_EMAIL,
                     .PER_TAX_DATE = e.PER_TAX_DATE,
                     .PER_TAX_PLACE = e.PER_TAX_PLACE,
                     .PASSPORT_DATE_EXPIRATION = e.PASSPORT_DATE_EXPIRATION,
                     .VISA_DATE = e.VISA_DATE,
                     .VISA_DATE_EXPIRATION = e.VISA_DATE_EXPIRATION,
                     .VISA_NUMBER = e.VISA_NUMBER,
                     .VISA_PLACE = e.VISA_PLACE,
                     .VNAIRLINES_DATE = e.VNAIRLINES_DATE,
                     .VNAIRLINES_DATE_EXPIRATION = e.VNAIRLINES_DATE_EXPIRATION,
                     .VNAIRLINES_NUMBER = e.VNAIRLINES_NUMBER,
                     .VNAIRLINES_PLACE = e.VNAIRLINES_PLACE,
                     .LABOUR_DATE = e.LABOUR_DATE,
                     .LABOUR_DATE_EXPIRATION = e.LABOUR_DATE_EXPIRATION,
                     .LABOUR_NUMBER = e.LABOUR_NUMBER,
                     .LABOUR_PLACE = e.LABOUR_PLACE,
                     .WORK_PERMIT = e.WORK_PERMIT,
                     .WORK_PERMIT_END = e.WORK_PERMIT_END,
                     .WORK_PERMIT_START = e.WORK_PERMIT_START,
                     .TEMP_RESIDENCE_CARD = e.TEMP_RESIDENCE_CARD,
                     .TEMP_RESIDENCE_CARD_END = e.TEMP_RESIDENCE_CARD_END,
                     .TEMP_RESIDENCE_CARD_START = e.TEMP_RESIDENCE_CARD_START,
                     .WORK_EMAIl = e.WORK_EMAIL,
                     .FINDER_NAME = e.FINDER_NAME,
                     .FINDER_SDT = e.FINDER_SDT,
                     .FINDER_ADDRESS = e.FINDER_ADDRESS,
                     .URGENT_PER_NAME = e.URGENT_PER_NAME,
                     .URGENT_PER_RELATION = If(e.URGENT_PER_RELATION Is Nothing, 0, e.URGENT_PER_RELATION),
                     .URGENT_PER_SDT = e.URGENT_PER_SDT,
                     .URGENT_ADDRESS = e.URGENT_ADDRESS,
                     .STRANGER = e.STRANGER,
                     .WEAKNESS = e.WEAKNESS,
                     .IS_WORK_PERMIT = e.IS_WORK_PERMIT
                     }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateCV")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy thông tin Nhân thân của ứng viên
    ''' </summary>
    ''' <param name="sCandidateID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_FAMILY
                         Where (e.CANDIDATE_ID = sCandidateID)
                         Select New CandidateFamilyDTO With {
                          .ID = e.ID,
                          .FULLNAME = e.FULLNAME,
                          .RELATION_ID = e.RELATION_ID,
                          .PHONE_NUMBER = e.PHONE_NUMBER,
                          .ADDRESS = e.ADDRESS
                          }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateFamily")
            Throw ex
        End Try
    End Function

    Public Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_EDUCATION
                         From OT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = e.LANGUAGE_ID).DefaultIfEmpty
                         From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = e.CERTIFICATE_ID).DefaultIfEmpty
                         Where e.CANDIDATE_ID = sCandidateID
                         Select New CandidateEduDTO With {
               .CANDIDATE_ID = e.CANDIDATE_ID,
               .ACADEMY = e.ACADEMY,
               .HE_DAO_TAO = e.HE_DAO_TAO,
               .YEAR_GRADUATE = e.YEAR_GRADUATE,
               .LANGUAGE_ID = e.LANGUAGE_ID,
               .CERTIFICATE_ID = e.CERTIFICATE_ID,
               .LANGUAGE_NAME = OT.NAME_VN,
               .CERTIFICATE_NAME = OT1.NAME_VN,
               .HIGHEST_EDU = e.HIGHEST_EDU,
               .HIGHEST_LEVEL = e.HIGHEST_LEVEL,
               .TRAIN_FORM = e.TRAIN_FORM,
               .SCHOOL_NAME = e.SCHOOL_NAME,
               .GRADUATE_YEAR = e.GRADUATE_YEAR,
               .MARK_EDU = e.MARK_EDU,
               .COMPUTER_LEVEL = e.COMPUTER_LEVEL,
               .LANGUAGE_LEVEL = e.LANGUAGE_LEVEL,
               .POLOTOCA_THEORY = e.POLOTOCA_THEORY,
               .STATE_MNG = e.STATE_MNG,
               .ADD_INFO = e.ADD_INFO,
               .LEARNING_LEVEL = e.LEARNING_LEVEL,
               .FIELD = e.FIELD,
               .SCHOOL = e.SCHOOL,
               .MAJOR = e.MAJOR,
               .DEGREE = e.DEGREE,
               .GPA = e.GPA,
               .IT_CERTIFICATE = e.IT_CERTIFICATE,
               .IT_LEVEL = e.IT_LEVEL,
               .IT_MARK = e.IT_MARK,
               .IT_CERTIFICATE1 = e.IT_CERTIFICATE1,
               .IT_LEVEL1 = e.IT_LEVEL1,
               .IT_MARK1 = e.IT_MARK1,
               .IT_CERTIFICATE2 = e.IT_CERTIFICATE2,
               .IT_LEVEL2 = e.IT_LEVEL2,
               .IT_MARK2 = e.IT_MARK2,
               .ENGLISH = e.ENGLISH,
               .ENGLISH_LEVEL = e.ENGLISH_LEVEL,
               .ENGLISH_MARK = e.ENGLISH_MARK,
               .ENGLISH1 = e.ENGLISH1,
               .ENGLISH_LEVEL1 = e.ENGLISH_LEVEL1,
               .ENGLISH_MARK1 = e.ENGLISH_MARK1,
               .ENGLISH2 = e.ENGLISH2,
               .ENGLISH_LEVEL2 = e.ENGLISH_LEVEL2,
               .ENGLISH_MARK2 = e.ENGLISH_MARK2,
                .RATE = e.RATE
           }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateEdu")
            Throw ex
        End Try
    End Function

    Public Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_OTHER_INFO Where e.CANDIDATE_ID = sCandidateID
                         From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) e.BANK_BRANCH = f.ID).DefaultIfEmpty
                         Select New CandidateOtherInfoDTO With {
                             .CANDIDATE_ID = e.CANDIDATE_ID,
                             .NOI_VAO_DOAN = e.NOIVAOCONGDOAN,
                             .NGAY_VAO_DOAN = e.NGAYVAOCONGDOAN,
                             .DOAN_PHI = e.CONGDOANPHI,
                             .ACCOUNT_NAME = e.ACCOUNT_NAME,
                             .ACCOUNT_NUMBER = e.ACCOUNT_NUMBER,
                             .BANK = e.BANK,
                             .BANK_BRANCH = e.BANK_BRANCH,
                             .IS_PAYMENT_VIA_BANK = e.IS_PAYMENT_VIA_BANK,
                             .ACCOUNT_EFFECT_DATE = e.ACCOUNT_EFFECT_DATE,
                             .BANK_BRANCH_Name = bankbranch.NAME
                         }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateOtherInfo")
            Throw ex
        End Try
    End Function

    Public Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_HEALTH Where e.CANDIDATE_ID = sCandidateID
                         Select New CandidateHealthDTO With {
                             .CANDIDATE_ID = e.CANDIDATE_ID,
                             .CHIEU_CAO = e.CHIEU_CAO,
                             .CAN_NANG = e.CAN_NANG,
                             .NHOM_MAU = e.NHOM_MAU,
                             .HUYET_AP = e.HUYET_AP,
                             .MAT_TRAI = e.MAT_TRAI,
                             .MAT_PHAI = e.MAT_PHAI,
                             .LOAI_SUC_KHOE = e.LOAI_SUC_KHOE,
                             .TAI_MUI_HONG = e.TAI_MUI_HONG,
                             .RANG_HAM_MAT = e.RANG_HAM_MAT,
                             .TIM = e.TIM,
                             .PHOI_NGUC = e.PHOI_NGUC,
                             .VIEM_GAN_B = e.VIEM_GAN_B,
                             .DA_HOA_LIEU = e.DA_HOA_LIEU,
                             .GHI_CHU_SUC_KHOE = e.GHI_CHU_SUC_KHOE
                             }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateHealthInfo")
            Throw ex
        End Try
    End Function
    Public Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO
        Try
            Dim query = (From e In Context.RC_CANDIDATE_EXPECT Where e.CANDIDATE_ID = sCandidateID
                         From TIT1 In Context.HU_TITLE.Where(Function(f) e.DESIRE_POSITION_1 = f.ID).DefaultIfEmpty
                         From TIT2 In Context.HU_TITLE.Where(Function(f) e.DESIRE_POSITION_2 = f.ID).DefaultIfEmpty
                         Select New CandidateExpectDTO With {
                .CANDIDATE_ID = e.CANDIDATE_ID,
                .TIME_START = e.TIME_START,
                .PROBATIONARY_SALARY = e.PROBATIONARY_SALARY,
                .OFFICIAL_SALARY = e.OFFICIAL_SALARY,
                .DATE_START = e.DATE_START,
                .OTHER_REQUEST = e.OTHER_REQUEST,
                .WORK_LOCATION = e.WORK_LOCATION,
                             .DESIRE_POSITION_1 = e.DESIRE_POSITION_1,
                             .DESIRE_POSITION_1_NAME = TIT1.NAME_VN,
                             .DESIRE_POSITION_2 = e.DESIRE_POSITION_2,
                             .DESIRE_POSITION_2_NAME = TIT2.NAME_VN,
                             .DESIRE_LEVEL = e.DESIRE_LEVEL,
                             .SENIORITY_EXPERIENCE = e.SENIORITY_EXPERIENCE,
                             .WORKED_HSV_HV = e.WORKED_HSV_HV
                }).FirstOrDefault
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateExpectInfo")
            Throw ex
        End Try
    End Function

    Public Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO)
        Try
            'Dim query = From c In Context.RC_CANDIDATE
            '            From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = c.ID).DefaultIfEmpty
            '            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = c.ORG_ID).DefaultIfEmpty
            '            From tittle In Context.HU_TITLE.Where(Function(f) f.ID = c.TITLE_ID).DefaultIfEmpty
            '            From pro In Context.RC_PROGRAM.Where(Function(f) f.ID = c.RC_PROGRAM_ID And (f.IS_PORTAL Is Nothing OrElse f.IS_PORTAL = 0)).DefaultIfEmpty
            '            From stage In Context.RC_STAGE.Where(Function(f) f.ID = pro.STAGE_ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = c.STATUS_ID).DefaultIfEmpty
            '            Where c.ID = sCandidateID
            '            Select New CandidateHistoryDTO With {
            '            .ID = c.ID,
            '            .CANDIDATE_ID = c.ID,
            '            .STAGE_NAME = stage.TITLE,
            '            .WORK_UNIT = org.NAME_VN,
            '            .TITLE_NAME = tittle.NAME_VN,
            '            .START_DATE = stage.STARTDATE,
            '            .END_DATE = stage.ENDDATE,
            '            .STATUS = status.NAME_VN
            '            }

            Dim query = From pro_can In Context.RC_PROGRAM_CANDIDATE
                        From pro In Context.RC_PROGRAM.Where(Function(f) f.ID = pro_can.RC_PROGRAM_ID).DefaultIfEmpty
                        From req In Context.RC_REQUEST.Where(Function(f) f.ID = pro.RC_REQUEST_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = req.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = req.TITLE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = pro_can.STATUS_ID).DefaultIfEmpty
                        Where pro_can.CANDIDATE_ID = sCandidateID
                        Select New CandidateHistoryDTO With {
                        .ID = pro_can.CANDIDATE_ID,
                        .CANDIDATE_ID = pro_can.CANDIDATE_ID,
                        .WORK_UNIT = org.NAME_VN,
                        .TITLE_NAME = title.NAME_VN,
                        .START_DATE = req.SEND_DATE,
                        .END_DATE = req.EXPECTED_JOIN_DATE,
                        .STATUS = status.NAME_VN,
                        .CODE_RC = req.CODE_RC
                        }

            Dim lst = query
            'Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateHistory")
            Throw ex
        End Try
    End Function

    Public Function FileInsert(ByVal fileID As String, ByVal sPath As String, ByVal fileBytes As Byte()) As Boolean
        Try
            If DeleteFile(fileID, sPath) And SaveFile(fileID, sPath, fileBytes) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function DeleteFile(ByVal fileId As Decimal, ByVal sPath As String) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & sPath
            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)

            If fInfo.Exists Then
                fInfo.Delete()
            End If

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function SaveFile(ByVal fileId As Decimal, ByVal sPath As String, ByVal fileBytes As Byte()) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & sPath
            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fPath = Path.Combine(filePath, fileId.ToString)
            Dim fInfo As New FileInfo(fPath)
            Dim fStream As FileStream

            fStream = fInfo.Open(FileMode.Create)

            fStream.Write(fileBytes, 0, fileBytes.Length)

            fStream.Close()
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCandidateImport() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_RECRUITMENT.GET_CANDIDATE_IMPORT",
                                           New With {.P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR,
                                                     .P_CUR4 = cls.OUT_CURSOR,
                                                     .P_CUR5 = cls.OUT_CURSOR,
                                                     .P_CUR6 = cls.OUT_CURSOR,
                                                     .P_CUR7 = cls.OUT_CURSOR,
                                                     .P_CUR8 = cls.OUT_CURSOR,
                                                     .P_CUR9 = cls.OUT_CURSOR,
                                                     .P_CUR10 = cls.OUT_CURSOR,
                                                     .P_CUR11 = cls.OUT_CURSOR,
                                                     .P_CUR12 = cls.OUT_CURSOR,
                                                     .P_CUR13 = cls.OUT_CURSOR,
                                                     .P_CUR14 = cls.OUT_CURSOR,
                                                     .P_CUR15 = cls.OUT_CURSOR,
                                                     .P_CUR16 = cls.OUT_CURSOR,
                                                     .P_CUR17 = cls.OUT_CURSOR,
                                                     .P_CUR18 = cls.OUT_CURSOR}, False)

                Return dsData
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ImportCandidate(ByVal lst As List(Of CandidateImportDTO), ByVal log As UserLog) As Boolean
        Try
            For Each can In lst
                InsertCandidate(can.can, log, 0, "", Nothing, can.can_cv, can.can_edu, can.can_other, Nothing, Nothing, Nothing)
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Family"
    Public Function InsertCandidateFamily(ByVal objDTO As CandidateFamilyDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal, Optional ByVal isInsert As Boolean = True) As Boolean
        Try

            Dim objData As New RC_CANDIDATE_FAMILY
            objData.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_FAMILY.EntitySet.Name)
            objData.CANDIDATE_ID = objDTO.CANDIDATE_ID
            objData.FULLNAME = objDTO.FULLNAME
            objData.BIRTH_DAY = objDTO.BIRTH_DAY
            objData.BIRTH_MONTH = objDTO.BIRTH_MONTH
            objData.BIRTH_YEAR = objDTO.BIRTH_YEAR
            objData.ID_NO = objDTO.ID_NO
            objData.RELATION_ID = objDTO.RELATION_ID
            objData.JOB = objDTO.JOB
            objData.COMPANY = objDTO.COMPANY
            objData.ADDRESS = objDTO.ADDRESS
            objData.IS_MLG = objDTO.IS_MLG
            objData.IS_DIED = objDTO.IS_DIED
            objData.DIED_DATE = objDTO.DIED_DATE
            objData.REMARK = objDTO.REMARK

            objData.PIT_CODE = objDTO.PIT_CODE
            objData.PIT_DATE = objDTO.PIT_DATE
            objData.ID_DATE = objDTO.ID_DATE
            objData.ID_PLACE = objDTO.ID_PLACE
            objData.PER_ADDRESS = objDTO.PER_ADDRESS
            objData.BIRTH_NO = objDTO.BIRTH_NO
            objData.BIRTH_BOOK = objDTO.BIRTH_BOOK
            objData.BIRTH_NAT = objDTO.BIRTH_NAT
            objData.BIRTH_PRO = objDTO.BIRTH_PRO
            objData.BIRTH_DIS = objDTO.BIRTH_DIS
            objData.BIRTH_NAT2 = objDTO.BIRTH_NAT2
            Context.RC_CANDIDATE_FAMILY.AddObject(objData)

            If isInsert Then
                Context.SaveChanges(log)
            End If
            gID = objData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidateFamily")
            Throw ex
        End Try
    End Function

    Public Function ModifyCandidateFamily(ByVal objDTO As CandidateFamilyDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New RC_CANDIDATE_FAMILY With {.ID = objDTO.ID}
        Try
            Context.RC_CANDIDATE_FAMILY.Attach(objData)
            objData.CANDIDATE_ID = objDTO.CANDIDATE_ID
            objData.FULLNAME = objDTO.FULLNAME
            objData.BIRTH_DAY = objDTO.BIRTH_DAY
            objData.BIRTH_MONTH = objDTO.BIRTH_MONTH
            objData.BIRTH_YEAR = objDTO.BIRTH_YEAR
            objData.ID_NO = objDTO.ID_NO
            objData.RELATION_ID = objDTO.RELATION_ID
            objData.JOB = objDTO.JOB
            objData.COMPANY = objDTO.COMPANY
            objData.ADDRESS = objDTO.ADDRESS
            objData.IS_MLG = objDTO.IS_MLG
            objData.IS_DIED = objDTO.IS_DIED
            objData.DIED_DATE = objDTO.DIED_DATE
            objData.REMARK = objDTO.REMARK

            objData.PIT_CODE = objDTO.PIT_CODE
            objData.PIT_DATE = objDTO.PIT_DATE
            objData.ID_DATE = objDTO.ID_DATE
            objData.ID_PLACE = objDTO.ID_PLACE
            objData.PER_ADDRESS = objDTO.PER_ADDRESS
            objData.BIRTH_NO = objDTO.BIRTH_NO
            objData.BIRTH_BOOK = objDTO.BIRTH_BOOK
            objData.BIRTH_NAT = objDTO.BIRTH_NAT
            objData.BIRTH_PRO = objDTO.BIRTH_PRO
            objData.BIRTH_DIS = objDTO.BIRTH_DIS
            objData.BIRTH_NAT2 = objDTO.BIRTH_NAT2
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCandidateFamily")
            Throw ex
        End Try

    End Function

    Public Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO)

        Try
            Dim queryEmp = (From p In Context.RC_CANDIDATE Where p.ID = _filter.CANDIDATE_ID).FirstOrDefault
            'Lấy quá trình khi dùng phần mềm

            Dim query As New List(Of CandidateFamilyDTO)
            'Dim lstWorkOther As List(Of CandidateFamilyDTO)

            query = (From p In Context.RC_CANDIDATE_FAMILY
                     From ot In Context.OT_OTHER_LIST.Where(Function(v) v.ID = p.RELATION_ID).DefaultIfEmpty()
                     Where p.CANDIDATE_ID = _filter.CANDIDATE_ID
                     Select New CandidateFamilyDTO With {.ID = p.ID,
                                                .CANDIDATE_ID = p.CANDIDATE_ID,
                                                .FULLNAME = p.FULLNAME,
                                                .BIRTH_DAY = p.BIRTH_DAY,
                                                .BIRTH_MONTH = p.BIRTH_MONTH,
                                                .BIRTH_YEAR = p.BIRTH_YEAR,
                                                .ID_NO = p.ID_NO,
                                                .RELATION_ID = p.RELATION_ID,
                                                .RELATION_NAME = ot.NAME_VN,
                                                .JOB = p.JOB,
                                                .COMPANY = p.COMPANY,
                                                .ADDRESS = p.ADDRESS,
                                                .IS_MLG = p.IS_MLG,
                                                .IS_DIED = p.IS_DIED,
                                                .DIED_DATE = p.DIED_DATE,
                                                .REMARK = p.REMARK,
                                                .PIT_CODE = p.PIT_CODE,
                                                .PIT_DATE = p.PIT_DATE,
                                                .ID_DATE = p.ID_DATE,
                                                .ID_PLACE = p.ID_PLACE,
                                                .PER_ADDRESS = p.PER_ADDRESS,
                                                .BIRTH_NO = p.BIRTH_NO,
                                                .BIRTH_BOOK = p.BIRTH_BOOK,
                                                .BIRTH_NAT = p.BIRTH_NAT,
                                                .BIRTH_PRO = p.BIRTH_PRO,
                                                .BIRTH_DIS = p.BIRTH_DIS,
                                                .BIRTH_NAT2 = p.BIRTH_NAT2}).ToList


            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateFamily")
            Throw ex
        End Try
    End Function

    Public Function DeleteCandidateFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of RC_CANDIDATE_FAMILY)
        Try
            lst = (From p In Context.RC_CANDIDATE_FAMILY Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.RC_CANDIDATE_FAMILY.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCandidateFamily")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Hàm check trùng CMND của thân nhân.
    ''' </summary>
    ''' <param name="_validate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function ValidateFamily(ByVal _validate As CandidateFamilyDTO)
        Dim query
        Try
            If _validate.ID_NO <> Nothing Then
                query = (From p In Context.RC_CANDIDATE_FAMILY Where p.ID_NO.ToUpper = _validate.ID_NO.ToUpper And p.ID <> _validate.ID And p.ID <> 0).SingleOrDefault
                Return (query Is Nothing)
            End If

            Return True
        Catch ex As Exception
            Return False
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateFamily")
        End Try
    End Function
#End Region

#Region "Cá nhân tự đào tạo"
    Public Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal, Optional ByVal isInsert As Boolean = True) As Boolean
        Try

            Dim objTrainSingerData As New RC_CANDIDATE_TRAINSINGER
            objTrainSingerData.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_TRAINSINGER.EntitySet.Name)
            objTrainSingerData.CANDIDATE_ID = objTrainSinger.CANDIDATE_ID
            objTrainSingerData.BRANCH_NAME = objTrainSinger.BRANCH_NAME
            objTrainSingerData.SCHOOL_NAME = objTrainSinger.SCHOOL_NAME
            objTrainSingerData.LEVEL = objTrainSinger.LEVEL
            objTrainSingerData.CERTIFICATE = objTrainSinger.CERTIFICATE
            objTrainSingerData.CONTENT = objTrainSinger.CONTENT
            objTrainSingerData.TRAINNING = objTrainSinger.TRAINNING
            objTrainSingerData.FROMDATE = objTrainSinger.FROMDATE
            objTrainSingerData.TODATE = objTrainSinger.TODATE
            objTrainSingerData.RANK = objTrainSinger.RANK
            objTrainSingerData.YEAR_GRADUATE = objTrainSinger.YEAR_GRADUATE
            objTrainSingerData.COST = objTrainSinger.COST
            objTrainSingerData.REMARK = objTrainSinger.REMARK
            Context.RC_CANDIDATE_TRAINSINGER.AddObject(objTrainSingerData)
            If isInsert Then
                Context.SaveChanges(log)
            End If
            gID = objTrainSingerData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidateTrainSinger")
            Throw ex
        End Try
    End Function

    Public Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTrainSingerData As New RC_CANDIDATE_TRAINSINGER With {.ID = objTrainSinger.ID}
        Try

            Context.RC_CANDIDATE_TRAINSINGER.Attach(objTrainSingerData)
            objTrainSingerData.CANDIDATE_ID = objTrainSinger.CANDIDATE_ID
            objTrainSingerData.SCHOOL_NAME = objTrainSinger.SCHOOL_NAME
            objTrainSingerData.BRANCH_NAME = objTrainSinger.BRANCH_NAME
            objTrainSingerData.LEVEL = objTrainSinger.LEVEL
            objTrainSingerData.CERTIFICATE = objTrainSinger.CERTIFICATE
            objTrainSingerData.CONTENT = objTrainSinger.CONTENT
            objTrainSingerData.TRAINNING = objTrainSinger.TRAINNING
            objTrainSingerData.FROMDATE = objTrainSinger.FROMDATE
            objTrainSingerData.TODATE = objTrainSinger.TODATE
            objTrainSingerData.RANK = objTrainSinger.RANK
            objTrainSingerData.COST = objTrainSinger.COST
            objTrainSingerData.REMARK = objTrainSinger.REMARK
            objTrainSingerData.YEAR_GRADUATE = objTrainSinger.YEAR_GRADUATE
            Context.SaveChanges(log)
            gID = objTrainSingerData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCandidateTrainSinger")
            Throw ex
        End Try

    End Function

    Public Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO)

        Try
            Dim query As New List(Of TrainSingerDTO)
            query = (From p In Context.RC_CANDIDATE_TRAINSINGER
                     Where p.CANDIDATE_ID = _filter.CANDIDATE_ID
                     Order By p.CREATED_DATE Descending
                     Select New TrainSingerDTO With {
                      .ID = p.ID,
                      .CANDIDATE_ID = p.CANDIDATE_ID,
                      .SCHOOL_NAME = p.SCHOOL_NAME,
                      .BRANCH_NAME = p.BRANCH_NAME,
                      .LEVEL = p.LEVEL,
                      .CERTIFICATE = p.CERTIFICATE,
                      .CONTENT = p.CONTENT,
                      .TRAINNING = p.TRAINNING,
                      .FROMDATE = p.FROMDATE,
                       .TODATE = p.TODATE,
                       .RANK = p.RANK,
                      .COST = p.COST,
                      .REMARK = p.REMARK,
                       .YEAR_GRADUATE = p.YEAR_GRADUATE}).ToList
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateTrainSinger")
            Throw ex
        End Try
    End Function

    Public Function DeleteCandidateTrainSinger(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of RC_CANDIDATE_TRAINSINGER)
        Try
            lst = (From p In Context.RC_CANDIDATE_TRAINSINGER Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.RC_CANDIDATE_TRAINSINGER.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCandidateTrainSinger")
            Throw ex
        End Try

    End Function


#End Region

#Region "Người tham chiếu"
    Public Function InsertCandidateReference(ByVal objReference As CandidateReferenceDTO, ByVal log As UserLog,
                                        ByRef gID As Decimal, Optional ByVal isInsert As Boolean = True) As Boolean
        Try

            Dim objReferenceData As New RC_CANDIDATE_REFERENCE
            objReferenceData.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_REFERENCE.EntitySet.Name)
            objReferenceData.CANDIDATE_ID = objReference.CANDIDATE_ID
            objReferenceData.FULLNAME = objReference.FULLNAME
            objReferenceData.TITLE_NAME = objReference.TITLE_NAME
            objReferenceData.WORK_UNIT = objReference.WORK_UNIT
            objReferenceData.ADDRESS_CONTACT = objReference.ADDRESS_CONTACT
            objReferenceData.PHONENUMBER = objReference.PHONENUMBER
            Context.RC_CANDIDATE_REFERENCE.AddObject(objReferenceData)
            If isInsert Then
                Context.SaveChanges(log)
            End If
            gID = objReferenceData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidateReference")
            Throw ex
        End Try
    End Function
    Public Function ModifyCandidateReference(ByVal objReference As CandidateReferenceDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objReferenceData As New RC_CANDIDATE_REFERENCE With {.ID = objReference.ID}
        Try

            Context.RC_CANDIDATE_REFERENCE.Attach(objReferenceData)
            objReferenceData.CANDIDATE_ID = objReference.CANDIDATE_ID
            objReferenceData.FULLNAME = objReference.FULLNAME
            objReferenceData.TITLE_NAME = objReference.TITLE_NAME
            objReferenceData.WORK_UNIT = objReference.WORK_UNIT
            objReferenceData.ADDRESS_CONTACT = objReference.ADDRESS_CONTACT
            objReferenceData.PHONENUMBER = objReference.PHONENUMBER
            Context.SaveChanges(log)
            gID = objReferenceData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCandidateReference")
            Throw ex
        End Try

    End Function
    Public Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO)

        Try
            Dim query As New List(Of CandidateReferenceDTO)
            query = (From p In Context.RC_CANDIDATE_REFERENCE
                     Where p.CANDIDATE_ID = _filter.CANDIDATE_ID
                     Order By p.CANDIDATE_ID Descending
                     Select New CandidateReferenceDTO With {
                      .ID = p.ID,
                      .CANDIDATE_ID = p.CANDIDATE_ID,
                      .FULLNAME = p.FULLNAME,
                      .TITLE_NAME = p.TITLE_NAME,
                      .WORK_UNIT = p.WORK_UNIT,
                      .ADDRESS_CONTACT = p.ADDRESS_CONTACT,
                      .PHONENUMBER = p.PHONENUMBER}).ToList
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateReference")
            Throw ex
        End Try
    End Function
    Public Function DeleteCandidateReference(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of RC_CANDIDATE_REFERENCE)
        Try
            lst = (From p In Context.RC_CANDIDATE_REFERENCE Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.RC_CANDIDATE_REFERENCE.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCandidateReference")
            Throw ex
        End Try

    End Function
#End Region
#Region "Trước khi vào ML"
    Public Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal, Optional ByVal isInsert As Boolean = True) As Boolean
        Try

            Dim objCandidateBeforeWTData As New RC_CANDIDATE_BEFOREWT
            objCandidateBeforeWTData.ID = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE_BEFOREWT.EntitySet.Name)
            objCandidateBeforeWTData.CANDIDATE_ID = objCandidateBeforeWT.CANDIDATE_ID
            objCandidateBeforeWTData.FROMDATE = objCandidateBeforeWT.FROMDATE
            objCandidateBeforeWTData.TODATE = objCandidateBeforeWT.TODATE
            objCandidateBeforeWTData.ORG_NAME = objCandidateBeforeWT.ORG_NAME
            objCandidateBeforeWTData.TITLE_NAME = objCandidateBeforeWT.TITLE_NAME
            objCandidateBeforeWTData.WORK = objCandidateBeforeWT.WORK
            objCandidateBeforeWTData.SALARY = objCandidateBeforeWT.SALARY
            objCandidateBeforeWTData.REFERENCE = objCandidateBeforeWT.REFERENCE
            objCandidateBeforeWTData.REMARK = objCandidateBeforeWT.REMARK
            objCandidateBeforeWTData.ORG_PHONE = objCandidateBeforeWT.ORG_PHONE
            objCandidateBeforeWTData.ORG_ADDRESS = objCandidateBeforeWT.ORG_ADDRESS
            objCandidateBeforeWTData.DIRECT_MANAGER = objCandidateBeforeWT.DIRECT_MANAGER
            objCandidateBeforeWTData.REASON_LEAVE = objCandidateBeforeWT.REASON_LEAVE

            Context.RC_CANDIDATE_BEFOREWT.AddObject(objCandidateBeforeWTData)
            If isInsert Then
                Context.SaveChanges(log)
            End If
            gID = objCandidateBeforeWTData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidateCandidateBeforeWT")
            Throw ex
        End Try
    End Function

    Public Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCandidateBeforeWTData As New RC_CANDIDATE_BEFOREWT With {.ID = objCandidateBeforeWT.ID}
        Try

            Context.RC_CANDIDATE_BEFOREWT.Attach(objCandidateBeforeWTData)
            objCandidateBeforeWTData.CANDIDATE_ID = objCandidateBeforeWT.CANDIDATE_ID
            objCandidateBeforeWTData.FROMDATE = objCandidateBeforeWT.FROMDATE
            objCandidateBeforeWTData.TODATE = objCandidateBeforeWT.TODATE
            objCandidateBeforeWTData.ORG_NAME = objCandidateBeforeWT.ORG_NAME
            objCandidateBeforeWTData.TITLE_NAME = objCandidateBeforeWT.TITLE_NAME
            objCandidateBeforeWTData.WORK = objCandidateBeforeWT.WORK
            objCandidateBeforeWTData.SALARY = objCandidateBeforeWT.SALARY
            objCandidateBeforeWTData.REFERENCE = objCandidateBeforeWT.REFERENCE
            objCandidateBeforeWTData.REMARK = objCandidateBeforeWT.REMARK
            objCandidateBeforeWTData.ORG_PHONE = objCandidateBeforeWT.ORG_PHONE
            objCandidateBeforeWTData.ORG_ADDRESS = objCandidateBeforeWT.ORG_ADDRESS
            objCandidateBeforeWTData.DIRECT_MANAGER = objCandidateBeforeWT.DIRECT_MANAGER
            objCandidateBeforeWTData.REASON_LEAVE = objCandidateBeforeWT.REASON_LEAVE
            Context.SaveChanges(log)
            gID = objCandidateBeforeWTData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyCandidateCandidateBeforeWT")
            Throw ex
        End Try

    End Function

    Public Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO)

        Try
            Dim query As New List(Of CandidateBeforeWTDTO)
            query = (From p In Context.RC_CANDIDATE_BEFOREWT
                     Where p.CANDIDATE_ID = _filter.CANDIDATE_ID
                     Order By p.CREATED_DATE Descending
                     Select New CandidateBeforeWTDTO With {
                      .ID = p.ID,
                      .CANDIDATE_ID = p.CANDIDATE_ID,
                      .FROMDATE = p.FROMDATE,
                      .TODATE = p.TODATE,
                      .ORG_NAME = p.ORG_NAME,
                      .TITLE_NAME = p.TITLE_NAME,
                      .WORK = p.WORK,
                      .SALARY = p.SALARY,
                      .REFERENCE = p.REFERENCE,
                      .REMARK = p.REMARK,
                      .ORG_PHONE = p.ORG_PHONE,
                      .ORG_ADDRESS = p.ORG_ADDRESS,
                      .DIRECT_MANAGER = p.DIRECT_MANAGER,
                      .REASON_LEAVE = p.REASON_LEAVE}).ToList
            Return query
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateCandidateBeforeWT")
            Throw ex
        End Try
    End Function

    Public Function DeleteCandidateBeforeWT(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of RC_CANDIDATE_BEFOREWT)
        Try
            lst = (From p In Context.RC_CANDIDATE_BEFOREWT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.RC_CANDIDATE_BEFOREWT.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCandidateCandidateBeforeWT")
            Throw ex
        End Try

    End Function


#End Region

#End Region

#Region "ProgramSchedule"

    Public Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO)

        Try
            Dim query = From p In Context.RC_PROGRAM_SCHEDULE
                        From exam In Context.RC_PROGRAM_EXAMS.Where(Function(f) f.ID = p.RC_PROGRAM_EXAMS_ID)
                        Where p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID
                        Select New ProgramScheduleDTO With {
                                        .ID = p.ID,
                                        .SCHEDULE_DATE = p.SCHEDULE_DATE,
                                        .EXAMS_NAME = exam.NAME,
                                        .EXAMS_ORDER = exam.EXAMS_ORDER,
                                        .EXAMS_POINT_LADDER = exam.POINT_LADDER,
                                        .EXAMS_POINT_PASS = exam.POINT_PASS}

            Dim lst = query

            If _filter.SCHEDULE_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.SCHEDULE_DATE = _filter.SCHEDULE_DATE)
            End If
            If _filter.EXAMS_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXAMS_NAME.ToUpper.Contains(_filter.EXAMS_NAME.ToUpper))
            End If
            If _filter.EXAMS_POINT_LADDER IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXAMS_POINT_LADDER = _filter.EXAMS_POINT_LADDER)
            End If
            If _filter.EXAMS_POINT_PASS IsNot Nothing Then
                lst = lst.Where(Function(f) f.EXAMS_POINT_PASS = _filter.EXAMS_POINT_PASS)
            End If
            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramSchedule")
            Throw ex
        End Try

    End Function
    Public Function CheckExist_Program_Schedule_Can(ByVal CanID As Decimal, ByVal ProID As Decimal, ByVal EXAMODER As Decimal) As Boolean
        Try
            Dim query = (From p In Context.RC_PROGRAM_SCHEDULE
                         From scan In Context.RC_PROGRAM_SCHEDULE_CAN.Where(Function(f) f.RC_PROGRAM_SCHEDULE_ID = p.ID)
                         From exam In Context.RC_PROGRAM_EXAMS.Where(Function(f) f.ID = scan.RC_PROGRAM_EXAMS_ID)
                         Where scan.CANDIDATE_ID = CanID And p.RC_PROGRAM_ID = ProID And exam.EXAMS_ORDER > EXAMODER).Count
            If query > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExist_Program_Schedule_Can")
            Throw ex
        End Try
    End Function
    Public Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO

        Try
            Dim query = From p In Context.RC_PROGRAM_SCHEDULE
                        From exam In Context.RC_PROGRAM_EXAMS.Where(Function(f) f.ID = p.RC_PROGRAM_EXAMS_ID)
                        Where p.ID = _filter.ID
                        Select New ProgramScheduleDTO With {
                                        .ID = p.ID,
                                        .RC_PROGRAM_EXAMS_ID = p.RC_PROGRAM_EXAMS_ID,
                                        .SCHEDULE_DATE = p.SCHEDULE_DATE,
                                        .EXAMS_NAME = exam.NAME,
                                        .EXAMS_ORDER = exam.EXAMS_ORDER,
                                        .EXAMS_POINT_LADDER = exam.POINT_LADDER,
                                        .EXAMS_POINT_PASS = exam.POINT_PASS,
                                        .EXAMS_PLACE = p.EXAMS_PLACE,
                                        .NOTE = p.NOTE}

            Dim lst = query.FirstOrDefault
            Dim lstUsher = (From p In Context.RC_PROGRAM_SCHEDULE_USHER
                            From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                            Where p.RC_PROGRAM_SCHEDULE_ID = lst.ID
                            Select New ProgramScheduleUsherDTO With {
                                .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                .EMPLOYEE_CODE = emp.EMPLOYEE_CODE,
                                .EMPLOYEE_NAME = emp.FULLNAME_VN}).ToList

            lst.lstScheduleUsher = lstUsher
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgramScheduleByID")
            Throw ex
        End Try

    End Function

    Public Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        Try
            Dim query = From p In Context.RC_CANDIDATE
                        From pro In Context.RC_PROGRAM.Where(Function(f) f.ID = p.RC_PROGRAM_ID And (f.IS_PORTAL Is Nothing OrElse f.IS_PORTAL = 0))
                        Where p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID And p.STATUS_ID = "DUDK" _
                        And Not (From can In Context.RC_PROGRAM_SCHEDULE_CAN
                                 From sche In Context.RC_PROGRAM_SCHEDULE.Where(Function(f) f.ID = can.RC_PROGRAM_SCHEDULE_ID)
                                 Where sche.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID _
                                 And sche.ID = _filter.RC_PROGRAM_SCHEDULE_ID
                                 Select can.CANDIDATE_ID).Contains(p.ID)
                        Order By p.CANDIDATE_CODE
                        Select New ProgramScheduleCanDTO With {
                            .CANDIDATE_ID = p.ID,
                            .CANDIDATE_CODE = p.CANDIDATE_CODE,
                            .CANDIDATE_NAME = p.FULLNAME_VN}

            Dim lst = query

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateNotScheduleByProgramID")
            Throw ex
        End Try

    End Function

    Public Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        Try
            Dim query = From p In Context.RC_PROGRAM_SCHEDULE_CAN
                        From can In Context.RC_CANDIDATE.Where(Function(f) f.ID = p.CANDIDATE_ID)
                        From can_cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = can.ID).DefaultIfEmpty
                        From ps In Context.RC_PROGRAM_SCHEDULE.Where(Function(f) f.ID = p.RC_PROGRAM_SCHEDULE_ID).DefaultIfEmpty
                        From us In Context.RC_PROGRAM_SCHEDULE_USHER.Where(Function(f) f.RC_PROGRAM_SCHEDULE_ID = ps.ID).DefaultIfEmpty
                        From hu In Context.HU_EMPLOYEE.Where(Function(f) f.ID = us.EMPLOYEE_ID).DefaultIfEmpty
                        Where p.RC_PROGRAM_SCHEDULE_ID = _filter.RC_PROGRAM_SCHEDULE_ID And can.STATUS_ID = "DATLICH"
                        Order By can.CANDIDATE_CODE
                        Select New ProgramScheduleCanDTO With {
                            .CANDIDATE_ID = can.ID,
                            .CANDIDATE_CODE = can.CANDIDATE_CODE,
                            .CANDIDATE_NAME = can.FULLNAME_VN,
                            .START_HOUR = p.START_HOUR,
                            .END_HOUR = p.END_HOUR,
                            .PER_EMAIL = can_cv.PER_EMAIL,
                            .CC_EMPLOYEE = hu.HU_EMPLOYEE_CV.WORK_EMAIL}

            Dim lst = query

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCandidateScheduleByScheduleID")
            Throw ex
        End Try

    End Function

    Public Function UpdateProgramSchedule(ByVal objSchedules As ProgramScheduleDTO,
                                          Optional ByVal log As UserLog = Nothing) As Boolean
        Dim objProgramScheduleData As RC_PROGRAM_SCHEDULE
        Try
            objProgramScheduleData = (From p In Context.RC_PROGRAM_SCHEDULE Where p.ID = objSchedules.ID).FirstOrDefault
            If objProgramScheduleData IsNot Nothing Then
                objProgramScheduleData.RC_PROGRAM_EXAMS_ID = objSchedules.RC_PROGRAM_EXAMS_ID
                objProgramScheduleData.RC_PROGRAM_ID = objSchedules.RC_PROGRAM_ID
                objProgramScheduleData.SCHEDULE_DATE = objSchedules.SCHEDULE_DATE
                objProgramScheduleData.EXAMS_PLACE = objSchedules.EXAMS_PLACE
                objProgramScheduleData.NOTE = objSchedules.NOTE

                Dim lstDelScheduleCans = (From p In Context.RC_PROGRAM_SCHEDULE_CAN
                                          Where p.RC_PROGRAM_SCHEDULE_ID = objProgramScheduleData.ID).ToList
                'For Each item In lstDelScheduleCans
                '    Context.RC_PROGRAM_SCHEDULE_CAN.DeleteObject(item)
                'Next

                Dim lstDelScheduleUshers = (From p In Context.RC_PROGRAM_SCHEDULE_USHER
                                            Where p.RC_PROGRAM_SCHEDULE_ID = objProgramScheduleData.ID).ToList
                'For Each item In lstDelScheduleUshers
                '    Context.RC_PROGRAM_SCHEDULE_USHER.DeleteObject(item)
                'Next
            Else
                objProgramScheduleData = New RC_PROGRAM_SCHEDULE
                objProgramScheduleData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_SCHEDULE.EntitySet.Name)
                objProgramScheduleData.SCHEDULE_DATE = objSchedules.SCHEDULE_DATE
                objProgramScheduleData.RC_PROGRAM_EXAMS_ID = objSchedules.RC_PROGRAM_EXAMS_ID
                objProgramScheduleData.RC_PROGRAM_ID = objSchedules.RC_PROGRAM_ID
                objProgramScheduleData.EXAMS_PLACE = objSchedules.EXAMS_PLACE
                objProgramScheduleData.NOTE = objSchedules.NOTE
                Context.RC_PROGRAM_SCHEDULE.AddObject(objProgramScheduleData)
            End If

            If objSchedules.lstScheduleCan IsNot Nothing Then
                For Each item In objSchedules.lstScheduleCan
                    Dim objProgramEmpData As New RC_PROGRAM_SCHEDULE_CAN
                    objProgramEmpData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_SCHEDULE_CAN.EntitySet.Name)
                    objProgramEmpData.RC_PROGRAM_SCHEDULE_ID = objProgramScheduleData.ID
                    objProgramEmpData.CANDIDATE_ID = item.CANDIDATE_ID
                    objProgramEmpData.END_HOUR = item.END_HOUR
                    objProgramEmpData.START_HOUR = item.START_HOUR
                    Context.RC_PROGRAM_SCHEDULE_CAN.AddObject(objProgramEmpData)

                    'Dim objEmpData As New RC_CANDIDATE
                    'objEmpData = (From can In Context.RC_CANDIDATE Where can.ID = item.CANDIDATE_ID).FirstOrDefault
                    ''Context.RC_CANDIDATE.Attach(objEmpData)
                    'objEmpData.STATUS_ID = item.STATUS_ID
                Next
            End If

            If objSchedules.lstScheduleUsher IsNot Nothing Then
                For Each item In objSchedules.lstScheduleUsher
                    Dim objProgramUsheData As New RC_PROGRAM_SCHEDULE_USHER
                    objProgramUsheData.ID = Utilities.GetNextSequence(Context, Context.RC_PROGRAM_SCHEDULE_USHER.EntitySet.Name)
                    objProgramUsheData.RC_PROGRAM_SCHEDULE_ID = objProgramScheduleData.ID
                    objProgramUsheData.EMPLOYEE_ID = item.EMPLOYEE_ID
                    Context.RC_PROGRAM_SCHEDULE_USHER.AddObject(objProgramUsheData)
                Next
            End If

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateProgramSchedule")
            Throw ex
        End Try
    End Function

#End Region

#Region "ProgramResult"


    Public Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        Try
            Dim lstStatusID As New List(Of Decimal)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGTHIDAT_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.TUCHOI_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID)

            Dim query = From can In Context.RC_CANDIDATE
                        From can_cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = can.ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = can_cv.GENDER).DefaultIfEmpty
                        From p In (From sche In Context.RC_PROGRAM_SCHEDULE
                                   From sche_can In Context.RC_PROGRAM_SCHEDULE_CAN.Where(Function(f) f.RC_PROGRAM_SCHEDULE_ID = sche.ID)
                                   Where sche.RC_PROGRAM_EXAMS_ID = _filter.RC_PROGRAM_EXAMS_ID
                                   Select sche_can).Where(Function(f) f.CANDIDATE_ID = can.ID).DefaultIfEmpty
                        From sche In Context.RC_PROGRAM_SCHEDULE.Where(Function(f) f.ID = p.RC_PROGRAM_SCHEDULE_ID).DefaultIfEmpty
                        From exam In Context.RC_PROGRAM_EXAMS.Where(Function(f) f.ID = sche.RC_PROGRAM_EXAMS_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = can.STATUS_ID)
                        Where can.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID And _
                        lstStatusID.Contains(can.STATUS_ID)
                        Select New ProgramScheduleCanDTO With {
                            .ID = p.ID,
                            .ASSESSMENT_INFO = p.ASSESSMENT_INFO,
                            .CANDIDATE_CODE = can.CANDIDATE_CODE,
                            .CANDIDATE_ID = p.CANDIDATE_ID,
                            .CANDIDATE_NAME = can.FULLNAME_VN,
                            .COMMENT_INFO = p.COMMENT_INFO,
                            .SCHEDULE_DATE = sche.SCHEDULE_DATE,
                            .END_HOUR = p.END_HOUR,
                            .EXAMS_NAME = exam.NAME,
                            .EXAMS_ORDER = exam.EXAMS_ORDER,
                            .EXAMS_POINT_LADDER = exam.POINT_LADDER,
                            .EXAMS_POINT_PASS = exam.POINT_PASS,
                            .POINT_RESULT = p.POINT_RESULT,
                            .START_HOUR = p.START_HOUR,
                            .BIRTH_DATE = can_cv.BIRTH_DATE,
                            .MOBILE_PHONE = can_cv.MOBILE_PHONE,
                            .ID_NO = can_cv.ID_NO,
                            .GENDER_NAME = gender.NAME_VN,
                            .IS_PASS = p.IS_PASS,
                            .IS_PV = exam.IS_PV,
                            .STATUS_ID = can.STATUS_ID,
                            .STATUS_NAME = status.NAME_VN}


            Return query.ToList
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO),
                                   ByVal log As UserLog) As Boolean

        Try
            For Each obj In lst
                Dim objData = (From p In Context.RC_PROGRAM_SCHEDULE_CAN
                               Where p.ID = obj.ID).FirstOrDefault
                objData.POINT_RESULT = obj.POINT_RESULT
                objData.COMMENT_INFO = obj.COMMENT_INFO
                objData.ASSESSMENT_INFO = obj.ASSESSMENT_INFO
                If obj.IS_PV Then
                    objData.IS_PASS = obj.IS_PASS
                Else
                    Dim pass As Decimal = 0
                    Dim result As Decimal = 0
                    If obj.EXAMS_POINT_PASS IsNot Nothing Then
                        pass = obj.EXAMS_POINT_PASS
                    End If
                    If obj.POINT_RESULT IsNot Nothing Then
                        result = obj.POINT_RESULT
                    End If
                    objData.IS_PASS = (result >= pass)
                End If
            Next

            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region

#Region "TransferCandidateToHSNV"

    Public Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer,
                                   ByVal _filter As CandidateDTO,
                                   Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)
        Try
            Dim lstStatusID As New List(Of Decimal)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGTHIDAT_ID)
            lstStatusID.Add(RecruitmentCommon.RC_CANDIDATE_STATUS.TUCHOI_ID)

            Dim query = From p In Context.RC_CANDIDATE
                        From pc In Context.RC_PROGRAM_CANDIDATE.Where(Function(f) p.ID = f.CANDIDATE_ID)
                        From ot In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From cv In Context.RC_CANDIDATE_CV.Where(Function(f) p.ID = f.CANDIDATE_ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = p.STATUS_ID).DefaultIfEmpty
                        Where pc.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID And
                        lstStatusID.Contains(p.STATUS_ID)
                        Order By p.CREATED_DATE Descending

            Dim lst = query.Select(Function(p) New CandidateDTO With {
                         .CANDIDATE_CODE = p.p.CANDIDATE_CODE,
                         .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                         .ID = p.p.ID,
                         .FULLNAME_VN = p.p.FULLNAME_VN,
                         .ORG_ID = p.p.ORG_ID,
                         .ORG_NAME = p.org.NAME_VN,
                         .ORG_DESC = p.org.DESCRIPTION_PATH,
                         .TITLE_ID = p.ot.ID,
                         .TITLE_NAME_VN = p.ot.NAME_VN,
                         .BIRTH_DATE = p.cv.BIRTH_DATE,
                         .ID_NO = p.cv.ID_NO,
                         .ID_DATE = p.cv.ID_DATE,
                         .IS_BLACKLIST = p.p.IS_BLACKLIST,
                         .IS_PONTENTIAL = p.p.IS_PONTENTIAL,
                         .IS_REHIRE = p.p.IS_REHIRE,
                         .MODIFIED_DATE = p.p.MODIFIED_DATE,
                         .STATUS_ID = p.p.STATUS_ID,
                         .STATUS_NAME = p.status.NAME_VN})

            'If _filter.CANDIDATE_CODE <> "" Then
            '    lst = lst.Where(Function(p) p.CANDIDATE_CODE.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
            '                                    p.FULLNAME_VN.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0 Or _
            '                                       p.ID_NO.ToUpper().IndexOf(_filter.CANDIDATE_CODE.ToUpper) >= 0)
            'End If
            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.JOIN_DATE)
            End If

            If _filter.TITLE_ID <> 0 Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If

            If _filter.STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.STATUS_NAME.ToUpper().IndexOf(_filter.STATUS_NAME.ToUpper) >= 0)
            End If
            lst = lst.OrderBy(Sorts)

            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetListCandidateTransferPaging")
            Throw ex
        End Try
    End Function

    Public Function TransferCandidateToHSNV(ByVal canID As Decimal, ByVal log As UserLog,
                                            ByRef empCode As String) As Boolean

        Try
            ' 1. Hồ sơ nhân viên
            Dim emp = (From p In Context.RC_CANDIDATE
                       From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                       From edu In Context.RC_CANDIDATE_EDUCATION.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                       From his In Context.RC_CANDIDATE_HISTORY.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                       From other In Context.RC_CANDIDATE_OTHER_INFO.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                       Where p.ID = canID
                       Select New With {
                           .emp = New ProfileDAL.EmployeeDTO With {
                               .FIRST_NAME_VN = p.FIRST_NAME_VN,
                               .FULLNAME_VN = p.FULLNAME_VN,
                               .LAST_NAME_VN = p.LAST_NAME_VN,
                               .ORG_ID = p.ORG_ID,
                               .TITLE_ID = p.TITLE_ID},
                           .emp_cv = New ProfileDAL.EmployeeCVDTO With {
                               .BIRTH_DATE = cv.BIRTH_DATE,
                               .GENDER_NAME = cv.GENDER,
                               .ID_NO = cv.ID_NO,
                               .ID_DATE = cv.ID_DATE,
                               .MOBILE_PHONE = cv.MOBILE_PHONE,
                               .BIRTH_PLACE = cv.BIRTH_ADDRESS,
                               .WORK_EMAIL = cv.WORK_EMAIL,
                               .PIT_CODE = cv.PIT_CODE,
                               .IMAGE = cv.IMAGE}}).FirstOrDefault

            Dim rep As New ProfileDAL.ProfileRepository
            Dim gID As Decimal = 0
            emp.emp_cv.GENDER = 565
            If emp.emp_cv.GENDER_NAME = 1 Then
                emp.emp_cv.GENDER = 566
            End If
            rep.InsertEmployee(emp.emp, log, gID, empCode, GetCandidateImage(canID, ""), emp.emp_cv, New ProfileDAL.EmployeeEduDTO, New ProfileDAL.EmployeeHealthDTO)

            '2. Quan hệ thân nhân
            Dim family = (From p In Context.RC_CANDIDATE_FAMILY
                          Where p.CANDIDATE_ID = canID
                          Select New ProfileDAL.FamilyDTO With {
                               .EMPLOYEE_ID = gID,
                               .ID_NO = p.ID_NO,
                               .FULLNAME = p.FULLNAME,
                               .RELATION_ID = p.RELATION_ID,
                               .ADDRESS = p.ADDRESS,
                               .REMARK = p.REMARK}).ToList

            For Each item As ProfileDAL.FamilyDTO In family
                rep.InsertEmployeeFamily(item, log, 0)
            Next

            rep.Context.SaveChanges(log)
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".TransferCandidateToHSNV")
            Throw ex
        End Try

        Return True
    End Function

    Public Function TransferHSNVToCandidate(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal,
                                            ByVal log As UserLog) As Boolean

        Try
            ' 1. Hồ sơ nhân viên
            ' From other In Context.HU_EMPLOYEE_OTHER_INFO.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            Dim emp = (From p In Context.HU_EMPLOYEE
                       From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                       From edu In Context.HU_EMPLOYEE_EDUCATION.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                       Where p.ID = empID
                       Select New With {
                           .emp = New CandidateDTO With {
                               .FIRST_NAME_VN = p.FIRST_NAME_VN,
                               .FULLNAME_VN = p.FULLNAME_VN,
                               .LAST_NAME_VN = p.LAST_NAME_VN,
                               .ORG_ID = orgID,
                               .TITLE_ID = titleID,
                               .RC_PROGRAM_ID = programID,
                               .WORK_STATUS = p.WORK_STATUS},
                           .emp_cv = New CandidateCVDTO With {
                               .BIRTH_DATE = cv.BIRTH_DATE,
                               .GENDER = cv.GENDER,
                               .ID_NO = cv.ID_NO,
                               .ID_DATE = cv.ID_DATE,
                               .ID_PLACE = cv.ID_PLACE,
                               .NAV_ADDRESS = cv.NAV_ADDRESS,
                               .NAV_PROVINCE = cv.NAV_PROVINCE,
                               .NATIVE = cv.NATIVE,
                               .RELIGION = cv.RELIGION,
                               .PER_ADDRESS = cv.PER_ADDRESS,
                               .PER_PROVINCE = cv.PER_PROVINCE,
                               .MARITAL_STATUS = cv.MARITAL_STATUS,
                               .MOBILE_PHONE = cv.MOBILE_PHONE,
                               .WORK_EMAIl = cv.WORK_EMAIL,
                               .PIT_CODE = cv.PIT_CODE,
                               .IMAGE = cv.IMAGE,
                               .PER_EMAIL = cv.PER_EMAIL,
                               .VISA_DATE = cv.VISA_DATE,
                               .VISA_PLACE = cv.VISA_PLACE,
                               .WORK_PERMIT = cv.WORK_PERMIT}}).FirstOrDefault


            Dim rep As New ProfileDAL.ProfileRepository
            InsertCandidate(emp.emp, log, 0, "", rep.GetEmployeeImage(empID, ""), emp.emp_cv)

            Context.SaveChanges(log)
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".TransferCandidateToHSNV")
            Throw ex
        End Try

        Return True
    End Function

    Public Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                   ByRef _strEmpCode As String,
                                   ByVal _imageBinary As Byte(),
                                   ByVal objEmpCV As CandidateCVDTO) As Boolean

        Try
            '---------- 1.0 Thêm vào bảng RC_Candidate ----------
            Dim objEmpData As New RC_CANDIDATE
            Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.RC_CANDIDATE.EntitySet.Name)
            'SaveCandidate(fileID, 222)
            objEmpData.ID = fileID

            'Sinh mã ứng viên động
            Dim checkEMP As Integer = 0
            Dim empCodeDB As Decimal = 0
            Dim EMPCODE As String

            Using query As New DataAccess.NonQueryData
                Dim temp = query.ExecuteSQLScalar("select Candidate_CODE from RC_Candidate order by Candidate_CODE DESC", New Object)
                If temp IsNot Nothing Then
                    empCodeDB = Decimal.Parse(temp)
                End If
            End Using
            Do
                empCodeDB += 1
                EMPCODE = String.Format("{0}", Format(empCodeDB, "000000"))
                checkEMP = (From p In Context.RC_CANDIDATE Where p.CANDIDATE_CODE = EMPCODE Select p.ID).Count
            Loop Until checkEMP = 0
            _strEmpCode = EMPCODE
            objEmpData.ID = fileID
            objEmpData.CANDIDATE_CODE = _strEmpCode
            objEmpData.RC_PROGRAM_ID = objEmp.RC_PROGRAM_ID
            objEmpData.FIRST_NAME_VN = objEmp.FIRST_NAME_VN
            objEmpData.LAST_NAME_VN = objEmp.LAST_NAME_VN
            objEmpData.FULLNAME_VN = objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.JOIN_DATE = objEmp.JOIN_DATE
            objEmpData.EFFECT_DATE = objEmp.EFFECT_DATE
            objEmpData.TITLE_NAME = objEmp.TITLE_NAME
            objEmpData.WORK = objEmp.WORK
            objEmpData.FILE_NAME = objEmp.FILE_NAME
            If objEmp.FILE_SIZE IsNot Nothing Then
                FileInsert(objEmp.ID, pathCandidate, objEmp.FILE_SIZE)
            End If

            If objEmp.WORK_STATUS IsNot Nothing Then
                '10,11,12,13,14 - nghỉ việc
                If ",10,11,12,13,14,".Contains("," & objEmp.WORK_STATUS.Value.ToString & ",") Then
                    objEmpData.IS_REHIRE = True
                End If
            End If

            Context.RC_CANDIDATE.AddObject(objEmpData)

            '---------- 2.0 Thêm vào bảng RC_Candidate_CV ----------
            Dim objEmpCVData As New RC_CANDIDATE_CV
            objEmpCVData.CANDIDATE_ID = objEmpData.ID
            objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
            objEmpCVData.GENDER = objEmpCV.GENDER
            objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
            objEmpCVData.ID_NO = objEmpCV.ID_NO
            objEmpCVData.ID_DATE = objEmpCV.ID_DATE
            objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
            objEmpCVData.NATIVE = objEmpCV.NATIVE
            objEmpCVData.RELIGION = objEmpCV.RELIGION
            objEmpCVData.PASSPORT_ID = objEmpCV.PASSPORT_ID
            objEmpCVData.PASSPORT_DATE = objEmpCV.PASSPORT_DATE
            objEmpCVData.PASSPORT_PLACE = objEmpCV.PASSPORT_PLACE_NAME
            objEmpCVData.BIRTH_NATION = objEmpCV.BIRTH_NATION_ID
            objEmpCVData.BIRTH_PROVINCE = objEmpCV.BIRTH_PROVINCE
            objEmpCVData.NATIONALITY_ID = objEmpCV.NATIONALITY_ID
            objEmpCVData.NAV_NATION = objEmpCV.NAV_NATION_ID
            objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
            objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
            objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
            objEmpCVData.PER_NATION = objEmpCV.PER_NATION_ID
            objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT_ID
            objEmpCVData.CONTACT_ADDRESS = objEmpCV.CONTACT_ADDRESS
            objEmpCVData.CONTACT_PROVINCE = objEmpCV.CONTACT_PROVINCE
            objEmpCVData.CONTACT_NATION = objEmpCV.CONTACT_NATION_ID
            objEmpCVData.CONTACT_DISTRICT = objEmpCV.CONTACT_DISTRICT_ID
            objEmpCVData.PERTAXCODE = objEmpCV.PERTAXCODE
            objEmpCVData.EDUCATION_MAJORS = objEmpCV.EDUCATION_MAJORS
            objEmpCVData.EDUCATION_ID = objEmpCV.EDUCATION_ID
            objEmpCVData.ID_DATE_EXPIRATION = objEmpCV.ID_DATE_EXPIRATION
            objEmpCVData.IS_RESIDENT = objEmpCV.IS_RESIDENT
            objEmpCVData.CONTACT_ADDRESS_TEMP = objEmpCV.CONTACT_ADDRESS_TEMP
            objEmpCVData.CONTACT_NATION_TEMP = objEmpCV.CONTACT_NATION_TEMP
            objEmpCVData.CONTACT_PROVINCE_TEMP = objEmpCV.CONTACT_PROVINCE_TEMP
            objEmpCVData.CONTACT_DISTRICT_TEMP = objEmpCV.CONTACT_DISTRICT_TEMP
            objEmpCVData.CONTACT_MOBILE = objEmpCV.CONTACT_MOBILE
            objEmpCVData.CONTACT_PHONE = objEmpCV.CONTACT_PHONE
            objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
            objEmpCVData.PER_TAX_DATE = objEmpCV.PER_TAX_DATE
            objEmpCVData.PER_TAX_PLACE = objEmpCV.PER_TAX_PLACE
            objEmpCVData.PASSPORT_DATE_EXPIRATION = objEmpCV.PASSPORT_DATE_EXPIRATION
            objEmpCVData.VISA_NUMBER = objEmpCV.VISA_NUMBER
            objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
            objEmpCVData.VISA_DATE_EXPIRATION = objEmpCV.VISA_DATE_EXPIRATION
            objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
            objEmpCVData.VNAIRLINES_NUMBER = objEmpCV.VNAIRLINES_NUMBER
            objEmpCVData.VNAIRLINES_DATE = objEmpCV.VNAIRLINES_DATE
            objEmpCVData.VNAIRLINES_DATE_EXPIRATION = objEmpCV.VNAIRLINES_DATE_EXPIRATION
            objEmpCVData.VNAIRLINES_PLACE = objEmpCV.VNAIRLINES_PLACE
            objEmpCVData.LABOUR_NUMBER = objEmpCV.LABOUR_NUMBER
            objEmpCVData.LABOUR_DATE = objEmpCV.LABOUR_DATE
            objEmpCVData.LABOUR_DATE_EXPIRATION = objEmpCV.LABOUR_DATE_EXPIRATION
            objEmpCVData.LABOUR_PLACE = objEmpCV.LABOUR_PLACE
            objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
            objEmpCVData.WORK_PERMIT_START = objEmpCV.WORK_PERMIT_START
            objEmpCVData.WORK_PERMIT_END = objEmpCV.WORK_PERMIT_END
            objEmpCVData.TEMP_RESIDENCE_CARD = objEmpCV.TEMP_RESIDENCE_CARD
            objEmpCVData.TEMP_RESIDENCE_CARD_START = objEmpCV.TEMP_RESIDENCE_CARD_START
            objEmpCVData.TEMP_RESIDENCE_CARD_END = objEmpCV.TEMP_RESIDENCE_CARD_END

            If objEmpCV.IMAGE <> "" Then
                objEmpCVData.IMAGE = objEmpData.CANDIDATE_CODE & objEmpCV.IMAGE
            End If
            Context.RC_CANDIDATE_CV.AddObject(objEmpCVData)
            Context.SaveChanges(log)
            gID = objEmpData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertCandidate")
            Throw ex
        End Try

    End Function

#End Region


#Region "Cost"

    Public Function GetCost(ByVal _filter As CostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO)

        Try
            Dim query = From p In Context.RC_COST
                        From pro In Context.RC_PROGRAM.Where(Function(f) f.ID = p.RC_PROGRAM_ID And (f.IS_PORTAL Is Nothing OrElse f.IS_PORTAL = 0))
                        Where pro.ORG_ID = _filter.ORG_ID
                        Select New CostDTO With {
                            .ID = p.ID,
                            .RC_PROGRAM_ID = p.RC_PROGRAM_ID,
                            .RC_PROGRAM_NAME = pro.CODE & " - " & pro.JOB_NAME,
                            .COST_ACTUAL = p.COST_ACTUAL,
                            .COST_DESCRIPTION = p.COST_DESCRIPTION,
                            .COST_EXPECTED = p.COST_EXPECTED,
                            .RC_FORM_NAMES = p.RC_FORM_NAMES,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.RC_PROGRAM_NAME <> "" Then
                lst = lst.Where(Function(p) p.RC_PROGRAM_NAME.ToUpper.Contains(_filter.RC_PROGRAM_NAME.ToUpper))
            End If

            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            If _filter.COST_DESCRIPTION <> "" Then
                lst = lst.Where(Function(p) p.COST_DESCRIPTION.ToUpper.Contains(_filter.COST_DESCRIPTION.ToUpper))
            End If

            If _filter.COST_ACTUAL IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_ACTUAL = _filter.COST_ACTUAL)
            End If

            If _filter.COST_EXPECTED IsNot Nothing Then
                lst = lst.Where(Function(p) p.COST_EXPECTED = _filter.COST_EXPECTED)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Dim lstResult = lst.ToList
            For Each item In lstResult
                Dim lstForm = (From p In Context.RC_COST_FORM
                               Where p.RC_COST_ID = item.ID
                               Select p.RC_FORM_ID.Value).ToList
                If lstForm.Count > 0 Then
                    item.RC_FORM_IDS = lstForm.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
                End If
            Next
            Return lstResult
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetCost")
            Throw ex
        End Try

    End Function

    Public Function UpdateCost(ByVal objExams As CostDTO, ByVal log As UserLog) As Boolean
        Dim objExamsData As RC_COST
        Try
            objExamsData = (From p In Context.RC_COST
                            Where p.RC_PROGRAM_ID = objExams.RC_PROGRAM_ID).FirstOrDefault
            If objExamsData Is Nothing Then
                objExamsData = New RC_COST
                objExamsData.ID = Utilities.GetNextSequence(Context, Context.RC_COST.EntitySet.Name)
                objExamsData.RC_PROGRAM_ID = objExams.RC_PROGRAM_ID
                objExamsData.COST_DESCRIPTION = objExams.COST_DESCRIPTION
                objExamsData.COST_EXPECTED = objExams.COST_EXPECTED
                objExamsData.COST_ACTUAL = objExams.COST_ACTUAL
                objExamsData.REMARK = objExams.REMARK
                objExamsData.RC_FORM_NAMES = objExams.RC_FORM_NAMES
                Context.RC_COST.AddObject(objExamsData)

            Else
                objExamsData.RC_PROGRAM_ID = objExams.RC_PROGRAM_ID
                objExamsData.COST_DESCRIPTION = objExams.COST_DESCRIPTION
                objExamsData.COST_EXPECTED = objExams.COST_EXPECTED
                objExamsData.COST_ACTUAL = objExams.COST_ACTUAL
                objExamsData.RC_FORM_NAMES = objExams.RC_FORM_NAMES
                objExamsData.REMARK = objExams.REMARK


                Dim oldForms = From i In Context.RC_COST_FORM Where i.RC_COST_ID = objExamsData.ID
                For Each form In oldForms
                    Context.RC_COST_FORM.DeleteObject(form)
                Next
            End If

            For Each form In objExams.RC_FORMs
                Context.RC_COST_FORM.AddObject(New RC_COST_FORM With {
                                               .ID = Utilities.GetNextSequence(Context, Context.RC_COST_FORM.EntitySet.Name),
                                               .RC_COST_ID = objExamsData.ID,
                                               .RC_FORM_ID = form.RC_FORM_ID})
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".UpdateCost")
            Throw ex
        End Try
    End Function


    Public Function ValidateCost(ByVal _validate As CostDTO)
        Dim query
        Try
            If _validate.RC_PROGRAM_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.RC_COST
                             Where p.RC_PROGRAM_ID = _validate.RC_PROGRAM_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.RC_COST
                             Where p.RC_PROGRAM_ID = _validate.RC_PROGRAM_ID).FirstOrDefault
                End If

                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateCost")
            Throw ex
        End Try
    End Function


    Public Function DeleteCost(ByVal obj As CostDTO) As Boolean
        Dim objDel As RC_COST
        Try
            objDel = (From p In Context.RC_COST Where obj.ID = p.ID).FirstOrDefault
            Context.RC_COST.DeleteObject(objDel)
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCost")
            Throw ex
        End Try

    End Function

#End Region

#Region "CV Pool"
    Public Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of CVPoolEmpDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = 1,
                                           .P_ISDISSOLVE = True})
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_NULL_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = 0,
                                           .P_ISDISSOLVE = True})
            End Using
            Dim lstID_Access = New List(Of Decimal?)
            Dim check As String = "X"
            Dim uncheck As String = ""
            lstID_Access = (From p In Context.RC_USER_TITLE_PERMISION
                            From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID).DefaultIfEmpty
                            Where user.USERNAME.ToUpper = log.Username.ToUpper
                            Select p.GROUP_TITLE_ID).Distinct.ToList
            Dim query = From p In Context.RC_CANDIDATE
                        From ep In Context.RC_CANDIDATE_EXPECT.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                        From po1 In Context.HU_TITLE.Where(Function(f) f.ID = ep.DESIRE_POSITION_1).DefaultIfEmpty
                        From po2 In Context.HU_TITLE.Where(Function(f) f.ID = ep.DESIRE_POSITION_2).DefaultIfEmpty
                        From cv In Context.RC_CANDIDATE_CV.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                        From ed In Context.RC_CANDIDATE_EDUCATION.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                        From hea In Context.RC_CANDIDATE_HEALTH.Where(Function(f) f.CANDIDATE_ID = p.ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER).DefaultIfEmpty
                        From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.MARITAL_STATUS).DefaultIfEmpty
                        From country In Context.HU_NATION.Where(Function(f) f.ID = cv.PER_NATION).DefaultIfEmpty
                        From provin In Context.HU_PROVINCE.Where(Function(f) f.ID = cv.BIRTH_PROVINCE).DefaultIfEmpty
                        From acede In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.ACADEMY).DefaultIfEmpty
                        From lear In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.LEARNING_LEVEL).DefaultIfEmpty
                        From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.MAJOR).DefaultIfEmpty
                        From deg In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.DEGREE).DefaultIfEmpty
                        From mark In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.MARK_EDU).DefaultIfEmpty
                        From comp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.COMPUTER_LEVEL).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ed.LANGUAGE_LEVEL).DefaultIfEmpty
                        From loaisk In Context.OT_OTHER_LIST.Where(Function(f) f.ID = hea.LOAI_SUC_KHOE).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.CODE = p.STATUS_ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From orgv In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = org.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From rc_source In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RC_SOURCE_REC_ID).DefaultIfEmpty
                        Select New CVPoolEmpDTO With {
                            .CANDIDATE_ID = p.ID,
                            .POS_1 = po1.NAME_VN,
                            .POS_2 = po2.NAME_VN,
                            .SAL_MM = ep.PROBATIONARY_SALARY,
                            .CANDIDATE_CODE = p.CANDIDATE_CODE,
                            .FULL_NAME_VN = p.FULLNAME_VN,
                            .GENDER = cv.GENDER,
                            .GENDER_NAME = gender.NAME_VN,
                            .MARITAL_STATUS = cv.MARITAL_STATUS,
                            .MARITAL_STATUS_NAME = marital.NAME_VN,
                            .PER_COUNTRY = cv.PER_NATION,
                            .PER_COUNTRY_NAME = country.NAME_VN,
                            .BIRTH_PLACE = cv.BIRTH_PROVINCE,
                            .BIRTH_PLACE_NAME = provin.NAME_VN,
                            .BIRTH_DATE = cv.BIRTH_DATE,
                            .NATIONALITY = cv.PER_NATION,
                            .NATIONALITY_NAME = country.NAME_VN,
                            .PER_PROVINCE = cv.PER_PROVINCE,
                            .PER_PROVINCE_NAME = provin.NAME_VN,
                            .ACADEMY = ed.ACADEMY,
                            .ACADEMY_NAME = acede.NAME_VN,
                            .LEARNING_LEVEL = ed.LEARNING_LEVEL,
                            .MAJOR = ed.MAJOR,
                            .MAJOR_NAME = major.NAME_VN,
                            .GRADUATE_SCHOOL = ed.SCHOOL,
                            .GRADUATE_SCHOOL_NAME = ed.SCHOOL_NAME,
                            .DEGREE = ed.DEGREE,
                            .DEGREE_NAME = deg.NAME_VN,
                            .MARK_EDU = ed.MARK_EDU,
                            .MARK_EDU_NAME = mark.NAME_VN,
                            .COMPUTER_RANK = ed.COMPUTER_LEVEL,
                            .COMPUTER_RANK_NAME = comp.NAME_VN,
                            .LANGUAGE = ed.LANGUAGE_LEVEL,
                            .LANGUAGE_NAME = lang.NAME_VN,
                            .CHIEU_CAO = hea.CHIEU_CAO,
                            .CAN_NANG = hea.CAN_NANG,
                            .LOAI_SUC_KHOE = hea.LOAI_SUC_KHOE,
                            .LOAI_SUC_KHOE_NAME = loaisk.NAME_VN,
                            .CANDIDATE_STATUS = status.CODE,
                            .CANDIDATE_STATUS_NAME = status.NAME_VN,
                            .CREATED_DATE = p.CREATED_DATE,
                            .ORG_NAME = orgv.NAME_C2,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .STATUS_ID = p.STATUS_ID,
                            .TITLE_NAME_VN = t.NAME_VN,
                            .TITLE_GROUP_ID = po1.TITLE_GROUP_ID,
                            .RC_SOURCE_REC_ID = p.RC_SOURCE_REC_ID,
                            .RC_SOURCE_REC_NAME = rc_source.NAME_VN,
                            .WORK_PERMIT = cv.WORK_PERMIT,
                            .IS_WORK_PERMIT = cv.IS_WORK_PERMIT,
                            .WORK_PERMIT_NAME = If(cv.IS_WORK_PERMIT = -1, check, uncheck),
                            .WORK_PERMIT_END = cv.WORK_PERMIT_END,
                            .WORK_PERMIT_START = cv.WORK_PERMIT_START}

            Dim lst = query

            If (_filter.CANDIDATE_CODE <> "") Then
                lst = lst.Where(Function(p) p.CANDIDATE_CODE.ToUpper.Contains(_filter.CANDIDATE_CODE.ToUpper))
            End If
            If (_filter.FULL_NAME_VN <> "") Then
                lst = lst.Where(Function(p) p.FULL_NAME_VN.ToUpper.Contains(_filter.FULL_NAME_VN.ToUpper))
            End If
            If _filter.GENDER IsNot Nothing Then
                lst = lst.Where(Function(p) p.GENDER = _filter.GENDER)
            End If
            If _filter.MARITAL_STATUS IsNot Nothing Then
                lst = lst.Where(Function(p) p.MARITAL_STATUS = _filter.MARITAL_STATUS)
            End If
            If _filter.PER_COUNTRY IsNot Nothing Then
                lst = lst.Where(Function(p) p.PER_COUNTRY = _filter.PER_COUNTRY)
            End If
            If _filter.BIRTH_PLACE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_PLACE = _filter.BIRTH_PLACE)
            End If
            If _filter.BIRTH_FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE >= _filter.BIRTH_FROM_DATE)
            End If
            If _filter.BIRTH_TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE <= _filter.BIRTH_TO_DATE)
            End If
            If _filter.NATIONALITY IsNot Nothing Then
                lst = lst.Where(Function(p) p.NATIONALITY = _filter.NATIONALITY)
            End If
            If _filter.PER_PROVINCE IsNot Nothing Then
                lst = lst.Where(Function(p) p.PER_PROVINCE = _filter.PER_PROVINCE)
            End If
            If _filter.ACADEMY IsNot Nothing Then
                lst = lst.Where(Function(p) p.ACADEMY = _filter.ACADEMY)
            End If
            If _filter.LEARNING_LEVEL IsNot Nothing Then
                lst = lst.Where(Function(p) p.LEARNING_LEVEL = _filter.LEARNING_LEVEL)
            End If
            If _filter.MAJOR IsNot Nothing Then
                lst = lst.Where(Function(p) p.MAJOR = _filter.MAJOR)
            End If
            If _filter.GRADUATE_SCHOOL IsNot Nothing Then
                lst = lst.Where(Function(p) p.GRADUATE_SCHOOL = _filter.GRADUATE_SCHOOL)
            End If
            If _filter.DEGREE IsNot Nothing Then
                lst = lst.Where(Function(p) p.DEGREE = _filter.DEGREE)
            End If
            If _filter.MARK_EDU IsNot Nothing Then
                lst = lst.Where(Function(p) p.MARK_EDU = _filter.MARK_EDU)
            End If
            If _filter.COMPUTER_RANK IsNot Nothing Then
                lst = lst.Where(Function(p) p.COMPUTER_RANK = _filter.COMPUTER_RANK)
            End If
            If _filter.LANGUAGE IsNot Nothing Then
                lst = lst.Where(Function(p) p.LANGUAGE = _filter.LANGUAGE)
            End If
            If _filter.CHIEU_CAO_TU IsNot Nothing Then
                lst = lst.Where(Function(p) p.CHIEU_CAO >= _filter.CHIEU_CAO_TU)
            End If
            If _filter.CHIEU_CAO_DEN IsNot Nothing Then
                lst = lst.Where(Function(p) p.CHIEU_CAO <= _filter.CHIEU_CAO_DEN)
            End If
            If _filter.CAN_NANG_TU IsNot Nothing Then
                lst = lst.Where(Function(p) p.CAN_NANG >= _filter.CAN_NANG_TU)
            End If
            If _filter.CAN_NANG_DEN IsNot Nothing Then
                lst = lst.Where(Function(p) p.CAN_NANG <= _filter.CAN_NANG_DEN)
            End If
            If _filter.LOAI_SUC_KHOE IsNot Nothing Then
                lst = lst.Where(Function(p) p.LOAI_SUC_KHOE = _filter.LOAI_SUC_KHOE)
            End If
            If _filter.CANDIDATE_STATUS IsNot Nothing Then
                lst = lst.Where(Function(p) p.CANDIDATE_STATUS = _filter.CANDIDATE_STATUS)
            End If
            If _filter.MODIFIED_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.MODIFIED_DATE <= _filter.MODIFIED_DATE)
            End If
            If (_filter.ORG_NAME <> "") Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If (_filter.TITLE_NAME_VN <> "") Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper.Contains(_filter.TITLE_NAME_VN.ToUpper))
            End If
            If (_filter.CANDIDATE_STATUS_NAME <> "") Then
                lst = lst.Where(Function(p) p.CANDIDATE_STATUS_NAME.ToUpper.Contains(_filter.CANDIDATE_STATUS_NAME.ToUpper))
            End If
            If lstID_Access.Count > 0 AndAlso log.Username.ToUpper.ToUpper <> "ADMIN" Then
                lst = lst.Where(Function(f) (lstID_Access.Contains(f.TITLE_GROUP_ID)))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetProgram")
            Throw ex
        End Try

    End Function
#End Region

#Region "HR_PLANING"
    Public Function GetHRYearPlaning(ByVal _filter As HRYearPlaningDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRYearPlaningDTO)

        Try
            Dim query = From p In Context.RC_HR_YEAR_PLANING
                        Select New HRYearPlaningDTO With {
                                           .ID = p.ID,
                                           .CREATED_DATE = p.CREATED_DATE,
                                           .YEAR = p.YEAR,
                                           .EFFECT_DATE = p.EFFECT_DATE,
                                           .EXPIRE_DATE = p.EXPIRE_DATE,
                                           .VERSION = p.VERSION,
                                           .NOTE = p.NOTE,
                                           .DETAIL_LINK = ""}
            Dim lst = query
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE >= _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE <= _filter.EXPIRE_DATE)
            End If
            If Not String.IsNullOrEmpty(_filter.VERSION) Then
                lst = lst.Where(Function(p) p.VERSION.ToUpper.Contains(_filter.VERSION.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetHRYearPlaning")
            Throw ex
        End Try

    End Function

    Public Function InsertHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objHRYearPlaningData As New RC_HR_YEAR_PLANING
        Try
            objHRYearPlaningData.ID = Utilities.GetNextSequence(Context, Context.RC_HR_YEAR_PLANING.EntitySet.Name)
            objHRYearPlaningData.YEAR = objHRYearPlaning.YEAR
            objHRYearPlaningData.VERSION = objHRYearPlaning.VERSION
            objHRYearPlaningData.EFFECT_DATE = objHRYearPlaning.EFFECT_DATE
            objHRYearPlaningData.EXPIRE_DATE = objHRYearPlaning.EXPIRE_DATE
            objHRYearPlaningData.NOTE = objHRYearPlaning.NOTE
            Context.RC_HR_YEAR_PLANING.AddObject(objHRYearPlaningData)
            Context.SaveChanges(log)
            gID = objHRYearPlaningData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertHRYearPlaning")
            Throw ex
        End Try
    End Function

    Public Function CheckExistEffectDate_HRYearPlaning(ByVal Id As Decimal, ByVal EffectDate As Date) As Boolean
        Try
            Dim count As Integer = (From p In Context.RC_HR_YEAR_PLANING.Where(Function(f) f.EFFECT_DATE = EffectDate And f.ID <> Id)).Count
            If count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistEffectDate_HRYearPlaning")
            Throw ex
        End Try
    End Function

    Public Function ModifyHRYearPlaning(ByVal objHRYearPlaning As HRYearPlaningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objHRYearPlaningData As New RC_HR_YEAR_PLANING With {.ID = objHRYearPlaning.ID}
        Try
            Context.RC_HR_YEAR_PLANING.Attach(objHRYearPlaningData)
            objHRYearPlaningData.YEAR = objHRYearPlaning.YEAR
            objHRYearPlaningData.VERSION = objHRYearPlaning.VERSION
            objHRYearPlaningData.EFFECT_DATE = objHRYearPlaning.EFFECT_DATE
            objHRYearPlaningData.EXPIRE_DATE = objHRYearPlaning.EXPIRE_DATE
            objHRYearPlaningData.NOTE = objHRYearPlaning.NOTE
            Context.SaveChanges(log)
            gID = objHRYearPlaningData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyHRYearPlaning")
            Throw ex
        End Try
    End Function

    Public Function DeleteHRYearPlaning(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHRYearPlaning As List(Of RC_HR_YEAR_PLANING)
        Try
            lstHRYearPlaning = (From p In Context.RC_HR_YEAR_PLANING Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHRYearPlaning.Count - 1
                Context.RC_HR_YEAR_PLANING.DeleteObject(lstHRYearPlaning(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteHRYearPlaning")
            Throw ex
        End Try
    End Function

    Public Function ValidateHRYearPlaning(ByVal year As Decimal, ByVal effect_date As Date, ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_HR_YEAR_PLANING Where p.YEAR = year AndAlso p.EFFECT_DATE = effect_date _
                         AndAlso p.ID <> _id).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateHRYearPlaning")
            Throw ex
        End Try
    End Function

    Public Function CheckExistData(ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_HR_PLANING_DETAIL Where p.YEAR_PLAN_ID = _id).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistData")
            Throw ex
        End Try
    End Function

    Public Function GetHrYearPlaningByID(ByVal _id As Decimal) As HRYearPlaningDTO
        Try
            Dim obj = (From p In Context.RC_HR_YEAR_PLANING
                       Where p.ID = _id
                       Select New HRYearPlaningDTO With {
                           .ID = p.ID,
                           .YEAR = p.YEAR,
                           .VERSION = p.VERSION,
                           .EFFECT_DATE = p.EFFECT_DATE,
                           .EXPIRE_DATE = p.EXPIRE_DATE,
                           .NOTE = p.NOTE
                           }).FirstOrDefault
            Return obj
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetHrYearPlaningByID")
            Throw ex
        End Try
    End Function

#End Region

#Region "HR_PL_DETAIL"

    Public Function GetPlanDetail(ByVal _filter As HRPlaningDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HRPlaningDetailDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.RC_HR_PLANING_DETAIL
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where p.YEAR_PLAN_ID = _filter.YEAR_PLAN_ID
                        Select New HRPlaningDetailDTO With {
                                       .ID = p.ID,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = p.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .MONTH_1 = p.MONTH_1,
                                       .MONTH_10 = p.MONTH_10,
                                       .MONTH_11 = p.MONTH_11,
                                       .MONTH_12 = p.MONTH_12,
                                       .MONTH_2 = p.MONTH_2,
                                       .MONTH_3 = p.MONTH_3,
                                       .MONTH_4 = p.MONTH_4,
                                       .MONTH_5 = p.MONTH_5,
                                       .MONTH_6 = p.MONTH_6,
                                       .MONTH_7 = p.MONTH_7,
                                       .MONTH_8 = p.MONTH_8,
                                       .MONTH_9 = p.MONTH_9,
                                       .RANK_SAL = p.RANK_SAL,
                                       .YEAR_PLAN_ID = p.YEAR_PLAN_ID}

            Dim lst = query
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If IsNumeric(_filter.MONTH_1) Then
                lst = lst.Where(Function(p) p.MONTH_1 = _filter.MONTH_1)
            End If
            If IsNumeric(_filter.MONTH_2) Then
                lst = lst.Where(Function(p) p.MONTH_2 = _filter.MONTH_2)
            End If
            If IsNumeric(_filter.MONTH_3) Then
                lst = lst.Where(Function(p) p.MONTH_3 = _filter.MONTH_3)
            End If
            If IsNumeric(_filter.MONTH_4) Then
                lst = lst.Where(Function(p) p.MONTH_4 = _filter.MONTH_4)
            End If
            If IsNumeric(_filter.MONTH_5) Then
                lst = lst.Where(Function(p) p.MONTH_5 = _filter.MONTH_5)
            End If
            If IsNumeric(_filter.MONTH_6) Then
                lst = lst.Where(Function(p) p.MONTH_6 = _filter.MONTH_6)
            End If
            If IsNumeric(_filter.MONTH_7) Then
                lst = lst.Where(Function(p) p.MONTH_7 = _filter.MONTH_7)
            End If
            If IsNumeric(_filter.MONTH_8) Then
                lst = lst.Where(Function(p) p.MONTH_8 = _filter.MONTH_8)
            End If
            If IsNumeric(_filter.MONTH_9) Then
                lst = lst.Where(Function(p) p.MONTH_9 = _filter.MONTH_9)
            End If
            If IsNumeric(_filter.MONTH_10) Then
                lst = lst.Where(Function(p) p.MONTH_10 = _filter.MONTH_10)
            End If
            If IsNumeric(_filter.MONTH_11) Then
                lst = lst.Where(Function(p) p.MONTH_11 = _filter.MONTH_11)
            End If
            If IsNumeric(_filter.MONTH_12) Then
                lst = lst.Where(Function(p) p.MONTH_12 = _filter.MONTH_12)
            End If
            If IsNumeric(_filter.RANK_SAL) Then
                lst = lst.Where(Function(p) p.RANK_SAL = _filter.RANK_SAL)
            End If

            If IsNumeric(_filter.TITLE_ID) Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetPlanDetail")
            Throw ex
        End Try

    End Function

    Public Function InsertHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean
        Try
            For Each objHRPlanDetail In lstObj
                Dim objHRPlanDetailData As New RC_HR_PLANING_DETAIL
                objHRPlanDetailData.ID = Utilities.GetNextSequence(Context, Context.RC_HR_PLANING_DETAIL.EntitySet.Name)
                objHRPlanDetailData.ORG_ID = objHRPlanDetail.ORG_ID
                objHRPlanDetailData.TITLE_ID = objHRPlanDetail.TITLE_ID
                objHRPlanDetailData.MONTH_1 = objHRPlanDetail.MONTH_1
                objHRPlanDetailData.MONTH_2 = objHRPlanDetail.MONTH_2
                objHRPlanDetailData.MONTH_3 = objHRPlanDetail.MONTH_3
                objHRPlanDetailData.MONTH_4 = objHRPlanDetail.MONTH_4
                objHRPlanDetailData.MONTH_5 = objHRPlanDetail.MONTH_5
                objHRPlanDetailData.MONTH_6 = objHRPlanDetail.MONTH_6
                objHRPlanDetailData.MONTH_7 = objHRPlanDetail.MONTH_7
                objHRPlanDetailData.MONTH_8 = objHRPlanDetail.MONTH_8
                objHRPlanDetailData.MONTH_9 = objHRPlanDetail.MONTH_9
                objHRPlanDetailData.MONTH_10 = objHRPlanDetail.MONTH_10
                objHRPlanDetailData.MONTH_11 = objHRPlanDetail.MONTH_11
                objHRPlanDetailData.MONTH_12 = objHRPlanDetail.MONTH_12
                objHRPlanDetailData.RANK_SAL = objHRPlanDetail.RANK_SAL
                objHRPlanDetailData.YEAR_PLAN_ID = objHRPlanDetail.YEAR_PLAN_ID
                objHRPlanDetailData.CREATED_BY = log.Username
                objHRPlanDetailData.CREATED_DATE = DateTime.Now
                objHRPlanDetailData.CREATED_LOG = log.Username + " / " + log.Ip
                objHRPlanDetailData.MODIFIED_BY = log.Username
                objHRPlanDetailData.MODIFIED_DATE = DateTime.Now
                objHRPlanDetailData.MODIFIED_LOG = log.Username + " / " + log.Ip
                Context.RC_HR_PLANING_DETAIL.AddObject(objHRPlanDetailData)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertHRPlanDetail")
            Throw ex
        End Try
    End Function

    Public Function ModifyHRPlanDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean
        Try
            For Each objHRPlanDetail In lstObj
                Dim objHRPlanDetailData As New RC_HR_PLANING_DETAIL With {.ID = objHRPlanDetail.ID}
                Context.RC_HR_PLANING_DETAIL.Attach(objHRPlanDetailData)
                objHRPlanDetailData.ORG_ID = objHRPlanDetail.ORG_ID
                objHRPlanDetailData.TITLE_ID = objHRPlanDetail.TITLE_ID
                objHRPlanDetailData.MONTH_1 = objHRPlanDetail.MONTH_1
                objHRPlanDetailData.MONTH_2 = objHRPlanDetail.MONTH_2
                objHRPlanDetailData.MONTH_3 = objHRPlanDetail.MONTH_3
                objHRPlanDetailData.MONTH_4 = objHRPlanDetail.MONTH_4
                objHRPlanDetailData.MONTH_5 = objHRPlanDetail.MONTH_5
                objHRPlanDetailData.MONTH_6 = objHRPlanDetail.MONTH_6
                objHRPlanDetailData.MONTH_7 = objHRPlanDetail.MONTH_7
                objHRPlanDetailData.MONTH_8 = objHRPlanDetail.MONTH_8
                objHRPlanDetailData.MONTH_9 = objHRPlanDetail.MONTH_9
                objHRPlanDetailData.MONTH_10 = objHRPlanDetail.MONTH_10
                objHRPlanDetailData.MONTH_11 = objHRPlanDetail.MONTH_11
                objHRPlanDetailData.MONTH_12 = objHRPlanDetail.MONTH_12
                objHRPlanDetailData.RANK_SAL = objHRPlanDetail.RANK_SAL
                objHRPlanDetailData.YEAR_PLAN_ID = objHRPlanDetail.YEAR_PLAN_ID
                objHRPlanDetailData.MODIFIED_BY = log.Username
                objHRPlanDetailData.MODIFIED_DATE = DateTime.Now
                objHRPlanDetailData.MODIFIED_LOG = log.Username + " / " + log.Ip
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyHRPlanDetail")
            Throw ex
        End Try
    End Function

    Public Function DeleteHRPlanDetail(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHRPlanDetail As List(Of RC_HR_PLANING_DETAIL)
        Try
            lstHRPlanDetail = (From p In Context.RC_HR_PLANING_DETAIL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHRPlanDetail.Count - 1
                Context.RC_HR_PLANING_DETAIL.DeleteObject(lstHRPlanDetail(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteHRPlanDetail")
            Throw ex
        End Try
    End Function

    Public Function ValidateHRPlanDetail(ByVal ORG_ID As Decimal, ByVal TITLE_ID As Decimal, ByVal YEAR_PLAN_ID As Decimal, ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_HR_PLANING_DETAIL Where p.ORG_ID = ORG_ID AndAlso p.TITLE_ID = TITLE_ID _
                         AndAlso p.YEAR_PLAN_ID = YEAR_PLAN_ID AndAlso p.ID <> _id).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateHRPlanDetail")
            Throw ex
        End Try
    End Function

    Public Function CheckExistRankSal(ByVal _id As Decimal) As Boolean
        Try
            Dim check = (From p In Context.RC_HR_PLANING_DETAIL Where p.ID = _id AndAlso p.RANK_SAL IsNot Nothing).Count
            If check > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckExistRankSal")
            Throw ex
        End Try
    End Function


    Public Function GetDetailOrgTitle(ByVal _filter As HRPlaningDetailDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "TITLE_NAME", Optional ByVal log As UserLog = Nothing) As DataTable
        Try
            Dim WHERE_CONDITION As String = " AND 1=1 "

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                WHERE_CONDITION += " AND UPPER(TITLE_NAME) LIKE '%" + _filter.TITLE_NAME.ToUpper() + "%'"
            End If
            ''WHERE_CONDITION += " ORDER BY " + Sorts
            Using cls As New DataAccess.QueryData
                'PROCEDURE GET_DETAIL_ORG_TITLE(P_ORG_ID      IN NUMBER,
                '               P_EFFECT_DATE IN DATE,
                '               P_USERNAME    IN NVARCHAR2,
                '               P_ISDISSOLVE  IN NUMBER,
                '               P_WHERE       IN NVARCHAR2,
                '               P_CUR         OUT CURSOR_TYPE)
                Dim dsData As DataSet = cls.ExecuteStore("PKG_RECRUITMENT_NEW.GET_DETAIL_ORG_TITLE",
                                               New With {.P_ORG_ID = _param.ORG_ID,
                                                         .P_EFFECT_DATE = _filter.EFFECT_DATE,
                                                         .P_USERNAME = log.Username.ToUpper,
                                                         .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                         .P_WHERE = WHERE_CONDITION,
                                                         .P_CUR = cls.OUT_CURSOR}, False)
                Total = If(dsData IsNot Nothing, dsData.Tables(0).Rows.Count, 0)
                Return dsData.Tables(0)
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return New DataTable
        End Try
    End Function

    Public Function ModifyHRBudgetDetail(ByVal lstObj As List(Of HRPlaningDetailDTO), ByVal log As UserLog) As Boolean
        Try
            For Each objHRPlanDetail In lstObj
                Dim objHRPlanDetailData As New RC_HR_PLANING_DETAIL With {.ID = objHRPlanDetail.ID}
                Context.RC_HR_PLANING_DETAIL.Attach(objHRPlanDetailData)
                objHRPlanDetailData.RANK_SAL = objHRPlanDetail.RANK_SAL
                objHRPlanDetailData.MODIFIED_BY = log.Username
                objHRPlanDetailData.MODIFIED_DATE = DateTime.Now
                objHRPlanDetailData.MODIFIED_LOG = log.Username + " / " + log.Ip
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".ModifyHRBudgetDetail")
            Throw ex
        End Try
    End Function


#End Region
#Region "RC_CANDIDATE_CONTRACT"
    Public Function GetRCNegotiate(ByVal _filter As RCNegotiateDTO,
                                    ByVal _param As ParamDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", _
                                    Optional ByVal log As UserLog = Nothing) As List(Of RCNegotiateDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = 0})
            '    From k In Context.SE_CHOSEN_ORG.Where(Function(f) pr.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            'End Using

            Dim query = From p In Context.RC_CONTRACT_CANDIDATE
                        From e In Context.RC_CANDIDATE.Where(Function(f) f.ID = p.RC_CANDIDATE_ID)
                        From pr In Context.RC_PROGRAM.Where(Function(f) f.ID = p.RC_PROGRAM_ID And (f.IS_PORTAL Is Nothing OrElse f.IS_PORTAL = 0))
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = pr.ORG_ID)
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = pr.TITLE_ID)
                        From rec In Context.RC_REQUEST.Where(Function(f) f.ID = pr.RC_REQUEST_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) f.ID = p.SAL_TYPE_ID).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TAX_TABLE_ID).DefaultIfEmpty
                        From EmpType In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_TYPE_ID).DefaultIfEmpty
                        From ConType In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Select New RCNegotiateDTO With {
                                       .ID = p.ID,
                                       .RC_PROGRAM_ID = p.RC_PROGRAM_ID,
                                       .CODE_YCTD = rec.CODE_RC,
                                       .NAME_YCTD = rec.NAME_RC,
                                       .RC_CANDIDATE_ID = p.RC_CANDIDATE_ID,
                                       .ORG_ID = pr.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .ORG_DESC = org.DESCRIPTION_PATH,
                                       .TITLE_ID = pr.TITLE_ID,
                                       .TITLE_NAME = title.NAME_VN,
                                       .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                       .CONTRACT_TYPE_NAME = ConType.NAME_VN,
                                       .CONTRACT_FROMDATE = p.CONTRACT_FROMDATE,
                                       .CONTRACT_TODATE = p.CONTRACT_TODATE,
                                       .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                       .SAL_TYPE_NAME = sal_type.NAME,
                                       .EMPLOYEE_TYPE_ID = p.EMPLOYEE_TYPE_ID,
                                       .EMPLOYEE_TYPE_NAME = EmpType.NAME_VN,
                                       .OTHERSALARY1 = p.OTHERSALARY1,
                                       .PERCENT_SAL = p.PERCENT_SAL,
                                       .SALARY_PROBATION = p.SALARY_PROBATION,
                                       .SALARY_OFFICIAL = p.SALARY_OFFICIAL,
                                       .NOTE = p.NOTE,
                                       .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                       .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            If IsNumeric(_filter.ORG_ID) AndAlso _filter.ORG_ID > 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If
            If IsNumeric(_filter.TITLE_ID) AndAlso _filter.TITLE_ID > 0 Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            If IsNumeric(_filter.RC_PROGRAM_ID) AndAlso _filter.RC_PROGRAM_ID > 0 Then
                lst = lst.Where(Function(p) p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID)
            End If
            If IsNumeric(_filter.RC_CANDIDATE_ID) AndAlso _filter.RC_CANDIDATE_ID > 0 Then
                lst = lst.Where(Function(p) p.RC_CANDIDATE_ID = _filter.RC_CANDIDATE_ID)
            End If
            If IsNumeric(_filter.CONTRACT_TYPE_ID) AndAlso _filter.CONTRACT_TYPE_ID > 0 Then
                lst = lst.Where(Function(p) p.CONTRACT_TYPE_ID = _filter.CONTRACT_TYPE_ID)
            End If
            If IsDate(_filter.CONTRACT_FROMDATE) Then
                lst = lst.Where(Function(p) p.CONTRACT_FROMDATE = _filter.CONTRACT_FROMDATE)
            End If
            If IsDate(_filter.CONTRACT_TODATE) Then
                lst = lst.Where(Function(p) p.CONTRACT_TODATE = _filter.CONTRACT_TODATE)
            End If
            If IsNumeric(_filter.SAL_TYPE_ID) AndAlso _filter.SAL_TYPE_ID > 0 Then
                lst = lst.Where(Function(p) p.SAL_TYPE_ID = _filter.SAL_TYPE_ID)
            End If
            If IsNumeric(_filter.EMPLOYEE_TYPE_ID) AndAlso _filter.EMPLOYEE_TYPE_ID > 0 Then
                lst = lst.Where(Function(p) p.EMPLOYEE_TYPE_ID = _filter.EMPLOYEE_TYPE_ID)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetRCNegotiate")
            Throw ex
        End Try

    End Function
    Public Function InsertRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objRCNegotiateData As New RC_CONTRACT_CANDIDATE
        Try
            Dim objFind = (From p In Context.RC_CONTRACT_CANDIDATE Where p.RC_CANDIDATE_ID = objRCNegotiate.RC_CANDIDATE_ID And p.RC_PROGRAM_ID = objRCNegotiate.RC_PROGRAM_ID).FirstOrDefault
            If objFind IsNot Nothing Then
                Context.RC_CONTRACT_CANDIDATE.DeleteObject(objFind)
                Context.SaveChanges(log)
            End If
            objRCNegotiateData.ID = Utilities.GetNextSequence(Context, Context.RC_CONTRACT_CANDIDATE.EntitySet.Name)
            objRCNegotiateData.RC_PROGRAM_ID = objRCNegotiate.RC_PROGRAM_ID
            objRCNegotiateData.RC_CANDIDATE_ID = objRCNegotiate.RC_CANDIDATE_ID
            objRCNegotiateData.CONTRACT_TYPE_ID = objRCNegotiate.CONTRACT_TYPE_ID
            objRCNegotiateData.CONTRACT_FROMDATE = objRCNegotiate.CONTRACT_FROMDATE
            objRCNegotiateData.CONTRACT_TODATE = objRCNegotiate.CONTRACT_TODATE
            objRCNegotiateData.SALARY_OFFICIAL = objRCNegotiate.SALARY_OFFICIAL
            objRCNegotiateData.SALARY_PROBATION = objRCNegotiate.SALARY_PROBATION
            objRCNegotiateData.NOTE = objRCNegotiate.NOTE
            objRCNegotiateData.SAL_TYPE_ID = objRCNegotiate.SAL_TYPE_ID
            objRCNegotiateData.TAX_TABLE_ID = objRCNegotiate.TAX_TABLE_ID
            objRCNegotiateData.EMPLOYEE_TYPE_ID = objRCNegotiate.EMPLOYEE_TYPE_ID
            objRCNegotiateData.OTHERSALARY1 = objRCNegotiate.OTHERSALARY1
            objRCNegotiateData.PERCENT_SAL = objRCNegotiate.PERCENT_SAL
            Context.RC_CONTRACT_CANDIDATE.AddObject(objRCNegotiateData)
            Context.SaveChanges(log)
            gID = objRCNegotiateData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRCNegotiate")
            Throw ex
        End Try
    End Function

    Public Function InsertRcSalaryCandidate(ByVal obj As RC_Salary_CandidateDTO) As Boolean
        Dim objData As New RC_SALARY_CANDIDATE
        Try
            Dim objFind = (From p In Context.RC_SALARY_CANDIDATE Where p.RC_CANDIDATE_ID = obj.RC_CANDIDATE_ID And p.RC_PROGRAM_ID = obj.RC_PROGRAM_ID).FirstOrDefault
            If objFind IsNot Nothing Then
                Context.RC_SALARY_CANDIDATE.DeleteObject(objFind)
                Context.SaveChanges()
            End If
            objData.ID = Utilities.GetNextSequence(Context, Context.RC_SALARY_CANDIDATE.EntitySet.Name)
            objData.RC_PROGRAM_ID = obj.RC_PROGRAM_ID
            objData.RC_CANDIDATE_ID = obj.RC_CANDIDATE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EMPLOYEE_TYPE_ID = obj.EMPLOYEE_TYPE_ID
            objData.OTHERSALARY1 = obj.OTHERSALARY1
            objData.PERCENT_SAL = obj.PERCENT_SAL
            objData.SAL_TYPE_ID = obj.SAL_TYPE_ID
            objData.SALARY_OFFICIAL = obj.SALARY_OFFICIAL
            objData.SALARY_PROBATION = obj.SALARY_PROBATION
            objData.TAX_TABLE_ID = obj.TAX_TABLE_ID
            Context.RC_SALARY_CANDIDATE.AddObject(objData)

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRcSalaryCandidate")
            Throw ex
        End Try
    End Function

    Public Function InsertRcAlowCandidate(ByVal obj As List(Of RC_Allowance_CandidateDTO)) As Boolean

        Try
            Dim RC_PROGRAM_ID As Decimal = obj(0).RC_PROGRAM_ID
            Dim RC_CANDIDATE_ID As Decimal = obj(0).RC_CANDIDATE_ID

            Dim lstRegEmp = (From p In Context.RC_ALLOWANCE_CANDIDATE
                             Where p.RC_CANDIDATE_ID = RC_CANDIDATE_ID And p.RC_PROGRAM_ID = RC_PROGRAM_ID).ToList
            For Each item In lstRegEmp
                Context.RC_ALLOWANCE_CANDIDATE.DeleteObject(item)
            Next
            Context.SaveChanges()

            For Each items In obj
                Dim objData As New RC_ALLOWANCE_CANDIDATE
                objData.ID = Utilities.GetNextSequence(Context, Context.RC_ALLOWANCE_CANDIDATE.EntitySet.Name)
                objData.RC_PROGRAM_ID = items.RC_PROGRAM_ID
                objData.RC_CANDIDATE_ID = items.RC_CANDIDATE_ID
                objData.ALLOWANCE_ID = items.ALLOWANCE_ID
                objData.EFFECT_FROM = items.EFFECT_FROM
                objData.EFFECT_TO = items.EFFECT_TO
                objData.ALLOWANCE_UNIT = items.ALLOWANCE_UNIT_ID
                objData.MONEY = items.MONEY
                Context.RC_ALLOWANCE_CANDIDATE.AddObject(objData)
            Next

            Context.SaveChanges()

            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".InsertRcSalaryCandidate")
            Throw ex
        End Try
    End Function


    Public Function DeleteRCNegotiate(ByVal objRCNegotiate As RCNegotiateDTO, ByVal log As UserLog) As Decimal
        Try

            Dim objFind = (From p In Context.RC_CONTRACT_CANDIDATE Where p.RC_CANDIDATE_ID = objRCNegotiate.RC_CANDIDATE_ID And p.RC_PROGRAM_ID = objRCNegotiate.RC_PROGRAM_ID).FirstOrDefault
            If objFind Is Nothing Then
                Return 0
            Else
                Context.RC_CONTRACT_CANDIDATE.DeleteObject(objFind)
                Context.SaveChanges(log)
                Return 1
            End If
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteRCNegotiate")
            Throw ex
            Return 2
        End Try
    End Function
#End Region
#Region "RC_TRANSFERCAN_TOEMPLOYEE"
    Public Function GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID(ByVal _filter As RC_TransferCAN_ToEmployeeDTO) As RC_TransferCAN_ToEmployeeDTO

        Try
            Dim query = From p In Context.RC_TRANSFERCAN_TOEMPLOYEE
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                        From req In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.DIRECT_MANAGER_ID).DefaultIfEmpty
                        Where p.RC_PROGRAM_ID = _filter.RC_PROGRAM_ID And p.CANDIDATE_ID = _filter.CANDIDATE_ID
                        Select New RC_TransferCAN_ToEmployeeDTO With {
                                           .ID = p.ID,
                                           .RC_PROGRAM_ID = p.RC_PROGRAM_ID,
                                           .CANDIDATE_ID = p.CANDIDATE_ID,
                                           .ORG_ID = p.ORG_ID,
                                           .ORG_NAME = org.NAME_VN,
                                           .ORG_DESC = org.DESCRIPTION_PATH,
                                           .TITLE_ID = p.TITLE_ID,
                                           .DIRECT_MANAGER_ID = p.DIRECT_MANAGER_ID,
                                           .DIRECT_MANAGER_NAME = req.EMPLOYEE_CODE + " - " + req.FULLNAME_VN,
                                           .OBJECT_LABOR_ID = p.OBJECT_LABOR_ID,
                                           .OBJECT_ATTENDANCE_ID = p.OBJECT_ATTENDANCE_ID,
                                           .OBJECT_EMPLOYEE_ID = p.OBJECT_EMPLOYEE_ID,
                                           .OBJECT_ATTENDANT_ID = p.OBJECT_ATTENDANT_ID,
                                           .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                           .CONTRACT_TYPE_ID = p.CONTRACT_TYPE_ID,
                                           .CONTRACT_FROM = p.CONTRACT_FROM,
                                           .CONTRACT_TO = p.CONTRACT_TO,
                                           .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                           .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                           .EMPLOYEE_TYPE_ID = p.EMPLOYEE_TYPE_ID,
                                           .SAL_BASIC = p.SAL_BASIC,
                                           .OTHERSALARY1 = p.OTHERSALARY1,
                                           .PERCENT_SAL = p.PERCENT_SAL,
                                           .SAL_TOTAL = p.SAL_TOTAL,
                                           .TRANSFER_HSL_HD = p.TRANSFER_HSL_HD,
                                           .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                           .STATUS = p.STATUS}

            Dim lst = query.FirstOrDefault
            Return lst
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GET_RC_TRANSFERCAN_TOEMPLOYEE_BYID")
            Throw ex
        End Try
    End Function

    Public Function Insert_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean
        'Dim obj As New RC_TRANSFERCAN_TOEMPLOYEE
        Try

            Dim objData As New RC_TRANSFERCAN_TOEMPLOYEE
            objData.ID = Utilities.GetNextSequence(Context, Context.RC_TRANSFERCAN_TOEMPLOYEE.EntitySet.Name)
            objData.RC_PROGRAM_ID = objDTO.RC_PROGRAM_ID
            objData.CANDIDATE_ID = objDTO.CANDIDATE_ID
            objData.ORG_ID = objDTO.ORG_ID
            objData.TITLE_ID = objDTO.TITLE_ID
            objData.DIRECT_MANAGER_ID = objDTO.DIRECT_MANAGER_ID
            objData.OBJECT_LABOR_ID = objDTO.OBJECT_LABOR_ID
            objData.OBJECT_ATTENDANCE_ID = objDTO.OBJECT_ATTENDANCE_ID
            objData.OBJECT_EMPLOYEE_ID = objDTO.OBJECT_EMPLOYEE_ID
            objData.OBJECT_ATTENDANT_ID = objDTO.OBJECT_ATTENDANT_ID
            objData.WORK_PLACE_ID = objDTO.WORK_PLACE_ID
            objData.CONTRACT_TYPE_ID = objDTO.CONTRACT_TYPE_ID
            objData.CONTRACT_FROM = objDTO.CONTRACT_FROM
            objData.CONTRACT_TO = objDTO.CONTRACT_TO
            objData.SAL_TYPE_ID = objDTO.SAL_TYPE_ID
            objData.TAX_TABLE_ID = objDTO.TAX_TABLE_ID
            objData.EMPLOYEE_TYPE_ID = objDTO.EMPLOYEE_TYPE_ID
            objData.SAL_BASIC = objDTO.SAL_BASIC
            objData.OTHERSALARY1 = objDTO.OTHERSALARY1
            objData.PERCENT_SAL = objDTO.PERCENT_SAL
            objData.SAL_TOTAL = objDTO.SAL_TOTAL
            objData.TRANSFER_HSL_HD = objDTO.TRANSFER_HSL_HD

            objData.EMPLOYEE_CODE = objDTO.EMPLOYEE_CODE
            objData.STATUS = objDTO.STATUS
            Context.RC_TRANSFERCAN_TOEMPLOYEE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True

        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".Insert_RC_TRANSFERCAN_TOEMPLOYEE")
            Throw ex
        End Try
    End Function

    Public Function Modify_RC_TRANSFERCAN_TOEMPLOYEE(ByVal objDTO As RC_TransferCAN_ToEmployeeDTO, ByVal log As UserLog,
                                         ByRef gID As Decimal) As Boolean

        Dim objData As New RC_TRANSFERCAN_TOEMPLOYEE With {.ID = objDTO.ID}
        Try
            Context.RC_TRANSFERCAN_TOEMPLOYEE.Attach(objData)
            objData.RC_PROGRAM_ID = objDTO.RC_PROGRAM_ID
            objData.CANDIDATE_ID = objDTO.CANDIDATE_ID
            objData.ORG_ID = objDTO.ORG_ID
            objData.TITLE_ID = objDTO.TITLE_ID
            objData.DIRECT_MANAGER_ID = objDTO.DIRECT_MANAGER_ID
            objData.OBJECT_LABOR_ID = objDTO.OBJECT_LABOR_ID
            objData.OBJECT_ATTENDANCE_ID = objDTO.OBJECT_ATTENDANCE_ID
            objData.OBJECT_EMPLOYEE_ID = objDTO.OBJECT_EMPLOYEE_ID
            objData.OBJECT_ATTENDANT_ID = objDTO.OBJECT_ATTENDANT_ID
            objData.WORK_PLACE_ID = objDTO.WORK_PLACE_ID
            objData.CONTRACT_TYPE_ID = objDTO.CONTRACT_TYPE_ID
            objData.CONTRACT_FROM = objDTO.CONTRACT_FROM
            objData.CONTRACT_TO = objDTO.CONTRACT_TO
            objData.SAL_TYPE_ID = objDTO.SAL_TYPE_ID
            objData.TAX_TABLE_ID = objDTO.TAX_TABLE_ID
            objData.EMPLOYEE_TYPE_ID = objDTO.EMPLOYEE_TYPE_ID
            objData.SAL_BASIC = objDTO.SAL_BASIC
            objData.OTHERSALARY1 = objDTO.OTHERSALARY1
            objData.PERCENT_SAL = objDTO.PERCENT_SAL
            objData.SAL_TOTAL = objDTO.SAL_TOTAL
            objData.TRANSFER_HSL_HD = objDTO.TRANSFER_HSL_HD

            objData.EMPLOYEE_CODE = objDTO.EMPLOYEE_CODE
            objData.STATUS = objDTO.STATUS
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".Modify_RC_TRANSFERCAN_TOEMPLOYEE")
            Throw ex
        End Try
    End Function
#End Region

    Public Function GetTitleByOrg(ByVal OrgID As Decimal) As DataTable
        Try
            Dim dt As New DataTable
            Dim lstAllOrg = (From p In Context.HU_ORGANIZATION.Where(Function(f) f.ACTFLG = "A")
                             Select New With {.ID = p.ID, .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH}).ToList()
            Dim lstOrgInt As New List(Of Decimal)
            For Each org In lstAllOrg
                If org.HIERARCHICAL_PATH IsNot Nothing And org.HIERARCHICAL_PATH <> "" Then
                    If org.HIERARCHICAL_PATH.Split(New String() {";"}, StringSplitOptions.None).Contains(OrgID.ToString()) Then
                        lstOrgInt.Add(Decimal.Parse(org.ID))
                    End If
                End If
            Next
            Dim title = (From p1 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = OrgID)
                         From p2 In Context.HU_ORG_TITLE.Where(Function(f) f.ORG_ID = p1.ID)
                         From p3 In Context.HU_TITLE.Where(Function(f) f.ID = p2.TITLE_ID)
                         Select New With {.ID = p3.ID, .NAME_VN = p3.NAME_VN}).ToList()
            dt = title.ToTable()
            Return dt
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetTitleByOrg")
            Throw ex
        End Try
    End Function
    Public Function GetRCAllowanceCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Allowance_CandidateDTO)

        Try
            Dim query = From p In Context.RC_ALLOWANCE_CANDIDATE
                        From a In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_ID)
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ALLOWANCE_UNIT).DefaultIfEmpty
                        Where p.RC_CANDIDATE_ID = candidateID AndAlso p.RC_PROGRAM_ID = programID
                        Select New RC_Allowance_CandidateDTO With {
                                           .ID = p.ID,
                                           .ALLOWANCE_ID = p.ALLOWANCE_ID,
                                           .ALLOWANCE_NAME = a.NAME,
                                           .EFFECT_FROM = p.EFFECT_FROM,
                                           .EFFECT_TO = p.EFFECT_TO,
                                           .MONEY = p.MONEY,
                                           .RC_PROGRAM_ID = p.RC_PROGRAM_ID,
                                           .RC_CANDIDATE_ID = p.RC_CANDIDATE_ID,
                                           .ALLOWANCE_UNIT_ID = p.ALLOWANCE_UNIT,
                                           .ALLOWANCE_UNIT_NAME = o.NAME_VN}
            Return query.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetHRYearPlaning")
            Throw ex
        End Try

    End Function

    Public Function GetRCSalaryCandidate(ByVal programID As Decimal, ByVal candidateID As Decimal) As List(Of RC_Salary_CandidateDTO)

        Try
            Dim query = From p In Context.RC_SALARY_CANDIDATE
                        Where p.RC_CANDIDATE_ID = candidateID AndAlso p.RC_PROGRAM_ID = programID
                        Select New RC_Salary_CandidateDTO With {
                                           .ID = p.ID,
                                           .OTHERSALARY1 = p.OTHERSALARY1,
                                           .PERCENT_SAL = p.PERCENT_SAL,
                                           .RC_PROGRAM_ID = p.RC_PROGRAM_ID,
                                           .RC_CANDIDATE_ID = p.RC_CANDIDATE_ID,
                                           .SAL_TYPE_ID = p.SAL_TYPE_ID,
                                           .SALARY_OFFICIAL = p.SALARY_OFFICIAL,
                                           .SALARY_PROBATION = p.SALARY_PROBATION,
                                           .TAX_TABLE_ID = p.TAX_TABLE_ID,
                                           .EFFECT_DATE = p.EFFECT_DATE,
                                           .EMPLOYEE_TYPE_ID = p.EMPLOYEE_TYPE_ID}
            Return query.ToList
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".GetHRYearPlaning")
            Throw ex
        End Try

    End Function

    Public Function CheckTransferToEmployee(ByVal CandidateId As Decimal) As Boolean

        Try
            Dim count As Integer = (From p In Context.RC_TRANSFERCAN_TOEMPLOYEE
                                    Where p.CANDIDATE_ID = CandidateId).Count
            If count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Utility.WriteExceptionLog(ex, Me.ToString() & ".CheckTransferToEmployee")
            Throw ex
        End Try

    End Function

End Class

