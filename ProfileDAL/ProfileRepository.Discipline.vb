Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.DataAccess
Imports Framework.Data.System.Linq.Dynamic
Imports Oracle.DataAccess.Client

Partial Class ProfileRepository

#Region "Discipline"
    'khi phe duyet thi bat buoc nhap file dinh kem
    Public Function CheckHasFileDiscipline(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim disciplines = Context.HU_DISCIPLINE.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each discipline As HU_DISCIPLINE In disciplines
                If discipline.FILENAME Is Nothing Or discipline.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ApproveListDiscipline(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        Dim objCommendData As HU_DISCIPLINE
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                objCommendData = (From p In Context.HU_DISCIPLINE Where item = p.ID).SingleOrDefault
                objCommendData.STATUS_ID = If(acti = "PD", 447, 446)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetDiscipline(ByVal _filter As DisciplineDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From d In Context.HU_DISCIPLINE
                        From de In Context.HU_DISCIPLINE_EMP.Where(Function(de) de.HU_DISCIPLINE_ID = d.ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = de.HU_EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.LEVEL_ID).DefaultIfEmpty
                        From dis_obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.DISCIPLINE_OBJ).DefaultIfEmpty
                        From dis_type In Context.HU_DISCIPLINE_LIST.Where(Function(f) f.ID = d.DISCIPLINE_TYPE).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) (org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper) OrElse (de.HU_EMPLOYEE_ID Is Nothing And org.ORG_ID = 0))
                        From c In Context.SE_USER.Where(Function(f) f.USERNAME = d.CREATED_BY).DefaultIfEmpty
                        From m In Context.SE_USER.Where(Function(f) f.USERNAME = d.MODIFIED_BY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(ot) ot.ID = d.STATUS_ID).DefaultIfEmpty
                        From at_per In Context.AT_PERIOD.Where(Function(f) f.ID = d.PERIOD_ID).DefaultIfEmpty


            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.NO IsNot Nothing Then
                query = query.Where(Function(p) p.d.NO.ToUpper.Contains(_filter.NO.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.VIOLATION_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.VIOLATION_DATE = _filter.VIOLATION_DATE)
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE >= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE <= _filter.EFFECT_TO)
            End If
            If _filter.DISCIPLINE_OBJ_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.dis_obj.NAME_VN.ToUpper.Contains(_filter.DISCIPLINE_OBJ_NAME.ToUpper))
            End If
            If _filter.MONEY IsNot Nothing Then
                query = query.Where(Function(p) p.de.MONEY = _filter.MONEY)
            End If
            If _filter.IS_PORTAL IsNot Nothing Then
                query = query.Where(Function(p) p.d.IS_PORTAL = _filter.IS_PORTAL)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.d.YEAR = _filter.YEAR)
            End If
            If _filter.REMARK IsNot Nothing Then
                query = query.Where(Function(p) p.d.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o1.NAME_VN.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If
            If _filter.DISCIPLINE_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.dis_type.NAME.ToUpper.Contains(_filter.DISCIPLINE_TYPE_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ot.NAME_VN.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If _filter.DISCIPLINE_REASON_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.d.DISCIPLINE_REASON_DETAIL.ToUpper.Contains(_filter.DISCIPLINE_REASON_NAME.ToUpper))
            End If
            If _filter.DECISION_NO IsNot Nothing Then
                query = query.Where(Function(p) p.d.NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If

            Dim isPortal = "X"
            Dim isNotPortal = ""
            Dim lst = query.Select(Function(d) New DisciplineDTO With {
                                                                       .ID = d.d.ID,
                                                                       .YEAR = d.d.YEAR,
                                                                       .LEVEL_ID = d.d.LEVEL_ID,
                                                                       .LEVEL_NAME = d.o1.NAME_VN,
                                                                       .EMPLOYEE_ID = d.de.HU_EMPLOYEE_ID,
                                                                       .EMPLOYEE_CODE = d.e.EMPLOYEE_CODE,
                                                                       .EMPLOYEE_NAME = If(d.de.HU_EMPLOYEE_ID Is Nothing, d.de.FULLNAME_TEXT, d.e.FULLNAME_VN),
                                                                       .DECISION_NO = d.d.NO,
                                                                       .DISCIPLINE_OBJ_NAME = d.dis_obj.NAME_VN,
                                                                       .DISCIPLINE_TYPE_NAME = d.dis_type.NAME,
                                                                       .DISCIPLINE_REASON_NAME = d.d.DISCIPLINE_REASON_DETAIL,
                                                                       .EFFECT_DATE = d.d.EFFECT_DATE,
                                                                       .PERIOD_ID = d.d.PERIOD_ID,
                                                                       .PERIOD_NAME = d.at_per.PERIOD_NAME,
                                                                       .DEDUCT_FROM_SALARY = d.d.DEDUCT_FROM_SALARY,
                                                                       .IS_AMOUNT_IN_MONTH = d.d.IS_AMOUNT_IN_MONTH,
                                                                       .MONEY = d.d.MONEY,
                                                                       .INDEMNIFY_MONEY = d.d.INDEMNIFY_MONEY,
                                                                       .TITLE_NAME = If(d.de.HU_EMPLOYEE_ID Is Nothing, d.de.TITLE_TEXT, d.t.NAME_VN),
                                                                       .ORG_NAME = If(d.de.HU_EMPLOYEE_ID Is Nothing, d.de.ORG_TEXT, d.o.NAME_VN),
                                                                       .ORG_DESC = d.o.DESCRIPTION_PATH,
                                                                       .REMARK = d.d.REMARK,
                                                                       .CREATED_DATE = d.d.CREATED_DATE,
                                                                       .STATUS_NAME = d.ot.NAME_VN,
                                                                       .STATUS_CODE = d.ot.CODE,
                                                                       .STATUS_ID = d.d.STATUS_ID,
                                                                       .PERFORM_TIME = d.d.PERFORM_TIME,
                                                                       .DEL_DISCIPLINE_DATE = d.d.DEL_DISCIPLINE_DATE,
                                                                       .NOTE_DISCIPLINE = d.d.NOTE_DISCIPLINE,
                                                                       .DISCIPLINE_REASON_DETAIL = d.d.DISCIPLINE_REASON_DETAIL,
                                                                       .VIOLATION_DATE = d.d.VIOLATION_DATE,
                                                                       .DECTECT_VIOLATION_DATE = d.d.DECTECT_VIOLATION_DATE,
                                                                       .EXPLAIN = d.d.EXPLAIN,
                                                                       .RERARK_DISCIPLINE = d.d.RERARK_DISCIPLINE,
                                                                       .PAIDMONEY = d.d.PAIDMONEY,
                                                                       .AMOUNT_PAID_CASH = d.d.AMOUNT_PAID_CASH,
                                                                       .AMOUNT_TO_PAID = d.d.AMOUNT_TO_PAID,
                                                                       .AMOUNT_SAL_MONTH = d.d.AMOUNT_SAL_MONTH,
                                                                       .AMOUNT_IN_MONTH = d.d.AMOUNT_IN_MONTH,
                                                                       .AMOUNT_DEDUCT_AMOUNT = d.d.AMOUNT_DEDUCT_AMOUNT,
                                                                       .NO_DISCIPLINE = d.d.NO_DISCIPLINE,
                                                                       .SIGN_DATE = d.d.SIGN_DATE,
                                                                       .SIGNER_NAME = d.d.SIGNER_NAME,
                                                                       .SIGNER_TITLE = d.d.SIGNER_TITLE,
                                                                       .IS_PORTAL_NAME = If(d.d.IS_PORTAL = 1, isPortal, isNotPortal),
                                                                       .CREATED_BY = d.c.FULLNAME,
                                                                       .MODIFIED_BY = d.m.FULLNAME,
                                                                       .MODIFIED_DATE = d.d.MODIFIED_DATE})

            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_ID.HasValue AndAlso _filter.EMPLOYEE_ID = -1 Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID Is Nothing)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim listall = lst.ToList
            listall.AddRange(GetDisciplineOrg(_filter, PageIndex, PageSize, Total, log, Sorts))

            Return listall.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function GetDisciplineOrg(ByVal _filter As DisciplineDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From d In Context.HU_DISCIPLINE
                        From de In Context.HU_DISCIPLINE_ORG.Where(Function(de) de.HU_DISCIPLINE_ID = d.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = de.ORG_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.LEVEL_ID).DefaultIfEmpty
                        From dis_obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.DISCIPLINE_OBJ).DefaultIfEmpty
                        From dis_type In Context.HU_DISCIPLINE_LIST.Where(Function(f) f.ID = d.DISCIPLINE_TYPE).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) (org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper))
                        From c In Context.SE_USER.Where(Function(f) f.USERNAME = d.CREATED_BY).DefaultIfEmpty
                        From m In Context.SE_USER.Where(Function(f) f.USERNAME = d.MODIFIED_BY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(ot) ot.ID = d.STATUS_ID).DefaultIfEmpty
                        From at_per In Context.AT_PERIOD.Where(Function(f) f.ID = d.PERIOD_ID).DefaultIfEmpty

            If _filter.NO IsNot Nothing Then
                query = query.Where(Function(p) p.d.NO.ToUpper.Contains(_filter.NO.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.VIOLATION_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.VIOLATION_DATE = _filter.VIOLATION_DATE)
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE >= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE <= _filter.EFFECT_TO)
            End If
            If _filter.DISCIPLINE_OBJ_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.dis_obj.NAME_VN.ToUpper.Contains(_filter.DISCIPLINE_OBJ_NAME.ToUpper))
            End If
            If _filter.IS_PORTAL IsNot Nothing Then
                query = query.Where(Function(p) p.d.IS_PORTAL = _filter.IS_PORTAL)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.d.YEAR = _filter.YEAR)
            End If
            If _filter.REMARK IsNot Nothing Then
                query = query.Where(Function(p) p.d.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o1.NAME_VN.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If
            If _filter.DISCIPLINE_TYPE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.dis_type.NAME.ToUpper.Contains(_filter.DISCIPLINE_TYPE_NAME.ToUpper))
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ot.NAME_VN.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If
            If _filter.DISCIPLINE_REASON_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.d.DISCIPLINE_REASON_DETAIL.ToUpper.Contains(_filter.DISCIPLINE_REASON_NAME.ToUpper))
            End If
            If _filter.DECISION_NO IsNot Nothing Then
                query = query.Where(Function(p) p.d.NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If

            Dim isPortal = "X"
            Dim isNotPortal = ""
            Dim lst = query.Select(Function(d) New DisciplineDTO With {
                                                                       .ID = d.d.ID,
                                                                       .YEAR = d.d.YEAR,
                                                                       .LEVEL_ID = d.d.LEVEL_ID,
                                                                       .LEVEL_NAME = d.o1.NAME_VN,
                                                                       .DECISION_NO = d.d.NO,
                                                                       .DISCIPLINE_OBJ_NAME = d.dis_obj.NAME_VN,
                                                                       .DISCIPLINE_TYPE_NAME = d.dis_type.NAME,
                                                                       .DISCIPLINE_REASON_NAME = d.d.DISCIPLINE_REASON_DETAIL,
                                                                       .EFFECT_DATE = d.d.EFFECT_DATE,
                                                                       .PERIOD_ID = d.d.PERIOD_ID,
                                                                       .PERIOD_NAME = d.at_per.PERIOD_NAME,
                                                                       .DEDUCT_FROM_SALARY = d.d.DEDUCT_FROM_SALARY,
                                                                       .IS_AMOUNT_IN_MONTH = d.d.IS_AMOUNT_IN_MONTH,
                                                                       .ORG_NAME = d.o.NAME_VN,
                                                                       .ORG_DESC = d.o.DESCRIPTION_PATH,
                                                                       .REMARK = d.d.REMARK,
                                                                       .CREATED_DATE = d.d.CREATED_DATE,
                                                                       .STATUS_NAME = d.ot.NAME_VN,
                                                                       .STATUS_CODE = d.ot.CODE,
                                                                       .STATUS_ID = d.d.STATUS_ID,
                                                                       .PERFORM_TIME = d.d.PERFORM_TIME,
                                                                       .DEL_DISCIPLINE_DATE = d.d.DEL_DISCIPLINE_DATE,
                                                                       .NOTE_DISCIPLINE = d.d.NOTE_DISCIPLINE,
                                                                       .DISCIPLINE_REASON_DETAIL = d.d.DISCIPLINE_REASON_DETAIL,
                                                                       .VIOLATION_DATE = d.d.VIOLATION_DATE,
                                                                       .DECTECT_VIOLATION_DATE = d.d.DECTECT_VIOLATION_DATE,
                                                                       .EXPLAIN = d.d.EXPLAIN,
                                                                       .RERARK_DISCIPLINE = d.d.RERARK_DISCIPLINE,
                                                                       .PAIDMONEY = d.d.PAIDMONEY,
                                                                       .AMOUNT_PAID_CASH = d.d.AMOUNT_PAID_CASH,
                                                                       .AMOUNT_TO_PAID = d.d.AMOUNT_TO_PAID,
                                                                       .AMOUNT_SAL_MONTH = d.d.AMOUNT_SAL_MONTH,
                                                                       .AMOUNT_IN_MONTH = d.d.AMOUNT_IN_MONTH,
                                                                       .AMOUNT_DEDUCT_AMOUNT = d.d.AMOUNT_DEDUCT_AMOUNT,
                                                                       .NO_DISCIPLINE = d.d.NO_DISCIPLINE,
                                                                       .SIGN_DATE = d.d.SIGN_DATE,
                                                                       .SIGNER_NAME = d.d.SIGNER_NAME,
                                                                       .SIGNER_TITLE = d.d.SIGNER_TITLE,
                                                                       .IS_PORTAL_NAME = If(d.d.IS_PORTAL = 1, isPortal, isNotPortal),
                                                                       .CREATED_BY = d.c.FULLNAME,
                                                                       .MODIFIED_BY = d.m.FULLNAME,
                                                                       .MODIFIED_DATE = d.d.MODIFIED_DATE})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function GetOrgDesciplineID(ByVal DisId As Decimal) As List(Of DisciplineOrgDTO)
        Try
            Dim q = (From d In Context.HU_DISCIPLINE_ORG
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = d.ORG_ID)
                     Where d.HU_DISCIPLINE_ID = DisId
                     Select New DisciplineOrgDTO With {.ID = d.ID,
                                                       .HU_DISCIPLINE_ID = d.HU_DISCIPLINE_ID,
                                                       .ORG_ID = d.ORG_ID,
                                                       .ORG_NAME = o.NAME_VN}).ToList
            Return q
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineEmpDTO)
        Try
            Dim q = (From d In Context.HU_DISCIPLINE_EMP Where d.HU_DISCIPLINE_ID = DesId
                     From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = d.HU_EMPLOYEE_ID).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                     From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                     Select New DisciplineEmpDTO With {.ID = d.ID,
                                                       .HU_EMPLOYEE_ID = d.HU_EMPLOYEE_ID,
                                                       .HU_DISCIPLINE_ID = d.HU_DISCIPLINE_ID,
                                                       .MONEY = d.MONEY,
                                                       .INDEMNIFY_MONEY = d.INDEMNIFY_MONEY,
                                                       .MONEY_PAY = d.MONEY_PAY,
                                                       .INDEMNIFY_MONEY_PAY = d.INDEMNIFY_MONEY_PAY,
                                                       .MONEY_REMAIN = d.MONEY_REMAIN,
                                                       .INDEMNIFY_MONEY_REMAIN = d.INDEMNIFY_MONEY_REMAIN,
                                                       .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                       .FULLNAME = If(d.HU_EMPLOYEE_ID Is Nothing, d.FULLNAME_TEXT, e.FULLNAME_VN),
                                                       .ORG_NAME = If(d.HU_EMPLOYEE_ID Is Nothing, d.ORG_TEXT, o.NAME_VN),
                                                       .TITLE_NAME = If(d.HU_EMPLOYEE_ID Is Nothing, d.TITLE_TEXT, t.NAME_VN),
                                                       .FULLNAME_TEXT = d.FULLNAME_TEXT,
                                                       .ORG_TEXT = d.ORG_TEXT,
                                                       .TITLE_TEXT = d.TITLE_TEXT,
                                                       .NO_PROCESS = d.NO_PROCESS}).ToList
            Return q
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetDisciplineByID(ByVal _filter As DisciplineDTO) As DisciplineDTO
        Try

            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From p In Context.HU_DISCIPLINE
                        From pe In Context.AT_PERIOD.Where(Function(f) f.ID = p.PERIOD_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID).DefaultIfEmpty
                        From otReason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_REASON).DefaultIfEmpty
                        Join ot In Context.OT_OTHER_LIST On ot.ID Equals p.STATUS_ID
                        Where p.ID = _filter.ID


            Dim obj = query.Select(Function(p) New DisciplineDTO With {.ID = p.p.ID,
                                                                       .YEAR = p.p.YEAR,
                                                                       .LEVEL_ID = p.p.LEVEL_ID,
                                                                       .LEVEL_NAME = p.o.NAME_VN,
                                                                       .DECISION_NO = p.p.NO,
                                                                       .FILENAME = p.p.FILENAME,
                                                                       .UPLOADFILE = p.p.UPLOADFILE,
                                                                       .DISCIPLINE_LEVEL = p.p.DISCIPLINE_LEVEL,
                                                                       .DISCIPLINE_OBJ = p.p.DISCIPLINE_OBJ,
                                                                       .DISCIPLINE_TYPE = p.p.DISCIPLINE_TYPE,
                                                                       .DISCIPLINE_REASON = p.otReason.ID,
                                                                       .MONEY = p.p.MONEY,
                                                                       .INDEMNIFY_MONEY = p.p.INDEMNIFY_MONEY,
                                                                       .REMARK = p.p.REMARK,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                                       .STATUS_CODE = p.ot.CODE,
                                                                       .STATUS_ID = p.p.STATUS_ID,
                                                                       .DEDUCT_FROM_SALARY = p.p.DEDUCT_FROM_SALARY,
                                                                       .IS_AMOUNT_IN_MONTH = p.p.IS_AMOUNT_IN_MONTH,
                                                                       .PERIOD_ID = p.p.PERIOD_ID,
                                                                       .YEAR_PERIOD = p.pe.YEAR,
                                                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                                                       .ISSUE_DATE = p.p.ISSUE_DATE,
                                                                       .NAME = p.p.NAME,
                                                                       .NO = p.p.NO,
                                                                       .SIGN_DATE = p.p.SIGN_DATE,
                                                                       .SIGNER_NAME = p.p.SIGNER_NAME,
                                                                       .SIGNER_TITLE = p.p.SIGNER_TITLE,
                                                                       .PERFORM_TIME = p.p.PERFORM_TIME,
                                                                       .DEL_DISCIPLINE_DATE = p.p.DEL_DISCIPLINE_DATE,
                                                                       .NOTE_DISCIPLINE = p.p.NOTE_DISCIPLINE,
                                                                       .DISCIPLINE_REASON_DETAIL = p.p.DISCIPLINE_REASON_DETAIL,
                                                                       .VIOLATION_DATE = p.p.VIOLATION_DATE,
                                                                       .DECTECT_VIOLATION_DATE = p.p.DECTECT_VIOLATION_DATE,
                                                                       .EXPLAIN = p.p.EXPLAIN,
                                                                       .RERARK_DISCIPLINE = p.p.RERARK_DISCIPLINE,
                                                                       .PAIDMONEY = p.p.PAIDMONEY,
                                                                       .AMOUNT_PAID_CASH = p.p.AMOUNT_PAID_CASH,
                                                                       .AMOUNT_TO_PAID = p.p.AMOUNT_TO_PAID,
                                                                       .AMOUNT_SAL_MONTH = p.p.AMOUNT_SAL_MONTH,
                                                                       .AMOUNT_IN_MONTH = p.p.AMOUNT_IN_MONTH,
                                                                       .AMOUNT_DEDUCT_AMOUNT = p.p.AMOUNT_DEDUCT_AMOUNT,
                                                                       .NO_DISCIPLINE = p.p.NO_DISCIPLINE
                                                                      })
            ''Logger.LogInfo(obj)
            Return obj.SingleOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function InsertDiscipline(ByVal objDiscipline As DisciplineDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objDisciplineData As New HU_DISCIPLINE
        Try
            ' thêm kỷ luật
            objDisciplineData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE.EntitySet.Name)

            objDisciplineData.YEAR = objDiscipline.YEAR
            objDisciplineData.LEVEL_ID = objDiscipline.LEVEL_ID
            objDisciplineData.DISCIPLINE_LEVEL = objDiscipline.DISCIPLINE_LEVEL
            objDisciplineData.DISCIPLINE_OBJ = objDiscipline.DISCIPLINE_OBJ
            objDisciplineData.DISCIPLINE_TYPE = objDiscipline.DISCIPLINE_TYPE
            objDisciplineData.DISCIPLINE_REASON = objDiscipline.DISCIPLINE_REASON
            objDisciplineData.MONEY = objDiscipline.MONEY
            objDisciplineData.DEDUCT_FROM_SALARY = objDiscipline.DEDUCT_FROM_SALARY
            objDisciplineData.IS_AMOUNT_IN_MONTH = objDiscipline.IS_AMOUNT_IN_MONTH
            objDisciplineData.INDEMNIFY_MONEY = objDiscipline.INDEMNIFY_MONEY
            objDisciplineData.PERIOD_ID = objDiscipline.PERIOD_ID
            objDisciplineData.REMARK = objDiscipline.REMARK
            objDisciplineData.STATUS_ID = objDiscipline.STATUS_ID
            objDisciplineData.UPLOADFILE = objDiscipline.UPLOADFILE
            objDisciplineData.FILENAME = objDiscipline.FILENAME
            ' thêm quyết định            
            objDisciplineData.EFFECT_DATE = objDiscipline.EFFECT_DATE
            objDisciplineData.EXPIRE_DATE = objDiscipline.EXPIRE_DATE
            objDisciplineData.ISSUE_DATE = objDiscipline.ISSUE_DATE
            objDisciplineData.NAME = objDiscipline.NAME
            objDisciplineData.NO = objDiscipline.NO
            objDisciplineData.SIGN_DATE = objDiscipline.SIGN_DATE
            objDisciplineData.SIGNER_NAME = objDiscipline.SIGNER_NAME
            objDisciplineData.SIGNER_TITLE = objDiscipline.SIGNER_TITLE
            objDisciplineData.PERFORM_TIME = objDiscipline.PERFORM_TIME

            objDisciplineData.DEL_DISCIPLINE_DATE = objDiscipline.DEL_DISCIPLINE_DATE
            objDisciplineData.NOTE_DISCIPLINE = objDiscipline.NOTE_DISCIPLINE
            objDisciplineData.DISCIPLINE_REASON_DETAIL = objDiscipline.DISCIPLINE_REASON_DETAIL
            objDisciplineData.VIOLATION_DATE = objDiscipline.VIOLATION_DATE
            objDisciplineData.DECTECT_VIOLATION_DATE = objDiscipline.DECTECT_VIOLATION_DATE
            objDisciplineData.EXPLAIN = objDiscipline.EXPLAIN
            objDisciplineData.RERARK_DISCIPLINE = objDiscipline.RERARK_DISCIPLINE
            objDisciplineData.PAIDMONEY = objDiscipline.PAIDMONEY
            objDisciplineData.AMOUNT_PAID_CASH = objDiscipline.AMOUNT_PAID_CASH
            objDisciplineData.AMOUNT_TO_PAID = objDiscipline.AMOUNT_TO_PAID
            objDisciplineData.AMOUNT_SAL_MONTH = objDiscipline.AMOUNT_SAL_MONTH
            objDisciplineData.AMOUNT_IN_MONTH = objDiscipline.AMOUNT_IN_MONTH
            objDisciplineData.AMOUNT_DEDUCT_AMOUNT = objDiscipline.AMOUNT_DEDUCT_AMOUNT
            objDisciplineData.NO_DISCIPLINE = objDiscipline.NO_DISCIPLINE
            objDisciplineData.IS_PORTAL = objDiscipline.IS_PORTAL

            Context.HU_DISCIPLINE.AddObject(objDisciplineData)

            'thêm danh sách nhân viên kỷ luật.
            For Each obj As DisciplineEmpDTO In objDiscipline.DISCIPLINE_EMP


                Dim objDISCIPLINE_EMPData As New HU_DISCIPLINE_EMP
                objDISCIPLINE_EMPData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE_EMP.EntitySet.Name)
                objDISCIPLINE_EMPData.HU_DISCIPLINE_ID = objDisciplineData.ID
                objDISCIPLINE_EMPData.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID
                objDISCIPLINE_EMPData.MONEY = obj.MONEY
                objDISCIPLINE_EMPData.INDEMNIFY_MONEY = obj.INDEMNIFY_MONEY
                objDISCIPLINE_EMPData.IS_STOP = 0
                objDISCIPLINE_EMPData.NO_PROCESS = obj.NO_PROCESS
                If obj.HU_EMPLOYEE_ID Is Nothing Then
                    objDISCIPLINE_EMPData.FULLNAME_TEXT = obj.FULLNAME_TEXT
                    objDISCIPLINE_EMPData.ORG_TEXT = obj.ORG_TEXT
                    objDISCIPLINE_EMPData.TITLE_TEXT = obj.TITLE_TEXT
                End If
                Context.HU_DISCIPLINE_EMP.AddObject(objDISCIPLINE_EMPData)

                'If objDiscipline.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID And
                '    objDiscipline.PERIOD_ID IsNot Nothing Then
                '    ' Lấy thông tin lương của nhân viên cho đến thời điểm áp dụng trên kỷ luật
                '    Dim wk = (From e In Context.HU_WORKING
                '              Where e.EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And _
                '              e.EFFECT_DATE <= objDisciplineData.EFFECT_DATE And _
                '              e.IS_3B = False
                '              Order By e.EFFECT_DATE Descending
                '              Select New WorkingDTO With {.ID = e.ID,
                '                                          .EFFECT_DATE = e.EFFECT_DATE,
                '                                          .SAL_BASIC = e.SAL_BASIC,
                '                                          .COST_SUPPORT = e.COST_SUPPORT}).FirstOrDefault


                '    ' nếu có quá trình thì lấy thông tin lương để xử lý
                '    If wk IsNot Nothing Then
                '        ' 1. Lấy số tiền phạt trong kỳ lương
                '        Dim sumMoneyPay = (From p In Context.HU_DISCIPLINE_EMP
                '                           From dis In Context.HU_DISCIPLINE.Where(Function(f) f.ID = p.HU_DISCIPLINE_ID)
                '                           Where p.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And _
                '                           dis.PERIOD_ID = objDiscipline.PERIOD_ID And _
                '                           dis.ID <> objDiscipline.ID
                '                           Select p.MONEY_PAY).Sum

                '        If wk.COST_SUPPORT IsNot Nothing Then
                '            If sumMoneyPay IsNot Nothing Then
                '                If sumMoneyPay >= wk.COST_SUPPORT Then ' Nểu tổng tiền thành >= chi phí hỗ trợ
                '                    ' Số tiền phạt còn lại = số tiền ghi nhận
                '                    ' Số tiền thanh toán = 0
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY
                '                    objDISCIPLINE_EMPData.MONEY_PAY = 0
                '                Else ' Nếu số tiền phạt < chi phí hỗ trợ
                '                    ' Tính số tiền còn lại được phép thanh toán
                '                    Dim sumMoneyRemain = wk.COST_SUPPORT - sumMoneyPay
                '                    If obj.MONEY IsNot Nothing AndAlso obj.MONEY > sumMoneyRemain Then
                '                        ' Nếu số tiền phạt ghi nhận > chi phí hỗ trợ còn lại được phép thanh toán
                '                        ' Số tiền phạt còn lại = số tiền ghi nhận - chi phí hỗ trợ còn lại được phép thanh toán
                '                        ' Số tiền thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY - sumMoneyRemain
                '                        objDISCIPLINE_EMPData.MONEY_PAY = sumMoneyRemain
                '                    Else
                '                        ' Số tiền phạt còn lại = 0
                '                        ' Số tiền thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.MONEY_REMAIN = 0
                '                        objDISCIPLINE_EMPData.MONEY_PAY = obj.MONEY
                '                    End If
                '                End If
                '            Else
                '                ' Nếu tổng thanh toán không có giá trị
                '                If obj.MONEY IsNot Nothing AndAlso obj.MONEY > wk.COST_SUPPORT Then
                '                    ' Nếu số tiền phạt > chi phí hỗ trợ
                '                    ' Số tiền phạt còn lại = số tiền ghi nhận - chi phí hỗ trợ
                '                    ' Số tiền thanh toán = số tiền ghi nhận
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY - wk.COST_SUPPORT
                '                    objDISCIPLINE_EMPData.MONEY_PAY = wk.COST_SUPPORT
                '                Else
                '                    ' Số tiền phạt còn lại = 0
                '                    ' Số tiền thanh toán = số tiền ghi nhận
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = 0
                '                    objDISCIPLINE_EMPData.MONEY_PAY = obj.MONEY
                '                End If
                '            End If
                '        Else
                '            ' Nếu chi phí hỗ trợ không có giá trị
                '            ' Số tiền phạt còn lại = số tiền ghi nhận
                '            ' Số tiền thanh toán = 0
                '            objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY
                '            objDISCIPLINE_EMPData.MONEY_PAY = 0
                '        End If

                '        ' 2. Lấy số tiền bồi thường trong kỳ lương
                '        Dim sumIndemMoneyPay = (From p In Context.HU_DISCIPLINE_EMP
                '                        From dis In Context.HU_DISCIPLINE.Where(Function(f) f.ID = p.HU_DISCIPLINE_ID)
                '                        Where p.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And dis.PERIOD_ID = objDiscipline.PERIOD_ID
                '                        Select p.INDEMNIFY_MONEY_PAY).Sum

                '        If wk.SAL_BASIC IsNot Nothing Then
                '            Dim salBasicPay = wk.SAL_BASIC.Value * 30 / 100
                '            If sumIndemMoneyPay IsNot Nothing Then
                '                ' Nểu tổng tiền thành > 30% LCB
                '                If sumIndemMoneyPay >= salBasicPay Then
                '                    ' Số tiền bồi thường còn lại = số tiền ghi nhận
                '                    ' Số tiền thanh toán = 0
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = 0
                '                Else
                '                    ' Nếu số tiền bồi thường < 30% LCB
                '                    ' Tính số tiền bồi thường còn lại được phép thanh toán
                '                    Dim sumMoneyIndemRemain = salBasicPay - sumIndemMoneyPay
                '                    If obj.INDEMNIFY_MONEY IsNot Nothing AndAlso obj.INDEMNIFY_MONEY > sumMoneyIndemRemain Then
                '                        ' Nếu số tiền bồi thường ghi nhận > 30% LCB còn lại được phép thanh toán
                '                        ' Số tiền bồi thường còn lại = số tiền bồi thường ghi nhận - 30% LCB còn lại được phép thanh toán
                '                        ' Số tiền bồi thường thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY - sumMoneyIndemRemain
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = sumMoneyIndemRemain
                '                    Else
                '                        ' Số tiền bồi thường còn lại = 0
                '                        ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = 0
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = obj.INDEMNIFY_MONEY
                '                    End If
                '                End If
                '            Else
                '                ' Nếu tổng thanh toán không có giá trị
                '                If obj.INDEMNIFY_MONEY IsNot Nothing AndAlso obj.INDEMNIFY_MONEY > salBasicPay Then
                '                    ' Nếu số tiền bồi thường > 30% LCB
                '                    ' Số tiền bồi thường còn lại = số tiền ghi nhận - 30% LCB
                '                    ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY - salBasicPay
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = salBasicPay
                '                Else
                '                    ' Số tiền bồi thường còn lại = 0
                '                    ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = 0
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = obj.INDEMNIFY_MONEY
                '                End If
                '            End If
                '        Else
                '            ' Nếu LCB không có giá trị
                '            ' Số tiền bồi thường còn lại = số tiền bồi thường ghi nhận
                '            ' Số tiền bồi thường thanh toán = 0
                '            objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY
                '            objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = 0
                '        End If
                '    End If
                'End If
            Next
            'Context.HU_DECISION.AddObject(objDecisionData)
            'thêm danh sách PHÒNG BAN kỷ luật.
            For Each obj As DisciplineOrgDTO In objDiscipline.DISCIPLINE_ORG
                Dim objDISCIPLINE_EMPData As New HU_DISCIPLINE_ORG
                objDISCIPLINE_EMPData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE_ORG.EntitySet.Name)
                objDISCIPLINE_EMPData.HU_DISCIPLINE_ID = objDisciplineData.ID
                objDISCIPLINE_EMPData.ORG_ID = obj.ORG_ID
                Context.HU_DISCIPLINE_ORG.AddObject(objDISCIPLINE_EMPData)
            Next
            Context.SaveChanges(log)
            gID = objDisciplineData.ID
            If objDiscipline.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID And
                   objDiscipline.PERIOD_ID IsNot Nothing Then
                Using cls As New DataAccess.NonQueryData

                    cls.ExecuteStore("PKG_PROFILE_BUSINESS.CALL_DISCIPLINE_EMP",
                                     New With {.P_DISCIPLINE_ID = objDisciplineData.ID,
                                               .P_PERIOD_ID = objDisciplineData.PERIOD_ID})
                End Using
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyDiscipline(ByVal objDiscipline As DisciplineDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDisciplineData As New HU_DISCIPLINE With {.ID = objDiscipline.ID}

        Try
            objDisciplineData = (From p In Context.HU_DISCIPLINE Where p.ID = objDiscipline.ID).FirstOrDefault
            ' sửa kỷ luật
            objDisciplineData.ID = objDiscipline.ID

            objDisciplineData.YEAR = objDiscipline.YEAR
            objDisciplineData.LEVEL_ID = objDiscipline.LEVEL_ID
            objDisciplineData.DISCIPLINE_LEVEL = objDiscipline.DISCIPLINE_LEVEL
            objDisciplineData.DISCIPLINE_OBJ = objDiscipline.DISCIPLINE_OBJ
            objDisciplineData.DISCIPLINE_TYPE = objDiscipline.DISCIPLINE_TYPE
            objDisciplineData.DISCIPLINE_REASON = objDiscipline.DISCIPLINE_REASON
            objDisciplineData.MONEY = objDiscipline.MONEY
            objDisciplineData.STATUS_ID = objDiscipline.STATUS_ID
            objDisciplineData.REMARK = objDiscipline.REMARK
            objDisciplineData.DEDUCT_FROM_SALARY = objDiscipline.DEDUCT_FROM_SALARY
            objDisciplineData.IS_AMOUNT_IN_MONTH = objDiscipline.IS_AMOUNT_IN_MONTH
            objDisciplineData.PERIOD_ID = objDiscipline.PERIOD_ID
            objDisciplineData.INDEMNIFY_MONEY = objDiscipline.INDEMNIFY_MONEY
            objDisciplineData.UPLOADFILE = objDiscipline.UPLOADFILE
            objDisciplineData.FILENAME = objDiscipline.FILENAME
            ' sua quyet dinh
            objDisciplineData.EFFECT_DATE = objDiscipline.EFFECT_DATE
            objDisciplineData.EXPIRE_DATE = objDiscipline.EXPIRE_DATE
            objDisciplineData.ISSUE_DATE = objDiscipline.ISSUE_DATE
            objDisciplineData.NAME = objDiscipline.NAME
            objDisciplineData.NO = objDiscipline.NO
            objDisciplineData.SIGN_DATE = objDiscipline.SIGN_DATE
            objDisciplineData.SIGNER_NAME = objDiscipline.SIGNER_NAME
            objDisciplineData.SIGNER_TITLE = objDiscipline.SIGNER_TITLE
            objDisciplineData.PERFORM_TIME = objDiscipline.PERFORM_TIME

            objDisciplineData.DEL_DISCIPLINE_DATE = objDiscipline.DEL_DISCIPLINE_DATE
            objDisciplineData.NOTE_DISCIPLINE = objDiscipline.NOTE_DISCIPLINE
            objDisciplineData.DISCIPLINE_REASON_DETAIL = objDiscipline.DISCIPLINE_REASON_DETAIL
            objDisciplineData.VIOLATION_DATE = objDiscipline.VIOLATION_DATE
            objDisciplineData.DECTECT_VIOLATION_DATE = objDiscipline.DECTECT_VIOLATION_DATE
            objDisciplineData.EXPLAIN = objDiscipline.EXPLAIN
            objDisciplineData.RERARK_DISCIPLINE = objDiscipline.RERARK_DISCIPLINE
            objDisciplineData.PAIDMONEY = objDiscipline.PAIDMONEY
            objDisciplineData.AMOUNT_PAID_CASH = objDiscipline.AMOUNT_PAID_CASH
            objDisciplineData.AMOUNT_TO_PAID = objDiscipline.AMOUNT_TO_PAID
            objDisciplineData.AMOUNT_SAL_MONTH = objDiscipline.AMOUNT_SAL_MONTH
            objDisciplineData.AMOUNT_IN_MONTH = objDiscipline.AMOUNT_IN_MONTH
            objDisciplineData.AMOUNT_DEDUCT_AMOUNT = objDiscipline.AMOUNT_DEDUCT_AMOUNT
            objDisciplineData.NO_DISCIPLINE = objDiscipline.NO_DISCIPLINE

            'objDisciplineData.PAY_DATE = objDiscipline.PAY_DAT            

            'Xóa danh sách nhân viên kỷ luật cũ
            Dim objD = (From d In Context.HU_DISCIPLINE_EMP Where d.HU_DISCIPLINE_ID = objDiscipline.ID).ToList
            For Each obj As HU_DISCIPLINE_EMP In objD
                Context.HU_DISCIPLINE_EMP.DeleteObject(obj)
            Next
            'thêm danh sách nhân viên kỷ luật mới
            For Each obj As DisciplineEmpDTO In objDiscipline.DISCIPLINE_EMP
                Dim objDISCIPLINE_EMPData As New HU_DISCIPLINE_EMP
                objDISCIPLINE_EMPData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE_EMP.EntitySet.Name)
                objDISCIPLINE_EMPData.HU_DISCIPLINE_ID = objDisciplineData.ID
                objDISCIPLINE_EMPData.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID
                objDISCIPLINE_EMPData.MONEY = obj.MONEY
                objDISCIPLINE_EMPData.INDEMNIFY_MONEY = obj.INDEMNIFY_MONEY
                objDISCIPLINE_EMPData.IS_STOP = 0
                objDISCIPLINE_EMPData.NO_PROCESS = obj.NO_PROCESS
                If obj.HU_EMPLOYEE_ID Is Nothing Then
                    objDISCIPLINE_EMPData.FULLNAME_TEXT = obj.FULLNAME_TEXT
                    objDISCIPLINE_EMPData.ORG_TEXT = obj.ORG_TEXT
                    objDISCIPLINE_EMPData.TITLE_TEXT = obj.TITLE_TEXT
                End If
                Context.HU_DISCIPLINE_EMP.AddObject(objDISCIPLINE_EMPData)
                'If objDiscipline.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID And
                '    objDiscipline.PERIOD_ID IsNot Nothing Then
                '    ' Lấy thông tin lương của nhân viên cho đến thời điểm áp dụng trên kỷ luật
                '    Dim wk = (From e In Context.HU_WORKING
                '              Where e.EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And _
                '              e.EFFECT_DATE <= objDisciplineData.EFFECT_DATE And _
                '              e.IS_3B = False
                '              Order By e.EFFECT_DATE Descending
                '              Select New WorkingDTO With {.ID = e.ID,
                '                                          .EFFECT_DATE = e.EFFECT_DATE,
                '                                          .SAL_BASIC = e.SAL_BASIC,
                '                                          .COST_SUPPORT = e.COST_SUPPORT}).FirstOrDefault
                '    ' nếu có quá trình thì lấy thông tin lương để xử lý
                '    If wk IsNot Nothing Then
                '        ' 1. Lấy số tiền phạt trong kỳ lương
                '        Dim sumMoneyPay = (From p In Context.HU_DISCIPLINE_EMP
                '                           From dis In Context.HU_DISCIPLINE.Where(Function(f) f.ID = p.HU_DISCIPLINE_ID)
                '                           Where p.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And _
                '                           dis.PERIOD_ID = objDiscipline.PERIOD_ID And _
                '                           dis.ID <> objDiscipline.ID
                '                           Select p.MONEY_PAY).Sum

                '        If wk.COST_SUPPORT IsNot Nothing Then
                '            If sumMoneyPay IsNot Nothing Then
                '                If sumMoneyPay >= wk.COST_SUPPORT Then ' Nểu tổng tiền thành >= chi phí hỗ trợ
                '                    ' Số tiền phạt còn lại = số tiền ghi nhận
                '                    ' Số tiền thanh toán = 0
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY
                '                    objDISCIPLINE_EMPData.MONEY_PAY = 0
                '                Else ' Nếu số tiền phạt < chi phí hỗ trợ
                '                    ' Tính số tiền còn lại được phép thanh toán
                '                    Dim sumMoneyRemain = wk.COST_SUPPORT - sumMoneyPay
                '                    If obj.MONEY IsNot Nothing AndAlso obj.MONEY > sumMoneyRemain Then
                '                        ' Nếu số tiền phạt ghi nhận > chi phí hỗ trợ còn lại được phép thanh toán
                '                        ' Số tiền phạt còn lại = số tiền ghi nhận - chi phí hỗ trợ còn lại được phép thanh toán
                '                        ' Số tiền thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY - sumMoneyRemain
                '                        objDISCIPLINE_EMPData.MONEY_PAY = sumMoneyRemain
                '                    Else
                '                        ' Số tiền phạt còn lại = 0
                '                        ' Số tiền thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.MONEY_REMAIN = 0
                '                        objDISCIPLINE_EMPData.MONEY_PAY = obj.MONEY
                '                    End If
                '                End If
                '            Else
                '                ' Nếu tổng thanh toán không có giá trị
                '                If obj.MONEY IsNot Nothing AndAlso obj.MONEY > wk.COST_SUPPORT Then
                '                    ' Nếu số tiền phạt > chi phí hỗ trợ
                '                    ' Số tiền phạt còn lại = số tiền ghi nhận - chi phí hỗ trợ
                '                    ' Số tiền thanh toán = số tiền ghi nhận
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY - wk.COST_SUPPORT
                '                    objDISCIPLINE_EMPData.MONEY_PAY = wk.COST_SUPPORT
                '                Else
                '                    ' Số tiền phạt còn lại = 0
                '                    ' Số tiền thanh toán = số tiền ghi nhận
                '                    objDISCIPLINE_EMPData.MONEY_REMAIN = 0
                '                    objDISCIPLINE_EMPData.MONEY_PAY = obj.MONEY
                '                End If
                '            End If
                '        Else
                '            ' Nếu chi phí hỗ trợ không có giá trị
                '            ' Số tiền phạt còn lại = số tiền ghi nhận
                '            ' Số tiền thanh toán = 0
                '            objDISCIPLINE_EMPData.MONEY_REMAIN = obj.MONEY
                '            objDISCIPLINE_EMPData.MONEY_PAY = 0
                '        End If

                '        ' 2. Lấy số tiền bồi thường trong kỳ lương
                '        Dim sumIndemMoneyPay = (From p In Context.HU_DISCIPLINE_EMP
                '                        From dis In Context.HU_DISCIPLINE.Where(Function(f) f.ID = p.HU_DISCIPLINE_ID)
                '                        Where p.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID And dis.PERIOD_ID = objDiscipline.PERIOD_ID
                '                        Select p.INDEMNIFY_MONEY_PAY).Sum

                '        If wk.SAL_BASIC IsNot Nothing Then
                '            Dim salBasicPay = wk.SAL_BASIC.Value * 30 / 100
                '            If sumIndemMoneyPay IsNot Nothing Then
                '                ' Nểu tổng tiền thành > 30% LCB
                '                If sumIndemMoneyPay >= salBasicPay Then
                '                    ' Số tiền bồi thường còn lại = số tiền ghi nhận
                '                    ' Số tiền thanh toán = 0
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = 0
                '                Else
                '                    ' Nếu số tiền bồi thường < 30% LCB
                '                    ' Tính số tiền bồi thường còn lại được phép thanh toán
                '                    Dim sumMoneyIndemRemain = salBasicPay - sumIndemMoneyPay
                '                    If obj.INDEMNIFY_MONEY IsNot Nothing AndAlso obj.INDEMNIFY_MONEY > sumMoneyIndemRemain Then
                '                        ' Nếu số tiền bồi thường ghi nhận > 30% LCB còn lại được phép thanh toán
                '                        ' Số tiền bồi thường còn lại = số tiền bồi thường ghi nhận - 30% LCB còn lại được phép thanh toán
                '                        ' Số tiền bồi thường thanh toán = số tiền ghi nhận
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY - sumMoneyIndemRemain
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = sumMoneyIndemRemain
                '                    Else
                '                        ' Số tiền bồi thường còn lại = 0
                '                        ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = 0
                '                        objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = obj.INDEMNIFY_MONEY
                '                    End If
                '                End If
                '            Else
                '                ' Nếu tổng thanh toán không có giá trị
                '                If obj.INDEMNIFY_MONEY IsNot Nothing AndAlso obj.INDEMNIFY_MONEY > salBasicPay Then
                '                    ' Nếu số tiền bồi thường > 30% LCB
                '                    ' Số tiền bồi thường còn lại = số tiền ghi nhận - 30% LCB
                '                    ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY - salBasicPay
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = salBasicPay
                '                Else
                '                    ' Số tiền bồi thường còn lại = 0
                '                    ' Số tiền bồi thường thanh toán = số tiền bồi thường ghi nhận
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = 0
                '                    objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = obj.INDEMNIFY_MONEY
                '                End If
                '            End If
                '        Else
                '            ' Nếu LCB không có giá trị
                '            ' Số tiền bồi thường còn lại = số tiền bồi thường ghi nhận
                '            ' Số tiền bồi thường thanh toán = 0
                '            objDISCIPLINE_EMPData.INDEMNIFY_MONEY_REMAIN = obj.INDEMNIFY_MONEY
                '            objDISCIPLINE_EMPData.INDEMNIFY_MONEY_PAY = 0
                '        End If
                '    End If
                'End If
            Next
            'Xóa danh sách nhân viên kỷ luật cũ
            Dim objOrg = (From d In Context.HU_DISCIPLINE_ORG Where d.HU_DISCIPLINE_ID = objDiscipline.ID).ToList
            For Each obj As HU_DISCIPLINE_ORG In objOrg
                Context.HU_DISCIPLINE_ORG.DeleteObject(obj)
            Next
            'thêm danh sách PHÒNG BAN kỷ luật.
            For Each obj As DisciplineOrgDTO In objDiscipline.DISCIPLINE_ORG
                Dim objDISCIPLINE_EMPData As New HU_DISCIPLINE_ORG
                objDISCIPLINE_EMPData.ID = Utilities.GetNextSequence(Context, Context.HU_DISCIPLINE_ORG.EntitySet.Name)
                objDISCIPLINE_EMPData.HU_DISCIPLINE_ID = objDisciplineData.ID
                objDISCIPLINE_EMPData.ORG_ID = obj.ORG_ID
                Context.HU_DISCIPLINE_ORG.AddObject(objDISCIPLINE_EMPData)
            Next
            Context.SaveChanges(log)
            gID = objDisciplineData.ID
            If objDiscipline.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID And
                   objDiscipline.PERIOD_ID IsNot Nothing Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PROFILE_BUSINESS.CALL_DISCIPLINE_EMP",
                                     New With {.P_DISCIPLINE_ID = objDisciplineData.ID,
                                               .P_PERIOD_ID = objDisciplineData.PERIOD_ID})
                End Using
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteDiscipline(ByVal lstDiscipline() As DisciplineDTO) As Boolean
        'Dim objDisciplineData As HU_DISCIPLINE
        Try
            'objDisciplineData = (From p In Context.HU_DISCIPLINE Where objDiscipline.ID = p.ID).FirstOrDefault
            ''objDecisionData = (From p In Context.HU_DECISION Where objDiscipline.DECISION_ID = p.ID).SingleOrDefault
            ''Context.HU_DISCIPLINE.DeleteObject(objDisciplineData)
            'Dim objD = (From d In Context.HU_DISCIPLINE_EMP Where d.HU_DISCIPLINE_ID = objDiscipline.ID).ToList
            'For Each obj As HU_DISCIPLINE_EMP In objD
            '    Context.HU_DISCIPLINE_EMP.DeleteObject(obj)
            'Next
            'Context.HU_DISCIPLINE.DeleteObject(objDisciplineData)
            'Context.SaveChanges()
            'Return True
            'Dim lstDisciplineData As List(Of HU_DISCIPLINE)
            'Dim lstIDDiscipline As List(Of Decimal) = (From p In objDiscipline.ToList Select p.ID).ToList
            'lstDisciplineData = (From p In Context.HU_DISCIPLINE Where lstIDDiscipline.Contains(p.ID)).ToList
            'For index = 0 To lstDisciplineData.Count - 1
            '    Dim lstEmp = From p In Context.HU_DISCIPLINE_EMP Where p.HU_DISCIPLINE_ID = lstDisciplineData(index).ID

            '    For Each item In lstEmp
            '        Context.HU_DISCIPLINE_EMP.DeleteObject(item)
            '    Next
            '    Context.HU_DISCIPLINE.DeleteObject(lstDisciplineData(index))
            'Next
            'Context.SaveChanges()

            Dim objDisciplineData As HU_DISCIPLINE
            For Each objDiscipline In lstDiscipline
                objDisciplineData = (From p In Context.HU_DISCIPLINE Where objDiscipline.ID = p.ID).SingleOrDefault
                If objDisciplineData IsNot Nothing Then
                    Dim lstEmp = (From p In Context.HU_DISCIPLINE_EMP Where p.HU_DISCIPLINE_ID = objDisciplineData.ID Select p).ToList()
                    If lstEmp IsNot Nothing Then
                        For Each emp As HU_DISCIPLINE_EMP In lstEmp
                            Context.HU_DISCIPLINE_EMP.DeleteObject(emp)
                        Next
                    End If
                    Dim lstOrg = (From p In Context.HU_DISCIPLINE_ORG Where p.HU_DISCIPLINE_ID = objDisciplineData.ID Select p).ToList()
                    If lstOrg IsNot Nothing Then
                        For Each org As HU_DISCIPLINE_ORG In lstOrg
                            Context.HU_DISCIPLINE_ORG.DeleteObject(org)
                        Next
                    End If
                End If
                Context.HU_DISCIPLINE.DeleteObject(objDisciplineData)
                Context.SaveChanges()
            Next

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateDiscipline(ByVal sType As String, ByVal obj As DisciplineDTO) As Boolean
        Try
            Select Case sType
                Case "EXIST_DECISION_NO"
                    Return (From e In Context.HU_DISCIPLINE
                            Where e.NO.ToUpper = obj.NO.ToUpper And
                            e.ID <> obj.ID).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ApproveDiscipline(ByVal objDiscipline As DisciplineDTO, Optional ByVal bCheck As Boolean = True) As Boolean
        Dim objDisciplineData As HU_DISCIPLINE
        Try
            If Format(objDiscipline.EFFECT_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Return False
            End If
            If bCheck Then
                objDisciplineData = (From p In Context.HU_DISCIPLINE Where objDiscipline.ID = p.ID).SingleOrDefault
                objDisciplineData.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID
            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function Open_ApproveDiscipline(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim objDisData As HU_DISCIPLINE
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                objDisData = (From p In Context.HU_DISCIPLINE Where item = p.ID).SingleOrDefault
                objDisData.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Discipline Salary"

    Public Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "YEAR ,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From d In Context.HU_DISCIPLINE
                        From de In Context.HU_DISCIPLINE_EMP.Where(Function(f) f.HU_DISCIPLINE_ID = d.ID)
                        From ds In Context.HU_DISCIPLINE_SALARY.Where(Function(f) f.DISCIPLINE_ID = d.ID And f.EMPLOYEE_ID = de.HU_EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ds.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                        From org In Context.SE_CHOSEN_ORG.Where(Function(org) org.ORG_ID = o.ID And
                                                                    org.USERNAME = log.Username.ToUpper)

            If Not _filter.IS_TERMINATE Then
                query = query.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.DECISION_NO IsNot Nothing Then
                query = query.Where(Function(p) p.d.NO.ToUpper.Contains(_filter.DECISION_NO.ToUpper))
            End If
            If _filter.MONTH IsNot Nothing Then
                query = query.Where(Function(p) p.ds.MONTH = _filter.MONTH)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.ds.YEAR = _filter.YEAR)
            End If
            If _filter.INDEMNIFY_MONEY IsNot Nothing Then
                query = query.Where(Function(p) p.ds.INDEMNIFY_MONEY = _filter.INDEMNIFY_MONEY)
            End If
            If _filter.MONEY IsNot Nothing Then
                query = query.Where(Function(p) p.ds.MONEY = _filter.MONEY)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.d.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            Dim lst = query.Select(Function(d) New DisciplineSalaryDTO With {.ID = d.ds.ID,
                                                                             .APPROVE_STATUS = d.ds.APPROVE_STATUS,
                                                                             .IS_STOP = d.ds.IS_STOP,
                                                                             .DISCIPLINE_ID = d.d.ID,
                                                                             .DECISION_NO = d.d.NO,
                                                                             .EFFECT_DATE = d.d.EFFECT_DATE,
                                                                             .EMPLOYEE_CODE = d.e.EMPLOYEE_CODE,
                                                                             .EMPLOYEE_ID = d.e.ID,
                                                                             .EMPLOYEE_NAME = d.e.FULLNAME_VN,
                                                                             .ORG_NAME = d.o.NAME_VN,
                                                                             .TITLE_NAME = d.t.NAME_VN,
                                                                             .MONTH = d.ds.MONTH,
                                                                             .YEAR = d.ds.YEAR,
                                                                             .ORG_DESC = d.o.DESCRIPTION_PATH,
                                                                             .MONEY = d.ds.MONEY,
                                                                             .INDEMNIFY_MONEY = d.ds.INDEMNIFY_MONEY,
                                                                             .MONEY_ORIGIN = d.de.MONEY,
                                                                             .INDEMNIFY_MONEY_ORIGIN = d.de.INDEMNIFY_MONEY})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetDisciplineSalaryByID(ByVal _filter As DisciplineSalaryDTO) As DisciplineSalaryDTO
        Try
            Dim query = From d In Context.HU_DISCIPLINE
                        From de In Context.HU_DISCIPLINE_EMP.Where(Function(f) f.HU_DISCIPLINE_ID = d.ID)
                        From ds In Context.HU_DISCIPLINE_SALARY.Where(Function(f) f.DISCIPLINE_ID = d.ID And f.EMPLOYEE_ID = de.HU_EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ds.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                        Where ds.ID = _filter.ID

            Dim lst = query.Select(Function(d) New DisciplineSalaryDTO With {.ID = d.ds.ID,
                                                                             .APPROVE_STATUS = d.ds.APPROVE_STATUS,
                                                                             .DISCIPLINE_ID = d.d.ID,
                                                                             .EMPLOYEE_CODE = d.e.EMPLOYEE_CODE,
                                                                             .EMPLOYEE_ID = d.e.ID,
                                                                             .EMPLOYEE_NAME = d.e.FULLNAME_VN,
                                                                             .ORG_NAME = d.o.NAME_VN,
                                                                             .TITLE_NAME = d.t.NAME_VN,
                                                                             .MONTH = d.ds.MONTH,
                                                                             .YEAR = d.ds.YEAR,
                                                                             .ORG_DESC = d.o.DESCRIPTION_PATH,
                                                                             .MONEY = d.ds.MONEY,
                                                                             .INDEMNIFY_MONEY = d.ds.INDEMNIFY_MONEY})


            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function EditDisciplineSalary(obj As DisciplineSalaryDTO) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_PROFILE_BUSINESS.UPDATE_DISCIPLINE_SALARY"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            cmd.Parameters.Clear()
                            Using resource As New DataAccess.OracleCommon()
                                Dim objParam = New With {.P_DISCIPLINE_ID = obj.DISCIPLINE_ID,
                                                         .P_DISCIPLINE_SALARY_ID = obj.ID,
                                                         .P_MONEY = obj.MONEY,
                                                         .P_MONTH = obj.MONTH,
                                                         .P_YEAR = obj.YEAR,
                                                         .P_EMPLOYEE_ID = obj.EMPLOYEE_ID}

                                If objParam IsNot Nothing Then
                                    For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End Using
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                            cmd.Transaction.Rollback()
                            Return False
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function


    Public Function ValidateDisciplineSalary(ByVal obj As DisciplineSalaryDTO, ByRef sError As String) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                Dim objParam = New With {.P_DISCIPLINE_ID = obj.DISCIPLINE_ID,
                                         .P_DISCIPLINE_SALARY_ID = obj.ID,
                                         .P_MONEY = obj.MONEY,
                                         .P_INDEMNIFY_MONEY = obj.INDEMNIFY_MONEY,
                                         .P_MONTH = obj.MONTH,
                                         .P_YEAR = obj.YEAR,
                                         .P_EMPLOYEE_ID = obj.EMPLOYEE_ID,
                                         .PO_SAL_BASIC = cls.OUT_NUMBER,
                                         .PO_COST_SUPPORT = cls.OUT_NUMBER,
                                         .PO_ERROR = cls.OUT_NUMBER}

                cls.ExecuteStore("PKG_PROFILE_BUSINESS.VALIDATE_DISCIPLINE_SALARY", objParam)
                If objParam.PO_ERROR <> "0" Then
                    Select Case objParam.PO_ERROR
                        Case "1"
                            sError = "Số tiền không thể lớn hơn Số tiền còn lại phải trừ: " &
                                Format(Decimal.Parse(objParam.PO_COST_SUPPORT), "n0")
                        Case "2"
                            sError = "Bồi thường vật chất không thể lớn hơn Bồi thường vật chất phải trừ: " &
                                Format(Decimal.Parse(objParam.PO_SAL_BASIC), "n0")
                        Case "3"
                            sError = "Số tiền không thể lớn hơn Chi phí hỗ trợ được phép trừ: " &
                                Format(Decimal.Parse(objParam.PO_COST_SUPPORT), "n0")
                        Case "4"
                            sError = "Bồi thường vật chất không thể lớn hơn Lương cơ bản * 30% được phép trừ: " &
                                Format(Decimal.Parse(objParam.PO_SAL_BASIC), "n0")
                    End Select
                    Return False
                End If

            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function ApproveDisciplineSalary(lstID As List(Of Decimal)) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_PROFILE_BUSINESS.APPROVE_DISCIPLINE_SALARY"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each id In lstID
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ID = id}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                            cmd.Transaction.Rollback()
                            Return False
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function StopDisciplineSalary(lstID As List(Of Decimal)) As Boolean
        Try
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            conn.Open()
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "PKG_PROFILE_BUSINESS.STOP_DISCIPLINE_SALARY"
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            For Each id In lstID
                                cmd.Parameters.Clear()
                                Using resource As New DataAccess.OracleCommon()
                                    Dim objParam = New With {.P_ID = id}

                                    If objParam IsNot Nothing Then
                                        For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                            Dim bOut As Boolean = False
                                            Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                            If para IsNot Nothing Then
                                                cmd.Parameters.Add(para)
                                            End If
                                        Next
                                    End If
                                    cmd.ExecuteNonQuery()
                                End Using
                            Next
                            cmd.Transaction.Commit()
                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                            cmd.Transaction.Rollback()
                            Return False
                        Finally
                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                        End Try
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

#End Region

End Class
