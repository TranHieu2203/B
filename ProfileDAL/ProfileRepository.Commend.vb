Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository
#Region "health exam"
    Public Function DeleteHealthExam(ByVal lstID As List(Of Decimal)) As Boolean
        Dim objCommendData As HU_HEALTH_EXAM
        Try
            For Each item In lstID
                objCommendData = (From p In Context.HU_HEALTH_EXAM Where item = p.ID).SingleOrDefault

                Context.HU_HEALTH_EXAM.DeleteObject(objCommendData)
            Next

            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


    Public Function GetHealthExam(ByVal _filter As HealthExamDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HealthExamDTO)

        Try
            Dim listCommend As New List(Of HealthExamDTO)

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' Lấy toàn bộ dữ liệu theo employee
            Dim queryEmp = From p In Context.HU_HEALTH_EXAM
                           From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                           From org In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                           From ty In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.LOAI_SUCKHOE).DefaultIfEmpty
                           Where (org.USERNAME.ToUpper = log.Username.ToUpper) Or (p.EMPLOYEE_ID = 0)

            If _filter.FROM_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EFFECT_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EFFECT_DATE <= _filter.TO_DATE)
            End If

            If Not _filter.IS_TERMINATE Then
                queryEmp = queryEmp.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.LOAISK_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.ty.NAME_VN.ToUpper.Contains(_filter.LOAISK_NAME.ToUpper))
            End If

            If _filter.NHOM_MAU IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.NHOM_MAU.ToUpper.Contains(_filter.NHOM_MAU.ToUpper))
            End If
            If _filter.NHIP_TIM IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.NHIP_TIM.ToUpper.Contains(_filter.NHIP_TIM.ToUpper))
            End If
            If _filter.HUYET_AP IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.HUYET_AP.ToUpper.Contains(_filter.HUYET_AP.ToUpper))
            End If
            If _filter.MAT_PHAI IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.MAT_PHAI.ToUpper.Contains(_filter.MAT_PHAI.ToUpper))
            End If
            If _filter.MAT_TRAI IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.MAT_TRAI.ToUpper.Contains(_filter.MAT_TRAI.ToUpper))
            End If
            If _filter.CHIEU_CAO IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.CHIEU_CAO.ToUpper.Contains(_filter.CHIEU_CAO.ToUpper))
            End If
            If _filter.CAN_NANG IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.CAN_NANG.ToUpper.Contains(_filter.CAN_NANG.ToUpper))
            End If
            If _filter.NOTE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If


            Dim lstEmp = queryEmp.Select(Function(p) New HealthExamDTO With {
                                                       .ID = p.p.ID,
                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                       .ORG_NAME = p.o.NAME_VN,
                                                       .TITLE_NAME = p.t.NAME_VN,
                                                       .YEAR = p.p.YEAR,
                                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                                       .LOAISK_ID = p.p.LOAI_SUCKHOE,
                                                       .LOAISK_NAME = p.ty.NAME_VN,
                                                       .NHOM_MAU = p.p.NHOM_MAU,
                                                       .NHIP_TIM = p.p.NHIP_TIM,
                                                       .HUYET_AP = p.p.HUYET_AP,
                                                       .MAT_TRAI = p.p.MAT_TRAI,
                                                       .MAT_PHAI = p.p.MAT_PHAI,
                                                       .CHIEU_CAO = p.p.CHIEU_CAO,
                                                       .CAN_NANG = p.p.CAN_NANG,
                                                       .NOTE = p.p.NOTE,
                                                       .CREATED_DATE = p.p.CREATED_DATE})

            lstEmp = lstEmp.OrderBy(Sorts)
            Total = lstEmp.Count
            lstEmp = lstEmp.Skip(PageIndex * PageSize).Take(PageSize)

            listCommend = lstEmp.ToList()

            Return listCommend
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


#End Region
#Region "Commend"
    'khi phe duyet thi bat buoc nhap file dinh kem
    Public Function CheckHasFileComend(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim comends = Context.HU_COMMEND.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each commend As HU_COMMEND In comends
                If commend.FILENAME Is Nothing Or commend.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCommend(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)

        Try
            Dim listCommend As New List(Of CommendDTO)

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' Lấy toàn bộ dữ liệu theo employee
            Dim queryEmp = From p In Context.HU_COMMEND
                           From ce In Context.HU_COMMEND_EMP.Where(Function(ce) ce.HU_COMMEND_ID = p.ID)
                           From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ce.HU_EMPLOYEE_ID).DefaultIfEmpty
                           From org In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper).DefaultIfEmpty
                           From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                           From o2 In Context.HU_ORGANIZATION.Where(Function(f) f.ID = ce.ORG_ID).DefaultIfEmpty
                           From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                           From title In Context.OT_OTHER_LIST.Where(Function(x) x.ID = t.LEVEL_ID).DefaultIfEmpty
                           From ct In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.TITLE_ID).DefaultIfEmpty
                           From pay In Context.OT_OTHER_LIST.Where(Function(x) x.ID = ce.COMMEND_PAY).DefaultIfEmpty
                           From lkt In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMEND_LIST).DefaultIfEmpty
                           Join ot In Context.OT_OTHER_LIST On ot.ID Equals p.STATUS_ID()
                           From nc In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_PAY).DefaultIfEmpty
                           From kl In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_ID).DefaultIfEmpty
                           From klt In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_TAX).DefaultIfEmpty
                           From ckt In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_LEVEL).DefaultIfEmpty
                           From htkt In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_TYPE).DefaultIfEmpty
                           From dtkt In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_OBJ).DefaultIfEmpty
                           From form In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty
                           Where (org.USERNAME.ToUpper = log.Username.ToUpper) Or (ce.HU_EMPLOYEE_ID = 0)

            If Not _filter.IS_TERMINATE Then
                queryEmp = queryEmp.Where(Function(p) p.e.WORK_STATUS <> 257 Or (p.e.WORK_STATUS = 257 And p.e.TER_LAST_DATE >= Date.Now) Or p.e.WORK_STATUS Is Nothing)
            End If
            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_ID IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.ce.HU_EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.NO IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.NO.ToUpper.Contains(_filter.NO.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EFFECT_DATE >= _filter.EFFECT_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EFFECT_DATE <= _filter.EXPIRE_DATE)
            End If
            'If _filter.COMMEND_OBJ_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_OBJ.NAME_VN.ToUpper.Contains(_filter.COMMEND_OBJ_NAME))
            'End If

            If _filter.COMMEND_OBJ IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.COMMEND_OBJ = _filter.COMMEND_OBJ)
            End If
            If _filter.MONEY IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.MONEY = _filter.MONEY)
            End If
            If _filter.YEAR IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.YEAR = _filter.YEAR)
            End If
            If _filter.REMARK IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.REMARK.ToUpper.Contains(_filter.REMARK))
            End If
            'If _filter.COMMEND_TYPE_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_TYPE.NAME_VN.Contains(_filter.COMMEND_TYPE_NAME))
            'End If

            If _filter.COMMEND_TYPE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.COMMEND_TYPE = _filter.COMMEND_TYPE)
            End If

            'If _filter.COMMEND_LEVEL_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_LEVEL.NAME_VN.Contains(_filter.COMMEND_LEVEL_NAME))
            'End If
            If _filter.STATUS_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.ot.NAME_VN.Contains(_filter.STATUS_NAME))
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If

            If _filter.NotEmp = 1 Then
                queryEmp = queryEmp.Where(Function(p) p.ce.HU_EMPLOYEE_ID = 0)
            End If

            ' danh sách thông tin khen thưởng nhân viên
            Dim lstEmp = queryEmp.Select(Function(p) New CommendDTO With {
                                                       .ID = p.p.ID,
                                                       .DECISION_NO = p.p.NO,
                                                       .MONEY = p.ce.MONEY,
                                                       .COMMEND_PAY_NAME = p.pay.NAME_VN,
                                                       .COMMEND_OBJ = p.p.COMMEND_OBJ,
                                                       .COMMEND_OBJ_NAME = p.dtkt.NAME_VN,
                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                       .EMPLOYEE_NAME = If(p.ce.HU_EMPLOYEE_ID = 0, p.ce.FULLNAME_TEXT, p.e.FULLNAME_VN),
                                                       .EMPLOYEE_ID = p.e.ID,
                                                       .ORG_NAME = If(p.ce.HU_EMPLOYEE_ID = 0, p.o2.NAME_VN, p.o.NAME_VN),
                                                       .ORG_DESC = If(p.ce.HU_EMPLOYEE_ID = 0, p.o2.DESCRIPTION_PATH, p.o.DESCRIPTION_PATH),
                                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                                        .TITLE_NAME = If(p.ce.HU_EMPLOYEE_ID = 0, p.ce.TITLE_TEXT, p.t.NAME_VN),
                                                       .REMARK = p.p.REMARK,
                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                       .STATUS_CODE = p.ot.CODE,
                                                       .STATUS_ID = p.p.STATUS_ID,
                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                        .COMMEND_TITLE_ID = p.p.TITLE_ID,
                                                       .COMMEND_TITLE_NAME = p.ct.NAME,
                                                        .COMMEND_PAY = p.p.COMMEND_PAY,
                                                       .LEVEL_NAME = p.title.NAME_VN,
                                                       .NO = p.ce.NO,
                                                       .SIGN_DATE = p.p.SIGN_DATE,
                                                       .SIGNER_NAME = p.p.SIGNER_NAME,
                                                       .SIGNER_TITLE = p.p.SIGNER_TITLE,
                                                       .SIGN_ID = p.p.SIGN_ID,
                                                       .DEDUCT_FROM_SALARY = p.p.DEDUCT_FROM_SALARY,
                                                       .COMMEND_LIST_ID = p.p.COMMEND_LIST,
                                                       .COMMEND_LIST_NAME = p.lkt.NAME,
                                                       .ORDER = p.lkt.NUMBER_ORDER,
                                                       .POWER_PAY_ID = p.p.POWER_PAY_ID,
                                                       .POWER_PAY_NAME = p.nc.NAME_VN,
                                                       .UPLOADFILE = p.p.UPLOADFILE,
                                                       .IS_TAX = p.p.IS_TAX,
                                                       .IS_TAX_BOOLEN = If(p.p.IS_TAX = -1, True, False),
                                                       .PERIOD_ID = p.p.PERIOD_ID,
                                                       .PERIOD_TAX = p.p.PERIOD_TAX,
                                                       .PERIOD_TAX_NAME = p.klt.PERIOD_NAME,
                                                       .PERIOD_NAME = p.kl.PERIOD_NAME,
                                                       .COMMEND_TYPE = p.p.COMMEND_TYPE,
                                                       .COMMEND_TYPE_NAME = p.htkt.NAME_VN,
                                                       .COMMEND_LEVEL_NAME = p.ckt.NAME_VN,
                                                       .COMMEND_LEVEL = p.p.COMMEND_LEVEL,
                                                       .FORM_ID = p.p.ID,
                                                       .FORM_NAME = p.form.NAME_VN,
                                                       .YEAR = p.p.YEAR})

            lstEmp = lstEmp.OrderBy(Sorts)
            Total = lstEmp.Count
            lstEmp = lstEmp.Skip(PageIndex * PageSize).Take(PageSize)



            listCommend = lstEmp.ToList()

            If _filter.COMMEND_OBJ Is Nothing OrElse _filter.COMMEND_OBJ = 389 Then
                listCommend.AddRange(GetListCommendOrg(_filter, PageIndex, PageSize, Total, log, Sorts))
            End If

            Return listCommend
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Private Function GetListCommendOrg(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)
        Try

            ' lấy toàn bộ dữ liệu theo Org
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            ' Lấy toàn bộ dữ liệu theo employee
            Dim queryEmp = From p In Context.HU_COMMEND
                           From org In Context.HU_COMMEND_ORG.Where(Function(ce) ce.HU_COMMEND_ID = p.ID)
                           From choose In Context.SE_CHOSEN_ORG.Where(Function(f) org.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                           From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = org.ORG_ID).DefaultIfEmpty
                           From ct In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.TITLE_ID).DefaultIfEmpty
                           From pay In Context.OT_OTHER_LIST.Where(Function(x) x.ID = org.COMMEND_PAY).DefaultIfEmpty
                           From lkt In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMEND_LIST).DefaultIfEmpty
                           Join ot In Context.OT_OTHER_LIST On ot.ID Equals p.STATUS_ID()
                           From nc In Context.PA_PAYMENTSOURCES.Where(Function(x) x.ID = p.POWER_PAY_ID).DefaultIfEmpty
                           From kl In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_ID).DefaultIfEmpty
                           From klt In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_TAX).DefaultIfEmpty
                           From ckt In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.COMMEND_LEVEL).DefaultIfEmpty
                           From htkt In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMEND_TYPE).DefaultIfEmpty
                           From dtkt In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_OBJ).DefaultIfEmpty()
                           From form In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty

            If _filter.ORG_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.NO IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.NO.ToUpper.Contains(_filter.NO.ToUpper))
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.EXPIRE_DATE >= _filter.EFFECT_DATE)
            End If

            'If _filter.COMMEND_OBJ_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_OBJ.NAME_VN.ToUpper.Contains(_filter.COMMEND_OBJ_NAME))
            'End If
            If _filter.MONEY IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.MONEY = _filter.MONEY)
            End If
            If _filter.REMARK IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.REMARK.ToUpper.Contains(_filter.REMARK))
            End If
            'If _filter.COMMEND_TYPE_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_TYPE.NAME_VN.Contains(_filter.COMMEND_TYPE_NAME))
            'End If

            If _filter.COMMEND_TYPE IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.COMMEND_TYPE = _filter.COMMEND_TYPE)
            End If

            If _filter.COMMEND_OBJ IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.COMMEND_OBJ = _filter.COMMEND_OBJ)
            End If

            'If _filter.COMMEND_LEVEL_NAME IsNot Nothing Then
            '    queryEmp = queryEmp.Where(Function(p) p.p.OT_CM_LEVEL.NAME_VN.Contains(_filter.COMMEND_LEVEL_NAME))
            'End If
            If _filter.STATUS_NAME IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.ot.NAME_VN.Contains(_filter.STATUS_NAME))
            End If

            If _filter.STATUS_ID IsNot Nothing Then
                queryEmp = queryEmp.Where(Function(p) p.p.STATUS_ID = _filter.STATUS_ID)
            End If
            ' danh sách thông tin khen thưởng nhân viên
            Dim lstEmp = queryEmp.Select(Function(p) New CommendDTO With {
                                                               .ID = p.p.ID,
                                                               .YEAR = p.p.YEAR,
                                                               .DECISION_NO = p.p.NO,
                                                               .COMMEND_OBJ = p.p.COMMEND_OBJ,
                                                               .COMMEND_OBJ_NAME = p.dtkt.NAME_VN,
                                                               .ORG_NAME = p.o.NAME_VN,
                                                               .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                               .EFFECT_DATE = p.p.EFFECT_DATE,
                                                               .MONEY = p.org.MONEY,
                                                               .REMARK = p.p.REMARK,
                                                               .STATUS_NAME = p.ot.NAME_VN,
                                                               .STATUS_CODE = p.ot.CODE,
                                                               .STATUS_ID = p.p.STATUS_ID,
                                                               .CREATED_DATE = p.p.CREATED_DATE,
                                                                .COMMEND_TITLE_ID = p.p.TITLE_ID,
                                                               .COMMEND_TITLE_NAME = p.ct.NAME,
                                                                .COMMEND_PAY = p.p.COMMEND_PAY,
                                                               .COMMEND_PAY_NAME = p.pay.NAME_VN,
                                                               .NO = p.p.NO,
                                                               .SIGN_DATE = p.p.SIGN_DATE,
                                                               .SIGNER_NAME = p.p.SIGNER_NAME,
                                                               .SIGNER_TITLE = p.p.SIGNER_TITLE,
                                                               .SIGN_ID = p.p.SIGN_ID,
                                                               .DEDUCT_FROM_SALARY = p.p.DEDUCT_FROM_SALARY,
                                                               .COMMEND_LIST_ID = p.p.COMMEND_LIST,
                                                               .COMMEND_LIST_NAME = p.lkt.NAME,
                                                               .POWER_PAY_ID = p.p.POWER_PAY_ID,
                                                               .POWER_PAY_NAME = p.nc.NAME,
                                                               .UPLOADFILE = p.p.UPLOADFILE,
                                                               .IS_TAX = p.p.IS_TAX,
                                                               .IS_TAX_BOOLEN = If(p.p.IS_TAX = -1, True, False),
                                                               .PERIOD_ID = p.p.PERIOD_ID,
                                                               .PERIOD_TAX = p.p.PERIOD_TAX,
                                                               .PERIOD_TAX_NAME = p.klt.PERIOD_NAME,
                                                               .PERIOD_NAME = p.kl.PERIOD_NAME,
                                                               .COMMEND_TYPE = p.p.COMMEND_TYPE,
                                                               .COMMEND_TYPE_NAME = p.htkt.NAME,
                                                               .COMMEND_LEVEL_NAME = p.ckt.NAME,
                                                               .COMMEND_LEVEL = p.p.COMMEND_LEVEL,
                                                               .HU_COMMEND_ORG_ID = p.org.ID,
                                                               .OBJ_ORG_ID = p.org.ORG_ID,
                                                               .OBJ_ORG_NAME = p.o.NAME_VN,
                                                               .FORM_ID = p.p.FORM_ID,
                                                               .FORM_NAME = p.form.NAME_VN})

            lstEmp = lstEmp.OrderBy(Sorts)
            Total += lstEmp.Count
            lstEmp = lstEmp.Skip(PageIndex * PageSize).Take(PageSize)
            Return lstEmp.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeCommendByID(ByVal ComId As Decimal) As List(Of CommendEmpDTO)
        Try
            Dim q = (From d In Context.HU_COMMEND_EMP Where d.HU_COMMEND_ID = ComId
                     From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = d.HU_EMPLOYEE_ID).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                     From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)).DefaultIfEmpty

            Dim lst As New List(Of CommendEmpDTO)
            For Each row In q
                Dim guidId As String = Guid.NewGuid.ToString()
                Dim obj As New CommendEmpDTO
                obj.GUID_ID = guidId
                obj.ID = row.d.ID
                obj.HU_EMPLOYEE_ID = row.d.HU_EMPLOYEE_ID
                obj.HU_COMMEND_ID = row.d.HU_COMMEND_ID
                obj.EMPLOYEE_CODE = row.e.EMPLOYEE_CODE
                obj.FULLNAME = If(row.d.HU_EMPLOYEE_ID = 0, row.d.FULLNAME_TEXT, row.e.FULLNAME_VN)
                obj.ORG_ID = row.d.ORG_ID
                obj.ORG_NAME = If(row.d.HU_EMPLOYEE_ID = 0, row.d.ORG_TEXT, row.o.NAME_VN)
                obj.TITLE_ID = row.d.TITLE_ID
                obj.TITLE_NAME = If(row.d.HU_EMPLOYEE_ID = 0, row.d.TITLE_TEXT, row.t.NAME_VN)
                obj.MONEY = row.d.MONEY
                obj.COMMEND_PAY = row.d.COMMEND_PAY
                obj.FULLNAME_TEXT = row.d.FULLNAME_TEXT
                obj.ORG_TEXT = row.d.ORG_TEXT
                obj.TITLE_TEXT = row.d.TITLE_TEXT
                obj.NO = row.d.NO
                lst.Add(obj)
            Next
            Return lst
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetOrgCommendByID(ByVal ComId As Decimal) As List(Of CommendOrgDTO)
        Try
            Dim q = (From d In Context.HU_COMMEND_ORG
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = d.ORG_ID)
                     Where d.HU_COMMEND_ID = ComId
                     Select New CommendOrgDTO With {.ID = d.ID,
                                                   .HU_COMMEND_ID = d.HU_COMMEND_ID,
                                                    .ORG_ID = d.ORG_ID,
                                                   .ORG_NAME = o.NAME_VN,
                                                    .MONEY = d.MONEY,
                                                    .COMMEND_PAY = d.COMMEND_PAY}).ToList
            Return q
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendByID(ByVal _filter As CommendDTO) As CommendDTO

        Try
            ' lấy toàn bộ dữ liệu theo Org
            Dim query = From p In Context.HU_COMMEND
                        From ce In Context.HU_COMMEND_EMP.Where(Function(ce) ce.HU_COMMEND_ID = p.ID).DefaultIfEmpty
                        From org In Context.HU_COMMEND_ORG.Where(Function(x) x.HU_COMMEND_ID = p.ID).DefaultIfEmpty
                        From org_name In Context.HU_ORGANIZATION.Where(Function(o) o.ID = org.ORG_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ce.HU_EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                        From title In Context.OT_OTHER_LIST.Where(Function(x) x.ID = t.LEVEL_ID).DefaultIfEmpty
                        From ct In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.TITLE_ID).DefaultIfEmpty
                        From pay In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_PAY).DefaultIfEmpty
                        From lkt In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMEND_LIST).DefaultIfEmpty
                        Join ot In Context.OT_OTHER_LIST On ot.ID Equals p.STATUS_ID()
                        From nc In Context.PA_PAYMENTSOURCES.Where(Function(x) x.ID = p.POWER_PAY_ID).DefaultIfEmpty
                        From kl In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_ID).DefaultIfEmpty
                        From klt In Context.AT_PERIOD.Where(Function(x) x.ID = p.PERIOD_TAX).DefaultIfEmpty
                        From ckt In Context.HU_COMMEND_LEVEL.Where(Function(x) x.ID = p.COMMEND_LEVEL).DefaultIfEmpty
                        From htkt In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMEND_TYPE).DefaultIfEmpty
                        From dtkt In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_OBJ).DefaultIfEmpty
                        From form In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.FORM_ID).DefaultIfEmpty
                        Where p.ID = _filter.ID

            ' danh sách thông tin khen thưởng nhân viên
            Dim obj = query.Select(Function(p) New CommendDTO With {
                                                       .ID = p.p.ID,
                                                       .YEAR = p.p.YEAR,
                                                       .DECISION_NO = p.p.NO,
                                                       .COMMEND_OBJ = p.p.COMMEND_OBJ,
                                                       .FILENAME = p.p.FILENAME,
                                                       .COMMEND_OBJ_NAME = p.dtkt.NAME_VN,
                                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                       .EMPLOYEE_ID = p.e.ID,
                                                       .ORG_NAME = p.o.NAME_VN,
                                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                                       .TITLE_NAME = p.t.NAME_VN,
                                                       .MONEY = p.p.MONEY,
                                                       .REMARK = p.p.REMARK,
                                                       .STATUS_NAME = p.ot.NAME_VN,
                                                       .STATUS_CODE = p.ot.CODE,
                                                       .STATUS_ID = p.p.STATUS_ID,
                                                       .CREATED_DATE = p.p.CREATED_DATE,
                                                        .COMMEND_TITLE_ID = p.p.TITLE_ID,
                                                       .COMMEND_TITLE_NAME = p.ct.NAME,
                                                        .COMMEND_PAY = p.p.COMMEND_PAY,
                                                       .LEVEL_NAME = p.title.NAME_VN,
                                                       .COMMEND_PAY_NAME = p.pay.NAME_VN,
                                                       .NO = p.p.NO,
                                                       .SIGN_DATE = p.p.SIGN_DATE,
                                                       .SIGNER_NAME = p.p.SIGNER_NAME,
                                                       .SIGNER_TITLE = p.p.SIGNER_TITLE,
                                                       .SIGN_ID = p.p.SIGN_ID,
                                                       .DEDUCT_FROM_SALARY = p.p.DEDUCT_FROM_SALARY,
                                                       .COMMEND_LIST_ID = p.p.COMMEND_LIST,
                                                       .COMMEND_LIST_NAME = p.lkt.NAME,
                                                       .POWER_PAY_ID = p.p.POWER_PAY_ID,
                                                       .POWER_PAY_NAME = p.nc.NAME,
                                                       .UPLOADFILE = p.p.UPLOADFILE,
                                                       .IS_TAX = p.p.IS_TAX,
                                                       .IS_TAX_BOOLEN = If(p.p.IS_TAX = -1, True, False),
                                                       .PERIOD_ID = p.p.PERIOD_ID,
                                                       .PERIOD_TAX = p.p.PERIOD_TAX,
                                                       .PERIOD_TAX_NAME = p.klt.PERIOD_NAME,
                                                       .PERIOD_NAME = p.kl.PERIOD_NAME,
                                                       .COMMEND_TYPE = p.p.COMMEND_TYPE,
                                                       .COMMEND_TYPE_NAME = p.htkt.NAME,
                                                       .COMMEND_LEVEL_NAME = p.ckt.NAME,
                                                       .COMMEND_LEVEL = p.p.COMMEND_LEVEL,
                                                       .HU_COMMEND_ORG_ID = p.org.ID,
                                                       .OBJ_ORG_ID = p.org.ORG_ID,
                                                       .OBJ_ORG_NAME = p.org_name.NAME_VN,
                                                       .FORM_ID = p.p.FORM_ID,
                                                       .FORM_NAME = p.form.NAME_VN,
                                                       .NOTE = p.p.NOTE})


            Return obj.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertCommend(ByVal objCommend As CommendDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objCommendData As New HU_COMMEND

        Try
            ' thêm kỷ luật
            objCommendData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND.EntitySet.Name)
            objCommend.ID = objCommendData.ID
            objCommendData.COMMEND_LEVEL = objCommend.COMMEND_LEVEL
            objCommendData.COMMEND_OBJ = objCommend.COMMEND_OBJ
            objCommendData.COMMEND_TYPE = objCommend.COMMEND_TYPE
            objCommendData.MONEY = objCommend.MONEY
            objCommendData.FILENAME = objCommend.FILENAME
            objCommendData.STATUS_ID = objCommend.STATUS_ID
            objCommendData.REMARK = objCommend.REMARK
            objCommendData.CREATED_DATE = DateTime.Now
            objCommendData.CREATED_BY = log.Username
            objCommendData.CREATED_LOG = log.ComputerName
            objCommendData.MODIFIED_DATE = DateTime.Now
            objCommendData.MODIFIED_BY = log.Username
            objCommendData.MODIFIED_LOG = log.ComputerName
            objCommendData.DEDUCT_FROM_SALARY = objCommend.DEDUCT_FROM_SALARY
            objCommendData.PERIOD_ID = objCommend.PERIOD_ID
            ' thêm quyết định            
            objCommendData.EFFECT_DATE = objCommend.EFFECT_DATE
            objCommendData.EXPIRE_DATE = objCommend.EXPIRE_DATE
            objCommendData.ISSUE_DATE = objCommend.ISSUE_DATE
            objCommendData.NAME = objCommend.NAME
            objCommendData.NO = objCommend.NO
            objCommendData.SIGN_DATE = objCommend.SIGN_DATE
            objCommendData.SIGNER_NAME = objCommend.SIGNER_NAME
            objCommendData.SIGNER_TITLE = objCommend.SIGNER_TITLE
            objCommendData.SIGN_ID = objCommend.SIGN_ID
            objCommendData.COMMEND_LIST = objCommend.COMMEND_LIST_ID
            objCommendData.TITLE_ID = objCommend.COMMEND_TITLE_ID
            objCommendData.COMMEND_PAY = objCommend.COMMEND_PAY
            objCommendData.POWER_PAY_ID = objCommend.POWER_PAY_ID
            objCommendData.UPLOADFILE = objCommend.UPLOADFILE
            objCommendData.NOTE = objCommend.NOTE
            objCommendData.YEAR = objCommend.YEAR
            objCommendData.IS_TAX = objCommend.IS_TAX
            objCommendData.PERIOD_TAX = objCommend.PERIOD_TAX

            objCommendData.FORM_ID = objCommend.FORM_ID
            Context.HU_COMMEND.AddObject(objCommendData)

            'Dim objD = (From d In Context.HU_COMMEND_EMP Where d.HU_COMMEND_ID = objCommend.ID).ToList
            'For Each obj As HU_COMMEND_EMP In objD
            '    Context.HU_COMMEND_EMP.DeleteObject(obj)
            'Next

            'For Each obj As CommendEmpDTO In objCommend.COMMEND_EMP
            '    Dim objDataEmp = New HU_COMMEND_EMP
            '    objDataEmp.HU_COMMEND_ID = objCommendData.ID
            '    objDataEmp.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID
            '    objDataEmp.MONEY = obj.MONEY
            '    objDataEmp.ORG_ID = obj.ORG_ID
            '    objDataEmp.TITLE_ID = obj.TITLE_ID
            '    Context.HU_COMMEND_EMP.AddObject(objDataEmp)
            'Next

            'insert nhan vien hoac phòng ban
            InsertObjectType(objCommend)

            Context.SaveChanges(log)
            gID = objCommendData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyCommend(ByVal objCommend As CommendDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendData As New HU_COMMEND With {.ID = objCommend.ID}

        Try
            Context.HU_COMMEND.Attach(objCommendData)
            ' sửa kỷ luật
            objCommendData.ID = objCommend.ID
            objCommendData.COMMEND_LEVEL = objCommend.COMMEND_LEVEL
            objCommendData.COMMEND_OBJ = objCommend.COMMEND_OBJ
            objCommendData.COMMEND_TYPE = objCommend.COMMEND_TYPE
            objCommendData.MONEY = objCommend.MONEY
            objCommendData.FILENAME = objCommend.FILENAME
            objCommendData.STATUS_ID = objCommend.STATUS_ID
            objCommendData.REMARK = objCommend.REMARK
            objCommendData.MODIFIED_DATE = DateTime.Now
            objCommendData.MODIFIED_BY = log.Username
            objCommendData.MODIFIED_LOG = log.ComputerName
            objCommendData.DEDUCT_FROM_SALARY = objCommend.DEDUCT_FROM_SALARY
            objCommendData.PERIOD_ID = objCommend.PERIOD_ID
            ' thêm quyết định            
            objCommendData.EFFECT_DATE = objCommend.EFFECT_DATE
            objCommendData.EXPIRE_DATE = objCommend.EXPIRE_DATE
            objCommendData.ISSUE_DATE = objCommend.ISSUE_DATE
            objCommendData.NAME = objCommend.NAME
            objCommendData.NO = objCommend.NO
            objCommendData.SIGN_DATE = objCommend.SIGN_DATE
            objCommendData.SIGNER_NAME = objCommend.SIGNER_NAME
            objCommendData.SIGNER_TITLE = objCommend.SIGNER_TITLE
            objCommendData.SIGN_ID = objCommend.SIGN_ID
            objCommendData.COMMEND_LIST = objCommend.COMMEND_LIST_ID
            objCommendData.TITLE_ID = objCommend.COMMEND_TITLE_ID
            objCommendData.COMMEND_PAY = objCommend.COMMEND_PAY
            objCommendData.POWER_PAY_ID = objCommend.POWER_PAY_ID
            objCommendData.UPLOADFILE = objCommend.UPLOADFILE
            objCommendData.NOTE = objCommend.NOTE
            objCommendData.YEAR = objCommend.YEAR
            objCommendData.IS_TAX = objCommend.IS_TAX
            objCommendData.PERIOD_TAX = objCommend.PERIOD_TAX

            objCommendData.FORM_ID = objCommend.FORM_ID
            InsertObjectType(objCommend)

            Context.SaveChanges(log)
            gID = objCommendData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Private Sub InsertObjectType(ByVal objCommend As CommendDTO)
        Dim objDataEmp As HU_COMMEND_EMP
        Dim objDataOrg As HU_COMMEND_ORG
        Dim rep As New ProfileRepository

        If objCommend.COMMEND_EMP IsNot Nothing Then
            'xoa danh sach nhan viên cũ truoc khi insert lại
            Dim objD = (From d In Context.HU_COMMEND_EMP Where d.HU_COMMEND_ID = objCommend.ID).ToList
            For Each obj As HU_COMMEND_EMP In objD
                Context.HU_COMMEND_EMP.DeleteObject(obj)
            Next
            'Dim emp_code As String
            'Dim org_name As String
            'insert nhan vien mới
            For Each obj As CommendEmpDTO In objCommend.COMMEND_EMP
                objDataEmp = New HU_COMMEND_EMP
                objDataEmp.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_EMP.EntitySet.Name)
                objDataEmp.GUID_ID = obj.GUID_ID
                objDataEmp.HU_COMMEND_ID = objCommend.ID
                objDataEmp.HU_EMPLOYEE_ID = obj.HU_EMPLOYEE_ID
                objDataEmp.MONEY = obj.MONEY
                objDataEmp.COMMEND_PAY = obj.COMMEND_PAY
                objDataEmp.ORG_ID = obj.ORG_ID
                objDataEmp.TITLE_ID = obj.TITLE_ID
                objDataEmp.FULLNAME_TEXT = If(obj.HU_EMPLOYEE_ID = 0, obj.FULLNAME, "")
                objDataEmp.ORG_TEXT = If(obj.HU_EMPLOYEE_ID = 0, obj.ORG_NAME, "")
                objDataEmp.TITLE_TEXT = If(obj.HU_EMPLOYEE_ID = 0, obj.TITLE_NAME, "")
                objDataEmp.NO = obj.NO
                Context.HU_COMMEND_EMP.AddObject(objDataEmp)
            Next
        End If

        If objCommend.LIST_COMMEND_ORG IsNot Nothing Then
            'xoa danh sach phòng ban cũ (tập thể )
            Dim objD = (From d In Context.HU_COMMEND_ORG Where d.HU_COMMEND_ID = objCommend.ID).ToList
            For Each obj As HU_COMMEND_ORG In objD
                Context.HU_COMMEND_ORG.DeleteObject(obj)
            Next

            'insert phòng ban
            For Each obj As CommendOrgDTO In objCommend.LIST_COMMEND_ORG
                objDataOrg = New HU_COMMEND_ORG
                objDataOrg.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_ORG.EntitySet.Name)
                'objDataOrg.ID = rep.AutoGenID("HU_COMMEND_ORG")
                objDataOrg.GUID_ID = obj.GUID_ID
                objDataOrg.HU_COMMEND_ID = objCommend.ID
                objDataOrg.MONEY = obj.MONEY
                objDataOrg.COMMEND_PAY = obj.COMMEND_PAY
                objDataOrg.ORG_ID = obj.ORG_ID
                Context.HU_COMMEND_ORG.AddObject(objDataOrg)
            Next
        End If
    End Sub

    Public Function ValidateCommend(ByVal sType As String, ByVal obj As CommendDTO) As Boolean
        Try
            Select Case sType
                Case "New"
                    If Context.HU_COMMEND.Any(Function(x) x.NO = obj.NO) Then
                        Return False
                    End If
                Case "Edit"
                    If Context.HU_COMMEND.Any(Function(x) x.NO = obj.NO AndAlso x.ID <> obj.ID) Then
                        Return False
                    End If
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function DeleteCommend(ByVal objCommend As CommendDTO) As Boolean
        Dim objCommendData As HU_COMMEND
        Try
            objCommendData = (From p In Context.HU_COMMEND Where objCommend.ID = p.ID).SingleOrDefault
            If objCommendData IsNot Nothing Then
                'xoa danh sach nhan viên ( cá nhân ) 
                Dim lstEmp = (From p In Context.HU_COMMEND_EMP Where p.HU_COMMEND_ID = objCommendData.ID Select p).ToList()
                If lstEmp IsNot Nothing Then
                    For Each emp As HU_COMMEND_EMP In lstEmp
                        Context.HU_COMMEND_EMP.DeleteObject(emp)
                    Next
                End If

                'xoa danh sach phong ban ( tập thể ) 
                Dim lstOrg = (From p In Context.HU_COMMEND_ORG Where p.HU_COMMEND_ID = objCommendData.ID Select p).ToList()
                If lstOrg IsNot Nothing Then
                    For Each org As HU_COMMEND_ORG In lstOrg
                        Context.HU_COMMEND_ORG.DeleteObject(org)
                    Next
                End If
            End If

            Context.HU_COMMEND.DeleteObject(objCommendData)
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ApproveCommend(ByVal objCommend As CommendDTO, Optional ByVal bCheck As Boolean = True) As Boolean
        Dim objCommendData As HU_COMMEND
        Try
            If Format(objCommend.EFFECT_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Return False
            End If
            If bCheck Then
                objCommendData = (From p In Context.HU_COMMEND Where objCommend.ID = p.ID).SingleOrDefault
                objCommendData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID

            End If
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ApproveListCommend(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean
        Dim objCommendData As HU_COMMEND
        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                objCommendData = (From p In Context.HU_COMMEND Where item = p.ID).SingleOrDefault
                objCommendData.STATUS_ID = If(acti = "PD", 714, 715)
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Công thức khen thưởng (Commend_formula)"
    Public Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)
        Try
            Dim query = From p In Context.HU_COMMEND_FORMULA
                        From cl In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMENDLIST_ID).DefaultIfEmpty
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = cl.TYPE_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = cl.OBJECT_ID).DefaultIfEmpty
                        Select New CommendFormulaDTO With {
                                                          .ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME = p.NAME,
                                                          .REMARK = p.REMARK,
                                                          .CREATED_DATE = p.CREATED_DATE,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                          .NUMBER_ORDER = p.NUMBER_ORDER,
                                                            .FORMULA = p.FORMULA,
                                                          .COMMENDLIST_ID = p.COMMENDLIST_ID,
                                                          .COMMENDLIST_NAME = cl.NAME,
                                                          .COMMENDTYPE_NAME = t.NAME_VN,
                                                          .COMMENDOBJECT_NAME = o.NAME_VN}

            Dim lst = query

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO
        Try
            Dim query = From p In Context.HU_COMMEND_FORMULA
                        From cl In Context.HU_COMMEND_LIST.Where(Function(x) x.ID = p.COMMENDLIST_ID)
                        From t In Context.OT_OTHER_LIST.Where(Function(x) x.ID = cl.TYPE_ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(x) x.ID = cl.OBJECT_ID).DefaultIfEmpty
                        Select New CommendFormulaDTO With {
                                                          .ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME = p.NAME,
                                                          .REMARK = p.REMARK,
                                                          .CREATED_DATE = p.CREATED_DATE,
                                                          .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                          .NUMBER_ORDER = p.NUMBER_ORDER,
                                                            .FORMULA = p.FORMULA,
                                                          .COMMENDLIST_ID = p.COMMENDLIST_ID,
                                                          .COMMENDLIST_NAME = cl.NAME,
                                                            .COMMENDTYPE_NAME = t.NAME_VN,
                                                            .COMMENDOBJECT_NAME = o.NAME_VN}

            Return query.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendFormulaData As New HU_COMMEND_FORMULA
        Try
            objCommendFormulaData.ID = Utilities.GetNextSequence(Context, Context.HU_COMMEND_FORMULA.EntitySet.Name)
            objCommendFormulaData.CODE = objCommendFormula.CODE
            objCommendFormulaData.NAME = objCommendFormula.NAME
            objCommendFormulaData.NUMBER_ORDER = objCommendFormula.NUMBER_ORDER
            objCommendFormulaData.REMARK = objCommendFormula.REMARK
            objCommendFormulaData.ACTFLG = objCommendFormula.ACTFLG
            objCommendFormulaData.COMMENDLIST_ID = objCommendFormula.COMMENDLIST_ID
            objCommendFormulaData.FORMULA = objCommendFormula.FORMULA
            Context.HU_COMMEND_FORMULA.AddObject(objCommendFormulaData)
            Context.SaveChanges(log)
            gID = objCommendFormulaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCommendFormulaData As New HU_COMMEND_FORMULA With {.ID = objCommendFormula.ID}
        Try
            Context.HU_COMMEND_FORMULA.Attach(objCommendFormulaData)
            objCommendFormulaData.ID = objCommendFormula.ID
            objCommendFormulaData.CODE = objCommendFormula.CODE
            objCommendFormulaData.NAME = objCommendFormula.NAME
            objCommendFormulaData.NUMBER_ORDER = objCommendFormula.NUMBER_ORDER
            objCommendFormulaData.COMMENDLIST_ID = objCommendFormula.COMMENDLIST_ID
            objCommendFormulaData.FORMULA = objCommendFormula.FORMULA
            objCommendFormulaData.REMARK = objCommendFormula.REMARK
            Context.SaveChanges(log)
            gID = objCommendFormulaData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean
        Dim lstCommendFormulaData As List(Of HU_COMMEND_FORMULA)
        Try
            lstCommendFormulaData = (From p In Context.HU_COMMEND_FORMULA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstCommendFormulaData.Count - 1
                lstCommendFormulaData(index).ACTFLG = sActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        Try
            Dim lstData As List(Of HU_COMMEND_FORMULA)
            lstData = (From p In Context.HU_COMMEND_FORMULA Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.HU_COMMEND_FORMULA.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "Import commend"
    Public Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
        Try
            Dim query = From p In Context.HU_IMPORT_COMMEND
                        From e In Context.HU_EMPLOYEE.Where(Function(x) x.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From ec In Context.HU_EMPLOYEE_CV.Where(Function(x) x.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(x) x.ID = p.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(x) x.ID = p.ORG_ID).DefaultIfEmpty
                        From cl In Context.HU_COMMEND_LIST.Where(Function(x) x.CODE = p.FIELD_DATA_CODE).DefaultIfEmpty
                        From obj In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.COMMEND_OBJ).DefaultIfEmpty


            If _filter.COMMEND_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.COMMEND_DATE = _filter.COMMEND_DATE)
            End If

            If _filter.FIELD_DATA_CODE <> "" Then
                query = query.Where(Function(p) p.p.FIELD_DATA_CODE = _filter.FIELD_DATA_CODE)
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) _
                                        Or p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.COMMEND_OBJ IsNot Nothing Then
                query = query.Where(Function(p) p.p.COMMEND_OBJ = _filter.COMMEND_OBJ)
            End If

            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If

            Dim lstImport = query.Select(Function(p) New ImportCommendDTO With {.ID = p.p.ID,
                                                                              .CALCULATE_DATA = p.p.CALCULATE_DATA,
                                                                              .COMMEND_DATE = p.p.COMMEND_DATE,
                                                                              .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                                              .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                                              .ID_NO = p.ec.ID_NO,
                                                                              .FULLNAME_VN = p.e.FULLNAME_VN,
                                                                              .FIELD_DATA_CODE = p.p.FIELD_DATA_CODE,
                                                                             .FIELD_DATE_NAME = p.cl.NAME,
                                                                              .IMPORT_VALUE = p.p.IMPORT_VALUE,
                                                                              .ORG_ID = p.p.ORG_ID,
                                                                              .TITLE_ID = p.p.TITLE_ID,
                                                                              .ORG_NAME = p.o.NAME_VN,
                                                                              .TITLE_NAME = p.t.NAME_VN,
                                                                                .COMMEND_OBJ = p.p.COMMEND_OBJ})
            Return lstImport.ToList()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean
        Dim objCommendData As HU_IMPORT_COMMEND
        Try
            If Context.HU_IMPORT_COMMEND.Count > 0 Then
                'xoa danh sach import cũ dựa vào ngày xét thưởng
                If Not DeleteImportCommend(lstImport(0).COMMEND_DATE, lstImport(0).COMMEND_OBJ) Then
                    Return False
                End If
            End If

            For Each import As ImportCommendDTO In lstImport
                objCommendData = New HU_IMPORT_COMMEND()
                objCommendData.ID = Utilities.GetNextSequence(Context, Context.HU_IMPORT_COMMEND.EntitySet.Name)
                objCommendData.EMPLOYEE_ID = import.EMPLOYEE_ID
                objCommendData.IMPORT_VALUE = import.IMPORT_VALUE
                objCommendData.FIELD_DATA_CODE = import.FIELD_DATA_CODE
                objCommendData.ORG_ID = import.ORG_ID
                objCommendData.TITLE_ID = import.TITLE_ID
                objCommendData.COMMEND_DATE = import.COMMEND_DATE
                objCommendData.NOTE = import.NOTE
                objCommendData.CALCULATE_DATA = import.CALCULATE_DATA
                objCommendData.COMMEND_OBJ = import.COMMEND_OBJ
                Context.HU_IMPORT_COMMEND.AddObject(objCommendData)
            Next
            Context.SaveChanges(log)

            ' THÀNH NỜ TÊ ADDED 07/09/2016 -> SYNC DATA FROM HU_IMPORT_COMMEND INTO HU_COMMEND_CALCULATED
            Dim rep As New DataAccess.QueryData
            Dim objD As Object = rep.ExecuteStore("PKG_HU_COMMEND_CALCULATE.SYNC_DATA_IMPORTED", New With {.P_DATE = lstImport(0).COMMEND_DATE, .P_OBJ_ID = lstImport(0).COMMEND_OBJ})

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function DeleteImportCommend(ByVal commendDate As Date, ByVal obj As Decimal) As Boolean
        Try
            Dim lstCommend = Context.HU_IMPORT_COMMEND.ToList().FindAll(Function(x) x.COMMEND_DATE.Value.Date = commendDate.Date AndAlso x.COMMEND_OBJ = obj)
            If lstCommend IsNot Nothing AndAlso lstCommend.Count > 0 Then
                For Each import As HU_IMPORT_COMMEND In lstCommend
                    Context.HU_IMPORT_COMMEND.DeleteObject(import)
                Next
                Context.SaveChanges()
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EXPORT_QLKT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.EXPORT_QLKT",
                                           New With {.P_CUR = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GET_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        Try
            Dim dtData As DataTable
            Using cls As New DataAccess.QueryData
                Dim dsData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_EMPLOYEE",
                                           New With {.P_EMP_CODE = P_EMP_CODE,
                                                     .P_CUR = cls.OUT_CURSOR}, False) ' FALSE : no datatable
                If dsData IsNot Nothing Then
                    dtData = dsData.Tables(0)
                End If
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region
End Class
