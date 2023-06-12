Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class InsuranceRepository

    Public Sub New()
    End Sub

#Region "Khai bao dong moi bao hiem"
    Public Function GetINS_ARISING(ByVal _filter As INS_ARISINGDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_ARISINGDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.INS_ARISING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_CHANE And f.TYPE_ID.Value = 2021).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From w In Context.HU_WORKING_MAX.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        Where p.STATUS <> p.ISBHTN + p.ISBHXH + p.ISBHYT
            Dim lst = query.Select(Function(p) New INS_ARISINGDTO With {
                           .ID = p.p.ID,
                           .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                           .VN_FULLNAME = p.e.FULLNAME_VN,
                           .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                           .TITLE_NAME = p.t.NAME_VN,
                           .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                           .STAFF_RANK_NAME = p.r.NAME,
                           .ORG_ID = p.p.ORG_ID,
                           .TYPE_CHANGE_ID = p.p.TYPE_CHANE,
                           .TYPE_CHANGE_NAME = p.ot.NAME_VN,
                           .EFFECTDATE = p.p.EFFECTDATE,
                           .CONTRACT_TYPE_NAME = p.ct.NAME,
                           .DATE_CHANGE = p.p.DATE_CHANGE,
                           .ISBHXH = p.p.ISBHXH,
                           .ISBHYT = p.p.ISBHYT,
                           .ISBHTN = p.p.ISBHTN,
                           .CREATED_BY = p.p.CREATED_BY,
                           .CREATED_DATE = p.p.CREATED_DATE,
                           .CREATED_LOG = p.p.CREATED_LOG,
                           .MODIFIED_BY = p.p.MODIFIED_BY,
                           .MODIFIED_DATE = p.p.MODIFIED_DATE,
                           .MODIFIED_LOG = p.p.MODIFIED_LOG})
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If

            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                lst = lst.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.SAL_BASIC.HasValue Then
                lst = lst.Where(Function(f) f.SAL_BASIC.Value = _filter.SAL_BASIC)
            End If
            If _filter.EFFECTDATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE.Value = _filter.EFFECTDATE)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE >= _filter.FROM_DATE_SEARCH And f.EFFECTDATE <= _filter.TO_DATE_SEARCH)
            End If
            If Not String.IsNullOrEmpty(_filter.CONTRACT_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.CONTRACT_TYPE_NAME.ToLower().Contains(_filter.CONTRACT_TYPE_NAME.ToLower()))
            End If
            If _filter.DATE_CHANGE.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE.Value = _filter.DATE_CHANGE)
            End If
            If _filter.ISBHXH.HasValue Then
                lst = lst.Where(Function(f) f.ISBHXH.Value = _filter.ISBHXH)
            End If
            If _filter.ISBHYT.HasValue Then
                lst = lst.Where(Function(f) f.ISBHYT.Value = _filter.ISBHYT)
            End If
            If _filter.ISBHTN.HasValue Then
                lst = lst.Where(Function(f) f.ISBHTN.Value = _filter.ISBHTN)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_ARISINGyById(ByVal _id As Decimal?) As INS_ARISINGDTO
        Try
            Dim query = From p In Context.INS_ARISING
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_CHANE And f.TYPE_ID = 2021).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From w In Context.HU_WORKING_MAX.Where(Function(f) c.WORKING_ID = f.ID).DefaultIfEmpty
                        Where p.ID = _id
            Dim lst = query.Select(Function(p) New INS_ARISINGDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.r.NAME,
                                       .ORG_ID = p.p.ORG_ID,
                                       .SAL_BASIC = p.w.SAL_BASIC,
                                       .EFFECTDATE = p.p.EFFECTDATE,
                                       .CONTRACT_TYPE_NAME = p.ct.NAME,
                                       .DATE_CHANGE = p.p.DATE_CHANGE,
                                       .TYPE_CHANGE_ID = p.p.TYPE_CHANE,
                                       .TYPE_CHANGE_NAME = p.ot.NAME_VN,
                                       .ISBHXH = p.p.ISBHXH,
                                       .ISBHYT = p.p.ISBHYT,
                                       .ISBHTN = p.p.ISBHTN,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_ARISING(ByVal lst As List(Of INS_ARISINGDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            ' insert du lieu sang cac table INS_CHANGE, INS_INFO
            Dim objTitleData As INS_CHANGE
            Dim iCount As Integer = 0
            Dim exits As Boolean?
            Dim PERCENT_SALARY As New Decimal?
            Dim ALLOW_SUM As New Decimal?
            Dim SAL_BASIC As New Decimal?
            ' UPDATE BAN GHI DUOC SAVE TRANG THAI =0
            For index = 0 To lst.Count - 1
                Dim id = Decimal.Parse(lst(index).ID)
                Dim queryArising = (From a In Context.INS_ARISING.Where(Function(f) f.ID = id)).FirstOrDefault
                queryArising.STATUS = queryArising.ISBHTN + queryArising.ISBHXH + queryArising.ISBHYT
                queryArising.EFFECTDATE = lst(index).EFFECTDATE
                queryArising.DATE_CHANGE = lst(index).DATE_CHANGE
            Next

            For index = 0 To lst.Count - 1
                Dim id = Decimal.Parse(lst(index).ID)
                Dim queryArising = (From a In Context.INS_ARISING.Where(Function(f) f.ID = id)
                                    From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = a.EMPLOYEE_ID).DefaultIfEmpty
                                    From v In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = a.EMPLOYEE_ID).DefaultIfEmpty
                                    From w In Context.HU_WORKING.Where(Function(f) f.ID = v.WORKING_ID).DefaultIfEmpty).FirstOrDefault
                If queryArising IsNot Nothing AndAlso
                    queryArising.w IsNot Nothing Then
                    objTitleData = New INS_CHANGE
                    objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE.EntitySet.Name)
                    objTitleData.EMPLOYEE_ID = queryArising.a.EMPLOYEE_ID
                    objTitleData.ORG_ID = queryArising.e.ORG_ID
                    objTitleData.CHANGE_TYPE = 1
                    PERCENT_SALARY = queryArising.w.PERCENT_SALARY
                    ALLOW_SUM = (From h In Context.HU_WORKING_ALLOW.Where(Function(h) h.HU_WORKING_ID = queryArising.w.ID And
                                                                              h.IS_INSURRANCE = -1) Select h.AMOUNT).Sum()
                    SAL_BASIC = queryArising.w.SAL_BASIC
                    objTitleData.NEWSALARY = Decimal.Round(If(SAL_BASIC.HasValue, SAL_BASIC, 0) * If(PERCENT_SALARY.HasValue, PERCENT_SALARY, 0) / 100) + If(ALLOW_SUM.HasValue, ALLOW_SUM, 0)
                    objTitleData.CHANGE_MONTH = queryArising.a.DATE_CHANGE
                    objTitleData.EFFECTDATE = queryArising.a.EFFECTDATE
                    objTitleData.ISBHXH = queryArising.a.ISBHXH
                    objTitleData.ISBHYT = queryArising.a.ISBHYT
                    objTitleData.ISBHTN = queryArising.a.ISBHTN
                    Context.INS_CHANGE.AddObject(objTitleData)
                End If
            Next
            Dim objData As INS_INFORMATION
            For index = 0 To lst.Count - 1
                Dim id = Decimal.Parse(lst(index).ID)
                Dim emID = Decimal.Parse(lst(index).EMPLOYEE_ID)
                exits = (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = emID).Any ' kiểm tra nếu có rồi thì ko insert vào nữa
                If Not exits Then
                    Dim queryArising = (From a In Context.INS_ARISING.Where(Function(f) f.ID = id)
                                        From v In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = a.EMPLOYEE_ID).DefaultIfEmpty
                                        From w In Context.HU_WORKING.Where(Function(f) f.ID = v.WORKING_ID And f.EFFECT_DATE <= Date.Now).DefaultIfEmpty).FirstOrDefault
                    If queryArising IsNot Nothing And
                        queryArising.w IsNot Nothing Then
                        objData = New INS_INFORMATION
                        objData.ID = Utilities.GetNextSequence(Context, Context.INS_INFORMATION.EntitySet.Name)
                        objData.EMPLOYEE_ID = queryArising.a.EMPLOYEE_ID
                        PERCENT_SALARY = queryArising.w.PERCENT_SALARY
                        SAL_BASIC = queryArising.w.SAL_BASIC
                        ALLOW_SUM = (From h In Context.HU_WORKING_ALLOW.Where(Function(h) h.HU_WORKING_ID = queryArising.w.ID And h.IS_INSURRANCE = -1) Select h.AMOUNT).Sum()
                        objData.SALARY = Decimal.Round(If(SAL_BASIC.HasValue, SAL_BASIC, 0) * If(PERCENT_SALARY.HasValue, PERCENT_SALARY, 0) / 100) + If(ALLOW_SUM.HasValue, ALLOW_SUM, 0)
                        objData.ORD_ID = queryArising.a.ORG_ID
                        objData.SOFROM = queryArising.a.DATE_CHANGE
                        objData.UEFROM = queryArising.a.DATE_CHANGE
                        objData.SO = queryArising.a.ISBHXH
                        objData.HE = queryArising.a.ISBHYT
                        objData.UE = queryArising.a.ISBHTN
                        Context.INS_INFORMATION.AddObject(objData)
                    End If
                End If
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_ARISING(ByVal objLeave As INS_ARISINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try
            Dim TimeSheetDaily = (From r In Context.INS_ARISING Where r.ID = objLeave.ID).FirstOrDefault

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function Get_List_ORG(ByVal parentID As Integer)
        Dim qurey = From e In Context.HU_CONTRACT

    End Function
#End Region

#Region "Quản lý thông tin bảo hiểm"
    Public Function GetINS_INFO(ByVal _filter As INS_INFORMATIONDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFORMATIONDTO)
        Try

            Dim PERCENT_SALARY As New Decimal?
            Dim ALLOW_SUM As New Decimal?

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'lấy phụ cấp đóng bảo hiểm
            'Dim allowMount = (From p In Context.INS_INFORMATION
            '                  From ws In Context.HU_WORKING.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID)
            '                  From ps In Context.HU_WORKING_ALLOW.Where(Function(f) ws.ID = f.HU_WORKING_ID)
            '                    Where (ws.EMPLOYEE_ID = p.EMPLOYEE_ID And _
            '                    ((ps.EXPIRE_DATE Is Nothing And ps.EFFECT_DATE <= Date.Now) Or _
            '                    (ps.EXPIRE_DATE IsNot Nothing And ps.EFFECT_DATE <= Date.Now And ps.EXPIRE_DATE >= Date.Now)) And _
            '                     ps.IS_INSURRANCE = -1)
            '                    Select ps.AMOUNT).Sum

            Dim query = From p In Context.INS_INFORMATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INSCENTERKEY And f.TYPE_ID = 2025).DefaultIfEmpty
                        From w In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = p.HEWHRREGISKEY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOPRVDBOOKSTATUS And f.TYPE_ID = 2022).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HEPRVDCARDSTATUS And f.TYPE_ID = 2023).DefaultIfEmpty
                        From wr In Context.HU_WORKING_MAX.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_INFORMATIONDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .POSITIONNAME = p.t.NAME_VN,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.r.NAME,
                                       .ORGNAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .ORG_ID = p.o.ID,
                                       .CMND = p.cv.ID_NO,
                                       .NOICAP = p.cv.ID_PLACE,
                                       .NGAYCAP = p.cv.ID_DATE,
                                       .NOISINH = p.cv.BIRTH_PLACE,
                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                       .INSCENTERKEY = p.p.INSCENTERKEY,
                                       .INSCENTER_NAME = p.o1.NAME_VN,
                                       .SALARY = Decimal.Round(p.wr.SAL_BASIC * (If(p.wr.PERCENT_SALARY.HasValue, p.wr.PERCENT_SALARY, 0) / 100)),
                                       .SOFROM = p.p.SOFROM,
                                       .SOBOOKNO = p.p.SOBOOKNO,
                                       .SOPRVDBOOKDAY = p.p.SOPRVDBOOKDAY,
                                       .SOWHRPRVBOOK = p.p.SOWHRPRVBOOK,
                                       .SOPRVDBOOKSTATUS = p.p.SOPRVDBOOKSTATUS,
                                       .SOPRVDBOOKSTATUS_NAME = p.ot.NAME_VN,
                                       .NOTE_SO = p.p.NOTE_SO,
                                       .DAYPAYMENTCOMPANY = p.p.DAYPAYMENTCOMPANY,
                                       .HEFROM = p.p.HEFROM,
                                       .HETO = p.p.HETO,
                                       .HECARDNO = p.p.HECARDNO,
                                       .HEPRVDCARDDAY = p.p.HEPRVDCARDDAY,
                                       .HEWHRPRVCARD = p.p.HEWHRPRVCARD,
                                       .HEPRVDCARDSTATUS = p.p.HEPRVDCARDSTATUS,
                                       .HEPRVDCARDSTATUS_NAME = p.ot1.NAME_VN,
                                       .HEWHRREGISKEY = p.p.HEWHRREGISKEY,
                                       .HEWHRREGIS_NAME = "[" & p.w.CODE & "] - " & p.w.NAME_VN,
                                       .HECARDEFFFROM = p.p.HECARDEFFFROM,
                                       .HECARDEFFTO = p.p.HECARDEFFTO,
                                       .UETO = p.p.UETO,
                                       .UEFROM = p.p.UEFROM,
                                       .SO = p.p.SO,
                                       .HE = p.p.HE,
                                       .UE = p.p.UE,
                                       .REGISTER = p.p.REGISTER,
                                       .CONTRACTNO = p.p.CONTRACTNO,
                                       .SENIORITY = p.p.SENIORITY,
                                       .TOTAL_TIME_INS_BEFOR = p.p.TOTAL_TIME_INS_BEFOR,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257)
            'End If
            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.SOFROM >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.SOFROM <= _filter.TO_DATE_SEARCH)
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.POSITIONNAME) Then
                lst = lst.Where(Function(f) f.POSITIONNAME.ToLower().Contains(_filter.POSITIONNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORGNAME) Then
                lst = lst.Where(Function(f) f.ORGNAME.ToLower().Contains(_filter.ORGNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.INSCENTER_NAME) Then
                lst = lst.Where(Function(f) f.INSCENTER_NAME.ToLower().Contains(_filter.INSCENTER_NAME.ToLower()))
            End If
            If _filter.SALARY.HasValue Then
                lst = lst.Where(Function(f) f.SALARY.Value = _filter.SALARY)
            End If
            If _filter.SO.HasValue Then
                lst = lst.Where(Function(f) f.SO.Value = _filter.SO)
            End If
            If _filter.HE.HasValue Then
                lst = lst.Where(Function(f) f.HE.Value = _filter.HE)
            End If
            If _filter.UE.HasValue Then
                lst = lst.Where(Function(f) f.UE.Value = _filter.UE)
            End If
            If _filter.SOFROM.HasValue Then
                lst = lst.Where(Function(f) f.SOFROM.Value = _filter.SOFROM)
            End If
            If _filter.SOTO.HasValue Then
                lst = lst.Where(Function(f) f.SOTO.Value = _filter.SOTO)
            End If
            If Not String.IsNullOrEmpty(_filter.SOBOOKNO) Then
                lst = lst.Where(Function(f) f.SOBOOKNO.ToLower().Contains(_filter.SOBOOKNO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SOPRVDBOOKSTATUS_NAME) Then
                lst = lst.Where(Function(f) f.SOPRVDBOOKSTATUS_NAME.ToLower().Contains(_filter.SOPRVDBOOKSTATUS_NAME.ToLower()))
            End If
            If _filter.SOPRVDBOOKDAY.HasValue Then
                lst = lst.Where(Function(f) f.SOPRVDBOOKDAY.Value = _filter.SOPRVDBOOKDAY)
            End If
            If _filter.DAYPAYMENTCOMPANY.HasValue Then
                lst = lst.Where(Function(f) f.DAYPAYMENTCOMPANY.Value = _filter.DAYPAYMENTCOMPANY)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE_SO) Then
                lst = lst.Where(Function(f) f.NOTE_SO.ToLower().Contains(_filter.NOTE_SO.ToLower()))
            End If
            If _filter.HEFROM.HasValue Then
                lst = lst.Where(Function(f) f.HEFROM.Value = _filter.HEFROM)
            End If
            If _filter.HETO.HasValue Then
                lst = lst.Where(Function(f) f.HETO.Value = _filter.HETO)
            End If
            If Not String.IsNullOrEmpty(_filter.HECARDNO) Then
                lst = lst.Where(Function(f) f.HECARDNO.ToLower().Contains(_filter.HECARDNO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.HEPRVDCARDSTATUS_NAME) Then
                lst = lst.Where(Function(f) f.HEPRVDCARDSTATUS_NAME.ToLower().Contains(_filter.HEPRVDCARDSTATUS_NAME.ToLower()))
            End If
            If _filter.HECARDEFFFROM.HasValue Then
                lst = lst.Where(Function(f) f.HECARDEFFFROM.Value = _filter.HECARDEFFFROM)
            End If
            If _filter.HECARDEFFTO.HasValue Then
                lst = lst.Where(Function(f) f.HECARDEFFTO.Value = _filter.HECARDEFFTO)
            End If
            If Not String.IsNullOrEmpty(_filter.HEWHRREGIS_NAME) Then
                lst = lst.Where(Function(f) f.HEWHRREGIS_NAME.ToLower().Contains(_filter.HEWHRREGIS_NAME.ToLower()))
            End If
            If _filter.UEFROM.HasValue Then
                lst = lst.Where(Function(f) f.UEFROM.Value = _filter.UEFROM)
            End If
            If _filter.UETO.HasValue Then
                lst = lst.Where(Function(f) f.UETO.Value = _filter.UETO)
            End If
            If _filter.TOTAL_TIME_INS_BEFOR.HasValue Then
                lst = lst.Where(Function(f) f.TOTAL_TIME_INS_BEFOR.Value = _filter.TOTAL_TIME_INS_BEFOR)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_INFOById(ByVal _id As Decimal?) As INS_INFORMATIONDTO
        Try

            Dim query = From p In Context.INS_INFORMATION
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INSCENTERKEY And f.TYPE_ID = 2025).DefaultIfEmpty
                        From w In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = p.HEWHRREGISKEY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOPRVDBOOKSTATUS And f.TYPE_ID = 2022).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HEPRVDCARDSTATUS And f.TYPE_ID = 2023).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From wr In Context.HU_WORKING_MAX.Where(Function(f) p.EMPLOYEE_ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        Where p.ID = _id

            Dim allowMount = (From p In Context.INS_INFORMATION
                              From ps In Context.HU_WORKING_ALLOW
                              From ws In Context.HU_WORKING.Where(Function(f) f.ID = ps.HU_WORKING_ID)
                              Where ws.EMPLOYEE_ID = p.EMPLOYEE_ID And _
                              ((ps.EXPIRE_DATE Is Nothing And ps.EFFECT_DATE <= Date.Now) Or _
                              (ps.EXPIRE_DATE IsNot Nothing And ps.EFFECT_DATE <= Date.Now And ps.EXPIRE_DATE >= Date.Now)) And _
                               ps.IS_INSURRANCE = -1
                              Select ps.AMOUNT).Sum

            Dim lst = query.Select(Function(p) New INS_INFORMATIONDTO With {
                                        .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .POSITIONNAME = p.t.NAME_VN,
                                       .ORGNAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = p.r.NAME,
                                       .ORG_ID = p.o.ID,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .CMND = p.cv.ID_NO,
                                       .NOICAP = p.cv.ID_PLACE,
                                       .NGAYCAP = p.cv.ID_DATE,
                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                        .NOISINH = p.cv.BIRTH_PLACE,
                                       .INSCENTERKEY = p.p.INSCENTERKEY,
                                       .INSCENTER_NAME = p.o1.NAME_VN,
                                       .SALARY = Decimal.Round(p.wr.SAL_BASIC * If(p.wr.PERCENT_SALARY.HasValue, p.wr.PERCENT_SALARY, 0) / 100),
                                       .SOFROM = p.p.SOFROM,
                                       .SOBOOKNO = p.p.SOBOOKNO,
                                       .SOPRVDBOOKDAY = p.p.SOPRVDBOOKDAY,
                                       .SOWHRPRVBOOK = p.p.SOWHRPRVBOOK,
                                       .SOPRVDBOOKSTATUS = p.p.SOPRVDBOOKSTATUS,
                                       .SOPRVDBOOKSTATUS_NAME = p.ot.NAME_VN,
                                       .NOTE_SO = p.p.NOTE_SO,
                                       .DAYPAYMENTCOMPANY = p.p.DAYPAYMENTCOMPANY,
                                       .HEFROM = p.p.HEFROM,
                                       .HETO = p.p.HETO,
                                       .HECARDNO = p.p.HECARDNO,
                                       .HEPRVDCARDDAY = p.p.HEPRVDCARDDAY,
                                       .HEWHRPRVCARD = p.p.HEWHRPRVCARD,
                                       .HEPRVDCARDSTATUS = p.p.HEPRVDCARDSTATUS,
                                       .HEPRVDCARDSTATUS_NAME = p.ot1.NAME_VN,
                                       .HEWHRREGISKEY = p.p.HEWHRREGISKEY,
                                       .HEWHRREGIS_NAME = "[" & p.w.CODE & "] - " & p.w.NAME_VN,
                                       .HECARDEFFFROM = p.p.HECARDEFFFROM,
                                       .HECARDEFFTO = p.p.HECARDEFFTO,
                                       .UETO = p.p.UETO,
                                       .UEFROM = p.p.UEFROM,
                                       .SO = p.p.SO,
                                       .HE = p.p.HE,
                                       .UE = p.p.UE,
                                       .REGISTER = p.p.REGISTER,
                                       .CONTRACTNO = p.p.CONTRACTNO,
                                       .SENIORITY = p.p.SENIORITY,
                                       .TOTAL_TIME_INS_BEFOR = p.p.TOTAL_TIME_INS_BEFOR,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_INFO(ByVal objData As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            'Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToLower().Contains(objData.EMPLOYEE_CODE.ToLower())).FirstOrDefault
            Dim exits As Boolean?
            exits = (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = objData.EMPLOYEE_ID).Any
            ' kiểm tra nếu chưa cho thì cho phép thêm mới và import mới. nếu có rồi chỉ cho phép update các trường trong file imprort vao.
            If Not exits Then
                Dim objDataData As New INS_INFORMATION
                objDataData.ID = Utilities.GetNextSequence(Context, Context.INS_INFORMATION.EntitySet.Name)
                objDataData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                objDataData.INSCENTERKEY = objData.INSCENTERKEY
                objDataData.SALARY = objData.SALARY
                objDataData.POSITIONNAME = objData.POSITIONNAME
                objDataData.ORGNAME = objData.ORGNAME
                objDataData.SOFROM = objData.SOFROM
                objDataData.SOBOOKNO = objData.SOBOOKNO
                objDataData.SOPRVDBOOKDAY = objData.SOPRVDBOOKDAY
                objDataData.SOWHRPRVBOOK = objData.SOWHRPRVBOOK
                objDataData.SOPRVDBOOKSTATUS = objData.SOPRVDBOOKSTATUS
                objDataData.NOTE_SO = objData.NOTE_SO
                objDataData.DAYPAYMENTCOMPANY = objData.DAYPAYMENTCOMPANY
                objDataData.HEFROM = objData.HEFROM
                objDataData.HETO = objData.HETO
                objDataData.HECARDNO = objData.HECARDNO
                objDataData.HEPRVDCARDDAY = objData.HEPRVDCARDDAY
                objDataData.HEWHRPRVCARD = objData.HEWHRPRVCARD
                objDataData.HEPRVDCARDSTATUS = objData.HEPRVDCARDSTATUS
                objDataData.HEWHRREGISKEY = objData.HEWHRREGISKEY
                objDataData.HECARDEFFFROM = objData.HECARDEFFFROM
                objDataData.HECARDEFFTO = objData.HECARDEFFTO
                objDataData.UEFROM = objData.UEFROM
                objDataData.UETO = objData.UETO
                objDataData.SO = objData.SO
                objDataData.HE = objData.HE
                objDataData.UE = objData.UE
                objDataData.TOTAL_TIME_INS_BEFOR = objData.TOTAL_TIME_INS_BEFOR
                objDataData.REGISTER = objData.REGISTER
                objDataData.ORD_ID = objData.ORG_ID
                Context.INS_INFORMATION.AddObject(objDataData)
            Else
                Dim objDataData As New INS_INFORMATION With {.EMPLOYEE_ID = objData.EMPLOYEE_ID}
                objDataData = (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = objData.EMPLOYEE_ID).SingleOrDefault
                objDataData.SOBOOKNO = objData.SOBOOKNO
                objDataData.SOPRVDBOOKDAY = objData.SOPRVDBOOKDAY
                objDataData.DAYPAYMENTCOMPANY = objData.DAYPAYMENTCOMPANY
                objDataData.HECARDNO = objData.HECARDNO
                objDataData.HECARDEFFFROM = objData.HECARDEFFFROM
                objDataData.HECARDEFFTO = objData.HECARDEFFTO
                objDataData.HEWHRREGISKEY = objData.HEWHRREGISKEY
                objDataData.SOPRVDBOOKSTATUS = objData.SOPRVDBOOKSTATUS
                objDataData.HEPRVDCARDSTATUS = objData.HEPRVDCARDSTATUS
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_INFO(ByVal objData As INS_INFORMATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDataData As New INS_INFORMATION With {.ID = objData.ID}
        Try
            objDataData = (From p In Context.INS_INFORMATION Where p.ID = objData.ID).SingleOrDefault
            Dim exists = (From r In Context.INS_INFORMATION Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.ID = objData.ID).Any
            If exists Then
                objDataData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                objDataData.INSCENTERKEY = objData.INSCENTERKEY
                objDataData.SALARY = objData.SALARY
                objDataData.POSITIONNAME = objData.POSITIONNAME
                objDataData.ORGNAME = objData.ORGNAME
                objDataData.SOFROM = objData.SOFROM
                objDataData.SOBOOKNO = objData.SOBOOKNO
                objDataData.SOPRVDBOOKDAY = objData.SOPRVDBOOKDAY
                objDataData.SOWHRPRVBOOK = objData.SOWHRPRVBOOK
                objDataData.SOPRVDBOOKSTATUS = objData.SOPRVDBOOKSTATUS
                objDataData.NOTE_SO = objData.NOTE_SO
                objDataData.DAYPAYMENTCOMPANY = objData.DAYPAYMENTCOMPANY
                objDataData.HEFROM = objData.HEFROM
                objDataData.HETO = objData.HETO
                objDataData.HECARDNO = objData.HECARDNO
                objDataData.HEPRVDCARDDAY = objData.HEPRVDCARDDAY
                objDataData.HEWHRPRVCARD = objData.HEWHRPRVCARD
                objDataData.HEPRVDCARDSTATUS = objData.HEPRVDCARDSTATUS
                objDataData.HEWHRREGISKEY = objData.HEWHRREGISKEY
                objDataData.HECARDEFFFROM = objData.HECARDEFFFROM
                objDataData.HECARDEFFTO = objData.HECARDEFFTO
                objDataData.UEFROM = objData.UEFROM
                objDataData.UETO = objData.UETO
                objDataData.SO = objData.SO
                objDataData.HE = objData.HE
                objDataData.UE = objData.UE
                objDataData.TOTAL_TIME_INS_BEFOR = objData.TOTAL_TIME_INS_BEFOR
                objDataData.REGISTER = objData.REGISTER
                objDataData.ORD_ID = objData.ORG_ID
                Context.SaveChanges(log)
                gID = objDataData.ID
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function DeleteINS_INFO(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of INS_INFORMATION)
        Try
            lstl = (From p In Context.INS_INFORMATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.INS_INFORMATION.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function GetInfoPrint(ByVal LISTID As String) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETINFOPRINT",
                                           New With {.LISTID = LISTID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetLuongBH(ByVal p_EMPLOYEE_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.SPS_TIENLUONG_CHEDO",
                                           New With {.P_EMPLOYEEID = p_EMPLOYEE_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetEmployeeID(ByVal employee_code As String) As DataTable
        Try

            Dim query = From p In Context.HU_EMPLOYEE
                        Where p.EMPLOYEE_CODE = employee_code
            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .EMPLOYEE_ID = p.ID,
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetAllowanceTotalByDate(ByVal employeeID As Decimal) As Decimal?
        Dim dInsuran As Decimal?
        dInsuran = (From p In Context.HU_WORKING_ALLOW
                    From w In Context.HU_WORKING.Where(Function(f) f.ID = p.HU_WORKING_ID)
                    Where w.EMPLOYEE_ID = employeeID And _
                    ((p.EXPIRE_DATE Is Nothing And p.EFFECT_DATE <= Date.Now) Or _
                     (p.EXPIRE_DATE IsNot Nothing And p.EFFECT_DATE <= Date.Now And p.EXPIRE_DATE >= Date.Now)) And _
                    p.IS_INSURRANCE = -1
                    Select p.AMOUNT).Sum
        Return dInsuran
    End Function
#End Region

#Region "Quan ly thong tin bao hiem cu"

    Public Function GetINS_INFOOLD(ByVal _filter As INS_INFOOLDDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_INFOOLDDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.INS_INFOOLD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEEID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From w In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = p.HEWHRREGISKEY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOSTATUS_BHXH And f.TYPE_ID = 2022).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HESTATUS_BHYT And f.TYPE_ID = 2023).DefaultIfEmpty

            Dim ls = query.Select(Function(p) New INS_INFOOLDDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEEID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.r.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.p.ORG_ID,
                                                                       .ORG_ID_INS = p.p.ORG_ID_INS,
                                                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                                                       .NOISINH = p.cv.BIRTH_PLACE,
                                                                       .CMND = p.cv.ID_NO,
                                                                       .NOICAP = p.cv.ID_PLACE,
                                                                       .SALARY = p.p.SALARY,
                                                                       .SOBOOKNO = p.p.SOBOOKNO,
                                                                       .SOFROM = p.p.SOFROM,
                                                                       .SOTO = p.p.SOTO,
                                                                       .SOPRVDBOOKDAY = p.p.SOPRVDBOOKDAY,
                                                                       .SOSTATUS_BHXH = p.p.SOSTATUS_BHXH,
                                                                       .SOSTATUS_BHXH_NAME = p.ot.NAME_VN,
                                                                       .SOWHRPRVBOOK = p.p.SOWHRPRVBOOK,
                                                                       .NOTE_BHXH = p.p.NOTE_BHXH,
                                                                       .DAYPAYMENTCOMPANY = p.p.DAYPAYMENTCOMPANY,
                                                                       .ISBHXH = p.p.ISBHXH,
                                                                       .HECARDEFFFROM = p.p.HECARDEFFFROM,
                                                                       .HECARDEFFTO = p.p.HECARDEFFTO,
                                                                       .HECARDNO = p.p.HECARDNO,
                                                                       .HEFROM = p.p.HEFROM,
                                                                       .HEPRVDCARDDAY = p.p.HEPRVDCARDDAY,
                                                                       .HESTATUS_BHYT = p.p.HESTATUS_BHYT,
                                                                       .HESTATUS_BHYT_NAME = p.ot1.NAME_VN,
                                                                       .HETO = p.p.HETO,
                                                                       .ISBHYT = p.p.ISBHYT,
                                                                       .HEWHRREGISKEY = p.p.HEWHRREGISKEY,
                                                                       .HEWHRREGIS_NAME = p.w.NAME_VN,
                                                                       .HEWHRPRVCARD = p.p.HEWHRPRVCARD,
                                                                       .ISBHTN = p.p.ISBHTN,
                                                                       .UEFROM = p.p.UEFROM,
                                                                       .UETO = p.p.UETO,
                                                                       .WORK_STATUS = p.e.WORK_STATUS,
                                                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})
            Dim dateNow = Date.Now.Date
            'If _filter.IS_TERMINATE Then
            '    ls = ls.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    ls = ls.Where(Function(f) f.WORK_STATUS <> 257)
            'End If
            If Not _filter.IS_TERMINATE Then
                ls = ls.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If _filter.FROM_DATE_SEARCH.HasValue Then
                ls = ls.Where(Function(f) f.SOFROM >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                ls = ls.Where(Function(f) f.SOTO <= _filter.TO_DATE_SEARCH)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                ls = ls.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                ls = ls.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                ls = ls.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                ls = ls.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                ls = ls.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_ID_INS) Then
                ls = ls.Where(Function(f) f.ORG_ID_INS.ToLower().Contains(_filter.ORG_ID_INS.ToLower()))
            End If
            If _filter.SALARY.HasValue Then
                ls = ls.Where(Function(f) f.SALARY.Value = _filter.SALARY)
            End If
            If _filter.SO.HasValue Then
                ls = ls.Where(Function(f) f.SO.Value = _filter.SO)
            End If
            If _filter.HE.HasValue Then
                ls = ls.Where(Function(f) f.HE.Value = _filter.HE)
            End If
            If _filter.UE.HasValue Then
                ls = ls.Where(Function(f) f.UE.Value = _filter.UE)
            End If
            If _filter.SOFROM.HasValue Then
                ls = ls.Where(Function(f) f.SOFROM.Value = _filter.SOFROM)
            End If
            If _filter.SOTO.HasValue Then
                ls = ls.Where(Function(f) f.SOTO.Value = _filter.SOTO)
            End If
            If Not String.IsNullOrEmpty(_filter.SOBOOKNO) Then
                ls = ls.Where(Function(f) f.SOBOOKNO.ToLower().Contains(_filter.SOBOOKNO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SOSTATUS_BHXH_NAME) Then
                ls = ls.Where(Function(f) f.SOSTATUS_BHXH_NAME.ToLower().Contains(_filter.SOSTATUS_BHXH_NAME.ToLower()))
            End If
            If _filter.SOPRVDBOOKDAY.HasValue Then
                ls = ls.Where(Function(f) f.SOPRVDBOOKDAY.Value = _filter.SOPRVDBOOKDAY)
            End If
            If _filter.DAYPAYMENTCOMPANY.HasValue Then
                ls = ls.Where(Function(f) f.DAYPAYMENTCOMPANY.Value = _filter.DAYPAYMENTCOMPANY)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE_BHXH) Then
                ls = ls.Where(Function(f) f.NOTE_BHXH.ToLower().Contains(_filter.NOTE_BHXH.ToLower()))
            End If
            If _filter.HEFROM.HasValue Then
                ls = ls.Where(Function(f) f.HEFROM.Value = _filter.HEFROM)
            End If
            If _filter.HETO.HasValue Then
                ls = ls.Where(Function(f) f.HETO.Value = _filter.HETO)
            End If
            If Not String.IsNullOrEmpty(_filter.HECARDNO) Then
                ls = ls.Where(Function(f) f.HECARDNO.ToLower().Contains(_filter.HECARDNO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.HESTATUS_BHYT_NAME) Then
                ls = ls.Where(Function(f) f.HESTATUS_BHYT_NAME.ToLower().Contains(_filter.HESTATUS_BHYT_NAME.ToLower()))
            End If
            If _filter.HECARDEFFFROM.HasValue Then
                ls = ls.Where(Function(f) f.HECARDEFFFROM.Value = _filter.HECARDEFFFROM)
            End If
            If _filter.HECARDEFFTO.HasValue Then
                ls = ls.Where(Function(f) f.HECARDEFFTO.Value = _filter.HECARDEFFTO)
            End If
            If Not String.IsNullOrEmpty(_filter.HEWHRREGIS_NAME) Then
                ls = ls.Where(Function(f) f.HEWHRREGIS_NAME.ToLower().Contains(_filter.HEWHRREGIS_NAME.ToLower()))
            End If
            If _filter.UEFROM.HasValue Then
                ls = ls.Where(Function(f) f.UEFROM.Value = _filter.UEFROM)
            End If
            If _filter.UETO.HasValue Then
                ls = ls.Where(Function(f) f.UETO.Value = _filter.UETO)
            End If

            ls = ls.OrderBy(Sorts)
            Total = ls.Count
            ls = ls.Skip(PageIndex * PageSize).Take(PageSize)
            Return ls.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_INFOOLDById(ByVal _id As Decimal?) As INS_INFOOLDDTO
        Try

            Dim query = From p In Context.INS_INFOOLD
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEEID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From w In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = p.HEWHRREGISKEY).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SOSTATUS_BHXH And f.TYPE_ID = 2022).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.HESTATUS_BHYT And f.TYPE_ID = 2023).DefaultIfEmpty
                        Where p.ID = _id

            Dim ls = query.Select(Function(p) New INS_INFOOLDDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEEID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.r.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.p.ORG_ID,
                                                                       .NOICAP = p.cv.ID_PLACE,
                                                                       .SALARY = p.p.SALARY,
                                                                       .ORG_ID_INS = p.p.ORG_ID_INS,
                                                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                                                       .CMND = p.cv.ID_NO,
                                                                       .NOISINH = p.cv.BIRTH_PLACE,
                                                                       .SOBOOKNO = p.p.SOBOOKNO,
                                                                       .SOFROM = p.p.SOFROM,
                                                                       .SOTO = p.p.SOTO,
                                                                       .SOPRVDBOOKDAY = p.p.SOPRVDBOOKDAY,
                                                                       .SOSTATUS_BHXH = p.p.SOSTATUS_BHXH,
                                                                       .SOSTATUS_BHXH_NAME = p.ot.NAME_VN,
                                                                       .SOWHRPRVBOOK = p.p.SOWHRPRVBOOK,
                                                                       .NOTE_BHXH = p.p.NOTE_BHXH,
                                                                       .DAYPAYMENTCOMPANY = p.p.DAYPAYMENTCOMPANY,
                                                                       .ISBHXH = p.p.ISBHXH,
                                                                       .HECARDEFFFROM = p.p.HECARDEFFFROM,
                                                                       .HECARDEFFTO = p.p.HECARDEFFTO,
                                                                       .HECARDNO = p.p.HECARDNO,
                                                                       .HEFROM = p.p.HEFROM,
                                                                       .HEPRVDCARDDAY = p.p.HEPRVDCARDDAY,
                                                                       .HESTATUS_BHYT = p.p.HESTATUS_BHYT,
                                                                       .HESTATUS_BHYT_NAME = p.ot1.NAME_VN,
                                                                       .HETO = p.p.HETO,
                                                                       .ISBHYT = p.p.ISBHYT,
                                                                       .HEWHRREGISKEY = p.p.HEWHRREGISKEY,
                                                                       .HEWHRREGIS_NAME = p.w.NAME_VN,
                                                                       .HEWHRPRVCARD = p.p.HEWHRPRVCARD,
                                                                       .ISBHTN = p.p.ISBHTN,
                                                                       .UEFROM = p.p.UEFROM,
                                                                       .UETO = p.p.UETO,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return ls
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeById(ByVal _id As Decimal?) As EmployeeDTO
        Try

            Dim query = From e In Context.HU_EMPLOYEE
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From r In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From i In Context.INS_INFORMATION.Where(Function(F) F.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        Where e.ID = _id

            Dim ls = query.Select(Function(p) New EmployeeDTO With {
                                                                       .EMPLOYEE_ID = p.e.ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .TITLE_ID = p.e.TITLE_ID,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .STAFF_RANK_ID = p.e.STAFF_RANK_ID,
                                                                       .STAFF_RANK_NAME = p.r.NAME,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_ID = p.o.ID,
                                                                       .SO = p.i.SO,
                                                                       .HE = p.i.HE,
                                                                       .UE = p.i.UE,
                                                                       .NOICAP = p.cv.ID_PLACE,
                                                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                                                       .CMND = p.cv.ID_NO,
                                                                       .NOISINH = p.cv.BIRTH_PLACE}).FirstOrDefault
            Return ls
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByIdProcess(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GET_PROCESS_BYEMPID",
                                           New With {.P_EMPLOYEEID = P_EMPLOYEEID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_INFOOLD(ByVal objLeave As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objTitleData As New INS_INFOOLD
            Dim iCount As Integer = 0
            Dim org_id As Decimal?
            Try
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_INFOOLD.EntitySet.Name)
                org_id = (From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = objLeave.EMPLOYEE_ID)).FirstOrDefault().ORG_ID
                objTitleData.EMPLOYEEID = objLeave.EMPLOYEE_ID
                objTitleData.ORG_ID = org_id
                objTitleData.SALARY = objLeave.SALARY
                objTitleData.ORG_ID_INS = objLeave.ORG_ID_INS
                objTitleData.SOBOOKNO = objLeave.SOBOOKNO
                objTitleData.SOFROM = objLeave.SOFROM
                objTitleData.SOTO = objLeave.SOTO
                objTitleData.SOPRVDBOOKDAY = objLeave.SOPRVDBOOKDAY
                objTitleData.SOSTATUS_BHXH = objLeave.SOSTATUS_BHXH
                objTitleData.SOWHRPRVBOOK = objLeave.SOWHRPRVBOOK
                objTitleData.NOTE_BHXH = objLeave.NOTE_BHXH
                objTitleData.DAYPAYMENTCOMPANY = objLeave.DAYPAYMENTCOMPANY
                objTitleData.ISBHXH = objLeave.ISBHXH
                objTitleData.HECARDEFFFROM = objLeave.HECARDEFFFROM
                objTitleData.HECARDEFFTO = objLeave.HECARDEFFTO
                objTitleData.HECARDNO = objLeave.HECARDNO
                objTitleData.HEFROM = objLeave.HEFROM
                objTitleData.HETO = objLeave.HETO
                objTitleData.HEPRVDCARDDAY = objLeave.HEPRVDCARDDAY
                objTitleData.HESTATUS_BHYT = objLeave.HESTATUS_BHYT
                objTitleData.HEWHRPRVCARD = objLeave.HEWHRPRVCARD
                objTitleData.HEWHRREGISKEY = objLeave.HEWHRREGISKEY
                objTitleData.ISBHYT = objLeave.ISBHYT
                objTitleData.ISBHTN = objLeave.ISBHTN
                objTitleData.UEFROM = objLeave.UEFROM
                objTitleData.UETO = objLeave.UETO
                Context.INS_INFOOLD.AddObject(objTitleData)
                Context.SaveChanges(log)
                gID = objTitleData.ID
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
                Throw ex
            End Try
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_INFOOLD(ByVal objLeave As INS_INFOOLDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_INFOOLD With {.ID = objLeave.ID}
        Dim org_id As Decimal?
        Try
            objTitleData = (From p In Context.INS_INFOOLD Where p.ID = objTitleData.ID).SingleOrDefault
            org_id = (From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = objLeave.EMPLOYEE_ID)).FirstOrDefault().ORG_ID
            objTitleData.ORG_ID = org_id
            objTitleData.EMPLOYEEID = objLeave.EMPLOYEE_ID
            objTitleData.SOBOOKNO = objLeave.SOBOOKNO
            objTitleData.SOFROM = objLeave.SOFROM
            objTitleData.SOTO = objLeave.SOTO
            objTitleData.SALARY = objLeave.SALARY
            objTitleData.ORG_ID_INS = objLeave.ORG_ID_INS
            objTitleData.SOPRVDBOOKDAY = objLeave.SOPRVDBOOKDAY
            objTitleData.SOSTATUS_BHXH = objLeave.SOSTATUS_BHXH
            objTitleData.SOWHRPRVBOOK = objLeave.SOWHRPRVBOOK
            objTitleData.NOTE_BHXH = objLeave.NOTE_BHXH
            objTitleData.DAYPAYMENTCOMPANY = objLeave.DAYPAYMENTCOMPANY
            objTitleData.ISBHXH = objLeave.ISBHXH
            objTitleData.HECARDEFFFROM = objLeave.HECARDEFFFROM
            objTitleData.HECARDEFFTO = objLeave.HECARDEFFTO
            objTitleData.HECARDNO = objLeave.HECARDNO
            objTitleData.HEFROM = objLeave.HEFROM
            objTitleData.HETO = objLeave.HETO
            objTitleData.HEPRVDCARDDAY = objLeave.HEPRVDCARDDAY
            objTitleData.HESTATUS_BHYT = objLeave.HESTATUS_BHYT
            objTitleData.HEWHRPRVCARD = objLeave.HEWHRPRVCARD
            objTitleData.HEWHRREGISKEY = objLeave.HEWHRREGISKEY
            objTitleData.ISBHYT = objLeave.ISBHYT
            objTitleData.ISBHTN = objLeave.ISBHTN
            objTitleData.UEFROM = objLeave.UEFROM
            objTitleData.UETO = objLeave.UETO
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function DeleteINS_INFOOLD(ByVal lstID As List(Of INS_INFOOLDDTO)) As Boolean
        Dim lstl As INS_INFOOLD
        Dim id As Decimal = 0
        Try

            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                lstl = (From p In Context.INS_INFOOLD Where id = p.ID).FirstOrDefault
                If Not lstl Is Nothing Then
                    Dim details = (From r In Context.INS_INFOOLD Where r.ID = lstl.ID).ToList
                    For index1 = 0 To details.Count - 1
                        Context.INS_INFOOLD.DeleteObject(details(index1))
                    Next
                End If
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteINS_INFOOLD")
            Throw ex
        End Try
    End Function


#End Region

#Region "Quan ly bien dong bao hiem"

    Public Function GetINS_CHANGE(ByVal _filter As INS_CHANGEDTO,
                                     ByVal _param As PARAMDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGEDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.INS_CHANGE
                        From i In Context.INS_INFORMATION.Where(Function(F) p.EMPLOYEE_ID = F.EMPLOYEE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ORG_ID_INS And f.TYPE_ID = 2025).DefaultIfEmpty
                        From ot In Context.INS_CHANGE_TYPE.Where(Function(f) f.ID = p.CHANGE_TYPE).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)

            Dim ls = query.Select(Function(p) New INS_CHANGEDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .CMND = p.cv.ID_NO,
                                                                       .NOICAP = p.cv.PASS_PLACE,
                                                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                                                       .NOISINH = p.cv.BIRTH_PLACE,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.p.ORG_ID,
                                                                       .ORG_NAMEBH = p.o1.NAME_EN,
                                                                       .CHANGE_TYPE_NAME = p.ot.ARISING_NAME,
                                                                       .CHANGE_TYPE = p.p.CHANGE_TYPE,
                                                                       .OLDSALARY = p.p.OLDSALARY,
                                                                       .NEWSALARY = p.p.NEWSALARY,
                                                                       .ISBHXH = p.i.SO,
                                                                       .ISBHYT = p.i.HE,
                                                                       .ISBHTN = p.i.UE,
                                                                       .CHANGE_MONTH = p.p.CHANGE_MONTH,
                                                                       .RETURN_DATEBHXH = p.p.RETURN_DATEBHXH,
                                                                       .RETURN_DATEBHYT = p.p.RETURN_DATEBHYT,
                                                                       .NOTE = p.p.NOTE,
                                                                       .CLTFRMMONTH = p.p.CLTFRMMONTH,
                                                                       .CLTTOMONTH = p.p.CLTTOMONTH,
                                                                       .CLTBHXH = p.p.CLTBHXH,
                                                                       .CLTBHYT = p.p.CLTBHYT,
                                                                       .CLTBHTN = p.p.CLTBHTN,
                                                                       .REPFRMMONTH = p.p.REPFRMMONTH,
                                                                       .REPTOMONTH = p.p.REPTOMONTH,
                                                                       .REPBHXH = p.p.REPBHXH,
                                                                       .REPBHYT = p.p.REPBHYT,
                                                                       .REPBHTN = p.p.REPBHTN,
                                                                       .WORK_STATUS = p.e.WORK_STATUS,
                                                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            Dim dateNow = Date.Now.Date
            'If _filter.IS_TERMINATE Then
            '    ls = ls.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    ls = ls.Where(Function(f) f.WORK_STATUS <> 257)
            'End If

            If Not _filter.IS_TERMINATE Then
                ls = ls.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                ls = ls.Where(Function(f) f.CHANGE_MONTH >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                ls = ls.Where(Function(f) f.CHANGE_MONTH <= _filter.TO_DATE_SEARCH)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                ls = ls.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()) Or f.VN_FULLNAME.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.VN_FULLNAME) Then
                ls = ls.Where(Function(f) f.VN_FULLNAME.ToLower().Contains(_filter.VN_FULLNAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                ls = ls.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                ls = ls.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If _filter.ISBHXH.HasValue Then
                ls = ls.Where(Function(f) f.ISBHXH.Value = _filter.ISBHXH)
            End If
            If _filter.ISBHYT.HasValue Then
                ls = ls.Where(Function(f) f.ISBHYT.Value = _filter.ISBHYT)
            End If
            If _filter.ISBHTN.HasValue Then
                ls = ls.Where(Function(f) f.ISBHTN.Value = _filter.ISBHTN)
            End If
            If Not String.IsNullOrEmpty(_filter.CHANGE_TYPE_NAME) Then
                ls = ls.Where(Function(f) f.CHANGE_TYPE_NAME.ToLower().Contains(_filter.CHANGE_TYPE_NAME.ToLower()))
            End If
            If _filter.CHANGE_MONTH.HasValue Then
                'Thay đổi
                'Dim iYear As Integer = 0
                'Dim iMonth As Integer = 0
                'iYear = Convert.ToInt32(Format(_filter.CHANGE_MONTH, "yyyy"))
                'iMonth = Convert.ToInt32(Format(_filter.CHANGE_MONTH, "MM"))
                'ls = ls.Where(Function(f) f.CHANGE_MONTH.Value.Year = iYear And f.CHANGE_MONTH.Value.Month = iMonth)
                'End thay đổi
                'Cũ
                ls = ls.Where(Function(f) f.CHANGE_MONTH.Value = _filter.CHANGE_MONTH.Value)
            End If
            If _filter.OLDSALARY.HasValue Then
                ls = ls.Where(Function(f) f.OLDSALARY.Value = _filter.OLDSALARY)
            End If
            If _filter.NEWSALARY.HasValue Then
                ls = ls.Where(Function(f) f.NEWSALARY.Value = _filter.NEWSALARY)
            End If
            If _filter.RETURN_DATEBHXH.HasValue Then
                ls = ls.Where(Function(f) f.RETURN_DATEBHXH.Value = _filter.RETURN_DATEBHXH)
            End If
            If _filter.RETURN_DATEBHYT.HasValue Then
                ls = ls.Where(Function(f) f.RETURN_DATEBHYT.Value = _filter.RETURN_DATEBHYT)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                ls = ls.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If _filter.CLTFRMMONTH.HasValue Then
                ls = ls.Where(Function(f) f.CLTFRMMONTH.Value = _filter.CLTFRMMONTH)
            End If
            If _filter.CLTTOMONTH.HasValue Then
                ls = ls.Where(Function(f) f.CLTTOMONTH.Value = _filter.CLTTOMONTH)
            End If
            If _filter.CLTBHXH.HasValue Then
                ls = ls.Where(Function(f) f.CLTBHXH.Value = _filter.CLTBHXH)
            End If
            If _filter.CLTBHYT.HasValue Then
                ls = ls.Where(Function(f) f.CLTBHYT.Value = _filter.CLTBHYT)
            End If
            If _filter.CLTBHTN.HasValue Then
                ls = ls.Where(Function(f) f.CLTBHTN.Value = _filter.CLTBHTN)
            End If
            If _filter.REPFRMMONTH.HasValue Then
                ls = ls.Where(Function(f) f.REPFRMMONTH.Value = _filter.REPFRMMONTH)
            End If
            If _filter.REPTOMONTH.HasValue Then
                ls = ls.Where(Function(f) f.REPTOMONTH.Value = _filter.REPTOMONTH)
            End If
            If _filter.REPBHXH.HasValue Then
                ls = ls.Where(Function(f) f.REPBHXH.Value = _filter.REPBHXH)
            End If
            If _filter.REPBHYT.HasValue Then
                ls = ls.Where(Function(f) f.REPBHYT.Value = _filter.REPBHYT)
            End If
            If _filter.REPBHTN.HasValue Then
                ls = ls.Where(Function(f) f.REPBHTN.Value = _filter.REPBHTN)
            End If

            ls = ls.OrderBy(Sorts)
            Total = ls.Count
            ls = ls.Skip(PageIndex * PageSize).Take(PageSize)
            Return ls.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_CHANGEById(ByVal _id As Decimal?) As INS_CHANGEDTO
        Try

            Dim query = From p In Context.INS_CHANGE
                        From i In Context.INS_INFORMATION.Where(Function(F) F.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ORG_ID_INS And f.TYPE_ID = 2025).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CHANGE_TYPE And f.TYPE_ID = 2021).DefaultIfEmpty
                        From ins_type In Context.INS_CHANGE_TYPE.Where(Function(f) f.ID = p.CHANGE_TYPE).DefaultIfEmpty
                        Where p.ID = _id

            Dim ls = query.Select(Function(p) New INS_CHANGEDTO With {
                                                                       .ID = p.p.ID,
                                                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                       .VN_FULLNAME = p.e.FULLNAME_VN,
                                                                       .CMND = p.cv.ID_NO,
                                                                       .NOICAP = p.cv.PASS_PLACE,
                                                                       .NGAYSINH = p.cv.BIRTH_DATE,
                                                                       .NOISINH = p.cv.BIRTH_PLACE,
                                                                       .TITLE_NAME = p.t.NAME_VN,
                                                                       .ORG_NAME = p.o.NAME_VN,
                                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                                       .ORG_ID = p.p.ORG_ID,
                                                                       .ORG_NAMEBH = p.o1.NAME_EN,
                                                                       .CHANGE_TYPE_NAME = p.ins_type.ARISING_NAME,
                                                                       .CHANGE_TYPE = p.p.CHANGE_TYPE,
                                                                       .OLDSALARY = p.p.OLDSALARY,
                                                                       .NEWSALARY = p.p.NEWSALARY,
                                                                       .ISBHXH = p.i.SO,
                                                                       .ISBHYT = p.i.HE,
                                                                       .ISBHTN = p.i.UE,
                                                                       .CHANGE_MONTH = p.p.CHANGE_MONTH,
                                                                       .RETURN_DATEBHXH = p.p.RETURN_DATEBHXH,
                                                                       .RETURN_DATEBHYT = p.p.RETURN_DATEBHYT,
                                                                       .NOTE = p.p.NOTE,
                                                                       .CLTFRMMONTH = p.p.CLTFRMMONTH,
                                                                       .CLTTOMONTH = p.p.CLTTOMONTH,
                                                                       .CLTBHXH = p.p.CLTBHXH,
                                                                       .CLTBHYT = p.p.CLTBHYT,
                                                                       .CLTBHTN = p.p.CLTBHTN,
                                                                       .REPFRMMONTH = p.p.REPFRMMONTH,
                                                                       .REPTOMONTH = p.p.REPTOMONTH,
                                                                       .REPBHXH = p.p.REPBHXH,
                                                                       .REPBHYT = p.p.REPBHYT,
                                                                       .REPBHTN = p.p.REPBHTN,
                                                                       .CREATED_BY = p.p.CREATED_BY,
                                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                                       .CREATED_LOG = p.p.CREATED_LOG,
                                                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return ls
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_CHANGE(ByVal objLeave As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objTitleData As New INS_CHANGE
            Dim iCount As Integer = 0
            Dim org_id As Decimal?
            Try
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE.EntitySet.Name)
                org_id = (From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = objLeave.EMPLOYEE_ID)).FirstOrDefault().ORG_ID
                objTitleData.EMPLOYEE_ID = objLeave.EMPLOYEE_ID
                objTitleData.ORG_ID = org_id
                objTitleData.CHANGE_TYPE = objLeave.CHANGE_TYPE
                objTitleData.ISBHYT = objLeave.ISBHYT
                objTitleData.ISBHTN = objLeave.ISBHTN
                objTitleData.ISBHXH = objLeave.ISBHXH
                objTitleData.OLDSALARY = objLeave.OLDSALARY
                objTitleData.NEWSALARY = objLeave.NEWSALARY
                objTitleData.CHANGE_TYPE = objLeave.CHANGE_TYPE
                objTitleData.CHANGE_MONTH = objLeave.CHANGE_MONTH
                objTitleData.RETURN_DATEBHXH = objLeave.RETURN_DATEBHXH
                objTitleData.RETURN_DATEBHYT = objLeave.RETURN_DATEBHYT
                objTitleData.NOTE = objLeave.NOTE
                objTitleData.CLTFRMMONTH = objLeave.CLTFRMMONTH
                objTitleData.CLTTOMONTH = objLeave.CLTTOMONTH
                objTitleData.CLTBHXH = objLeave.CLTBHXH
                objTitleData.CLTBHYT = objLeave.CLTBHYT
                objTitleData.CLTBHTN = objLeave.CLTBHTN
                objTitleData.REPFRMMONTH = objLeave.REPFRMMONTH
                objTitleData.REPTOMONTH = objLeave.REPTOMONTH
                objTitleData.REPBHXH = objLeave.REPBHXH
                objTitleData.REPBHYT = objLeave.REPBHYT
                objTitleData.REPBHTN = objLeave.REPBHTN
                Context.INS_CHANGE.AddObject(objTitleData)
                Context.SaveChanges(log)
                gID = objTitleData.ID
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
                Throw ex
            End Try
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_CHANGE(ByVal objLeave As INS_CHANGEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_CHANGE With {.ID = objLeave.ID}
        Dim org_id As Decimal?
        Try
            objTitleData = (From p In Context.INS_CHANGE Where p.ID = objTitleData.ID).SingleOrDefault
            org_id = (From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = objLeave.EMPLOYEE_ID)).FirstOrDefault().ORG_ID
            objTitleData.ORG_ID = org_id
            objTitleData.EMPLOYEE_ID = objLeave.EMPLOYEE_ID
            objTitleData.CHANGE_TYPE = objLeave.CHANGE_TYPE
            objTitleData.ISBHYT = objLeave.ISBHYT
            objTitleData.ISBHTN = objLeave.ISBHTN
            objTitleData.ISBHXH = objLeave.ISBHXH
            objTitleData.OLDSALARY = objLeave.OLDSALARY
            objTitleData.NEWSALARY = objLeave.NEWSALARY
            objTitleData.CHANGE_TYPE = objLeave.CHANGE_TYPE
            objTitleData.CHANGE_MONTH = objLeave.CHANGE_MONTH
            objTitleData.RETURN_DATEBHXH = objLeave.RETURN_DATEBHXH
            objTitleData.RETURN_DATEBHYT = objLeave.RETURN_DATEBHYT
            objTitleData.NOTE = objLeave.NOTE
            objTitleData.CLTFRMMONTH = objLeave.CLTFRMMONTH
            objTitleData.CLTTOMONTH = objLeave.CLTTOMONTH
            objTitleData.CLTBHXH = objLeave.CLTBHXH
            objTitleData.CLTBHYT = objLeave.CLTBHYT
            objTitleData.CLTBHTN = objLeave.CLTBHTN
            objTitleData.REPFRMMONTH = objLeave.REPFRMMONTH
            objTitleData.REPTOMONTH = objLeave.REPTOMONTH
            objTitleData.REPBHXH = objLeave.REPBHXH
            objTitleData.REPBHYT = objLeave.REPBHYT
            objTitleData.REPBHTN = objLeave.REPBHTN
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function DeleteINS_CHANGE(ByVal lstID As List(Of INS_CHANGEDTO)) As Boolean
        Dim lstl As INS_CHANGE
        Dim id As Decimal = 0
        Try

            For index = 0 To lstID.Count - 1
                id = lstID(index).ID
                lstl = (From p In Context.INS_CHANGE Where id = p.ID).FirstOrDefault
                Dim details = (From r In Context.INS_CHANGE Where r.ID = lstl.ID).ToList
                For index1 = 0 To details.Count - 1
                    Context.INS_CHANGE.DeleteObject(details(index1))
                Next
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteINS_CHANGE")
            Throw ex
        End Try
    End Function

    Public Function GetTiLeDong() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETTILEDONG",
                                           New With {.P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GETLUONGBIENDONG(ByRef P_EMPLOYEEID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETLUONGBIENDONG",
                                           New With {.P_EMPLOYEE_ID = P_EMPLOYEEID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function
#End Region

#Region "thay doi thong tin tham gia bao hiem"
    Public Function CheckDouble(ByVal id As Decimal, ByVal emp_id As Decimal, ByVal type_id As Decimal, ByVal date_change As Date) As Decimal
        Dim re = 0
        If id = 0 Then
            re = (From p In Context.INS_CHANGE_INFO.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.TYPE_CHANGE_ID = type_id And f.DATE_CHANGE = date_change)).Count
        Else
            re = (From p In Context.INS_CHANGE_INFO.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.TYPE_CHANGE_ID = type_id And f.DATE_CHANGE = date_change And f.ID <> id)).Count
        End If
        Return re
    End Function
    Public Function GetChangeInfoById(ByVal _id As Decimal?) As INS_CHANGE_INFO_DTO
        Try

            Dim query = From s In Context.INS_CHANGE_INFO
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From t In Context.INS_TYPE_CHANGE.Where(Function(f1) s.TYPE_CHANGE_ID = f1.ID)
                        Where (s.ID = _id)

            Dim lst = query.Select(Function(f) New INS_CHANGE_INFO_DTO With {
                                        .ID = f.s.ID,
                                        .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                        .ORG_NAME = f.o.NAME_VN,
                                        .ORG_DESC = f.o.DESCRIPTION_PATH,
                                        .TYPE_CHANGE_ID = f.s.TYPE_CHANGE_ID,
                                        .TYPE_CHANGE_NAME = f.t.NAME,
                                        .DATE_CHANGE = f.s.DATE_CHANGE,
                                        .CONTENT_CHANGE = f.s.CONTENT_CHANGE,
                                        .OLD_INFO = f.s.OLD_INFO,
                                        .NEW_INFO = f.s.NEW_INFO,
                                        .REASON_CHANGE = f.s.REASON_CHANGE,
                                        .MONTH_DECLARE = f.s.MONTH_DECLARE,
                                        .TITLE_NAME = f.p.NAME_VN}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function InsertOrModifyChangeInfo(ByVal objTitle As INS_CHANGE_INFO_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_CHANGE_INFO
        Dim iCount As Integer = 0
        Try
            If objTitle.ID Is Nothing Then
                objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE_INFO.EntitySet.Name)
                objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
                objTitleData.TYPE_CHANGE_ID = objTitle.TYPE_CHANGE_ID
                objTitleData.DATE_CHANGE = objTitle.DATE_CHANGE
                objTitleData.CONTENT_CHANGE = objTitle.CONTENT_CHANGE
                objTitleData.OLD_INFO = objTitle.OLD_INFO
                objTitleData.NEW_INFO = objTitle.NEW_INFO
                objTitleData.REASON_CHANGE = objTitle.REASON_CHANGE
                objTitleData.MONTH_DECLARE = objTitle.MONTH_DECLARE
                Context.INS_CHANGE_INFO.AddObject(objTitleData)
                Context.SaveChanges(log)
                gID = objTitleData.ID
                Return True
            Else
                objTitleData = (From p In Context.INS_CHANGE_INFO Where p.ID = objTitle.ID).SingleOrDefault
                objTitleData.ID = objTitle.ID
                objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
                objTitleData.TYPE_CHANGE_ID = objTitle.TYPE_CHANGE_ID
                objTitleData.DATE_CHANGE = objTitle.DATE_CHANGE
                objTitleData.CONTENT_CHANGE = objTitle.CONTENT_CHANGE
                objTitleData.OLD_INFO = objTitle.OLD_INFO
                objTitleData.NEW_INFO = objTitle.NEW_INFO
                objTitleData.REASON_CHANGE = objTitle.REASON_CHANGE
                objTitleData.MONTH_DECLARE = objTitle.MONTH_DECLARE
                Context.SaveChanges(log)
                gID = objTitleData.ID
                Return True
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function GetChangeInfo(ByVal _filter As INS_CHANGE_INFO_DTO,
                               ByVal OrgId As Integer,
                               ByVal Fillter As String,
                               Optional ByVal PageIndex As Integer = 0,
                               Optional ByVal PageSize As Integer = Integer.MaxValue,
                               Optional ByRef Total As Integer = 0,
                               Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_CHANGE_INFO_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = True})
            End Using
            Dim query = From s In Context.INS_CHANGE_INFO
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From t In Context.INS_TYPE_CHANGE.Where(Function(f) f.ID = s.TYPE_CHANGE_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where e.FULLNAME_VN.EndsWith(Fillter) Or e.FULLNAME_VN.StartsWith(Fillter) Or e.FULLNAME_VN.Contains(Fillter) Or e.FULLNAME_VN = Fillter Or Fillter = ""
                        Order By e.EMPLOYEE_CODE Ascending, s.CREATED_DATE Descending
            Dim lst = query.Select(Function(f) New INS_CHANGE_INFO_DTO With {
                                       .ID = f.s.ID,
                                       .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                       .ORG_NAME = f.o.NAME_VN,
                                       .ORG_DESC = f.o.DESCRIPTION_PATH,
                                       .TYPE_CHANGE_ID = f.s.TYPE_CHANGE_ID,
                                       .TYPE_CHANGE_NAME = f.t.NAME,
                                       .DATE_CHANGE = f.s.DATE_CHANGE,
                                       .CONTENT_CHANGE = f.s.CONTENT_CHANGE,
                                       .OLD_INFO = f.s.OLD_INFO,
                                       .NEW_INFO = f.s.NEW_INFO,
                                       .MONTH_DECLARE = f.s.MONTH_DECLARE,
                                       .MONTH_DECLARE1 = If(f.s.MONTH_DECLARE.Value.Month < 10, "0" & f.s.MONTH_DECLARE.Value.Month & "/" & f.s.MONTH_DECLARE.Value.Year, "" & f.s.MONTH_DECLARE.Value.Month & "/" & f.s.MONTH_DECLARE.Value.Year),
                                       .REASON_CHANGE = f.s.REASON_CHANGE,
                                       .WORK_STATUS = f.e.WORK_STATUS,
                                       .TITLE_NAME = f.p.NAME_VN,
                                       .TER_LAST_DATE = f.e.TER_LAST_DATE,
                                       .CREATED_BY = f.s.CREATED_BY,
                                       .CREATED_DATE = f.s.CREATED_DATE,
                                       .CREATED_LOG = f.s.CREATED_LOG})

            '.MONTH_DECLARE1 = If(f.s.MONTH_DECLARE.Value.Month < 10, "0" & f.s.MONTH_DECLARE.Value.Month & "/" & f.s.MONTH_DECLARE.Value.Year, "" & f.s.MONTH_DECLARE.Value.Month) & "/" & f.s.MONTH_DECLARE.Value.Year,
            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            ''=======date change
            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE <= _filter.TO_DATE_SEARCH)
            End If
            ''=======month change
            If _filter.MONTH_FROM.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE.Value.Month >= _filter.MONTH_FROM.Value.Month And f.DATE_CHANGE.Value.Year >= _filter.MONTH_FROM.Value.Year)
            End If
            If _filter.MONTH_TO.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE.Value.Month <= _filter.MONTH_TO.Value.Month And f.DATE_CHANGE.Value.Year <= _filter.MONTH_TO.Value.Year)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MONTH_DECLARE1) Then
                Dim a As DateTime
                If IsDate(_filter.MONTH_DECLARE1) Then
                    a = ToDate(_filter.MONTH_DECLARE1)
                    lst = lst.Where(Function(f) f.MONTH_DECLARE.Value.Month <= a.Month And f.MONTH_DECLARE.Value.Year <= a.Year)
                Else
                    lst = lst.Where(Function(f) f.MONTH_DECLARE.Value.Month = 13)
                End If
            End If

            If _filter.DATE_CHANGE.HasValue Then
                lst = lst.Where(Function(f) f.DATE_CHANGE.Value = _filter.DATE_CHANGE)
            End If
            'If _filter.MONTH_DECLARE.HasValue Then
            '    lst = lst.Where(Function(f) f.MONTH_DECLARE.Value = _filter.MONTH_DECLARE)
            'End If
            If Not String.IsNullOrEmpty(_filter.CONTENT_CHANGE) Then
                lst = lst.Where(Function(f) f.CONTENT_CHANGE.ToLower().Contains(_filter.CONTENT_CHANGE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.OLD_INFO) Then
                lst = lst.Where(Function(f) f.OLD_INFO.ToLower().Contains(_filter.OLD_INFO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NEW_INFO) Then
                lst = lst.Where(Function(f) f.NEW_INFO.ToLower().Contains(_filter.NEW_INFO.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_CHANGE_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_CHANGE_NAME.ToLower().Contains(_filter.TYPE_CHANGE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REASON_CHANGE) Then
                lst = lst.Where(Function(f) f.REASON_CHANGE.ToLower().Contains(_filter.REASON_CHANGE.ToLower()))
            End If

            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function DeleteChangeInfo(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstChangeInfoData As List(Of INS_CHANGE_INFO)
        Try
            lstChangeInfoData = (From p In Context.INS_CHANGE_INFO Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstChangeInfoData.Count - 1
                Context.INS_CHANGE_INFO.DeleteObject(lstChangeInfoData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region

#Region "Quản lý Sun Care"
    Public Function GetSunCare(ByVal _filter As INS_SUN_CARE_DTO,
                               ByVal OrgId As Integer,
                               ByVal Fillter As String,
                               Optional ByVal PageIndex As Integer = 0,
                               Optional ByVal PageSize As Integer = Integer.MaxValue,
                               Optional ByRef Total As Integer = 0,
                               Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_SUN_CARE_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = True})
            End Using
            Dim query = From s In Context.INS_SUN_CARE
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = s.LEVEL_ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Where e.FULLNAME_VN.EndsWith(Fillter) Or e.FULLNAME_VN.StartsWith(Fillter) Or e.FULLNAME_VN.Contains(Fillter) Or e.FULLNAME_VN = Fillter Or Fillter = ""
                        Order By e.EMPLOYEE_CODE Ascending, s.START_DATE Descending
            Dim lst = query.Select(Function(f) New INS_SUN_CARE_DTO With {
                                       .ID = f.s.ID,
                                       .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                       .ORG_NAME = f.o.NAME_VN,
                                       .ORG_DESC = f.o.DESCRIPTION_PATH,
                                       .TITLE_NAME = f.p.NAME_VN,
                                       .STAFF_RANK_ID = f.e.STAFF_RANK_ID,
                                       .STAFF_RANK_NAME = f.r.NAME,
                                       .START_DATE = f.s.START_DATE,
                                       .END_DATE = f.s.END_DATE,
                                       .LEVEL_ID = f.s.LEVEL_ID,
                                       .LEVEL_NAME = f.ot.NAME_VN,
                                       .COST = f.s.COST,
                                       .THOIDIEMHUONG = f.s.THOIDIEMHUONG,
                                       .NOTE = f.s.NOTE,
                                       .WORK_STATUS = f.e.WORK_STATUS,
                                       .TER_LAST_DATE = f.e.TER_EFFECT_DATE,
                                       .CREATED_BY = f.s.CREATED_BY,
                                       .CREATED_DATE = f.s.CREATED_DATE,
                                       .CREATED_LOG = f.s.CREATED_LOG,
                                       .COST_SAL = f.s.COST_SAL})

            Dim dateNow = Date.Now.Date
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257)
            'End If

            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.THOIDIEMHUONG >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.THOIDIEMHUONG <= _filter.TO_DATE_SEARCH)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.START_DATE.Value = _filter.START_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.END_DATE.Value = _filter.END_DATE)
            End If
            If _filter.THOIDIEMHUONG.HasValue Then
                lst = lst.Where(Function(f) f.THOIDIEMHUONG.Value = _filter.THOIDIEMHUONG)
            End If
            If Not String.IsNullOrEmpty(_filter.LEVEL_NAME) Then
                lst = lst.Where(Function(f) f.LEVEL_NAME.ToLower().Contains(_filter.LEVEL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function


    Public Function GetSunCareById(ByVal _id As Decimal?) As INS_SUN_CARE_DTO
        Try

            Dim query = From s In Context.INS_SUN_CARE
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From r In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = s.LEVEL_ID).DefaultIfEmpty
                        Where (s.ID = _id)

            Dim lst = query.Select(Function(f) New INS_SUN_CARE_DTO With {
                                        .ID = f.s.ID,
                                        .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                        .ORG_NAME = f.o.NAME_VN,
                                        .ORG_DESC = f.o.DESCRIPTION_PATH,
                                        .TITLE_NAME = f.p.NAME_VN,
                                        .STAFF_RANK_ID = f.e.STAFF_RANK_ID,
                                        .STAFF_RANK_NAME = f.r.NAME,
                                        .START_DATE = f.s.START_DATE,
                                        .END_DATE = f.s.END_DATE,
                                        .LEVEL_ID = f.s.LEVEL_ID,
                                        .LEVEL_NAME = f.ot.NAME_VN,
                                        .COST = f.s.COST,
                                        .THOIDIEMHUONG = f.s.THOIDIEMHUONG,
                                        .NOTE = f.s.NOTE,
                                        .CREATED_BY = f.s.CREATED_BY,
                                        .CREATED_DATE = f.s.CREATED_DATE,
                                        .CREATED_LOG = f.s.CREATED_LOG,
                                        .COST_SAL = f.s.COST_SAL}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetIns_Cost_LeverByID(ByVal _id As Decimal?) As INS_COST_FOLLOW_LEVERDTO
        Try

            Dim query = From s In Context.INS_COST_FOLLOW_LEVER
                        Where (s.ID = _id)
            Dim lst = query.Select(Function(f) New INS_COST_FOLLOW_LEVERDTO With {
                                        .ID = f.ID,
                                        .COST_MONEY = f.COST_MONEY}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertSunCare(ByVal objTitle As INS_SUN_CARE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_SUN_CARE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_SUN_CARE.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.END_DATE = objTitle.END_DATE
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.COST = objTitle.COST
            objTitleData.THOIDIEMHUONG = objTitle.THOIDIEMHUONG
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.COST_SAL = objTitle.COST_SAL
            Context.INS_SUN_CARE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function INPORT_MANAGER_SUN_CARE(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_BUSINESS.INPORT_MANAGER_SUN_CARE",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function

    Public Function ModifySunCare(ByVal objTitle As INS_SUN_CARE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As INS_SUN_CARE
        Try
            objTitleData = (From p In Context.INS_SUN_CARE Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.END_DATE = objTitle.END_DATE
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.COST = objTitle.COST
            objTitleData.THOIDIEMHUONG = objTitle.THOIDIEMHUONG
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.COST_SAL = objTitle.COST_SAL
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function CHECK_MANAGER_SUN_CARE(ByVal P_EMP_CODE As String, ByVal P_START_DATE As String, ByVal P_END_DATE As String, ByVal P_LEVEL_ID As Decimal) As Integer
        Try
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_EMP_CODE = P_EMP_CODE,
                                    .P_START_DATE = P_START_DATE,
                                    .P_END_DATE = P_END_DATE,
                                    .P_LEVEL_ID = P_LEVEL_ID,
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_INS_BUSINESS.CHECK_MANAGER_SUN_CARE", obj)
                Return Integer.Parse(obj.P_OUT)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActiveSunCare(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of INS_SUN_CARE)
        Try
            lstData = (From p In Context.INS_SUN_CARE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteSunCare(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSunCareData As List(Of INS_SUN_CARE)
        Try
            lstSunCareData = (From p In Context.INS_SUN_CARE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstSunCareData.Count - 1
                Context.INS_SUN_CARE.DeleteObject(lstSunCareData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SunCare")
            Throw ex
        End Try
    End Function

    Public Function GetLevelImport() As DataTable
        Try

            Dim query = From p In Context.OT_OTHER_LIST
                        Where p.TYPE_CODE = "ORG_INS" Order By p.NAME_VN
            Dim lst = query.Select(Function(p) New OT_OTHERLIST_DTO With {
                                       .ID = p.ID,
                                       .NAME_VN = p.NAME_VN}).ToList
            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region
#Region "Quản lý bảo hiểm sức khỏe cá nhân, người thân"
    Public Function InsertHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New INS_HEALTH_INSURANCE
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.INS_HEALTH_INSURANCE.EntitySet.Name)
            objData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objData.ORG_ID = objTitle.ORG_ID
            objData.YEAR = objTitle.YEAR
            objData.INS_CONTRACT_ID = objTitle.INS_CONTRACT_ID
            objData.INS_CONTRACT_DE_ID = objTitle.INS_CONTRACT_DE_ID
            objData.CHECK_BHNT = objTitle.CHECK_BHNT
            objData.FAMYLI_ID = objTitle.FAMYLI_ID
            objData.DT_CHITRA = objTitle.DT_CHITRA
            objData.JOIN_DATE = objTitle.JOIN_DATE
            objData.EFFECT_DATE = objTitle.EFFECT_DATE
            objData.MONEY_INS = objTitle.MONEY_INS
            objData.REDUCE_DATE = objTitle.REDUCE_DATE
            objData.REFUND = objTitle.REFUND
            objData.DATE_RECEIVE_MONEY = objTitle.DATE_RECEIVE_MONEY
            objData.EMP_RECEIVE_MONEY = objTitle.EMP_RECEIVE_MONEY
            objData.NOTES = objTitle.NOTES
            Context.INS_HEALTH_INSURANCE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function ModifyHEALTH_INSURANCE(ByVal objTitle As INS_HEALTH_INSURANCE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As INS_HEALTH_INSURANCE
        Try
            objData = (From p In Context.INS_HEALTH_INSURANCE Where p.ID = objTitle.ID).SingleOrDefault
            objData.ID = objTitle.ID

            objData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objData.ORG_ID = objTitle.ORG_ID
            objData.YEAR = objTitle.YEAR
            objData.INS_CONTRACT_ID = objTitle.INS_CONTRACT_ID
            objData.INS_CONTRACT_DE_ID = objTitle.INS_CONTRACT_DE_ID
            objData.CHECK_BHNT = objTitle.CHECK_BHNT
            objData.FAMYLI_ID = objTitle.FAMYLI_ID
            objData.DT_CHITRA = objTitle.DT_CHITRA
            objData.JOIN_DATE = objTitle.JOIN_DATE
            objData.EFFECT_DATE = objTitle.EFFECT_DATE
            objData.MONEY_INS = objTitle.MONEY_INS
            objData.REDUCE_DATE = objTitle.REDUCE_DATE
            objData.REFUND = objTitle.REFUND
            objData.DATE_RECEIVE_MONEY = objTitle.DATE_RECEIVE_MONEY
            objData.EMP_RECEIVE_MONEY = objTitle.EMP_RECEIVE_MONEY
            objData.NOTES = objTitle.NOTES
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function InsertCLAIME_INSURANCE(ByVal objTitle As INS_CLAIME_INSURANCEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New INS_CLAIME_INSURANCE
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.INS_CLAIME_INSURANCE.EntitySet.Name)
            objData.INS_HEALTH_ID = objTitle.INS_HEALTH_ID
            objData.EXAMINE_DATE = objTitle.EXAMINE_DATE
            objData.DISEASE_NAME = objTitle.DISEASE_NAME
            objData.AMOUNT_OF_CLAIMS = objTitle.AMOUNT_OF_CLAIMS
            objData.AMOUNT_OF_COMPENSATION = objTitle.AMOUNT_OF_COMPENSATION
            objData.COMPENSATION_DATE = objTitle.COMPENSATION_DATE
            objData.NOTE = objTitle.NOTE
            Context.INS_CLAIME_INSURANCE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ValidateHealth_Insurance(ByVal _validate As INS_HEALTH_INSURANCE_DTO)
        Dim query
        Try
            If _validate.EMPLOYEE_ID IsNot Nothing AndAlso _validate.YEAR IsNot Nothing AndAlso _validate.INS_CONTRACT_ID IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_HEALTH_INSURANCE
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID And p.YEAR = _validate.YEAR And p.INS_CONTRACT_ID = _validate.INS_CONTRACT_ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_HEALTH_INSURANCE
                             Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID And p.YEAR = _validate.YEAR And p.INS_CONTRACT_ID = _validate.INS_CONTRACT_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_HEALTH_INSURANCE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GET_INS_HEALTH_INSURANCE(
                                             ByVal ORGID As Integer?,
                                             ByVal IS_TERMINATE As Integer?,
                                             ByVal IS_CLAIM As Integer?,
                                             ByVal ISDISSOLVE As Integer?,
                                             ByVal DJOIN_START As Date?,
                                             ByVal DJOIN_END As Date?,
                                             ByVal D_EFFECT_START As Date?,
                                             ByVal D_EFFECT_END As Date?,
                                             ByVal DREDUCE_START As Date?,
                                             ByVal DREDUCE_END As Date?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_HEALTH_INSURANCE_DTO)
        Using cls As New DataAccess.QueryData
            Dim lst As New List(Of INS_HEALTH_INSURANCE_DTO)
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INS_LIST.GET_INS_HEALTH_INSURANCE",
                                           New With {.P_USERID = log.Username,
                                                     .P_ORGID = ORGID,
                                                     .P_IS_TERMINATE = IS_TERMINATE,
                                                     .P_IS_CLAIM = IS_CLAIM,
                                                     .P_ISDISSOLVE = ISDISSOLVE,
                                                     .P_DJOIN_START = DJOIN_START,
                                                     .P_DJOIN_END = DJOIN_END,
                                                     .P_D_EFFECT_START = D_EFFECT_START,
                                                     .P_D_EFFECT_END = D_EFFECT_END,
                                                     .P_DREDUCE_START = DREDUCE_START,
                                                     .P_DREDUCE_END = DREDUCE_END,
                                                     .P_PAGE = PAGE,
                                                     .P_SIZE = SIZE,
                                                     .P_CUR = cls.OUT_CURSOR})
            If dtData.Rows.Count > 0 Then
                For Each dr As DataRow In dtData.Rows
                    Dim itm As New INS_HEALTH_INSURANCE_DTO
                    itm.ID = dr("ID")
                    itm.EMPLOYEE_ID = If(IsNumeric(dr("EMPLOYEE_ID")), dr("EMPLOYEE_ID"), Nothing)
                    itm.ORG_ID = If(IsNumeric(dr("ORG_ID")), dr("ORG_ID"), Nothing)
                    itm.YEAR = If(IsNumeric(dr("YEAR")), dr("YEAR"), Nothing)
                    itm.INS_CONTRACT_ID = If(IsNumeric(dr("INS_CONTRACT_ID")), dr("INS_CONTRACT_ID"), Nothing)

                    itm.INS_CONTRACT_DE_ID = If(IsNumeric(dr("INS_CONTRACT_DE_ID")), dr("INS_CONTRACT_DE_ID"), Nothing)
                    itm.CHECK_BHNT = If(IsNumeric(dr("CHECK_BHNT")), dr("CHECK_BHNT"), Nothing)
                    itm.FAMYLI_ID = If(IsNumeric(dr("FAMYLI_ID")), dr("FAMYLI_ID"), Nothing)
                    itm.DT_CHITRA_NAME = dr("DT_CHITRA_NAME").ToString
                    itm.DT_CHITRA = If(IsNumeric(dr("DT_CHITRA")), dr("DT_CHITRA"), Nothing)
                    itm.JOIN_DATE = If(IsDate(dr("JOIN_DATE")), dr("JOIN_DATE"), Nothing)
                    itm.EFFECT_DATE = If(IsDate(dr("EFFECT_DATE")), dr("EFFECT_DATE"), Nothing)
                    itm.MONEY_INS = If(IsNumeric(dr("MONEY_INS")), dr("MONEY_INS"), Nothing)
                    itm.REDUCE_DATE = If(IsDate(dr("REDUCE_DATE")), dr("REDUCE_DATE"), Nothing)
                    itm.REFUND = If(IsNumeric(dr("REFUND")), dr("REFUND"), Nothing)
                    itm.DATE_RECEIVE_MONEY = If(IsDate(dr("DATE_RECEIVE_MONEY")), dr("DATE_RECEIVE_MONEY"), Nothing)
                    itm.EMP_RECEIVE_MONEY = dr("EMP_RECEIVE_MONEY").ToString
                    itm.NOTES = dr("NOTES").ToString

                    itm.NAME_PROGRAM = dr("NAME").ToString
                    itm.SOTIEN = If(IsNumeric(dr("SOTIEN")), dr("SOTIEN"), Nothing)
                    itm.ORG_INSURANCE = dr("ORG_INSURANCE").ToString
                    itm.VAL_CO = If(IsNumeric(dr("VAL_CO")), dr("VAL_CO"), Nothing)
                    itm.START_DATE = If(IsDate(dr("START_DATE")), dr("START_DATE"), Nothing)
                    itm.EXPIRE_DATE = If(IsDate(dr("EXPIRE_DATE")), dr("EXPIRE_DATE"), Nothing)
                    itm.CLAIM = dr("CLAIM").ToString
                    itm.TOTAL = dr("TOTAL").ToString

                    itm.FULLNAME = dr("FULLNAME_VN").ToString
                    itm.BIRTH_DATE = If(IsDate(dr("BIRTH_DATE")), dr("BIRTH_DATE"), Nothing)
                    itm.ID_NO = dr("ID_NO").ToString
                    itm.PHONG_BAN = dr("PHONG_BAN").ToString
                    itm.HOTENNGUOITHAN = dr("HOTENNGUOITHAN").ToString
                    itm.MOIQUANHE = dr("MOIQUANHE").ToString
                    itm.NGAYSINHTN = If(IsDate(dr("NGAYSINHTN")), dr("NGAYSINHTN"), Nothing)
                    itm.CMNDTN = dr("CMNDTN").ToString
                    itm.CONTRACT_INS_NO = dr("CONTRACT_INS_NO").ToString
                    itm.EMPLOYEE_CODE = dr("EMPLOYEE_CODE").ToString
                    itm.ORG_DESC = dr("ORG_DESC").ToString
                    'itm.
                    lst.Add(itm)
                Next
            End If
            Total = lst.Count
            Return lst
        End Using
        Return Nothing
    End Function

    Public Function CHECK_INS_LIST_CONTRACT(ByVal ID As Integer) As Integer
        Try
            Dim result As Integer
            If (From p In Context.INS_LIST_CONTRACT Where p.CONTRACT_INS_NO.ToUpper = ID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    'Public Function CHECK_INS_LIST_CONTRACT_DETAIL(ByVal CODE As String) As Integer
    '    Try
    '        Dim result As Integer
    '        If (From p In Context.INS_LIST_CONTRACT_DETAIL Where p.ID = ID).Count > 0 Then
    '            result = 1
    '        Else
    '            result = 0
    '        End If

    '        Return result
    '    Catch ex As Exception
    '        WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
    '        Throw ex
    '    End Try
    'End Function
    Public Function Delete_INS_HEALTH_INSURANCE(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of INS_HEALTH_INSURANCE)
        Dim lst_claime As List(Of INS_CLAIME_INSURANCE)
        Try
            lst = (From p In Context.INS_HEALTH_INSURANCE Where lstID.Contains(p.ID)).ToList
            lst_claime = (From l In Context.INS_CLAIME_INSURANCE Where lstID.Contains(l.INS_HEALTH_ID)).ToList
            For index = 0 To lst.Count - 1
                Context.INS_HEALTH_INSURANCE.DeleteObject(lst(index))
            Next
            For index = 0 To lst_claime.Count - 1
                Context.INS_CLAIME_INSURANCE.DeleteObject(lst_claime(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SunCare")
            Throw ex
        End Try
    End Function
#End Region
#Region "Quản lý chế độ bảo hiểm"

    Public Function GetInfoInsByEmpID(ByVal employee_id As Integer) As INS_INFORMATIONDTO
        Try
            Dim query = (From s In Context.INS_INFORMATION
                         From c In Context.INS_CHANGE.Where(Function(F) F.EMPLOYEE_ID = s.EMPLOYEE_ID)
                         Where s.EMPLOYEE_ID = employee_id And c.CHANGE_TYPE = 1
                         Select s)

            Dim lst = query.Select(Function(f) New INS_INFORMATIONDTO With {
                                        .EMPLOYEE_ID = f.EMPLOYEE_ID,
                                        .SOBOOKNO = f.SOBOOKNO,
                                        .HECARDNO = f.HECARDNO}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetLuyKe(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date,
                                      ByRef P_EMPLOYEEID As Integer,
                                      ByVal P_ENTITLED_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.SPS_LUYKE",
                                           New With {.P_TUNGAY = P_TUNGAY,
                                                     .P_DENNGAY = P_DENNGAY,
                                                     .P_EMPLOYEEID = P_EMPLOYEEID,
                                                     .P_ENTITLED_ID = P_ENTITLED_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function CALCULATOR_DAY(ByVal P_TUNGAY As Date,
                                      ByVal P_DENNGAY As Date) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.CALCULATOR_DAY",
                                           New With {.P_FROM_DATE = P_TUNGAY,
                                                     .P_TO_DATE = P_DENNGAY,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetMaxDayByID(ByVal P_ENTITLED_ID As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETMAXDAYBYID",
                                           New With {.P_ENTITLED_ID = P_ENTITLED_ID,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetTienHuong(ByVal P_NUMOFF As Integer,
                                     ByVal P_ATHOME As Integer,
                                     ByRef P_EMPLOYEEID As Integer,
                                     ByVal P_INSENTILEDKEY As Integer,
                                     ByVal P_SALARY_ADJACENT As Decimal,
                                     ByVal P_FROMDATE As Date,
                                     ByVal P_SOCON As Integer) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.SPS_TIEN_CHEDO",
                                           New With {.P_EMPLOYEEID = P_EMPLOYEEID,
                                                     .P_INSENTILEDKEY = P_INSENTILEDKEY,
                                                     .P_NUMOFF = P_NUMOFF,
                                                     .P_ATHOME = P_ATHOME,
                                                     .P_SALARY_ADJACENT = P_SALARY_ADJACENT,
                                                     .P_FROMDATE = P_FROMDATE,
                                                     .P_SOCON = P_SOCON,
                                                     .P_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetRegimeManager(ByVal _filter As INS_REMIGE_MANAGER_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal OrgId As Integer,
                                        ByVal IsDissolve As Integer,
                                        ByVal EntiledID As Integer,
                                        ByVal Fillter As String,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REMIGE_MANAGER_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Dim query = From s In Context.INS_REMIGE_MANAGER
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From ose In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = o.ID And se.USERNAME.ToUpper = log.Username.ToUpper)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From t In Context.INS_ENTITLED_TYPE.Where(Function(ts) ts.ID = s.ENTITLED_ID)
                        From i In Context.INS_INFORMATION.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From r In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = s.EMPLOYEE_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) c.WORKING_ID = f.ID And f.EFFECT_DATE <= Date.Now).DefaultIfEmpty
                        Where ((e.FULLNAME_VN.EndsWith(Fillter) Or e.FULLNAME_VN.StartsWith(Fillter) Or e.FULLNAME_VN.Contains(Fillter) Or e.FULLNAME_VN = Fillter Or Fillter = "")) _
                            And (t.ID = EntiledID Or EntiledID = 0)
            Dim lst = query.Select(Function(f) New INS_REMIGE_MANAGER_DTO With {
                                        .ID = f.s.ID,
                                        .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                        .BIRTH_DATE = f.cv.BIRTH_DATE,
                                        .BIRTH_ADDRESS = f.cv.BIRTH_PLACE,
                                        .STAFF_RANK_ID = f.e.STAFF_RANK_ID,
                                        .STAFF_RANK_NAME = f.r.NAME,
                                        .ORG_NAME = f.o.NAME_VN,
                                        .ORG_DESC = f.o.DESCRIPTION_PATH,
                                        .TITLE_NAME = f.p.NAME_VN,
                                        .INSURRANCE_NUM = f.i.SOBOOKNO,
                                        .is_ATHOME = If(f.s.IS_ATHOME = -1, -1, 0),
                                        .is_FOCUSED = If(f.s.IS_ATHOME = 0, -1, 0),
                                        .BOOK_NUM = f.i.HECARDNO,
                                        .ENTITLED_ID = f.s.ENTITLED_ID,
                                        .ENTITLED_NAME = f.t.NAME_VN,
                                        .START_DATE = f.s.START_DATE,
                                        .END_DATE = f.s.END_DATE,
                                        .CHANGE_MONTH = f.s.CHANGE_MONTH,
                                        .NUM_DATE = f.s.NUM_DATE,
                                        .DATE_OFF_BIRT = f.s.DATE_OFF_BIRT,
                                        .BABY_NAME = f.s.BABY_NAME,
                                        .NUM_BABY = f.s.NUM_BABY,
                                        .ACCUMULATED_DATE = f.s.ACCUMULATED_DATE,
                                        .ALLOWANCE_SAL = f.s.ALLOWANCE_SAL,
                                        .ALLOWANCE_MONEY = f.s.ALLOWANCE_MONEY,
                                        .ALLOWANCE_MONEY_EDIT = f.s.ALLOWANCE_MONEY_EDIT,
                                        .CONDITION_ALLOWANCE = f.s.CONDITION_ALLOWANCE,
                                        .TIME_ALLOWANCE = f.s.TIME_ALLOWANCE,
                                        .SDESC = f.s.SDESC,
                                        .WORK_STATUS = f.e.WORK_STATUS,
                                        .TER_LAST_DATE = f.e.TER_EFFECT_DATE,
                                        .APPROVAL_INSURRANCE = f.s.APPROVAL_INSURRANCE,
                                        .APPROVAL_DATE = f.s.APPROVAL_DATE,
                                        .APPROVAL_NUM = f.s.APPROVAL_NUM,
                                        .CREATED_DATE = f.s.CREATED_DATE,
                                        .CREATED_BY = f.s.CREATED_BY})

            Dim dateNow = Date.Now.Date
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257)
            'End If
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.START_DATE >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.END_DATE <= _filter.TO_DATE_SEARCH)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME.ToLower().Contains(_filter.STAFF_RANK_NAME.ToLower()))
            End If
            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) f.START_DATE.Value = _filter.START_DATE)
            End If
            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) f.END_DATE.Value = _filter.END_DATE)
            End If
            If _filter.CHANGE_MONTH.HasValue Then
                lst = lst.Where(Function(f) f.CHANGE_MONTH.Value = _filter.CHANGE_MONTH)
            End If
            If _filter.NUM_DATE.HasValue Then
                lst = lst.Where(Function(f) f.NUM_DATE.Value = _filter.NUM_DATE)
            End If
            If _filter.DATE_OFF_BIRT.HasValue Then
                lst = lst.Where(Function(f) f.DATE_OFF_BIRT.Value = _filter.DATE_OFF_BIRT)
            End If
            If Not String.IsNullOrEmpty(_filter.BABY_NAME) Then
                lst = lst.Where(Function(f) f.BABY_NAME.ToLower().Contains(_filter.BABY_NAME.ToLower()))
            End If
            If _filter.NUM_BABY.HasValue Then
                lst = lst.Where(Function(f) f.NUM_BABY.Value = _filter.NUM_BABY)
            End If
            If _filter.ACCUMULATED_DATE.HasValue Then
                lst = lst.Where(Function(f) f.ACCUMULATED_DATE.Value = _filter.ACCUMULATED_DATE)
            End If
            If _filter.ALLOWANCE_SAL.HasValue Then
                lst = lst.Where(Function(f) f.ALLOWANCE_SAL.Value = _filter.ALLOWANCE_SAL)
            End If
            If _filter.ALLOWANCE_MONEY.HasValue Then
                lst = lst.Where(Function(f) f.ALLOWANCE_MONEY.Value = _filter.ALLOWANCE_MONEY)
            End If
            If _filter.ALLOWANCE_MONEY_EDIT.HasValue Then
                lst = lst.Where(Function(f) f.ALLOWANCE_MONEY_EDIT.Value = _filter.ALLOWANCE_MONEY_EDIT)
            End If
            If Not String.IsNullOrEmpty(_filter.TIME_ALLOWANCE) Then
                lst = lst.Where(Function(f) f.TIME_ALLOWANCE.ToLower().Contains(_filter.TIME_ALLOWANCE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CONDITION_ALLOWANCE) Then
                lst = lst.Where(Function(f) f.CONDITION_ALLOWANCE.ToLower().Contains(_filter.CONDITION_ALLOWANCE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SDESC) Then
                lst = lst.Where(Function(f) f.SDESC.ToLower().Contains(_filter.SDESC.ToLower()))
            End If
            If _filter.APPROVAL_INSURRANCE.HasValue Then
                lst = lst.Where(Function(f) f.APPROVAL_INSURRANCE.Value = _filter.APPROVAL_INSURRANCE)
            End If
            If _filter.APPROVAL_DATE.HasValue Then
                lst = lst.Where(Function(f) f.APPROVAL_DATE.Value = _filter.APPROVAL_DATE)
            End If
            If _filter.APPROVAL_NUM.HasValue Then
                lst = lst.Where(Function(f) f.APPROVAL_NUM.Value = _filter.APPROVAL_NUM)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetRegimeManagerByID(ByVal _id As Decimal?) As INS_REMIGE_MANAGER_DTO
        Try
            Dim query = From s In Context.INS_REMIGE_MANAGER
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From t In Context.INS_ENTITLED_TYPE.Where(Function(ts) ts.ID = s.ENTITLED_ID)
                        From i In Context.INS_INFORMATION.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From r In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_MAX_V.Where(Function(f) f.EMPLOYEE_ID = s.EMPLOYEE_ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) c.WORKING_ID = f.ID And f.EFFECT_DATE <= Date.Now).DefaultIfEmpty
                        Where s.ID = _id
            Dim lst = query.Select(Function(f) New INS_REMIGE_MANAGER_DTO With {
                                        .ID = f.s.ID,
                                        .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                        .BIRTH_DATE = f.cv.BIRTH_DATE,
                                        .BIRTH_ADDRESS = f.cv.BIRTH_PLACE,
                                        .STAFF_RANK_ID = f.e.STAFF_RANK_ID,
                                        .STAFF_RANK_NAME = f.r.NAME,
                                        .ORG_NAME = f.o.NAME_VN,
                                        .ORG_DESC = f.o.DESCRIPTION_PATH,
                                        .TITLE_NAME = f.p.NAME_VN,
                                        .INSURRANCE_NUM = f.i.SOBOOKNO,
                                        .BOOK_NUM = f.i.HECARDNO,
                                        .ENTITLED_ID = f.s.ENTITLED_ID,
                                        .ENTITLED_NAME = f.t.NAME_VN,
                                        .START_DATE = f.s.START_DATE,
                                        .END_DATE = f.s.END_DATE,
                                        .CHANGE_MONTH = f.s.CHANGE_MONTH,
                                        .NUM_DATE = f.s.NUM_DATE,
                                        .is_ATHOME = If(f.s.IS_ATHOME = -1, -1, 0),
                                        .is_FOCUSED = If(f.s.IS_ATHOME = 0, -1, 0),
                                        .DATE_OFF_BIRT = f.s.DATE_OFF_BIRT,
                                        .BABY_NAME = f.s.BABY_NAME,
                                        .NUM_BABY = f.s.NUM_BABY,
                                        .ACCUMULATED_DATE = f.s.ACCUMULATED_DATE,
                                        .ALLOWANCE_SAL = Decimal.Round(f.w.SAL_BASIC * If(f.w.PERCENT_SALARY.HasValue, f.w.PERCENT_SALARY, 0) / 100) + If((From h In Context.HU_WORKING_ALLOW.Where(Function(h) h.HU_WORKING_ID = f.w.ID And h.IS_INSURRANCE = -1) Select h.AMOUNT).Sum().HasValue, (From h In Context.HU_WORKING_ALLOW.Where(Function(h) h.HU_WORKING_ID = f.w.ID And h.IS_INSURRANCE = -1) Select h.AMOUNT).Sum(), 0),
                                        .ALLOWANCE_MONEY = f.s.ALLOWANCE_MONEY,
                                        .ALLOWANCE_MONEY_EDIT = f.s.ALLOWANCE_MONEY_EDIT,
                                        .CONDITION_ALLOWANCE = f.s.CONDITION_ALLOWANCE,
                                        .TIME_ALLOWANCE = f.s.TIME_ALLOWANCE,
                                        .SDESC = f.s.SDESC,
                                        .APPROVAL_INSURRANCE = f.s.APPROVAL_INSURRANCE,
                                        .APPROVAL_DATE = f.s.APPROVAL_DATE,
                                        .APPROVAL_NUM = f.s.APPROVAL_NUM,
                                        .CREATED_DATE = f.s.CREATED_DATE,
                                        .CREATED_BY = f.s.CREATED_BY}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_REMIGE_MANAGER
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_REMIGE_MANAGER.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ENTITLED_ID = objTitle.ENTITLED_ID
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.END_DATE = objTitle.END_DATE
            objTitleData.CHANGE_MONTH = objTitle.CHANGE_MONTH
            objTitleData.NUM_DATE = objTitle.NUM_DATE
            objTitleData.DATE_OFF_BIRT = objTitle.DATE_OFF_BIRT
            objTitleData.BABY_NAME = objTitle.BABY_NAME
            objTitleData.NUM_BABY = objTitle.NUM_BABY
            objTitleData.IS_ATHOME = objTitle.is_ATHOME
            objTitleData.ACCUMULATED_DATE = objTitle.ACCUMULATED_DATE
            objTitleData.ALLOWANCE_SAL = objTitle.ALLOWANCE_SAL
            objTitleData.ALLOWANCE_MONEY = objTitle.ALLOWANCE_MONEY
            objTitleData.ALLOWANCE_MONEY_EDIT = objTitle.ALLOWANCE_MONEY_EDIT
            objTitleData.TIME_ALLOWANCE = objTitle.TIME_ALLOWANCE
            objTitleData.CONDITION_ALLOWANCE = objTitle.CONDITION_ALLOWANCE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.APPROVAL_INSURRANCE = objTitle.APPROVAL_INSURRANCE
            objTitleData.APPROVAL_DATE = objTitle.APPROVAL_DATE
            objTitleData.APPROVAL_NUM = objTitle.APPROVAL_NUM
            Context.INS_REMIGE_MANAGER.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyRegimeManager(ByVal objTitle As INS_REMIGE_MANAGER_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_REMIGE_MANAGER With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_REMIGE_MANAGER Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ENTITLED_ID = objTitle.ENTITLED_ID
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.END_DATE = objTitle.END_DATE
            objTitleData.CHANGE_MONTH = objTitle.CHANGE_MONTH
            objTitleData.NUM_DATE = objTitle.NUM_DATE
            objTitleData.DATE_OFF_BIRT = objTitle.DATE_OFF_BIRT
            objTitleData.BABY_NAME = objTitle.BABY_NAME
            objTitleData.NUM_BABY = objTitle.NUM_BABY
            objTitleData.IS_ATHOME = objTitle.is_ATHOME
            objTitleData.ACCUMULATED_DATE = objTitle.ACCUMULATED_DATE
            objTitleData.ALLOWANCE_SAL = objTitle.ALLOWANCE_SAL
            objTitleData.ALLOWANCE_MONEY = objTitle.ALLOWANCE_MONEY
            objTitleData.ALLOWANCE_MONEY_EDIT = objTitle.ALLOWANCE_MONEY_EDIT
            objTitleData.CONDITION_ALLOWANCE = objTitle.CONDITION_ALLOWANCE
            objTitleData.TIME_ALLOWANCE = objTitle.TIME_ALLOWANCE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.APPROVAL_INSURRANCE = objTitle.APPROVAL_INSURRANCE
            objTitleData.APPROVAL_DATE = objTitle.APPROVAL_DATE
            objTitleData.APPROVAL_NUM = objTitle.APPROVAL_NUM
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function
    Public Function ValidateGroupRegime(ByVal _validate As INS_GROUP_REGIMESDTO)
        Dim query
        Try
            If _validate.REGIMES_NAME <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_GROUP_REGIMES
                             Where p.REGIMES_NAME.ToUpper = _validate.REGIMES_NAME.ToUpper _
                             And p.ID = _validate.ID And p.ACTFLG.Trim.ToUpper.Contains("A")).FirstOrDefault
                    Return (Not query Is Nothing)
                Else
                    query = (From p In Context.INS_GROUP_REGIMES
                             Where p.REGIMES_NAME.ToUpper = _validate.REGIMES_NAME.ToUpper And p.ACTFLG.Trim.ToUpper.Contains("A")).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_GROUP_REGIMES
                             Where p.ID = _validate.ID And p.ACTFLG.Trim.ToUpper.Contains("A")).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ValidateListProgram(ByVal _validate As INS_LIST_PROGRAMDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_LIST_PROGRAM
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID And p.IS_DELETED = 0).FirstOrDefault
                Else
                    query = (From p In Context.INS_LIST_PROGRAM
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_LIST_PROGRAM
                             Where p.ID = _validate.ID And p.IS_DELETED = 0).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function ValidateListContract(ByVal _validate As INS_LIST_CONTRACTDTO)
        Dim query
        Try
            If _validate.CONTRACT_INS_NO <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_LIST_CONTRACT
                             Where p.CONTRACT_INS_NO.ToUpper = _validate.CONTRACT_INS_NO.ToUpper _
                             And p.ID <> _validate.ID And p.IS_DELETED = 0).FirstOrDefault
                Else
                    query = (From p In Context.INS_LIST_CONTRACT
                             Where p.CONTRACT_INS_NO.ToUpper = _validate.CONTRACT_INS_NO.ToUpper And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_LIST_CONTRACT
                             Where p.ID = _validate.ID And p.IS_DELETED = 0).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function ValidateRegimeManager(ByVal _validate As INS_REMIGE_MANAGER_DTO)
        Dim query
        Try
            If _validate.START_DATE IsNot Nothing And _validate.END_DATE IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_REMIGE_MANAGER
                             Where p.START_DATE >= _validate.START_DATE _
                             And p.END_DATE <= _validate.END_DATE _
                             And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_REMIGE_MANAGER
                             Where p.START_DATE >= _validate.START_DATE _
                             And p.END_DATE <= _validate.END_DATE _
                             And p.EMPLOYEE_ID = _validate.EMPLOYEE_ID).FirstOrDefault
                End If


                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteRegimeManager(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstRegimeManagerData As List(Of INS_REMIGE_MANAGER)
        Try
            lstRegimeManagerData = (From p In Context.INS_REMIGE_MANAGER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstRegimeManagerData.Count - 1
                Context.INS_REMIGE_MANAGER.DeleteObject(lstRegimeManagerData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".RegimeManager")
            Throw ex
        End Try
    End Function

    Public Function Validate_KhamThai(ByVal _validate As INS_REMIGE_MANAGER_DTO) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.VALIDATE_KHAMTHAI",
                                               New With {.P_ENTITLED_ID = _validate.ENTITLED_ID,
                                                         .P_EMPLOYEE_ID = _validate.EMPLOYEE_ID,
                                                         .P_END_DATE = _validate.END_DATE,
                                                         .P_ID = _validate.ID,
                                                         .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
            Return Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return Nothing
        End Try
    End Function
#End Region

#Region "Khái báo nhóm bảo hiểm Sun care"
    Public Function GetGroup_SunCare(ByVal _filter As INS_GROUP_SUN_CAREDTO,
                                    ByVal _param As PARAMDTO,
                                    Optional ByRef Total As Integer = 0,
                                    Optional ByVal PageIndex As Integer = 0,
                                    Optional ByVal PageSize As Integer = Integer.MaxValue,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_GROUP_SUN_CAREDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.INS_GROUP_SUN_CARE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t1 In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID_OLD).DefaultIfEmpty
                        From tn In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID_NEW).DefaultIfEmpty
                        From so In Context.HU_STAFF_RANK.Where(Function(S) S.ID = p.STAFF_RANK_ID_OLD).DefaultIfEmpty
                        From sn In Context.HU_STAFF_RANK.Where(Function(S) S.ID = p.STAFF_RANK_ID_NEW).DefaultIfEmpty
                        From co In Context.INS_COST_FOLLOW_LEVER.Where(Function(F) F.ID = p.COST_LEVER_ID_OLD).DefaultIfEmpty
                        From cn In Context.INS_COST_FOLLOW_LEVER.Where(Function(F) F.ID = p.COST_LEVER_ID_NEW).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
            Dim lst = query.Select(Function(p) New INS_GROUP_SUN_CAREDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .TITLE_ID_OLD = p.p.TITLE_ID_OLD,
                                       .TITLE_NAME_OLD = p.t1.NAME_VN,
                                       .TITLE_ID_NEW = p.p.TITLE_ID_NEW,
                                       .TITLE_NAME_NEW = p.tn.NAME_VN,
                                       .STAFF_RANK_ID_OLD = p.p.STAFF_RANK_ID_OLD,
                                       .STAFF_RANK_NAME_OLD = p.so.NAME,
                                       .STAFF_RANK_ID_NEW = p.p.STAFF_RANK_ID_NEW,
                                       .STAFF_RANK_NAME_NEW = p.sn.NAME,
                                       .COST_LEVER_ID_OLD = p.p.COST_LEVER_ID_OLD,
                                       .COST_LEVER_NAME_OLD = p.co.COST_NAME,
                                       .COST_LEVER_ID_NEW = p.p.COST_LEVER_ID_NEW,
                                       .COST_LEVER_NAME_NEW = p.cn.COST_NAME,
                                       .COST_MONEY = p.p.COST_MONEY,
                                       .EFFECTDATE_COST_NEW = p.p.EFFECTDATE_COST_NEW,
                                       .NOTE = p.p.NOTE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            Dim dateNow = Date.Now.Date
            'If _filter.IS_TERMINATE Then
            '    lst = lst.Where(Function(f) f.WORK_STATUS = 257 And f.TER_LAST_DATE <= dateNow)
            'Else
            '    lst = lst.Where(Function(f) f.WORK_STATUS <> 257)
            'End If
            If Not _filter.IS_TERMINATE Then
                lst = lst.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If
            If _filter.FROM_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE_COST_NEW >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE_COST_NEW <= _filter.TO_DATE_SEARCH)
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_OLD) Then
                lst = lst.Where(Function(f) f.TITLE_NAME_OLD.ToLower().Contains(_filter.TITLE_NAME_OLD.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME_NEW) Then
                lst = lst.Where(Function(f) f.TITLE_NAME_NEW.ToLower().Contains(_filter.TITLE_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME_OLD) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME_OLD.ToLower().Contains(_filter.STAFF_RANK_NAME_OLD.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STAFF_RANK_NAME_NEW) Then
                lst = lst.Where(Function(f) f.STAFF_RANK_NAME_NEW.ToLower().Contains(_filter.STAFF_RANK_NAME_NEW.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.COST_LEVER_NAME_OLD) Then
                lst = lst.Where(Function(f) f.COST_LEVER_NAME_OLD.ToLower().Contains(_filter.COST_LEVER_NAME_OLD.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.COST_LEVER_NAME_NEW) Then
                lst = lst.Where(Function(f) f.COST_LEVER_NAME_NEW.ToLower().Contains(_filter.COST_LEVER_NAME_NEW.ToLower()))
            End If
            If _filter.EFFECTDATE_COST_NEW.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE_COST_NEW.Value = _filter.EFFECTDATE_COST_NEW)
            End If
            If _filter.COST_MONEY Then
                lst = lst.Where(Function(f) f.COST_LEVER_NAME_NEW = _filter.COST_MONEY)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetGroup_SunCareById(ByVal _id As Decimal?) As INS_GROUP_SUN_CAREDTO
        Try

            Dim query = From p In Context.INS_GROUP_SUN_CARE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t1 In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID_OLD).DefaultIfEmpty
                        From tn In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID_NEW).DefaultIfEmpty
                        From so In Context.HU_STAFF_RANK.Where(Function(S) S.ID = p.STAFF_RANK_ID_OLD).DefaultIfEmpty
                        From sn In Context.HU_STAFF_RANK.Where(Function(S) S.ID = p.STAFF_RANK_ID_NEW).DefaultIfEmpty
                        From co In Context.INS_COST_FOLLOW_LEVER.Where(Function(F) F.ID = p.COST_LEVER_ID_OLD).DefaultIfEmpty
                        From cn In Context.INS_COST_FOLLOW_LEVER.Where(Function(F) F.ID = p.COST_LEVER_ID_NEW).DefaultIfEmpty
                        Where p.ID = _id

            Dim lst = query.Select(Function(p) New INS_GROUP_SUN_CAREDTO With {
                                        .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.o.NAME_EN,
                                       .TITLE_ID_OLD = p.p.TITLE_ID_OLD,
                                       .TITLE_NAME_OLD = p.t1.NAME_VN,
                                       .TITLE_ID_NEW = p.p.TITLE_ID_NEW,
                                       .TITLE_NAME_NEW = p.tn.NAME_VN,
                                       .STAFF_RANK_ID_OLD = p.p.STAFF_RANK_ID_OLD,
                                       .STAFF_RANK_NAME_OLD = p.so.NAME,
                                       .STAFF_RANK_ID_NEW = p.p.STAFF_RANK_ID_NEW,
                                       .STAFF_RANK_NAME_NEW = p.sn.NAME,
                                       .COST_LEVER_ID_OLD = p.p.COST_LEVER_ID_OLD,
                                       .COST_LEVER_NAME_OLD = p.co.COST_NAME,
                                       .COST_LEVER_ID_NEW = p.p.COST_LEVER_ID_NEW,
                                       .COST_LEVER_NAME_NEW = p.cn.COST_NAME,
                                       .COST_MONEY = p.p.COST_MONEY,
                                       .EFFECTDATE_COST_NEW = p.p.EFFECTDATE_COST_NEW,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG}).FirstOrDefault
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertGroup_SunCare(ByVal objGroupINS As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try
            Dim objData As New INS_GROUP_SUN_CARE
            objData.ID = Utilities.GetNextSequence(Context, Context.INS_GROUP_SUN_CARE.EntitySet.Name)
            objData.EMPLOYEE_ID = objGroupINS.EMPLOYEE_ID
            objData.TITLE_ID_OLD = objGroupINS.TITLE_ID_OLD
            objData.TITLE_ID_NEW = objGroupINS.TITLE_ID_NEW
            objData.STAFF_RANK_ID_OLD = objGroupINS.STAFF_RANK_ID_OLD
            objData.STAFF_RANK_ID_NEW = objGroupINS.STAFF_RANK_ID_NEW
            objData.COST_LEVER_ID_OLD = objGroupINS.COST_LEVER_ID_OLD
            objData.COST_LEVER_ID_NEW = objGroupINS.COST_LEVER_ID_NEW
            objData.COST_MONEY = objGroupINS.COST_MONEY
            objData.EFFECTDATE_COST_NEW = objGroupINS.EFFECTDATE_COST_NEW
            objData.NOTE = objGroupINS.NOTE
            Context.INS_GROUP_SUN_CARE.AddObject(objData)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyGroup_SunCare(ByVal objData As INS_GROUP_SUN_CAREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objDataData As New INS_GROUP_SUN_CARE With {.ID = objData.ID}
        Try
            objDataData = (From p In Context.INS_GROUP_SUN_CARE Where p.ID = objData.ID).SingleOrDefault
            Dim exists = (From r In Context.INS_GROUP_SUN_CARE Where r.EMPLOYEE_ID = objData.EMPLOYEE_ID And r.ID = objData.ID).Any
            If exists Then
                objDataData.EMPLOYEE_ID = objData.EMPLOYEE_ID
                objDataData.TITLE_ID_OLD = objData.TITLE_ID_OLD
                objDataData.TITLE_ID_NEW = objData.TITLE_ID_NEW
                objDataData.STAFF_RANK_ID_OLD = objData.STAFF_RANK_ID_OLD
                objDataData.STAFF_RANK_ID_NEW = objData.STAFF_RANK_ID_NEW
                objDataData.COST_LEVER_ID_OLD = objData.COST_LEVER_ID_OLD
                objDataData.COST_LEVER_ID_NEW = objData.COST_LEVER_ID_NEW
                objDataData.COST_MONEY = objData.COST_MONEY
                objDataData.EFFECTDATE_COST_NEW = objData.EFFECTDATE_COST_NEW
                objDataData.NOTE = objData.NOTE
                Context.SaveChanges(log)
                gID = objDataData.ID
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function DeleteGroup_SunCare(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstl As List(Of INS_GROUP_SUN_CARE)
        Try
            lstl = (From p In Context.INS_GROUP_SUN_CARE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstl.Count - 1
                Context.INS_GROUP_SUN_CARE.DeleteObject(lstl(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region

#Region "Báo cáo bảo hiểm"
    Public Function GetReportList() As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETREPORTLIST",
                                           New With {.CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        Try
            Dim query As IQueryable(Of Se_ReportDTO)
            If log.Username.ToUpper <> "ADMIN" And log.Username.ToUpper <> "SYS.ADMIN" And log.Username.ToUpper <> "HR.ADMIN" Then
                query = From u In Context.SE_USER
                        From p In u.SE_REPORT
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {.ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .NAME = p.NAME,
                                                        .MODULE_ID = p.MODULE_ID}
            Else
                query = From p In Context.SE_REPORT
                        Where p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {.ID = p.ID,
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetD02Tang(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_D02TS_TANG",
                                           New With {.P_MONTH = p_MONTH,
                                                     .P_YEAR = p_YEAR,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetD02Giam(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_D02TS_GIAM",
                                           New With {.P_MONTH = p_MONTH,
                                                     .P_YEAR = p_YEAR,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetC70_HD(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_C70_HD",
                                           New With {.P_MONTH = p_MONTH,
                                                     .P_YEAR = p_YEAR,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetQuyLuongBH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_QUYLUONGBH",
                                           New With {.P_MONTH = p_MONTH,
                                                     .P_YEAR = p_YEAR,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetDsBHSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_DSBH_SUNCARE",
                                           New With {.P_TUNGAY = p_Tungay,
                                                     .P_DENNGAY = p_Toingay,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetDsDieuChinhSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_DSDC_SUNCARE",
                                           New With {.P_TUNGAY = p_Tungay,
                                                     .P_DENNGAY = p_Toingay,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetChiPhiSunCare(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Username As String, ByVal p_Org_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.INS_CHIPHISUNCARE",
                                           New With {.P_TUNGAY = p_Tungay,
                                                     .P_DENNGAY = p_Toingay,
                                                     .P_USERNAME = p_Username,
                                                     .P_ORG_ID = p_Org_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function GetOrgInfo(ByVal p_Tungay As Date, ByVal p_Toingay As Date, ByVal p_Org_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.RPT_ORG_INFO",
                                               New With {.P_ORG_ID = p_Org_ID,
                                                         .P_TUNGAY = p_Tungay,
                                                         .P_DENNGAY = p_Toingay,
                                                         .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "INS")
            Return Nothing
        End Try
    End Function

    Public Function GetOrgInfoMONTH(ByVal p_MONTH As Decimal, ByVal p_YEAR As Decimal, ByVal p_Org_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE_REPORT.RPT_ORG_INFOMONTH",
                                               New With {.P_ORG_ID = p_Org_ID,
                                                         .P_MONTH = p_MONTH,
                                                         .P_YEAR = p_YEAR,
                                                         .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "INS")
            Return Nothing
        End Try
    End Function
#End Region

#Region " Tổng hợp loại nghỉ bảo hiểm"
    Public Function GetInsArisingLeaveSheet(ByVal _filter As INS_ARISING_LEAVESHEET_DTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     ByVal OrgId As Integer,
                                     ByVal IsDissolve As Integer,
                                     ByVal EntiledID As Integer,
                                     ByVal Fillter As String,
                                     ByVal log As UserLog,
                                     Optional ByVal Sorts As String = "EMPLOYEE_CODE asc") As List(Of INS_ARISING_LEAVESHEET_DTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using
            Dim query = From p In Context.INS_ARISING_LEAVESHEET
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From i In Context.INS_LIST_INSURANCE.Where(Function(f) f.ID = p.INS_ORG_ID).DefaultIfEmpty
                        From ose In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = o.ID And se.USERNAME.ToUpper = log.Username.ToUpper)
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From m In Context.AT_TIME_MANUAL.Where(Function(f) f.ID = p.MANUAL_ID)

            Dim lst = query.Select(Function(f) New INS_ARISING_LEAVESHEET_DTO With {
                                        .ID = f.p.ID,
                                        .EMPLOYEE_ID = f.p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                        .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                        .ORG_ID = f.p.ORG_ID,
                                        .ORG_NAME = f.o.NAME_VN,
                                        .TITLE_ID = f.p.TITLE_ID,
                                        .TITLE_NAME = f.t.NAME_VN,
                                        .MANUAL_ID = f.p.MANUAL_ID,
                                        .MANUAL_NAME = f.m.NAME,
                                        .LEAVE_FROM = f.p.LEAVE_FROM,
                                        .LEAVE_TO = f.p.LEAVE_TO,
                                        .DAY_NUM = f.p.DAY_NUM,
                                        .FROM_MONTH = f.p.FROM_MONTH,
                                        .TO_MONTH = f.p.TO_MONTH,
                                        .ORG_DESC = f.o.DESCRIPTION_PATH,
                                        .CREATED_DATE = f.p.CREATED_DATE,
                                        .CREATED_BY = f.p.CREATED_BY,
                                        .INS_ORG_ID = f.p.INS_ORG_ID,
                                        .INS_ORG_NAME = f.o.NAME_VN})

            If Not String.IsNullOrEmpty(_filter.MANUAL_ID) Then
                lst = lst.Where(Function(f) _filter.MANUAL_ID.Contains(f.MANUAL_ID))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_CODE.ToLower().Contains(_filter.EMPLOYEE_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(f) f.ORG_NAME.ToLower().Contains(_filter.ORG_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.MANUAL_NAME) Then
                lst = lst.Where(Function(f) f.MANUAL_NAME.ToLower().Contains(_filter.MANUAL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.INS_ORG_NAME) Then
                lst = lst.Where(Function(f) f.INS_ORG_NAME.ToLower().Contains(_filter.INS_ORG_NAME.ToLower()))
            End If
            If _filter.LEAVE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.LEAVE_FROM = _filter.LEAVE_FROM)
            End If
            If _filter.LEAVE_TO.HasValue Then
                lst = lst.Where(Function(f) f.LEAVE_TO = _filter.LEAVE_TO)
            End If
            If _filter.FROM_MONTH.HasValue Then
                lst = lst.Where(Function(f) f.FROM_MONTH = _filter.FROM_MONTH)
            End If
            If _filter.TO_MONTH.HasValue Then
                lst = lst.Where(Function(f) f.TO_MONTH = _filter.TO_MONTH)
            End If
            If _filter.DAY_NUM.HasValue Then
                lst = lst.Where(Function(f) f.DAY_NUM = _filter.DAY_NUM)
            End If

            If _filter.START_DATE.HasValue Then
                lst = lst.Where(Function(f) (f.LEAVE_FROM >= _filter.START_DATE) Or (f.LEAVE_TO >= _filter.START_DATE))
            End If

            If _filter.END_DATE.HasValue Then
                lst = lst.Where(Function(f) (f.LEAVE_FROM <= _filter.END_DATE) Or (f.LEAVE_TO <= _filter.END_DATE))
            End If

            If _filter.START_MONTH.HasValue Then
                lst = lst.Where(Function(f) (f.FROM_MONTH >= _filter.START_MONTH) Or (f.TO_MONTH >= _filter.START_MONTH))
            End If

            If _filter.END_MONTH.HasValue Then
                lst = lst.Where(Function(f) (f.FROM_MONTH <= _filter.END_MONTH.Value) Or (f.TO_MONTH <= _filter.END_MONTH))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GET_INS_ARSING_LEAVE_SHEET(ByVal OrgId As Integer,
                                               ByVal IsDissolve As Integer,
                                               ByVal startDate As Date,
                                               ByVal endDate As Date,
                                               ByVal LeaveType As String,
                                               ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_LIST.GET_INS_ARSING_LEAVE_SHEET",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve,
                                               .P_START_DATE = startDate,
                                               .P_END_DATE = endDate,
                                               .P_LEAVE_TYPE = LeaveType})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function UPDATE_INS_ARSING_LEAVE_SHEET(ByVal P_LIST_ID As String,
                                                  ByVal P_FROM_MONTH As Date,
                                                  ByVal P_TO_MONTH As Date,
                                                  ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_LIST.UPDATE_INS_ARSING_LEAVE_SHEET",
                                     New With {.P_LIST_ID = P_LIST_ID,
                                               .P_FROM_MONTH = P_FROM_MONTH,
                                               .P_TO_MONTH = P_TO_MONTH,
                                               .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function INSERT_INS_ARSING_TANG(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_LIST.INSERT_INS_ARSING_TANG",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORG_ID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve,
                                               .P_FROM_MONTH = P_FROM_MONTH,
                                               .P_TO_MONTH = P_TO_MONTH})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function INSERT_INS_ARSING_GIAM(ByVal P_FROM_MONTH As Date,
                                           ByVal P_TO_MONTH As Date,
                                           ByVal OrgId As Integer,
                                           ByVal IsDissolve As Integer,
                                           ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_LIST.INSERT_INS_ARSING_GIAM",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORG_ID = OrgId,
                                               .P_ISDISSOLVE = IsDissolve,
                                               .P_FROM_MONTH = P_FROM_MONTH,
                                               .P_TO_MONTH = P_TO_MONTH})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function CHECK_INS_ORG(ByVal P_EMP_ID As Decimal,
                                  ByVal P_INS_ORG_ID As Decimal,
                                  ByVal P_ARISING_FROM_MONTH As Date,
                                  ByRef pDate As String) As Integer
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_INS_LIST.CHECK_INS_ORG",
                                    New With {.P_EMP_ID = P_EMP_ID,
                                            .P_INS_ORG_ID = P_INS_ORG_ID,
                                            .P_ARISING_FROM_MONTH = P_ARISING_FROM_MONTH,
                                            .CUR = cls.OUT_CURSOR})

            End Using

            pDate = dtData(0)("ARISING_FROM_MONTH")

            Return dtData(0)("A")
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function CHECK_INS_ORG_TANG_GIAM(ByVal P_INS_ORG_ID As Decimal) As String
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_INS_LIST.CHECK_INS_ORG_TANG_GIAM",
                                    New With {.P_INS_ORG_ID = P_INS_ORG_ID,
                                             .CUR = cls.OUT_CURSOR})

            End Using
            Return dtData(0)("A")
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Public Function CHECK_INS_MANUAL(ByVal P_EMP_ID As Decimal,
                                  ByVal P_INS_ORG_ID As Decimal,
                                  ByVal P_ARISING_FROM_MONTH As Date,
                                  ByRef pDate As String) As Integer
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                dtData = cls.ExecuteStore("PKG_INS_LIST.CHECK_INS_MANUAL",
                                    New With {.P_EMP_ID = P_EMP_ID,
                                            .P_INS_ORG_ID = P_INS_ORG_ID,
                                            .P_ARISING_FROM_MONTH = P_ARISING_FROM_MONTH,
                                            .CUR = cls.OUT_CURSOR})

            End Using

            pDate = dtData(0)("ARISING_FROM_MONTH")

            Return dtData(0)("A")
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function


    Public Function UPDATE_INS_INFORMATION(ByVal P_EMP_ID As Decimal,
                                           ByVal P_ARISING_FROM_MONTH As Date) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_LIST.UPDATE_INS_INFORMATION",
                                     New With {.P_EMP_ID = P_EMP_ID,
                                               .P_ARISING_FROM_MONTH = P_ARISING_FROM_MONTH})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

#End Region

#Region "Quá trình bảo hiểm trước khi vào công ty"
    Public Function GetInsWorkingBefore(ByVal _filter As INS_WORKING_BEFOREDTO,
                              ByVal OrgId As Integer,
                              ByVal Fillter As String,
                              Optional ByVal PageIndex As Integer = 0,
                              Optional ByVal PageSize As Integer = Integer.MaxValue,
                              Optional ByRef Total As Integer = 0,
                              Optional ByVal Sorts As String = "CREATED_DATE desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_WORKING_BEFOREDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = OrgId,
                                           .P_ISDISSOLVE = True})
            End Using
            Dim query = From s In Context.INS_WORKING_BEFORE
                        From e In Context.HU_EMPLOYEE.Where(Function(es) es.ID = s.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(eo) eo.ID = e.ORG_ID)
                        From p In Context.HU_TITLE.Where(Function(ep) ep.ID = e.TITLE_ID)
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        Order By e.EMPLOYEE_CODE Ascending
            Dim lst = query.Select(Function(f) New INS_WORKING_BEFOREDTO With {
                                       .ID = f.s.ID,
                                       .EMPLOYEE_ID = f.s.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = f.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = f.e.FULLNAME_VN,
                                       .ORG_NAME = f.o.NAME_VN,
                                       .ORG_DESC = f.o.DESCRIPTION_PATH,
                                       .TITLE = f.s.TITLE,
                                       .FROM_MONTH = f.s.FROM_MONTH,
                                       .TO_MONTH = f.s.TO_MONTH,
                                       .INS_SALARY = f.s.INS_SALARY,
                                       .SALARY = f.s.SALARY,
                                       .CREATED_BY = f.s.CREATED_BY,
                                       .TOXIC = f.s.TOXIC,
                                       .COEFFICIENT = f.s.COEFFICIENT,
                                       .COMPANY = f.s.COMPANY,
                                       .REMARK = f.s.REMARK,
                                       .TITTLE_EMP_NAME = f.p.NAME_VN,
                                       .CREATED_DATE = f.s.CREATED_DATE,
                                       .CREATED_LOG = f.s.CREATED_LOG})
            Dim dateNow = Date.Now.Date
            'If _filter.FROM_DATE_SEARCH.HasValue Then
            '    lst = lst.Where(Function(f) f.THOIDIEMHUONG >= _filter.FROM_DATE_SEARCH)
            'End If
            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function ModifyInsWorkingBefore(ByVal obj As INS_WORKING_BEFOREDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As INS_WORKING_BEFORE
        Try
            objData = (From p In Context.INS_WORKING_BEFORE Where p.ID = obj.ID).SingleOrDefault
            objData.ID = obj.ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.FROM_MONTH = obj.FROM_MONTH
            objData.TO_MONTH = obj.TO_MONTH
            objData.COMPANY = obj.COMPANY
            objData.TITLE = obj.TITLE
            objData.INS_SALARY = obj.INS_SALARY
            objData.SALARY = obj.SALARY
            objData.TOXIC = obj.TOXIC
            objData.COEFFICIENT = obj.COEFFICIENT
            objData.REMARK = obj.REMARK
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function InsertInsWorkingBefore(ByVal obj As INS_WORKING_BEFOREDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New INS_WORKING_BEFORE
        Dim iCount As Integer = 0
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.INS_WORKING_BEFORE.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.FROM_MONTH = obj.FROM_MONTH
            objData.TO_MONTH = obj.TO_MONTH
            objData.COMPANY = obj.COMPANY
            objData.TITLE = obj.TITLE
            objData.INS_SALARY = obj.INS_SALARY
            objData.SALARY = obj.SALARY
            objData.TOXIC = obj.TOXIC
            objData.COEFFICIENT = obj.COEFFICIENT
            objData.REMARK = obj.REMARK
            Context.INS_WORKING_BEFORE.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function Delete_InsWorkingBefore(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lst As List(Of INS_WORKING_BEFORE)
        Try
            lst = (From p In Context.INS_WORKING_BEFORE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lst.Count - 1
                Context.INS_WORKING_BEFORE.DeleteObject(lst(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function INPORT_InsWorkingBefore(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_INS_BUSINESS.INPORT_INS_WORKING_BEFORE",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
            Return False
        End Try
    End Function
    Public Function ValidateWorkingBefore(ByVal _validate As INS_WORKING_BEFOREDTO)
        Dim query
        Try

            If _validate.ID <> 0 Then
                query = (From p In Context.INS_WORKING_BEFORE
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.ID <> _validate.ID And p.FROM_MONTH >= _validate.FROM_MONTH And p.TO_MONTH <= _validate.TO_MONTH).FirstOrDefault
            Else
                query = (From p In Context.INS_WORKING_BEFORE
                         Where p.EMPLOYEE_ID = _validate.EMPLOYEE_ID _
                             And p.FROM_MONTH >= _validate.FROM_MONTH And p.TO_MONTH = _validate.TO_MONTH).FirstOrDefault
            End If
            Return (query Is Nothing)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region


End Class